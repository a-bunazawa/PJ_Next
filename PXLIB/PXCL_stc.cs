using System;
using System.Collections.Generic;
using System.Text;

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
            protected string ServerURL { get; set; }
            /// <summary>ログインURL</summary>
            protected string LoginURL { get; set; }
            /// <summary>接続URL</summary>
            protected string AccessURL { get; set; }
            /// <summary>クライアントＩＤ</summary>
            protected string ClientIP { get; set; }
            /// <summary>クライアントホスト名</summary>
            protected string ClientHostName { get; set; }
            /// <summary>ユーザＩＤ</summary>
            protected string UserID { get; set; }
            /// <summary>ユーザ名</summary>
            protected string UserName { get; set; }
            /// <summary>会社コード区分</summary>
            protected string DefCompanyTp { get; set; }
            /// <summary>会社コード</summary>
            protected string CompanyCD { get; set; }
            /// <summary>会社名</summary>
            protected string CompanyName { get; set; }
            /// <summary>システムID</summary>
            protected string SystemID { get; set; }
            /// <summary>システムDB名</summary>
            protected string SystemDBName { get; set; }
            /// <summary>システムサーバ名</summary>
            protected string SystemDBServerName { get; set; }
            /// <summary>システムサーバIP</summary>
            protected string SystemDBServerIP { get; set; }
            /// <summary>システムDBユーザ名</summary>
            protected string SystemDBUserName { get; set; }
            /// <summary>システムDBパスワード</summary>
            protected string SystemDBPass { get; set; }
            /// <summary>ユーザDB名</summary>
            protected string UserDBName { get; set; }
            /// <summary>ユーザサーバ名</summary>
            protected string UserDBServerName { get; set; }
            /// <summary>ユーザサーバIP</summary>
            protected string UserDBServerIP { get; set; }
            /// <summary>ユーザDBユーザ名</summary>
            protected string UserDBUserName { get; set; }
            /// <summary>ユーザDBパスワード</summary>
            protected string UserDBPass { get; set; }
            /// <summary>システム分類</summary>
            protected string SystemType { get; set; }
            /// <summary>システム種別</summary>
            protected string SystemGroup { get; set; }
            /// <summary>メニューＩＤ</summary>
            protected string MenuID { get; set; }
            /// <summary>権限パラメータ区分</summary>
            protected string AuthoritySection { get; set; }
            /// <summary>初期荷主コード</summary>
            protected string InitCargoOwnerCD { get; set; }
            /// <summary>初期部署コード</summary>
            protected string InitUnitCD { get; set; }
            /// <summary>初期倉庫コード</summary>
            protected string InitWarehouseCD { get; set; }
            /// <summary>初期企業コード</summary>
            protected string InitCompanyCD { get; set; }
            /// <summary>初期取引先コード</summary>
            protected string InitCustomerCD { get; set; }
            /// <summary>初期出荷先コード</summary>
            protected string InitShipToCD { get; set; }
            /// <summary>確認メッセージ</summary>
            protected string ConfirmingMessage { get; set; }
            /// <summary>ログイン時刻</summary>
            protected string LoginTime { get; set; }
            /// <summary>ユーザ種別</summary>
            protected string UserType { get; set; }
            /// <summary>自動ログインフラグ</summary>
            protected bool FlgAutoLogin { get; set; }
            /// <summary>リンク元URL</summary>
            protected string RefUrl { get; set; }
            /// <summary>Byteチェックフラグ</summary>
            protected string ByteCheckFlg { get; set; }
            /// <summary> ログイン遷移先 </summary>
            protected string CallTop { get; set; }

            public PX_COMMON()
            {
                ServerURL = "";
                LoginURL = "";
                AccessURL = "";
                ClientIP = "";
                ClientHostName = "";
                UserID = "";
                UserName = "";
                DefCompanyTp = "";
                CompanyCD = "";
                CompanyName = "";
                SystemID = "";
                SystemDBName = "";
                SystemDBServerName = "";
                SystemDBServerIP = "";
                SystemDBUserName = "";
                SystemDBPass = "";
                UserDBName = "";
                UserDBServerName = "";
                UserDBServerIP = "";
                UserDBUserName = "";
                UserDBPass = "";
                SystemType = "";
                SystemGroup = "";
                MenuID = "";
                AuthoritySection = "";
                InitCargoOwnerCD = "";
                InitUnitCD = "";
                InitWarehouseCD = "";
                InitCompanyCD = "";
                InitCustomerCD = "";
                InitShipToCD = "";
                ConfirmingMessage = "";
                LoginTime = "";
                UserType = "";
                FlgAutoLogin = false;
                RefUrl = "";
                ByteCheckFlg = "";
                CallTop = "";
            }

            /// <summary>サーバURL</summary>
            public string SERVERURL { get { return ServerURL; } set { ServerURL = value; } }
            /// <summary>ログインURL</summary>
            public string LOGINURL { get { return LoginURL; } set { LoginURL = value; } }
            /// <summary>ログイン時刻</summary>
            public string LOGINTIME { get { return LoginTime; } set { LoginTime = value; } }
            /// <summary>接続URL</summary>
            public string ACCESSURL { get { return AccessURL; } set { AccessURL = value; } }
            /// <summary>クライアントＩＤ</summary>
            public string CLIENTIP { get { return ClientIP; } set { ClientIP = value; } }
            /// <summary>クライアントホスト名</summary>
            public string CLIENTHNM { get { return ClientHostName; } set { ClientHostName = value; } }
            /// <summary>ユーザＩＤ</summary>
            public string USERID { get { return UserID; } set { UserID = value; } }
            /// <summary>ユーザ名</summary>
            public string USERNM { get { return UserName; } set { UserName = value; } }
            /// <summary>会社コード区分</summary>
            public string DEFCOPTP { get { return DefCompanyTp; } set { DefCompanyTp = value; } }
            /// <summary>会社コード</summary>
            public string COPCD { get { return CompanyCD; } set { CompanyCD = value; } }
            /// <summary>会社名</summary>
            public string COPNM { get { return CompanyName; } set { CompanyName = value; } }
            /// <summary>システムID</summary>
            public string SYSID { get { return SystemID; } set { SystemID = value; } }
            /// <summary>システムDB名</summary>
            public string SYSDBNM { get { return SystemDBName; } set { SystemDBName = value; } }
            /// <summary>システムサーバ名</summary>
            public string SYSDBSVRNM { get { return SystemDBServerName; } set { SystemDBServerName = value; } }
            /// <summary>システムサーバIP</summary>
            public string SYSDBSVRIP { get { return SystemDBServerIP; } set { SystemDBServerIP = value; } }
            /// <summary>システムDBユーザ名</summary>
            public string SYSDBSVRUR { get { return SystemDBUserName; } set { SystemDBUserName = value; } }
            /// <summary>システムDBパスワード</summary>
            public string SYSDBSVRPW { get { return SystemDBPass; } set { SystemDBPass = value; } }
            /// <summary>ユーザDB名</summary>
            public string USERDBNM { get { return UserDBName; } set { UserDBName = value; } }
            /// <summary>ユーザサーバ名</summary>
            public string USERDBSVRNM { get { return UserDBServerName; } set { UserDBServerName = value; } }
            /// <summary>ユーザサーバIP</summary>
            public string USERDBSVRIP { get { return UserDBServerIP; } set { UserDBServerIP = value; } }
            /// <summary>ユーザDBユーザ名</summary>
            public string USERDBSVRUR { get { return UserDBUserName; } set { UserDBUserName = value; } }
            /// <summary>ユーザDBパスワード</summary>
            public string USERDBSVRPW { get { return UserDBPass; } set { UserDBPass = value; } }
            /// <summary>システム分類</summary>
            public string SYSTP { get { return SystemType; } set { SystemType = value; } }
            /// <summary>システム種別</summary>
            public string SYSGRP { get { return SystemGroup; } set { SystemGroup = value; } }
            /// <summary>メニューＩＤ</summary>
            public string MENUID { get { return MenuID; } set { MenuID = value; } }
            /// <summary>権限パラメータ区分</summary>
            public string AUTKBN { get { return AuthoritySection; } set { AuthoritySection = value; } }
            /// <summary>初期荷主コード</summary>
            public string INIGRPCD { get { return InitCargoOwnerCD; } set { InitCargoOwnerCD = value; } }
            /// <summary>初期部署コード</summary>
            public string INIDPTCD { get { return InitUnitCD; } set { InitUnitCD = value; } }
            /// <summary>初期倉庫コード</summary>
            public string INIWHSCD { get { return InitWarehouseCD; } set { InitWarehouseCD = value; } }
            /// <summary>初期企業コード</summary>
            public string INICMPCD { get { return InitCompanyCD; } set { InitCompanyCD = value; } }
            /// <summary>初期取引先コード</summary>
            public string INICSTCD { get { return InitCustomerCD; } set { InitCustomerCD = value; } }
            /// <summary>初期出荷先コード</summary>
            public string INISHPCD { get { return InitShipToCD; } set { InitShipToCD = value; } }
            /// <summary>確認メッセージ</summary>
            public string ACPTTP { get { return ConfirmingMessage; } set { ConfirmingMessage = value; } }
            /// <summary>ユーザ種別</summary>
            public string USERTYPE { get { return UserType; } set { UserType = value; } }
            /// <summary>自動ログインフラグ</summary>
            public bool FLGAUTOLOGIN { get { return FlgAutoLogin; } set { FlgAutoLogin = value; } }
            /// <summary>リンク元URL</summary>
            public string REFURL { get { return RefUrl; } set { RefUrl = value; } }
            /// <summary> DB情報パラメーター </summary>
            public string DBINF { get; set; }
            /// <summary> Byteチェックフラグ </summary>
            public string BYTECHECKFLG { get { return ByteCheckFlg; } set { ByteCheckFlg = value; } }
            /// <summary> ログイン遷移先 </summary>
            public string CALLTOP { get { return CallTop; } set { CallTop = value; } }
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

    }
}
