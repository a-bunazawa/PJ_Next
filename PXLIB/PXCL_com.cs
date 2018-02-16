using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using static PXLIB.PXCL_stc;
using System.Security.Cryptography;
using System.IO;

namespace PXLIB
{
    public class PXCL_com
    {
        /// <summary>
        /// ダイアログ表示情報設定
        /// </summary>
        /// <param name="CopCd">会社コード</param>
        /// <param name="SndMsgKbn">メッセージ区分</param>
        /// <param name="SndMsgNo">メッセージ番号</param>
        /// <param name="P3VALUECWData">P3VALUECWのモデルデータ</param>
        /// <returns>ダイアログ情報</returns>
        public static string GetDialogIndication(string CopCd, string SndMsgKbn, string SndMsgNo, PX_COMMON PX_COMMONData)
        {
            string SplitString = ",";
            string DialogIndication = "";
            string Title = "";
            string ShowMsg = "";
            int DefaultButton = -1;
            StringBuilder Button = new StringBuilder();
            PXCL_dba DbAccess = new PXCL_dba(PXCL_dba.ConnectionSystem, PX_COMMONData);
            if (string.IsNullOrEmpty(CopCd)) { CopCd = "00000"; }
            int Cnt = 0;
            StringBuilder CmdTxt = new StringBuilder();
            try
            {
                //  データベースを開く
                DbAccess.DBConect();

                //  SELECT文作成
                CmdTxt.AppendLine("SELECT * FROM P3AS_PROGRAM_MSG");
                CmdTxt.AppendLine("WHERE COPCD = @COPCD");
                CmdTxt.AppendLine("  AND SNDMSGKBN = @SNDMSGKBN");
                CmdTxt.AppendLine("  AND SNDMSGNO = @SNDMSGNO");
                using (SqlCommand SqlCmd = new SqlCommand())
                {
                    //◆条件の設定
                    SqlCmd.Parameters.Add("@COPCD", SqlDbType.VarChar).Value = CopCd;
                    SqlCmd.Parameters.Add("@SNDMSGKBN", SqlDbType.VarChar).Value = SndMsgKbn;
                    SqlCmd.Parameters.Add("@SNDMSGNO", SqlDbType.VarChar).Value = SndMsgNo;
                    //◆SQL実行
                    using (SqlDataReader Res = DbAccess.SQLSelectParameter(CmdTxt.ToString(), SqlCmd))
                    {
                        if (Res.HasRows)
                        {
                            while (Res.Read())
                            {
                                Title = Res["SNDMSG1"].ToString();
                                ShowMsg = Res["SNDMSG2"].ToString();
                                if (Res["SNDMSG3"].ToString() != "")
                                {
                                    ShowMsg += "\n" + Res["SNDMSG3"].ToString();
                                }
                                DefaultButton = int.Parse(Res["ANSNMDEF"].ToString()) - 1;

                                if (Res["ANSNM1"].ToString() != "" && Res["ANSVALUE1"].ToString() != "")
                                {
                                    Cnt++;
                                    Button.Append(Res["ANSNM1"].ToString());
                                    Button.Append(SplitString);
                                    Button.Append(SndMsgNo);
                                    Button.Append("_");
                                    Button.Append(Res["ANSVALUE1"].ToString());
                                }
                                if (Res["ANSNM2"].ToString() != "" && Res["ANSVALUE2"].ToString() != "")
                                {
                                    Cnt++;
                                    if (Button.ToString() != "") { Button.Append(SplitString); }
                                    Button.Append(Res["ANSNM2"].ToString());
                                    Button.Append(SplitString);
                                    Button.Append(SndMsgNo);
                                    Button.Append("_");
                                    Button.Append(Res["ANSVALUE2"].ToString());
                                }
                                if (Res["ANSNM3"].ToString() != "" && Res["ANSVALUE3"].ToString() != "")
                                {
                                    Cnt++;
                                    if (Button.ToString() != "") { Button.Append(SplitString); }
                                    Button.Append(Res["ANSNM3"].ToString());
                                    Button.Append(SplitString);
                                    Button.Append(SndMsgNo);
                                    Button.Append("_");
                                    Button.Append(Res["ANSVALUE3"].ToString());
                                }
                                if (Res["ANSNM4"].ToString() != "" && Res["ANSVALUE4"].ToString() != "")
                                {
                                    Cnt++;
                                    if (Button.ToString() != "") { Button.Append(SplitString); }
                                    Button.Append(Res["ANSNM4"].ToString());
                                    Button.Append(SplitString);
                                    Button.Append(SndMsgNo);
                                    Button.Append("_");
                                    Button.Append(Res["ANSVALUE4"].ToString());
                                }
                                if (Res["ANSNM5"].ToString() != "" && Res["ANSVALUE5"].ToString() != "")
                                {
                                    Cnt++;
                                    if (Button.ToString() != "") { Button.Append(SplitString); }
                                    Button.Append(Res["ANSNM5"].ToString());
                                    Button.Append(SplitString);
                                    Button.Append(SndMsgNo);
                                    Button.Append("_");
                                    Button.Append(Res["ANSVALUE5"].ToString());
                                }
                            }
                            if (DefaultButton == -1) { DefaultButton = 0; }
                            DialogIndication = Cnt + SplitString + DefaultButton + SplitString + Title + SplitString + ShowMsg + SplitString + Button.ToString();
                        }
                        else
                        {
                            Title = "エラー";
                            ShowMsg = "環境設定に誤りがあります:" + CopCd + "、" + SndMsgKbn + "、" + SndMsgNo;
                            Button.Append("OK");
                            Button.Append(SplitString);
                            Button.Append(SndMsgNo);
                            Button.Append("_");
                            Button.Append("00");
                            DialogIndication = "1" + SplitString + "0" + SplitString + Title + SplitString + ShowMsg + SplitString + Button.ToString();
                        }
                    }
                }
            }
            catch (Exception Exc)
            {
                string LogTitle = "ダイアログ表示情報取得";
                string LogMsg = "エラー「" + Exc.Message + "」";
                System.Diagnostics.StackFrame callerFrame = new System.Diagnostics.StackFrame(1);
                PXCL_log.writeLog(PXCL_log.ERR, PXCL_log.SELECT, LogTitle, LogMsg, System.Reflection.MethodBase.GetCurrentMethod(), PX_COMMONData);

                Title = "エラー";
                ShowMsg = "環境設定に誤りがあります:" + CopCd + "、" + SndMsgKbn + "、" + SndMsgNo;
                Button.Append("OK");
                Button.Append(SplitString);
                Button.Append(SndMsgNo);
                Button.Append("_");
                Button.Append("00");
                DialogIndication = "1" + SplitString + "0" + SplitString + Title + SplitString + ShowMsg + SplitString + Button.ToString();
            }
            finally
            {
                //データベースの接続解除
                DbAccess.DBClose();
            }

            return DialogIndication;
        }
        public static string GetDialogIndication(string CopCd, PX_COMMON PX_COMMONData, string title, string mag, string SplitString)
        {
            string errmsg = GetDialogIndication(CopCd, "SYSTEM", "RUNERR-99", PX_COMMONData);
            string[] tmp = errmsg.Split(new string[] { SplitString }, StringSplitOptions.None);
            if (tmp.Length >= 5)
            {
                tmp[2] = title;
                tmp[3] = mag;
                errmsg = string.Join(SplitString, tmp);
            }
            return errmsg;
        }

