using Application.Products.Commands;
using Application.Products.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/v1/products")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts(
        [FromQuery] string? search,
        [FromQuery] int? categoryId,
        [FromQuery] int? laboratoryId,
        [FromQuery] string? activeIngredient,
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice,
        CancellationToken ct)
    {
        try
        {
            var result = await _mediator.Send(new GetProductsCommand
            {
                Params = new ProductQueryParams
                {
                    Search          = search,
                    CategoryId      = categoryId,
                    LaboratoryId    = laboratoryId,
                    ActiveIngredient = activeIngredient,
                    MinPrice        = minPrice,
                    MaxPrice        = maxPrice
                }
            }, ct);

            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    [HttpGet("{slug}")]
    public async Task<IActionResult> GetProductDetail(string slug, CancellationToken ct)
    {
        try
        {
            var result = await _mediator.Send(new GetProductDetailCommand { Slug = slug }, ct);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}