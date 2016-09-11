using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Hunter.Security
{
    internal sealed class TestRequirement : IAuthorizationRequirement
    {
        private readonly bool _preSatisfied;

        public TestRequirement(bool preSatisfied)
        {
            _preSatisfied = preSatisfied;
        }

        public bool IsSatisfiedByUser(ClaimsPrincipal user)
        {
            return _preSatisfied;
        }
    }

    internal sealed class RequirementBasedAuthorizationHandler : AuthorizationHandler<TestRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TestRequirement requirement)
        {
            if (requirement.IsSatisfiedByUser(context.User))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.FromResult(0);
        }
    }
}