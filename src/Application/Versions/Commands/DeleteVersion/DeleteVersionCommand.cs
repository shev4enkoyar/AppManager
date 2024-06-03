using System;
using System.Threading;
using System.Threading.Tasks;
using AppManager.Application.Common.Interfaces;

namespace AppManager.Application.Versions.Commands.DeleteVersion;

public record DeleteVersionCommand(Guid Id) : IRequest;

public class DeleteVersionCommandHandler : IRequestHandler<DeleteVersionCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public DeleteVersionCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(DeleteVersionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Versions
            .FindAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _dbContext.Versions.Remove(entity);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
