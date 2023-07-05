using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shipyard.Api.Controllers;

[Authorize(Roles = "admin")]
[Route("models")]
public class ShipModelsController: ApiController
{
    [HttpGet("")]
    public IActionResult MyModels()
    {
        return Ok(new[]{1, 2, 3});
    }
}