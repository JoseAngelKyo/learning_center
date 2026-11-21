using practica01.Services.Domain.Model.Aggregates;
using practica01.Shared.Domain.Model.Errors;
using practica01.Shared.Domain.Repositories;
namespace practica01.Services.Domain.Repositories;

/// <summary>Provides rental order persistence with atomic uniqueness checks.</summary>
public interface IRentalOrderRepository : IBaseRepository<RentalOrder>
{
    /// <summary>Checks cross-order rules and saves atomically; returns a conflict if rejected.</summary>
    Task<Error?> CreateIfAvailableAsync(RentalOrder order, CancellationToken cancellationToken = default);
}
