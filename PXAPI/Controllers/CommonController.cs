using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PXLIB;
using static PXLIB.PXCL_stc;
using static PXAPI.StructureCW;

namespace PXAPI.Controllers
{
    public class CommonController : Controller
    {
        public String GetDialogData(JsonGetDialogData data)
        {
            string result = "";

            PX_COMMON PX_COMMONData = new PX_COMMON();
            PX_COMMONData.DBINF = data.DBINF;
            PX_COMMONData.COPCD = data.COPCD;
            PX_COMMONData.USERID = data.USERID;
            PX_COMMONData.MENUID = data.MENUID;
            PX_COMMONData.USERDBSVRNM = data.USERDBSVRNM;
            PX_COMMONData.COPCD = data.COPCD;
            PX_COMMONData.COPCD = data.COPCD;
            PX_COMMONData.COPCD = data.COPCD;
            PX_COMMONData.COPCD = data.COPCD;


            //LNVALUEMDData.SERVERDB = data.DB;
            //LNVALUEMDData.USERID = data.Id;
            //LNVALUEMDData.MENUID = data.MENUID;
            //data.Name = LNAS0001Data.LNVALUEMDData.USERNM;
            //data.COPCD = LNAS0001Data.LNVALUEMDData.COPCD;
            //data.SYSID = LNAS0001Data.LNVALUEMDData.SYSID;
            //data. = LNAS0001Data.LNVALUEMDData.MENUID;
            //data. = LNAS0001Data.LNVALUEMDData.USERDBNM;
            //data. = LNAS0001Data.LNVALUEMDData.USERDBSVRNM;
            //data.USERDBSVRIP = LNAS0001Data.LNVALUEMDData.USERDBSVRIP;
            //data.USERDBSVRUR = LNAS0001Data.LNVALUEMDData.USERDBSVRUR;
            //data.USERDBSVRPW = LNAS0001Data.LNVALUEMDData.USERDBSVRPW;
            //data.INIGRPCD = LNAS0001Data.LNVALUEMDData.INIGRPCD;
            //data.INIDPTCD = LNAS0001Data.LNVALUEMDData.INIDPTCD;
            //data.INIWHSCD = LNAS0001Data.LNVALUEMDData.INIWHSCD;
            //data.INICMPCD = LNAS0001Data.LNVALUEMDData.INICMPCD;
            //data.INICSTCD = LNAS0001Data.LNVALUEMDData.INICSTCD;
            //data.INISHPCD = LNAS0001Data.LNVALUEMDData.INISHPCD;




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

            // 以下テストコード
            PX_COMMONData.SYSDBSVRNM = appSettings.Knet.SVRName;
            PX_COMMONData.SYSDBSVRIP = appSettings.Knet.SVRIP;
            PX_COMMONData.SYSDBNM = appSettings.Knet.DBName;
            PX_COMMONData.SYSDBSVRUR = appSettings.Knet.DBUser;
            PX_COMMONData.SYSDBSVRPW = appSettings.Knet.DBPass;

            return PX_COMMONData;
        }

    }
}