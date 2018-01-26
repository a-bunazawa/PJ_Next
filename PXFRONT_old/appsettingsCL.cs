using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PXFRONT
{
    public class appsettingsCL
    {
        public class PXAS_AppSetCL
        {
            public string SysURL { get; set; }
            public string HogeHoge { get; set; }
            
        }
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
