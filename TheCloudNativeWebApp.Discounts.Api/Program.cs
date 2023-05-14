using System.Text;
using Microsoft.AspNetCore.Mvc;
using TheCloudNativeWebApp.Discounts.Api.Configuration;
using TheCloudNativeWebApp.Discounts.Core.Configuration;
using TheCloudNativeWebApp.Discounts.Infrastructure.Sql.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapperProfiles();
builder.Services.AddVoucherApiControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDefinition();

var connectionString = builder.Configuration.GetValue<string>("ConnectionString");
builder.Services
    .AddApplicationCore()
    .AddSql(connectionString);

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.SetDefaultCulture("en-US");
    options.AddSupportedUICultures("en-US", "nl-NL");
    options.FallBackToParentUICultures = true;
    options.RequestCultureProviders.Clear();
});

var app = builder.Build();

app.Map("/get-headers", (HttpContext context) =>
{
    var response = new StringBuilder();
    
    var headers = context.Request.Headers;
    foreach (var header in headers)
    {
        response.Append($"{header.Key} = {header.Value}");
    }

    return response.ToString();
});

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseRequestLocalization();

app.Services.MigrateDatabase();

app.Run();

public partial class Program // Expose this app to the testing assembly
{
}