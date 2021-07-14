using AltaMiraDatabase.DataAccess.DTOS;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaMiraDatabase.DataAccess.Abstract
{
    public interface ILoggerRepository
    {
        SearchResponseDTO<T> Search<T>(ISearchRequest searchRequest) where T : class;
    }
}
