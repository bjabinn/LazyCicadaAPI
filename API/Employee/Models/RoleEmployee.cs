using System;
using System.Collections.Generic;

namespace LazyCicada.API.Models
{
    public partial class RoleEmployee : Entity<long>
    {
        // public long Pk { get; set; }
        public long Role { get; set; }
        public long Employee { get; set; }

        public Employee EmployeeNavigation { get; set; }
        public Role RoleNavigation { get; set; }
    }
}
