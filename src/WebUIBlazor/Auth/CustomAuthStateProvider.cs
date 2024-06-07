using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;

namespace WebUIBlazor.Auth;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly LoginService _loginService;

    public CustomAuthStateProvider(LoginService loginService)
    {
        _loginService = loginService;
    }
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var claims = await _loginService.GetLoginInfoAsync();
        
        var claimsIdentity = claims.Count() != 0 
            ? new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme, "name", "role") 
            : new ClaimsIdentity();
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        return new AuthenticationState(claimsPrincipal);
    }
}
