using System;
using System.Collections.Generic;

namespace LazyCicada.API.Models
{
    public partial class Role : Entity<long>
    {
        public Role()
        {
            RoleEmployee = new HashSet<RoleEmployee>();
        }

        // public long Pk { get; set; }
        public string Name { get; set; }

        public ICollection<RoleEmployee> RoleEmployee { get; set; }
    }
}
