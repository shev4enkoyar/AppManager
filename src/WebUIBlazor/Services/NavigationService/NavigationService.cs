using Microsoft.AspNetCore.Components;

namespace WebUIBlazor.Services.NavigationService;

public class NavigationService(NavigationManager navigationManager) : INavigationService
{
    public void GoToProjectCreate()
    {
        navigationManager.NavigateTo(Paths.ProjectCreate);
    }

    public void GoToProjectList(bool forceLoad = false)
    {
        navigationManager.NavigateTo(Paths.ProjectList, forceLoad);
    }

    public void GoToProject(Guid projectId)
    {
        navigationManager.NavigateTo(GetPathWithVariables(Paths.Project,
            new Dictionary<string, string> { { "{projectId:guid}", projectId.ToString() } }));
    }

    public void GoToProjectManage(Guid projectId)
    {
        navigationManager.NavigateTo(GetPathWithVariables(Paths.ProjectManage,
            new Dictionary<string, string> { { "{projectId:guid}", projectId.ToString() } }));
    }

    public void GoToAnnexCreate(Guid projectId)
    {
        navigationManager.NavigateTo(GetPathWithVariables(Paths.AnnexCreate,
            new Dictionary<string, string> { { "{projectId:guid}", projectId.ToString() } }));
    }

    public void GoToAnnexListPage(Guid projectId)
    {
        navigationManager.NavigateTo(GetPathWithVariables(Paths.AnnexList,
            new Dictionary<string, string> { { "{projectId:guid}", projectId.ToString() } }));
    }

    public void GoToAnnex(Guid projectId, Guid annexId)
    {
        navigationManager.NavigateTo(GetPathWithVariables(Paths.Annex,
            new Dictionary<string, string>
            {
                { "{projectId:guid}", projectId.ToString() }, { "{annexId:guid}", annexId.ToString() }
            }));
    }

    public void GoToAnnexManage(Guid projectId, Guid annexId)
    {
        navigationManager.NavigateTo(GetPathWithVariables(Paths.AnnexManage,
            new Dictionary<string, string>
            {
                { "{projectId:guid}", projectId.ToString() }, { "{annexId:guid}", annexId.ToString() }
            }));
    }

    public void GoToBranchCreate(Guid projectId, Guid annexId)
    {
        navigationManager.NavigateTo(GetPathWithVariables(Paths.BranchCreate,
            new Dictionary<string, string>
            {
                { "{projectId:guid}", projectId.ToString() }, { "{annexId:guid}", annexId.ToString() }
            }));
    }

    public void GoToBranchList(Guid projectId, Guid annexId)
    {
        navigationManager.NavigateTo(GetPathWithVariables(Paths.BranchList,
            new Dictionary<string, string>
            {
                { "{projectId:guid}", projectId.ToString() }, { "{annexId:guid}", annexId.ToString() }
            }));
    }

    public void GoToBranch(Guid projectId, Guid annexId, Guid branchId)
    {
        navigationManager.NavigateTo(GetPathWithVariables(Paths.Branch,
            new Dictionary<string, string>
            {
                { "{projectId:guid}", projectId.ToString() },
                { "{annexId:guid}", annexId.ToString() },
                { "{branchId:guid}", branchId.ToString() }
            }));
    }

    public void GoToBranchManage(Guid projectId, Guid annexId, Guid branchId)
    {
        navigationManager.NavigateTo(GetPathWithVariables(Paths.BranchManage,
            new Dictionary<string, string>
            {
                { "{projectId:guid}", projectId.ToString() },
                { "{annexId:guid}", annexId.ToString() },
                { "{branchId:guid}", branchId.ToString() }
            }));
    }

    public void GoToVersionCreate(Guid projectId, Guid annexId, Guid branchId)
    {
        navigationManager.NavigateTo(GetPathWithVariables(Paths.VersionCreate,
            new Dictionary<string, string>
            {
                { "{projectId:guid}", projectId.ToString() },
                { "{annexId:guid}", annexId.ToString() },
                { "{branchId:guid}", branchId.ToString() }
            }));
    }

    public void GoToVersionList(Guid projectId, Guid annexId, Guid branchId)
    {
        navigationManager.NavigateTo(GetPathWithVariables(Paths.VersionList,
            new Dictionary<string, string>
            {
                { "{projectId:guid}", projectId.ToString() },
                { "{annexId:guid}", annexId.ToString() },
                { "{branchId:guid}", branchId.ToString() }
            }));
    }

    public void GoToVersion(Guid projectId, Guid annexId, Guid branchId, Guid versionId)
    {
        navigationManager.NavigateTo(GetPathWithVariables(Paths.Version,
            new Dictionary<string, string>
            {
                { "{projectId:guid}", projectId.ToString() },
                { "{annexId:guid}", annexId.ToString() },
                { "{branchId:guid}", branchId.ToString() },
                { "{versionId:guid}", versionId.ToString() }
            }));
    }

    public void GoToVersionManage(Guid projectId, Guid annexId, Guid branchId, Guid versionId)
    {
        navigationManager.NavigateTo(GetPathWithVariables(Paths.VersionManage,
            new Dictionary<string, string>
            {
                { "{projectId:guid}", projectId.ToString() },
                { "{annexId:guid}", annexId.ToString() },
                { "{branchId:guid}", branchId.ToString() },
                { "{versionId:guid}", versionId.ToString() }
            }));
    }

    public void GoToLoginPage()
    {
        navigationManager.NavigateTo($"/login?redirectUri={navigationManager.Uri}");
    }

    private static string GetPathWithVariables(string initialPath, Dictionary<string, string> variables)
    {
        return variables.Aggregate(initialPath, (current, variable) =>
            current.Replace(variable.Key, variable.Value));
    }
}
