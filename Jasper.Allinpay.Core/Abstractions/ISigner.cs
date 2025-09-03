namespace Jasper.Allinpay.Core.Abstractions;

public interface ISigner {
    string Sign(string content, string privateString);

    bool Verify(string content, string signature, string publicString);
}