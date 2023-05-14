using TheCloudNativeWebApp.Discounts.Core;

namespace TheCloudNativeWebApp.Discounts.Infrastructure.Sql.Tests;

public class VoucherFaker
{
    public T CreateStruct<T>() where T : struct
    {   
        if (typeof(T) == typeof(VoucherCode))
        {
            return (T)(object)CreateVoucherCode();
        }

        throw new NotImplementedException();
    }
    
    public T Create<T>() where T : class
    {
        if (typeof(T) == typeof(Voucher))
        {
            return (T)(object)CreateVoucher();
        }
        
        if (typeof(T) == typeof(VoucherCode))
        {
            return (T)(object)CreateVoucherCode();
        }

        var faker = new Faker<T>();
        return faker.Generate();
    }
    
    private VoucherCode CreateVoucherCode()
    {
        var faker = new Faker();
        return VoucherCode.Create(faker.Random.Replace("########"));
    }
    
    private Euro CreateEuro()
    {
        var faker = new Faker();
        var valueString = faker.Random.Replace("######,##");
        var value = decimal.Parse(valueString);
        return Euro.Create(value);
    }
    
    private Voucher CreateVoucher()
    {
        var voucherCode = CreateVoucherCode();
        var value = CreateEuro();
        return new Voucher(voucherCode, value);
    }
}