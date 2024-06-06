namespace WebUIBlazor;

public abstract class Paths
{
    #region Project

    public const string ProjectCreate = "/projects/create";
    
    public const string ProjectList = "/projects";
    
    public const string Project = "/projects/{projectId:guid}";

    public const string ProjectManage = "/projects/{projectId:guid}/manage";

    #endregion
    
    #region Annex
    
    public const string AnnexCreate = "/projects/{projectId:guid}/annexes/create";
    
    public const string AnnexList = "/projects/{projectId:guid}/annexes";
    
    public const string Annex = "/projects/{projectId:guid}/annexes/{annexId:guid}";
    
    public const string AnnexManage = "/projects/{projectId:guid}/annexes/{annexId:guid}/manage";
    
    #endregion

    #region Branch
    
    public const string BranchCreate = "/projects/{projectId:guid}/annexes/{annexId:guid}/branches/create";

    public const string BranchList = "/projects/{projectId:guid}/annexes/{annexId:guid}/branches";
    
    public const string Branch = "/projects/{projectId:guid}/annexes/{annexId:guid}/branches/{branchId:guid}";
    
    public const string BranchManage = "/projects/{projectId:guid}/annexes/{annexId:guid}/branches/{branchId:guid}/manage";

    #endregion

    #region Version
    
    public const string VersionCreate = "/projects/{projectId:guid}/annexes/{annexId:guid}/branches/{branchId:guid}/versions/create";

    public const string VersionList = "/projects/{projectId:guid}/annexes/{annexId:guid}/branches/{branchId:guid}/versions";
    
    public const string Version = "/projects/{projectId:guid}/annexes/{annexId:guid}/branches/{branchId:guid}/versions/{versionId:guid}";
    
    public const string VersionManage = "/projects/{projectId:guid}/annexes/{annexId:guid}/branches/{branchId:guid}/versions/{versionId:guid}/manage";

    #endregion
}
