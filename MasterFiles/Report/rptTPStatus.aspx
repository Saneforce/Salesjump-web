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
            <asp:Panel ID="pnlbutton" runat="server">
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
                                        <asp:Button ID="btnPDF" runat="server" Text="PDF" Visible="false" Font-Names="Verdana" Font-Size="10px"
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
                <div align="center">
                    <asp:Label ID="lblHead" runat="server" Text="TP Status" Font-Underline="True" Font-Bold="True"
                        Font-Size="Small"></asp:Label>
                </div>
                <br />
                <br />
                <table width="100%" align="center">
                    <tbody>
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblFieldForce" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblMonth" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblYear" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="3">
                                <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" Font-Size="8pt"
                                    GridLines="Both" Width="80%" align="center">
                                </asp:Table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </asp:Panel>
        </center>
    </div>
    </form>
</body>
</html>
