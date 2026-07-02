using Application.UseCases.Product.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.Ports.Services;
using Domain.DTO_s.AdminProducts;
using MediatR;

namespace API.Controllers;

[ApiController]
[Route("api/v1/admin/products")]
public sealed class AdminProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(SaveProductAdminDto dto, CancellationToken ct)
    {
        var result = await _mediator.Send(new CreateAdminProductCommand { Request = dto }, ct);
        return CreatedAtAction(nameof(Create), new { id = result.ProductId }, result);
    }

    [HttpPut("{productId:int}")]
    public async Task<IActionResult> Update(int productId, SaveProductAdminDto dto, CancellationToken ct)
    {
        var result = await _mediator.Send(new UpdateAdminProductCommand { ProductId = productId, Request = dto }, ct);
        return Ok(result);
    }

    [HttpDelete("{productId:int}")]
    public async Task<IActionResult> SoftDelete(int productId, CancellationToken ct)
    {
        await _mediator.Send(new SoftDeleteAdminProductCommand(productId), ct);
        return NoContent();
    }
}