using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Web.Caching;

using Hunter.Domain.Core;
using Hunter.Domain.Interfaces;
using Hunter.Infrastructure.Data;
using Hunter.ViewModels.Project;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;

namespace Hunter.Controllers
{
    [Produces("application/json")]
    [Route("api/project/")]
    public class ProjectController : Controller
    {
        private readonly IRepository<Project> _projectRepo;

        public ProjectController(IRepository<Project> projectRepo)
        {
            _projectRepo = projectRepo;
        }

        //public IActionResult /*IEnumerable<object>*/ GetAll()
        // GET: api/project
        [HttpGet]
        public async Task<IEnumerable<ProjectViewModel>> GetAll()
        {
            var projects =
                from p in _projectRepo.GetAll()
                let v = p.Vacancies.FirstOrDefault()
                select new ProjectViewModel
                {
                    ID = p.Id,
                    Name = p.Name,
                    FirstVacancy = v != null ? v.Name : string.Empty
                };
            //            return Ok(projects);
            return projects.AsEnumerable();
        }

        // GET: api/project/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            //Project project = await _context.Project.SingleAsync(m => m.Id == id);
            var project = await _projectRepo.GetAsync(id);

            if (project == null)
            {
                return HttpNotFound();
            }

            return Ok(project);
        }

        // PUT: api/project/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Project project)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            if (id != project.Id || project.Name == "Bad request")
            {
                return HttpBadRequest();
            }

            //_context.Entry(project).State = EntityState.Modified;

            try
            {
                await _projectRepo.UpdateAsync(project);
            }
            catch (RowNotFoundException)
            {
                return HttpNotFound();
            }

            return new HttpStatusCodeResult(StatusCodes.Status204NoContent);
        }

        // POST: api/project
        [HttpPost]
        public async Task<IActionResult> PostProject([FromBody] Project project)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }
            try
            {
                await _projectRepo.CreateAsync(project);
            }
            catch (ProjectExistsException)
            {
                return new HttpStatusCodeResult(StatusCodes.Status409Conflict);
            }
            return CreatedAtRoute("Get", new { id = project.Id }, project);
        }


        // DELETE: api/project/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            Project project = await _projectRepo.GetAsync(id);
            if (project == null || project.Name == "Not Found")
            {
                return HttpNotFound();
            }

            await _projectRepo.DeleteAsync(id);
            return Ok(project);
        }

        //        protected override void Dispose(bool disposing)
        //        {
        //            if (disposing)
        //            {
        //                _context.Dispose();
        //            }
        //            base.Dispose(disposing);
        //        }
    }
}