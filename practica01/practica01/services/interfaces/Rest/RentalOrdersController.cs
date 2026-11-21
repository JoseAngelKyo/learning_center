using Microsoft.AspNetCore.Mvc;
using practica01.Services.Application.CommandServices;
using practica01.Services.Interfaces.Rest.Resources;
using practica01.Services.Interfaces.Rest.Transform;
using practica01.Shared.Domain.Model.Errors;
namespace practica01.Services.Interfaces.Rest;

/// <summary>Exposes the rental order creation endpoint.</summary>
[ApiController]
[Route("api/v1/rental-orders")]
public class RentalOrdersController(IRentalOrderCommandService commandService) : ControllerBase
{
    /// <summary>Creates an order after validating the Hertz business rules.</summary>
    /// <param name="resource">The customer, vehicle, plate, date, address and amount.</param>
    /// <param name="cancellationToken">The request cancellation token.</param>
    /// <returns>The generated identifier and public order data, or an error.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(RentalOrderResource), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateRentalOrderResource resource, CancellationToken cancellationToken)
    {
        var result = await commandService.Handle(RentalOrderResourceAssembler.ToCommand(resource), cancellationToken);
        if (result.IsFailure)
        {
            var status = result.DomainError.Type == EErrorType.Conflict ? StatusCodes.Status409Conflict : StatusCodes.Status400BadRequest;
            return Problem(statusCode: status, title: result.DomainError.Code, detail: result.DomainError.Message);
        }
        var response = RentalOrderResourceAssembler.ToResource(result.Value!);
        return Created($"{Request.Path}/{response.RentalOrderId}", response);
    }
}
