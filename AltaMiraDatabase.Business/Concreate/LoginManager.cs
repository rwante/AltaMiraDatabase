using AltaMiraDatabase.Business.Abstract;
using AltaMiraDatabase.DataAccess.Abstract;
using AltaMiraDatabase.DataAccess.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaMiraDatabase.Business.Concreate
{
    public class LoginManager : ILoginService
    {
        private ILoginRepository loginRepository;
        public LoginManager()
        {
            loginRepository = new LoginRepository();
        }
        public async Task<int> Login(string userName, string pass)
        {
            return await loginRepository.Login(userName, pass);
        }
    }
}
