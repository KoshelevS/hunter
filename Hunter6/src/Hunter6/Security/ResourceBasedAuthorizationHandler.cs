﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hunter6.Models;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Authorization.Infrastructure;

namespace Hunter6.Security
{
    internal sealed class TestResource
    {
        
    }

    internal sealed class ResourceBasedAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, TestResource>
    {
        protected override void Handle(AuthorizationContext context, OperationAuthorizationRequirement requirement, TestResource resource)
        {
            context.Succeed(requirement);
        }
    }
}