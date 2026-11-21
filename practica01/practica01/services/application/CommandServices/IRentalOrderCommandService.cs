using practica01.Services.Domain.Model.Aggregates;
using practica01.Services.Domain.Model.Commands;
using practica01.Shared.Application.Model;
namespace practica01.Services.Application.CommandServices;

/// <summary>Defines rental order commands.</summary>
public interface IRentalOrderCommandService
{
    Task<Result<RentalOrder>> Handle(CreateRentalOrderCommand command, CancellationToken cancellationToken = default);
}
