using System.Collections.Generic;

using LazyCicada.API.Models;
using LazyCicada.API.Repositories;
using LazyCicada.API.Helpers;

namespace LazyCicada.API.Services
{
    public class EmployeeService : EntityService<Employee>, IEmployeeService
    {
        IUnitOfWork _unitOfWork;
        IEmployeeRepository _employeeRepository;
        public EmployeeService(IUnitOfWork unitOfWork, IEmployeeRepository employeeRepository)
            : base(unitOfWork, employeeRepository)
        {
            _unitOfWork = unitOfWork;
            _employeeRepository = employeeRepository;
        }
        public Employee GetByPk(long pk)
            => _employeeRepository.GetByPk(pk);
        public Employee GetByEmployeeNumber(long employeeNumber)
            => _employeeRepository.GetByEmployeeNumber(employeeNumber);
        public IEnumerable<Employee> GetBySamAccountName(string samAccountName)
        {
            string normalized = Helper.RemoveAccentsWithNormalization(samAccountName.ToUpperInvariant());
            return _employeeRepository.GetBy(e => (Helper.RemoveAccentsWithNormalization(e.SamAccountName.ToUpperInvariant()).Contains(normalized)));
        }
        public IEnumerable<Employee> GetByDisplayName(string displayName)
        {
            string normalized = Helper.RemoveAccentsWithNormalization(displayName.ToUpperInvariant());
            return _employeeRepository.GetBy(e => (Helper.RemoveAccentsWithNormalization(e.DisplayName.ToUpperInvariant()).Contains(normalized)));
        }
        public IEnumerable<Employee> GetByName(string name)
        {
            string normalized = Helper.RemoveAccentsWithNormalization(name.ToUpperInvariant());
            return _employeeRepository.GetBy(
                e => (
                    Helper.RemoveAccentsWithNormalization(e.SamAccountName.ToUpperInvariant()).Contains(normalized) ||
                    Helper.RemoveAccentsWithNormalization(e.DisplayName.ToUpperInvariant()).Contains(normalized)
                )
            );
        }
    }
}