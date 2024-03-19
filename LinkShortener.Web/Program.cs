using LinkShortener.Application.Services;
using LinkShortener.Infrastructure.Daos;
using LinkShortener.Infrastructure.Services;
using LinkShortener.Web;
using LinkShortener.Web.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(opts => opts.AddDefaultPolicy(bld =>
{
    bld
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithExposedHeaders("*");
}));

builder.Services.AddLogging(logging => logging.AddConsole());

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.TypeInfoResolver = new PolymorphicTypeResolver());

builder.Services.AddRouting(options => options.LowercaseUrls = true);

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
app.UseCors();
app.MapControllers();

app.Run();