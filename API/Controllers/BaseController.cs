using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Enigma.API.Controllers
{
    [ApiController]
    [Authorize]
    public class BaseController : ControllerBase
    {

    }
}
