using System;
using Newtonsoft.Json;


namespace PXLIB
{
    public class PXCL_stc
    {
        /// <summary> 共通値管理 </summary>
        public class PX_COMMON
        {
            /// <summary>Sessionへの保存名</summary>
            public static string SessionName = "ShareData";

            /// <summary>サーバURL</summary>
            [JsonProperty(PropertyName = "SERVERURL")]
            public string SERVERURL { get; set; }
            /// <summary>ログインURL</summary>
            [JsonProperty(PropertyName = "LOGINURL")]
            public string LOGINURL { get; set; }
            /// <summary>ログイン時刻</summary>
            [JsonProperty(PropertyName = "LOGINTIME")]
            public string LOGINTIME { get; set; }
            /// <summary>接続URL</summary>
            [JsonProperty(PropertyName = "ACCESSURL")]
            public string ACCESSURL { get; set; }
            /// <summary>クライアントＩＤ</summary>
            [JsonProperty(PropertyName = "CLIENTIP")]
            public string CLIENTIP { get; set; }
            /// <summary>クライアントホスト名</summary>
            [JsonProperty(PropertyName = "CLIENTHNM")]
            public string CLIENTHNM { get; set; }

            /// <summary>ユーザ種別</summary>
            /// 
            public enum USER_TYPE
            {
                ONETIME,
                NORMAL
            }
            protected string USERTP { get; set; }
            /// <summary>ユーザＩＤ</summary>
            [JsonProperty(PropertyName = "USERID")]
            public string USERID { get; set; }
            /// <summary>ユーザ名</summary>
            [JsonProperty(PropertyName = "USERNM")]
            public string USERNM { get; set; }
            /// <summary>会社コード区分</summary>
            [JsonProperty(PropertyName = "DEFCOPTP")]
            public string DEFCOPTP { get; set; }
            /// <summary>会社コード</summary>
            [JsonProperty(PropertyName = "COPCD")]
            public string COPCD { get; set; }
            /// <summary>会社名</summary>
            [JsonProperty(PropertyName = "COPNM")]
            public string COPNM { get; set; }
            /// <summary>システムID</summary>
            [JsonProperty(PropertyName = "SYSID")]
            public string SYSID { get; set; }
            /// <summary>システムDB名</summary>
            [JsonProperty(PropertyName = "SYSDBNM")]
            public string SYSDBNM { get; set; }
            /// <summary>システムサーバ名</summary>
            [JsonProperty(PropertyName = "SYSDBSVRNM")]
            public string SYSDBSVRNM { get; set; }
            /// <summary>システムサーバIP</summary>
            [JsonProperty(PropertyName = "SYSDBSVRIP")]
            public string SYSDBSVRIP { get; set; }
            /// <summary>システムDBユーザ名</summary>
            [JsonProperty(PropertyName = "SYSDBSVRUR")]
            public string SYSDBSVRUR { get; set; }
            /// <summary>システムDBパスワード</summary>
            [JsonProperty(PropertyName = "SYSDBSVRPW")]
            public string SYSDBSVRPW { get; set; }
            /// <summary>ユーザDB名</summary>
            [JsonProperty(PropertyName = "USERDBNM")]
            public string USERDBNM { get; set; }
            [JsonProperty(PropertyName = "USERDBSVRNM")]
            public string USERDBSVRNM { get; set; }
            /// <summary>ユーザサーバIP</summary>
            [JsonProperty(PropertyName = "USERDBSVRIP")]
            public string USERDBSVRIP { get; set; }
            /// <summary>ユーザDBユーザ名</summary>
            [JsonProperty(PropertyName = "USERDBSVRUR")]
            public string USERDBSVRUR { get; set; }
            /// <summary>ユーザDBパスワード</summary>
            [JsonProperty(PropertyName = "USERDBSVRPW")]
            public string USERDBSVRPW { get; set; }
            /// <summary>システム分類</summary>
            [JsonProperty(PropertyName = "SYSTP")]
            public string SYSTP { get; set; }
            /// <summary>システム種別</summary>
            [JsonProperty(PropertyName = "SYSGRP")]
            public string SYSGRP { get; set; }
            /// <summary>メニューＩＤ</summary>
            [JsonProperty(PropertyName = "MENUID")]
            public string MENUID { get; set; }
            /// <summary>権限パラメータ区分</summary>
            [JsonProperty(PropertyName = "AUTKBN")]
            public string AUTKBN { get; set; }
            /// <summary>初期荷主コード</summary>
            [JsonProperty(PropertyName = "INIGRPCD")]
            public string INIGRPCD { get; set; }
            /// <summary>初期部署コード</summary>
            [JsonProperty(PropertyName = "INIDPTCD")]
            public string INIDPTCD { get; set; }
            /// <summary>初期倉庫コード</summary>
            [JsonProperty(PropertyName = "INIWHSCD")]
            public string INIWHSCD { get; set; }
            /// <summary>初期企業コード</summary>
            [JsonProperty(PropertyName = "USERDBNM")]
            public string INICMPCD { get; set; }
            /// <summary>ユーザサーバ名</summary>
            /// <summary>初期取引先コード</summary>
            [JsonProperty(PropertyName = "INICSTCD")]
            public string INICSTCD { get; set; }
            /// <summary>初期出荷先コード</summary>
            [JsonProperty(PropertyName = "INISHPCD")]
            public string INISHPCD { get; set; }
            /// <summary>確認メッセージ</summary>
            [JsonProperty(PropertyName = "ACPTTP")]
            public string ACPTTP { get; set; }
            /// <summary>ユーザ種別</summary>
            [JsonProperty(PropertyName = "USERTYPE")]
            public string USERTYPE { get; set; }
            /// <summary>自動ログインフラグ</summary>
            [JsonProperty(PropertyName = "FLGAUTOLOGIN")]
            public bool FLGAUTOLOGIN { get; set; }
            /// <summary>リンク元URL</summary>
            [JsonProperty(PropertyName = "REFURL")]
            public string REFURL { get; set; }
            /// <summary> DB情報パラメーター </summary>
            [JsonProperty(PropertyName = "DBINF")]
            public string DBINF { get; set; }
            /// <summary> Byteチェックフラグ </summary>
            [JsonProperty(PropertyName = "BYTECHECKFLG")]
            public string BYTECHECKFLG { get; set; }
            /// <summary> ログイン遷移先 </summary>
            [JsonProperty(PropertyName = "CALLTOP")]
            public string CALLTOP { get; set; }
            /// <summary>  </summary>
            [JsonProperty(PropertyName = "SYSVERNO")]
            public string SYSVERNO { get; set; }

