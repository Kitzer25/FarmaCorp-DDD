using Application.UseCases.Product.Querys;
using Core.DTO_s.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/v1/products")]
public sealed class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] ProductQueryParams queryParams, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetProductsQuery { Params = queryParams }, ct);
        return Ok(result);
    }

    [HttpGet("{slug}")]
    public async Task<IActionResult> GetDetail(string slug, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetProductDetailQuery { Slug = slug}, ct);
        return Ok(result);
    }

    [HttpGet("categories")]
    public async Task<IActionResult> GetCategories(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetCategoriesQuery(), ct);
        return Ok(result);
    }
}