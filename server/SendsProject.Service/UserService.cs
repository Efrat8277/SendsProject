using SendsProject.Core.Models.Classes;
using SendsProject.Core.Repositories;
using SendsProject.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendsProject.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetByUserNameAsync(string UserName, string Password)
        {
            return await _userRepository.GetByUserNameAsync(UserName, Password);
        }

        public async Task<User> AddUserAsync(User user)
        {
            return await _userRepository.AddUserAsync(user);
        }
    }
}
