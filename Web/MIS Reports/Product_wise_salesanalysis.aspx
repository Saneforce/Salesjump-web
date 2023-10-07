<%@ Page Title="Productwise Sales Analysis" Language="C#" EnableEventValidation ="false"   AutoEventWireup="true" CodeFile="Product_wise_salesanalysis.aspx.cs" Inherits="MIS_Reports_Product_wise_salesanalysis" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head  Runat="Server">
   
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

#gvsalesanalysis
{
    text-align: right;
}
#gvsalesanalysis tr:nth-child(1),#gvsalesanalysis tr:nth-child(2)
{
    text-align: center;
}
#gvsalesanalysis tr:nth-child(3) td:nth-child(1)
{
	min-width: 250px;
	text-align: left;
}
#gvsalesanalysis tr td:nth-child(2)
{
	text-align: left;
}
#gvsalesanalysis tr:nth-child(3) td:nth-child(2)
{

	text-align: right;
}
#gvsalesanalysis tr:nth-child(1)  td:nth-child(2)
{
	text-align: center;
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
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px" Text="Excel" OnClick = "ExportToExcel" /></td>
                               
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
                            <asp:GridView ID="gvsalesanalysis" runat="server" Width="95%"   ShowHeader="false"
                                HorizontalAlign="Center" OnDataBound = "OnDataBound" OnRowCreated = "OnRowCreated"   OnRowDataBound="GridView1_RowDataBound"
                                BorderWidth="1px" CellPadding="2" EmptyDataText="No Data found for View" 
                                AutoGenerateColumns="true" CssClass="newStly" Font-Size="Small"
                                HeaderStyle-HorizontalAlign="Center" >                               
                                <Columns>
                              <%--   <asp:TemplateField>
        <ItemTemplate>
             <%#Container.DataItemIndex+1 %>
        </ItemTemplate>
    </asp:TemplateField>--%>
                                   <%-- <asp:TemplateField  ItemStyle-Width="50" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                             <asp:Label ID="lblSNo" runat="server" Font-Size="9pt" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField  ItemStyle-Width="250" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="stockist" style="white-space:nowrap" runat="server" Font-Size="9pt" Text='<%#Eval("Stockist_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField  ItemStyle-Width="570" HeaderStyle-BorderWidth="1" 
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="SF_name" runat="server" Font-Size="9pt" Text='<%#Eval("Sf_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

 <asp:TemplateField  ItemStyle-Width="270" HeaderStyle-BorderWidth="1" 
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="Route_name" runat="server" Font-Size="9pt" Text='<%#Eval("routename")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:TemplateField  ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="Retailer_name" runat="server" Font-Size="9pt" Text='<%#Eval("retailername")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField  ItemStyle-Width="70" HeaderStyle-BorderWidth="1" 
                                        HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="netval" runat="server" Font-Size="9pt" Text='<%#DataBinder.Eval(Container.DataItem, "net_weight_value", "{0:N2}")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField  ItemStyle-Width="120" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="orderval" runat="server" Font-Size="9pt" Text='<%#DataBinder.Eval(Container.DataItem, "Order_value", "{0:N2}")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                     <%--<asp:TemplateField  ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                            <asp:Repeater ID="Repeater1" runat="server">

                                              <ItemTemplate>
                                                   <asp:Label ID="stockist" runat="server" Font-Size="9pt" Text="dd"></asp:Label>
                                              </ItemTemplate>
                                            </asp:Repeater>
                                      
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                   
                                </Columns>
                                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
                        </td>
                    </tr>
                  
    </asp:Panel>
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
              </div>
         
     
     
     
     
<script type="text/javascript">

    function generate() {

        var doc = new jsPDF('l', 'pt');

        var res = doc.autoTableHtmlToJson(document.getElementById("gvsalesanalysis"));



        var header = function (data) {
            doc.setFontSize(10);
            doc.setTextColor(40);
            doc.setFontStyle('normal');
            //doc.addImage(headerImgData, 'JPEG', data.settings.margin.left, 17, 50, 50);
            doc.text("Productwise_sales_analysis", data.settings.margin.top, 70);
        };
        var options = {
            beforePageContent: header,
            margin: {
                top: 80
            },
            startY: doc.autoTableEndPosY() + 20
        };
        doc.autoTable(res.columns, res.data, options);




        doc.save("RetailerOfftake.pdf");
    }</script>
  </form>
</body>
</html>



