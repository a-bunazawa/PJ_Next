
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


    data.DB = "KN";

    return data;
}

//ローカルストレージの消去(ログアウト時etc...)
function RemoveLocalStorage() {
    localStorage.removeItem("LOGINEXT_WMSAPI_URL");
    localStorage.removeItem("LOGINEXT_WMSAPI_SYSTEMDB");
    localStorage.removeItem("LOGINEXT_WMSAPI_USERID");
    localStorage.removeItem("LOGINEXT_WMSAPI_USERNM");
    localStorage.removeItem("LOGINEXT_WMSAPI_COPCD");
    localStorage.removeItem("LOGINEXT_WMSAPI_INIGRPCD");
    localStorage.removeItem("LOGINEXT_WMSAPI_INIDPTCD");
    localStorage.removeItem("LOGINEXT_WMSAPI_INIWHSCD");
    localStorage.removeItem("LOGINEXT_WMSAPI_INICMPCD");
    localStorage.removeItem("LOGINEXT_WMSAPI_INICSTCD");
    localStorage.removeItem("LOGINEXT_WMSAPI_INISHPCD");
    localStorage.removeItem("LOGINEXT_WMSAPI_SYSID");
    localStorage.removeItem("LOGINEXT_WMSAPI_MENUID");
    localStorage.removeItem("LOGINEXT_WMSAPI_SYSTEMDB");
    localStorage.removeItem("LOGINEXT_WMSAPI_USERDBNM");
    localStorage.removeItem("LOGINEXT_WMSAPI_USERDBSVRNM");
    localStorage.removeItem("LOGINEXT_WMSAPI_USERDBSVRIP");
    localStorage.removeItem("LOGINEXT_WMSAPI_USERDBSVRUR");
    localStorage.removeItem("LOGINEXT_WMSAPI_USERDBSVRPW");
}

//レイアウトオプションの日本語化
function LocalLayoutOption() {
    $(".demo").width(200);
    $(".demo legend").text("レイアウト設定");
    $("#smart-fixed-header").next("span").text("ヘッダー固定");
    $("#smart-fixed-navigation").next("span").text("左部メニュー固定");
    $("#smart-fixed-ribbon").next("span").text("リボン固定");
    $("#smart-fixed-footer").next("span").text("フッター固定");
    $("#smart-fixed-container").next("span").text("コンテナサイズに縮小");
    $("#smart-rtl").next("span").text("左部メニューを右寄せ");
    $("#smart-topmenu").next("span").text("左部メニューを上寄せ");
    $("#colorblind-friendly").next("span").text("枠線の強調");
    $("#reset-smart-widget").prev("h6").text("ローカルストレージの消去");
    $("#reset-smart-widget").html("<i class='fa fa-refresh'></i> 初期化");
    $("#smart-styles").prev("h6").text("スキンの変更");

    $("#smart-fixed-container").click(function () {
        $("#smart-bgimages h6").text("背景の変更");
    });
}

function toHalfWidth(value) {
    return value.trim().replace(/[０-９]/g, function (s) { return String.fromCharCode(s.charCodeAt(0) - 0xFEE0) });
}

// dateFormat 関数の定義
function DateCheck(value) {
    if (value.length == 10) {
        var PTN_YYYYMMDD = /^\d{4}\/\d{2}\/\d{2}$/;
        var date = new Date(value);

        // invalidな日付または、フォーマット通りに入力されていない場合はNGとなる
        if (/Invalid|NaN/.test(date.toString()) || !PTN_YYYYMMDD.test(value)) {
            return false;
        }

        // 入力値とnewDate.toStringを文字列比較する。
        // 実際には無い日付（2013/04/31）をnewDateすると勝手に変換（2013/05/01）するのでその対策。
        // なお、31日だけこの現象が起こる。32日以降はnewDateでもinvalid判定になる。
        var m = '0' + (date.getMonth() + 1);
        var d = '0' + date.getDate();
        var newDateStr = date.getFullYear() + "/" + m.slice(-2) + "/" + d.slice(-2);

        return newDateStr === value;
    } else {
        return false;
    }
}

