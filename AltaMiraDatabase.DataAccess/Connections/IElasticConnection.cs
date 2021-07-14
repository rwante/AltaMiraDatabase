using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaMiraDatabase.DataAccess.Connections
{
    public interface IElasticConnection
    {
        ConnectionSettings GetConnectionSettings();
    }
}
