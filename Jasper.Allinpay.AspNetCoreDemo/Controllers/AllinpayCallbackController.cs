using System.Text.Json.Nodes;
using Jasper.Allinpay.Core.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Jasper.Allinpay.AspNetCoreDemo.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AllinpayCallbackController(
    ILogger<AllinpayCallbackController> logger,
    IAllinpayClient allinpayClient
) : ControllerBase {

    [HttpPost]
    [Consumes("application/x-www-form-urlencoded")]
    public IActionResult PaymentNotify([FromForm] IFormCollection form) {
        logger.LogInformation("----------收到回调----------");


        var dict = form.ToDictionary(k => k.Key, v => v.Value.ToString());

        var jsonObject = new JsonObject();


        foreach (var item in form) {
            jsonObject[item.Key] = JsonValue.Create(item.Value.ToString());
        }
        logger.LogInformation("{content}", jsonObject.ToJsonString());
  
        
        if (allinpayClient.CheckSign(dict)) {
            logger.LogInformation("验签成功+++");
        } else {
            logger.LogInformation("验签失败---");
        }

        return Ok();
    }

}