<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserList.aspx.cs" Inherits="MasterFiles_UserList" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User List</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>    
        <ucl:Menu ID="menu1" runat="server" /> 
<table width="95%" cellpadding="0" cellspacing="0" align="center" frame="box">
            <tr>
                <td>
                    <table id="Table2" runat="server" width="100%">
                        <tr>
                            <td style="width: 30%">
                                <asp:Label ID="lblStatus" runat="server" CssClass="Statuslbl" ForeColor="Black" Style="font-size: 13px;
                                    text-align: center;" Font-Bold="True" Font-Names="Times New Roman"></asp:Label>
                            </td>
                            <td align="center" style="width: 45%">
                                <asp:Label ID="lblHeading" Text="User List" runat="server" CssClass="under" Style="text-transform: capitalize;
                                    font-size: 14px; text-align: center;" ForeColor="#336277" Font-Bold="True" Font-Names="Verdana">
                                </asp:Label>
                            </td>
                            <td align="right" class="style3" style="width: 55%">
                                <asp:Button ID="btnBack" runat="server" CssClass="BUTTON" Visible="false" Height="25px" Width="60px"
                                    Text="Back"  />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
            <br />
        <br />

        <table id="Table1" height="50%" align="center" cellSpacing="0" cellPadding="8">
            <tr height="100%">
                <td valign="top">
                    <asp:TreeView ID="trvuser" runat="server" >
                    </asp:TreeView>    
                </td>
            </tr>
        </table> 
    </div>
    </form>
</body>
</html>
