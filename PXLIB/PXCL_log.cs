using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using static PXLIB.PXCL_stc;

namespace PXLIB
{
    class PXCL_log
    {
        #region PxASlogCW_Declarations

        //ログレベル
        /// <summary>トレース</summary>
        public const string TRC = "TRC";
        /// <summary>情報</summary>
        public const string INF = "INF";
        /// <summary>警告</summary>
        public const string WAR = "WAR";
        /// <summary>エラー</summary>
        public const string ERR = "ERR";

        /// <summary>端末ログ</summary>
        public const string TERMINAL = "TERMINAL";
        /// <summary>ログイン</summary>
        public const string LOGIN = "LOGIN";
        /// <summary>ログアウト</summary>
        public const string LOGOUT = "LOGOUT";
        /// <summary>ハードコピー</summary>
        public const string HARDCOPY = "HARDCOPY";
        /// <summary>データ参照(検索)</summary>
        public const string SELECT = "SELECT";
        /// <summary>入力(新規登録)</summary>
        public const string INSART = "INSART";
        /// <summary>入力(修正)</summary>
        public const string UPDATE = "UPDATE";
        /// <summary>入力(削除)</summary>
        public const string DELETE = "DELETE";
        /// <summary>インポート(外部データ)</summary>
        public const string IMPORT = "IMPORT";
        /// <summary>エクスポート(外部データ)</summary>
        public const string EXPORT = "EXPORT";
        /// <summary>印刷・プレビュー</summary>
        public const string PRINT = "PRINT";
        /// <summary>一括更新</summary>
        public const string EXECUTE = "EXECUTE";
        /// <summary>画面遷移</summary>
        public const string START = "START";
        /// <summary>メール処理</summary>
        public const string MAIL = "MAIL";
        /// <summary>ログ(開発用)</summary>
        public const string LOG = "LOG";

        /// <summary>区切り文字</summary>
        protected static string separation = ",  ";

        #endregion

        /// <summary>
        /// ログ出力処理 - ログファイルに1行書き込む
        /// </summary>
        /// <param name="LogTp">ログレベル</param>
        /// <param name="ProcessId">処理ID</param>
        /// <param name="LogNm">ログタイトル</param>
        /// <param name="LogMemo">内容(コメント)</param>
        /// <param name="MethodInfo">クラス名、メソッド名情報</param>
        /// <param name="PX_COMMONData">共通データ(IP、URL取得)</param>
        public static void writeLog(string LogTp, string ProcessId, string LogNm, string LogMemo, MethodBase MethodInfo, PX_COMMON PX_COMMONData)
        {
            WrtLog4net(LogTp, ProcessId, LogNm, LogMemo, null, null, null, MethodInfo, PX_COMMONData);
        }

        /// <summary>
        /// ログ出力処理 - ログファイルに1行書き込む
        /// </summary>
        /// <param name="LogTp">ログレベル</param>
        /// <param name="ProcessId">処理ID</param>
        /// <param name="LogNm">ログタイトル</param>
        /// <param name="LogMemo">内容(コメント)</param>
        /// <param name="LogQryMemo">内容(Query等)</param>
        /// <param name="MethodInfo">クラス名、メソッド名情報</param>
        /// <param name="PX_COMMONData">共通データ(IP、URL取得)</param>
        public static void writeLog(string LogTp, string ProcessId, string LogNm, string LogMemo, string LogQryMemo, MethodBase MethodInfo, PX_COMMON PX_COMMONData)
        {
            WrtLog4net(LogTp, ProcessId, LogNm, LogMemo, LogQryMemo, null, null, MethodInfo, PX_COMMONData);
        }

        /// <summary>
        /// ログ出力処理 - ログファイルに1行書き込む
        /// </summary>
        /// <param name="LogTp">ログレベル</param>
        /// <param name="ProcessId">処理ID</param>
        /// <param name="LogNm">ログタイトル</param>
        /// <param name="LogMemo">内容(コメント)</param>
        /// <param name="LogQryMemo">内容(Query等)</param>
        /// <param name="LogSubMemo">内容(予備)</param>
        /// <param name="MethodInfo">クラス名、メソッド名情報</param>
        /// <param name="PX_COMMONData">共通データ(IP、URL取得)</param>
        private static void writeLog(string LogTp, string ProcessId, string LogNm, string LogMemo, string LogQryMemo, string LogSubMemo, MethodBase MethodInfo, PX_COMMON PX_COMMONData)
        {
            WrtLog4net(LogTp, ProcessId, LogNm, LogMemo, LogQryMemo, LogSubMemo, null, MethodInfo, PX_COMMONData);
        }

