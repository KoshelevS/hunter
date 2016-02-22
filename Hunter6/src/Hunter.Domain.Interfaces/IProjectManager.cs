using System.Collections.Generic;
using Hunter.Domain.Core;

namespace Hunter.Domain.Interfaces
{
    public interface IProjectManager
    {
        IEnumerable<Project> GetAll();
        Project GetProjectById(int id);
    }
}