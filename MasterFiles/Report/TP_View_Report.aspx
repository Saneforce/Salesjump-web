<%@ Page Title="TP View Report" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master"
    CodeFile="TP_View_Report.aspx.cs" Inherits="MasterFiles_Report_TP_View_Report" %>

<%--<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>
<%@ Register Src ="~/UserControl/MGR_Menu.ascx" TagName ="Menu1" TagPrefix="ucl" %>
<%@ Register Src ="~/UserControl/MR_Menu.ascx" TagName ="Menu2" TagPrefix="ucl" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!doctype html public "-//w3c//dtd xhtml 1.0 transitional//en" "http://www.w3.org/tr/xhtml1/dtd/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1">
        <title>TP View Report</title>
        <link type="text/css" rel="stylesheet" href="../css/style1.css" />
        <style type="text/css">
            #tblTpRpt
            {
            }
            input[type='text'], select, label
            {
                line-height: 22px;
                padding: 4px 6px;
                font-size: medium;
                border-radius: 7px;
                width: 100%;
                font-weight: normal;
            }
			.ddlDivision{
                height:31px !important;
            }
        </style>
        <script type="text/javascript">
            var popUpObj;
            function showModalPopUp(sfcode, fmon, fyr, level, sf_name) {
                //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
                popUpObj = window.open("rptTPView.aspx?sf_code=" + sfcode + "&cur_month=" + fmon + "&cur_year=" + fyr + "&level=-1" + "&sf_name=" + sf_name,
                    "ModalPopUp",
                    "toolbar=no," +
                    "scrollbars=yes," +
                    "location=no," +
                    "statusbar=no," +
                    "menubar=no," +
                    "addressbar=no," +
                    "resizable=yes," +
                    "width=800," +
                    "height=600," +
                    "left = 0," +
                    "top=0"
                    );
                popUpObj.focus();
                // LoadModalDiv();
            }
        </script>
        <script type="text/javascript" language="javascript">

            function MyFunc(hypLevel, ddlFF, ddlMon, ddlYr) {
                var hypLevel = document.getElementById(hypLevel);
                var ddlFF = document.getElementById(ddlFF);
                var ddlMon = document.getElementById(ddlMon);
                var ddlYr = document.getElementById(ddlYr);
                //hypLevel.href =

            }    
        </script>
        <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
        <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
        <script type="text/javascript">
            $(document).ready(function () {
                //   $('input:text:first').focus();
                $('input:text').bind("keydown", function (e) {
                    var n = $("input:text").length;
                    if (e.which == 13) { //Enter key
                        e.preventDefault(); //to skip default behavior of the enter key
                        var curIndex = $('input:text').index(this);
                        if ($('input:text')[curIndex].attributes['onfocus'].value != "this.style.backgroundColor='LavenderBlush'" && ($('input:text')[curIndex].value == '')) {
                            $('input:text')[curIndex].focus();
                        }
                        else {
                            var nextIndex = $('input:text').index(this) + 1;

                            if (nextIndex < n) {
                                e.preventDefault();
                                $('input:text')[nextIndex].focus();
                            }
                            else {
                                $('input:text')[nextIndex - 1].blur();
                                $('#btnSubmit').focus();
                            }
                        }
                    }
                });
                $("input:text").on("keypress", function (e) {
                    if (e.which === 32 && !this.value.length)
                        e.preventDefault();
                });

            });

            function NewWindow() {
                var Sf = $('#<%=ddlFilterby.ClientID%> :selected').text();
                if (Sf == "---Select---") { alert("Select Filter by."); $('#ddlFilterby').focus(); return false; }
                var Name = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (Name == "---Select---") { alert("Select Fieldforce Name."); $('#ddlFieldForce').focus(); return false; }
                var Year = $('#<%=ddlYear.ClientID%> :selected').text();
                if (Year == "---Select---") { alert("Select Year."); $('#ddlYear').focus(); return false; }
                var Month = $('#<%=ddlMonth.ClientID%> :selected').text();
                if (Month == "---Select---") { alert("Select Month."); $('#ddlMonth').focus(); return false; }
                var sf_Code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                var Year1 = document.getElementById('<%=ddlYear.ClientID%>').value;
                var Month1 = document.getElementById('<%=ddlMonth.ClientID%>').value;

                var rodhyp = $('#<%=rdoHypLevel.ClientID%>').val();

                if (rodhyp >= 0) {
                    var sURL = "rptTPView.aspx?sf_code=" + sf_Code + "&cur_month=" + Month1 + "&cur_year=" + Year1 + "&sf_name=" + Name + "&level=" + rodhyp;
                    window.open(sURL, null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');

                }
                else {
                    var sURL = "rptTPView.aspx?sf_code=" + sf_Code + "&cur_month=" + Month1 + "&cur_year=" + Year1 + "&sf_name=" + Name + "&level=-1";
                    window.open(sURL, null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');


                }

                //showModalPopUp(sf_Code, Month1, Year1, -1, Name);
            }
        </script>
    </head>
    <body>
        <form id="form1" runat="server">
        <div id="Divid" runat="server">
        </div>
        <asp:ScriptManager runat="server" ID="sm">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server">
            <ContentTemplate>
                <div class="container" style="width: 100%">
                    <div class="row">
                        <label id="lblFilterby1" runat="server" class="col-md-2 col-md-offset-3 control-label">
                            Sub Division</label>
                        <div class="col-md-4 inputGroupContainer">
                            <div class="input-group">
                                <span class="input-group-addon" id="icond" runat="server"><i class="glyphicon glyphicon-user"></i></span>
                                 <asp:DropDownList ID="ddlDivision" SkinID="ddlRequired" runat="server" AutoPostBack="true" CssClass="form-control ddlDivision" 
                                     OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged">

                                 </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <label id="lblFilterby" runat="server" class="col-md-2 col-md-offset-3 control-label">
                            Manager</label>
                        <div class="col-md-5 inputGroupContainer">
                            <div class="input-group">
                                <span id="icon" runat="server" class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                <asp:DropDownList ID="ddlFilterby" Width="360px" AutoPostBack="true" CssClass="form-control"
                                    OnSelectedIndexChanged="ddlFilterBy_SelectedChange" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <label id="lblFF" runat="server" class="col-md-2 col-md-offset-3 control-label">
                            Field Force</label>
                        <div class="col-md-5 inputGroupContainer">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged"
                                    CssClass="form-control" Visible="false">
                                    <asp:ListItem Value="0" Text="---Select---" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Alphabetical"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                                    OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" CssClass="form-control">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlFieldForce" runat="server" Width="360px" AutoPostBack="false"
                                    CssClass="form-control">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSF" runat="server" Visible="false" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <%--<tr>
               <td>
                   <asp:Label ID="lblDivision" runat="server" Text="Division"></asp:Label>
               </td>
               <td align="left">
                   <asp:DropDownList ID="ddlDivision" Width="110px" SkinID="ddlRequired" runat="server">
                   </asp:DropDownList>
               </td>
           </tr>--%>
                    <div class="row">
                        <label id="lblMonth" runat="server" class="col-md-2 col-md-offset-3 control-label">
                            Month</label>
                        <div class="col-md-3 inputGroupContainer">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                <asp:DropDownList ID="ddlMonth" runat="server" Width="120px" CssClass="form-control">
                                    <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
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
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <label id="lblYear" runat="server" class="col-md-2 col-md-offset-3 control-label">
                            Year</label>
                        <div class="col-md-3 inputGroupContainer">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                <asp:DropDownList ID="ddlYear" runat="server" Width="120px" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <asp:CheckBox ID="chkConsolidate" runat="server" Text="Consolidate" Font-Size="11px"
                        Font-Names="Verdana" AutoPostBack="true" OnCheckedChanged="chkConsolidate_CheckedChanged" />
                    <table>
                        <tr>
                            <td id="hypConsolidate" runat="server" align="left">
                                <%--  <asp:HyperLink ID="hypLevel1" runat="server" Text="Level1" NavigateUrl="javascript:MyFunc();" ></asp:HyperLink>
                    <asp:HyperLink ID="hypLevel2" runat="server" Text="Level2" NavigateUrl="#"></asp:HyperLink>
                    <asp:HyperLink ID="hypLevel3" runat="server" Text="Level3" NavigateUrl="#"></asp:HyperLink>
                    <asp:HyperLink ID="hypLevel4" runat="server" Text="Level4" NavigateUrl="#"></asp:HyperLink>--%>
                                <asp:RadioButtonList ID="rdoHypLevel" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Level1" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Level2" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Level3" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Level4" Value="4"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                    <div class="row">
                        <div class="col-md-6 col-md-offset-5">
                            <%-- <asp:Button ID="btnSubmit" runat="server" Width="100px" CssClass="btn btn-primary"
                                Text="View" OnClick="btnSubmit_Click" />--%>
                            <button id="btnGo" runat="server" onclick="NewWindow().this" class="btn btn-primary"
                                style="width: 100px">
                                <span>View</span></button>
                            <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                                Width="80%">
                            </asp:Table>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>
        </form>
    </body>
    </html>
</asp:Content>
