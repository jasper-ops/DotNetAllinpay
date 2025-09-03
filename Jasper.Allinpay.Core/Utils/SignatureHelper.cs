using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using Jasper.Allinpay.Core.Abstractions;
using Jasper.Allinpay.Core.Implementations.Signatures;

namespace Jasper.Allinpay.Core.Utils;

static class SignatureHelper {
    public static readonly Dictionary<string, ISigner> Signers = new() {
        {"RSA", new RsaSigner()},
        {"SM2", new Sm2Signer()},
    };

    public static ISigner GetSigner(string signType) {
        if (Signers.TryGetValue(signType.ToUpper(), out var signature)) {
            return signature;
        }

        throw new InvalidOperationException("不支持的签名类型");
    }

    // 构建待签名字符串（排除 sign 字段，按 key 升序）
    public static string BuildSignContent(IDictionary<string, string> parameters) {
        var sorted = parameters
            .Where(kv => kv.Value != null && kv.Key.ToLower() != "sign")
            .OrderBy(kv => kv.Key)
            .Select(kv => $"{kv.Key}={kv.Value}");

        return string.Join("&", sorted);
    }


    // 专用重载，接受 IAllinpayResponse<TData>
    public static string BuildSignContentForResponse<TData>(IAllinpayResponse<TData> response) where TData : class {
        if (response == null) return string.Empty;

        var keyValuePairs = new List<(string Key, string Value)>();

        // 添加公共字段
        AddProperty(response);

        // 按 JSON 名字排序
        var list = keyValuePairs.OrderBy(x => x.Key)
            .Select(x => $"{x.Key}={x.Value}");

        return string.Join("&", list);
        
        void AddProperty<T>(T obj) {
            foreach (var prop in obj.GetType().GetProperties()) {
                if (string.Equals(prop.Name, "Sign", StringComparison.OrdinalIgnoreCase)) continue;

                var value = prop.GetValue(obj);
                if (value is null or string {Length: < 1}) continue;

                if (typeof(TData).IsAssignableFrom(prop.PropertyType)) {
                    // 如果是业务对象，递归添加
                    AddProperty((TData)value);
                } else {
                    var name = GetJsonPropertyName(prop);
                    keyValuePairs.Add((name, value.ToString()!));
                }
            }
        }
    }

    // public static string BuildSignContent<T>(T data) {
    //     if (data == null) return string.Empty;
    //
    //     // 获取所有属性，排除 Sign，值为 null 的也排除
    //     var props = typeof(T).GetProperties()
    //         .Where(p => !string.Equals(p.Name, "Sign", StringComparison.OrdinalIgnoreCase)
    //                     && p.GetValue(data) != null)
    //         .OrderBy(GetJsonPropertyName); // 按 JSON 名字排序
    //
    //     var list = props.Select(p => {
    //         var name = GetJsonPropertyName(p);
    //         var value = p.GetValue(data)!;
    //         return $"{name}={value}";
    //     });
    //
    //     return string.Join("&", list);
    // }

    private static string GetJsonPropertyName(PropertyInfo property) {
        var attr = property.GetCustomAttribute<JsonPropertyNameAttribute>();
        return attr != null ? attr.Name : property.Name;
    }

}