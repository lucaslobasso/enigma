﻿using Enigma.API.DTOs;
using Enigma.API.Exceptions;
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
            if (await _repository.ExistsAsync(user.Username, cancellationToken))
            {
                throw new ValidationException("User already exists");
            }

            CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var entity = new User(user.Username, passwordHash, passwordSalt);
            
            _repository.Add(entity);
            _repository.SaveChangesAsync(cancellationToken);

            return CreateToken(entity.Username);
        }

        public async Task<string> LoginAsync(UserDTO user, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.TryGetAsync(user.Username, cancellationToken);

            if (entity is null)
            {
                throw new ValidationException("User not exists");
            }
            else if (!VerifyPasswordHash(user.Password, entity.PasswordHash, entity.PasswordSalt))
            {
                throw new ValidationException("Wrong password");
            }

            return CreateToken(entity.Username);
        }

        private string CreateToken(string username)
        {
            var key    = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Token));
            var creds  = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var claims = new List<Claim>{ new Claim(ClaimTypes.Name, username) };
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

        private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac   = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return computedHash.SequenceEqual(passwordHash);
        }
    }
}
