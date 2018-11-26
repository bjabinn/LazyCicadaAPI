using System.Collections.Generic;

using LazyCicada.API.Models;

namespace LazyCicada.API.Services
{   
    public interface IRoleService : IEntityService<Role>
    {
        Role GetByPk(long pk);
        IEnumerable<Role> GetByName(string name);
    }
}
