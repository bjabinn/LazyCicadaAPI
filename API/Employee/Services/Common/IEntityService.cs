using System.Collections.Generic;

using LazyCicada.API.Models;

namespace LazyCicada.API.Services
{
    public interface IEntityService<T> : IService
     where T : BaseEntity
    {
        void Create(T entity);
        IEnumerable<T> Get();
        void Update(T entity);
        void Delete(T entity);
    }
}
