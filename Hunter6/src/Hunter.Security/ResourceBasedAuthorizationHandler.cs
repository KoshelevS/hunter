using System.Security.Claims;
//using Microsoft.AspNet.Authorization;
//using Microsoft.AspNet.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Hunter.Security
{
    public sealed class TestResource
    {

    }

    internal sealed class ResourceBasedAuthorizationHandler
        : AuthorizationHandler<OperationAuthorizationRequirement, TestResource>
    {
        protected override void Handle(
            AuthorizationContext context, OperationAuthorizationRequirement requirement, TestResource resource)
        {
            if (CanOperate(context.User, resource, requirement))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }

        private bool CanOperate(ClaimsPrincipal user, TestResource resource, OperationAuthorizationRequirement requirement)
        {
            if (requirement == ResourceOperations.Read)
            {
                return CanRead(user, resource);
            }

            if (requirement == ResourceOperations.Create)
            {
                return CanCreate(user, resource);
            }

            if (requirement == ResourceOperations.Update)
            {
                return CanUpdate(user, resource);
            }

            if (requirement == ResourceOperations.Delete)
            {
                return CanDelete(user, resource);
            }

            return false;
        }

#pragma warning disable RECS0154 // Parameter is never used
        private bool CanRead(ClaimsPrincipal user, TestResource resource)
        {
            return true;
        }

        private bool CanCreate(ClaimsPrincipal user, TestResource resource)
        {
            return false;
        }

        private bool CanUpdate(ClaimsPrincipal user, TestResource resource)
        {
            return false;
        }

        private bool CanDelete(ClaimsPrincipal user, TestResource resource)
        {
            return false;
        }
#pragma warning restore RECS0154 // Parameter is never used
    }
}
