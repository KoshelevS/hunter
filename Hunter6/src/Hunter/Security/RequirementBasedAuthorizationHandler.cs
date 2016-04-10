using System.Security.Claims;
using Microsoft.AspNet.Authorization;

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
        protected override void Handle(AuthorizationContext context, TestRequirement requirement)
        {
            if (requirement.IsSatisfiedByUser(context.User))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
}