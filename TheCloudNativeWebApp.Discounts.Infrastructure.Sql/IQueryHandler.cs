namespace TheCloudNativeWebApp.Discounts.Infrastructure.Sql;

public interface IQueryHandler<TIn, TOut>
{
    Task<TOut> ExecuteAsync(TIn query);
}