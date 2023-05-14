using AutoMapper;
using TheCloudNativeWebApp.Discounts.Core;

namespace TheCloudNativeWebApp.Discounts.Infrastructure.Sql.DataModel.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<global::TheCloudNativeWebApp.Discounts.Core.Voucher, Voucher>()
            .ForMember(d => d.VoucherCode, o => o.MapFrom(s => s.VoucherCode.ToString()))
            .ForMember(d => d.Value, o => o.MapFrom(s => s.Value.ToDecimal()))
            .ForMember(d => d.OrderId, o => o.MapFrom(s => s.OrderId == null ? (Guid?)null : s.OrderId.Value.ToGuid()));
        
        CreateMap<Voucher, global::TheCloudNativeWebApp.Discounts.Core.Voucher>()
            .ConvertUsing<VoucherConverter>();
        
        CreateMap<string, VoucherCode>()
            .ConvertUsing(x => VoucherCode.Create(x));
        
        CreateMap<decimal, Euro>()
            .ConvertUsing(x => Euro.Create(x));
        
        CreateMap<Guid?, OrderId?>()
            .ConvertUsing(x => x.HasValue ? OrderId.Create(x.Value) : null);
    }

    private class VoucherConverter : ITypeConverter<Voucher, global::TheCloudNativeWebApp.Discounts.Core.Voucher>
    {
        public global::TheCloudNativeWebApp.Discounts.Core.Voucher Convert(Voucher source, global::TheCloudNativeWebApp.Discounts.Core.Voucher destination, ResolutionContext context)
        {
            var voucherCode = context.Mapper.Map<VoucherCode>(source.VoucherCode);
            var value = context.Mapper.Map<Euro>(source.Value);
            var orderId = context.Mapper.Map<OrderId?>(source.OrderId);

            return global::TheCloudNativeWebApp.Discounts.Core.Voucher.Deflate(voucherCode, value, orderId);
        }
    }
}