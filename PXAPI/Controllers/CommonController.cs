using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PXLIB;
using static PXLIB.PXCL_stc;
using static PXAPI.StructureCW;
using Microsoft.AspNetCore.Hosting;

namespace PXAPI.Controllers
{
    public class CommonController : Controller
    {
        readonly IOptions<PXAS_AppSetCL> _appSettings;
        private IHostingEnvironment _hostingEnvironment = null;
        /// <summary>
        /// コンストラクターを定義し、引数に構成情報を取得するクラスを定義する。
        /// </summary>
        /// <param name="userSettings"></param>
        public CommonController(IOptions<PXAS_AppSetCL> appSettings, IHostingEnvironment hostingEnvironment)
        {
            this._appSettings = appSettings;
            //ユーザー設定情報インスタンスをフィールドに保持
            this._hostingEnvironment = hostingEnvironment;
        }

        public String GetDialogData(JsonGetDialogData data)
        {
            string result = "";

            string path = _hostingEnvironment.ContentRootPath + "/DBConnect.xml";
            XmlRoot xmlData = PXCL_com.LoadXmlData<XmlRoot>(path);


            //PX_COMMON PX_COMMONData = SetSysDB(_appSettings.Value, Request, data);
            PX_COMMON PX_COMMONData = new PX_COMMON();
            result = PXCL_com.GetDialogIndication(data.COPCD, data.SNDMSGKBN, data.SNDMSGNO, PX_COMMONData);

            return result;
        }


    }
}