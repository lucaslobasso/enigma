using Enigma.Domain.Repositories;
using Enigma.Infrastructure.Contexts;

namespace Enigma.Infrastructure.Repositories
{
    public class LogRepository : BaseRepository<Log>, ILogRepository
    {
        public LogRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