            /// <summary>エラーステータス(空の場合正常)およびエラーコード</summary>
            public string ERRORCODE { get; set; }
            public string ERRORMSG { get; set; }

            public PX_COMMON()
            {
                Init_AllData();
            }
            public PX_COMMON(string typename, PX_COMMON data)
            {
                Init_AllData();

                switch (typename)
                {
                    case "JsonGetDialogData":
                        DBINF = data.DBINF;
                        COPCD = data.COPCD;
                        USERID = data.USERID;
                        MENUID = data.MENUID;
                        USERDBSVRNM = data.USERDBSVRNM;
                        break;
                }
            }

            private void Init_AllData()
            {
                SERVERURL = "";
                LOGINURL = "";
                LOGINTIME = "";
                ACCESSURL = "";
                CLIENTIP = "";
                CLIENTHNM = "";
                USERTP = Enum.GetName(typeof(USER_TYPE), USER_TYPE.NORMAL);
                USERID = "";
                USERNM = "";
                DEFCOPTP = "";
                COPCD = "";
                COPNM = "";
                SYSID = "";
                SYSDBNM = "";
                SYSDBSVRNM = "";
                SYSDBSVRIP = "";
                SYSDBSVRUR = "";
                SYSDBSVRPW = "";
                USERDBNM = "";
                USERDBSVRNM = "";
                USERDBSVRIP = "";
                USERDBSVRUR = "";
                USERDBSVRPW = "";
                SYSTP = "";
                SYSGRP = "";
                MENUID = "";
                AUTKBN = "";
                INIGRPCD = "";
                INIDPTCD = "";
                INIWHSCD = "";
                INICSTCD = "";
                INISHPCD = "";
                ACPTTP = "";
                USERTYPE = "";
                REFURL = "";
                FLGAUTOLOGIN = false;
                DBINF = "";
                BYTECHECKFLG = "";
                CALLTOP = "";
                ERRORCODE = "";
                ERRORMSG = "";
            }
        }

