
namespace Enigma.Domain.Entities
{
    public class User : BaseEntity
    {
        public User(string username, byte[] passwordHash, byte[] passwordSalt)
        {
            Username     = username;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

        public string Username { get; internal set; }
        public byte[] PasswordHash { get; internal set; }
        public byte[] PasswordSalt { get; internal set; }
    }
}
