using System;
using System.Collections.Generic;
using Hunter.Domain.Core;
using Hunter.Domain.Interfaces;

namespace Hunter6.Models
{
    public class ProjectManager : IProjectManager
    {
        readonly List<Project> _projects = new List<Project>() {
            new Project { Id = 1, Name = "T360",
                Vacancies = new List<Vacancy>
                {
                    new Vacancy {Id = 1, Name = ".Net"},
                    new Vacancy {Id = 2, Name = "ASP.Net"},
                    new Vacancy {Id = 3, Name = "Angular"},
                    new Vacancy {Id = 4, Name = "MVC6"}
                } },
            new Project
            {
                Id = 2, Name ="ACE",
                Vacancies = new List<Vacancy>
                {
                    new Vacancy {Id = 1, Name = ".Net"},
                    new Vacancy {Id = 2, Name = "ASP.Net"},
                    new Vacancy {Id = 3, Name = "Angular"},
                    new Vacancy {Id = 4, Name = "MVC6"}
                }
            },
            new Project { Id = 3, Name ="VIACode"},
            new Project { Id = 4, Name ="Angular"},
            new Project { Id = 5, Name ="M-Packs"},
        };
        public IEnumerable<Project> GetAll() { return _projects;  }
        
        public Project GetProjectById(int id)
        {
            return _projects.Find(o => o.Id == id);
        }
    }
}
