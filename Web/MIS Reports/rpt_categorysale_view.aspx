<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="rpt_categorysale_view.aspx.cs" Inherits="MIS_Reports_rpt_categorysale_view" %>
    <!DOCTYPE html>

    <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
      <head id="Head1" runat="server">
    <title>Shop sales</title>
 <script src='https://cdn.rawgit.com/simonbengtsson/jsPDF/requirejs-fix-dist/dist/jspdf.debug.js'></script>
<script src='https://unpkg.com/jspdf-autotable@2.3.2'></script>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
  <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />

      <style>
             #grid {
            border: 1px solid #ddd;
            border-collapse: collapse;
            width: 80%;
           
    overflow:scroll;
        }

    th {
        position: sticky;
        top: 0;
        background: #6c7ae0;
        font-weight: normal;
        font-size: 15px;
        color: white;
    }
            #grid td, table th {
                padding: 5px;
                border: 1px solid #ddd;
              
            }
        </style> 
          </head>
<body>
    <form id="form1" runat="server">
        <div>
             <center>
        <div style="margin-top: 12px;">
              <img  style="cursor: pointer;float:right;" src="/img/pdf.png" alt="" width="40" height="40" id="btnpdf">
            <img style="cursor: pointer;float:right;" src="/img/excel.png" alt="" width="40" height="40" id="btnExport">
        <table id="grid" class="grids" style="display: none;">
                <thead>
                    <tr>
                        <th>S.No</th>
                        <th>Shop</th>
						 <th>Category</th>
                        <th>Visited Date</th>
                        <th>TotalSales</th>
                        <th>Our Brand</th>
                         <th>%</th>
                    </tr>
                  </thead>
                <tbody></tbody>
             <tfoot></tfoot>
                </table></div></center>
        </div>
    </form>
     <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.3.3/jspdf.min.js"></script>
    <script type="text/javascript" src="https://html2canvas.hertzen.com/dist/html2canvas.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script type="text/javascript">
             $(document).ready(function () {
                 loadvisitdates();
				  loaddata();
             });
             var url = window.location.href;
             var newurl = new URL(url)
             var stcodes = newurl.searchParams.get('stcode');
             var fromdt = newurl.searchParams.get('fd');
             var todt = newurl.searchParams.get('td');
             var vdates = [];
			  function loadvisitdates() {
                $.ajax({
                    type: "Post",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "rpt_categorysale_view.aspx/saledr_visitdate",
                    data: "{'fromdate':'" + fromdt + "','todate':'" + todt + "','stcode':'" + stcodes + "'}",
                    dataType: "json",
                    success: function (data) {
                        vdates = JSON.parse(data.d);
                       
                       }
                });
            };
             function loaddata() {
                 $('#grid').show();
                 $('#grid tbody').html('');
               
                 var totalsal = 0;
                 var oursal = 0;
                 var percen = 0;
                 $.ajax({
                     type: "Post",
                     contentType: "application/json; charset=utf-8",
                     async:false,
                     url: "rpt_categorysale_view.aspx/saledr",
                     data: "{'fromdate':'" + fromdt + "','todate':'" + todt + "','stcode':'" + stcodes + "'}",
                     dataType: "json",
                     success: function (data) {
                         sales = JSON.parse(data.d);
                         str = '';

                         for (var i = 0; i < sales.length; i++) {
                             totalsals = sales[i].Total_sales
                             oursals = sales[i].our_sales
                             percents = ((oursals / (Number(totalsals) == 0 ? 1 : totalsals)) * 100)
                             per = isNaN(parseFloat(percents)) ? 0 : parseFloat(percents).toFixed(2);
							  var arr=vdates.filter(function (a) { return a.Trans_Detail_Info_Code == sales[i].ListedDrCode }).map(function (elem) {
                                 return elem.dt;
                             }).join(",");
                             str += "<tr><td>" + (i + 1) + "</td><td id=" + sales[i].Stockist_Code + ">" + sales[i].ListedDr_Name + "</td><td>" + sales[i].Doc_Class_ShortName + "</td><td>" + arr + "</td><td>" + sales[i].Total_sales + "</td><td>" + sales[i].our_sales + "</td><td>" + per + "</td></tr>";
                             totalsal += sales[i].Total_sales
                             oursal += sales[i].our_sales
                             
                         }
                         percent = ((oursal / (Number(totalsal) == 0 ? 1 : totalsal)) * 100)
                         percen = isNaN(parseFloat(percent)) ? 0 : parseFloat(percent).toFixed(2);

                         $('#grid tbody').append(str);
                         $('#grid tfoot').html("<tr><td colspan=4 style='text-align: center;font-weight: bold'>Total  Value</td><td><label>" + totalsal + "</label></td><td><label>" + oursal + "</label></td><td><label>" + percen + "</label></td></tr>");

                     }
                 });
            };
            $('#btnpdf').click(function () {

                var HTML_Width = $(".grids").width();
                var HTML_Height = $(".grids").height();
                var top_left_margin = 15;
                var PDF_Width = HTML_Width + (top_left_margin * 2);
                var PDF_Height = (PDF_Width * 1.5) + (top_left_margin * 2);
                var canvas_image_width = HTML_Width;
                var canvas_image_height = HTML_Height;

                var totalPDFPages = Math.ceil(HTML_Height / PDF_Height) - 1;


                html2canvas($(".grids")[0], { allowTaint: true }).then(function (canvas) {
                    canvas.getContext('2d');

                    console.log(canvas.height + "  " + canvas.width);


                    var imgData = canvas.toDataURL("image/jpeg", 1.0);
                    var pdf = new jsPDF('p', 'pt', [PDF_Width, PDF_Height]);
                    pdf.addImage(imgData, 'JPG', top_left_margin, top_left_margin, canvas_image_width, canvas_image_height);


                    for (var i = 1; i <= totalPDFPages; i++) {
                        pdf.addPage(PDF_Width, PDF_Height);
                        pdf.addImage(imgData, 'JPG', top_left_margin, -(PDF_Height * i) + (top_left_margin * 4), canvas_image_width, canvas_image_height);
                    }

                    pdf.save("shopsales.pdf");
                });
            });
            $('#btnExport').click(function () {

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
                htmls = document.getElementById("grid").innerHTML;


                var ctx = {
                    worksheet: 'Worksheet',
                    table: htmls
                }
                var link = document.createElement("a");
                var tets = 'shopsales' + '.xls';   //create fname

                link.download = tets;
                link.href = uri + base64(format(template, ctx));
                link.click();
            });
             </script>
</body>

</html>
