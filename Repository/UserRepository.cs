using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WritersClub.Data;
using WritersClub.Models;

namespace WritersClub.Repository
{
    public class UserRepository:IUser
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<User> AddUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }

            bool isLoginAvailable = await IsUserNameAvailable(user.Login);
            if (!isLoginAvailable)
            {
                throw new ArgumentException("Логин уже используется", nameof(user.Login));
            }

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<bool> IsUserNameAvailable(string login)
        {
            return !await _context.Users.AnyAsync(e => e.Login == login);
        }


        public async Task<User> GetUserByLogin(string login)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
        }
        public async Task<User> UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<User> DeleteUser(int id)
        {
            var user = await GetUserById(id);
            if (user != null)
            {
                user.State = UserState.Deleted;
                await _context.SaveChangesAsync();
            }
            return user;
        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
