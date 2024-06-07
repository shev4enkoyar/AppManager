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
        var token = await _loginService.GetTokenAsync();
        var identity = string.IsNullOrEmpty(token) ? new ClaimsIdentity() : new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");

        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    public void NotifyUserAuthentication(string token)
    {
        var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
        var user = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public void NotifyUserLogout()
    {
        var identity = new ClaimsIdentity();
        var user = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var claims = new List<Claim>();
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));
        return claims;
    }

    private byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }

    // public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    // {
    //     var claims = await _loginService.GetLoginInfoAsync();
    //     var claimsIdentity = claims.Count != 0 
    //         ? new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme, "name", "role") 
    //         : new ClaimsIdentity();
    //     var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
    //     return new AuthenticationState(claimsPrincipal);
    // }
}
