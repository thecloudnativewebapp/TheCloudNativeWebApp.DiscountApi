using TheCloudNativeWebApp.Discounts.Infrastructure.Sql.DataModel;
using TheCloudNativeWebApp.Discounts.Infrastructure.Sql.QueryHandlers.Vouchers;

namespace TheCloudNativeWebApp.Discounts.Infrastructure.Sql.Tests.UnitTests.QueryHandlers;

public class GetVoucherQueryHandlerTests
{
    private readonly string DatabaseName = Guid.NewGuid().ToString();

    private const string VoucherId = "00000001";
    
    [Fact]
    public async Task WhenData_ShouldReturnVouchers()
    {
        // Arrange
        await CreateDataContextWithData();
        
        await using var dbContext = new InMemoryDiscountsDbContext(DatabaseName);

        var sut = new GetVoucherQueryHandler(dbContext);
        
        // Act
        var vouchers = await sut.ExecuteAsync(new GetVoucherQuery());
        
        // Assert
        vouchers.Should().NotBeEmpty();
    }
    
    [Fact]
    public async Task WhenQueryingForId_ShouldReturnSingleItem()
    {
        // Arrange
        await CreateDataContextWithData();
        
        await using var dbContext = new InMemoryDiscountsDbContext(DatabaseName);

        var sut = new GetVoucherQueryHandler(dbContext);
        
        // Act
        var vouchers = await sut.ExecuteAsync(new GetVoucherQuery { VoucherId = VoucherId });
        
        // Assert
        vouchers.Should().HaveCount(1);
    }
    
    private async Task CreateDataContextWithData()
    {
        await using var dbContext = new InMemoryDiscountsDbContext(DatabaseName);

        await dbContext.Vouchers.AddAsync(new Voucher
        {
            VoucherCode = VoucherId,
            Value = 3.14m,
        });
        
        await dbContext.Vouchers.AddAsync(new Voucher
        {
            VoucherCode = "00000002",
            Value = 6.28m,
        });

        await dbContext.SaveChangesAsync();
    }
}