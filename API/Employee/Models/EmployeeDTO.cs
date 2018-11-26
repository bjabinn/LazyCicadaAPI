using System;
using System.Collections.Generic;

namespace LazyCicada.API.Models
{
    public partial class EmployeeDTO : Entity<long>
    {
        public EmployeeDTO() {}
        public long EmployeeNumber { get; set; }
        public string SamAccountName { get; set; }
        public string DisplayName { get; set; }
        public IEnumerable<String> Roles { get; set; }
    }
}
