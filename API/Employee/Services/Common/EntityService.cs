using System;
using System.Collections.Generic;

using LazyCicada.API.Models;
using LazyCicada.API.Repositories;

namespace LazyCicada.API.Services
{
    public abstract class EntityService<T> : IEntityService<T>
        where T : BaseEntity
    {
        IUnitOfWork _unitOfWork;
        IRepository<T> _repository;
        public EntityService(IUnitOfWork unitOfWork, IRepository<T> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }
        public virtual void Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _repository.Add(entity);
            _unitOfWork.Commit();
        }
        public virtual IEnumerable<T> Get()
        {
            return _repository.Get();
        }
        public virtual void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _repository.Edit(entity);
            _unitOfWork.Commit();
        }
        public virtual void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _repository.Delete(entity);
            _unitOfWork.Commit();
        }
    }
}
