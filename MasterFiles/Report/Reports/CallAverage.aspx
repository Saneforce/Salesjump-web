<%@ Page Title="Call Average" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master_MR.master" CodeFile="CallAverage.aspx.cs" Inherits="Reports_CallAverage" %>

<%--<%@ Register Src ="~/UserControl/MGR_Menu.ascx" TagName ="Menu1" TagPrefix="ucl" %>
<%@ Register Src ="~/UserControl/MR_Menu.ascx" TagName ="Menu2" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Call Average</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
     <script type="text/javascript">
         var popUpObj;
         function showModalPopUp(sfcode, fmon, fyr, To_Month, To_Year, sf_name, div_Code, Mode) {
             //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
             popUpObj = window.open("rptCallAverage.aspx?sf_code=" + sfcode + "&frm_month=" + fmon + "&frm_year=" + fyr + "&To_Month=" + To_Month + "&To_Year=" + To_Year + "&sf_name=" + sf_name + "&div_Code=" + div_Code + "&Mode=" + Mode,
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
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $('form').live("submit", function () {
            ShowProgress();
        });
    </script>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
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
                            $('#<%=btnSubmit.ClientID%>').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });
            $('#<%=btnSubmit.ClientID%>').click(function () {

                var ddlDivisionName = $('#<%=ddlDivision.ClientID%> :selected').text();
                if (ddlDivisionName == "--Select--") { alert("Select Division."); $('#<%=ddlDivision.ClientID%>').focus(); return false; }
                var grp = $('#<%=ddlOption.ClientID%> :selected').text();
                if (grp == "---Select---") { alert("Select Mode."); $('#<%=ddlOption.ClientID%>').focus(); return false; }
                var grpFieldForce = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (grpFieldForce == "---Select---") { alert("Select Field Force."); $('#<%=ddlFieldForce.ClientID%>').focus(); return false; }
                var Month = $('#<%=ddlMonth.ClientID%> :selected').text();
                if (Month == "---Select---") { alert("Select Month."); $('#<%=ddlMonth.ClientID%>').focus(); return false; }
                var Year = $('#<%=ddlYear.ClientID%> :selected').text();
                if (Year == "---Select---") { alert("Select Year."); $('#<%=ddlYear.ClientID%>').focus(); return false; }
                var FrmMonth = $('#<%=ddlFrmMonth.ClientID%> :selected').text();
                if (FrmMonth == "---Select---") { alert("Select From Month."); $('#<%=ddlFrmMonth.ClientID%>').focus(); return false; }
                var FrmYear = $('#<%=ddlFrmYear.ClientID%> :selected').text();
                if (FrmYear == "---Select---") { alert("Select From Year."); $('#<%=ddlFrmYear.ClientID%>').focus(); return false; }
                var ToMonth = $('#<%=ddlToMonth.ClientID%> :selected').text();
                if (ToMonth == "---Select---") { alert("Select To Month."); $('#<%=ddlToMonth.ClientID%>').focus(); return false; }
                var ToMonth = $('#<%=ddlToYear.ClientID%> :selected').text();
                if (ToMonth == "---Select---") { alert("Select To Year."); $('#<%=ddlToYear.ClientID%>').focus(); return false; }

                if (grpFieldForce != '') {
                    var ddlFieldForceValue = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                }

                if (Month != '') {
                    var ddlMonth = document.getElementById('<%=ddlMonth.ClientID%>').value;
                }
                if (Year != '') {
                    var ddlYear = document.getElementById('<%=ddlYear.ClientID%>').value;
                }
                if (FrmMonth != '') {
                    var ddlFrmMonth = document.getElementById('<%=ddlFrmMonth.ClientID%>').value;
                }
                if (FrmYear != '') {
                    var ddlFrmYear = document.getElementById('<%=ddlFrmYear.ClientID%>').value;
                }
                if (ToMonth != '') {
                    var ddlToMonth = document.getElementById('<%=ddlToMonth.ClientID%>').value;
                }
                //alert(ddlFrmMonth)


                if (FrmYear != '') {
                    var ddlToYear = document.getElementById('<%=ddlToYear.ClientID%>').value;
                }


                var ddlOption = document.getElementById('<%=ddlOption.ClientID%>').value;

                if (ddlDivisionName != '') {
                    var ddlDivision = document.getElementById('<%=ddlDivision.ClientID%>').value;
                }



                if (grp == "MonthWise") {

                    showModalPopUp(ddlFieldForceValue, ddlMonth, ddlYear, 0, 0, grpFieldForce, ddlDivision, grp)
                }
                else if (grp == "Periodically") {

                    showModalPopUp(ddlFieldForceValue, ddlFrmMonth, ddlFrmYear, ddlToMonth, ddlToYear, grpFieldForce, ddlDivision, grp)
                }
                else if (grp == "Periodically All Field Force") {

                    showModalPopUp(ddlFieldForceValue, ddlFrmMonth, ddlFrmYear, ddlToMonth, ddlToYear, grpFieldForce, ddlDivision, grp)

                }
            });
        }); 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
        <div id="Divid" runat="server"></div>
            <%--<ucl:Menu ID="Menu1" runat="server" />--%>
        </div>
        <br />
        <center>
            <table>
                <tr>
                    <td align="left" class="stylespc" width="120px">
                        <asp:Label ID="lblDivision" runat="server" Text="Division Name " SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlDivision" runat="server" SkinID="ddlRequired" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="Label1" runat="server" Text="Mode" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlOption" runat="server" AutoPostBack="true" SkinID="ddlRequired"
                            OnSelectedIndexChanged="ddlOption_SelectedIndexChanged1">
                            <asp:ListItem >---Select---</asp:ListItem>
                            <asp:ListItem >MonthWise</asp:ListItem>
                            <asp:ListItem >Periodically</asp:ListItem>
                            <asp:ListItem >Periodically All Field Force</asp:ListItem>                          
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblFilter" runat="server" Text="Filed Force Name " SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired">
                        <asp:ListItem Selected="True">---Select---</asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired">
                        </asp:DropDownList>
                    </td>
                </tr>

                <asp:Panel ID="pnlMonthly" Visible="false" runat="server">
                 <tr>
                        <td align="left" class="stylespc">
                            <asp:Label ID="lblMonth" runat="server" Text="Month" SkinID="lblMand"></asp:Label>
                        </td>
                        <td align="left" class="stylespc">
                            <asp:DropDownList ID="ddlMonth" runat="server" SkinID="ddlRequired">
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
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="stylespc">
                            <asp:Label ID="lblYear" runat="server" SkinID="lblMand" Text="Year"></asp:Label>
                        </td>
                        <td align="left" class="stylespc">
                            <asp:DropDownList ID="ddlYear" runat="server" Width="80px" SkinID="ddlRequired">
                                <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                   
                </asp:Panel>
                <asp:Panel ID="pnlPeriodically" Visible="false" runat="server">
                    <tr>
                        <td align="left" class="stylespc">
                            <asp:Label ID="lblFrmMoth" runat="server" Text="From Month" SkinID="lblMand"></asp:Label>
                        </td>
                        <td align="left" class="stylespc">
                            <asp:DropDownList ID="ddlFrmMonth" runat="server" SkinID="ddlRequired">
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
                            <asp:Label ID="Label2" runat="server" SkinID="lblMand" Text="From Year"></asp:Label>
                            <asp:DropDownList ID="ddlFrmYear" runat="server" Width="80px" SkinID="ddlRequired">
                                <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="stylespc">
                            <asp:Label ID="Label4" runat="server" SkinID="lblMand" Text="To Month"></asp:Label>
                        </td>
                        <td align="left" class="stylespc">
                            <asp:DropDownList ID="ddlToMonth" runat="server" SkinID="ddlRequired">
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
                            <asp:Label ID="lblToYear" runat="server" Text="To Year" SkinID="lblMand"></asp:Label>
                            <span style="margin-left:14px"></span>
                            <asp:DropDownList ID="ddlToYear" runat="server" Width="80px" SkinID="ddlRequired">
                                <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                        </td>
                        <td align="left">
                        </td>
                    </tr>
                </asp:Panel>
            </table>
            <br />
            <asp:Button ID="btnSubmit" runat="server" Width="70px" Height="25px" Text="View"
                CssClass="btnnew" />
        </center>
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
</asp:Content>