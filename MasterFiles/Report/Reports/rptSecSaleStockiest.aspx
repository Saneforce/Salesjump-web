<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptSecSaleStockiest.aspx.cs" Inherits="MasterFiles_Reports_rptSecSaleStockiest" EnableEventValidation="false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Secondary Sales Entry Status</title>
    <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
    <script language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <style type="text/css">
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
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <div>
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
                                        <asp:Button ID="btnPrint" runat="server" Visible="false" Text="Print" Font-Names="Verdana"
                                            Font-Size="10px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px"
                                            Width="60px" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                            OnClick="btnExcel_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnPDF" runat="server" Text="PDF" Visible="false" Font-Names="Verdana"
                                            Font-Size="10px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px"
                                            Width="60px" />
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
                    <asp:Label ID="lblText" runat="server" Font-Size="Medium" Font-Names="Bookman Old Style"
                        Font-Underline="true" Text="Secondary Sales Entry Stauts for "></asp:Label>
                </div>
                <br />
                <br />
                <table width="100%" align="center">
                    <asp:Table ID="tbl" runat="server" GridLines="Both" Width="90%">
                    </asp:Table>
                </table>
            </asp:Panel>
        </center>
    
    </div>
    </form>
</body>
</html>
