using System;
using System.Threading;
using System.Threading.Tasks;
using AppManager.Application.Common.Interfaces;
using AppManager.Domain.Entities;

namespace AppManager.Application.Projects.Commands.CreateProject;

public record CreateProjectCommand : IRequest<Guid>
{
    public string Name { get; init; } = null!;

    public string Description { get; init; } = null!;
}

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Guid>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateProjectCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = new Project
        {
            Name = request.Name,
            Description = request.Description
        };

        await _dbContext.Projects.AddAsync(project, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return project.Id;
    }
}
