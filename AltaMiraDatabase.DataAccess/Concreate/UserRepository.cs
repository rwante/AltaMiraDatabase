using AltaMiraDatabase.DataAccess.Abstract;
using AltaMiraDatabase.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaMiraDatabase.DataAccess.Concreate
{
    public class UserRepository : IUserRepository
    {
        public UserDbContext userDbContext;
        
        public UserRepository(UserDbContext _userDbContext)
        {
            userDbContext = _userDbContext;
        }
        public async Task<User> CreateUser(User user)
        {
            user.Pass = RandomString(8,true);
            userDbContext.Users.Add(user);
            await userDbContext.SaveChangesAsync();
            return user;
    }

        public async Task DeleteUser(int id)
        {
            var deletedUser = await GetUserById(id);
            userDbContext.Users.Remove(deletedUser);
            await userDbContext.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllUsers()
        {
            var list = await userDbContext.Users.Include(x => x.Address.Geo).Include(y => y.Company).ToListAsync();
            foreach(var i in list)
            {
                i.Pass = null;
            }
            return list;
        }

        public async Task<User> GetUserById(int id)
        {
            return await userDbContext.Users.Where(x => x.Id == id).Include(x => x.Address.Geo).Include(y => y.Company).FirstOrDefaultAsync();
        }

        public async Task<User> UpdateUser(User user)
        {
            userDbContext.Users.Update(user);
            await userDbContext.SaveChangesAsync();
            return user;
        }

        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
    }
}
