using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PXLIB.PXCL_stc;

namespace PXAPI
{
    public class StructureCW
    {
        /// <summary>
        /// JSONのデータの入れ物
        /// </summary>
        public class JsonGetDialogData : PX_COMMON
        {
            [JsonProperty(PropertyName = "SNDMSGKBN")]
            public string SNDMSGKBN { get; set; }
            [JsonProperty(PropertyName = "SNDMSGNO")]
            public string SNDMSGNO { get; set; }
        }
    }
}
