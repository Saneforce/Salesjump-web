<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="rpt_newstatewise.aspx.cs" Inherits="MIS_Reports_rpt_newstatewise" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml"><head runat="server">
    <title>Statewise Order Details</title>
     <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
     <link href="../css/style.css" rel="stylesheet" />   
    <style type="text/css">
      #grid {
            border: 1px solid #ddd;
            border-collapse: collapse;
        }

        th {
          
            top: 0;
            background: #6c7ae0;
            text-align: center;
            font-weight: normal;
            font-size: 15px;
            color: white;
        }

       

        table td, table th {
            padding: 5px;
            border: 1px solid #ddd;
            font-size: 12px;
        }
    </style>
<script language="Javascript">
    function RefreshParent() {

        window.close();
    }
    </script>
</head>
<body>
       <form id="form1" runat="server">
    <div class="container">
        <br />
        <asp:Label ID="lblHead" Text="Retailer Field Force Wise" SkinID="lblMand" Font-Bold="true" style="font-size:large"
            Font-Underline="true" runat="server" />
        <br /> 

        <div style="text-align: left; padding: 2px 50px;">
                    <b>
                        <asp:Label ID="lblsf_name" runat="server"></asp:Label>
                        <asp:HiddenField ID="hidn_sf_code" runat="server" />
                         <asp:HiddenField ID="divcd" runat="server" />
                        <asp:HiddenField ID="hidnYears" runat="server" />
                        <asp:HiddenField ID="statefl" runat="server" />
                   </b>
                </div>
        <img  style="cursor: pointer;float:right;" src="/limg/pdf.png" alt="" width="40" height="40" id="btnpdf">
            <img style="cursor: pointer;float:right;" src="/img/excel.png" alt="" width="40" height="40" id="btnExport">
      <table class="grids" id="grid">
                <thead>
                    <tr>
                        <th rowspan="2">S.No</th>
                        <th rowspan="2">State</th>
                        <th rowspan="2">SO Name</th>
                        <th rowspan="2">TSM Name</th>
                        <th rowspan="2">Beat</th>
                        <th rowspan="2">Retailer Name</th>
                        <th rowspan="2">Distributor</th>
                        <th colspan="2">Jan</th>
                        <th colspan="2">Feb</th>
                        <th colspan="2">Mar</th>
                        <th colspan="2">Apr</th>
                        <th colspan="2">May</th>
                        <th colspan="2">Jun</th>
                        <th colspan="2">Jul</th>
                        <th colspan="2">Aug</th>
                         <th colspan="2">Sep</th>
                        <th colspan="2">Oct</th>
                        <th colspan="2">Nov</th>
                        <th colspan="2">Dec</th>
                    </tr>
                    <tr>

                        <th>Value</th>
                        <th>SKU</th>
                        <th>Value</th>
                        <th>SKU</th>
                        <th>Value</th>
                        <th>SKU</th>
                        <th>Value</th>
                        <th>SKU</th>
                         <th>Value</th>
                        <th>SKU</th>
                        <th>Value</th>
                        <th>SKU</th>
                        <th>Value</th>
                        <th>SKU</th>
                        <th>Value</th>
                        <th>SKU</th>
                        <th>Value</th>
                        <th>SKU</th>
                        <th>Value</th>
                        <th>SKU</th>
                         <th>Value</th>
                        <th>SKU</th>
                        <th>Value</th>
                        <th>SKU</th>
                    </tr>
                </thead>
                <tbody></tbody>
				  <tfoot></tfoot></table>
    </div>
    </form>
     <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
     <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.3.3/jspdf.min.js"></script>
    <script type="text/javascript" src="https://html2canvas.hertzen.com/dist/html2canvas.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
      <script type="text/javascript">
          var str = "";
          var arr = [];
          var prods = [];
          $(document).ready(function () {
             
              FillProduct();

            
          });
          function FillProduct() {
              var sfCode = $('#<%=hidn_sf_code.ClientID%>').val();
              var divcode = $('#<%=divcd.ClientID%>').val();
              var fYears = $('#<%=hidnYears.ClientID%>').val();
              var stcode = $('#<%=statefl.ClientID%>').val();
			   var janv = 0; var jans = 0; var fbv = 0; var fbs = 0; var mv = 0; var ms = 0;
              var apv = 0; var aps = 0; var myv = 0; var mys = 0; var jnv = 0; var jns = 0;
              var jlv = 0; var jls = 0; var auv = 0; var aus = 0; var sev = 0; var ses = 0;
              var ocv = 0; var ocs = 0; var novv = 0; var novs = 0; var dev = 0; var des = 0;
              $.ajax({
                  type: "Post",
                  contentType: "application/json; charset=utf-8",
                  url: "rpt_newstatewise.aspx/Filldtl",
                  data: "{'sf_code':'" + sfCode + "','div':'" + divcode + "','fdt':'" + fYears + "','stat':'" + stcode + "'}",
                  async: true,
                  dataType: "json",
                  success: function (data) {
                      $('#grid tbody').html('');
                      prods = JSON.parse(data.d);
                      str = '';
                      for (var i = 0; i < prods.length; i++) {

                          str += "<tr><td>" + (i + 1) + "</td><td>" + prods[i].StateName + "</td><td>" + prods[i].Sf_Name + "</td><td>" + prods[i].mgrname + "</td><td>" + prods[i].route + "</td><td>" + prods[i].retailern + "</td>"
                              + "<td>" + prods[i].distributor + "</td><td>" + prods[i].Jan_value + "</td><td>" + prods[i].Jan_sku + "</td><td>" + prods[i].Feb_value + "</td><td>" + prods[i].Feb_sku + "</td><td>" + prods[i].Mar_value + "</td><td>" + prods[i].Mar_sku + "</td>"
                              + "<td>" + prods[i].Apr_value + "</td><td>" + prods[i].Apr_sku + "</td><td>" + prods[i].May_value + "</td><td>" + prods[i].May_sku + "</td><td>" + prods[i].Jun_value + "</td><td>" + prods[i].Jun_sku + "</td><td>" + prods[i].Jul_value + "</td><td>" + prods[i].Jul_sku + "</td>"
                              + "<td>" + prods[i].Aug_value + "</td><td>" + prods[i].Aug_sku + "</td><td>" + prods[i].Sep_value + "</td><td>" + prods[i].Sep_sku + "</td><td>" + prods[i].Oct_value + "</td><td>" + prods[i].Oct_sku + "</td><td>" + prods[i].Nov_value + "</td><td>" + prods[i].Nov_sku + "</td>"
                              + "<td>" + prods[i].Dec_value + "</td><td>" + prods[i].Dec_sku + "</td></tr>";
							  
							   janv += prods[i].Jan_value; jans += prods[i].Jan_sku; fbv += prods[i].Feb_value; fbs += prods[i].Feb_sku; mv += prods[i].Mar_value; ms += prods[i].Mar_sku;
                          apv += prods[i].Apr_value; aps += prods[i].Apr_sku; myv += prods[i].May_value; mys += prods[i].May_sku; jnv += prods[i].Jun_value; jns += prods[i].Jun_sku;
                          jlv += prods[i].Jul_value; jls += prods[i].Jul_sku; auv += prods[i].Aug_value; aus += prods[i].Aug_sku; sev += prods[i].Sep_value; ses += prods[i].Sep_sku;
                          ocv += prods[i].Oct_value; ocs += prods[i].Oct_sku; novv += prods[i].Nov_value; novs += prods[i].Nov_sku; dev += prods[i].Dec_value; des += prods[i].Dec_sku;
                      
                       
                      }
                      $('#grid tbody').append(str);
					  
					   $('#grid tfoot').html("<tr><td colspan=7 style='text-align: center;font-weight: bold'>Total  Value</td><td>" + janv.toFixed(2) + "</td><td>" + jans + "</td><td>" + fbv.toFixed(2) + "</td><td>" + fbs + "</td><td>" + mv.toFixed(2) + "</td><td>" + ms + "</td><td>" + apv.toFixed(2) + "</td><td>" + aps + "</td>"
                          + "<td>" + myv.toFixed(2) + "</td><td>" + mys + "</td><td>" + jnv.toFixed(2) + "</td><td>" + jns + "</td><td>" + jlv.toFixed(2) + "</td><td>" + jls + "</td><td>" + auv.toFixed(2) + "</td><td>" + aus + "</td>"
                          + "<td>" + sev.toFixed(2) + "</td><td>" + ses + "</td><td>" + ocv.toFixed(2) + "</td><td>" + ocs + "</td><td>" + novv.toFixed(2) + "</td><td>" + novs + "</td><td>" + dev.toFixed(2) + "</td><td>" + des + "</td></tr > ");
                  }
              });
          } $('#btnpdf').click(function () {

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

                  pdf.save("statewise.pdf");
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
              var tets = 'statewise' + '.xls';   //create fname

              link.download = tets;
              link.href = uri + base64(format(template, ctx));
              link.click();
          });
      </script>
</body>
</html>
