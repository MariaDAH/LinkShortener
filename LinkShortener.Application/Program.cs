using LinkShortener.Application;
using LinkShortener.Application.Services;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddHttpClient();
builder.Services.AddScoped<ILink, Link>();
builder.Services.AddScoped<IQRGeneratorService, QRGeneratorService>();

var host = builder.Build();
host.Run();