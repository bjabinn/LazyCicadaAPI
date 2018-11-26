using System;
using System.Collections.Generic;

namespace LazyCicada.API.Models
{
    public partial class Employee : Entity<long>
    {
        public Employee()
        {
            RoleEmployee = new HashSet<RoleEmployee>();
        }

        // public long Pk { get; set; }
        public long EmployeeNumber { get; set; }
        public string SamAccountName { get; set; }
        public string DisplayName { get; set; }
        public ICollection<RoleEmployee> RoleEmployee { get; set; }

    }
}
