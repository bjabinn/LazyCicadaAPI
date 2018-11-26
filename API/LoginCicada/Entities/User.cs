using System;
namespace LoginCicada.Entities {
    public class User {
        public int Id {get; set;}
        public string Login {get; set;}
        public string Password {get; set;}
        public string Token {get; set;}

        public string FullName {get; set;}

        public string roles {get; set;}

        [Flags]
        public enum  Rol{ 
            None =0x0,
            Administrator= 0x01, 
            Supervisor= 0x02
        };
    }
}