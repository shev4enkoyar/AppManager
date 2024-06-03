using System;
using System.Threading;
using System.Threading.Tasks;
using AppManager.Application.Common.Interfaces;

namespace AppManager.Application.Branches.Queries.GetBranch;

public record GetBranchQuery(Guid Id) : IRequest<BranchDto>;

public class GetBranchQueryHandler : IRequestHandler<GetBranchQuery, BranchDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetBranchQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<BranchDto> Handle(GetBranchQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Branches
            .FindAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        return _mapper.Map<BranchDto>(entity);
    }
}
