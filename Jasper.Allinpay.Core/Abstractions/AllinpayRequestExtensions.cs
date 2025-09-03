using System.Collections.Generic;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Jasper.Allinpay.Core.Abstractions;

public static class AllinpayRequestExtensions {
    public static IDictionary<string, string> GetParameters(this IAllinpayRequest request) {
        
        var dict = new Dictionary<string, string>();
        var props = request.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var prop in props) {
            var value = prop.GetValue(request);
            if (value == null) continue;

            var jsonAttr = prop.GetCustomAttribute<JsonPropertyNameAttribute>();
            var key = jsonAttr != null ? jsonAttr.Name : prop.Name;

            dict[key] = value.ToString() ?? "";
        }

        return dict;
    }
}