using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Html;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using static PXLIB.PXCL_stc;
using static PXAPI.StructureCW;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PXAPI.Areas.PXAS.Controllers
{
    public class PXAS0000Controller : Controller
    {

        public JsonGetMenuData Test(String id)
        {
            JsonGetMenuData test = new JsonGetMenuData
            {
                Id = id,
                DB = "aaa"
            };

            return test;
        }
        // GET: PXAS/Test2
        public String Test2(String aaa, String id)
        {
            return id;
        }
        // GET: PXAS/Test3
        public String Test3(String aaa, String id)
        {
            return id;
        }

        /// <summary>
        /// JSONのデータの入れ物
        /// </summary>
        public class JsonGetMenuData
        {
            [JsonProperty(PropertyName = "Id")]
            public string Id { get; set; }
            [JsonProperty(PropertyName = "DB")]
            public string DB { get; set; }

            [JsonProperty(PropertyName = "COPCD")]
            public string COPCD { get; set; }
            [JsonProperty(PropertyName = "MENUID")]
            public string MENUID { get; set; }

        }
    }
}
