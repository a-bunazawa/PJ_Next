using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static PXLIB.PXCL_stc;
using static PXAPI.StructureCW;
using Microsoft.AspNetCore.Hosting;


namespace PXAPI.Controllers
{
    /// <summary>
    /// Debug Only
    /// </summary>
    public class HomeController : Controller
    {

        readonly IOptions<PXAS_AppSetCL> _appSettings;
        private IHostingEnvironment _hostingEnvironment = null;
        /// <summary>
        /// コンストラクターを定義し、引数に構成情報を取得するクラスを定義する。
        /// </summary>
        /// <param name="userSettings"></param>
        public HomeController(IOptions<PXAS_AppSetCL> appSettings, IHostingEnvironment hostingEnvironment)
        {
            this._appSettings = appSettings;
            //ユーザー設定情報インスタンスをフィールドに保持
            this._hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {

            CommonController controller = new CommonController(_appSettings, _hostingEnvironment);




            ViewBag.Title = "Home Page";
            return View();
        }
    }
}