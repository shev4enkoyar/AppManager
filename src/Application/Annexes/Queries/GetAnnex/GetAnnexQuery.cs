using System;
using System.Threading;
using System.Threading.Tasks;
using AppManager.Application.Common.Interfaces;

namespace AppManager.Application.Annexes.Queries.GetAnnex;

public record GetAnnexQuery(Guid Id) : IRequest<AnnexDto>;

public class GetAnnexQueryHandler : IRequestHandler<GetAnnexQuery, AnnexDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAnnexQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<AnnexDto> Handle(GetAnnexQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Annexes
            .FindAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        return _mapper.Map<AnnexDto>(entity);
    }
}
