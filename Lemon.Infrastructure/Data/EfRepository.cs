using Lemon.Core.Entities;
using Lemon.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lemon.Infrastructure.Data
{
    public class EfRepository<T> : IRepository<T>, IAsyncRepository<T> where T : BaseEntity
    {
        private readonly LemonContext _dbContext;

        public EfRepository(LemonContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().AsEnumerable();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public T Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public bool Exists(int id)
        {
            return _dbContext.Set<T>().Any(x => x.Id == id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _dbContext.Set<T>().AnyAsync(x => x.Id == id);
        }
    }
}
