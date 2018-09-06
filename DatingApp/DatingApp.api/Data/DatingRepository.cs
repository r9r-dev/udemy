using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.api.Helpers;
using DatingApp.api.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.api.Data
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext _db;

        public DatingRepository(DataContext db)
        {
            _db = db;
        }

        public void Add<T>(T entity) where T : class
        {
            _db.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _db.Remove(entity);
        }

        public async Task<Photo> GetMainPhotoForUser(int userId)
        {
            return await _db.Photos.Where(u => u.OwnerId == userId).FirstOrDefaultAsync(x => x.IsMain);
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _db.Photos.FirstOrDefaultAsync(p => p.Id == id);
            return photo;
        }

        public async Task<User> GetUser(int id)
        {
            return await _db.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<PagedList<User>> GetUsers(UserParams userParams)
        {
            var users = _db.Users.Include(p => p.Photos).AsQueryable();
            users = users.Where(u => u.Id != userParams.UserId);
            users = users.Where(u => u.Gender == userParams.Gender);
            if (userParams.MinAge != 18 || userParams.MaxAge != 99)
            {
                var minDateOfBirth = DateTime.Today.AddYears(-userParams.MaxAge - 1);
                var maxDateOfBirth = DateTime.Today.AddYears(-userParams.MinAge);
                users = users.Where(u => u.DateOfBirth >= minDateOfBirth && u.DateOfBirth <= maxDateOfBirth);
            }
            return await PagedList<User>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<bool> SaveAll()
        {
            return await _db.SaveChangesAsync() > 0;
        }
    }
}