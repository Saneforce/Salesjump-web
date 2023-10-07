<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptDcrViewDetails.aspx.cs" Inherits="Reports_rptDcrViewDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>DCR View Report</title>
    <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
    <style>
        .tblHead
        {
          font-family:Verdana;
          color :White;
          font-size:9pt;
          font-weight:bold;
          font-family:Calibri;
          background :#0097AC;
          height:25px;
          border-color :Black;
        }
    </style>

    <script language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
</script>  
</head>
<body >
    <form id="form1" runat="server" >
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
                    <asp:Label ID="lblHead" runat="server" Text="Daily Call Report for "  Font-Underline="True" Font-Size="Small" Font-Bold="True"></asp:Label>                    
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
