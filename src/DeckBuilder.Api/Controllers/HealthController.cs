namespace DeckBuilder.Api.Controllers;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Provides a simple health-check endpoint for the API.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public sealed class HealthController : ControllerBase
{
    /// <summary>
    /// Returns a simple status payload indicating that the API is running.
    /// </summary>
    /// <returns>A 200 OK result with health metadata.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        var payload = new
        {
            status = "ok",
            service = "DeckBuilder.Api",
            timestampUtc = DateTime.UtcNow,
        };

        return this.Ok(payload);
    }
}