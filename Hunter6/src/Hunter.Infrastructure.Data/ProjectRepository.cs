﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Hunter.Domain.Interfaces;
using Hunter.Domain.Core;
using Microsoft.EntityFrameworkCore;


namespace Hunter.Infrastructure.Data
{
    public class ProjectRepository : IRepository<Project>
    {
        private readonly DomainContext _context;

        public ProjectRepository(DomainContext context)
        {
            _context = context;
        }

        public IEnumerable<Project> GetAll()
        {
            var projects = _context.Project.Include(p => p.Vacancies);
            return projects.AsEnumerable();
        }

        public Task<List<Project>> GetAllAsync()
        {
            var projects = _context.Project.Include(p => p.Vacancies);
            return projects.ToListAsync();
        }

        public Project Get(int id)
        {
            return _context.Project.FirstOrDefault(o => o.Id == id);
        }

        public Task<Project> GetAsync(int id)
        {
            return _context.Project.FirstOrDefaultAsync(m => m.Id == id);
        }

        public void Create(Project item)
        {
            _context.Project.Add(item);
            _context.SaveChanges();
        }

        public async Task CreateAsync(Project project)
        {
            _context.Project.Add(project);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProjectExists(project.Id))
                {
                    throw new ItemAlreadyExistsException();
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task UpdateAsync(Project project)
        {
            _context.Entry(project).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(project.Id))
                {
                    throw new RowNotFoundException();
                }
                else
                {
                    throw;
                }
            }
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

        public async Task DeleteAsync(int id)
        {
            var project = Get(id);
            _context.Project.Remove(project);
            await _context.SaveChangesAsync();
        }

        private bool ProjectExists(int id)
        {
            return _context.Project.Any(project => project.Id == id);
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

    public class RowNotFoundException : Exception
    {
    }

    public class ItemAlreadyExistsException : Exception
    {
    }
}
