using AutoMapper;
using TheCloudNativeWebApp.Discounts.Infrastructure.Sql.DataModel;
using TheCloudNativeWebApp.Discounts.Infrastructure.Sql.Repositories;
using Voucher = TheCloudNativeWebApp.Discounts.Core.Voucher;

namespace TheCloudNativeWebApp.Discounts.Infrastructure.Sql.Tests.UnitTests.Repositories;

public class VoucherRepositoryInsertTests
{
    private readonly string _testId = Guid.NewGuid().ToString();

    private readonly VoucherFaker _faker = new ();
    
    private readonly IMapper _mapper = MapperFactory.Create();
    
    private DiscountsDbContext CreateDbContext() => new InMemoryDiscountsDbContext(_testId);
    
    [Fact]
    public async Task ShouldSaveVoucher()
    {
        // Arrange
        var payload = _faker.Create<Voucher>();
        
        await using (var dbContext = CreateDbContext())
        {
            var sut = new VoucherRepository(dbContext, _mapper);
            
            // Act
            await sut.InsertAsync(payload);
        }
        
        // Assert
        await using (var dbContext = CreateDbContext())
        {
            dbContext.Vouchers.Any(x => x.VoucherCode == payload.VoucherCode.ToString()).Should().BeTrue();
            dbContext.Vouchers.Any(x => x.Value == payload.Value.ToDecimal()).Should().BeTrue();
        }
    }
}