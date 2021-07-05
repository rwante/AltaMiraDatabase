using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaMiraDatabase.Business.Abstract
{
    public interface ILoginService
    {
        Task<int> Login(string userName, string pass);
    }
}
