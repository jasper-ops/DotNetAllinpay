using System;
using System.Threading;
using System.Threading.Tasks;
using Jasper.Allinpay.Core.Implementations.Payments;
using Jasper.Allinpay.Core.Utils;

namespace Jasper.Allinpay.Core.Abstractions;

public static class UnifiedPayment {
    public static Task<IAllinpayResponse<UnifiedPaymentResponse>> ExecuteUnifiedPaymentAsync(this IAllinpayClient client, Action<UnifiedPaymentRequest> cb, CancellationToken cancellationToken = default) {
        var request = new UnifiedPaymentRequest(
            cusId: client.Options.MerchantId,
            appid: client.Options.AppId,
            randomStr: RandomUtils.RandomString(8),
            payType: "",
            signType: client.Options.SignType
        );

        cb(request);

        return client.ExecuteAsync<UnifiedPaymentResponse>(request, cancellationToken);
    }
}