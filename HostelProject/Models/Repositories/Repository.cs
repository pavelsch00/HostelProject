using HostelProject.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HostelProject.Models.Repositories
{
    public class Repository<T> : IRepository<T>
                where T : class
    {
        private readonly DbContext _dbContext;

        protected DbSet<T> DbSet { get; }

        public Repository(HostelDbContext dbContext)
        {
            _dbContext = dbContext;
            DbSet = _dbContext.Set<T>();
        }

        public async Task<T> Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Edit(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Edit(int id, T entity)
        {
            _dbContext.Entry(DbSet.Find(id)).State = EntityState.Detached;
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }


        public IQueryable<T> GetAll()
        {
            var result = _dbContext.Set<T>();

            return result;
        }

        public async Task<T> GetById(object id)
        {
            var result = await _dbContext.Set<T>().FindAsync(id);

            return result;
        }
    }
}