// dateFormat 関数の定義
function dateFormat(date) {
    var y = date.getFullYear();
    var m = date.getMonth() + 1;
    var d = date.getDate();
    var w = date.getDay();

    m = ('0' + m).slice(-2);
    d = ('0' + d).slice(-2);

    // フォーマット整形済みの文字列を戻り値にする
    return y + "/" + m + "/" + d;
}

function setComma(value) {
    // 事前準備
    var value = advancePreparation(value);
    // カンマ区切り
    while (value != (value = value.replace(/^(-?\d+)(\d{3})/, "$1,$2")));
    // 数値以外の場合は 0
    if (isNaN(parseInt(value))) {
        value = "0";
    }

    return value;
}

function advancePreparation(value) {
    // 正規表現で扱うために文字列に変換
    value = "" + value;

    // スペースとカンマを削除
    return value.replace(/^\s+|\s+$|,/g, "");
}

function isNumber(x) {
    if (typeof (x) != 'number' && typeof (x) != 'string')
        return false;
    else
        return (x == parseFloat(x) && isFinite(x));
}

function isInt(x) {
    x = x + '';
    if (x.match(/[^0-9]/g))
        return false;
    else
        return true;
}

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

function DialogButtonSelect(value, Case, Type) {
    switch (Type) {
        case 0:
            // ボタンのアイコン等のhtmlを返還
            switch (Case) {
                case "OK":
                    return "<i class='fa fa-check'></i>&nbsp; " + value;
                    break;
                case "OK2":
                    return "<i class='fa fa-check-circle'></i>&nbsp; " + value;
                    break;
                case "OK3":
                    return "<i class='fa fa-thumbs-up'></i>&nbsp; " + value;
                    break;
                case "NG":
                    return "<i class='fa fa-stop'></i>&nbsp; " + value;
                    break;
                case "NG2":
                    return "<i class='fa fa-close'></i>&nbsp; " + value;
                    break;
                case "CA":
                    return "<i class='fa fa-hand-paper-o'></i>&nbsp; " + value;
                    break;
                case "CA2":
                    return "<i class='fa fa-remove'></i>&nbsp; " + value;
                    break;
                case "CA3":
                    return "<i class='fa fa-pause'></i>&nbsp; " + value;
                    break;
                case "RT":
                    return "<i class='fa fa-mail-reply-all'></i>&nbsp; " + value;
                    break;
                case "RT2":
                    return "<i class='fa fa-refresh'></i>&nbsp; " + value;
                    break;
                case "RT3":
                    return "<i class='fa fa-rotate-right'></i>&nbsp; " + value;
                    break;
                case "DS":
                    return "<i class='fa fa-minus-circle'></i>&nbsp; " + value;
                    break;
                default:
                    return value;
                    break;
            }
            break;
        case 1:
            // ボタンのstyleクラスを返還
            switch (Case) {
                case "OK":
                    return "btn btn-primary";
                    break;
                case "OK2":
                    return "btn btn-primary";
                    break;
                case "OK3":
                    return "btn btn-primary";
                    break;
                case "NG":
                    return "btn btn-danger";
                    break;
                case "NG2":
                    return "btn btn-danger";
                    break;
                case "CA":
                    return value;
                    break;
                case "CA2":
                    return value;
                    break;
                case "CA3":
                    return value;
                    break;
                case "RT":
                    return value;
                    break;
                case "RT2":
                    return value;
                    break;
                case "RT3":
                    return value;
                    break;
                case "DS":
                    return "btn btn-danger";
                    break;
                default:
                    return value;
                    break;
            }
            break;
        default:
            return value;
            break;
    }
}

