using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.Ports.Services;
using Domain.Commons;
using Domain.Ports.Services.EServices;

namespace API.Controllers;

[ApiController]
[Authorize(Policy = PolicyNames.ManagerAccess)]
[Route("api/v1/admin/reports")]
public class AdminReportsController : ControllerBase
{
    private readonly IReportService _reportService;

    public AdminReportsController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet("dashboard")]
    public async Task<IActionResult> GetDashboard(CancellationToken ct)
    {
        return Ok(await _reportService.GetDashboardSummaryAsync(ct));
    }
}
