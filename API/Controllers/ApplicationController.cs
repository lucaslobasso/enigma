using Enigma.API.Controllers;
using Enigma.API.DTOs.ApplicationDTO;
using Enigma.API.Services.ApplicationService;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ApplicationController : BaseController
    {
        private readonly IApplicationService _service;

        public ApplicationController(IApplicationService service)
        {
            _service = service;
        }

        [HttpGet("Get")]
        public async Task<ActionResult<ApplicationDTO>> Get(Guid id, CancellationToken cancellationToken = default)
        {
            var application = await _service.GetAsync(id, cancellationToken);
            return Ok(application);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<ApplicationDTO>>> GetAll(CancellationToken cancellationToken = default)
        {
            var applications = await _service.GetAllAsync(cancellationToken);
            return Ok(applications);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Guid>> Create(CreateApplicationDTO application, CancellationToken cancellationToken = default)
        {
            var id = await _service.CreateAsync(application, cancellationToken);
            return Ok(id);
        }

        [HttpPost("Update")]
        public async Task<ActionResult> Update(UpdateApplicationDTO application, CancellationToken cancellationToken = default)
        {
            await _service.UpdateAync(application, cancellationToken);
            return Ok();
        }

        [HttpPost("Delete")]
        public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            await _service.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