        /// <summary>
        /// 書き込み処理(log4net)
        /// </summary>
        /// <param name="LogTp">ログレベル</param>
        /// <param name="ProcessId">処理ID</param>
        /// <param name="LogNm">ログタイトル</param>
        /// <param name="LogMemo">内容(コメント)</param>
        /// <param name="LogQryMemo">内容(Query等)</param>
        /// <param name="LogSubMemo">内容(予備)</param>
        /// <param name="LogCmm">内容(備考)</param>
        /// <param name="MethodInfo">クラス名、メソッド名情報</param>
        /// <param name="PX_COMMONData">共通データ(IP、URL取得)</param>
        private static void WrtLog4net(string LogTp, string ProcessId, string LogNm, string LogMemo, string LogQryMemo, string LogSubMemo, string LogCmm, MethodBase MethodInfo, PX_COMMON PX_COMMONData)
        {
            try
            {
                StringBuilder LogMsg = new StringBuilder();
                log4net.ILog Lg4nLogger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                LogMsg.Append("processId=");
                LogMsg.Append("\"" + ProcessId + "\"");
                LogMsg.Append(separation);
                LogMsg.Append("clientIp=");
                LogMsg.Append("\"" + PX_COMMONData.CLIENTIP + "\"");
                LogMsg.Append(separation);
                LogMsg.Append("title=");
                LogMsg.Append("\"" + LogNm + "\"");
                LogMsg.Append(separation);
                LogMsg.Append("logInfo=");
                LogMsg.Append("\"" + LogMemo + "\"");
                LogMsg.Append(separation);
                LogMsg.Append("class=");
                LogMsg.Append("\"" + MethodInfo.DeclaringType.FullName + "\"");
                LogMsg.Append(separation);
                LogMsg.Append("method=");
                LogMsg.Append("\"" + MethodInfo.Name + "\"");
                LogMsg.Append(separation);
                LogMsg.Append("accessUrl=");
                LogMsg.Append("\"" + PX_COMMONData.ACCESSURL + "\"");
                LogMsg.Append(separation);
                LogMsg.Append("copCd=");
                LogMsg.Append("\"" + PX_COMMONData.COPCD + "\"");
                LogMsg.Append(separation);
                LogMsg.Append("userId=");
                LogMsg.Append("\"" + PX_COMMONData.USERID + "\"");
                LogMsg.Append(separation);
                LogMsg.Append("sysId=");
                LogMsg.Append("\"" + PX_COMMONData.SYSID + "\"");
                LogMsg.Append(separation);
                LogMsg.Append("userHostName=");
                LogMsg.Append("\"" + PX_COMMONData.CLIENTHNM + "\"");
                LogMsg.Append(separation);
                LogMsg.Append("logInfo(sub)=");
                LogMsg.Append((!string.IsNullOrEmpty(LogSubMemo)) ? "\"" + LogSubMemo + "\"" : "");
                LogMsg.Append(separation);
                LogMsg.Append("logInfo(remarks)=");
                LogMsg.Append((!string.IsNullOrEmpty(LogCmm)) ? "\"" + LogCmm + "\"" : null);
                LogMsg.Append(separation);
                LogMsg.Append((!string.IsNullOrEmpty(LogQryMemo)) ? "logInfo(detail)↓\r\n" + LogQryMemo : "");
                LogMsg.Append("\r\n");

                switch (LogTp)
                {
                    case ERR:
                        Lg4nLogger.Error(LogMsg.ToString());
                        break;
                    case WAR:
                        Lg4nLogger.Warn(LogMsg.ToString());
                        break;
                    default:
                        Lg4nLogger.Info(LogMsg.ToString());
                        break;
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// ログ出力処理 - ログファイルに1行書き込む
        /// </summary>
        /// <param name="LogTp">ログレベル</param>
        /// <param name="ProcessId">処理ID</param>
        /// <param name="LogNm">ログ名</param>
        /// <param name="LogMemo">内容(コメント)</param>
        /// <param name="MethodInfo">クラス名、メソッド名情報</param>
        /// <param name="PX_COMMONData">共通データ(IP、URL取得)</param>
        public static void writeDBLog(string LogTp, string ProcessId, string LogNm, string LogMemo, MethodBase MethodInfo, PX_COMMON PX_COMMONData)
        {
            InsDbLog(LogTp, ProcessId, LogNm, LogMemo, null, null, null, MethodInfo, PX_COMMONData);
        }

        /// <summary>
        /// ログ出力処理 - ログファイルに1行書き込む
        /// </summary>
        /// <param name="LogTp">ログレベル</param>
        /// <param name="ProcessId">処理ID</param>
        /// <param name="LogNm">ログ名</param>
        /// <param name="LogMemo">内容(コメント)</param>
        /// <param name="LogQryMemo">内容(Query等)</param>
        /// <param name="MethodInfo">クラス名、メソッド名情報</param>
        /// <param name="PX_COMMONData">共通データ(IP、URL取得)</param>
        public static void writeDBLog(string LogTp, string ProcessId, string LogNm, string LogMemo, string LogQryMemo, MethodBase MethodInfo, PX_COMMON PX_COMMONData)
        {
            InsDbLog(LogTp, ProcessId, LogNm, LogMemo, LogQryMemo, null, null, MethodInfo, PX_COMMONData);
        }

        /// <summary>
        /// ログ出力処理 - ログファイルに1行書き込む
        /// </summary>
        /// <param name="LogTp">ログレベル</param>
        /// <param name="ProcessId">処理ID</param>
        /// <param name="LogNm">ログ名</param>
        /// <param name="LogMemo">内容(コメント)</param>
        /// <param name="LogQryMemo">内容(Query等)</param>
        /// <param name="LogSubMemo">内容(予備)</param>
        /// <param name="MethodInfo">クラス名、メソッド名情報</param>
        /// <param name="PX_COMMONData">共通データ(IP、URL取得)</param>
        public static void writeDBLog(string LogTp, string ProcessId, string LogNm, string LogMemo, string LogQryMemo, string LogSubMemo, MethodBase MethodInfo, PX_COMMON PX_COMMONData)
        {
            InsDbLog(LogTp, ProcessId, LogNm, LogMemo, LogQryMemo, LogSubMemo, null, MethodInfo, PX_COMMONData);
        }

        /// <summary>
        /// ログ出力
        /// </summary>
        /// <param name="LogTp">ログレベル</param>
        /// <param name="ProcessId">処理ID</param>
        /// <param name="LogNm">ログ名</param>
        /// <param name="LogMemo">内容(コメント)</param>
        /// <param name="LogQryMemo">内容(Query等)</param>
        /// <param name="LogSubMemo">内容(予備)</param>
        /// <param name="LogCmm">内容(備考)</param>
        /// <param name="MethodInfo">クラス名、メソッド名情報</param>
        /// <param name="PX_COMMONData">共通データ(IP、URL取得)</param>
        public static void writeDBLog(string LogTp, string ProcessId, string LogNm, string LogMemo, string LogQryMemo, string LogSubMemo, string LogCmm, MethodBase MethodInfo, PX_COMMON PX_COMMONData)
        {
            InsDbLog(LogTp, ProcessId, LogNm, LogMemo, LogQryMemo, LogSubMemo, LogCmm, MethodInfo, PX_COMMONData);
        }

        /// <summary>
        /// ログ登録処理
        /// </summary>
        /// <param name="LogTp">ログレベル</param>
        /// <param name="ProcessId">処理ID</param>
        /// <param name="LogNm">ログタイトル</param>
        /// <param name="LogMemo">内容(コメント)</param>
        /// <param name="LogQryMemo">内容(Query等)</param>
        /// <param name="LogSubMemo">内容(予備)</param>
        /// <param name="LogCmm">内容(備考)</param>
        /// <param name="MethodInfo">クラス名、メソッド名情報</param>
        /// <param name="PX_COMMONData">共通データ(IP、URL取得)</param>
        private static void InsDbLog(string LogTp, string ProcessId, string LogNm, string LogMemo, string LogQryMemo, string LogSubMemo, string LogCmm, MethodBase MethodInfo, PX_COMMON PX_COMMONData)
        {
            int Res = 0;
            StringBuilder CmdTxt = new StringBuilder();
            //  【PRGID】パスが長いのでSPLIT
            string[] SplMethodInfo = MethodInfo.DeclaringType.FullName.Split('.');
            string PrgId = SplMethodInfo[SplMethodInfo.Length - 1];
            // ◆ＤＢの接続
            // 接続文字列の設定
            PXCL_dba DbAccess = new PXCL_dba(PXCL_dba.ConnectionSystem, PX_COMMONData);
            try
            {
                //◆データベースを開く
                DbAccess.DBConect();

                CmdTxt.AppendLine("INSERT INTO P3AS_LOG_SYS (");
                CmdTxt.AppendLine(" COPCD, LOGDATE, USERID, SYSID,");
                CmdTxt.AppendLine(" PRGID, PROCESSID, LOGNM, LOGMEMO,");
                CmdTxt.AppendLine(" LOGQRYMEMO, LOGSUBMEMO, LOGCMM, LOGMET,");
                CmdTxt.AppendLine(" LOGTP, ACCESSURL, CLIENTCP, CLIENTIP ");
                CmdTxt.AppendLine(") VALUES (");
                CmdTxt.AppendLine(" @COPCD, @DATE, @USERID, @SYSID,");
                CmdTxt.AppendLine(" @PRGID, @PROCESSID, @LOGNM, @LOGMEMO,");
                CmdTxt.AppendLine(" @LOGQRYMEMO, @LOGSUBMEMO, @LOGCMM, @LOGMET,");
                CmdTxt.AppendLine(" @LOGTP, @ACCESSURL, @CLIENTCP, @CLIENTIP ");
                CmdTxt.AppendLine(")");
                using (SqlCommand SqlCmd = new SqlCommand())
                {
                    SqlCmd.Parameters.Add("@COPCD", SqlDbType.Char).Value = PX_COMMONData.COPCD;
                    SqlCmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = PX_COMMONData.USERID;
                    SqlCmd.Parameters.Add("@SYSID", SqlDbType.VarChar).Value = PX_COMMONData.SYSID;
                    SqlCmd.Parameters.Add("@PRGID", SqlDbType.VarChar).Value = PrgId;
                    SqlCmd.Parameters.Add("@PROCESSID", SqlDbType.VarChar).Value = ProcessId;
                    SqlCmd.Parameters.Add("@LOGNM", SqlDbType.VarChar).Value = LogNm;
                    SqlCmd.Parameters.Add("@LOGMEMO", SqlDbType.VarChar).Value = LogMemo;
                    SqlCmd.Parameters.Add("@LOGQRYMEMO", SqlDbType.VarChar).Value = (!string.IsNullOrEmpty(LogQryMemo)) ? LogQryMemo : "";
                    SqlCmd.Parameters.Add("@LOGSUBMEMO", SqlDbType.VarChar).Value = (!string.IsNullOrEmpty(LogSubMemo)) ? LogSubMemo : "";
                    SqlCmd.Parameters.Add("@LOGCMM", SqlDbType.VarChar).Value = (!string.IsNullOrEmpty(LogCmm)) ? LogCmm : "";
                    SqlCmd.Parameters.Add("@LOGMET", SqlDbType.Char).Value = MethodInfo.Name;
                    SqlCmd.Parameters.Add("@LOGTP", SqlDbType.VarChar).Value = LogTp;
                    SqlCmd.Parameters.Add("@ACCESSURL", SqlDbType.VarChar).Value = PX_COMMONData.ACCESSURL;
                    SqlCmd.Parameters.Add("@CLIENTCP", SqlDbType.VarChar).Value = PX_COMMONData.CLIENTHNM;
                    SqlCmd.Parameters.Add("@CLIENTIP", SqlDbType.VarChar).Value = PX_COMMONData.CLIENTIP;
                    SqlCmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = DateTime.Now;
                    //◆SQL実行
                    Res = DbAccess.SQLInsertReturnParameter(CmdTxt.ToString(), SqlCmd);
                }
            }
            catch (Exception Exc)
            {
                string LogTitle = "ログ登録";
                string LogMsg = "エラー「" + Exc.Message + "」";
                writeLog(PXCL_log.ERR, "null", LogTitle, LogMsg, System.Reflection.MethodBase.GetCurrentMethod(), PX_COMMONData);
            }
            finally
            {
                //◆データベースの接続解除
                DbAccess.DBClose();
            }

        }
    }
}
