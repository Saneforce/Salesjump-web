<%@ Page Title="" Language="C#" MasterPageFile="~/Master_DIS.master" AutoEventWireup="true" CodeFile="Rpt_Sales_By_Salesman.aspx.cs" Inherits="Stockist_Sales_Rpt_Sales_By_Salesman" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <html>
    <title></title>
    <head>
        <body>
            <form id="form1" runat="server">
                <div class="row">
                    <div class="col-md-offset-4 col-md-4 sub-header">
                        Sales by SalesMan
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
                        <table class="table table-hover" id="sales_man_table">
                            <thead class="text-warning">
                                <tr>
                                    <th style="text-align: left">Name</th>
                                    <th style="text-align: left">Inv Count</th>
                                    <th style="text-align: right">Inv Sales</th>
                                    <th style="text-align: right">Inv Sales With Tax</th>
                                    <th style="text-align: right">CN Count</th>
                                    <th style="text-align: right">CN Sales</th>
                                    <th style="text-align: right">CN Sales With Tax</th>
                                    <th style="text-align: right">Total Sales</th>
                                    <th style="text-align: right">Total Sales With Tax</th>
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

            $("#reportrange").on("DOMSubtreeModified", function () {
                id = $('#ordDate').text();
                id = id.split('-');
                fdt = id[2].trim() + '-' + id[1] + '-' + id[0] + ' 00:00:00';
                tdt = id[5] + '-' + id[4] + '-' + id[3].trim() + ' 00:00:00';
                loadData();
            });

            function ReloadTable() {

                var inv_count = 0; var cn_count = 0; var inv_sales = 0; var inv_sales_with_tax = 0; var cn_sales = 0; var cn_sales_with_tax = 0; var tot = 0; var tot1 = 0; var total_sales = 0; var total_sales_with_tax = 0;
                $("#sales_man_table TBODY").html("");
                for ($i = 0; Orders.length > $i; $i++) {
                    if ($i < Orders.length) {

                        total_sales = Orders[$i].Sub_Total - Orders[$i].credit_amt;
                        total_sales_with_tax = Orders[$i].Total - Orders[$i].credit_amt;

                        tr = $("<tr></tr>");
                        $(tr).html("<td style='display:none';>" + Orders[$i].Dis_Code + "</td><td>" + Orders[$i].Dis_Name + "</td><td><a id='clc' href='Rpt_Sales_By_Salesman_Details.aspx?Stk_Code=" + Orders[$i].Dis_Code + "&FDate=" + fdt + "&TDate=" + tdt + "&Stk_Name=" + Orders[$i].Dis_Name + "'>" + Orders[$i].inv_count + "</td><td align=\"right\">" + Orders[$i].Sub_Total.toFixed(2) + "</td><td align='right'>" + Orders[$i].Total.toFixed(2) + "</td><td align='right'>" + Orders[$i].credit_count + "</td><td align='right'>" + Orders[$i].credit_amt.toFixed(2) + "</td><td align='right'>" + Orders[$i].credit_amt.toFixed(2) + "</td><td align='right'>" + total_sales.toFixed(2) + "</td><td align='right'>" + total_sales_with_tax.toFixed(2) + "</td>");

                        inv_count = parseFloat(Orders[$i].inv_count || 0) + parseFloat(inv_count);
                        cn_count = parseFloat(Orders[$i].credit_count || 0) + parseFloat(cn_count);

                        inv_sales = parseFloat(Orders[$i].Sub_Total || 0) + parseFloat(inv_sales)
                        inv_sales_with_tax = parseFloat(Orders[$i].Total) + parseFloat(inv_sales_with_tax);

                        cn_sales = parseFloat(Orders[$i].credit_amt || 0) + parseFloat(cn_sales)
                        cn_sales_with_tax = parseFloat(Orders[$i].credit_amt) + parseFloat(cn_sales_with_tax);

                        tot = parseFloat(total_sales || 0) + parseFloat(tot)
                        tot1 = parseFloat(total_sales_with_tax) + parseFloat(tot1);

                        $("#sales_man_table TBODY").append(tr);
                    }
                }
                if (Orders.length > 0) {

                    $("#sales_man_table TFOOT").html("<tr><td style='font-weight: bold;'>Total</td><td>" + inv_count + "</td><td style='text-align: right;font-weight: bold;'>" + inv_sales.toFixed(2) + "</td><td style='text-align: right;font-weight: bold;'>" + inv_sales_with_tax.toFixed(2) + "</td><td style='text-align: right;font-weight: bold;'>" + cn_count + "</td><td style='text-align: right; font-weight: bold;'>" + cn_sales.toFixed(2) + "</td><td style='text-align: right;font-weight: bold;'>" + cn_sales_with_tax.toFixed(2) + "</td><td style='text-align: right;font-weight: bold;'>" + tot.toFixed(2) + "</td><td style='text-align: right;font-weight: bold;'>" + tot1.toFixed(2) + "</td></tr>");
                }
                else {
                    $("#sales_man_table TFOOT").html("<tr><td colspan='9'>There were no sales during the selected date range.</td></tr>");
                }
            }

            function loadData() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Rpt_Sales_By_Salesman.aspx/Get_salebysalesman_Count",
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

                var start = moment(); var end = moment();

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

