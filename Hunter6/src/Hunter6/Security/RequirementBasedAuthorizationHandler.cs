using System;
using Microsoft.AspNet.Authorization;

namespace Hunter6.Security
{
    internal sealed class TestRequirement : IAuthorizationRequirement
    {

    }

    internal sealed class RequirementBasedAuthorizationHandler : AuthorizationHandler<TestRequirement>
    {
        protected override void Handle(AuthorizationContext context, TestRequirement requirement)
        {
            context.Succeed(requirement);
        }
    }
}