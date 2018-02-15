window.onresize = resizeWindow;

var domainType = "";
$(document).ready(function () {
    SetSysDB();
    resizeWindow();

    Ladda.bind('.button-demo button', { timeout: true });

    //レイアウトオプションの日本語化
    //LocalLayoutOption();

    $("#email").focus();
    $("input").keypress(function (e) {
        if (e.keyCode == 13) {
            $("#LoginBtn").click();
        }
    });

    //formタグによるサブミットの中止
    $('form').submit(function () {
        return false;
    });
});

// ブラウザサイズ変更処理
function resizeWindow() {
    // メイン窓サイズ変更
    //var windowWidth = document.body.clientWidth - 225;
    var windowHeight = document.documentElement.clientHeight;

    //if (windowWidth < 1050) { windowWidth = 1050; }
    if (windowHeight < 500) { windowHeight = 500; }
    //$("#mainWindow").width(windowWidth);
    $("#mainWindow").height(windowHeight);
};

function Login() {
    //var url = localStorage.getItem(domainType + "URL") + "api/LNAS0001/Login";
    var url = "http://kanda/KANDANET/API/api/LNAS0001/Login";

    var data = {};
    //data.Id = $("#email").val();
    data.Id = "pmt_admin";
    //data.Pass = $("#password").val();
    data.Pass = "kcom";
    //data.DBINF = localStorage.getItem(domainType + "SYSTEMDB");
    data.DB = localStorage.getItem(domainType + "SYSTEMDB");

    $.ajax(url, { type: "POST", data: data }).then(function (r) {

        if (r.SendUrl != "" && r.DomainType != "") {
            localStorage.setItem(domainType + "USERNM", r.Name);
            localStorage.setItem(domainType + "COPCD", r.COPCD);
            localStorage.setItem(domainType + "SYSID", r.SYSID);
            localStorage.setItem(domainType + "MENUID", r.MENUID);
            localStorage.setItem(domainType + "USERDBNM", r.USERDBNM);
            localStorage.setItem(domainType + "USERDBSVRNM", r.USERDBSVRNM);
            localStorage.setItem(domainType + "USERDBSVRIP", r.USERDBSVRIP);
            localStorage.setItem(domainType + "USERDBSVRUR", r.USERDBSVRUR);
            localStorage.setItem(domainType + "USERDBSVRPW", r.USERDBSVRPW);
            localStorage.setItem(domainType + "INIGRPCD", r.INIGRPCD);
            localStorage.setItem(domainType + "INIDPTCD", r.INIDPTCD);
            localStorage.setItem(domainType + "INIWHSCD", r.INIWHSCD);
            localStorage.setItem(domainType + "INICMPCD", r.INICMPCD);
            localStorage.setItem(domainType + "INICSTCD", r.INICSTCD);
            localStorage.setItem(domainType + "INISHPCD", r.INISHPCD);
            localStorage.setItem(domainType + "USERID", r.Id);

            localStorage.setItem("Local_PXAPI_DomainType", domainType);
            //window.location.href = r.SendUrl;
            window.location.href = "http://" + window.location.host + "/PXAS/PXAS0010/PXAS0010VW";
        } else {
            createDialog("dialog", "1,0,ログイン画面,ALM," + r.ErrorMsg + ",ＯＫ,ALM000-01,OK");
            $("#dialog").dialog("open");
            $("#dialog").focus();
            Ladda.stopAll();
        }
    }, function (e) {
        createDialog("dialog", "1,0,ログイン画面,ALM,error: " + e + ",ＯＫ,ALM000-01,OK");
        $("#dialog").dialog("open");
        Ladda.stopAll();
    });

    //window.location.href = "https://" + window.location.host + "/PXAS/PXAS0010/PXAS0010VW";

    //var error = "2,0,アラーム,ALM,システム利用期限が間もなく終了します。,ＯＫ,ALM000-01_OK,OK,キャンセル,ALM000-01_NG,NG";
    //var dialog = $("#dialog");
    //if (dialog != null) {
    //    createDialog("dialog", error);
    //    dialog.dialog("open");
    //}

    return false;
}
function SetSysDB() {
    domainType = $("#DomainType").val();
    localStorage.setItem(domainType + "URL", $("#SysURL").val());
    localStorage.setItem(domainType + "SYSTEMDB", $("#SysDB").val());

    $("#DomainType").val("")
    return false;
}