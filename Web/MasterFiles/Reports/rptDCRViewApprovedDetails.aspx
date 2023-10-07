<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptDCRViewApprovedDetails.aspx.cs" Inherits="Reports_rptDCRViewApprovedDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .tblHead
        {
          font-family:Verdana;
          color :White;
          font-size:9pt;
          font-weight:bold;
          font-family:Calibri;
          background :#0097AC;
          height:25px;
          border-color:Black;
        }
        .tblRow
        {
            font-size :8pt;
        }
    </style>
     <script language="Javascript" type="text/javascript">
         function RefreshParent() {
             window.opener.document.getElementById('form1').click();
             window.close();
         }
</script> 
</head>
<body>
    <form id="form1" runat="server">
     <div>
      <center>
        <asp:Panel ID="pnlbutton" runat="server">

        <table width="100%" >
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
                                    Height="25px" Width="60px" />    
                            </td>
                            <td>
                                <asp:Button ID="btnPDF" runat="server" Text="PDF"
                                 Font-Names="Verdana" Font-Size="10px" BorderColor="Black" 
                                 BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px" 
                                     />    
                            </td>
                             <td>
                                <asp:Button ID="btnClose" runat="server" Text="Close" 
                                Font-Names="Verdana" Font-Size="10px" BorderColor="Black" 
                                BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px" 
                                      OnClientClick="RefreshParent();" />    
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        </asp:Panel>
        <asp:Panel ID="pnlContents" runat="server" Width="100%">
        <table border="0" width="90%">
            <tr>
                <td align="center"> 
                    <asp:Label ID="lblHead" runat="server" Text="Daily Call Report for " Visible="false" Font-Underline="True" Font-Size="8pt" Font-Bold="True"></asp:Label>                    
                </td>
                
            </tr>
        </table>        
        </asp:Panel>
        <br />
        </center> 
    </div>
    </form>
</body>
</html>
