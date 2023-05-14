namespace TheCloudNativeWebApp.Discounts.Api.Endpoints.Vouchers.Get;

public class GetVouchersResponse
{
    public GetVouchersResponse()
    {
        Items = Array.Empty<VoucherViewModel>();
    }

    public GetVouchersResponse(IEnumerable<VoucherViewModel> items)
    {
        Items = items;
    }
    
    public IEnumerable<VoucherViewModel> Items { get; set; }
}