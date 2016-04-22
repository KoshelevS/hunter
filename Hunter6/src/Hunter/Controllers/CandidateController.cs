using System.Threading.Tasks;
using Hunter.Domain.Core;
using Hunter.Domain.Interfaces;
using Hunter.Infrastructure.Data;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Http;
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

        // POST: api/project
        [HttpPost]
        public async Task<IActionResult> PostCandidate([FromBody] Applicant item)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }
            try
            {
                await _applicantRepo.CreateAsync(item);
            }
            catch (ItemAlreadyExistsException)
            {
                return new HttpStatusCodeResult(StatusCodes.Status409Conflict);
            }
            return CreatedAtRoute("Get", new { id = item.Id }, item);
        }

    }
}
