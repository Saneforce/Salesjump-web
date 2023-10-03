<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="view_subdiv_wtype.aspx.cs" Inherits="MIS_Reports_view_subdiv_wtype" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!DOCTYPE html>
 <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
 <title></title>
 <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
       <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.3.3/jspdf.min.js"></script>
    <script type="text/javascript" src="https://html2canvas.hertzen.com/dist/html2canvas.js"></script>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

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
        <div class="card">
       <div class="card-body table-responsive" style="overflow-x: auto;">
            <br />
        <asp:Label ID="lblHead"  SkinID="lblMand" Font-Bold="true" style="font-size:large"
            Font-Underline="true" runat="server" />
        <br /> 

        <div style="text-align: left; padding: 2px 50px;">
                    <b>
                        <asp:Label ID="lblsf_name" runat="server"></asp:Label>
                       </b>
                </div>
         <img  style="cursor: pointer;float:right;" src="/img/pdf.png" alt="" width="40" height="40" id="btnpdf">
            <img style="cursor: pointer;float:right;" src="/img/excel.png" alt="" width="40" height="40" id="btnExport">
        <table id="grid" class="grids">
                <thead>
                    <tr>
                        <th>S.No</th>
                        <th>Division</th>
                        <th>Fieldwork</th>
                        <th>Leave</th>
                        <th>Others</th>
                        <th>Not Login</th>
                      </tr>
                  </thead>
                <tbody></tbody>
             <tfoot></tfoot>
                </table></div></div>
       <div class="modal fade" id="leaveModal" style="z-index: 10000000; overflow-y: scroll;" tabindex="0" aria-hidden="true">
            <div class="modal-dialog" role="document" style="width: 98% !important">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="leaveModalLabel"></h5>
                        <button type="button" class="close" style="margin-top: -20px;" data-dismiss="modal" data-toggle="modal" data-target="#summaryModal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    </div>
                    <div class="card">
                        <img style="cursor: pointer;float:right;" src="/img/excel.png" alt="" width="40" height="40" id="btnExportmd">
                        <table id="leavedets" style="width: 100%; font-size: 12px;">
                                    <thead class="text-warning">
                                        <tr>
                                            <th style="text-align: left">SlNO</th>
											<th style="text-align: left">Employee Name</th>
                                            <th style="text-align: left">State</th>
                                            <th style="text-align: left">Reporting Manager</th>
                                            <th style="text-align: left">Route</th>
                                            <th style="text-align: left">Distributor</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                         </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal" data-toggle="modal" data-target="#summaryModal">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </form>
       <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
  
    
    <script type="text/javascript">
          var str = "";
          var arr = [];
        var prods = []; let type = '';
          $(document).ready(function () {
             FillList();
             
          });
        $(document).on("click", ".deptsurs", function () {
            let departmentName = $(this).attr("wtype");
            let subd = $(this).attr("sub");
             type = $(this).attr("name");
            $('#leaveModal').modal('toggle');
            $('#leavedets TBODY').html("<tr><td colspan=10>Loading please wait...</td></tr>");
            $("#leaveModalLabel").html(`${type} Users Details`)
          
                setTimeout(function () {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "view_subdiv_wtype.aspx/getdatalist",
                        data: "{'subdiv':'" + subd + "','wtyp':'" + departmentName + "'}",
                        dataType: "json",
                        success: function (data) {
                            let AllOrders = JSON.parse(data.d) || [];
                            $('#leavedets TBODY').html("");
                            var slno = 0;
                            for ($i = 0; $i < AllOrders.length; $i++) {
                                if (AllOrders.length > 0) {
                                    slno += 1;
                                    tr = $("<tr></tr>");
                                    $(tr).html("<td>" + slno + "</td><td>" + AllOrders[$i].Sf_Name + "</td><td>" + AllOrders[$i].StateName + "</td><td>" + AllOrders[$i].rsf + "</td><td>" + ((AllOrders[$i].ClstrName == null) ? "" : AllOrders[$i].ClstrName) + " </td><td>" + ((AllOrders[$i].dist_name == null) ? "" : AllOrders[$i].dist_name) + "</td>");
                                    $("#leavedets TBODY").append(tr);
                                }
                            }
                        },
                        error: function (resp) {
                            $('#leavedets TBODY').html("<tr><td colspan='4'>Something went wrong. Try again.</td></tr>");
                        }
                    });
                }, 500);
      
        })
        function FillList() {
           
            $.ajax({
                    type: "Post",
                    contentType: "application/json; charset=utf-8",
                    url: "view_subdiv_wtype.aspx/getdata",
                    dataType: "json",
                    success: function (data) {
                        $('#grid tbody').html('');
                        salesnl = JSON.parse(data.d);
                        var str = '';

                        for (var i = 0; i < salesnl.length; i++) {

                            str += '<tr><td>' + (i + 1) + '</td><td>' + salesnl[i].subdivision_name + '</td><td wtype=F name="Field Work"  sub=' + salesnl[i].subdivision_code + ' class="deptsurs">' + ((salesnl[i].F == null) ? "" : salesnl[i].F) + '</td><td wtype=L name="Leave" sub=' + salesnl[i].subdivision_code + ' class="deptsurs">' + ((salesnl[i].L == null) ? "" : salesnl[i].L) + '</td><td wtype=N name="Other Work" sub=' + salesnl[i].subdivision_code + ' class="deptsurs">' + ((salesnl[i].N == null) ? "" : salesnl[i].N) + '</td><td wtype=iNA name="Not Login" sub=' + salesnl[i].subdivision_code + ' class="deptsurs">' + ((salesnl[i].iNA == null) ? "" : salesnl[i].iNA) + '</td></tr>';
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

                pdf.save("worktypedetails.pdf");
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
            var tets = 'worktypedetails' + '.xls';   //create fname

            link.download = tets;
            link.href = uri + base64(format(template, ctx));
            link.click();
        });
        $('#btnExportmd').click(function () {

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
            htmls = document.getElementById("leavedets").innerHTML;


            var ctx = {
                worksheet: 'Worksheet',
                table: htmls
            }
            var link = document.createElement("a");
           
                var txt = type + '_Details';
           
            var tets = txt + '.xls';    //create fname

            link.download = tets;
            link.href = uri + base64(format(template, ctx));
            link.click();
        });
        </script>
    
    </body>

</html>
    </asp:Content>