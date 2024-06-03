using System;
using System.Threading;
using System.Threading.Tasks;
using AppManager.Application.Common.Interfaces;

namespace AppManager.Application.Projects.Commands.DeleteProject;

public record DeleteProjectCommand(Guid Id) : IRequest;

public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public DeleteProjectCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Projects
            .FindAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _dbContext.Projects.Remove(entity);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
