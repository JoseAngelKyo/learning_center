using practica01.Services.Domain.Model.Aggregates;
using practica01.Services.Domain.Model.Commands;
using practica01.Services.Interfaces.Rest.Resources;
namespace practica01.Services.Interfaces.Rest.Transform;

/// <summary>Converts rental resources to commands and aggregates to public resources.</summary>
public static class RentalOrderResourceAssembler
{
    public static CreateRentalOrderCommand ToCommand(CreateRentalOrderResource resource) =>
        new(resource.Customer, resource.VehiclesId, resource.Plate, resource.RequestedAt,
            resource.Address.Street, resource.Address.City, resource.Address.PostalCode, resource.Amount);

    public static RentalOrderResource ToResource(RentalOrder order) =>
        new(order.RentalOrderId, order.Customer, (int)order.VehiclesId, order.Plate, order.RequestedAt,
            new AddressResource(order.Address.Street, order.Address.City, order.Address.PostalCode));
}
