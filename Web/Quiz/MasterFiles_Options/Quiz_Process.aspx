<%@ Page Language="C#"  MasterPageFile="~/Master.master"  AutoEventWireup="true" CodeFile="Quiz_Process.aspx.cs" Inherits="MasterFiles_Options_Quiz_Process" %>

<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Online Quiz - Processing Zone</title>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.js" type="text/javascript"></script>
    <script src="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/jquery-ui.js" type="text/javascript"></script>
    <link href="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/themes/humanity/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <link href="../../css/timepicker/bootstrap-clockpicker.min.css" rel="stylesheet"
        type="text/css" />
    <link href="../../JScript/DateJs/assets/css/github.min.css" rel="stylesheet" type="text/css" />
 
    <script type="text/javascript">

        var j = jQuery.noConflict();
        j(document).ready(function () {
            j('#datepicker').datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: '2018:2020',
                dateFormat: 'mm/dd/yy'
            });

            j("#datepicker").datepicker("setDate", new Date());

        });

        var j = jQuery.noConflict();
        j(document).ready(function () {
            j('#datepickerFrom').datepicker({
                changeMonth: true,
                changeYear: true,
                //    yearRange: '2017:' + new Date().getFullYear().toString(),
                yearRange: '2018:2020',
                dateFormat: 'mm/dd/yy'
            });

            j("#datepickerFrom").datepicker("setDate", new Date());

        });
        var j = jQuery.noConflict();
        j(document).ready(function () {
            j('#datepickerTo').datepicker({
                changeMonth: true,
                changeYear: true,
                //   yearRange: '2017:' + new Date().getFullYear().toString(),
                yearRange: '2018:2020',
                dateFormat: 'mm/dd/yy'
            });

            j("#datepickerTo").datepicker("setDate", new Date());

        });
    
    </script>
     <script type="text/javascript">
         function showDrop(select) {

             if (select.value == 0) {
                 document.getElementById('ddlFrom').style.display = "none";
                 document.getElementById('ddlTo').style.display = "none";
                 document.getElementById('lbljoin').style.display = "none";
                 document.getElementById('lblSt').style.display = "none";
                 document.getElementById('ddlst').style.display = "none";
                 document.getElementById('lblDesig').style.display = "none";
                 document.getElementById('ddlDesig').style.display = "none";
                 document.getElementById('lblsub').style.display = "none";
                 document.getElementById('ddlsubdiv').style.display = "none";
               //  document.getElementById('btngo').style.display = "none";
             }
             else if (select.value == 1) {
                 document.getElementById('ddlFrom').style.display = "block";
                 document.getElementById('ddlTo').style.display = "block";
                 document.getElementById('lbljoin').style.display = "block";
                 document.getElementById('lblSt').style.display = "block";
                 document.getElementById('ddlst').style.display = "block";
                 document.getElementById('lblDesig').style.display = "none";
                 document.getElementById('ddlDesig').style.display = "none";
                 document.getElementById('lblsub').style.display = "none";
                 document.getElementById('ddlsubdiv').style.display = "none";
               //  document.getElementById('btngo').style.display = "block";

             }

             else if (select.value == 2) {
                 document.getElementById('ddlFrom').style.display = "none";
                 document.getElementById('ddlTo').style.display = "none";
                 document.getElementById('lbljoin').style.display = "none";
                 document.getElementById('lblSt').style.display = "none";
                 document.getElementById('ddlst').style.display = "none";
                 document.getElementById('lblDesig').style.display = "block";
                 document.getElementById('ddlDesig').style.display = "block";
                 document.getElementById('lblsub').style.display = "none";
                 document.getElementById('ddlsubdiv').style.display = "none";

             }
             else if (select.value == 3) {
                 document.getElementById('ddlFrom').style.display = "none";
                 document.getElementById('ddlTo').style.display = "none";
                 document.getElementById('lbljoin').style.display = "none";
                 document.getElementById('lblSt').style.display = "none";
                 document.getElementById('ddlst').style.display = "none";
                 document.getElementById('lblDesig').style.display = "none";
                 document.getElementById('ddlDesig').style.display = "none";
                 document.getElementById('lblsub').style.display = "block";
                 document.getElementById('ddlsubdiv').style.display = "block";

             }
         }
    </script>
    <script type="text/javascript">
        $(document)["ready"](function () {
            $('#btnback').on('click', function () {
                window.location = 'Quiz_List.aspx';
            });
            $("#loading")["show"]();
            var e = document.getElementById("ddlwise");
            var mode = e.options[e.selectedIndex].value;
            if (mode == 0) {
                $('#<%=ddlFrom.ClientID%>').css('display', 'none');
                $('#<%=ddlTo.ClientID%>').css('display', 'none');
                $('#<%=lbljoin.ClientID%>').css('display', 'none');
                $('#<%=lblSt.ClientID%>').css('display', 'none');
                $('#<%=ddlst.ClientID%>').css('display', 'none');
                $('#<%=lblDesig.ClientID%>').css('display', 'none');
                $('#<%=ddlDesig.ClientID%>').css('display', 'none');
                $('#<%=lblsub.ClientID%>').css('display', 'none');
                $('#<%=ddlsubdiv.ClientID%>').css('display', 'none');
                // $('#btngo').css('display','none');
            }
            var TMonth = $('#<%=ddlFrom.ClientID%>').val();
            var TYear = $('#<%=ddlTo.ClientID%>').val();
            var St = $('#<%=ddlst.ClientID%>').val();
            var Sub = $('#<%=ddlsubdiv.ClientID%>').val();
            var Data = mode + "^" + TMonth + "^" + TYear + "^" + St + "^" + Sub
            $.ajax({
                type: "POST",
                url: "Quiz_Process.aspx/BindUserList",
                async:false,
                data: '{objData:' + JSON.stringify(Data) + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (variable_0) {
                    if (variable_0["d"]["length"] > 0) {
                        var variable_1 = "<thead><tr>";
                        variable_1 += "<th style=\"width:34px\"><input type=\"checkbox\" id=\"chkHead\" class=\"selectAll\" onchange=\"checkAll(this)\"/><div><input type=\"checkbox\" id=\"chkHead\" class=\"selectAll\" style=\"border-color:white\" onchange=\"checkAll(this)\"/></div></th>";
                        variable_1 += "<th style=\"width:33px\">S.No<div>S.No</div></th>";
                        variable_1 += "<th style=\"width:270px\">Field Force Name<div>Field Force Name</div></th>";
                        variable_1 += "<th style=\"width:130px\">HQ<div>HQ</div></th>";
                        variable_1 += "<th style=\"width:10px\">Designation<div>Designation</div></th>";
                        variable_1 += "<th style=\"width:167px\">State<div>State</div></th>";
                        variable_1 += "<th style=\"display:none\">SF Code<div>SF Code</div></th>";
                        variable_1 += "</tr></thead>";
                        variable_1 += "<tbody>";
                        for (var variable_2 = 0; variable_2 < variable_0["d"]["length"]; variable_2++) {
                            variable_1 += "<tr>";
                            if (variable_0["d"][variable_2]["ChkBox"] == "True") {
                                variable_1 += "<td style=\"width:50px;color:Blue;text-decoration: line-through\"><input type=\"checkbox\" class = \"chcktbl\" disabled=\"disabled\" style=\"display:none;border-color:Red;\"  id=\"delit_" + variable_0["d"][variable_2]["RowNo"] + "\" /></td>";
                                variable_1 += "<td style=\"width:50px;color:Blue;text-decoration: line-through\">" + variable_0["d"][variable_2]["RowNo"] + "</td>";
                                variable_1 += "<td id=\"SFName\" style=\"width:250px;color:Blue;text-decoration: line-through\">" + variable_0["d"][variable_2]["FieldForceName"] + "</td>"
                            } else {
                                variable_1 += "<td style=\"width:50px\"><input type=\"checkbox\" class = \"chcktbl\" id=\"delit_" + variable_0["d"][variable_2]["RowNo"] + "\" /></td>";
                                variable_1 += "<td style=\"width:50px\">" + variable_0["d"][variable_2]["RowNo"] + "</td>";
                                variable_1 += "<td id=\"SFName\" style=\"width:250px;\">" + variable_0["d"][variable_2]["FieldForceName"] + "</td>"
                            };
                            variable_1 += "<td style=\"width:150px\">" + variable_0["d"][variable_2]["HQ"] + "</td>";
                            variable_1 += "<td style=\"width:100px\">" + variable_0["d"][variable_2]["Designation"] + "</td>";
                            variable_1 += "<td style=\"width:150px\">" + variable_0["d"][variable_2]["State"] + "</td>";
                            variable_1 += "<td style=\"display:none\">" + variable_0["d"][variable_2]["SF_Code"] + "</td>";
                            variable_1 += "</tr>"
                        };
                        variable_1 += "</tbody>";
                        $("#tblUserList")["html"](variable_1);
                        $("#loading")["hide"]()
                    }
                    else {
                        $("#loading")["hide"]()
                    }
                },
                error: function (variable_3) {
                    $("#loading")["hide"]()
                }
            });
            $("#btngo").click(function () {
                var e = document.getElementById("ddlwise");
                var mode = e.options[e.selectedIndex].value;
                var TMonth = $('#<%=ddlFrom.ClientID%>').val();
                var TYear = $('#<%=ddlTo.ClientID%>').val();
                var St = $('#<%=ddlst.ClientID%>').val();
                var Desig = $('#<%=ddlDesig.ClientID%> :selected').text();
                var Sub = $("#<%=ddlDesig.ClientID%> option:selected").val();
                var Data = mode + "^" + TMonth + "^" + TYear + "^" + St + "^" + Desig + "^" + Sub
                $("#loading").show();
                $.ajax({
                    type: "POST",
                    url: "Quiz_Process.aspx/BindUserList",
                    async:false,
                    data: '{objData:' + JSON.stringify(Data) + '}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (variable_0) {
                        if (variable_0["d"]["length"] > 0) {
                            var variable_1 = "<thead><tr>";
                            variable_1 += "<th style=\"width:34px\"><input type=\"checkbox\" id=\"chkHead\" class=\"selectAll\" onchange=\"checkAll(this)\"/><div><input type=\"checkbox\" id=\"chkHead\" class=\"selectAll\" style=\"border-color:white\" onchange=\"checkAll(this)\"/></div></th>";
                            variable_1 += "<th style=\"width:33px\">S.No<div>S.No</div></th>";
                            variable_1 += "<th style=\"width:270px\">Field Force Name<div>Field Force Name</div></th>";
                            variable_1 += "<th style=\"width:130px\">HQ<div>HQ</div></th>";
                            variable_1 += "<th style=\"width:10px\">Designation<div>Designation</div></th>";
                            variable_1 += "<th style=\"width:167px\">State<div>State</div></th>";
                            variable_1 += "<th style=\"display:none\">SF Code<div>SF Code</div></th>";
                            variable_1 += "</tr></thead>";
                            variable_1 += "<tbody>";
                            for (var variable_2 = 0; variable_2 < variable_0["d"]["length"]; variable_2++) {
                                variable_1 += "<tr>";
                                if (variable_0["d"][variable_2]["ChkBox"] == "True") {
                                    variable_1 += "<td style=\"width:50px;color:Blue;text-decoration: line-through\"><input type=\"checkbox\" class = \"chcktbl\" disabled=\"disabled\" style=\"display:none;border-color:Red;\"  id=\"delit_" + variable_0["d"][variable_2]["RowNo"] + "\" /></td>";
                                    variable_1 += "<td style=\"width:50px;color:Blue;text-decoration: line-through\">" + variable_0["d"][variable_2]["RowNo"] + "</td>";
                                    variable_1 += "<td id=\"SFName\" style=\"width:250px;color:Blue;text-decoration: line-through\">" + variable_0["d"][variable_2]["FieldForceName"] + "</td>"
                                } else {
                                    variable_1 += "<td style=\"width:50px\"><input type=\"checkbox\" class = \"chcktbl\" id=\"delit_" + variable_0["d"][variable_2]["RowNo"] + "\" /></td>";
                                    variable_1 += "<td style=\"width:50px\">" + variable_0["d"][variable_2]["RowNo"] + "</td>";
                                    variable_1 += "<td id=\"SFName\" style=\"width:250px;\">" + variable_0["d"][variable_2]["FieldForceName"] + "</td>"
                                };
                                variable_1 += "<td style=\"width:150px\">" + variable_0["d"][variable_2]["HQ"] + "</td>";
                                variable_1 += "<td style=\"width:100px\">" + variable_0["d"][variable_2]["Designation"] + "</td>";
                                variable_1 += "<td style=\"width:150px\">" + variable_0["d"][variable_2]["State"] + "</td>";
                                variable_1 += "<td style=\"display:none\">" + variable_0["d"][variable_2]["SF_Code"] + "</td>";
                                variable_1 += "</tr>"
                            };
                            variable_1 += "</tbody>";
                            $("#tblUserList")["html"](variable_1);
                            $("#loading")["hide"]()
                        }
                        else {


                            var variable_1 = "<thead><tr>";
                            variable_1 += "<th style=\"width:34px\"><input type=\"checkbox\" id=\"chkHead\" class=\"selectAll\" onchange=\"checkAll(this)\"/><div><input type=\"checkbox\" id=\"chkHead\" class=\"selectAll\" style=\"border-color:white\" onchange=\"checkAll(this)\"/></div></th>";
                            variable_1 += "<th style=\"width:33px\">S.No<div>S.No</div></th>";
                            variable_1 += "<th style=\"width:270px\">Field Force Name<div>Field Force Name</div></th>";
                            variable_1 += "<th style=\"width:130px\">HQ<div>HQ</div></th>";
                            variable_1 += "<th style=\"width:10px\">Designation<div>Designation</div></th>";
                            variable_1 += "<th style=\"width:167px\">State<div>State</div></th>";
                            variable_1 += "<th style=\"display:none\">SF Code<div>SF Code</div></th>";
                            variable_1 += "</tr></thead>";
                            variable_1 += "<tbody>";
                            variable_1 += "<tr>";
                            variable_1 += "<td colspan=\"6\" style=\"font-size:15px;color:Red;width:250px;\">No Records Found</td>";
                            variable_1 += "</tr>"
                            variable_1 += "</tbody>";
                            $("#tblUserList")["html"](variable_1);
                            $("#loading")["hide"]()
                        }
                    },
                    error: function (variable_3) {
                        $("#loading")["hide"]()
                    }
                });
            });
            $("#btnProcess")["click"](function () {
                if ($("#tblUserList input[type=checkbox]:checked")["length"] === 0) {
                    event["preventDefault"]();
                    var variable_4 = "Please Select Atleast one Checkbox";
                    createCustomAlert(variable_4)
                } else {
                    var variable_5 = $("#txtTime")["val"]();
                    var variable_6 = $("#datepicker")["val"]();
                    var variable_7 = new Date();
                    var variable_8 = variable_7["getMonth"]() + 1;
                    var variable_9 = variable_7["getFullYear"]();
                    var variable_10 = variable_8 + "^" + variable_9;
                    var variable_11 = $("#ddlType")["val"]();
                    var variable_12 = $("#ddlNoOfAttempt")["val"]();
                    selected = $("#tblUserList tr td input[type='checkbox']:checked");
                    var variable_13 = $("#tblUserList");
                    var variable_14 = $("#tblUserList tr td");
                    var variable_15 = $("#datepickerFrom")["val"]();
                    var variable_16 = $("#datepickerTo")["val"]();
                    var variable_00 = [];
                    $("input:checkbox:checked", variable_14)["each"](function (variable_2) {
                        var variable_01 = $(selected[variable_2])["closest"]("td")["siblings"]("td:eq(0)")["text"]() + "," + $(selected[variable_2])["closest"]("td")["siblings"]("td:eq(1)")["text"]() + "," + $(selected[variable_2])["closest"]("td")["siblings"]("td:eq(2)")["text"]() + "," + $(selected[variable_2])["closest"]("td")["siblings"]("td:eq(3)")["text"]() + "," + $(selected[variable_2])["closest"]("td")["siblings"]("td:eq(4)")["text"]() + "," + $(selected[variable_2])["closest"]("td")["siblings"]("td:eq(5)")["text"]() + "," + $(selected[variable_2])["closest"]("td")["siblings"]("td:eq(6)")["text"]() + "," + variable_5 + "," + variable_6 + "," + variable_11 + "," + variable_12 + "," + variable_8 + "," + variable_9 + "," + variable_15 + "," + variable_16;
                        variable_00["push"](variable_01)
                    })["get"]();
                    $.ajax({
                        type: "POST",
                        url: "Quiz_Process.aspx/AddProcessDetails",
                        async:false,
                        data: "{objUserData:" + JSON["stringify"](variable_00) + ",objMonth:" + JSON["stringify"](variable_10) + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (variable_0) { },
                        error: function (variable_3) { }
                    })
                }
            });
            var variable_02 = document["getElementById"]("tblUserList")["rows"]["length"];
            var variable_03 = [];
            for (var variable_2 = 1; variable_2 < variable_02; variable_2++) {
                variable_03[variable_2] = document["getElementById"]("tblUserList")["rows"][variable_2];
                variable_03[variable_2]["onmouseover"] = function () {
                    this["style"]["backgroundColor"] = "#f3f8aa"
                };
                variable_03[variable_2]["onmouseout"] = function () {
                    this["style"]["backgroundColor"] = "#ffffff"
                }
            }
        });

        function checkAll(variable_05) {
            var variable_06 = document["getElementsByTagName"]("input");
            if (variable_05["checked"]) {
                for (var variable_2 = 0; variable_2 < variable_06["length"]; variable_2++) {
                    if (variable_06[variable_2]["type"] == "checkbox" && variable_06[variable_2]["style"]["borderColor"] == "red") {
                        variable_06[variable_2]["checked"] = false
                    } else {
                        variable_06[variable_2]["checked"] = true
                    }
                }
            } else {
                for (var variable_2 = 0; variable_2 < variable_06["length"]; variable_2++) {
                    if (variable_06[variable_2]["type"] == "checkbox") {
                        variable_06[variable_2]["checked"] = false
                    }
                }
            }
        }
    </script>
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script src="../../JScript/jquery.min.js" type="text/javascript"></script>
    <link href="../../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>    
 
    <%--<script src="../../js/Quiz_JS/AddQuiz_ProcessingJS.js" type="text/javascript"></script>--%>
    <link href="../Quiz_ProcessCSS.css" rel="stylesheet"
        type="text/css" />
  
  <%--  <script src="../../JScript/Add_QuizProcessing_JS.js" type="text/javascript"></script>--%>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.hide();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $('form').live("submit", function () {
            ShowProgress();
        });
    </script>
    <script type="text/javascript">

        //         function preventMultipleSubmissions() 
        //         {
        //             $('#btnProcess').prop('disabled', true);
        //         }
        //         window.onbeforeunload = preventMultipleSubmissions;
    </script>

