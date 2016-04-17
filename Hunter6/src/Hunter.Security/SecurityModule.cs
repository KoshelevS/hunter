using Hunter.Security.DataAccess;
using Hunter.Security.Model;

using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Hunter.Security
{
    public static class SecurityModule
    {
        public static EntityFrameworkServicesBuilder AddSecurityContext(
            this EntityFrameworkServicesBuilder builder,
            string connectionString)
        {
            return builder.AddDbContext<SecurityDbContext>(
                options => options.UseSqlServer(connectionString));
        }

        public static void MigrateSecurityContext(this IServiceScope serviceScope)
        {
            serviceScope.ServiceProvider.GetService<SecurityDbContext>().Database.Migrate();
        }

        public static void ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<SecurityDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireClimePolicyTest", policy =>
                {
                    policy.RequireClaim("RequireClimePolicyTest");
                });

                options.AddPolicy("RequireRolePolicyTest", policy =>
                {
                    policy.RequireRole("RequireRolePolicyTest");
                });

                options.AddPolicy("RequirementBasedPolicyTest", policy =>
                {
                    policy.AddRequirements(new TestRequirement(preSatisfied: true));
                });
            });

            services.AddSingleton<IAuthorizationHandler, ResourceBasedAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, RequirementBasedAuthorizationHandler>();
        }
    }
}
