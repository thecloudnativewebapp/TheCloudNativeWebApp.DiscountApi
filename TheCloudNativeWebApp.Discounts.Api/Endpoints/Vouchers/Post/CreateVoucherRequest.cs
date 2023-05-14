using TheCloudNativeWebApp.Discounts.Api.Configuration.Json;
using TheCloudNativeWebApp.Discounts.Core;

namespace TheCloudNativeWebApp.Discounts.Api.Endpoints.Vouchers.Post;

public class CreateVoucherRequest
{
    public VoucherCode VoucherCode { get; set; }
    
    [MinAmount(0.01)]
    public Euro Value { get; set; }
}