﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Unlisteddr_Terr_View.aspx.cs" Inherits="MasterFiles_MR_UnListedDoctor_Unlisteddr_Terr_View" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UnListed Customer View</title>
     <style type="text/css">
             table {
    border-collapse: collapse;
}
table, td, th {
    border: 1px solid black;
}
</style>
</head>
<body>
    <form id="form1" runat="server">
      <div id="Divid" runat="server">
    </div>
      <br />
      <center>
    <asp:Panel ID="pnlSf" runat="server">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblSF" runat="server" Text="Field Force Name " SkinID="lblMand"></asp:Label>
                </td>
                <td>&nbsp;
                    <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired" >
                    </asp:DropDownList>
                </td>
                <td>&nbsp;
                    <asp:Button ID="Button1" runat="server" Text="Go" OnClick="btnGo_Click" CssClass="BUTTON" />
                </td>
            </tr>
        </table>
        
    </asp:Panel>
    </center>
    <br />
    <asp:Panel ID="pnltype" runat="server">
        <table style="width:100%">
            <tr>
            <td width="55%"></td>
                <td align="right" width="10%">
                    <asp:Label ID="lblType" runat="server" Text="Type" SkinID="lblMand"></asp:Label>
                </td>
                <td align="right" width="8%">&nbsp;
                    <asp:DropDownList ID="rdoType" runat="server" SkinID="ddlRequired">
                      
                        <asp:ListItem Value="0" Text="Category" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Channel"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Class"></asp:ListItem>
                       
                    </asp:DropDownList>
                </td>
                <td>&nbsp;
                    <asp:Button ID="btnGo" runat="server" CssClass="BUTTON" Text=">>" 
                        onclick="btnGo_Click" />
                </td>
            </tr>
        </table>
   </asp:Panel>
       <br />
    <center>
    <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
        Width="60%">
    </asp:Table>
      <asp:Label ID="lblNoRecord" runat="server" Width="60%" ForeColor="Black" BackColor="AliceBlue" Visible="false" Height="20px" BorderColor="Black"  BorderStyle="Solid" BorderWidth="2" Font-Bold="True" >No Records Found</asp:Label>
    </center>
    </form>
</body>
</html>
