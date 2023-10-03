<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SS_rpt_Total_Order_View.aspx.cs" Inherits="MIS_Reports_SS_rpt_Total_Order_View" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=SecOrderCap %> View</title>
     <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.3.3/jspdf.min.js"></script>
    <script type="text/javascript" src="https://html2canvas.hertzen.com/dist/html2canvas.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
 <script src='https://cdn.rawgit.com/simonbengtsson/jsPDF/requirejs-fix-dist/dist/jspdf.debug.js'></script>
<script src='https://unpkg.com/jspdf-autotable@2.3.2'></script>

  <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            if(<%=divcode%>!="32"){
                $(document).find('.hidecl').hide();
                $(document).find('.hidecll').closest('td').hide();
            }
			//CssClass="hidecll" HeaderStyle-CssClass="hidecl" Order Type
            if ('<%=divcode%>' == '52') {
                $('.hidecancl').show();$('.hidecancll').show();
                $('.hidecancl').closest('td').show();
            }
            else {
                $('.hidecancl').hide();
                $('.hidecancll').hide();
                $('.hidecancl').closest('td').hide();
            }
			var Today = '<%=date%>';
            $(document).on('click', "#btnPrint", function () {
                var originalContents = $("body").html();
                var printContents = $("#content").html();
                $("body").html(printContents);
                window.print();
                $("body").html(originalContents);
                return false;
            });
           $(document).on('click', ".sub", function () {
                var dist_code = $(this).closest('tr').attr('data-stk');
                var Order_date = document.getElementById("Label2").innerHTML.split(':')[1].trim().split('-').reverse().join('-');

                strOpen = "rpt_cuswsprod.aspx?Date=" + Order_date + "&Distcode=" + dist_code
                window.open(strOpen, '_blank', 'statusbar=1,scrollbar=1,locator=0,width=1000,height=500,menubar=1,menubar=0,resizable=1,top=0,bottom=0');
            })
    $(document).on("click",".cat",  function () {
                strOpen = "rpt_HQ_Wise_Order.aspx?Order_date=" + Today
                window.open(strOpen,'_blank','statusbar=1,scrollbar=1,locator=0,height=600,width=1000,menubar=1,menubar=0,resizable=1,top=0,bottom=0');
    });
               $(document).on('click', ".btnPdf", function () {

              var HTML_Width = $("#gvtotalorder").width();
              var HTML_Height = $("#gvtotalorder").height();
              var top_left_margin = 15;
              var PDF_Width = HTML_Width + (top_left_margin * 2);
              var PDF_Height = (PDF_Width * 1.5) + (top_left_margin * 2);
              var canvas_image_width = HTML_Width;
              var canvas_image_height = HTML_Height;

              var totalPDFPages = Math.ceil(HTML_Height / PDF_Height) - 1;


              html2canvas($("#gvtotalorder")[0], { allowTaint: true }).then(function (canvas) {
                  canvas.getContext('2d');

                  console.log(canvas.height + "  " + canvas.width);


                  var imgData = canvas.toDataURL("image/jpeg", 1.0);
                  var pdf = new jsPDF('p', 'pt', [PDF_Width, PDF_Height]);
                  pdf.addImage(imgData, 'JPG', top_left_margin, top_left_margin, canvas_image_width, canvas_image_height);


                  for (var i = 1; i <= totalPDFPages; i++) {
                      pdf.addPage(PDF_Width, PDF_Height);
                      pdf.addImage(imgData, 'JPG', top_left_margin, -(PDF_Height * i) + (top_left_margin * 4), canvas_image_width, canvas_image_height);
                  }

                  pdf.save("Reteiler_distributorwise_values.pdf");
              });
          });
        });
        
        function cancelOrder(translno) {
            if (confirm("Do you want delete this order ?")) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "rpt_Total_Order_View.aspx/deleteOrder",
                    data: "{'TransslNo':'" + translno + "'}",
                    dataType: "json",
                    success: function (data) {
                        alert('Order Deleted');
                    },
                    error: function (exception) {
                        alert(exception.responseText);
                    }
                });
            }
        }
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
        , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="https://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets>'
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
	                type: "pie",
					showInLegend: true,
                    toolTipContent: "{y} - #percent %",					// Change type to "bar", "area", "spline", "pie",etc.
	                dataPoints: arrDta
	            }]
	        });
	        chart.render();
	    }

