namespace Jasper.Allinpay.Core.Configs;

/// <summary>
/// 通联支付配置选项（存储商户信息、接口地址等核心配置）
/// </summary>
public class AllinpayOptions {
    public string AppId { get; set; } = string.Empty;
    /// <summary>
    /// 商户号（通联分配，必填）
    /// </summary>
    public string MerchantId { get; set; } = string.Empty;

    /// <summary>
    /// 商户密钥（用于签名/验签，通联分配，需保密，必填）
    /// </summary>
    public string MerchantKey { get; set; } = string.Empty;

    /// <summary>
    /// 签名方式，支持: rsa、sm2
    /// </summary>
    public string SignType { get; set; } = "rsa";
    
    public string  ApiBaseUrl { get; set; } = "https://vsp.allinpay.com";

    /// <summary>
    /// HTTP请求超时时间（单位：秒，默认30秒）
    /// </summary>
    public int TimeoutSeconds { get; set; } = 30;

    public string AllinpayPublicKey { get; set; } = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCm9OV6zH5DYH/ZnAVYHscEELdCNfNTHGuBv1nYYEY9FrOzE0/4kLl9f7Y9dkWHlc2ocDwbrFSm0Vqz0q2rJPxXUYBCQl5yW3jzuKSXif7q1yOwkFVtJXvuhf5WRy+1X5FOFoMvS7538No0RpnLzmNi3ktmiqmhpcY/1pmt20FHQQIDAQAB";
}