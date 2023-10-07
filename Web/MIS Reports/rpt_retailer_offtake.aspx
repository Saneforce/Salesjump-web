<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_retailer_offtake.aspx.cs"  EnableEventValidation ="false" Inherits="MIS_Reports_rpt_retailer_offtake" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Retailer Offtake</title>
 <script src='https://cdn.rawgit.com/simonbengtsson/jsPDF/requirejs-fix-dist/dist/jspdf.debug.js'></script>
<script src='https://unpkg.com/jspdf-autotable@2.3.2'></script>

   <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
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

             filename: "RetailerOfftake",
             fileext: ".xls"
         });
     };
</script>
</head>
<body>
    <form id="form1" runat="server">
  
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
   
  
      <asp:Panel id="pnlContents"  runat="server" Width="100%" align="center">
        <Center>
                <table border="0" id='1'   width="100%">                 
                    </br>
            </br>                                
          <tr align="right"><td  width="40%">&nbsp;&nbsp;</td><td align="left" style="font-size: small; font-weight: bold;font-family: Andalus;">Retailer Name:
                   <asp:Label ID="lblretailer" runat="server" Text="" ForeColor="Red"></asp:Label></td></tr>
                   </table>      
            </br>
           
                   
                   
                            <asp:GridView ID="GridView1" runat="server" GridLines="None" AutoGenerateColumns="false"  ForeColor="Black" BackColor="#cffabd " Width="60%"   HeaderStyle-BackColor="#1A4A85"  HeaderStyle-ForeColor="White" Font-Names="Andalus"
                             BorderStyle="Solid" AlternatingRowStyle-BackColor="#b9f6ca" HeaderStyle-Height="38px">
                    <Columns>
                    

                     <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50"   HeaderStyle-Width="10"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-HorizontalAlign="Center"  >
                                        <ItemTemplate>
                                            
                                             <asp:Label ID="lblSNo" runat="server"
                                            Font-Size="9pt" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Product  Name" ItemStyle-Width="160" ItemStyle-Height="35"    HeaderStyle-Width="10"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                         <asp:HiddenField ID="HiddenField2" runat="server" Value='<%# Eval("Product_code") %>' />
                                  
                                            <asp:Label ID="sname" style="white-space:nowrap" runat="server" Font-Size="9pt" Text='<%#Eval("Product_name")%>'></asp:Label>
                                       
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="OP" ItemStyle-Width="100" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                         
                                            <asp:Label ID="oplbl" runat="server" Font-Size="9pt" Text='<%#Eval("op")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="CL" ItemStyle-Width="100" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                         
                                            <asp:Label ID="cllbl" runat="server" Font-Size="9pt" Text='<%#Eval("cl")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sale" ItemStyle-Width="100" ItemStyle-Height="35"
                                        HeaderStyle-Font-Size="10pt"  ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                         
                                            <asp:Label ID="sllbl" runat="server" Font-Size="9pt" Text='<%#Eval("sale")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                     
                    </Columns>
                    <HeaderStyle HorizontalAlign="Left" />

                    </asp:GridView>
                      
                     </center>
 </asp:Panel>


            
        
     
     
<script type="text/javascript">

    function generate() {

        var doc = new jsPDF('l', 'pt');

        var res = doc.autoTableHtmlToJson(document.getElementById("GridView1"));



        var header = function (data) {
            doc.setFontSize(10);
            doc.setTextColor(40);
            doc.setFontStyle('normal');
            //doc.addImage(headerImgData, 'JPEG', data.settings.margin.left, 17, 50, 50);
            doc.text("RetailerOfftake", data.settings.margin.top, 70);
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

