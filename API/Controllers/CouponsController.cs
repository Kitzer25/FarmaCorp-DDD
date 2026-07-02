using Application.UseCases.PromotionUseCase.Queries;
using Domain.DTO_s.Promotions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace API.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/v1/coupons")]
public class CouponsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CouponsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("validate")]
    [ProducesResponseType(typeof(CouponValidationDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Validate(ValidateCouponRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(new POSTValidateCouponQuery { Code = request.Code, OrderSubtotal = request.OrderSubtotal }, ct);
        return Ok(result);
    }
}

public class ValidateCouponRequest
{
    public string Code { get; set; } = null!;
    public decimal OrderSubtotal { get; set; }
}
