using codi.Models;

namespace codi.Services
{
    public interface IAuthService
    {
        void Register(UserDto input);

        string Login(UserDto input);
    }
}