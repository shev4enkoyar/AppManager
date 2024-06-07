using Microsoft.AspNetCore.Components;
using WebUIBlazor.Models;
using WebUIBlazor.Services.AppManagerClient;

namespace WebUIBlazor.Components.Pages.ProjectPages;

public partial class ProjectPage : ComponentBase
{
    [Parameter] public Guid ProjectId { get; set; }

    public Project? ProjectModel { get; set; }

    [Inject] private IAppManagerClient AppManagerClient { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        ProjectModel = await AppManagerClient.GetProject(ProjectId);
    }

    private void OnAnnexesCardClickHandler()
    {
        NavigationService.GoToAnnexListPage(ProjectId);
    }

    private void OnSettingButtonClickedHandler()
    {
        NavigationService.GoToProjectManage(ProjectId);
    }
}
