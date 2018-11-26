using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LoginCicada.Entities;
using LoginCicada.Services;
using LoginCicada.Helpers;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace LoginCicada.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService){
            _userService=userService;
        }
    
        [AllowAnonymous]
        [Route("/api/loginService/authenticate")]
        [HttpPost]
        public IActionResult Authenticate([FromBody]User userParam){
            var user=_userService.Authenticate(userParam.Login, userParam.Password);
            if(user==null) {
                return BadRequest(new {message = "Username or password incorrect"});
            }
            return Ok(user);
        }

        [Authorize]
        [Route("/api/loginService/refreshToken")]
        [HttpPost]
        public string refreshToken(){
            string tokenOld=Utils.getToken(Request);
            string tokenNew=_userService.resfreshToken(tokenOld);
            return tokenNew;
        }
    }      
}
