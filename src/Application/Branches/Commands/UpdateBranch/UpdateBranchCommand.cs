using System;
using System.Threading;
using System.Threading.Tasks;
using AppManager.Application.Common.Interfaces;

namespace AppManager.Application.Branches.Commands.UpdateBranch;

public record UpdateBranchCommand : IRequest
{
    public Guid Id { get; init; }

    public string Name { get; init; } = null!;

    public string Description { get; init; } = null!;
}

public class UpdateBranchCommandHandler : IRequestHandler<UpdateBranchCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public UpdateBranchCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(UpdateBranchCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Branches
            .FindAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Name = request.Name;
        entity.Description = request.Description;

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
