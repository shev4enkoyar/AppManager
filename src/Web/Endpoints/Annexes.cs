using System;
using System.Threading.Tasks;
using AppManager.Application.Annexes.Commands.CreateAnnex;
using AppManager.Application.Annexes.Commands.DeleteAnnex;
using AppManager.Application.Annexes.Commands.UpdateAnnex;
using AppManager.Application.Annexes.Queries.GetAnnex;
using AppManager.Application.Annexes.Queries.GetAnnexesWithPagination;
using AppManager.Application.Common.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace AppManager.Web.Endpoints;

public class Annexes : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetAnnex, "{id}", allowAnonymous: true)
            .MapGet(GetAnnexesWithPagination)
            .MapPost(CreateAnnex)
            .MapPut(UpdateAnnex, "{id}")
            .MapDelete(DeleteAnnex, "{id}");
    }
    
    public async Task<AnnexDto> GetAnnex(ISender sender, Guid id)
    {
        return await sender.Send(new GetAnnexQuery(id));
    }

    public async Task<PaginatedList<AnnexBriefDto>> GetAnnexesWithPagination(ISender sender,
        [AsParameters] GetAnnexesWithPaginationQuery query)
    {
        return await sender.Send(query);
    }

    public async Task<Guid> CreateAnnex(ISender sender, CreateAnnexCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<IResult> UpdateAnnex(ISender sender, Guid id, UpdateAnnexCommand command)
    {
        if (id != command.Id) return 
            Results.BadRequest();

        await sender.Send(command);

        return Results.NoContent();
    }

    public async Task<IResult> DeleteAnnex(ISender sender, Guid id)
    {
        await sender.Send(new DeleteAnnexCommand(id));

        return Results.NoContent();
    }
}
