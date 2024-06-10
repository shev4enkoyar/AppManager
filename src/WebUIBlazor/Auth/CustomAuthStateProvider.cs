using System.Security.Claims;
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
        List<Claim> claims = await _loginService.GetLoginInfoAsync();

        ClaimsIdentity claimsIdentity = claims.Count != 0
            ? new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role)
            : new ClaimsIdentity();
        ClaimsPrincipal claimsPrincipal = new(claimsIdentity);
        return new AuthenticationState(claimsPrincipal);
    }
}
