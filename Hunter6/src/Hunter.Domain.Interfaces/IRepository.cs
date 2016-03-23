using System.Collections.Generic;
using System.Threading.Tasks;
using Hunter.Domain.Core;

namespace Hunter.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        Task<Project> GetAsync(int id);
        Task UpdateAsync(Project project);
    }
}
