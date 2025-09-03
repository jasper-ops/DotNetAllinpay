using System;
using System.Text.Json.Serialization;
using Jasper.Allinpay.Core.Abstractions;

namespace Jasper.Allinpay.Core.Implementations.Payments;

public class UnifiedPaymentRequest : IAllinpayRequest {
    public UnifiedPaymentRequest(string cusId, string appid, string payType, string randomStr, string signType) {
        CusId = cusId;
        Appid = appid;
        PayType = payType;
        RandomStr = randomStr;
        SignType = signType.ToUpper();
    }

    public UnifiedPaymentRequest(string? orgId, string cusId, string appid, string? version, int trxAmt, string? reqSn, string? unireqSn, string payType, string randomStr, string? body, string? remark, int? validTime, string? expireTime, string? acct, string? notifyUrl, string? limitPay, string? subAppid, string? goodsTag, string? benefitDetail, string? chnlStoreId, string? subBranch, string? extendParams, string? cusIp, string? frontUrl, string? idNo, string? trueName, string? asInfo, string? fqNum, string signType, string? unPid, string? finOrg, string? operatorId) {
        OrgId = orgId;
        CusId = cusId ?? throw new ArgumentNullException(nameof(cusId));
        Appid = appid ?? throw new ArgumentNullException(nameof(appid));
        Version = version;
        TrxAmt = trxAmt;
        ReqSn = reqSn;
        UnireqSn = unireqSn;
        PayType = payType ?? throw new ArgumentNullException(nameof(payType));
        RandomStr = randomStr ?? throw new ArgumentNullException(nameof(randomStr));
        Body = body;
        Remark = remark;
        ValidTime = validTime;
        ExpireTime = expireTime;
        Acct = acct;
        NotifyUrl = notifyUrl;
        LimitPay = limitPay;
        SubAppid = subAppid;
        GoodsTag = goodsTag;
        BenefitDetail = benefitDetail;
        ChnlStoreId = chnlStoreId;
        SubBranch = subBranch;
        ExtendParams = extendParams;
        CusIp = cusIp;
        FrontUrl = frontUrl;
        IdNo = idNo;
        TrueName = trueName;
        AsInfo = asInfo;
        FqNum = fqNum;
        SignType = signType ?? throw new ArgumentNullException(nameof(signType));
        UnPid = unPid;
        FinOrg = finOrg;
        OperatorId = operatorId;
    }

    /// <summary>
    /// 集团/代理商商户号
    /// </summary>
    [JsonPropertyName("orgid")]
    public string? OrgId { get; set; }

    /// <summary>
    /// 实际交易的商户号
    /// </summary>
    [JsonPropertyName("cusid")]
    public string CusId { get; set; }

    /// <summary>
    /// 应用ID
    /// </summary>
    [JsonPropertyName("appid")]
    public string Appid { get; set; }

    /// <summary>
    /// 接口版本号
    /// </summary>
    [JsonPropertyName("version")]
    public string? Version { get; set; } = "11";

    /// <summary>
    /// 交易金额，单位：分
    /// </summary>
    [JsonPropertyName("trxamt")]
    public int TrxAmt { get; set; }

    /// <summary>
    /// 商户的交易订单号，与UnireqSn二选一
    /// </summary>
    [JsonPropertyName("reqsn")]
    public string? ReqSn { get; set; }

    /// <summary>
    /// 商户的交易订单号，与ReqSn二选一
    /// </summary>
    [JsonPropertyName("unireqsn")]
    public string? UnireqSn { get; set; }

    /// <summary>
    /// 交易方式
    /// </summary>
    [JsonPropertyName("paytype")]
    public string PayType { get; set; }

    /// <summary>
    /// 商户自行生成的随机字符串
    /// </summary>
    [JsonPropertyName("randomstr")]
    public string RandomStr { get; set; }


    /// <summary>
    /// 订单标题
    /// </summary>
    [JsonPropertyName("body")]
    public string? Body { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [JsonPropertyName("remark")]
    public string? Remark { get; set; }

    /// <summary>
    /// 有效时间，单位：分钟
    /// </summary>
    [JsonPropertyName("validtime")]
    public int? ValidTime { get; set; }

    /// <summary>
    /// 绝对时间，格式：yyyyMMddHHmmss
    /// </summary>
    [JsonPropertyName("expiretime")]
    public string? ExpireTime { get; set; }

    /// <summary>
    /// 支付平台用户标识
    /// </summary>
    [JsonPropertyName("acct")]
    public string? Acct { get; set; }

    /// <summary>
    /// 交易结果通知地址
    /// </summary>
    [JsonPropertyName("notify_url")]
    public string? NotifyUrl { get; set; }

    /// <summary>
    /// 支付限制
    /// </summary>
    [JsonPropertyName("limit_pay")]
    public string? LimitPay { get; set; }

    /// <summary>
    /// 微信子appid
    /// </summary>
    [JsonPropertyName("sub_appid")]
    public string? SubAppid { get; set; }

    /// <summary>
    /// 订单优惠标识
    /// </summary>
    [JsonPropertyName("goods_tag")]
    public string? GoodsTag { get; set; }

    /// <summary>
    /// 优惠信息（JSON字符串）
    /// </summary>
    [JsonPropertyName("benefitdetail")]
    public string? BenefitDetail { get; set; }

    /// <summary>
    /// 渠道门店编号
    /// </summary>
    [JsonPropertyName("chnlstoreid")]
    public string? ChnlStoreId { get; set; }

    /// <summary>
    /// 门店号
    /// </summary>
    [JsonPropertyName("subbranch")]
    public string? SubBranch { get; set; }

    /// <summary>
    /// 拓展参数（JSON字符串）
    /// </summary>
    [JsonPropertyName("extendparams")]
    public string? ExtendParams { get; set; }

    /// <summary>
    /// 终端IP
    /// </summary>
    [JsonPropertyName("cusip")]
    public string? CusIp { get; set; }

    /// <summary>
    /// 支付完成跳转地址
    /// </summary>
    [JsonPropertyName("front_url")]
    public string? FrontUrl { get; set; }

    /// <summary>
    /// 证件号
    /// </summary>
    [JsonPropertyName("idno")]
    public string? IdNo { get; set; }

    /// <summary>
    /// 付款人真实姓名
    /// </summary>
    [JsonPropertyName("truename")]
    public string? TrueName { get; set; }

    /// <summary>
    /// 分账信息
    /// </summary>
    [JsonPropertyName("asinfo")]
    public string? AsInfo { get; set; }

    /// <summary>
    /// 分期
    /// </summary>
    [JsonPropertyName("fqnum")]
    public string? FqNum { get; set; }

    /// <summary>
    /// 签名方式
    /// </summary>
    [JsonPropertyName("signtype")]
    public string SignType { get; set; }

    /// <summary>
    /// 银联PID
    /// </summary>
    [JsonPropertyName("unpid")]
    public string? UnPid { get; set; }

    /// <summary>
    /// 金融机构号
    /// </summary>
    [JsonPropertyName("finorg")]
    public string? FinOrg { get; set; }

    /// <summary>
    /// 收银员号
    /// </summary>
    [JsonPropertyName("operatorid")]
    public string? OperatorId { get; set; }

    public string GetApiPath() => "/apiweb/unitorder/pay";
}