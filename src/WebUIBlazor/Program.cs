using MudBlazor.Services;
using MudExtensions.Services;
using Polly;
using Polly.Extensions.Http;
using WebUIBlazor.Components;
using WebUIBlazor.Services.ContentApi;
using WebUIBlazor.Services.NavigationService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();
builder.Services.AddMudExtensions();

// Create the retry policy we want
var retryPolicy = HttpPolicyExtensions.HandleTransientHttpError() // HttpRequestException, 5XX and 408  
    .RetryAsync();

// Register the InventoryClient with Polly policies
builder.Services.AddHttpClient<IContentApi, ContentApi>().ConfigureHttpClient(httpClient =>
{
    httpClient.BaseAddress = new Uri(builder.Configuration["ApiAddress"]);
});

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
