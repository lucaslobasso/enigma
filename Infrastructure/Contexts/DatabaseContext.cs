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
        public DbSet<Application> Applications => base.Set<Application>();
        public DbSet<Log> Logs => base.Set<Log>();
    }
}
