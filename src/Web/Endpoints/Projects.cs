using System;
using System.Threading.Tasks;
using AppManager.Application.Common.Models;
using AppManager.Application.Projects.Commands.CreateProject;
using AppManager.Application.Projects.Commands.DeleteProject;
using AppManager.Application.Projects.Commands.UpdateProject;
using AppManager.Application.Projects.Queries.GetProject;
using AppManager.Application.Projects.Queries.GetProjectWithPagination;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace AppManager.Web.Endpoints;

public class Projects : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            // .RequireAuthorization()
            .MapGet(GetProject, "{id}")
            .MapGet(GetProjectsWithPagination)
            .MapPost(CreateProject)
            .MapPut(UpdateProject, "{id}")
            .MapDelete(DeleteProject, "{id}");
    }
    
    public async Task<ProjectDto> GetProject(ISender sender, Guid id)
    {
        return await sender.Send(new GetProjectQuery(id));
    }
    
    public async Task<PaginatedList<ProjectBriefDto>> GetProjectsWithPagination(ISender sender,
        [AsParameters] GetProjectsWithPaginationQuery query)
    {
        return await sender.Send(query);
    }

    public async Task<Guid> CreateProject(ISender sender, CreateProjectCommand command)
    {
        return await sender.Send(command);
    }
    
    public async Task<IResult> UpdateProject(ISender sender, Guid id, UpdateProjectCommand command)
    {
        if (id != command.Id) return 
            Results.BadRequest();

        await sender.Send(command);

        return Results.NoContent();
    }

    public async Task<IResult> DeleteProject(ISender sender, Guid id)
    {
        await sender.Send(new DeleteProjectCommand(id));

        return Results.NoContent();
    }
}
