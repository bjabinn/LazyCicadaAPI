using System;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;

namespace EverisLdap.Helpers
{
    public class Utils {

          [Flags]
        public enum  Rol{ 
            None =0x0,
            Administrator= 0x01, 
            Supervisor= 0x02
        };

        public static string getToken(HttpRequest request) {
            string token = request.Headers["Authorization"];
            if(!String.IsNullOrEmpty(token) && token.Length > 7) {
                token = token.Substring(7);
            }
            return token;
        }

        public static string getRoles(IEnumerable<Claim>clains) {
            Claim res=clains.First(x => x.Type == "role");
            return res.Value;
        }

        public static string getUserName(IEnumerable<Claim>clains) {
            Claim c=clains.First(x => x.Type == "unique_name");
            return c.Value;
        }
    }
}