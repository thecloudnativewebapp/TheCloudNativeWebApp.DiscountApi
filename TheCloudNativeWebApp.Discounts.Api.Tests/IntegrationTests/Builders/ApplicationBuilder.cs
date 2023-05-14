using TheCloudNativeWebApp.Discounts.Infrastructure.Sql.DataModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TheCloudNativeWebApp.Discounts.Api.Tests.IntegrationTests.Builders;

public class ApplicationBuilder : WebApplicationFactory<Program>
{
    private readonly string _connectionString = $"Data Source=discountapi-testdb.sqlite";

    private List<Action<DiscountsDbContext>> _precoditions = new();
    
    public DiscountsDbContext CreateDiscountDbContext()
    {
        var options = new DbContextOptionsBuilder<DiscountsDbContext>()
            .UseSqlite(_connectionString)
            .Options;

        return new DiscountsDbContext(options);
    }

    public ApplicationBuilder()
    {
        using var dbContext = CreateDiscountDbContext();

        // Clear the databases
        dbContext.Database.CloseConnection();
        dbContext.Database.EnsureDeleted();
    }

    public ApplicationBuilder WithPrecondition(Action<DiscountsDbContext> @delegate)
    {
        // Save delegate to be executed after the database was migrated.. Otherwise it won't work..
        _precoditions.Add(@delegate);
        return this;
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(ConfigureSqlite);
    }

    protected override void ConfigureClient(HttpClient client)
    {
        base.ConfigureClient(client);

        // Apply preconditions after API spun up (it has to migrate the database first)
        using var dbContext = CreateDiscountDbContext();
        foreach (var precodition in _precoditions)
        {
            precodition(dbContext);
        }
    }
    
    private void ConfigureSqlite(IServiceCollection collection)
    {
        // Replace all entity framework configuration
        collection
            .Where(d => d.ServiceType.Namespace != null &&
                        d.ServiceType.Namespace.Contains("entity", StringComparison.OrdinalIgnoreCase))
            .ToList()
            .ForEach(d => collection.Remove(d));
        
        // Configure Sqlite
        collection.AddDbContext<DiscountsDbContext>(x => x.UseSqlite(_connectionString));
    }
}