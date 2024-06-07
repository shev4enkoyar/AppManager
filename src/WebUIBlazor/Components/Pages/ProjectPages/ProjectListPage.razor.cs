using Microsoft.AspNetCore.Components;
using WebUIBlazor.Models;
using WebUIBlazor.Services.AppManagerClient;

namespace WebUIBlazor.Components.Pages.ProjectPages;

public partial class ProjectListPage : ComponentBase
{
    private PaginatedList<ProjectBrief> _projectListPage = new();
    [Inject] private IAppManagerClient AppManagerClient { get; set; } = default!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _projectListPage = await AppManagerClient.GetProjectList(1);
            StateHasChanged();
        }
    }

    private void OnCardClickHandler(Guid projectId)
    {
        NavigationService.GoToProject(projectId);
    }

    private void OnSettingButtonClickedHandler(Guid projectId)
    {
        NavigationService.GoToProjectManage(projectId);
    }
}
