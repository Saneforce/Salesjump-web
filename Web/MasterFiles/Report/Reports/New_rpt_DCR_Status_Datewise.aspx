<%@ Page Language="C#" AutoEventWireup="true" CodeFile="New_rpt_DCR_Status_Datewise.aspx.cs" Inherits="MasterFiles_Reports_New_rpt_DCR_Status_Datewise" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>DCR Status Report</title>
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
    <asp:Panel ID="pnlbutton" runat="server">
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
                                    OnClick="btnPrint_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                    OnClick="btnExcel_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnPDF" runat="server" Text="PDF" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                    OnClick="btnPDF_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
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
                    <asp:Label ID="lblHead" runat="server" Text="Daily Call Status between "
                        Font-Underline="True" Font-Size="Small" Font-Bold="True"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <center>
            <br />
            <asp:Table ID="tbl" runat="server" Style="border-collapse: collapse;
                border: solid 1px Black;" BorderStyle="Solid" BorderWidth="1"
                GridLines="Both" Width="95%">
            </asp:Table>
            <br />
            <asp:Table ID="tblworktype" runat="server" Width="95%">
            </asp:Table>
        </center>
    </asp:Panel>
    </form>
</body>
</html>
