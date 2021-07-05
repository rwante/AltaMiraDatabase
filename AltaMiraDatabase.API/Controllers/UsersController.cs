using AltaMiraDatabase.Business.Abstract;
using AltaMiraDatabase.Business.Concreate;
using AltaMiraDatabase.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AltaMiraDatabase.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService userService;
        private readonly IDistributedCache _distributedCache;

        /*
        public UsersController()
        {
            userService = new UserManager();
        }*/

        public UsersController(IDistributedCache distributedCache)
        {
            userService = new UserManager();
            _distributedCache = distributedCache;
        }

        [HttpGet]
        public async Task<List<User>> Get()
        {
            return await userService.GetAllUsers();
        }

        [HttpGet("{id}")]
        public async Task<User> Get(int id)
        {
            string cacheKey = id.ToString();
            var usersFromCache = await _distributedCache.GetStringAsync(cacheKey);
            if(usersFromCache == null)
            {
                var usersFromDb = await userService.GetUserById(id);
                var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(1))
                        .SetAbsoluteExpiration(DateTime.Now.AddMonths(1));
                var json_str = JsonConvert.SerializeObject(usersFromDb);
                await _distributedCache.SetStringAsync(cacheKey, json_str, options);
                return usersFromDb;
            }
            var cache_json = JsonConvert.DeserializeObject<User>(usersFromCache);
            return cache_json;
            
        }

        [HttpPost]
        public async Task<User> Post([FromBody]User user)
        {
            var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(1))
                        .SetAbsoluteExpiration(DateTime.Now.AddMonths(1));
            var json_str = JsonConvert.SerializeObject(user);
            string cacheKey = user.Id.ToString();
            await _distributedCache.SetStringAsync(cacheKey, json_str, options);
            return await userService.CreateUser(user);
        }

        [HttpPut]
        public async Task<User> Put([FromBody] User user)
        {
            var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(1))
                        .SetAbsoluteExpiration(DateTime.Now.AddMonths(1));
            var json_str = JsonConvert.SerializeObject(user);
            string cacheKey = user.Id.ToString();
            await _distributedCache.SetStringAsync(cacheKey, json_str, options);
            return await userService.UpdateUser(user);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _distributedCache.RemoveAsync(id.ToString());
            await userService.DeleteUser(id);
        }


    }
}
