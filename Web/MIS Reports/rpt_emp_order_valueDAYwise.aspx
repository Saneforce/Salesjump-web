<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_emp_order_valueDAYwise.aspx.cs" Inherits="MIS_Reports_rpt_emp_order_valueDAYwise" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Secondary Order View</title>
 <script src='https://cdn.rawgit.com/simonbengtsson/jsPDF/requirejs-fix-dist/dist/jspdf.debug.js'></script>
<script src='https://unpkg.com/jspdf-autotable@2.3.2'></script>

  <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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

        });
  </script>

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

                window.location.href = uri + base64(format(allOfIt, ctx));

            }
        })();



    </script>
    <script type="text/javascript">
        function exportTabletoPdf() {

        };


    </script>

    <script src="../JsFiles/canvasjs.min.js"></script>
	<script type="text/javascript">

        function genChart(arrDta) {
            var chart = new CanvasJS.Chart("chartContainer", {
                theme: "theme2", //theme1
                title: {
                    text: "Categorywise Order"
                },
                animationEnabled: true,   // change to true
                data: [{
                    type: "pie",       // Change type to "bar", "area", "spline", "pie",etc.
                    dataPoints: arrDta
                }]
            });
            chart.render();
        }
        function popUp(SF_Code, Order_date, Sf_Name, div_no, retailername, retailercode) {
            strOpen = "../MasterFiles/Reports/rpt_dcrproductdetail.aspx?Sf_Code=" + SF_Code + "&Activity_date=" + Order_date + "&div_code=" + div_no + "&Sf_Name=" + Sf_Name + "&retailer_name=" + retailername + "&retailer_code=" + retailercode;
            window.open(strOpen, '_blank', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=1000,height=600,left = 0,top = 0');

        }

     </script>
    <style type="text/css">
        .rptCellBorder
        {
            border: 1px solid;
            border-color :#999999;
        }
        td{padding:2px 5px;}
		.subTot{Font-size:11pt;font-weight:bold;}
		.GrndTot{Font-size:13pt;font-weight:bold;}
        .remove  
  {
    text-decoration:none;
  }
  	.TopButton{
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <asp:Panel ID="pnlbutton" runat="server">
            <table width="100%">
                <tr>
                <td width="20%"></td>
                    <td width="80%" align="center" >
                   
                    </td>
                    <td align="right">
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="btnPrint"  style="padding: 0px 20px;" class="btn btnPrint" OnClick="btnPrint_Click"/>
                                </td>
                                <td>


                                         <input id="Export"  style="padding: 0px 20px;" class="btn btnExcel"    type="linkbutton" value="Excel" height="35px"
	 
		 onclick="tablesToExcel(array1, array2, 'myfile.xls')"">   
                               
                                </td>
                                <td> 
 <input id="pdfexport" type="linkbutton" style="padding: 0px 20px;" class="btn btnPdf" value="PDF" height="35px"  onclick="generate()"" >   


</td>
                               
                                <td>
                                   <asp:LinkButton ID="LinkButton1" runat="Server" style="padding: 0px 20px;" class="btn btnClose"   OnClientClick="javascript:window.open('','_self').close();"/>


                                </td>

                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
      <asp:Panel id="pnlContents"  runat="server" Width="100%">
                <table border="0" id='1'   width="90%" style="margin:auto;">                 
                          
            <tr align="center"><td style="font-size:x-large;  font-weight: bold;font-family: Andalus;"><asp:Label ID="lblHead"   Font-Bold="true"  runat="server"></asp:Label> </td></tr>
            <tr align="right"><td align="left" style="font-size: LARGE; font-weight: bold;font-family: Andalus;">
                Order Taken By : <asp:Label ID="Feild" runat="server" Text="" ForeColor="Red"></asp:Label></td></tr>
                  <tr ><td align="left" style="font-size: LARGE; font-weight: bold;font-family: Andalus;">
                Distributer Name : <asp:Label ID="Label2" Font-Bold="true"  ForeColor="Red"  runat="server"></asp:Label></td></tr>
                
                         
            
                   
                    <tr> 
                        <td width="100%">
                            <asp:GridView ID="gvtotalorder" runat="server" Width="100%"   CssClass="newStly" 
                                HorizontalAlign="Center"
                                BorderWidth="1px" CellPadding="2" EmptyDataText="No Data found for View" 
                                AutoGenerateColumns="false"  HeaderStyle-BackColor="#33CCCC" 
                                HeaderStyle-HorizontalAlign="Center" BorderColor="Black" 
                                BorderStyle="Solid" >                               
                                <Columns>
                                
                                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                             <asp:Label ID="lblSNo" runat="server" Font-Size="9pt" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Distributor" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="stockist" style="white-space:nowrap" runat="server" Font-Size="9pt" Text='<%#Eval("Stockist_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order Taken By" ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="SF_name" runat="server" Font-Size="9pt" Text='<%#Eval("Sf_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

 									<asp:TemplateField HeaderText="Route" ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="Route_name" runat="server" Font-Size="9pt" Text='<%#Eval("routename")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order_Date" ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="Retailer_name" runat="server" Font-Size="9pt" Text='<%#Eval("Order_date")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Retailer" ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="Retailer_name" runat="server" Font-Size="9pt" Text='<%#Eval("retailername")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Net weight" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="netval" runat="server" Font-Size="9pt" Text='<%#DataBinder.Eval(Container.DataItem, "net_weight_value", "{0:N2}")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

									<asp:TemplateField HeaderText="Free" ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="freept" runat="server" Font-Size="9pt" Text='<%#Eval("free")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


  									<asp:TemplateField HeaderText="Total Value" ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="toval" runat="server" Font-Size="9pt" Text='<%#DataBinder.Eval(Container.DataItem, "Totalvalue", "{0:N2}")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Discount" ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="discountpri" runat="server" Font-Size="9pt" Text='<%#Eval("discount_price")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Order Value" ItemStyle-Width="120" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                              
                                            <a href="javascript:popUp('<%#Eval("SF_Code")%>','<%#Eval("Order_date")%>','<%#Eval("Sf_Name")%>','<%#Eval("Div_ID")%>','<%#Eval ("retailername")%>','<%#Eval("retailercode")%>')"> 
                                            <asp:Label ID="orderval" runat="server" Visible="true" Font-Size="9pt" Text='<%#DataBinder.Eval(Container.DataItem, "Order_value", "{0:N2}")%>'></asp:Label></a>
                                          
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                  
                                </Columns>
                                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
                        </td>
                    </tr>
                    </table>
          </asp:Panel>
         <script type="text/javascript">

             function generate() {

                 var doc = new jsPDF('l', 'pt');

                 var res = doc.autoTableHtmlToJson(document.getElementById("gvtotalorder"));
                 var res1 = doc.autoTableHtmlToJson(document.getElementById("GridViewcat"));



                 var header = function (data) {
                     doc.setFontSize(15);
                     doc.setTextColor(40);
                     doc.setFontStyle('normal');
                     //doc.addImage(headerImgData, 'JPEG', data.settings.margin.left, 17, 50, 50);
                     doc.text("Secondary Order View", data.settings.margin.top, 30);
                 };
                 var options = {
                     beforePageContent: header,
                     margin: {
                         top: 40
                     },
                     startY: doc.autoTableEndPosY() + 50
                 };
                 doc.autoTable(res.columns, res.data, options);

                 var header1 = function (data) {
                     doc.setFontSize(12);
                     doc.setTextColor(40);
                     doc.setFontStyle('normal');
                     //doc.addImage(headerImgData, 'JPEG', data.settings.margin.left, 17, 50, 50);
                     doc.text("Category-wise Order", data.settings.margin.left, doc.autoTableEndPosY() + 30);
                 };
                 var options1 = {
                     beforePageContent: header1,
                     margin: {
                         top: 40
                     },
                     startY: doc.autoTableEndPosY() + 50
                 };
                 doc.autoTable(res1.columns, res1.data, options1);
                 doc.save("Secondary_Order_View.pdf");
             }</script>
          
               </form>
</body>
</html>
