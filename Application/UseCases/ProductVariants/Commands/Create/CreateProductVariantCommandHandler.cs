using MediatR;
// Asegúrate de importar tus entidades y tu DbContext/Repositorio

namespace FarmaCorp.Application.UseCases.ProductVariants.Commands.Create;

public class CreateProductVariantCommandHandler : IRequestHandler<CreateProductVariantCommand, int>
{
    // Reemplaza esto con tu DbContext o IUnitOfWork real
    // private readonly AppDbContext _context; 

    // public CreateProductVariantCommandHandler(AppDbContext context) { _context = context; }

    public async Task<int> Handle(CreateProductVariantCommand request, CancellationToken cancellationToken)
    {
        // AQUÍ VA LA REGLA: SKU Único
        // var skuExists = await _context.ProductVariants.AnyAsync(v => v.Sku == request.Sku, cancellationToken);
        // if (skuExists) throw new Exception("Ya existe una variante con este SKU.");

        // 1. Mapear el Command a la Entidad
        /*
        var variant = new ProductVariant {
            ProductId = request.ProductId,
            Sku = request.Sku,
            Price = request.Price,
            // ... mapear el resto de propiedades
        };
        */

        // 2. Guardar en base de datos
        // _context.ProductVariants.Add(variant);
        // await _context.SaveChangesAsync(cancellationToken);

        // 3. El trigger de la BD creará el inventario en 0 automáticamente.
        return 1; // Retornar variant.Id real
    }
}