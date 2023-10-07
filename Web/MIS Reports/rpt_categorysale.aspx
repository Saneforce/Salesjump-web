<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_categorysale.aspx.cs" MasterPageFile="~/Master.master" Inherits="MIS_Reports_rpt_categorysale" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!DOCTYPE html>

    <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
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
        text-align: center;
        font-weight: normal;
        font-size: 15px;
        color: white;
    }
            #grid td, table th {
                padding: 5px;
                border: 1px solid #ddd;
                text-align: center;
            }
        </style>
    <head>
        <meta charset="utf-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
	<link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
	</head>
	<body>
    <form runat="server">
        <div class="row">
            <div class="col-lg-12 sub-header">
              Depot Sales
            </div>
        </div>
               <div class="row" style="margin-top: 1rem;">
            <label class="col-md-2  col-md-offset-3  control-label">State</label>
            <div class="col-md-5 inputGroupContainer">
                <div class="input-group">
                    <select id="ddlstates" data-dropup-auto="false" data-size="5" ></select>
                </div>
            </div>
        </div>
       
        <div class="row" style="margin-top: 1rem;">
            <label class="col-md-2  col-md-offset-3  control-label">From Date</label>
            <div class="col-md-2 inputGroupContainer">
                <div class="input-group">
                    <input type="date" class="form-control" id="fdate" />
                </div>
            </div>
        </div>
        <div class="row" style="margin-top: 1rem;">
            <label class="col-md-2  col-md-offset-3  control-label">To Date</label>
            <div class="col-md-2 inputGroupContainer">
                <div class="input-group">
                    <input type="date" class="form-control" id="tdate" />
                </div>
            </div>
        </div>
        <div class="row" style="margin-top: 5px">
            <div class="col-md-6  col-md-offset-5">
                <button type="button" class="btn" id="btnview" style="background-color:#1a73e8;color:white;">View</button>
            </div>
        </div>
        <center>
        <div style="margin-top: 12px;">
             <img  style="cursor: pointer;float:right;" src="/img/pdf.png" alt="" width="40" height="40" id="btnpdf">
            <img style="cursor: pointer;float:right;" src="/img/excel.png" alt="" width="40" height="40" id="btnExport">
			  <div class="card">
        <div class="card-body table-responsive">
        <table id="grid" class="grids" style="display: none;">
                <thead>
                    <tr>
                        <th>S.No</th>
                        <th>Depot</th>
                        <th>TotalSales</th>
                        <th>Our Brand</th>
                         <th>%</th>
                    </tr>
                  </thead>
                <tbody></tbody>
             <tfoot></tfoot>
                </table>  </div>
    </div></div></center>
    </form>
         <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.3.3/jspdf.min.js"></script>
    <script type="text/javascript" src="https://html2canvas.hertzen.com/dist/html2canvas.js"></script>
    <script type="text/javascript">
        var AllDiv = [], AllState = [], AllFF = [];
        var Div = [], States = [], SFF = [];
        $(document).ready(function () {
            loadStates();
        });
        function loadStates() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_categorysale.aspx/getStates",
                data: '{"divcode":"<%=Session["div_code"]%>"}',
                dataType: "json",
                success: function (data) {
                    AllState = JSON.parse(data.d) || [];
                    States = AllState;
                    if (States.length > 0) {
                        var dept = $("#ddlstates");
                        dept.empty().append('<option selected="selected" value="">Select State</option>');
                        for (var i = 0; i < States.length; i++) {
                            dept.append($('<option value="' + States[i].scode + '">' + States[i].sname + '</option>'))
                        }
                    }
                }
            });
             $('#ddlstates').selectpicker({
                 liveSearch: true
             });
        }
        $('#btnview').click(function () {
            $('#grid').show();
            $('#grid tbody').html('');
            var stcodes = $('#ddlstates').val(); 
            var fromdt = $('#fdate').val();
            var todt = $('#tdate').val();
            var totalsal = 0;
            var oursal = 0;
            var percen = 0;
            var totalsals = 0;
            var oursals = 0;
            var per = 0;
            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "rpt_categorysale.aspx/dissale",
                data: "{'fromdate':'" + fromdt + "','todate':'" + todt + "','stcode':'" + stcodes + "'}",
                dataType: "json",
                success: function (data) {
                    sales = JSON.parse(data.d);
                    str = '';

                    for (var i = 0; i < sales.length; i++) {

                        totalsals = sales[i].Total_sales
                        oursals = sales[i].our_sales
                        percents = ((oursals / (Number(totalsals) == 0 ? 1 : totalsals)) * 100)
                        per = isNaN(parseFloat(percents)) ? 0 : parseFloat(percents).toFixed(2);;
                        str += "<tr><td>" + (i + 1) + "</td><td id=" + sales[i].Stockist_Code + "><a href='#' onclick=newreport('" + sales[i].Stockist_Code + "') >" + sales[i].Stockist_Name + "<a/></td><td>" + sales[i].Total_sales + "</td><td>" + sales[i].our_sales + "</td><td>" + per + "</td></tr>";
                        totalsal += sales[i].Total_sales
                        oursal += sales[i].our_sales
                    }
                    percent = ((oursal / (Number(totalsal) == 0 ? 1 : totalsal)) * 100)
                    percen = isNaN(parseFloat(percent)) ? 0 : parseFloat(percent).toFixed(2);;
                   
                    $('#grid tbody').append(str);
                    $('#grid tfoot').html("<tr><td colspan=2 style='text-align: center;font-weight: bold'>Total  Value</td><td><label>" + totalsal + "</label></td><td><label>" + oursal + "</label></td><td><label>" + percen + "</label></td></tr>");
                   
                }
            });
        });
        function newreport($x) {
            var fromdt = $('#fdate').val();
            var todt = $('#tdate').val();
            var url = 'rpt_categorysale_view.aspx?stcode=' + $x + '&fd=' + fromdt + '&td=' + todt;
            window.open(url, 'poprpExpense1', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=1000,height=600,left = 0,top = 0');
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

                pdf.save("depotsales.pdf");
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
            var tets = 'depotsales' + '.xls';   //create fname

            link.download = tets;
            link.href = uri + base64(format(template, ctx));
            link.click();
        });
      </script>
	</body>
	</html>
</asp:Content>
