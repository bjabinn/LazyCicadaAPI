namespace LazyCicada.API.Models
{
    public interface IEntity<T> 
   {
       T Pk { get; set; }
   }
}
