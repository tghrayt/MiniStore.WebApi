using MiniStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniStore.Repositories
{
    public interface IUserRepository
    {

        Task<User> Register(User user, string password);

        Task<User> Login(string UserName, string Password);

        Task<bool> UserExist(string UserName);


    }
}
