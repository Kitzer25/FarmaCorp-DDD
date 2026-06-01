using System.Text;
using Application.AdminProducts.Dtos;
using Application.Audit.Services;
using Core.Entities;
using Core.Ports;

namespace Application.AdminProducts.Services;

public class AdminProductService : IAdminProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuditService _auditService;

    public AdminProductService(IUnitOfWork unitOfWork, IAuditService auditService)
    {
        _unitOfWork = unitOfWork;
        _auditService = auditService;
    }

    public async Task<ProductAdminDto> CreateAsync(SaveProductAdminDto request, CancellationToken ct)
    {
        Validate(request);

        var slug = BuildSlug(request);
        await EnsureSlugAvailableAsync(slug, null, ct);

        var product = new Product
        {
            name = request.Name.Trim(),
            generic_name = request.GenericName,
            description = request.Description,
            short_description = request.ShortDescription,
            category_id = request.CategoryId,
            laboratory_id = request.LaboratoryId,
            requires_prescription = request.RequiresPrescription,
            is_controlled = request.IsControlled,
            active_ingredient = request.ActiveIngredient,
            slug = slug,
            tags = request.Tags,
            is_active = true,
            created_at = DateTime.UtcNow
        };

        await _unitOfWork.ProductRepo.AddAsync(product, ct);
        await _auditService.RegisterAsync("products", product.product_id.ToString(), "CREATE", null, ToDto(product), null, null, ct);

        return ToDto(product);
    }

    public async Task<ProductAdminDto> UpdateAsync(int productId, SaveProductAdminDto request, CancellationToken ct)
    {
        Validate(request);

        var product = await _unitOfWork.ProductRepo.GetByIdAsync(productId, ct);

        if (product == null || product.deleted_at != null)
        {
            throw new InvalidOperationException("El producto no existe.");
        }

        var slug = BuildSlug(request);
        await EnsureSlugAvailableAsync(slug, productId, ct);

        var before = ToDto(product);
        product.name = request.Name.Trim();
        product.generic_name = request.GenericName;
        product.description = request.Description;
        product.short_description = request.ShortDescription;
        product.category_id = request.CategoryId;
        product.laboratory_id = request.LaboratoryId;
        product.requires_prescription = request.RequiresPrescription;
        product.is_controlled = request.IsControlled;
        product.active_ingredient = request.ActiveIngredient;
        product.slug = slug;
        product.tags = request.Tags;
        product.updated_at = DateTime.UtcNow;

        await _unitOfWork.ProductRepo.UpdateAsync(product.product_id, product, ct);
        await _auditService.RegisterAsync("products", product.product_id.ToString(), "UPDATE", before, ToDto(product), null, null, ct);

        return ToDto(product);
    }

    public async Task SoftDeleteAsync(int productId, CancellationToken ct)
    {
        var product = await _unitOfWork.ProductRepo.GetByIdAsync(productId, ct);

        if (product == null || product.deleted_at != null)
        {
            throw new InvalidOperationException("El producto no existe.");
        }

        var before = ToDto(product);
        product.is_active = false;
        product.deleted_at = DateTime.UtcNow;
        product.updated_at = DateTime.UtcNow;

        await _unitOfWork.ProductRepo.UpdateAsync(product.product_id, product, ct);
        await _auditService.RegisterAsync("products", product.product_id.ToString(), "SOFT_DELETE", before, ToDto(product), null, null, ct);
    }

    private async Task EnsureSlugAvailableAsync(string slug, int? currentProductId, CancellationToken ct)
    {
        var existing = await _unitOfWork.ProductRepo.GetBySlugAsync(slug, ct);

        if (existing != null && existing.product_id != currentProductId)
        {
            throw new InvalidOperationException("Ya existe un producto con ese slug.");
        }
    }

    private static void Validate(SaveProductAdminDto request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new InvalidOperationException("El nombre del producto es obligatorio.");
        }

        if (request.CategoryId <= 0 || request.LaboratoryId <= 0)
        {
            throw new InvalidOperationException("La categoría y el laboratorio son obligatorios.");
        }
    }

    private static string BuildSlug(SaveProductAdminDto request)
    {
        return string.IsNullOrWhiteSpace(request.Slug)
            ? Slugify(request.Name)
            : Slugify(request.Slug);
    }

    private static string Slugify(string value)
    {
        var builder = new StringBuilder();

        foreach (var ch in value.Trim().ToLowerInvariant())
        {
            if (char.IsLetterOrDigit(ch))
            {
                builder.Append(ch);
            }
            else if (builder.Length > 0 && builder[^1] != '-')
            {
                builder.Append('-');
            }
        }

        return builder.ToString().Trim('-');
    }

    private static ProductAdminDto ToDto(Product product)
    {
        return new ProductAdminDto
        {
            ProductId = product.product_id,
            Name = product.name,
            GenericName = product.generic_name,
            Description = product.description,
            ShortDescription = product.short_description,
            CategoryId = product.category_id,
            LaboratoryId = product.laboratory_id,
            RequiresPrescription = product.requires_prescription,
            IsControlled = product.is_controlled,
            ActiveIngredient = product.active_ingredient,
            Slug = product.slug,
            Tags = product.tags,
            IsActive = product.is_active
        };
    }
}
