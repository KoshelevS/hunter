using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hunter.Domain.Core;

namespace Hunter.Domain.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        IAsyncEnumerable<Project> GetAllAsync();
        Task<Project> GetAsync(int id);
        Task CreateAsync(Project project);
        Task UpdateAsync(Project project);
        Task DeleteAsync(int id);
    }
}
