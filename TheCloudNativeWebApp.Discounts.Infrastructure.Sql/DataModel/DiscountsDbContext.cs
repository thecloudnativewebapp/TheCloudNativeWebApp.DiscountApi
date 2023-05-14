using Microsoft.EntityFrameworkCore;

namespace TheCloudNativeWebApp.Discounts.Infrastructure.Sql.DataModel;

public class DiscountsDbContext : DbContext
{
    public DiscountsDbContext(DbContextOptions<DiscountsDbContext> options) : base(options)
    {   
    }
    
    public DbSet<Voucher> Vouchers { get; set; }
}