        /// <summary>
        ///  プログラムの利用権限情報取得
        /// </summary>
        /// <param name="ProgramId">プログラムID</param>
        /// <param name="PX_COMMONData">PX_COMMON</param>
        /// <returns>P3PROGRAMAUT プログラムの利用権限情報</returns>
        public static PX_PROGRAM_AUT GetP3PROGRAMAUT(string ProgramId, PX_COMMON PX_COMMONData)
        {
            StringBuilder CmdTxt = new StringBuilder();
            PXCL_dba DbAccess = new PXCL_dba(PXCL_dba.ConnectionSystem, PX_COMMONData);
            PX_PROGRAM_AUT P3PROGRAMAUTData = new PX_PROGRAM_AUT();

            //  権限パラメータ区分が未設定の場合はすべてを許可
            P3PROGRAMAUTData.AUTCTLDSP = "YES";
            P3PROGRAMAUTData.AUTCTLINS = "YES";
            P3PROGRAMAUTData.AUTCTLMNT = "YES";
            P3PROGRAMAUTData.AUTCTLDEL = "YES";
            P3PROGRAMAUTData.AUTCTLPRC = "YES";
            P3PROGRAMAUTData.AUTCTLSPC = "YES";
            P3PROGRAMAUTData.AUTCTLSUB1 = "YES";
            P3PROGRAMAUTData.AUTCTLSUB2 = "YES";

            if (PX_COMMONData.AUTKBN != "")
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
                        SqlCmd.Parameters.Add("@COPCD", SqlDbType.Char).Value = PX_COMMONData.COPCD;
                        SqlCmd.Parameters.Add("@PRGID", SqlDbType.VarChar).Value = ProgramId;
                        SqlCmd.Parameters.Add("@AUTKBN", SqlDbType.VarChar).Value = PX_COMMONData.AUTKBN;
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
                    PXCL_log.writeLog(PXCL_log.ERR, PXCL_log.SELECT, LogTitle, LogMsg, System.Reflection.MethodBase.GetCurrentMethod(), PX_COMMONData);
                }
                finally
                {
                    //データベースの接続解除
                    DbAccess.DBClose();
                }
            }

