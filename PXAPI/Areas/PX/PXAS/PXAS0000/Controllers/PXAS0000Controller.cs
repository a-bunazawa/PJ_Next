using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static PXLIB.PXCL_stc;
using static PXAPI.StructureCW;
using Microsoft.AspNetCore.Hosting;
using PXAPI.Controllers;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Libuv.Internal.Networking;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PXAPI.Areas.PXAS
{
    public class PXAS0000Controller : Controller
    {

        /// <summary>ドメイン名</summary>
        private string ms_DomainHost;
        /// <summary>ドメイン名</summary>
        private string ms_DomainTP;

        /// <summary>DB未接続時のエラーメッセージのフォーマット</summary>
        private string ms_dlgErrMsgFormat;


        private IHostingEnvironment hostingEnvironment = null;
        /// <summary>
        /// コンストラクターを定義し、引数に構成情報を取得するクラスを定義する。
        /// </summary>
        public PXAS0000Controller(IHostingEnvironment _hostingEnvironment)
        {
            //this._appSettings = appSettings;
            //ユーザー設定情報インスタンスをフィールドに保持
            this.hostingEnvironment = _hostingEnvironment;

            // ドメイン名の取得と保存
            //var request = HttpContext.Request;
            //string hostname = request.Host.ToString();
            //string[] domain = hostname.Split(".");
            //ms_DomainName = domain[0];

            //// For Debug：リリース時には削除すること！！
            //if (ms_DomainName == "localhost") ms_DomainName = "Knet";

            ms_DomainHost = "Knet";
            ms_DomainTP = Enum.GetName(typeof(PX_PJ3CONFIG.PAGE_TYPE), PX_PJ3CONFIG.PAGE_TYPE.USE);

            ms_dlgErrMsgFormat = "1,0,エラー,システムに接続できません。\r\n管理者へお問い合わせください。(エラーコード: {0}),OK,{0}_00";
        }

        private string[] GetDBAccessInfoFromXML(ref PX_COMMON PX_COMMONData)
        {
            string[] syspara = new string[] { "", "", "" };

            // Configファイルの読込み
            string path = hostingEnvironment.ContentRootPath + "/DBConnect.xml";
            XmlRoot xmlData = PXLIB.PXCL_com.LoadXmlData<XmlRoot>(path);
            foreach (var item in xmlData.Domain)
            {
                if (ms_DomainHost == item.DomainName)
                {
                    PX_COMMONData.SYSDBSVRNM = item.SVRName;
                    PX_COMMONData.SYSDBSVRIP = item.SVRIP;
                    PX_COMMONData.SYSDBNM = item.DBName;
                    PX_COMMONData.SYSDBSVRUR = item.DBUser;
                    PX_COMMONData.SYSDBSVRPW = item.DBPass;

                    PX_COMMONData.COPCD = item.COPCD;
                    syspara[0] = item.SYSPARAID1;
                    syspara[1] = item.SYSPARAID2;
                    syspara[2] = item.SYSPARAID3;
                    break;
                }
            }
            return syspara;
        }


        /// <summary>
        /// Login準備1（画面生成に必要な最低限の情報を取得する）
        /// </summary>
        /// <returns></returns>
        public List<PX_LANGUAGE> PrepareStartView()
        {
            PX_COMMON PX_COMMONData = new PX_COMMON();
            string[] syspara = GetDBAccessInfoFromXML(ref PX_COMMONData);

            //* 事前準備(DBより情報取得) *//
            // システムDBへの接続
            // PJ3SystemConfig(SysPara)の取得
            PXLIB.PXCL_dba cnn = null;
            List<PX_LANGUAGE> LangList = new List<PX_LANGUAGE>
            {
                new PX_LANGUAGE("JPN")
            };
            try
            {
                // DBへの接続確認
                cnn = new PXLIB.PXCL_dba(PXLIB.PXCL_dba.ConnectionSystem, PX_COMMONData);
                if (cnn.DBConect() != "")
                {
                    PX_COMMONData.ERRORCODE = "PXERR301";
                    PX_COMMONData.ERRORMSG = string.Format(ms_dlgErrMsgFormat, PX_COMMONData.ERRORCODE);
                    return LangList;
                }
                cnn.DBClose();

                PX_PJ3CONFIG sysParaList = PXLIB.PXCL_com.GetPJ3Config(syspara, ref PX_COMMONData);
                if (PX_COMMONData.ERRORCODE != "")
                {
                    PX_COMMONData.ERRORMSG = string.Format(ms_dlgErrMsgFormat, PX_COMMONData.ERRORCODE, PX_COMMONData.ERRORCODE);
                    return LangList;
                }

                LangList = PXLIB.PXCL_com.GetLanguageList(sysParaList.DEFLANG, PX_COMMONData);
            }
            catch
            {
                PX_COMMONData.ERRORCODE = "PXERR300";
                PX_COMMONData.ERRORMSG = string.Format(ms_dlgErrMsgFormat, PX_COMMONData.ERRORCODE);
            }
            return LangList;
        }

        /// <summary>
        ///  Login準備２
        /// </summary>
        /// <param name="BrowserType">起動元判定</param>
        /// <param name="SelectedLanguage">選択された言語</param>
        /// <returns></returns>
        public PX_PJ3CONFIG PrepareLogin(string BrowserType, string SelectedLanguage)
        {
            PX_COMMON PX_COMMONData = new PX_COMMON();
            string[] syspara = GetDBAccessInfoFromXML(ref PX_COMMONData);

            //* 事前準備(DBより情報取得) *//
            // PJ3SystemConfig(SysPara)の取得
            PX_PJ3CONFIG sysParaList = new PX_PJ3CONFIG();
            try
            {
                sysParaList = PXLIB.PXCL_com.GetPJ3Config(syspara, ref PX_COMMONData);
                if (PX_COMMONData.ERRORCODE != "")
                {
                    PX_COMMONData.ERRORMSG = string.Format(ms_dlgErrMsgFormat, PX_COMMONData.ERRORCODE, PX_COMMONData.ERRORCODE);
                    return new PX_PJ3CONFIG();
                }
                if (SelectedLanguage == null)
                {
                    SelectedLanguage = "1041";
                    // TEST
                }
                sysParaList = PXLIB.PXCL_com.GetLoginCaption(SelectedLanguage, sysParaList, PX_COMMONData);
                sysParaList.PAGETP = ms_DomainTP;
            }       
            catch
            {
                PX_COMMONData.ERRORCODE = "PXERR300";
                PX_COMMONData.ERRORMSG = string.Format(ms_dlgErrMsgFormat, PX_COMMONData.ERRORCODE);
                return new PX_PJ3CONFIG();
            }

            return sysParaList;
        }

        public PX_COMMON UserAuthenticationProcess(string userid, string pass)
        {
            // ログイン情報の取得

            //* ユーザー認証 *//
            PX_COMMON PX_COMMONData = new PX_COMMON();
            string[] syspara = GetDBAccessInfoFromXML(ref PX_COMMONData);

            // For Debug Only
            if (userid == null)
            {
                userid = "1600008"; pass = "kcom";
                //userid = "kaiya@primecast.co.jp";pass = "kcom";
            }
            List<string> passinfo = PXLIB.PXCL_com.GetUserKeyInfo(userid, ref PX_COMMONData);
            if (PX_COMMONData.ERRORCODE != "")
            {
                // エラーメッセージ生成
                return GetErrMessageForDialog("PASSERR-02", PX_COMMONData);
            }

            string nowdateOT = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
            string nowdate = DateTime.Today.ToString("yyyyMMdd");
            if (passinfo[0].Length==0)
            {
                // 通常のユーザー
                // 未入力時
                if (pass.Trim().Length == 0)
                {
                    if (passinfo[3] == "YES")
                    {
                        if (passinfo[4].CompareTo(nowdate) < 0)
                        {
                            // 有効期限切れ
                            return GetErrMessageForDialog("PASSERR - 01", PX_COMMONData);
                        }
                    }
                    else
                    {
                        return GetErrMessageForDialog("PASSERR - 01", PX_COMMONData);
                    }
                }
                else
                {
                    // 入力あり
                    if (pass == passinfo[4])
                    {
                        if (passinfo[5] != "00000000")
                        {
                            if (passinfo[5].CompareTo(nowdate) < 0)
                            {
                                // 期限切れ
                                return GetErrMessageForDialog("PASSERR - 02", PX_COMMONData);
                            }
                            else
                            {
                                if (passinfo[6] != "00000000")
                                {
                                    if (passinfo[6].CompareTo(nowdate) < 0)
                                    {
                                        // 警告なので処理続行
                                        PX_COMMONData = GetErrMessageForDialog("PASSALM - 01", PX_COMMONData);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        // パスワードUnMatch
                        return GetErrMessageForDialog("PASSERR - 01", PX_COMMONData);
                    }
                }
            }
            else
            {
                // *ワンタイムユーザー* //
                // パスワード未入力
                if (pass.Trim().Length == 0)
                {
                    return GetErrMessageForDialog("PASSERR - 01", PX_COMMONData);
                }
                else
                {
                    if (pass == passinfo[0])
                    {
                        // 有効期限チェック
                        if (passinfo[1] != "")
                        {
                            if (passinfo[1].CompareTo(nowdateOT) < 0)
                            {
                                // 期限切れ
                                return GetErrMessageForDialog("PASSERR - 02", PX_COMMONData);
                            }
                        }
                        if (passinfo[2] != "")
                        {
                            if (passinfo[2].CompareTo(nowdateOT) < 0)
                            {
                                // 警告期限切れ（警告なので処理続行）
                                PX_COMMONData = GetErrMessageForDialog("PASSALM-01", PX_COMMONData);
                            }
                        }
                    }
                    else
                    {
                        // パスワードUnMatch
                        return GetErrMessageForDialog("PASSERR - 01", PX_COMMONData);
                    }
                }
            }

            // LoginOK
            PX_COMMONData.DEFCOPTP = passinfo[7];
            switch (passinfo[7])
            {
                case "DEF":
                    PX_COMMONData.COPCD = passinfo[8];
                    break;
                case "END":
                    PX_COMMONData.COPCD = passinfo[9];
                    break;
                default:
                    // ToDo
                    break;
            }

            // ユーザーファイル(制御)情報読込み
            PX_COMMONData = PXLIB.PXCL_com.GetUserCTL(PX_COMMONData);
            // ユーザーファイル(オプション)情報読込みー今は不要
            //PXLIB.PXCL_com.GetUserOPT(PX_COMMONData);

            // システム情報(ユーザーDB情報)
            PX_COMMONData = PXLIB.PXCL_com.GetSystemParam(PX_COMMONData);

            // ユーザーDBへの接続確認
            PXLIB.PXCL_dba cnn = new PXLIB.PXCL_dba(PXLIB.PXCL_dba.ConnectionUser, PX_COMMONData);
            if (cnn.DBConect() != "")
            {
                return GetErrMessageForDialog("SYSERR-00", PX_COMMONData);
            }
            cnn.DBClose();

            int mcount = PXLIB.PXCL_com.GetCopMasterCount(PX_COMMONData);
            if (mcount==0)
            {
                return GetErrMessageForDialog("SYSERR-00", PX_COMMONData);
            }

            string status = PXLIB.PXCL_com.GetSystemProcessingStatus(PX_COMMONData);
            switch (status)
            {
                case "NOT":
                    return GetErrMessageForDialog("SYSERR-11", PX_COMMONData);
                case "MNT":
                    return GetErrMessageForDialog("SYSERR-12", PX_COMMONData);
                case "000":
                    break;
                default:
                    return GetErrMessageForDialog("SYSERR-10", PX_COMMONData);
            }
            PXLIB.PXCL_com.UpdateLoginStatus(PX_COMMONData);

            return PX_COMMONData;
        }

        private PX_COMMON GetErrMessageForDialog(string errcode, PX_COMMON pX_COMMON)
        {
            //ToDo: SNDMSGKBN部分入れ替え
            pX_COMMON.ERRORCODE = errcode;
            string SNDMSGKBN = "";
            if (errcode.Contains("SYSTEM"))
            {
                SNDMSGKBN = "SYSTEM";
            }
            else
            {
                SNDMSGKBN = "PXAS0000WP";
            }
            pX_COMMON.ERRORMSG = PXLIB.PXCL_com.GetDialogIndication(pX_COMMON.COPCD, SNDMSGKBN, pX_COMMON.ERRORCODE, pX_COMMON);
            return pX_COMMON;
        }
    }
}
