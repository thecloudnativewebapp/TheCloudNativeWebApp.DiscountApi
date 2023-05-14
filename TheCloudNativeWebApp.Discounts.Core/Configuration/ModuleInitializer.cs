using Microsoft.Extensions.DependencyInjection;
using TheCloudNativeWebApp.Discounts.Core.UseCases.ChangeVoucherValue;
using TheCloudNativeWebApp.Discounts.Core.UseCases.NewVoucher;
using TheCloudNativeWebApp.Discounts.Core.UseCases.UseVoucherForOrder;

namespace TheCloudNativeWebApp.Discounts.Core.Configuration;

public static class ModuleInitializer
{
    public static IServiceCollection AddApplicationCore(this IServiceCollection serviceCollection) => serviceCollection
        .AddTransient<IChangeVoucherValueUseCase, ChangeVoucherValueUseCase>()
        .AddTransient<IUseVoucherForOrderUseCase, UseVoucherForOrderUseCase>()
        .AddTransient<INewVoucherUseCase, NewVoucherUseCase>();

}