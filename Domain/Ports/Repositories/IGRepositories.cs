namespace Core.Ports;

public interface IGRepositories<T>
    where T : class 
{
    Task<IEnumerable<T>> GetAllAsync(CancellationToken ct);
    Task<T?> GetByIdAsync(int id, CancellationToken ct);
    Task AddAsync(T entity, CancellationToken ct);
    Task UpdateAsync(int id, T entity, CancellationToken ct);
    Task<bool> DeleteAsync(T entity, CancellationToken ct);
}