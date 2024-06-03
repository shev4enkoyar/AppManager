using System;
using System.Threading;
using System.Threading.Tasks;
using AppManager.Application.Common.Interfaces;

namespace AppManager.Application.Projects.Commands.UpdateProject;

public record UpdateProjectCommand : IRequest
{
    public Guid Id { get; init; }

    public string Name { get; init; } = null!;

    public string Description { get; init; } = null!;
}

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public UpdateProjectCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Projects
            .FindAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Name = request.Name;
        entity.Description = request.Description;

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
