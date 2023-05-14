using TheCloudNativeWebApp.Discounts.Core;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace TheCloudNativeWebApp.Discounts.Api.Configuration.ModelBinding;

public class ValueTypeBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context.Metadata.ModelType == typeof(VoucherCode))
        {
            return new BinderTypeModelBinder(typeof(VoucherCodeBinder));
        }

        return null;
    }
}