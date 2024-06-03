using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AppManager.Application.Common.Interfaces;
using AppManager.Application.Common.Mappings;
using AppManager.Application.Common.Models;

namespace AppManager.Application.Branches.Queries.GetBranchesWithPagination;

public record GetBranchesWithPaginationQuery : IRequest<PaginatedList<BranchBriefDto>>
{
    public Guid AnnexId { get; init; }

    public int PageNumber { get; init; } = 1;

    public int PageSize { get; init; } = 10;
}

public class
    GetBranchesWithPaginationQueryHandler : IRequestHandler<GetBranchesWithPaginationQuery,
    PaginatedList<BranchBriefDto>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetBranchesWithPaginationQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<PaginatedList<BranchBriefDto>> Handle(GetBranchesWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Branches
            .Where(x => x.AnnexId.Equals(request.AnnexId))
            .OrderBy(x => x.Name)
            .ProjectTo<BranchBriefDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
