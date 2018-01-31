
//パンくずリストの値設定
function SetRibbonList(list) {
    $('#ribbon').empty();
    var ol = $('<ol class="breadcrumb">');
    var li;
    for (var i = 0; i < list.length; i++) {
        li = $('<li>');
        li.append(list[i]);
        ol.append(li);
    }
    $('#ribbon').append(ol);
}

//ログイン情報をローカルストレージから取得
function GetLoginData() {
    var data = {};
    data.Id = localStorage.getItem("LOGINEXT_WMSAPI_USERID");
    data.USERNM = localStorage.getItem("LOGINEXT_WMSAPI_USERNM");
    data.COPCD = localStorage.getItem("LOGINEXT_WMSAPI_COPCD");
    data.INIGRPCD = localStorage.getItem("LOGINEXT_WMSAPI_INIGRPCD");
    data.INIDPTCD = localStorage.getItem("LOGINEXT_WMSAPI_INIDPTCD");
    data.INIWHSCD = localStorage.getItem("LOGINEXT_WMSAPI_INIWHSCD");
    data.INICMPCD = localStorage.getItem("LOGINEXT_WMSAPI_INICMPCD");
    data.INICSTCD = localStorage.getItem("LOGINEXT_WMSAPI_INICSTCD");
    data.INISHPCD = localStorage.getItem("LOGINEXT_WMSAPI_INISHPCD");
    data.SYSID = localStorage.getItem("LOGINEXT_WMSAPI_SYSID");
    data.MENUID = localStorage.getItem("LOGINEXT_WMSAPI_MENUID");
    data.DB = localStorage.getItem("LOGINEXT_WMSAPI_SYSTEMDB");
    data.USERDBNM = localStorage.getItem("LOGINEXT_WMSAPI_USERDBNM");
    data.USERDBSVRNM = localStorage.getItem("LOGINEXT_WMSAPI_USERDBSVRNM");
    data.USERDBSVRIP = localStorage.getItem("LOGINEXT_WMSAPI_USERDBSVRIP");
    data.USERDBSVRUR = localStorage.getItem("LOGINEXT_WMSAPI_USERDBSVRUR");
    data.USERDBSVRPW = localStorage.getItem("LOGINEXT_WMSAPI_USERDBSVRPW");
    return data;
}
