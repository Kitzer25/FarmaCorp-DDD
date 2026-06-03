using FarmaCorp.Application.UseCases.ProductImages.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmaCorp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductImagesController : ControllerBase
{
    private readonly ISender _sender;

    public ProductImagesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductImageCommand command)
    {
        var imageId = await _sender.Send(command);
        return Ok(new { message = "Imagen guardada correctamente", id = imageId });
    }
}