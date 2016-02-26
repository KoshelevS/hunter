using System.Collections.Generic;
using Hunter.Domain.Interfaces;
using Hunter.Domain.Core;

namespace Hunter.Infrastructure.Data
{
    public class ProjectRepository: IRepository<Project>
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

        public IEnumerable<Project> GetAll()
        {
            return _projects;
        }

        public Project Get(int id)
        {
            return _projects.Find(o => o.Id == id);
        }

        public void Create(Project item)
        {
            _projects.Add(item);
        }

        public void Update(Project item)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            var project = _projects.Find(o => o.Id == id);
            _projects.Remove(project);
        }
    }
}
