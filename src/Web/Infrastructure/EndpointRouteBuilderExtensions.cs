using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace AppManager.Web.Infrastructure;

public static class EndpointRouteBuilderExtensions
{
    public static IEndpointRouteBuilder MapGet(this IEndpointRouteBuilder builder, Delegate handler, [StringSyntax("Route")] string pattern = "", bool disableAntiforgery = false, bool allowAnonymous = false)
    {
        Guard.Against.AnonymousMethod(handler);

        var getBuilder = builder.MapGet(pattern, handler)
            .WithName(handler.Method.Name);

        if (disableAntiforgery)
        {
            getBuilder.DisableAntiforgery();
        }

        if (allowAnonymous)
        {
            getBuilder.AllowAnonymous();
        }
        
        return builder;
    }

    public static IEndpointRouteBuilder MapPost(this IEndpointRouteBuilder builder, Delegate handler, [StringSyntax("Route")] string pattern = "")
    {
        Guard.Against.AnonymousMethod(handler);

        builder.MapPost(pattern, handler)
            .WithName(handler.Method.Name);
        
        return builder;
    }

    public static IEndpointRouteBuilder MapPostFile(this IEndpointRouteBuilder builder, Delegate handler,
        [StringSyntax("Route")] string pattern = "", bool disableAntiforgery = true, long requestSizeLimit = 204857600)
    {
        Guard.Against.AnonymousMethod(handler);

        var postBuilder = builder.MapPost(pattern, handler)
            .WithName(handler.Method.Name)
            .WithMetadata(new RequestSizeLimitAttribute(requestSizeLimit));

        if (disableAntiforgery)
        {
            postBuilder.DisableAntiforgery();
        }
        
        return builder;
    }

    public static IEndpointRouteBuilder MapPut(this IEndpointRouteBuilder builder, Delegate handler, [StringSyntax("Route")] string pattern)
    {
        Guard.Against.AnonymousMethod(handler);

        builder.MapPut(pattern, handler)
            .WithName(handler.Method.Name);

        return builder;
    }

    public static IEndpointRouteBuilder MapDelete(this IEndpointRouteBuilder builder, Delegate handler, [StringSyntax("Route")] string pattern)
    {
        Guard.Against.AnonymousMethod(handler);

        builder.MapDelete(pattern, handler)
            .WithName(handler.Method.Name);

        return builder;
    }
}
