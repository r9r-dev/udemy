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

        public async Task<Like> GetLike(int userId, int recipientId)
        {
            return await _db.Likes.FirstOrDefaultAsync(u => u.LikerId == userId && u.LikeeId == recipientId);
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
            var users = _db.Users.Include(p => p.Photos)
                .OrderByDescending(u => u.LastActive).AsQueryable();
            users = users.Where(u => u.Id != userParams.UserId);
            users = users.Where(u => u.Gender == userParams.Gender);

            if (userParams.Likers)
            {
                var userLikers = await GetUserLikes(userParams.UserId, true);
                users = users.Where(u => userLikers.Contains(u.Id));
            }

            if (userParams.Likees)
            {
                var userLikees = await GetUserLikes(userParams.UserId, false);
                users = users.Where(u => userLikees.Contains(u.Id));
            }

            if (userParams.MinAge != 18 || userParams.MaxAge != 99)
            {
                var minDateOfBirth = DateTime.Today.AddYears(-userParams.MaxAge - 1);
                var maxDateOfBirth = DateTime.Today.AddYears(-userParams.MinAge);
                users = users.Where(u => u.DateOfBirth >= minDateOfBirth && u.DateOfBirth <= maxDateOfBirth);
            }

            if (!string.IsNullOrEmpty(userParams.OrderBy))
            {
                switch (userParams.OrderBy)
                {
                    case "created":
                        users = users.OrderByDescending(u => u.Created);
                        break;
                    default:
                        users.OrderByDescending(u => u.LastActive);
                        break;
                }
            }

            return await PagedList<User>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
        }

        private async Task<IEnumerable<int>> GetUserLikes(int id, bool likers)
        {
            var user = await _db.Users
                .Include(x => x.Likers)
                .Include(x => x.Likees)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (likers)
            {
                return user.Likers.Where(u => u.LikeeId == id).Select(u => u.LikerId);
            }
            else
            {
                return user.Likees.Where(u => u.LikerId == id).Select(u => u.LikeeId);
            }
        }

        public async Task<bool> SaveAll()
        {
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<Message> GetMessage(int id)
        {
            return await _db.Messages.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<PagedList<Message>> GetMessagesForUser(MessageParams msgParams)
        {
            var messages = _db.Messages
                                .Include(u => u.Sender).ThenInclude(p => p.Photos)
                                .Include(u => u.Recipient).ThenInclude(p => p.Photos)
                                .AsQueryable();
            
            switch (msgParams.MessageContainer)
            {
                case "Inbox":
                    messages = messages.Where(u => u.RecipientId == msgParams.UserId && u.RecipientDeleted == false);
                    break;
                case "Outbox":
                    messages = messages.Where(u => u.SenderId == msgParams.UserId && u.SenderDeleted == false);
                    break;
                default: // Unread
                    messages = messages.Where(u => u.RecipientId == msgParams.UserId && u.IsRead == false && u.RecipientDeleted == false);
                    break;
            }

            messages = messages.OrderByDescending(u => u.MessageSent);

            return await PagedList<Message>.CreateAsync(messages, msgParams.PageNumber, msgParams.PageSize);
        }

        public async Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId)
        {
            var messages = await _db.Messages
                                .Include(u => u.Sender).ThenInclude(p => p.Photos)
                                .Include(u => u.Recipient).ThenInclude(p => p.Photos)
                                .Where(m => m.RecipientId == userId && m.RecipientDeleted == false && m.SenderId == recipientId
                                    || m.RecipientId == recipientId && m.SenderDeleted == false && m.SenderId == userId)
                                .OrderByDescending(m => m.MessageSent)
                                .ToListAsync();

            return messages;
        }
    }
}