            return P3PROGRAMAUTData;
        }

        /// <summary>
        /// 暗号化処理
        /// </summary>
        /// <param name="Value">暗号化を行う文字列</param>
        /// <returns>暗号化した文字列</returns>
        public static string Encrypt(string Value)
        {
            string CipherValue = "";

            if (!string.IsNullOrEmpty(Value))
            {
                try
                {
                    //  暗号用のキー情報を設定
                    byte[] EcdUtf8ByteKey = Encoding.UTF8.GetBytes(PXCL_fix.EncryptKey);
                    byte[] EcdUtf8ByteIv = Encoding.UTF8.GetBytes(PXCL_fix.EncryptIV);
                    byte[] EcdUtf8ByteValue = Encoding.UTF8.GetBytes(Value);
                    //  暗号化用の3DESクラス生成
                    TripleDESCryptoServiceProvider TripleDes = new TripleDESCryptoServiceProvider();

                    using (MemoryStream MemSt = new MemoryStream())
                    {
                        using (CryptoStream CryptoStr = new CryptoStream(MemSt, TripleDes.CreateEncryptor(EcdUtf8ByteKey, EcdUtf8ByteIv), CryptoStreamMode.Write))
                        {
                            //  ◆暗号化処理
                            CryptoStr.Write(EcdUtf8ByteValue, 0, EcdUtf8ByteValue.Length);
                        }
                        byte[] EncryptedData = MemSt.ToArray();
                        CipherValue = byte2HexString(EncryptedData);
                    }

                }
                catch (Exception Exc)
                {

                }
            }

            return CipherValue;
        }

        /// <summary>
        /// 復号化処理
        /// </summary>
        /// <param name="CipherValue">復号化を行う文字列</param>
        /// <returns>復号化した文字列</returns>
        public static string Decrypt(string CipherValue)
        {
            string DecryptionValue = "";

            if (!string.IsNullOrEmpty(CipherValue))
            {
                //  復号用のキー情報をセットする
                byte[] EcdUtf8ByteKey = Encoding.UTF8.GetBytes(PXCL_fix.EncryptKey);
                byte[] EcdUtf8ByteIv = Encoding.UTF8.GetBytes(PXCL_fix.EncryptIV);
                //  暗号化用の3DESクラス生成
                TripleDESCryptoServiceProvider TripleDes = new TripleDESCryptoServiceProvider();

                try
                {
                    byte[] HexStr2ByteValue = hexString2Byte(CipherValue);
                    using (MemoryStream MemSt = new MemoryStream())
                    {
                        using (CryptoStream CryptoStr = new CryptoStream(MemSt, TripleDes.CreateDecryptor(EcdUtf8ByteKey, EcdUtf8ByteIv), CryptoStreamMode.Write))
                        {
                            //  復号化
                            CryptoStr.Write(HexStr2ByteValue, 0, HexStr2ByteValue.Length);
                        }
                        byte[] DecryptionData = MemSt.ToArray();
                        DecryptionValue = Encoding.UTF8.GetString(DecryptionData);
                    }
                }
                catch (Exception Exc)
                {

                }
            }

            return DecryptionValue;
        }

        /// <summary>
        /// 16進数の文字列をバイト列に変換する
        /// </summary>
        /// <param name="Value">文字列</param>
        /// <returns>バイト配列</returns>
        protected static byte[] hexString2Byte(string Value)
        {
            int Size = Value.Length / 2;
            byte[] Res = new byte[Size];

            //2文字ずつ進み、バイトに変換
            for (int i = 0; i < Size; i++)
            {
                string GetVal = Value.Substring(i * 2, 2);
                Res[i] = Convert.ToByte(GetVal, 16);
            }

            return Res;
        }

        /// <summary>
        /// バイト列を16進数の文字列に変換する
        /// </summary>
        /// <param name="Value">バイト配列</param>
        /// <returns>16進数の文字列</returns>
        protected static string byte2HexString(byte[] Value)
        {
            //00-11-22形式を001122に変換
            return BitConverter.ToString(Value).Replace("-", "");
        }

    }
}
