using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hunter6.Models
{
    public class ProjectManager
    {
        readonly List<Project> _projects = new List<Project>() {
            new Project { ID = 1, Name = "T360"},
            new Project { ID = 2, Name ="ACE"},
            new Project { ID = 3, Name ="VIACode"},
            new Project { ID = 4, Name ="Angular"},
            new Project { ID = 5, Name ="M-Packs"},
        };
        public IEnumerable<Project> GetAll { get { return _projects; } }
        
        public Project GetProjectByID(int Id)
        {
            return _projects.Find(o => o.ID == Id);
        }
    }
}
