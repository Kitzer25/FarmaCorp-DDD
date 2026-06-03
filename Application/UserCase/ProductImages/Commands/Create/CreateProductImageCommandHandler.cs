using MediatR;

namespace FarmaCorp.Application.UseCases.ProductImages.Commands.Create;

public class CreateProductImageCommandHandler : IRequestHandler<CreateProductImageCommand, int>
{
    public async Task<int> Handle(CreateProductImageCommand request, CancellationToken cancellationToken)
    {
        // Aquí luego inyectaremos la base de datos para guardar la URL.
        // Por ahora retornamos un ID simulado para que compile.
        return 1; 
    }
}