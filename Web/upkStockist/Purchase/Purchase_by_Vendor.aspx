<%@ Page Title="" Language="C#" MasterPageFile="~/Master_DIS.master" AutoEventWireup="true" CodeFile="Purchase_by_Vendor.aspx.cs" Inherits="Stockist_Purchase_Purchase_by_Vendor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <html>
    <title></title>
    <head>
        <body>
            <form id="form1" runat="server">

                <div class="row">
                    <div class="col-md-offset-4 col-md-4 sub-header">
                        Purchase By Vendor
                    </div>
                    <div class="col-md-4 sub-header">
                        <span style="float: right"></span><span style="float: right; margin-right: 15px;">
                            <div id="reportrange" class="form-control mt-5 mb-5" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc;">
                                <i class="fa fa-calendar"></i>&nbsp;<span id="ordDate"></span><i class="fa fa-caret-down"></i>
                            </div>
                        </span>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-4 col-md-offset-4">
                        <h5 style="padding: 0px 0px 0px 23px;" id="date_details"></h5>
                    </div>
                </div>

                <div class="card">
                    <div class="card-body table-responsive">
                        <table class="table table-hover" id="ItemList">
                            <thead class="text-warning">
                                <tr>
                                    <th style="text-align: left">Vendor Name</th>
                                    <th style="text-align: left">Purchase Order Count</th>
                                    <th style="text-align: right">Amount</th>                                    
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                            <tfoot>
                            </tfoot>
                        </table>
                    </div>
                </div>

            </form>
        </body>

        <script type="text/javascript">

            var AllOrders = []; var Orders = []; var Invoice_details = [];
            // loadData();
            $("#reportrange").on("DOMSubtreeModified", function () {
                id = $('#ordDate').text();
                id = id.split('-');
                fdt = id[2].trim() + '-' + id[1] + '-' + id[0] + ' 00:00:00';
                tdt = id[5] + '-' + id[4] + '-' + id[3].trim() + ' 00:00:00';
                loadData();
            });

            function ReloadTable() {
                var cnt = 0;
                var amtt = 0;
                $("#ItemList TBODY").html("");

                for ($i = 0; Orders.length > $i; $i++) {
                    if ($i < Orders.length) {
                        tr = $("<tr></tr>");
                        $(tr).html("<td style='display:none';>" + Orders[$i].Sup_No + "</td><td>" + Orders[$i].Sup_Name + "</td><td><a id='clc' href='Purchase_by_Vendor_details.aspx?Vendor_Name=" + Orders[$i].Sup_Name + "&FDate=" + fdt + "&TDate=" + tdt + "&Vendor_Id=" + Orders[$i].Sup_No + "'>" + Orders[$i].Order_count + "</td><td align=\"right\">" + Orders[$i].Total_value.toFixed(2) + "</td>");

                        amtt = parseFloat(Orders[$i].Total_value) + parseFloat(amtt);
                        cnt=parseFloat(Orders[$i].Order_count) + parseFloat(cnt);
                        $("#ItemList TBODY").append(tr);
                    }
                }
                if (Orders.length > 0) {

                    $("#ItemList TFOOT").html("<tr><td style='font-weight: bold;'>Total</td><td style='font-weight: bold;'>" + cnt + "</td><td style='text-align: right;font-weight: bold;'>" + amtt.toFixed(2) + "</td><td></td></tr>");
                }
                else {
                    $("#ItemList TFOOT").html("<h5>There were no Purchase during the selected date range.</h5>");
                }
            }

            function loadData() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Purchase_by_Vendor.aspx/Get_Purchase_vendor_count",
                    data: "{'FDT':'" + fdt + "','TDT':'" + tdt + "'}",
                    dataType: "json",
                    success: function (data) {
                        AllOrders = JSON.parse(data.d) || [];
                        Orders = AllOrders; ReloadTable();

                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            }

        </script>

        <script type="text/javascript">
            $(function () {

                var start = moment();
                var end = moment();

                function cb(start, end) {
                    $('#reportrange span').html(start.format('DD-MM-YYYY') + ' - ' + end.format('DD-MM-YYYY'));
                    $('#date_details').text(' From ' + start.format('DD/MM/YYYY') + ' To ' + end.format('DD/MM/YYYY'));

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

    </head>
    </html>

</asp:Content>

