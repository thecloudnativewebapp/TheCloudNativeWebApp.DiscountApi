namespace TheCloudNativeWebApp.Discounts.Core;

public sealed class Voucher
{
    public VoucherCode VoucherCode { get; }
    
    public Euro Value { get; private set; }
    
    public OrderId? OrderId { get; private set; }

    public static Voucher Deflate(VoucherCode voucherCode, Euro value, OrderId? orderId) => new(voucherCode, value, orderId);

    private Voucher(VoucherCode voucherCode, Euro value, OrderId? orderId)
    {
        VoucherCode = voucherCode;
        Value = value;
        OrderId = orderId;
    }

    public Voucher(VoucherCode voucherCode, Euro value)
    {
        VoucherCode = voucherCode;
        Value = value;
    }
    
    public void ChangeValue(Euro newValue)
    {
        if (OrderId.HasValue)
        {
            throw new VoucherUsedException(VoucherCode);
        }

        Value = newValue;
    }

    public void UseVoucherForOrder(OrderId orderId)
    {
        if (OrderId.HasValue)
        {
            throw new VoucherUsedException(VoucherCode);
        }

        OrderId = orderId;
    }
}