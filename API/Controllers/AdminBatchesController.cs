using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.UseCases.Batch.Querys;
using Application.UseCases.Batch.Commands;
using Domain.Commons;
using Domain.DTO_s.Batches;
using MediatR;

namespace API.Controllers;

[ApiController]
[Authorize(Policy = PolicyNames.SalesAccess)]
[Route("api/v1/admin/batches")]
public class AdminBatchesController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminBatchesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetBatches(CancellationToken ct)
    {
        return Ok(await _mediator.Send(new GetBatchesQuery(), ct));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductBatchDto request, CancellationToken ct)
    {
        try
        {
            var result = await _mediator.Send(new CreateBatchCommand { Request = request }, ct);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
