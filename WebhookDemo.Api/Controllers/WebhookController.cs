using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebhookDemo.Api.Controllers;

[ApiController, Authorize]
[Route("[controller]")]
public class WebhookController : ControllerBase
{
    
    private readonly ILogger<WebhookController> _logger;

    public WebhookController(ILogger<WebhookController> logger)
    {
        _logger = logger;
    }
    
    
    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogInformation("This is an informational message from the WebhookController.");
        _logger.LogWarning("This is a warning message from the WebhookController.");

        // Your controller logic...

        return Ok("WebhookController action executed successfully.");
    }
}