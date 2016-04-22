using System.Collections.Generic;
using System.Threading.Tasks;
using Hunter.Domain.Core;
using Hunter.Domain.Interfaces;
using Hunter.Infrastructure.Data;
using Hunter.ViewModels.Candidate;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using System.Linq;

namespace Hunter.Controllers
{
    [Route("api/[controller]")]
    public class ApplicantController : Controller
    {
        private readonly IRepository<Applicant> _applicantRepo;

        public ApplicantController(IRepository<Applicant> applicantRepo)
        {
            _applicantRepo = applicantRepo;
        }

        [HttpGet]
        [ResponseCache(NoStore = true)]
        public async Task<IEnumerable<ApplicantViewModel>> GetAll()
        {
            var applicants =
                from p in await _applicantRepo.GetAllAsync()
                select new ApplicantViewModel
                {
                    ID = p.Id,
                    Name = p.Name,
                };
            return applicants;
        }

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
