using Domain.DTO_s.Prescriptions;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.PrescriptionUseCase.Commands;

public sealed class POSTCreatePrescriptionCommand : IRequest<PrescriptionDto>
{
    public int CustomerId { get; set; }
    public CreatePrescriptionDto Request { get; set; } = null!;
}

public sealed class POSTCreatePrescriptionCommandHandler : IRequestHandler<POSTCreatePrescriptionCommand, PrescriptionDto>
{
    private readonly IPrescriptionService _prescriptionService;

    public POSTCreatePrescriptionCommandHandler(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    public async Task<PrescriptionDto> Handle(POSTCreatePrescriptionCommand command, CancellationToken ct)
    {
        return await _prescriptionService.CreateAsync(command.CustomerId, command.Request, ct);
    }
}
