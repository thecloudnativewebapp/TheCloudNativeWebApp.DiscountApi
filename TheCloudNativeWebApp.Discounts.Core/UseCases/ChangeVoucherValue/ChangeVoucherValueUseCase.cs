namespace TheCloudNativeWebApp.Discounts.Core.UseCases.ChangeVoucherValue;

public class ChangeVoucherValueUseCase : IChangeVoucherValueUseCase
{
    private readonly IVoucherRepository _voucherRepository;

    public ChangeVoucherValueUseCase(IVoucherRepository voucherRepository)
    {
        _voucherRepository = voucherRepository;
    }
    
    public async Task ChangeValue(VoucherCode voucherCode, Euro value)
    {
        var voucher = await _voucherRepository.GetAsync(voucherCode);
        voucher.ChangeValue(value);

        await _voucherRepository.UpdateAsync(voucher);
    }
}