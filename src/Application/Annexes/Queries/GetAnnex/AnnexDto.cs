using AppManager.Domain.Entities;

namespace AppManager.Application.Annexes.Queries.GetAnnex;

public class AnnexDto
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Annex, AnnexDto>();
        }
    }
}
