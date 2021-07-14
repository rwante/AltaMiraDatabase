using AltaMiraDatabase.Business.Abstract;
using AltaMiraDatabase.DataAccess;
using AltaMiraDatabase.DataAccess.Abstract;
using AltaMiraDatabase.DataAccess.Concreate;
using AltaMiraDatabase.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaMiraDatabase.Business.Concreate
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;
        private readonly IDistributedCache _distributedCache;
        public UserService(IDistributedCache distributedCache, IUserRepository _userRepository)
        {
            userRepository = _userRepository;
            _distributedCache = distributedCache;
        }
        public async Task<User> CreateUser(User user)
        {
            var UserFromDb = await userRepository.CreateUser(user);
            if(UserFromDb != null)
            {
                var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(1))
                        .SetAbsoluteExpiration(DateTime.Now.AddMonths(1));
                var json_str = JsonConvert.SerializeObject(UserFromDb);
                string cacheKey = UserFromDb.Id.ToString();
                await _distributedCache.SetStringAsync(cacheKey, json_str, options);
            }
            return UserFromDb;
        }

        public async Task DeleteUser(int id)
        {
            await _distributedCache.RemoveAsync(id.ToString());
            await userRepository.DeleteUser(id);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await userRepository.GetAllUsers();
        }

        public async Task<User> GetUserById(int id)
        {
            string cacheKey = id.ToString();
            var usersFromCache = await _distributedCache.GetStringAsync(cacheKey);
            if (usersFromCache == null)
            {
                var usersFromDb = await userRepository.GetUserById(id);
                var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(1))
                        .SetAbsoluteExpiration(DateTime.Now.AddMonths(1));
                var json_str = JsonConvert.SerializeObject(usersFromDb);
                await _distributedCache.SetStringAsync(cacheKey, json_str, options);
                return usersFromDb;
            }
            var cache_json = JsonConvert.DeserializeObject<User>(usersFromCache);
            return cache_json;
        }

        public async Task<User> UpdateUser(User user)
        {
            var UserFromDb = await userRepository.UpdateUser(user);
            if(UserFromDb != null)
            {
                var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(1))
                        .SetAbsoluteExpiration(DateTime.Now.AddMonths(1));
                var json_str = JsonConvert.SerializeObject(UserFromDb);
                string cacheKey = UserFromDb.Id.ToString();
                await _distributedCache.SetStringAsync(cacheKey, json_str, options);
            }
            return UserFromDb;
        }
    }
}
