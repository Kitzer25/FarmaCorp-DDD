using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.UseCases.Prescription.Commands;
using Domain.Commons;
using Domain.DTO_s.Prescriptions;
using MediatR;

namespace API.Controllers;

[ApiController]
[Authorize(Policy = "TotalAccess")]
[Route(PolicyNames.PharmacistAccess)]
public class AdminPrescriptionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminPrescriptionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPatch("{prescriptionId:int}/verification")]
    public async Task<IActionResult> Verify(int prescriptionId, VerifyPrescriptionDto request, CancellationToken ct)
    {
        try
        {
            var result = await _mediator.Send(new VerifyPrescriptionCommand
            {
                PrescriptionId = prescriptionId,
                UserId = GetUserId(),
                Request = request
            }, ct);
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
