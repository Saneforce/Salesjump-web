<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptWrkTypeViewStatus.aspx.cs"
    Inherits="Reports_rptWrkTypeViewStatus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Work Type Status View </title>
    <link type="text/css" rel="Stylesheet" href="../../css/rptMissCall.css" />
    <style>
        .tr_det_head
        {
            font-family: Verdana;
            color: White;
            font-size: 9pt;
            height: 22px;
            font-weight: bold;
            font-family: Calibri;
            background: #0097AC;
            border-color: Black; 
            border-style:solid;           
        }
    </style>
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
            <center>
                <asp:Panel ID="pnlContents" runat="server">
                    <div align="center">
                        <asp:Label ID="lblHead" runat="server" Text="Visit Customer Field for the month of "
                            Font-Underline="True" Font-Bold="True" Font-Size="8pt"></asp:Label>
                            <br />
                        <asp:Label ID="LblForceName" runat="server" Font-Underline="True" Font-Bold="True"
                            Font-Size="8pt"></asp:Label>
                    </div>
                    <br />
                    <table width="100%" align="center">
                        <tr>
                            <td>
                                <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                                    Width="95%">
                                </asp:Table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </center>
        </center>
    </div>
    </form>
</body>
</html>
