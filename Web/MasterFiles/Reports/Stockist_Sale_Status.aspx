<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Stockist_Sale_Status.aspx.cs"
    Inherits="MasterFiles_Reports_Stockist_Sale_Status" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Stockist Sale Entry Status</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
    </div>
    <br />
    <center>
        <table border="0" cellpadding="3" cellspacing="3" id="tblddlDetails"
            align="center">
            <%--<tr style="height: 30px">
                <td>
                    <asp:Label ID="lblStockistName" runat="server" SkinID="lblMand" Text="Stockist Name "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlStockist" runat="server" AutoPostBack="true" SkinID="ddlRequired">
                    </asp:DropDownList>
                </td>
            </tr>--%>
            <tr style="height: 30px">
                <td align="left">
                    <asp:Label ID="lblFF" SkinID="lblMand" runat="server" Text="FieldForce Name"></asp:Label>
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
                    <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="height: 30px">
                <td align="left">
                    <asp:Label ID="lblFrom" runat="server" SkinID="lblMand" Text="Month"></asp:Label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlMonth" runat="server" SkinID="ddlRequired">
                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        <asp:ListItem Value="1" Text="January"></asp:ListItem>
                        <asp:ListItem Value="2" Text="February"></asp:ListItem>
                        <asp:ListItem Value="3" Text="March"></asp:ListItem>
                        <asp:ListItem Value="4" Text="April"></asp:ListItem>
                        <asp:ListItem Value="5" Text="May"></asp:ListItem>
                        <asp:ListItem Value="6" Text="June"></asp:ListItem>
                        <asp:ListItem Value="7" Text="July"></asp:ListItem>
                        <asp:ListItem Value="8" Text="August"></asp:ListItem>
                        <asp:ListItem Value="9" Text="September"></asp:ListItem>
                        <asp:ListItem Value="10" Text="October"></asp:ListItem>
                        <asp:ListItem Value="11" Text="November"></asp:ListItem>
                        <asp:ListItem Value="12" Text="December"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="height: 30px">
                <td align="left">
                    <asp:Label ID="lblyear" runat="server" SkinID="lblMand" Text="Year"></asp:Label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" SkinID="ddlRequired">
                        <asp:ListItem Selected="True" Value="0" Text="--Select--"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </center>
    <br />
    <center>
        <asp:Button ID="btnGo" runat="server" CssClass="BUTTON" Width="70px" Height="25px" Text="View" 
            onclick="btnGo_Click" />
    </center>
    </form>
</body>
</html>
