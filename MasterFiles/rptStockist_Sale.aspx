<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptStockist_Sale.aspx.cs" Inherits="MasterFiles_rptStockist_Sale" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../css/repstyle.css" />
</head>
<body>
    <form id="form1" runat="server">
          <br />
        <table width="100%">
            <tr>
                <td width="80%">
                </td>
                <td align="right">
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnPrint" runat="server" Text="Print" 
                                    Font-Names="Verdana" Font-Size="10px" BorderColor="Black" BorderStyle="Solid" 
                                    BorderWidth="1" Height="25px" Width="60px" />    
                            </td>
                           
                             <td>
                                <asp:Button ID="btnClose" runat="server" Text="Close" 
                                Font-Names="Verdana" Font-Size="10px" BorderColor="Black" 
                                BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px" />    
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
     <table border="0" width="90%">
            <tr>
                <td align="center"> 
                    <asp:Label ID="lblHead" runat="server" Text="Stockist Sale Entry for the month of " Font-Underline="True" Font-Size="Small" Font-Bold="True"></asp:Label>                    
                </td>
            </tr>
        </table>
        <br />
        <br />
  
    </form>
</body>
</html>
