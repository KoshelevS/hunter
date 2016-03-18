using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using Hunter.Domain.Core;
using Hunter.Domain.Interfaces;
using Hunter.Infrastructure.Data;
using Hunter.ViewModels.Project;
using Microsoft.AspNet.Mvc;
namespace Hunter.Controllers
{
    [Route("api/[controller]")]
    public class ProjectController : ApiController
    {
        private readonly IRepository<Project> _projectService;

        public ProjectController(IRepository<Project> projectService)
        {
            _projectService = projectService;
        }

        //GET: api/values
        [HttpGet]
        public IActionResult /*IEnumerable<Project>*/ Get()
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
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var project = _projectService.Get(id);
            if (project == null)
            {
                return NotFound();
                //throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            var result = new ProjectViewModel
            {
                ID = project.Id,
                Name = project.Name,
            };
            return Ok(result);
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
        public HttpResponseMessage Update(int id, [FromBody] ProjectViewModel project)
        {
            var projectDto = _projectService.Get(id);
            if (projectDto == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no project found");
            }
            projectDto.Name = project.Name;
            _projectService.Update(projectDto);
            return Request.CreateResponse(HttpStatusCode.OK);
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