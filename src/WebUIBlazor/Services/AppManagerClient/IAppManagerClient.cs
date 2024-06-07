using WebUIBlazor.Models;

namespace WebUIBlazor.Services.AppManagerClient;

public interface IAppManagerClient
{
    Task<PaginatedList<ProjectBrief>> GetProjectList(int page, int pageSize = 10);
    Task<Project?> GetProject(Guid projectId);
    
    Task<AuthResponse> LoginUserAsync(LoginModel model, CancellationToken? cancellationToken = null);

    Task<AuthResponse> RefreshTokenAsync(string refreshToken,
        CancellationToken? cancellationToken = null);
}
