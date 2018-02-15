using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PXFRONT.Models;
using System.Diagnostics;
using Microsoft.Extensions.Options;
using static PXLIB.PXCL_stc;

namespace PXFRONT.Controllers
{
    public class StartController : Controller
    {
        readonly IOptions<PXAS_AppSetCL> appSettings;
        /// <summary>
        /// コンストラクターを定義し、引数に構成情報を取得するクラスを定義する。
        /// </summary>
        /// <param name="userSettings"></param>
        public StartController(IOptions<PXAS_AppSetCL> _appSettings)
        {
            //ユーザー設定情報インスタンスをフィールドに保持
            this.appSettings = _appSettings;
        }

        public IActionResult Assortment()
        {
            //  ◆遷移元URL情報
            string host = Request.Host.ToString();
            string domain = host.Split(".")[0];
            
            string parameter = "?";
            switch (domain)
            {
                case "Knet":
                    parameter += "SysURL=" + this.appSettings.Value.Knet.SysURL;
                    parameter += "&SysDB=" + this.appSettings.Value.Knet.SysDB;
                    parameter += "&DomainType=" + "testt3";

                    return Redirect("PXAS/PXAS0000/PXAS0000VW" + parameter);
                default: break;
            }

            parameter += "SysURL=" + "test1";
            parameter += "&SysDB=" + "KN";
            parameter += "&DomainType=" + "PJ3_Next_PXAPI_";

            return Redirect("PXAS/PXAS0000/PXAS0000VW" + parameter);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}