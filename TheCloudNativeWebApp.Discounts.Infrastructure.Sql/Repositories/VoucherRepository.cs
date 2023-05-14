using AutoMapper;
using TheCloudNativeWebApp.Discounts.Core;
using TheCloudNativeWebApp.Discounts.Core.UseCases;
using TheCloudNativeWebApp.Discounts.Infrastructure.Sql.DataModel.Mapping;
using Microsoft.EntityFrameworkCore;
using TheCloudNativeWebApp.Discounts.Infrastructure.Sql.DataModel;
using Voucher = TheCloudNativeWebApp.Discounts.Infrastructure.Sql.DataModel.Voucher;

namespace TheCloudNativeWebApp.Discounts.Infrastructure.Sql.Repositories;

internal class VoucherRepository : IVoucherRepository
{
    private readonly DiscountsDbContext _dbContext;
    private readonly IMapper _mapper;

    public VoucherRepository(DiscountsDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<bool> ExistsAsync(VoucherCode voucherCode)
    {
        return await _dbContext.Vouchers.AnyAsync(x => x.VoucherCode == voucherCode.ToString());
    }

    public async Task InsertAsync(global::TheCloudNativeWebApp.Discounts.Core.Voucher voucher)
    {
        var poco = _mapper.Map<DataModel.Voucher>(voucher);

        await _dbContext.Vouchers.AddAsync(poco);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<global::TheCloudNativeWebApp.Discounts.Core.Voucher> GetAsync(VoucherCode voucherCode)
    {
        var poco = await _dbContext.Vouchers.SingleAsync(x => x.VoucherCode == voucherCode.ToString());
        return _mapper.Map<global::TheCloudNativeWebApp.Discounts.Core.Voucher>(poco);
    }

    public async Task UpdateAsync(global::TheCloudNativeWebApp.Discounts.Core.Voucher voucher)
    {        
        var poco = await _dbContext.Vouchers.SingleAsync(x => x.VoucherCode == voucher.VoucherCode.ToString());
        voucher.ApplyChanges(poco);
        
        await _dbContext.SaveChangesAsync();
    }
}
