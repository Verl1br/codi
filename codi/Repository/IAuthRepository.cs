using codi.Models;

namespace codi.Repository
{
    public interface IAuthRepository
    {
        void Insert(User input);

        User GetUser(UserDto input);
    }
}