using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static PXLIB.PXCL_stc;
using System.Text;
using Newtonsoft.Json;

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
            
            String result = "";
            String url = "http://localhost/PXAPI/api/PXAS0000/PrepareStartView";
            // →　言語リストを返すメソッド：引数なし

            //String url = "http://localhost/PXAPI/api/PXAS0000/PrepareLogin";
            // →　ログイン準備メソッド。Configを返す：引数　BrowserType：システム起動元, SelectedLanguage：選択された言語No

            //String url = "http://localhost/PXAPI/api/PXAS0000/UserAuthenticationProcess";
            // →　ログイン認証メソッド。PX_COMMONを返す：userid, pass

            try
            {
                // WebClient
                System.Net.WebClient wc = new System.Net.WebClient();
                //NameValueCollectionの作成
                System.Collections.Specialized.NameValueCollection ps = new System.Collections.Specialized.NameValueCollection();

                //ヘッダにContent-Typeを加える
                wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                
                System.Collections.Specialized.NameValueCollection postData =
                    new System.Collections.Specialized.NameValueCollection();
                //postData.Add("userid", "1600008");
                //postData.Add("pass", "kcom");

                //postData.Add("HostName", "Knet");
                //postData.Add("SYSDBNM", "pmt_admin");
                //postData.Add("Id", "pmt_admin");
                //postData.Add("Pass", "kcom");
                //postData.Add("DB", "KN");

                byte[] resData = wc.UploadValues(url, postData);

                wc.Dispose();
                //受信したデータを表示する
                result = System.Text.Encoding.UTF8.GetString(resData);
            }
            catch (Exception exc)
            {
                String LogTitle = "エラータイトル";
                String LogMsg = "エラー内容「" + exc.Message + "」";
                LogTitle = LogMsg;
            }




            return View("PXAS0000VW", data);
        }
        [Area("PXAS")]
        public ActionResult _PXAS0000VW()
        {
            return View("_PXAS0000VW");
        }
    }
}
