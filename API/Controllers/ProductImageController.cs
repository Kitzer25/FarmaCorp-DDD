using Application.UseCases.ProductImageUseCase.Commands;
using Application.UseCases.ProductImageUseCase.Queries;
using Domain.Commons;
using Domain.DTO_s.ProductImage;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Authorize(Policy = PolicyNames.ManagerAccess)]
[Route("api/v1/admin/products")]
public sealed class ProductImageController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductImageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{productId:int}/images")]
    [ProducesResponseType(typeof(IEnumerable<ProductImageDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByProduct(int productId, CancellationToken ct)
    {
        var result = await _mediator.Send(new GETProductImagesQuery { ProductId = productId }, ct);
        return Ok(result);
    }

    [HttpPost("{productId:int}/images")]
    [ProducesResponseType(typeof(ProductImageDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> AddImage(int productId, [FromBody] CreateProductImageDto request, CancellationToken ct)
    {
        request.ProductId = productId;
        var result = await _mediator.Send(new POSTAddProductImageCommand { Request = request }, ct);
        return CreatedAtAction(nameof(GetByProduct), new { productId }, result);
    }

    [HttpPatch("images/{productImageId:int}/main")]
    [ProducesResponseType(typeof(ProductImageDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> SetMainImage(int productImageId, CancellationToken ct)
    {
        var result = await _mediator.Send(new PATCHSetMainProductImageCommand { ProductImageId = productImageId }, ct);
        return Ok(result);
    }

    [HttpDelete("images/{productImageId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteImage(int productImageId, CancellationToken ct)
    {
        var deleted = await _mediator.Send(new DELETEProductImageCommand { ProductImageId = productImageId }, ct);
        return deleted ? NoContent() : NotFound();
    }
}
