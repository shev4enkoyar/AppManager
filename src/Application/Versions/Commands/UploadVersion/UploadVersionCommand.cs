using System;
using System.Threading;
using System.Threading.Tasks;
using AppManager.Application.Common.Interfaces;
using AppManager.Domain.Abstractions;

namespace AppManager.Application.Versions.Commands.UploadVersion;

public record UploadVersionCommand : IRequest
{
    public Guid VersionId { get; init; }
    
    public IFile File { get; init; } = null!;
};

public class UploadVersionCommandHandler : IRequestHandler<UploadVersionCommand>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IFileManager _fileManager;

    public UploadVersionCommandHandler(IApplicationDbContext dbContext, IFileManager fileManager)
    {
        _dbContext = dbContext;
        _fileManager = fileManager;
    }

    public async Task Handle(UploadVersionCommand request, CancellationToken cancellationToken)
    {
        var version = await _dbContext.Versions.FindAsync([request.VersionId], cancellationToken);

        Guard.Against.NotFound(request.VersionId, version);
        
        var filePath = await _fileManager.UploadFileAsync(request.File, cancellationToken);

        if (filePath == null)
        {
            throw new Exception("Could not upload version file");
        }
        
        if (!string.IsNullOrWhiteSpace(version.FilePath))
        {
            await _fileManager.DeleteFileAsync(version.FilePath, cancellationToken);
        }

        version.FilePath = filePath;

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
