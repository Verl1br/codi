using System.Security.Cryptography;
using codi.Repository;
using codi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;


namespace codi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository repo;
        private readonly IConfiguration configuration;

        public AuthService(IAuthRepository repo, IConfiguration configuration)
        {
            this.repo = repo;
            this.configuration = configuration;
        }

        public void Register(UserDto input)
        {
            CreatePasswordHash(input.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User? user = new User();

            user.Email = input.Email;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            repo.Insert(user);
        }

        public string Login(UserDto input)
        {
            User? user = repo.GetUser(input);

            if (!VerifyPasswordHash(input.Password, user.PasswordHash, user.PasswordSalt))
            {
                return "Wrong password.";
            }

            string token = CreateToken(user);
            return token;
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, "User")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}