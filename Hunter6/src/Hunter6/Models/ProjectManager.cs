using System;
using System.Collections.Generic;

namespace Hunter6.Models
{
    public class ProjectManager : IProjectManager
    {
        readonly List<Project> _projects = new List<Project>() {
            new Project { ID = 1, Name = "T360",
                Vacancies = new List<Vacancy>
                {
                    new Vacancy {ID = 1, Name = ".Net"},
                    new Vacancy {ID = 2, Name = "ASP.Net"},
                    new Vacancy {ID = 3, Name = "Angular"},
                    new Vacancy {ID = 4, Name = "MVC6"}
                } },
            new Project
            {
                ID = 2, Name ="ACE",
                Vacancies = new List<Vacancy>
                {
                    new Vacancy {ID = 1, Name = ".Net"},
                    new Vacancy {ID = 2, Name = "ASP.Net"},
                    new Vacancy {ID = 3, Name = "Angular"},
                    new Vacancy {ID = 4, Name = "MVC6"}
                }
            },
            new Project { ID = 3, Name ="VIACode"},
            new Project { ID = 4, Name ="Angular"},
            new Project { ID = 5, Name ="M-Packs"},
        };
        public IEnumerable<Project> GetAll() { return _projects;  }
        
        public Project GetProjectByID(int Id)
        {
            return _projects.Find(o => o.ID == Id);
        }
    }
}
