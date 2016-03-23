using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hunter.Domain.Core;
using Hunter.Domain.Interfaces;
using Hunter.Infrastructure.Data;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;

namespace Hunter.Controllers
{
    [Produces("application/json")]
    [Route("api/ProjectsNew")]
    public class ProjectsNewController : Controller
    {
        private readonly IRepository<Project> _projectService;

        public ProjectsNewController(IRepository<Project> projectService)
        {
            _projectService = projectService;
        }

        //private readonly ProjectContext _context;
        //        public ProjectsNewController(ProjectContext context)
        //        {
        //            _context = context;
        //        }

        // GET: api/ProjectsNew
        [HttpGet]
        public IActionResult /*IEnumerable<object>*/ Get()
        {
            var projects =
                from p in _projectService.GetAll()
                let v = p.Vacancies.FirstOrDefault()
                select new
                {
                    ID = p.Id,
                    Name = p.Name,
                    FirstVacancy = v != null ? v.Name : string.Empty
                };
            return Ok(projects);
//            return projects.AsEnumerable();
        }

        // GET: api/ProjectsNew/5
//        [HttpGet("{id}", Name = "GetProject")]
//        public async Task<IActionResult> GetProject([FromRoute] int id)
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            //Project project = await _context.Project.SingleAsync(m => m.Id == id);
            var project = await _projectService.GetAsync(id);

            if (project == null)
            {
                return HttpNotFound();
            }

            return Ok(project);
        }

        // PUT: api/ProjectsNew/5
        [HttpPut("{id}")]
//        public async Task<IActionResult> PutProject([FromRoute] int id, [FromBody] Project project)
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Project project)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            if (id != project.Id)
            {
                return HttpBadRequest();
            }

            //_context.Entry(project).State = EntityState.Modified;

            try
            {
                await _projectService.UpdateAsync(project);
            }
            catch (RowNotFoundException)
            {
                    return HttpNotFound();
            }

            return new HttpStatusCodeResult(StatusCodes.Status204NoContent);
        }
        /*
        // POST: api/ProjectsNew
        [HttpPost]
        public async Task<IActionResult> PostProject([FromBody] Project project)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            _context.Project.Add(project);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProjectExists(project.Id))
                {
                    return new HttpStatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetProject", new { id = project.Id }, project);
        }

        // DELETE: api/ProjectsNew/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            Project project = await _context.Project.SingleAsync(m => m.Id == id);
            if (project == null)
            {
                return HttpNotFound();
            }

            _context.Project.Remove(project);
            await _context.SaveChangesAsync();

            return Ok(project);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
        */
    }
}