        /// <summary> 権限マスタ </summary>
        public class PX_PROGRAM_AUT
        {
            /// <summary>制御区分：参照権限	</summary>
            protected string Access { get; set; }
            /// <summary>制御区分：登録権限	</summary>
            protected string Insert { get; set; }
            /// <summary>制御区分：更新権限	</summary>
            protected string Update { get; set; }
            /// <summary>制御区分：削除権限	</summary>
            protected string Delete { get; set; }
            /// <summary>制御区分：処理権限	</summary>
            protected string Processing { get; set; }
            /// <summary>制御区分：特殊権限	</summary>
            protected string Special { get; set; }
            /// <summary>制御区分：予備１</summary>
            protected string Etc1 { get; set; }
            /// <summary>制御区分：予備２</summary>
            protected string Etc2 { get; set; }

            public PX_PROGRAM_AUT()
            {
                Access = "";
                Insert = "";
                Update = "";
                Delete = "";
                Processing = "";
                Special = "";
                Etc1 = "";
                Etc2 = "";
            }

            /// <summary>制御区分：参照権限	</summary>
            public string AUTCTLDSP { get { return Access; } set { Access = value; } }
            /// <summary>制御区分：登録権限	</summary>
            public string AUTCTLINS { get { return Insert; } set { Insert = value; } }
            /// <summary>制御区分：更新権限	</summary>
            public string AUTCTLMNT { get { return Update; } set { Update = value; } }
            /// <summary>制御区分：削除権限	</summary>
            public string AUTCTLDEL { get { return Delete; } set { Delete = value; } }
            /// <summary>制御区分：処理権限	</summary>
            public string AUTCTLPRC { get { return Processing; } set { Processing = value; } }
            /// <summary>制御区分：特殊権限	</summary>
            public string AUTCTLSPC { get { return Special; } set { Special = value; } }
            /// <summary>制御区分：予備１</summary>
            public string AUTCTLSUB1 { get { return Etc1; } set { Etc1 = value; } }
            /// <summary>制御区分：予備２</summary>
            public string AUTCTLSUB2 { get { return Etc2; } set { Etc2 = value; } }
        }

        public class PXAS_AppSetCL
        {
            public PXAS_DomainCL Knet { get; set; }
            public PXAS_DomainCL AAA { get; set; }
            public PXAS_DomainCL BBB { get; set; }

        }
        public class PXAS_DomainCL
        {
            public string SysDB { get; set; }
            public string SysURL { get; set; }

            public string SVRName { get; set; }
            public string SVRIP { get; set; }
            public string DBName { get; set; }
            public string DBUser { get; set; }
            public string DBPass { get; set; }

            public string COPCD { get; set; }
            public string SYSPARAID1 { get; set; }
            public string SYSPARAID2 { get; set; }
            public string SYSPARAID3 { get; set; }

        }

