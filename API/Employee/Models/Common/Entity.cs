namespace LazyCicada.API.Models
{
    public abstract class BaseEntity { 
    
    }

    public abstract class Entity<T> : BaseEntity, IEntity<T> 
    {
        public virtual T Pk { get; set; }
    }
}
