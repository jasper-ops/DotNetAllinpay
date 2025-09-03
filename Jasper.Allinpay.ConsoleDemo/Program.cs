using Jasper.Allinpay.Core.Abstractions;
using Jasper.Allinpay.Core.Configs;
using Jasper.Allinpay.Core.Implementations.Clients;
using Microsoft.Extensions.DependencyInjection;

namespace Jasper.Allinpay.ConsoleDemo;

class Program {
    static async Task Main(string[] args) {

        var services = new ServiceCollection();
        services.AddLogging();
        services.AddSingleton(new AllinpayOptions {
            MerchantId = "XXXXXX",
            MerchantKey = "XXX",
            SignType = "RSA",
            AppId = "XXXXXX",
        });
        services.AddSingleton<IAllinpayClient, AllinpayClient>();

        var sp = services.BuildServiceProvider();
        var client = sp.GetRequiredService<IAllinpayClient>();

        var orderNo = Guid.NewGuid().ToString("N");
        
        var response = await client.ExecuteUnifiedPaymentAsync(request => {
            request.PayType = "A01";
            request.TrxAmt = 1;
            request.UnireqSn = orderNo;
        });
        
        Console.WriteLine("结束");
    }
}