using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hunter.Domain.Core;
using Hunter.Domain.Interfaces;
using Microsoft.Data.Entity;
using EntityFrameworkQueryableExtensions = Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions;

namespace Hunter.Infrastructure.Data
{
    public class ApplicantRepository : IRepository<Applicant>
    {
        private readonly DomainContext _context;

        public ApplicantRepository(DomainContext context)
        {
            _context = context;
        }

        public IEnumerable<Applicant> GetAll()
        {
            return _context.Applicant.AsEnumerable();
        }

        public Applicant Get(int id)
        {
            return _context.Applicant.FirstOrDefault(applicant => applicant.Id == id);
        }

        public void Create(Applicant item)
        {
            _context.Applicant.Add(item);
            _context.SaveChanges();
        }

        public void Update(Applicant item)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            var item = Get(id);
            _context.Remove(item);
            _context.SaveChanges();
        }

        public Task<List<Applicant>> GetAllAsync()
        {
            return EntityFrameworkQueryableExtensions.ToListAsync(_context.Applicant);
        }

        public Task<Applicant> GetAsync(int id)
        {
            return EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(_context.Applicant, (applicant => applicant.Id == id));
        }

        public async Task CreateAsync(Applicant item)
        {
            _context.Applicant.Add(item);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ApplicantExists(item.Id))
                {
                    throw new ItemAlreadyExistsException();
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task UpdateAsync(Applicant item)
        {
            _context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicantExists(item.Id))
                {
                    throw new RowNotFoundException();
                }
                else
                {
                    throw;
                }
            }
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

        private bool ApplicantExists(int id)
        {
            return _context.Applicant.Any(applicant => applicant.Id == id);
        }

    }
}