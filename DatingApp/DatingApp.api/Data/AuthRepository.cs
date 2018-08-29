using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DatingApp.api.Models;

namespace DatingApp.api.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _ctx;

        public AuthRepository(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<User> Login(string username, string password)
        {
            
        }

        public async Task<User> Register(User user, string password)
        {
            var keys = CreatePasswordHash(password);
            user.PasswordHash = keys.PasswordHash;
            user.PasswordSalt = keys.PasswordSalt;

            await _ctx.Users.AddAsync(user);
            await _ctx.SaveChangesAsync();

            return user;
        }

        private (byte[] PasswordHash, byte[] PasswordSalt) CreatePasswordHash(string password)
        {
            using(var hmac = new HMACSHA512())
            {
                var salt = hmac.Key;
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return (hash, salt);
            }
        }

        public async Task<bool> UserExists(string username)
        {
            
        }
    }
}