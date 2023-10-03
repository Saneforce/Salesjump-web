<%@ Page Title="" Language="C#" MasterPageFile="~/Master_DIS.master" AutoEventWireup="true" CodeFile="Rpt_Retailer_Payment_Pending.aspx.cs" Inherits="Stockist_Sales_Rpt_Retailer_Payment_Pending" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.2/js/bootstrap-select.min.js"></script>
    <%--<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-confirm/3.3.2/jquery-confirm.min.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-confirm/3.3.2/jquery-confirm.min.js"></script>--%>
    <link href="../../alertstyle/jquery-confirm.min.css" rel="stylesheet" />
    <link href="../../css/SpinnerInput.css" rel="stylesheet" type="text/css" />
    <link href="../../css/select2.min.css" rel="stylesheet" />
    <script src="../../js/select2.full.min.js"></script>
    <script type="text/javascript" src="../../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../../js/plugins/datatables/dataTables.bootstrap.js"></script>
<script src="../../canvas/canvasjs.min.js"></script>
<script src="../../canvas/canvasjs.stock.min.js"></script>
<script src="../../canvas/jquery.canvasjs.min.js"></script>
    <script src="../../alertstyle/jquery-confirm.min.js"></script>
    <div class="row" style="min-height: 543px;">
        <style>
            .daterangepicker {
                z-index: 10000000;
            }

            .chosen-container {
                width: 100% !important;
            }

            .Spinner-Input {
                width: auto !important;
            }

            .rowCheckbox, #select_all {
                display: none;
            }

            .chosen-container {
                width: 320px !important;
            }

            input[type='text'], select {
                border: 1.5px solid #19a4c6a3 !important;
                background: aliceblue;
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

            .stockClass {
                font-size: 12px;
                font-weight: bolder;
            }

            .fa-stack-overflow:before {
                content: "\f16c";
                margin-right: 6px;
                margin-left: 15px;
                color: #00bcd4;
            }

            .fa-tags:before {
                content: "\f02c";
                color: #ef1b1b;
                margin-right: 6px;
            }

            .fa-code:before {
                content: "\f121";
                margin-left: 17px;
                margin-right: 6px;
                color: #0075ff;
                font-size: 13px;
                font-weight: bolder;
            }

            tbody {
                border-bottom: 1.5px solid #6cc5db !important;
            }

            tfoot :last-child {
                border-top: 1.5px solid #6cc5db !important;
            }

            .headcard {
                font-size: 23px;
                font-weight: 500;
                padding-bottom: 10px;
                color: #19a4c6;
            }
        </style>
        <div class="col-lg-12 sub-header headcard">
            Pending Details <span style="float: right; margin-right: 15px;">
                <%--<div id="reportrange" class="form-control mt-5 mb-5" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc;">
                    <i class="fa fa-calendar"></i>&nbsp;
        <span id="ordDate"></span><i class="fa fa-caret-down"></i>
                </div>
            </span>--%>
                  <label style="padding-left: 63px;font-weight:100;font-size: 14px;">Financial Year</label>
                <select name="year_select" class="form-control year_select">
                </select>
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
                <div style="white-space: nowrap">
                    Search&nbsp;&nbsp;<input type="text" id="tSearchOrd" autocomplete="off" style="width: 250px;" />
                    <label style="float: right; padding: 5px;">
                        Show
                    <select class="data-table-basic_length" aria-controls="data-table-basic">
                        <option value="10">10</option>
                        <option value="25">25</option>
                        <option value="50">50</option>
                        <option value="100">100</option>
                    </select>
                        entries</label>

                </div>
                <table class="table table-hover" id="OrderList">
                    <thead class="text-warning">
                        <tr>
                            <th>Sl NO</th>
                            <th>Customer Name</th>
                            <th>Total Orders</th>
                            <th>Pending Orders</th>
                            <th style="text-align: right">Bill Amount</th>
                            <th style="text-align: right">Collected Amount</th>
                            <th style="text-align: right">Due Amount</th>
                        </tr>
                    </thead>
                    <tbody style="cursor: pointer;">
                    </tbody>
                    <tfoot></tfoot>
                </table>
                <div class="row">
                    <div class="col-sm-5">
                        <div class="dataTables_info" id="orders_info" role="status" aria-live="polite">Showing 0 to 0 of 0 entries</div>
                    </div>
                    <div class="col-sm-7">
                        <div class="dataTables_paginate paging_simple_numbers" id="example2_paginate">
                            <ul class="pagination">
                                <li class="paginate_button previous" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">Previous</a></li>
                                <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="7" tabindex="0">Next</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="exampleModal" style="z-index: 10000000; overflow-y: auto;" tabindex="0" aria-hidden="true" data-keyboard="false" data-backdrop="static">
            <div class="modal-dialog" role="document" style="width: 1200px !important">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title " id="exampleModalLabel">Payment Detail view</h5>
                        <button type="button" id="btntimesClose" class="close" style="margin-top: -20px;" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    </div>
                    <div class="modal-body">
                        <div class="" style="padding: 8px; margin-top: 0px;">
                            <div class="row">
                                <div class="col-sm-3">
                                    <label for="Customer">Customer Name:</label>
                                    <input type="text" disabled class="form-control" id="Customer" />
                                </div>
                                <div class="col-sm-3">
                                    <label for="Bill">Total Bill Amount:</label>
                                    <input type="text" disabled class="form-control" id="Bill" />

                                </div>
                                <div class="col-sm-3">
                                    <label for="recpt-dt">Total Collection Amount :</label>
                                    <input type="text" disabled class="form-control" id="Collection" />
                                </div>
                                <div class="col-sm-3">
                                    <label for="pend">Total Due Amount :</label>
                                    <input type="text" disabled class="form-control" id="pend" />
                                </div>

                            </div>
                            
                        </div>


                        <div class="row">
                            <div class="col-sm-12">
                                <div class="tableBodyScroll card">
                                    <table class="table table-hover" id="OrderEntry">
                                        <thead class="text-warning">
                                            <tr>
                                                <th style="text-align: center">S.No</th>
                                                <th style="text-align: center">Invoice No</th>
                                                <th style="text-align: center">Invoice Date</th>
                                                <th style="text-align: center;">Due Date</th>
                                                <th style="text-align: right">Bill Amount </th>
                                                <th style="text-align: right">Collection Amount</th>
                                                <th style="text-align: right">Due Amount</th>
                                            </tr>
                                        </thead>
                                        <tbody style="cursor: pointer;">
                                        </tbody>
                                    </table>
                                </div>
                            </div>

                        </div>
                        <div class="row" >
                         <div id="chartContainer" class="col-6 " style="height:250px!important;width: 501px!important;"></div>
                         <div id="chartContainer1" class="col-6 " style="height:250px;max-width: 501px;right:594px;top: 293px;position:absolute"></div>
                    </div>
                        </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" id="btnclose" data-dismiss="modal">Close</button>

                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="exampleModal1" style="z-index: 10000000; overflow-y: auto;" tabindex="0" aria-hidden="true" data-keyboard="false" data-backdrop="static">
            <div class="modal-dialog" role="document" style="width: 1200px !important">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title " id="exampleModalLabel1">Invoice Detail view</h5>
                        <button type="button" id="btntimesClose11" class="close" style="margin-top: -20px;" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    </div>
                    <div class="modal-body">
                        <table class="table table-hover" id="tolltip" style="z-index: 1000">
                            <thead class="text-warning">
                                <tr>
                                    <th style="text-align: center">S.No</th>
                                    <th style="text-align: center">Order No</th>
                                    <th style="text-align: center">Order Date</th>
                                    <th style="text-align: center">Product name</th>
                                    <th style="text-align: center;">Unit</th>
                                    <th style="text-align: right">Qty</th>
                                    <th style="text-align: right">Amount</th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" id="btnclose1">Close</button>

                    </div>
                </div>
            </div>
        </div>
        <script type="text/javascript">
    var stockist_Code = ("<%=Session["Sf_Code"]%>");
    var Div_Code = ("<%=Session["div_code"]%>");

    //var subdiv = ("<%=Session["subdivision_code"]%>");

    var subdiv = ("<%=Session["Sub_Div"]%>");

    var new_height = 0; var currentheight = 0; var scrollhightchnage = 0; var scrollhightchnageDel = 0;

    var AllOrders = []; var fdt = ''; var tdt = ''; Prds = ""; var serch = ''; var namesArr = []; var path = ''; var page = '';
    var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "ListedDr_Name,CustCode"; var flag = 0; var CQ = '';
    var Rate_List_Code = ''; var Cust_Price = []; var arr = []; var OrderNum = ''; var filtrkey = 'All';
    pgNo_Print = 1; PgRecords_print = 40; TotalPg_print = 0; var netval = 0; var totlcase = 0;
    var cntu = 10; pagenumber = 1;

    Dt = new Date(); sDt = Dt.getFullYear() + '-' + ((Dt.getMonth() < 9) ? "0" : "") + (Dt.getMonth() + 1) + '-' + ((Dt.getDate() < 10) ? "0" : "") + Dt.getDate() + ' 00:00:00';

    path = window.location.pathname;
    page = path.split("/").pop();
    var Dist_state_code = ("<%=Session["State"].ToString()%>");
    var Dis_Code = ("<%=Session["Sf_Code"].ToString()%>");

    $('#tSearchOrd').focus();

    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1;
    var yyyy = today.getFullYear();
    if (dd < 10) { dd = '0' + dd }
    if (mm < 10) { mm = '0' + mm }
    today = yyyy + '-' + mm + '-' + dd;
    $('#Inv_date').val(today);
    $(document).ready(function () {
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
            loadData();
        }
        else {

            var get_finanacial_year = $('.year_select').val();
            get_finanacial_year = get_finanacial_year.split('-');
            splited_form_year = get_finanacial_year[1];
            splited_to_year = get_finanacial_year[3];
            loadData();

        }
        $(document).on("change", ".year_select", function () {

            var selected_year = $(this).val();

            if (access_type == '0') {

                splited_form_year = selected_year;
                splited_to_year = selected_year;
                loadData();

            }
            else {

                get_finanacial_year = selected_year.split('-');
                splited_form_year = get_finanacial_year[1];
                splited_to_year = get_finanacial_year[3];
                loadData();

            }

        });
        function get_Years(from_mnth, to_mnth, type) {

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                data: "{'FM':'" + from_mnth + "','TM':'" + to_mnth + "','Type':'" + type + "'}",
                url: "Rpt_Retailer_Payment_Pending.aspx/Get_Year_Data",
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
        resetOrderEntry();
        loadData();
        $("#OrderList").on('click', 'tbody tr', function () {
            var cust = $(this).attr('id')
            var rname = $(this).find("td:eq(1)").text();
            var rtotord = $(this).find("td:eq(2)").text();
            var rtotpenct = $(this).find("td:eq(3)").text();
            var rtotbill = $(this).find("td:eq(4)").text();
            var rtotcal = $(this).find("td:eq(5)").text();
            var rtotpend = $(this).find("td:eq(6)").text();
            if (rtotpenct == '0')
                rjalert('Error!', "This Customer Don't have any pending orders", 'error');
            fetchPendingdetails(cust, rname, rtotord, rtotpenct, rtotbill, rtotcal, rtotpend)
        });
        $("#OrderEntry").on('click', 'tbody tr', function () {
            var rname = $(this).find("td:eq(1)").text();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Rpt_Retailer_Payment_Pending.aspx/Getorder_details",
                data: "{'stk':'<%=Session["Sf_Code"]%>','Div':'" + Div_Code + "','invno':'" + rname + "'}",
                dataType: "json",
                success: function (data) {
                    var Order = JSON.parse(data.d) || [];
                    if (Order.length > 0) {
                        $("#tolltip TBODY").html("");
                        for (var i = 0; i < Order.length; i++) {
                            tr = $("<tr id=" + Order[i].Trans_Order_No + "></tr>");
                            slno = i + 1;
                            $(tr).html("<td>" + slno + "</td><td style='text-align:center;'>" + Order[i].Order_No + "</td><td class='class_orderid' id=" + Order[i].Order_Date + " style='text-align:center;'>" + Order[i].Order_Date + "</td><td style='text-align:center;'>" + Order[i].Product_Name + "</td><td style='text-align:center;'>" + Order[i].Unit + "</td><td style='text-align:right;'>" + Order[i].QTY + "</td><td style='text-align:right;'>" + Order[i].Amount + "</td>");
                            $("#tolltip TBODY").append(tr);

                        }

                    }
                    $('#exampleModal').modal('toggle');
                    $('#exampleModal1').modal('toggle');
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });

        });
        $("#btnclose1 , #btntimesClose11").click(function () {

            $('#exampleModal1').modal('toggle');
            $('#exampleModal').modal('toggle');
        })
    });
    $("#reportrange").on("DOMSubtreeModified", function () {

        id = $('#ordDate').text();
        id = id.split('-');
        fdt = id[2].trim() + '-' + id[1] + '-' + id[0] + ' 00:00:00';
        tdt = id[5] + '-' + id[4] + '-' + id[3].trim() + ' 00:00:00';
        $(".data-table-basic_length").val(PgRecords);
        loadData();
        var Get_localData1 = localStorage.getItem("Date_Details");
        Get_localData1 = JSON.parse(Get_localData1);
        if (Get_localData1 != null) { if (Get_localData1[3] != '' && Get_localData1[4] == page) { $('#tSearchOrd').val(Get_localData1[3]); search_fun(Get_localData1[3]); } else { $('#tSearchOrd').val(''); } }

    });
    function loadData() {

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            async: false,
            url: "Rpt_Retailer_Payment_Pending.aspx/Getpending_summary",
            data: "{'stk':'<%=Session["Sf_Code"]%>','Div':'" + Div_Code + "','splited_form_year':'" + splited_form_year + "','splited_to_year':'" + splited_to_year + "','From_month':'" + From_month + "','To_month':'" + To_month + "'}",
            dataType: "json",
            success: function (data) {
                AllOrders = JSON.parse(data.d) || [];
                Orders = AllOrders; ReloadTable(0);
            },
            error: function (result) {
                alert(JSON.stringify(result));
            }
        });
    }
    $('#cust_select').change(function () {

        var selected_cust = $(this).val();
        ReloadTable(0);

    });
    function ReloadTable(typ) {
        var totord = 0; var totbill = 0; var totcoll = 0; var totpen = 0; var totpencount = 0;
        var ttotord = 0; var ttotbill = 0; var ttotcoll = 0; var ttotpen = 0; var ttotpencount = 0;
        $("#OrderList TBODY").html("");
        if ($('#cust_select').val() != "0") {
            Orders = AllOrders.filter(function (d) {
                return (d.CustCode == $('#cust_select').val());
            });

        }
        else {
            Orders = AllOrders;
        }

        if (typ == 1) { st = 0; } else { st = PgRecords * (pgNo - 1); slno = 0; }
        for ($j = 0; $j < Orders.length; $j++) {
            ttotord = parseFloat(Orders[$j].cout || 0) + parseFloat(ttotord);
            ttotpencount = parseFloat(Orders[$j].pendcount || 0) + parseFloat(ttotpencount);
            ttotbill = parseFloat(Orders[$j].billamt || 0) + parseFloat(ttotbill);
            ttotcoll = parseFloat(Orders[$j].colamt || 0) + parseFloat(ttotcoll);
            ttotpen = parseFloat(Orders[$j].bal || 0) + parseFloat(ttotpen);
        }
        for ($i = st; $i < st + PgRecords; $i++) {
            if ($i < Orders.length) {
                tr = $("<tr id=" + Orders[$i].CustCode + "></tr>");
                slno = $i + 1;
                $(tr).html("<td>" + slno + "</td><td class='class_orderid' id=" + Orders[$i].CustCode + " style='text-align:center;'>" + Orders[$i].ListedDr_Name + "</td><td style='text-align:center;'>" + Orders[$i].cout + "</td><td style='text-align:center;'>" + Orders[$i].pendcount + "</td><td style='text-align:right;'>" + Orders[$i].billamt + "</td><td style='text-align:right;'>" + Orders[$i].colamt + "</td><td style='text-align:right;'>" + Orders[$i].bal + "</td>");
                totord = parseFloat(Orders[$i].cout || 0) + parseFloat(totord);
                totpencount = parseFloat(Orders[$i].pendcount || 0) + parseFloat(totpencount);
                totbill = parseFloat(Orders[$i].billamt || 0) + parseFloat(totbill);
                totcoll = parseFloat(Orders[$i].colamt || 0) + parseFloat(totcoll);
                totpen = parseFloat(Orders[$i].bal || 0) + parseFloat(totpen);

                //tota = parseFloat(Orders[$i].Order_Value || 0) + parseFloat(tota)
                $("#OrderList TBODY").append(tr);

            }
        }
        $("#OrderList TFOOT").html("<tr><td colspan='2' style='font-weight: bold;text-align:right;'>Total</td><td style='text-align: center; font-weight: bold;'>" + totord + "</td><td style='text-align: center; font-weight: bold;'>" + totpencount + "</td><td style='text-align: right; font-weight: bold;text-align:right;'>" + (Math.round(totbill * 100) / 100) + "</td><td style='text-align: right; font-weight: bold;text-align:right;'>" + (Math.round(totcoll * 100) / 100) + "</td><td style='text-align: right; font-weight: bold;text-align:right;'>" + (Math.round(totpen * 100) / 100) + "</td></tr><tr><td colspan='2' style='font-weight: bold;text-align:right;'>Overall Total</td><td style='text-align: center; font-weight: bold;'>" + ttotord + "</td><td style='text-align: center; font-weight: bold;'>" + ttotpencount + "</td><td style='text-align: right; font-weight: bold;text-align:right;'>" + (Math.round(ttotbill * 100) / 100) + "</td><td style='text-align: right; font-weight: bold;text-align:right;'>" + (Math.round(ttotcoll * 100) / 100) + "</td><td style='text-align: right; font-weight: bold;text-align:right;'>" + (Math.round(ttotpen * 100) / 100) + "</td></tr>");
        $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")

        loadPgNos();
    }
    function loadPgNos() {
        prepg = parseInt($(".paginate_button.previous>a").attr("data-dt-idx"));
        Nxtpg = parseInt($(".paginate_button.next>a").attr("data-dt-idx"));
        $(".pagination").html("");
        TotalPg = parseInt(parseInt(Orders.length / PgRecords) + ((Orders.length % PgRecords) ? 1 : 0)); selpg = 1;
        if (isNaN(prepg)) prepg = 0;
        if (isNaN(Nxtpg)) Nxtpg = 2;
        selpg = (pgNo > 7) ? (parseInt(pgNo) + 1) - 7 : 1;
        if ((Nxtpg) == pgNo) {
            selpg = (parseInt(TotalPg)) - 7;
            selpg = (selpg > 1) ? selpg : 1;
        }
        spg = '<li class="paginate_button previous" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">First</a></li>';
        for (il = selpg - 1; il < selpg + 7; il++) {
            if (il < TotalPg)
                spg += '<li class="paginate_button' + ((pgNo == (il + 1)) ? " active" : "") + '"><a href="#" aria-controls="example2" data-dt-idx="' + (il + 1) + '" tabindex="0">' + (il + 1) + '</a></li>';
        }
        spg += '<li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="' + TotalPg + '" tabindex="0">Last</a></li>';
        $(".pagination").html(spg);

        $(".paginate_button > a").on("click", function () {
            pgNo = parseInt($(this).attr("data-dt-idx"));
            ReloadTable(0);

        }
        );
    }
    $("#tSearchOrd").on("keyup", function () {
        pgNo = 1;
        if ($(this).val() != '') {
            set_local_storage();
            search_fun($(this).val());
        }
        else {
            set_local_storage();
            search_fun($(this).val());
        }
    })

    function search_fun(Search_text) {

        if (Search_text != "") {
            shText = Search_text;
            Orders = Orders.filter(function (a) {
                chk = false;
                $.each(a, function (key, val) {
                    if (val != null && val.toString().toLowerCase().indexOf(shText) > -1 && (',' + searchKeys).indexOf(',' + key + ',') > -1) {
                        chk = true;
                    }
                })
                return chk;
            })
        }
        ReloadTable(1);
    }
    function set_local_storage() {
        namesArr = [];
        namesArr.push($('#reportrange span').text());
        namesArr.push(pgNo);
        namesArr.push($(".data-table-basic_length option:selected").text());
        namesArr.push($('#tSearchOrd').val());
        namesArr.push(page);
        window.localStorage.setItem("Date_Details", JSON.stringify(namesArr));
    }

    $("#tSearchOrd").on("keyup", function () {
        pgNo = 1;
        if ($(this).val() != '') {
            set_local_storage();
            search_fun($(this).val());
        }
        else {
            set_local_storage();
            search_fun($(this).val());
        }
    })
    function resetOrderEntry() {
        $("#ordDate").val(new Date().toLocaleDateString('en-GB'));
    }
    function toolTipFormatter(e) {
        var str = "";
        var total = 0;
        var str3;
        var str2;
        for (var i = 0; i < e.entries.length; i++) {
            var str1 = "<span style= 'color:" + e.entries[i].dataSeries.color + "'>" + e.entries[i].dataPoint.legendText + "</span>: <strong>" + e.entries[i].dataPoint.y + "</strong> <br/>";
            total = e.entries[i].dataPoint.y + total;
            str = str.concat(str1);
        }
        str2 = "<strong>" + e.entries[0].dataPoint.label + "</strong> <br/>";
        str3 = "<span style = 'color:Tomato'>Total: </span><strong>" + total + "</strong><br/>";
        return (str2.concat(str)).concat(str3);
    }
    function fetchPendingdetails(custcode, name, totord, totpenct, totbill, totcol, totpen) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            async: false,
            url: "Rpt_Retailer_Payment_Pending.aspx/Getpending_details",
            data: "{'stk':'<%=Session["Sf_Code"]%>','Div':'" + Div_Code + "','custcode':'" + custcode + "','splited_form_year':'" + splited_form_year + "','splited_to_year':'" + splited_to_year + "','From_month':'" + From_month + "','To_month':'" + To_month + "'}",
            dataType: "json",
            success: function (data) {
                var AllOrder = JSON.parse(data.d) || [];
                if (AllOrder.length > 0) {
                    $("#OrderEntry TBODY").html("");
                    for (var i = 0; i < AllOrder.length; i++) {
                        tr = $("<tr id=" + AllOrder[i].CustCode + "></tr>");
                        slno = i + 1;
                        var dd = AllOrder[i].pendday == '0' ? 'Today' : AllOrder[i].pendday == '1' ? 'Yesterday' : AllOrder[i].pendday + ' days ago';
                        $(tr).html("<td>" + slno + "</td><td style='text-align:center;'>" + AllOrder[i].Invoice_No + "</td><td class='class_orderid' id=" + AllOrder[i].Inv_Dt + " style='text-align:center;'>" + AllOrder[i].Inv_Dt + "</td><td style='text-align:center;'>" + dd + "</td><td style='text-align:right;'>" + AllOrder[i].BillAmt + "</td><td style='text-align:right;'>" + AllOrder[i].Coll_Amt + "</td><td style='text-align:right;'>" + AllOrder[i].bal + "</td>");
                        $("#OrderEntry TBODY").append(tr);

                    }
                    var dataPoints = [];

                    chart1 = new CanvasJS.Chart("chartContainer", {
                        animationEnabled: true,
                        title: {
                            text: "",
                            fontSize: 16,
                            fontWeight: "lighter",
                            fontColor: "#19a4c6"
                        },
                        width: 500,
                        height: 245,
                        axisY: {
                            title: "Amount",
                            suffix: "rs",
                            labelFontSize: 10,
                            labelFontColor: "dimGrey"
                        },
                        axisX: {
                            labelFontSize: 10,
                            labelFontColor: "dimGrey"
                        },
                        legend: {
                            cursor: "pointer",
                            itemclick: explodePie
                        },
                        toolTip: {
                            shared: true,
                            content: toolTipFormatter
                        },
                        data: [{
                            type: "pie",
                            showInLegend: true,
                            toolTipContent: "{name}: <strong>{y}%</strong>",
                            indexLabel: "{name} - {y}%",
                            dataPoints: [
                                { y: parseFloat(totbill), name: "Bill Amount" },
                                { y: parseFloat(totcol), name: "Collection Amount" },
                                { y: parseFloat(totpen), name: "Due Amount", exploded: true }
                            ]
                        }]
                    });
                    chart1.render();
                    var dataPoints1 = [];
                    var dataPoints2 = [];
                    var dataPoints3 = [];
                    for (var i = 0; i <= AllOrder.length - 1; i++) {
                        dataPoints1.push({ x: new Date(AllOrder[i].Inv_Dt), y: Number(AllOrder[i].BillAmt) });
                        dataPoints2.push({ x: new Date(AllOrder[i].Inv_Dt), y: Number(AllOrder[i].Coll_Amt) });
                        dataPoints3.push({ x: new Date(AllOrder[i].Inv_Dt), y: Number(AllOrder[i].bal) });
                    }
                    var chart = new CanvasJS.Chart("chartContainer1", {
                        animationEnabled: true,
                        title: {
                            text: ""
                        },
                        axisY: {
                            title: "Bill Amount",
                            titleFontColor: "#4F81BC",
                            lineColor: "#4F81BC",
                            labelFontColor: "#4F81BC",
                            tickColor: "#4F81BC"
                        },
                        axisY3: {
                            title: "Due Amount",
                            titleFontColor: "#C0504E",
                            lineColor: "#C0504E",
                            labelFontColor: "#C0504E",
                            tickColor: "#C0504E"
                        },
                         width: 550,
                        height: 245,
                        
                        toolTip: {
                            shared: true
                        },
                        legend: {
                            cursor: "pointer",
                            itemclick: toggleDataSeries
                        },
                        data: [{
                            type: "column",
                            name: "Bill Amount",
                            legendText: "Bill Amount",
                            showInLegend: true,
                            dataPoints: dataPoints1
                        },
                        {
                            type: "column",
                            name: "Collection Amount",
                            legendText: "Collection Amount",
                            axisYType: "secondary",
                            showInLegend: true,
                            dataPoints: dataPoints2
                        },
                        {
                            type: "column",
                            name: "Due Amount",
                            legendText: "Due Amount",
                            axisYType: "secondary",
                            showInLegend: true,
                            dataPoints: dataPoints3
                        }]
                    });
                    chart.render();
                    $("#Customer").val(name);
                    $("#Bill").val(totbill);
                    $("#Collection").val(totcol);
                    $("#pend").val(totpen);
                    $('#exampleModal').modal('toggle');
                }
            },
            error: function (result) {
                alert(JSON.stringify(result));
            }
        });
    }
    function explodePie(e) {
        if (typeof (e.dataSeries.dataPoints[e.dataPointIndex].exploded) === "undefined" || !e.dataSeries.dataPoints[e.dataPointIndex].exploded) {
            e.dataSeries.dataPoints[e.dataPointIndex].exploded = true;
        } else {
            e.dataSeries.dataPoints[e.dataPointIndex].exploded = false;
        }
        e.chart.render();

    }

    function toggleDataSeries(e) {
        if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
            e.dataSeries.visible = false;
        }
        else {
            e.dataSeries.visible = true;
        }
        chart.render();
    }
    function rjalert(titles, contents, types) {
        if (types == 'error') {
            var tp = 'red';
            var icons = 'fa fa-warning';
            var btn = 'btn-red';
        }
        else {
            var tp = 'green';
            var icons = 'fa fa-check fa-2x';
            var btn = 'btn-green';
        }
        $.confirm({
            title: '' + titles + '',
            content: '' + contents + '',
            type: '' + tp + '',
            typeAnimated: true,
            autoClose: 'action|8000',
            icon: '' + icons + '',
            buttons: {
                tryAgain: {
                    text: 'OK',
                    btnClass: '' + btn + '',
                    action: function () {

                    }
                }
            }
        });
    }

        </script>
        <script type="text/javascript">
    $(function () {

        //var Get_localData = localStorage.getItem("Date_Details");

        //Get_localData = JSON.parse(Get_localData);

        ////if (Get_localData != "" && Get_localData != null && Get_localData[4] == page) {
        //if (Get_localData != "" && Get_localData != null) {

        //    var Dates = Get_localData[0].split('-');

        //    var fdj = Dates[2].trim() + '-' + Dates[1] + '-' + Dates[0] + ' ' + ' 00:00:00';
        //    var nfgresd = Dates[5] + '-' + Dates[4] + '-' + Dates[3] + ' ' + ' 00:00:00';

        //    pgNo = parseFloat(Get_localData[1]); PgRecords = parseFloat(Get_localData[2]); flag = '1';

        //    const utcDate = new Date(fdj);
        //    const utcDate1 = new Date(nfgresd);

        //    var start = utcDate;
        //    var end = utcDate1;
        //}
        //else {
        //    var start = moment();
        //    var end = moment();
        //}

        //function cb(start, end, flag) {

        //    if (flag == '1') {

        //        var F_dat = start.getDate();
        //        var F_dat1 = start.getMonth() + 1;
        //        var F_dat2 = start.getFullYear();
        //        var f_date3 = F_dat + '-' + F_dat1 + '-' + F_dat2;

        //        var E_dat = end.getDate();
        //        var E_dat1 = end.getMonth() + 1;
        //        var E_dat2 = end.getFullYear();
        //        var E_date3 = E_dat + '-' + E_dat1 + '-' + E_dat2;

        //        $('#reportrange span').html(f_date3 + ' - ' + E_date3);
        //    }
        //    else {
        //        pgNo = 1;
        //        $('#reportrange span').html(start.format('DD-MM-YYYY') + ' - ' + end.format('DD-MM-YYYY'));
        //    }

        //    namesArr = [];
        //    namesArr.push($('#reportrange span').text());
        //    namesArr.push(pgNo);
        //    namesArr.push($(".data-table-basic_length option:selected").text());
        //    namesArr.push($('#tSearchOrd').val());
        //    namesArr.push(page);
        //    window.localStorage.setItem("Date_Details", JSON.stringify(namesArr));
        //}

        //$('#reportrange').daterangepicker({
        //    startDate: start,
        //    endDate: end,
        //    ranges: {
        //        'Today': [moment(), moment()],
        //        'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
        //        'Last 7 Days': [moment().subtract(6, 'days'), moment()],
        //        'Last 30 Days': [moment().subtract(29, 'days'), moment()],
        //        'This Month': [moment().startOf('month'), moment().endOf('month')],
        //        'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
        //    }
        //}, cb);
        //cb(start, end, flag);

    });

        </script>
</asp:Content>

