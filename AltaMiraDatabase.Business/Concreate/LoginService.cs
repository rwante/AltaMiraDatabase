using AltaMiraDatabase.Business.Abstract;
using AltaMiraDatabase.DataAccess;
using AltaMiraDatabase.DataAccess.Abstract;
using AltaMiraDatabase.DataAccess.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaMiraDatabase.Business.Concreate
{
    public class LoginService : ILoginService
    {
        private ILoginRepository loginRepository;
        public LoginService(UserDbContext userDbContext)
        {
            loginRepository = new LoginRepository(userDbContext);
        }
        public async Task<int> Login(string userName, string pass)
        {
            return await loginRepository.Login(userName, pass);
        }
    }
}
