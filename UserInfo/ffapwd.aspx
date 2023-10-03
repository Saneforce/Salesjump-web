<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ffapwd.aspx.cs" Inherits="UserInfo_ffapwd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <br />

    <br />
    <br />
    <br />
    <center>
        <table border="1" style="border-style:solid; border-collapse:collapse" width="30%">
        <tr>
        <th colspan="2" style="background-color:LightBlue">
        User Info
        </th>
        </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="lblUsername" runat="server" SkinID="lblMand" Text="Username"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtUsername" runat="server"  Width="200px" SkinID="MandTxtBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="lblPassword" runat="server" SkinID="lblMand" Text="Password"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPassword" runat="server" Width="200px" SkinID="MandTxtBox" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="lblsec" runat="server" SkinID="lblMand" Text="Security Code"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtsecurty" runat="server" Width="200px" SkinID="MandTxtBox" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
            <td></td>
                <td >
                    <asp:Button ID="btnlogin" runat="server" Text="Verify" Font-Bold="true" BackColor="LightPink" OnClick="btnlogin_Click" Width="80px" Height="25px" />
                 
                </td>
            </tr>
        </table>
        </center>
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label></div>
    </form>
</bo