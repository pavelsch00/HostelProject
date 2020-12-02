using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HostelProject.Interfaces
{
    public interface IRepository<T>
               where T : class
    {
        Task<T> GetById(object id);

        IQueryable<T> GetAll();

        IQueryable<T> GetBy(Expression<Func<T, bool>> predicate);

        Task<T> Add(T entity);

        Task Delete(T entity);

        Task Edit(T entity);

        Task Edit(int id, T entity);
    }
}
