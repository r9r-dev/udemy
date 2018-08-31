using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using DatingApp.api.Models;
using Newtonsoft.Json;

namespace DatingApp.api.Data
{
    public class Seed
    {
        private readonly DataContext _ctx;

        public Seed(DataContext ctx)
        {
            _ctx = ctx;
        }

        public void SeedUsers()
        {
            var userData = File.ReadAllText("Data/UserSeedData.json");
            var users = JsonConvert.DeserializeObject<List<User>>(userData);
            foreach (var user in users)
            {
                var keys = CreatePasswordHash("password");

                user.PasswordHash = keys.PasswordHash;
                user.PasswordSalt = keys.PasswordSalt;
                user.Username = user.Username.ToLower();

                _ctx.Users.Add(user);
            }

            _ctx.SaveChanges();
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
    }
}