        public class PX_SYSPARA
        {
            public string COPCD { get; set; }
            public string SYSPARAID1 { get; set; }
            public string SYSPARAID2 { get; set; }
            public string SYSPARAID3 { get; set; }
            public int SYSPARASEQ { get; set; }
            public string SYSPARANM { get; set; }
            public int DSPSORT { get; set; }
            public string CTLCD1 { get; set; }
            public string CTLCD2 { get; set; }
            public string CTLCD3 { get; set; }
            public string CTLCD4 { get; set; }
            public string CTLCD5 { get; set; }
            public string CTLPARA1 { get; set; }
            public string CTLPARA2 { get; set; }
            public string CTLPARA3 { get; set; }
            public string CTLPARA4 { get; set; }
            public string CTLPARA5 { get; set; }
            public decimal CTLSEQ1 { get; set; }
            public decimal CTLSEQ2 { get; set; }
            public decimal CTLSEQ3 { get; set; }
            public decimal CTLSEQ4 { get; set; }
            public decimal CTLSEQ5 { get; set; }
            public decimal CTLQNT1 { get; set; }
            public decimal CTLQNT2 { get; set; }
            public decimal CTLQNT3 { get; set; }
            public decimal CTLQNT4 { get; set; }
            public decimal CTLQNT5 { get; set; }
            public string CTLFLG1 { get; set; }
            public string CTLFLG2 { get; set; }
            public string CTLFLG3 { get; set; }
            public string CTLFLG4 { get; set; }
            public string CTLFLG5 { get; set; }
            public DateTime? CTLDATE1 { get; set; }
            public DateTime? CTLDATE2 { get; set; }
            public DateTime? CTLDATE3 { get; set; }
            public DateTime? CTLDATE4 { get; set; }
            public DateTime? CTLDATE5 { get; set; }
            public string GUIDENM { get; set; }
            public string SUBCMM { get; set; }

            public PX_SYSPARA()
            {
                COPCD = "";
                SYSPARAID1 = "";
                SYSPARAID2 = "";
                SYSPARAID3 = "";
                SYSPARASEQ = 0;
                SYSPARANM = "";
                DSPSORT = 0;
                CTLCD1 = "";
                CTLCD2 = "";
                CTLCD3 = "";
                CTLCD4 = "";
                CTLCD5 = "";
                CTLPARA1 = "";
                CTLPARA2 = "";
                CTLPARA3 = "";
                CTLPARA4 = "";
                CTLPARA5 = "";
                CTLSEQ1 = 0;
                CTLSEQ2 = 0;
                CTLSEQ3 = 0;
                CTLSEQ4 = 0;
                CTLSEQ5 = 0;
                CTLQNT1 = 0.00M;
                CTLQNT2 = 0.00M;
                CTLQNT3 = 0.00M;
                CTLQNT4 = 0.00M;
                CTLQNT5 = 0.00M;
                CTLFLG1 = "";
                CTLFLG2 = "";
                CTLFLG3 = "";
                CTLFLG4 = "";
                CTLFLG5 = "";
                CTLDATE1 = null;
                CTLDATE2 = null;
                CTLDATE3 = null;
                CTLDATE4 = null;
                CTLDATE5 = null;
                GUIDENM = null;
                SUBCMM = null;
            }
        }
        public class PX_PJ3CONFIG
        {
            /// <summary>タイトルロゴ：ファイル名を設定。未設定の場合、タイトルロゴは表示されない</summary>
            public string TITLEPIC { get; set; }
            /// <summary>画面背景：ファイル名を設定。未設定の場合、背景は表示されない(白)</summary>
            public string BGPIC { get; set; }
            /// <summary>アカウント追加ボタンの表示：[アカウント追加]ボタンの表示の有無</summary>
            public string ADDACCOUNT { get; set; }
            /// <summary>アカウント追加ボタンの表示：[ログイン状態の維持]のチェック有無(デフォルト)</summary>
            public string KEEPLOGIN { get; set; }
            /// <summary>インフォメーション表示：[表示テキスト]    ※未設定の場合は、テキスト表示なし</summary>
            public string VIEWINFO { get; set; }
            /// <summary>
            /// デフォルトの言語コード：[言語コード] のデフォルト
            /// 　※未設定の場合は、プルダウンの1件目とする
            /// </summary>
            public string DEFLANG { get; set; }
            /// <summary>
            /// ヘッダーフッターのカラーコード：[カラーコード]　
            /// ※未設定の場合は、デフォルトのカラーコードとする（#F4F4F4）
            /// </summary>
            public string FLAMECOLOR { get; set; }
            /// <summary>ユーザIDのポリシー：[ユーザIDのポリシー:文字列制御]</summary>
            public string IDCHAR { get; set; }
            /// <summary>ユーザIDのポリシー：ユーザIDの最低文字数　※0の場合は最低制限なし</summary>
            public int IDLENMIN { get; set; }
            /// <summary>ユーザIDのポリシー：ユーザIDの最大文字数　※0の場合は最大制限なし</summary>
            public int IDLENMAX { get; set; }
            /// <summary>パスワードのポリシー：[パスワードのポリシー:文字列制御]</summary>
            public string PACHAR { get; set; }
            /// <summary>パスワードのポリシー：ユーザIDの最低文字数　※0の場合は最低制限なし</summary>
            public int PALENMIN { get; set; }
            /// <summary>パスワードのポリシー：ユーザIDの最大文字数　※0の場合は最低制限なし</summary>
            public int PALENMAX { get; set; }
            /// <summary>パスワードロック処理：パスワード入力回数エラー発生時の処理</summary>
            public string PALOCACT { get; set; }
            /// <summary>パスワードロック処理：パスワード入力回数エラーの基準回数</summary>
            public int PALOCCNT { get; set; }

