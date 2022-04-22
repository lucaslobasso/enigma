
using Enigma.Domain.Exceptions;

namespace Enigma.Domain.Entities
{
    public class Application : BaseEntity
    {
        public Application(string denomination)
        {
            Denomination = denomination;
        }

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
