<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Rpt_TPMyDayPlan.aspx.cs" Inherits="MasterFiles_Rpt_TPMyDayPlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../js/lib/xls.core.min.js" type="text/javascript"></script>
    <script src="../js/lib/xlsx.core.min.js" type="text/javascript"></script>
    <script src="../js/lib/import_data.js" type="text/javascript"></script>
    <script src="../js/jquery.table2excel.js"></script>
    <form id="form1" runat="server">
        <div class="overlay" id="loadover" style="display: none;">
            <div id="loader"></div>
        </div>
        <div class="row">
            <div class="col-lg-12 sub-header">
                My Day Plan<span style="float: right; margin-right: 15px;">
                    <div id="reportrange" class="form-control mt-5 mb-5" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc;">
                        <i class="fa fa-calendar"></i>&nbsp;
               
                        <span id="ordDate"></span><i class="fa fa-caret-down"></i>
                    </div>
                    <img style="cursor: pointer; width: 40px; height: 40px; float: right;" alt="" onclick="exportToExcel()" src="../img/Excel-icon.png" /></span>
            </div>
        </div>
        <div class="card">
            <div class="card-body table-responsive" style="overflow: auto">
                <table class="table table-hover" id="Rpt_MyDay_Plan_Mnth">
                    <thead class="text-warning" style="white-space: nowrap;">
                        <tr>
                            <th style="text-align: left">Sl NO</th>
                            <th style="text-align: left">Activity Date</th>
                            <th style="text-align: left">Employee ID</th>
                            <th style="text-align: left">Employee Name</th>
                            <th style="text-align: left">Reporting Manager</th>
                            <th style="text-align: left">Mobile No.</th>
                            <th style="text-align: left">Start Time</th>
                            <th style="text-align: left">Work Type</th>
                            <th style="text-align: left">Route Name</th>
                            <th style="text-align: left">Remarks</th>
                            <th style="text-align: left">Lat</th>
                            <th style="text-align: left">Long</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
    </form>
    <style>
        #loader {
            position: absolute;
            left: 50%;
            top: 50%;
            z-index: 1;
            width: 120px;
            height: 120px;
            margin: -76px 0 0 -76px;
            border: 16px solid #f3f3f3;
            border-radius: 50%;
            border-top: 16px solid #3498db;
            -webkit-animation: spin 2s linear infinite;
            animation: spin 2s linear infinite;
        }

        .overlay {
            background-color: #EFEFEF;
            position: fixed;
            width: 100%;
            height: 100%;
            z-index: 1000;
            top: 0px;
            left: 0px;
            opacity: .5; /* in FireFox */
            filter: alpha(opacity=50); /* in IE */
        }
    </style>
    <script language="javascript" type="text/javascript">
        var AllOrders = []; var Orders = []; var Invoice_details = []; var FDT = '';
        var TDT = '';
        $(document).ready(function () {
            $("#reportrange").on("DOMSubtreeModified", function () {
                id = $('#ordDate').text();
                id = id.split('-');
                FDT = (id[2]).trim() + '-' + id[1] + '-' + id[0] + ' 00:00:00';
                TDT = id[5] + '-' + id[4] + '-' + (id[3]).trim() + ' 00:00:00';
                $('#loadover').show();
                setTimeout(function () {
                    setTimeout(loadData(), 500);
                }, 500);
            });
        });
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
            htmls = document.getElementById("Rpt_MyDay_Plan_Mnth").innerHTML;

            var ctx = {
                worksheet: 'Worksheet',
                table: htmls
            }
            var link = document.createElement("a");
            var tets = 'Rpt_MyDay_Plan_Mnth' + '.xls';   //create fname

            link.download = tets;
            link.href = uri + base64(format(template, ctx));
            link.click();
        }
        function loadData() {

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Rpt_TPMyDayPlan.aspx/GetRptMyDayPlanMnth",
                data: "{'FDT':'" + FDT + "','TDT':'" + TDT + "'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders; ReloadTable();
                },
                error: function (result) {
                }
            });
        }
        function ReloadTable() {
            $("#Rpt_MyDay_Plan_Mnth TBODY").html("");
            slno = 0;
            for ($i = 0; $i < Orders.length; $i++) {

                if ($i < Orders.length) {
                    tr = $("<tr></tr>");
                    slno = $i + 1;
                    // $(tr).html("<td>" + slno + "</td><td>" + Orders[$i].Trans_Inv_Slno + "</td><td>" + Orders[$i].Cus_Name + "</td><td>" + Orders[$i].Invoice_Date + "</td><td>" + Orders[$i].Pay_Due + "</td><td>" + Orders[$i].Status + "</td><td style='text-align: right'>" + parseFloat(Orders[$i].Total).toFixed(2) + "</td><td style='text-align: right'><a href='Invoice_Order_View.aspx?Order_No=" + Orders[$i].Trans_Inv_Slno + "&Stockist_Code=" + stockist_Code + "&Div_Code=" + Div_Code + "&Status=" + Orders[$i].Status + "'><span class='glyphicon glyphicon-eye-open'></span></a></td>");
                    //$(tr).html("<td>" + slno + "</td><td>" + Orders[$i].Trans_Inv_Slno + "</td><td>" + Orders[$i].Cus_Name + "</td><td>" + Orders[$i].Invoice_Date + "</td><td>" + Orders[$i].Pay_Due + "</td><td>" + Orders[$i].Status + "</td><td style='text-align: right'>" + parseFloat(Orders[$i].Total).toFixed(2) + "</td><td style='text-align: right'><a id='myButton' href='#' onClick='popup(\"" + Orders[$i].Trans_Inv_Slno + "\",\"" + stockist_Code + "\",\"" + Div_Code + "\",\"" + Orders[$i].Status + "\",\"" + FDT + "\",\"" + TDT + "\")'><span class='glyphicon glyphicon-eye-open'></span></a></td>");
                    var date = Orders[$i].Plan_Date.split('T')
                    var dt = date[0];
                    $(tr).html("<td >" + slno + "</td><td >" + dt + "</td><td >" + Orders[$i].sf_emp_id + "</td><td >" + Orders[$i].SF_Name + "</td><td >" + Orders[$i].Reporting_SF_Name + "</td><td >" + Orders[$i].SF_Mobile + "</td><td >" + Orders[$i].Plan_Time + "</td><td >" + Orders[$i].Worktype + "</td><td >" + Orders[$i].Route + "</td><td >" + Orders[$i].Remarks + "</td><td >" + Orders[$i].Start_Lat + "</td><td >" + Orders[$i].Start_Long + "</td>");
                    $("#Rpt_MyDay_Plan_Mnth TBODY").append(tr);
                }
            }
            //$("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")
            //loadPgNos();
            $('#loadover').hide();
        }
        $(function () {

            var start = moment();
            var end = moment();

            function cb(start, end) {
                $('#reportrange span').html(start.format('DD-MM-YYYY') + ' - ' + end.format('DD-MM-YYYY'));
                //$('#date_details').text(' From ' + start.format('DD/MM/YYYY') + ' To ' + end.format('DD/MM/YYYY'));

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
    </script>
</asp:Content>

