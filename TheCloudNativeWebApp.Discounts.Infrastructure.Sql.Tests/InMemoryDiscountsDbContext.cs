using TheCloudNativeWebApp.Discounts.Infrastructure.Sql.DataModel;
using Microsoft.EntityFrameworkCore;

namespace TheCloudNativeWebApp.Discounts.Infrastructure.Sql.Tests;

public class InMemoryDiscountsDbContext : DiscountsDbContext
{
    public InMemoryDiscountsDbContext(string databaseName) : base(new DbContextOptionsBuilder<DiscountsDbContext>()
        .UseInMemoryDatabase(databaseName)
        .Options)
    {
    }
}