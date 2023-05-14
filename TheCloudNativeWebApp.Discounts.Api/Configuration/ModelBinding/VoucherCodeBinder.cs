using TheCloudNativeWebApp.Discounts.Core;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TheCloudNativeWebApp.Discounts.Api.Configuration.ModelBinding;

public class VoucherCodeBinder : CustomBinder<VoucherCode>
{
    protected override VoucherCode Create(ValueProviderResult valueProviderResult)
    {
        var value = valueProviderResult.FirstValue;
        return VoucherCode.Create(value);
    }
}