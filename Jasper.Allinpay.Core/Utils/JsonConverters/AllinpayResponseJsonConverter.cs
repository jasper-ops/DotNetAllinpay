using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Jasper.Allinpay.Core.Abstractions;
using Jasper.Allinpay.Core.Implementations.Payments;

namespace Jasper.Allinpay.Core.Utils.JsonConverters;

class AllinpayResponseJsonConverter<T> : JsonConverter<IAllinpayResponse<T>> where T : new() {

    public override IAllinpayResponse<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
        using var doc = JsonDocument.ParseValue(ref reader);

        var root = doc.RootElement;
        var response = new AllinpayBaseResponse<T> {
            RetCode = root.GetProperty("retcode").GetString() ?? "",
            RetMsg = root.TryGetProperty("retmsg", out var msg) ? msg.GetString() : null,
            Sign = root.TryGetProperty("sign", out var sign) ? sign.GetString() : null,
        };

        if (!response.RetCode.Equals("SUCCESS", StringComparison.OrdinalIgnoreCase)) return response;

        // 反序列化成 T，但要排除公共字段
        var dict = new Dictionary<string, JsonElement>();

        foreach (var prop in root.EnumerateObject()) {
            if (prop.NameEquals("retcode") || prop.NameEquals("retmsg") || prop.NameEquals("sign")) continue;

            dict[prop.Name] = prop.Value;
        }

        var json = JsonSerializer.Serialize(dict);
        response.Data = JsonSerializer.Deserialize<T>(json, options);

        return response;
    }

    public override void Write(Utf8JsonWriter writer, IAllinpayResponse<T> value, JsonSerializerOptions options) {
        writer.WriteStartObject();
        writer.WriteString("retcode", value.RetCode);
        writer.WriteString("retmsg", value.RetMsg);
        writer.WriteString("sign", value.Sign);

        if (value.Data != null) {
            var dict = JsonSerializer.SerializeToElement(value.Data, options);
            foreach (var prop in dict.EnumerateObject()) {
                writer.WritePropertyName(prop.Name);
                prop.Value.WriteTo(writer);
            }
        }

        writer.WriteEndObject();
    }
}