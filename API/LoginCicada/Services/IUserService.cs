using LoginCicada.Entities;
namespace LoginCicada.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        string resfreshToken(string token);
    }
}