            /// <summary>起動元タイプ</summary>
            public enum BROWSE_TYPE
            {
                BWS,  // ブラウザ
                SSO,   // SSO
                MOB,   // モバイル端末
                OTH    // その他 
            }
            /// <summary>起動元</summary>
            public string BROWSETP { get; set; }

            /// <summary>システム起動：</summary>
            public enum PAGE_TYPE
            {
                MAN,  // 管理者サイト(ReOSYS/Filix/K-NET/WMS)
                USE    // ユーザーサイト(ReOSYS/Filix)
            }
            /// <summary>システム起動</summary>
            public string PAGETP { get; set; }

            // *CAPTION * //
            /// <summary>[ログイン]</summary>
            public string LOGIN { get; set; }
            /// <summary>[ユーザーID]</summary>
            public string USERID { get; set; }
            /// <summary>[パスワード]</summary>
            public string USERPASS { get; set; }
            /// <summary>[ロパスワードを忘れた方へ]</summary>
            public string PASSFORGET { get; set; }
            /// <summary>[ログイン情報を保持]</summary>
            public string LOGINKEEP { get; set; }
            /// <summary>[アカウントを作成しますか？]</summary>
            public string USERADD { get; set; }

            /// <summary>タイトルロゴ</summary>
            public PX_PJ3CONFIG()
            {
                BROWSETP = Enum.GetName(typeof(BROWSE_TYPE), BROWSE_TYPE.BWS);
                PAGETP = Enum.GetName(typeof(PAGE_TYPE), PAGE_TYPE.MAN);  // Defaultは管理者里とする
            }
        }

