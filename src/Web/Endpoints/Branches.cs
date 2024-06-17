using System;
using System.Threading.Tasks;
using AppManager.Application.Branches.Commands.CreateBranch;
using AppManager.Application.Branches.Commands.DeleteBranch;
using AppManager.Application.Branches.Commands.UpdateBranch;
using AppManager.Application.Branches.Queries.GetBranch;
using AppManager.Application.Branches.Queries.GetBranchesWithPagination;
using AppManager.Application.Common.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace AppManager.Web.Endpoints;

public class Branches : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetBranch, "{id}", allowAnonymous: true)
            .MapGet(GetBranchesWithPagination, allowAnonymous: true)
            .MapPost(CreateBranch)
            .MapPut(UpdateBranch, "{id}")
            .MapDelete(DeleteBranch, "{id}");
    }
    
    public async Task<BranchDto> GetBranch(ISender sender, Guid id)
    {
        return await sender.Send(new GetBranchQuery(id));
    }

    public async Task<PaginatedList<BranchBriefDto>> GetBranchesWithPagination(ISender sender,
        [AsParameters] GetBranchesWithPaginationQuery query)
    {
        return await sender.Send(query);
    }

    public async Task<Guid> CreateBranch(ISender sender, CreateBranchCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<IResult> UpdateBranch(ISender sender, Guid id, UpdateBranchCommand command)
    {
        if (id != command.Id) return 
            Results.BadRequest();

        await sender.Send(command);

        return Results.NoContent();
    }

    public async Task<IResult> DeleteBranch(ISender sender, Guid id)
    {
        await sender.Send(new DeleteBranchCommand(id));

        return Results.NoContent();
    }
}
