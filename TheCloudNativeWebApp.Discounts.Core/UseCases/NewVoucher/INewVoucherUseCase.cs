namespace TheCloudNativeWebApp.Discounts.Core.UseCases.NewVoucher;

public interface INewVoucherUseCase
{
    Task CreateNewVoucher(VoucherCode voucherCode, Euro value);
}