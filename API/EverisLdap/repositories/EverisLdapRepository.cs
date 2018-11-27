using System.DirectoryServices;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System;
using System.Linq;
using EverisLdap.Models;

using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace EverisLdap.Repositories
{
    class EverisLdapRepository : IEverisLdapRepository
    {
        // Este método sirve para hacer login contra el AD de Everis.
        // Es importante tener en cuenta que el párametro loginDn
        // Debe tener el siguiente formato:
        // loginDn="Usersad\\jmunozga"

        // Devuelve un booleano con el resultado del éxito de la operación
        public bool checkUser(UserRequest user){
            bool res=false;
            DirectoryEntry entry = new DirectoryEntry();
            entry.Username=user.loginDn;
            entry.Password=user.password;
            DirectorySearcher searcher = new DirectorySearcher(entry);
            // El campo userAccountControl guarda una serie de bits de control:
             // Script = 1,                                     // 0x1
            // AccountDisabled = 2,                            // 0x2
            // HomeDirectoryRequired = 8,                      // 0x8
            // AccountLockedOut = 16,                          // 0x10
            // PasswordNotRequired = 32,                       // 0x20
            // PasswordCannotChange = 64,                      // 0x40
            // EncryptedTextPasswordAllowed = 128,             // 0x80
            // TempDuplicateAccount = 256,                     // 0x100
            // NormalAccount = 512,                            // 0x200
            // InterDomainTrustAccount = 2048,                 // 0x800
            // WorkstationTrustAccount = 4096,                 // 0x1000
            // ServerTrustAccount = 8192,                      // 0x2000
            // PasswordDoesNotExpire = 65536,                  // 0x10000 (Also 66048 )
            // MnsLogonAccount = 131072,                       // 0x20000
            // SmartCardRequired = 262144,                     // 0x40000
            // TrustedForDelegation = 524288,                  // 0x80000
            // AccountNotDelegated = 1048576,                  // 0x100000
            // UseDesKeyOnly = 2097152,                        // 0x200000
            // DontRequirePreauth = 4194304,                   // 0x400000
            // PasswordExpired = 8388608,                      // 0x800000 (Applicable only in Window 2000 and Window Server 2003)
            // TrustedToAuthenticateForDelegation = 16777216,  // 0x1000000
            // NoAuthDataRequired = 33554432                   // 0x2000000

            // El 2 nos indica si el usuario esta de baja y por tanto no 
            // está en la empresa.
            searcher.Filter = "(&(objectClass=user)(!(userAccountControl:1.2.840.113556.1.4.803:=2)))";
            try{
                searcher.FindOne();
                res=true;
            }catch (COMException) {
                /* Podemos saber si hay error de password de la siguiente manera.

                // Autentication Error.
                if (ex.ErrorCode == -2147023570) {
                    res=false;
                } */
            }
            return res;
        }
        // Esta es la versión más eficiente para buscar empleados de Everis por 
        // nombre corto (= SamAccountName en AD).
        // ó nombre completo (= DisplayName en AD).

        // Por eficiencia se propone mutilar a n resultados. 
        // Es el parametro pageSize por defecto a 5.

        // Devuelve en un directorio la fotos que cumplen el patrón 
        // con el nombre corto.jpg
        public List<SearchInfoResponse> getDirectorySearcher(SearchInfoRequest se){
            List<SearchInfoResponse> result = new List<SearchInfoResponse>();
            DirectorySearcher searcher = new DirectorySearcher();
            // Esta es la manera de mutilar el número de resultados.
            searcher.SearchRoot= new DirectoryEntry("LDAP://OU=Spain,OU=Europe,OU=Everis,DC=usersad,DC=everis,DC=int");
            searcher.SizeLimit=se.pagesize;
            // Desactivamos el timeout. En principio no sería necesario.
            searcher.ClientTimeout=TimeSpan.FromSeconds(-1);
            // A continuación el patrón.
            // Con userAccountControl controlamos que el usuario esté enable (ver método anterior)
            // Se permite * para subcadenas.
            //searcher.Filter = string.Format("(&(objectCategory=person)(objectClass=user)(!(userAccountControl:1.2.840.113556.1.4.803:=2))(|(DisplayName=*{0}*)(SamAccountName=*{0}*)))",se.stringSearch);
            searcher.Filter = string.Format("(|(&(objectCategory=person)(objectClass=user)(!(userAccountControl:1.2.840.113556.1.4.803:=2))(DisplayName=*{0}*))(&(objectCategory=person)(objectClass=user)(!(userAccountControl:1.2.840.113556.1.4.803:=2))(SamAccountName=*{0}*)))",se.stringSearch);
            
            // Por eficiencia indicamos los parámetros a recuperar. 
            // Esto mejora el tiempo
            searcher.PropertiesToLoad.Add("SamAccountName");
            searcher.PropertiesToLoad.Add("thumbnailPhoto");
            searcher.PropertiesToLoad.Add("displayName");
            searcher.PropertiesToLoad.Add("employeeNumber");
            searcher.PropertiesToLoad.Add("title");
            using (SearchResultCollection results = searcher.FindAll()) {                
                foreach (SearchResult domainUsers in results) {
                    DirectoryEntry user = domainUsers.GetDirectoryEntry();
                    if(user!=null){
                        // Cacheamos por el nombre corto que puede constituir 
                        // una clave primeria. Con esto conseguimos más eficiencia.
                        user.RefreshCache(new string[]{"SamAccountName"});
                        SearchInfoResponse elem = new SearchInfoResponse();
                        
                        // Extraemos el nombre corto y la foto.
                        elem.shortName=user.Properties["SamAccountName"][0].ToString();
                        elem.longName=user.Properties["displayName"][0].ToString();
                        elem.numEmployee=(long) Convert.ToInt64(user.Properties["employeeNumber"][0].ToString());
                        elem.title=user.Properties["title"][0].ToString();
                        var photoP=user.Properties["thumbnailPhoto"].Value;
                        // Como ejemplo del método las salvamos en un directorio.
                        // quizás se deberían devolver de forma rest.
                        
                        if(photoP!=null){
                            elem.photo=(byte[])photoP;
                            /*
                            Bitmap photo = new Bitmap(new MemoryStream(photoInBytes)); 
                            photo.Save(String.Format("./fotos1/{0}.jpg",samAccountName),ImageFormat.Jpeg);
                            */
                        }
                        result.Add(elem);
                    }
                }
            }
            return result;
        }
        // Implementación alternativa a la busqueda de empleados de Everis.
        // Su implementación es más simple puesto que utiliza un objeto de 
        // más alto nivel pero es menos eficiente. 
        // alrededor de 10 veces en media.
        public void getUsersPrincipal(string stringSearch, int pageSize=5)
        {
            using (var ctx= new PrincipalContext(ContextType.Domain)){
                var myDomainUsers = new List<string>();
                List <UserPrincipal> searchPrinciples = new List<UserPrincipal>();
                searchPrinciples.Add(new UserPrincipal(ctx){DisplayName= String.Format("*{0}*",stringSearch), Enabled=true});
                searchPrinciples.Add(new UserPrincipal(ctx){SamAccountName=String.Format("*{0}*",stringSearch), Enabled=true});
                foreach (UserPrincipal item in searchPrinciples) {
                    PrincipalSearcher search = new PrincipalSearcher(item);
                    foreach(var domainUsers in search.FindAll()){ 
                        pageSize--;                     
                        string samAccountName= domainUsers.SamAccountName;
                        DirectoryEntry directoryEntry = (DirectoryEntry)domainUsers.GetUnderlyingObject();
                        PropertyValueCollection photoProperty=directoryEntry.Properties["thumbnailPhoto"];
                        if(photoProperty.Value!=null && photoProperty.Value is byte[]) {
                            byte[] photoInBytes = (byte [])photoProperty.Value;
                            Bitmap photo = new Bitmap(new MemoryStream(photoInBytes)); 
                            photo.Save(String.Format("./fotos/{0}.jpg",samAccountName),ImageFormat.Jpeg);
                        }
                        if(pageSize==0)
                            return;  
                    }
                }   
            }
        }

        // Un método para devolver toda la información de un usuario concreto.
        public PropertyCollection getInfoUser(string nombreCorto) {
            DirectorySearcher searcher = new DirectorySearcher();
            searcher.Filter=string.Format("(&(objectCategory=person)(objectClass=user)(SamAccountName={0}))",nombreCorto);
            SearchResult domainUser = searcher.FindOne();
            DirectoryEntry user = domainUser.GetDirectoryEntry();
            PropertyCollection props = user.Properties;
            return props;
        }
    }
}
