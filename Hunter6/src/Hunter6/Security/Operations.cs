using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Authorization.Infrastructure;

namespace Hunter6.Security
{
    internal static class Operations
    {
        public static OperationAuthorizationRequirement Create = new OperationAuthorizationRequirement { Name = "Create" };
        public static OperationAuthorizationRequirement Read = new OperationAuthorizationRequirement { Name = "Read" };
        public static OperationAuthorizationRequirement Update = new OperationAuthorizationRequirement { Name = "Update" };
        public static OperationAuthorizationRequirement Delete = new OperationAuthorizationRequirement { Name = "Delete" };
    }
}
