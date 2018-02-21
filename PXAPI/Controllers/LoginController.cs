using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static PXLIB.PXCL_stc;
using static PXAPI.StructureCW;
using Microsoft.AspNetCore.Hosting;

namespace PXAPI.Controllers
{
    public class LoginController : Controller
    {

        readonly IOptions<PXAS_AppSetCL> _appSettings;
        private IHostingEnvironment _hostingEnvironment = null;
        /// <summary>
        /// コンストラクターを定義し、引数に構成情報を取得するクラスを定義する。
        /// </summary>
        public LoginController(IOptions<PXAS_AppSetCL> appSettings, IHostingEnvironment hostingEnvironment)
        {
            this._appSettings = appSettings;
            //ユーザー設定情報インスタンスをフィールドに保持
            this._hostingEnvironment = hostingEnvironment;
        }
        /// <summary>
        /// Login準備（ログイン前に必要な情報を取得する）
        /// </summary>
        /// <param name="Request"></param>
        /// <param name="PX_COMMONData"></param>
        /// <returns></returns>
        public PX_PJ3CONFIG PrepareLogin(HttpRequest Request, PX_COMMON PX_COMMONData)
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
            PX_COMMONData = CommonController.SetSysDB(this._appSettings.Value, Request, PX_COMMONData);
            if (PX_COMMONData.ERRORCODE != "")
            {
                PX_COMMONData.ERRORMSG = string.Format(dlgMsgFormat, "PXERR201", data.SNDMSGNO);
                return new PX_PJ3CONFIG();
            }

            //* 事前準備(DBより情報取得) *//
            // システムDBへの接続
            // PJ3SystemConfig(SysPara)の取得
            PXLIB.PXCL_dba cnn = null;
            PX_PJ3CONFIG sysParaList = new PX_PJ3CONFIG();
            try
            {
                cnn = new PXLIB.PXCL_dba(PXLIB.PXCL_dba.ConnectionSystem, PX_COMMONData);
                if (cnn.DBConect() != "")
                {
                    PX_COMMONData.ERRORCODE = "PXERR301";
                    PX_COMMONData.ERRORMSG = string.Format(dlgMsgFormat, "PXERR301", data.SNDMSGNO);
                    return new PX_PJ3CONFIG();

                }
                cnn.DBClose();

                sysParaList = PXLIB.PXCL_com.GetPJ3Config(this._appSettings.Value.Knet.SYSPARAID1, this._appSettings.Value.Knet.SYSPARAID2, this._appSettings.Value.Knet.SYSPARAID3, ref PX_COMMONData);
                if (PX_COMMONData.ERRORCODE != "")
                {
                    PX_COMMONData.ERRORMSG = string.Format(dlgMsgFormat, PX_COMMONData.ERRORCODE, data.SNDMSGNO);
                    return new PX_PJ3CONFIG();
                }
            }
            catch
            {
                PX_COMMONData.ERRORCODE = "PXERR300";
                PX_COMMONData.ERRORMSG = string.Format(dlgMsgFormat, "PXERR300", data.SNDMSGNO);
                return new PX_PJ3CONFIG();
            }

            return sysParaList;
        }

