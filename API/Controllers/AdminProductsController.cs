using Application.UseCases.AdminProductUseCase.Commands;
using Application.UseCases.ProductUseCase.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.Commons;
using Domain.Ports.Services;
using Domain.DTO_s.AdminProducts;
using Domain.DTO_s.Products;
using MediatR;

namespace API.Controllers;

[ApiController]
[Authorize(Policy = PolicyNames.ManagerAccess)]
[Route("api/v1/admin/products")]
public sealed class AdminProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ProductListDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProducts([FromQuery] ProductQueryParams queryParams, CancellationToken ct)
    {
        var result = await _mediator.Send(new GETProductsQuery { Params = queryParams }, ct);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductAdminDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(SaveProductAdminDto dto, CancellationToken ct)
    {
        var result = await _mediator.Send(new POSTCreateAdminProductCommand { Request = dto }, ct);
        return CreatedAtAction(nameof(Create), new { id = result.ProductId }, result);
    }

    [HttpPut("{productId:int}")]
    [ProducesResponseType(typeof(ProductAdminDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int productId, SaveProductAdminDto dto, CancellationToken ct)
    {
        var result = await _mediator.Send(new PUTUpdateAdminProductCommand { ProductId = productId, Request = dto }, ct);
        return Ok(result);
    }

    [HttpDelete("{productId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> SoftDelete(int productId, CancellationToken ct)
    {
        await _mediator.Send(new DELETESoftDeleteAdminProductCommand(productId), ct);
        return NoContent();
    }
}