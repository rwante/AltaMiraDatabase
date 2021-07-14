using AltaMiraDatabase.DataAccess.Abstract;
using AltaMiraDatabase.DataAccess.DTOS;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaMiraDatabase.DataAccess.Concreate
{
    public class LoggerRepository : ILoggerRepository
    {
        private readonly IElasticClient elasticClient;

        public LoggerRepository(IElasticClient _elasticClient)
        {
            elasticClient = _elasticClient;
        }

        public SearchResponseDTO<T> Search<T>(ISearchRequest searchRequest) where T : class
        {
            var response = elasticClient.Search<T>(searchRequest);

            return new SearchResponseDTO<T>()
            {
                IsValid = response.IsValid,
                StatusMessage = response.DebugInformation,
                Exception = response.OriginalException,
                Documents = response.Documents
            };
        }
    }
}
