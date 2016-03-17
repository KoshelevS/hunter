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

        public ProjectController(IRepository<Project> projectService)
        {
            _projectService = projectService;
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

        [HttpPost]
        public void Create([FromBody] ProjectViewModel project)
        {
            var projectDto = new Project()
            {
                Name = project.Name
            };

            _projectService.Create(projectDto);
        }

        [HttpPut("{id}")]
        public void Update(int id, [FromBody] ProjectViewModel project)
        {
            var projectDto = _projectService.Get(id);
            projectDto.Name = project.Name;
            _projectService.Update(projectDto);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            if (id % 2 == 0)
            {
                _projectService.Delete(id);
            }
            else
            {
                throw new Exception("Dummy error");
            }
        }
    }
}