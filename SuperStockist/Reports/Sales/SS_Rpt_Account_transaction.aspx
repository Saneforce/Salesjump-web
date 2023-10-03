<%@ Page Title="" Language="C#" MasterPageFile="~/Master_SS.master" AutoEventWireup="true" CodeFile="SS_Rpt_Account_transaction.aspx.cs" Inherits="SuperStockist_Reports_Sales_SS_Rpt_Account_transaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <html>
    <head>
        <title></title>
        <style>
            input[type='text'], select {
                border: 1.5px solid #19a4c6a3 !important;
                background: aliceblue;
            }

            .table > thead > tr > th {
                vertical-align: bottom;
                border-bottom: 2px solid #19a4c6a3;
            }

            .table > tfoot > tr > td {
                vertical-align: bottom;
                border-top: 1px solid #19a4c6a3;
            }


            .table-hover > tbody > tr:hover > td, .table-hover > tbody > tr:hover > th {
                background-color: aliceblue !important;
            }

            .txtblue {
                border: 1.5px solid #19a4c6a3 !important;
                background-color: aliceblue !important;
                /*border: 1px solid #19a4c6!important;*/
            }

            .chosen-container-single .chosen-single {
                border: 1.5px solid #19a4c6a3 !important;
                height: 30px;
                background: aliceblue !important;
            }

            .card {
                border: 1.5px solid #b2ebf9b5 !important;
            }

            .select2-container--default .select2-selection--single {
                border: 1.5px solid #19a4c6a3 !important;
                background: aliceblue !important;
            }

            .ms-options-wrap > button:focus, .ms-options-wrap > button {
                border: 1.5px solid #19a4c6a3 !important;
            }

            .stockClass {
                font-size: 12px;
                font-weight: bolder;
            }

            .btn-group > .btn:first-child {
                border: 1.5px solid #19a4c6a3 !important;
                background: aliceblue !important;
            }

            .modal {
                background-color: transparent !important;
            }

            .btn .category .active {
                background-color: #00bcd4 !important;
                color: white !important;
            }
        </style>
    </head>
    <body>
        <form id="form1" runat="server">
            <div class="row">
                <div class="col-md-offset-4 col-md-4 sub-header" style="text-align: center">
                    Account Transaction Details
                </div>
                <div class="col-md-4 sub-header">
                    <span style="float: right"></span><span style="float: right; margin-right: 50px;">
                        <div id="reportrange" class="form-control mt-5 mb-5" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc;">
                            <i class="fa fa-calendar"></i>&nbsp;<span id="ordDate"></span><i class="fa fa-caret-down"></i>
                        </div>
                    </span>
                    <span style="float: right; margin-top: -5px;">
                        <div>
                            <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                                Style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 15px;" OnClick="ImageButton1_Click1" />
                        </div>
                    </span>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4 col-md-offset-4" style="text-align: center">
                    <h5 id="date_details"></h5>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                    <div class="form-group">
                        <label class="control-label" for="focusedInput">
                            Customer Name</label>
                        <select class="form-control" id="cust_select" style="width: 100%;">
                            <option value="0">All</option>
                        </select>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-body table-responsive">
                    <table class="table table-hover" id="ItemList">
                        <thead class="text-warning">
                            <tr>
                                <th id="dat1" style="text-align: left">Date</th>
                                <th id="Name" style="text-align: left">Name</th>
                                <th id="Type" style="text-align: left">Type</th>
                                <th id="No" style="text-align: left">Trans No</th>
                                <th id="Reference" style="text-align: left">Reference</th>
                                <th style="text-align: left">Debit</th>
                                <th style="text-align: left">Credit</th>
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
        var sortid = '';
        var asc = true;
        var AllOrders = []; var Orders = []; var Invoice_details = [];

        // loadData();

        $("#reportrange").on("DOMSubtreeModified", function () {
            id = $('#ordDate').text();
            id = id.split('-');
            fdt = id[2].trim() + '-' + id[1] + '-' + id[0] + ' 00:00:00';
            tdt = id[5] + '-' + id[4] + '-' + id[3].trim() + ' 00:00:00';
            loadData();
        });

        $('th').on('click', function () {
            sortid = this.id;
            asc = $(this).attr('asc');
            if (asc == undefined) asc = 'true';
            Orders.sort(function (a, b) {
                if (a[sortid].toLowerCase() < b[sortid].toLowerCase() && asc == 'true')
                    return -1;
                if (a[sortid].toLowerCase() > b[sortid].toLowerCase() && asc == 'true')
                    return 1;
                if (b[sortid].toLowerCase() < a[sortid].toLowerCase() && asc == 'false')
                    return -1;
                if (b[sortid].toLowerCase() > a[sortid].toLowerCase() && asc == 'false')
                    return 1;
                return 0;
            });

            $(this).attr('asc', ((asc == 'true') ? 'false' : 'true'));
            ReloadTable(Orders);
        });

        function ReloadTable(Orders) {

            $("#ItemList TBODY").html("");

            if ($('#cust_select').val() != "0") {

                Orders = Orders.filter(function (d) {
                    return (d.code == $('#cust_select').val());
                });
            }
			else{
			Orders = Orders;
			}
            var ctot = 0; var dtot = 0;
            for ($i = 0; Orders.length > $i; $i++) {

                if ($i < Orders.length) {
                    tr = $("<tr></tr>");
                    $(tr).html("<td>" + Orders[$i].dat + "</td><td>" + Orders[$i].Name + "</td><td>" + Orders[$i].Type + "</td><td>" + Orders[$i].No + "</td><td>" + Orders[$i].Reference + "</td><td>" + parseFloat(Orders[$i].Debit).toFixed(2) + "</td><td>" + parseFloat(Orders[$i].Credit).toFixed(2) + "</td>");
                    dtot += parseFloat(Orders[$i].Debit);
                    ctot += parseFloat(Orders[$i].Credit);
                    $("#ItemList TBODY").append(tr);
                }
            }

            if (Orders.length > 0) {
                var str = "<tr><td colspan='5' style='text-align:right;padding-right:28px;'><b>Total</b></td><td style='text-align:left;'>" + dtot.toFixed(2) + "</td><td style='text-align:left;'>" + ctot.toFixed(2) + "</td></tr>";
                $("#ItemList TFOOT").html(str);
            }
            else {
                $("#ItemList TFOOT").html("<tr><td colspan='6'>There were no Account Transaction during the selected date range.</td></tr>");
            }
        }

        function loadData() {

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "SS_Rpt_Account_transaction.aspx/Get_Acc_trans_details",
                data: "{'FDT':'" + fdt + "','TDT':'" + tdt + "'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders; ReloadTable(Orders);
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "SS_Rpt_Account_transaction.aspx/bindretailer",
                data: "{'stk':'<%=Session["Sf_Code"]%>','Div':'<%=Session["Division_Code"]%>'}",
                dataType: "json",
                success: function (data) {
                    itms = JSON.parse(data.d) || [];
                    for (var i = 0; i < itms.length; i++) {
                        $('#cust_select').append($("<option></option>").val(itms[i].ListedDrCode).html(itms[i].ListedDr_Name1)).trigger('chosen:updated').css("width", "100%");
                    }
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
            $('#cust_select').chosen();
        }


        //function filter_trans_cust(code) {
        //    var filter_cust = Orders.filter(function (u) {
        //        return (code == u.code);
        //    });
        //     ReloadTable(filter_cust);
        //}

        $('#cust_select').change(function () {

            var selected_cust = $(this).val();
            ReloadTable(Orders);

        });


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
    </html>
</asp:Content>

