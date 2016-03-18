using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
//using System.Transactions;
using Hunter.Domain.Interfaces;
using Hunter.Domain.Core;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Storage;

namespace Hunter.Infrastructure.Data
{
    public class ProjectRepository : IRepository<Project>
    {
        private readonly ProjectContext _context;


        public ProjectRepository(ProjectContext context)
        {
            _context = context;
        }

        public IEnumerable<Project> GetAll()
        {

            var projects = _context.Project.Include(p => p.Vacancies);

            //            foreach (var v in _context.Project.FirstOrDefault()?.Vacancies)
            //            {
            //                    Debug.WriteLine(v.Name);
            //            }

            return projects.AsEnumerable();
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

        public void Update(Project item)
        {
            _context.Update(item);
            _context.SaveChanges();
            //using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            //{
            //_context.Database.OpenConnection();
            //IRelationalTransaction t = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            //_context.Database.UseTransaction(dbContextTransaction.UnderlyingTransaction);
            //await _context.SaveChangesAsync();
            //    scope.Complete();
            //}
            // see also https://msdn.microsoft.com/en-us/data/dn456843.aspx
        }

        public void Delete(int id)
        {
            var project = Get(id);
            _context.Remove(project);
            _context.SaveChanges();
        }
    }
}
