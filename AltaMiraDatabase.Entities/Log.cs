using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AltaMiraDatabase.Entities
{
    public class Log
    {
        [Text(Name = "@timestamp")]
        public string Timestamp { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
    }
}
