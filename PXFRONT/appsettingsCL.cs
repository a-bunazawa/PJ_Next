using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PXFRONT
{
    public class appsettingsCL
    {
        public class Logging
        {
            public string IncludeScopes { get; set; }
            public LogLevel LogLevel { get; set; }
        }
        public class LogLevel
        {
            public string Default { get; set; }
        }
    }
}
