using MiniStore.Models;
using MiniStore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniStore.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> Login(string UserName, string Password)
        {
            return await _userRepository.Login(UserName,Password);
        }

        public async Task<User> Register(User user, string password)
        {
            return await _userRepository.Register(user, password);
        }

        public async Task<bool> UserExist(string UserName)
        {
            return await _userRepository.UserExist(UserName);
        }
    }
}
