using System;
using System.Text;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Diagnostics;
using static PXAS.PXAS_stcCW;

namespace PXAS
{
    class PXAS_dbaCW
    {
        #region PxASdbaCW_Declarations

        /// <summary>接続先DB：システム</summary>
        public const int ConnectionSystem = 0;
        /// <summary>接続先DB：ユーザ</summary>
        public const int ConnectionUser = 1;
        /// <summary>接続フラグ (True = 接続/False = 切断)</summary>
        private bool ConnectFlg;
        /// <summary>コネクション</summary>
        private SqlConnection SqlCon;
        /// <summary>トランザクション</summary>
        private SqlTransaction SqlTran;
        /// <summary>共通データ</summary>
        private PxASuserVal PxASuserValData;
        /// <summary>接続文</summary>
        private string ConnectString = "";

        #endregion

        /// <summary>
        /// データベースコネクション　設定・取得
        /// </summary>
        public SqlConnection SQLConnection
        {
            get { return SqlCon; }
            set { SqlCon = value; }
        }

        /// <summary>
        /// データベーストランザクション　設定・取得
        /// </summary>
        public SqlTransaction SQLTransaction
        {
            get { return SqlTran; }
            set { SqlTran = value; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="Connect">接続先DB</param>
        /// <param name="PxASuserValData">PxASuserValのモデル</param>
        public PXAS_dbaCW(int Connect, PxASuserVal PxASuserValData)
        {
            if (SqlCon == null) { SqlCon = new SqlConnection(); }
            try
            {
                // 接続文字列の設定
                StringBuilder DbConnection = new StringBuilder();
                if (Connect == ConnectionUser)
                {
                    DbConnection.Append("Data Source=");
                    DbConnection.Append((!string.IsNullOrEmpty(PxASuserValData.USERDBSVRIP)) ? PxASuserValData.USERDBSVRIP : PxASuserValData.USERDBSVRNM);
                    DbConnection.Append(";Initial Catalog=");
                    DbConnection.Append(PxASuserValData.USERDBNM);
                    DbConnection.Append(";User ID=");
                    DbConnection.Append(PxASuserValData.USERDBSVRUR);
                    DbConnection.Append(";Password=");
                    DbConnection.Append(P3COMMONCW.Decrypt(PxASuserValData.USERDBSVRPW));
                    DbConnection.Append("; Max Pool Size=100;");
                    ConnectString = DbConnection.ToString();
                }
                else
                {
                    DbConnection.Append("Data Source=");
                    DbConnection.Append((!string.IsNullOrEmpty(PxASuserValData.SYSDBSVRIP)) ? PxASuserValData.SYSDBSVRIP : PxASuserValData.SYSDBSVRNM);
                    DbConnection.Append(";Initial Catalog=");
                    DbConnection.Append(PxASuserValData.SYSDBNM);
                    DbConnection.Append(";User ID=");
                    DbConnection.Append(PxASuserValData.SYSDBSVRUR);
                    DbConnection.Append(";Password=");
                    DbConnection.Append(P3COMMONCW.Decrypt(PxASuserValData.SYSDBSVRPW));
                    DbConnection.Append("; Max Pool Size=100;");
                    ConnectString = DbConnection.ToString();
                }
                this.PxASuserValData = PxASuserValData;
            }
            catch (Exception Exc)
            {
                throw Exc;
            }
        }

        /// <summary>
        /// コネクションオープン処理
        /// </summary>
        /// <returns>エラーの場合エラーメッセージを返す</returns>
        public string DBConect()
        {
            string ErrMsg = "";

            try
            {
                if (SqlCon.State == ConnectionState.Closed)
                {
                    //DB接続用コネクション
                    SqlCon = new SqlConnection(ConnectString);
                    SqlCon.Open();
                    ConnectFlg = true;
                }
            }
            catch (Exception Exc)
            {
                ErrMsg = "データベース接続「" + Exc.Message + "」";
                StackFrame callerFrame = new StackFrame(1);
                PXAS_logCW.writeLog(PXAS_logCW.ERR, "DB", "DBアクセスエラー", ErrMsg, ConnectString, callerFrame.GetMethod(), PxASuserValData);
            }

            return ErrMsg;
        }

        /// <summary>
        /// データベース切断
        /// </summary>
        /// <returns>エラーの場合エラーメッセージを返す</returns>
        public string DBClose()
        {
            string ErrMsg = "";

            try
            {
                if (ConnectFlg)
                {
                    SqlCon.Close();
                }
                else
                {
                    if (SqlCon.State != ConnectionState.Closed)
                    {
                        SqlCon.Close();
                    }
                }

                ConnectFlg = false;
            }
            catch (Exception Exc)
            {
                ErrMsg = "データベース切断「" + Exc.Message + "」";
                StackFrame CallerFrame = new StackFrame(1);
                PXAS_logCW.writeLog(PXAS_logCW.ERR, "DB", "DBアクセスエラー", ErrMsg, CallerFrame.GetMethod(), PxASuserValData);
            }

            return ErrMsg;
        }

        /// <summary>
        /// SELECT文実行(Return SqlDataReader)
        /// </summary>
        /// <param name="CmdTxt">SQLステートメント</param>
        /// <returns>取得したデータのレコードセット</returns>
        public SqlDataReader SQLSelect(string CmdTxt)
        {
            SqlDataReader Res = null;
            try
            {
                using (SqlCommand SqlCmd = new SqlCommand())
                {
                    SqlCmd.Connection = SqlCon;
                    SqlCmd.Transaction = SqlTran;
                    SqlCmd.CommandText = CmdTxt;
                    SqlCmd.CommandTimeout = 30000;
                    Res = SqlCmd.ExecuteReader();
                }
            }
            catch (Exception Exc)
            {
                string ErrMsg = "データ取得「" + Exc.Message + "」";
                StackFrame CallerFrame = new StackFrame(1);
                PXAS_logCW.writeLog(PXAS_logCW.ERR, PXAS_logCW.SELECT, "DBデータ取得エラー", ErrMsg, CmdTxt, CallerFrame.GetMethod(), PxASuserValData);
                return null;
            }

            return Res;
        }

        /// <summary>
        /// SELECT文実行(パラメータ指定)
        /// </summary>
        /// <param name="CmdTxt">SQLステートメント</param>
        /// <param name="CmdPrm">SQLパラメータ</param>
        /// <returns>実行結果</returns>
        public SqlDataReader SQLSelectParameter(string CmdTxt, SqlCommand CmdPrm)
        {
            SqlDataReader Res = null;
            StringBuilder CmdTxtLog = new StringBuilder();

            try
            {
                CmdTxtLog.Append(CmdTxt);
                using (SqlCommand SqlCmd = new SqlCommand())
                {
                    for (int i = 0; i < CmdPrm.Parameters.Count; i++)
                    {
                        SqlCmd.Parameters.AddWithValue(CmdPrm.Parameters[i].ParameterName.ToString(), CmdPrm.Parameters[i].DbType);
                        SqlCmd.Parameters[i].Value = CmdPrm.Parameters[i].Value;
                        CmdTxtLog.Append("、");
                        CmdTxtLog.Append(CmdPrm.Parameters[i].ParameterName.ToString());
                        CmdTxtLog.Append("：");
                        SqlCmd.Parameters[i].Direction = CmdPrm.Parameters[i].Direction;
                        CmdTxtLog.Append(CmdPrm.Parameters[i].Value);
                    }

                    SqlCmd.Connection = SqlCon;
                    SqlCmd.Transaction = SqlTran;
                    SqlCmd.CommandText = CmdTxt;
                    SqlCmd.CommandTimeout = 30000;
                    Res = SqlCmd.ExecuteReader();
                }
            }
            catch (Exception Exc)
            {
                string ErrMsg = "データ取得「" + Exc.Message + "」";
                StackFrame CallerFrame = new StackFrame(1);
                PXAS_logCW.writeLog(PXAS_logCW.ERR, PXAS_logCW.SELECT, "DBデータ取得エラー", ErrMsg, CmdTxt, CallerFrame.GetMethod(), PxASuserValData);

                return null;
            }

            return Res;
        }

        /// <summary>
        /// クエリ結果をデータテーブルにて取得
        /// </summary>
        /// <param name="CmdTxt">SQLステートメント</param>
        /// <returns>データテーブル</returns>
        public DataTable SQLDataTable(string CmdTxt)
        {
            DataTable Res = new DataTable();

            try
            {
                SqlCon = new SqlConnection(ConnectString);
                using (SqlDataAdapter SqlDataAdapter = new SqlDataAdapter(CmdTxt, SqlCon))
                {
                    SqlDataAdapter.Fill(Res);
                }
            }
            catch (Exception Exc)
            {
                string ErrMsg = "データ取得「" + Exc.Message + "」";
                StackFrame CallerFrame = new StackFrame(1);
                PXAS_logCW.writeLog(PXAS_logCW.ERR, PXAS_logCW.SELECT, "DBデータ取得エラー", ErrMsg, CmdTxt, CallerFrame.GetMethod(), PxASuserValData);

                return null;
            }

            return Res;
        }

        /// <summary>
        /// INSERT文実行
        /// </summary>
        /// <param name="CmdTxt">INSERTステートメント</param>
        /// <returns>0以上: 成功　-1:エラー</returns>
        public int SQLInsert(string CmdTxt)
        {
            int Res = -1;

            try
            {
                using (SqlCommand SqlCmd = new SqlCommand())
                {
                    SqlCmd.Connection = SqlCon;
                    SqlCmd.Transaction = SqlTran;
                    SqlCmd.CommandText = CmdTxt;
                    Res = SqlCmd.ExecuteNonQuery();
                }
            }
            catch (Exception Exc)
            {
                Res = -1;
                string ErrMsg = "データ登録「" + Exc.Message + "」";
                StackFrame CallerFrame = new StackFrame(1);
                PXAS_logCW.writeLog(PXAS_logCW.ERR, PXAS_logCW.SELECT, "DBデータ登録エラー", ErrMsg, CmdTxt, CallerFrame.GetMethod(), PxASuserValData);
            }

            return Res;
        }

        /// <summary>
        /// INSERT文実行(パラメータ指定)
        /// </summary>
        /// <param name="CmdTxt">INSERTステートメント</param>
        /// <param name="CmdParam">SQLパラメータ</param>
        /// <returns>新規ID(エラー時: -1)</returns>
        public int SQLInsertReturnParameter(string CmdTxt, SqlCommand CmdPrm)
        {
            int Res = -1;
            StringBuilder CmdTxtLog = new StringBuilder();

            try
            {
                CmdTxtLog.Append(CmdTxt);
                using (SqlCommand SqlCmd = new SqlCommand())
                {
                    for (int i = 0; i < CmdPrm.Parameters.Count; i++)
                    {
                        SqlCmd.Parameters.AddWithValue(CmdPrm.Parameters[i].ParameterName.ToString(), CmdPrm.Parameters[i].DbType);
                        SqlCmd.Parameters[i].Value = CmdPrm.Parameters[i].Value;
                        CmdTxtLog.Append("、");
                        CmdTxtLog.Append(CmdPrm.Parameters[i].ParameterName.ToString());
                        CmdTxtLog.Append("：");
                        SqlCmd.Parameters[i].Direction = CmdPrm.Parameters[i].Direction;
                        CmdTxtLog.Append(CmdPrm.Parameters[i].Value);
                    }
                    SqlCmd.Connection = SqlCon;
                    SqlCmd.Transaction = SqlTran;
                    SqlCmd.CommandText = CmdTxt + "; SELECT SCOPE_IDENTITY();";
                    int.TryParse(SqlCmd.ExecuteScalar().ToString(), out Res);
                }
            }
            catch (Exception Exc)
            {
                Res = -1;
                string ErrMsg = "データ登録「" + Exc.Message + "」";
                StackFrame CallerFrame = new StackFrame(1);
                PXAS_logCW.writeLog(PXAS_logCW.ERR, PXAS_logCW.INSART, "DBデータ登録エラー", ErrMsg, CmdTxt, CallerFrame.GetMethod(), PxASuserValData);
            }

            return Res;
        }

        /// <summary>
        /// UPDATE文実行
        /// </summary>
        /// <param name="CmdTxt">UPDATEステートメント</param>
        /// <returns>更新行数(エラー時: -1)</returns>
        public int SQLUpdate(string CmdTxt)
        {
            int Res = -1;

            try
            {
                using (SqlCommand SqlCmd = new SqlCommand())
                {
                    SqlCmd.Connection = SqlCon;
                    SqlCmd.Transaction = SqlTran;
                    SqlCmd.CommandText = CmdTxt;
                    SqlCmd.CommandTimeout = 30000;
                    Res = SqlCmd.ExecuteNonQuery();
                }
            }
            catch (Exception Exc)
            {
                Res = -1;
                string ErrMsg = "データ更新「" + Exc.Message + "」";
                StackFrame CallerFrame = new StackFrame(1);
                PXAS_logCW.writeLog(PXAS_logCW.ERR, PXAS_logCW.UPDATE, "DBデータ更新エラー", ErrMsg, CmdTxt, CallerFrame.GetMethod(), PxASuserValData);
            }

            return Res;
        }

        /// <summary>
        /// UPDATE文実行(パラメータ指定)
        /// </summary>
        /// <param name="CmdTxt">UPDATEステートメント</param>
        /// <param name="CmdPrm">SQLパラメータ</param>
        /// <returns>更新行数(エラー時: -1)</returns>
        public int SQLUpdateParameter(string CmdTxt, SqlCommand CmdPrm)
        {
            int Res = -1;
            StringBuilder CmdTxtLog = new StringBuilder();

            try
            {
                CmdTxtLog.Append(CmdTxt);
                using (SqlCommand SqlCmd = new SqlCommand())
                {
                    for (int i = 0; i < CmdPrm.Parameters.Count; i++)
                    {
                        SqlCmd.Parameters.AddWithValue(CmdPrm.Parameters[i].ParameterName.ToString(), CmdPrm.Parameters[i].DbType);
                        SqlCmd.Parameters[i].Value = CmdPrm.Parameters[i].Value;
                        CmdTxtLog.Append("、");
                        CmdTxtLog.Append(CmdPrm.Parameters[i].ParameterName.ToString());
                        CmdTxtLog.Append("：");
                        SqlCmd.Parameters[i].Direction = CmdPrm.Parameters[i].Direction;
                        CmdTxtLog.Append(CmdPrm.Parameters[i].Value);
                    }

                    SqlCmd.Connection = SqlCon;
                    SqlCmd.Transaction = SqlTran;
                    SqlCmd.CommandText = CmdTxt;
                    SqlCmd.CommandTimeout = 30000;
                    Res = SqlCmd.ExecuteNonQuery();
                }
            }
            catch (Exception Exc)
            {
                Res = -1;
                string ErrMsg = "データ更新「" + Exc.Message + "」";
                StackFrame CallerFrame = new StackFrame(1);
                PXAS_logCW.writeLog(PXAS_logCW.ERR, PXAS_logCW.SELECT, "DBデータ更新エラー", ErrMsg, CmdTxt, CallerFrame.GetMethod(), PxASuserValData);
            }

            return Res;
        }

        /// <summary>
        /// DELETE文実行
        /// </summary>
        /// <param name="CmdTxt">DELETEステートメント</param>
        /// <returns>0以上: 成功　-1:エラー</returns>
        public int SQLDelete(string CmdTxt)
        {
            int Res = -1;

            try
            {
                using (SqlCommand SqlCmd = new SqlCommand())
                {
                    SqlCmd.Connection = SqlCon;
                    SqlCmd.Transaction = SqlTran;
                    SqlCmd.CommandText = CmdTxt;
                    Res = SqlCmd.ExecuteNonQuery();
                }
            }
            catch (Exception Exc)
            {
                Res = -1;
                string ErrMsg = "データ削除「" + Exc.Message + "」";
                StackFrame CallerFrame = new StackFrame(1);
                PXAS_logCW.writeLog(PXAS_logCW.ERR, PXAS_logCW.SELECT, "DBデータ削除エラー", ErrMsg, CmdTxt, CallerFrame.GetMethod(), PxASuserValData);
            }

            return Res;
        }

        /// <summary>
        /// DELETE文実行(パラメータ指定)
        /// </summary>
        /// <param name="CmdTxt">DELETEステートメント</param>
        /// <param name="CmdPrm">SQLパラメータ</param>
        /// <returns>0以上: 成功　-1:エラー</returns>
        public int SQLDeleteParameter(string CmdTxt, SqlCommand CmdPrm)
        {
            int Res = -1;
            StringBuilder CmdTxtLog = new StringBuilder();

            try
            {
                CmdTxtLog.Append(CmdTxt);
                using (SqlCommand SqlCmd = new SqlCommand())
                {
                    for (int i = 0; i < CmdPrm.Parameters.Count; i++)
                    {
                        SqlCmd.Parameters.AddWithValue(CmdPrm.Parameters[i].ParameterName.ToString(), CmdPrm.Parameters[i].DbType);
                        SqlCmd.Parameters[i].Value = CmdPrm.Parameters[i].Value;
                        CmdTxtLog.Append("、");
                        CmdTxtLog.Append(CmdPrm.Parameters[i].ParameterName.ToString());
                        CmdTxtLog.Append("：");
                        SqlCmd.Parameters[i].Direction = CmdPrm.Parameters[i].Direction;
                        CmdTxtLog.Append(CmdPrm.Parameters[i].Value);
                    }

                    SqlCmd.Connection = SqlCon;
                    SqlCmd.Transaction = SqlTran;
                    SqlCmd.CommandText = CmdTxt;
                    Res = SqlCmd.ExecuteNonQuery();
                }
            }
            catch (Exception Exc)
            {
                Res = -1;
                string ErrMsg = "データ削除「" + Exc.Message + "」";
                StackFrame CallerFrame = new StackFrame(1);
                PXAS_logCW.writeLog(PXAS_logCW.ERR, PXAS_logCW.DELETE, "DBデータ削除エラー", ErrMsg, CmdTxt, CallerFrame.GetMethod(), PxASuserValData);
            }

            return Res;
        }

        /// <summary>
        /// トランザクション開始処理・コミット処理・ロールバック処理(Return bool型)
        /// </summary>
        /// <param name="TrsMode">処理モード(1:トランザクション開始  2: コミット 3: ロールバック)</param>
        /// <returns>TRUE:処理成功 FALSE:処理失敗</returns>
        public bool Tran(int TrsMode)
        {
            bool Res = true;

            try
            {
                switch (TrsMode)
                {
                    case 1:
                        SqlTran = SqlCon.BeginTransaction();
                        break;
                    case 2:
                        SqlTran.Commit();
                        SqlTran = null;
                        break;
                    case 3:
                        SqlTran.Rollback();
                        SqlTran = null;
                        break;
                }
            }
            catch (Exception Exc)
            {
                Res = false;
                string ErrMsg = "";
                switch (TrsMode)
                {
                    case 1:
                        ErrMsg = "トランザクション「" + Exc.Message + "」";
                        break;
                    case 2:
                        ErrMsg = "コミット「" + Exc.Message + "」";
                        break;
                    case 3:
                        ErrMsg = "ロールバック「" + Exc.Message + "」";
                        break;
                }
                StackFrame CallerFrame = new StackFrame(1);
                PXAS_logCW.writeLog(PXAS_logCW.ERR, "DB", "DBデータトランザクションエラー", ErrMsg, CallerFrame.GetMethod(), PxASuserValData);
            }

            return Res;
        }

        /// <summary>
        /// ストアド実行処理
        /// </summary>
        /// <param name="CmdTxt">ストアド名</param>
        /// <param name="CmdPrm">SQLパラメータ</param>
        /// <returns>実行行数(null: エラー)</returns>
        public SqlCommand ExecStoredProcedureParameter(string CmdTxt, SqlCommand CmdPrm)
        {
            SqlCommand SqlCmd = new SqlCommand();
            StringBuilder CmdTxtLog = new StringBuilder();

            try
            {
                CmdTxtLog.Append(CmdTxt);

                SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlCmd.CommandText = CmdTxt;
                for (int i = 0; i < CmdPrm.Parameters.Count; i++)
                {
                    SqlCmd.Parameters.AddWithValue(CmdPrm.Parameters[i].ParameterName.ToString(), CmdPrm.Parameters[i].DbType);
                    SqlCmd.Parameters[i].Value = CmdPrm.Parameters[i].Value;
                    CmdTxtLog.Append("、");
                    CmdTxtLog.Append(CmdPrm.Parameters[i].ParameterName.ToString());
                    CmdTxtLog.Append("：");
                    SqlCmd.Parameters[i].Direction = CmdPrm.Parameters[i].Direction;
                    SqlCmd.Parameters[i].Size = CmdPrm.Parameters[i].Size;
                    CmdTxtLog.Append(CmdPrm.Parameters[i].Value);
                }

                SqlCmd.Connection = SqlCon;
                SqlCmd.Transaction = SqlTran;
                int Res = SqlCmd.ExecuteNonQuery();
            }
            catch (Exception Exc)
            {
                string ErrMsg = "ストアド処理「" + Exc.Message + "」";
                StackFrame CallerFrame = new StackFrame(1);
                PXAS_logCW.writeLog(PXAS_logCW.ERR, "DB", "DBストアドエラー", ErrMsg, ErrMsg.ToString(), CallerFrame.GetMethod(), PxASuserValData);

                return null;
            }

            return SqlCmd;
        }

    }
}
