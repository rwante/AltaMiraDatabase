using AltaMiraDatabase.Business.Abstract;
using AltaMiraDatabase.DataAccess.Abstract;
using AltaMiraDatabase.DataAccess.Concreate;
using AltaMiraDatabase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaMiraDatabase.Business.Concreate
{
    public class UserManager : IUserService
    {
        private IUserRepository userRepository;
        public UserManager()
        {
            userRepository = new UserRepository();
        }
        public async Task<User> CreateUser(User user)
        {
            return await userRepository.CreateUser(user);
        }

        public async Task DeleteUser(int id)
        {
            await userRepository.DeleteUser(id);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await userRepository.GetAllUsers();
        }

        public async Task<User> GetUserById(int id)
        {
            return await userRepository.GetUserById(id);
        }

        public async Task<User> UpdateUser(User user)
        {
            return await userRepository.UpdateUser(user);
        }
    }
}
