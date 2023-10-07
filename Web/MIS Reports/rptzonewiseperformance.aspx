<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="rptzonewiseperformance.aspx.cs" Inherits="MIS_Reports_rptzonewiseperformance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Zonewise Performance</title>
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
	
    
 
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <asp:Panel ID="pnlbutton" runat="server">
            <table width="100%">
                <tr>
                <td width="20%"></td>
                    <td width="80%" align="center" >
                    <asp:Label ID="lblHead"    Font-Names="Andalus"   Font-Underline="true"
                runat="server" Font-Size="Large"></asp:Label>
                    </td>
                    <td align="right">
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="btnPrint" runat="Server" style="padding: 0px 20px;" class="btn btnPrint" OnClick="btnPrint_Click"/>
                                </td>
                                <td>


                                          <asp:LinkButton id="Export"  runat="Server" style="padding: 0px 20px;" class="btn btnExcel"    type="linkbutton" value="Excel" height="35px"
	 
		 onclick="ExportToExcel"> </asp:LinkButton>  
                               
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
      <asp:Panel id="pnlContents"  runat="server" Width="80%" style="margin:5px;">
               <table border="0" id='1'   width="80%" style="margin:auto;">                  
                                                   
          <tr align="right"><td align="left" style="font-size: small; font-weight: bold;font-family: Andalus;">Designation Name:
                   <asp:Label ID="desig" runat="server" Text="" ForeColor="Red"></asp:Label></td></tr>
                         
            
                   
                    <tr> 
                        <td width="80%">
                            <asp:HiddenField ID="HiddenField1" runat="server"  />

                            <asp:GridView ID="gvtotalorder" runat="server" Width="100%"   ShowHeader="false" Font-Size="9pt" CssClass="newStly" 
                                HorizontalAlign="Center" OnRowCreated = "OnRowCreated"   
                                BorderWidth="1px" CellPadding="2" EmptyDataText="No Data found for View" 
                                AutoGenerateColumns="true" BackColor="#ffffe0" 
                                HeaderStyle-HorizontalAlign="Center" BorderColor="Black" BorderStyle="Solid">                               
                                
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




        doc.save("zonewiseperformance.pdf");
    }</script>
  </form>
</body>
</html>