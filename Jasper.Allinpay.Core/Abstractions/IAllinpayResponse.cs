namespace Jasper.Allinpay.Core.Abstractions;

public interface IAllinpayResponse<TData> {
    /// <summary>
    /// 接口返回码（0000=成功，其他为失败，参考通联文档）
    /// </summary>
    string RetCode { get; set; }

    /// <summary>
    /// 返回信息（成功为"success"，失败为具体错误描述）
    /// </summary>
    string? RetMsg { get; set; }

    string? Sign { get; }

    /// <summary>
    /// 业务数据
    /// </summary>
    TData? Data { get; set; }

    bool IsSuccess();
}