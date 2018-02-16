using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PXLIB;
using static PXLIB.PXCL_stc;
using static PXAPI.StructureCW;
using Microsoft.AspNetCore.Http;

namespace PXAPI.Controllers
{
    public class CommonController : Controller
    {

        public static String GetDialogData(JsonGetDialogData data)
        {
            string result = "";

            PX_COMMON PX_COMMONData = new PX_COMMON("JsonGetDialogData", data);
            result = PXCL_com.GetDialogIndication(data.COPCD, data.SNDMSGKBN, data.SNDMSGNO, PX_COMMONData);
            
            return result;
        }

        /// <summary>
        ///  システムDB設定メソッド
        /// </summary>
        public static PX_COMMON SetSysDB(PXAS_AppSetCL appSettings, HttpRequest Request, PX_COMMON PX_COMMONData)
        {
            string host = Request.Host.ToString();
            string domain = host.Split(".")[0];
            
            switch (domain)
            {
                case "Knet":
                    PX_COMMONData.SYSDBSVRNM = appSettings.Knet.SVRName;
                    PX_COMMONData.SYSDBSVRIP = appSettings.Knet.SVRIP;
                    PX_COMMONData.SYSDBNM = appSettings.Knet.DBName;
                    PX_COMMONData.SYSDBSVRUR = appSettings.Knet.DBUser;
                    PX_COMMONData.SYSDBSVRPW = appSettings.Knet.DBPass;
                    break;
                default: break;
            }
            PX_COMMONData = SetConfigKEY(appSettings, Request, PX_COMMONData);
            
            // 以下テストコード
            PX_COMMONData.SYSDBSVRNM = appSettings.Knet.SVRName;
            PX_COMMONData.SYSDBSVRIP = appSettings.Knet.SVRIP;
            PX_COMMONData.SYSDBNM = appSettings.Knet.DBName;
            PX_COMMONData.SYSDBSVRUR = appSettings.Knet.DBUser;
            PX_COMMONData.SYSDBSVRPW = appSettings.Knet.DBPass;

            return PX_COMMONData;
        }

        /// <summary>
        ///  ConfigKey設定メソッドー暫定（あとでSetSysDBと合体するかも）
        /// </summary>
        /// <param name="appSettings"></param>
        /// <param name="Request"></param>
        /// <param name="PX_COMMONData"></param>
        /// <returns></returns>
        public static PX_COMMON SetConfigKEY(PXAS_AppSetCL appSettings, HttpRequest Request, PX_COMMON PX_COMMONData)
        {
            string host = Request.Host.ToString();
            string domain = host.Split(".")[0];

            switch (domain)
            {
                case "Knet":
                    PX_COMMONData.COPCD = appSettings.Knet.COPCD;
                    break;
                default: break;
            }

            // 以下テストコード
            PX_COMMONData.COPCD = appSettings.Knet.COPCD;

            return PX_COMMONData;
        }

    }
}