<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_orderwise_count.aspx.cs" Inherits="MIS_Reports_rpt_orderwise_count" %>

<!DOCTYPE html>
<script runat="server">

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }

    protected void Order_Count_Grid_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
</script>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Secondary Order Wise Count</title>
     <script src='https://cdn.rawgit.com/simonbengtsson/jsPDF/requirejs-fix-dist/dist/jspdf.debug.js'></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src='https://unpkg.com/jspdf-autotable@2.3.2'></script>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
<%-- <script src="table2excel.js" type="text/javascript"></script>--%>
    
       <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />

     <script>
         function RefreshParent() {
             window.close();
         }
    </script>

        <script type="text/javascript">
            $(document).ready(function () {

                $(document).on('click', '#btnExport', function (e) {
                    var data_type = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html());
                    var a = document.createElement('a');
                    a.href = data_type;
                    a.download = 'Sec_OrderWise_Count xls';
                    a.click();
                    e.preventDefault();
                });
            });
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

              /*#Order_Count_Grid {
             text-align: right;
         }*/

             #Order_Count_Grid tr:nth-child(1), #Order_Count_Grid tr:nth-child(2) {
                 text-align: left;
             }

             #Order_Count_Grid tr:nth-child(3) td:nth-child(1) {
                 min-width: 260px;
                 text-align: left;
             }

             #Order_Count_Grid tr td:nth-child(2) {
                 text-align: left;
             }

             #Order_Count_Grid tr:nth-child(3) td:nth-child(2) {
                 text-align: left;
             }

             #Order_Count_Grid tr:nth-child(1) td:nth-child(2) {
                 text-align: right;
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

    <script type="text/javascript">
        $(document).ready(function () {

            $(document).on('click', "#btnPrint", function () {
                var originalContents = $("body").html();
                var printContents = $("#content").html();
                $("body").html(printContents);
                window.print();
                $("body").html(originalContents);
                return false;
            });

            $(document).on('click', ".retpro", function () {

                var Sf_Code = $(this).attr('Sf_Code');
                var sfname = $(this).attr('sf_name');
                var Month = $(this).attr('month');
                var Month_Name = $(this).attr('Month_Name');
                //   var subdiv_code = $(this).attr('subdiv_code');
                //var Order_date = document.getElementById("Label2").innerHTML.split(':')[1].trim().split('-').reverse().join('-');

                strOpen = "View_Monthly_Order_Count_Details.aspx?Sf_Code=" + Sf_Code + "&sfname=" + sfname + "&cur_year=" +<%=FYear%> + "&Month=" + Month + "&Month_Name=" + Month_Name
                window.open(strOpen, '_blank', 'statusbar=1,scrollbar=1,locator=0,width=1000,height=500,menubar=1,menubar=0,resizable=1,top=0,bottom=0');
            })
        });


    </script>
</head>
<body>
    <form id="form1" runat="server">
   <div>
      <asp:Panel ID="pnlbutton" runat="server">
             <table width="100%">
                <tr>
                <td width="20%"></td>
                    <td width="80%" align="center" >
                    <asp:Label ID="lblHead" Text="Retailer Offtake Report"  Font-Names="Andalus" Font-Bold="true"  Font-Underline="true"              
                       runat="server" Font-Size="Large"></asp:Label>
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

<td><asp:Button ID="btnExport" runat="server" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px" Text="Excel" OnClick="btnExport_Click" /></td>
                               
                                </td>
                                <td> 
 <input id="pdfexport" class="TopButton" type="button" value="PDF" height="35px"  onclick="generate()"" >   


</td>
                               
                                <td>
                                    <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClientClick="RefreshParent();" OnClick="btnClose_Click" />
                                </td>

                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>


              <asp:Panel id="pnlContents"  runat="server" Width="100%">
                <table border="0" id='1'   width="100%">                 
                                                   
          <tr align="right"><td align="left" style="font-size: small; PADDING: 0PX 52PX; font-weight: bold;font-family: Andalus;">FieldForce Name:
                   <asp:Label ID="Feild" runat="server" Text="" ForeColor="Red"></asp:Label></td></tr>
                     <tr> 
                        <td width="100%" style="TEXT-ALIGN: -webkit-center;">
                          
                  


                           <%-- <asp:GridView ID="Order_Count_Grid" runat="server" Width="95%" ShowHeader="false"
                                HorizontalAlign="Center" OnDataBound="Order_Count_Grid_DataBound1" OnRowDataBound="Order_Count_Grid_RowDataBound1"
                                  OnRowCreated="Order_Count_Grid_RowCreated1" BorderWidth="1px" CellPadding="2" EmptyDataText="No Data Found"
                                 AutoGenerateColumns="false" CssClass="newStly" Font-Size="Small" HeaderStyle-HorizontalAlign="Center">
                                <Columns>

                                </Columns>
                                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="true" HorizontalAlign="Center" VerticalAlign="Middle" />
                                
                                </asp:GridView>--%>
                            <asp:GridView ID="Order_Count_Grid" runat="server" Width="95%"   ShowHeader="false"
                                HorizontalAlign="Center"
                                BorderWidth="1px" CellPadding="2" EmptyDataText="No Data found for View" OnRowCreated="Order_Count_Grid_RowCreated1"
                                AutoGenerateColumns="true" CssClass="newStly" Font-Size="Small"
                                HeaderStyle-HorizontalAlign="Center" >                               
                                <Columns>
                               </Columns>
                                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>             
                    </td>
                         </tr>
    </asp:Panel>

        <script type="text/javascript">

            function generate() {

                var doc = new jsPDF('l', 'pt');

                var res = doc.autoTableHtmlToJson(document.getElementById("Order_Count_Grid"));

                var header = function (data) {
                    doc.setFontSize(10);
                    doc.setTextColor(40);
                    doc.setFontStyle('normal');
                    //doc.addImage(headerImgData, 'JPEG', data.settings.margin.left, 17, 50, 50);
                    doc.text("Sec_OrderWise_Count.aspx", data.settings.margin.top, 70);
                };
                var options = {
                    beforePageContent: header,
                    margin: {
                        top: 80
                    },
                    startY: doc.autoTableEndPosY() + 20
                };
                doc.autoTable(res.columns, res.data, options);
                doc.save("Sec_OrderWise_Count.pdf");
            }</script>

    </form>
</body>
</html>
