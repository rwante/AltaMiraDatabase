using AltaMiraDatabase.Business.Abstract;
using AltaMiraDatabase.Business.Concreate;
using AltaMiraDatabase.DataAccess;
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


        public UsersController(IDistributedCache distributedCache, UserDbContext userDbContext)
        {
            userService = new UserService(distributedCache,userDbContext);
        }

        [HttpGet]
        public async Task<List<User>> Get()
        {
            return await userService.GetAllUsers();
        }

        [HttpGet("{id}")]
        public async Task<User> Get(int id)
        {
            return await userService.GetUserById(id);
            
        }

        [HttpPost]
        public async Task<User> Post([FromBody]User user)
        {
            return await userService.CreateUser(user);
        }

        [HttpPut]
        public async Task<User> Put([FromBody] User user)
        {
            return await userService.UpdateUser(user);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await userService.DeleteUser(id);
        }


    }
}
