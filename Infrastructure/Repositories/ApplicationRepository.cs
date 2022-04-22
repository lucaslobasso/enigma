using Enigma.Domain.Repositories;
using Enigma.Infrastructure.Contexts;

namespace Enigma.Infrastructure.Repositories
{
    public class ApplicationRepository : BaseRepository<Application>, IApplicationRepository
    {
        public ApplicationRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
