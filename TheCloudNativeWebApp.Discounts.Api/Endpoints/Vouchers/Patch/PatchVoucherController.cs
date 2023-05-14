using TheCloudNativeWebApp.Discounts.Core;
using TheCloudNativeWebApp.Discounts.Core.UseCases.ChangeVoucherValue;
using TheCloudNativeWebApp.Discounts.Core.UseCases.UseVoucherForOrder;
using Microsoft.AspNetCore.Mvc;

namespace TheCloudNativeWebApp.Discounts.Api.Endpoints.Vouchers.Patch;

[ApiController]
[Tags("Voucher")]
[Route("api/voucher")]
public class PatchVoucherController : ControllerBase
{
    [HttpPatch("{voucherCode}")]
    public async Task<IActionResult> Post([FromServices]IChangeVoucherValueUseCase changeVoucherValueUseCase, 
        [FromServices]IUseVoucherForOrderUseCase useVoucherForOrderUseCase,
        [FromRoute] VoucherCode voucherCode,
        PatchVoucherRequest request)
    {
        if (request.Value.HasValue)
        {
            await changeVoucherValueUseCase.ChangeValue(voucherCode, request.Value.Value);    
        }

        if (request.OrderId.HasValue)
        {
            await useVoucherForOrderUseCase.Use(voucherCode, request.OrderId.Value);
        }

        return Ok();
    }
}