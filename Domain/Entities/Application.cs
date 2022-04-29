
using Enigma.Domain.Exceptions;

namespace Enigma.Domain.Entities
{
    public class Application : BaseEntity
    {
        public Application(Guid userId, string denomination)
        {
            UserId       = userId;
            Denomination = denomination;
        }

        public Guid UserId { get; internal set; }
        public string Denomination { get; internal set; }

        public void UpdateDenomination(string denomination)
        {
            if (string.IsNullOrEmpty(denomination))
            {
                throw new ValidationException("Denomination is empty");
            }

            Denomination = denomination;
        }
    }
}
