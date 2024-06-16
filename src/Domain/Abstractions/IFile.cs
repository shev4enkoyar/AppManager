using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AppManager.Domain.Abstractions;

public interface IFile
{
    string FileName { get; }
    string ContentType { get; }
    long Length { get; }
    Task CopyToAsync(Stream target, CancellationToken cancellationToken);
    Task<byte[]> GetData(CancellationToken cancellationToken);
}
