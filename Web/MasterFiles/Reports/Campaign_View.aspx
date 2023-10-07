<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Campaign_View.aspx.cs" Inherits="Reports_Campaign_View" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Campaign DRs Mapping</title>
    <link type="text/css" rel="stylesheet" href="../../../css/style.css" />
    <style type="text/css">
        table
        {
            border-collapse: collapse;
        }
        table, td, th
        {
            border: 1px solid black;
        }
    </style>
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="Divid" runat="server">
        </div>
        <center>
            <br />
            <table>
                <tr>
                    <td align="left" class="stylespc" width="120px">
                        <asp:Label ID="lblFF" runat="server" SkinID="lblMand" Text="FieldForce Name"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged"
                            SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                            OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </center>
        <br />
        <center>
            <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="BUTTON" OnClick="btnGo_Click" />
        </center>
        <br />
        <center>
            <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                Width="60%">
            </asp:Table>
            <asp:Label ID="lblNoRecord" runat="server" Width="60%" ForeColor="Black" BackColor="AliceBlue"
                Visible="false" Height="20px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2"
                Font-Bold="True">No Records Found</asp:Label>

<%--            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../../Images/loader.gif" alt="" />
            </div>--%>
        </center>
    </div>
    </form>
</body>
</html>
