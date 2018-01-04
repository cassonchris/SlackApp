using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SlackApp.Config;
using SlackApp.Repositories;
using SlackApp.Services;

namespace SlackApp
{
    public class Startup
    {
        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TestAppContext>();

            services.AddMvc();

            services.Configure<TestAppConfig>(Configuration.GetSection("TestApp"));
            services.Configure<SlackWebApiConfig>(Configuration.GetSection("SlackWebApi"));

            services.AddScoped<IAppInstallRepository, AppInstallRepository>();

            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<IDndService, DndService>();
            services.AddScoped<IUsersService, UsersService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new {controller = "Install", action = "Index"}
                );
            });
        }
    }
}
