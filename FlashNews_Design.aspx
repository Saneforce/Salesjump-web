<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FlashNews_Design.aspx.cs"
    Inherits="FlashNews_Design" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .roundCorner
        {
            border-radius: 25px;
            background-color: #4F81BD;
            text-align: center;
            font-family: arial, helvetica, sans-serif;
            padding: 5px 10px 10px 10px;
            font-weight: bold;
            width: 150px;
            height: 30px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     
        <table width="100%" border="0" cellpadding="0" cellspacing="4" align="center">
            <tr>
                <td>
                    <asp:Label ID="LblUser" runat="server" Text="User" Style="text-transform: capitalize;
                        font-size: 14px;" ForeColor="DarkGreen" Font-Bold="True" Font-Names="Verdana"> </asp:Label>
                </td>
                <td align="right">
                    <asp:Label ID="lbldiv" runat="server" Text="User" Style="text-transform: capitalize;
                        font-size: 14px;" ForeColor="DarkGreen" Font-Bold="True" Font-Names="Verdana"> </asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr />
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                 <asp:Button ID="btnNext" runat="server" Width="150px" Height="30px" Text="Next to Home Page"
                        OnClick="btnHome_Click" BackColor="LightPink" ForeColor="Black" CssClass="roundCorner" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnHome" runat="server" Width="150px" Height="30px" Text="Goto Home Page" CssClass="roundCorner"  
                        OnClick="btnHomepage_Click" BackColor="Green" ForeColor="White" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnLogout" runat="server" Width="90px" Height="30px" Text="Logout" CssClass="roundCorner" 
                        OnClick="btnLogout_Click" BackColor="Red" ForeColor="White" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr />
                </td>
            </tr>
        </table>
        <br />
        <center>
            <asp:Label ID="lblHome" Font-Size="X-Large" ForeColor="Blue" Font-Bold="true" Font-Names="Verdana"
                runat="server">Flash News</asp:Label>
        </center>
        <br />
        <br />
        <center>
            <asp:Panel ID="Panel2" runat="server" ForeColor="Red" Font-Names="Verdana" Font-Size="Medium"
                Width="80%" Height="80px" BorderStyle="Solid" BorderColor="ActiveBorder">
                <marquee onmouseover="this.setAttribute('scrollamount', 0, 0);" onmouseout="this.setAttribute('scrollamount', 6, 0);"><asp:Label ID="lblFlash" runat="Server" Text='<%# Eval("FN_Cont1") %>' /></marquee>
            </asp:Panel>
        </center>
    </div>
    </form>
</body>
</html>
