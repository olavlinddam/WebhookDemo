using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace WebhookDemo.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController: ControllerBase 
{
    private readonly ILogger<AuthenticationController> _logger;

    public AuthenticationController(ILogger<AuthenticationController> logger)
    {
        _logger = logger;
    }
    
    [HttpPost("login")]
    public IActionResult Login(string userName, string password)
    {
        var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        try
        {
            if (userName == "username" && password == "password")
            {
                // Generate and return JWT token
                var token = GenerateJwtToken();

                _logger.LogInformation("User '{user}' has logged in from ip: {ip}.", userName, ip);
                return Ok(new { Token = token });
            }
            else
            {
                _logger.LogInformation("Invalid login attempt from ip: {ip}", ip);
                return Unauthorized("Invalid username or password");
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Something went wrong when logging user in from ip: {ip}", ip);
            return StatusCode(500, "Internal Server Error");
        }
    }

    
    
    private string GenerateJwtToken()
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication")); // Replace with your secret key
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "https://localhost:5001",          // Replace with your issuer
            audience: "https://localhost:5001",      // Replace with your audience
            claims: new[] { new Claim(JwtRegisteredClaimNames.Sub, "username") },
            expires: DateTime.UtcNow.AddHours(1), // Token expiration time
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}