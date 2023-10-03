﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master_DIS.master" AutoEventWireup="true" CodeFile="MyOrder_Edit1.aspx.cs" Inherits="MyOrder_Edit1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.2/js/bootstrap-select.min.js"></script>
    <%--  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-confirm/3.3.2/jquery-confirm.min.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-confirm/3.3.2/jquery-confirm.min.js"></script>--%>
    <link href="../alertstyle/jquery-confirm.min.css" rel="stylesheet" />
    <script src="../alertstyle/jquery-confirm.min.js"></script>
    <style>
        .daterangepicker {
            z-index: 10000000;
        }

        .Spinner-Input {
            width: auto !important;
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
    </style>
    <div class="modal fade" id="exampleModal" style="z-index: 10000000; overflow-y: auto;" tabindex="0" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" role="document" style="width: 1200px !important">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Edit Order Entry</h5>
                    <button type="button" id="btntimesClose" class="close" style="margin-top: -20px;" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <div class="card" style="padding: 8px; margin-top: 0px;">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="rou-name">Route Name:</label>
                                <span style="color: Red">*</span>
                                <select id="route-name">
                                </select>
                            </div>
                            <div class="col-sm-4">
                                <label for="recipient-name">Customer Name:</label>
                                <span style="color: Red">*</span>
                                <select class="form-control" id="recipient-name">
                                </select>
                                <br />
                                <br />
                                <label id="addressid"></label>
                            </div>
                            <div class="col-sm-4">
                                <label for="recpt-dt">Date:</label><span style="color: Red">*</span>
                                <input type="text" readonly="readonly" class="form-control" id="recpt-dt" />
                            </div>
                            <div class="col-sm-4" style="display: none;">
                                <label for="message-text" class="col-form-label">Note:</label>
                                <textarea class="form-control" id="message-text"></textarea>
                            </div>
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

                        <div class="col-sm-offset-8 form-horizontal" style="margin-top: 80px;">
                            <label class=" col-xs-3 control-label">
                                Tax Total :
                            </label>
                            <div class="col-sm-9">
                                <div class="input-group">
                                    <div class="input-group-addon currency" style="background: #00bcd4; color: white; border-color: #00bcd4;">
                                        <i class="fa fa-inr"></i>
                                    </div>
                                    <%--<input data-cell="G1" id="Tax_GST" data-format="0,0.00" class="form-control" readonly />--%>
                                    <input data-cell="G1" id="txt_tax_total" data-format="0,0.00" class="form-control txtblue" readonly />
                                </div>
                            </div>
                        </div>

                        <%--<div class="col-sm-offset-8 form-horizontal div_cgst" style="margin-top: 80px;">
                            <label class=" col-xs-3 control-label" style="font-size: 12.5px;">
                                CGST Total :
                            </label>
                            <div class="col-sm-9">
                                <div class="input-group">
                                    <div class="input-group-addon currency">
                                        <i class="fa fa-inr"></i>
                                    </div>                                   
                                    <input data-cell="G1" id="txt_cgst" data-format="0,0.00" class="form-control" readonly />
                                </div>
                            </div>
                        </div>--%>

                        <%--<div class="col-sm-offset-8 form-horizontal div_sgst" style="margin-top: 120px;">
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
                        </div>--%>


                        <%--<div class="col-sm-offset-8 form-horizontal div_igst" style="margin-top: 160px;">
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
                    <button type="button" class="btn btn-secondary" data-dismiss="modal" id="btnclose">Close</button>
                    <button type="button" class="btn btn-primary" id="svorders">Save</button>
                </div>
            </div>
        </div>
    </div>

    <div class="container" style="display: none;" id="myAlert">
        <h2>Dismissal Alert Messages</h2>
        <div class="alert alert-success alert-dismissable" id="myAlert2">
            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
            Success! message sent successfully.
        </div>
    </div>

    <link href="../css/SpinnerInput.css" rel="stylesheet" type="text/css" />
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
            background-color: #666;
            color: white;
        }

        .chosen-container {
            width: 100% !important;
        }

        .modal {
            background-color: #0000000f;
        }
    </style>

    <script language="javascript" type="text/javascript">

        var AllOrders = []; var Orders = []; Prds = ""; var Product_array = []; NewOrd = []; var filter_unit = []; units = ""; units1 = "";
        var scheme = []; var today = ''; var arr = []; var namesArr = []; var flag = 0; var CQ = ''; var All_Tax = []; var Retailer_State = 0;
        var Product_Details = []; var All_Product_Details = []; var Cust_Price = []; var Rate_List_Code = '';
        var All_Retailer = []; var new_height = 0; var currentheight = 0; var scrollhightchnage = 0; var scrollhightchnageDel = 0; var tax_arr = [];

        Dt = new Date(); sDt = Dt.getFullYear() + '-' + ((Dt.getMonth() < 9) ? "0" : "") + (Dt.getMonth() + 1) + '-' + ((Dt.getDate() < 10) ? "0" : "") + Dt.getDate() + ' 00:00:00';

        var Dist_state_code = ("<%=Session["State"].ToString()%>");
        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1;
        var yyyy = today.getFullYear();
        if (dd < 10) { dd = '0' + dd }
        if (mm < 10) { mm = '0' + mm }
        today = yyyy + '-' + mm + '-' + dd;

        var Order_id = "<%=Order_ID%>";
        var division_Code = "<%=Div%>";
        var Stockist_Code = "<%=stk%>";
        var Cust_Code = "<%=Cust_Code%>";
        var order_date = "<%=Order_date%>";
        var type = "<%=type%>";

        $(document).keydown(function (e) {
            if (e.keyCode == 27)
                return false;
        });

        $(document).on('keypress', '#message-text', function (e) {
            if (e.keyCode == 34 || e.keyCode == 39 || e.keyCode == 38 || e.keyCode == 60 || e.keyCode == 62 || e.keyCode == 92) return false;
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
            if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57) && e.which != 46) {
                return false;
            }
        });
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
        function loadmodal() {

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "MyOrder_Edit1.aspx/Get_Product_Tax",
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
                url: "MyOrder_Edit1.aspx/Get_Product_unit",
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
                url: "myOrders.aspx/Get_Product_Cat_Details",
                dataType: "json",
                success: function (data) {
                    Cat_Details = JSON.parse(data.d) || [];
                    $('#myDIV').html('');
                    for (var g = 0; g < Cat_Details.length; g++) {
                        $('#myDIV').append('<button class="btn category ' + ((Cat_Details[g].Product_Cat_Code == -1) ? "active" : "") + '" value="' + Cat_Details[g].Product_Cat_Code + '" >' + Cat_Details[g].Product_Cat_Name + '</button>');
                    }
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }

        function bind_product(prdCode) {
            Prds = '';
            for (var k = 0; k < Product_array.length; k++) {

                if (Product_array[k].Product_Detail_Code == prdCode) {
                    Prds += "<option selected value='" + Product_array[k].Product_Detail_Code + "'>" + Product_array[k].Product_Detail_Name + "</option>";
                }
                else {
                    Prds += "<option value='" + Product_array[k].Product_Detail_Code + "'>" + Product_array[k].Product_Detail_Name + "</option>";
                }
            }
        }

        function add_new() {

            itm = {}
            itm.PCd = ''; itm.sPCd = ''; itm.s_pcode = ''; itm.PName = ''; itm.Unit = ''; itm.Rate = "0"; itm.Qty = 0; itm.Qty_c = 0; itm.Free = "0"; itm.Discount = "0"; itm.Dis_value = "0";
            itm.Tax = "0"; itm.Tax_value = "0"; itm.Total = "0"; itm.Gross_Amt = "0"; itm.Sub_Total = 1; itm.of_Pro_Code = ''; itm.of_Pro_Name = ''; itm.of_Pro_Unit = ''; itm.umo_unit = ''; itm.con_fac = '';
            itm.Tax_details = '';
            NewOrd.push(itm);
        }

        function ReloadTable() {

            currentheight = $('.modal-content').height();
            $("#OrderList TBODY").html("");

            for (var st = 0; st < Orders.length; st++) {

                Prds = '';
                add_new();
                bind_product(Orders[st].Product_Code);

                tr = $("<tr class=" + Orders[st].Product_Code + "></tr>");
                slno = st + 1;
                var filter_unit = []; units = ""; units1 = "";

                filter_unit = All_unit.filter(function (w) {
                    return (Orders[st].Product_Code == w.Product_Detail_Code);
                });

                if (filter_unit.length > 0) {
                    for (var z = 0; z < filter_unit.length; z++) {
                        units += "<li id='s' name='itms' value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</li>";
                        units1 += "<option value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";
                    }
                }
                if (parseInt(Orders[st].TotalStock) > 0) {
                    $(tr).html("<td class='rowno'><label style='margin-bottom:0px;'><input type='checkbox' class='slitm'> <span class='rwsl'>" + slno + "</span></label></td><td class='pro_td' style='width:23%;padding: 9px 0px 0px 0px;'><select class='ddlProd' style='margin-top:-3px;height:25px;'><option value=''>--select--</option>" + Prds + "</select><div class='second_row_div' style='display:none; font-size:11.5px;padding: 5px 0px 0px 3px;'><i class='fa fa-tags'></i><label>Dis % :</label>&nbsp;&nbsp;<label class='dis_val_class' id='dis_val_class'>" + Orders[st].discount + "</label>&nbsp;&nbsp;<i class='fa fa-stack-overflow' ></i><label>Stock :</label>&nbsp;&nbsp;<label class='stockClass' style='color:rgb(39 194 76)' id='stock_id'>" + Orders[st].TotalStock + "</label>&nbsp;&nbsp;<i class='fa fa-code' ></i><label>Con_Fav :</label>&nbsp;&nbsp;<label id='Con_fac' class='Con_fac'>" + Orders[st].Con_Qty + "</label></div></td><td style='display:none;'><input type='hidden' class='sale_code' /></td><td id='Td1' style='width: 18%;padding: 8px 0px 0px 30px;'><select class='cbAlwTyp ispinner'><option value='1'>Select</option></select><div class='Spinner-Input'><div style='display:none;' class='spinner-Value_val' value=" + Orders[st].umo + "></div><div class='Spinner-Value'>" + Orders[st].Unit_Name + "</div><div class='Spinner-Modal'><ul>" + units + "</ul></div></div><input type='text' class='txtQty validate' pval='0.00' value=" + Orders[st].qty + " style='text-align:right;width: 42%;'></td><td class='tdRate' style='text-align:right; padding: 17px 0px 0px 0px;'>" + parseFloat(Orders[st].Rate).toFixed(2) + "</td><td class='fre' style='text-align:right;padding: 17px 9px 0px 0px;'>0</td><td style='display:none;'<label name='free' class='fre'></lable></td><td style='display:none' ><input type='hidden' class='fre1' name='fre1' ></td><td style='display:none' ><input type='hidden' class='of_pro_name'  name='of_pro_name' ></td><td style='display:none' ><input type='hidden' class='of_pro_code'  name='of_pro_code' ></td><td class='tddis_val' style='text-align:right; padding: 17px 11px 0px 0px;'>" + parseFloat(Orders[st].discount_price).toFixed(2) + "</td><td style='display:none'><input type='hidden' class='disc_value' name='disc_value'></td><td style='display:none'><input type='hidden' class='disc_value'  name='disc_value' ></td><td class='tdtotal' style='text-align:right;padding: 17px 7px 0px 0px;'>" + parseFloat(Orders[st].value).toFixed(2) + "</td><td style='display:none';><input class='tax_val' type='hidden' value=" + Orders[st].Tax_Val + " /></td><td class='tdtax' style='text-align:right;padding: 17px 0px 0px 0px;'>" + parseFloat(Orders[st].Tax_value).toFixed(2) + "</td><td style='display:none;'><input type='hidden' class='tdcgst' id='tdcgst' /></td><td style='display:none;'><input type='hidden' class='tdsgst' id='tsgst' ></td><td style='display:none;'><input type='hidden' class='tdigst' id='tigst' ></td><td class='tdAmt' style='text-align:right;padding: 17px 6px 0px 0px;width: 10%;'>" + parseFloat(Orders[st].Tax_value + Orders[st].value).toFixed(2) + "</td><td style='display:none;'><label id='pcode' class='pcode'></label></td><td style='display:none;'><input type='hidden' class='erp_code' value=" + Orders[st].Con_Qty + " /><input type='hidden' class='default_umo' value=" + Orders[st].Default_UOM + " /><input type='hidden' class='default_umo_val' value=" + Orders[st].Default_UOMQty + " /></td>");
                }
                else {
                    $(tr).html("<td class='rowno'><label style='margin-bottom:0px;'><input type='checkbox' class='slitm'> <span class='rwsl'>" + slno + "</span></label></td><td class='pro_td' style='width:23%;padding: 9px 0px 0px 0px;'><select class='ddlProd' style='margin-top:-3px;height:25px;'><option value=''>--select--</option>" + Prds + "</select><div class='second_row_div' style='display:none; font-size:11.5px;padding: 5px 0px 0px 3px;'><i class='fa fa-tags'></i><label>Dis % :</label>&nbsp;&nbsp;<label class='dis_val_class' id='dis_val_class'>" + Orders[st].discount + "</label>&nbsp;&nbsp;<i class='fa fa-stack-overflow' ></i><label>Stock :</label>&nbsp;&nbsp;<label class='stockClass' style='color:red' id='stock_id'>" + Orders[st].TotalStock + "</label>&nbsp;&nbsp;<i class='fa fa-code' ></i><label>Con_Fav :</label>&nbsp;&nbsp;<label id='Con_fac' class='Con_fac'>" + Orders[st].Con_Qty + "</label></div></td><td style='display:none;'><input type='hidden' class='sale_code' /></td><td id='Td1' style='width: 18%;padding: 8px 0px 0px 30px;'><select class='cbAlwTyp ispinner'><option value='1'>Select</option></select><div class='Spinner-Input'><div style='display:none;' class='spinner-Value_val' value=" + Orders[st].umo + "></div><div class='Spinner-Value'>" + Orders[st].Unit_Name + "</div><div class='Spinner-Modal'><ul>" + units + "</ul></div></div><input type='text' class='txtQty validate' pval='0.00' value=" + Orders[st].qty + " style='text-align:right;width: 42%;'></td><td class='tdRate' style='text-align:right; padding: 17px 0px 0px 0px;'>" + parseFloat(Orders[st].Rate).toFixed(2) + "</td><td class='fre' style='text-align:right;padding: 17px 9px 0px 0px;'>0</td><td style='display:none;'<label name='free' class='fre'></lable></td><td style='display:none' ><input type='hidden' class='fre1' name='fre1' ></td><td style='display:none' ><input type='hidden' class='of_pro_name'  name='of_pro_name' ></td><td style='display:none' ><input type='hidden' class='of_pro_code'  name='of_pro_code' ></td><td class='tddis_val' style='text-align:right; padding: 17px 11px 0px 0px;'>" + parseFloat(Orders[st].discount_price).toFixed(2) + "</td><td style='display:none'><input type='hidden' class='disc_value' name='disc_value'></td><td style='display:none'><input type='hidden' class='disc_value'  name='disc_value' ></td><td class='tdtotal' style='text-align:right;padding: 17px 7px 0px 0px;'>" + parseFloat(Orders[st].value).toFixed(2) + "</td><td style='display:none';><input class='tax_val' type='hidden' value=" + Orders[st].Tax_Val + " /></td><td class='tdtax' style='text-align:right;padding: 17px 0px 0px 0px;'>" + parseFloat(Orders[st].Tax_value).toFixed(2) + "</td><td style='display:none;'><input type='hidden' class='tdcgst' id='tdcgst' /></td><td style='display:none;'><input type='hidden' class='tdsgst' id='tsgst' ></td><td style='display:none;'><input type='hidden' class='tdigst' id='tigst' ></td><td class='tdAmt' style='text-align:right;padding: 17px 6px 0px 0px;width: 10%;'>" + parseFloat(Orders[st].Tax_value + Orders[st].value).toFixed(2) + "</td><td style='display:none;'><label id='pcode' class='pcode'></label></td><td style='display:none;'><input type='hidden' class='erp_code' value=" + Orders[st].Con_Qty + " /><input type='hidden' class='default_umo' value=" + Orders[st].Default_UOM + " /><input type='hidden' class='default_umo_val' value=" + Orders[st].Default_UOMQty + " /></td>");
                }
                $("#OrderEntry > TBODY").append(tr);

                $('.second_row_div').show();
                var idx = $("#OrderEntry > TBODY > tr").index();
                var roww = $("#OrderEntry > TBODY > tr").eq([idx]).closest('tr');

                if (Dist_state_code == Retailer_State || Retailer_State == 0) { var type = 0; } else { var type = 1; }

                tax_filter = All_Tax.filter(function (r) {
                    return (r.Product_Detail_Code == Orders[st].Product_Code)// && (r.Tax_Method_Id == type || r.Tax_Method_Id == 2))
                });

                var append = ''; var total_tax_per = 0;

                if (tax_filter.length > 0) {

                    for (var z = 0; z < tax_filter.length; z++) {
                        append += "<label style='margin-left: 6px;' class='lbl_tax_type'>" + tax_filter[z].Tax_Type + "</label>:&nbsp;&nbsp;<label class='Tax_name' id='Tax_name'>" + tax_filter[z].Tax_name + "</label>&nbsp;&nbsp;";
                        var Push_data = tax_filter[z].Tax_Type;
                        total_tax_per = total_tax_per + parseFloat(tax_filter[z].Tax_Val);
                        NewOrd[idx][Push_data] = tax_filter[z].Tax_Val;

                        tax_arr.push({
                            pro_code: Orders[st].Product_Code,
                            Tax_Code: tax_filter[z].Tax_Id,
                            Tax_Name: tax_filter[z].Tax_Type,
                            Tax_Amt: 0,
                            Tax_Per: tax_filter[z].Tax_Val,
                            umo_code: 0
                        });
                    }
                    NewOrd[idx].Tax_details = tax_arr;
                    NewOrd[idx].Total_Tax = total_tax_per;
                    roww.find('.second_row_div').append(append);
                }

                chck_pck(Orders[st].Con_Qty, Orders[st].Con_Qty, Orders[st].Rate, Orders[st].qty, idx, $("#OrderEntry > TBODY > tr").eq([idx]).find('.txtQty '));

                NewOrd[idx].PCd = Orders[st].Product_Code;
                NewOrd[idx].sPCd = Orders[st].Product_Code;
                NewOrd[idx].s_pcode = Orders[st].Sale_Erp_Code;
                NewOrd[idx].Rate = Orders[st].Rate;
                NewOrd[idx].Unit = Orders[st].Unit_Name;
                NewOrd[idx].PName = Orders[st].Product_Name;
                NewOrd[idx].Qty = Orders[st].Quantity;
                NewOrd[idx].Free = 0;
                NewOrd[idx].con_fac = Orders[st].Con_Qty;
                NewOrd[idx].Qty_c = Orders[st].qty;
                NewOrd[idx].umo_unit = Orders[st].umo;

                CQ = Orders[st].qty;

                $("#OrderEntry > TBODY > tr").eq([idx]).closest('tr').find('.sale_code').html(Orders[st].Sale_Erp_Code);
                $("#OrderEntry > TBODY > tr").eq([idx]).closest('tr').find('.fre').attr("id", Orders[st].Product_Code);
                $("#OrderEntry > TBODY > tr").eq([idx]).closest('tr').find('.fre1').attr("id", Orders[st].Product_Code);

                //getscheme(Orders[st].Sale_Erp_Code, Orders[st].qty, Orders[st].unit, $("#OrderEntry > TBODY > tr")[idx], '', '', Orders[st].Product_Code);

                NewOrd[idx].Free = Orders[st].free;
                NewOrd[idx].of_Pro_Code = Orders[st].Offer_ProductCd;
                NewOrd[idx].of_Pro_Name = Orders[st].Offer_ProductNm;
                NewOrd[idx].of_Pro_Unit = Orders[st].off_pro_unit;
                NewOrd[idx].Discount = Orders[st].discount;
                NewOrd[idx].Dis_value = Orders[st].discount_price;

                //CalcAmt($("#OrderEntry > TBODY > tr")[idx]);
                itr = $("#OrderEntry > TBODY > tr")[idx].closest('tr');
                idx = $(itr).index();
                rt = parseFloat($(itr).find('.tdRate').text()); if (isNaN(rt)) rt = 0; qt = parseFloat($(itr).find('.txtQty').val()); if (isNaN(qt)) qt = 0;
                tax = parseFloat($(itr).find('.tdtax').text()); if (isNaN(tax)) tax = 0;
                conf = parseFloat($(itr).find('.Con_fac').text()); if (isNaN(conf)) conf = 1;
                NewOrd[idx].Sub_Total = ((qt * conf) * rt).toFixed(2);

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

                // calc_stock($("#OrderEntry > TBODY > tr")[idx]);

                new_height = $('.modal-content').height();
                scrollhightchnage = scrollhightchnage + ((new_height - currentheight) * 2);
                scrollhightchnageDel = scrollhightchnage;
                $('#exampleModal').scrollTop(scrollhightchnage);

            }
            $('.ddlProd').chosen();

            $('.ddlProd').on('chosen:showing_dropdown', function () {

                var prdcd = $('.ddlProd option:selected').val();
                var selected_cat_code = $('.btn.active').val();
                var selected_cat_Name = $('.btn.active').text();

                var filtered_prd = Product_array.filter(function (k) {
                    return (k.Product_Cat_Code == selected_cat_code);
                    //return (k.Product_Brd_Code == selected_cat_code);
                });

                $(this).html('');
                filtered_prd = selected_cat_code == -1 ? Product_array : filtered_prd;

                let str = '<option value=-1>--select--</option>';
                for (var h = 0; h < filtered_prd.length; h++) {
                    str += `<option value="${filtered_prd[h].Product_Detail_Code}">${filtered_prd[h].Product_Detail_Name}</option>`;
                }
                $(this).html(str);
                $(this).trigger("chosen:updated");

            });
        }

        function AddRow(type) {
            var ckselect = 0
            var ckselect = 0
            $('.ddlProd').each(function () {
                var rr = $(this).find('option:selected').val();
                if (rr == '-1') {
                    ckselect += 1;
                    rjalert('Error!', 'enter empty rows', 'error');
                    return false;
                }
            })
            if (ckselect == 0) {

                add_new();
                currentheight = $('.modal-content').height();

                bind_product("");

                tr = $("<tr class='subRow'></tr>");
                $(tr).html("<td class='rowno'><label style='margin-bottom:0px;'><input type='checkbox' class='slitm'> <span class='rwsl'>" + ($("#OrderList > TBODY > tr").length + 1) + "</span></label></td><td class='pro_td' style='width:23%;padding: 9px 0px 0px 0px;'><select class='ddlProd' style='margin-top:-3px;height:25px;'><option value=''>--select--</option>" + Prds + "</select><div class='second_row_div' style='display:none; font-size:11.5px;padding: 5px 0px 0px 3px;'><label class='cgst_lbl'>CGST :</label>&nbsp;&nbsp;<label class='cgst_tax' id='cgst_tax'></label><input class='cgst_tax_val' type='hidden' />&nbsp;&nbsp;<label class='sgst_lbl'>SGST :</label>&nbsp;&nbsp;<label class='sgst_tax' id='sgst_tax'></label><input class='sgst_tax_val' type='hidden' />&nbsp;&nbsp;<label class='igst_lbl'>IGST :</label>&nbsp;&nbsp;<label class='igst_tax' id='igst_tax'></label><input class='igst_tax_val' type='hidden' />&nbsp;&nbsp;<i class='fa fa-tags'></i><label>Dis % :</label>&nbsp;&nbsp;<label class='dis_val_class' id='dis_val_class'></label>&nbsp;&nbsp;<i class='fa fa-stack-overflow' ></i><label>Stock :</label>&nbsp;&nbsp;<label class='stockClass' id='stock_id'></label>&nbsp;&nbsp;<i class='fa fa-code' ></i><label>Con_Fav :</label>&nbsp;&nbsp;<label id='Con_fac' class='Con_fac'></label></div></td><td style='display:none;' ><input type='hidden' class='sale_code' /></td><td id='Td1' style='width: 18%;padding: 8px 0px 0px 30px;'><select class='cbAlwTyp ispinner'><option value='1'>Select</option></select><div class='Spinner-Input'><div class='Spinner-Value'>Select</div><div class='Spinner-Modal'><ul>Select</ul></div></div><input type='text' class='txtQty validate' pval='0.00' value   style='text-align:right;width: 42%;'></td><td class='tdRate' style='text-align:right; padding: 17px 0px 0px 0px;'>0.00</td><td class='fre' style='text-align:right;padding: 17px 9px 0px 0px;'>0</td><td style='display:none;'<label name='free' class='fre'></lable></td><td style='display:none' ><input type='hidden' class='fre1'  name='fre1' ></td><td style='display:none' ><input type='hidden' class='of_pro_name'  name='of_pro_name' ></td><td style='display:none' ><input type='hidden' class='of_pro_code'  name='of_pro_code' ></td><td class='tddis_val' style='text-align:right; padding: 17px 11px 0px 0px;'>0</td><td style='display:none'><input type='hidden' class='disc_value' name='disc_value'></td><td style='display:none'><input type='hidden' class='disc_value'  name='disc_value' ></td><td class='tdtotal' style='text-align:right;padding: 17px 7px 0px 0px;'>0.00</td><td style='display:none';><input class='tax_val' type='hidden' /></td><td class='tdtax' style='text-align:right;padding: 17px 0px 0px 0px;'>0.00</td><td style='display:none;'><input type='hidden' class='tdcgst' id='tdcgst' /></td><td style='display:none;'><input type='hidden' class='tdsgst' id='tsgst' ></td><td style='display:none;'><input type='hidden' class='tdigst' id='tigst' ></td><td class='tdAmt' style='text-align:right;padding: 17px 6px 0px 0px;width: 10%;'>0.00</td><td style='display:none;'><label id='pcode' class='pcode'></label></td><td style='display:none;' ><input type='hidden' class='erp_code' /><input type='hidden' class='default_umo' /><input type='hidden' class='default_umo_val' /></td>");
                $("#OrderEntry > TBODY").append(tr);
                resetSL();
                $('.ddlProd').chosen();

                $('.ddlProd').on('chosen:showing_dropdown', function () {

                    var selected_cat_code = $('.btn.active').val();
                    var selected_cat_Name = $('.btn.active').text();

                    var filtered_prd = Product_array.filter(function (k) {
                        return (k.Product_Cat_Code == selected_cat_code);
                        //return (k.Product_Brd_Code == selected_cat_code);
                    });

                    $(this).html('');
                    filtered_prd = selected_cat_code == -1 ? Product_array : filtered_prd;

                    let str = '<option value=-1>--select--</option>';
                    for (var h = 0; h < filtered_prd.length; h++) {
                        str += `<option value="${filtered_prd[h].Product_Detail_Code}">${filtered_prd[h].Product_Detail_Name}</option>`;
                    }
                    $(this).html(str);
                    $(this).trigger("chosen:updated");

                });

                if (type == 1) {
                    event.stopPropagation();
                    $('#OrderEntry tr:last').find(".ddlProd").trigger('chosen:open');
                }

                new_height = $('.modal-content').height();
                scrollhightchnage = scrollhightchnage + ((new_height - currentheight) * 2);
                scrollhightchnageDel = scrollhightchnage;
                $('#exampleModal').scrollTop(scrollhightchnage);
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
                               $(".slitm:checked").each(function () {

                itr = $(this).closest('tr');
                idx = $(itr).index();
                var prod = itr.find('.ddlProd').val();
                //getscheme($(itr).find('.sale_code').text(), 0, $(itr).find('.Spinner-Value').text(), itr, $(itr).find('.fre1').text(), '', $(itr).find('.pcode').text());
                $(this).closest('tr').remove();
                NewOrd.splice(idx, 1);
                if (prod != "") {
                    $(document).find('.' + prod).each(function () {
                        // calc_stock($(this).closest('tr'));
                    });
                }

            }); resetSL(); ReCalc();
         
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

        $(document).on("click", ".category", function (e) {
            e.preventDefault();
            $('.category').removeClass('active');
            $('.category').css('color', 'black');
            $(this).addClass('active');
            $(this).css('color', 'white');
        });
        $(document).on("change", ".ddlProd", function () {

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
                        rjalert('Error!', 'Rate not available', 'error');
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

                        if (Prod.length > 0) {
                            var unitrate = 0;
                            for (var z = 0; z < filter_unit.length; z++) {
                                if (filter_unit[z].Move_MailFolder_Id == Prod[0].Base_Unit_code) {
                                    unitrate = Prod[0].RP_Base_Rate;
                                    units += "<li id=" + unitrate + " name='itms' value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</li>";
                                    units1 += "<option  id=" + unitrate + " value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";
                                }
                                //else if (filter_unit[z].Move_MailFolder_Id != Prod[0].UnitCode && filter_unit[z].Move_MailFolder_Id == Prod[0].Base_Unit_code) {
                                //    unitrate = Prod[0].RP_Base_Rate / Prod[0].Sample_Erp_Code
                                //    units += "<li id=" + unitrate + " name='itms' value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</li>";
                                //    units1 += "<option  id=" + unitrate + " value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";
                                //}

                                else {
                                    unitrate = Prod[0].RP_Base_Rate * filter_unit[z].Sample_Erp_Code
                                    units += "<li id=" + unitrate + " name='itms' value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</li>";
                                    units1 += "<option  id=" + unitrate + " value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";

                                }

                                //else if (filter_unit[z].Move_MailFolder_Id == Prod[0].Base_Unit_code) {
                                //    unitrate = Prod[0].PTS / Prod[0].Sample_Erp_Code
                                //    units += "<li id=" + unitrate + " name='itms' value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</li>";
                                //    units1 += "<option  id=" + unitrate + " value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";

                                //}

                            }
                        }

                    }
                    // if (filter_unit.length > 0) {

                    //for (var z = 0; z < filter_unit.length; z++) {
                    //   units += "<li id='s' name='itms' value='" + filter_unit[z].Move_MailFolder_Id + "'>" + //filter_unit[z].Move_MailFolder_Name + "</li>";
                    //    units1 += "<option value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + //"</option>";
                    //  }
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

                                $(itr).find('#Td1').html("<select class='cbAlwTyp ispinner'><option value='1'>Select</option>" + units1 + "</select><div class='Spinner-Input'><div style='display:none;' class='spinner-Value_val' value=" + Prod[0].Unit_code + "></div><div class='Spinner-Value'>" + Prod[0].product_unit + "</div><div class='Spinner-Modal'><ul>" + units + "</ul></div></div><input type='text' autocomplete='off' id='txtQty' class='txtQty validate' pval='0.00' value='" + ((qt == 0) ? '' : qt) + "' style='text-align:right;width: 42%;'>");
                                setTimeout(function () { $(itr).find('#Td1').find('.txtQty').focus() }, 100);

                                if (Prod[0].Unit_code == Prod[0].UnitCode)
                                    var unt_rate = Prod[0].RP_Base_Rate;
                                else
                                    var unt_rate = Prod[0].RP_Base_Rate * Prod[0].Sample_Erp_Code;
                                if (unt_rate == 'null' || typeof unt_rate == "undefined" || unt_rate == "" || isNaN(unt_rate)) { unt_rate = 0; }

                                $(itr).find('.tdRate').text(parseFloat(unt_rate).toFixed(2));
                                NewOrd[idx].Rate = unt_rate;
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
                            //        $(itr).find('.Con_fac').text(1);
                            //        NewOrd[idx].con_fac = 1;
                            //        NewOrd[idx].umo_unit = Prod[0].Base_Unit_code;
                            //    }
                            //    else {
                            //        if (Prod[0].Base_Unit_code == Prod[0].UnitCode)
                            //            var unt_rate = Prod[0].RP_Base_Rate;
                            //        else
                            //            var unt_rate = Prod[0].RP_Base_Rate / Prod[0].Sample_Erp_Code;
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
                            //        $(itr).find('.Con_fac').text(Prod[0].Sample_Erp_Code);
                            //        NewOrd[idx].con_fac = Prod[0].Sample_Erp_Code;
                            //        NewOrd[idx].umo_unit = Prod[0].Unit_code;

                            //    }
                            //    else {
                            //        var unt_rate = Prod[0].RP_Base_Rate * Prod[0].Con_fac;
                            //        if (unt_rate == 'null' || typeof unt_rate == "undefined" || unt_rate == "" || isNaN(unt_rate)) { unt_rate = 0; }

                            //        $(itr).find('.tdRate').text(parseFloat(unt_rate).toFixed(2));
                            //        NewOrd[idx].Rate = unt_rate;
                            //    }
                            //}
                            set = 0;
                        }
                        else {
                            $(itr).find('.ddlProd').val('');
                            $(itr).find('.ddlProd ').chosen("destroy");
                            $(itr).find('.ddlProd').chosen();
                            $(itr).find('.Spinner-Value').text('Select');
                            $(itr).find('.tdRate').text("0.00");
                            $(itr).find('.txtQty').val('');
                            $(itr).find('.second_row_div').text('');
                            NewOrd[idx].Unit = 'Select';
                            NewOrd[idx].umo_unit = '';
                            CalcAmt($(itr));
                            rjalert('Error!', 'Product Units Already Selected', 'error');
                            //alert('Product Units Already Selected');
                            set = 1;
                            return false;
                        }
                    });

                    if (set == 0) {

                        $(itr).find('.fre').attr("id", sPCd);
                        $(itr).find('.fre1').attr("id", sPCd);
                        $(itr).find('.tddis_val').text('0.00');

                        // getscheme(Prod[0].Sale_Erp_Code, '', '', $(itr).find('.Spinner-Value').text(), itr, $(itr).find('.fre1').text(), $(itr).find('.pcode').text(), idx, $(this).find('option:selected').text());

                        $(itr).find('.pcode').html(sPCd);
                        NewOrd[idx].Discount = $(itr).find('.dis_per').text() || 0;
                        CalcAmt(itr);
                    }
                }
                else {
                    rjalert('Error!', 'Please Select Retailer', 'error');
                    //alert('Please Select Retailer');
                    $('.ddlProd').val('');
                    $('.ddlProd ').chosen("destroy");
                    $('.ddlProd').chosen();
                }
                calc_stock($(this).closest('tr'));
            }
        })
        //$(document).on("change", ".ddlProd", function () {

        //    itr = $(this).closest('tr');
        //    idx = $(itr).index();
        //    var selected_retailer_code = $('#recipient-name').val();

        //    if (selected_retailer_code != '' && selected_retailer_code != 0 && selected_retailer_code != '--- Select Retailer Name---') {

        //        $('.second_row_div').show();
        //        sPCd = $(this).val();
        //        $(this).closest("tr").attr('class', $(this).val());
        //        var P_Name = itr.find('.ddlProd').find('option:selected').text();
        //        var unt_rate = 0;

        //        Repeat_filter = NewOrd.filter(function (s, key) {
        //            return (s.PCd == sPCd && key != idx);
        //        });

        //        if (Repeat_filter.length == 0) {

        //            var Cust_price_details = Cust_Price.filter(function (a) {
        //                return (a.Product_Detail_Code == sPCd );
        //            })

        //            if (Cust_price_details.length > 0) {

        //                unt_rate = Cust_price_details[0].Rate;
        //                if (unt_rate == 'null' || typeof unt_rate == "undefined" || unt_rate == "" || isNaN(unt_rate)) { unt_rate = 0; }

        //                $(itr).find('.tdRate').text(parseFloat(unt_rate).toFixed(2));
        //                NewOrd[idx].Rate = unt_rate;

        //            }
        //            else {
        //                $(itr).find('.tdRate').text(0);
        //                NewOrd[idx].Rate = 0; unt_rate = 0;
        //            }

        //            Prod = Product_array.filter(function (a) {
        //                return (a.Product_Detail_Code == sPCd);
        //            })

        //            rt = parseFloat($(itr).find('.tdRate').text()); if (isNaN(rt)) rt = 0; qt = parseFloat($(itr).find('.txtQty').val()); if (isNaN(qt)) qt = 0; //tax = parseFloat(Prod[0].Tax_Val || 0);

        //            // chck_pck(Prod[0].Default_UOMQty, Prod[0].Default_UOM, unt_rate, qt, idx, $("#OrderEntry > TBODY > tr").eq([idx]).find('.txtQty '));
        //            //chck_pck(umoqty, DefaultUmo, prd_rate, CQ, idx, $(this));

        //            NewOrd[idx].PCd = sPCd;
        //            NewOrd[idx].sPCd = $(this).val();
        //            NewOrd[idx].s_pcode = Prod[0].Sale_Erp_Code;
        //            NewOrd[idx].Unit = Prod[0].Move_MailFolder_Name;
        //            NewOrd[idx].PName = P_Name;
        //            NewOrd[idx].Qty = qt;
        //            NewOrd[idx].Free = 0;


        //            if (Dist_state_code == Retailer_State || Retailer_State == 0) { var type = 0; } else { var type = 1; }

        //            var tax_filter = All_Tax.filter(function (r) {
        //                return (r.Product_Detail_Code == sPCd && (r.Tax_Method_Id == type || r.Tax_Method_Id == 2))
        //            });
        //            $(itr).find('.second_row_div').text('');
        //            $(itr).find('.second_row_div').append("<label>Dis % :</label>&nbsp;&nbsp;<label class='dis_val_class' id='dis_val_class'></label>&nbsp;&nbsp;<label>Stock :</label>&nbsp;&nbsp;<label class='stockClass' id='stock_id'></label>&nbsp;&nbsp;<label>Con_Fac :</label>&nbsp;&nbsp;<label id='Con_fac' class='Con_fac'></label>&nbsp;&nbsp;");

        //            var append = ''; var total_tax_per = 0;

        //            if (tax_filter.length > 0) {

        //                for (var z = 0; z < tax_filter.length; z++) {
        //                    append += "<label class='lbl_tax_type'>" + tax_filter[z].Tax_Type + "</label>:&nbsp;&nbsp;<label class='Tax_name' id='Tax_name'>" + tax_filter[z].Tax_name + "</label>&nbsp;&nbsp;";
        //                    var Push_data = tax_filter[z].Tax_Type;
        //                    total_tax_per = total_tax_per + parseFloat(tax_filter[z].Tax_Val);
        //                    NewOrd[idx][Push_data] = tax_filter[z].Tax_Val;

        //                    tax_arr.push({
        //                        pro_code: sPCd,
        //                        Tax_Code: tax_filter[z].Tax_Id,
        //                        Tax_Name: tax_filter[z].Tax_Type,
        //                        Tax_Amt: 0,
        //                        Tax_Per: tax_filter[z].Tax_Val,
        //                        umo_code: 0
        //                    });
        //                }
        //                NewOrd[idx].Tax_details = tax_arr;
        //                NewOrd[idx].Total_Tax = total_tax_per;
        //                $(itr).find('.second_row_div').append(append);
        //            }
        //            $(itr).find('#Td1').html("<select class='cbAlwTyp ispinner'><option value='1'>Select</option>" + Prod[0].Default_UOM + "</select><div class='Spinner-Input'><div style='display:none;' class='spinner-Value_val' value=" + Prod[0].Move_MailFolder_Name + "></div><div class='Spinner-Value'>" + Prod[0].Move_MailFolder_Name + "</div><div class='Spinner-Modal'><ul>" + Prod[0].Move_MailFolder_Name + "</ul></div></div><input type='text' autocomplete='off' id='txtQty' class='txtQty validate' pval='0.00' value='" + ((qt == 0) ? '' : qt) + "' style='text-align:right;width: 42%;'>");
        //            chck_pck(Prod[0].Default_UOMQty, Prod[0].Default_UOM, unt_rate, qt, idx, $("#OrderEntry > TBODY > tr").eq([idx]).find('.txtQty '));
        //            $(itr).find('.stockClass').text(0);
        //            $(itr).find('.stockClass').val(0);
        //            $(itr).find('.dis_per').text("0");
        //            $(itr).find('.sale_code').html(Prod[0].Sale_Erp_Code);
        //            $(itr).find('.tdAmt').html((qt * rt).toFixed(2));
        //            $(itr).find('.erp_code').val(Prod[0].Sample_Erp_Code);
        //            $(itr).find('.dis_val_class').text(0);
        //            $(itr).find('.default_umo_val').val(Prod[0].Default_UOMQty);
        //            $(itr).find('.default_umo').val(Prod[0].Default_UOM);


        //            $(itr).find('.Con_fac').text(Prod[0].Sample_Erp_Code);
        //            NewOrd[idx].con_fac = Prod[0].Sample_Erp_Code;
        //            NewOrd[idx].umo_unit = Prod[0].Default_UOM;


        //            $(itr).find('.fre').attr("id", sPCd);
        //            $(itr).find('.fre1').attr("id", sPCd);
        //            $(itr).find('.tddis_val').text('0.00');

        //            //getscheme(Prod[0].Sale_Erp_Code, '', '', $(itr).find('.Spinner-Value').text(), itr, $(itr).find('.fre1').text(), $(itr).find('.pcode').text(), idx, $(this).find('option:selected').text());

        //            $(itr).find('.pcode').html(sPCd);
        //            NewOrd[idx].Discount = $(itr).find('.dis_per').text() || 0;
        //            CalcAmt(itr);
        //        }
        //        else {

        //            $(itr).find('.ddlProd').val('-1');
        //            $(itr).find('.ddlProd').trigger('chosen:updated');
        //            $(itr).find('.Spinner-Value').text('Select');
        //            $(itr).find('.tdRate').text("0.00");
        //            $(itr).find('.txtQty').val('');
        //            $(itr).find('.second_row_div').text('');
        //            NewOrd[idx].Unit = 'Select';
        //            NewOrd[idx].umo_unit = '';
        //            NewOrd[idx].PCd = '';
        //            NewOrd[idx].PName = '';
        //            CalcAmt($(itr));
        //            alert('Product Already Selected');
        //            return false;
        //        }
        //    }
        //    else {
        //        alert('Please Select Retailer');
        //        $('.ddlProd').val('');
        //        $('.ddlProd ').chosen("destroy");
        //        $('.ddlProd').chosen();
        //    }
        //    //calc_stock($(this).closest('tr'));
        //});

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
            $('.tddis_val').each(function () {
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

            //if (Retailer_Extra_Tax_Type == 'TCS') {

            //    var extra_tax = Extra_Tax;
            //    var Calc_extra_tax = extra_tax / 100 * tv;
            //    $('#txt_tds_amt').val('0.00');
            //    $('#txt_tcs_amt').val(Math.round((Calc_extra_tax + Number.EPSILON) * 100) / 100);
            //    var total_with_extra = gross + Calc_extra_tax;
            //    $('#gross').val(total_with_extra.toFixed(2));

            //}
            //else {

            //    var extra_tax = Extra_Tax;
            //    var Calc_extra_tax = extra_tax / 100 * tv;
            //    $('#txt_tcs_amt').val('0.00');
            //    $('#txt_tds_amt').val(Math.round((Calc_extra_tax + Number.EPSILON) * 100) / 100);
            //    var total_with_extra = gross + Calc_extra_tax;
            //    $('#gross').val(total_with_extra.toFixed(2));

            //}
        }

        resetSL = function () {
            $(".rwsl").each(function () {
                $(this).text($(this).closest('tr').index() + 1);
            });
        }

        clearOrder = function () {
            $('#recpt-dt').val('');
            $('#recipient-name').val(0);
            $('#recipient_name_chzn').remove();
            $('#recipient-name').removeClass('chzn-done');
            $('#recipient-name').chosen();
            $("#OrderEntry > TBODY").html("");
            NewOrd = []; resetSL(); ReCalc();
        }

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
                    thisObj.css("background-color", "");
                    // thisObj.addClass('focus');
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
                $(this).find('.txtQty').css("background-color", "");
                // $(this).find('.txtQty').removeClass('focus');
            }
            else {
                $(this).find('.txtQty').css('background-color', '');
                //rjalert('Error!', 'Stock is not Available for the product', 'error');
                //alert('Stock is not Available for the product');
                //$(this).find('.txtQty').addClass('focus');
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
                        // $(this).find('.txtQty').removeClass('focus');
                    }
                    else {
                        $(this).find('.validate').css('background-color', '');
                        // rjalert('Error!', 'Stock is not Available for the product', 'error');
                        //alert('Stock is not Available for the product');
                        //$(this).find('.txtQty').addClass('focus');
                    }
                });
            }
        }
        $(document).on('keyup', '.txtQty', function (e) {

            tr = $(this).closest("tr");
            idx = $(tr).index();

            un = $(tr).find('.Spinner-Value').text();

            if (un == 'Select') {
                rjalert('Error!', 'Please Select UOM', 'error');
                //alert('Please Select UOM');
                $(tr).find('.txtQty').val('');
                return false;
            }

            CQ = $(this).val();

            var umoqty = $(this).closest('tr').find('.Con_fac').text();
            var DefaultUmo = $(this).closest('tr').find('.Con_fac').text();
            var prd_rate = $(tr).find(".tdRate").text();
            if (prd_rate == '' || prd_rate == undefined || isNaN(prd_rate)) { prd_rate = 0; };

            chck_pck(umoqty, DefaultUmo, prd_rate, CQ, idx, $(this));
            calc_stock($(this).closest("tr"));
            var disrate = $(this).closest("tr").find('.tdRate').text();
            result = (CQ * disrate);

            pCode = $(tr).find(".sale_code").text();
            pname = $(tr).find(".ddlProd :selected").text();
            ff = $(tr).find(".fre1").text();

            NewOrd[idx].Unit = tr.closest('tr').find('.Spinner-Value').text();
            NewOrd[idx].Free = $(tr).find('.fre1').text() || 0;
            // NewOrd[idx].Qty_c = $(this).val();
            NewOrd[idx].Qty = parseFloat($(this).val()) * $(tr).find('.Con_fac').text();
            // NewOrd[idx].Qty = RoundPack;
            NewOrd[idx].Discount = $(tr).find(".dis_val_class").text() || 0;
            NewOrd[idx].of_Pro_Code = $(tr).find('.of_pro_code').val();
            NewOrd[idx].of_Pro_Name = $(tr).find('.of_pro_name').val();
            NewOrd[idx].of_Pro_Unit = $(tr).find('.fre').attr('unit') || 0;
            CalcAmt(tr);

            if (e.keyCode == 13) {
                AddRow(1);
            }

        });
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
                retnm = retnm.replace(/&/g, "&amp;");
                var recdate = $('#recpt-dt').val();
                var odate = recdate.split('/');
                //var ordate = odate[0].split('-');
                var fnldate = odate[2] + '-' + odate[1] + '-' + odate[0];
                if (retcode == "" || retcode == null) {
                    approve = 0;
                    rjalert('Error!', 'Select a Customer Name', 'error');
                    //alert('Select a Customer Name');
                    return false;
                }
                if (recdate == "") {
                    approve = 0;
                    rjalert('Error!', 'Select a Order Date', 'error');
                    //alert('Select a Order Date');
                    return false;
                }
                if (NewOrd.length == 0) {
                    approve = 0;
                    rjalert('Error!', 'Atleast select a Product', 'error');
                    //alert('Atleast select a Product');
                    return false;
                }
                for (var i = 0; i < NewOrd.length; i++) {

                    if (NewOrd[i].PName.indexOf('&') > -1) {
                        NewOrd[i].PName = NewOrd[i].PName.replace(/&/g, "&amp;");
                    }

                    if (NewOrd[i].sPCd == '') {
                        approve = 0;
                        rjalert('Error!', 'Select a Product', 'error');
                        //alert('Select a Product');
                        return false;
                    }
                    if (NewOrd[i].Qty_c == '' || isNaN(NewOrd[i].Qty_c)) {
                        approve = 0;
                        rjalert('Error!', 'Remove the Product or Enter the Quantity', 'error');
                        //alert('Remove the Product or Enter the Quantity');
                        return false;
                    }
                }

                var sub = $('#sub_tot').val(); var dIs = $('#Txt_Dis_tot').val(); var tax = $('#txt_tax_total').val();

                var focus_count = 0;
                $('#OrderEntry tbody tr').each(function (f) {
                    if ($(this).find('.txtQty ').hasClass('focus')) {
                        focus_count++;
                    }
                });

                if (orderval > 0) {

                    if (focus_count == 0) {

                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            url: "MyOrder_Edit1.aspx/saveorders",
                            data: "{'NewOrd':'" + JSON.stringify(NewOrd) + "','Remark':'" + remark + "','Ordrval':'" + orderval + "','RetCode':'" + retcode + "','RecDate':'" + fnldate + "','Ntwt':'" + netwt + "','retnm':'" + retnm + "','Type':'0','ref_order':'','sub_total':'" + sub + "','dis_total':'" + dIs + "','tax_total':'" + tax + "','Ord_id':'" + Order_id + "'}",
                            dataType: "json",
                            success: function (data) {
                                if (data.d == "Success") {
                                    $.confirm({
                                        title: 'Success!',
                                        content: 'Order Updated Successfully!',
                                        type: 'green',
                                        typeAnimated: true,
                                        autoClose: 'action|8000',
                                        icon: 'fa fa-check fa-2x',
                                        buttons: {
                                            tryAgain: {
                                                text: 'OK',
                                                btnClass: 'btn-green',
                                                action: function () {
                                                    clearOrder();
                                                    $(document).find('#free_table tbody').empty();
                                                    arr = [];
                                                    if (type == 1) {
                                                        var url = "../Stockist/MultipleSalesInvoiceOrder.aspx?type=2"
                                                        window.open(url, '_self');
                                                    }
                                                    else {
                                                        var url = "../Stockist/myOrders.aspx"
                                                        window.open(url, '_self');
                                                    }
                                                }
                                            }
                                        }
                                    });
                                    //alert("Order Updated Successfully");

                                }
                                else {
                                    approve = 0;
                                    alert(data.d);
                                }
                            },
                            error: function (result) {
                                approve = 0;
                            }
                        });
                    }
                    else {
                        approve = 0;
                        rjalert('Error!', 'Please Check the Quantity', 'error');
                        //alert("Please Check the Quantity");
                    }
                }
                else {
                    approve = 0;
                    rjalert('Error!', 'Order Minimum Value to create a Order', 'error');
                    //alert("Order Minimum Value to create a Order");
                    return false;
                }
            }
        }

        $(document).ready(function () {

            loadmodal(); //resetOrderEntry();
            clearOrder();
            $('#exampleModal').modal('toggle');

            $(document).on('click', '#svorders', function () {
                svOrder();
            });

            $('#btnclose').on('click', function () {

                var Ans = confirm("Do you want to close this page?");
                if (Ans == true) {

                    if (type == 1) {
                        var url = "../Stockist/MultipleSalesInvoiceOrder.aspx?type=2"
                        window.open(url, '_self');
                    }
                    else {
                        var url = "../Stockist/myOrders.aspx"
                        window.open(url, '_self');
                    }

                    return true;
                }
                else {
                    return false;
                }
            });

            $('#btntimesClose').on('click', function () {
                var Ans = confirm("Do you want to close this page?");
                if (Ans == true) {

                    if (type == 1) {
                        var url = "../Stockist/MultipleSalesInvoiceOrder.aspx?type=2"
                        window.open(url, '_self');
                        return true;
                    }
                    else {
                        var url = "../Stockist/myOrders.aspx"
                        window.open(url, '_self');
                        return true;
                    }
                }
                else {
                    return false;
                }
            });

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "MyOrder_Edit1.aspx/GetProducts",
                dataType: "json",
                success: function (data) {
                    Product_array = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "MyOrder_Edit1.aspx/GetSecOrderDetails",
                data: "{'Order_No':'" + Order_id + "'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders;
                    $('#message-text').val(Orders[0].Remarks);
                    $("#recpt-dt").val(Orders[0].Order_Date);
                    $('#addressid').text(Orders[0].ListedDr_Address1);
                    Retailer_State = Orders[0].retailer_state;
                    $("#recipient-name").html("<option value='" + Orders[0].Cust_Code + "'>" + Orders[0].ListedDr_Name + "</option>");
                    $("#recipient-name").prop('disabled', true).trigger("chosen:updated");
                    $('#route-name').append($("<option></option>").val(Orders[0].Route).html(Orders[0].Territory_name)).trigger('chosen:updated').css("width", "100%");
                    $("#recipient-name").prop('disabled', true);
                    $("#route-name").prop('disabled', true).trigger("chosen:updated");
                    ReloadTable(0);

                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                data: "{'Div_Code':'" + division_Code + "','Stockist_Code':'" + Stockist_Code + "'}",
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
                url: "MyOrder_Edit1.aspx/GetCustWise_price",
                dataType: "json",
                success: function (data) {
                    Cust_Price = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });

        });
        $(document).on("click", ".Spinner-Modal>ul>li", function () {

            var CQvalue = '';
            var umo = $(this).val();
            var ans = []; var pro_filter = [];
            var factor = $(this).text();
            var row = $(this).closest("tr");
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
                return (a.Product_Detail_Code == pc );
            })

            if (pro_filter.length > 0) {

                row.find('.Spinner-Value').text('Select');
                row.find('.tdRate').text("0.00");
                row.find('.txtQty').val('');
                NewOrd[idx].Unit = 'Select';
                NewOrd[idx].umo_unit = '';
                CalcAmt(row);
                rjalert('Error!', 'Product Unit Already Selected', 'error');
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
                    var unitfilt = All_unit.filter(function (w) {
                        return (pc == w.Product_Detail_Code && w.Move_MailFolder_Id == umo);
                    });
                    row.find('.erp_code').val(unitfilt[0].Sample_Erp_Code);
                    row.find('.Con_fac').text(unitfilt[0].Sample_Erp_Code);
                    NewOrd[idx].Unit = factor;
                    
                    NewOrd[idx].Qty = qqty;
                    NewOrd[idx].Qty_c = qqty;
                    NewOrd[idx].umo_unit = umo;
                    NewOrd[idx].con_fac = unitfilt[0].Sample_Erp_Code;
                    sale_p_code = row.find('.sale_code').text();
                    ori_p_code = row.find('.ddlProd ').find('option:selected').val();
                    unit = $(this).text();
                    cq = row.find('.txtQty').val();
                    fr = row.find('.fre1').text();

                    getscheme(sale_p_code, cq, unit, row, fr, '', pc);

                    NewOrd[idx].Free = $(tr).find('.fre1').text() || 0;
                    NewOrd[idx].of_Pro_Code = $(tr).find('.of_pro_code').val();
                    NewOrd[idx].of_Pro_Name = $(tr).find('.of_pro_name').val();
                    NewOrd[idx].of_Pro_Unit = $(tr).find('.fre').attr('unit') || 0;
                    if (umo == ans[0].Base_Unit_code) {
                        var r = Number(ans[0].RP_Base_Rate).toFixed(2);
                        row.find('.tdRate').text(r);
                        NewOrd[idx].Rate = r;
                        CalcAmt(row);
                    }
                    else {
                        var r = Number(parseFloat(ans[0].RP_Base_Rate) * parseFloat(unitfilt[0].Sample_Erp_Code)).toFixed(2);
                        row.find('.tdRate').text(r);
                        NewOrd[idx].Rate = r;
                        CalcAmt(row);
                    }
                    //if (umo == ans[0].UnitCode) {

                    //    if (umo == ans[0].Base_Unit_code) {
                    //        row.find('.erp_code').val(1);
                    //        row.find('.Con_fac').text(1);
                            
                    //    }
                    //    else {
                    //        row.find('.erp_code').val(ans[0].Sample_Erp_Code);
                    //        row.find('.Con_fac').text(ans[0].Sample_Erp_Code);
                    //    }

                    //    NewOrd[idx].Unit = factor;
                    //    NewOrd[idx].Rate = ans[0].RP_Base_Rate;
                    //    NewOrd[idx].Qty = qqty;
                    //    NewOrd[idx].Qty_c = qqty;
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

                    //    var r = Number(ans[0].RP_Base_Rate).toFixed(2);
                    //    row.find('.tdRate').text(r);
                    //    CalcAmt(row);
                    //}
                    //else if (umo != ans[0].UnitCode && umo == ans[0].Base_Unit_code) {
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
        //function calc_stock(n) {

        //    var pduct_code = n.find('.ddlProd').val();
        //    var con_fc = n.find('.erp_code').val();
        //    var u_n = n.find('.Spinner-Value').text();

        //    var sa_qty = 0;
        //    var ad_qty = 0;

        //    var sto_filter = [];
        //    if (Cust_Price[0].type == 0) {
        //        sto_filter = Allrate.filter(function (y) {
        //            return (y.Product_Detail_Code == pduct_code);
        //        })
        //    }
        //    else {
        //        sto_filter = Cust_Price.filter(function (a) {
        //            return (a.Product_Detail_Code == pduct_code);
        //        })
        //    }


        //    if (sto_filter > 0) {

        //        $(document).find('.' + pduct_code).each(function () {

        //            var producode = $(this).find('.ddlProd').val();
        //            var uni = $(this).find('.Spinner-Value').text();
        //            var con_fc = $(this).find('.erp_code').val();
        //            var qqty = $(this).find('.txtQty').val(); if (qqty == "" || qqty == null) { qqty = 0; }

        //            if (sto_filter.length > 0) {
        //                ad_qty += parseFloat(qqty) * con_fc;
        //            }


        //            if (sto_filter[0].TotalStock > 0 && sto_filter[0].TotalStock >= ad_qty) {
        //                $(this).find('.validate').css('background-color', '');
        //                $(this).find('.txtQty').removeClass('focus');
        //            }
        //            else {
        //                $(this).find('.validate').css('background-color', '');
        //                alert('Stock is not Available for the product');
        //                $(this).find('.txtQty').addClass('focus');
        //            }
        //        });
        //    }
        //}

    </script>
</asp:Content>

