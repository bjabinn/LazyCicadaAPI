using System.Linq;
using Microsoft.EntityFrameworkCore;

using LazyCicada.API.Models;

namespace LazyCicada.API.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(DbContext context)
            : base(context)
        {
        }
        public Role GetByPk(long pk)
            //=> _dbset.Include(r => r.RoleEmployee).Where(r => r.Pk == pk).FirstOrDefault();
            => _dbset.Where(r => r.Pk == pk).FirstOrDefault();
    }
}