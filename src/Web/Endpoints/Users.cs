using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AppManager.Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;

namespace AppManager.Web.Endpoints;

public class Users : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetUserInfo, "info")
            .MapIdentityApi<ApplicationUser>();
    }

    public async Task<IResult> GetUserInfo(ClaimsPrincipal claims, UserManager<ApplicationUser> userManager)
    {
        ApplicationUser user = await userManager.GetUserAsync(claims);
        if (user == null)
        {
            return Results.Unauthorized();
        }

        IList<string> role = await userManager.GetRolesAsync(user);

        var userInfo = new { user.Id, user.UserName, user.Email, Role = role };

        return Results.Ok(userInfo);
    }
}
