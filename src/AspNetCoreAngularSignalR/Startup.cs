using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AspNetCoreAngularSignalR.SignalRHubs;
using AspNetCoreAngularSignalR.Providers;
using Microsoft.EntityFrameworkCore;
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

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseHsts();

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
