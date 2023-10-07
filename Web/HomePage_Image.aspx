<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HomePage_Image.aspx.cs" Inherits="HomePage_Image" %>

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
    <link type="text/css" rel="stylesheet" href="../css/Grid.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
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
                                <div align="center">
                                   <asp:Label ID="lblsub" runat="server" Text='<%# Eval("subject") %>'  Font-Bold="true" Font-Names="Verdana" Font-Size="Medium"></asp:Label>
</div>
         <br />
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
