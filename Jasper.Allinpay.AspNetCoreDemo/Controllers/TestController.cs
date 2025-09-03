using Jasper.Allinpay.Core.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Jasper.Allinpay.AspNetCoreDemo.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class TestController(IAllinpayClient allinpayClient) : ControllerBase {

    [HttpGet]
    public async Task<IActionResult> Pay() {
        var orderNo = Guid.NewGuid().ToString("N");

        var response = await allinpayClient.ExecuteUnifiedPaymentAsync(request => {
            request.PayType = "A01";
            request.TrxAmt = 1;
            request.UnireqSn = orderNo;
            request.NotifyUrl = "https://localhost:5029/api/AllinpayCallback/PaymentNotify";
        });

        return Ok(response);
    }
}