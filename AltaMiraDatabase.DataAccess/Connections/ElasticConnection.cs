using Microsoft.Extensions.Configuration;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaMiraDatabase.DataAccess.Connections
{
    public class ElasticConnection : IElasticConnection
    {
        public IConfiguration configuration;
        public ElasticConnection(IConfiguration _configuration)
        {
            configuration = _configuration;

        }

        public ConnectionSettings GetConnectionSettings()
        {
            var url = configuration.GetConnectionString("ElasticSearchUri");

            var settings = new ConnectionSettings(new Uri(url));

            return settings;
        }
    }
}
