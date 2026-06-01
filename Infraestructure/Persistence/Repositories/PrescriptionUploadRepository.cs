using Core.Entities;
using Core.Ports.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence.Repositories;

public class PrescriptionUploadRepository : 
    GRepositories<PrescriptionUpload>,
    IPrescriptionUploadRepository
{ 
    public PrescriptionUploadRepository(AppDbContext context) : base(context)
    { }

    public async Task<IEnumerable<PrescriptionUpload>> GetByCustomerIdAsync(int customerId, CancellationToken ct)
    {
        return await _context.prescription_uploads
            .AsNoTracking()
            .Where(p => p.customer_id == customerId)
            .OrderByDescending(p => p.created_at)
            .ToListAsync(ct);
    }

    public async Task<PrescriptionUpload?> GetByIdForCustomerAsync(int prescriptionId, int customerId, CancellationToken ct)
    {
        return await _context.prescription_uploads
            .FirstOrDefaultAsync(p => p.prescription_id == prescriptionId && p.customer_id == customerId, ct);
    }
}
