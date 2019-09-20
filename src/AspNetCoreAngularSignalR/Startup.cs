using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AspNetCoreAngularSignalR.SignalRHubs;
using AspNetCoreAngularSignalR.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreAngularSignalR;

namespace Angular2WebpackVisualStudio
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ValidateMimeMultipartContentFilter>();

            var sqlConnectionString = Configuration.GetConnectionString("DefaultConnection");

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
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            services.AddSingleton<NewsStore>();

            services.AddSignalR()
              .AddMessagePackProtocol();

            services.AddControllersWithViews().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        public void Configure(IApplicationBuilder app)
        {
            var angularRoutes = new[] {
                 "/home",
                 "/news",
                 "/images"
             };

            app.UseHsts();

            app.Use(async (context, next) =>
            {
                if (context.Request.Path.HasValue && null != angularRoutes.FirstOrDefault(
                    (ar) => context.Request.Path.Value.StartsWith(ar, StringComparison.OrdinalIgnoreCase)))
                {
                    context.Request.Path = new PathString("/");
                }

                await next();
            });

            app.UseCors("AllowAllOrigins");

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<LoopyHub>("/loopy");
                endpoints.MapHub<NewsHub>("/looney");
                endpoints.MapHub<LoopyMessageHub>("/loopymessage");
                endpoints.MapHub<ImagesMessageHub>("/zub");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
