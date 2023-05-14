namespace TheCloudNativeWebApp.Discounts.Infrastructure.Sql.DataModel.Mapping;

public static class VoucherExtensions
{
    public static void ApplyChanges(this global::TheCloudNativeWebApp.Discounts.Core.Voucher source, Voucher destination)
    {
        destination.Value = source.Value.ToDecimal();
        destination.OrderId = source.OrderId?.ToGuid();
        destination.VoucherCode = source.VoucherCode.ToString();
    }
}