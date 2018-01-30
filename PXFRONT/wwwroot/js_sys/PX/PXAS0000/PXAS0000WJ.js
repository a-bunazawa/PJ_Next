window.onresize = resizeWindow;

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
    //var url = localStorage.getItem("LOGINEXT_WMSAPI_URL") + "api/LNAS0001/Login";
    var url = "https://kanda/KANDANET/API/api/LNAS0001/Login";

    var data = {};
    data.Id = $("#email").val();
    data.Pass = $("#password").val();
    data.DB = localStorage.getItem("LOGINEXT_WMSAPI_SYSTEMDB");

    $.ajax(url, { type: "POST", data: data }).then(function (r) {
        localStorage.setItem("LOGINEXT_WMSAPI_USERNM", r.Name);
        localStorage.setItem("LOGINEXT_WMSAPI_COPCD", r.COPCD);
        localStorage.setItem("LOGINEXT_WMSAPI_SYSID", r.SYSID);
        localStorage.setItem("LOGINEXT_WMSAPI_MENUID", r.MENUID);
        localStorage.setItem("LOGINEXT_WMSAPI_USERDBNM", r.USERDBNM);
        localStorage.setItem("LOGINEXT_WMSAPI_USERDBSVRNM", r.USERDBSVRNM);
        localStorage.setItem("LOGINEXT_WMSAPI_USERDBSVRIP", r.USERDBSVRIP);
        localStorage.setItem("LOGINEXT_WMSAPI_USERDBSVRUR", r.USERDBSVRUR);
        localStorage.setItem("LOGINEXT_WMSAPI_USERDBSVRPW", r.USERDBSVRPW);
        localStorage.setItem("LOGINEXT_WMSAPI_INIGRPCD", r.INIGRPCD);
        localStorage.setItem("LOGINEXT_WMSAPI_INIDPTCD", r.INIDPTCD);
        localStorage.setItem("LOGINEXT_WMSAPI_INIWHSCD", r.INIWHSCD);
        localStorage.setItem("LOGINEXT_WMSAPI_INICMPCD", r.INICMPCD);
        localStorage.setItem("LOGINEXT_WMSAPI_INICSTCD", r.INICSTCD);
        localStorage.setItem("LOGINEXT_WMSAPI_INISHPCD", r.INISHPCD);
        localStorage.setItem("LOGINEXT_WMSAPI_USERID", r.Id);

        if (r.SendUrl != "") {
            window.location.href = r.SendUrl;
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

    window.location.href = "https://" + window.location.host + "/PXAS/PXAS0010/PXAS0010VW";

    var error = "2,0,アラーム,ALM,システム利用期限が間もなく終了します。,ＯＫ,ALM000-01_OK,OK,キャンセル,ALM000-01_NG,NG";
    var dialog = $("#dialog");
    if (dialog != null) {
        createDialog("dialog", error);
        dialog.dialog("open");
    }

    return false;
}
function SetSysDB() {
    localStorage.setItem("LOGINEXT_WMSAPI_URL", $("#SysURL").val());
    localStorage.setItem("LOGINEXT_WMSAPI_SYSTEMDB", $("#SysDB").val());
    return false;
}