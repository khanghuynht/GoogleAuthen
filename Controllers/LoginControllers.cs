using GoogleAuthenticationLogin.Models;

namespace GoogleAuthenticationLogin.Controllers;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;


[ApiController]
public class LoginControllers : ControllerBase 
{
    public LoginControllers()
    {
        
    }
    
    [HttpGet("login-google")]
    public async Task<IActionResult> LoginGoogle()
    {
        await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, 
            new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogelResposne")
            });
        return Ok();    
    }
    
    [HttpGet("response-google")]
    public async Task<IActionResult> GoogelResposne()
    {
        var authResult = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
        var claims = authResult.Principal.Claims;
        var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        var name = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        var picture = claims.FirstOrDefault(c => c.Type == "picture")?.Value;
        var id = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var country = claims.FirstOrDefault(c=>c.Type == ClaimTypes.Country)?.Value;
        var actor = claims.FirstOrDefault(c=>c.Type == ClaimTypes.Actor)?.Value;
        var expired = claims.FirstOrDefault(c=>c.Type == ClaimTypes.Expired)?.Value;
        var expiration = claims.FirstOrDefault(c=>c.Type == ClaimTypes.Expiration)?.Value;
        var authentication = claims.FirstOrDefault(c=>c.Type == ClaimTypes.Authentication)?.Value;
        var phone = claims.FirstOrDefault(c=>c.Type == ClaimTypes.MobilePhone)?.Value;
        var gender = claims.FirstOrDefault(c=>c.Type == ClaimTypes.Gender)?.Value;
        var token = authResult.Properties.GetTokenValue("access_token");
        var refreshToken = authResult.Properties.GetTokenValue("refresh_token");
        var expiresAt = authResult.Properties.GetTokenValue("expires_at");
        var userInformation = new UserInformation
        {
            Email = email,
            Name = name,
            Picture = picture,
            Id = id,
            Token = token,
            RefreshToken = refreshToken,
            ExpiresAt = expiresAt,
            Country = country,
            Actor = actor,
            Expired = expired,
            Expiration = expiration,
            Authentication = authentication,
            Phone = phone,
            Gender = gender
        };
        return Ok(userInformation);
        // Do something with the user information
    }
    
}