<%--</head>
<body>--%>
    <form id="form1" runat="server">
      <div class="row">
        <div class="col-lg-12 sub-header">
            Quiz Process  <button type="button" class="btn btn-primary" style="float:right;" id="btnback">Back</button>
        </div>
    </div>
    <%--<ucl:Menu ID="menu1" runat="server" />--%>
     <br />
    <center style="display:none;">
        <table>
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblFilter" runat="server" Visible="false">Filter By</asp:Label>
                </td>
                <td align="left" class="stylespc" colspan="2">
                    <select id="ddlwise" name="form_select" style="FONT-SIZE: xx-small;display:none; COLOR: #000000; padding: 1px 3px  0.2em; Height:24px;
    BORDER-TOP-STYLE: groove;    FONT-FAMILY: Verdana;    BORDER-RIGHT-STYLE: groove;    BORDER-LEFT-STYLE: groove;       BORDER-BOTTOM-STYLE: groove;
                        width: 80px;" onchange="showDrop(this)">
                        <option value="0">ALL</option>
                        <option value="1">DOJ</option>
                          <option value="2">Desig.</option>
                        <option value="3">Subdiv.</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblSt" runat="server">State</asp:Label>
                </td>
                <td align="left" class="stylespc"  colspan="2">
                    <asp:DropDownList ID="ddlst" runat="server" SkinID="ddlRequired">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="lbljoin" runat="server">DOJ Mnth & Yr</asp:Label>
                </td>
                <td align="left" class="stylespc">
                    <asp:DropDownList ID="ddlFrom" runat="server" SkinID="ddlRequired">
                        <asp:ListItem Value="0" Text="ALL"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                        <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                        <asp:ListItem Value="5" Text="May"></asp:ListItem>
                        <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                        <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                        <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                        <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                        <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                        <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                        <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="left" class="stylespc">
                    <asp:DropDownList ID="ddlTo" runat="server" SkinID="ddlRequired">
                        <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
             <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblDesig" runat="server">Desig</asp:Label>
                </td>
                <td align="left" class="stylespc"  colspan="2">
                    <asp:DropDownList ID="ddlDesig" runat="server" SkinID="ddlRequired">
                    </asp:DropDownList>
                </td>
            </tr>
             <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblsub" runat="server">Subdivision</asp:Label>
                </td>
                <td align="left" class="stylespc"  colspan="2">
                    <asp:DropDownList ID="ddlsubdiv" runat="server" SkinID="ddlRequired">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
        <%-- <asp:Button ID="btngo" runat="server" Text="Go" />--%>
        <input type="button" id="btngo" value="Go" style="background-color: Yellow;display:none;" />
    </center>
    <div style="width: 100%">
        <div style="margin-top: 20px;">
            <div class="UserDiv">
                <div class="TitleDiv">
                    <span>User List</span>
                </div>
                <div>
                    <div class="headercontainer">
                        <div class="tablecontainer">
                            <table id="tblUserList">
                            </table>
                        </div>
                    </div>
                </div>
                <div>
                    <input type="hidden" id="hdnSfCode" />
                </div>
            </div>
            <div class="ProcessDiv">
                <div class="TitleDiv">
                    <span>Process</span>
                </div>
                <div class="ProcessSub_Div">
                    <div style="height: 300px; margin-top: 5%">
                        <center>
                            <table id="tblProcess" style="width: 70%">
                                <tr>
                                    <td>
                                        <span id="lblTime">Time Limit</span>
                                    </td>
                                    <td>
                                        <div class="form-group">
                                            <div class="input-group clockpicker">
                                                <input type="text" class="form-control Textbox" id="txtTime" value="00:10" />
                                                <span class="input-group-addon"><span class="glyphicon glyphicon-time"></span></span>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span id="lblProcessDate">Date</span>
                                    </td>
                                    <td>
                                        <input type="text" id="datepicker" class="Textbox" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span id="lblType">Type</span>
                                    </td>
                                    <td>
                                        <select id="ddlType" class="dropDown">
                                            <option>Shuffle</option>
                                            <option selected="selected">No Shuffle</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span id="lblNoOfAttempt">No Of Attempts</span>
                                    </td>
                                    <td>
                                        <select id="ddlNoOfAttempt" class="dropDown">
                                            <option selected="selected">1</option>
                                            <option >2</option>
                                            <%--<option>3</option>--%>
                                        </select>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <span id="Span1">Process From Date</span>
                                    </td>
                                    <td>
                                        <input type="text" id="datepickerFrom" class="Textbox" />
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <span id="Span2">Process To Date</span>
                                    </td>
                                    <td>
                                        <input type="text" id="datepickerTo" class="Textbox" />
                                    </td>
                                </tr>
                            </table>
                            <div style="margin-top: 3%">
                                <input type="submit" id="btnProcess" value="Process" class="ProcessBtn" />
                                <%--<button class="btn btn-primary btn-lg" id="btnProcess" data-toggle="modal" data-target="#processing-modal">
                                <i class="glyphicon glyphicon-play"></i>Start Processing
                            </button>--%>
                            </div>

                            <%--<input type="submit" id="btnEx" value="Test"  class="ProcessBtn" />--%>

                        </center>
                    </div>
                </div>
            </div>
        </div>
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>
        <div id="loading" class="bar" style="display:none;">
            <p>loading</p>
        </div>
    </div>
    </form>
    <script src="../../JScript/DateJs/assets/js/jquery.min.js" type="text/javascript"></script>
    <script src="../../js/plugins/timepicker/bootstrap-clockpicker.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var k = jQuery.noConflict();
        k('.clockpicker').clockpicker()
	    .find('input').change(function () {
	        console.log(this.value);
	    });
   </script>
<%--</body>
</html>--%>
</asp:Content>