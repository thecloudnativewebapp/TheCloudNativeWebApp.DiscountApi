namespace TheCloudNativeWebApp.Discounts.Core.UseCases.ChangeVoucherValue;

public interface IChangeVoucherValueUseCase
{
    Task ChangeValue(VoucherCode voucherCode, Euro value);
}