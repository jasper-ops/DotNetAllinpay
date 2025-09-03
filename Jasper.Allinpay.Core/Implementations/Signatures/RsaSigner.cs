using System;
using System.Text;
using Jasper.Allinpay.Core.Abstractions;
using Jasper.Allinpay.Core.Utils;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Signers;

namespace Jasper.Allinpay.Core.Implementations.Signatures;

public class RsaSigner : ISigner {

    public string Sign(string content, string privateKeyStr) {
        if (string.IsNullOrEmpty(content)) throw new ArgumentNullException(nameof(content));
        if (string.IsNullOrEmpty(privateKeyStr)) throw new ArgumentNullException(nameof(privateKeyStr));

        var key = KeyParser.ParseRsaPrivateKey(privateKeyStr);
        var signer = new RsaDigestSigner(new Sha1Digest());
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

        var key = KeyParser.ParseRsaPublicKey(publicKeyStr);
        var signer = new RsaDigestSigner(new Sha1Digest());
        signer.Init(false, key);

        var data = Encoding.UTF8.GetBytes(content);
        signer.BlockUpdate(data, 0, data.Length);

        var signatureBytes = Convert.FromBase64String(signature);
        return signer.VerifySignature(signatureBytes);
    }
}