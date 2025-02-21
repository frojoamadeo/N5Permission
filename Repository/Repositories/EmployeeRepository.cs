using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public sealed class EmployeeRepository : IEmployeeRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<Employee> _dbSet;

        public EmployeeRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Employee>();
        }

        public Task AddAsync(Employee entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Employee entity)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Employee>> ListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Employee>> ListAsync(Expression<Func<Employee, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Employee entity)
        {
            throw new NotImplementedException();
        }
    }
}
