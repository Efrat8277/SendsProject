using SendsProject.Core.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendsProject.Core.Repositories
{
    public interface IUserRepository
    {
        public Task<User> GetByUserNameAsync(string UserName, string Password);
        public Task<User> AddUserAsync(User user);
            
    }
}