        public string UserAuthenticationProcess(string userid, string pass, int PALOCCNT, ref PX_COMMON PX_COMMONData)
        {
            // ログイン情報の取得
            string retMessage = "";

            //* ユーザー認証 *//
            List<string> otpass = PXLIB.PXCL_com.GetUserKeyInfo(userid, ref PX_COMMONData);
            if (PX_COMMONData.ERRORCODE != "")
            {
                // エラーメッセージ生成
                //ToDo: SNDMSGKBN部分入れ替え
                return PXLIB.PXCL_com.GetDialogIndication(PX_COMMONData.COPCD, "SNDMSGKBN", PX_COMMONData.ERRORCODE, PX_COMMONData);
            }

            string nowdateOT = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
            string nowdate = DateTime.Today.ToString("yyyyMMdd");
            if (otpass.Count == 0)
            {
                // 通常のユーザー
                // 未入力時
                if (pass.Trim().Length == 0)
                {
                    if (otpass[3]=="YES")
                    {
                        if (otpass[4].CompareTo(nowdate) < 0)
                        {
                            // 有効期限切れ

                        }
                    }
                    else
                    {
                        //ToDo: SNDMSGKBN部分入れ替え
                        PX_COMMONData.ERRORCODE = "PASSERR - 01";
                        return PXLIB.PXCL_com.GetDialogIndication(PX_COMMONData.COPCD, "SNDMSGKBN", PX_COMMONData.ERRORCODE, PX_COMMONData);
                    }
                }
                else
                {
                    // 入力あり
                    if (pass == otpass[4])
                    {
                        if (otpass[5]!="00000000")
                        {
                            if (otpass[5].CompareTo(nowdate) < 0)
                            {
                                // 期限切れ
                                PX_COMMONData.ERRORCODE = "PASSERR - 02";
                                return PXLIB.PXCL_com.GetDialogIndication(PX_COMMONData.COPCD, "SNDMSGKBN", PX_COMMONData.ERRORCODE, PX_COMMONData);
                            }
                            else
                            {
                                if (otpass[6] != "00000000")
                                {
                                    if (otpass[6].CompareTo(nowdate) < 0)
                                    {
                                        PX_COMMONData.ERRORCODE = "PASSALM-01";
                                        retMessage = PXLIB.PXCL_com.GetDialogIndication(PX_COMMONData.COPCD, "SNDMSGKBN", PX_COMMONData.ERRORCODE, PX_COMMONData);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        // パスワードUnMatch
                        //ToDo: SNDMSGKBN部分入れ替え
                        PX_COMMONData.ERRORCODE = "PASSERR - 01";
                        return PXLIB.PXCL_com.GetDialogIndication(PX_COMMONData.COPCD, "SNDMSGKBN", PX_COMMONData.ERRORCODE, PX_COMMONData);
                    }
                }
            }
            else
            {
                // *ワンタイムユーザー* //
                // パスワード未入力
                if (pass.Trim().Length == 0)
                {
                    //ToDo: SNDMSGKBN部分入れ替え
                    PX_COMMONData.ERRORCODE = "PASSERR - 01";
                    return PXLIB.PXCL_com.GetDialogIndication(PX_COMMONData.COPCD, "SNDMSGKBN", PX_COMMONData.ERRORCODE, PX_COMMONData);
                }
                else
                {
                    if (pass == otpass[0])
                    {
                        // 有効期限チェック
                        if (otpass[1] != "")
                        {
                            if (otpass[1].CompareTo(nowdateOT) < 0)
                            {
                                // 期限切れ
                                PX_COMMONData.ERRORCODE = "PASSERR - 02";
                                return PXLIB.PXCL_com.GetDialogIndication(PX_COMMONData.COPCD, "SNDMSGKBN", PX_COMMONData.ERRORCODE, PX_COMMONData);
                            }
                        }
                        if (otpass[2] != "")
                        {
                            if (otpass[2].CompareTo(nowdateOT) < 0)
                            {
                                // 警告期限切れ
                                PX_COMMONData.ERRORCODE = "PASSALM-01";
                                retMessage = PXLIB.PXCL_com.GetDialogIndication(PX_COMMONData.COPCD, "SNDMSGKBN", PX_COMMONData.ERRORCODE, PX_COMMONData);
                            }
                        }
                    }
                    else
                    {
                        // パスワードUnMatch
                        //ToDo: SNDMSGKBN部分入れ替え
                        PX_COMMONData.ERRORCODE = "PASSERR - 01";
                        return PXLIB.PXCL_com.GetDialogIndication(PX_COMMONData.COPCD, "SNDMSGKBN", PX_COMMONData.ERRORCODE, PX_COMMONData);
                    }
                }
            }

            // LoginOK
            PX_COMMONData.DEFCOPTP = otpass[7];
            switch (otpass[7])
            {
                case "DEF":
                    PX_COMMONData.COPCD = otpass[8];
                    break;
                case "END":
                    PX_COMMONData.COPCD = otpass[9];
                    break;
                default:
                    // ToDo
                    break;
            }

            PXLIB.PXCL_com.GetUserCTL(PX_COMMONData);
            PXLIB.PXCL_com.GetUserOPT(PX_COMMONData);


            // ユーザーログイン
            // ユーザー認証
            // ワンタイムユーザー処理
            // 通常ユーザーの処理
            // 

            return retMessage;
        }
    }
}