using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PXFRONT
{
    public class PXAS_AppSetCL
    {
        public string Hoge { get; set; }
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
