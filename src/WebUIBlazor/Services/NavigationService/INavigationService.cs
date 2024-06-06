namespace WebUIBlazor.Services.NavigationService;

public interface INavigationService
{
    void GoToProjectCreate();
    
    void GoToProjectList();
    
    void GoToProject(Guid projectId);
    
    void GoToProjectManage(Guid projectId);
    
    void GoToAnnexCreate(Guid projectId);
    
    void GoToAnnexListPage(Guid projectId);
    
    void GoToAnnex(Guid projectId, Guid annexId);
    
    void GoToAnnexManage(Guid projectId, Guid annexId);
    
    void GoToBranchCreate(Guid projectId, Guid annexId);
    
    void GoToBranchList(Guid projectId, Guid annexId);
    
    void GoToBranch(Guid projectId, Guid annexId, Guid branchId);
    
    void GoToBranchManage(Guid projectId, Guid annexId, Guid branchId);
    
    void GoToVersionCreate(Guid projectId, Guid annexId, Guid branchId);
    
    void GoToVersionList(Guid projectId, Guid annexId, Guid branchId);
    
    void GoToVersion(Guid projectId, Guid annexId, Guid branchId, Guid versionId);
    
    void GoToVersionManage(Guid projectId, Guid annexId, Guid branchId, Guid versionId);
}
