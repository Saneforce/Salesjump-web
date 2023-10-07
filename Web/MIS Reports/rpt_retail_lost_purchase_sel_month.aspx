<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeFile="rpt_retail_lost_purchase_sel_month.aspx.cs"
    Inherits="MIS_Reports_rpt_retail_lost_purchase_sel_month" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Retail Last Product Details</title>
    <script src="http://canvasjs.com/assets/script/canvasjs.min.js"></script>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script language="Javascript">
        function RefreshParent() {
            // window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <style type="text/css">
        .rptCellBorder
        {
            border: 1px solid;
            border-color: #999999;
        }
        
        .remove
        {
            text-decoration: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="pnlbutton" runat="server">
            <br />
            <table width="100%">
                <tr>
                    <td width="60%" align="center">
                        <asp:Label ID="lblHead" Text="Retail Last  Details" SkinID="lblMand" Font-Bold="true"
                            Font-Underline="true" runat="server"></asp:Label>
                    </td>
                    <td width="40%" align="right">
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="btnPrint" runat="Server" Style="padding: 0px 20px;" class="btn btnPrint"
                                        OnClick="btnPrint_Click" />
                                    <asp:LinkButton ID="btnExport" runat="Server" Style="padding: 0px 20px;" class="btn btnPdf"
                                        OnClick="btnExport_Click" />
                                    <asp:LinkButton ID="btnExcel" runat="Server" Style="padding: 0px 20px;" class="btn btnExcel"
                                        OnClick="btnExcel_Click" />
                                    <asp:LinkButton ID="btnClose" ID="btnClose" OnClientClick="RefreshParent().this;" Style="padding: 0px 20px;"
                                        class="btn btnClose" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <asp:Panel ID="pnlContents" runat="server" Width="100%">
        <div>
            <div align="center">
            </div>
            <center>
                <table align="center">
                    <tr>
                        <td width="400px">
                            <asp:Label ID="lblIDMonth" Text="Fieldforce:" runat="server" Font-Bold="True" Font-Names="Verdana"
                                SkinID="lblMand"></asp:Label>
                            <asp:Label ID="lblforce" runat="server" SkinID="lblMand"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:Label ID="lblIDYear" Text="Distributor:" runat="server" Font-Bold="True" Font-Names="Verdana"
                                SkinID="lblMand"></asp:Label>
                            <asp:Label ID="lbldist" runat="server" SkinID="lblMand"></asp:Label>
                        </td>
                    </tr>
                </table>
            </center>
            <br />
            <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td align="center">
                            <asp:Table ID="tbl" runat="server" class="newStly" GridLines="Both" Width="98%">
                            </asp:Table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Label runat="server" ID="lblResultMsg" ForeColor="Red" Visible="False" Width="750px"
                                Height="25px" BorderColor="#66CCFF" BorderWidth="1px" Font-Size="Medium" BackColor="#d6e9c6" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </asp:Panel>
    </form>
</body>
</html>
