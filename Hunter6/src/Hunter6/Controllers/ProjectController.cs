using System.Collections.Generic;
using Hunter6.Data;
using Microsoft.AspNet.Mvc;
using Hunter6.Models;

namespace Hunter6.Controllers
{
    [Route("api/[controller]")]
    public class ProjectController : Controller
    {
        private readonly IProjectManager _projectService;
        private readonly ProjectContext _context;

        public ProjectController(IProjectManager projectService, ProjectContext context)
        {
            _projectService = projectService;
            _context = context;
        }

        //GET: api/values
        [HttpGet]
        public IEnumerable<Project> Get()
        {
            ProjectManager pm = new ProjectManager();
            return _projectService.GetAll();
        }

        // GET: api/values/2
        [HttpGet("{id}")]
        public Project Get(int id)
        {
            return _projectService.GetProjectByID(id);
        }

        [HttpPost]
        public async void Save(Project project)
        {
            _context.Update(project);
            await _context.SaveChangesAsync();
        }
    }
}