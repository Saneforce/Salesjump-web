<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Birthday_Wish.aspx.cs" Inherits="Birthday_Wish" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <table width="100%" border="0" cellpadding="0" cellspacing="4" align="center">
                    <tr>
                        <td width="1px"></td>
                        <td align="center">
                            <asp:Label ID="LblUser" runat="server" Text="User" Style="text-transform: capitalize;font-size: 22px;" ForeColor="DarkGreen" 
                                Font-Bold="True" Font-Names="Verdana"> </asp:Label>
                        </td>
                        <td align="left"></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td align="right">
                            <asp:Button ID="btnHome" runat="server" Width="130px" Height="30px" Text="Goto Home Page" OnClick="btnHome_Click" 
                                BackColor="Green" ForeColor="White"  />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnLogout" runat="server" Width="90px" Height="30px" Text="Logout" OnClick="btnLogout_Click"
                                BackColor="Red" ForeColor="White" />
                        </td>
                    </tr>
                </table>
                <center>
                    <table width="100%">
                        <tr>
                            <td style="width: 10%; "></td>
                            <td align="center" style="width: 70%; "><asp:Label ID="Label1" runat="server"  Text="Happy Birthday" Font-Size="Larger" 
                                ForeColor="Green" Font-Bold="true" Font-Underline="true" />
                            </td>
                            <td align="right" style="width: 10%">
                                <asp:Label ID="lbldob_sf" runat="server" Text="DOB :"></asp:Label>&nbsp;
                                <asp:Label ID="lbldob" runat="server" Font-Bold="true" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </center>
                <center>
                    <div><img src="Images/happy-birth2.gif" alt="" /></div>
                </center>
            </div>
        </form>
    </body>
</html>
