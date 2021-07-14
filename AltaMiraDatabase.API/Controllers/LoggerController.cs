using AltaMiraDatabase.Business.Abstract;
using AltaMiraDatabase.Entities;
using Microsoft.AspNetCore.Mvc;
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
        public LoggerController(ILoggerService _loggerService)
        {
            loggerService = _loggerService;
        }

        [HttpGet("{id}")]
        public List<Log> Get(int id)
        {
            return loggerService.Execute<Log>(id);
        }
    }
}
