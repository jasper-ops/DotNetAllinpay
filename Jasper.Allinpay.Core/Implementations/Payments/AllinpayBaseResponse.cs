using System;
using System.Text.Json.Serialization;
using Jasper.Allinpay.Core.Abstractions;

namespace Jasper.Allinpay.Core.Implementations.Payments;

public class AllinpayBaseResponse<TData> : IAllinpayResponse<TData> {
    [JsonPropertyName("retcode")]
    public string RetCode { get; set; }

    [JsonPropertyName("retmsg")]
    public string? RetMsg { get; set; }

    [JsonPropertyName("sign")]
    public string? Sign { get; set; }

    public TData? Data { get; set; }

    public bool IsSuccess() => "SUCCESS".Equals(RetCode, StringComparison.OrdinalIgnoreCase);
}