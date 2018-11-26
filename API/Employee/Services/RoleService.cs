using System.Collections.Generic;

using LazyCicada.API.Models;
using LazyCicada.API.Repositories;
using LazyCicada.API.Helpers;

namespace LazyCicada.API.Services
{
    public class RoleService : EntityService<Role>, IRoleService
    {
        IUnitOfWork _unitOfWork;
        IRoleRepository _roleRepository;
        public RoleService(IUnitOfWork unitOfWork, IRoleRepository roleRepository)
            : base(unitOfWork, roleRepository)
        {
            _unitOfWork = unitOfWork;
            _roleRepository = roleRepository;
        }
        public Role GetByPk(long pk)
            => _roleRepository.GetByPk(pk);
        public IEnumerable<Role> GetByName(string name)
        {
            string normalized = Helper.RemoveAccentsWithNormalization(name.ToUpperInvariant());
            return _roleRepository.GetBy(r => (Helper.RemoveAccentsWithNormalization(r.Name.ToUpperInvariant()).Contains(normalized)));
        }
    }
}