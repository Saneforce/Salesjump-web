<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_Retailer_Closing_Summary.aspx.cs" Inherits="MIS_Reports_rpt_Retailer_Closing_Summary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Productwise Reason Analysis</title>
    <script src='https://cdn.rawgit.com/simonbengtsson/jsPDF/requirejs-fix-dist/dist/jspdf.debug.js'></script>
    <script src='https://unpkg.com/jspdf-autotable@2.3.2'></script>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />

    <script language="Javascript">
        function RefreshParent() {
            window.close();
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {

            $(document).on('click', ".remark", function () {
                var order_date = $(this).attr('values');
                var pcode = $(this).attr('code');
                var pname = $(this).attr('name');
                var sfcode = $(this).attr('sfcode');
                var date1 = order_date.split(' ');
                var date2 = date1[1];
                date1 = date1[0].split('/');
                var order_date1 = date1[2] + '-' + date1[1] + '-' + date1[0];

                strOpen = "rpt_Retailer_Competitor_View.aspx?sfcode=" + sfcode + "&pcode=" + pcode + "&pname=" + pname + "&odate=" + order_date1
                window.open(strOpen, '_blank', 'statusbar=1,scrollbar=1,locator=0,width=1000,height=500,menubar=1,menubar=0,resizable=1,top=0,bottom=0');
            });
        })
    </script>
    <script type="text/javascript">
        function btnExcel_Click() {
            var htmls = "";
            var uri = 'data:application/vnd.ms-excel;base64,';
            var template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>';
            var base64 = function (s) {
                return window.btoa(unescape(encodeURIComponent(s)))
            };
            var format = function (s, c) {
                return s.replace(/{(\w+)}/g, function (m, p) {
                    return c[p];
                })
            };
            
            $("tr").each(function () {
                if (this.innerText === '') {
                    this.closest('tr').remove();
                }
            });
            htmls = document.getElementById("pnlContents").innerHTML;
            var ctx = {
                worksheet: 'Retailer_Closing_Summary',
                table: htmls
            }
            var link = document.createElement("a");
            var tets = 'Rpt_Retailer_Closing_Summary' + '.xls';

            link.download = tets;
            link.href = uri + base64(format(template, ctx));
            link.click();
        }
    </script>
    <script type="text/javascript">
        function exportTabletoPdf() {

        };


    </script>

    <script src="../JsFiles/canvasjs.min.js">
    </script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="table2excel.js" type="text/javascript"></script>
    <%--<script type="text/javascript">

        $("body").on("click", "#btnExcel_Click", function () {
            $("[id*=gdclosing]").table2excel({
                filename: "Table.xls"
            });
        });
    </script>--%>

    <style type="text/css">
        .rptCellBorder {
            border: 1px solid;
            border-color: #999999;
        }

        td {
            padding: 2px 5px;
        }

        .subTot {
            Font-size: 11pt;
            font-weight: bold;
        }

        .GrndTot {
            Font-size: 13pt;
            font-weight: bold;
        }

        .remove {
            text-decoration: none;
        }

        .TopButton {
            border-color: Black;
            border-width: 1px;
            border-style: Solid;
            font-family: Verdana;
            font-size: 10px;
            height: 25px;
            width: 60px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="pnlbutton" runat="server">
                <table width="100%">
                    <tr>

                        <td width="60%" align="center">
                            <asp:Label ID="lblHead" Font-Bold="true" Style="color: #3F51B5; padding-right: 217PX;" Font-Size="Large" Font-Underline="true"
                                runat="server"></asp:Label>
                        </td>
                        <td width="40%" align="right">
                            <table>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="btnPrint" runat="Server" Style="padding: 0px 20px;" class="btn btnPrint" OnClick="btnPrint_Click" />
                                        <asp:LinkButton ID="btnExport" runat="Server" Style="padding: 0px 20px;" class="btn btnPdf" Visible="false" />
                                        <asp:LinkButton ID="btnExcel" runat="Server" Style="padding: 0px 20px;" class="btn btnExcel"
                                            OnClientClick="btnExcel_Click()" />
                                        <a name="btnClose" id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();" class="btn btnClose"></a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <asp:Panel ID="pnlContents" EnableViewState="false" runat="server" Width="100%" Style="margin-left: 35px">
            <table border="0" id='tab1' style="margin: auto" width="100%">
                <tr>
                    <td>&nbsp;&nbsp;</td>
                </tr>
                <tr align="left">
                    <td align="left" style="font-size: small; font-weight: bold; font-family: Andalus; padding-left: 180px;">
                        <asp:Label ID="Feild" runat="server" Text="" Font-Bold="true" Style="font-family: Andalus; color: Blue;"></asp:Label></td>
                </tr>




                <tr>
                    <td width="100%">
                        <asp:GridView ID="gdclosing" runat="server" ShowHeader="false"
                            HorizontalAlign="Center" Width="100%" Font-Names="andalus" OnRowCreated="OnRowCreated" OnDataBound="OnDataBound"
                            BorderWidth="1px" CellPadding="2" EmptyDataText="No Data found for View"
                            AutoGenerateColumns="true" class="newStly"
                            HeaderStyle-HorizontalAlign="Center" BorderColor="Black" BorderStyle="Solid">
                            <Columns>

                                <%--            <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                             <asp:Label ID="lblSNo" runat="server" Font-Size="9pt" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Retailer" ItemStyle-Width="70" HeaderStyle-BorderWidth="1" ItemStyle-Font-Bold="true"  
                                        HeaderStyle-Font-Size="12pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="stockist" style="white-space:nowrap" runat="server" Font-Size="10pt" Text='<%#Eval("Listeddr_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Product" ItemStyle-Width="70" HeaderStyle-BorderWidth="1" ItemStyle-Font-Bold="true"  
                                        HeaderStyle-Font-Size="12pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="prtname" style="white-space:nowrap" runat="server" Font-Size="10pt" Text='<%#Eval("Product_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Closing Stock" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="12pt" ItemStyle-BorderWidth="1"  ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="SF_name" runat="server" Font-Size="10pt" Text='<%#Eval("ClStock")%>'></asp:Label>
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


    </form>
</body>
</html>
