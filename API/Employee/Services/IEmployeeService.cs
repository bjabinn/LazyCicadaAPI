using System.Collections.Generic;

using LazyCicada.API.Models;

namespace LazyCicada.API.Services
{
    public interface IEmployeeService : IEntityService<Employee>
    {
        Employee GetByPk(long pk);
        Employee GetByEmployeeNumber(long employeeNumber);
        IEnumerable<Employee> GetBySamAccountName(string samAccountName);
        IEnumerable<Employee> GetByDisplayName(string displayName);
        IEnumerable<Employee> GetByName(string name);
    }
}
