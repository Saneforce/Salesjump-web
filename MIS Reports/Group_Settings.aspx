<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Group_Settings.aspx.cs" Inherits="MasterFiles_Group_Setting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <html>
    <head>
        <title>Group Setting</title>
        <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
        <script src="../js/jQuery-2.2.0.min.js" type="text/javascript"></script>
        <script language="javascript" type="text/javascript">
            var str = '', rd_str = '', Divstr = ''; var grpItems = []; var Jresult = []; crId = ''; var arr = [];
            var Data_ColRow = [], DataVal = [];
            $(document).ready(function () {

                $.ajax({
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    url: "Group_Settings.aspx/GetMenuValue",
                    dataType: "json",
                    success: function (data) {
                        $('#tbl_Grp TBODY').html("");
                        Jresult = JSON.parse(data.d) || [];
                        if (Jresult.length > 0) {
                            unpivot();
                        }
                        else {
                            alert("No Data Available!!!");
                        }
                    },
                    error: function (res) {
                        alert(res);
                    }
                });


                $('#submit').on('click', function () {
                    /*arr = []; var j = 0;
                    for (var i = 0; i < Jresult.length; i++) {
                        if (Jresult[i].Setup_Type == 'CheckBox') {
                            arr[j] = $('#' + Jresult[i].SetUp_ID).attr("checked") ? 1 : 0;
                            j++;
                        }
                    }*/
                    const Arr = JSON.stringify(Data_ColRow);
                    $.ajax({
                        type: "POST",
                        contentType: "application/json;charset=utf-8",
                        url: "Group_Settings.aspx/Submit_SettingDetails",
                        dataType: "json",
                        data: "{'data':'" + Arr + "'}",
                        success: function (data) {
                            var Result = data.d || '';
                            if (Result == 'Success') {
                                alert(Result);
                            }
                            else {
                                alert(Result);
                            }
                        }
                    });
                });

            });
            function unpivot() {
                //const Unpivot_Arr = JSON.stringify(Jresult);
                var Qry = '', castvar = '';
                for (var $i = 0; $i < Jresult.length; $i++) {
                    //castvar += "isnull(CAST(" + Jresult[$i].Field_Name + " AS NVARCHAR(MAX)),\"\") AS " + Jresult[$i].Field_Name + "" + ",";
                    Qry += Jresult[$i].Field_Name + ",";
                }
                //castvar = castvar.slice(0, -1);
                Qry = Qry.slice(0, -1);
                $.ajax({
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    url: "Group_Settings.aspx/GetData_colrow",
                    dataType: "json",
                    data: "{'Unpivot':'" + Qry + "'}",
                    success: function (data) {
                        Data_ColRow = JSON.parse(data.d) || [];
                        if (Data_ColRow.length > 0) {
                            NavTab();
                        }
                        //if (Data_ColRow.length > 0) {
                        //    for (var j = 0; j < Jresult.length; j++) {
                        //        Value = Data_ColRow.filter(function (a) {
                        //            return (a.Field_Name = Jresult[j].Field_Name)
                        //        })
                        //        DataVal.push({ Data: Value });
                        //    }
                        //}
                    },
                    Error: function (res) {
                        alert(res);
                    }
                });
            }
            function NavTab() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    url: "Group_Settings.aspx/GetTabMenu",
                    dataType: "json",
                    success: function (data) {
                        grpItems = JSON.parse(data.d) || [];
                        if (grpItems.length > 0) {
                            var Div = $('.tab-content').html("");
                            Divstr = '';
                            for (var i = 0; i < grpItems.length; i++) {
                                str = "<li " + ((i == 0) ? "class='active'" : " ") + " ><a data-toggle='tab' onclick=\"changetab(\'" + grpItems[i].ID + "\',\'" + grpItems[i].Setting_Field_Name + "\')\" id='tab_" + grpItems[i].ID + "'>" + grpItems[i].Setting_Field_Name + "</a></li>";
                                $('#ul_tab_menu').append(str);
                                Divstr = "<div id='ara_" + grpItems[i].ID + "' class='" + ((i == 0) ? "active in" : "") + "' ><div class='table-responsive'><table class='table borderless' id='cnta_" + grpItems[i].ID + "'><tbody></tbody></table></div></div>";
                                $(Div).append(Divstr);
                                if (i == 0) changetab(grpItems[i].ID, '');
                            }
                        }
                    },
                    error: function (res) {
                        alert(res);
                    }
                });
            }
            function chngArr(ID, FieldName, Setuptyp) {
                getval = Jresult.filter(function (a) {
                    return a.SetUp_ID == ID;
                });
                var valarr = getval[0].Setup_Values;
                var valuessetup = valarr.slice(0, -1).split(/,/);
                var Listtxt_Val = [], TxtArr = [];
                $.each(valuessetup, function (key, val) {
                    Listtxt_Val = val.split('#');
                    TxtArr.push({
                        id: Listtxt_Val[1],
                        value: Listtxt_Val[0]
                    });
                });
                var chngval = '';
                if (Setuptyp == "Text") {
                    chngval = $('#' + ID).val();
                }
                else if (Setuptyp == "Number") {
                    chngval = $('#' + ID).val();
                }
                else if (Setuptyp == "CheckBox") {
                    chngval = $('#' + ID).attr("checked") ? 1 : 0;
                }
                else if (Setuptyp == "Toggle") {
                    for (var $y = 0; $y < TxtArr.length; $y++) {
                        if ($('#' + ID).attr("checked") && (TxtArr[$y].value == 'ON' || TxtArr[$y].value == 'Yes')) {
                            chngval = TxtArr[$y].id;
                        }
                        else if (!$('#' + ID).attr("checked") && (TxtArr[$y].value == 'OFF' || TxtArr[$y].value == 'No')) {
                            chngval = TxtArr[$y].id;
                        }
                    }
                }
                else if (Setuptyp == "Selection") {
                    chngval = $('#' + ID + ' option:selected').val();
                }
                else if (Setuptyp == "Option") {
                    chngval = ID;
                }
                for (var i = 0; i < Data_ColRow.length; i++) {
                    if (Data_ColRow[i].Field_Name == FieldName) {
                        Data_ColRow[i]['Value'] = chngval;
                    }
                }
            }

            function changetab(id, gid) {
                if (crId != "") {
                    $('#ara_' + crId).hide();
                    $('#cnta_' + crId + ' TBODY').html('')
                }
                $("#cnta_" + id).show();
                $("#ara_" + id).show();
                crId = id;
                items = Jresult.filter(function (a) {
                    return (a.Group_Setup == id)
                })
                str = '';
                for (var i = 0; i < items.length; i++) {
                    var valarr = items[i].Setup_Values;
                    var valuessetup = valarr.slice(0, -1).split(/,/);
                    var Listtxt_Val = [], TxtArr = [];
                    $.each(valuessetup, function (key, val) {
                        Listtxt_Val = val.split('#');
                        TxtArr.push({
                            id: Listtxt_Val[1],
                            value: Listtxt_Val[0]
                        });
                    });
                    var chkdata = '';
                    for (var j = 0; j < Data_ColRow.length; j++) {
                        if (Data_ColRow[j].Field_Name == items[i].Field_Name) {
                            if (items[i].Setup_Type == 'Text' || items[i].Setup_Type == 'Number' || items[i].Setup_Type == 'Date')
                                chkdata = Data_ColRow[j].Value;
                            else if (items[i].Setup_Type == 'Toggle')
                                for (var $m = 0; $m < TxtArr.length; $m++) {
                                    if ((TxtArr[$m].value == 'ON' || TxtArr[$m].value == 'Yes') && Data_ColRow[j].Value == items[i].Value)
                                        chkdata = 'checked=checked';
                                }
                            else {
                                if (Data_ColRow[j].Value == items[i].Value) {
                                    chkdata = 'checked=checked';
                                }
                            }
                        }
                    }

                    str = "<tr><td>" + items[i].Setup_Name + "<div colspan='3' style='position:absolute;font-size:11px;color:#878585;'>" + items[i].Setup_Desc + "</div></td>";
                    if (items[i].Setup_Type == 'Text' || items[i].Setup_Type == 'Number' || items[i].Setup_Type == 'Date') {
                        str += "<td class='txt1'><input onfocus=this.style.backgroundColor='#E0EE9D' onblur=this.style.backgroundColor='White' style='height: 24px; width: 100px; background-color: white;' ";
                        str += "onchange=\"chngArr(\'" + items[i].SetUp_ID + "'\,\'" + items[i].Field_Name + "'\,\'" + items[i].Setup_Type + "'\)\"";
                        str += "value = '" + ((chkdata != '') ? chkdata : items[i].Field_Name) + "'";
                        str += "type = '" + items[i].Setup_Type + "' id='" + items[i].SetUp_ID + "' /></td > ";
                    }
                    else if (items[i].Setup_Type == 'CheckBox') {
                        str += "<td class='txt1' style='text-align: -webkit-left;'><input id='" + items[i].SetUp_ID + "' type = '" + items[i].Setup_Type + "' style='width:35px;' " + ((chkdata != '') ? chkdata : '') + " onclick=\"chngArr(\'" + items[i].SetUp_ID + "'\,\'" + items[i].Field_Name + "'\,\'" + items[i].Setup_Type + "'\)\" />\
                                <lable style='font-size: smaller;'>" + items[i].Value + "</lable></td>";
                    }
                    else if (items[i].Setup_Type == 'Toggle') {

                        str += "<td class='txt1 col-xs-1' > <input class='tgl tgl-skewed " + items[i].Field_Name + "' onclick=\"chngArr(\'" + items[i].SetUp_ID + "'\,\'" + items[i].Field_Name + "'\,\'" + items[i].Setup_Type + "'\)\"";
                        str += "id = '" + items[i].SetUp_ID + "' type='CheckBox' name = 'get" + items[i].Setup_Name + "' " + ((chkdata != '') ? chkdata : '') + "'/><label class='tgl-btn' data-tg-off='OFF' data-tg-on='ON' for= '" + items[i].SetUp_ID + "' ></label></td>";
                    }
                    else if (items[i].Setup_Type == 'Selection') {
                        rd_str = '';
                        for (var j = 0; j < valuessetup.length; j++) {
                            rd_str += "<option value='" + TxtArr[j].id + "'>" + TxtArr[j].value + "</option>";
                        }
                        str += "<td class='txt1'><select size='1' id='" + items[i].SetUp_ID + "' ";
                        str += "onchange =\"chngArr(\'" + items[i].SetUp_ID + "'\,\'" + items[i].Field_Name + "'\,\'" + items[i].Setup_Type + "'\)\" style='width: 100px; height:24px' >" + rd_str + "</select></td >";
                        /*for (var $j = 0; $j < valuessetup.length; $j++)
                        $('#' + items[i].SetUp_ID + ' option:contains(' + items[i].Value + ')').attr('selected', true);*/

                    }
                    else if (items[i].Setup_Type == 'Option') {
                        rd_str = '';
                        for (var j = 0; j < valuessetup.length; j++) {
                            rd_str += "<div><input type='radio' style='width:35px;' id='" + j + "'  name='btnrd_" + i + "' " + ((items[i].Value == TxtArr[j].id) ? 'checked=checked' : '') + " onchange =\"chngArr(\'" + TxtArr[j].id + "'\,\'" + items[i].Field_Name + "'\,\'" + items[i].Setup_Type + "'\)\"/><lable style='font-size: smaller;'>" + TxtArr[j].value + "</lable></div> ";
                        }
                        str += "<td style='text-align:right'>" + rd_str + "</td>";
                    }
                    str += "</tr><br />";
                    $('#cnta_' + id + ' TBODY').append(str);
                }
            }
        </script>
        <style type="text/css">
            .modal {
                position: fixed;
                top: 0;
                left: 0;
                background-color: gray;
                z-index: 99;
                opacity: 0.8;
                filter: alpha(opacity=80);
                -moz-opacity: 0.8;
                min-height: 100%;
                width: 100%;
            }

            .loading {
                font-family: Arial;
                font-size: 10pt;
                border: 5px solid #67CFF5;
                width: 200px;
                height: 100px;
                display: none;
                position: fixed;
                background-color: White;
                z-index: 999;
            }

            .ajax__tab_xp .ajax__tab_tab {
                height: 300px;
                min-height: 300px;
            }

            .ajax__tab_header {
                font-family: "Helvetica Neue", Arial, Sans-Serif;
                font-size: 14px;
                font-weight: bold;
                display: block;
            }

            .ajax__tab_hover .ajax__tab_outer {
                background-color: #9c3;
            }

            .ajax__tab_hover .ajax__tab_inner {
                color: Blue;
            }

            .ajax__tab_active .ajax__tab_outer {
                border-bottom-color: #ffffff;
                background-color: #d7d7d7;
            }

            .ajax__tab_active .ajax__tab_inner {
                color: #000;
                border-color: #333;
            }

            .ajax__tab_body {
                font-family: verdana,tahoma,helvetica;
                font-size: 10pt;
                background-color: #fff;
                border-top-width: 0;
                border: solid 1px #d7d7d7;
                border-top-color: #ffffff;
            }
            /*style for new check box */
            .txt1 {
                text-align: right;
                padding: 4px 20px;
            }




            .tab-content {
                border: 1px solid lightgray;
                margin-top: -1px;
                padding: 20px 8px 0px 20px;
            }


            .table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {
                border-top: 0px solid;
            }


            .table td, .table th, .table tr {
                border: none;
            }


            .tg-list-item {
                margin: 0 0em;
            }


            .tgl {
                display: none;
            }

                .tgl, .tgl:after, .tgl:before, .tgl *, .tgl *:after, .tgl *:before, .tgl + .tgl-btn {
                    box-sizing: border-box;
                }

                    .tgl::-moz-selection, .tgl:after::-moz-selection, .tgl:before::-moz-selection, .tgl *::-moz-selection, .tgl *:after::-moz-selection, .tgl *:before::-moz-selection, .tgl + .tgl-btn::-moz-selection {
                        background: none;
                    }

                    .tgl::selection, .tgl:after::selection, .tgl:before::selection, .tgl *::selection, .tgl *:after::selection, .tgl *:before::selection, .tgl + .tgl-btn::selection {
                        background: none;
                    }

                    .tgl + .tgl-btn {
                        outline: 0;
                        display: block;
                        width: 4em;
                        height: 2em;
                        position: relative;
                        cursor: pointer;
                        -webkit-user-select: none;
                        -moz-user-select: none;
                        -ms-user-select: none;
                        user-select: none;
                    }


            .tgl-skewed + .tgl-btn {
                overflow: hidden;
                -webkit-transform: skew(-10deg);
                transform: skew(-10deg);
                -webkit-backface-visibility: hidden;
                backface-visibility: hidden;
                -webkit-transition: all .2s ease;
                transition: all .2s ease;
                font-family: sans-serif;
                background: #888;
            }

                .tgl-skewed + .tgl-btn:after, .tgl-skewed + .tgl-btn:before {
                    -webkit-transform: skew(10deg);
                    transform: skew(10deg);
                    display: inline-block;
                    -webkit-transition: all .2s ease;
                    transition: all .2s ease;
                    width: 100%;
                    text-align: center;
                    position: absolute;
                    line-height: 2em;
                    font-weight: bold;
                    color: #fff;
                    text-shadow: 0 1px 0 rgba(0, 0, 0, 0.4);
                }

                .tgl-skewed + .tgl-btn:after {
                    left: 100%;
                    content: attr(data-tg-on);
                }

                .tgl-skewed + .tgl-btn:before {
                    left: 0;
                    content: attr(data-tg-off);
                }

                .tgl-skewed + .tgl-btn:active {
                    background: #888;
                }

                    .tgl-skewed + .tgl-btn:active:before {
                        left: -10%;
                    }

            .tgl-skewed:checked + .tgl-btn {
                background: #86d993;
            }

                .tgl-skewed:checked + .tgl-btn:before {
                    left: -100%;
                }

                .tgl-skewed:checked + .tgl-btn:after {
                    left: 0;
                }

                .tgl-skewed:checked + .tgl-btn:active:after {
                    left: 10%;
                }

            select {
                padding: 0 4px;
            }


            .table > tbody > tr > td {
                vertical-align: middle;
            }

            label {
                margin-bottom: 0px;
                margin: 4px;
            }
        </style>
        <link type="text/css" rel="stylesheet" href="../css/style.css" />
        <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
        <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
        <script src="../js/jQuery-2.2.0.min.js" type="text/javascript"></script>
    </head>
    <body>
        <div class="container" style="max-width: 100%">
            <ul class="nav nav-tabs" id="ul_tab_menu"></ul>
            <div class="tab-content">
            </div>
            <div style="text-align: center">
                <br />
                <button type="button" class="btn btn-primary" id="submit">Submit</button>
            </div>
        </div>
    </body>
    </html>
</asp:Content>

