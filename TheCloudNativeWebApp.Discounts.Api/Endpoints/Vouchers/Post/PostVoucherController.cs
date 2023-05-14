using TheCloudNativeWebApp.Discounts.Core.UseCases.NewVoucher;
using Microsoft.AspNetCore.Mvc;

namespace TheCloudNativeWebApp.Discounts.Api.Endpoints.Vouchers.Post;

[ApiController]
[Tags("Voucher")]
[Route("api/voucher")]
public class PostVoucherController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromServices] INewVoucherUseCase useCase, CreateVoucherRequest request)
    {
        await useCase.CreateNewVoucher(request.VoucherCode, request.Value);
        
        return Ok();
    }
}
