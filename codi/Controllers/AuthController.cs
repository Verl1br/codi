using Microsoft.AspNetCore.Mvc;
using codi.Services;
using codi.Models;

namespace codi.Controllers
{
    [ApiController]
    [Route("api")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("registration")]
        public IActionResult Register(UserDto request)
        {
            authService.Register(request);
            return Ok("User was created");
        }

        [HttpPost("login")]
        public IActionResult Login(UserDto request)
        {
            string token = authService.Login(request);
            return Ok(token);
        }
    }
}