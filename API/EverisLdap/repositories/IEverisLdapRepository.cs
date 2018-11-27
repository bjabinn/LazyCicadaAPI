using System.DirectoryServices;
using EverisLdap.Models;
using System.Collections.Generic;

namespace EverisLdap.Repositories
{
    public interface IEverisLdapRepository
    {
        bool checkUser(UserRequest user);
        List<SearchInfoResponse> getDirectorySearcher(SearchInfoRequest se); 
        void getUsersPrincipal(string stringSearch, int pageSize=5);
        PropertyCollection getInfoUser(string nombreCorto);
    }
}