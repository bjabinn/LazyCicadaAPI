using System.Collections.Generic;

using LazyCicada.API.Models;

namespace LazyCicada.API.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Role GetByPk(long pk);
    }
}
