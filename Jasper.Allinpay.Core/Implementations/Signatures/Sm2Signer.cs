using System;
using System.Text;
using Jasper.Allinpay.Core.Abstractions;
using Jasper.Allinpay.Core.Utils;
using Org.BouncyCastle.Crypto.Signers;

namespace Jasper.Allinpay.Core.Implementations.Signatures;

public class Sm2Signer : ISigner {
    public string Sign(string content, string privateKeyStr) {
        if (string.IsNullOrEmpty(content)) throw new ArgumentNullException(nameof(content));
        if (string.IsNullOrEmpty(privateKeyStr)) throw new ArgumentNullException(nameof(privateKeyStr));

        var key = KeyParser.ParseSm2PrivateKey(privateKeyStr); // 你需要在 KeyParser 中实现 SM2 私钥解析
        var signer = new SM2Signer();
        signer.Init(true, key);

        var data = Encoding.UTF8.GetBytes(content);
        signer.BlockUpdate(data, 0, data.Length);
        var signature = signer.GenerateSignature();

        return Convert.ToBase64String(signature);
    }

    public bool Verify(string content, string signature, string publicKeyStr) {
        if (string.IsNullOrEmpty(content)) throw new ArgumentNullException(nameof(content));
        if (string.IsNullOrEmpty(signature)) throw new ArgumentNullException(nameof(signature));
        if (string.IsNullOrEmpty(publicKeyStr)) throw new ArgumentNullException(nameof(publicKeyStr));

        var key = KeyParser.ParseSm2PublicKey(publicKeyStr); // KeyParser 实现 SM2 公钥解析
        var signer = new SM2Signer();
        signer.Init(false, key);

        var data = Encoding.UTF8.GetBytes(content);
        signer.BlockUpdate(data, 0, data.Length);

        var signatureBytes = Convert.FromBase64String(signature);
        return signer.VerifySignature(signatureBytes);
    }
}