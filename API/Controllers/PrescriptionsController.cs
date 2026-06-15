using System.Security.Claims;
using Core.Commons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Core.DTO_s.Prescriptions;
using Core.Ports.Services;
using Core.Ports.Services.EServices;

namespace API.Controllers;

[ApiController]
[Authorize(Policy = PolicyNames.ClientAccess)]
[Route("api/v1/prescriptions")]
public class PrescriptionsController : ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;

    public PrescriptionsController(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePrescriptionDto request, CancellationToken ct)
    {
        try
        {
            var result = await _prescriptionService.CreateAsync(GetUserId(), request, ct);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    private int GetUserId()
    {
        var claimValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(claimValue, out var userId))
        {
            throw new UnauthorizedAccessException("Token inválido.");
        }

        return userId;
    }
}
