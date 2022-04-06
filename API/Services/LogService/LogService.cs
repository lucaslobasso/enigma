namespace Enigma.API.Services.LogService
{
    public class LogService
    {
        private readonly ILogService _service;

        public LogService(ILogService service)
        {
            _service = service;
        }
    }
}
