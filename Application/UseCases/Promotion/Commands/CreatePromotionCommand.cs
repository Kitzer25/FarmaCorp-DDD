using Domain.DTO_s.Promotions;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.Promotion.Commands;

public sealed class CreatePromotionCommand : IRequest<PromotionDto>
{
    public CreatePromotionDto Request { get; set; } = null!;
}

public sealed class CreatePromotionCommandHandler : IRequestHandler<CreatePromotionCommand, PromotionDto>
{
    private readonly IPromotionService _promotionService;

    public CreatePromotionCommandHandler(IPromotionService promotionService)
    {
        _promotionService = promotionService;
    }

    public async Task<PromotionDto> Handle(CreatePromotionCommand command, CancellationToken ct)
    {
        return await _promotionService.CreateAsync(command.Request, ct);
    }
}
