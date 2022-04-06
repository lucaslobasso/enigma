using Enigma.Domain.Interfaces;

namespace Enigma.Domain.Entities
{
    public abstract class BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; }
    }
}
