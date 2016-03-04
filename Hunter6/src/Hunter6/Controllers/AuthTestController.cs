using System.Linq;
using System.Threading.Tasks;
using Hunter6.Security;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Microsoft.AspNet.Authorization;

namespace Hunter6.Controllers
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

            if (!await authorizationService.AuthorizeAsync(User, resource, Operations.Read))
            {
                return new ChallengeResult();
            }

            return View();
        }
    }
}
