using System;
using System.Threading;
using System.Threading.Tasks;
using AppManager.Application.Common.Interfaces;
using AppManager.Domain.Entities;

namespace AppManager.Application.Branches.Commands.CreateBranch;

public record CreateBranchCommand : IRequest<Guid>
{
    public Guid AnnexId { get; init; }

    public string Name { get; init; } = null!;

    public string Description { get; init; } = null!;

    public bool IsPrivate { get; init; }

    public string Tag { get; init; } = null!;
}

public class CreateBranchCommandHandler : IRequestHandler<CreateBranchCommand, Guid>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateBranchCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
    {
        var annex = await _dbContext.Annexes
            .FindAsync(request.AnnexId, cancellationToken);

        Guard.Against.NotFound(request.AnnexId, annex);

        var branchWithTag = await _dbContext.Branches
            .AsNoTracking()
            .SingleOrDefaultAsync(x =>
                x.AnnexId.Equals(request.AnnexId) &&
                x.Tag.Equals(request.Tag), cancellationToken);

        if (branchWithTag != null)
            throw new ArgumentException($"Branch with tag {request.Tag} already exists.");

        var branch = new Branch
        {
            Name = request.Name,
            Description = request.Description,
            AnnexId = request.AnnexId,
            IsPrivate = request.IsPrivate,
            Tag = request.Tag
        };

        await _dbContext.Branches.AddAsync(branch, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return branch.Id;
    }
}
