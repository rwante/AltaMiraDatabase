using AltaMiraDatabase.Business.Abstract;
using AltaMiraDatabase.Business.Concreate;
using AltaMiraDatabase.DataAccess;
using AltaMiraDatabase.Entities;
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

        public LoginController(ILoginService _loginService)
        {
            loginService = _loginService;
        }
        [HttpPost("login")]
        public async Task<string> Login([FromBody]Login login)
        {
            int value = await loginService.Login(login.Username, login.Pass);
            if(value == 1)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,login.Username)
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
