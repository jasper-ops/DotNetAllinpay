using Jasper.Allinpay.Core.Abstractions;
using Jasper.Allinpay.Core.Configs;
using Jasper.Allinpay.Core.Implementations.Clients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Jasper.Allinpay.AspNetCore;

public static class AllinpayServiceCollectionExtensions {
    private static void ProvideAllinpayClient(this IServiceCollection services) {
        services.AddSingleton<IAllinpayClient>(sp => {
            var options = sp.GetRequiredService<IOptions<AllinpayOptions>>();

            return new AllinpayClient(
                options: options.Value
            );
        });
    }

    public static IServiceCollection AddAllinpayClient(this IServiceCollection services, string? sectionName = null) {
        services.AddOptions<AllinpayOptions>()
            .Configure<IConfiguration>((options, config) => {
                sectionName ??= nameof(AllinpayOptions).Replace("Options", "");
                config.GetSection(sectionName).Bind(options);
            });

        services.ProvideAllinpayClient();

        return services;
    }

    public static IServiceCollection AddAllinpayClient(this IServiceCollection services, Action<AllinpayOptions> configure) {
        services.Configure(configure);

        services.ProvideAllinpayClient();

        return services;
    }
}