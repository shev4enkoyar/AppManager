using System;
using System.Threading;
using System.Threading.Tasks;
using AppManager.Application.Common.Interfaces;

namespace AppManager.Application.Annexes.Commands.UpdateAnnex;

public record UpdateAnnexCommand : IRequest
{
    public Guid Id { get; init; }

    public string Name { get; init; } = null!;

    public string Description { get; init; } = null!;
}

public class UpdateAnnexCommandHandler : IRequestHandler<UpdateAnnexCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public UpdateAnnexCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(UpdateAnnexCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Annexes
            .FindAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Name = request.Name;
        entity.Description = request.Description;

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
