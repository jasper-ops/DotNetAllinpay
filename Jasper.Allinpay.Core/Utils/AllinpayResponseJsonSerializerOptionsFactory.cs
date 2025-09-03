using System.Text.Json;
using Jasper.Allinpay.Core.Utils.JsonConverters;

namespace Jasper.Allinpay.Core.Utils;

/// <summary>
/// 利用泛型缓存JsonSerializerOptions的工厂
/// </summary>
/// <typeparam name="TData"></typeparam>
static class AllinpayResponseJsonSerializerOptionsFactory<TData> where TData : class, new() {

    private static readonly JsonSerializerOptions JsonSerializerOptions = new();

    static AllinpayResponseJsonSerializerOptionsFactory() {
        JsonSerializerOptions.Converters.Add(new AllinpayResponseJsonConverter<TData>());
    }

    public static JsonSerializerOptions GetJsonSerializerOptions() => JsonSerializerOptions;
}