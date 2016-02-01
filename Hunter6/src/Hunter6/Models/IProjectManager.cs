using System.Collections.Generic;

namespace Hunter6.Models
{
    public interface IProjectManager
    {
        IEnumerable<Project> GetAll();
        Project GetProjectByID(int Id);
    }
}