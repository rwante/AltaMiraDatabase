using AltaMiraDatabase.Business.Abstract;
using AltaMiraDatabase.Business.Concreate;
using AltaMiraDatabase.DataAccess;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AltaMiraDatabase.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private ILoginService loginService;

        public LoginController(UserDbContext userDbContext)
        {
            loginService = new LoginService(userDbContext);
        }
        [HttpGet("login")]
        public async Task<string> Login(string userName, string pass)
        {
            int value = await loginService.Login(userName, pass);
            if(value == 1)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,userName)
                };
                var userIdentity = new ClaimsIdentity(claims, "Users");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);
                return "Now you can access api";
            }
            return "Wrong username or pass";
        }
    }
}
