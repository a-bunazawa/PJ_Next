using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PXLIB.PXCL_stc;

namespace PXFRONT
{
    public class StructureCW
    {
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

            [System.Xml.Serialization.XmlElement("DomainType")]
            public string DomainType { get; set; }
            [System.Xml.Serialization.XmlElement("SysURL")]
            public string SysURL { get; set; }
        }
    }
}
