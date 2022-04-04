using Enigma.Domain.Interfaces;

namespace Enigma.Domain.Entities
{
    public class BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; }
    }
}
