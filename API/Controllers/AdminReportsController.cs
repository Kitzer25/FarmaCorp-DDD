using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.UseCases.ReportUseCase.Queries;
using Application.UseCases.CustomerWishlistUseCase.Queries;
using Domain.Commons;
using Domain.DTO_s.Reports;
using Domain.DTO_s.CustomerWhislist;
using MediatR;

namespace API.Controllers;

[ApiController]
[Authorize(Policy = PolicyNames.ManagerAccess)]
[Route("api/v1/admin/reports")]
public class AdminReportsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminReportsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("dashboard")]
    [ProducesResponseType(typeof(DashboardSummaryDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDashboard(CancellationToken ct)
    {
        return Ok(await _mediator.Send(new GETDashboardSummaryQuery(), ct));
    }

    [HttpGet("most-wishlisted")]
    [ProducesResponseType(typeof(IEnumerable<MostWishlistedVariantDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMostWishlisted([FromQuery] int top, CancellationToken ct)
    {
        return Ok(await _mediator.Send(new GETMostWishlistedQuery { Top = top }, ct));
    }
}
