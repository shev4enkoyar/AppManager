using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AppManager.Domain.Abstractions;

namespace AppManager.Application.Common.Interfaces;

public interface IFileManager
{
    Task<string?> UploadFileAsync(IFile file, CancellationToken cancellationToken = default);
    
    Task<FileStream> DownloadFileAsync(string path, CancellationToken cancellationToken = default);
    
    Task DeleteFileAsync(string path, CancellationToken cancellationToken = default);
}
