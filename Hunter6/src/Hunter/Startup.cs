using System.IO;
using Hunter.Domain.Core;
using Hunter.Domain.Interfaces;
using Hunter.Filters;
using Hunter.Infrastructure.Data;
using Hunter.Security;
using Hunter.Security.DataAccess;
using Hunter.Services;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Hunter
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange:true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            //Configuration = builder.Build().ReloadOnChanged("appsettings.json"); ??
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            string connectionString = Configuration["Data:DefaultConnection:ConnectionString"];

            services
                .AddDbContext<SecurityDbContext>(options => options.UseSqlServer(connectionString))
                .AddDbContext<DomainContext>(options => options.UseSqlServer(connectionString));
            services.ConfigureAuthorization();

            services.AddMvc();

            services.AddScoped<ValidateMimeMultipartContentFilter>();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddTransient<IRepository<Project>, ProjectRepository>();
            services.AddTransient<IRepository<Applicant>, ApplicantRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                DbSetup(app);
            }
            else
            {
                DbSetup(app);
                app.UseExceptionHandler("/Home/Error");
            }

            //app.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());

            app.UseStaticFiles();

            app.UseIdentity();

            // To configure external authentication please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static void DbSetup(IApplicationBuilder app)
        {
            // For more details on creating database during deployment see http://go.microsoft.com/fwlink/?LinkID=615859
            try
            {
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    serviceScope.MigrateProjectContext();
                }
            }
            catch
            {
            }
            try
            {
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    serviceScope.MigrateSecurityContext();
                }
            }
            catch
            {
            }
        }

        // Entry point for the application.
        //public static void Main(string[] args) => WebApplication.Run<Startup>(args);
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }

    }
}
