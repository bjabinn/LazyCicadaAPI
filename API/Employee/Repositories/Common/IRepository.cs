using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using LazyCicada.API.Models;

namespace LazyCicada.API.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> Get();
        IEnumerable<T> GetBy(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
        void Save();
    }
}
