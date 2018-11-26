using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

using LazyCicada.API.Models;

namespace LazyCicada.API.Repositories
{
    public abstract class Repository<T> : IRepository<T>
       where T : BaseEntity
    {
        protected DbContext _dbcontext;
        protected readonly DbSet<T> _dbset;

        public Repository(DbContext context)
        {
            _dbcontext = context;
            _dbset = context.Set<T>();
        }
        public virtual IEnumerable<T> Get() => _dbset.AsEnumerable<T>();
        public virtual IEnumerable<T> GetBy(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> query = _dbset.Where(predicate).AsEnumerable();
            return query;
        }
        public virtual void Add(T entity) => _dbset.Add(entity);
        public virtual void Delete(T entity) => _dbset.Remove(entity);
        public virtual void Edit(T entity) => _dbcontext.Entry(entity).State = EntityState.Modified;
        public virtual void Save() => _dbcontext.SaveChanges();
    }
}
