using System.IO;

namespace AppManager.Application.Versions.Queries.DownloadVersion;

public class FileDto
{
    public string FileName { get; init; } = null!;
    
    public FileStream FileStream { get; init; } = null!;
}
