using MudBlazor.Services;
using MudExtensions.Services;
using WebUIBlazor.Components;
using WebUIBlazor.Services.NavigationService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();
builder.Services.AddMudExtensions();

builder.Services.AddTransient<INavigationService, NavigationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseStatusCodePagesWithRedirects("/404");

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
