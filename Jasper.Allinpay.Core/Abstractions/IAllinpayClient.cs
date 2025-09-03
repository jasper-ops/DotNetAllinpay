using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Jasper.Allinpay.Core.Configs;

namespace Jasper.Allinpay.Core.Abstractions;

public interface IAllinpayClient {
    AllinpayOptions Options { get; }

    /// <summary>
    /// 异步执行API请求，返回响应结果（自动处理签名、验签、日志）
    /// </summary>
    /// <typeparam name="TResponse">响应类型</typeparam>
    /// <typeparam name="TData"></typeparam>
    /// <param name="request">请求对象</param>
    /// <param name="cancellationToken">取消令牌（可选）</param>
    /// <returns>响应结果（已验签）</returns>
    Task<IAllinpayResponse<TData>> ExecuteAsync<TData>(IAllinpayRequest request, CancellationToken cancellationToken = default)
        where TData : class, new();

    bool CheckSign(IDictionary<string, string> parameters);
}