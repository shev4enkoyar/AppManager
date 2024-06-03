using System;
using System.Threading;
using System.Threading.Tasks;
using AppManager.Application.Common.Interfaces;
using AppManager.Domain.Entities;

namespace AppManager.Application.Annexes.Commands.CreateAnnex;

public record CreateAnnexCommand : IRequest<Guid>
{
    public string Name { get; init; } = null!;

    public string Description { get; init; } = null!;

    public Guid ProjectId { get; init; }
}

public class CreateAnnexCommandHandler : IRequestHandler<CreateAnnexCommand, Guid>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateAnnexCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(CreateAnnexCommand request, CancellationToken cancellationToken)
    {
        var project = await _dbContext.Projects
            .FindAsync(request.ProjectId, cancellationToken);

        Guard.Against.NotFound(request.ProjectId, project);

        var annex = new Annex
        {
            Name = request.Name,
            Description = request.Description,
            ProjectId = request.ProjectId
        };

        await _dbContext.Annexes.AddAsync(annex, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return annex.Id;
    }
}
