using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using WebUIBlazor.Models;
using WebUIBlazor.Services.AppManagerClient;

namespace WebUIBlazor.Auth;

public class LoginService
{
    private const string AccessToken = nameof(AccessToken);
    private const string RefreshToken = nameof(RefreshToken);

    private readonly ProtectedLocalStorage _localStorage;
    private readonly NavigationManager _navigation;
    private readonly IConfiguration _configuration;
    private readonly IAppManagerClient _appManagerClient;

    public LoginService(ProtectedLocalStorage localStorage, NavigationManager navigation, IConfiguration configuration, IAppManagerClient appManagerClient)
    {
        _localStorage = localStorage;
        _navigation = navigation;
        _configuration = configuration;
        _appManagerClient = appManagerClient;
    }

    public async Task<bool> LoginAsync(LoginModel model)
    {
        var response = await _appManagerClient.LoginUserAsync(model);
        if (string.IsNullOrEmpty(response.AccessToken)) 
            return false;
        
        await _localStorage.SetAsync(AccessToken, response.AccessToken);
        await _localStorage.SetAsync(RefreshToken, response.RefreshToken);

        return true;
    }


    // public async Task<List<Claim>> GetLoginInfoAsync()
    // {
    //     var emptyResult = new List<Claim>();
    //     ProtectedBrowserStorageResult<string> accessToken;
    //     ProtectedBrowserStorageResult<string> refreshToken;
    //     try
    //     {
    //         accessToken = await _localStorage.GetAsync<string>(AccessToken);
    //         refreshToken = await _localStorage.GetAsync<string>(RefreshToken);
    //     }
    //     catch (CryptographicException)
    //     {
    //         await LogoutAsync();
    //         return emptyResult;
    //     }
    //
    //     if (accessToken.Success is false || accessToken.Value == default) 
    //         return emptyResult;
    //     
    //     var claims = JwtTokenHelper.ValidateDecodeToken(accessToken.Value, _configuration);
    //         
    //     if (claims.Count != 0) 
    //         return claims;
    //         
    //     if (refreshToken.Value != default)
    //     {
    //         var response = await _appManagerClient.RefreshTokenAsync(refreshToken.Value);
    //         if (!string.IsNullOrWhiteSpace(response.AccessToken))
    //         {
    //             await _localStorage.SetAsync(AccessToken, response.AccessToken);
    //             await _localStorage.SetAsync(RefreshToken, response.RefreshToken);
    //             claims = JwtTokenHelper.ValidateDecodeToken(response.AccessToken, _configuration);
    //             return claims;
    //         }
    //
    //         await LogoutAsync();
    //     }
    //     else
    //     {
    //         await LogoutAsync();
    //     }
    //     return claims;
    // }
    
    public async Task<string> GetTokenAsync()
    {
        return (await _localStorage.GetAsync<string>(AccessToken)).Value;
    }

    private async Task<string> GetRefreshTokenAsync()
    {
        return (await _localStorage.GetAsync<string>(RefreshToken)).Value;
    }

    public async Task LogoutAsync()
    {
        await RemoveAuthDataFromStorageAsync();
        _navigation.NavigateTo("/", true);
    }

    private async Task RemoveAuthDataFromStorageAsync()
    {
        await _localStorage.DeleteAsync(AccessToken);
        await _localStorage.DeleteAsync(RefreshToken);
    }
}
