using TheCloudNativeWebApp.Discounts.Core;

namespace TheCloudNativeWebApp.Discounts.Api.Endpoints.Vouchers.Get;

public class VoucherViewModel
{
    public VoucherCode VoucherCode { get; set; }
    public Euro Value { get; set; }
}