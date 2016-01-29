using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Hunter6.Models;
using System.Threading;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Hunter6.Controllers
{
    [Route("api/[controller]")]
    public class ProjectController : Controller
    {
        //GET: api/values
        [HttpGet]
        public IEnumerable<Project> Get()
        {
            ProjectManager pm = new ProjectManager();
            return pm.GetAll;
        }

        // GET: api/values/2
        [HttpGet("{id}")]
        public Project Get(int id)
        {
            ProjectManager pm = new ProjectManager();
            return pm.GetProjectByID(id);
        }
    }
}