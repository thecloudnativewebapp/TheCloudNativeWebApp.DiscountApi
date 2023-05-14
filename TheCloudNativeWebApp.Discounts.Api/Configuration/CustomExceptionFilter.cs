using TheCloudNativeWebApp.Discounts.Core;
using TheCloudNativeWebApp.Discounts.Core.UseCases.NewVoucher;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TheCloudNativeWebApp.Discounts.Api.Configuration;

public class CustomExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var conflictTypes = new[]
        {
            typeof(VoucherUsedException),
            typeof(DuplicateVoucherException)
        };
        
        if (conflictTypes.Any(type => context.Exception.GetType() == type))
        {
            context.Result = new ConflictResult();
            return;
        }

        // Suppress exception. Don't want every developer to know what our code looks like...
        context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
    }
}