using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using static PXAS.PXAS_stcCW;
using System.Security.Cryptography;
using System.IO;

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
                    byte[] EcdUtf8ByteKey = Encoding.UTF8.GetBytes(PXAS_fixCW.EncryptKey);
                    byte[] EcdUtf8ByteIv = Encoding.UTF8.GetBytes(PXAS_fixCW.EncryptIV);
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
                byte[] EcdUtf8ByteKey = Encoding.UTF8.GetBytes(PXAS_fixCW.EncryptKey);
                byte[] EcdUtf8ByteIv = Encoding.UTF8.GetBytes(PXAS_fixCW.EncryptIV);
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
