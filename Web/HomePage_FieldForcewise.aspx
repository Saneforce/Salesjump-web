<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HomePage_FieldForcewise.aspx.cs"
    Inherits="HomePage_FieldForcewise" %>

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
                <td align="center">
                    <asp:Label ID="LblUser" runat="server" Text="User" Style="text-transform: capitalize;
                        font-size: 22px;" ForeColor="DarkGreen" Font-Bold="True" Font-Names="Verdana"> </asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right">
                  <asp:Button ID="btnNext" runat="server" Width="150px" Height="30px" Text="Next to Home Page"
                        OnClick="btnHome_Click" BackColor="LightPink" ForeColor="Black" CssClass="roundCorner" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnHome" runat="server" Width="150px" Height="30px" CssClass="roundCorner"
                        Text="Direct to Home Page" OnClick="btnHomepage_Click" BackColor="Green" ForeColor="White" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnLogout" runat="server" Width="90px" Height="30px" CssClass="roundCorner"
                        Text="Logout" OnClick="btnLogout_Click" BackColor="Red" ForeColor="White" />
                </td>
            </tr>
        </table>
        <center>
            <table >
                <tr>
                    <td>
                        <asp:DataList ID="DataList1" runat="server" HorizontalAlign="Center">
                            <ItemTemplate>
                                <div>
                                    <asp:Image ID="imgHome" ImageUrl='<%# Eval("FilePath") %>' ImageAlign="Top" runat="server" />
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </td>
                </tr>
            </table>
        </center>
    </div>
    </form>
</body>
</html>
