global using Enigma.Domain.Entities;
global using Microsoft.EntityFrameworkCore;

namespace Enigma.Infrastructure.Contexts
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<User> Users => base.Set<User>();
    }
}
