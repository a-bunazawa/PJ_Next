using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PXFRONT.Areas.PXMS.Controllers
{
    public class PXMS0000Controller : Controller
    {
        /// <summary>
        /// メニュー画面
        /// </summary>
        /// <returns> 画面表示 </returns>
        [Area("PXMS")]
        public ActionResult PXMS0000VW()
        {
            return View("PXMS0000VW");
        }
    }
}
