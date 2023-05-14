namespace TheCloudNativeWebApp.Discounts.Core.UseCases.UseVoucherForOrder;

public class UseVoucherForOrderUseCase : IUseVoucherForOrderUseCase
{
    private readonly IVoucherRepository _voucherRepository;

    public UseVoucherForOrderUseCase(IVoucherRepository voucherRepository)
    {
        _voucherRepository = voucherRepository;
    }
    
    public async Task Use(VoucherCode voucherCode, OrderId orderId)
    {
        var voucher = await _voucherRepository.GetAsync(voucherCode);
        voucher.UseVoucherForOrder(orderId);

        await _voucherRepository.UpdateAsync(voucher);
    }
}