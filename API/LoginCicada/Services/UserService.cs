using LoginCicada.Entities;
using LoginCicada.Helpers;
using System.Collections.Generic;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using ServiceStack.Redis;
using System;
using System.Linq;

namespace LoginCicada.Services {
    public class UserService : IUserService {
       private List<User> _users= new List<User>{
           new User{Id=1, Login="jmunozga",Password="clave",FullName="Jesús Muñoz Galiano", roles=(User.Rol.Administrator | User.Rol.Supervisor).ToString()}
       };
       private readonly AppSettings _appSettings;

       private JwtSecurityTokenHandler _tokenHandler;

       private RedisClient _clientRedis;

       public UserService(IOptions<AppSettings> appSettings){
           _appSettings = appSettings.Value;
           _tokenHandler = new JwtSecurityTokenHandler();
           _clientRedis = new RedisClient(_appSettings.redisServer,_appSettings.redisPort,_appSettings.redisPassword);
       }
       public User Authenticate(string username, string password) {
            if (!username.Equals("jmunozga")) {
                return null;
            } else {
                // generate a token 
                var user = _users[0];
                user.Token=generateToken(username,user.roles);
                // Delete password before returning 
                user.Password=null;
                // Register token in redis.
                _clientRedis.SetEntryInHash(user.Login,"name",user.Login);
                _clientRedis.SetEntryInHash(user.Login,"token",user.Token);
                _clientRedis.Expire(user.Login,_appSettings.ttlRedis);
                return user;
            }
            
        }
        public string resfreshToken(string token){
            // First read token from Reddis to check is real.
            var securityToken=_tokenHandler.ReadJwtToken(token);
            string userName=Utils.getUserName(securityToken.Claims);
            string roles = Utils.getRoles(securityToken.Claims);
            if(!String.IsNullOrEmpty(userName)) {
                if(_clientRedis.ContainsKey(userName)) {
                    List<string>tokensRedis=_clientRedis.GetValuesFromHash(userName,new string[]{"token"});
                    string tokenRedis= tokensRedis[0];
                    if(tokenRedis!=token){
                        // Different tokens => false token!!!! 
                        throw new SecurityTokenValidationException("No real token");
                    } else {
                        // Check validTo
                        if(securityToken.ValidTo < DateTime.UtcNow) {
                            // No valid => generateToken
                            return generateToken(userName,roles);
                        } else {
                            // Else nothing to do.
                            return token;
                        }
                    }
                } else {
                    // No entry => no login 
                    throw new SecurityTokenNotYetValidException("User not logged"); 
                }
            } else  {
                // No userName => false token!!!!
                throw new SecurityTokenInvalidSignatureException("No Unique_name");
            }
        }
        
        private string generateToken(string userName, string rol) {
            var user = _users[0];
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);   
            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Role, rol)
                }),
                Expires = DateTime.UtcNow.AddSeconds(_appSettings.ttlToken),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = _tokenHandler.CreateToken(tokenDescriptor);
            var tokenString=_tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}