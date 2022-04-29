using Enigma.API.DTOs;
using Enigma.API.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Enigma.API.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _service;

        public AuthController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<string>> Register(UserDTO user, CancellationToken cancellationToken = default)
        {
            var accessToken = await _service.RegisterAsync(user, cancellationToken);
            return Ok(accessToken);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(UserDTO user, CancellationToken cancellationToken = default)
        {
            var accessToken = await _service.LoginAsync(user, cancellationToken);
            return Ok(accessToken);
        }
    }
}
