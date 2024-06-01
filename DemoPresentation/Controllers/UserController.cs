using DemoPresentation.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoPresentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserInfo user,
    [FromServices] IUserService userService, [FromServices] IJwtService jwtService)
        {
            var storedUser = userService.GetUser(user?.Username);
            if (!userService.IsAuthenticated(user?.Password, storedUser?.PasswordHash))
            {
                return Unauthorized();
            }

            var tokenString = jwtService.GenerateToken(storedUser);
            return Ok(new { token = tokenString });

        }

        [HttpGet("dummy")]
        [Authorize(Roles = "Admin")]
        public IActionResult Dummy()
        {
            return Ok("lmao ayy");
        }
    }
}
