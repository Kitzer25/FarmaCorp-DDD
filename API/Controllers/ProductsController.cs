using Application.UseCases.ProductUseCase.Queries;
using Application.UseCases.CategoryUseCase.Queries;
using Domain.DTO_s.Products;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/v1/products")]
public sealed class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
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

    [HttpGet("{slug}")]
    [ProducesResponseType(typeof(ProductDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDetail(string slug, CancellationToken ct)
    {
        var result = await _mediator.Send(new GETProductDetailQuery { Slug = slug}, ct);
        return Ok(result);
    }

    [HttpGet("categories")]
    [ProducesResponseType(typeof(List<Domain.DTO_s.Products.CategoryDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategories(CancellationToken ct)
    {
        var result = await _mediator.Send(new GETCategoriesQuery(), ct);
        return Ok(result);
    }
}