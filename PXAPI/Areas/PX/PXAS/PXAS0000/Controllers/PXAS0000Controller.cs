using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using static PXLIB.PXCL_stc;
using static PXAPI.StructureCW;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PXAPI.Areas.PXAS.Controllers
{
    public class PXAS0000Controller : Controller
    {
        readonly IOptions<PXAS_AppSetCL> appSettings;
        /// <summary>
        /// コンストラクターを定義し、引数に構成情報を取得するクラスを定義する。
        /// </summary>
        /// <param name="userSettings"></param>
        public PXAS0000Controller(IOptions<PXAS_AppSetCL> _appSettings)
        {
            //ユーザー設定情報インスタンスをフィールドに保持
            this.appSettings = _appSettings;
        }

        public JsonGetMenuData Test(String id)
        {
            JsonGetMenuData test = new JsonGetMenuData();
            test.Id = id;
            test.DB = "aaa";

            JsonGetDialogData data = new JsonGetDialogData();
            PX_COMMON PX_COMMONData = new PX_COMMON();
            PX_COMMONData = PXAPI.Controllers.CommonController.SetSysDB(this.appSettings.Value, Request, (PX_COMMON)data);





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
