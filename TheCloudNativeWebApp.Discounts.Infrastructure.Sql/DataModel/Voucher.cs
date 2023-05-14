using System.ComponentModel.DataAnnotations;

namespace TheCloudNativeWebApp.Discounts.Infrastructure.Sql.DataModel;

public class Voucher
{
    [Key] public string VoucherCode { get; set; }
    
    public decimal Value { get; set; }
    
    public Guid? OrderId { get; set; }
}