using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using Hunter6.Models;

namespace Hunter6.Controllers
{
    [Route("api/[controller]")]
    public class ProjectController : Controller
    {
        private readonly IProjectManager _projectService;

        public ProjectController(IProjectManager projectService)
        {
            this._projectService = projectService;
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
    }
}