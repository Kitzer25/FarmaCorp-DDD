using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.UseCases.PrescriptionUseCase.Commands;
using Domain.Commons;
using Domain.DTO_s.Prescriptions;
using MediatR;

namespace API.Controllers;

[ApiController]
[Authorize(Policy = PolicyNames.TotalAccess)]
[Route("api/v1/admin/prescriptions")]
public class AdminPrescriptionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminPrescriptionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPatch("{prescriptionId:int}/verification")]
    [ProducesResponseType(typeof(PrescriptionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Verify(int prescriptionId, VerifyPrescriptionDto request, CancellationToken ct)
    {
        var result = await _mediator.Send(new PATCHVerifyPrescriptionCommand
        {
            PrescriptionId = prescriptionId,
            UserId = GetUserId(),
            Request = request
        }, ct);
        return Ok(result);
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
