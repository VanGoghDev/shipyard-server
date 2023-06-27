using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace Shipyard.Api.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
    // GET
    protected IActionResult Problem(List<Error> errors)
    {
        var error = errors[0];
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };
        return Problem(statusCode: statusCode, detail: error.Description);
    }
}