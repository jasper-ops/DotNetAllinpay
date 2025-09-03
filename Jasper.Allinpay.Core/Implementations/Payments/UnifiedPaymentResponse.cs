using System.Text.Json.Serialization;

namespace Jasper.Allinpay.Core.Implementations.Payments;

/// <summary>
/// 统一支付接口返回的业务字段（retcode=SUCCESS时才会出现）
/// </summary>
public class UnifiedPaymentResponse {
    [JsonPropertyName("cusid")]
    public string CusId { get; set; }

    [JsonPropertyName("appid")]
    public string Appid { get; set; }

    [JsonPropertyName("trxid")]
    public string Trxid { get; set; }

    [JsonPropertyName("chnltrxid")]
    public string? ChnltrxId { get; set; }

    [JsonPropertyName("reqsn")]
    public string Reqsn { get; set; }

    [JsonPropertyName("randomstr")]
    public string RandomStr { get; set; }

    [JsonPropertyName("trxstatus")]
    public string TrxStatus { get; set; }

    [JsonPropertyName("fintime")]
    public string? FinTime { get; set; }

    [JsonPropertyName("errmsg")]
    public string? ErrMsg { get; set; }

    [JsonPropertyName("payinfo")]
    public string? PayInfo { get; set; }

    [JsonPropertyName("trxcode")]
    public string? TrxCode { get; set; }
}