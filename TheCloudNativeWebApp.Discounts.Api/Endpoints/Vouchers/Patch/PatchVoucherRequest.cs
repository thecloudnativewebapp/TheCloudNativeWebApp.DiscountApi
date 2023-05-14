using TheCloudNativeWebApp.Discounts.Api.Configuration.Json;
using TheCloudNativeWebApp.Discounts.Core;

namespace TheCloudNativeWebApp.Discounts.Api.Endpoints.Vouchers.Patch;

public class PatchVoucherRequest
{
    [MinAmount(0.01)]
    public Euro? Value { get; set; }
    public OrderId? OrderId { get; set; }
}