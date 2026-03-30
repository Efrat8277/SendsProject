using Microsoft.EntityFrameworkCore;
using SendsProject.Core.Models.Classes;
using SendsProject.Core.Repositories;

namespace SendsProject.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;

        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<User> GetByUserNameAsync(string userName, string Password)
        {
            // חיפוש המשתמש בבסיס הנתונים
            return await _dataContext.User.FirstOrDefaultAsync(u => u.UserName == userName && u.Password == Password);
        }

        public async Task<User> AddUserAsync(User user)
        {
            // 1. מוסיף את האובייקט ל-Context (בזיכרון)
            await _dataContext.User.AddAsync(user);

            // 2. השורה הקריטית שחובה להוסיף!!! 
            // בלי זה הנתונים פשוט נעלמים בסוף הבקשה
            await _dataContext.SaveChangesAsync();

            return user;
        }
    }
}