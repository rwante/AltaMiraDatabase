using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaMiraDatabase.DataAccess.Connections
{
    public static class ElasticConnection
    {
        public static void GetConnectionSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration.GetConnectionString("ElasticSearchUri");

            var settings = new ConnectionSettings(new Uri(url));

            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);
        }
    }
}
