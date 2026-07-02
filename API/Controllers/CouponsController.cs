using Domain.Ports.Services;
using Domain.Ports.Services.EServices;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/v1/coupons")]
public class CouponsController : ControllerBase
{
    private readonly IPromotionService _promotionService;

    public CouponsController(IPromotionService promotionService)
    {
        _promotionService = promotionService;
    }

    [HttpPost("validate")]
    public async Task<IActionResult> Validate(ValidateCouponRequest request, CancellationToken ct)
    {
        return Ok(await _promotionService.ValidateCouponAsync(request.Code, request.OrderSubtotal, ct));
    }
}

public class ValidateCouponRequest
{
    public string Code { get; set; } = null!;
    public decimal OrderSubtotal { get; set; }
}
