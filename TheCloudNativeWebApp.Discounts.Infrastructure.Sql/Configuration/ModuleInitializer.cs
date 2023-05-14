using TheCloudNativeWebApp.Discounts.Core.UseCases;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TheCloudNativeWebApp.Discounts.Infrastructure.Sql.DataModel;
using TheCloudNativeWebApp.Discounts.Infrastructure.Sql.QueryHandlers.Vouchers;
using TheCloudNativeWebApp.Discounts.Infrastructure.Sql.Repositories;

namespace TheCloudNativeWebApp.Discounts.Infrastructure.Sql.Configuration;

public static class ModuleInitializer
{
    public static IServiceCollection AddSql(this IServiceCollection serviceCollection, string connectionString) =>
        AddSql(serviceCollection, builder =>
        {
            builder.UseSqlServer(connectionString, optionsBuilder => optionsBuilder.EnableRetryOnFailure(10, TimeSpan.FromSeconds(10), null)); 
        });
    
    internal static IServiceCollection AddSql(this IServiceCollection serviceCollection, Action<DbContextOptionsBuilder> dbContextOptionsBuilder)
    {
        return serviceCollection
            .AddDbContext<DiscountsDbContext>(dbContextOptionsBuilder)
            .AddTransient<IQueryHandler<GetVoucherQuery, IEnumerable<Voucher>>, GetVoucherQueryHandler>()
            .AddTransient<IVoucherRepository, VoucherRepository>();
    }

    public static void MigrateDatabase(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        using var dataContext = scope.ServiceProvider.GetRequiredService<DiscountsDbContext>();

        dataContext.Database.Migrate();
    }
}