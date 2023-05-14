namespace TheCloudNativeWebApp.Discounts.Core.UseCases.NewVoucher;

public class NewVoucherUseCase : INewVoucherUseCase
{
    private readonly IVoucherRepository _voucherRepository;

    public NewVoucherUseCase(IVoucherRepository voucherRepository)
    {
        _voucherRepository = voucherRepository;
    }

    public async Task CreateNewVoucher(VoucherCode voucherCode, Euro value)
    {
        if (await _voucherRepository.ExistsAsync(voucherCode))
        {
            throw new DuplicateVoucherException();
        }

        var voucher = new Voucher(voucherCode, value);
        await _voucherRepository.InsertAsync(voucher);
    }
}