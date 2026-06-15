using Core.Commons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Core.DTO_s.Batches;
using Core.Ports.Services;
using Core.Ports.Services.EServices;

namespace API.Controllers;

[ApiController]
[Authorize(Policy = PolicyNames.SalesAccess)]
[Route("api/v1/admin/batches")]
public class AdminBatchesController : ControllerBase
{
    private readonly IBatchService _batchService;

    public AdminBatchesController(IBatchService batchService)
    {
        _batchService = batchService;
    }

    [HttpGet]
    public async Task<IActionResult> GetBatches(CancellationToken ct)
    {
        return Ok(await _batchService.GetBatchesAsync(ct));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductBatchDto request, CancellationToken ct)
    {
        try
        {
            var result = await _batchService.CreateAsync(request, ct);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
