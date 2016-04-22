using Hunter.Domain.Core;
using Hunter.Domain.Interfaces;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

namespace Hunter.Controllers
{
    [Route("api/[controller]")]
    public class CandidateController : Controller
    {
        private readonly IRepository<Applicant> _applicantRepo;

        public CandidateController(IRepository<Applicant> applicantRepo)
        {
            _applicantRepo = applicantRepo;
        }

        // GET: api/values
        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
