<%@ Page Title="Tour Plan Status Report" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master_MR.master" CodeFile="TP_Status_Report.aspx.cs"
    Inherits="Reports_TP_Status_Report" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Tour Plan Status Report</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />

    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(sfcode, fmon, fyr, Option, ddlState, sf_name) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rptTPStatus.aspx?sf_code=" + sfcode + "&cur_month=" + fmon + "&cur_year=" + fyr + "&Option=" + Option + "&state_code=" + ddlState + "&sf_name=" + sf_name,
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

                var sf_name = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                var ddlMRName = $('#<%=ddlState.ClientID%> :selected').text();
                if (sf_name == "---Select---") { alert("Select Fieldforce Name."); $('#<%=ddlFieldForce.ClientID%>').focus(); return false; }

                var Month = $('#<%=ddlMonth.ClientID%> :selected').text();
                if (Month == "---Select---") { alert("Select Month."); $('#<%=ddlMonth.ClientID%>').focus(); return false; }

                if (ddlMRName != '') {
                    var ddlState = document.getElementById('<%=ddlState.ClientID%>').value;
                }

                var ddlMonth = document.getElementById('<%=ddlMonth.ClientID%>').value;

                var ddlYear = document.getElementById('<%=ddlYear.ClientID%>').value

                var radio = document.getElementsByName('rdoMGRState');
                for (var i = 0; i < radio.length; i++) {
                    if (radio[i].checked) {
                        var Option = radio[i].value;
                    }
                }

                if (sf_name != '') {
                    var sf_code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                }

                if (ddlMRName != '') {
                    showModalPopUp(0, ddlMonth, ddlYear, Option, ddlState, sf_name);
                }
                else {
                    showModalPopUp(sf_code, ddlMonth, ddlYear, Option, 0, sf_name);
                }

            });
        }); 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="Divid" runat="server">
        </div>
        <center>

        <br />
            <br />
            <table>
                <%-- <tr>
                   <td align="left" class="lblSpace">
                        <asp:Label ID="lblDivision" Visible="false" runat="server" Text="Division Name " SkinID="lblMand"></asp:Label>
                    </td>
                  <td align="left">
                        <asp:DropDownList ID="ddlDivision" Visible="false" runat="server" SkinID="ddlRequired" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"
                            AutoPostBack="true" 
                            >
                        </asp:DropDownList>
                    </td>
                </tr>--%>

                <tr>
                    <td align="left" class="stylespc" width="120px">
                        <asp:Label ID="Label2" runat="server" Text="View By" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:RadioButtonList ID="rdoMGRState" runat="server" RepeatDirection="Horizontal"
                            Font-Names="Verdana" Font-Size="11px" AutoPostBack="true" OnSelectedIndexChanged="rdoMGRState_SelectedIndexChanged">
                            <asp:ListItem Value="0" Text="FieldForce-wise&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                Selected="True"></asp:ListItem>
                            <asp:ListItem Value="1" Text="State-wise"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblFieldforce" runat="server" Text="FieldForce Name" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged"
                            SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                            <%--<asp:ListItem Value="2" Text="HQ"></asp:ListItem>--%>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                            OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="false" SkinID="ddlRequired">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblState" runat="server" SkinID="lblMand" Text="State"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlState" runat="server" SkinID="ddlRequired">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblMonth" runat="server" SkinID="lblMand" Text="Month"></asp:Label>
                    </td>
                    <td align="left">
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
                    <td align="left">
                        <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="Label1" runat="server" Visible="false" SkinID="lblMand" Text="Without Vacants"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:CheckBox ID="chkVacant" Visible="false" runat="server" />
                    </td>
                </tr>
            </table>
            <br />
            <asp:Button ID="btnSubmit" runat="server" Width="70px" Height="25px" Text="View" CssClass="BUTTON"
                 />
            <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                Width="80%">
            </asp:Table>
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