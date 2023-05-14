using AutoMapper;
using TheCloudNativeWebApp.Discounts.Core;

namespace TheCloudNativeWebApp.Discounts.Infrastructure.Sql.Tests.UnitTests;

public class AutoMapperProfileTest
{
    private readonly VoucherFaker _faker = new ();
    
    private readonly IMapper _mapper = MapperFactory.Create();
   
    [Fact]
    public void ShouldMapVoucherDomainObjectToPoco()
    {
        var payload = _faker.Create<Voucher>();

        var actual = _mapper.Map<TheCloudNativeWebApp.Discounts.Infrastructure.Sql.DataModel.Voucher>(payload);

        actual.VoucherCode.Should().Be(payload.VoucherCode.ToString());
        actual.Value.Should().Be(payload.Value.ToDecimal());
    }
}