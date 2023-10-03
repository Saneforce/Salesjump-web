<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_priorder_detailwise.aspx.cs" Inherits="MIS_Reports_rpt_priorder_detailwise" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Dcr Product Details</title>


    <script src="http://canvasjs.com/assets/script/canvasjs.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/0.4.1/html2canvas.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.0.272/jspdf.debug.js"></script>

    <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
    <script language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();

        }
        </script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.22/pdfmake.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/0.4.1/html2canvas.min.js"></script>
    <script type="text/javascript">
        function genrete() {
            html2canvas($('#pnlContents')[0], {
                onrendered: function (canvas) {
                    var data = canvas.toDataURL();
                    var docDefinition = {
                        content: [{
                            image: data,
                            width: 500
                        }]
                    };
                    pdfMake.createPdf(docDefinition).download("cutomer-details.pdf");
                }
            });

        }
        function pdf() {
		$("#butons").hide();
            let doc = new jsPDF('p', 'pt', 'a4');
            doc.addHTML(document.body, function () {
                doc.save('Primary order details.pdf');
            });
        }
        function webprint() {
		$("#btnPrint,#btnExcel,#btnExport,#btnClose").hide();
            var originalContents = $("#pnlContents").html();
            var printContents = $("#pnlContents").html();
            $("#pnlContents").html(printContents);
            window.print();
            $("#pnlContents").html(originalContents);
			$("#btnPrint,#btnExcel,#btnExport,#btnClose").show();
            return false;
        }

    </script>


    <style type="text/css">
        .rptCellBorder {
            border: 1px solid;
            border-color: #999999;
        }

        .remove {
            text-decoration: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="pnlbutton" runat="server">
                <table width="100%">
                    <tr>
                        <td width="30%"></td>
                        <td width="70%" align="Center">
                            <br></br>

                        </td>
                        <td align="right">
			<div id="butons">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnPrint" runat="server" Text="Print" Font-Names="Verdana" Font-Size="10px"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                            OnClientClick=" webprint()" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                            OnClick="btnExcel_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnExport" runat="server" Text="PDF" Font-Names="Verdana" Font-Size="10px" BorderWidth="1" Height="25px" Width="60px"
                                            BorderColor="Black" BorderStyle="Solid" OnClientClick="pdf()" /></td>




                                    <td>
                                        <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                            OnClientClick="RefreshParent();" OnClick="btnClose_Click" />
                                    </td>
                                </tr>

                            </table></div>
                        </td>
                    </tr>
                </table>
                <table style="visibility: hidden">
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td align="right">
                            <asp:Label ID="lblHead" SkinID="lblMand" Font-Bold="true" Width="290px" Font-Underline="true"
                                runat="server"></asp:Label></td>
                    </tr>
                </table>
            </asp:Panel>
        </div>

        <asp:Panel ID="pnlContents" runat="server" Width="100%">
            <div>
                <div align="center">
                    <asp:Label ID="lblHead1" SkinID="lblMand" Font-Bold="true" Width="290px" Font-Underline="true" Visible="true"
                        runat="server"></asp:Label>
                </div>
                <div>
                    <table width="100%" align="center">
                        <tr>
                            <br></br>
                            <td width="2.5%"></td>
                            <td align="left">&nbsp;</td>

                            <td align="Center">
                                <asp:Label ID="lblIDMonth" Text="Field Force:" runat="server" SkinID="lblMand"
                                    Font-Bold="True" Font-Names="Andalus"></asp:Label>
                                <asp:Label ID="lblMonth" runat="server" SkinID="lblMand"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblIDYear" Text="Stockist Name:" runat="server" Font-Bold="True" Font-Names="Andalus" SkinID="lblMand"></asp:Label>
                                <asp:Label ID="lblYear" runat="server" SkinID="lblMand"></asp:Label>
                            </td>

                        </tr>
                    </table>
                </div>
                <br />
                <%--<div id="chartContainer" style="height: 300px; width: 100%;"></div>--%>
                <table width="80%" align="center">
                    <tbody>
                        <tr>
                            <td align="center">
                                <asp:Table ID="tbl" runat="server" Style="border-collapse: collapse; border: solid 1px Black; font-family: Calibri"
                                    Font-Size="8pt" GridLines="Both" Width="78%">
                                </asp:Table>
                            </td>

                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </asp:Panel>
    </form>
</body>
</html>
