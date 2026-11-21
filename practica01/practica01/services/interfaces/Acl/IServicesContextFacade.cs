using practica01.Shared.Application.Model;
namespace practica01.Services.Interfaces.Acl;

/// <summary>Defines the services context boundary for future context integrations.</summary>
public interface IServicesContextFacade
{
    Task<Result<RentalOrderContextResource>> CreateRentalOrderAsync(CreateRentalOrderContextResource request, CancellationToken cancellationToken = default);
}

/// <summary>Provides an independent contract for calls from another context.</summary>
public record CreateRentalOrderContextResource(string Customer, int VehiclesId, string Plate,
    DateTime RequestedAt, string Street, string City, string PostalCode, int Amount);

/// <summary>Exposes public order data across the context boundary.</summary>
public record RentalOrderContextResource(int RentalOrderId, string Customer, int VehiclesId,
    string Plate, DateTime RequestedAt, string Street, string City, string PostalCode);
