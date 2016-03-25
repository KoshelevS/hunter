using Microsoft.AspNet.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Hunter.Security
{
    public static class AuthorizationConfig
    {
        public static void ConfigureAuthorization(this IServiceCollection services)
        {
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
