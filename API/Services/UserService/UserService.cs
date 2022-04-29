using Enigma.API.DTOs;
using Enigma.Domain.Exceptions;
using Enigma.Domain.Repositories;
using Enigma.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace Enigma.API.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly AuthenticationOptions _options;

        public UserService(IUserRepository repository, IOptions<AuthenticationOptions> options)
        {
            _repository = repository;
            _options    = options.Value;
        }

        public async Task<string> RegisterAsync(UserDTO user, CancellationToken cancellationToken = default)
        {
            ValidateNewUser(user.Email, cancellationToken);
            var entity = await AddUserAsync(user, cancellationToken);
            return CreateAccessToken(entity);
        }

        public async Task<string> LoginAsync(UserDTO user, CancellationToken cancellationToken = default)
        {
            var entity = await GetUserAsync(user.Email, cancellationToken);
            ValidatePasswordHash(user.Password, entity.PasswordHash, entity.PasswordSalt);
            return CreateAccessToken(entity);
        }

        private async Task<User> AddUserAsync(UserDTO user, CancellationToken cancellationToken = default)
        {
            CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var entity = new User(user.Email, passwordHash, passwordSalt);

            _repository.Add(entity);
            await _repository.SaveChangesAsync(cancellationToken);
            return entity;
        }

        private async Task<User> GetUserAsync(string email, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.TryGetAsync(email, cancellationToken);

            if (entity is null)
            {
                throw new ValidationException("User not exists");
            }

            return entity;
        }

        private async void ValidateNewUser(string email, CancellationToken cancellationToken = default)
        {
            if (await _repository.ExistsAsync(email, cancellationToken))
            {
                throw new ValidationException("User already exists");
            }
        }

        private static void ValidatePasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac   = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            if (!computedHash.SequenceEqual(passwordHash))
            {
                throw new ValidationException("Wrong password");
            }
        }

        private string CreateAccessToken(User user)
        {
            var key    = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Token));
            var creds  = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var token  = new JwtSecurityToken(
                claims             : claims,
                signingCredentials : creds,
                expires            : DateTime.Now.AddDays(1)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt   = hmac.Key;
            passwordHash   = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}
