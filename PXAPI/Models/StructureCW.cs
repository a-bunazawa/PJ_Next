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

        /// <summary> xmlの解析クラス </summary>
        [System.Xml.Serialization.XmlRoot("Root")]
        public class XmlRoot
        {
            [System.Xml.Serialization.XmlElement("Domain")]
            public List<DomainCL> Domain { get; set; }
        }
        public class DomainCL
        {
            [System.Xml.Serialization.XmlElement("DomainName")]
            public string DomainName { get; set; }

            [System.Xml.Serialization.XmlElement("SVRName")]
            public string SVRName { get; set; }
            [System.Xml.Serialization.XmlElement("SVRIP")]
            public string SVRIP { get; set; }
            [System.Xml.Serialization.XmlElement("DBName")]
            public string DBName { get; set; }
            [System.Xml.Serialization.XmlElement("DBUser")]
            public string DBUser { get; set; }
            [System.Xml.Serialization.XmlElement("DBPass")]
            public string DBPass { get; set; }

            [System.Xml.Serialization.XmlElement("COPCD")]
            public string COPCD { get; set; }
            [System.Xml.Serialization.XmlElement("SYSPARAID1")]
            public string SYSPARAID1 { get; set; }
            [System.Xml.Serialization.XmlElement("SYSPARAID2")]
            public string SYSPARAID2 { get; set; }
            [System.Xml.Serialization.XmlElement("SYSPARAID3")]
            public string SYSPARAID3 { get; set; }
        }
    }
}
