using AutoMapper;
using TheCloudNativeWebApp.Discounts.Core;
using TheCloudNativeWebApp.Discounts.Infrastructure.Sql;
using TheCloudNativeWebApp.Discounts.Infrastructure.Sql.QueryHandlers.Vouchers;
using Microsoft.AspNetCore.Mvc;
using Voucher = TheCloudNativeWebApp.Discounts.Infrastructure.Sql.DataModel.Voucher;

namespace TheCloudNativeWebApp.Discounts.Api.Endpoints.Vouchers.Get;

[ApiController]
[Route("api/voucher")]
public class VoucherController : ControllerBase
{
    private readonly IQueryHandler<GetVoucherQuery, IEnumerable<Voucher>> _queryHandler;
    private readonly IMapper _mapper;

    public VoucherController(IQueryHandler<GetVoucherQuery, IEnumerable<Voucher>> queryHandler,
        IMapper mapper)
    {
        _queryHandler = queryHandler;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var entities = await _queryHandler.ExecuteAsync(new GetVoucherQuery());
        var items = _mapper.Map<IEnumerable<VoucherViewModel>>(entities);
        return Ok(new GetVouchersResponse(items));
    }
    
    [HttpGet("{voucherCode}")]
    public async Task<ActionResult> Get([FromRoute] VoucherCode voucherCode)
    {
        var query = new GetVoucherQuery
        {
            VoucherId = voucherCode.ToString() 
        };
        
        var entities = await _queryHandler.ExecuteAsync(query);
        var items = _mapper.Map<IEnumerable<VoucherViewModel>>(entities);
        return Ok(new GetVouchersResponse(items));
    }
}