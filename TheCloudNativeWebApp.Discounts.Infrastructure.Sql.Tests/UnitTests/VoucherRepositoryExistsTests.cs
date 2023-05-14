using AutoMapper;
using TheCloudNativeWebApp.Discounts.Core;
using TheCloudNativeWebApp.Discounts.Infrastructure.Sql.DataModel;
using TheCloudNativeWebApp.Discounts.Infrastructure.Sql.Repositories;
using Voucher = TheCloudNativeWebApp.Discounts.Infrastructure.Sql.DataModel.Voucher;

namespace TheCloudNativeWebApp.Discounts.Infrastructure.Sql.Tests.UnitTests;

public class VoucherRepositoryExistsTests
{
    private readonly string _testId = Guid.NewGuid().ToString();

    private readonly VoucherFaker _faker = new ();
    
    private DiscountsDbContext CreateDbContext() => new InMemoryDiscountsDbContext(_testId);
    
    [Fact]
    public async Task WhenUnique_ShouldReturnFalse()
    {
        // Arrange
        var payload = _faker.CreateStruct<VoucherCode>();
        
        await using (var dbContext = CreateDbContext())
        {
            var sut = new VoucherRepository(dbContext, Substitute.For<IMapper>());
            
            // Act
            var actual = await sut.ExistsAsync(payload);
            
            // Assert
            actual.Should().BeFalse();
        }
    }
    
    [Fact]
    public async Task WhenExists_ShouldReturnTrue()
    {
        // Arrange
        var payload = _faker.CreateStruct<VoucherCode>();
        
        await using (var dbContext = CreateDbContext())
        {
            var voucher = _faker.Create<Voucher>();
            voucher.VoucherCode = payload.ToString();
            dbContext.Vouchers.Add(voucher);
            await dbContext.SaveChangesAsync();
            
            var sut = new VoucherRepository(dbContext, Substitute.For<IMapper>());
            
            // Act
            var actual = await sut.ExistsAsync(payload);
            
            // Assert
            actual.Should().BeTrue();
        }
    }
}