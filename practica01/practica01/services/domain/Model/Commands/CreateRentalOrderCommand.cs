namespace practica01.Services.Domain.Model.Commands;

/// <summary>Requests creation of a rental order.</summary>
public record CreateRentalOrderCommand(string Customer, int VehiclesId, string Plate,
    DateTime RequestedAt, string Street, string City, string PostalCode, int Amount);
