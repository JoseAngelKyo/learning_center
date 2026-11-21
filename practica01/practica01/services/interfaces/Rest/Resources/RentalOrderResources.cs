using System.ComponentModel.DataAnnotations;
namespace practica01.Services.Interfaces.Rest.Resources;

/// <summary>Contains the required owned address components.</summary>
public record AddressResource(
    [Required, StringLength(40)] string Street,
    [Required, StringLength(40)] string City,
    [Required, StringLength(40)] string PostalCode);

/// <summary>Contains the input for creating a rental order.</summary>
public record CreateRentalOrderResource(
    [Required, StringLength(90)] string Customer,
    [Range(1, 6)] int VehiclesId,
    [Required, StringLength(12)] string Plate,
    [Required] DateTime RequestedAt,
    [Required] AddressResource Address,
    [Range(1, int.MaxValue)] int Amount);

/// <summary>Represents a public rental order, excluding amount and audit fields.</summary>
public record RentalOrderResource(int RentalOrderId, string Customer, int VehiclesId,
    string Plate, DateTime RequestedAt, AddressResource Address);

