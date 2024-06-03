using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AppManager.Application.Common.Interfaces;
using AppManager.Application.Common.Mappings;
using AppManager.Application.Common.Models;

namespace AppManager.Application.Versions.Queries.GetVersionsWithPagination;

public record GetVersionsWithPaginationQuery : IRequest<PaginatedList<VersionBriefDto>>
{
    public Guid BranchId { get; init; }

    public int PageNumber { get; init; } = 1;

    public int PageSize { get; init; } = 10;
}

public class
    GetVersionsWithPaginationQueryHandler : IRequestHandler<GetVersionsWithPaginationQuery,
    PaginatedList<VersionBriefDto>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetVersionsWithPaginationQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<PaginatedList<VersionBriefDto>> Handle(GetVersionsWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Versions
            .Where(x => x.BranchId.Equals(request.BranchId))
            .OrderBy(x => x.Name)
            .ProjectTo<VersionBriefDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
