using System.Security.Claims;
using Application.Prescriptions.Dtos;
using Application.Prescriptions.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Core.Constants;

namespace API.Controllers;

[ApiController]
[Authorize(Policy = "TotalAccess")]
[Route(PolicyNames.PharmacistAccess)]
public class AdminPrescriptionsController : ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;

    public AdminPrescriptionsController(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    [HttpPatch("{prescriptionId:int}/verification")]
    public async Task<IActionResult> Verify(int prescriptionId, VerifyPrescriptionDto request, CancellationToken ct)
    {
        try
        {
            var result = await _prescriptionService.VerifyAsync(prescriptionId, GetUserId(), request, ct);
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
