using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

namespace Hunter.Controllers
{
    [Route("api/[controller]")]
    public class CandidateController : Controller
    {
        // GET: api/values
        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
