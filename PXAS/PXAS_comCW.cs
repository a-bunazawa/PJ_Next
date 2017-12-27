using System;
using System.Collections.Generic;
using System.Text;
using static PXAS.PXAS_stcCW;

namespace PXAS
{
    class PXAS_comCW
    {
        /// <summary>
        ///  プログラムの利用権限情報取得
        /// </summary>
        /// <param name="ProgramId">プログラムID</param>
        /// <param name="PxASuserValData">PxASuserVal</param>
        /// <returns>P3PROGRAMAUT プログラムの利用権限情報</returns>
        public static PxASprerogative GetP3PROGRAMAUT(string ProgramId, PxASuserVal PxASuserValData)
        {
            StringBuilder CmdTxt = new StringBuilder();
            PXAS_dbaCW DbAccess = new PXAS_dbaCW(PXAS_dbaCW.ConnectionSystem, PxASuserValData);
            PxASprerogative P3PROGRAMAUTData = new PxASprerogative();

            //  権限パラメータ区分が未設定の場合はすべてを許可
            P3PROGRAMAUTData.AUTCTLDSP = "YES";
            P3PROGRAMAUTData.AUTCTLINS = "YES";
            P3PROGRAMAUTData.AUTCTLMNT = "YES";
            P3PROGRAMAUTData.AUTCTLDEL = "YES";
            P3PROGRAMAUTData.AUTCTLPRC = "YES";
            P3PROGRAMAUTData.AUTCTLSPC = "YES";
            P3PROGRAMAUTData.AUTCTLSUB1 = "YES";
            P3PROGRAMAUTData.AUTCTLSUB2 = "YES";

            if (PxASuserValData.AUTKBN != "")
            {
                try
                {
                    //  データベースを開く
                    DbAccess.DBConect();

                    //  プログラムの利用権限情報取得SQL
                    CmdTxt.AppendLine("SELECT * FROM P3AS_PROGRAM_AUT ");
                    CmdTxt.AppendLine(" WHERE COPCD = @COPCD");
                    CmdTxt.AppendLine("   AND PRGID = @PRGID");
                    CmdTxt.AppendLine("   AND AUTKBN = @AUTKBN");
                    using (SqlCommand SqlCmd = new SqlCommand())
                    {
                        SqlCmd.Parameters.Add("@COPCD", SqlDbType.Char).Value = PxASuserValData.COPCD;
                        SqlCmd.Parameters.Add("@PRGID", SqlDbType.VarChar).Value = ProgramId;
                        SqlCmd.Parameters.Add("@AUTKBN", SqlDbType.VarChar).Value = PxASuserValData.AUTKBN;
                        //  ◆SELECT文実行
                        using (SqlDataReader Res = DbAccess.SQLSelectParameter(CmdTxt.ToString(), SqlCmd))
                        {
                            if (Res != null && Res.HasRows)
                            {
                                while (Res.Read())
                                {
                                    P3PROGRAMAUTData.AUTCTLDSP = Res["AUTCTLDSP"].ToString();
                                    P3PROGRAMAUTData.AUTCTLINS = Res["AUTCTLINS"].ToString();
                                    P3PROGRAMAUTData.AUTCTLMNT = Res["AUTCTLMNT"].ToString();
                                    P3PROGRAMAUTData.AUTCTLDEL = Res["AUTCTLDEL"].ToString();
                                    P3PROGRAMAUTData.AUTCTLPRC = Res["AUTCTLPRC"].ToString();
                                    P3PROGRAMAUTData.AUTCTLSPC = Res["AUTCTLSPC"].ToString();
                                    P3PROGRAMAUTData.AUTCTLSUB1 = Res["AUTCTLSUB1"].ToString();
                                    P3PROGRAMAUTData.AUTCTLSUB2 = Res["AUTCTLSUB2"].ToString();
                                }
                            }
                        }
                    }
                }
                catch (Exception Exc)
                {
                    string LogTitle = "プログラムの利用権限情報取得";
                    string LogMsg = "エラー「" + Exc.Message + "」";
                    PXAS_logCW.writeLog(PXAS_logCW.ERR, PXAS_logCW.SELECT, LogTitle, LogMsg, System.Reflection.MethodBase.GetCurrentMethod(), PxASuserValData);
                }
                finally
                {
                    //データベースの接続解除
                    DbAccess.DBClose();
                }
            }

            return P3PROGRAMAUTData;
        }

    }
}
