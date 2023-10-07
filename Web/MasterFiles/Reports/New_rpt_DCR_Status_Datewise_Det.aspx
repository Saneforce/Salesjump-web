<%@ Page Language="C#" AutoEventWireup="true" CodeFile="New_rpt_DCR_Status_Datewise_Det.aspx.cs" Inherits="MasterFiles_Reports_New_rpt_DCR_Status_Datewise_Det" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <title>E-Report</title>
     <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
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
                                    BorderWidth="1" Height="25px" Width="60px" onclick="btnPrint_Click" />    
                            </td>
                            <td>
                                <asp:Button ID="btnExcel" runat="server" Text="Excel" 
                                    Font-Names="Verdana" Font-Size="10px" 
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" 
                                    Height="25px" Width="60px" onclick="btnExcel_Click"     />    
                            </td>
                            <td>
                                <asp:Button ID="btnPDF" runat="server" Text="PDF"
                                 Font-Names="Verdana" Font-Size="10px" BorderColor="Black" 
                                 BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px" 
                                    onclick="btnPDF_Click" />    
                            </td>
                             <td>
                                <asp:Button ID="btnClose" runat="server" Text="Close" 
                                Font-Names="Verdana" Font-Size="10px" BorderColor="Black" 
                                BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px" OnClientClick="RefreshParent();"
                                      />    
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
         <asp:Panel ID="pnlContents" runat="server" Width="100%">
        <table border="0" width="90%">
            <tr>
                <td align="center"> 
                    <asp:Label ID="lblHead" runat="server" Text="Daily Call Status between " Font-Underline="True" Font-Size="Small" Font-Bold="True"></asp:Label>                    
                </td>
            </tr>
        </table>
        <br />
        
        <center>

        <br />
        <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both" Width="95%">
        </asp:Table>
        <br />
        <asp:Table ID="tblworktype" runat="server" Width="95%">
            </asp:Table>
        </center>
        </asp:Panel> 
    </div>
    </form>
</body>
</html>