function popUp(SF_Code, Order_date, Sf_Name, div_no, retailername, retailercode ) {
                strOpen = "../MasterFiles/Reports/rpt_dcrproductdetail.aspx?Sf_Code=" + SF_Code + "&Activity_date=" + Order_date + "&div_code=" + div_no + "&Sf_Name=" + Sf_Name + "&retailer_name=" + retailername + "&retailer_code=" + retailercode;
                window.open(strOpen, '_blank', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=1000,height=600,left = 0,top = 0');

        } 
  function popUp1(Date11, Product_Cat_Code, Product_Cat_Name) {
            strOpen = "rpt_Catagory_Wise_Order.aspx?Product_Cat_Code=" + Product_Cat_Code + "&Order_date=" + Date11 + "&Product_Cat_Name=" + Product_Cat_Name 
            window.open(strOpen, '_blank', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=1000,height=600,left = 0,top = 0');

        }  
       function popUp123(sfcode, year, sfname, date, month) {
            var strOpen = "rpt_emp_order_valueSTKwise.aspx?SF_Code=" + sfcode + "&Year=" + year + "&SF_Name=" + sfname + "&Date=" + date + "&cur_month=" + month + "&Sub_Div=" + "0";
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
                 fileext: ".xls"           });  
     }

     $(document).ready(function () {
         $(document).on('click', "#btnPrint", function () {
                var originalContents = $("body").html();
                var printContents = $("#content").html();
                $("body").html(printContents);
             window.print();
            return false;
                //location.reload();
                //$("body").html(originalContents);
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
                <td width="20%"></td>
                    <td width="80%" align="center" >
                   
                    </td>
                    <td align="right">
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="btnPrint"  style="padding: 0px 20px;" class="btn btnPrint"/> <%--OnClick="btnPrint_Click--%>
                                </td>
                                <td>


                                         <input id="Export"  style="padding: 0px 20px;" class="btn btnExcel"    type="linkbutton" value="Excel" height="35px"
	 
		 onclick="tablesToExcel(array1, array2, 'myfile.xls')"">   
                               
                                </td>
                                <td> 
 <input id="pdfexport" type="linkbutton" style="padding: 0px 20px;" class="btn btnPdf" value="PDF" height="35px"  " >   


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


<a style="display:none;" href="javascript:popUp123('<%=sfcode%>','<%=year%>','<%=sfname%>','<%=date%>','<%=month%>')">
                            RPT View</a>
                <table border="0" id='1'   width="90%" style="margin:auto;">                 
                          
            <tr align="center"><td style="font-size:x-large;  font-weight: bold;font-family: Andalus;"><asp:Label ID="lblHead"   Font-Bold="true"  runat="server"></asp:Label> </td></tr>
            <tr align="right"><td align="left" style="font-size: LARGE; font-weight: bold;font-family: Andalus;">FieldForce Name:<asp:Label ID="Feild" runat="server" Text="" ForeColor="Red"></asp:Label> <asp:Label ID="Label2" Font-Bold="true" style="float:right"   runat="server"></asp:Label></td></tr>
                         
            
                   
                    <tr> 
                        <td width="100%">
                            <asp:GridView ID="gvtotalorder" runat="server" Width="100%"   CssClass="newStly" 
                                HorizontalAlign="Center" OnDataBound = "OnDataBound" OnRowCreated = "OnRowCreated"
                                BorderWidth="1px" CellPadding="2" EmptyDataText="No Data found for View" 
                                AutoGenerateColumns="false"  HeaderStyle-BackColor="#33CCCC" 
                                HeaderStyle-HorizontalAlign="Center" BorderColor="Black" 
                                BorderStyle="Solid" onrowdatabound="gvtotalorder_RowDataBound">                               
                                <Columns>
                                
                                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                             <asp:Label ID="lblSNo" runat="server" Font-Size="9pt" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Supplier" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
					     <asp:HiddenField ID="stkcode" runat="server" Value='<%#Eval("Stockist_Code")%>'></asp:HiddenField>
                                            <asp:Label ID="stockist" style="white-space:nowrap" runat="server" Font-Size="9pt" Text='<%#Eval("Stockist_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order Taken By" ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="SF_name" runat="server" Font-Size="9pt" Text='<%#Eval("Sf_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField  HeaderText="Order Type" ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="Order_Type"  runat="server" Font-Size="9pt" Text='<%#Eval("OrderType")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

 									<asp:TemplateField HeaderText="Route" ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="Route_name" runat="server" Font-Size="9pt" Text='<%#Eval("routename")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Distributor" ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
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
                                            <asp:Label ID="toval" runat="server" Font-Size="9pt" Text='<%#Eval("Totalvalue")%>'></asp:Label>
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
                                            <asp:Label ID="orderval" runat="server" Font-Size="9pt" Text='<%#DataBinder.Eval(Container.DataItem, "Sub_Total", "{0:N2}")%>'></asp:Label></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Order Cancel" HeaderStyle-CssClass="hidecancll" ItemStyle-Width="120" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
  										    <a href="#" class="hidecancl" onclick="cancelOrder('<%#Eval("Trans_Sl_No")%>')">Cancel</a>
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
                    <table id='2'  width="90%" style="margin:auto;"> 
                    <tr><td>                     
                    <tr>
                    <td align="left" width="50%"> <asp:Label ID="Label1" runat="server" Font-Size="Medium" Font-Bold="True" Font-Names="Andalus" Font-Underline="true" Text="Categorywise Order"></asp:Label>
                     <asp:GridView ID="GridViewcat" runat="server" Width="100%" CssClass="newStly"  OnDataBound="catOnDataBound"
                                HorizontalAlign="Center"
                                BorderWidth="1px" CellPadding="2" EmptyDataText="No Data found for View" 
                                AutoGenerateColumns="false"  HeaderStyle-BackColor="#33CCCC" 
                                HeaderStyle-HorizontalAlign="Center" BorderColor="Black" BorderStyle="Solid">                               
                                <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo1" runat="server" Font-Size="9pt" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Product Category" ItemStyle-Width="50" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNou" runat="server" Font-Size="9pt" Text='<%#Eval("Product_Cat_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Net weight" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lnetweight" runat="server" Font-Size="9pt" Text='<%#Eval("net_weight")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lquantity" runat="server" Font-Size="9pt" Text='<%#Eval("quantity")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                   
                                   
                                    <asp:TemplateField HeaderText="Value" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                                <a href="javascript:popUp1('<%=Date11%>','<%#Eval("Product_Cat_Code")%>','<%#Eval("Product_Cat_Name")%>')"> 
                                            <asp:Label ID="cval" runat="server" Font-Size="9pt" Text='<%#Eval("value")%>'></asp:Label>
                                                </a>  </ItemTemplate>
                                    </asp:TemplateField>  
                                   
                                </Columns>
                               

<EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
                    </td><td align="right">   <div id="chartContainer" style="text-align:center;height:180px; width: 95%; padding-top:30px;"></div>          </td></tr>       </td></tr>
                </table>
 </asp:Panel>
              </div>
          
     
     
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