        //public class PX_USERCTL
        //{
        //    //public string COPCD { get; set; }
        //    //public string SYSID { get; set; }
        //    //public string USERID { get; set; }
        //    public string MENUID { get; set; }
        //    //public string MENUPATH { get; set; }
        //    public string AUTKBN { get; set; }
        //    //public string GRPSELTP { get; set; }
        //    public string INIGRPCD { get; set; }
        //    public string INIDPTCD { get; set; }
        //    public string INIWHSCD { get; set; }
        //    public string INICMPCD { get; set; }
        //    public string INICSTCD { get; set; }
        //    public string INISHPCD { get; set; }
        //    //public string ADRCD1 { get; set; }
        //    //public string ADRCD2 { get; set; }
        //    //public decimal ODRDDMAXWGT { get; set; }
        //    //public decimal ODRDDMAXAMT { get; set; }
        //    //public decimal ODRDDMAXSUB { get; set; }
        //    //public decimal ODRWKMAXWGT { get; set; }
        //    //public decimal ODRWKMAXAMT { get; set; }
        //    //public decimal ODRWKMAXSUB { get; set; }
        //    //public decimal ODRMMMAXWGT { get; set; }
        //    //public decimal ODRMMMAXAMT { get; set; }
        //    //public decimal ODRMMMAXCNT { get; set; }
        //    //public decimal ODRMMMAXSUB { get; set; }
        //    //public string ODRMMMAXMON { get; set; }
        //    //public decimal ODRYYMAXWGT { get; set; }
        //    //public decimal ODRYYMAXAMT { get; set; }
        //    //public decimal ODRYYMAXCNT { get; set; }
        //    //public decimal ODRYYMAXSUB { get; set; }
        //    //public string ODRYYMAXSMD { get; set; }
        //    //public decimal ODRSZMAXWGT { get; set; }
        //    //public decimal ODRSZMAXAMT { get; set; }
        //    //public decimal ODRSZMAXSUB { get; set; }
        //    //public string USERLMTYMD { get; set; }
        //    public string ACPTTP { get; set; }
        //    //public string LOGDUPKBN { get; set; }
        //    //public string LOGINSTS { get; set; }
        //    //public DateTime? LOGINDATE { get; set; }
        //    //public DateTime? LOGOUTDATE { get; set; }
        //    //public int LOGINNGCNT { get; set; }
        //    //public DateTime? LOGINTRYDATE { get; set; }
        //    //public string USERSTS { get; set; }
        //    //public string SUBCMM { get; set; }

        //    public PX_USERCTL()
        //    {
        //        COPCD = "";
        //        SYSID = "";
        //        USERID = "";
        //        MENUID = "";
        //        MENUPATH = "";
        //        AUTKBN = "";
        //        GRPSELTP = "";
        //        INIGRPCD = "";
        //        INIDPTCD = "";
        //        INIWHSCD = "";
        //        INICMPCD = "";
        //        INICSTCD = "";
        //        INISHPCD = "";
        //        ADRCD1 = "";
        //        ADRCD2 = "";
        //        ODRDDMAXWGT = 0;
        //        ODRDDMAXAMT = 0;
        //        ODRDDMAXSUB = 0;
        //        ODRWKMAXWGT = 0;
        //        ODRWKMAXAMT = 0;
        //        ODRWKMAXSUB = 0;
        //        ODRMMMAXWGT = 0;
        //        ODRMMMAXAMT = 0;
        //        ODRMMMAXCNT = 0;
        //        ODRMMMAXSUB = 0;
        //        ODRMMMAXMON = "";
        //        ODRYYMAXWGT = 0;
        //        ODRYYMAXAMT = 0;
        //        ODRYYMAXCNT = 0;
        //        ODRYYMAXSUB = 0;
        //        ODRYYMAXSMD = "";
        //        ODRSZMAXWGT = 0;
        //        ODRSZMAXAMT = 0;
        //        ODRSZMAXSUB = 0;
        //        USERLMTYMD = "";
        //        ACPTTP = "";
        //        LOGDUPKBN = "";
        //        LOGINSTS = "";
        //        LOGINDATE = null;
        //        LOGOUTDATE = null;
        //        LOGINNGCNT = 0;
        //        LOGINTRYDATE = null;
        //        USERSTS = "";
        //        SUBCMM = "";
        //    }
        //}

