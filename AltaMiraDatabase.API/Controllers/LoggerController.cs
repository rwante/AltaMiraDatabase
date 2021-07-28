using AltaMiraDatabase.Business.Abstract;
using AltaMiraDatabase.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AltaMiraDatabase.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoggerController : Controller
    {
        public ILoggerService loggerService;
        public LoggerController(ILoggerService _loggerService, ILogger<IStartup> _ilog)
        {
            loggerService = _loggerService;
        }

        [HttpGet("{id}")]
        public List<Log> Get(int id)
        {
            return loggerService.Execute<Log>(id);
        }

        [HttpGet("Error")]
        public List<Log> GetError()
        {
            return loggerService.Execute<Log>(-1);
        }

        [HttpGet("Info")]
        public List<Log> GetInfo()
        {
            return loggerService.Execute<Log>(1);
        }
    }
}
