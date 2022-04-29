
namespace Enigma.Domain.Entities
{
    public class User : BaseEntity
    {
        public User(string email, byte[] passwordHash, byte[] passwordSalt)
        {
            Email        = email;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

        public string Email { get; internal set; }
        public byte[] PasswordHash { get; internal set; }
        public byte[] PasswordSalt { get; internal set; }
    }
}
