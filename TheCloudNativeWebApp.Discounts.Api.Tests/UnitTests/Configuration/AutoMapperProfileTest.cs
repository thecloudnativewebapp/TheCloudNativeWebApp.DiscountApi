using AutoMapper;
using TheCloudNativeWebApp.Discounts.Infrastructure.Sql.DataModel;
using TheCloudNativeWebApp.Discounts.Api.Configuration;
using TheCloudNativeWebApp.Discounts.Api.Endpoints.Vouchers.Get;

namespace TheCloudNativeWebApp.Discounts.Api.Tests.UnitTests.Configuration;

public class AutoMapperProfileTest
{
    private readonly Faker _faker = new();

    [Fact]
    public void ShouldMapVoucherDtoToVoucherModel()
    {
        // Arrange
        var payload = new List<Voucher>
        {
            new()
            {
                VoucherCode = _faker.Random.Replace("########"),
                Value = 3.14m
            },
            new()
            {
                VoucherCode = _faker.Random.Replace("########"),
                Value = 6.28m
            }
        };

        var sut = CreateMapper();
        
        // Act
        var actual = sut.Map<IEnumerable<VoucherViewModel>>(payload);
        
        // Assert
        actual.Should().NotBeEmpty();
        actual.Select(x => x.VoucherCode.ToString()).Should().BeEquivalentTo(payload.Select(x => x.VoucherCode));
        actual.Select(x => x.Value.ToDecimal()).Should().BeEquivalentTo(payload.Select(x => x.Value));
    }

    private static IMapper CreateMapper() => new MapperConfiguration(x => x.AddProfile<AutoMapperProfile>())
        .CreateMapper();
}