<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="rptdaywisesummary.aspx.cs"
    Inherits="MIS_Reports_rptdaywisesummary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Daywise Summary</title>
    <script src='https://cdn.rawgit.com/simonbengtsson/jsPDF/requirejs-fix-dist/dist/jspdf.debug.js'></script>
    <script src='https://unpkg.com/jspdf-autotable@2.3.2'></script>
    <link href="../css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
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
    </style>
    <script type="text/javascript">
        function exportexcel() {
            $("#pnlContents").table2excel({

                filename: "TodayOrderView",
                fileext: ".xls"
            });
        };
    </script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {



            $(document).on('click', "#Export", function (e) {
                var dt = new Date();
                var day = dt.getDate();
                var month = dt.getMonth() + 1;
                var year = dt.getFullYear();
                var postfix = day + "_" + month + "_" + year;
                //creating a temporary HTML link element (they support setting file names)
                var a = document.createElement('a');
                //getting data from our div that contains the HTML table
                var data_type = 'data:application/vnd.ms-excel';
                var table_div = document.getElementById('pnlContents');
                var table_html = table_div.outerHTML.replace(/ /g, '%20');
                a.href = data_type + ', ' + table_html;
                //setting the file name
                a.download = 'DaywiseSummary_' + postfix + '.xls';
                //triggering the function
                a.click();
                //just in case, prevent default behaviour
                e.preventDefault();
            });

            $(document).on('click', "#btnPrint", function () {
                var originalContents = $("body").html();
                var printContents = $("#pnlContents").html();
                $("body").html(printContents);
                window.print();
                $("body").html(originalContents);
                return false;
            });



        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="pnlbutton" runat="server">
            <table width="100%">
                <tr>
                    <td width="20%">
                    </td>
                    <td width="80%" align="cENTER">
                        <asp:Label ID="lblHead" Text="Daywise Summary" Font-Name="Andalus" Font-Bold="true"
                            Style="padding-right: 217PX;" Font-Size="Large" Font-Underline="true" runat="server"></asp:Label>
                    </td>
                    <td align="right">
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="btnPrint" runat="server" class="btn btnPrint" OnClick="btnPrint_Click" />
                                </td>
                                <td>
                               <asp:LinkButton ID="LinkButton1" runat="server" class="btn btnExcel" OnClick="btnExcel_Click" />
                                    
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnClose" runat="server" type="button" Style="padding: 0px 20px;"
                                        href="javascript:window.open('','_self').close();" class="btn btnClose" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <asp:Panel ID="pnlContents" runat="server" Width="100%" align="center">
        <table border="0" id='1' width="90%" align="center">
            <tr align="right">
                <td align="left" style="font-size: small; font-weight: bold; font-family: Andalus;">
                    FieldForce Name:
                    <asp:Label ID="Feild" runat="server" Text="" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="100%">
                    <asp:GridView ID="gvdaywisesummary" runat="server" HorizontalAlign="Center" Width="80%"
                        Font-Names="andalus" BorderWidth="1px" CellPadding="2" EmptyDataText="No Data found for View"
                        AutoGenerateColumns="false" BackColor="#ffffe0" HeaderStyle-BackColor="#3F51B5"
                        HeaderStyle-ForeColor="#a3b9ef" HeaderStyle-HorizontalAlign="Center" BorderColor="Black"
                        BorderStyle="Solid" ShowFooter="true">
                        <Columns>
                            <%--<asp:TemplateField HeaderText="S.No" ItemStyle-Width="20" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                             <asp:Label ID="lblSNo" runat="server" Font-Size="9pt" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Date" ItemStyle-Width="50" HeaderStyle-BorderWidth="1"
                                ItemStyle-Font-Bold="true" ItemStyle-Font-Names="Rockwell" HeaderStyle-Font-Size="12pt"
                                ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="stockist" Style="white-space: nowrap" runat="server" Font-Size="10pt"
                                        Text='<%#Eval("DayPart")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                HeaderStyle-Font-Size="12pt" ItemStyle-BorderWidth="1" ItemStyle-Font-Bold="true"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="SF_name" runat="server" Font-Size="10pt" Text='<%#Eval("Totall")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Present" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                ItemStyle-Font-Bold="true" HeaderStyle-Font-Size="12pt" ItemStyle-BorderWidth="1"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="Route_name" runat="server" Font-Size="10pt" Text='<%#Eval("presentee")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Absent" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                ItemStyle-Font-Bold="true" HeaderStyle-Font-Size="12pt" ItemStyle-BorderWidth="1"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="Retailer_name" runat="server" Font-Size="10pt" Text='<%#Eval("Absent")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sales" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                ItemStyle-Font-Bold="true" HeaderStyle-Font-Size="12pt" ItemStyle-BorderWidth="1"
                                ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="netval" runat="server" Font-Size="10pt" Text='<%#DataBinder.Eval(Container.DataItem, "Sales", "{0:N2}")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Average" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                ItemStyle-Font-Bold="true" HeaderStyle-Font-Size="12pt" ItemStyle-BorderWidth="1"
                                ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="freept" runat="server" Font-Size="10pt" Text='<%#Eval("Average")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- <asp:TemplateField HeaderText="Total Value" ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="toval" runat="server" Font-Size="9pt" Text='<%#Eval("Totall")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                            <%-- <asp:TemplateField HeaderText="Discount Price" ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="discountpri" runat="server" Font-Size="9pt" Text='<%#Eval("discount_price")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                
                                     <asp:TemplateField HeaderText="Order Value" ItemStyle-Width="120" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="orderval" runat="server" Font-Size="9pt" Text='<%#DataBinder.Eval(Container.DataItem, "Order_value", "{0:N2}")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                        </Columns>
                        <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                            BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                            VerticalAlign="Middle" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </asp:Panel>
    </div>
    <div id="chartContainer" style="text-align: center; height: 180px; width: 95%;">
    </div>
    <script type="text/javascript">

        function generate() {

            var doc = new jsPDF('l', 'pt');

            var res = doc.autoTableHtmlToJson(document.getElementById("gvdaywisesummary"));




            var header = function (data) {
                doc.setFontSize(10);
                doc.setTextColor(40);
                doc.setFontStyle('normal');
                //doc.addImage(headerImgData, 'JPEG', data.settings.margin.left, 17, 50, 50);
                doc.text("DAYWISE SUMMARY", data.settings.margin.top, 70);
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
