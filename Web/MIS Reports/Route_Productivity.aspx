<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Route_Productivity.aspx.cs"
    Inherits="MIS_Reports_Route_Productivity" %>

<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Route_Productivity</title>
    <script src="../js/canvasjs.min.js" type="text/javascript"></script>
    <script type="text/javascript">




        var popUpObj;
        function openPopupWindow(sfCode, FMonth, TMonth, FYear, TYear, sfname, stCrtDtaPnts, stCrtDtaPntss) {

            //popUpObj = window.open("VisitDetailsReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("Listviewof_retailers.aspx?sfcode=" + sfCode + "&FMonth=" + FMonth + "&FYear=" + FYear + "&TMonth=" + TMonth + "&TYear=" + TYear + "&sfname=" + sfname + "&Route=" + stCrtDtaPnts + "&Routename=" + stCrtDtaPntss,
                "ModalPopUp",
                "toolbar=no," +
                "scrollbars=yes," +
                "location=no," +
                "statusbar=no," +
                "menubar=no," +
                "addressbar=no," +
                "resizable=yes," +
                "width=900," +
                "height=600," +
                "left = 0," +
                "top=0"
            );
            popUpObj.focus();
            //LoadModalDiv();
        }

        function genChart(Obj, arrDta, title) {
            var chart = new CanvasJS.Chart(Obj, {

                theme: "theme2", //theme1
                title: {
                    text: title
                },
                exportFileName: "Pie Chart",
                exportEnabled: true,
                animationEnabled: true,

                data: [{
                    type: "pie",
                    showInLegend: false,
                    // Change type to "bar", "area", "spline", "pie",etc.
                    dataPoints: arrDta
                }]
            });
            chart.render();



        }
    </script>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script language="Javascript">
        function RefreshParent() {
            //window.opener.document.getElementById('form1').click();
            window.close();
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

        .hiddenColumn {
            display: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="pnlbutton" runat="server">
                <table width="100%">
                    <tr>
                        <%--  <td width="60%" align="center">
                        <asp:Label ID="lblHead" Text="Route_Productivity" SkinID="lblMand" Font-Bold="true"
                            Font-Underline="true" runat="server"></asp:Label>
                    </td>--%>
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
                                        <a name="btnClose" id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();"
                                            class="btn btnClose"></a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <asp:Panel ID="pnlContents" runat="server" Width="100%">
            <div style="text-align: center;">
                <div align="center">
                </div>
                <div>
                    <table width="100%">
                        <tr align="center">
                            <td style="font-size: x-large; font-weight: bold; font-family: Andalus;">
                                <asp:Label ID="lblHead" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr align="right">
                            <td align="left" style="font-size: LARGE; font-weight: bold; font-family: Andalus;">FieldForce Name:<asp:Label ID="Feild" runat="server" Text="" ForeColor="Red"></asp:Label>
                                <asp:Label ID="Label2" Font-Bold="true" Style="float: right" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <%-- <tr>
                        <td width="2.5%">
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td align="left">
                            <asp:Label ID="lblIDMonth" Text="Month :" runat="server" SkinID="lblMand"></asp:Label>
                            <asp:Label ID="lblMonth" runat="server" SkinID="lblMand"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:Label ID="lblIDYear" Text="Year :" runat="server" SkinID="lblMand"></asp:Label>
                            <asp:Label ID="lblYear" runat="server" SkinID="lblMand"></asp:Label>
                        </td>
                    </tr>--%>
                    </table>
                </div>
                <br />
                <div id="chartContainer" style="text-align: center; height: 300px; width: 95%;">
                </div>
            </div>
            <br />
            <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td align="center">
                            <asp:Table ID="tbl" runat="server" class="newStly" Style="border-collapse: collapse; border: solid 1px Black;"
                                GridLines="Both" Width="95%">
                            </asp:Table>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                    </td>
                    </tr>
                </tbody>
            </table>
            <br />
        </asp:Panel>
    </form>
</body>
</html>
