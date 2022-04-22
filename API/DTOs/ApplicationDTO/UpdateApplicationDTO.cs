
namespace Enigma.API.DTOs.ApplicationDTO
{
    public class UpdateApplicationDTO
    {
        public Guid Id { get; set; }
        public string Denomination { get; set; } = string.Empty;
    }
}
