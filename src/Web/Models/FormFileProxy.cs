using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AppManager.Domain.Abstractions;
using Microsoft.AspNetCore.Http;

namespace AppManager.Web.Models;

public sealed class FormFileProxy : IFile
{
    private readonly IFormFile _formFile;
    public string ContentType => _formFile.ContentType;

    public long Length => _formFile.Length;

    public string FileName => _formFile.FileName;

    public FormFileProxy(IFormFile formFile)
    {
        _formFile = Guard.Against.Null(formFile);
    }

    public Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
    {
        return _formFile.CopyToAsync(target, cancellationToken);
    }

    public async Task<byte[]> GetData(CancellationToken cancellationToken = default)
    {
        using var stream = new MemoryStream();
        await CopyToAsync(stream, cancellationToken);
        return stream.ToArray();
    }
}
