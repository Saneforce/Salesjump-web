<%@ Page Title="" Language="C#" MasterPageFile="~/Master_DIS.master" AutoEventWireup="true" CodeFile="Rpt_Customer_Bal.aspx.cs" Inherits="Stockist_Sales_Rpt_Customer_Bal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title>Customer Balance</title>
        
        <%--<link href="../../datatable/DataTables-1.13.1/css/dataTables.dataTables.min.css" rel="stylesheet" />--%>
        <link href="../../datatable/DataTables-1.13.1/css/jquery.dataTables.min.css" rel="stylesheet" />
        <link href="../../datatable/DataTables-1.13.1/css/dataTables.bootstrap.min.css" rel="stylesheet" />
        <link href="../../datatable/Responsive-2.4.0/css/responsive.bootstrap.min.css" rel="stylesheet" />
        <link href="../../datatable/Responsive-2.4.0/css/responsive.dataTables.min.css" rel="stylesheet" />
        <link href="../../datatable/Responsive-2.4.0/css/responsive.jqueryui.min.css" rel="stylesheet" />
        <link href="../../datatable/SearchPanes-2.1.0/css/searchPanes.bootstrap.css" rel="stylesheet" />
        <link href="../../datatable/SearchPanes-2.1.0/css/searchPanes.dataTables.min.css" rel="stylesheet" />
        <link href="../../datatable/SearchPanes-2.1.0/css/searchPanes.jqueryui.min.css" rel="stylesheet" />
        <script src="../../datatable/DataTables-1.13.1/js/dataTables.bootstrap.min.js"></script>
        <script src="../../datatable/DataTables-1.13.1/js/dataTables.dataTables.js"></script>
        <script src="../../datatable/DataTables-1.13.1/js/jquery.dataTables.min.js"></script>
        <script src="../../datatable/JSZip-2.5.0/jszip.min.js"></script>
        <script src="../../datatable/pdfmake-0.1.36/pdfmake.min.js"></script>
        <script src="../../datatable/Responsive-2.4.0/js/dataTables.responsive.min.js"></script>
        <script src="../../datatable/Responsive-2.4.0/js/responsive.dataTables.min.js"></script>
        <script src="../../datatable/Responsive-2.4.0/js/responsive.jqueryui.min.js"></script>
        <script src="../../datatable/SearchPanes-2.1.0/js/dataTables.searchPanes.min.js"></script>
        <script src="../../datatable/SearchPanes-2.1.0/js/searchPanes.dataTables.min.js"></script>
        <script src="../../datatable/SearchPanes-2.1.0/js/searchPanes.jqueryui.min.js"></script>
        <style>
           .chosen-container {
                width: 320px !important;
            }

            input[type='text'], select {
                border: 1.5px solid #19a4c6a3 !important;
                background: aliceblue!important;
            }
            input[type='search'], select {
                border: 1.5px solid #19a4c6a3 !important;
                background: aliceblue!important;
            }

            .table > thead > tr > th {
                vertical-align: bottom;
                border-bottom: 2px solid #19a4c6a3;
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

            table.dataTable {
                border-collapse: collapse !important;
            }

                table.dataTable thead th, table.dataTable thead td {
                    border-bottom: 1px solid #19a4c6a3 !important;
                }

            .dataTables_wrapper .dataTables_paginate .paginate_button.current, .dataTables_wrapper .dataTables_paginate .paginate_button.current:hover {
                background-color: #428bca !important;
                border-color: #428bca !important;
                color: white !important;
            }

            .dataTables_wrapper .dataTables_paginate .paginate_button:hover {
                background-color: #428bca !important;
                border-color: #428bca !important;
                background: linear-gradient(to bottom, #428bca 0%, #428bca 100%);
                color: white !important;
                border: 1px solid #428bca !important;
            }

            div.dataTables_wrapper div.dataTables_filter input {
                margin-left: 0.5em;
                display: inline-block;
                width: 230px !important;
            }

            .dataTables_wrapper .dataTables_length, .dataTables_wrapper .dataTables_filter, .dataTables_wrapper .dataTables_info, .dataTables_wrapper .dataTables_processing, .dataTables_wrapper .dataTables_paginate {
                color: #428bca !important;
                font-weight: bold !important;
            }

            table.dataTable.table-hover > tbody > tr:hover > * {
                box-shadow: inset 0 0 0 9999px rgb(152 221 215 / 8%) !important;
            }

            .table-hover > tbody > tr:hover > td, .table-hover > tbody > tr:hover > th {
                background-color: aliceblue !important;
            }

            .card {
                border: 1.5px solid #b2ebf9b5 !important;
            }
        </style>
    </head>
    <body>
        <form id="form1" runat="server">
            <div class="row">
                <div class="col-md-offset-4 col-md-4 sub-header" style="text-align: center">
                    Retailer  Balance
                </div>
                <div class="col-md-4 sub-header">
                    <div id="Year_Div">
                        <label style="padding-left: 63px;">Financial Year</label><span style="float: right"></span><span style="float: right; margin-right: 25px;">
                            <select name="year_select" class="form-control year_select">
                            </select>
                        </span>
                    </div>
                    <%--      <div id="cal_div" style="display: none;">
                        <label style="padding-left: 63px;">Calendar Year</label><span style="float: right"></span><span style="float: right; margin-right: 25px;">
                            <select name="year_select" class="form-control calender_selected">
                            </select>
                        </span>
                    </div>--%>
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
                    <table class="table table-hover" id="Cust_bal_table">
                        <thead class="text-warning">
                            <tr>
                                <th style="text-align: left">Customer Name</th>
                                <th style="text-align: right">Opening Balance</th>
                                <th style="text-align: right">Total Bill Amount</th>
                                <th style="text-align: right">Collected Amount</th>
                                <th style="text-align: right">Balance Amount</th>
                                <%--  <th style="text-align: right">Advance Amount</th>--%>
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

        <script type="text/javascript">
            $(document).ready(function () {
                $('#Cust_bal_table').DataTable();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Rpt_Account_transaction.aspx/bindretailer",
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
            });
            var AllOrders = []; var Orders = []; var Invoice_details = []; var Year_Data = []; var splited_form_year = 0; var splited_to_year = 0; var From_month = ''; var To_month = '';
            var tdyDate = new Date();
            var thisyear = tdyDate.getFullYear();

            var get_access_master = localStorage.getItem("Access_Details");
            get_access_master = JSON.parse(get_access_master);
            var access_month = get_access_master[0].Year_value;
            var access_type = get_access_master[0].Year_setting;
            access_month = access_month.split('-');
            From_month = access_month[0];
            To_month = access_month[1];

            get_Years(From_month, To_month, access_type);


            if (access_type == '0') {
                var get_finanacial_year = $('.year_select').val();
                splited_form_year = get_finanacial_year;
                splited_to_year = get_finanacial_year;
                loadData(get_finanacial_year, get_finanacial_year, From_month, To_month, access_type);
            }
            else {

                var get_finanacial_year = $('.year_select').val();
                get_finanacial_year = get_finanacial_year.split('-');
                splited_form_year = get_finanacial_year[1];
                splited_to_year = get_finanacial_year[3];
                loadData(splited_form_year, splited_to_year, From_month, To_month, access_type);

            }


            //if (get_access_master[0].Year_setting == '0') {


            //    get_Years();
            //    var get_finanacial_year = $('.calender_selected').val();
            //    var get_month = get_access_master[0].Year_value;
            //    get_month = get_month.split('-');
            //    var from_month = get_month[0];
            //    var to_month = get_month[1];
            //    Tye = 'C';
            //    

            //}

            //else {

            //    var get_finanacial_year = $('.Financial_selected').val();
            //    get_finanacial_year = get_finanacial_year.split('-');
            //    splited_year = get_finanacial_year[0];
            //    splited_year1 = '20' + get_finanacial_year[1];
            //    var get_month = get_access_master[0].Year_value;
            //    get_month = get_month.split('-');
            //    var from_month = get_month[0];
            //    var to_month = get_month[1];
            //    get_Years(from_month, to_month);
            //    Tye = 'F';
            //    loadData(splited_year, splited_year1, from_month, to_month, 'F');
            //}

            $("#reportrange").on("DOMSubtreeModified", function () {

                id = $('#ordDate').text();
                id = id.split('-');
                fdt = id[2].trim() + '-' + id[1] + '-' + id[0] + ' 00:00:00';
                tdt = id[5] + '-' + id[4] + '-' + id[3].trim() + ' 00:00:00';
            });

            //$(document).on("change", ".calender_selected", function () {
            //    var selected_year = $(this).val();
            //    splited_year = selected_year;
            //    splited_year1 = splited_year;
            //    Tye = 'C';
            //    loadData(selected_year, selected_year, 'C');
            //});


            $(document).on("change", ".year_select", function () {

                var selected_year = $(this).val();

                if (access_type == '0') {

                    splited_form_year = selected_year;
                    splited_to_year = selected_year;
                    loadData(splited_form_year, splited_to_year, From_month, To_month, access_type);

                }
                else {

                    get_finanacial_year = selected_year.split('-');
                    splited_form_year = get_finanacial_year[1];
                    splited_to_year = get_finanacial_year[3];
                    loadData(splited_form_year, splited_to_year, From_month, To_month, access_type);

                }

            });

            $('#cust_select').change(function () {

                var selected_cust = $(this).val();
                ReloadTable(Orders);

            });
            function ReloadTable() {
                var total = 0; var collected = 0; var bal = 0; var adv = 0; var open = 0;
                $("#Cust_bal_table TBODY").html("");
                $('#Cust_bal_table').DataTable().clear().destroy();
                if ($('#cust_select').val() != "0") {
                    Orders = AllOrders.filter(function (d) {
                        return (d.Cust_Id == $('#cust_select').val());
                    });
                }
				else{
				Orders = AllOrders;
				}
                for ($i = 0; Orders.length > $i; $i++) {
                    if ($i < Orders.length) {
                        tr = $("<tr></tr>");
                        //<td style='display:none';>" + Orders[$i].Cust_Id + "</td>
                        $(tr).html("<td><a id='clc' href='Rpt_Customer_Bal_Details.aspx?Cust_Code=" + Orders[$i].Cust_Id + "&Cust_Name=" + Orders[$i].Cust_Name + "&From_Year=" + splited_form_year + "&To_Year=" + splited_to_year + "&From_Month=" + From_month + "&To_Month=" + To_month + "&Tpe=" + access_type + "'>" + Orders[$i].Cust_Name + "</td><td align=\"right\">" + Orders[$i].Opening.toFixed(2) + "</td><td align=\"right\">" + Orders[$i].Total.toFixed(2) + "</td><td align=\"right\">" + Orders[$i].col_amt.toFixed(2) + "</td><td align=\"right\">" + Orders[$i].pen.toFixed(2) + "</td>");

                        total = parseFloat(Orders[$i].Total || 0) + parseFloat(total)
                        collected = parseFloat(Orders[$i].col_amt) + parseFloat(collected);
                        bal = parseFloat(Orders[$i].pen) + parseFloat(bal);
                        adv = parseFloat(Orders[$i].Advance_amount) + parseFloat(adv);
                        open = parseFloat(Orders[$i].Opening) + open;

                        $("#Cust_bal_table TBODY").append(tr);
                    }
                }
                if (Orders.length > 0) {

                    $("#Cust_bal_table TFOOT").html("<tr><td style='font-weight: bold;'>Total</td><td style='font-weight: bold;text-align:right;'>" + open.toFixed(2) + "</td><td style='font-weight: bold;text-align:right;'>" + total.toFixed(2) + "</td><td style='text-align: right;font-weight: bold;'>" + collected.toFixed(2) + "</td><td style='text-align:right;font-weight: bold;'>" + bal.toFixed(2) + "</td></tr>");
                }
                else {
                    $("#Cust_bal_table TFOOT").html("<tr><td colspan='3'>There were no sales during the selected date range.</td></tr>");
                }
                if ($('#cust_select').val() == "0") {
                    $('#Cust_bal_table').DataTable({
                        retrieve: true,
                        scrollY: 300,
                        scroller: true
                    });
                }
                else {
                    $('#Cust_bal_table').DataTable({
                        retrieve: true
                    });
                }
            }

            //<td align=\"right\">" + Orders[$i].Advance_amount.toFixed(2) + "</td>
            //<td style='text-align:right;font-weight: bold;'>" + adv.toFixed(2) + "</td>

            //function call_calender_year() {

            //    $.ajax({
            //        type: "POST",
            //        contentType: "application/json; charset=utf-8",
            //        async: false,
            //        url: "Rpt_Customer_Bal.aspx/Get_Calender_Year",
            //        dataType: "json",
            //        success: function (data) {
            //            Calender_Year = JSON.parse(data.d);
            //            for (var t = 0; t < Calender_Year.length; t++) {
            //                if (thisyear == Calender_Year[t].Year) {
            //                    $('.calender_selected').append("<option selected value=" + Calender_Year[t].Year + ">" + Calender_Year[t].Year + "</option>");
            //                }
            //                else {
            //                    $('.calender_selected').append("<option value=" + Calender_Year[t].Year + ">" + Calender_Year[t].Year + "</option>");
            //                }
            //            }
            //        },
            //        error: function (result) {
            //            alert(JSON.stringify(result));
            //        }
            //    });
            //}

            function get_Years(from_mnth, to_mnth, type) {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    data: "{'FM':'" + from_mnth + "','TM':'" + to_mnth + "','Type':'" + type + "'}",
                    url: "Rpt_Customer_Bal.aspx/Get_Year_Data",
                    dataType: "json",
                    success: function (data) {
                        Year_Data = JSON.parse(data.d);
                        for (var t = 0; t < Year_Data.length; t++) {
                            $('.year_select').append("<option selected value=" + Year_Data[t].Get_years + ">" + Year_Data[t].Get_years + "</option>");
                        }
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            }

            function loadData(splited_form_year, splited_to_year, From_month, To_month, access_type) {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Rpt_Customer_Bal.aspx/Get_Cust_bal",
                    data: "{'From_Year':'" + splited_form_year + "','To_Year':'" + splited_to_year + "','FM':'" + From_month + "','TM':'" + To_month + "','Type':'" + access_type + "'}",
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
    </body>
    </html>
</asp:Content>

