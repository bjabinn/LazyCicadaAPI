using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using LazyCicada.API.Models;

namespace LazyCicada.API.Repositories
{    
    public interface IEmployeeDTORepository
    {
        IEnumerable<EmployeeDTO> Get();
        EmployeeDTO GetByPk(long pk);
        EmployeeDTO GetByEmployeeNumber(long employeeNumber);
        IEnumerable<EmployeeDTO> GetBySamAccountName(string samAccountName);
        IEnumerable<EmployeeDTO> GetByDisplayName(string displayName);
        IEnumerable<EmployeeDTO> GetByName(string name);
        IEnumerable<EmployeeDTO> GetByRoleName(string roleName);
    }
}
