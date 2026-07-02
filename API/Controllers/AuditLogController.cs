using Application.UseCases.AuditLogUseCase.Queries;
using Domain.Commons;
using Domain.DTO_s.AuditLog;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Authorize(Policy = PolicyNames.AdminOnly)]
[Route("api/v1/admin/audit-logs")]
public sealed class AuditLogController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuditLogController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("record")]
    [ProducesResponseType(typeof(IEnumerable<AuditLogDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByRecord([FromQuery] string tableName, [FromQuery] string recordId, CancellationToken ct)
    {
        var result = await _mediator.Send(new GETRecordAuditLogQuery { TableName = tableName, RecordId = recordId }, ct);
        return Ok(result);
    }

    [HttpGet("user/{userId:int}")]
    [ProducesResponseType(typeof(IEnumerable<AuditLogDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByUser(int userId, [FromQuery] DateTime from, [FromQuery] DateTime to, CancellationToken ct)
    {
        var result = await _mediator.Send(new GETUserAuditLogQuery { UserId = userId, From = from, To = to }, ct);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AuditLogDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByDateRange([FromQuery] DateTime from, [FromQuery] DateTime to, CancellationToken ct)
    {
        var result = await _mediator.Send(new GETDateRangeAuditLogQuery { From = from, To = to }, ct);
        return Ok(result);
    }
}
