using Domain.DTO_s.Promotions;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.PromotionUseCase.Commands;

public sealed class POSTCreatePromotionCommand : IRequest<PromotionDto>
{
    public CreatePromotionDto Request { get; set; } = null!;
}

public sealed class POSTCreatePromotionCommandHandler : IRequestHandler<POSTCreatePromotionCommand, PromotionDto>
{
    private readonly IPromotionService _promotionService;

    public POSTCreatePromotionCommandHandler(IPromotionService promotionService)
    {
        _promotionService = promotionService;
    }

    public async Task<PromotionDto> Handle(POSTCreatePromotionCommand command, CancellationToken ct)
    {
        return await _promotionService.CreateAsync(command.Request, ct);
    }
}
