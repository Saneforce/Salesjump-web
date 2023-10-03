<%@ Page Language="C#" Title="SalesManDailyCallReport" AutoEventWireup="true" CodeFile="rptsalesmandailycallreportfl.aspx.cs"
    Inherits="MIS_Reports_rptsalesmandailycallreportfl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script src='https://cdn.rawgit.com/simonbengtsson/jsPDF/requirejs-fix-dist/dist/jspdf.debug.js'></script>
    <script src='https://unpkg.com/jspdf-autotable@2.3.2'></script>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script language="Javascript">
        function RefreshParent() {
            // window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <script type="text/javascript">
        var array1 = new Array();
        var array2 = new Array();
        var n = 2; //Total table
        for (var x = 1; x <= n; x++) {
            array1[x - 1] = x;
            array2[x - 1] = x + 'th';
        }

        var tablesToExcel = (function () {
            var uri = 'data:application/vnd.ms-excel;base64,'
        , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets>'
        , templateend = '</x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head>'
        , body = '<body>'
        , tablevar = '<table>{table'
        , tablevarend = '}</table>'
        , bodyend = '</body></html>'
        , worksheet = '<x:ExcelWorksheet><x:Name>'
        , worksheetend = '</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet>'
        , worksheetvar = '{worksheet'
        , worksheetvarend = '}'
        , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
        , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
        , wstemplate = ''
        , tabletemplate = '';

            return function (table, name, filename) {
                var tables = table;

                for (var i = 0; i < tables.length; ++i) {
                    wstemplate += worksheet + worksheetvar + i + worksheetvarend + worksheetend;
                    tabletemplate += tablevar + i + tablevarend;
                }

                var allTemplate = template + wstemplate + templateend;
                var allWorksheet = body + tabletemplate + bodyend;
                var allOfIt = allTemplate + allWorksheet;

                var ctx = {};
                for (var j = 0; j < tables.length; ++j) {
                    ctx['worksheet' + j] = name[j];
                }

                for (var k = 0; k < tables.length; ++k) {
                    var exceltable;
                    if (!tables[k].nodeType) exceltable = document.getElementById(tables[k]);
                    ctx['table' + k] = exceltable.innerHTML;
                }

                //document.getElementById("dlink").href = uri + base64(format(template, ctx));
                //document.getElementById("dlink").download = filename;
                //document.getElementById("dlink").click();

                window.location.href = uri + base64(format(allOfIt, ctx));

            }
        })();

    </script>
    <script type="text/javascript">
        function exportTabletoPdf() {

        };

      
    </script>
    <script src="../JsFiles/canvasjs.min.js"></script>
    <style type="text/css">
        .rptCellBorder
        {
            border: 1px solid;
            border-color: #999999;
        }
        td
        {
            padding: 2px 5px;
        }
        .subTot
        {
            font-size: 11pt;
            font-weight: bold;
        }
        .GrndTot
        {
            font-size: 13pt;
            font-weight: bold;
        }
        .remove
        {
            text-decoration: none;
        }
        .TopButton
        {
            border-color: Black;
            border-width: 1px;
            border-style: Solid;
            font-family: Verdana;
            font-size: 10px;
            height: 25px;
            width: 60px;
        }
        *
        {
            margin-left: auto;
            margin-right: auto;
        }
    </style>
    <script type="text/javascript">
        function exportexcel() {
            $("#pnlContents").table2excel({

                filename: "SalesManDailycall Reports",
                fileext: ".xls"
            });
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="pnlbutton" runat="server">
            <table width="100%">
                <tr>
                    <td width="60%" align="center">
                        <asp:Label ID="lblHead" Text="SalesMan DailyCall Report" Font-Bold="true" Style="color: #3F51B5;
                            padding-right: 117PX;" Font-Size="Large" Font-Underline="true" runat="server"></asp:Label>
                    </td>
                    <td width="40%" align="right">
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="btnPrint" runat="Server" Style="padding: 0px 20px;" class="btn btnPrint"
                                        OnClick="btnPrint_Click" />
                                    <asp:LinkButton ID="btnExport" runat="Server" Style="padding: 0px 20px;" class="btn btnPdf"
                                        Visible="false" />
                                    <asp:LinkButton ID="btnExcel" runat="Server" Style="padding: 0px 20px;" class="btn btnExcel"
                                        OnClick="ExportToExcel" />
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
    <center>
        <table border="0" id='1' width="100%">
            <tr>
                <td>
                    &nbsp;&nbsp;
                </td>
            </tr>
            <tr align="left">
                <td align="Center" style="font-size: Medium; font-weight: bold; font-family: Andalus;
                    padding-left: 10px;">
                    FieldForce Name
                    <asp:Label ID="Feild" runat="server" Text="" Font-Bold="true" Style="font-family: Andalus;
                        color: Blue; font-size: medium;"></asp:Label>
                </td>
            </tr>
            <tr >
                <td width="100%">
                    <asp:GridView ID="gvclosingstockanalysis" runat="server" HorizontalAlign="Center"
                        Width="90%" BorderWidth="1px" CellPadding="2" CellSpacing="5" EmptyDataText="No Data found for View"
                        AutoGenerateColumns="true" class="newStly">
                        <Columns>
                        </Columns>
                        <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                            BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                            VerticalAlign="Middle" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </center>
    </div>
    <div id="chartContainer" style="text-align: center; height: 180px; width: 95%;">
    </div>
    <script type="text/javascript">

        function generate() {

            var doc = new jsPDF('l', 'pt');

            var res = doc.autoTableHtmlToJson(document.getElementById("gvclosingstockanalysis"));




            var header = function (data) {
                doc.setFontSize(10);
                doc.setTextColor(40);
                doc.setFontStyle('normal');
                //doc.addImage(headerImgData, 'JPEG', data.settings.margin.left, 17, 50, 50);
                doc.text("SalesMan Daily", data.settings.margin.top, 70);
            };
            var options = {
                beforePageContent: header,
                margin: {
                    top: 80
                },
                startY: doc.autoTableEndPosY() + 20
            };
            doc.autoTable(res.columns, res.data, options);
            var header1 = function (data) {
                doc.setFontSize(13);
                doc.setTextColor(40);
                doc.setFontStyle('normal');
                //doc.addImage(headerImgData, 'JPEG', data.settings.margin.left, 17, 50, 50);
                doc.text("Categorywise", data.settings.margin.left, 40);
            };
            var options1 = {
                beforePageContent: header1,
                margin: {
                    top: 80
                },
                startY: doc.autoTableEndPosY() + 20
            };



            doc.autoTable(res1.columns, res1.data, options1);

            doc.save("Daywisesummary.pdf");
        }</script>
    </form>
</body>
</html>
