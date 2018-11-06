using System;
using System.Collections.Generic;

namespace LazyCicadaApi.Models
{
    public partial class Employee
    {
        public long EpyPk { get; set; }
        public long EpyNumber { get; set; }
        public string EpyFirstName { get; set; }
        public string EpyLastName { get; set; }
        public string EpyShortName { get; set; }
    }
}
