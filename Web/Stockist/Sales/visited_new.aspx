<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Master.master" CodeFile="visited_new.aspx.cs" Inherits="Stockist_Sales_visited_new" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #grid {
            border: 1px solid #ddd;
            border-collapse: collapse;
            width: 80%;
        }

        th {
            position: sticky;
            top: 0;
            background: #177a9e;
            text-align: center;
            font-weight: normal;
            font-size: 15px;
            color: white;
        }

      table td, table th {
            padding: 5px;
            border: 1px solid #ddd;
             text-align: center;
        }
        </style>
    <form id="form1" runat="server">
        <div class="row">
            <div class="col-lg-12 sub-header">
               Visited Details
                 <span style="float: right; margin-right: 15px;">
                     <button type="button" class="btn" id="btnview" style="background-color:#1a73e8;color:white;">GO</button>
                 </span>
                <span style="float: right; margin-right: 15px;">
                    <div id="reportrange" class="form-control mt-5 mb-5" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc;">
                        <i class="fa fa-calendar"></i>&nbsp;
                            <span id="ordDate" runat="server"></span><i class="fa fa-caret-down"></i>
                    </div>
                </span>
                <span style="float: right; margin-right: 15px;">
                    <div>
                        <select class="form-control" clientidmode="Static" id="idpro" runat="server">
                        </select>
                    </div>
                </span>

            </div>
        </div><br /><br />
         <asp:HiddenField ID="fdt" runat="server" Value="" />
        <asp:HiddenField ID="tdt" runat="server" Value="" />
        <asp:HiddenField ID="prod" runat="server" Value="" />
        <asp:HiddenField ID="dt" runat="server" Value="" />
         <img onclick="exportToExcel()"
                     style="cursor: pointer;float:right;" src="../../img/Excel-icon.png"
                     alt="" width="40" height="40" id="btnExport" />
            
                <img onclick="exportToprint()"
                     style="cursor: pointer;float:right;" src="../../img/print.png"
                     alt="" width="40" height="40" id="btnprint" />
        
                <img onclick="getPDF()"
                     style="cursor: pointer;float:right;" src="../../img/pdf.png"
                     alt="" width="40" height="40" id="btnpdf" />
         <div class="card">
            <div class="card-body table-responsive">
             <center><br /><br />
        <div id="expex" class="canvas_div_pdf">
        <table class="auto-index" id="grid">
                <thead>
                </thead>
                <tbody></tbody>
               
            </table>
             </div>
                     </center>
        </div>
              </div>
    </form>
      <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        var sales = [];
        var salesc = [];
        var salesshp = [];
        var fdt = '';
        var tdt = '';
        var prod = '';
        var flit = [];
        var cls = [];
        var saledate = [];
        $(function () {

            var start = moment();
            var end = moment();

       function cb(start, end) {
                $('#reportrange span').html(start.format('DD-MM-YYYY') + ' - ' + end.format('DD-MM-YYYY'));
                $('#<%=dt.ClientID%>').val(start.format('DD-MM-YYYY') + ' - ' + end.format('DD-MM-YYYY'));
            }

            $('#reportrange').daterangepicker({
                startDate: start,
                endDate: end,
                ranges: {
                    'Today': [moment(), moment()],
                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
                }
            }, cb);

            cb(start, end);

        });
        $(document).ready(function () {
            clasdtl();
            
           });
        $('#btnview').on('click', function () {
           
            $('#grid table').html('');
           // $('#grid ').html('');
           
            id = $('#<%=dt.ClientID%>').val();
            id = id.split('-');
            fdt = id[2].trim() + '-' + id[1] + '-' + id[0] + ' 00:00:00';
            tdt = id[5] + '-' + id[4] + '-' + id[3].trim() + ' 00:00:00';
            prod = $('#<%=idpro.ClientID%>').val();
            userdates();
            shopdtl();
          
            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "Visited_new.aspx/getdatauser",
                data: "{'fdate':'" + fdt + "','tdate':'" + tdt + "','sf':'" + prod + "'}",
                dataType: "json",
                success: function (data) {
                    sales = JSON.parse(data.d);
                    $('#grid thead').html('');
                    $('#grid tbody').html('');
                    str = '';
                    strh = '';
                    strc = '';
                    strcr = '';
                    strh ="<tr><th>S.NO</th><th>Day</th><th>Visited Area</th><th>Worked with</th><th>No of Shop Visited</th><th>Shop Name</th><th>Remarks</th>";
                    for (var j = 0; j < salesc.length; j++) {
                        strh += "<th>" + salesc[j].Doc_ClsName + "</th>";
                    }
                    strhs = "</tr>";
                    for (var i = 0; i < saledate.length; i++) {
                        let mymap = new Map();
                        let uniquerts = [];
                        var flit = salesshp.filter(function (a) {
                            return a.Activity_Date == saledate[i].AllDates;
                        });
                        var shpname = flit.map(function (a) { return a.ListedDr_Name }).join(',');
                       
                            var flitdt = sales.filter(function (a) {
                                return a.Pln_Date == saledate[i].AllDates;
                            });
                        //var varea = flitdt.map(function (a) { return a.ClstrName }).join(',');
                        var worked = flitdt.map(function (a) { return a.worked_with_name }).join(',');
                        var remrks = flitdt.map(function (a) { return a.remarks }).join(',');
                        var varea = flitdt.filter(function (el) {
                            const val = mymap.get(el.ClstrName);
                            if (val) {                                
                                    return false;                                
                            }
                            mymap.set(el.ClstrName, el.cluster);
                            return true;
                        }).map(function (a) { return a.ClstrName }).join(',');
                        console.log(uniquerts);
                        str += "<tr><td>" + (i + 1) + "</td><td>" + saledate[i].AllDates + "</td><td>" + varea + "</td><td>" + worked + "</td><td>" + flit.length + "</td><td>" + shpname + "</td><td>" + remrks + "</td>";
                        
                            for (var m = 0; m < salesc.length; m++) {
                            var cls = flit.filter(function (a) {
                                return a.Doc_ClsCode == salesc[m].Doc_ClsCode;
                            });
                            
                                str += "<td>" + cls.length + "</td>";
                         }
                      
                        strcr = "</tr>";
                    }
                   
                    $('#grid tbody').append(str);
                    $('#grid tbody').append(strcr);
                    $('#grid thead').append(strh);
                    $('#grid thead').append(strhs);
                }
            });

        });
        function clasdtl() {
            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "Visited_new.aspx/getclasdtl",
                dataType: "json",
                success: function (data) {
                    salesc = JSON.parse(data.d);

                }
            });
        }
        function shopdtl() {
         
            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "Visited_new.aspx/getshpdtl",
                data: "{'fdate':'" + fdt + "','tdate':'" + tdt + "','sf':'" + prod + "'}",
                dataType: "json",
                success: function (data) {
                    salesshp = JSON.parse(data.d);

                }
            });
        }
        function userdates() {

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "Visited_new.aspx/getuserdates",
                data: "{'fdate':'" + fdt + "','tdate':'" + tdt + "','sf':'" + prod + "'}",
                dataType: "json",
                success: function (data) {
                    saledate = JSON.parse(data.d);

                }
            });
        }
        function exportToExcel() {
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

            htmls = document.getElementById("expex").innerHTML;

            var ctx = {
                worksheet: 'Worksheet',
                table: htmls
            }


            var link = document.createElement("a");


            var tets = 'Misreports' + '.xls';   //create fname

            link.download = tets;
            link.href = uri + base64(format(template, ctx));
            link.click();
        }
        function exportToprint() {
            var divToPrint = document.getElementById("expex");
            newWin = window.open("");
            newWin.document.write(divToPrint.outerHTML);
            newWin.print();
            newWin.close();
        }
        function getPDF() {

            var HTML_Width = $(".canvas_div_pdf").width();
            var HTML_Height = $(".canvas_div_pdf").height();
            var top_left_margin = 15;
            var PDF_Width = HTML_Width + (top_left_margin * 2);
            var PDF_Height = (PDF_Width * 1.5) + (top_left_margin * 2);
            var canvas_image_width = HTML_Width;
            var canvas_image_height = HTML_Height;

            var totalPDFPages = Math.ceil(HTML_Height / PDF_Height) - 1;


            html2canvas($(".canvas_div_pdf")[0], { allowTaint: true }).then(function (canvas) {
                canvas.getContext('2d');

                console.log(canvas.height + "  " + canvas.width);


                var imgData = canvas.toDataURL("image/jpeg", 1.0);
                var pdf = new jsPDF('p', 'pt', [PDF_Width, PDF_Height]);
                pdf.addImage(imgData, 'JPG', top_left_margin, top_left_margin, canvas_image_width, canvas_image_height);


                for (var i = 1; i <= totalPDFPages; i++) {
                    pdf.addPage(PDF_Width, PDF_Height);
                    pdf.addImage(imgData, 'JPG', top_left_margin, -(PDF_Height * i) + (top_left_margin * 4), canvas_image_width, canvas_image_height);
                }

                pdf.save("Misreports.pdf");
            });
        };
        </script>
</asp:Content>