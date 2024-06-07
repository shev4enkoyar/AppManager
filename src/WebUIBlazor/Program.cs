using System.Reflection;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using MudExtensions.Services;
using WebUIBlazor.Auth;
using WebUIBlazor.Components;
using WebUIBlazor.Services.AppManagerClient;
using WebUIBlazor.Services.NavigationService;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();
builder.Services.AddMudExtensions();
builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<LoginService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddHttpClient<IAppManagerClient, AppManagerClient>(options =>
{
    options.BaseAddress = new Uri(builder.Configuration["ApiAddress"]);
    options.Timeout = TimeSpan.FromSeconds(30);
    // options.DefaultRequestHeaders.TryAddWithoutValidation("Service", Assembly.GetAssembly(typeof(Program))?.GetName().Name);
});

builder.Services.AddTransient<INavigationService, NavigationService>();

WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error", true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseStatusCodePagesWithRedirects("/404");

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
