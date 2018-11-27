using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using EverisLdap.Repositories;
using EverisLdap.Models;
using Microsoft.AspNetCore.Authorization;

namespace EverisLdap.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class EverisLdapController : Controller
    {
        // This is to catch the parameters in the attribute authorize
        IAuthorizationService authorizationService;
        IEverisLdapRepository everisLdapRepository;
        public EverisLdapController(IEverisLdapRepository everisLdapRepository, IAuthorizationService authorizationService) {
            this.everisLdapRepository = everisLdapRepository;
            this.authorizationService = authorizationService;
        }

        // Service to login Everis AD
        [Route("/api/CheckUser")]
        [HttpPost]
       // [Authorize]
        //[Authorize(AuthenticationSchemes=JwtBearerDefaults.AuthenticationScheme, Policy="Trusted")]
        public ActionResult checkUser(UserRequest user){
            if(string.IsNullOrEmpty(user.loginDn) || string.IsNullOrEmpty(user.password)){
                return NotFound();
            } else {
                var result=everisLdapRepository.checkUser(user);
                return Ok(result);
            }
        }
        [Route ("/api/DirectorySearcher")]
        [HttpPost]
        //[Authorize]
        public ActionResult<List<SearchInfoResponse>> getDirectorySearcher(SearchInfoRequest se) {
            var result=everisLdapRepository.getDirectorySearcher(se);
            return Ok(result);
        }

        [Route ("/api/validate")]
        [HttpPost]
        [Authorize (Policy="tokenValidation")]
        [Authorize (Policy="Administrator")]
        public ActionResult validate(UserRequest user) {
            return Ok();
        }
    }
}
