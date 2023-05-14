namespace TheCloudNativeWebApp.Discounts.Core.UseCases;

public interface IVoucherRepository
{
    Task<bool> ExistsAsync(VoucherCode voucherCode);
    
    Task InsertAsync(Voucher voucher);
    
    Task<Voucher> GetAsync(VoucherCode voucherCode);
    
    Task UpdateAsync(Voucher voucher);
}