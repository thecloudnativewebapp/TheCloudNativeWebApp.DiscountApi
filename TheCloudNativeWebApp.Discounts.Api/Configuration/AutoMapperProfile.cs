using AutoMapper;
using TheCloudNativeWebApp.Discounts.Api.Endpoints.Vouchers.Get;
using TheCloudNativeWebApp.Discounts.Core;
using Voucher = TheCloudNativeWebApp.Discounts.Infrastructure.Sql.DataModel.Voucher;

namespace TheCloudNativeWebApp.Discounts.Api.Configuration;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<IEnumerable<Voucher>, IEnumerable<VoucherViewModel>>()
            .ConvertUsing<EnumerableConverter<Voucher, VoucherViewModel>>();
        
        CreateMap<Voucher, VoucherViewModel>();
        CreateMap<decimal, Euro>().ConvertUsing(x => Euro.Create(x));
    }

    private sealed class EnumerableConverter<TSource, TTarget> : ITypeConverter<IEnumerable<TSource>, IEnumerable<TTarget>>
    {
        public IEnumerable<TTarget> Convert(IEnumerable<TSource> source, IEnumerable<TTarget> destination, ResolutionContext context)
        {
            return source.Select(x => context.Mapper.Map<TTarget>(x));
        }
    }
}