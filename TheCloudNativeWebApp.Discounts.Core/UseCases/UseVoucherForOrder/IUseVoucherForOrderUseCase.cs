namespace TheCloudNativeWebApp.Discounts.Core.UseCases.UseVoucherForOrder;

public interface IUseVoucherForOrderUseCase
{
    Task Use(VoucherCode voucherCode, OrderId orderId);
}