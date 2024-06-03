using System;
using System.Threading;
using System.Threading.Tasks;
using AppManager.Application.Common.Interfaces;

namespace AppManager.Application.Annexes.Commands.DeleteAnnex;

public record DeleteAnnexCommand(Guid Id) : IRequest;

public class DeleteAnnexCommandHandler : IRequestHandler<DeleteAnnexCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public DeleteAnnexCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(DeleteAnnexCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Annexes
            .FindAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _dbContext.Annexes.Remove(entity);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
