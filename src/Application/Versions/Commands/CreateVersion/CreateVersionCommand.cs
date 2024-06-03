using System;
using System.Threading;
using System.Threading.Tasks;
using AppManager.Application.Common.Interfaces;
using Version = AppManager.Domain.Entities.Version;

namespace AppManager.Application.Versions.Commands.CreateVersion;

public record CreateVersionCommand : IRequest<Guid>
{
    public Guid BranchId { get; init; }

    public string Name { get; init; } = null!;

    public string Description { get; init; } = null!;

    public string Number { get; init; } = null!;
}

public class CreateVersionCommandHandler : IRequestHandler<CreateVersionCommand, Guid>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateVersionCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(CreateVersionCommand request, CancellationToken cancellationToken)
    {
        var branch = await _dbContext.Branches
            .FindAsync(request.BranchId, cancellationToken);

        Guard.Against.NotFound(request.BranchId, branch);

        var version = new Version
        {
            Name = request.Name,
            Description = request.Description,
            Number = request.Number,
            BranchId = request.BranchId
        };

        await _dbContext.Versions.AddAsync(version, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return version.Id;
    }
}
