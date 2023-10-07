<%@ Page Title="" Language="C#" MasterPageFile="~/Master_DIS.master" AutoEventWireup="true" CodeFile="myOrders.aspx.cs" Inherits="Stockist_myOrders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../css/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.2/js/bootstrap-select.min.js"></script>
    <%--<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-confirm/3.3.2/jquery-confirm.min.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-confirm/3.3.2/jquery-confirm.min.js"></script>--%>
    <link href="../alertstyle/jquery-confirm.min.css" rel="stylesheet" />
    <script src="../alertstyle/jquery-confirm.min.js"></script>
    <div class="row">
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
        </style>
        <div class="col-lg-12 sub-header">
            My Orders <span style="float: right"><a href="#" class="btn btn-primary btn-update" id="newsorder">New Order</a></span><span style="float: right; margin-right: 15px;"><div id="reportrange" class="form-control mt-5 mb-5" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc;">
                <i class="fa fa-calendar"></i>&nbsp;
        <span id="ordDate"></span><i class="fa fa-caret-down"></i>
            </div>
            </span>
        </div>
    </div>

    <div class="row">
        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
            <div class="form-group">
                <label class="control-label " for="focusedInput">
                    Route Name</label>
                <select class="form-control" id="rtname" style="width: 100%;" data-size="5">
                    <option value="0">Select Route Name</option>
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
                <button type='button' class='btn btn-primary btnAllPendingOrderSave' id='btnAllPendingOrderId' style='display: none; float: right; margin-right: 10px;'>Save Invoice</button>
                <input type="date" id="Inv_date" style="float: right; margin-right: 16px; margin-top: 4px;" />
                <label style="float: right; margin-right: 16px; margin-top: 6px;">Invoice Date :</label>
                <div style="float: right; padding: 4px 0px 0px 0px;">
                    <ul class="segment">
                        <li data-va='All' class="active">ALL</li>
                        <li data-va='0'>Pending</li>
                        <li data-va='1'>Invoiced</li>
                        <li data-va='2'>Cancelled</li>
                    </ul>
                </div>
            </div>
            <table class="table table-hover" id="OrderList">
                <thead class="text-warning">
                    <tr>
                        <th>
                            <input type="checkbox" name="checkAll" id="select_all" /></th>
                        <th>Sl NO</th>
                        <th>Order ID</th>
                        <th>Order Date</th>
                        <th>Order taken</th>
                        <th>Retailer</th>
                        <th style="display: none;">Order taken</th>
                        <th>Status</th>
                        <th style="text-align: right">Amount</th>
                        <th style="text-align: right">Edit</th>
                        <th style="text-align: right">View</th>
                        <th style="text-align: right">Print</th>
                        <th style="text-align: right">Cancel</th>
                        <th style="text-align: right">Invoice</th>
                    </tr>
                </thead>
                <tbody>
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

    <div id="div_Print" style="display: none; padding-right: 10px;">
    </div>

    <div class="modal fade" id="exampleModal" style="z-index: 10000000; overflow-y: auto;" tabindex="0" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" role="document" style="width: 1200px !important">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">New Order Entry</h5>
                    <button type="button" id="btntimesClose" class="close" style="margin-top: -20px;" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <div class="card" style="padding: 8px; margin-top: 0px;">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="rou-name">Route Name:</label>
                                <span style="color: Red">*</span>
                                <select id="route-name" data-dropup-auto="false" data-width="100%" data-size="5">
                                </select>
                            </div>
                            <div class="col-sm-4">
                                <label for="recipient-name">Retailer Name:</label>
                                <span style="color: Red">*</span>
                                <select id="recipient-name" data-dropup-auto="false" data-width="100%" data-size="5">
                                </select>
                                <br />
                                <br />
                                <label id="addressid"></label>
                            </div>
                            <div class="col-sm-4">
                                <label for="recpt-dt">Date:</label><span style="color: Red">*</span>
                                <input type="text" class="form-control" id="recpt-dt" />
                            </div>
                            <%--<div class="col-sm-4">
                            <label for="message-text" class="col-form-label">Note:</label>
                            <textarea class="form-control" id="message-text"></textarea>
                        </div>--%>
                        </div>
                    </div>

                    <div class="row" style="text-align: center;">
                        <div id="myDIV">
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-12" style="padding: 15px">
                            <div class="tableBodyScroll card">
                                <table class="table table-hover" id="OrderEntry">
                                    <thead class="text-warning">
                                        <tr>
                                            <th style="text-align: left">S.No</th>
                                            <th style="text-align: left">Product</th>
                                            <th style="text-align: left; word-spacing: 41px; padding: 0px 0px 8px 50px;">Unit Qty</th>
                                            <th style="text-align: right">Rate</th>
                                            <th style="text-align: right">Free</th>
                                            <th style="text-align: right">Dis Val</th>
                                            <th style="text-align: right">Total</th>
                                            <th style="text-align: right">Tax</th>
                                            <th style="text-align: right">Gross_Total</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                        </div>

                    </div>
                    <div class="row" style="margin-left: 0%;">
                        <div>
                            <div style="text-align: left" colspan="9">
                                <button type="button" class="btn btn-success" onclick="AddRow(1)" style="font-size: 12px">+ Add Product </button>
                                <button type="button" class="btn btn-danger" onclick="DelRow()" style="font-size: 12px">- Remove Product</button>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-8">
                            <div class="card freecard">
                                <div class="card-body table-responsive">
                                    <div style="white-space: nowrap; padding: 0px 0px 7px 7px; font-weight: 900;">View Offer Products</div>
                                    <div class="tddd">
                                        <table id="free_table" class="table table-hover">
                                            <thead class="text-warning">
                                                <tr>
                                                    <th style="width: 14%;">SlNo</th>
                                                    <th style="width: 22%;">Product Code </th>
                                                    <th style="width: 37%;">Product Name</th>
                                                    <th style="width: 14%;">Free</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                            </tbody>
                                            <tfoot>
                                            </tfoot>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-offset-8 form-horizontal">
                            <label class=" col-xs-3 control-label" style="font-size: 12.8px;">
                                Subtotal :
                            </label>
                            <div class="col-sm-9">
                                <div class="input-group">
                                    <div class="input-group-addon currency" style="background: #00bcd4; color: white; border-color: #00bcd4;">
                                        <i class="fa fa-inr"></i>
                                    </div>
                                    <input data-cell="G1" id="sub_tot" data-format="0,0.00" class="form-control txtblue" readonly />
                                </div>
                            </div>
                        </div>


                        <div class="col-sm-offset-8 form-horizontal" style="margin-top: 40px;">
                            <label class=" col-xs-3 control-label" style="font-size: 12.8px;">
                                Dis Total :
                            </label>
                            <div class="col-sm-9">
                                <div class="input-group">
                                    <div class="input-group-addon currency" style="background: #00bcd4; color: white; border-color: #00bcd4;">
                                        <i class="fa fa-inr"></i>
                                    </div>
                                    <input data-cell="G1" id="Txt_Dis_tot" data-format="0,0.00" class="form-control txtblue" readonly />
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-offset-8 form-horizontal" style="display: none;">
                            <label class=" col-xs-3 control-label">
                                Tax Total :
                            </label>
                            <div class="col-sm-9">
                                <div class="input-group">
                                    <div class="input-group-addon currency" style="background: #00bcd4; color: white; border-color: #00bcd4;">
                                        <i class="fa fa-inr"></i>
                                    </div>
                                    <input data-cell="G1" id="Tax_GST" data-format="0,0.00" class="form-control txtblue" readonly />
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-offset-8 form-horizontal div_cgst" style="margin-top: 80px;">
                            <label class=" col-xs-3 control-label" style="font-size: 12.5px;">
                                Tax Total :
                            </label>
                            <div class="col-sm-9">
                                <div class="input-group">
                                    <div class="input-group-addon currency" style="background: #00bcd4; color: white; border-color: #00bcd4;">
                                        <i class="fa fa-inr"></i>
                                    </div>
                                    <input data-cell="G1" id="txt_tax_total" data-format="0,0.00" class="form-control txtblue" readonly />
                                </div>
                            </div>
                        </div>

                        <%--                        <div class="col-sm-offset-8 form-horizontal div_sgst" style="margin-top: 120px;">
                            <label class=" col-xs-3 control-label" style="font-size: 12.5px;">
                                SGST Total :
                            </label>
                            <div class="col-sm-9">
                                <div class="input-group">
                                    <div class="input-group-addon currency">
                                        <i class="fa fa-inr"></i>
                                    </div>
                                    <input data-cell="G1" id="txt_sgst" data-format="0,0.00" class="form-control" readonly />
                                </div>
                            </div>
                        </div>


                        <div class="col-sm-offset-8 form-horizontal div_igst" style="margin-top: 160px;">
                            <label class=" col-xs-3 control-label" style="font-size: 12.5px;">
                                ISGST Total :
                            </label>
                            <div class="col-sm-9">
                                <div class="input-group">
                                    <div class="input-group-addon currency">
                                        <i class="fa fa-inr"></i>
                                    </div>
                                    <input data-cell="G1" id="txt_igst" data-format="0,0.00" class="form-control" readonly />
                                </div>
                            </div>
                        </div>--%>

                        <div class="col-sm-offset-8 form-horizontal div_tcs" style="margin-top: 120px;">
                            <label class=" col-xs-3 control-label" style="font-size: 12.5px;">
                                TCS Amt :
                            </label>
                            <div class="col-sm-9">
                                <div class="input-group">
                                    <div class="input-group-addon currency" style="background: #00bcd4; color: white; border-color: #00bcd4;">
                                        <i class="fa fa-inr"></i>
                                    </div>
                                    <input data-cell="G1" id="txt_tcs_amt" data-format="0,0.00" class="form-control txtblue" readonly />
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-offset-8 form-horizontal div_tds" style="margin-top: 160px;">
                            <label class=" col-xs-3 control-label" style="font-size: 12.5px;">
                                TDS Amt :
                            </label>
                            <div class="col-sm-9">
                                <div class="input-group">
                                    <div class="input-group-addon currency" style="background: #00bcd4; color: white; border-color: #00bcd4;">
                                        <i class="fa fa-inr"></i>
                                    </div>
                                    <input data-cell="G1" id="txt_tds_amt" data-format="0,0.00" class="form-control txtblue" readonly />
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-offset-8 form-horizontal div_gross" style="margin-top: 200px;">
                            <label class=" col-xs-3 control-label" style="padding: 5px 16px 0px 0px; font-size: 12.8px;">
                                Gross Total :
                            </label>
                            <div class="col-sm-9">
                                <div class="input-group">
                                    <div class="input-group-addon currency" style="background: #00bcd4; color: white; border-color: #00bcd4;">
                                        <i class="fa fa-inr"></i>
                                    </div>
                                    <input data-cell="G1" id="gross" data-format="0,0.00" class="form-control txtblue" readonly />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" id="btnclose" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary" id="svorders">Save</button>
                </div>
            </div>
        </div>
    </div>

    <link href="../css/SpinnerInput.css" rel="stylesheet" type="text/css" />
    <link href="../css/select2.min.css" rel="stylesheet" />
    <script src="../js/select2.full.min.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>

    <style type="text/css">
        .green {
            color: #28a745;
        }

        .red {
            color: #f10b0b;
        }

        .edit_class {
            pointer-events: none;
            cursor: default;
            text-decoration: line-through;
            color: black;
        }

        .category {
            border: none;
            outline: none;
            padding: 10px 16px;
            background-color: #f1f1f1;
            cursor: pointer;
            font-size: 15px;
        }

        .active, .btn:hover {
            background-color: #00bcd4;
            color: white;
        }
    </style>

    <script language="javascript" type="text/javascript">

        var stockist_Code = ("<%=Session["Sf_Code"]%>");
        var Div_Code = ("<%=Session["div_code"]%>");

		//var subdiv = ("<%=Session["subdivision_code"]%>");

        var subdiv = ("<%=Session["Sub_Div"]%>");

        var new_height = 0; var currentheight = 0; var scrollhightchnage = 0; var scrollhightchnageDel = 0;

        var AllOrders = []; NewOrd = []; var arr = []; var fdt = ''; var tdt = ''; Prds = ""; var serch = ''; var namesArr = []; var path = ''; var page = '';
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "OrderNo,Order_Date_Disp,Retail,Order_Value,Sf_Name,Status,"; var flag = 0; var CQ = '';
        var All_Tax = []; var Retailer_State = 0; var Retailer_Extra_Tax_Type = ''; var Extra_Tax = ''; var Cat_Details = []; var Product_Details = []; var All_Retailer = []; var All_Product_Details = []; var All_Route = []; var Rate_List_Code = ''; var Cust_Price = []; var arr = []; var OrderNum = ''; var filtrkey = 'All';
        optStatus = "<li><a href='#' v='2'>Cancel</a></li>"; pgNo_Print = 1; PgRecords_print = 40; TotalPg_print = 0; var netval = 0; var cgst = 0; var sgst = 0; var totlcase = 0; var totalpie = 0; var cashdis = 0.00;
        var grossvalue = 0; var totalGst = 0.00; var roundoff = 0.00; var netvalue = 0.00; var rndoff = 0.00; var netwrd = ''; var netwrd1 = ''; var netwrd2 = '';
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

        $(document).keydown(function (e) {
            if (e.keyCode == 27)
                return false;
        });

        $(document).on('keypress', '#message-text', function (e) {
            if (e.keyCode == 34 || e.keyCode == 39 || e.keyCode == 38 || e.keyCode == 60 || e.keyCode == 62 || e.keyCode == 92) return false;
        });

        $(".data-table-basic_length").on("change",
            function () {
                pgNo = 1;
                PgRecords = parseFloat($(this).val());
                ReloadTable(0);
            }
        );

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


        $(document).on("click", "#select_all", function () {

            if (this.checked) {

                $('.rowCheckbox').prop('checked', true);

                Orders.map(function (x) {
                    x.Chk = 1;
                });
            }
            else {
                Orders.map(function (x) {
                    $('.rowCheckbox').prop('checked', false);
                });

                $('#select_all').attr('checked', false);
                $('#OrderList tbody tr').removeClass('Clicked');
                Orders.map(function (x) {
                    x.Chk = 0;
                });
            }
        });

        $(document).on("click", ".rowCheckbox", function () {

            var row = $(this).closest("tr");
            idx = $(row).index(); var findidx = 0;
            var orderid = row.find('.class_orderid').text();

            findidx = Orders.findIndex(function (y) {
                return (y.OrderNo == orderid);
            });

            if (this.checked) {
                row.addClass('Clicked');
                Orders[findidx].Chk = "1";

            }
            else {
                row.removeClass('Clicked');
                Orders[findidx].Chk = "0";
            }

        });

        $(document).on("click", ".btnAllPendingOrderSave", function () {

            var count_chk = 0; var arr = [];

            var Invoice_Date = $('#Inv_date').val();

            $(Orders).each(function (i) {

                if (Orders[i].Chk == 1) { count_chk++; };

                if (Orders[i].Status == "Pending" && Orders[i].Chk == '1') {
                    arr.push({
                        OrderNum: Orders[i].OrderNo
                    })
                }
            });

            if (count_chk > 0) {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "myOrders.aspx/AllPendingOrder",
                    data: "{'OrderNo':'" + JSON.stringify(arr) + "','Inv_Date':'" + Invoice_Date + "'}",
                    dataType: "json",
                    success: function (data) {
                        if (data.d == "success") {
                            $.confirm({
                                title: 'Success!',
                                content: 'Secondary Order Submitted Successfully!',
                                type: 'green',
                                typeAnimated: true,
                                autoClose: 'action|8000',
                                icon: 'fa fa-check fa-2x',
                                buttons: {
                                    tryAgain: {
                                        text: 'OK',
                                        btnClass: 'btn-green',
                                        action: function () {
                                            loadData()
                                        }
                                    }
                                }
                            });
                            // rjalertfn('Success!', 'Invoiced Successfully..', 'success', loadData());
                            // alert("Invoiced Successfully");
                            //loadData();
                        }
                        else {
                            alert(data.d);
                        }
                    },
                    error: function (result) {
                    }
                });
            }
            else {
                rjalert('Error!', 'Please Select Atlease One Order.', 'error');
                //alert('Please Select Atlease One Order');
            }
        });

        $(document).on("click", ".Spinner-Input", function () {
            $ix = $(this).find(".Spinner-Modal");
            $dsp = $($ix).hasClass("open");
            $(".Spinner-Modal").removeClass("open");
            if ($dsp == false)
                $($ix).addClass("open");
        })

        $(document).on("click", ".Spinner-Modal>ul>li", function () {
            $vl = $(this).attr('val');
            $(this).closest(".Spinner-Input").find(".Spinner-Value").html($(this).html())
        })

        $(document).on('keypress', '.txtQty', function (e) {
            if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                return false;
            }
        });

        function resetOrderEntry() {
            $("#ordDate").val(new Date().toLocaleDateString('en-GB'));
        }
        function rjalertfn(titles, contents, types, func) {
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
                            func();
                        }
                    }
                }
            });
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

        $(document).on("click", ".btnMyOrderSave", function () {

            var row = $(this).closest("tr");
            var OrderId = row.find('.class_orderid').text();
            var status = row.find('.status').text();
            var Invoice_Date = $('#Inv_date').val();

            if (row.find('.Flag').val() == '0' || row.find('.Flag').val() == 'null') {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "myOrders.aspx/ChangeOrder",
                    data: "{'OrderNo':'" + OrderId + "','Invoice_Date':'" + Invoice_Date + "'}",
                    dataType: "json",
                    success: function (data) {
                        if (data.d.length > 0) {
                            if (data.d == "No_stock") {
                                rjalert('Error!', 'Stock Not Available.', 'error');
                                return false;
                                //alert('Stock Not Available.'); 
                            }
                            $.confirm({
                                title: 'Success!',
                                content: 'Secondary Order Invoiced Successfully!',
                                type: 'green',
                                typeAnimated: true,
                                autoClose: 'action|8000',
                                icon: 'fa fa-check fa-2x',
                                buttons: {
                                    tryAgain: {
                                        text: 'OK',
                                        btnClass: 'btn-green',
                                        action: function () {
                                            loadData()
                                        }
                                    }
                                }
                            });
                            //rjalertfn('Success!', 'Invoiced Successfully..', 'success', loadData());
                            //alert("Successfully Invoiced");
                            //loadData();
                        }
                        else {
                            alert(data.d);
                        }
                    },
                    error: function (result) {
                        approve = 0;
                    }
                });
            }
            else {
                rjalert('Error!', 'Already ' + status + '!.', 'error');
                // alert('Already ' + status);
            }

        });

        function ReloadTable(typ) {
            var tota = 0;
            $("#OrderList TBODY").html("");

            if (filtrkey != "All") {
                Orders = Orders.filter(function (a) {
                    return a.Order_Flag == filtrkey;
                })
            }
            if (typ == 1) { st = 0; } else { st = PgRecords * (pgNo - 1); slno = 0; }

            for ($i = st; $i < st + PgRecords; $i++) {
                if ($i < Orders.length) {
                    tr = $("<tr></tr>");
                    slno = $i + 1;
                    $(tr).html("<td><input type='hidden' class='Flag' value=" + Orders[$i].Order_Flag + "><input " + ((Orders[$i].Chk == '1') ? 'checked' : '') + " type='checkbox' class='rowCheckbox' id='SelectedCheckbox'/></td><td>" + slno + "</td><td class='class_orderid'>" + Orders[$i].OrderNo + "</td><td>" + Orders[$i].Order_Date_Disp + "</td><td>" + Orders[$i].OrdTaken_by + "</td><td>" + Orders[$i].Retail + "</td><td style='display:none;'>" + Orders[$i].Stockist_Name + "</td><td class='status'>" + Orders[$i].Status + "</td><td style='text-align:right;'>" + Orders[$i].Order_Value.toFixed(2) + "</td><td style='text-align:right; cursor: pointer;'><a href='#' class ='" + ((Orders[$i].Order_Flag == 1 || Orders[$i].Order_Flag == 2 || Orders[$i].Order_Flag == 3) ? 'edit_class' : 'edit_class1') + "' id='Btn_Edit' onclick='Edit_Screen(\"" + Orders[$i].OrderNo + "\",\"" + stockist_Code + "\",\"" + Div_Code + "\",\"" + Orders[$i].Order_Flag + "\" ,\"" + Orders[$i].Cust_Code + "\" ,\"" + Orders[$i].Order_Date + "\" )'</a>Edit</td ><td style='text-align:right'><a id='myButton' href='#' onClick='popup(\"" + Orders[$i].OrderNo + "\",\"" + stockist_Code + "\",\"" + Div_Code + "\",\"" + Orders[$i].Status + "\",\"" + fdt + "\",\"" + tdt + "\")'> <span class='glyphicon glyphicon-eye-open'></span></a></td><td style='text-align: right'><a href='#' onClick='PrintIView(\"" + Orders[$i].OrderNo + "\",\"" + stockist_Code + "\",\"" + Div_Code + "\",\"" + Orders[$i].Cust_Code + "\",\"" + fdt + "\",\"" + tdt + "\")'><span class='glyphicon glyphicon-print'></span></a></td><td class='cancelOrder' style='text-align:right'><a href='#'><span class='glyphicon glyphicon-remove' onClick='cancel(\"" + Orders[$i].OrderNo + "\",\"" + stockist_Code + "\",\"" + Div_Code + "\",\"" + Orders[$i].Status + "\")'></span></a></td><td><button type='button' class='btn btn-primary btnMyOrderSave' id='btnMyOrderId' style='display: inline - block;'>Save</button></td>");
                    tota = parseFloat(Orders[$i].Order_Value || 0) + parseFloat(tota)
                    $("#OrderList TBODY").append(tr);
                    if (Orders[$i].Order_Flag == '1') { $('#OrderList tbody tr').find('.rowCheckbox').attr("disabled", true); }

                    if (Orders[$i].Status == 'Pending') {
                        $("td:contains('Pending')").addClass('red');
                    }
                    else if (Orders[$i].Status == 'Invoiced') {
                        $("td:contains('Invoiced')").addClass('green');
                    }
                }
            }
            $("#OrderList TFOOT").html("<tr><td colspan='6' style='font-weight: bold;'>Total</td><td style='text-align: right; font-weight: bold;'>" + tota.toFixed(2) + "</td><td></td><td></td><td></td></tr>");
            $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")
            $(".ddlStatus>li>a").on("click", function () {

                cStus = $(this).closest("td").find(".aState");
                stus = $(this).attr("v");
                order_id = $(this).closest("tr").find(".class_orderid").text();

                var fi_idx = Orders.findIndex(function (u) {
                    return (u.OrderNo == order_id);
                })
                cStusNm = $(this).text();
                cnfrmmsg = '';
                if (stus == 0) { cnfrmmsg = "Do you want to active the order?"; }
                else if (stus == 2) { cnfrmmsg = "Do you want to cancel the order ?"; }
                if (Orders[fi_idx].Status == "Invoiced") {
                    rjalert('Error!', 'Already Order Invoiced Cannot Change the Status', 'error');
                    //alert("Already Order Invoiced Cannot Change the Status");
                }
                else if (Orders[fi_idx].Status == "Pending" && stus == 0) {
                    rjalert('Error!', 'Already order is in pending', 'error');
                    //alert("Already order is in pending");
                }
                else if (Orders[fi_idx].Status == "Cancelled" && stus == 2) {
                    rjalert('Error!', 'Already order is Cancelled', 'error');
                    //alert("Already order is Cancelled");
                }
                else if (confirm(cnfrmmsg)) {
                    sf = Orders[fi_idx].OrderNo;
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "myOrders.aspx/SetNewStatus",
                        data: "{'SF':'" + sf + "','stus':'" + stus + "'}",
                        dataType: "json",
                        success: function (data) {

                            if (data.d == 1) {

                                if (stus == 0) {
                                    cStusNm = "Pending";
                                }
                                else if (stus == 2) {
                                    cStusNm = "Cancelled";
                                }
                                Orders[fi_idx].Status = cStusNm;
                                Orders[fi_idx].Order_Flag = stus;
                                $(cStus).html(cStusNm);

                                ReloadTable(0);
                                rjalert('Error!', 'Status Changed Successfully...', 'error');
                                //alert('Status Changed Successfully...');
                            }
                            else {
                                rjalert('Error!', 'Status Not Changed Successfully...', 'error');
                                // alert('Status Not Changed Successfully...');
                            }
                        },
                        error: function (result) {
                        }
                    });
                }
            });
            loadPgNos();
        }
        function cancel(orderid, stockist, div, Status) {
            if (Status == 'Cancelled')
                return false
            if (Status == 'Invoiced') {
                $.alert('Alreadly Invoiced!');
                return false;
            }
            if (orderid != '' || stockist != '' || div != '') {
                $.confirm({
                    title: 'Confirm!',
                    content: 'Do you want to cancel this order <b style="color:red">' + orderid + '</b>!',
                    type: 'red',
                    typeAnimated: true,
                    autoClose: 'cancel|8000',
                    icon: 'fa fa-warning',
                    buttons: {
                        tryAgain: {
                            text: 'Confirm',
                            btnClass: 'btn-red',
                            action: function () {
                                cancelorder(orderid, stockist, div);

                            }
                        },
                        cancel: function () {
                            //$.alert('Time Expired!');
                        }
                        //somethingElse: {
                        //    text: 'Something else',
                        //    btnClass: 'btn-blue',
                        //    keys: ['enter', 'shift'],
                        //    action: function () {
                        //        $.alert('Something else?');
                        //    }
                        //}
                    }
                });
            }
        }
        function cancelorder(orderid, stockist, div) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "myOrders.aspx/cancelorder",
                data: "{'orderid':'" + orderid + "','stockist':'" + stockist + "','div':'" + div + "'}",
                dataType: "json",
                success: function (data) {

                    if (data.d > 1) {

                        $.confirm({
                            title: 'Confirm!',
                            content: 'Order Cancelled Successfully!',
                            type: 'green',
                            typeAnimated: true,
                            autoClose: 'Ok|5000',
                            icon: 'fa fa-check',
                            buttons: {
                                Ok: {
                                    text: 'OK',
                                    btnClass: 'btn-blue',
                                    action: function () {
                                        loadData();

                                    }
                                }
                            }
                        });


                        //$.alert('Order Cancelled Successfully!');
                        //location.reload();
                    }
                    else
                        $.alert('Something Went Wrong!');

                },
                error: function (result) {
                }
            });
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

        function Edit_Screen(id, stk, div, flag, cust, order_date) {

            if (flag == 1) {
                rjalert('success!', 'Order Invoiced', 'success');
                //alert('Order Invoiced');
                return false;
            }
            else {
                var url = "../Stockist/MyOrder_Edit1.aspx?Order_No=" + id + "&Stockist_Code=" + stk + "&Div_Code=" + div + "&Cust_Code=" + cust + "&Order_Date=" + order_date + ""
                window.open(url, '_self');
            }
        }

        function popup(orderid, stk, div, sts, fdt, tdt) {

            set_local_storage();
            var url = "../Stockist/My_Order_View.aspx?Order_No=" + orderid + "&Stockist_Code=" + stk + "&Div_Code=" + div + "&Status=" + sts + "&FDATE=" + fdt + "&TDATE=" + tdt + ""
            window.open(url, '_self');
        }

        $(".segment>li").on("click", function () {
            $(".segment>li").removeClass('active');
            $(this).addClass('active');
            filtrkey = $(this).attr('data-va');
            set_order();
            $("#tSearchOrd").val('');
            ReloadTable(0);
        });

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
                set_order();
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
            else {
                set_order();
            }
            ReloadTable(1);
        }

        function set_order() {

            if ($('#rtname option:selected').val() == 0) {
                Orders = AllOrders
            }
            else {
                Orders = AllOrders.filter(function (a) {
                    return a.Route == $('#rtname option:selected').val();
                });
            }
        }

        function loadData() {

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "myOrders.aspx/GetOrders",
                data: "{'stk':'<%=Session["Sf_Code"]%>','FDt':'" + fdt + "','TDt':'" + tdt + "'}",
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

        function loadmodal() {

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                data: "{'Div_Code':'" + Div_Code + "','Stockist_Code':'" + stockist_Code + "'}",
                url: "myOrders.aspx/getratenew",
                dataType: "json",
                success: function (data) {
                    Allrate = JSON.parse(data.d) || [];
                },
                error: function (result) {
                }
            });

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "myOrders.aspx/Get_Product_unit",
                dataType: "json",
                success: function (data) {
                    All_unit = JSON.parse(data.d) || [];
                },
                error: function (result) {
                }
            });

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "myOrders.aspx/Get_Product_Tax",
                dataType: "json",
                success: function (data) {
                    All_Tax = JSON.parse(data.d) || [];
                },
                error: function (result) {
                }
            });

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "myOrders.aspx/GetProducts",
                data: "{'div':'<%=Session["Division_Code"]%>'}",
                dataType: "json",
                success: function (data) {
                    All_Product_Details = JSON.parse(data.d) || [];
                    Product_Details = All_Product_Details;
                    Prds += "<option  selected='selected' value='0'>Select Product</option>";
                    for ($k = 0; $k < Product_Details.length; $k++) {
                        Prds += "<option value='" + Product_Details[$k].Product_Detail_Code + "'>" + Product_Details[$k].Product_Detail_Name + "</option>";
                    }
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "myOrders.aspx/bindretailer",
                data: "{'stk':'<%=Session["Sf_Code"]%>','Div':'<%=Session["Division_Code"]%>'}",
                dataType: "json",
                success: function (data) {
                    All_Retailer = JSON.parse(data.d) || [];
                    //for (var i = 0; i < All_Retailer.length; i++) {
                    //    $('#recipient-name').append($("<option></option>").val(All_Retailer[i].ListedDrCode).html(All_Retailer[i].ListedDr_Name1)).trigger('chosen:updated').css("width", "100%");
                    //}
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
            // $('#recipient-name').chosen();

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "myOrders.aspx/Get_Product_Cat_Details",
                dataType: "json",
                success: function (data) {
                    Cat_Details = JSON.parse(data.d) || [];
                    $('#myDIV').html('');
                    for (var g = 0; g < Cat_Details.length; g++) {
                        $('#myDIV').append('<button class = "' + ((Cat_Details[g].Product_Cat_Code == -1) ? "btn category active" : "btn category") + '" value="' + Cat_Details[g].Product_Cat_Code + '" >' + Cat_Details[g].Product_Cat_Name + '</button>');
                    }
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "myOrders.aspx/Get_Route",
                dataType: "json",
                success: function (data) {
                    All_Route = JSON.parse(data.d) || [];
                    var route = $("#route-name"); var route = $("#rtname");
                    route.empty().append('<option selected="selected" value="0">Select Route</option>');
                    $("#route-name").empty().append('<option selected="selected" value="0">Select Route</option>');
                    for (var i = 0; i < All_Route.length; i++) {
                        route.append($('<option value="' + All_Route[i].Territory_Code + '">' + All_Route[i].Territory_Name + '</option>'));
                        $("#route-name").append($('<option value="' + All_Route[i].Territory_Code + '">' + All_Route[i].Territory_Name + '</option>'));
                    }
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
            $('#route-name').selectpicker({
                liveSearch: true
            });

            $('#rtname').selectpicker({
                liveSearch: true
            });

            //$.ajax({
              //  type: "POST",
                //contentType: "application/json; charset=utf-8",
                //async: false,
                //url: "myOrders.aspx/GetCustWise_price",
                //dataType: "json",
                //success: function (data) {
                  //  Cust_Price = JSON.parse(data.d) || [];
                //},
                //error: function (result) {
                  //  alert(JSON.stringify(result));
              //  }
            //});
        }

        $('#rtname').on('change', function () {

            var selctd_rt = $('#rtname option:selected').val();
            var selctd_rt_name = $('#rtname option:selected').text();
            $("#tSearchOrd").val('');
            Orders = AllOrders;
            if (selctd_rt != 0) {
                Orders = Orders.filter(function (u) {
                    return (u.Route == selctd_rt);
                });
            }
            ReloadTable(0);
        });

        $('#route-name').on('change', function () {

            var selected_route = $(this).val();
            var filtered_retailer = [];

            $('#recipient-name').empty();
            $('#recipient-name').selectpicker('destroy');
            $('#addressid').text('');

            filtered_retailer = All_Retailer.filter(function (k) {
                return (k.Territory_Code == selected_route);
            });

            $('#recipient-name').append("<option selected='selected' value='0'>Select Retailer Name</option>");
            for (var i = 0; i < filtered_retailer.length; i++) {
                $('#recipient-name').append($('<option value="' + filtered_retailer[i].ListedDrCode + '">' + filtered_retailer[i].ListedDr_Name1 + '</option>'));
            }

            $('#recipient-name').selectpicker({
                liveSearch: true
            });
        });


        $('#recipient-name').on('change', function () {
            var cusCode = $(this).val();
            var sPCd = $('.ddlProd').val();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                data: "{'retailercode':'" + cusCode + "'}",
                url: "myOrders.aspx/GetWise_price",
                dataType: "json",
                success: function (data) {
                    Cust_Price = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
            if (Cust_Price.length > 0) {
                Prod = Cust_Price.filter(function (a) {
                    return (a.Product_Detail_Code == sPCd);
                })

                if (Prod.length == 0) {
                    /*alert('No rate Available');*/

                    Prod = Allrate.filter(function (a) {
                        return (a.Product_Detail_Code == sPCd);
                    })
                }

                var Filtered_retailer = [];

                if (cusCode != "0" && cusCode != '') {

                    Filtered_retailer = All_Retailer.filter(function (a) {
                        return a.ListedDrCode == cusCode
                    });

                    $('#addressid').text(Filtered_retailer[0].Addres);
                    Retailer_State = Filtered_retailer[0].State_Code;
                    Rate_List_Code = Filtered_retailer[0].List_Code

                    if (Filtered_retailer[0].Tcs != 0) {
                        Retailer_Extra_Tax_Type = 'TCS';
                        Extra_Tax = Filtered_retailer[0].Tcs;
                    }
                    else if (Filtered_retailer[0].Tds != 0) {
                        Retailer_Extra_Tax_Type = 'TDS';
                        Extra_Tax = Filtered_retailer[0].Tds;
                    }

                    $("#OrderEntry > TBODY").html("");
                    NewOrd = []; resetSL(); ReCalc(); AddRow(1);
                }
                else {
                    $('#addressid').html('');
                }
            }
            else {
                rjalert('Attention!', 'Please contact the admin regarding the unavailability of order acceptance. The price for this retailer is not provided', 'error');
                $('#recipient-name').selectpicker('val', '0');
                return false;
            }
        });

        $(document).on("click", ".category", function (e) {
            e.preventDefault();
            $('.category').removeClass('active');
            $('.category').css('color', 'black');
            $(this).addClass('active');
            $(this).css('color', 'white');
        });

        function AddRow(type) {
            var ckselect = 0
            $('.ddlProd').each(function () {
                var rr = $(this).find('option:selected').val();
                if (rr == '-1') {
                    ckselect += 1;
                    rjalert('Error!', 'Select a Product', 'error');
                    return false;
                }
            })
            if (ckselect == 0) {
                itm = {}
                itm.PCd = ''; itm.sPCd = ''; itm.s_pcode = ''; itm.mrp = ''; itm.PName = ''; itm.Unit = ''; itm.Rate = "0";itm.Rate_in_peice = "0"; itm.Qty = 1; itm.Qty_c = 1; itm.Free = "0"; itm.Discount = "0"; itm.Dis_value = "0"; itm.Total_Tax = 0;
                itm.Tax_details = ''; itm.Tax = "0"; itm.Tax_value = "0"; itm.Total = "0"; itm.Gross_Amt = "0"; itm.Sub_Total = 1; itm.of_Pro_Code = ''; itm.of_Pro_Name = ''; itm.of_Pro_Unit = ''; itm.umo_unit = ''; itm.con_fac = '';
                currentheight = $('.modal-content').height();
                NewOrd.push(itm);

                tr = "<tr class='subRow'>";
                tr += "<td class='rowno'><label style='margin-bottom:0px;'><input type='checkbox' class='slitm'> <span class='rwsl'>" + ($("#OrderList > TBODY > tr").length + 1) + "</span></label></td><td class='pro_td' style='width:27%;padding: 9px 0px 0px 0px;'><select  class='ddlProd ' data-width='100%'>" + Prds + "</select><div class='second_row_div' style='display:none; font-size:11.5px;padding: 5px 0px 0px 3px;'></div></td><td style='display:none;' ><input type='hidden' class='sale_code' /></td><td id='Td1' style='width: 18%;padding: 8px 0px 0px 30px;'><select class='cbAlwTyp ispinner'><option value='1'>Select</option></select><div class='Spinner-Input'><div style='display:none;' class='spinner-Value_val'></div><div class='Spinner-Value'>Select</div><div class='Spinner-Modal'><ul>Select</ul></div></div><input type='text' name='txqty' id='txqty' autocomplete='off' class='txtQty validate' pval='0.00' value style='text-align:right;width: 42%;'></td><td class='tdRate' style='text-align:right; padding: 17px 0px 0px 0px; font-weight: bold;'>0.00</td><td class='fre' style='text-align:right;padding: 17px 9px 0px 0px;font-weight: bold;'>0</td><td style='display:none;'<label name='free' class='fre'></lable></td><td style='display:none' ><input type='hidden' class='fre1'  name='fre1' ></td><td style='display:none' ><input type='hidden' class='of_pro_name'  name='of_pro_name' ></td><td style='display:none' ><input type='hidden' class='of_pro_code'  name='of_pro_code' ></td><td class='tddis_val' style='text-align:right; padding: 17px 11px 0px 0px;font-weight: bold;'>0</td><td style='display:none'><input type='hidden' class='disc_value'  name='disc_value' ></td><td style='display:none'><input type='hidden' class='disc_value'  name='disc_value' ></td><td class='tdtotal' style='text-align:right;padding: 17px 7px 0px 0px;font-weight: bold;'>0.00</td><td class='tdtax' style='text-align:right;padding: 17px 0px 0px 0px;font-weight: bold;'>0.00</td><td style='display:none;'><input type='hidden' class='tdcgst' id='tdcgst' /></td><td style='display:none;'><input type='hidden' class='tdsgst' id='tsgst' ></td><td style='display:none;'><input type='hidden' class='tdigst' id='tigst' ></td><td class='tdAmt' style='text-align:right;padding: 17px 6px 0px 0px;width: 10%;font-weight: bold;'>0.00</td><td style='display:none;'><label id='pcode' class='pcode'></label></td><td style='display:none;' ><input type='hidden' class='erp_code' /></td></tr>";
                $("#OrderEntry > tbody").append(tr);
                resetSL();

                //$(".ddlProd").select2();
                $(".ddlProd").chosen();

                //  $('#OrderEntry tr:last').find(".ddlProd").on('chosen:showingdorpdown', function () {
                $('.ddlProd').chosen().on('chosen:showing_dropdown', function () {
                    // $('.ddlProd').on('select2:open', function () {
                    var selected_cat_code = $('.btn.active').val();
                    var selected_cat_Name = $('.btn.active').text();

                    var filtered_prd = Product_Details.filter(function (k) {
                        return (k.Product_Cat_Code == selected_cat_code);
                    });

                    $(this).html('');
                    filtered_prd = selected_cat_code == -1 ? All_Product_Details : filtered_prd;

                    let str = '<option value=-1>--select--</option>';
                    for (var h = 0; h < filtered_prd.length; h++) {
                        str += `<option value="${filtered_prd[h].Product_Detail_Code}">${filtered_prd[h].Product_Detail_Name}</option>`;
                    }
                    //$('#OrderEntry tr:last').find(".ddlProd").html(str);
                    //$('#OrderEntry tr:last').find(".ddlProd").trigger("chosen:updated");
					 $(this).closest('tr').find(".ddlProd").html(str);
					 $(this).closest('tr').find(".ddlProd").trigger("chosen:updated");

                });

                if (type == 1) {
                    event.stopPropagation();
                    $('#OrderEntry tr:last').find(".ddlProd").trigger('chosen:open');
                }

                $(".ddlProd").on("change", function () {
                    var retcode = $('#recipient-name :selected').val();
				    var rotnm = $('#route-name :selected').val();
					if (rotnm == "" || rotnm == null || rotnm == "0") {
                        approve = 0;
                        rjalert('Error!', 'Select Route And Retailer Name.', 'error');
                        $('.ddlProd').val('');
                        // $('.ddlProd ').select2("destroy");
                        $('.ddlProd ').chosen("destroy");
                        $('.ddlProd').chosen();
                        //alert('Select a Customer Name');
                        return false;
                    }
                    if (retcode == "" || retcode == null) {
                        approve = 0;
                        rjalert('Error!', 'Select a Retailer Name.', 'error');
                        $('.ddlProd').val('');
                        // $('.ddlProd ').select2("destroy");
                        $('.ddlProd ').chosen("destroy");
                        $('.ddlProd').chosen();
                        //alert('Select a Customer Name');
                        return false;
                    }
                    if ($(this).val() != -1) {

                        itr = $(this).closest('tr');
                        idx = $(itr).index();
                        var selected_retailer_code = $('#recipient-name').val();

                        if (selected_retailer_code != '' && selected_retailer_code != 0 && selected_retailer_code != '--- Select Retailer Name---') {

                            $('.second_row_div').show();
                            sPCd = $(this).val();
                            $(this).closest("tr").attr('class', $(this).val());
                            var P_Name = itr.find('.ddlProd').find('option:selected').text();
                            if (Cust_Price[0].type == 0) {
                                Prod = Allrate.filter(function (a) {
                                    return (a.Product_Detail_Code == sPCd);
                                })
                            }

                            else {
                                Prod = Cust_Price.filter(function (a) {
                                    return (a.Product_Detail_Code == sPCd);
                                })
                            }


                            if (Prod.length == 0) {
                                rjalert('error!', 'Rate not available', 'error');
                                //alert('Rate not available')
                                //    Prod = Allrate.filter(function (a) {
                                //        return (a.Product_Detail_Code == sPCd);
                                //    })
                            }

                            rt = parseFloat($(itr).find('.tdRate').text()); if (isNaN(rt)) rt = 0; qt = parseFloat($(itr).find('.txtQty').val()); if (isNaN(qt)) qt = 0; //tax = parseFloat(Prod[0].Tax_Val || 0);
			    qt=0;
                            NewOrd[idx].PCd = sPCd;
                            NewOrd[idx].sPCd = $(this).val();
                            NewOrd[idx].s_pcode = Prod[0].Sale_Erp_Code;
                            NewOrd[idx].Unit = Prod[0].product_unit;
                            NewOrd[idx].PName = P_Name;
                            NewOrd[idx].Qty = qt;
                            NewOrd[idx].Free = 0;

                            var filter_unit = []; units = ""; units1 = ""; var tax_filter = []; var tax_arr = [];

                            filter_unit = All_unit.filter(function (w) {
                                return (sPCd == w.Product_Detail_Code);
                            });
                            if (filter_unit.length > 0) {

                                //for (var z = 0; z < filter_unit.length; z++) {
                                //    if (filter_unit[z].Move_MailFolder_Id == Prod[0].Unit_code) {
                                //        units += "<li id='s' name='itms' value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</li>";
                                //        units1 += "<option value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";
                                //    }

                                //    else if (filter_unit[z].Move_MailFolder_Id == Prod[0].Base_Unit_code) {
                                //        units += "<li id='s' name='itms' value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</li>";
                                //        units1 += "<option value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";

                                //    }

                                //}
                                if (Prod.length > 0) {
                                    var unitrate = 0;
                                    var Rate_in_peice = 0;
                                    for (var z = 0; z < filter_unit.length; z++) {
                                        if (filter_unit[z].Move_MailFolder_Id == Prod[0].Base_Unit_code) {
                                            unitrate = Prod[0].RP_Base_Rate;
                                            Rate_in_peice = Prod[0].RP_Base_Rate;
                                            unitconfac = filter_unit[z].Sample_Erp_Code;
                                            units += "<li id=" + unitrate + " name='itms'  conf=" + unitconfac + " Rate_in_peice=" + Rate_in_peice+"   value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</li>";
                                            units1 += "<option  id=" + unitrate + "   conf=" + unitconfac + " Rate_in_peice=" + Rate_in_peice +"  value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";
                                        }
                                        //else if (filter_unit[z].Move_MailFolder_Id != Prod[0].UnitCode && filter_unit[z].Move_MailFolder_Id == Prod[0].Base_Unit_code) {
                                        //    unitrate = Prod[0].RP_Base_Rate / Prod[0].Sample_Erp_Code
                                        //    units += "<li id=" + unitrate + " name='itms' value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</li>";
                                        //    units1 += "<option  id=" + unitrate + " value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";
                                        //}

                                        else {
                                            unitrate = Prod[0].RP_Base_Rate * filter_unit[z].Sample_Erp_Code
                                            unitconfac = filter_unit[z].Sample_Erp_Code;
                                            Rate_in_peice = Prod[0].RP_Base_Rate;
                                            units += "<li id=" + unitrate + "  Rate_in_peice=" + Rate_in_peice +" conf=" + unitconfac + "  name='itms' value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</li>";
                                            units1 += "<option  id=" + unitrate + " Rate_in_peice=" + Rate_in_peice +" conf=" + unitconfac + "  value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";

                                        }

                                        //else if (filter_unit[z].Move_MailFolder_Id == Prod[0].Base_Unit_code) {
                                        //    unitrate = Prod[0].PTS / Prod[0].Sample_Erp_Code
                                        //    units += "<li id=" + unitrate + " name='itms' value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</li>";
                                        //    units1 += "<option  id=" + unitrate + " value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";

                                        //}

                                    }
                                }
                            }
                            //if (filter_unit.length > 0) {

                            //  for (var z = 0; z < filter_unit.length; z++) {
                            //      units += "<li id='s' name='itms' value='" + filter_unit[z].Move_MailFolder_Id + "'>" + //filter_unit[z].Move_MailFolder_Name + "</li>";
                            //      units1 += "<option value='" + filter_unit[z].Move_MailFolder_Id + "'>" + //filter_unit[z].Move_MailFolder_Name + "</option>";
                            //}
                            // }

                            if (Dist_state_code == Retailer_State || Retailer_State == 0) { var type = 0; } else { var type = 1; }

                            tax_filter = All_Tax.filter(function (r) {
                                return (r.Product_Detail_Code == sPCd) //&& (r.Tax_Method_Id == type || r.Tax_Method_Id == 2)
                            });
                            $(itr).find('.second_row_div').text('');
                            $(itr).find('.second_row_div').append("<i class='fa fa-tags'></i><label>Dis % :</label>&nbsp;&nbsp;<label class='dis_val_class' id='dis_val_class'></label>&nbsp;&nbsp;<i class='fa fa-stack-overflow' ></i><label>Stock :</label>&nbsp;&nbsp;<label class='stockClass' id='stock_id'></label>&nbsp;&nbsp;<i class='fa fa-code' ></i><label>Con_Fac :</label>&nbsp;&nbsp;<label id='Con_fac' class='Con_fac'></label>&nbsp;&nbsp;");

                            var append = ''; var total_tax_per = 0;

                            if (tax_filter.length > 0) {

                                for (var z = 0; z < tax_filter.length; z++) {

                                    append += "<label class='lbl_tax_type'>" + tax_filter[z].Tax_Type + "</label>:&nbsp;&nbsp;<label class='Tax_name' id='Tax_name'>" + tax_filter[z].Tax_name + "</label>&nbsp;&nbsp;";
                                    var Push_data = tax_filter[z].Tax_Type;
                                    total_tax_per = total_tax_per + parseFloat(tax_filter[z].Tax_Val);
                                    NewOrd[idx][Push_data] = tax_filter[z].Tax_Val;

                                    tax_arr.push({
                                        pro_code: sPCd,
                                        Tax_Code: tax_filter[z].Tax_Id,
                                        Tax_Name: tax_filter[z].Tax_Type,
                                        Tax_Amt: 0,
                                        Tax_Per: tax_filter[z].Tax_Val,
                                        umo_code: 0
                                    });
                                }
                                NewOrd[idx].Tax_details = tax_arr;
                                NewOrd[idx].Total_Tax = total_tax_per;
                                $(itr).find('.second_row_div').append(append);
                            }
                            if (parseInt(Prod[0].TotalStock) > 0) {
                                $(itr).find('.stockClass').css("color", "rgb(39 194 76)");
                            }
                            else {
                                $(itr).find('.stockClass').css("color", "red");
                            }
                            $(itr).find('.stockClass').text(Prod[0].TotalStock);
                            $(itr).find('.stockClass').val(Prod[0].TotalStock);
                            $(itr).find('.dis_per').text("0");
                            $(itr).find('.sale_code').html(Prod[0].Sale_Erp_Code);
                            $(itr).find('.tdAmt').html((qt * rt).toFixed(2));
                            $(itr).find('.erp_code').val(Prod[0].Sample_Erp_Code);
                            $(itr).find('.dis_val_class').text(0);
                            $(itr).find('.Con_fac').text(Prod[0].Sample_Erp_Code);

                            var set = 0;

                            $(document).find('.' + sPCd).each(function () {

                                var rowww = $(this).closest('tr');
                                var indxx = $(rowww).index();

                                var Unit_length = Prod.length;
                                var row_length = $('#OrderEntry tbody').find('.' + sPCd).length;

                                var Pre_pro = $(this).closest("tr").attr('Prev_Prod_Code');

                                if (Pre_pro == sPCd) {
                                    var row_unit = rowww.find('.Spinner-Value').text();
                                    var row_unit_Code = rowww.find('.spinner-Value_val').attr('value');
                                }
                                else {
                                    var row_unit = "";
                                    var row_unit_Code = "";
                                }
                                $(itr).closest("tr").attr('Prev_Prod_Code', sPCd);
                                if (filter_unit.length >= row_length) {

                                    if (row_unit_Code == "" || row_unit_Code == undefined) {

                                        $(itr).find('#Td1').html("<select class='cbAlwTyp ispinner'><option value='1'>Select</option>" + units1 + "</select><div class='Spinner-Input'><div style='display:none;' class='spinner-Value_val' value=" + Prod[0].Unit_code + "></div><div class='Spinner-Value' umo='" + Prod[0].Unit_code + "'>" + Prod[0].product_unit + "</div><div class='Spinner-Modal'><ul>" + units + "</ul></div></div><input type='text' autocomplete='off' id='txtQty' class='txtQty validate' pval='0.00' value='" + ((qt == 0) ? '' : qt) + "' style='text-align:right;width: 42%;'>");
                                        setTimeout(function () { $(itr).find('#Td1').find('.txtQty').focus() }, 100);
                                        if (Prod[0].Unit_code == Prod[0].UnitCode)
                                            var unt_rate = Prod[0].RP_Base_Rate;
                                        else
                                            var unt_rate = Prod[0].RP_Base_Rate * Prod[0].Sample_Erp_Code;
                                        if (unt_rate == 'null' || typeof unt_rate == "undefined" || unt_rate == "" || isNaN(unt_rate)) { unt_rate = 0; }

                                        $(itr).find('.tdRate').text(parseFloat(unt_rate).toFixed(2));
                                        NewOrd[idx].Rate = unt_rate;
                                        NewOrd[idx].Rate_in_peice = Prod[0].RP_Base_Rate;

                                        NewOrd[idx].mrp = Prod[0].MRP_Rate;
                                        $(itr).find('.Con_fac').text(Prod[0].Sample_Erp_Code);
                                        NewOrd[idx].con_fac = Prod[0].Sample_Erp_Code;
                                        NewOrd[idx].umo_unit = Prod[0].Unit_code;

                                    }
                                    else {
                                        for (var z = 0; z < filter_unit.length; z++) {
                                            if (filter_unit[z].Move_MailFolder_Id == row_unit_Code) {
                                                $(itr).find('#Td1').html("<select class='cbAlwTyp ispinner'><option value='1'>Select</option>" + units1 + "</select><div class='Spinner-Input'><div style='display:none;' class='spinner-Value_val' value='0'></div><div class='Spinner-Value'>Select</div><div class='Spinner-Modal'><ul>" + units + "</ul></div></div><input type='text' autocomplete='off' id='txtQty' class='txtQty validate' pval='0.00' value='" + ((qt == 0) ? '' : qt) + "' style='text-align:right;width: 42%;'>");
                                                //        setTimeout(function () { $(itr).find('#Td1').find('.txtQty').focus() }, 100);
                                                //        
                                                //row.find('.unit').append(units1);
                                                //var unt_rate = Pro_filter[0].RP_Base_Rate * Pro_filter[0].Sample_Erp_Code;
                                                //if (unt_rate == 'null' || typeof unt_rate == "undefined" || unt_rate == "" || isNaN(unt_rate)) { unt_rate = 0; }

                                                //row.find('#Price_').val(unt_rate.toFixed(2));
                                                //multivalue[indx].Rate = unt_rate;
                                            }
                                        }
                                    }
                                    //else if (Prod[0].Unit_code == row_unit_Code) {

                                    //    if (indxx != idx) {

                                    //        $(itr).find('#Td1').html("<select class='cbAlwTyp ispinner'><option value='1'>Select</option>" + units1 + "</select><div class='Spinner-Input'><div style='display:none;' class='spinner-Value_val' value=" + Prod[0].Base_Unit_code + "></div><div class='Spinner-Value'>" + Prod[0].Product_Sale_Unit + "</div><div class='Spinner-Modal'><ul>" + units + "</ul></div></div><input type='text' autocomplete='off' id='txtQty' class='txtQty validate' pval='0.00' value='" + ((qt == 0) ? '' : qt) + "' style='text-align:right;width: 42%;'>");
                                    //        setTimeout(function () { $(itr).find('#Td1').find('.txtQty').focus() }, 100);

                                    //        if (Prod[0].Unit_code == Prod[0].UnitCode)
                                    //            var unt_rate = Prod[0].RP_Base_Rate;
                                    //        else
                                    //            var unt_rate = Prod[0].RP_Base_Rate * Prod[0].Sample_Erp_Code;

                                    //        if (unt_rate == 'null' || typeof unt_rate == "undefined" || unt_rate == "" || isNaN(unt_rate)) { unt_rate = 0; }

                                    //        $(itr).find('.tdRate').text(parseFloat(unt_rate).toFixed(2));
                                    //        NewOrd[idx].Rate = unt_rate;
                                    //        NewOrd[idx].mrp = Prod[0].MRP_Rate;
                                    //        $(itr).find('.Con_fac').text(1);
                                    //        NewOrd[idx].con_fac = 1;
                                    //        NewOrd[idx].umo_unit = Prod[0].Base_Unit_code;
                                    //    }
                                    //    else {

                                    //        if (Prod[0].Base_Unit_code == Prod[0].UnitCode)
                                    //            var unt_rate = Prod[0].RP_Base_Rate;
                                    //        else
                                    //            var unt_rate = Prod[0].RP_Base_Rate / Prod[0].Sample_Erp_Code;
                                    //        //var unt_rate = Prod[0].RP_Base_Rate * Prod[0].Sample_Erp_Code;
                                    //        if (unt_rate == 'null' || typeof unt_rate == "undefined" || unt_rate == "" || isNaN(unt_rate)) { unt_rate = 0; }
                                    //        $(itr).find('.tdRate').text(parseFloat(unt_rate).toFixed(2));
                                    //    }
                                    //}
                                    //else if (Prod[0].Base_Unit_code == row_unit_Code) {
                                    //    if (indxx != idx) {
                                    //        $(itr).find('#Td1').html("<select class='cbAlwTyp ispinner'><option value='1'>Select</option>" + units1 + "</select><div class='Spinner-Input'><div style='display:none;' class='spinner-Value_val' value=" + Prod[0].Unit_code + "></div><div class='Spinner-Value'>" + Prod[0].product_unit + "</div><div class='Spinner-Modal'><ul>" + units + "</ul></div></div><input type='text' autocomplete='off' id='txtQty' class='txtQty validate' pval='0.00' value='" + ((qt == 0) ? '' : qt) + "' style='text-align:right;width: 42%;'>");
                                    //        setTimeout(function () { $(itr).find('#Td1').find('.txtQty').focus() }, 100);
                                    //        if (Prod[0].Unit_code == Prod[0].UnitCode)
                                    //            var unt_rate = Prod[0].RP_Base_Rate;
                                    //        else
                                    //            var unt_rate = Prod[0].RP_Base_Rate * Prod[0].Sample_Erp_Code;
                                    //        if (unt_rate == 'null' || typeof unt_rate == "undefined" || unt_rate == "" || isNaN(unt_rate)) { unt_rate = 0; }

                                    //        $(itr).find('.tdRate').text(parseFloat(unt_rate).toFixed(2));
                                    //        NewOrd[idx].Rate = unt_rate;
                                    //        NewOrd[idx].mrp = Prod[0].MRP_Rate;
                                    //        $(itr).find('.Con_fac').text(Prod[0].Sample_Erp_Code);
                                    //        NewOrd[idx].con_fac = Prod[0].Sample_Erp_Code;
                                    //        NewOrd[idx].umo_unit = Prod[0].Unit_code;

                                    //    }
                                    //    else {
                                    //        var unt_rate = Prod[0].RP_Base_Rate * Prod[0].Con_fac;
                                    //        if (unt_rate == 'null' || typeof unt_rate == "undefined" || unt_rate == "" || isNaN(unt_rate)) { unt_rate = 0; }

                                    //        $(itr).find('.tdRate').text(parseFloat(unt_rate).toFixed(2));
                                    //        NewOrd[idx].Rate = unt_rate;
                                    //        NewOrd[idx].mrp = Prod[0].MRP_Rate;
                                    //    }
                                    //}
                                    set = 0;
                                }
                                else {
                                    $(itr).find('.ddlProd').val('');
                                    $(itr).find('.ddlProd ').chosen("destroy");
                                    //$(itr).find('.ddlProd ').select2("destroy");
                                    $(itr).find('.ddlProd').chosen();
                                    // $(itr).find('.ddlProd').select2();
                                    $(itr).find('.Spinner-Value').text('Select');
                                    $(itr).find('.tdRate').text("0.00");
                                    $(itr).find('.txtQty').val('');
                                    $(itr).find('.second_row_div').text('');
                                    NewOrd[idx].Unit = 'Select';
                                    NewOrd[idx].umo_unit = '';
                                    CalcAmt($(itr));
                                    rjalert('error!', 'Product Units Already Selected', 'error');
                                    //alert('Product Units Already Selected');
                                    set = 1;
                                    return false;
                                }
                            });

                            if (set == 0) {

                                $(itr).find('.fre').attr("id", sPCd);
                                $(itr).find('.fre1').attr("id", sPCd);
                                $(itr).find('.tddis_val').text('0.00');

                                //getscheme(Prod[0].Sale_Erp_Code, '', '', $(itr).find('.Spinner-Value').text(), itr, $(itr).find('.fre1').text(), $(itr).find('.pcode').text(), idx, $(this).find('option:selected').text());

                                $(itr).find('.pcode').html(sPCd);
                                NewOrd[idx].Discount = $(itr).find('.dis_per').text() || 0;
                                CalcAmt(itr);
                            }
                        }
                        else {
                            rjalert('Error!', 'Please Select Retailer.', 'error');
                            //alert('Please Select Retailer');
                            $('.ddlProd').val('');
                            // $('.ddlProd ').select2("destroy");
                            $('.ddlProd ').chosen("destroy");
                            $('.ddlProd').chosen();
                            // $('.ddlProd').select2();
                        }
                        calc_stock($(this).closest('tr'));
                    }
                })
                new_height = $('.modal-content').height();
                scrollhightchnage = scrollhightchnage + ((new_height - currentheight) * 2);
                scrollhightchnageDel = scrollhightchnage;
                $('#exampleModal').scrollTop(scrollhightchnage);
            }
        }

        $(document).on("click", ".Spinner-Modal>ul>li", function () {

            var CQvalue = '';
            var umo = $(this).val();
            var ans = []; var pro_filter = [];
            var factor = $(this).text();
            var row = $(this).closest("tr");
            var unitval = $(this).attr('id');
            var Rate_in_peice = $(this).attr('Rate_in_peice');
            var conf = $(this).attr('conf');
            var umoval = $(this).val();
            idx = $(row).index();
            var pc = row.find('.ddlProd').find('option:selected').val();
            var pn = row.find('.ddlProd').find('option:selected').text();
            var qqty = row.find('.txtQty').val();
            if (qqty == 0 || qqty == "" || qqty == undefined) { qqty = 0; }
            qt = parseFloat($(row).find('.txtQty').val()); if (isNaN(qt)) qt = 0;
            var sdsd = row.find('.Spinner-Value').text();

            pro_filter = NewOrd.filter(function (s, key) {
                //return (s.PCd == pc && s.Unit == factor && key != idx);
                return (s.PCd == pc && s.umo_unit == umo && key != idx);

            });

            //ans = Allrate.filter(function (t) {
            //    return (t.Product_Detail_Code == pc && t.Move_MailFolder_Id == umo);
            //});

            ans = Cust_Price.filter(function (a) {
                return (a.Product_Detail_Code == pc);
            })

            if (pro_filter.length > 0) {

                row.find('.Spinner-Value').text('Select');
                row.find('.tdRate').text("0.00");
                row.find('.txtQty').val('');
                NewOrd[idx].Unit = 'Select';
                NewOrd[idx].umo_unit = '';
                CalcAmt(row);
                rjalert('Error!', 'Product Unit Already Selected.', 'error');
                //alert('Product Unit Already Selected');
            }
            else {

                NewOrd[idx].Unit = factor;

                //row.find('.Con_fac').text(ans[0].con_fac);
                //row.find('.erp_code').val(ans[0].con_fac);

                //NewOrd[idx].Unit = factor;
                //NewOrd[idx].Rate = ans[0].RP_Base_Rate * ans[0].con_fac;
                //NewOrd[idx].Qty_c = qqty;
                //NewOrd[idx].Qty = qqty * ans[0].con_fac;
                //NewOrd[idx].umo_unit = umo;
                //NewOrd[idx].con_fac = ans[0].con_fac;

                //sale_p_code = row.find('.sale_code').text();
                //ori_p_code = row.find('.ddlProd ').find('option:selected').val();
                //unit = $(this).text();
                //cq = row.find('.txtQty').val();
                //fr = row.find('.fre1').text();

                //getscheme(sale_p_code, cq, unit, row, fr, '', pc);

                //NewOrd[idx].Free = $(tr).find('.fre1').text() || 0;
                //NewOrd[idx].of_Pro_Code = $(tr).find('.of_pro_code').val();
                //NewOrd[idx].of_Pro_Name = $(tr).find('.of_pro_name').val();
                //NewOrd[idx].of_Pro_Unit = $(tr).find('.fre').attr('unit') || 0;


                //var calc_rate = ans[0].RP_Base_Rate * ans[0].con_fac;;
                //row.find('.tdRate').text(parseFloat(calc_rate).toFixed(2));
                //CalcAmt(row);
                if (ans.length > 0) {
                    NewOrd[idx].umo_unit = umoval;
                    NewOrd[idx].Rate = parseFloat(unitval).toFixed(2);
                    row.find('.erp_code').val(conf);
                    row.find('.Con_fac').text(conf);
                    NewOrd[idx].Unit = factor;
                    NewOrd[idx].Rate_in_peice = ans[0].RP_Base_Rate;
                    NewOrd[idx].Qty = qqty;
                    NewOrd[idx].Qty_c = qqty;
                    NewOrd[idx].umo_unit = umo;
                    NewOrd[idx].con_fac = conf;

                    sale_p_code = row.find('.sale_code').text();
                    ori_p_code = row.find('.ddlProd ').find('option:selected').val();
                    unit = $(this).text();
                    cq = row.find('.txtQty').val();
                    fr = row.find('.fre1').text();

                    //getscheme(sale_p_code, cq, unit, row, fr, '', pc);

                    NewOrd[idx].Free = $(tr).find('.fre1').text() || 0;
                    NewOrd[idx].of_Pro_Code = $(tr).find('.of_pro_code').val();
                    NewOrd[idx].of_Pro_Name = $(tr).find('.of_pro_name').val();
                    NewOrd[idx].of_Pro_Unit = $(tr).find('.fre').attr('unit') || 0;

                    row.find('.tdRate').text(parseFloat(unitval).toFixed(2));
                    CalcAmt(row);
                    //if (umo == ans[0].UnitCode) {
                    //    if (umo == ans[0].Base_Unit_code) {
                    //        row.find('.erp_code').val(1);
                    //        row.find('.Con_fac').text(1);
                    //    }
                    //    else {
                    //          row.find('.erp_code').val(ans[0].Sample_Erp_Code);
                    //        row.find('.Con_fac').text(ans[0].Sample_Erp_Code);
                    //    }


                    //    NewOrd[idx].Unit = factor;
                    //    NewOrd[idx].Rate = ans[0].RP_Base_Rate;
                    //    NewOrd[idx].Qty = qqty;
                    //    NewOrd[idx].Qty_c = qqty;
                    //    NewOrd[idx].umo_unit = umo;
                    //    NewOrd[idx].con_fac =ans[0].Sample_Erp_Code;

                    //    sale_p_code = row.find('.sale_code').text();
                    //    ori_p_code = row.find('.ddlProd ').find('option:selected').val();
                    //    unit = $(this).text();
                    //    cq = row.find('.txtQty').val();
                    //    fr = row.find('.fre1').text();

                    //    getscheme(sale_p_code, cq, unit, row, fr, '', pc);

                    //    NewOrd[idx].Free = $(tr).find('.fre1').text() || 0;
                    //    NewOrd[idx].of_Pro_Code = $(tr).find('.of_pro_code').val();
                    //    NewOrd[idx].of_Pro_Name = $(tr).find('.of_pro_name').val();
                    //    NewOrd[idx].of_Pro_Unit = $(tr).find('.fre').attr('unit') || 0;

                    //    var r = Number(ans[0].RP_Base_Rate).toFixed(2);
                    //    row.find('.tdRate').text(r);
                    //    CalcAmt(row);
                    //}
                    //else if (umo != Prod[0].UnitCode && umo == Prod[0].Base_Unit_code) {
                    //    row.find('.Con_fac').text(1);
                    //    row.find('.erp_code').val(1);

                    //    NewOrd[idx].Unit = factor;
                    //    NewOrd[idx].Rate = ans[0].RP_Base_Rate / ans[0].Sample_Erp_Code;
                    //    NewOrd[idx].mrp = ans[0].MRP_Rate / ans[0].Sample_Erp_Code;
                    //    NewOrd[idx].Qty_c = qqty;
                    //    NewOrd[idx].Qty = qqty;
                    //    NewOrd[idx].umo_unit = umo;
                    //    NewOrd[idx].con_fac = 1;

                    //    sale_p_code = row.find('.sale_code').text();
                    //    ori_p_code = row.find('.ddlProd ').find('option:selected').val();
                    //    unit = $(this).text();
                    //    cq = row.find('.txtQty').val();
                    //    fr = row.find('.fre1').text();

                    //    getscheme(sale_p_code, cq, unit, row, fr, '', pc);

                    //    NewOrd[idx].Free = $(tr).find('.fre1').text() || 0;
                    //    NewOrd[idx].of_Pro_Code = $(tr).find('.of_pro_code').val();
                    //    NewOrd[idx].of_Pro_Name = $(tr).find('.of_pro_name').val();
                    //    NewOrd[idx].of_Pro_Unit = $(tr).find('.fre').attr('unit') || 0;


                    //    var calc_rate = ans[0].RP_Base_Rate / ans[0].Sample_Erp_Code;;
                    //    row.find('.tdRate').text(parseFloat(calc_rate).toFixed(2));
                    //    CalcAmt(row);
                    //}
                    //else {
                    //    row.find('.Con_fac').text(ans[0].Sample_Erp_Code);
                    //    row.find('.erp_code').val(ans[0].Sample_Erp_Code);

                    //    NewOrd[idx].Unit = factor;
                    //    NewOrd[idx].Rate = ans[0].RP_Base_Rate * ans[0].Sample_Erp_Code;
                    //    NewOrd[idx].mrp = ans[0].MRP_Rate * ans[0].Sample_Erp_Code;
                    //    NewOrd[idx].Qty_c = qqty;
                    //    NewOrd[idx].Qty = qqty * ans[0].Sample_Erp_Code;
                    //    NewOrd[idx].umo_unit = umo;
                    //    NewOrd[idx].con_fac = ans[0].Sample_Erp_Code;

                    //    sale_p_code = row.find('.sale_code').text();
                    //    ori_p_code = row.find('.ddlProd ').find('option:selected').val();
                    //    unit = $(this).text();
                    //    cq = row.find('.txtQty').val();
                    //    fr = row.find('.fre1').text();

                    //    getscheme(sale_p_code, cq, unit, row, fr, '', pc);

                    //    NewOrd[idx].Free = $(tr).find('.fre1').text() || 0;
                    //    NewOrd[idx].of_Pro_Code = $(tr).find('.of_pro_code').val();
                    //    NewOrd[idx].of_Pro_Name = $(tr).find('.of_pro_name').val();
                    //    NewOrd[idx].of_Pro_Unit = $(tr).find('.fre').attr('unit') || 0;


                    //    var calc_rate = ans[0].RP_Base_Rate * ans[0].Sample_Erp_Code;;
                    //    row.find('.tdRate').text(parseFloat(calc_rate).toFixed(2));
                    //    CalcAmt(row);
                    //}
                }
            }
            calc_stock($(this).closest("tr"));
        });

        function CalcAmt(x) {

            itr = $(x).closest('tr');
            idx = $(itr).index();
            rt = parseFloat($(itr).find('.tdRate').text()); if (isNaN(rt)) rt = 0; qt = parseFloat($(itr).find('.txtQty').val()); if (isNaN(qt)) qt = 0;
            tax = parseFloat($(itr).find('.tdtax').text()); if (isNaN(tax)) tax = 0;

            NewOrd[idx].Sub_Total = (qt * rt).toFixed(2);

            if ($(itr).find('.dis_per').text() == "") { var dis_per = 0; }
            if (NewOrd[idx].Total_Tax == '' || isNaN(NewOrd[idx].Total_Tax)) { NewOrd[idx].Total_Tax = 0; }

            NewOrd[idx].Dis_value = (parseFloat(dis_per) / 100 * parseFloat(NewOrd[idx].Sub_Total)).toFixed(2);
            NewOrd[idx].Sub_Total = (parseFloat(NewOrd[idx].Sub_Total) - parseFloat(NewOrd[idx].Dis_value)).toFixed(2);

            for (var g = 0; g < NewOrd[idx].Tax_details.length; g++) {
                NewOrd[idx].Tax_details[g].Tax_Amt = parseFloat(NewOrd[idx].Tax_details[g].Tax_Per / 100 * parseFloat(NewOrd[idx].Sub_Total)).toFixed(2);
            }

            $(itr).find('.tdRate').css("padding", "21px 0px 0px 0px;");
            $(itr).find('.tdRate').css("text-align", "right");
            $(itr).find('.tdtotal').html(NewOrd[idx].Sub_Total);
            $(itr).find('.disc_value').html(NewOrd[idx].Dis_value);
            $(itr).find('.tddis_val').html(NewOrd[idx].Dis_value);

            $(itr).find('.tdtax').html((parseFloat(NewOrd[idx].Total_Tax) / 100 * parseFloat(NewOrd[idx].Sub_Total)).toFixed(2));
            NewOrd[idx].Gross_Amt = (parseFloat(NewOrd[idx].Total_Tax) / 100 * parseFloat(NewOrd[idx].Sub_Total) + parseFloat(NewOrd[idx].Sub_Total)).toFixed(2);

            NewOrd[idx].Tax_value = (parseFloat(NewOrd[idx].Total_Tax) / 100 * parseFloat(NewOrd[idx].Sub_Total)).toFixed(2);
            $(itr).find('.tdAmt').html(NewOrd[idx].Gross_Amt);
            ReCalc();
        }


        function ReCalc() {

            tv = 0;
            $('.tdtotal').each(function () {
                v = parseFloat($(this).text()); if (isNaN(v)) v = 0;
                tv += v;
            })
            $('#sub_tot').val(tv.toFixed(2));

            tax_value = 0;
            $('.tdtax').each(function () {
                k = parseFloat($(this).text()); if (isNaN(k)) k = 0;
                tax_value += k;
            })
            $('#txt_tax_total').val(tax_value.toFixed(2));

            dis = 0;
            $('.tddis').each(function () {
                z = parseFloat($(this).text()); if (isNaN(z)) z = 0;
                dis += z;
            })
            $('#Txt_Dis_tot').val(dis.toFixed(2));

            gross = 0;
            $('.tdAmt').each(function () {
                i = parseFloat($(this).text()); if (isNaN(i)) i = 0;
                gross += i;
            })
            $('#gross').val(gross.toFixed(2));

            if (Retailer_Extra_Tax_Type == 'TCS') {

                var extra_tax = Extra_Tax;
                var Calc_extra_tax = extra_tax / 100 * tv;
                $('#txt_tds_amt').val('0.00');
                if (Calc_extra_tax > 0) {
                    $('#txt_tcs_amt').val(Math.round((Calc_extra_tax + Number.EPSILON) * 100) / 100) || 0;
                }
                else {
                    $('#txt_tcs_amt').val('0.00');
                }
                Calc_extra_tax = Calc_extra_tax || 0;
                var total_with_extra = gross + Calc_extra_tax;
                $('#gross').val(total_with_extra.toFixed(2));

            }
            else {

                var extra_tax = Extra_Tax;
                var Calc_extra_tax = extra_tax / 100 * tv;
                $('#txt_tcs_amt').val('0.00');
                if (Calc_extra_tax > 0) {
                    $('#txt_tds_amt').val(Math.round((Calc_extra_tax + Number.EPSILON) * 100) / 100) || 0;
                }
                else {
                    $('#txt_tds_amt').val('0.00');
                }
                Calc_extra_tax = Calc_extra_tax || 0;
                var total_with_extra = gross + Calc_extra_tax;
                $('#gross').val(total_with_extra.toFixed(2));

            }
        }

        function DelRow() {
		 if($(".slitm:checked").length==0)
	    {
 		rjalert('Error!', 'Select Any Product To Delete', 'error');
		return false;
            }
$.confirm({
                    title: 'Confirm!',
                    content: 'Do you want to remove this product',
                    type: 'red',
                    typeAnimated: true,
                    autoClose: 'cancel|8000',
                    icon: 'fa fa-warning',
                    buttons: {
                        tryAgain: {
                            text: 'Confirm',
                            btnClass: 'btn-red',
                            action: function () {
                               
            currentheight = $('.modal-content').height();
	   
            $(".slitm:checked").each(function () {

                itr = $(this).closest('tr');
                idx = $(itr).index();
                var prod = itr.find('.ddlProd').val();
                //getscheme($(itr).find('.sale_code').text(), 0, $(itr).find('.Spinner-Value').text(), itr, $(itr).find('.fre1').text(), '', $(itr).find('.pcode').text());
                $(this).closest('tr').remove();
                NewOrd.splice(idx, 1);
                if (prod != "") {
                    $(document).find('.' + prod).each(function () {
                        calc_stock($(this).closest('tr'));
                    });
                }

            }); resetSL(); ReCalc();
            new_height = $('.modal-content').height();
            scrollhightchnageDel = scrollhightchnageDel - (currentheight - new_height);
            $('#exampleModal').scrollTop(scrollhightchnageDel);
                            }
                        },
                        cancel: function () {
                            //$.alert('Time Expired!');
                        }
                        //somethingElse: {
                        //    text: 'Something else',
                        //    btnClass: 'btn-blue',
                        //    keys: ['enter', 'shift'],
                        //    action: function () {
                        //        $.alert('Something else?');
                        //    }
                        //}
                    }
                });
            }
	    resetSL = function () {
            $(".rwsl").each(function () {
                $(this).text($(this).closest('tr').index() + 1);
            });
            }

        clearOrder = function () {
              $('#recpt-dt').val('');
            //$('#route-name').val(0);
            //$('#route-name').chosen("destroy");
            //$('#route-name').chosen();
            //$('#recipient-name').val(0);
            //$('#recipient-name').chosen("destroy");
            //$('#recipient-name').chosen();

            $('#recipient-name').selectpicker({
                liveSearch: true
            });

            $('#recpt-dt').daterangepicker({
                singleDatePicker: true,
                timePicker: true,
                minDate: moment(),
                timePicker24Hour: true,
                timePickerSeconds: true,
                locale: {
                    format: 'DD-MM-YYYY H:mm:ss'
                }
            });
            $("#OrderEntry > TBODY").html("");
            NewOrd = []; resetSL(); ReCalc();
        }

        var approve = 0;
        svOrder = function () {

            approve += 1; var Tax_Array = [];
            if (approve == "1") {

                var netwt = 0;
                for (var i = 0; i < NewOrd.length; i++) {
                    netwt += parseFloat(NewOrd[i].Qty);
                }
                var remark = $('#message-text').val();

                var orderval = $('#gross').val();
                var retcode = $('#recipient-name :selected').val();
                var retnm = $('#recipient-name :selected').text();
                var rotnm = $('#route-name :selected').val();
				
                retnm = retnm.replace(/&/g, "&amp;");
                var recdate = $('#recpt-dt').val();
                var odate = recdate.split(' ');
                var ordate = odate[0].split('-');
                var fnldate = ordate[2] + '-' + ordate[1] + '-' + ordate[0] + ' ' + odate[1];
                if (rotnm == "0" || rotnm == null) {
                    approve = 0;
                    rjalert('Error!', ' Select any Route.', 'error');
                    //alert('Select a Customer Name');
                    return false;
                }
				if (retcode == "0" || retcode == null) {
                    approve = 0;
                    rjalert('Error!', ' Select any retailer name.', 'error');
                    //alert('Select a Customer Name');
                    return false;
                }
                if (recdate == "") {
                    approve = 0;
                    rjalert('Error!', 'Select a Order Date.', 'error');
                    //alert('Select a Order Date');
                    return false;
                }
                if (NewOrd.length == 0) {
                    approve = 0;
                    rjalert('Error!', 'Atleast select a Product.', 'error');
                    //alert('Atleast select a Product');
                    return false;
                }
                for (var i = 0; i < NewOrd.length; i++) {

                    if (NewOrd[i].PName.indexOf('&') > -1) {
                        NewOrd[i].PName = NewOrd[i].PName.replace(/&/g, "&amp;");
                    }

                    if (NewOrd[i].sPCd == '') {
                        approve = 0;
                        rjalert('Error!', 'Select a Product.', 'error');
                        //alert('Select a Product');
                        return false;
                    }
                    if (NewOrd[i].Qty == '') {
                        approve = 0;
                        rjalert('Error!', 'Remove the Product or Enter the Quantity.', 'error');
                        //alert('Remove the Product or Enter the Quantity');
                        return false;
                    }

                    for (var f = 0; f < NewOrd[i].Tax_details.length; f++) {
                        NewOrd[i].Tax_details[f]["umo_code"] = NewOrd[i].umo_unit;
                        Tax_Array.push(NewOrd[i].Tax_details[f]);
                    }

                }
                var sub = $('#sub_tot').val(); var dIs = $('#Txt_Dis_tot').val(); var tax = $('#txt_tax_total').val(); var Extra_Tax_Type = Retailer_Extra_Tax_Type;
                if (Retailer_Extra_Tax_Type == 'TCS') { var Extra_Tax_Value = $('#txt_tcs_amt').val(); } else { var Extra_Tax_Value = $('#txt_tds_amt').val(); }
                var stock_chck = $('#OrderEntry tbody tr').find('.focus').length;

                if (stock_chck == 0) {

                    if (orderval > 0) {

                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            url: "myOrders.aspx/saveorders",
                            data: "{'NewOrd':'" + JSON.stringify(NewOrd) + "','NewOrd_Tax_Details':'" + JSON.stringify(Tax_Array) + "','Remark':'" + remark + "','Ordrval':'" + orderval + "','RetCode':'" + retcode + "','RecDate':'" + fnldate + "','Ntwt':'" + netwt + "','retnm':'" + retnm + "','Type':'0','ref_order':'','sub_total':'" + sub + "','dis_total':'" + dIs + "','tax_total':'" + tax + "','Extra_Tax_type':'" + Extra_Tax_Type + "','Extra_Tax_value':'" + Extra_Tax_Value + "'}",
                            dataType: "json",
                            success: function (data) {
                                if (data.d.length > 0) {
                                    //alert("Order Successfully");
                                    clearOrder();
                                    $(document).find('#free_table tbody').empty();
                                    arr = [];
                                    $.confirm({
                                        title: 'Success!',
                                        content: 'Secondary Order Submitted Successfully!',
                                        type: 'green',
                                        typeAnimated: true,
                                        autoClose: 'action|8000',
                                        icon: 'fa fa-check fa-2x',
                                        buttons: {
                                            tryAgain: {
                                                text: 'OK',
                                                btnClass: 'btn-green',
                                                action: function () {
                                                    location.reload();
                                                }
                                            }
                                        }
                                    });

                                }
                                else {
                                    alert(data.d);
                                }
                            },
                            error: function (result) {
                                approve = 0;
                            }
                        });
                    } else {
                        approve = 0;
                        rjalert('Error!', 'Order Minimum Value to create a Order.', 'error');
                        //alert("Order Minimum Value to create a Order");
                        return false;
                    }
                }
                else {
                    approve = 0;
                    rjalert('Error!', 'Stock value is lesser than qunatity.', 'error');
                    //alert("Stock value is lesser than qunatity");
                    return false;
                }
            }
        }

        function loadscheme() {

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "myOrders.aspx/getscheme",
                data: "{'date':'" + today + "','Div_Code':'" + Div_Code + "','Stockist_Code':'" + stockist_Code + "'}",
                dataType: "json",
                async: false,
                success: function (data) {
                    scheme = JSON.parse(data.d) || [];

                    for (var a = 0; a < scheme.length; a++) {

                        var product_code = scheme[a].Product_Code;
                        var out = [];

                        out = Allrate.filter(function (t) {
                            return (t.Product_Detail_Code == product_code);
                        });

                        if (out.length > 0) {

                            if (scheme[a].Scheme_Unit == out[0].product_unit) {

                                var cal_erp = scheme[a].Sample_Erp_Code * scheme[a].Scheme;
                                scheme[a]["erp_val"] = cal_erp;
                            }
                            else {
                                scheme[a]["erp_val"] = scheme[a].Scheme;
                            }
                        }
                    }
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }


        $(document).on('keyup', '.txtQty', function (e) {

            tr = $(this).closest("tr");
            idx = $(tr).index();
            NewOrd[idx].PCd = tr.closest('tr').find('.ddlProd option:selected ').val();
            NewOrd[idx].PName = tr.closest('tr').find('.ddlProd option:selected ').text();
            NewOrd[idx].Unit = tr.closest('tr').find('.Spinner-Value').text();

            un = $(tr).find('.Spinner-Value').text();

            if (un == 'Select') {
                rjalert('Error!', 'Please Select UOM.', 'error');
                //alert('Please Select UOM');
                $(tr).find('.txtQty').val('');
                return false;
            }

            calc_stock($(this).closest("tr"));
            CQ = $(this).val();

            var umoqty = $(this).closest('tr').find('.Con_fac').text();
            var DefaultUmo = $(this).closest('tr').find('.Con_fac').text();
            var prd_rate = $(tr).find(".tdRate").text();
            if (prd_rate == '' || prd_rate == undefined || isNaN(prd_rate)) { prd_rate = 0; };
            //chck_pck(umoqty, DefaultUmo, prd_rate, CQ, idx, $(this));
            var erpcode = $(tr).find('.erp_code').val();

            CQ = $(this).val();
            opcode = $(tr).find(".ddlProd :selected").val();

            var disrate = $(this).closest("tr").find('.tdRate').text();
            result = (CQ * disrate);

            pCode = $(tr).find(".sale_code").text();
            pname = $(tr).find(".ddlProd :selected").text();
            ff = $(tr).find(".fre1").text();

            //getscheme(pCode, CQ, un, tr, ff, '', opcode);

            if ($(tr).find('.Con_fac').text() == '' || $(tr).find('.Con_fac').text() == undefined) { var con_fac = 0; } else { var con_fac = $(tr).find('.Con_fac').text() }
            if ($(this).val() == '' || $(this).val() == undefined || isNaN($(this).val())) { var qtys = 0; } else { var qtys = $(this).val(); }

            NewOrd[idx].Free = $(tr).find('.fre1').text() || 0;
            NewOrd[idx].Qty_c = qtys;
            NewOrd[idx].Qty = qtys * con_fac;
            NewOrd[idx].Discount = $(tr).find(".dis_val_class").text() || 0;
            NewOrd[idx].of_Pro_Code = $(tr).find('.of_pro_code').val();
            NewOrd[idx].of_Pro_Name = $(tr).find('.of_pro_name').val();
            NewOrd[idx].of_Pro_Unit = $(tr).find('.fre').attr('unit') || 0;
            CalcAmt(tr);

            if (e.keyCode == 13) {

                if ($(this).val() != '' || isNaN($(this).val())) {
                    AddRow(1);
                }
            }
        });

        function chck_pck(umoqty, DefaultUmo, prd_rate, CQ, idx, thisObj) {

            if (DefaultUmo == "15" || DefaultUmo == "14") {
                var pack = CQ * umoqty;
                if (pack % 1 != 0) { pack = 0; }
                var RoundPack = parseInt(pack);

            }
            else {
                var pack = CQ / umoqty; if (pack % 1 != 0) { pack = 0; }

                var RoundPack = parseInt(pack);
                if (isNaN(RoundPack)) RoundPack = 0;

                if (RoundPack != 0 && prd_rate != 0 || CQ == '') {
                    thisObj.css("background-color", "");
                    thisObj.removeClass('focus');
                }
                else {
                    thisObj.css("background-color", "red");
                    thisObj.addClass('focus');
                }

            }



            NewOrd[idx].Qty = RoundPack;
            NewOrd[idx].Qty_c = CQ;
        }
        function calc_stock(n) {

            var pduct_code = n.find('.ddlProd').val();
            var con_fc = n.find('.erp_code').val();
            var u_n = n.find('.Spinner-Value').text();
            var qqty = n.find('.txtQty').val(); if (qqty == "" || qqty == null) { qqty = 0; }
            var sa_qty = 0;
            var ad_qty = 0;

            var sto_filter = [];

            sto_filter = Allrate.filter(function (y) {
                return (y.Product_Detail_Code == pduct_code);
            });
            if (sto_filter[0].TotalStock > 0 && sto_filter[0].TotalStock >= (qqty * con_fc)) {
                $(this).find('.validate').css('background-color', '');
                $(this).find('.txtQty').removeClass('focus');
            }
            else {
                $(this).find('.validate').css('background-color', '#ff6666');
                // rjalert('Error!', 'Stock is not Available for the product.', 'error');
                //alert('Stock is not Available for the product');
                $(this).find('.txtQty').addClass('focus');
            }
            if (sto_filter > 0) {

                $(document).find('.' + pduct_code).each(function () {

                    var producode = $(this).find('.ddlProd').val();
                    var uni = $(this).find('.Spinner-Value').text();
                    var con_fc = $(this).find('.erp_code').val();
                    var qqty = $(this).find('.txtQty').val(); if (qqty == "" || qqty == null) { qqty = 0; }

                    if (sto_filter.length > 0) {
                        ad_qty += parseFloat(qqty) * con_fc;
                    }


                    if (sto_filter[0].TotalStock > 0 && sto_filter[0].TotalStock >= ad_qty) {
                        $(this).find('.validate').css('background-color', '');
                        $(this).find('.txtQty').removeClass('focus');
                    }
                    else {
                        $(this).find('.validate').css('background-color', '#ff6666');
                        //rjalert('Error!', 'Stock is not Available for the product.', 'error');
                        //alert('Stock is not Available for the product');
                        $(this).find('.txtQty').addClass('focus');
                    }
                });
            }
        }


        function getscheme(pCode, cq, un, tr, ff, disss, opcode) {

            var res = scheme.filter(function (a) {
                return (a.Sale_Erp_Code == pCode && (Number(cq) >= Number(a.Scheme)) && a.Scheme_Unit == un)
            });

            ans = [];

            $(tr).find('.dis_val_class').text(0);
            $(tr).find('.disc_value').text(0);

            for (var c = 0; c < arr.length; c++) {

                if (arr[c].Product_Code.indexOf(opcode) >= 0) {

                    if (arr[c].Free > 0) {

                        arr[c].Free = arr[c].Free - ff;
                        $(tr).find('.fre').text('#' + opcode).text('0');
                        $(tr).find('.fre1').text('#' + opcode).text('0');
                        $(tr).find('.fre').attr('freecqty', arr[c].Free);
                        $(tr).find('.fre').attr('unit', '');
                        tr.find('.of_pro_name').val(0);
                        tr.find('.of_pro_code').val(0);
                        $(tr).find('.disc_value').text($(tr).find('.disc_value').text() - $(tr).find('.disc_value').text());
                    }
                }
            }

            if (res.length > 0) {

                schemedefinewithoutpackage(res[0], tr, ff, ans);
            }

            $('#free_table').find('tbody tr').remove();
            for (var r = 0; r < arr.length; r++) {

                if (arr[r].Free != '0') {
                    var str = "<tr><td style='width: 14%;' class='td_id'>" + (r + 1) + "</td><td style='width:21%;'><input type='hidden' value=" + arr[r].Offer_Product_Code + " id='apc'/>" + arr[r].Offer_Product_Code + "</td><td style='left: 201px;width: 38%;' class='td_name'><input type='hidden' value=" + arr[r].Offer_Product_Name + " id='apn'/>" + arr[r].Offer_Product_Name + "</td><td style='left: 454px;' class='td_free'><input type='hidden' value=" + arr[r].Free + " id='fr'/>" + (arr[r].Free + ' ' + arr[r].Offer_Product_Free_Unit) + "</td></tr>";
                    $('#free_table tbody').append(str);
                }
            }
        }

        function schemedefinewithoutpackage(res, tr, ff) {

            if (res.Discount == '0') {

                if (res.Package != "Y") {

                    if (CQ > 0) {

                        var free = parseInt(parseFloat((CQ / parseInt(res.Scheme))) * parseInt(res.Free))

                        var x = arr.filter(function (b) {
                            return (b.Offer_Product_Code == res.Offer_Product && b.Offer_Product_Free_Unit == res.Free_Unit)
                        })

                        if (res.Against == "N") {

                            if (x.length > 0) {

                                idx = arr.indexOf(x[0]);
                                arr[idx].Free += free;
                                arr[idx].Product_Free_Unit = res.Scheme_Unit;
                                arr[idx].Product_Code = (arr[idx].Product_Code.indexOf(opcode) >= 0) ? arr[idx].Product_Code : arr[idx].Product_Code + ',' + opcode;
                                $(tr).find('.fre').text(free + ' ' + (res.Free_Unit));
                                $(tr).find('.fre').attr('unit', res.Free_Unit);
                                $(tr).find('.fre').attr('freepro', res.Product_Code);
                                $(tr).find('.fre').attr('cqty', CQ);
                                $(tr).find('.fre').attr('freecqty', free);

                                $(tr).find('.fre1').text(free);

                                if (free != "") {
                                    tr.find('.of_pro_name').val(pname);
                                    tr.find('.of_pro_code').val(res.Product_Code);
                                }
                            }
                            else {

                                $(tr).find('.fre').text(free + ' ' + ((res.Free_Unit)));
                                $(tr).find('.fre').attr('unit', res.Free_Unit);
                                $(tr).find('.fre').attr('freepro', res.Product_Code);
                                $(tr).find('.fre').attr('cqty', CQ);
                                $(tr).find('.fre').attr('freecqty', free);

                                $(tr).find('.fre1').text(free);

                                if (free != "") {
                                    tr.find('.of_pro_name').val(res.Offer_Product_Name);
                                    tr.find('.of_pro_code').val(res.Offer_Product);
                                }
                                arr.push({

                                    Product_Code: res.Product_Code,
                                    Product_Name: res.Product_Name,
                                    Offer_Product_Code: res.Offer_Product,
                                    Offer_Product_Name: res.Offer_Product_Name,
                                    Free: free,
                                    Product_Free_Unit: res.Scheme_Unit,
                                    Offer_Product_Free_Unit: res.Free_Unit

                                })
                            }
                        }
                        else {

                            if (parseInt(CQ) >= parseInt(res.Scheme)) {

                                if (x.length > 0) {
                                    idx = arr.indexOf(x[0]);
                                    arr[idx].Free += free;
                                    arr[idx].Product_Free_Unit = res.Scheme_Unit;
                                    arr[idx].Product_Code = (arr[idx].Product_Code.indexOf(opcode) >= 0) ? arr[idx].Product_Code : arr[idx].Product_Code + ',' + opcode;
                                    $(tr).find('.fre').text(free + ' ' + (res.Free_Unit));
                                    $(tr).find('.fre').attr('unit', res.Free_Unit);
                                    $(tr).find('.fre').attr('freepro', res.Offer_Product);
                                    $(tr).find('.fre').attr('cqty', CQ);
                                    $(tr).find('.fre').attr('freecqty', free);
                                    $(tr).find('.fre1').text(free);
                                    tr.find('.of_pro_name').val(res.Offer_Product_Name);
                                    tr.find('.of_pro_code').val(res.Offer_Product);
                                }
                                else {
                                    $(tr).find('.fre').text(free + ' ' + ((res.Free_Unit)));
                                    $(tr).find('.fre').attr('unit', res.Free_Unit);
                                    $(tr).find('.fre').attr('freepro', res.Offer_Product);
                                    $(tr).find('.fre').attr('cqty', CQ);
                                    $(tr).find('.fre').attr('freecqty', free);
                                    $(tr).find('.fre1').text(free);

                                    tr.find('.of_pro_name').val(res.Offer_Product_Name);
                                    tr.find('.of_pro_code').val(res.Offer_Product);
                                    arr.push({

                                        Product_Code: res.Product_Code,
                                        Product_Name: res.Product_Name,
                                        Offer_Product_Code: res.Offer_Product,
                                        Offer_Product_Name: res.Offer_Product_Name,
                                        Free: free,
                                        Product_Free_Unit: res.Scheme_Unit,
                                        Offer_Product_Free_Unit: res.Free_Unit
                                    })
                                }
                            }
                        }
                    }
                }
                else {

                    var TotalQuantity = CQ / res.Scheme;
                    var free = parseInt(TotalQuantity) * res.Free;

                    var x = arr.filter(function (b) {
                        return (b.Offer_Product_Code == res.Offer_Product && b.Offer_Product_Free_Unit == res.Free_Unit)
                    })

                    if (res.Against == "N") {

                        if (x.length > 0) {
                            idx = arr.indexOf(x[0]);
                            arr[idx].Free += free;
                            arr[idx].Product_Free_Unit = res.Scheme_Unit;
                            arr[idx].Product_Code = (arr[idx].Product_Code.indexOf(opcode) >= 0) ? arr[idx].Product_Code : arr[idx].Product_Code + ',' + opcode;
                            $(tr).find('.fre').text(free + ' ' + (res.Free_Unit));
                            $(tr).find('.fre').attr('unit', res.Free_Unit);
                            $(tr).find('.fre').attr('freepro', res.Product_Code);
                            $(tr).find('.fre').attr('cqty', CQ);
                            $(tr).find('.fre').attr('freecqty', free);

                            $(tr).find('.fre1').text(free);
                            if (free != "") {
                                tr.find('.of_pro_name').val(res.Offer_Product_Name);
                                tr.find('.of_pro_code').val(res.Offer_Product);
                            }
                        }
                        else {
                            $(tr).find('.fre').text(free + ' ' + ((res.Free_Unit)));
                            $(tr).find('.fre').attr('unit', res.Free_Unit);
                            $(tr).find('.fre').attr('freepro', res.Product_Code);
                            $(tr).find('.fre').attr('cqty', CQ);
                            $(tr).find('.fre').attr('freecqty', free);
                            $(tr).find('.fre1').text(free);

                            if (free != "") {
                                tr.find('.of_pro_name').val(res.Offer_Product_Name);
                                tr.find('.of_pro_code').val(res.Offer_Product);
                            }

                            arr.push({

                                Product_Code: res.Product_Code,
                                Product_Name: res.Product_Name,
                                Offer_Product_Code: res.Offer_Product,
                                Offer_Product_Name: res.Offer_Product_Name,
                                Free: free,
                                Product_Free_Unit: res.Scheme_Unit,
                                Offer_Product_Free_Unit: res.Free_Unit

                            })
                        }
                    }
                    else {
                        if (x.length > 0) {

                            idx = arr.indexOf(x[0]);
                            arr[idx].Free += free;
                            arr[idx].Product_Code = (arr[idx].Product_Code.indexOf(opcode) >= 0) ? arr[idx].Product_Code : arr[idx].Product_Code + ',' + opcode;
                            $(tr).find('.fre').text(free + ' ' + (res.Free_Unit));
                            $(tr).find('.fre').attr('unit', res.Free_Unit);
                            $(tr).find('.fre').attr('freepro', res.Offer_Product);
                            $(tr).find('.fre').attr('cqty', CQ);
                            $(tr).find('.fre').attr('freecqty', free);

                            $(tr).find('.fre1').text(free);
                            tr.find('.of_pro_name').val(res.Offer_Product_Name);
                            tr.find('.of_pro_code').val(res.Offer_Product);
                        }
                        else {
                            $(tr).find('.fre').text(free + ' ' + ((res.Free_Unit)));
                            $(tr).find('.fre').attr('unit', res.Free_Unit);
                            $(tr).find('.fre').attr('freepro', res.Offer_Product);
                            $(tr).find('.fre').attr('cqty', CQ);
                            $(tr).find('.fre').attr('freecqty', free);
                            $(tr).find('.fre1').text(free);

                            tr.find('.of_pro_name').val(res.Offer_Product_Name);
                            tr.find('.of_pro_code').val(res.Offer_Product);

                            arr.push({

                                Product_Code: res.Product_Code,
                                Product_Name: res.Product_Name,
                                Offer_Product_Code: res.Offer_Product,
                                Offer_Product_Name: res.Offer_Product_Name,
                                Free: free,
                                Product_Free_Unit: res.Scheme_Unit,
                                Offer_Product_Free_Unit: res.Free_Unit

                            })
                        }
                    }
                }
            }

            $(tr).find('.dis_val_class').text(res.Discount);
            var d = $(tr).find('.tdRate').text() * $(tr).find('.txtQty ').val();
            var discalc = parseInt(res.Discount) / 100;
            var distotal = d * discalc;
            var finaltotal = d - distotal;

            if (distotal != '0') {
                $(tr).find('.tdtotal').text(finaltotal.toFixed(2));
                $(tr).find('.disc_value').val(distotal.toFixed(2));
                $(tr).find('.tddis_val').text(distotal.toFixed(2));
            }

            if (finaltotal != "0") {
                var after_cal = $(tr).find('.tax_val').val() / 100 * $(tr).find('.tdtotal').text()
                var fin = after_cal + parseFloat($(tr).find('.tdtotal').text());
                $(tr).find('.tdtax').text(after_cal.toFixed(2));
                $(tr).find('.tdAmt').text(fin.toFixed(2));
            }
        }

        function PrintIView(order_id, stk, div, cust_code) {
            PrintData(order_id, stk, div, cust_code);
        }

        function PrintData(order_id, stk, div, cust_code) {

            $('#div_Print').html('');
            netval = 0; cgst = 0; sgst = 0; totlcase = 0; totalpie = 0; cashdis = 0.00; grossvalue = 0; totalGst = 0.00; roundoff = 0.00; netvalue = 0.00;
            rndoff = 0.00; netwrd = ''; netwrd1 = ''; netwrd2 = ''; cntu = 10; pagenumber = 1; amt_without_tax = 0;
            Get_Tax_Details(order_id);
            loadDataPrint(order_id, stk, div, cust_code);
            print();
        }

        function Get_Tax_Details(order_id) {

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "myOrders.aspx/Get_Sec_Print_tax_Details",
                data: "{'Order_Id':'" + order_id + "'}",
                dataType: "json",
                async: false,
                success: function (data) {
                    Print_Prd_tax_Details = JSON.parse(data.d) || [];
                },
                error: function (result) {
                }
            });
        }

        function loadDataPrint(order_id, stk, div, cust_code) {

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "myOrders.aspx/GetProductdetails",
                data: "{'Order_Id':'" + order_id + "'}",
                dataType: "json",
                async: false,
                success: function (data) {
                    AllOrderspro = JSON.parse(data.d) || [];
                    Orders = AllOrderspro;
                    ReloadTablePrint(order_id, stk, div, cust_code);
                },
                error: function (result) {
                }
            });
        }

        function ReloadTablePrint(order_id, stk, div, cust_code) {

            $('#div').empty(); var tot_gross = 0; var rowcount = 0; var total_tax_Per = 0;
            Multipage = '<page size="A4" layout="portrait" class="Printarea" ><div  class="page" style="font-family: "Times New Roman", Times, serif;">';
            singlepg = '<div  class="page" style="font-family: "Times New Roman", Times, serif;">';

            for (x = 0; x < (Orders.length / PgRecords_print); x++) {
                str = '';
                st = PgRecords_print * (pgNo_Print - 1); slno = 0;
                if (PgRecords > Orders.length) {
                    str += singlepg;
                }
                else
                    str += Multipage;

                loadDistributor(order_id, stk, div, cust_code);
                productheader();
                for ($i = st; $i < st + PgRecords_print; $i++) {
                    if ($i > Orders.length) {
                        str += "<tr><td class='Sh' style='padding-top:0px;padding-bottom:0px;height:14px;'></td></tr>";
                    }
                    if ($i < Orders.length) {

                        var caseval = "";
                        var pc = "";
                        var amtbyCase = 0.00; var CGST = 0; var SGST = 0;

                        var amount = Orders[$i].Amount;
                        var discnt = Orders[$i].Discount;

                        //var filted_tax = Print_Prd_tax_Details.filter(function (r) {
                        //    return (r.Product_Code == Orders[$i].Product_Code && r.Trans_Sl_No == order_id);
                        //});

                        //for (var f = 0; f < filted_tax.length; f++) {
                        //     CGST = filted_tax[f].Tax_Amt;
                        //     SGST = filted_tax[f].Tax_Amt;
                        //}
                        var peice = 0;
                        var tax = Orders[$i].Tax / 2;
                        var Total_Tax_Value = Orders[$i].Tax;
                        peice = Orders[$i].Quantity;
                        //var qty_in_case = Orders[$i].Quantity / Orders[$i].Sample_Erp_Code;
                        //if (qty_in_case >= 1) {
                        //   qty_in_case = parseInt(qty_in_case);
                        //   peice = Orders[$i].Quantity - (Orders[$i].Sample_Erp_Code * qty_in_case)
                        // }
                        // else {
                        //    qty_in_case = 0;
                        //     peice = Orders[$i].Quantity;
                        //  }
                        var cgstv = tax;
                        var sgstv = tax;
                        //var sgstv = Orders[$i].SGST;

                        //var grossamnt = amount + Orders[$i].Tax;
                        //if (Orders[$i].qty == 0) {
                        //    amtbyCase = grossamnt;
                        //} else {
                        //    amtbyCase = grossamnt / Orders[$i].qty;
                        //}


                        rowcount = rowcount + 1;

                        str += "<tr><td class='Sh' align='center' style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'><b>" + ($i + 1) + "</b></td>";
                        str += "<td class='Sh' align='left'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'><b>" + Orders[$i].Product_Name + "</b></td>";
                        str += "<td class='Sh' align='center' style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + Orders[$i].HSN_Code + "</td>";
                        str += "<td class='Sh' align='right' style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + Orders[$i].Move_MailFolder_Name + "</td>";
                        str += "<td class='Sh' align='right' style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + peice + "</td>";
                        str += "<td class='Sh' align='right'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + Orders[$i].MRP_Price + "</td>";
                        str += "<td class='Sh' align='right'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + Orders[$i].Rate + "</td>";
                        str += "<td class='Sh' align='right' style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + Orders[$i].Free + "</td>";
                        str += "<td class='Sh' align='right'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + discnt.toFixed(2) + "</td>";
                        str += "<td class='Sh' align='right'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + cgstv.toFixed(2) + "</td>";
                        str += "<td class='Sh' align='right'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + sgstv.toFixed(2) + "</td>";
                        str += "<td class='Sh' align='right'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + amount.toFixed(2) + "</td>";
                        str += "<td class='Sh' align='right'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;display:none;'>" + amount.toFixed(2) + "</td>";
                        str += "<td class='Sh' align='right'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + amount.toFixed(2) + "</td></tr>";

                        cashdis += Orders[$i].Discount;
                        //totlcase += qty_in_case;
                        // totalpie += Orders[$i].Quantity;
                        cgst += cgstv;
                        sgst += sgstv;
                        grossvalue += Orders[$i].Amount;
                        amt_without_tax += (Orders[$i].Amount - Orders[$i].Tax);
                        totalGst += Orders[$i].Tax;
                    }
                }
                pagenumber = pgNo;
                if (Orders.length > PgRecords_print) {
                    str += "<tr><td class='Sh' align='right' colspan='13' style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'><b>Continue</b></td></tr>";
                    str += '</tbody></table><table class="pageno"><tr style="font:12px TimesNewRomen;"><td colspan="8" style="padding-left:350px;float:right">' + pagenumber + '</td><td colspan="3" style="float:right"></td></tr></table></div><div style="break-after:page"></div>';
                    str += '</div></page>';
                }
                else {
                    str += '</div>';
                }
                $('#div_Print').append(str);
                pgNo++;
            }
            gstcalculi();

        }

        function loadDistributor(order_id, stk, div, cust_code) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "myOrders.aspx/GetDistributor",
                data: "{'Order_Id':'" + order_id + "','Stockist':'" + stk + "','Division':'" + div + "','Cust_code':'" + cust_code + "'}",
                dataType: "json",
                success: function (data) {
                    AllOrdersdetails = JSON.parse(data.d) || [];

                    var str1 = '<div class="head" style="font-family: "Times New Roman", Times, serif;"><table border="0" class="headtable" align="left" style="width: 35%; border-collapse: collapse;"><tbody>';
                    var str2 = '<table border="0" align="left" class="middletable" style="width: 35%; border-collapse: collapse;"><tbody>';
                    var str3 = '<table border="0" align="left" class="lasttable" style="width: 30%; border-collapse: collapse;"><tbody>';

                    var a = 0;

                    str1 += '<tr><td  align="left" style="padding-right:0px; padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen; TimesNewRomen;"></td></tr>';
                    str1 += '<tr><td  align="left" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen; "></td></tr>';
                    str1 += '<tr><td  align="left" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;"><b></b>&nbsp;&nbsp;' + AllOrdersdetails[a].RetgstTin + '</td></tr>';
                    str1 += '<tr><td  align="left" style ="font: bold 14px TimesNewRomen;padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;"><b>' + AllOrdersdetails[a].ListedDr_Name + '</b></td></tr>';
                    str1 += '<tr><td  align="left" style="width:275px;padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;">' + AllOrdersdetails[a].ListedDr_Address1 + '</td></tr>';
                    str1 += '<tr><td  align="left" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;">Retailer Ph NO:- ' + AllOrdersdetails[a].RetMobile + '</td></tr></tbody></table>';

                    str2 += '<tr><td align="left" style="font: bold 16px TimesNewRomen;padding-top: 0px;padding-bottom: 0px;"><b>' + AllOrdersdetails[a].Stockist_Name + '</b></td></tr>';
                    str2 += '<tr><td align="left" style="width:250px;padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;">' + AllOrdersdetails[a].Stockist_Address + '</td></tr>';
                    str2 += '<tr><td align="center" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;">PH NO:&nbsp;&nbsp;' + AllOrdersdetails[a].Stockist_Mobile + '</b></td></tr>';
                    str2 += '<tr><td colspan="5" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;"></td></tr>';
                    str2 += '<tr><td colspan="5" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;"></td></tr></tbody></table>';

                    str3 += '<tr><td align="left" style="font: bold 16px TimesNewRomen;padding-top: 0px;padding-bottom: 0px;"><b>GST INVOICE</b></td></tr>';
                    str3 += '<tr><td align="left" style="padding-top:0px;padding-bottom:0px;font:12px TimesNewRomen;">Dist GSTIN:-' + AllOrdersdetails[a].DistgstTin + '</td></tr >';
                    str3 += '<tr><td align="left" style="padding-top:0px;padding-bottom:0px;font:12px TimesNewRomen;">Bill Date :-' + AllOrdersdetails[a].billdate + '</td></tr>';
                    str3 += '<tr><td align="left" style="padding-top:0px;padding-bottom:0px;font:12px TimesNewRomen;">Bill No:-' + AllOrdersdetails[a].billno + '</td></tr>';
                    str3 += '<tr><td align="left" style="padding-top:0px;padding-bottom:0px;font:12px TimesNewRomen;">D.State Name ' + AllOrdersdetails[a].Diststate + '</td></tr> ';
                    str3 += '<tr><td align="left" style="padding-top:0px;padding-bottom:0px;font:12px TimesNewRomen;">D.State Code:-' + AllOrdersdetails[a].DistStatecd + '</td></tr></tbody></table></div>';

                    str += str1 + str2 + str3;

                }
            });
        }

        function productheader() {
            str += '<div class="product" style="font:12px TimesNewRomen;"><table class="rptOrders" align="center" style="width: 100%; border-collapse: collapse; bordercolor:blue;">';
            str += '<thead><tr style="border-top:thin dashed;border-bottom:thin dashed;font:12px TimesNewRomen;"><td class="Sh1" align="center" rowspan="2"><font face="calibri"/><b>Sl.No</b></td><td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>DESCRIPTION</b></td>';
            str += '<td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>HSN Code</b></td><td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>UOM</b></td><td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>QTY</b></td>';
            str += '<td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>MRP</b></td><td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>Rate</b></td>';
            str += '<td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>FrQty</b></td>';
            str += '<td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>S.Disc</b></td></td><td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>CGST</b></td>';
            str += '<td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>SGST</b></td></td><td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>Amt/Case</b></td>';
            str += '<td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>Amount</b></td></tr></thead>';
            str += '<tbody>';
        }

        function numberToWords(number) {
            var digit = ['zero', 'one', 'two', 'three', 'four', 'five', 'six', 'seven', 'eight', 'nine'];
            var elevenSeries = ['ten', 'eleven', 'twelve', 'thirteen', 'fourteen', 'fifteen', 'sixteen', 'seventeen', 'eighteen', 'nineteen'];
            var countingByTens = ['twenty', 'thirty', 'forty', 'fifty', 'sixty', 'seventy', 'eighty', 'ninety'];
            var shortScale = ['', 'thousand', 'million', 'billion', 'trillion'];

            number = number.toString(); number = number.replace(/[\, ]/g, ''); if (number != parseFloat(number)) return 'not a number'; var x = number.indexOf('.'); if (x == -1) x = number.length; if (x > 15) return 'too big'; var n = number.split(''); var str = ''; var sk = 0; for (var i = 0; i < x; i++) { if ((x - i) % 3 == 2) { if (n[i] == '1') { str += elevenSeries[Number(n[i + 1])] + ' '; i++; sk = 1; } else if (n[i] != 0) { str += countingByTens[n[i] - 2] + ' '; sk = 1; } } else if (n[i] != 0) { str += digit[n[i]] + ' '; if ((x - i) % 3 == 0) str += 'hundred '; sk = 1; } if ((x - i) % 3 == 1) { if (sk) str += shortScale[(x - i - 1) / 3] + ' '; sk = 0; } } if (x != number.length) { var y = number.length; str += 'point '; for (var i = x + 1; i < y; i++) str += digit[n[i]] + ' '; } str = str.replace(/\number+/g, ' '); return str.trim() + ".";

        }

        function gstcalculi() {
            netval = amt_without_tax + totalGst;
            netvalue = Math.round(netval);
            rndoff = netvalue - netval;
            intpart = netvalue.toString().split(".")[0];
            floatpart = netvalue.toString().split(".")[1];

            if (floatpart == undefined) {
                netwrd1 = "Zero"
            } else if (floatpart.length != 2) {
                floatpart += "0";
                netno1 = numberToWords(parseInt(floatpart)).replace(".", "");
                netwrd1 = netno1.replace(/^(.)|\s+(.)/g, c => c.toUpperCase());
            }
            netno = numberToWords(parseInt(intpart)).replace(".", "");
            netwrd = netno.replace(/^(.)|\s+(.)/g, c => c.toUpperCase());

            var distributor = ("<%=Session["Sf_Name"]%>");

            str = '<div style="padding-bottom: -5px;" class="total"><table border="0" class="rpttotal" align="center" style="width: 100%; border-collapse: collapse; bordercolor:blue;"><tbody>';
            str += '<tr style="border-top:thin dashed;font:12px TimesNewRomen;"> <td></td><td></td> <td colspan="12" ><b>** GST Summary **</b></td></tr>';
            str += '<tr style="font:12px TimesNewRomen;"><td style="padding-top:0px;padding-bottom:0px;" colspan="2"><b>GST Desc</b></td><td style="padding-top: 0px;padding-bottom: 0px;"><b>GST%</b></td><td style="padding-top: 0px;padding-bottom: 0px;"><b>GST Amount</b></td><td style="padding-top: 0px;padding-bottom: 0px;" colspan="2"><b></b></td><td  colspan="4" style="padding-top:0px;padding-bottom:0px;"><b></b>&nbsp;&nbsp;</td><td style="padding-top:0px;padding-bottom:0px;"><b>Gross Value :</b>&nbsp;&nbsp;</td><td style="float:right;padding-top:0px;padding-bottom:0px;">' + amt_without_tax.toFixed(2) + '</td><td></td></tr>';
            str += '<tr style="font:12px TimesNewRomen;"><td style="padding-top:0px;padding-bottom:0px;" colspan="2"><b>CGST</b></td><td style="padding-top: 0px;padding-bottom: 0px;">0</td><td style="padding-top:0px;padding-bottom:0px;">' + cgst.toFixed(2) + '</td><td style="padding-top:0px;padding-bottom:0px;" colspan="3"></td><td  colspan="3" style="padding-top:0px;padding-bottom:0px;"><b></b>&nbsp;&nbsp;</td><td style="padding-top:0px;padding-bottom:0px;"><b>Scheme Amt :</b></td><td style="float:right;padding-top:0px;padding-bottom:0px;">0.00</td><td></td></tr>';
            str += '<tr style="font:12px TimesNewRomen;"><td style="padding-top:0px;padding-bottom:0px;" colspan="2"><b>SGST</b></td><td style="padding-top: 0px;padding-bottom: 0px;">0</td><td style="padding-top:0px;padding-bottom:0px;">' + cgst.toFixed(2) + '</td><td style="padding-top:0px;padding-bottom:0px;" colspan="6"></td><td style="padding-top:0px;padding-bottom:0px;"><b>Cash Disc :</b>&nbsp;&nbsp;</td><td style="float:right;padding-top:0px;padding-bottom:0px;">' + cashdis.toFixed(2) + '</td><td></td></tr>';
            str += '<tr style="font:12px TimesNewRomen;"><td colspan="10" style="padding-top: 0px;padding-bottom: 0px;"></td><td style="padding-top: 0px;padding-bottom: 0px;"><b>GST Value :</b>&nbsp;&nbsp;</td><td style="float:right;padding-bottom: 0px;padding-top: 0px;">' + totalGst.toFixed(2) + '</td><td></td></tr>';
            str += '<tr style="border-bottom:thin dashed;font:12px TimesNewRomen;"><td colspan="10" style="padding-top: 0px;padding-bottom: 0px;"></td><td style="padding-top: 0px;padding-bottom: 0px;"><b>Round Off(+/-) :</b>&nbsp;&nbsp;</td><td style="float:right;padding-top: 0px;padding-bottom: 0px;">' + rndoff.toFixed(2) + '</td><td></td></tr>';
            str += '<tr style="border-bottom:thin dashed; padding-left:20px;font-family:monospace;font-size:14px;"><td  colspan="8">' + netwrd + ' Rupees  and ' + netwrd1 + ' Paise</td> </td><td></td><td></td><td><b>Net Value :</b>&nbsp;&nbsp;</td><td style="float:right;padding-top: 0px;padding-bottom: 0px;">' + netvalue.toFixed(2) + '</td><td></td></tr></tbody></table ></div>';
            str += '<table border="0" class="rpttotal1" align="center" style="width: 100%; border-collapse: collapse; bordercolor:blue;"><tbody><tr style="font:12px TimesNewRomen;"><td  colspan="8" style="padding-left:20px"></td><td colspan="1" style="padding-top: 20px;font:bold 12px TimesNewRomen;float:right;"><b>For ' + distributor + '</b></td></tr>';
            str += '<tr style="font:12px TimesNewRomen;"><td colspan="8" style="padding-left:20px">Buyers Sign</td><td colspan="3" style="float:right">Authorized Signatory</td></tr></tbody></table><table class="pageno"><tr style="font:12px TimesNewRomen;"><td colspan="8" style="padding-left:350px;float:right">' + pagenumber + '</td><td colspan="3" style="float:right"></td></tr></table></div>';
            $('#div_Print').append(str);
        }

        function print() {
            var contents = $("#div_Print").html();
            var frame1 = $('<iframe />');
            frame1[0].name = "frame1";
            frame1.css({ "position": "absolute", "top": "-1000000px" });
            $("body").append(frame1);
            var frameDoc = frame1[0].contentWindow ? frame1[0].contentWindow : frame1[0].contentDocument.document ? frame1[0].contentDocument.document : frame1[0].contentDocument;
            frameDoc.document.open();
            frameDoc.document.write('<html><head><title>Order Print</title>');
            frameDoc.document.write('</head><body>');
            frameDoc.document.write(contents);
            frameDoc.document.write('</body></html>');
            frameDoc.document.close();
            setTimeout(function () {
                window.frames["frame1"].focus();
                window.frames["frame1"].print();
                frame1.remove();
            }, 500);
        }


        $(document).ready(function () {
            loadData(); resetOrderEntry();
            loadmodal(); loadscheme();

            $(document).on('click', '#newsorder', function () {
                if (<%=Session["sf_type"]%>== 5) {
                    window.location.href = "Purchase_Order1.aspx?Type=2";
                }
                else {
                    clearOrder();
                    //$('#exampleModal').modal('toggle');
                    if (subdiv == "41") {
                        var url = "../Stockisthap/MyOrder1.aspx";
                        window.open(url, '_self');
                    }
                    else {
                        $('#exampleModal').modal('toggle');
                    }

                    $('#addressid').text('');
                    AddRow(0);
                    $(document).find('#free_table tbody').empty();
                    $('#message-text').val('');
                    arr = [];
                    //$('#recipient-name').trigger('chosen:open');
                }
            });
            $(document).on('click', '#svorders', function () {
                svOrder();
            });

            $('#btnclose').on('click', function () {

                var Ans = confirm("Do you want to close this page?");
                if (Ans == true)
                    return true;
                else
                    return false;
            });

            $('#btntimesClose').on('click', function () {
                var Ans = confirm("Do you want to close this page?");
                if (Ans == true)
                    return true;
                else
                    return false;
            });
        });

    </script>
    <script type="text/javascript">
        $(function () {

            var Get_localData = localStorage.getItem("Date_Details");

            Get_localData = JSON.parse(Get_localData);

            //if (Get_localData != "" && Get_localData != null && Get_localData[4] == page) {
            if (Get_localData != "" && Get_localData != null) {

                var Dates = Get_localData[0].split('-');

                var fdj = Dates[2].trim() + '-' + Dates[1] + '-' + Dates[0] + ' ' + ' 00:00:00';
                var nfgresd = Dates[5] + '-' + Dates[4] + '-' + Dates[3].trim() + ' ' + ' 00:00:00';

                pgNo = parseFloat(Get_localData[1]); PgRecords = parseFloat(Get_localData[2]); flag = '1';

                const utcDate = new Date(fdj);
                const utcDate1 = new Date(nfgresd);

                var start = utcDate;
                var end = utcDate1;
            }
            else {
                var start = moment();
                var end = moment();
            }

            function cb(start, end, flag) {

                if (flag == '1') {

                    var F_dat = start.getDate();
                    var F_dat1 = start.getMonth() + 1;
                    var F_dat2 = start.getFullYear();
                    var f_date3 = F_dat + '-' + F_dat1 + '-' + F_dat2;

                    var E_dat = end.getDate();
                    var E_dat1 = end.getMonth() + 1;
                    var E_dat2 = end.getFullYear();
                    var E_date3 = E_dat + '-' + E_dat1 + '-' + E_dat2;

                    $('#reportrange span').html(f_date3 + ' - ' + E_date3);
                }
                else {
                    pgNo = 1;
                    $('#reportrange span').html(start.format('DD-MM-YYYY') + ' - ' + end.format('DD-MM-YYYY'));
                }

                namesArr = [];
                namesArr.push($('#reportrange span').text());
                namesArr.push(pgNo);
                namesArr.push($(".data-table-basic_length option:selected").text());
                namesArr.push($('#tSearchOrd').val());
                namesArr.push(page);
                window.localStorage.setItem("Date_Details", JSON.stringify(namesArr));
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
            cb(start, end, flag);

        });
    </script>
</asp:Content>

