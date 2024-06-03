using Microsoft.AspNetCore.Builder;

namespace AppManager.Web.Infrastructure;

public abstract class EndpointGroupBase
{
    public abstract void Map(WebApplication app);
}
