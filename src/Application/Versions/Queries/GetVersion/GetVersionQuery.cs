using System;
using System.Threading;
using System.Threading.Tasks;
using AppManager.Application.Common.Interfaces;

namespace AppManager.Application.Versions.Queries.GetVersion;

public record GetVersionQuery(Guid Id) : IRequest<VersionDto>;

public class GetVersionQueryHandler : IRequestHandler<GetVersionQuery, VersionDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetVersionQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<VersionDto> Handle(GetVersionQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Versions
            .FindAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);
        
        var resultEntity = _mapper.Map<VersionDto>(entity);
        return resultEntity;
    }
}
