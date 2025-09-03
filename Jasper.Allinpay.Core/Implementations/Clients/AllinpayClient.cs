using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Jasper.Allinpay.Core.Abstractions;
using Jasper.Allinpay.Core.Configs;
using Jasper.Allinpay.Core.Utils;
using Microsoft.Extensions.Logging;

namespace Jasper.Allinpay.Core.Implementations.Clients;

public class AllinpayClient(AllinpayOptions options) : IAllinpayClient {

    public AllinpayOptions Options => options;
    private readonly ISigner _signer = SignatureHelper.GetSigner(options.SignType);

    public async Task<IAllinpayResponse<TData>> ExecuteAsync<TData>(IAllinpayRequest request, CancellationToken cancellationToken = default)
        where TData : class, new() {
        // 生成请求参数并签名
        var parameters = request.GetParameters();

        var plainText = SignatureHelper.BuildSignContent(parameters);

        var sign = _signer.Sign(plainText, options.MerchantKey);
        parameters["sign"] = sign;

        var responseText = await (options.ApiBaseUrl + request.GetApiPath())
            .PostUrlEncodedAsync(parameters, cancellationToken: cancellationToken)
            .ReceiveString();


        var jsonSerializerOptions = AllinpayResponseJsonSerializerOptionsFactory<TData>.GetJsonSerializerOptions();

        var response = JsonSerializer.Deserialize<IAllinpayResponse<TData>>(responseText, jsonSerializerOptions);
        if (response is null) {
            throw new Exception("响应异常");
        }

        // 没有sign可能是业务出错了
        if (string.IsNullOrWhiteSpace(response.Sign)) return response;

        var signContent = SignatureHelper.BuildSignContentForResponse(response);

        if (_signer.Verify(signContent, response.Sign, options.AllinpayPublicKey)) {
            return response;
        }

        throw new InvalidOperationException("验签失败");
    }

    public bool CheckSign(IDictionary<string, string> parameters) {
        var signContent = SignatureHelper.BuildSignContent(parameters);

        if (!parameters.TryGetValue("sign", out var signature)) {
            throw new Exception("参数中未包含签名信息");
        }

        return _signer.Verify(signContent, signature, options.AllinpayPublicKey);
    }
}