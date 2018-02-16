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
            protected string ClientHostName { get; set; }
            /// <summary>ユーザ種別</summary>
            /// 
            public enum USER_TYPE
            {
                ONEIME,
                NORMAL
            }
            protected USER_TYPE UserTP { get; set; }
            [JsonProperty(PropertyName = "CLIENTHNM")]
            public string CLIENTHNM { get; set; }
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
            /// <summary>エラーステータス(空の場合正常)およびエラーコード</summary>
            public string ERRORCODE { get; set; }

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
    }
}
