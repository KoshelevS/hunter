using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hunter.Domain.Core;
using Hunter.Domain.Interfaces;
using Microsoft.Data.Entity;

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
            throw new System.NotImplementedException();
        }

        public void Create(Applicant item)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Applicant item)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Applicant>> GetAllAsync()
        {
            return _context.Applicant.ToListAsync();
        }

        public Task<Applicant> GetAsync(int id)
        {
            throw new System.NotImplementedException();
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

        public Task UpdateAsync(Applicant item)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
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