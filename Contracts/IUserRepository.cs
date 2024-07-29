using Microsoft.AspNetCore.Mvc;
using TechPhone.Models;
using TechPhone.Request;

namespace TechPhone.Contracts
{
    public interface IUserRepository
    {
        Task<string> Login(UserLogin userLogin);        
        Task<UserModel >Authenticate(UserLogin userLogin);
    }
}
