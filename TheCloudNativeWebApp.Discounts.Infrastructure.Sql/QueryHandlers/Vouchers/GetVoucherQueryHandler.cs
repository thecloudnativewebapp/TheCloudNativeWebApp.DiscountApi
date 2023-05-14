using Microsoft.EntityFrameworkCore;
using TheCloudNativeWebApp.Discounts.Infrastructure.Sql.DataModel;

namespace TheCloudNativeWebApp.Discounts.Infrastructure.Sql.QueryHandlers.Vouchers;

public class GetVoucherQueryHandler : IQueryHandler<GetVoucherQuery, IEnumerable<Voucher>>
{
    private readonly DiscountsDbContext _discountsDbContext;

    public GetVoucherQueryHandler(DiscountsDbContext discountsDbContext)
    {
        _discountsDbContext = discountsDbContext;
    }
    
    public async Task<IEnumerable<Voucher>> ExecuteAsync(GetVoucherQuery query)
    {
        IQueryable<Voucher> queryable = _discountsDbContext.Vouchers;

        if (query.VoucherId != null)
        {
            queryable = queryable.Where(x => x.VoucherCode == query.VoucherId);
        }

        return await queryable.AsNoTracking().ToArrayAsync();
    }
}