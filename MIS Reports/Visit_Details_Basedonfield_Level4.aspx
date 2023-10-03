<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Visit_Details_Basedonfield_Level4.aspx.cs" Inherits="MIS_Reports_Visit_Details_Field4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Visit Details Field Report</title>
     <link type="text/css" rel="stylesheet" href="../css/repstyle.css" />
     <link type="text/css" rel="Stylesheet" href="../css/rptMissCall.css" />

</head>

<script language="Javascript">
    function RefreshParent() {
        window.opener.document.getElementById('form1').click();
        window.close();
    }
    </script>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <center>
    
           
            <br />
            <table width="100%">
                <tr>
                    <td width="80%">
                    </td>
                    <td align="right">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnPrint" runat="server" Text="Print" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        onclick="btnPrint_Click"
                                         />
                                </td>
                                <td>
                                    <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        onclick="btnExcel_Click"
                                         />
                                </td>
                                <td>
                                    <asp:Button ID="btnPDF" runat="server" Text="PDF" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        onclick="btnPDF_Click"
                                        />
                                </td>
                                <td>
                                    <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClientClick="RefreshParent()"
                                        />
                                </td>
                            </tr>

                          
                        </table>
                    </td>
                </tr>
            </table>
            <br />
    
    <br />
    <asp:Panel ID="pnlContents" runat="server">
    <div align="center">
                        <asp:Label ID="lblHead" runat="server" Text="Visit Customer Field for the month of " Font-Underline="True"
                            Font-Bold="True" Font-Size="8pt"></asp:Label>
                   </div>
            <br />
    <asp:Table ID="tbl" Font-Size="8pt" runat="server" BorderStyle="Solid" BorderColor="Black" style="border-collapse: collapse;  border: solid 1px Black;" BorderWidth="1" GridLines="Both" Width = "95%">
    </asp:Table>
    </asp:Panel>
     </center>    
    </div>
    </form>
</body>
</html>
