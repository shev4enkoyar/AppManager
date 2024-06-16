using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AppManager.Application.Common.Interfaces;

namespace AppManager.Application.Versions.Queries.DownloadVersion;

public record DownloadVersionQuery() : IRequest<FileDto>
{
    public Guid VersionId { get; init; }
};

public class DownloadVersionQueryHandler : IRequestHandler<DownloadVersionQuery, FileDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IFileManager _fileManager;

    public DownloadVersionQueryHandler(IApplicationDbContext dbContext, IFileManager fileManager)
    {
        _dbContext = dbContext;
        _fileManager = fileManager;
    }

    public async Task<FileDto> Handle(DownloadVersionQuery request, CancellationToken cancellationToken)
    {
        var version = await _dbContext.Versions
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id.Equals(request.VersionId), cancellationToken);

        Guard.Against.NotFound(request.VersionId, version);

        return new FileDto
        {
            FileName = $"{version.Name}{Path.GetExtension(version.FilePath)}",
            FileStream = await _fileManager.DownloadFileAsync(version.FilePath, cancellationToken)
        };
    }
}
