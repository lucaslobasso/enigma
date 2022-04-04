using Enigma.API.DTOs;

namespace Enigma.API.Services.UserService
{
    public interface IUserService
    {
        Task<string> RegisterAsync(UserDTO user);
        Task<string> LoginAsync(UserDTO user);
    }
}
