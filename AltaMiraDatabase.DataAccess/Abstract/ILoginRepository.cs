using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaMiraDatabase.DataAccess.Abstract
{
    public interface ILoginRepository
    {
        Task<int> Login(string userName, string pass);
    }
}
