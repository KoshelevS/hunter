﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Hunter.Domain.Core;
using Hunter.Domain.Interfaces;
using Hunter.Infrastructure.Data;
using Hunter.ViewModels.Applicant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [Authorize]
        [Route("[controller]")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [ResponseCache(NoStore = true)]
        public async Task<IEnumerable<ApplicantViewModel>> GetAll()
        {
            return
                from p in await _applicantRepo.GetAllAsync()
                select new ApplicantViewModel
                {
                    ID = p.Id,
                    Name = p.Name,
                    Address = p.Address,
                    Phone = p.Phone,
                    Birthday = p.Birthday,
                    Email = p.Email,
                };
        }

        // GET: http://localhost:55675/api/Applicant/8
        //[Route("api/[controller]/[action]/{id}")]
        [HttpGet("{id}", Name = "GetApplicant")]
        [ResponseCache(NoStore = true)]
        public async Task<IActionResult> GetApplicant([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var applicant = await _applicantRepo.GetAsync(id);

            if (applicant == null)
            {
                return NotFound();
            }

            return Ok(applicant);
        }

        [HttpPost]
        public async Task<IActionResult> PostApplicant([FromBody] Applicant item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _applicantRepo.CreateAsync(item);
            }
            catch (ItemAlreadyExistsException)
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            return CreatedAtRoute("GetApplicant", new { controller = "Applicant", id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Applicant item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != item.Id || item.Name == "Bad request")
            {
                return BadRequest();
            }

            //_context.Entry(project).State = EntityState.Modified;

            try
            {
                await _applicantRepo.UpdateAsync(item);
            }
            catch (RowNotFoundException)
            {
                return NotFound();
            }
            return new NoContentResult();
        }

        // DELETE: api/Applicant/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicant([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var applicant= await _applicantRepo.GetAsync(id);
            if (applicant == null)
            {
                return NotFound();
            }

            await _applicantRepo.DeleteAsync(id);
            return Ok(applicant);
        }

    }
}
