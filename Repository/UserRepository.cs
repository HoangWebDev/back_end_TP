using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechPhone.Contracts;
using TechPhone.Models;
using TechPhone.Request;

namespace TechPhone.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _config;
        public UserRepository(IConfiguration config)
        {
            _config = config;
        }

        private List<UserModel> _users = new List<UserModel>()
        {
            new UserModel
            {
                Id = 1, Username = "hoang", Password = "hoang123", Role = "Admin"
            },
            new UserModel
            {
                Id = 2, Username = "joydip", Password = "joydip123"
            },            
        };

        public async Task<UserModel> Authenticate(UserLogin userLogin)
        {
            var currentUser = _users.FirstOrDefault(x => x.Username == userLogin.Username && x.Password == userLogin.Password);
            return currentUser;
        }

        public async Task<string> Login(UserLogin userLogin)
        {
            var user = await Authenticate(userLogin);
            if (user != null)
            {
                return GenerateToken(user); // Return token as string
            }
            return null; // Return null if user not found
        }

        // To generate token
        private string GenerateToken(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Username),
                new Claim(ClaimTypes.Role,user.Role)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
