using Core.Commons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Core.DTO_s.Promotions;
using Core.Ports.Services;
using Core.Ports.Services.EServices;

namespace API.Controllers;

[ApiController]
[Authorize(Policy = PolicyNames.ManagerAccess)]
[Route("api/v1/admin/promotions")]
public class AdminPromotionsController : ControllerBase
{
    private readonly IPromotionService _promotionService;

    public AdminPromotionsController(IPromotionService promotionService)
    {
        _promotionService = promotionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetActive(CancellationToken ct)
    {
        return Ok(await _promotionService.GetActiveAsync(ct));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePromotionDto request, CancellationToken ct)
    {
        return Ok(await _promotionService.CreateAsync(request, ct));
    }
}
