using System.Security.Cryptography;

namespace Jasper.Allinpay.Core.Utils;

static class RandomUtils {
    private static readonly char[] Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();

    public static string RandomString(int length) {
        if (length <= 0) return string.Empty;

        var stringChars = new char[length];
        using var rng = RandomNumberGenerator.Create();
        var bytes = new byte[length];

        rng.GetBytes(bytes); // 生成随机字节

        for (var i = 0; i < length; i++) {
            // 对字节取模，映射到字符数组索引
            stringChars[i] = Chars[bytes[i] % Chars.Length];
        }

        return new string(stringChars);
    }
}