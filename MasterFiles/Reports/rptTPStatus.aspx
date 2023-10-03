<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptTPStatus.aspx.cs" Inherits="Reports_rptTPStatus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TP Status Report</title>
    
    <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />

     <script language="Javascript">
         function RefreshParent() {
             window.opener.document.getElementById('form1').click();
             window.close();
         }
</script> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <center>
        <table>
            <tr>
                <td align="center">
                    <asp:Label ID="lblHead" runat="server" Text="Listed Customer Details" Font-Underline="True" Font-Bold="True" Font-Size="Small" ></asp:Label>
                </td>
            </tr>
        </table>
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
                                <asp:Button ID="btnExcel" runat="server" Text="Excel" 
                                    Font-Names="Verdana" Font-Size="10px" 
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" 
                                    Height="25px" Width="60px"     />    
                            </td>
                            <td>
                                <asp:Button ID="btnPDF" runat="server" Text="PDF"
                                 Font-Names="Verdana" Font-Size="10px" BorderColor="Black" 
                                 BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px" />    
                            </td>
                             <td>
                                <asp:Button ID="btnClose" runat="server" Text="Close" 
                                Font-Names="Verdana" Font-Size="10px" BorderColor="Black" 
                                BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px" OnClientClick="RefreshParent();" />    
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />

       <table width="100%" align="center">
        <tbody>               
            <tr>
                <td align="center">
                    <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both" Width="80%" align="center">
                    </asp:Table>                
                </td>
            </tr>
        </tbody>
    </table>
    
    </div>
    </form>
</body>
</html>