        /// <summary>
        /// 今は使わないけど後々使用（2018/02/22現在）
        /// </summary>
        public class PX_USEROPT
        {
            public string COPCD { get; set; }
            public string SYSID { get; set; }
            public string USERID { get; set; }
            public string USERSEX { get; set; }
            public string BRTDYMD { get; set; }
            public string RECPYMD { get; set; }
            public string JOINYMD { get; set; }
            public string JOINACPTID { get; set; }
            public string APPYM { get; set; }
            public string CONTNAMENM1 { get; set; }
            public string CONTNAMENM1C { get; set; }
            public string CONTTEL11 { get; set; }
            public string CONTTEL12 { get; set; }
            public string CONTRLTN1 { get; set; }
            public string CONTNAMENM2 { get; set; }
            public string CONTNAMENM2C { get; set; }
            public string CONTTEL21 { get; set; }
            public string CONTTEL22 { get; set; }
            public string CONTRLTN2 { get; set; }
            public string FAMPRTNKBN { get; set; }
            public string FAMPRTNYMFR { get; set; }
            public string FAMPRTNYMTO { get; set; }
            public string FAMCHLDKBN { get; set; }
            public string FAMCHLDYMFR1 { get; set; }
            public string FAMCHLDYMTO1 { get; set; }
            public string FAMCHLDYMFR2 { get; set; }
            public string FAMCHLDYMTO2 { get; set; }
            public string FAMCHLDYMFR3 { get; set; }
            public string FAMCHLDYMTO3 { get; set; }
            public string FAMCHLDYMFR4 { get; set; }
            public string FAMCHLDYMTO4 { get; set; }
            public string FAMCHLDYMD1 { get; set; }
            public int FAMCHLDQNT { get; set; }
            public int FAMGSONQNT { get; set; }
            public int FAMHMATQNT { get; set; }
            public string MEMBERCD1 { get; set; }
            public string MEMBERCD2 { get; set; }
            public string MEMBERCD3 { get; set; }
            public string MEMBERCD4 { get; set; }
            public string MEMBERCD5 { get; set; }
            public string MEMBERFLG1 { get; set; }
            public string MEMBERFLG2 { get; set; }
            public string MEMBERFLG3 { get; set; }
            public string MEMBERFLG4 { get; set; }
            public string MEMBERFLG5 { get; set; }
            public string MEMPROCFLG1 { get; set; }
            public string MEMPROCFLG2 { get; set; }
            public string MEMPROCFLG3 { get; set; }
            public string MEMPROCFLG4 { get; set; }
            public string MEMPROCFLG5 { get; set; }
            public string LOGPROCFLG1 { get; set; }
            public string LOGPROCFLG2 { get; set; }
            public string LOGPROCFLG3 { get; set; }
            public string LOGPROCFLG4 { get; set; }
            public string LOGPROCFLG5 { get; set; }
            public string QUESTANSFLG01 { get; set; }
            public string QUESTANSFLG02 { get; set; }
            public string QUESTANSFLG03 { get; set; }
            public string QUESTANSFLG04 { get; set; }
            public string QUESTANSFLG05 { get; set; }
            public string QUESTANSFLG06 { get; set; }
            public string QUESTANSFLG07 { get; set; }
            public string QUESTANSFLG08 { get; set; }
            public string QUESTANSFLG09 { get; set; }
            public string QUESTANSFLG10 { get; set; }
            public string QUESTANSCMM1 { get; set; }
            public string QUESTANSCMM2 { get; set; }
            public string QUESTANSCMM3 { get; set; }


