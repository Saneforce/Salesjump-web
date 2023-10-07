<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Vacant_MR_Login.aspx.cs" Inherits="MasterFiles_MGR_Vacant_MR_Login" %>
<%@ Register Src ="~/UserControl/MGR_Menu.ascx" TagName ="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Login To Vacant ID's</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <style type="text/css">
        table.gridtable
        {
            font-family: verdana,arial,sans-serif;
            font-size: 11px;
            color: #333333;
            border-width: 1px;
            border-color: #666666;
            border-collapse: collapse;
        }
        table.gridtable th
        {
            border-width: 1px;
            padding: 5px;
            border-style: solid;
            border-color: 1px #666666;
            background-color: #99B7B7;
        }
        table.gridtable td
        {
            border-width: 1px;
            padding: 5px;
            border-style: solid;
            border-color: #666666;
            background-color: #ffffff;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <br />
        <center>
   
            <table width="80%" border="1" class="gridtable">
                <thead>
                    <tr>
                        <th style="color: White; border-color: Black">
                            Field Force Name
                        </th>
                        <th style="color: White; border-color: Black">
                            Login
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td style="height: 400px; width: 30%;" valign="top" align="center">
                            <asp:Label ID="lblmr" runat="server" Font-Size="Small" Font-Bold="true" ForeColor="DarkGreen">Vacant ID's</asp:Label><br />
                            <asp:ListBox ID="lstVacant" Font-Size="Small" Font-Names="Verdana" Width="100%"  Height="400px" runat="server"
                                Style="border: solid 1px black; border-collapse: collapse;" AutoPostBack="true" OnSelectedIndexChanged="lstVacant_SelectedIndexChanged">
                            </asp:ListBox>
                        </td>
                        <td align="center">
                            <br />
                            <br />
                            <br />
                                 <asp:Panel ID="pnlSf" runat="server" Visible="false">
                            <table align="center" border="1" width="60%" style="border-collapse">
                                <tr>
                                    <th align="center" colspan="2" style="color: White">
                                        Login
                                    </th>
                                </tr>
                                <tr >
                                    <td valign="middle" style="width:20%">
                                        <asp:Label ID="lblPassword" runat="server" Text="Password" SkinID="lblMand"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPassword" runat="server" MaxLength="15" TextMode="Password" SkinID="TxtBoxStyle"></asp:TextBox>
<asp:Label ID="lblPassword1" runat="server" Text="(Type your own Password)" ForeColor="Red" SkinID="lblMand"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblLoginto" runat="server" Text="Login To:" SkinID="lblMand"></asp:Label>
                                    </td>
                                    <td>
                                    <asp:Label ID="lblLogin" runat="server" ></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:Button ID="btnLogin" runat="server" CssClass="BUTTON" Text="Login" OnClick="btnLogin_Click" />
                                    </td>
                                </tr>
                            </table>
                                 </asp:Panel>
                        </td>
                    </tr>
                </tbody>
            </table>
    
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
