using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static PXLIB.PXCL_stc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PXFRONT.Areas.PXAS.Controllers
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

        [Area("PXAS")]
        public IActionResult PXAS0000VW()
        {
            PXAS0000CW data = new PXAS0000CW();
            data.SysURL = Request.Query["SysURL"].ToString();
            data.SysDB = Request.Query["SysDB"].ToString();
            data.DomainType = Request.Query["DomainType"].ToString();


            return View("PXAS0000VW", data);
        }
        [Area("PXAS")]
        public ActionResult _PXAS0000VW()
        {
            return View("_PXAS0000VW");
        }
    }
}
