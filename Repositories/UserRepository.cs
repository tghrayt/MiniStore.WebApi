using MiniStore.Context;
using MiniStore.Helper;
using MiniStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniStore.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly StoreContext _storeContext;

        public UserRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        public async Task<User> Login(string UserName, string Password)
        {
            var user =  _storeContext.users.FirstOrDefault(u=>u.UserName==UserName);
            if(user == null)  return null;
            if (! await UserHelper.VerfyPasswordHash(Password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }
            return user;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] PasswordHash, PasswordSalt;
            UserHelper.CreatePasswordHash(password, out PasswordHash, out PasswordSalt);
            user.PasswordHash = PasswordHash;
            user.PasswordSalt = PasswordSalt;
            await _storeContext.users.AddAsync(user);
            await _storeContext.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UserExist(string UserName)
        {
            if( _storeContext.users.Any(u => u.UserName == UserName))
            {
                return true;
            }
            return false;
        }
    }
}
