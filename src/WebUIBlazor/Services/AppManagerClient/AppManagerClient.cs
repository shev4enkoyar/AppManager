using System.Net.Http.Headers;
using WebUIBlazor.Models;

namespace WebUIBlazor.Services.AppManagerClient;

public class AppManagerClient : IAppManagerClient
{
    private readonly HttpClient _httpClient;

    public AppManagerClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PaginatedList<ProjectBrief>> GetProjectList(int page, int pageSize = 10)
    {
        try
        {
            using HttpResponseMessage response =
                await _httpClient.GetAsync($"projects?PageNumber={page}&PageSize={pageSize}");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<PaginatedList<ProjectBrief>>() ??
                   new PaginatedList<ProjectBrief>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return new PaginatedList<ProjectBrief>();
    }

    public async Task<Project?> GetProject(Guid projectId)
    {
        try
        {
            using HttpResponseMessage response = await _httpClient.GetAsync($"projects/{projectId}");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Project>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return null;
    }

    public async Task<AuthResponse> LoginUserAsync(LoginModel model, CancellationToken cancellationToken = default)
    {
        using HttpResponseMessage response = await _httpClient.PostAsJsonAsync("users/login", model, cancellationToken);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<AuthResponse>(cancellationToken) ?? new AuthResponse();
    }

    public async Task<AuthResponse> RefreshTokenAsync(string refreshToken, CancellationToken? cancellationToken = null)
    {
        using HttpResponseMessage response = await _httpClient.PostAsJsonAsync("users/refresh", new { refreshToken });

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<AuthResponse>(cancellationToken ?? CancellationToken.None) ??
               new AuthResponse();
    }

    public async Task<UserBriefInfo?> GetUserBriefInfoAsync(string token, CancellationToken cancellationToken = default)
    {
        //var token = await _loginService.GetTokenAsync();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        using HttpResponseMessage response = await _httpClient.GetAsync("users/info", cancellationToken);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<UserBriefInfo>(cancellationToken);
    }

    public async Task<AuthResponse> LoginUserAsync(LoginModel model, CancellationToken? cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
