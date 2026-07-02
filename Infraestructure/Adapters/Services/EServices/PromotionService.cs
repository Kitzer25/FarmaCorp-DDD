using Domain.Ports;
using Domain.Ports.Services;
using Domain.DTO_s.Promotions;
using Domain.Entities;
using Domain.Ports.Repositories;
using Domain.Ports.Services.EServices;

namespace Infraestructure.Adapters.Services.EServices;

public class PromotionService : IPromotionService
{
    private readonly IUnitOfWork _unitOfWork;

    public PromotionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<PromotionDto>> GetActiveAsync(CancellationToken ct)
    {
        var promotions = await _unitOfWork.PromotionRepo.GetActiveAsync(ct);

        return promotions.Select(ToDto);
    }

    public async Task<PromotionDto> CreateAsync(CreatePromotionDto request, CancellationToken ct)
    {
        ValidatePromotion(request);

        foreach (var code in request.Codes.Select(NormalizeCode))
        {
            if (await _unitOfWork.PromotionCodeRepo.ExistsByCodeAsync(code, ct))
            {
                throw new InvalidOperationException($"El cupón {code} ya existe.");
            }
        }

        var promotion = new Promotion
        {
            name = request.Name.Trim(),
            description = request.Description,
            discount_type_id = request.DiscountTypeId,
            discount_value = request.DiscountValue,
            min_order_amount = request.MinOrderAmount,
            max_discount_amount = request.MaxDiscountAmount,
            max_uses = request.MaxUses,
            current_uses = 0,
            applies_to_all = request.AppliesToAll,
            start_date = request.StartDate,
            end_date = request.EndDate,
            is_active = true,
            created_at = DateTime.UtcNow
        };

        await _unitOfWork.PromotionRepo.AddAsync(promotion, ct);

        foreach (var code in request.Codes.Select(NormalizeCode).Distinct())
        {
            await _unitOfWork.PromotionCodeRepo.AddAsync(new PromotionCode
            {
                promotion_id = promotion.promotion_id,
                code = code,
                max_uses = request.MaxUses,
                current_uses = 0,
                is_active = true,
                created_at = DateTime.UtcNow
            }, ct);
        }

        var saved = await _unitOfWork.PromotionRepo.GetByIdAsync(promotion.promotion_id, ct) ?? promotion;
        return ToDto(saved);
    }

    public async Task<CouponValidationDto> ValidateCouponAsync(string code, decimal orderSubtotal, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return Invalid("El cupón es obligatorio.", orderSubtotal);
        }

        var coupon = await _unitOfWork.PromotionCodeRepo.GetActiveByCodeWithPromotionAsync(code, ct);

        if (coupon == null)
        {
            return Invalid("Cupón inválido.", orderSubtotal, code);
        }

        var promotion = coupon.promotion;
        var now = DateTime.UtcNow;

        if (!promotion.is_active || now < promotion.start_date || (promotion.end_date.HasValue && now > promotion.end_date.Value))
        {
            return Invalid("Cupón vencido o inactivo.", orderSubtotal, coupon.code);
        }

        if (promotion.max_uses.HasValue && promotion.current_uses >= promotion.max_uses.Value)
        {
            return Invalid("La promoción ya alcanzó su límite de usos.", orderSubtotal, coupon.code);
        }

        if (coupon.max_uses.HasValue && coupon.current_uses >= coupon.max_uses.Value)
        {
            return Invalid("El cupón ya alcanzó su límite de usos.", orderSubtotal, coupon.code);
        }

        if (promotion.min_order_amount.HasValue && orderSubtotal < promotion.min_order_amount.Value)
        {
            return Invalid("El monto mínimo para este cupón no se cumple.", orderSubtotal, coupon.code);
        }

        var discount = CalculateDiscount(promotion, orderSubtotal);

        return new CouponValidationDto
        {
            IsValid = true,
            Message = "Cupón válido.",
            Code = coupon.code,
            DiscountAmount = discount,
            OrderTotalAfterDiscount = Math.Max(0, orderSubtotal - discount)
        };
    }

    private static void ValidatePromotion(CreatePromotionDto request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new InvalidOperationException("El nombre de la promoción es obligatorio.");
        }

        if (request.DiscountValue <= 0)
        {
            throw new InvalidOperationException("El descuento debe ser mayor a 0.");
        }

        if (request.EndDate.HasValue && request.EndDate.Value < request.StartDate)
        {
            throw new InvalidOperationException("La fecha final no puede ser menor a la fecha inicial.");
        }
    }

    private static decimal CalculateDiscount(Promotion promotion, decimal subtotal)
    {
        var discountType = promotion.discount_type?.name?.ToLowerInvariant() ?? "";
        var discount = discountType.Contains("percent") || discountType.Contains("porcentaje")
            ? subtotal * (promotion.discount_value / 100m)
            : promotion.discount_value;

        if (promotion.max_discount_amount.HasValue)
        {
            discount = Math.Min(discount, promotion.max_discount_amount.Value);
        }

        return decimal.Round(Math.Min(discount, subtotal), 2);
    }

    private static PromotionDto ToDto(Promotion promotion)
    {
        return new PromotionDto
        {
            PromotionId = promotion.promotion_id,
            Name = promotion.name,
            Description = promotion.description,
            DiscountTypeId = promotion.discount_type_id,
            DiscountValue = promotion.discount_value,
            MinOrderAmount = promotion.min_order_amount,
            MaxDiscountAmount = promotion.max_discount_amount,
            MaxUses = promotion.max_uses,
            CurrentUses = promotion.current_uses,
            StartDate = promotion.start_date,
            EndDate = promotion.end_date,
            IsActive = promotion.is_active,
            Codes = promotion.promotion_codes.Select(c => c.code).ToList()
        };
    }

    private static CouponValidationDto Invalid(string message, decimal subtotal, string? code = null)
    {
        return new CouponValidationDto
        {
            IsValid = false,
            Message = message,
            Code = code,
            DiscountAmount = 0,
            OrderTotalAfterDiscount = subtotal
        };
    }

    private static string NormalizeCode(string code)
    {
        return code.Trim().ToUpperInvariant();
    }
}