//ダイアログ作成処理
//  dialogId:ダイアログ化を行う領域のID
//  dialogMsg:ダイアログ化を行う情報
function createDialog(dialogId, dialogMsg) {
    var j$ = jQuery;
    var dialogInfo = dialogMsg.split(",");

    if (dialogInfo.length > 4) {
        var defaultButton = 'button:eq(' + dialogInfo[1] + ')';
        dialogId = "#" + dialogId;

        var title = "";
        var titleType = dialogInfo[3];
        switch (titleType) {
            case "000":
                title = "<div class='widget-header'><h4><i class='fa fa-info-circle'></i>&nbsp;" + dialogInfo[2] + "</h4></div>";
                break;
            case "0002":
                title = "<div class='widget-header'><h4><i class='fa fa-info'></i>&nbsp;" + dialogInfo[2] + "</h4></div>";
                break;
            case "0003":
                title = "<div class='widget-header'><h4><i class='fa fa-comment'></i>&nbsp;" + dialogInfo[2] + "</h4></div>";
                break;
            case "ALM":
                title = "<div class='widget-header'><h4><i class='fa fa-bell'></i>&nbsp;" + dialogInfo[2] + "</h4></div>";
                break;
            case "ERR":
                title = "<div class='widget-header'><h4><i class='fa fa-ban'></i>&nbsp;" + dialogInfo[2] + "</h4></div>";
                break;
            case "ERR2":
                title = "<div class='widget-header'><h4><i class='fa fa-exclamation-triangle'></i>&nbsp;" + dialogInfo[2] + "</h4></div>";
                break;
            case "ERR3":
                title = "<div class='widget-header'><h4><i class='fa fa-flash'></i>&nbsp;" + dialogInfo[2] + "</h4></div>";
                break;
            case "CHK":
                title = "<div class='widget-header'><h4><i class='fa fa-question-circle'></i>&nbsp;" + dialogInfo[2] + "</h4></div>";
                break;
            default:
                title = dialogInfo[2];
                break;
        }
        var mainText = dialogInfo[4].replace(/\n/g, "<br/>");

        switch (dialogInfo[0]) {
            case "1":
                if (dialogInfo.length > 7) {
                    var btnNm = DialogButtonSelect(dialogInfo[5], dialogInfo[7], 0);
                    var btnId = "#" + dialogInfo[6];
                    var btnCs = DialogButtonSelect("btn btn-default", dialogInfo[7], 1);

                    //  ◆メッセージ設定
                    j$(dialogId).html(mainText);
                    //  ◆ダイアログ設定
                    j$(dialogId).dialog({
                        width: 500,
                        autoOpen: false,
                        modal: true,
                        buttons: [{
                            html: btnNm,
                            "class": btnCs,
                            click: function () {
                                j$(btnId).click();
                                j$(this).dialog("close");
                            }
                        }],
                        open: function () {
                            j$(this).siblings('.ui-dialog-buttonpane').find(defaultButton).focus();
                        },
                        close: function () {
                            closeEvent();
                        }
                    });
                } else {
                    j$(dialogId).css("display", "none");
                }
                break;
            case "2":
                if (dialogInfo.length > 10) {
                    j$(dialogId).html(mainText);
                    var btnName1 = DialogButtonSelect(dialogInfo[5], dialogInfo[7], 0);
                    var btnAnser1 = "#" + dialogInfo[6];
                    var btnType1 = DialogButtonSelect("btn btn-default", dialogInfo[7], 1);
                    var btnName2 = DialogButtonSelect(dialogInfo[8], dialogInfo[10], 0);
                    var btnAnser2 = "#" + dialogInfo[9];
                    var btnType2 = DialogButtonSelect("btn btn-default", dialogInfo[10], 1);

                    j$(dialogId).dialog({
                        title: title,
                        width: 500,
                        autoOpen: false,
                        modal: true,
                        buttons: [{
                            html: btnName1,
                            "class": btnType1,
                            click: function () {
                                j$(btnAnser1).click();
                                j$(this).dialog("close");
                            }
                        },
                        {
                            html: btnName2,
                            "class": btnType2,
                            click: function () {
                                j$(btnAnser2).click();
                                j$(this).dialog("close");
                            }
                        }],
                        open: function () {
                            j$(this).siblings('.ui-dialog-buttonpane').find(defaultButton).focus();
                        },
                        close: function () {
                            closeEvent();
                        }
                    });
                } else {
                    j$(dialogId).css("display", "none");
                }
                break;
            case "3":
                if (dialogInfo.length > 13) {
                    j$(dialogId).html(mainText);
                    var btnName1 = DialogButtonSelect(dialogInfo[5], dialogInfo[7], 0);
                    var btnAnser1 = "#" + dialogInfo[6];
                    var btnType1 = DialogButtonSelect("btn btn-default", dialogInfo[7], 1);
                    var btnName2 = DialogButtonSelect(dialogInfo[8], dialogInfo[10], 0);
                    var btnAnser2 = "#" + dialogInfo[9];
                    var btnType2 = DialogButtonSelect("btn btn-default", dialogInfo[10], 1);
                    var btnName3 = DialogButtonSelect(dialogInfo[11], dialogInfo[13], 0);
                    var btnAnser3 = "#" + dialogInfo[12];
                    var btnType3 = DialogButtonSelect("btn btn-default", dialogInfo[13], 1);

                    j$(dialogId).dialog({
                        title: title,
                        width: 500,
                        autoOpen: false,
                        modal: true,
                        buttons: [{
                            html: btnName1,
                            "class": btnType1,
                            click: function () {
                                j$(btnAnser1).click();
                                j$(this).dialog("close");
                            }
                        },
                        {
                            html: btnName2,
                            "class": btnType2,
                            click: function () {
                                j$(btnAnser2).click();
                                j$(this).dialog("close");
                            }
                        },
                        {
                            html: btnName3,
                            "class": btnType3,
                            click: function () {
                                j$(btnAnser3).click();
                                j$(this).dialog("close");
                            }
                        }],
                        open: function () {
                            j$(this).siblings('.ui-dialog-buttonpane').find(defaultButton).focus();
                        },
                        close: function () {
                            closeEvent();
                        }
                    });
                } else {
                    j$(dialogId).css("display", "none");
                }
                break;
            case "4":
                if (dialogInfo.length > 16) {
                    j$(dialogId).html(mainText);
                    var btnName1 = DialogButtonSelect(dialogInfo[5], dialogInfo[7], 0);
                    var btnAnser1 = "#" + dialogInfo[6];
                    var btnType1 = DialogButtonSelect("btn btn-default", dialogInfo[7], 1);
                    var btnName2 = DialogButtonSelect(dialogInfo[8], dialogInfo[10], 0);
                    var btnAnser2 = "#" + dialogInfo[9];
                    var btnType2 = DialogButtonSelect("btn btn-default", dialogInfo[10], 1);
                    var btnName3 = DialogButtonSelect(dialogInfo[11], dialogInfo[13], 0);
                    var btnAnser3 = "#" + dialogInfo[12];
                    var btnType3 = DialogButtonSelect("btn btn-default", dialogInfo[13], 1);
                    var btnName4 = DialogButtonSelect(dialogInfo[14], dialogInfo[16], 0);
                    var btnAnser4 = "#" + dialogInfo[15];
                    var btnType4 = DialogButtonSelect("btn btn-default", dialogInfo[16], 1);

                    j$(dialogId).dialog({
                        title: title,
                        width: 500,
                        autoOpen: false,
                        modal: true,
                        buttons: [{
                            html: btnName1,
                            "class": btnType1,
                            click: function () {
                                j$(btnAnser1).click();
                                j$(this).dialog("close");
                            }
                        },
                        {
                            html: btnName2,
                            "class": btnType2,
                            click: function () {
                                j$(btnAnser2).click();
                                j$(this).dialog("close");
                            }
                        },
                        {
                            html: btnName3,
                            "class": btnType3,
                            click: function () {
                                j$(btnAnser3).click();
                                j$(this).dialog("close");
                            }
                        },
                        {
                            html: btnName4,
                            "class": btnType4,
                            click: function () {
                                j$(btnAnser4).click();
                                j$(this).dialog("close");
                            }
                        }],
                        open: function () {
                            j$(this).siblings('.ui-dialog-buttonpane').find(defaultButton).focus();
                        },
                        close: function () {
                            closeEvent();
                        }
                    });
                } else {
                    j$(dialogId).css("display", "none");
                }
                break;
            case "5":
                if (dialogInfo.length > 19) {
                    j$(dialogId).html(mainText);
                    var btnName1 = DialogButtonSelect(dialogInfo[5], dialogInfo[7], 0);
                    var btnAnser1 = "#" + dialogInfo[6];
                    var btnType1 = DialogButtonSelect("btn btn-default", dialogInfo[7], 1);
                    var btnName2 = DialogButtonSelect(dialogInfo[8], dialogInfo[10], 0);
                    var btnAnser2 = "#" + dialogInfo[9];
                    var btnType2 = DialogButtonSelect("btn btn-default", dialogInfo[10], 1);
                    var btnName3 = DialogButtonSelect(dialogInfo[11], dialogInfo[13], 0);
                    var btnAnser3 = "#" + dialogInfo[12];
                    var btnType3 = DialogButtonSelect("btn btn-default", dialogInfo[13], 1);
                    var btnName4 = DialogButtonSelect(dialogInfo[14], dialogInfo[16], 0);
                    var btnAnser4 = "#" + dialogInfo[15];
                    var btnType4 = DialogButtonSelect("btn btn-default", dialogInfo[16], 1);
                    var btnName5 = DialogButtonSelect(dialogInfo[17], dialogInfo[19], 0);
                    var btnAnser5 = "#" + dialogInfo[18];
                    var btnType5 = DialogButtonSelect("btn btn-default", dialogInfo[19], 1);

                    j$(dialogId).dialog({
                        title: title,
                        width: 500,
                        autoOpen: false,
                        modal: true,
                        buttons: [{
                            html: btnName1,
                            "class": btnType1,
                            click: function () {
                                j$(btnAnser1).click();
                                j$(this).dialog("close");
                            }
                        },
                        {
                            html: btnName2,
                            "class": btnType2,
                            click: function () {
                                j$(btnAnser2).click();
                                j$(this).dialog("close");
                            }
                        },
                        {
                            html: btnName3,
                            "class": btnType3,
                            click: function () {
                                j$(btnAnser3).click();
                                j$(this).dialog("close");
                            }
                        },
                        {
                            html: btnName4,
                            "class": btnType4,
                            click: function () {
                                j$(btnAnser4).click();
                                j$(this).dialog("close");
                            }
                        },
                        {
                            html: btnName5,
                            "class": btnType5,
                            click: function () {
                                j$(btnAnser5).click();
                                j$(this).dialog("close");
                            }
                        }],
                        open: function () {
                            j$(this).siblings('.ui-dialog-buttonpane').find(defaultButton).focus();
                        },
                        close: function () {
                            closeEvent();
                        }
                    });
                } else {
                    j$(dialogId).css("display", "none");
                }
                break;
            default: j$(dialogId).css("display", "none"); break;
        }
        // TODO タイトルへのタグ入力は現状、力業で実行中。　適宜上記の「◆ダイアログ設定」内に収まるよう調整
        j$(".ui-dialog-title").html(title);
    } else {
        j$(dialogId).css("display", "none");
    }
}

