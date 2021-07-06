using AltaMiraDatabase.DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaMiraDatabase.DataAccess.Concreate
{
    public class LoginRepository : ILoginRepository
    {
        public UserDbContext userDbContext;

        public LoginRepository(UserDbContext _userDbContext)
        {
            userDbContext = _userDbContext;
        }
        
        public async Task<int> Login(string userName, string pass)
        {
            var info = await userDbContext.Users.FirstOrDefaultAsync(
                x => x.Username == userName && x.Pass == pass);
            if(info != null)
            {
                return 1;
            }
            return 0;
        }
    }
}
