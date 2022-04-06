using Enigma.API.Services.LogService;

namespace Enigma.API.Controllers
{
    public class LogController : BaseController
    {
        private readonly ILogService _service;

        public LogController(ILogService service)
        {
            _service = service;
        }
    }
}
