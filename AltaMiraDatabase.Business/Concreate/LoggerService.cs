using AltaMiraDatabase.Business.Abstract;
using AltaMiraDatabase.DataAccess.Abstract;
using Microsoft.Extensions.Configuration;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaMiraDatabase.Business.Concreate
{
    public class LoggerService : ILoggerService
    {
        public ILoggerRepository loggerRepository;
        public IConfiguration configuration;

        public LoggerService(ILoggerRepository _loggerRepository, IConfiguration _configuration)
        {
            loggerRepository = _loggerRepository;
            configuration = _configuration;
        }

        public List<T> Execute<T>(int level = 0) where T : class
        {
            IQueryContainer queryContainer = new QueryContainer();
            queryContainer.Term = new TermQuery();
            queryContainer.Term.Field = "level";
            if (level == 1)
            {
                queryContainer.Term.Value = "Info";
            }
            else if(level == -1)
            {
                queryContainer.Term.Value = "Error";
            }
            var response = loggerRepository.Search<T>(new SearchRequest(configuration.GetConnectionString("ElasticSearchIndex"))
            {
                Query = (QueryContainer)queryContainer
            });


            if (response.IsValid)
            {
                return response.Documents.ToList();
            }

            return null;
        }
    }
}
