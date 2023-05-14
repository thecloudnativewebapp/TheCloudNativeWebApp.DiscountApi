using TheCloudNativeWebApp.Discounts.Api.Configuration.Json;
using TheCloudNativeWebApp.Discounts.Api.Configuration.ModelBinding;

namespace TheCloudNativeWebApp.Discounts.Api.Configuration;

public static class MvcInitializer
{
    public static IMvcBuilder AddVoucherApiControllers(this IServiceCollection serviceCollection) => serviceCollection
        .AddControllers(options =>
        {
            options.Filters.Add<CustomExceptionFilter>();
            
            options.ModelBinderProviders.Insert(0, new ValueTypeBinderProvider());
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new VoucherCodeConverter());
            options.JsonSerializerOptions.Converters.Add(new EuroConverter());
            options.JsonSerializerOptions.Converters.Add(new OrderIdConverter());
        }); 

}