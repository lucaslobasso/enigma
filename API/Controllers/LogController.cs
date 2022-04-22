using Enigma.API.DTOs.LogDTO;
using Enigma.API.Services.LogService;
using Microsoft.AspNetCore.Mvc;

namespace Enigma.API.Controllers
{
    public class LogController : BaseController
    {
        private readonly ILogService _service;

        public LogController(ILogService service)
        {
            _service = service;
        }

        [HttpGet("Get")]
        public async Task<ActionResult<LogDTO>> Get(Guid id, CancellationToken cancellationToken = default)
        {
            var log = await _service.GetAsync(id, cancellationToken);
            return Ok(log);
        }

        [HttpGet("GetBy")]
        public async Task<ActionResult<List<LogDTO>>> GetBy(GetByLogDTO getBy, CancellationToken cancellationToken = default)
        {
            var logs = await _service.GetByAsync(getBy, cancellationToken);
            return Ok(logs);
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(CreateLogDTO log, CancellationToken cancellationToken = default)
        {
            await _service.Create(log, cancellationToken);
            return Ok();
        }
    }
}
