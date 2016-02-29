using System;
using System.Collections.Generic;
using System.Threading;
using Hunter.Domain.Core;
using Hunter.Domain.Interfaces;
using Hunter.Infrastructure.Data;
using Hunter6.ViewModels.Project;
using Microsoft.AspNet.Mvc;

namespace Hunter.Controllers
{
    [Route("api/[controller]")]
    public class ProjectController : Controller
    {
        private readonly IRepository<Project> _projectService;
        private readonly ProjectContext _context;

        public ProjectController(IRepository<Project> projectService, ProjectContext context)
        {
            _projectService = projectService;
            _context = context;
        }

        //GET: api/values
        [HttpGet]
        public IEnumerable<Project> Get()
        {
            return _projectService.GetAll();
        }

        [HttpGet("{id}")]
        public ProjectViewModel Get(int id)
        {
            var project = _projectService.Get(id);

            return new ProjectViewModel()
            {
                ID = project.Id,
                Name = project.Name
            };
        }

        [HttpPut("{id}")]
        public void Update(int id, [FromBody] ProjectViewModel project)
        {
            var projectDto = _projectService.Get(id);
            projectDto.Name = project.Name;
            Save(projectDto);
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