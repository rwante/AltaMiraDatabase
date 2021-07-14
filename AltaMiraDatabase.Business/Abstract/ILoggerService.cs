using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaMiraDatabase.Business.Abstract
{
    public interface ILoggerService
    {
        List<T> Execute<T>(int i = 0) where T : class;
    }
}
