using Domain.DTO_s.Prescriptions;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.Prescription.Commands;

public sealed class CreatePrescriptionCommand : IRequest<PrescriptionDto>
{
    public int CustomerId { get; set; }
    public CreatePrescriptionDto Request { get; set; } = null!;
}

public sealed class CreatePrescriptionCommandHandler : IRequestHandler<CreatePrescriptionCommand, PrescriptionDto>
{
    private readonly IPrescriptionService _prescriptionService;

    public CreatePrescriptionCommandHandler(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    public async Task<PrescriptionDto> Handle(CreatePrescriptionCommand command, CancellationToken ct)
    {
        return await _prescriptionService.CreateAsync(command.CustomerId, command.Request, ct);
    }
}
