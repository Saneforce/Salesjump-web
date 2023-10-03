<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_Productwise_SuperStockist_Order.aspx.cs" Inherits="MIS_Reports_rpt_Productwise_SuperStockist_Order" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Productwise SuperStockist Order</title>
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
            });
        })
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
            htmls = document.getElementById("form1").innerHTML;

            var ctx = {
                worksheet: 'Productwise_SuperStockist_Order',
                table: htmls
            }
            var link = document.createElement("a");
            var tets = 'Rpt_Productwise_SuperStockist_Order' + '.xls';   //create fname

            link.download = tets;
            link.href = uri + base64(format(template, ctx));
            link.click();
        }
    </script>
    <script src="../JsFiles/canvasjs.min.js"></script>

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
					<a name="btnClose" id="btnClose" type="button" style="padding: 0px 20px;float:right" href="javascript:window.open('','_self').close();" class="btn btnClose"></a>
                             <asp:LinkButton ID="LinkButton1" runat="Server" Style="padding: 0px 20px;float:right" class="btn btnExcel"
                                        OnClientClick="btnExcel_Click()" /> 
                        </td>
                        </tr>
                    <tr>
                        <div class="row" style="margin: 25px 0px 0px 11px;">
                            <asp:Label ID="Label2" Text="Field Force Name :" runat="server" Style="font-size: larger"></asp:Label>
                            <asp:Label ID="lblsf_name" runat="server" Style="font-size: larger"></asp:Label>
                        </div>                       
                        
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <asp:Panel ID="pnlContents" EnableViewState="false" runat="server" Width="100%" Style="margin-left: 35px">
            <table border="0" id='1' style="margin: auto" width="100%">
                <tr>
                    <td>&nbsp;&nbsp;</td>
                </tr>
                <tr align="left">
                    <td align="left" style="font-size: small; font-weight: bold; font-family: Andalus; padding-left: 180px;">
                        <asp:Label ID="Feild" runat="server" Text="" Font-Bold="true" Style="font-family: Andalus; color: Blue;"></asp:Label></td>
                </tr>
                <tr>
                    <td width="100%">
                        <asp:GridView ID="gdorder" runat="server" ShowHeader="false"
                            HorizontalAlign="Center" Width="100%" Font-Names="andalus" OnRowCreated="OnRowCreated" OnDataBound="OnDataBound"
                            BorderWidth="1px" CellPadding="2" EmptyDataText="No Data found for View"
                            AutoGenerateColumns="true" class="newStly"
                            HeaderStyle-HorizontalAlign="Center" BorderColor="Black" BorderStyle="Solid">
                            <Columns>
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
