using practica01.Services.Application.CommandServices;
using practica01.Services.Domain.Model.Commands;
using practica01.Services.Interfaces.Acl;
using practica01.Shared.Application.Model;
namespace practica01.Services.Application.Acl;

/// <summary>Translates external context contracts into services commands and public data.</summary>
public class ServicesContextFacade(IRentalOrderCommandService commandService) : IServicesContextFacade
{
    public async Task<Result<RentalOrderContextResource>> CreateRentalOrderAsync(CreateRentalOrderContextResource request, CancellationToken cancellationToken = default)
    {
        var command = new CreateRentalOrderCommand(request.Customer, request.VehiclesId, request.Plate,
            request.RequestedAt, request.Street, request.City, request.PostalCode, request.Amount);
        var result = await commandService.Handle(command, cancellationToken);
        if (result.IsFailure) return Result<RentalOrderContextResource>.Failure(result.DomainError);
        var order = result.Value!;
        return Result<RentalOrderContextResource>.Success(new RentalOrderContextResource(order.RentalOrderId,
            order.Customer, (int)order.VehiclesId, order.Plate, order.RequestedAt,
            order.Address.Street, order.Address.City, order.Address.PostalCode));
    }
}
