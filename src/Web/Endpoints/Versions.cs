using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using AppManager.Application.Common.Models;
using AppManager.Application.Versions.Commands.CreateVersion;
using AppManager.Application.Versions.Commands.DeleteVersion;
using AppManager.Application.Versions.Commands.UpdateVersion;
using AppManager.Application.Versions.Commands.UploadVersion;
using AppManager.Application.Versions.Queries.DownloadVersion;
using AppManager.Application.Versions.Queries.GetVersion;
using AppManager.Application.Versions.Queries.GetVersionsWithPagination;
using AppManager.Domain.Constants;
using AppManager.Web.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using tusdotnet;
using tusdotnet.Models;
using tusdotnet.Models.Concatenation;
using tusdotnet.Models.Configuration;
using tusdotnet.Stores;

namespace AppManager.Web.Endpoints;

public class Versions : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetVersionInfo, "{id}")
            .MapGet(GetVersionsWithPagination)
            .MapPost(CreateVersion)
            .MapPut(UpdateVersion, "{id}")
            .MapDelete(DeleteVersion, "{id}")
            .MapPostUpload(UploadVersion, "{id}/upload")
            .MapGet(DownloadVersion, "{id}/download", true)
            .MapTus("{id}/bigupload", TusConfigurationFactory);
    }

    public async Task<VersionDto> GetVersionInfo(ISender sender, Guid id)
    {
        return await sender.Send(new GetVersionQuery(id));
    }

    public async Task<PaginatedList<VersionBriefDto>> GetVersionsWithPagination(ISender sender,
        [AsParameters] GetVersionsWithPaginationQuery query)
    {
        return await sender.Send(query);
    }

    public async Task<Guid> CreateVersion(ISender sender, CreateVersionCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<IResult> UpdateVersion(ISender sender, Guid id, UpdateVersionCommand command)
    {
        if (id != command.Id)
        {
            return Results.BadRequest();
        }

        await sender.Send(command);

        return Results.NoContent();
    }


    public async Task<IResult> DeleteVersion(ISender sender, Guid id)
    {
        await sender.Send(new DeleteVersionCommand(id));

        return Results.NoContent();
    }
    
    public async Task<IResult> UploadVersion(ISender sender, Guid id, IFormFile file)
    {
        await sender.Send(new UploadVersionCommand
        {
            VersionId = id,
            File = new FormFileProxy(file)
        });
        
        return Results.NoContent();
    }
    
    public async Task<IResult> DownloadVersion(ISender sender, Guid id)
    {
        var file = await sender.Send(new DownloadVersionQuery { VersionId = id });
        return Results.Stream(file.FileStream, "application/octet-stream", file.FileName);
    }
    
    

    private Task<DefaultTusConfiguration> TusConfigurationFactory(HttpContext httpContext)
    {
        ILogger<Versions> logger = httpContext.RequestServices.GetRequiredService<ILoggerFactory>()
            .CreateLogger<Versions>();
        ISender sender = httpContext.RequestServices.GetService<ISender>();

        if (!Directory.Exists("tusfiles"))
        {
            Directory.CreateDirectory("tusfiles");
        }

        DefaultTusConfiguration config = new()
        {
            Store = new TusDiskStore("tusfiles"),
            UsePipelinesIfAvailable = true,
            Events = new Events
            {
                OnAuthorizeAsync = eventContext =>
                {
                    if (eventContext.HttpContext.User.Identity is not { IsAuthenticated: true })
                    {
                        eventContext.FailRequest(HttpStatusCode.Unauthorized);
                        return Task.CompletedTask;
                    }

                    if (!eventContext.HttpContext.User.IsInRole(Roles.Administrator))
                    {
                        eventContext.FailRequest(HttpStatusCode.Forbidden,
                            $"'{Roles.Administrator}' is the only allowed user");
                        return Task.CompletedTask;
                    }

                    return Task.CompletedTask;
                },
                OnBeforeCreateAsync = eventContext =>
                {
                    if (eventContext.FileConcatenation is FileConcatPartial)
                    {
                        return Task.CompletedTask;
                    }

                    if (!eventContext.HttpContext.Request.RouteValues.TryGetValue("id", out object versionIdObject)
                        || versionIdObject is not string versionIdText
                        || !Guid.TryParse(versionIdText, out Guid versionId))
                    {
                        eventContext.FailRequest("ID must be a valid Guid");
                        return Task.CompletedTask;
                    }

                    if (!eventContext.Metadata.ContainsKey("extension") ||
                        eventContext.Metadata["extension"].HasEmptyValue)
                    {
                        eventContext.FailRequest("Extension metadata must be specified.");
                    }

                    return Task.CompletedTask;
                },
                OnCreateCompleteAsync = ctx =>
                {
                    logger.LogInformation($"Created file {ctx.FileId} using {ctx.Store.GetType().FullName}");
                    return Task.CompletedTask;
                },
                OnBeforeDeleteAsync = ctx =>
                {
                    // Can the file be deleted? If not call ctx.FailRequest(<message>);
                    return Task.CompletedTask;
                },
                OnDeleteCompleteAsync = ctx =>
                {
                    logger.LogInformation($"Deleted file {ctx.FileId} using {ctx.Store.GetType().FullName}");
                    return Task.CompletedTask;
                },
                OnFileCompleteAsync = ctx =>
                {
                    logger.LogInformation($"Upload of {ctx.FileId} completed using {ctx.Store.GetType().FullName}");
                    // If the store implements ITusReadableStore one could access the completed file here.
                    // The default TusDiskStore implements this interface:
                    //var file = await ctx.GetFileAsync();
                    return Task.CompletedTask;
                }
            }
        };

        return Task.FromResult(config);
    }
}
