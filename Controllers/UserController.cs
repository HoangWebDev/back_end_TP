using Microsoft.AspNetCore.Mvc;
using TechPhone.Contracts;
using TechPhone.Request;


namespace TechPhone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            var token = await _userRepository.Login(userLogin);
            if (token != null)
            {
                return Ok(token); // Return the token if authentication is successful
            }

            return NotFound("User not found"); // Return NotFound if authentication fails
        }
    }
}
