using System;
using System.Text;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Jasper.Allinpay.Core.Utils;

static class KeyParser {
    /// <summary>
    /// 解析 RSA 私钥，自动兼容 PKCS#1 / PKCS#8 + PEM / 纯 Base64
    /// </summary>
    public static AsymmetricKeyParameter ParseRsaPrivateKey(string privateKeyStr) {
        var keyBytes = Convert.FromBase64String(StripPemHeaders(privateKeyStr));

        try {
            // 尝试 PKCS#8
            return PrivateKeyFactory.CreateKey(keyBytes);
        } catch {
            // PKCS#1
            var rsa = RsaPrivateKeyStructure.GetInstance(keyBytes);
            return new RsaPrivateCrtKeyParameters(
                rsa.Modulus,
                rsa.PublicExponent,
                rsa.PrivateExponent,
                rsa.Prime1,
                rsa.Prime2,
                rsa.Exponent1,
                rsa.Exponent2,
                rsa.Coefficient
            );
        }
    }

    /// <summary>
    /// 解析 RSA 公钥，自动兼容 X.509 / PKCS#1 + PEM / 纯 Base64
    /// </summary>
    public static AsymmetricKeyParameter ParseRsaPublicKey(string publicKeyStr) {
        var keyBytes = Convert.FromBase64String(StripPemHeaders(publicKeyStr));

        try {
            // 尝试 X.509 / PKCS#8
            return PublicKeyFactory.CreateKey(keyBytes);
        } catch {
            // PKCS#1 公钥（没有 PEM 头的纯 DER）
            var seq = (Asn1Sequence)Asn1Object.FromByteArray(keyBytes);
            var rsaStruct = RsaPublicKeyStructure.GetInstance(seq);
            return new RsaKeyParameters(false, rsaStruct.Modulus, rsaStruct.PublicExponent);
        }
    }

    /// <summary>
    /// 去掉 PEM 头尾，如果没有 PEM 头则直接返回
    /// </summary>
    private static string StripPemHeaders(string key) {
        if (!key.Contains("-----BEGIN")) return key.Trim();

        var lines = key.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);
        var sb = new StringBuilder();
        foreach (var line in lines) {
            if (line.StartsWith("-----")) continue;
            sb.Append(line);
        }

        return sb.ToString();

    }

    public static AsymmetricKeyParameter ParseSm2PrivateKey(string privateKeyStr) {
        var base64 = StripPemHeaders(privateKeyStr);
        var keyBytes = Convert.FromBase64String(base64);
        // 使用 SM2PrivateKeyParameters 解析
        return PrivateKeyFactory.CreateKey(keyBytes);
    }

    public static AsymmetricKeyParameter ParseSm2PublicKey(string publicKeyStr) {
        var base64 = StripPemHeaders(publicKeyStr);
        var keyBytes = Convert.FromBase64String(base64);
        // 使用 SM2PublicKeyParameters 解析
        return PublicKeyFactory.CreateKey(keyBytes);
    }
}