using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaMiraDatabase.DataAccess.DTOS
{
    public class IndexResponseDTO
    {
        public bool IsValid { get; set; }
        public string StatusMessage { get; set; }
        public Exception Exception { get; set; }
    }
}
