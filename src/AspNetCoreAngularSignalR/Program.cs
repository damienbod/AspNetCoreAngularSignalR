using AspNetCoreAngularSignalR.Providers;
using AspNetCoreAngularSignalR;
using Microsoft.EntityFrameworkCore;
using AspNetCoreAngularSignalR.SignalRHubs;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.AddServerHeader = false;
});

var services = builder.Services;
var configuration = builder.Configuration;
var env = builder.Environment;

services.AddTransient<ValidateMimeMultipartContentFilter>();

var sqlConnectionString = configuration.GetConnectionString("DefaultConnection");

services.AddDbContext<NewsContext>(options =>
    options.UseSqlite(
        sqlConnectionString
    ), ServiceLifetime.Singleton
);

services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder
                .AllowCredentials()
                .WithOrigins(
                    "https://localhost:4200")
                .SetIsOriginAllowedToAllowWildcardSubdomains()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

services.AddSingleton<NewsStore>();

services.AddSignalR()
  .AddMessagePackProtocol();

services.AddControllersWithViews();


var app = builder.Build();

// IdentityModelEventSource.ShowPII = true;

app.UseCors("AllowAllOrigins");

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapHub<LoopyHub>("/loopy");
app.MapHub<NewsHub>("/looney");
app.MapHub<LoopyMessageHub>("/loopymessage");
app.MapHub<ImagesMessageHub>("/zub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
