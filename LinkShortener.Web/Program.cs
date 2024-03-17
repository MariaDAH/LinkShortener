using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using LinkShortener.Application.Services;
using LinkShortener.Infrastructure.Daos;
using LinkShortener.Infrastructure.Services;
using LinkShortener.Web;
using LinkShortener.Web.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        policy  =>
        {
            policy.WithOrigins("http://localhost:5087/",
                    "https://localhost:7175")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddLogging(logging => logging.AddConsole());

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.TypeInfoResolver = new PolymorphicTypeResolver());

builder.Services.AddRouting(options => options.LowercaseUrls = true);

//ToDo: Move data access configuration to infrastructure component and remove ef dependencies in this project
builder.Services.AddDbContext<ApplicationDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:LinkShortener"]);
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IShortenerService, ShortenerService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILinkConverterCommand, LinkConverterCommand>(_ => new LinkConverterCommand());
builder.Services.AddTransient<IApiKeyValidation, ApiKeyValidation>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.MapControllers();

app.Run();