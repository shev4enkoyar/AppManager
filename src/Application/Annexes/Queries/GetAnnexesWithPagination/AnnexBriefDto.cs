using System;
using AppManager.Domain.Entities;

namespace AppManager.Application.Annexes.Queries.GetAnnexesWithPagination;

public class AnnexBriefDto
{
    public Guid Id { get; init; }

    public string Name { get; init; } = null!;

    public string Description { get; init; } = null!;

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Annex, AnnexBriefDto>();
        }
    }
}
