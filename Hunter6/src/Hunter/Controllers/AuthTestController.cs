using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Hunter.Security;

namespace Hunter.Controllers
{
    public class AuthTestController : Controller
    {
        private readonly IAuthorizationService authorizationService;

        public AuthTestController(IAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy = "RequireClimePolicyTest")]
        public IActionResult RequireClimePolicyTest()
        {
            return View();
        }

        [Authorize(Policy = "RequireRolePolicyTest")]
        public IActionResult RequireRolePolicyTest()
        {
            return View();
        }

        [Authorize(Policy = "RequirementBasedPolicyTest")]
        public IActionResult RequirementBasedPolicyTest()
        {
            return View();
        }

        public async Task<IActionResult> ResourceBasedPolicyTest()
        {
            var resource = new TestResource();

            if (!await authorizationService.AuthorizeAsync(User, resource, ResourceOperations.Read))
            {
                return new ChallengeResult();
            }

            return View();
        }
    }
}
