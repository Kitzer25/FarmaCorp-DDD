using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.UseCases.BatchUseCase.Queries;
using Application.UseCases.BatchUseCase.Commands;
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
    [ProducesResponseType(typeof(IEnumerable<ProductBatchDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBatches(CancellationToken ct)
    {
        return Ok(await _mediator.Send(new GETBatchesQuery(), ct));
    }

    [HttpGet("expiring")]
    [ProducesResponseType(typeof(IEnumerable<ExpiringBatchDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetExpiringBatches(CancellationToken ct)
    {
        return Ok(await _mediator.Send(new GETExpiringBatchesQuery(), ct));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductBatchDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(CreateProductBatchDto request, CancellationToken ct)
    {
        var result = await _mediator.Send(new POSTCreateBatchCommand { Request = request }, ct);
        return Ok(result);
    }
}
