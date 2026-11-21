using practica01.Shared.Domain.Model.Errors;
namespace practica01.Services.Domain.Model.Errors;

/// <summary>Defines rental order business conflicts.</summary>
public static class ServicesErrors
{
    public static Error DuplicateOrder => Error.Conflict("RentalOrder.Duplicate", "A rental order already exists for this customer and vehicle.");
    public static Error PlateConflict => Error.Conflict("RentalOrder.PlateConflict", "This plate already belongs to a different vehicle.");
    public static Error ConcurrentWrite => Error.Conflict("RentalOrder.ConcurrentWrite", "A concurrent rental order could not be completed. Please retry.");
}
