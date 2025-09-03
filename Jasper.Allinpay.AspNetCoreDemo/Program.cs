

using Jasper.Allinpay.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 配置方式注入，约定Section名称为"Allinpay"
builder.Services.AddAllinpayClient();

// 代码配置方式注入
// builder.Services.AddAllinpayClient(options => {
//     options.MerchantId = "XXXXXX";
//     options.MerchantKey = "XXX";
//     options.SignType = "RSA";
//     options.AppId = "XXXXXX";
// });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();