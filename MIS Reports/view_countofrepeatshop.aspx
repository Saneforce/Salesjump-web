<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="view_countofrepeatshop.aspx.cs" EnableEventValidation="false" Inherits="MIS_Reports_view_countofrepeatshop" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!doctype html public "-//w3c//dtd xhtml 1.0 transitional//en" "http://www.w3.org/tr/xhtml1/dtd/xhtml1-transitional.dtd">    <title>Designationwise_wtype</title>
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
       <div class="container">
            <button style="float:right;margin-right: 21px;" type="button" id="btnback" class="btn btn-primary">Back</button>
            <br />
        <asp:Label ID="lblHead" Text="" SkinID="lblMand" Font-Bold="true" style="font-size:large"
            Font-Underline="true" runat="server" />
        <br /> 
          
    
         <img  style="cursor: pointer;float:right;" src="/img/pdf.png" alt="" width="40" height="40" id="btnpdf">
            <img style="cursor: pointer;float:right;" src="/img/excel.png" alt="" width="40" height="40" id="btnExport">
        <table id="grid" class="grids" style="display:none;">
                <thead>
                    <tr>
                        <th>S.No</th>
                        <th>Outlet Name</th>
                        <th>Field Force</th>
                        <th>Route</th>
                        <th>Class</th>
						<th>Category</th>
                        </tr>
                  </thead>
                <tbody></tbody>
             <tfoot></tfoot>
                </table>
				 <div class="overlay" id="loadover" style="display:none;">
            <p>Loading Please Wait....</p>
        </div></div>
       
    </form>
       <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.3.3/jspdf.min.js"></script>
    <script type="text/javascript" src="https://html2canvas.hertzen.com/dist/html2canvas.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    
    <script type="text/javascript">
        $('#btnback').on('click', function () {
            window.location.href = "SKU_Analysis.aspx";
        });
        var salesnl = [];
          $(document).ready(function () {
              $('#loadover').show();
              FillList();

          });
        function FillList() {
          
                $.ajax({
                    type: "Post",
                    contentType: "application/json; charset=utf-8",
                    url: "view_countofrepeatshop.aspx/getdatanshp",
                    dataType: "json",
                    success: function (data) {
					 salesnl = JSON.parse(data.d);
						$('#loadover').hide();
                        $('#grid').show();
                        $('#grid tbody').html('');
                        var str = '';

                        for (var i = 0; i < salesnl.length; i++) {

                            str += '<tr><td>' + (i + 1) + '</td><td>' + salesnl[i].ListedDr_Name + '</td><td>' + salesnl[i].sf_name + '</td><td>' + salesnl[i].Territory_Name + '</td><td>' + salesnl[i].Doc_Class_ShortName + '</td><td>' + salesnl[i].Doc_Spec_ShortName + '</td></tr>';
                        }

                        $('#grid tbody').append(str);

                    }
                })
            
        
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

                pdf.save("Repeatedbilledoutlet_details.pdf");
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
            var tets = 'Repeatedbilledoutlet_details' + '.xls';   //create fname

            link.download = tets;
            link.href = uri + base64(format(template, ctx));
            link.click();
        });
        </script>
    </body>


     
</asp:Content>
