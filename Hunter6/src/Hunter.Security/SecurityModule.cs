using System;
using Hunter.Security.DataAccess;
using Hunter.Security.Model;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hunter.Security
{
    public static class SecurityModule
    {
        // todo moved to startup
        //public static EntityFrameworkServicesBuilder AddSecurityContext(this EntityFrameworkServicesBuilder builder, string connectionString)
        //{
        //    return builder.AddDbContext<SecurityDbContext>(
        //        options => options.UseSqlServer(connectionString));
        //}

        public static void MigrateSecurityContext(this IServiceScope serviceScope)
        {
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;
            SecurityDbContext securityDbContext = serviceProvider.GetService<SecurityDbContext>();
            securityDbContext.Database.Migrate();
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
