using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Hunter.Domain.Core;
using Hunter.Infrastructure.Data;

namespace Hunter6.Controllers
{
    [Produces("application/json")]
    [Route("api/ProjectsNew")]
    public class ProjectsNewController : Controller
    {
        private ProjectContext _context;

        public ProjectsNewController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/ProjectsNew
        [HttpGet]
        public IEnumerable<Project> GetProject()
        {
            return _context.Project;
        }

        // GET: api/ProjectsNew/5
        [HttpGet("{id}", Name = "GetProject")]
        public async Task<IActionResult> GetProject([FromRoute] int id)
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

            return Ok(project);
        }

        // PUT: api/ProjectsNew/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject([FromRoute] int id, [FromBody] Project project)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            if (id != project.Id)
            {
                return HttpBadRequest();
            }

            _context.Entry(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
                {
                    return HttpNotFound();
                }
                else
                {
                    throw;
                }
            }

            return new HttpStatusCodeResult(StatusCodes.Status204NoContent);
        }

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

        private bool ProjectExists(int id)
        {
            return _context.Project.Count(e => e.Id == id) > 0;
        }
    }
}