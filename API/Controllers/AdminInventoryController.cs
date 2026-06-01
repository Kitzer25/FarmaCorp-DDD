using System.Security.Claims;
using Application.Inventory.Dtos;
using Application.Inventory.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Authorize(Policy = "TotalAccess")]
[Route("api/v1/admin/inventory")]
public class AdminInventoryController : ControllerBase
{
    private readonly IInventoryAdminService _inventoryService;

    public AdminInventoryController(IInventoryAdminService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetInventory(CancellationToken ct)
    {
        return Ok(await _inventoryService.GetInventoryAsync(ct));
    }

    [HttpPost("movements")]
    public async Task<IActionResult> RegisterMovement(CreateInventoryMovementDto request, CancellationToken ct)
    {
        try
        {
            var result = await _inventoryService.RegisterMovementAsync(GetUserId(), request, ct);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    private int? GetUserId()
    {
        var claimValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

        return int.TryParse(claimValue, out var userId) ? userId : null;
    }
}
