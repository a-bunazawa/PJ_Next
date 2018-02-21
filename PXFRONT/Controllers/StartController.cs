using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PXFRONT.Models;
using System.Diagnostics;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting;
using PXLIB;
using static PXLIB.PXCL_stc;
using static PXFRONT.StructureCW;

namespace PXFRONT.Controllers
{
    public class StartController : Controller
    {
        readonly IOptions<PXAS_AppSetCL> appSettings;
        private IHostingEnvironment hostingEnvironment = null;
        /// <summary>
        /// コンストラクターを定義し、引数に構成情報を取得するクラスを定義する。
        /// </summary>
        /// <param name="userSettings"></param>
        public StartController(IOptions<PXAS_AppSetCL> _appSettings, IHostingEnvironment _hostingEnvironment)
        {
            //ユーザー設定情報インスタンスをフィールドに保持
            this.appSettings = _appSettings;
            this.hostingEnvironment = _hostingEnvironment;
        }

        public IActionResult Assortment()
        {
            //  ◆遷移元URL情報
            string host = Request.Host.ToString();
            string domain = host.Split(".")[0];

            string parameter = "?";
            try
            {
                string path = hostingEnvironment.ContentRootPath + "/DomainAccess.xml";
                
                XmlRoot xmlData = PXCL_com.LoadXmlData<XmlRoot>(path);

                foreach (DomainCL DomainData in xmlData.Domain)
                {
                    if (DomainData.DomainName == domain)
                    {
                        parameter += "SysURL=" + DomainData.SysURL;
                        parameter += "&DomainType=" + DomainData.DomainType;
                        parameter += "&SysDB=" + "";

                    }
                }
            }
            catch (Exception ex)
            {
                string LogTitle = "ドメイン判定処理";
                string LogMsg = "エラー「" + ex.Message + "」";
                PXCL_log.writeLog(PXCL_log.ERR, PXCL_log.SELECT, LogTitle, LogMsg, System.Reflection.MethodBase.GetCurrentMethod(), new PX_COMMON());

            }

            parameter += "SysURL=" + "test1";
            parameter += "&SysDB=" + "KN";
            parameter += "&DomainType=" + "PJ3_Next_PXAPI_";

            return Redirect("~/PXAS/PXAS0000/PXAS0000VW" + parameter);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}