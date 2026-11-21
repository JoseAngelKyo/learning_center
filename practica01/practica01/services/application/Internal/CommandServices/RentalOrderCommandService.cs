using practica01.Services.Application.CommandServices;
using practica01.Services.Domain.Model.Aggregates;
using practica01.Services.Domain.Model.Commands;
using practica01.Services.Domain.Repositories;
using practica01.Shared.Application.Model;
using practica01.Shared.Domain.Model.Errors;
namespace practica01.Services.Application.Internal.CommandServices;

/// <summary>Validates and creates rental orders.</summary>
public class RentalOrderCommandService(IRentalOrderRepository repository) : IRentalOrderCommandService
{
    public async Task<Result<RentalOrder>> Handle(CreateRentalOrderCommand command, CancellationToken cancellationToken = default)
    {
        RentalOrder order;
        try { order = new RentalOrder(command); }
        catch (ArgumentException exception)
        {
            return Result<RentalOrder>.Failure(Error.Validation("RentalOrder.Invalid", exception.Message));
        }
        var error = await repository.CreateIfAvailableAsync(order, cancellationToken);
        return error is null ? Result<RentalOrder>.Success(order) : Result<RentalOrder>.Failure(error);
    }
}
