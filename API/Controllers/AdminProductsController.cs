using Application.AdminProducts.Dtos;
using Application.AdminProducts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Core.Constants;

namespace API.Controllers;

[ApiController]
[Authorize(Policy = PolicyNames.SalesAccess)]
[Route("api/v1/admin/products")]
public class AdminProductsController : ControllerBase
{
    private readonly IAdminProductService _productService;

    public AdminProductsController(IAdminProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(SaveProductAdminDto request, CancellationToken ct)
    {
        try
        {
            var result = await _productService.CreateAsync(request, ct);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{productId:int}")]
    public async Task<IActionResult> Update(int productId, SaveProductAdminDto request, CancellationToken ct)
    {
        try
        {
            var result = await _productService.UpdateAsync(productId, request, ct);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{productId:int}")]
    public async Task<IActionResult> SoftDelete(int productId, CancellationToken ct)
    {
        try
        {
            await _productService.SoftDeleteAsync(productId, ct);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
