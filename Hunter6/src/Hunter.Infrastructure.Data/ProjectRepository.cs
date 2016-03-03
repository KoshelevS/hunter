using System.Collections.Generic;
using System.Linq;
using Hunter.Domain.Interfaces;
using Hunter.Domain.Core;

namespace Hunter.Infrastructure.Data
{
    public class ProjectRepository: IRepository<Project>
    {
        private readonly ProjectContext _context;


        public ProjectRepository(ProjectContext context)
        {
            _context = context;
        }

        public IEnumerable<Project> GetAll()
        {
            return _context.Project.AsEnumerable();
        }

        public Project Get(int id)
        {
            return _context.Project.SingleOrDefault(o => o.Id == id);
        }

        public void Create(Project item)
        {
            _context.Project.Add(item);
            _context.SaveChanges();
        }

        public async void Update(Project item)
        {
            _context.Update(item);
            //await _context.SaveChangesAsync();
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var project = Get(id);
            _context.Remove(project);
            _context.SaveChanges();
        }
    }
}
