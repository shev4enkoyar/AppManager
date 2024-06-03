using System;
using System.Threading;
using System.Threading.Tasks;
using AppManager.Application.Common.Interfaces;

namespace AppManager.Application.Branches.Commands.DeleteBranch;

public record DeleteBranchCommand(Guid Id) : IRequest;

public class DeleteBranchCommandHandler : IRequestHandler<DeleteBranchCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public DeleteBranchCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(DeleteBranchCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Branches
            .FindAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _dbContext.Branches.Remove(entity);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
