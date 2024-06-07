using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AppManager.Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace AppManager.Web.Endpoints;

public class Users : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetClaims,"claims")
            .MapIdentityApi<ApplicationUser>();
    }

    public async Task<IEnumerable<Claim>> GetClaims(ClaimsPrincipal claims)
    {
        return claims.Claims;
    }
}
