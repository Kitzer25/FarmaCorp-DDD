using MediatR;

namespace FarmaCorp.Application.UseCases.ProductVariants.Commands.Update;

public class UpdateProductVariantCommandHandler : IRequestHandler<UpdateProductVariantCommand, bool>
{
    public async Task<bool> Handle(UpdateProductVariantCommand request, CancellationToken cancellationToken)
    {
        // 1. Buscar la variante en la BD por request.ProductVariantId
        // 2. REGLA: No duplicar SKU (Verificar que el nuevo SKU no lo tenga otra variante DISTINTA a esta)
        // var skuExists = await _context.ProductVariants.AnyAsync(v => v.Sku == request.Sku && v.Id != request.ProductVariantId);
        // if (skuExists) throw new Exception("El SKU ya está en uso.");
        
        // 3. Actualizar los campos (Price, Sku, Concentration, UnitId, IsActive)
        // 4. await _context.SaveChangesAsync();
        
        return true;
    }
}