function closeEvent() { }

//ダイアログ作成処理
//  dialogId:ダイアログ化を行う領域のID
//  dialogMsg:ダイアログ化を行う情報
function OpenDialog(dialogId, dialogMsgId) {
}

function createDialogOpen(dialogId, copcd, pgId, dialogMsgId) {
    var j$ = jQuery;
    dialogId = "#" + dialogId;
    //LoaderControl("open");
    //j$(dialogId).load("http://" + window.location.host + "/PXAS/PXAS0000/_PXAS0000VW", function () {
    //    LoaderControl("close");
    //    createDialogOpen(dialogId, dialogMsgId);
    //});
    var dialogMsg  = "1,0,グループ登録ダイアログ,ALM,test,登 録,Ins_OK,OK";
    var dialogInfo = dialogMsg.split(",");

    if (dialogInfo.length > 4) {
        var defaultButton = 'button:eq(' + dialogInfo[1] + ')';

        var dialog = j$(dialogId);
        if (dialog != null) {
            var title = "";
            var titleType = dialogInfo[3];
            switch (titleType) {
                case "000":
                    title = "<div class='widget-header'><h4><i class='fa fa-info-circle'></i>&nbsp;" + dialogInfo[2] + "</h4></div>";
                    break;
                case "0002":
                    title = "<div class='widget-header'><h4><i class='fa fa-info'></i>&nbsp;" + dialogInfo[2] + "</h4></div>";
                    break;
                case "0003":
                    title = "<div class='widget-header'><h4><i class='fa fa-comment'></i>&nbsp;" + dialogInfo[2] + "</h4></div>";
                    break;
                case "ALM":
                    title = "<div class='widget-header'><h4><i class='fa fa-bell'></i>&nbsp;" + dialogInfo[2] + "</h4></div>";
                    break;
                case "ERR":
                    title = "<div class='widget-header'><h4><i class='fa fa-ban'></i>&nbsp;" + dialogInfo[2] + "</h4></div>";
                    break;
                case "ERR2":
                    title = "<div class='widget-header'><h4><i class='fa fa-exclamation-triangle'></i>&nbsp;" + dialogInfo[2] + "</h4></div>";
                    break;
                case "ERR3":
                    title = "<div class='widget-header'><h4><i class='fa fa-flash'></i>&nbsp;" + dialogInfo[2] + "</h4></div>";
                    break;
                case "CHK":
                    title = "<div class='widget-header'><h4><i class='fa fa-question-circle'></i>&nbsp;" + dialogInfo[2] + "</h4></div>";
                    break;
                default:
                    title = dialogInfo[2];
                    break;
            }
            var mainText = dialogInfo[4].replace(/\n/g, "<br/>");

            switch (dialogInfo[0]) {
                case "1":
                    if (dialogInfo.length > 7) {
                        var btnNm = DialogButtonSelect(dialogInfo[5], dialogInfo[7], 0);
                        var btnId = dialogInfo[6];
                        var btnCs = DialogButtonSelect("btn btn-default", dialogInfo[7], 1);

                        //  ◆メッセージ設定
                        dialog.html(mainText);
                        //  ◆ダイアログ設定
                        dialog.dialog({
                            width: 500,
                            autoOpen: false,
                            modal: true,
                            buttons: [{
                                html: btnNm,
                                "class": btnCs,
                                click: function () {
                                    ReturnDialog(dialogMsgId, btnId);
                                    j$(this).dialog("close");
                                }
                            }],
                            open: function () {
                                j$(this).siblings('.ui-dialog-buttonpane').find(defaultButton).focus();
                            },
                            close: function () {
                                closeEvent();
                            }
                        });
                    } else {
                        dialog.css("display", "none");
                    }
                    break;
                case "2":
                    if (dialogInfo.length > 10) {
                        dialog.html(mainText);
                        var btnName1 = DialogButtonSelect(dialogInfo[5], dialogInfo[7], 0);
                        var btnAnser1 = dialogInfo[6];
                        var btnType1 = DialogButtonSelect("btn btn-default", dialogInfo[7], 1);
                        var btnName2 = DialogButtonSelect(dialogInfo[8], dialogInfo[10], 0);
                        var btnAnser2 = dialogInfo[9];
                        var btnType2 = DialogButtonSelect("btn btn-default", dialogInfo[10], 1);

                        dialog.dialog({
                            title: title,
                            width: 500,
                            autoOpen: false,
                            modal: true,
                            buttons: [{
                                html: btnName1,
                                "class": btnType1,
                                click: function () {
                                    j$(btnAnser1).click();
                                    ReturnDialog(dialogMsgId, btnAnser1);
                                    j$(this).dialog("close");
                                }
                            },
                            {
                                html: btnName2,
                                "class": btnType2,
                                click: function () {
                                    ReturnDialog(dialogMsgId, btnAnser2);
                                    j$(this).dialog("close");
                                }
                            }],
                            open: function () {
                                j$(this).siblings('.ui-dialog-buttonpane').find(defaultButton).focus();
                            },
                            close: function () {
                                closeEvent();
                            }
                        });
                    } else {
                        dialog.css("display", "none");
                    }
                    break;
                case "3":
                    if (dialogInfo.length > 13) {
                        dialog.html(mainText);
                        var btnName1 = DialogButtonSelect(dialogInfo[5], dialogInfo[7], 0);
                        var btnAnser1 = dialogInfo[6];
                        var btnType1 = DialogButtonSelect("btn btn-default", dialogInfo[7], 1);
                        var btnName2 = DialogButtonSelect(dialogInfo[8], dialogInfo[10], 0);
                        var btnAnser2 = dialogInfo[9];
                        var btnType2 = DialogButtonSelect("btn btn-default", dialogInfo[10], 1);
                        var btnName3 = DialogButtonSelect(dialogInfo[11], dialogInfo[13], 0);
                        var btnAnser3 = dialogInfo[12];
                        var btnType3 = DialogButtonSelect("btn btn-default", dialogInfo[13], 1);

                        dialog.dialog({
                            title: title,
                            width: 500,
                            autoOpen: false,
                            modal: true,
                            buttons: [{
                                html: btnName1,
                                "class": btnType1,
                                click: function () {
                                    ReturnDialog(dialogMsgId, btnAnser1);
                                    j$(this).dialog("close");
                                }
                            },
                            {
                                html: btnName2,
                                "class": btnType2,
                                click: function () {
                                    ReturnDialog(dialogMsgId, btnAnser2);
                                    j$(this).dialog("close");
                                }
                            },
                            {
                                html: btnName3,
                                "class": btnType3,
                                click: function () {
                                    ReturnDialog(dialogMsgId, btnAnser3);
                                    j$(this).dialog("close");
                                }
                            }],
                            open: function () {
                                j$(this).siblings('.ui-dialog-buttonpane').find(defaultButton).focus();
                            },
                            close: function () {
                                closeEvent();
                            }
                        });
                    } else {
                        dialog.css("display", "none");
                    }
                    break;
                case "4":
                    if (dialogInfo.length > 16) {
                        dialog.html(mainText);
                        var btnName1 = DialogButtonSelect(dialogInfo[5], dialogInfo[7], 0);
                        var btnAnser1 = dialogInfo[6];
                        var btnType1 = DialogButtonSelect("btn btn-default", dialogInfo[7], 1);
                        var btnName2 = DialogButtonSelect(dialogInfo[8], dialogInfo[10], 0);
                        var btnAnser2 = dialogInfo[9];
                        var btnType2 = DialogButtonSelect("btn btn-default", dialogInfo[10], 1);
                        var btnName3 = DialogButtonSelect(dialogInfo[11], dialogInfo[13], 0);
                        var btnAnser3 = dialogInfo[12];
                        var btnType3 = DialogButtonSelect("btn btn-default", dialogInfo[13], 1);
                        var btnName4 = DialogButtonSelect(dialogInfo[14], dialogInfo[16], 0);
                        var btnAnser4 = dialogInfo[15];
                        var btnType4 = DialogButtonSelect("btn btn-default", dialogInfo[16], 1);

                        dialog.dialog({
                            title: title,
                            width: 500,
                            autoOpen: false,
                            modal: true,
                            buttons: [{
                                html: btnName1,
                                "class": btnType1,
                                click: function () {
                                    ReturnDialog(dialogMsgId, btnAnser1);
                                    j$(this).dialog("close");
                                }
                            },
                            {
                                html: btnName2,
                                "class": btnType2,
                                click: function () {
                                    ReturnDialog(dialogMsgId, btnAnser2);
                                    j$(this).dialog("close");
                                }
                            },
                            {
                                html: btnName3,
                                "class": btnType3,
                                click: function () {
                                    ReturnDialog(dialogMsgId, btnAnser3);
                                    j$(this).dialog("close");
                                }
                            },
                            {
                                html: btnName4,
                                "class": btnType4,
                                click: function () {
                                    ReturnDialog(dialogMsgId, btnAnser4);
                                    j$(this).dialog("close");
                                }
                            }],
                            open: function () {
                                j$(this).siblings('.ui-dialog-buttonpane').find(defaultButton).focus();
                            },
                            close: function () {
                                closeEvent();
                            }
                        });
                    } else {
                        dialog.css("display", "none");
                    }
                    break;
                case "5":
                    if (dialogInfo.length > 19) {
                        dialog.html(mainText);
                        var btnName1 = DialogButtonSelect(dialogInfo[5], dialogInfo[7], 0);
                        var btnAnser1 = dialogInfo[6];
                        var btnType1 = DialogButtonSelect("btn btn-default", dialogInfo[7], 1);
                        var btnName2 = DialogButtonSelect(dialogInfo[8], dialogInfo[10], 0);
                        var btnAnser2 = dialogInfo[9];
                        var btnType2 = DialogButtonSelect("btn btn-default", dialogInfo[10], 1);
                        var btnName3 = DialogButtonSelect(dialogInfo[11], dialogInfo[13], 0);
                        var btnAnser3 = dialogInfo[12];
                        var btnType3 = DialogButtonSelect("btn btn-default", dialogInfo[13], 1);
                        var btnName4 = DialogButtonSelect(dialogInfo[14], dialogInfo[16], 0);
                        var btnAnser4 = dialogInfo[15];
                        var btnType4 = DialogButtonSelect("btn btn-default", dialogInfo[16], 1);
                        var btnName5 = DialogButtonSelect(dialogInfo[17], dialogInfo[19], 0);
                        var btnAnser5 = dialogInfo[18];
                        var btnType5 = DialogButtonSelect("btn btn-default", dialogInfo[19], 1);

                        dialog.dialog({
                            title: title,
                            width: 500,
                            autoOpen: false,
                            modal: true,
                            buttons: [{
                                html: btnName1,
                                "class": btnType1,
                                click: function () {
                                    ReturnDialog(dialogMsgId, btnAnser1);
                                    j$(this).dialog("close");
                                }
                            },
                            {
                                html: btnName2,
                                "class": btnType2,
                                click: function () {
                                    ReturnDialog(dialogMsgId, btnAnser2);
                                    j$(this).dialog("close");
                                }
                            },
                            {
                                html: btnName3,
                                "class": btnType3,
                                click: function () {
                                    ReturnDialog(dialogMsgId, btnAnser3);
                                    j$(this).dialog("close");
                                }
                            },
                            {
                                html: btnName4,
                                "class": btnType4,
                                click: function () {
                                    ReturnDialog(dialogMsgId, btnAnser4);
                                    j$(this).dialog("close");
                                }
                            },
                            {
                                html: btnName5,
                                "class": btnType5,
                                click: function () {
                                    ReturnDialog(dialogMsgId, btnAnser5);
                                    j$(this).dialog("close");
                                }
                            }],
                            open: function () {
                                j$(this).siblings('.ui-dialog-buttonpane').find(defaultButton).focus();
                            },
                            close: function () {
                                closeEvent();
                            }
                        });
                    } else {
                        dialog.css("display", "none");
                    }
                    break;
                default: dialog.css("display", "none"); break;
            }
            // TODO タイトルへのタグ入力は現状、力業で実行中。　適宜上記の「◆ダイアログ設定」内に収まるよう調整
            j$(".ui-dialog-title").html(title);
            dialog.dialog({ width: "600", height: "auto" });
            dialog.dialog("open");
            dialog.focus();
        }
    } else {
        j$(dialogId).css("display", "none");
    }
}
