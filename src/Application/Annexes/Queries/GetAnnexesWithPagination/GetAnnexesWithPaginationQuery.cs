using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AppManager.Application.Common.Interfaces;
using AppManager.Application.Common.Mappings;
using AppManager.Application.Common.Models;

namespace AppManager.Application.Annexes.Queries.GetAnnexesWithPagination;

public record GetAnnexesWithPaginationQuery : IRequest<PaginatedList<AnnexBriefDto>>
{
    public Guid ProjectId { get; init; }

    public int PageNumber { get; init; } = 1;

    public int PageSize { get; init; } = 10;
}

public class
    GetAnnexesWithPaginationQueryHandler : IRequestHandler<GetAnnexesWithPaginationQuery, PaginatedList<AnnexBriefDto>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAnnexesWithPaginationQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<PaginatedList<AnnexBriefDto>> Handle(GetAnnexesWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Annexes
            .Where(x => x.ProjectId.Equals(request.ProjectId))
            .OrderBy(x => x.Name)
            .ProjectTo<AnnexBriefDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
