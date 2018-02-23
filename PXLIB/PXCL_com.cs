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

        /// <summary>
        ///  プログラムの利用権限情報取得
        /// </summary>
        /// <param name="ProgramId">プログラムID</param>
        /// <param name="PX_COMMONData">PX_COMMON</param>
        /// <returns>P3PROGRAMAUT プログラムの利用権限情報</returns>
        public static PX_PROGRAM_AUT GetP3ProgramAuth(string ProgramId, PX_COMMON PX_COMMONData)
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

        /// <summary>
        /// Xmlファイル読み込みメソッド
        /// </summary>
        /// <typeparam name="M">xmlファイルの型</typeparam>
        /// <param name="filePath">ファイルパス</param>
        /// <returns>M型の読み込みデータ</returns>
        public static M LoadXmlData<M>(String filePath)
        {
            M resolut;
            try
            {
                System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Open);
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(M));
                resolut = (M)serializer.Deserialize(fs);
                fs.Flush();
                fs.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resolut;
        }
        /// <summary>
        /// Xmlファイル書き込みメソッド
        /// </summary>
        /// <typeparam name="M">xmlファイルの型</typeparam>
        /// <param name="filePath">ファイルパス</param>
        /// <param name="data">ファイル書き込み内容</param>
        /// <returns>書き込み成否</returns>
        public static bool WriteXmlData<M>(String filePath, M data)
        {
            bool resolut = false;
            try
            {
                System.IO.FileStream stream = new System.IO.FileStream(filePath, System.IO.FileMode.Create);
                System.IO.StreamWriter writer = new System.IO.StreamWriter(stream, System.Text.Encoding.UTF8);
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(M));
                serializer.Serialize(writer, data);

                writer.Flush();
                writer.Close();
                resolut = true;
            }
            catch (Exception ex)
            {
                resolut = false;
                throw ex;
            }
            return resolut;
        }

        public static List<PX_SYSPARA> GetPGParameter(string SysPrId1, string SysPrId2, string SysPrId3, PX_COMMON PX_COMMONData)
        {
            return GetPGParameter(SysPrId1, SysPrId2, SysPrId3, null, new List<string>(), PX_COMMONData);
        }
        public static List<PX_SYSPARA> GetPGParameter(string SysPrId1, string SysPrId2, string SysPrId3, int SYSPARASEQ, PX_COMMON PX_COMMONData)
        {
            return GetPGParameter(SysPrId1, SysPrId2, SysPrId3, SYSPARASEQ, new List<string>(), PX_COMMONData);
        }

        /// <summary>
        /// プログラムパラメータの取得
        /// </summary>
        /// <param name="SysPrId1">パラメータID1</param>
        /// <param name="SysPrId2">パラメータID2</param>
        /// <param name="SysPrId3">パラメータID3</param>
        /// <param name="SYSPARASEQ">SeqNo</param>
        /// <param name="OtherParam">その他のパラメータを指定したい場合使用：列名,値→をListにAdd</param>
        /// <param name="PX_COMMONData">PX_COMMON</param>
        /// <returns>パラメータ</returns>
        public static List<PX_SYSPARA> GetPGParameter(string SysPrId1, string SysPrId2, string SysPrId3, int? SYSPARASEQ, List<string> OtherParam, PX_COMMON PX_COMMONData)
        {
            StringBuilder CmdTxt = new StringBuilder();
            PXCL_dba DbAccess = new PXCL_dba(PXCL_dba.ConnectionSystem, PX_COMMONData);
            List<PX_SYSPARA> DrList = new List<PX_SYSPARA>();
            try
            {
                // データベースを開く
                DbAccess.DBConect();

                // システムパラメータの取得SQL
                CmdTxt.AppendLine("SELECT * FROM P3AS_SYSPARA ");
                CmdTxt.AppendLine(" WHERE COPCD = @COPCD ");
                CmdTxt.AppendLine("   AND SYSPARAID1 = @SYSPARAID1");
                if (SysPrId2.Trim().Length > 0)
                {
                    CmdTxt.AppendLine("   AND SYSPARAID2 = @SYSPARAID2");
                }
                if (SysPrId3.Trim().Length > 0)
                {
                    CmdTxt.AppendLine("   AND SYSPARAID3 = @SYSPARAID3");
                }
                if (SYSPARASEQ != null)
                {
                    CmdTxt.AppendLine("   AND SYSPARASEQ = @SYSPARASEQ");
                }
                if (OtherParam.Count > 0)
                {
                    foreach (var item in OtherParam)
                    {
                        string[] tmp = item.Split(new string[] { "," }, StringSplitOptions.None);
                        CmdTxt.AppendFormat("   AND {0} = @{0} {1}", tmp[0], Environment.NewLine);
                    }
                }
                CmdTxt.AppendLine(" ORDER BY DSPSORT");

                //◆条件の設定
                using (SqlCommand SqlCmd = new SqlCommand())
                {
                    SqlCmd.Parameters.Add("@COPCD", SqlDbType.Char).Value = PX_COMMONData.COPCD;
                    SqlCmd.Parameters.Add("@SYSPARAID1", SqlDbType.VarChar).Value = SysPrId1;
                    if (SysPrId2.Trim().Length > 0)
                    {
                        SqlCmd.Parameters.Add("@SYSPARAID2", SqlDbType.VarChar).Value = SysPrId2;
                    }
                    if (SysPrId3.Trim().Length > 0)
                    {
                        SqlCmd.Parameters.Add("@SYSPARAID3", SqlDbType.VarChar).Value = SysPrId3;
                    }
                    if (SYSPARASEQ != null)
                    {
                        SqlCmd.Parameters.Add("@SYSPARASEQ", SqlDbType.SmallInt).Value = SYSPARASEQ;
                    }

                    if (OtherParam.Count > 0)
                    {
                        foreach (var item in OtherParam)
                        {
                            string[] tmp = item.Split(new string[] { "," }, StringSplitOptions.None);
                            switch (tmp[0])
                            {
                                case "CTLFLG1":
                                case "CTLFLG2":
                                case "CTLFLG3":
                                case "CTLFLG4":
                                case "CTLFLG5":
                                    SqlCmd.Parameters.Add("@" + tmp[0], SqlDbType.Char).Value = tmp[1];
                                    break;
                                case "CTLCD1":
                                case "CTLCD2":
                                case "CTLCD3":
                                case "CTLCD4":
                                case "CTLCD5":
                                    SqlCmd.Parameters.Add("@" + tmp[0], SqlDbType.VarChar).Value = tmp[1];
                                    break;
                                case "SYSPARANM":
                                case "CTLPARA1":
                                case "CTLPARA2":
                                case "CTLPARA3":
                                case "CTLPARA4":
                                case "CTLPARA5":
                                case "GUIDENM":
                                case "SUBCMM":
                                    SqlCmd.Parameters.Add("@" + tmp[0], SqlDbType.NVarChar).Value = tmp[1];
                                    break;
                                case "DSPSORT":
                                    SqlCmd.Parameters.Add("@" + tmp[0], SqlDbType.SmallInt).Value = tmp[1];
                                    break;
                                case "CTLSEQ1":
                                case "CTLSEQ2":
                                case "CTLSEQ3":
                                case "CTLSEQ4":
                                case "CTLSEQ5":
                                case "CTLQNT1":
                                case "CTLQNT2":
                                case "CTLQNT3":
                                case "CTLQNT4":
                                case "CTLQNT5":
                                    SqlCmd.Parameters.Add("@" + tmp[0], SqlDbType.Decimal).Value = tmp[1];
                                    break;
                                case "CTLDATE1":
                                case "CTLDATE2":
                                case "CTLDATE3":
                                case "CTLDATE4":
                                case "CTLDATE5":
                                    SqlCmd.Parameters.Add("@" + tmp[0], SqlDbType.DateTime).Value = tmp[1];
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    //◆SQL実行
                    using (SqlDataReader Res = DbAccess.SQLSelectParameter(CmdTxt.ToString(), SqlCmd))
                    {
                        if (Res != null && Res.HasRows)
                        {
                            while (Res.Read())
                            {
                                PX_SYSPARA data = new PX_SYSPARA
                                {
                                    COPCD = Res["COPCD"].ToString(),
                                    SYSPARAID1 = Res["SYSPARAID1"].ToString(),
                                    SYSPARAID2 = Res["SYSPARAID2"].ToString(),
                                    SYSPARAID3 = Res["SYSPARAID3"].ToString(),
                                    SYSPARASEQ = int.Parse(Res["SYSPARASEQ"].ToString()),
                                    SYSPARANM = Res["SYSPARANM"] == DBNull.Value ? "" : Res["SYSPARANM"].ToString(),
                                    DSPSORT = int.Parse(Res["DSPSORT"].ToString()),
                                    CTLCD1 = Res["CTLCD1"] == DBNull.Value ? "" : Res["CTLCD1"].ToString(),
                                    CTLCD2 = Res["CTLCD2"] == DBNull.Value ? "" : Res["CTLCD2"].ToString(),
                                    CTLCD3 = Res["CTLCD3"] == DBNull.Value ? "" : Res["CTLCD3"].ToString(),
                                    CTLCD4 = Res["CTLCD4"] == DBNull.Value ? "" : Res["CTLCD4"].ToString(),
                                    CTLCD5 = Res["CTLCD5"] == DBNull.Value ? "" : Res["CTLCD5"].ToString(),
                                    CTLPARA1 = Res["CTLPARA1"] == DBNull.Value ? "" : Res["CTLPARA1"].ToString(),
                                    CTLPARA2 = Res["CTLPARA2"] == DBNull.Value ? "" : Res["CTLPARA2"].ToString(),
                                    CTLPARA3 = Res["CTLPARA3"] == DBNull.Value ? "" : Res["CTLPARA3"].ToString(),
                                    CTLPARA4 = Res["CTLPARA4"] == DBNull.Value ? "" : Res["CTLPARA4"].ToString(),
                                    CTLPARA5 = Res["CTLPARA5"] == DBNull.Value ? "" : Res["CTLPARA5"].ToString(),
                                    CTLSEQ1 = int.Parse(Res["CTLSEQ1"].ToString()),
                                    CTLSEQ2 = int.Parse(Res["CTLSEQ2"].ToString()),
                                    CTLSEQ3 = int.Parse(Res["CTLSEQ3"].ToString()),
                                    CTLSEQ4 = int.Parse(Res["CTLSEQ4"].ToString()),
                                    CTLSEQ5 = int.Parse(Res["CTLSEQ5"].ToString()),
                                    CTLQNT1 = decimal.Parse(Res["CTLQNT1"].ToString()),
                                    CTLQNT2 = decimal.Parse(Res["CTLQNT2"].ToString()),
                                    CTLQNT3 = decimal.Parse(Res["CTLQNT3"].ToString()),
                                    CTLQNT4 = decimal.Parse(Res["CTLQNT4"].ToString()),
                                    CTLQNT5 = decimal.Parse(Res["CTLQNT5"].ToString()),
                                    CTLFLG1 = Res["CTLFLG1"].ToString(),
                                    CTLFLG2 = Res["CTLFLG2"].ToString(),
                                    CTLFLG3 = Res["CTLFLG3"].ToString(),
                                    CTLFLG4 = Res["CTLFLG4"].ToString(),
                                    CTLFLG5 = Res["CTLFLG5"].ToString(),
                                    CTLDATE1 = Res["CTLDATE1"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(Res["CTLDATE1"]),
                                    CTLDATE2 = Res["CTLDATE2"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(Res["CTLDATE2"]),
                                    CTLDATE3 = Res["CTLDATE3"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(Res["CTLDATE3"]),
                                    CTLDATE4 = Res["CTLDATE4"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(Res["CTLDATE4"]),
                                    CTLDATE5 = Res["CTLDATE5"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(Res["CTLDATE5"]),
                                    GUIDENM = Res["GUIDENM"] == DBNull.Value ? "" : Res["GUIDENM"].ToString(),
                                    SUBCMM = Res["SUBCMM"] == DBNull.Value ? "" : Res["SUBCMM"].ToString()
                                };
                                DrList.Add(data);
                            }
                        }
                    }
                }
            }
            catch (Exception Exc)
            {
                string LogTitle = "システムパラメータ取得";
                string LogMsg = "エラー「" + Exc.Message + "」";
                PXCL_log.writeLog(PXCL_log.ERR, PXCL_log.SELECT, LogTitle, LogMsg, System.Reflection.MethodBase.GetCurrentMethod(), PX_COMMONData);
            }
            finally
            {
                //データベースの接続解除
                DbAccess.DBClose();
            }
            return DrList;
        }

        public static PX_PJ3CONFIG GetPJ3Config(string[] SysPrId, ref PX_COMMON PX_COMMONData)
        {
            StringBuilder CmdTxt = new StringBuilder();
            List<PX_SYSPARA> DrList = new List<PX_SYSPARA>();
            PX_PJ3CONFIG cfg = new PX_PJ3CONFIG();
            try
            {
                DrList = GetPGParameter(SysPrId[0], SysPrId[1], SysPrId[2], PX_COMMONData);
                if (DrList.Count > 0)
                {
                    foreach (var item in DrList)
                    {
                        switch (item.SYSPARASEQ)
                        {
                            case 1:
                                cfg.TITLEPIC = item.CTLPARA1; // ファイル名のみ
                                cfg.BGPIC = item.CTLPARA2; // ファイル名のみ
                                cfg.ADDACCOUNT = item.CTLFLG1;
                                cfg.KEEPLOGIN = item.CTLFLG2;
                                cfg.VIEWINFO = item.CTLPARA5;
                                cfg.DEFLANG = item.CTLCD1;
                                cfg.FLAMECOLOR = item.CTLCD2;
                                break;
                            case 2:
                                cfg.IDCHAR = item.CTLFLG1;
                                cfg.IDLENMIN = (int)item.CTLSEQ1;
                                cfg.IDLENMAX = (int)item.CTLSEQ2;
                                cfg.PACHAR = item.CTLFLG2;
                                cfg.PALENMIN = (int)item.CTLSEQ3;
                                cfg.PALENMAX = (int)item.CTLSEQ4;
                                cfg.PALOCACT = item.CTLFLG3;
                                cfg.PALOCCNT = (int)item.CTLSEQ5;
                                break;
                            default:
                                break;
                        }
                    }
                }
                else
                {
                    PX_COMMONData.ERRORCODE = "PXERR402";
                }
            }
            catch (Exception Exc)
            {
                string LogTitle = "システムパラメータ取得";
                string LogMsg = "エラー「" + Exc.Message + "」";
                PXCL_log.writeLog(PXCL_log.ERR, PXCL_log.SELECT, LogTitle, LogMsg, System.Reflection.MethodBase.GetCurrentMethod(), PX_COMMONData);
                PX_COMMONData.ERRORCODE = "PXERR402-2";
            }
            return cfg;
        }

        /// <summary>
        /// ユーザーIDのチェック
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="PX_COMMONData"></param>
        /// <returns>ワンタイムキーユーザのキー（空の場合は通常ユーザー）</returns>
        public static List<string> GetUserKeyInfo(string userid, ref PX_COMMON PX_COMMONData)
        {
            StringBuilder CmdTxt = new StringBuilder();
            PXCL_dba DbAccess = new PXCL_dba(PXCL_dba.ConnectionSystem, PX_COMMONData);
            List<string> retList = new List<string>();
            try
            {
                // データベースを開く
                DbAccess.DBConect();

                // システムパラメータの取得SQL
                CmdTxt.AppendLine("SELECT SYSID, USERPASS, PASSCTL, ONETPASS ");
                CmdTxt.AppendLine(" ,PASSLMTYMD, PASSALMYMD ");
                CmdTxt.AppendLine(" ,DEFCOPTP, DEFCOPCD, ENDCOPCD");
                CmdTxt.AppendLine(" , CONVERT(NVARCHAR, ONETLMTDATE, 111)  + ' ' + CONVERT(NVARCHAR, ONETLMTDATE, 108) AS ONETLMTDATE  ");
                CmdTxt.AppendLine(" , CONVERT(NVARCHAR, ONETALMDATE, 111)  + ' ' + CONVERT(NVARCHAR, ONETALMDATE, 108) AS ONETALMDATE ");
                CmdTxt.AppendLine(" , PASSCTL");
                CmdTxt.AppendLine(" , USERID, USERNM ");
                CmdTxt.AppendLine("FROM P3AS_USER");
                CmdTxt.AppendLine(" WHERE USERID = @USERID ");
                CmdTxt.AppendLine("     AND  DELFLG = '000' ");
                //CmdTxt.AppendLine("");

                using (SqlCommand SqlCmd = new SqlCommand())
                {
                    SqlCmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = userid;

                    using (SqlDataReader Res = DbAccess.SQLSelectParameter(CmdTxt.ToString(), SqlCmd))
                    {
                        if (Res != null && Res.HasRows)
                        {
                            while (Res.Read())
                            {
                                string tmp = Res["ONETPASS"] == DBNull.Value ? "" : PXCL_com.Decrypt(Res["ONETPASS"].ToString());
                                retList.Add(tmp);
                                PX_COMMONData.USERTYPE = tmp.Length == 0 ? "ONETIME" : "NORMAL";
                                retList.Add(Res["ONETLMTDATE"] == DBNull.Value ? "" : Res["ONETLMTDATE"].ToString());
                                retList.Add(Res["ONETALMDATE"] == DBNull.Value ? "" : Res["ONETALMDATE"].ToString());
                                retList.Add(Res["PASSCTL"] == DBNull.Value ? "" : Res["PASSCTL"].ToString());
                                tmp = Res["USERPASS"] == DBNull.Value ? "" : PXCL_com.Decrypt(Res["USERPASS"].ToString());
                                retList.Add(tmp);
                                retList.Add(Res["PASSLMTYMD"] == DBNull.Value ? "" : Res["PASSLMTYMD"].ToString());
                                retList.Add(Res["PASSALMYMD"] == DBNull.Value ? "" : Res["PASSALMYMD"].ToString());
                                retList.Add(Res["DEFCOPTP"] == DBNull.Value ? "" : Res["DEFCOPTP"].ToString());
                                retList.Add(Res["DEFCOPCD"] == DBNull.Value ? "" : Res["DEFCOPCD"].ToString());
                                retList.Add(Res["ENDCOPCD"] == DBNull.Value ? "" : Res["ENDCOPCD"].ToString());

                                PX_COMMONData.USERID = Res["USERID"].ToString();
                                PX_COMMONData.USERNM = Res["USERNM"].ToString();
                                PX_COMMONData.SYSID = Res["SYSID"] == DBNull.Value ? "" : Res["SYSID"].ToString();
                            }
                        }
                        else
                        {
                            //UserID登録なし
                            PX_COMMONData.ERRORCODE = "USERERR-01";
                        }
                    }
                }
            }
            catch (Exception Exc)
            {
                string LogTitle = "システムパラメータ取得";
                string LogMsg = "エラー「" + Exc.Message + "」";
                PXCL_log.writeLog(PXCL_log.ERR, PXCL_log.SELECT, LogTitle, LogMsg, System.Reflection.MethodBase.GetCurrentMethod(), PX_COMMONData);
            }
            finally
            {
                //データベースの接続解除
                DbAccess.DBClose();
            }
            return retList;
        }

        public static PX_COMMON GetUserCTL(PX_COMMON PX_COMMONData)
        {
            StringBuilder CmdTxt = new StringBuilder();
            PXCL_dba DbAccess = new PXCL_dba(PXCL_dba.ConnectionSystem, PX_COMMONData);
            try
            {
                // データベースを開く
                DbAccess.DBConect();

                // システムパラメータの取得SQL
                CmdTxt.AppendLine("SELECT * ");
                CmdTxt.AppendLine("FROM P3AS_USER_CTL");
                CmdTxt.AppendLine(" WHERE USERID = @USERID ");
                CmdTxt.AppendLine("     AND  COPCD = @COPCD ");
                CmdTxt.AppendLine("     AND  SYSID = @SYSID ");
                CmdTxt.AppendLine("     AND (USERLMTYMD = '00000000' OR USERLMTYMD >= @USERLMTYMD)");
                //CmdTxt.AppendLine("");

                using (SqlCommand SqlCmd = new SqlCommand())
                {
                    SqlCmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = PX_COMMONData.USERID;
                    SqlCmd.Parameters.Add("@COPCD", SqlDbType.VarChar).Value = PX_COMMONData.COPCD;
                    SqlCmd.Parameters.Add("@SYSID", SqlDbType.VarChar).Value = PX_COMMONData.SYSID;
                    SqlCmd.Parameters.Add("@USERLMTYMD", SqlDbType.Char).Value = DateTime.Today.ToString("yyyyMMdd");

                    using (SqlDataReader Res = DbAccess.SQLSelectParameter(CmdTxt.ToString(), SqlCmd))
                    {
                        if (Res != null && Res.HasRows)
                        {
                            while (Res.Read())
                            {
                                PX_COMMONData.MENUID = Res["MENUID"] == DBNull.Value ? "" : Res["MENUID"].ToString();
                                PX_COMMONData.AUTKBN = Res["AUTKBN"] == DBNull.Value ? "" : Res["AUTKBN"].ToString();
                                PX_COMMONData.INIGRPCD = Res["INIGRPCD"] == DBNull.Value ? "" : Res["INIGRPCD"].ToString();
                                PX_COMMONData.INIDPTCD = Res["MENUPATH"] == DBNull.Value ? "" : Res["MENUPATH"].ToString();
                                PX_COMMONData.INIWHSCD = Res["MENUPATH"] == DBNull.Value ? "" : Res["MENUPATH"].ToString();
                                PX_COMMONData.INICMPCD = Res["MENUPATH"] == DBNull.Value ? "" : Res["MENUPATH"].ToString();
                                PX_COMMONData.INICSTCD = Res["MENUPATH"] == DBNull.Value ? "" : Res["MENUPATH"].ToString();
                                PX_COMMONData.INISHPCD = Res["MENUPATH"] == DBNull.Value ? "" : Res["MENUPATH"].ToString();
                                PX_COMMONData.ACPTTP = Res["ADRCD2"].ToString();
                            }
                        }
                        else
                        {
                            //UserID登録なし
                            PX_COMMONData.ERRORCODE = "USERERR-02";
                        }
                    }
                }
            }
            catch (Exception Exc)
            {
                string LogTitle = "システムパラメータ取得";
                string LogMsg = "エラー「" + Exc.Message + "」";
                PXCL_log.writeLog(PXCL_log.ERR, PXCL_log.SELECT, LogTitle, LogMsg, System.Reflection.MethodBase.GetCurrentMethod(), PX_COMMONData);
            }
            finally
            {
                //データベースの接続解除
                DbAccess.DBClose();
            }
            return PX_COMMONData;
        }

        public static PX_COMMON GetSystemParam(PX_COMMON PX_COMMONData)
        {
            StringBuilder CmdTxt = new StringBuilder();
            PXCL_dba DbAccess = new PXCL_dba(PXCL_dba.ConnectionSystem, PX_COMMONData);
            try
            {
                // データベースを開く
                DbAccess.DBConect();

                // システムパラメータの取得SQL
                CmdTxt.AppendLine("SELECT DBNM, DBSVRNM, DBSVRIP, DBUSER, DBPASS ");
                CmdTxt.AppendLine(" ,SYSTP, SYSGRP, SYSVERNO");
                CmdTxt.AppendLine("FROM P3AS_SYSTEM");
                CmdTxt.AppendLine(" WHERE SYSID = @SYSID ");
                //CmdTxt.AppendLine("");

                using (SqlCommand SqlCmd = new SqlCommand())
                {
                    SqlCmd.Parameters.Add("@SYSID", SqlDbType.VarChar).Value = PX_COMMONData.SYSID;

                    using (SqlDataReader Res = DbAccess.SQLSelectParameter(CmdTxt.ToString(), SqlCmd))
                    {
                        if (Res != null && Res.HasRows)
                        {
                            while (Res.Read())
                            {
                                PX_COMMONData.USERDBNM = Res["DBNM"] == DBNull.Value ? "" : Res["DBNM"].ToString();
                                PX_COMMONData.USERDBSVRNM = Res["DBSVRNM"] == DBNull.Value ? "" : Res["DBSVRNM"].ToString();
                                PX_COMMONData.USERDBSVRIP = Res["DBSVRIP"] == DBNull.Value ? "" : Res["DBSVRIP"].ToString();
                                PX_COMMONData.USERDBSVRUR = Res["DBUSER"] == DBNull.Value ? "" : Res["DBUSER"].ToString();
                                PX_COMMONData.USERDBSVRPW = Res["DBPASS"] == DBNull.Value ? "" : Res["DBPASS"].ToString();
                                PX_COMMONData.SYSTP = Res["SYSTP"].ToString();
                                PX_COMMONData.SYSGRP = Res["SYSGRP"].ToString();
                                PX_COMMONData.SYSVERNO = Res["SYSVERNO"] == DBNull.Value ? "" : Res["SYSVERNO"].ToString();
                            }
                        }
                        else
                        {
                            //UserID登録なし
                            PX_COMMONData.ERRORCODE = "SYSERR-INV";
                        }
                    }
                }
            }
            catch (Exception Exc)
            {
                string LogTitle = "システムパラメータ取得";
                string LogMsg = "エラー「" + Exc.Message + "」";
                PXCL_log.writeLog(PXCL_log.ERR, PXCL_log.SELECT, LogTitle, LogMsg, System.Reflection.MethodBase.GetCurrentMethod(), PX_COMMONData);

                PX_COMMONData.ERRORCODE = "SYSERR-INV";
            }
            finally
            {
                //データベースの接続解除
                DbAccess.DBClose();
            }
            return PX_COMMONData;
        }

        /// <summary>
        /// 今後使用するかもー一応取っておく（2018/02/22）
        /// </summary>
        /// <param name="PX_COMMONData"></param>
        /// <returns></returns>
        public static PX_USEROPT GetUserOPT(PX_COMMON PX_COMMONData)
        {
            StringBuilder CmdTxt = new StringBuilder();
            PXCL_dba DbAccess = new PXCL_dba(PXCL_dba.ConnectionSystem, PX_COMMONData);
            PX_USEROPT retList = new PX_USEROPT();
            try
            {
                // データベースを開く
                DbAccess.DBConect();

                // システムパラメータの取得SQL
                CmdTxt.AppendLine("SELECT * ");
                CmdTxt.AppendLine("FROM P3AS_USER_OPT");
                CmdTxt.AppendLine(" WHERE USERID = @USERID ");
                CmdTxt.AppendLine("     AND  COPCD = @COPCD ");
                CmdTxt.AppendLine("     AND  SYSID = @SYSID ");
                //CmdTxt.AppendLine("");

                using (SqlCommand SqlCmd = new SqlCommand())
                {
                    SqlCmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = PX_COMMONData.USERID;
                    SqlCmd.Parameters.Add("@COPCD", SqlDbType.VarChar).Value = PX_COMMONData.COPCD;
                    SqlCmd.Parameters.Add("@SYSID", SqlDbType.VarChar).Value = PX_COMMONData.SYSID;

                    using (SqlDataReader Res = DbAccess.SQLSelectParameter(CmdTxt.ToString(), SqlCmd))
                    {
                        if (Res != null && Res.HasRows)
                        {
                            while (Res.Read())
                            {
                                retList = new PX_USEROPT
                                {
                                    COPCD = Res["COPCD"].ToString(),
                                    SYSID = Res["SYSID"].ToString(),
                                    USERID = Res["USERID"].ToString(),
                                    USERSEX = Res["USERSEX"].ToString(),
                                    BRTDYMD = Res["BRTDYMD"].ToString(),
                                    RECPYMD = Res["RECPYMD"].ToString(),
                                    JOINYMD = Res["JOINYMD"].ToString(),
                                    JOINACPTID = Res["JOINACPTID"] == DBNull.Value ? "" : Res["JOINACPTID"].ToString(),
                                    APPYM = Res["APPYM"].ToString(),
                                   CONTNAMENM1 = Res["CONTNAMENM1"] == DBNull.Value ? "" : Res["CONTNAMENM1"].ToString(),
                                   CONTNAMENM1C = Res["CONTNAMENM1C"] == DBNull.Value ? "" : Res["CONTNAMENM1C"].ToString(),
                                   CONTTEL11 = Res["CONTTEL11"] == DBNull.Value ? "" : Res["CONTTEL11"].ToString(),
                                   CONTTEL12 = Res["CONTTEL12"] == DBNull.Value ? "" : Res["CONTTEL12"].ToString(),
                                   CONTRLTN1 = Res["CONTRLTN1"] == DBNull.Value ? "" : Res["CONTRLTN1"].ToString(),
                                   CONTNAMENM2 = Res["CONTNAMENM2"] == DBNull.Value ? "" : Res["CONTNAMENM2"].ToString(),
                                   CONTNAMENM2C = Res["CONTNAMENM2C"] == DBNull.Value ? "" : Res["CONTNAMENM2C"].ToString(),
                                   CONTTEL21 = Res["CONTTEL21"] == DBNull.Value ? "" : Res["CONTTEL21"].ToString(),
                                   CONTTEL22 = Res["CONTTEL22"] == DBNull.Value ? "" : Res["CONTTEL22"].ToString(),
                                   CONTRLTN2 = Res["CONTRLTN2"] == DBNull.Value ? "" : Res["CONTRLTN2"].ToString(),
                                    FAMPRTNKBN = Res["FAMPRTNKBN"].ToString(),
                                   FAMPRTNYMFR = Res["FAMPRTNYMFR"] == DBNull.Value ? "" : Res["FAMPRTNYMFR"].ToString(),
                                   FAMPRTNYMTO = Res["FAMPRTNYMTO"] == DBNull.Value ? "" : Res["FAMPRTNYMTO"].ToString(),
                                    FAMCHLDKBN = Res["FAMCHLDKBN"].ToString(),
                                   FAMCHLDYMFR1 = Res["FAMCHLDYMFR1"] == DBNull.Value ? "" : Res["FAMCHLDYMFR1"].ToString(),
                                   FAMCHLDYMTO1 = Res["FAMCHLDYMTO1"] == DBNull.Value ? "" : Res["FAMCHLDYMTO1"].ToString(),
                                   FAMCHLDYMFR2 = Res["FAMCHLDYMFR2"] == DBNull.Value ? "" : Res["FAMCHLDYMFR2"].ToString(),
                                   FAMCHLDYMTO2 = Res["FAMCHLDYMTO2"] == DBNull.Value ? "" : Res["FAMCHLDYMTO2"].ToString(),
                                   FAMCHLDYMFR3 = Res["FAMCHLDYMFR3"] == DBNull.Value ? "" : Res["FAMCHLDYMFR3"].ToString(),
                                   FAMCHLDYMTO3 = Res["FAMCHLDYMTO3"] == DBNull.Value ? "" : Res["FAMCHLDYMTO3"].ToString(),
                                   FAMCHLDYMFR4 = Res["FAMCHLDYMFR4"] == DBNull.Value ? "" : Res["FAMCHLDYMFR4"].ToString(),
                                   FAMCHLDYMTO4 = Res["FAMCHLDYMTO4"] == DBNull.Value ? "" : Res["FAMCHLDYMTO4"].ToString(),
                                   FAMCHLDYMD1 = Res["FAMCHLDYMD1"] == DBNull.Value ? "" : Res["FAMCHLDYMD1"].ToString(),
                                    FAMCHLDQNT = Res["FAMCHLDQNT"] == DBNull.Value ? 0 : int.Parse(Res["FAMCHLDQNT"].ToString()),
                                    FAMGSONQNT = Res["FAMGSONQNT"] == DBNull.Value ? 0 : int.Parse(Res["FAMGSONQNT"].ToString()),
                                    FAMHMATQNT = Res["FAMHMATQNT"] == DBNull.Value ? 0 : int.Parse(Res["FAMHMATQNT"].ToString()),
                                    MEMBERCD1 = Res["MEMBERCD1"] == DBNull.Value ? "" : Res["MEMBERCD1"].ToString(),
                                   MEMBERCD2 = Res["MEMBERCD2"] == DBNull.Value ? "" : Res["MEMBERCD2"].ToString(),
                                   MEMBERCD3 = Res["MEMBERCD3"] == DBNull.Value ? "" : Res["MEMBERCD3"].ToString(),
                                    MEMBERCD4 = Res["MEMBERCD4"].ToString(),
                                    MEMBERCD5 = Res["MEMBERCD5"].ToString(),
                                    MEMBERFLG1 = Res["MEMBERFLG1"].ToString(),
                                    MEMBERFLG2 = Res["MEMBERFLG2"].ToString(),
                                    MEMBERFLG3 = Res["MEMBERFLG3"].ToString(),
                                    MEMBERFLG4 = Res["MEMBERFLG4"].ToString(),
                                    MEMBERFLG5 = Res["MEMBERFLG5"].ToString(),
                                    MEMPROCFLG1 = Res["MEMPROCFLG1"].ToString(),
                                    MEMPROCFLG2 = Res["MEMPROCFLG2"].ToString(),
                                    MEMPROCFLG3 = Res["MEMPROCFLG3"].ToString(),
                                    MEMPROCFLG4 = Res["MEMPROCFLG4"].ToString(),
                                    MEMPROCFLG5 = Res["MEMPROCFLG5"].ToString(),
                                    LOGPROCFLG1 = Res["LOGPROCFLG1"].ToString(),
                                    LOGPROCFLG2 = Res["LOGPROCFLG2"].ToString(),
                                    LOGPROCFLG3 = Res["LOGPROCFLG3"].ToString(),
                                    LOGPROCFLG4 = Res["LOGPROCFLG4"].ToString(),
                                    LOGPROCFLG5 = Res["LOGPROCFLG5"].ToString(),
                                    QUESTANSFLG01 = Res["QUESTANSFLG01"].ToString(),
                                    QUESTANSFLG02 = Res["QUESTANSFLG02"].ToString(),
                                    QUESTANSFLG03 = Res["QUESTANSFLG03"].ToString(),
                                    QUESTANSFLG04 = Res["QUESTANSFLG04"].ToString(),
                                    QUESTANSFLG05 = Res["QUESTANSFLG05"].ToString(),
                                    QUESTANSFLG06 = Res["QUESTANSFLG06"].ToString(),
                                    QUESTANSFLG07 = Res["QUESTANSFLG07"].ToString(),
                                    QUESTANSFLG08 = Res["QUESTANSFLG08"].ToString(),
                                    QUESTANSFLG09 = Res["QUESTANSFLG09"].ToString(),
                                    QUESTANSFLG10 = Res["QUESTANSFLG10"].ToString(),
                                    QUESTANSCMM1 = Res["QUESTANSCMM1"].ToString(),
                                    QUESTANSCMM2 = Res["QUESTANSCMM2"].ToString(),
                                    QUESTANSCMM3 = Res["QUESTANSCMM3"].ToString(),
                                };
                            }
                        }
                        else
                        {
                            //UserID登録なし
                            PX_COMMONData.ERRORCODE = "USERERR-02";
                        }
                    }
                }
            }
            catch (Exception Exc)
            {
                string LogTitle = "システムパラメータ取得";
                string LogMsg = "エラー「" + Exc.Message + "」";
                PXCL_log.writeLog(PXCL_log.ERR, PXCL_log.SELECT, LogTitle, LogMsg, System.Reflection.MethodBase.GetCurrentMethod(), PX_COMMONData);
            }
            finally
            {
                //データベースの接続解除
                DbAccess.DBClose();
            }
            return retList;
        }

        public static List<PX_LANGUAGE> GetLanguageList(string deflang, PX_COMMON PX_COMMONData)
        {
            StringBuilder CmdTxt = new StringBuilder();
            PXCL_dba DbAccess = new PXCL_dba(PXCL_dba.ConnectionSystem, PX_COMMONData);
            List<PX_LANGUAGE> retList = new List<PX_LANGUAGE>();
            try
            {
                // データベースを開く
                DbAccess.DBConect();

                // システムパラメータの取得SQL
                CmdTxt.AppendLine("SELECT CTLCD1, CTLCD2, CTLPARA1, CTLPARA2, CTLSEQ1 ");
                CmdTxt.AppendLine("FROM P3AS_SYSPARA");
                CmdTxt.AppendLine(" WHERE COPCD = '00000' ");
                CmdTxt.AppendLine("     AND  SYSPARAID1 = 'SYSTEM' ");
                CmdTxt.AppendLine("     AND  SYSPARAID2 = 'LANGUAGE' ");
                CmdTxt.AppendLine("ORDER BY DSPSORT");
                using (SqlCommand SqlCmd = new SqlCommand())
                {
                    SqlCmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = PX_COMMONData.USERID;
                    SqlCmd.Parameters.Add("@COPCD", SqlDbType.VarChar).Value = PX_COMMONData.COPCD;
                    SqlCmd.Parameters.Add("@SYSID", SqlDbType.VarChar).Value = PX_COMMONData.SYSID;
                    SqlCmd.Parameters.Add("@USERLMTYMD", SqlDbType.Char).Value = DateTime.Today.ToString("yyyyMMdd");
                    using (SqlDataReader Res = DbAccess.SQLSelectParameter(CmdTxt.ToString(),SqlCmd))
                    {
                        if (Res != null && Res.HasRows)
                        {
                            while (Res.Read())
                            {
                                PX_LANGUAGE lng = new PX_LANGUAGE
                                {
                                    LANGCLCODE = Res["CTLCD1"] == DBNull.Value ? "" : Res["CTLCD1"].ToString(),
                                    LANGCODE = Res["CTLCD2"] == DBNull.Value ? "" : Res["CTLCD2"].ToString(),
                                    LANGNAME = Res["CTLPARA1"] == DBNull.Value ? "" : Res["CTLPARA1"].ToString(),
                                    FLAGID = Res["CTLPARA2"] == DBNull.Value ? "" : Res["CTLPARA2"].ToString(),
                                    LANGNO = Res["CTLSEQ1"].ToString(),
                                    DEFLANGCODE = deflang
                                };
                                retList.Add(lng);
                            }
                        }
                        else
                        {
                            //Language登録なし - デフォルトで日本語を返す
                            PX_LANGUAGE lng = new PX_LANGUAGE("JPN");
                            retList.Add(lng);
                        }
                    }
                }
            }
            catch (Exception Exc)
            {
                string LogTitle = "システムパラメータ取得";
                string LogMsg = "エラー「" + Exc.Message + "」";
                PXCL_log.writeLog(PXCL_log.ERR, PXCL_log.SELECT, LogTitle, LogMsg, System.Reflection.MethodBase.GetCurrentMethod(), PX_COMMONData);
            }
            finally
            {
                //データベースの接続解除
                DbAccess.DBClose();
            }
            return retList;
        }

        public static PX_PJ3CONFIG GetLoginCaption(string LangNo, PX_PJ3CONFIG pX_PJ3CONFIG, PX_COMMON PX_COMMONData)
        {
            StringBuilder CmdTxt = new StringBuilder();
            PXCL_dba DbAccess = new PXCL_dba(PXCL_dba.ConnectionSystem, PX_COMMONData);
            try
            {
                // データベースを開く
                DbAccess.DBConect();

                // システムパラメータの取得SQL
                CmdTxt.AppendLine("SELECT SYSPARAID3, CTLPARA1 ");
                CmdTxt.AppendLine("FROM P3AS_SYSPARA");
                CmdTxt.AppendLine(" WHERE COPCD = '00000' ");
                CmdTxt.AppendLine("     AND  SYSPARAID1 = 'PXAS0001WP' ");
                CmdTxt.AppendLine("     AND  SYSPARAID2 = 'CAP' ");
                CmdTxt.AppendLine("     AND  SYSPARAID3 IN ('LOGIN', 'USERID', 'USERPASS', 'PASSFORGET', 'LOGINKEEP', 'USERADD')");
                CmdTxt.AppendLine("     AND  SYSPARASEQ = @SYSPARASEQ ");

                using (SqlCommand SqlCmd = new SqlCommand())
                {
                    SqlCmd.Parameters.Add("@SYSPARASEQ", SqlDbType.SmallInt).Value = int.Parse(LangNo);
                    using (SqlDataReader Res = DbAccess.SQLSelectParameter(CmdTxt.ToString(),SqlCmd))
                    {
                        if (Res != null && Res.HasRows)
                        {
                            while (Res.Read())
                            {
                                string tmp = Res["CTLPARA1"] == DBNull.Value ? "" : Res["CTLPARA1"].ToString();
                                switch (Res["SYSPARAID3"].ToString())
                                {
                                    case "LOGIN":
                                        pX_PJ3CONFIG.LOGIN = tmp;
                                        break;
                                    case "USERID":
                                        pX_PJ3CONFIG.USERID = tmp;
                                        break;
                                    case "USERPASS":
                                        pX_PJ3CONFIG.USERPASS = tmp;
                                        break;
                                    case "PASSFORGET":
                                        pX_PJ3CONFIG.PASSFORGET = tmp;
                                        break;
                                    case "LOGINKEEP":
                                        pX_PJ3CONFIG.LOGINKEEP = tmp;
                                        break;
                                    case "USERADD":
                                        pX_PJ3CONFIG.USERADD = tmp;
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception Exc)
            {
                string LogTitle = "システムパラメータ取得";
                string LogMsg = "エラー「" + Exc.Message + "」";
                PXCL_log.writeLog(PXCL_log.ERR, PXCL_log.SELECT, LogTitle, LogMsg, System.Reflection.MethodBase.GetCurrentMethod(), PX_COMMONData);
            }
            finally
            {
                //データベースの接続解除
                DbAccess.DBClose();
            }
            return pX_PJ3CONFIG;
        }

        public static int GetCopMasterCount(PX_COMMON PX_COMMONData)
        {
            StringBuilder CmdTxt = new StringBuilder();
            PXCL_dba DbAccess = new PXCL_dba(PXCL_dba.ConnectionUser, PX_COMMONData);
            int retCount=0;
            try
            {
                // データベースを開く
                DbAccess.DBConect();

                // システムパラメータの取得SQL
                CmdTxt.AppendLine("SELECT COUNT(COPCD) ");
                CmdTxt.AppendLine("FROM P3MS_COPMAS");
                CmdTxt.AppendLine(" WHERE COPCD = @COPCD ");
                CmdTxt.AppendLine("     AND  DELFLG = '000' ");
                CmdTxt.AppendLine("     AND  ACTFLG = '000' ");

                using (SqlCommand SqlCmd = new SqlCommand())
                {
                    SqlCmd.Parameters.Add("@COPCD", SqlDbType.Char).Value = PX_COMMONData.COPCD;
                    using (SqlDataReader Res = DbAccess.SQLSelectParameter(CmdTxt.ToString(), SqlCmd))
                    {
                        if (Res != null && Res.HasRows)
                        {
                            while (Res.Read())
                            {
                                retCount = int.Parse(Res[0].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception Exc)
            {
                string LogTitle = "システムパラメータ取得";
                string LogMsg = "エラー「" + Exc.Message + "」";
                PXCL_log.writeLog(PXCL_log.ERR, PXCL_log.SELECT, LogTitle, LogMsg, System.Reflection.MethodBase.GetCurrentMethod(), PX_COMMONData);
            }
            finally
            {
                //データベースの接続解除
                DbAccess.DBClose();
            }
            return retCount;
        }

        public static string GetSystemProcessingStatus(PX_COMMON PX_COMMONData)
        {
            StringBuilder CmdTxt = new StringBuilder();
            PXCL_dba DbAccess = new PXCL_dba(PXCL_dba.ConnectionUser, PX_COMMONData);
            string retStatsu = "";
            try
            {
                List<PX_SYSPARA> DrLis = GetPGParameter("SYSTEM", "OPERATION", "STATUS",0, PX_COMMONData);
                if (DrLis.Count>0)
                {
                    retStatsu = DrLis[0].CTLFLG1;
                }
            }
            catch (Exception Exc)
            {
                string LogTitle = "システムパラメータ取得";
                string LogMsg = "エラー「" + Exc.Message + "」";
                PXCL_log.writeLog(PXCL_log.ERR, PXCL_log.SELECT, LogTitle, LogMsg, System.Reflection.MethodBase.GetCurrentMethod(), PX_COMMONData);
            }
            finally
            {
                //データベースの接続解除
                DbAccess.DBClose();
            }
            return retStatsu;
        }

        public static int UpdateLoginStatus(PX_COMMON PX_COMMONData)
        {
            StringBuilder CmdTxt = new StringBuilder();
            PXCL_dba DbAccess = new PXCL_dba(PXCL_dba.ConnectionSystem, PX_COMMONData);
            int retCount = 0;
            try
            {
                // データベースを開く
                DbAccess.DBConect();
                DbAccess.Tran(PXCL_dba.TRNS_MODE.BEGIN);

                // システムパラメータの取得SQL
                CmdTxt.AppendLine("UPDATE P3AS_USER_CTL SET ");
                CmdTxt.AppendLine("  LOGINSTS = 'YES' ");
                CmdTxt.AppendLine(", LOGINDATE = GETDATE()");
                CmdTxt.AppendLine(" WHERE COPCD = @COPCD ");
                CmdTxt.AppendLine("     AND  SYSID = @SYSID ");
                CmdTxt.AppendLine("     AND  USERID = @USERID ");

                using (SqlCommand SqlCmd = new SqlCommand())
                {
                    SqlCmd.Parameters.Add("@COPCD", SqlDbType.Char).Value = PX_COMMONData.COPCD;
                    SqlCmd.Parameters.Add("@SYSID", SqlDbType.VarChar).Value = PX_COMMONData.SYSID;
                    SqlCmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = PX_COMMONData.USERID;
                    retCount = DbAccess.SQLUpdateParameter(CmdTxt.ToString(), SqlCmd);
                }
                if (retCount>0)
                {
                    CmdTxt = new StringBuilder();
                    CmdTxt.AppendLine("UPDATE P3AS_USER SET ");
                    CmdTxt.AppendLine("  ENDCOPCD = @COPCD ");
                    CmdTxt.AppendLine(" WHERE USERID = @USERID ");

                    using (SqlCommand SqlCmd = new SqlCommand())
                    {
                        SqlCmd.Parameters.Add("@COPCD", SqlDbType.Char).Value = PX_COMMONData.COPCD;
                        SqlCmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = PX_COMMONData.USERID;
                        retCount = DbAccess.SQLUpdateParameter(CmdTxt.ToString(), SqlCmd);
                    }
                }
                if (retCount > 0)
                {
                    DbAccess.Tran(PXCL_dba.TRNS_MODE.COMMIT);
                }
                else
                {
                    DbAccess.Tran(PXCL_dba.TRNS_MODE.ROLLBACK);
                }
            }
            catch (Exception Exc)
            {
                string LogTitle = "プロセスID：LOGIN";
                string LogMsg = "エラー「" + Exc.Message + "」";
                PXCL_log.writeLog(PXCL_log.ERR, PXCL_log.SELECT, LogTitle, LogMsg, System.Reflection.MethodBase.GetCurrentMethod(), PX_COMMONData);
                retCount = -1;
                DbAccess.Tran(PXCL_dba.TRNS_MODE.ROLLBACK);
            }
            finally
            {
                //データベースの接続解除
                DbAccess.DBClose();
            }
            return retCount;
        }

    }


}
