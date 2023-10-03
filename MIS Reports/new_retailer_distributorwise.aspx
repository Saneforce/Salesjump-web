<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Master.master" CodeFile="new_retailer_distributorwise.aspx.cs" Inherits="MIS_Reports_new_retailer_distributorwise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../css/jquery.multiselect.css" rel="stylesheet" />
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
        <asp:Label ID="lblHead"  SkinID="lblMand" Font-Bold="true" style="font-size:large"
            Font-Underline="true" runat="server" />
        <br /> 

       
        <img  style="cursor: pointer;float:right;" src="/img/pdf.png" alt="" width="40" height="40" id="btnpdf">
            <img style="cursor: pointer;float:right;" src="/img/excel.png" alt="" width="40" height="40" id="btnExport"><br /><br />
      <table class="grids" id="grid">
                <thead>
                  </thead>
                <tbody></tbody>
         <tfoot></tfoot>
      </table>
           <div class="modal fade" id="leaveModal" style="z-index: 10000000; overflow-y: scroll;" tabindex="0" aria-hidden="true">
            <div class="modal-dialog" role="document" style="width: 98% !important">
                <div class="modal-content">
                    <div class="modal-header">
                            <h5 class="modal-title" id="leaveModalLabel"></h5>
                           <button type="button" class="close" style="margin-top: -20px;" data-dismiss="modal"  data-toggle="modal" data-target="#summaryModal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    </div>
                   
                    <div class="modal-body" style="padding-top: 10px">
                        <div class="row">
                            <div class="col-sm-12">
                                 <table id="leavedets" style="width: 100%;font-size:12px;">
                                    <thead class="text-warning">
                                        <tr>
                                            <th style="text-align: left">S.No</th>
                                            <th style="text-align: left">Retailer_code</th>
                                            <th style="text-align: left">Retailer_Name</th>
                                          </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal" data-toggle="modal" data-target="#summaryModal">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
          
    </form>
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.3.3/jspdf.min.js"></script>
    <script type="text/javascript" src="https://html2canvas.hertzen.com/dist/html2canvas.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
     <script type="text/javascript" src="http://code.jquery.com/jquery-3.3.1.js"></script>
    <script type="text/javascript">
          var str = "";
          var arr = [];
        var prods = []; var prodcat = []; var prodcnt = []; var prodrt = [];
          $(document).ready(function () {
              Fillcat();
              Fillroutecnt();
              Fillprodcnt();
              FillProduct();
             
          });
        function Fillcat() {
            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "new_retailer_distributorwise.aspx/Fillcat",
                async: false,
                dataType: "json",
                success: function (data) {
                    prodcat = JSON.parse(data.d);
                }
            });
        }
        function Fillroutecnt() {
            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "new_retailer_distributorwise.aspx/Fillroutecnt",
                async: false,
                dataType: "json",
                success: function (data) {
                    prodrt = JSON.parse(data.d);
                }
            });
        }
        function Fillprodcnt() {
            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "new_retailer_distributorwise.aspx/Fillprodcnt",
                async: false,
                dataType: "json",
                success: function (data) {
                    prodcnt = JSON.parse(data.d);
                }
            });
        }
          function FillProduct() {
             $.ajax({
                  type: "Post",
                  contentType: "application/json; charset=utf-8",
                  url: "new_retailer_distributorwise.aspx/Filldtl",
                 async: false,
                  dataType: "json",
                 success: function (data) {
                     prods = JSON.parse(data.d);
                     Reloadtable();
                     }
              });
        }
        function Reloadtable() {
            $('#grid tbody').html('');
            sth = '';
            str = ''; var tt = []; var valtot = 0; var nettot = 0; var col = 0; var rot = []; var value = []; var net_weight_value = [];
            sth += "<tr><th>S.NO</th><th>State Name</th><th>District</th><th>Name</th><th>Route</th><th>Retailer</th>";
            //for (var n = 0; n < prodcat.length; n++) {
            //    sth += "<th>" + prodcat[n].Product_Cat_Name + "</th>";
            //}
            sth += "<th>Values</th><th>Net Weight</th></tr>";
            for (var i = 0; i < prods.length; i++) {
                rot = prodrt.filter(function (a) {
                    return a.Stockist_Code == prods[i].Stockist_Code;
                });
                str += "<tr><td>" + (i + 1) + "</td><td>" + prods[i].Dist_Name + "</td><td>" + prods[i].Taluk_Name + "</td><td>" + prods[i].sf_name + "</td><td>" + (rot.length > 0 ? rot[0].route : '') + "</td><td>" + (prods[i].retail > 0 ? prods[i].retail : '') + "</td>";
                
                //for (var k = 0; k < prodcat.length; k++) {
                //    tt = prodcnt.filter(function (a) {
                //        return a.Stockist_Code == prods[i].Stockist_Code && a.Product_Cat_Code == prodcat[k].Product_Cat_Code;
                //    });
                //    str += "<td><a href='#' onclick='sfleave(\"" + prods[i].Stockist_Code + "\",\"" + prodcat[k].Product_Cat_Code + "\")'>" + (tt.length > 0 ? tt[0].rretail : '') + "<a/></td>"
                    
                //    //value = prodcnt.filter(function (a) {
                //    //    return a.Stockist_Code == prods[i].Stockist_Code && a.Product_Cat_Code == prodcat[k].Product_Cat_Code;
                //    //});
                //    //str += "<td>" + (value.length > 0 ? value[0].value : '0') + "</td>"
                //    //valtot += value[0].value;
                //    //net_weight_value = prodcnt.filter(function (a) {
                //    //    return a.Stockist_Code == prods[i].Stockist_Code && a.Product_Cat_Code == prodcat[k].Product_Cat_Code;
                //    //});
                //    //str += "<td>" + (net_weight_value.length > 0 ? net_weight_value[0].net_weight_value : '0') + "</td>"
                //    //nettot += net_weight_value[0].net_weight_value;
                    
                //}
               
                str += "<td>" + prods[i].value.toFixed(2) + "</td><td>" + prods[i].net_weight_value.toFixed(2) + "</td></tr>";
                valtot += prods[i].value; nettot += prods[i].net_weight_value;
            }
            $('#grid thead').append(sth);
            $('#grid tbody').append(str);
            col = 6;
            $('#grid tfoot').html("<tr><td colspan=" + col + " style='text-align: center;font-weight: bold'>Total  Value</td><td>" + valtot.toFixed(2) + "</td><td>" + nettot.toFixed(2) + "</td></tr > ");

        }
        function sfleave(lsfc, at) {
            var dtk = lsfc;
            var prd = at;
             $('#summaryModal').modal('hide');
            $("#leaveModal .modal-title").html("Retailer Details");
            $('#leaveModal').modal('toggle');
            $('#leavedets TBODY').html("<tr><td colspan=10>Loading please wait...</td></tr>");
            setTimeout(function () {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "new_retailer_distributorwise.aspx/Fillretaildetail",
                    data: "{'stckcode':'" + dtk + "','prdcode':'" + prd + "'}",
                    dataType: "json",
                    success: function (data) {
                        AllOrders3 = JSON.parse(data.d) || [];
                        $('#leavedets TBODY').html("");
                        var slno = 0;
                        for ($j = 0; $j < AllOrders3.length; $j++) {
                            if (AllOrders3.length > 0) {
                                slno += 1;
                                tr = $("<tr></tr>");
                                $(tr).html("<td>" + slno + "</td><td>" + AllOrders3[$j].ListedDrCode + "</td><td>" + AllOrders3[$j].rretail + "</td>");
                                $("#leavedets TBODY").append(tr);

                            }
                        }
                    },
                    error: function (result) {
                        $('#leavedets TBODY').html("<tr><td colspan=14>Something went wrong. Try again.</td></tr>");
                        //alert(JSON.stringify(result));
                    }
                })
            }, 500);
        }
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

                  pdf.save("Reteiler_distributorwise_values.pdf");
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
              var tets = 'Reteiler_distributorwise_values' + '.xls';   //create fname

              link.download = tets;
              link.href = uri + base64(format(template, ctx));
              link.click();
          });
      </script>
</body>
</asp:Content>