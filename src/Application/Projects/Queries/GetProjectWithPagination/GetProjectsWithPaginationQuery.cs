using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AppManager.Application.Common.Interfaces;
using AppManager.Application.Common.Mappings;
using AppManager.Application.Common.Models;

namespace AppManager.Application.Projects.Queries.GetProjectWithPagination;

public record GetProjectsWithPaginationQuery : IRequest<PaginatedList<ProjectBriefDto>>
{
    public int PageNumber { get; init; } = 1;

    public int PageSize { get; init; } = 10;
}

public class GetProjectsWithPaginationQueryHandler : IRequestHandler<GetProjectsWithPaginationQuery,
    PaginatedList<ProjectBriefDto>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetProjectsWithPaginationQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ProjectBriefDto>> Handle(GetProjectsWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Projects
            .OrderBy(x => x.Name)
            .ProjectTo<ProjectBriefDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