            public PX_USEROPT()
            {
                COPCD = "";
                SYSID = "";
                USERID = "";
                USERSEX = "";
                BRTDYMD = "";
                RECPYMD = "";
                JOINYMD = "";
                JOINACPTID = "";
                APPYM = "";
                CONTNAMENM1 = "";
                CONTNAMENM1C = "";
                CONTTEL11 = "";
                CONTTEL12 = "";
                CONTRLTN1 = "";
                CONTNAMENM2 = "";
                CONTNAMENM2C = "";
                CONTTEL21 = "";
                CONTTEL22 = "";
                CONTRLTN2 = "";
                FAMPRTNKBN = "";
                FAMPRTNYMFR = "";
                FAMPRTNYMTO = "";
                FAMCHLDKBN = "";
                FAMCHLDYMFR1 = "";
                FAMCHLDYMTO1 = "";
                FAMCHLDYMFR2 = "";
                FAMCHLDYMTO2 = "";
                FAMCHLDYMFR3 = "";
                FAMCHLDYMTO3 = "";
                FAMCHLDYMFR4 = "";
                FAMCHLDYMTO4 = "";
                FAMCHLDYMD1 = "";
                FAMCHLDQNT = 0;
                FAMGSONQNT = 0;
                FAMHMATQNT = 0;
                MEMBERCD1 = "";
                MEMBERCD2 = "";
                MEMBERCD3 = "";
                MEMBERCD4 = "";
                MEMBERCD5 = "";
                MEMBERFLG1 = "";
                MEMBERFLG2 = "";
                MEMBERFLG3 = "";
                MEMBERFLG4 = "";
                MEMBERFLG5 = "";
                MEMPROCFLG1 = "";
                MEMPROCFLG2 = "";
                MEMPROCFLG3 = "";
                MEMPROCFLG4 = "";
                MEMPROCFLG5 = "";
                LOGPROCFLG1 = "";
                LOGPROCFLG2 = "";
                LOGPROCFLG3 = "";
                LOGPROCFLG4 = "";
                LOGPROCFLG5 = "";
                QUESTANSFLG01 = "";
                QUESTANSFLG02 = "";
                QUESTANSFLG03 = "";
                QUESTANSFLG04 = "";
                QUESTANSFLG05 = "";
                QUESTANSFLG06 = "";
                QUESTANSFLG07 = "";
                QUESTANSFLG08 = "";
                QUESTANSFLG09 = "";
                QUESTANSFLG10 = "";
                QUESTANSCMM1 = "";
                QUESTANSCMM2 = "";
                QUESTANSCMM3 = "";
            }
        }


        public class PX_LANGUAGE
        {
            /// <summary>言語種別コード</summary>
            public string LANGCLCODE { get; set; }
            /// <summary>言語コード</summary>
            public string LANGCODE { get; set; }
            /// <summary>種別名</summary>
            public string LANGNAME { get; set; }
            /// <summary>フラグID</summary>
            public string FLAGID { get; set; }
            /// <summary>言語No.</summary>
            public string LANGNO { get; set; }
            /// <summary>表示順</summary>
            public int DSPSORT { get; set; }
            /// <summary>デフォルトの言語コード</summary>
            public string DEFLANGCODE { get; set; }

            public PX_LANGUAGE()
            {
                LANGCLCODE = "";
                LANGCODE = "";
                LANGNAME = "";
                FLAGID = "";
                LANGNO = "";
                DEFLANGCODE = "";
            }
            public PX_LANGUAGE(string DefFlag)
            {
                LANGCLCODE = "JPN";
                LANGCODE = "ja";
                LANGNAME = "日本語";
                FLAGID = "flag-jp";
                LANGNO = "1041";
                DEFLANGCODE = "ja";
            }

        }

        public class PX_CAPTIONLOGIN
        {
            /// <summary>[ログイン]</summary>
            public string LOGIN { get; set; }
            /// <summary>[ユーザーID]</summary>
            public string USERID { get; set; }
            /// <summary>[パスワード]</summary>
            public string USERPASS { get; set; }
            /// <summary>[ロパスワードを忘れた方へ]</summary>
            public string PASSFORGET { get; set; }
            /// <summary>[ログイン情報を保持]</summary>
            public string LOGINKEEP { get; set; }
            /// <summary>[アカウントを作成しますか？]</summary>
            public string USERADD { get; set; }

            public PX_CAPTIONLOGIN()
            {
                LOGIN = "";
                USERID = "";
                USERPASS = "";
                PASSFORGET = "";
                LOGINKEEP = "";
                USERADD = "";
            }
        }

        public class PX_CAPTION
        {
            /// <summary>[ログイン]</summary>
            public string CAPNAME { get; set; }
            /// <summary>[ユーザーID]</summary>
            public string CAPTION { get; set; }
        }


    }
}
