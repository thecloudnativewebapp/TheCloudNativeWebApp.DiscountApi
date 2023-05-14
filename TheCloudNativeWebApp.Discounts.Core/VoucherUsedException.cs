namespace TheCloudNativeWebApp.Discounts.Core;

public class VoucherUsedException : Exception
{
    public VoucherUsedException(VoucherCode voucherCode) : base($"Voucher {voucherCode} has already been used.")
    {
        
    }
}