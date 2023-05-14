using TheCloudNativeWebApp.Discounts.Core;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace TheCloudNativeWebApp.Discounts.Api.Configuration;

public static class SwaggerInitializer
{
    public static IServiceCollection AddSwaggerDefinition(this IServiceCollection serviceCollection) => serviceCollection
        .AddSwaggerGen(options =>
        {   
            options.MapType(typeof(OrderId), () => new OpenApiSchema
            {
                Type = "string",
                Example = new OpenApiString("00000000-0000-0000-0000-000000000000")
            });
            
            options.MapType(typeof(VoucherCode), () => new OpenApiSchema
            {
                Type = "string",
                Example = new OpenApiString("00000000")
            });
            
            options.MapType(typeof(Euro), () => new OpenApiSchema
            {
                Type = "decimal",
                Example = new OpenApiString("3.14")
            });
        });
}