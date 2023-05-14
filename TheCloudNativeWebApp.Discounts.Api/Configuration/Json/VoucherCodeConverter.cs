using TheCloudNativeWebApp.Discounts.Core;

namespace TheCloudNativeWebApp.Discounts.Api.Configuration.Json;

public class VoucherCodeConverter : JsonStringConverter<VoucherCode>
{
    protected override VoucherCode Create(string value) => VoucherCode.Create(value);
}