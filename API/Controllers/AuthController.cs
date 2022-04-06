using Enigma.API.DTOs;
using Enigma.API.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Enigma.API.Controllers
{
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _service;

        public AuthController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<string>> Register(UserDTO request, CancellationToken cancellationToken = default)
        {
            var accessToken = await _service.RegisterAsync(request, cancellationToken);
            return Ok(accessToken);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(UserDTO request, CancellationToken cancellationToken = default)
        {
            var accessToken = await _service.LoginAsync(request, cancellationToken);
            return Ok(accessToken);
        }
    }
}
