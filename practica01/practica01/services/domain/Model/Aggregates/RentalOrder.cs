using practica01.Services.Domain.Model.Commands;
using practica01.Services.Domain.Model.ValueObjects;
using practica01.Shared.Domain.Model.Aggregates.Internal;
using practica01.Shared.Domain.Model.Entities;

namespace practica01.Services.Domain.Model.Aggregates;

/// <summary>Represents an auditable rental order and protects its business invariants.</summary>
public class RentalOrder : AggregateRoot, IAuditableEntity
{
    public int RentalOrderId { get; private set; }
    public string Customer { get; private set; } = string.Empty;
    public EVehicles VehiclesId { get; private set; }
    public string Plate { get; private set; } = string.Empty;
    public DateTime RequestedAt { get; private set; }
    public Address Address { get; private set; } = null!;
    public int Amount { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    private RentalOrder() { }

    /// <summary>Creates a valid rental order using the current system date.</summary>
    /// <param name="command">The rental order data.</param>
    public RentalOrder(CreateRentalOrderCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.Customer) || command.Customer.Trim().Length > 90)
            throw new ArgumentException("Customer is required and must not exceed 90 characters.");
        if (!Enum.IsDefined(typeof(EVehicles), command.VehiclesId))
            throw new ArgumentException("VehiclesId must be between 1 and 6.");
        if (string.IsNullOrWhiteSpace(command.Plate) || command.Plate.Trim().Length > 12)
            throw new ArgumentException("Plate is required and must not exceed 12 characters.");
        if (command.RequestedAt.Date < DateTime.Today)
            throw new ArgumentException("RequestedAt cannot be earlier than the current system date.");
        if (command.Amount <= 0)
            throw new ArgumentException("Amount must be greater than zero.");

        Customer = command.Customer.Trim();
        VehiclesId = (EVehicles)command.VehiclesId;
        Plate = command.Plate.Trim().ToUpperInvariant();
        RequestedAt = command.RequestedAt;
        Address = new Address(command.Street, command.City, command.PostalCode);
        Amount = command.Amount;
    }
}
