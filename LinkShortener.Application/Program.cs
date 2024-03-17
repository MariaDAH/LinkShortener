using LinkShortener.Application;
using LinkShortener.Application.Services;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddScoped<ILink, Link>();

var host = builder.Build();
host.Run();