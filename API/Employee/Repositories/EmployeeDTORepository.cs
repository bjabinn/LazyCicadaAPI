using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using LazyCicada.API.Models;
using LazyCicada.API.Helpers;

namespace LazyCicada.API.Repositories
{
    public class EmployeeDTORepository: IEmployeeDTORepository
    {
        private DbContext _dbcontext;
        private readonly DbSet<Employee> _dbSetEmployee;
        private readonly DbSet<Role> _dbSetRole;
        private readonly DbSet<RoleEmployee> _dbSetRoleEmployee;

        public EmployeeDTORepository(DbContext context)
        {
            _dbcontext = context;
            _dbSetEmployee = context.Set<Employee>();
            _dbSetRole = context.Set<Role>();
            _dbSetRoleEmployee = context.Set<RoleEmployee>();
        }
        public virtual IEnumerable<EmployeeDTO> Get()
        {
            var employees =
                from e in _dbSetEmployee
                let roles = (
                    from r in _dbSetRole
                    join re in _dbSetRoleEmployee on r.Pk equals re.Role
                    where e.Pk == re.Employee
                    select r.Name
                )
                select new EmployeeDTO()
                {
                    Pk = e.Pk,
                    EmployeeNumber = e.EmployeeNumber,
                    SamAccountName = e.SamAccountName,
                    DisplayName = e.DisplayName,
                    Roles = roles
                };
            return employees;
        }
        public virtual EmployeeDTO GetByPk(long pk)
        {
            var employee =
                from e in _dbSetEmployee
                let roles = (
                    from r in _dbSetRole
                    join re in _dbSetRoleEmployee on r.Pk equals re.Role
                    where e.Pk == re.Employee
                    select r.Name
                )
                let filter = (e.Pk == pk)
                where filter
                select new EmployeeDTO()
                {
                    Pk = e.Pk,
                    EmployeeNumber = e.EmployeeNumber,
                    SamAccountName = e.SamAccountName,
                    DisplayName = e.DisplayName,
                    Roles = roles
                };
            return employee.FirstOrDefault();
        }
        public virtual EmployeeDTO GetByEmployeeNumber(long employeeNumber)
        {
            var employee =
                from e in _dbSetEmployee
                let roles = (
                    from r in _dbSetRole
                    join re in _dbSetRoleEmployee on r.Pk equals re.Role
                    where e.Pk == re.Employee
                    select r.Name
                )
                let filter = (e.EmployeeNumber == employeeNumber)
                where filter
                select new EmployeeDTO()
                {
                    Pk = e.Pk,
                    EmployeeNumber = e.EmployeeNumber,
                    SamAccountName = e.SamAccountName,
                    DisplayName = e.DisplayName,
                    Roles = roles
                };
            return employee.FirstOrDefault();
        }
        public virtual IEnumerable<EmployeeDTO> GetBySamAccountName(string samAccountName)
        {
            string normalized = Helper.RemoveAccentsWithNormalization(samAccountName.ToUpperInvariant());
            var employees =
                from e in _dbSetEmployee
                let roles = (
                    from r in _dbSetRole
                    join re in _dbSetRoleEmployee on r.Pk equals re.Role
                    where e.Pk == re.Employee
                    select r.Name
                )
                let filter = Helper.RemoveAccentsWithNormalization(e.SamAccountName.ToUpperInvariant()).Contains(normalized) 
                where filter
                select new EmployeeDTO()
                {
                    Pk = e.Pk,
                    EmployeeNumber = e.EmployeeNumber,
                    SamAccountName = e.SamAccountName,
                    DisplayName = e.DisplayName,
                    Roles = roles
                };
            return employees;
        }
        public virtual IEnumerable<EmployeeDTO> GetByDisplayName(string displayName)
        {
            string normalized = Helper.RemoveAccentsWithNormalization(displayName.ToUpperInvariant());
            var employees =
                from e in _dbSetEmployee
                let roles = (
                    from r in _dbSetRole
                    join re in _dbSetRoleEmployee on r.Pk equals re.Role
                    where e.Pk == re.Employee
                    select r.Name
                )
                let filter = Helper.RemoveAccentsWithNormalization(e.DisplayName.ToUpperInvariant()).Contains(normalized)
                where filter
                select new EmployeeDTO()
                {
                    Pk = e.Pk,
                    EmployeeNumber = e.EmployeeNumber,
                    SamAccountName = e.SamAccountName,
                    DisplayName = e.DisplayName,
                    Roles = roles
                };
            return employees;
        }
        public virtual IEnumerable<EmployeeDTO> GetByName(string name)
        {
            string normalized = Helper.RemoveAccentsWithNormalization(name.ToUpperInvariant());
            var employees =
                from e in _dbSetEmployee
                let roles = (
                    from r in _dbSetRole
                    join re in _dbSetRoleEmployee on r.Pk equals re.Role
                    where e.Pk == re.Employee
                    select r.Name
                )
                let filter = (
                    Helper.RemoveAccentsWithNormalization(e.SamAccountName.ToUpperInvariant()).Contains(normalized) ||
                    Helper.RemoveAccentsWithNormalization(e.DisplayName.ToUpperInvariant()).Contains(normalized)
                )
                where filter
                select new EmployeeDTO()
                {
                    Pk = e.Pk,
                    EmployeeNumber = e.EmployeeNumber,
                    SamAccountName = e.SamAccountName,
                    DisplayName = e.DisplayName,
                    Roles = roles
                };
            return employees;
        }
        public virtual IEnumerable<EmployeeDTO> GetByRoleName(string roleName)
        {
            string normalized = Helper.RemoveAccentsWithNormalization(roleName.ToUpperInvariant());
            return this.Get().Where(e
                => e.Roles.Any(r
                    => Helper.RemoveAccentsWithNormalization(r.ToUpperInvariant()).Contains(normalized)
                )
            );
        }
    }
}