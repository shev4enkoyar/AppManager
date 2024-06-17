using AppManager.Application;
using AppManager.Infrastructure;
using AppManager.Infrastructure.Data;
using AppManager.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddWebServices();

var app = builder.Build();

await app.InitializeDatabaseAsync();
if (app.Environment.IsDevelopment())
{
    
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHealthChecks("/health");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSwaggerUi(settings =>
{
    settings.Path = "/swagger";
    settings.DocumentPath = "/api/specification.json";
});

app.UseReDoc(settings =>
{
    settings.Path = "/api";
    settings.DocumentPath = "/api/specification.json";
});

app.MapRazorPages();

app.MapFallbackToFile("index.html");

app.UseExceptionHandler(options => { });

app.Map("/", () => Results.Redirect("/api"));

app.UseAntiforgery();

app.MapEndpoints();

await app.RunAsync();
