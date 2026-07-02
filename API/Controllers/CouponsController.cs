using Application.UseCases.Promotion.Querys;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace API.Controllers;

[ApiController]
[Route("api/v1/coupons")]
public class CouponsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CouponsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("validate")]
    public async Task<IActionResult> Validate(ValidateCouponRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(new ValidateCouponQuery { Code = request.Code, OrderSubtotal = request.OrderSubtotal }, ct);
        return Ok(result);
    }
}

public class ValidateCouponRequest
{
    public string Code { get; set; } = null!;
    public decimal OrderSubtotal { get; set; }
}
