using Microsoft.EntityFrameworkCore;
using SendsProject.Core.Models.Classes;
using SendsProject.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendsProject.Data.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly DataContext _dataContext;
        

        public UserRepository(DataContext dataContext)
        {
                _dataContext = dataContext;
        }

        public async Task<User> GetByUserNameAsync(string userName, string Password)
        {
             return await _dataContext.User.FirstOrDefaultAsync(u => u.UserName == userName && u.Password == Password);
        }


        public async Task<User> AddUserAsync(User user)
        {
             await _dataContext.User.AddAsync(user);
             return user;

        }

    }
}
