using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static PXLIB.PXCL_stc;
using static PXAPI.StructureCW;

namespace PXAPI.Controllers
{
    public class LoginController : Controller
    {

        readonly IOptions<PXAS_AppSetCL> appSettings;

        public LoginController(IOptions<PXAS_AppSetCL> _appSettings)
        {
            //ユーザー設定情報インスタンスをフィールドに保持
            this.appSettings = _appSettings;
        }

        public string PrepareLogin(HttpRequest Request, PX_COMMON PX_COMMONData)
        {
            JsonGetDialogData data = new JsonGetDialogData
            {
                DBINF = "TESTDB",
                COPCD = "0001",
                SNDMSGKBN = "123",
                SNDMSGNO = "999"
            };

            //* 事前準備(DB接続準備) *//

            // システム起動元の判別 - Client側

            // Configファイルの読込み
            string dlgMsgFormat = "1,0,エラー,システムに接続できません。\r\nc管理者へお問い合わせください。(エラーコード: {0}),OK,{1}_00";
            PX_COMMONData = CommonController.SetSysDB(this.appSettings.Value, Request, PX_COMMONData);
            if (PX_COMMONData.ERRORCODE != "")
            {
                return string.Format(dlgMsgFormat, "PXERR201", data.SNDMSGNO);
            }

            //* 事前準備(DBより情報取得) *//
            // システムDBへの接続
            PXLIB.PXCL_dba cnn = null;
            try
            {
                cnn = new PXLIB.PXCL_dba(PXLIB.PXCL_dba.ConnectionSystem, PX_COMMONData);
            }
            catch
            {
                PX_COMMONData.ERRORCODE = "PXERR301";
                return string.Format(dlgMsgFormat, "PXERR301", data.SNDMSGNO);
            }

            // PJ3SystemConfig(SysPara)の取得




            // ログイン情報の取得

            //* ユーザー認証 *//
            // ユーザーログイン
            // ユーザー認証
            // ワンタイムユーザー処理
            // 通常ユーザーの処理
            // 


            return CommonController.GetDialogData(data);
        }

    }
}