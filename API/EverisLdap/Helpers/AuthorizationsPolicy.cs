using System.Net;
using System.Text;
using System.IO;
using System;
using Microsoft.Extensions.Options;

using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Windows;
using System.Linq;
using System.Security.Permissions;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;


namespace EverisLdap.Helpers
{
    public class TokenRequirement : IAuthorizationRequirement{
    }
    public class TokenHandler : AuthorizationHandler<TokenRequirement>
   {
        private IHttpContextAccessor httpContextAccessor = null;
        private readonly AppSettings appSettings;
        
        public TokenHandler(IHttpContextAccessor httpContextAccessor, IOptions<AppSettings> appSettings) {
            this.httpContextAccessor=httpContextAccessor;
            this.appSettings=appSettings.Value;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TokenRequirement requirement)
        {
            HttpContext httpContext = httpContextAccessor.HttpContext;
            string token = Utils.getToken(httpContext.Request);
            AuthorizationFilterContext filterContext = context.Resource as AuthorizationFilterContext;
            string res= null;
            var Response =  filterContext.HttpContext.Response;
            var message=Encoding.UTF8.GetBytes("Token Validation failed");
            if(!String.IsNullOrEmpty(token)) {
                try {
                    res = callRefreshMethod(token);
                    if(String.IsNullOrEmpty(res)) {
                        context.Fail();
                        Response.OnStarting( async () => {
                            filterContext.HttpContext.Response.StatusCode = 401;
                            await Response.Body.WriteAsync(message, 0, message.Length);
                        });
                    } else {
                        context.Succeed(requirement);
                    }
                } catch (Exception) {
                    context.Fail();
                    Response.OnStarting( async () => {
                        filterContext.HttpContext.Response.StatusCode = 401;
                        await Response.Body.WriteAsync(message, 0, message.Length);
                    });
                }
            }
            
            return Task.CompletedTask;
        }
        private string callRefreshMethod(string securityToken) {
            ServicePointManager.ServerCertificateValidationCallback = delegate{return true; };
            var req= WebRequest.Create(appSettings.refreshMethod);
            req.Method="POST";
            req.ContentType="application/json";
            req.PreAuthenticate=true;
            req.Headers.Add("Authorization", "Bearer " + securityToken);
            var res1=new StreamReader(req.GetResponse().GetResponseStream()).ReadToEnd().Trim('"');
            return res1;
        }
   }
   
   public class RolRequirement : IAuthorizationRequirement {
       public string requiredRoles {get;}

       public RolRequirement(string requiredRoles) {
           this.requiredRoles=requiredRoles;
       }
   }
   public class RolHandler : AuthorizationHandler<RolRequirement> {
       private IHttpContextAccessor httpContextAccessor = null;
       private JwtSecurityTokenHandler tokenHandler;

        public RolHandler(IHttpContextAccessor httpContextAccessor) {
            this.httpContextAccessor=httpContextAccessor;
            this.tokenHandler = new JwtSecurityTokenHandler();
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RolRequirement requirement){
            // try get roles in token.
            HttpContext httpContext = httpContextAccessor.HttpContext;
            string tokenString=Utils.getToken(httpContext.Request);
            JwtSecurityToken token=tokenHandler.ReadJwtToken(tokenString);
            String rolesTokenString=Utils.getRoles(token.Claims);
            Utils.Rol rolesPolicy = (Utils.Rol) Enum.Parse(typeof (Utils.Rol),requirement.requiredRoles);
            Utils.Rol rolesToken = (Utils.Rol) Enum.Parse(typeof (Utils.Rol),rolesTokenString);
            var res=rolesPolicy & rolesToken;
            if(res!=Utils.Rol.None) {
                context.Succeed(requirement);
            } else {
                context.Fail();
                AuthorizationFilterContext filterContext = context.Resource as AuthorizationFilterContext;
                var Response =  filterContext.HttpContext.Response;
                Response.OnStarting( async () => {
                    var message=Encoding.UTF8.GetBytes("Rol Validation failed");
                    filterContext.HttpContext.Response.StatusCode = 401;
                    await Response.Body.WriteAsync(message, 0, message.Length);
                });
            }
            return Task.CompletedTask;
        }
    }
}