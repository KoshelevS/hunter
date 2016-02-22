using System.Collections.Generic;
using Hunter.Domain.Core;
using Hunter.Domain.Interfaces;
using Hunter6.Data;
using Microsoft.AspNet.Mvc;
using Hunter6.Models;
using Hunter6.ViewModels.Project;

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

        [HttpGet("{id}")]
        public ProjectViewModel Get(int id)
        {
            var project = _projectService.GetProjectById(id);

            return new ProjectViewModel()
            {
                ID = project.Id,
                Name = project.Name
            };
        }

        [HttpPut("{id}")]
        public void Update(int id, ProjectViewModel project)
        {
            //Dummy method
        }

        [HttpPost]
        public async void Save(Project project)
        {
            _context.Update(project);
            await _context.SaveChangesAsync();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            //Dummy method
        }
    }
}