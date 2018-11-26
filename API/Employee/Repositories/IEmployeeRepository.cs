using System.Collections.Generic;

using LazyCicada.API.Models;

namespace LazyCicada.API.Repositories
{
    
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Employee GetByPk(long pk);
        Employee GetByEmployeeNumber(long employeeNumber);
    }
}
