using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hunter.Domain.Core;
using Hunter.Domain.Interfaces;
using EntityFrameworkQueryableExtensions = Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions;

namespace Hunter.Infrastructure.Data
{
    public class FileRepository : IRepository<File>
    {
        private readonly DomainContext _context;

        public FileRepository(DomainContext context)
        {
            _context = context;
        }

        public IEnumerable<File> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public File Get(int id)
        {
            return _context.File.FirstOrDefault(f => f.Id == id);
        }

        public void Create(File item)
        {
            _context.File.Add(item);
            _context.SaveChanges();
        }

        public void Update(File item)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            var item = Get(id);
            _context.Remove(item);
            _context.SaveChanges();
        }

        public Task<List<File>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<File> GetAsync(int id)
        {
            return EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(_context.File, (f => f.Id == id));
        }

        public async Task CreateAsync(File item)
        {
            _context.File.Add(item);
            await _context.SaveChangesAsync();
        }

        public Task UpdateAsync(File item)
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            var item = Get(id);
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}