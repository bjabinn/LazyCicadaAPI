using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using LazyCicada.API.Models;
using LazyCicada.API.Repositories;
using LazyCicada.API.Helpers;

namespace LazyCicada.API.Services
{
    public class EmployeeDTOService : IEmployeeDTOService
    {
        IEmployeeDTORepository _employeeDTORepository;
        public EmployeeDTOService(IEmployeeDTORepository employeeDTORepository)
        {
           _employeeDTORepository = employeeDTORepository;
        }
        public IEnumerable<EmployeeDTO> Get()
         => _employeeDTORepository.Get();
        public EmployeeDTO GetByPk(long pk)
            => _employeeDTORepository.GetByPk(pk);
         public EmployeeDTO GetByEmployeeNumber(long employeeNumber)
            => _employeeDTORepository.GetByEmployeeNumber(employeeNumber);
         public IEnumerable<EmployeeDTO> GetBySamAccountName(string samAccountName)
            => _employeeDTORepository.GetBySamAccountName(samAccountName);
         public IEnumerable<EmployeeDTO> GetByDisplayName(string displayName)
            => _employeeDTORepository.GetByDisplayName(displayName);
         public IEnumerable<EmployeeDTO> GetByName(string name)
            => _employeeDTORepository.GetByName(name);
         public IEnumerable<EmployeeDTO> GetByRoleName(string roleName)
            => _employeeDTORepository.GetByRoleName(roleName);
    }
}