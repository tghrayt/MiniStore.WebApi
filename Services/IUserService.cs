using MiniStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniStore.Services
{
   public interface IUserService
    {
        Task<User> Register(User user, string password);

        Task<User> Login(string UserName, string Password);

        Task<bool> UserExist(string UserName);
    }
}
