using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TheCloudNativeWebApp.Discounts.Api.Configuration.ModelBinding;

public abstract class CustomBinder<T> : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {        
        var modelName = bindingContext.ModelName;

        // Try to fetch the value of the argument by name
        var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);
        
        var model = Create(valueProviderResult);
        bindingContext.Result = ModelBindingResult.Success(model);
        return Task.CompletedTask;
    }

    protected abstract T Create(ValueProviderResult valueProviderResult);
}