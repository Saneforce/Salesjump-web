<%@ Page Language="C#" AutoEventWireup="true" CodeFile="User_Count.aspx.cs" Inherits="User_Count" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Count</title>
    <style type="text/css">
      td.stylespc
        {
            padding-bottom:5px;
            padding-right :5px;
        }
    
    </style>
</head>
<body style="background-color:LightBlue">
    <form id="form1" runat="server">
    <div>
    <br />
    <center>
    <h3 style="text-decoration:underline">User Count</h3>
    </center>
    <br />
     <center>
    <asp:Panel ID="pnlSf" runat="server">
        <table>
                <tr style="height:25px;">
                <td align="left" class="stylespc">
                    <asp:Label ID="lblYear" runat="server" Text="Year" SkinID="lblMand"></asp:Label>                    
                 </td>
                 <td align="left" class="stylespc"> 
                    <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired">
                    </asp:DropDownList>
                </td>
            </tr>
                    <tr>
                        <td align="left" class="stylespc">
                            <asp:Label ID="lblMonth" runat="server" Text="Month" SkinID="lblMand"></asp:Label>
                        </td>
                        <td align="left" class="stylespc">
                            <asp:DropDownList ID="ddlMonth" runat="server" SkinID="ddlRequired">
                                <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                                <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                                <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                                <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                                <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                                <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                                <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                                <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                                <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                </tr>
        </table>
        
    </asp:Panel>
    </center>
     <center>
                <asp:Button ID="btnGo" runat="server" CssClass="BUTTON" Width="35px" Height="25px" Text="Go" OnClick="btnGo_Click" />
            </center>
            <br />
              <center>
    <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
        Width="60%">
    </asp:Table>
     <asp:Label ID="lblNoRecord" runat="server" Width="60%" ForeColor="Black" BackColor="AliceBlue" Visible="false" Height="20px" BorderColor="Black"  BorderStyle="Solid" BorderWidth="2" Font-Bold="True" >No Records Found</asp:Label>
    </center>
     <br />  
    </div>
    </form>
</body>
</html>
