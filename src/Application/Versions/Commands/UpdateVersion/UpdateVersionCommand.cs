using System;
using System.Threading;
using System.Threading.Tasks;
using AppManager.Application.Common.Interfaces;

namespace AppManager.Application.Versions.Commands.UpdateVersion;

public record UpdateVersionCommand : IRequest
{
    public Guid Id { get; init; }

    public string Name { get; init; } = null!;

    public string Description { get; init; } = null!;
}

public class UpdateVersionCommandHandler : IRequestHandler<UpdateVersionCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public UpdateVersionCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(UpdateVersionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Versions
            .FindAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Name = request.Name;
        entity.Description = request.Description;

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
