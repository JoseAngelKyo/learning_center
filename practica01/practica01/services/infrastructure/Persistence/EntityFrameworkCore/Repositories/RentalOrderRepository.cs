using System.Data;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using practica01.Services.Domain.Model.Aggregates;
using practica01.Services.Domain.Model.Errors;
using practica01.Services.Domain.Repositories;
using practica01.Shared.Domain.Model.Errors;
using practica01.Shared.Domain.Repositories;
using practica01.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using practica01.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
namespace practica01.Services.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>Persists orders using serializable checks to protect concurrent requests.</summary>
public class RentalOrderRepository(AppDbContext context, IUnitOfWork unitOfWork)
    : BaseRepository<RentalOrder>(context), IRentalOrderRepository
{
    public async Task<Error?> CreateIfAvailableAsync(RentalOrder order, CancellationToken cancellationToken = default)
    {
        await using var transaction = await Context.Database.BeginTransactionAsync(IsolationLevel.Serializable, cancellationToken);
        try
        {
            var orders = Context.Set<RentalOrder>();
            if (await orders.AnyAsync(item => item.Customer == order.Customer && item.VehiclesId == order.VehiclesId, cancellationToken))
                return ServicesErrors.DuplicateOrder;
            if (await orders.AnyAsync(item => item.Plate == order.Plate && item.VehiclesId != order.VehiclesId, cancellationToken))
                return ServicesErrors.PlateConflict;
            await AddAsync(order, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return null;
        }
        catch (DbUpdateException exception) when (exception.InnerException is MySqlException { Number: 1062 })
        {
            return ServicesErrors.DuplicateOrder;
        }
        catch (Exception exception) when (IsConcurrentWrite(exception))
        {
            return ServicesErrors.ConcurrentWrite;
        }
    }

    private static bool IsConcurrentWrite(Exception exception) =>
        exception is MySqlException { Number: 1213 or 1205 } ||
        exception.InnerException is MySqlException { Number: 1213 or 1205 };
}
