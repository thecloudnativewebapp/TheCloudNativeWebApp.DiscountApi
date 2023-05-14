using TheCloudNativeWebApp.Discounts.Core;

namespace TheCloudNativeWebApp.Discounts.Api.Configuration.Json;

public class OrderIdConverter : JsonStringConverter<OrderId>
{
    protected override OrderId Create(string value)
    {
        var guid = Guid.Parse(value);
        return OrderId.Create(guid);
    }
}