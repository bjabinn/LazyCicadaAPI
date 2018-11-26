using System.Linq;
using Microsoft.EntityFrameworkCore;

using LazyCicada.API.Models;

namespace LazyCicada.API.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DbContext context)
            : base(context)
        {
        }
        public Employee GetByPk(long pk)
            //=> _dbset.Include(e => e.RoleEmployee).Where(e => e.Pk == pk).FirstOrDefault();
            => _dbset.Where(e => e.Pk == pk).FirstOrDefault();
        public Employee GetByEmployeeNumber(long employeeNumber)
            //=> _dbset.Include(e => e.RoleEmployee).Where(e => e.EmployeeNumber == employeeNumber).FirstOrDefault();
            => _dbset.Where(e => e.EmployeeNumber == employeeNumber).FirstOrDefault();
    }
}