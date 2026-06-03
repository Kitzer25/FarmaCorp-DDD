using Microsoft.AspNetCore.Mvc;
using MediatR;
using FarmaCorp.Application.UseCases.ProductVariants.Commands.Create;
using FarmaCorp.Application.UseCases.ProductVariants.Commands.Update;

namespace FarmaCorp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductVariantsController : ControllerBase
{
    private readonly ISender _sender;

    public ProductVariantsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductVariantCommand command)
    {
        var resultId = await _sender.Send(command);
        return Ok(new { message = "Variante creada", id = resultId });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductVariantCommand command)
    {
        if (id != command.ProductVariantId) return BadRequest("El ID de la ruta no coincide con el payload.");
        
        var success = await _sender.Send(command);
        return Ok(new { message = "Variante actualizada", success });
    }
}