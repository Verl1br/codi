using codi.DataBase;
using codi.Models;

namespace codi.Repository
{
    public class AuthRepository : IAuthRepository
    {
        ApplicationContext db;
        public AuthRepository(ApplicationContext context)
        {
            db = context;
        }
        public void Insert(User input)
        {
            db.Users.Add(input);
            db.SaveChanges();
        }

        public User GetUser(UserDto input)
        {
            User? user = db.Users.FirstOrDefault(x => x.Email == input.Email);

            if (user == null) throw new InvalidOperationException("User not found");

            return user;
        }
    }
}