using System;
using System.Collections.Generic;

namespace Hunter6.Models
{
    public class ProjectManager : IProjectManager
    {
        readonly List<Project> _projects = new List<Project>() {
            new Project { ProjectId = 1, Name = "T360",
                Vacancies = new List<Vacancy>
                {
                    new Vacancy {VacancyId = 1, Name = ".Net"},
                    new Vacancy {VacancyId = 2, Name = "ASP.Net"},
                    new Vacancy {VacancyId = 3, Name = "Angular"},
                    new Vacancy {VacancyId = 4, Name = "MVC6"}
                } },
            new Project
            {
                ProjectId = 2, Name ="ACE",
                Vacancies = new List<Vacancy>
                {
                    new Vacancy {VacancyId = 1, Name = ".Net"},
                    new Vacancy {VacancyId = 2, Name = "ASP.Net"},
                    new Vacancy {VacancyId = 3, Name = "Angular"},
                    new Vacancy {VacancyId = 4, Name = "MVC6"}
                }
            },
            new Project { ProjectId = 3, Name ="VIACode"},
            new Project { ProjectId = 4, Name ="Angular"},
            new Project { ProjectId = 5, Name ="M-Packs"},
        };
        public IEnumerable<Project> GetAll() { return _projects;  }
        
        public Project GetProjectByID(int id)
        {
            return _projects.Find(o => o.ProjectId == id);
        }
    }
}
