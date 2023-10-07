<%@ Page Title="" Language="C#" MasterPageFile="~/Master_DIS.master" AutoEventWireup="true"
    CodeFile="Invoice_Entry_2.aspx.cs" Inherits="Invoice_Entry_2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!doctype html>
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>


        <link href="../css/jquery.multiselect.css" rel="stylesheet" type="text/css" />
        <script src="../js/jquery.multiselect.js" type="text/javascript"></script>
        <link href="../css/daterangepicker/daterangepicker-bs3.css" rel="stylesheet" />
        <link href="../css/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
        <%--<link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />--%>
        <link href="../css/select2.min.css" rel="stylesheet" />
        <link href="../alertstyle/jquery-confirm.min.css" rel="stylesheet" />
        <script src="../alertstyle/jquery-confirm.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/js/bootstrap-datepicker.js" type="text/javascript"></script>

        <style type="text/css">
            .ui-menu-item > a {
                display: block;
            }

            .fixed {
                position: fixed;
                width: 97%;
                bottom: 10px;
            }

            .ms-options {
                cursor: pointer;
            }

            .example {
                display: contents;
                position: fixed;
                margin-left: 156px;
            }

            .focus {
                background-color: #ff6666;
            }

            .focus1 {
                background-color: white;
            }

            .tr_class {
                background-color: #9fdfbe;
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
                width: 360px !important;
            }

            input[type='text'], select {
                border: 1.5px solid #19a4c6a3 !important;
            }

            .select2-container--default .select2-selection--single {
                border: 1.5px solid #19a4c6a3 !important;
            }

            .ms-options-wrap > button:focus, .ms-options-wrap > button {
                border: 1.5px solid #19a4c6a3 !important;
            }

            .table > thead > tr > th {
                vertical-align: bottom;
                border-bottom: 2px solid #19a4c6a3;
            }

            .txtblue {
                border: 1.5px solid #19a4c6a3 !important;
                /*border: 1px solid #19a4c6!important;*/
            }

            .chosen-container-single .chosen-single {
                border: 1.5px solid #19a4c6a3 !important;
                height: 30px;
            }

            .card {
                border: 1.5px solid #b2ebf9b5 !important;
            }

            .lbl_stock {
                color: red;
                font-size: 12px;
                font-weight: bolder !important;
            }

            .deleffect {
                animation-name: example;
                animation-duration: 0.3s;
            }

            @keyframes example {
                0% {
                    left: -1px;
                }



                100% {
                    left: -1400px;
                }
            }

            .addeffect {
                animation-name: example1;
                animation-duration: 1s;
            }

            @keyframes example1 {
                0% {
                    left: -1400px;
                }

                100% {
                    left: 0px;
                }
            }

            .fa-stack-overflow:before {
                content: "\f16c";
                margin-right: 6px;
                margin-left: 4px;
                color: #00bcd4;
            }

            .fa-tags:before {
                content: "\f02c";
                color: #ef1b1b;
                margin-right: 6px;
            }

            .fa-code:before {
                content: "\f121";
                margin-left: 3px;
                margin-right: 6px;
                color: #0075ff;
                font-size: 13px;
                font-weight: bolder;
            }

            .spinner {
                margin: 100px auto;
                width: 50px;
                height: 40px;
                text-align: center;
                font-size: 10px;
            }

                .spinner > div {
                    background-color: #333;
                    height: 100%;
                    width: 6px;
                    display: inline-block;
                    -webkit-animation: sk-stretchdelay 1.2s infinite ease-in-out;
                    animation: sk-stretchdelay 1.2s infinite ease-in-out;
                }

                .spinner .rect2 {
                    -webkit-animation-delay: -1.1s;
                    animation-delay: -1.1s;
                }

                .spinner .rect3 {
                    -webkit-animation-delay: -1.0s;
                    animation-delay: -1.0s;
                }

                .spinner .rect4 {
                    -webkit-animation-delay: -0.9s;
                    animation-delay: -0.9s;
                }

                .spinner .rect5 {
                    -webkit-animation-delay: -0.8s;
                    animation-delay: -0.8s;
                }

            @-webkit-keyframes sk-stretchdelay {
                0%, 40%, 100% {
                    -webkit-transform: scaleY(0.4);
                }

                20% {
                    -webkit-transform: scaleY(1.0);
                }
            }

            @keyframes sk-stretchdelay {
                0%, 40%, 100% {
                    transform: scaleY(0.4);
                    -webkit-transform: scaleY(0.4);
                }

                20% {
                    transform: scaleY(1.0);
                    -webkit-transform: scaleY(1.0);
                }
            }

            .spinnner_div {
                width: 1200px;
                height: 1000px;
                background: rgba(255, 255, 255, 0.1);
                backdrop-filter: blur(2px);
                position: absolute;
                z-index: 100;
                overflow-y: hidden;
            }
        </style>
        <script src="../js/daterangepicker-3.0.5.min.js"></script>

        <%--<script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>--%>
        <script src="../js/select2.full.min.js"></script>
        <script type="text/javascript">

            var SingleSelValue = []; var FirstCheck = []; var Free_Data = []; var alldata = []; var arr = []; var retailer_ID; var retailer_name;
            var dts = []; var AllProduct = []; var s = []; var od = []; var ar = []; var da; var AllOrderIDs = []; var All_Pending_order = []; All_Pending_order_details = [];
            var extra = 0; var All_Product = []; var r = 0; var adv_Cre_Amt = []; var All_stock = []; var Allrate = []; var count = 0; var erp_cd = ''; var multivalue = [];
            var All_Tax = []; var to = 0; var retailer_details = []; var dis_state_code = ''; var filter_ret_state = []; var total_tax = ''; var Extra_Tax = 0; var Extra_Tax_Type = '';
            var Extra_TDS_Percentage = ''; var Extra_TCS_Percentage = ''; var Cat_Details = []; var All_Route = []; var Cust_Price = []; var Rate_List_Code = '';
            var pgNo_Print = 1; PgRecords_print = 50; TotalPg_print = 0; pgNo = 1; PgRecords = 10; TotalPg = 0;
            $(document).ready(function ($) {

                $(function () {

                    $('.example-popover').popover({
                        trigger: "hover",
                        title: "Details",
                        html: true,
                        content: function () {

                            var cur_inv_val = 0; var pend = 0; var cre_at = 0;

                            if ($("[id*=Amt_Tot]").val() == "") { cur_inv_val = cur_inv_val.toFixed(2) } else { cur_inv_val = parseFloat($("[id*=Amt_Tot]").val()).toFixed(2) }
                            $('#popover-content').find('#lbl_cu_inv_amt').text(cur_inv_val);

                            if ($('#lbl_pending_tbl_amt').text() == "") { pend = pend.toFixed(2) } else { var pend = parseFloat($('#lbl_pending_tbl_amt').text()).toFixed(2) }
                            $('#popover-content').find('#lbl_out_amt').text(pend);

                            var calc_cre_tot = parseFloat(cur_inv_val) + parseFloat(pend);
                            $('#popover-content').find('#lbl_before_creit_amt').text(calc_cre_tot.toFixed(2));

                            if ($('#lbl_credit_amt').text() == "") { cre_at = cre_at.toFixed(2) } else { cre_at = parseFloat($('#lbl_credit_amt').text()).toFixed(2) }
                            $('#popover-content').find('#lbl_creit_amt').text(cre_at);

                            to = calc_cre_tot - cre_at;
                            $('#popover-content').find('#lbl_tot_amt').text(to.toFixed(2));
                            return $('#popover-content').html();
                        }
                    })
                })

                $('#load').hide();
                $('.example').hide();

                var today = new Date();
                var dd = today.getDate();
                var mm = today.getMonth() + 1;
                var yyyy = today.getFullYear();

                if (dd < 10) {
                    dd = '0' + dd
                }
                if (mm < 10) {
                    mm = '0' + mm
                }
                today = mm + '/' + dd + '/' + yyyy;
                $('.invoice').val(today);
                $('#datepicker').datepicker({ format: 'dd/mm/yyyy', startDate: '-3d', minDate: 0, showButtonPanel: true, showWeek: true }); $('#datepicker').attr('min', today);
                $('#datepicker1').datepicker({ format: 'mm/dd/yyyy', startDate: '-3d', minDate: 0 });

                var div_code = ("<%=Session["div_code"].ToString()%>");
                var stockistcode = ("<%=Session["Sf_Code"].ToString()%>");
                var stockistname = ("<%=Session["sf_name"].ToString()%>");
                dis_state_code = ("<%=Session["State"]%>");

                $(document).on('keypress', '.validate, .Ad_Paid', function (e) {
                    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                        return false;
                    }
                });

                $(document).on('keypress', '#notes', function (e) {
                    if (e.keyCode == 34 || e.keyCode == 39 || e.keyCode == 38 || e.keyCode == 60 || e.keyCode == 62 || e.keyCode == 92) return false;
                });

                $(document).on('change', '#Sel_Pay_Term', function () {

                    var sel_val = $('#Sel_Pay_Term option:selected').text();
                    if (sel_val == "Cash" || sel_val == "Select") {
                        $('.hid').hide();
                    }
                    else {
                        $('.hid').show();
                    }
                });

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Invoice_Entry_2.aspx/GetProductDetails",
                    dataType: "json",
                    success: function (data) {
                        All_Product = JSON.parse(data.d);
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });

                $('#example-multiple-selected').hide();

                $.ajax({
                    type: "Post",
                    contentType: "application/json; charset=utf-8",
                    url: "Invoice_Entry_2.aspx/bindretailer",
                    dataType: "json",
                    async: false,
                    success: function (data) {
                        retailer_details = JSON.parse(data.d);
                        if (retailer_details.length > 0) {
                            for (var f = 0; f < retailer_details.length; f++) {
                                $('#recipient-name').append($('<option style ="' + ((retailer_details[f].Order_Flag == 0) ? 'background: #5cb85c; color: #fff;' : "") + '" value="' + retailer_details[f].ListedDrCode + '">' + retailer_details[f].ListedDr_Name + '</option>'));
                            }
                        }
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });

                //$('#recipient-name').selectpicker({
                //    liveSearch: true
                //});
                // $("[data-rel='chosen']").chosen();
                $("#recipient-name").select2();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    data: "{'Div_Code':'" + div_code + "','Stockist_Code':'" + stockistcode + "'}",
                    url: "Invoice_Entry_2.aspx/getratenew",
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
                    url: "Invoice_Entry_2.aspx/Get_Product_unit",
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
                    data: "{'Div_Code':'" + div_code + "','Stockist_Code':'" + stockistcode + "'}",
                    url: "Invoice_Entry_2.aspx/Get_All_Stock",
                    dataType: "json",
                    success: function (data) {
                        All_stock = JSON.parse(data.d) || [];
                    },
                    error: function (result) {
                    }
                });

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Invoice_Entry_2.aspx/GetTransType",
                    dataType: "json",
                    success: function (data) {
                        if (data.d.length > 0) {
                            var ddlCustomers = $("#Sel_Shi_Med");
                            ddlCustomers.empty().append('<option selected="selected" value="0">Select</option>');
                            $.each(data.d, function () {
                                ddlCustomers.append($("<option></option>").val(this['Trans_code']).html(this['Trans_name']));
                            });
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
                    url: "Invoice_Entry_2.aspx/Get_Product_Cat_Details",
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
                $('#route-name').empty();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Invoice_Entry_2.aspx/Get_Route",
                    dataType: "json",
                    success: function (data) {
                        All_Route = JSON.parse(data.d) || [];
                        $('#route-name').append("<option value='0'>---Select Route Name---</option>");
                        for (var i = 0; i < All_Route.length; i++) {
                            $('#route-name').append($('<option value="' + All_Route[i].Territory_Code + '">' + All_Route[i].Territory_Name + '</option>'));
                        }
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
                $("#route-name").chosen();
                //$('#route-name').selectpicker({
                //    liveSearch: true
                //});

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Invoice_Entry_2.aspx/GetCustWise_price",
                    dataType: "json",
                    success: function (data) {
                        Cust_Price = JSON.parse(data.d) || [];
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });

                $('#route-name').on('change', function () {

                    var selected_route = $(this).val();
                    var filtered_retailer = [];

                    // $('#recipient-name').empty();
                    //// $('#ecipient-name').html();
                    // $('#recipient-name').trigger("chosen:destroy");
                    // //$('#recipient-name').chosen('destroy');
                    // $('#txt_Ship_add').text('');
                    //// $('#recipient-name').trigger('change');
                    $('#recipient-name').empty();
                    $('#recipient-name').trigger('select2:change');
                    // $("[data-rel='chosen']").trigger("chosen:updated");
                    /*$('#recipient_name_chzn chzn-drop').remove();*/
                    filtered_retailer = retailer_details.filter(function (k) {
                        return (k.Territory_Code == selected_route);
                    });
					if(selected_route=='0'){
					filtered_retailer = retailer_details;
					}
                    $('#recipient-name').append("<option value='0'>---Select Retailer Name---</option>");
                    var newState = new Option('', '', true, true);
                    for (var f = 0; f < filtered_retailer.length; f++) {
                        // $('.recipient-name').append($('<option style ="' + ((filtered_retailer[f].Order_Flag == 0) ? 'background: #5cb85c; color: #fff;' : "") + '" value="' + filtered_retailer[f].ListedDrCode + '">' + retailer_details[f].ListedDr_Name + '</option>'));
                        //newState = new Option(retailer_details[f].ListedDr_Name,filtered_retailer[f].ListedDrCode,  false, false);
                        //$('#recipient-name').append(newState).trigger('select2:change');
                        $('#recipient-name').append('<option value="' + filtered_retailer[f].ListedDrCode + '">' + filtered_retailer[f].ListedDr_Name + '</option>');
                        //$('#recipient-name').append('<option value="' + filtered_retailer[f].ListedDrCode + '">' + retailer_details[f].ListedDr_Name + '</option>');
                    }
                    //$('#recipient-name').selectpicker({
                    //    liveSearch: true
                    //});
                    //$('#recipient-name').chosen();
                    $('#recipient-name').trigger('select2:change');
                    $('#recipient-name').trigger('change');
                });


                $('#recipient-name').change(function () {
                    $('.spinnner_div').show();
                    retailer_ID = $(this).children("option:selected").val();
                    if (retailer_ID === undefined || retailer_ID === null) {
                        $('.spinnner_div').hide();
                        return false;

                    }


                    filter_ret_state = retailer_details.filter(function (h) {
                        return (h.ListedDrCode == retailer_ID);
                    });
                    if (filter_ret_state.length > 0) {
                        $('#txt_Ship_add').text(filter_ret_state[0].Addres);
                        Rate_List_Code = filter_ret_state[0].List_Code;

                        if (filter_ret_state[0].Tcs != 0) {

                            Extra_Tax_Type = 'TCS';
                            Extra_Tax = filter_ret_state[0].Tcs;
                        }
                        else if (filter_ret_state[0].Tds != 0) {
                            Extra_Tax_Type = 'TDS';
                            Extra_Tax = filter_ret_state[0].Tds;
                        }
                    }
                    $("#example-multiple-selected").html('');
                    $('#load').show();
                    $('.example').show();
                    $('#tblCustomers').find('tfoot tr').remove();

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        data: "{'retailercode':'" + retailer_ID + "'}",
                        url: "Invoice_Entry_2.aspx/GetWise_price",
                        dataType: "json",
                        success: function (data) {
                            Cust_Price = JSON.parse(data.d) || [];
                        },
                        error: function (result) {
                            $('.spinnner_div').hide();
                            alert(JSON.stringify(result));
                        }
                    });
                    //if (Cust_Price.length > 0) {
                    setTimeout(function () {
                        $.ajax({
                            type: "Post",
                            contentType: "application/json; charset=utf-8",
                            url: "Invoice_Entry_2.aspx/GetPendingOrder",
                            data: "{'Retailer_ID':'" + retailer_ID + "'}",
                            dataType: "json",
                            success: function (data) {
                                $('#load').hide();
                                $('.example').hide();
                                All_Pending_order = JSON.parse(data.d);
                                var Datas = data.d;
                                if (Datas.length > 0) {

                                    for (var k = 0; All_Pending_order.length > k; k++) {
                                        $('#example-multiple-selected').append('<option value="' + All_Pending_order[k].Trans_Sl_No + '">' + All_Pending_order[k].Order_Date + '</option>');
                                    }
                                    $('#example-multiple-selected').multiselect({
                                        columns: 4,
                                        placeholder: 'Select Order IDs',
                                        searchOptions: {
                                            'default': 'Search Order IDs'
                                        },
                                        selectAll: true,
                                        maxHeight: 20,
                                    }).multiselect("reload");
                                    Datas = [];
                                    clear();
                                }
                                else {
                                    clear();
                                }
                            },
                            error: function (result) {
                                alert(JSON.stringify(result));
                                $('.spinnner_div').hide();

                            }
                        }, 30);
                    });

                    $.ajax({
                        type: "Post",
                        contentType: "application/json; charset=utf-8",
                        url: "Invoice_Entry_2.aspx/GetPendingOrder_details",
                        data: "{'Retailer_ID':'" + retailer_ID + "'}",
                        dataType: "json",
                        success: function (data) {
                            All_Pending_order_details = JSON.parse(data.d);
                            if (All_Pending_order_details.length > 0) {
                                $("#txt_Ship_add").text(All_Pending_order_details[0].SHIPADD);
                            }

                        },
                        error: function (result) {
                            $('.spinnner_div').hide();
                            alert(JSON.stringify(result));
                        }
                    });

                    $.ajax({
                        type: "Post",
                        contentType: "application/json; charset=utf-8",
                        url: "Invoice_Entry_2.aspx/Get_Adv_Credit_amt_details",
                        data: "{'Retailer_ID':'" + retailer_ID + "'}",
                        dataType: "json",
                        success: function (data) {
                            adv_Cre_Amt = JSON.parse(data.d);
                            if (adv_Cre_Amt.length > 0) {
                                extra = adv_Cre_Amt[0].Total_amt;
                                $('#tot_ex_amt').val(parseFloat(extra).toFixed(2));
                                $('#tot_Credit_amt').val(parseFloat(adv_Cre_Amt[0].Credit_Limit).toFixed(2));
                                $('#lbl_pending_tbl_amt').text(adv_Cre_Amt[0].Amt_due_from_pending_tbl);
                                $('#lbl_credit_amt').text(adv_Cre_Amt[0].Credit_bal);
                            }
                        },
                        error: function (result) {
                            $('.spinnner_div').hide();
                            alert(JSON.stringify(result));
                        }
                    });
                    $('.spinnner_div').hide();

                    //}
                    //else {
                    //    rjalert('Attention!', "it's currently not possible to generate an invoice as the category for the retailer is not fixed. Kindly reach out to the administrator for further assistance", 'error');
                    //    var rtname = $('#route-name').val();
                    //    $("#route-name").val(rtname).trigger('change');
                    //    $('#txt_Ship_add').text('');
                    //    return false;
                    //}
                });

                function clear() {
                    $('#tblCustomers').find('tbody tr').remove();
                    $('#free_table').find('tbody tr').remove();
                    $("[id*=sub_tot]").val('');
                    $("[id*=in_Tot]").val('');
                    $("[id*=Amt_Tot]").val('');
                    $("[id*=Txt_Dis_tot]").val('');
                    $('.innumber').val('');
                    $('#Tax_tot').val('');
                    $('#Ad_Paid').val('');
                    $('#Tax_GST').val('');
                    $('#Tax_CGST').val('');
                    $('#extra_amt').val('');
                    $('#Tax_SGST').val('');
                    $('#tot_adju').val('');
                    AllProduct = [];
                    multivalue = [];
                    arr = [];
                }

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Invoice_Entry_2.aspx/GetPayType",
                    dataType: "json",
                    success: function (data) {
                        if (data.d.length > 0) {
                            var ddlCustomers = $("#Sel_Pay_Term");
                            ddlCustomers.empty().append('<option selected="selected" value="0">Select</option>');
                            $.each(data.d, function () {
                                ddlCustomers.append($("<option></option>").val(this['Pay_code']).html(this['Pay_name']));
                            });
                        }
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });

                $(document).on('click', '.ms-selectall', function () {

                    $('.selected').find('input[type="checkbox"]').map(function () {
                        AllOrderIDs.push({
                            OrderIds: $(this).val()
                        });
                    });

                    if (AllOrderIDs.length == 0) {
                        var tbl = $('#tblCustomers');
                        $(tbl).find('tbody tr').remove();
                        $('#free_table').find('tbody tr').remove();
                        all();
                        AllProduct = [];
                        multivalue = [];
                        arr = [];
                    }
                    else {
                        AllProduct = [];
                        multivalue = [];
                        arr = [];
                        bindvalue(AllOrderIDs);
                    }
                });
                $('.btnsave').on('click', function () {
                    approve += 1;
                    $(this).prop('disabled', true);
                    $('body,html').animate({
                        scrollTop: 0
                    }, 800);
                    if (approve == "1") {
                        $('.spinnner_div').show();
                        setTimeout(svorder, 2000);
                    }
                    else {
                        rjalert('Error!', 'Please Wait We Are Processing Your Order..', 'error');
                    }
                });
                $(".ms-options input[type='checkbox']").live("click", function () {

                    AllOrderIDs.push({
                        OrderIds: $(this).val()
                    });

                    if ($(this).is(":checked")) {
                        bindvalue(AllOrderIDs);
                    }
                    else {

                        exO = AllProduct.filter(function (a) {
                            return (a.Order_No == AllOrderIDs[0].OrderIds);
                        });
                        reloadTable(exO[0].Order_Details, 1);
                        check_free(exO[0].Order_Details, 1);
                        idxx = AllProduct.indexOf(exO[0]);
                        all();
                        AllProduct.splice(idxx, 1);

                    }
                });

                function bindvalue(ordervalues) {

                    for (var r = 0; r < ordervalues.length; r++) {

                        var value = ordervalues[r].OrderIds;

                        var selected_order = [];
                        selected_order = All_Pending_order_details.filter(function (g) {
                            return (g.Trans_Order_No == ordervalues[r].OrderIds);
                        })

                        FirstCheck = selected_order;
                        var Order = [];
                        AllProduct.push({
                            Order_No: value,
                            Order_Details: FirstCheck,
                            Extra_Tax_Type: selected_order[0].Extra_tax_type,
                            Extra_Tax: selected_order[0].Extra_Tax_Amt
                        });
                        reloadTable(FirstCheck, 0);
                        check_free(FirstCheck, 0);
                    }
                }

                function check_free(items, flag) {

                    for (k = 0; k < items.length; k++) {
                        OrdP = arr.filter(function (a) {
                            return (items[k].of_pro_code == a.Off_Pro_Code && items[k].Offer_Product_Unit == a.Off_Pro_Unit);
                        })
                        if (OrdP.length > 0) {

                            idx = arr.indexOf(OrdP[0]);
                            if (flag == 0) {
                                if (OrdP[0].Free == "" || isNaN(OrdP[0].Free)) { OrdP[0].Free = 0; }
                                if (items[k].Free == "" || isNaN(items[k].Free)) { items[k].Free = 0; }

                                arr[idx].Free = parseFloat(OrdP[0].Free) + parseFloat(items[k].Free);

                            } else {

                                if (OrdP[0].Free == "" || isNaN(OrdP[0].Free)) { OrdP[0].Free = 0; }
                                if (items[k].Free == "" || isNaN(items[k].Free)) { items[k].Free = 0; }

                                $vq = parseFloat(OrdP[0].Free) - parseFloat(items[k].Free)
                                if ($vq > 0) {
                                    arr[idx].Free = $vq;
                                } else {
                                    arr.splice(idx, 1);
                                }
                            }
                        } else {
                            itm1 = {};
                            if (flag == 0) {
                                itm1.Free = items[k].Free;
                                itm1.Off_Pro_Code = items[k].of_pro_code || 0;
                                itm1.Off_Pro_Name = items[k].of_pro_name;
                                itm1.Off_Pro_Unit = items[k].Offer_Product_Unit;
                                itm1.Product_Code = items[k].of_pro_code;
                                itm1.Product_Name = items[k].of_pro_name;
                                itm1.pppcode = items[k].Product_Code;
                                itm1.pppname = items[k].Product_Name;

                                var ans3 = [];
                                ans3 = Allrate.filter(function (t) {
                                    return (t.Product_Detail_Code == items[k].Product_Code);
                                });
                                if (ans3.length > 0) {
                                    if (ans3[0].Unit_code == items[k].Unit_Code) {
                                        itm1.P_Free = 0;
                                        itm1.P_fre_uni = ans3[0].Product_Sale_Unit;
                                        itm1.C_fre_uni = items[k].Offer_Product_Unit;
                                        itm1.C_join = items[k].unit;
                                        itm1.P_join = ans3[0].Product_Sale_Unit;
                                        itm1.units = items[k].unit;
                                    }
                                    else {
                                        itm1.Free = 0;
                                        itm1.P_Free = items[k].Free;
                                        itm1.P_fre_uni = items[k].Offer_Product_Unit;
                                        itm1.C_fre_uni = 0;
                                        itm1.C_join = ans3[0].product_unit;
                                        itm1.P_join = items[k].unit;
                                        itm1.units = items[k].unit;
                                    }
                                }
                                arr.push(itm1);
                            }
                        }
                    }
                    bind_free(arr);
                }

                function bind_free(arr) {
                    var l = 0;
                    $("#free_table TBODY").html("");

                    if (arr.length > 0) {
                        for (var r = 0; r < arr.length; r++) {
                            if (arr[r].Free != '0' || arr[r].P_Free != '0') {
                                var l = l + 1;
                                if (arr[r].Free != '0' && arr[r].P_Free != '0') {
                                    var str = "<tr><td style='width: 14%;' class='td_id'>" + l + "</td><td style='width:21%;'><input type='hidden' value=" + arr[r].Product_Code + " id='apc'/>" + arr[r].Product_Code + "</td><td style='left: 201px;width: 38%;' class='td_name'><input type='hidden' value=" + arr[r].Product_Name + " id='apn'/>" + arr[r].Product_Name + "</td><td style='left: 454px;' class='td_free'><input type='hidden' value=" + arr[r].Free + " id='fr'/>" + (arr[r].Free + ' ' + arr[r].C_join) + "</td><td style='display:none'><input type='hidden' id='opc' value=" + arr[r].Product_Code1 + " />" + arr[r].Product_sale_Code + "</td><td>" + (arr[r].P_Free + ' ' + arr[r].P_join) + "</td></tr>";
                                    $('#free_table tbody').append(str);
                                }

                                if (arr[r].Free != '0' && arr[r].P_Free == '0') {
                                    var str = "<tr><td style='width: 14%;' class='td_id'>" + l + "</td><td style='width:21%;'><input type='hidden' value=" + arr[r].Product_Code + " id='apc'/>" + arr[r].Product_Code + "</td><td style='left: 201px;width: 38%;' class='td_name'><input type='hidden' value=" + arr[r].Product_Name + " id='apn'/>" + arr[r].Product_Name + "</td><td style='left: 454px;' class='td_free'><input type='hidden' value=" + arr[r].Free + " id='fr'/>" + (arr[r].Free + ' ' + arr[r].C_join) + "</td><td style='display:none'><input type='hidden' id='opc' value=" + arr[r].Product_Code1 + " />" + arr[r].Product_sale_Code + "</td></tr>";
                                    $('#free_table tbody').append(str);
                                }

                                if (arr[r].P_Free != '0' && arr[r].Free == '0') {
                                    var str = "<tr><td style='width: 14%;' class='td_id'>" + l + "</td><td style='width:21%;'><input type='hidden' value=" + arr[r].Product_Code + " id='apc'/>" + arr[r].Product_Code + "</td><td style='left: 201px;width: 38%;' class='td_name'><input type='hidden' value=" + arr[r].Product_Name + " id='apn'/>" + arr[r].Product_Name + "</td><td style='display:none'><input type='hidden' id='opc' value=" + arr[r].Product_Code1 + " />" + arr[r].Product_sale_Code + "</td><td>" + (arr[r].P_Free + ' ' + arr[r].P_join) + "</td></tr>";
                                    $('#free_table tbody').append(str);
                                }
                            }
                        }
                    }
                }

                function reloadTable(items, flag) {

                    var eject_multi = [];
                    eject_multi = multivalue.filter(function (w) {
                        //return (w.Product_Code == "" && w.Unit == "0");
                        return (w.Product_Code == "" && w.Unit_Code == "0");
                    });

                    if (eject_multi.length > 0) {

                        for (var f = 0; f < eject_multi.length; f++) {
                            idx = multivalue.indexOf(eject_multi[f]);
                            multivalue.splice(idx, 1);
                        }
                    }

                    for (k = 0; k < items.length; k++) {

                        OrdP1 = multivalue.filter(function (a) {
                            //return (items[k].Product_Code == a.Product_Code && items[k].unit == a.Unit && a.Type == "Add");
                            return (items[k].Product_Code == a.Product_Code && items[k].Unit_code == a.Unit_Code && a.Type == "Add");
                        })
                        if (OrdP1.length > 0) { idx = multivalue.indexOf(OrdP1[0]); multivalue.splice(idx, 1); }


                        OrdP = multivalue.filter(function (a) {
                            //return (items[k].Product_Code == a.Product_Code && items[k].unit == a.Unit)
                            return (items[k].Product_Code == a.Product_Code && items[k].Unit_code == a.Unit_Code)
                        })

                        if (OrdP.length > 0) {

                            idx = multivalue.indexOf(OrdP[0]);
                            if (flag == 0) {

                                multivalue[idx].Quantity = parseFloat(OrdP[0].Quantity) + parseFloat(items[k].qty);
                                multivalue[idx].qty_in_sale_unit = parseFloat(OrdP[0].qty_in_sale_unit) + parseFloat(items[k].qty_in_sale_unit);
                                multivalue[idx].Sale_qt = multivalue[idx].Quantity;

                                if (OrdP[0].Free == "") { OrdP[0].Free = 0; }
                                if (OrdP[0].Discount == "") { OrdP[0].Discount = 0; }
                                if (items[k].Free == "") { items[k].Free = 0; }
                                if (items[k].Discount == "") { items[k].Discount = 0; }

                                multivalue[idx].Free = parseFloat(OrdP[0].Free) + parseFloat(items[k].Free);
                                multivalue[idx].Discount = parseFloat(OrdP[0].Discount) + parseFloat(items[k].Discount);
                            } else {
                                $vq = parseFloat(OrdP[0].Quantity) - parseFloat(items[k].qty)
                                if ($vq > 0) {
                                    multivalue[idx].Quantity = $vq;
                                    multivalue[idx].Sale_qt = $vq;
                                    multivalue[idx].Free = parseFloat(OrdP[0].Free) - parseFloat(items[k].Free);
                                } else {
                                    multivalue.splice(idx, 1);
                                }
                            }
                        } else {
                            itm = {};
                            if (flag == 0) {

                                itm.Trans_Order_No = items[k].Trans_Order_No;
                                itm.Product_Code = items[k].Product_Code;
                                itm.Sale_Code = items[k].Sale_Erp_Code;
                                itm.Product_Name = items[k].Product_Name;
                                itm.Rate = items[k].Rate || 0;
                                itm.Rate_in_peice = items[k].Rate_in_peice || 0;
                                itm.Unit = items[k].unit || 0;
                                itm.Unit_Code = items[k].Unit_code || 0;
                                itm.Quantity = items[k].qty;
                                itm.qty_in_sale_unit = items[k].Quantity;
                                itm.Sale_qt = items[k].qty;
                                itm.Amount = items[k].Amount;
                                itm.Free = items[k].Free;
                                itm.Discount = items[k].Discount;
                                itm.date = items[k].date;
                                itm.Free = items[k].Free;
                                itm.Off_Pro_Code = items[k].of_pro_code || 0;
                                itm.Off_Pro_Name = items[k].of_pro_name;
                                itm.Off_Pro_Unit = items[k].Offer_Product_Unit;
                                itm.Tax_Id = items[k].Tax_Id;
                                itm.Tax_Name = items[k].Tax_Name || 0;
                                itm.Tax_Val = items[k].Tax_Val;
                                itm.E_Code = items[k].Sample_Erp_Code;
                                itm.Type = "Order";
                                itm.Sl_No = items[k].Sl_No;
                                itm.Stock = items[k].Stock;
                                itm.Con_fac_qty = 0;
                                itm.Avail_Stock = items[k].Stock;
                                multivalue.push(itm);
                            }
                        }
                    }
                    var tbl = $('#tblCustomers');
                    $(tbl).find('tbody tr').remove();
                    $(tbl).find('tfoot tr').remove();

                    multivalue.sort(function (l, z) {
                        { return l.Sl_No - z.Sl_No }
                    });

                    if (multivalue.length > 0) {

                        for (var i = 0; i < multivalue.length; i++) {

                            if (multivalue[i].Product_Code != null && multivalue[i].Unit != null) {

                                bindunit1 = ""; var filter_unit = [];

                                filter_unit = All_unit.filter(function (w) {
                                    return (multivalue[i].Product_Code == w.Product_Detail_Code);
                                });

                                if (filter_unit.length > 0) {
                                    for (var z = 0; z < filter_unit.length; z++) {
                                        if (filter_unit[z].Move_MailFolder_Id == multivalue[i].Unit_Code) {

                                            bindunit1 += "<option selected value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";
                                        }
                                        else {
                                            bindunit1 += "<option value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";
                                        }
                                    }
                                }
                                var filter_erp = [];
                                filter_erp = Allrate.filter(function (t) {
                                    return (t.Product_Detail_Code == multivalue[i].Product_Code && t.Move_MailFolder_Name == multivalue[i].Unit);
                                    //return (t.Product_Detail_Code == multivalue[i].Product_Code && t.Unit_code == multivalue[i].Unit_Code);
                                })

                                if (filter_erp.length > 0) {
                                    if (filter_erp[0].Move_MailFolder_Name == filter_erp[0].Product_Sale_Unit) {
                                        erp_cd = 1;
                                    }
                                    else {
                                        erp_cd = filter_erp[0].Sample_Erp_Code;
                                    }
                                }

                                if (multivalue[i].Type == "Order") {

                                    var dis_val = parseFloat((multivalue[i].Rate * multivalue[i].Quantity)) * parseFloat((multivalue[i].Discount / 100));
                                    var fr = multivalue[i].Free;
                                    var str = "<td><input type='checkbox' class='case' /></td>";
                                    str += '<td>' + (i + 1) + '</td>';
                                    str += "<td><input class='form-control autoc' type='text' style='display:none' data-validation='required' id='Item_" + (i + 1) + "' t='" + multivalue[i].Product_Name + "'  value='" + multivalue[i].Product_Code + "' name='item_name[]' >" + multivalue[i].Product_Name + "<input type ='hidden' id ='Hid_Pro_code' class='rrr' name ='Hid_Pro_code' value='" + multivalue[i].Product_Code + "'><input type='hidden' id='pro_sale_code' value=" + multivalue[i].Sale_Code + "><input type='hidden' id='Hid_order_code'  name='Hid_order_code' value=" + multivalue[i].Trans_Order_No + " ><div class='second_row_div'></div></td>";
                                    str += "<td><select class='form-control unit' disabled='disabled' name='unit'>" + bindunit1 + "</select></td>";
                                    str += "<td><input class='form-control' type='text' id='Price_' name='price[]' data-cell='C1' data-format='0.00' data-validation='required' readonly=''  value=" + parseFloat(multivalue[i].Rate).toFixed(2) + " /><input class='form-control' type='hidden' id='Rate_in_peice' name='Rate_in_peice[]' data-cell='C1' data-format='0.00' data-validation='required' readonly=''  value=" + parseFloat(multivalue[i].Rate_in_peice).toFixed(2) + " /><input type='hidden' name='stkval' stkgood='0' stkdamage='0' /></td>";
                                    str += "<td><input class='form-control' type='text' data-validation='required ' id='Order_qty' readonly autocomplete='off'  name='Order_quantity[]' data-cell='K1' data-format='0' value=" + multivalue[i].Quantity + " /></td>";
                                    str += "<td><input class='form-control validate' type='text' data-validation='required' id='english' autocomplete='off'  name='quantity[]' data-cell='D1' data-format='0' value=" + multivalue[i].Quantity + " /></td>";
                                    str += "<td><input class='form-control Dis' type='text' autocomplete='off' id='Dis' name='dis[]' readonly data-cell='Y1' data-format='0' data-validation='required' value=" + dis_val + " ><input class='form-control hid' type='hidden' id='hid_dis' value=" + dis_val + "> </td>";
                                    str += "<td><input class='form-control' type='text' id='Free' autocomplete='o ff' name='free[]' readonly data-cell='I1' data-format='0'  data-validation='required' value=" + multivalue[i].Free + " ></td><td style='display:none' ><input type='hidden' class='of_pro_name'  name='of_pro_name' value='" + multivalue[i].Off_Pro_Name + "' ></td><td style='display:none' ><input type='hidden' class='of_pro_code'  name='of_pro_code' value=" + multivalue[i].Off_Pro_Code + " ></td><td style='display:none;'<label id=" + multivalue[i].Product_Code + " name='free' class='fre' freecqty=" + multivalue[i].Free + " cqty=" + multivalue[i].Quantity + " freepro=" + multivalue[i].Product_Code + " unit=" + multivalue[i].Off_Pro_Unit + ">" + multivalue[i].Free + '' + multivalue[i].Off_Pro_Unit + "</lable></td> <td style='display:none' ><input type='hidden' id=" + multivalue[i].Product_Code + " class='fre1' name='fre1' >" + multivalue[i].Free + "</td><td style='display:none' ><input type='hidden' id='erp_code' class='erp_code' value=" + erp_cd + " </td>";
                                    str += "<td><input class='form-control' type='text' id='total' value=" + multivalue[i].Amount + " data-cell='E1' data-format='0.00' readonly  /><input type ='hidden' id ='row_tot' class='row_tot' name ='row_tot' ><input type ='hidden' id ='type' class='type' name ='type' value='Order'><input type ='hidden' id ='setting' class='setting' name ='setting'></td>";
                                    str += "<td><input class='form-control tax_val' type='text' data-cell='E1' data-format='0.00' readonly /></td>";
                                    str += "<td style='display:none;'><input type='hidden' class='tot_tax_per'/></td>";
                                    str += "<td><input class='form-control' type='text' id='grs_tot' class='grs_tot' data-cell='E1' data-format='0.00' readonly /></td>";
                                    $('#tblCustomers tbody').append('<tr class=' + multivalue[i].Product_Code + '>' + str + '</tr>');
                                    AllOrderIDs = [];
                                }
                                else {

                                    var str = "<td><input type='checkbox' class='case' /></td>";
                                    str += '<td>' + (i + 1) + '</td>';
                                    str += "<td><input class='form-control autoc' type='text' style='display:none' data-validation='required' id='Item_" + (i + 1) + "' t='" + multivalue[i].Product_Name + "'  value='" + multivalue[i].Product_Code + "' name='item_name[]' >" + multivalue[i].Product_Name + "<input type ='hidden' id ='Hid_Pro_code' class='rrr' name ='Hid_Pro_code' value='" + multivalue[i].Product_Code + "'><input type='hidden' id='pro_sale_code' value=" + multivalue[i].Sale_Code + "><input type='hidden' id='Hid_order_code'  name='Hid_order_code' value=" + multivalue[i].Trans_Order_No + " >&nbsp&nbsp<div class='second_row_div'></div></td>";
                                    str += "<td><select class='form-control unit' name='unit' >" + bindunit1 + "</select></td>";
                                    str += "<td><input class='form-control' type='text' id='Price_' name='price[]' data-cell='C1' data-format='0.00' data-validation='required' readonly=''  value=" + parseFloat(multivalue[i].Rate).toFixed(2) + " /><input class='form-control' type='hidden' id='Rate_in_peice' name='Rate_in_peice[]' data-cell='C1' data-format='0.00' data-validation='required' readonly=''  value=" + parseFloat(multivalue[i].Rate_in_peice).toFixed(2) + " /><input type='hidden' name='stkval' stkgood='0' stkdamage='0' /></td>";
                                    str += "<td><input class='form-control ' type='text' data-validation='required' id='Order_qty' readonly autocomplete='off'  name='Order_quantity[]' data-cell='K1' data-format='0' value=" + multivalue[i].Quantity + " /></td>";
                                    str += "<td><input class='form-control validate' type='text' data-validation='required' id='english' autocomplete='off'  name='quantity[]' data-cell='D1' data-format='0' value=" + multivalue[i].Quantity + " /></td>";
                                    str += "<td><input class='form-control Dis' type='text' autocomplete='off' id='Dis' name='dis[]' readonly data-cell='Y1' data-format='0' data-validation='required' value=" + multivalue[i].Discount + " ><input class='form-control hid' type='hidden' id='hid_dis' value=" + dis_val + "> </td>";
                                    str += "<td><input class='form-control ' type='text' id='Free' autocomplete='off' name='free[]' readonly data-cell='I1' data-format='0'  data-validation='required' value=" + multivalue[i].Free + " ></td><td style='display:none' ><input type='hidden' class='of_pro_name'  name='of_pro_name' value='" + multivalue[i].Off_Pro_Name + "' ></td><td style='display:none' ><input type='hidden' class='of_pro_code'  name='of_pro_code' value=" + multivalue[i].Off_Pro_Code + " ></td><td style='display:none;'<label id=" + multivalue[i].Product_Code + " name='free' class='fre' freecqty=" + multivalue[i].Free + " cqty=" + multivalue[i].Quantity + " freepro=" + multivalue[i].Product_Code + " unit=" + multivalue[i].Off_Pro_Unit + ">" + multivalue[i].Free + '' + multivalue[i].Off_Pro_Unit + "</lable></td> <td style='display:none' ><input type='hidden' id=" + multivalue[i].Product_Code + " class='fre1' name='fre1' >" + multivalue[i].Free + "</td><td style='display:none' ><input type='hidden' id='erp_code' class='erp_code' value=" + erp_cd + " </td>";
                                    str += "<td><input class='form-control' type='text' id='total' value=" + multivalue[i].Amount + " data-cell='E1' data-format='0.00' readonly  /><input type ='hidden' id ='row_tot' class='row_tot' name ='row_tot'><input type ='hidden' id ='type' class='type' name ='type' value='Add'><input type ='hidden' id ='setting' class='setting' name ='setting'></td>";
                                    str += "<td><input class='form-control tax_val' type='text' data-cell='E1' data-format='0.00' class='tax_val' readonly /></td>";
                                    str += "<td style='display:none;'><input type='hidden' class='tot_tax_per'/></td>";
                                    str += "<td><input class='form-control' type='text' id='grs_tot' class='grs_tot' data-cell='E1' data-format='0.00' readonly /></td>";
                                    $('#tblCustomers tbody').append('<tr class=' + multivalue[i].Product_Code + '>' + str + '</tr>');
                                    $($('#tblCustomers tbody tr')[i]).css('background-color', '#9fdfbe')
                                    d(i);
                                    $('#Item' + i + '').val(multivalue[i].Product_Code);
                                    $('#Item' + i + '').chosen();
                                    AllOrderIDs = [];
                                }

                                get_tax_Details(multivalue[i].Product_Code, $('#tblCustomers tbody tr')[i], i);
                                // calc_stock($($('#tblCustomers tbody tr')[i]), 1);
                                if (parseInt(multivalue[i].Stock) > 0) {
                                    $('#tblCustomers tbody tr :last').find('.lbl_stock').css("color", "rgb(39 194 76)");
                                }
                                else {
                                    $('#tblCustomers tbody tr :last').find('.lbl_stock').css("color", "red");
                                }
                                $('#tblCustomers tbody tr :last').find('.lbl_stock').text(multivalue[i].Stock);
                                $('#tblCustomers tbody tr :last').find('.lbl_con_fac').text(erp_cd);
                                $('#tblCustomers tbody tr :last').find('.lbl_dis_per').text(multivalue[i].Discount);
                            }
                            else {
                                alert('failed');
                            }
                        }
                        // get();
                        getreload();
                    }
                    else {
                        $("[id*=sub_tot]").val();
                        $("[id*=Txt_Dis_tot]").val();
                        $("[id*=Tax_GST]").val();
                        $("[id*=Tax_CGST]").val();
                        $("[id*=Tax_SGST]").val()
                        $("[id*=in_Tot]").val();
                        $("[id*=Ad_Paid]").val();
                        $("[id*=Amt_Tot]").val();
                        AllOrderIDs = [];
                    }
                }

                function get_tax_Details(Produc_Code, row, idx) {

                    var tax_filter = []; var append = '';

                    if (dis_state_code == filter_ret_state[0].State_Code || filter_ret_state[0].State_Code == 0) { var type = 0; } else { var type = 1; }

                    tax_filter = All_Tax.filter(function (r) {
                        return (r.Product_Detail_Code == Produc_Code /*&& (r.Tax_Method_Id == type || r.Tax_Method_Id == 2)*/)
                    });
                    $(row).find('.second_row_div').text('');
                    $(row).find('.second_row_div').append("<i class='fa fa-stack-overflow' ></i><label style='padding-top: 8px;font-size: 12px;'>Stock :</label>&nbsp<label style='padding-top: 8px;font-weight:100;font-size: 12px;' class='lbl_stock' id='id_stock'></label>&nbsp&nbsp<i class='fa fa-code' ></i><label style='padding-top: 8px;font-size: 12px;'>Con_Fac :</label>&nbsp<label style='padding-top: 8px;font-weight:100;font-size: 12px;' class='lbl_con_fac' id='id_con_fac'></label>&nbsp&nbsp<i class='fa fa-tags'></i><label style='padding-top: 8px;font-size: 12px;'>Dis % :</label>&nbsp<label style='padding-top: 8px;font-weight:100;font-size: 12px;'id='lbl_dis_per' class='lbl_dis_per'></label>");

                    var append = ''; var total_tax_per = 0;

                    if (tax_filter.length > 0) {

                        for (var z = 0; z < tax_filter.length; z++) {
                            append += "&nbsp;&nbsp;<label style='padding-top: 8px;font-weight: bold;font-size: 12px;'>" + tax_filter[z].Tax_Type + "</label> :<label style='padding-top: 8px;font-weight: bold;font-size: 12px;' class=" + tax_filter[z].Tax_Type + " id=" + tax_filter[z].Tax_Type + ">" + tax_filter[z].Tax_name + "</label>";
                            var Push_data = tax_filter[z].Tax_Type;
                            total_tax_per = total_tax_per + parseFloat(tax_filter[z].Tax_Val);
                            multivalue[idx][Push_data] = tax_filter[z].Tax_Val;
                            $(row).find('td:last').after('<td style="display:none;"><input type="hidden" class="each_tax_per" text=' + tax_filter[z].Tax_Type + ' value=' + tax_filter[z].Tax_Val + ' tax_id=' + tax_filter[z].Tax_Id + ' ></td>');
                        }
                        $(row).find('.tot_tax_per').val(total_tax_per);
                        $(row).find('.second_row_div').append(append);
                    }
                }

                function get() {

                    $("#tblCustomers >tbody >tr").each(function () {

                        var row = $(this).closest("tr");
                        idx = $(row).index();

                        var price = $(this).find('[id*=Price_]').val();
                        if (price == 'null' || typeof price == "undefined" || price == "" || isNaN(price)) { price = 0; }

                        var qty = $(this).find('[id*=english]').val();
                        if (qty == 'null' || typeof qty == "undefined" || qty == "" || isNaN(qty)) { qty = 0; }

                        var dis = $(this).find('.lbl_dis_per').text();
                        if (dis == 'null' || typeof dis == "undefined" || dis == "" || isNaN(dis)) { dis = 0; }

                        var dis_cal = parseFloat(price) * parseFloat(qty) * (parseFloat(dis) / 100);
                        $(this).find('[id*=hid_dis]').text(dis_cal.toFixed(2));

                        var cal = ((parseFloat(price) * parseFloat(qty)) - ((parseFloat(price) * parseFloat(qty)) * (dis / 100)));
                        $(this).find('[id*=total]').val(cal.toFixed(2));

                        var tots_tax_perc = $(this).find('.tot_tax_per').val();
                        if (isNaN(tots_tax_perc) || tots_tax_perc == "" || typeof tots_tax_perc == "undefined") { tots_tax_perc = 0; }
                        var cal_tax_perc = tots_tax_perc / 100 * $(this).find('[id*=total]').val();
                        $(this).find('.tax_val').val(parseFloat(cal_tax_perc).toFixed(2));

                        var tax_len = $(this).find('.each_tax_per').length;

                        for (var g = 0; g < tax_len; g++) {

                            var each_tax_per = $(this).find('.each_tax_per').eq(g).attr('value') || 0;
                            if (isNaN(each_tax_per) || each_tax_per == "" || typeof each_tax_per == "undefined") { each_tax_per = 0; }
                            var cal_each_tax_perc = each_tax_per / 100 * $(this).find('[id*=total]').val();
                            $(this).find('.each_tax_per').eq(g).attr('tax_value', parseFloat(cal_each_tax_perc).toFixed(2));
                        }

                        var gos = parseFloat(cal) + parseFloat(cal_tax_perc);
                        $(this).find('[id*=grs_tot]').val(gos.toFixed(2));

                    });
                    all();
                };
                function getreload() {

                    $("#tblCustomers >tbody >tr").each(function () {

                        var row = $(this).closest("tr");
                        idx = $(row).index();

                        var price = $(this).find('[id*=Price_]').val();
                        if (price == 'null' || typeof price == "undefined" || price == "" || isNaN(price)) { price = 0; }
                        var conf = $(this).find('[id*=id_con_fac]').text();
                        if (conf == 'null' || typeof conf == "undefined" || conf == "" || isNaN(conf)) { conf = 1; }

                        var qty = $(this).find('[id*=english]').val();
                        if (qty == 'null' || typeof qty == "undefined" || qty == "" || isNaN(qty)) { qty = 0; }

                        var dis = $(this).find('.lbl_dis_per').text();
                        if (dis == 'null' || typeof dis == "undefined" || dis == "" || isNaN(dis)) { dis = 0; }

                        var dis_cal = parseFloat(price) * parseFloat(qty) * (parseFloat(dis) / 100);
                        $(this).find('[id*=hid_dis]').text(dis_cal.toFixed(2));

                        var cal = ((parseFloat(price)  * parseFloat(qty)) - (parseFloat(price)  * parseFloat(qty)) * (dis / 100));
                        $(this).find('[id*=total]').val(cal.toFixed(2));

                        var tots_tax_perc = $(this).find('.tot_tax_per').val();
                        if (isNaN(tots_tax_perc) || tots_tax_perc == "" || typeof tots_tax_perc == "undefined") { tots_tax_perc = 0; }
                        var cal_tax_perc = tots_tax_perc / 100 * $(this).find('[id*=total]').val();
                        $(this).find('.tax_val').val(parseFloat(cal_tax_perc).toFixed(2));

                        var tax_len = $(this).find('.each_tax_per').length;

                        for (var g = 0; g < tax_len; g++) {

                            var each_tax_per = $(this).find('.each_tax_per').eq(g).attr('value') || 0;
                            if (isNaN(each_tax_per) || each_tax_per == "" || typeof each_tax_per == "undefined") { each_tax_per = 0; }
                            var cal_each_tax_perc = each_tax_per / 100 * $(this).find('[id*=total]').val();
                            $(this).find('.each_tax_per').eq(g).attr('tax_value', parseFloat(cal_each_tax_perc).toFixed(2));
                        }

                        var gos = parseFloat(cal) + parseFloat(cal_tax_perc);
                        $(this).find('[id*=grs_tot]').val(gos.toFixed(2));

                    });
                    all();
                };
                function all() {

                    var myorder_qty = 0;
                    $("[id*=Order_qty]").each(function () {
                        var rs = $(this).closest('tr').find('#english').val();
                        var myq = $(this).val();
                        if (rs != '0') {
                            if (myq != "") {
                                myorder_qty = myorder_qty + parseFloat($(this).val());
                            }
                        }
                    });

                    var sale_order_qty = 0;
                    $("[id*=english]").each(function () {

                        var soq = $(this).val();
                        if (soq != "") {
                            sale_order_qty = sale_order_qty + parseFloat($(this).val());
                        }
                    });

                    var sale_dis = 0;
                    $("[id*=Dis]").each(function () {

                        var sd = $(this).val();
                        if (sd != "") {
                            sale_dis = sale_dis + parseFloat($(this).val());
                        }
                    });

                    var sale_free = 0;
                    $("[id*=Free]").each(function () {

                        var sf = $(this).val();
                        if (sf != "") {
                            sale_free = sale_free + parseFloat($(this).val());
                        }
                    });

                    var TaxTotal = 0;
                    $(".tax_val").each(function () {
                        var ta = $(this).val();
                        if (ta != "") {
                            TaxTotal = TaxTotal + parseFloat($(this).val());
                        }
                    });

                    $("[id*=Tax_GST]").val(TaxTotal.toFixed(2));

                    var dis_Tot = 0;
                    $("[id*=hid_dis]").each(function () {
                        var x = $(this).text();
                        if (isNaN(x) || x == "") x = 0;
                        dis_Tot = (dis_Tot + parseFloat(x));
                    });
                    $("[id*=Txt_Dis_tot]").val(dis_Tot.toFixed(2));

                    var grandTotal = 0;
                    $("[id*=total]").each(function () {

                        var gt = $(this).val();
                        if (gt != "") {
                            grandTotal = grandTotal + parseFloat($(this).val());
                        }
                    });
                    $("[id*=sub_tot]").val(grandTotal.toFixed(2));

                    var gs_tot = 0;
                    $("[id*=grs_tot]").each(function () {
                        var gt = $(this).val();
                        if (gt != "") {
                            gs_tot = gs_tot + parseFloat($(this).val());
                        }
                    });

                    //if (Extra_Tax_Type == 'TDS') {

                    //    var extra_tax = Extra_Tax;
                    //    var Calc_extra_tax = extra_tax / 100 * grandTotal;
                    //    var Calc_extra_total_tax = Calc_extra_tax + gs_tot;
                    //    $('#txt_tds').val(Math.round((Calc_extra_tax + Number.EPSILON) * 100) / 100);
                    //    var ffff = Math.round((Calc_extra_total_tax + Number.EPSILON) * 100 / 100);
                    //    $("[id*=in_Tot]").val(ffff.toFixed(2));

                    //}
                    //else {

                    //    var extra_tax = Extra_Tax;
                    //    var Calc_extra_tax = extra_tax / 100 * grandTotal;
                    //    var Calc_extra_total_tax = Calc_extra_tax + gs_tot;
                    //    $('#txt_tcs').val(Math.round((Calc_extra_tax + Number.EPSILON) * 100) / 100);
                    //    var ffff = Math.round((Calc_extra_total_tax + Number.EPSILON) * 100 / 100);
                    //    $("[id*=in_Tot]").val(ffff.toFixed(2));
                    //}
                    $("[id*=in_Tot]").val(gs_tot.toFixed(2));
                    $("[id*=Amt_Tot]").val(Math.round(gs_tot).toFixed(2));

                    var tbl = $('#tblCustomers');
                    $(tbl).find('tfoot tr').remove();

                    if (myorder_qty != '0' || sale_order_qty != '0' || sale_dis != '0' || sale_free != '0' || grandTotal != '0') {
                        $("#tblCustomers").append('<tfoot><tr><th></th><th></th><th></th><th></th><th></th><th id="Order_q">' + myorder_qty + '</th><th id="sal_q">' + sale_order_qty + '</th><th id="dis_t">' + sale_dis + '</th><th id="free_t">' + sale_free + '</th><th id="tot_sub_amt">' + grandTotal.toFixed(2) + '</th><th id="tax_th">' + TaxTotal.toFixed(2) + '</th><th id="tot_gs_tot">' + gs_tot.toFixed(2) + '</th></tr></tfoot>');
                    }
                }

                function calacl() {

                    var ext_amt = $(document).find('#tot_ex_amt').val();
                    var ttt = $(document).find('#in_Tot').val();
                    var set = 0;

                    if (ext_amt != "") {

                        if (ttt > parseFloat(ext_amt)) {

                            var dfdfdf = ttt - parseFloat(ext_amt);
                            $(document).find('#Amt_Tot').val(parseFloat(dfdfdf).toFixed(2));
                            $(document).find('#extra_amt').val(parseFloat(set).toFixed(2));
                            $(document).find('#tot_adju').val(parseFloat(ext_amt).toFixed(2));
                        }
                        else {

                            var dfdfdf = parseFloat(ext_amt) - ttt;
                            $(document).find('#extra_amt').val(parseFloat(dfdfdf).toFixed(2));
                            $(document).find('#tot_adju').val(parseFloat(ttt).toFixed(2));
                            $(document).find('#Amt_Tot').val(parseFloat(set).toFixed(2));
                        }
                    }
                }

                $(document).on('keyup', '[id*=Ad_Paid]', function () {
                    withadvance($(this).val());
                });

                function withadvance(get_adv) {

                    if (!jQuery.trim(get_adv) == '') {

                        if (!isNaN(parseFloat(get_adv))) {

                        }
                    } else {
                        get_adv = '';
                    }
                    var adTotal = 0;
                    var total_adv_amt = 0;
                    var dfd = 0;
                    var fdess = 0;
                    $("[id*=Ad_Paid]").each(function () {

                        var tal = parseInt(get_adv) || 0;
                        var d = parseFloat(get_adv) == NaN ? 0 : parseFloat(get_adv);
                        var totalvalue = $("[id*=in_Tot]").val();

                        if (totalvalue == "" || typeof totalvalue == "undefined") {
                            $("[id*=Ad_Paid]").val('');
                            rjalert('Error!', 'Please Select Order!..', 'error');
                            //alert("Please Select Order");
                            return false;
                        }
                        else {
                            total_adv_amt = $(document).find('#tot_ex_amt').val();

                            if (total_adv_amt != "") {

                                var ff = parseFloat(total_adv_amt) + tal;

                                if (ff > totalvalue) {

                                    adTotal = ff - totalvalue;
                                    $("[id*=Amt_Tot]").val(parseFloat(fdess).toFixed(2));
                                    $("[id*=extra_amt]").val(adTotal.toFixed(2));

                                }
                                else {

                                    adTotal = totalvalue - ff;
                                    $("[id*=extra_amt]").val(parseFloat(fdess).toFixed(2));
                                    $("[id*=Amt_Tot]").val(adTotal.toFixed(2));
                                }
                            }
                        }
                    });
                }

                $(document).on("focus", "#Free", function () {
                    var qua = $(this).closest("tr").find('#english').val();
                    if (qua.length <= 0) {
                        rjalert('Error!', 'Enter Quantity!..', 'error');
                        //alert('Enter Quantity');
                        $(this).closest('tr').find('#english').focus();
                    }
                });

                $(document).on("focus", "#Dis", function () {
                    var qua = $(this).closest("tr").find('#english').val();
                    if (qua.length <= 0) {
                        rjalert('Error!', 'Enter Quantity!..', 'error');
                        // alert('Enter Quantity');
                        $(this).closest('tr').find('#english').focus();
                    }
                });

                $(document).on("focus", "#total", function () {
                    var qua = $(this).closest("tr").find('#english').val();
                    if (qua.length <= 0) {
                        rjalert('Error!', 'Enter Quantity!..', 'error');
                        // alert('Enter Quantity');
                        $(this).closest('tr').find('#english').focus();
                    }
                });

                function calc_stock(n, flag) {

                    var pduct_code = n.find('.autoc').val();
                    var con_fc = n.find('.erp_code').val();
                    var u_n = n.find('.unit option:selected').text();

                    var sa_qty = 0;
                    var ad_qty = 0;

                    var sto_filter = [];

                    sto_filter = All_stock.filter(function (y) {
                        return (y.Prod_Code == pduct_code);
                    });

                    $(document).find('.' + pduct_code).each(function () {

                        var producode = $(this).find('#Hid_Pro_code').val();
                        var uni = $(this).find('.unit option:selected').val();
                        var con_fc = $(this).find('.erp_code').val();
                        var qqty = $(this).find('#english').val(); if (qqty == "" || qqty == null) { qqty = 0; }

                        if (sto_filter.length > 0) {
                            ad_qty += parseFloat(qqty) * con_fc;
                        }

                        if (sto_filter[0].Total_Stock > 0 && sto_filter[0].Total_Stock >= ad_qty) {
                            $(this).find('.validate').css('background-color', '');
                            $(this).find('.validate').removeClass('focus');
                        }
                        else {
                            $(this).css('background-color', '#ff6666');
                            if (flag != 1) {
                                rjalert('Error!', 'Stock is not Available for the product!..', 'error');
                                //alert('Stock is not Available for the product');
                            }
                            $(this).find('.validate').addClass('focus');
                        }
                    });
                }

                $(document).on('blur', '[id*=english]', function (e) {

                    var row = $(this).closest("tr");
                    idx = $(row).index();

                    if (row.find('.autoc').attr('t') == "") {
                        rjalert('Error!', 'Select Product!..', 'error');
                        //alert('Select Product');
                        $(this).val("");
                        return false;
                        $(this).closest('tr').find('.item').children("option:selected").focus();
                    }

                    var qqty = $(this).val();

                    if (parseFloat(qqty) > parseFloat(row.find('#Order_qty').val())) {
                        rjalert('Error!', 'To increase please create a new order..', 'error');
                        //alert('To increase please create a new order');
                        $(this).val(row.find('#Order_qty').val());
                        get();
                        return false;
                    }

                    if (parseFloat($(this).val()) < parseFloat(row.find('#Order_qty').val())) {


                        var txt;
                        $.confirm({
                            title: 'Alert!',
                            content: 'Do you want to Add the balance quantity to pending order!',
                            type: 'green',
                            typeAnimated: true,
                            autoClose: 'cancel|8000',
                            icon: 'fa fa-check fa-2x',
                            buttons: {
                                tryAgain: {
                                    text: 'OK',
                                    btnClass: 'btn-green',
                                    action: function () {
                                        row.find('.setting').attr('sett', 'yes');
                                        all();
                                        // $.alert('Quantity Will added to the pending order');
                                        $.confirm({
                                            title: 'Alert!',
                                            content: 'Quantity Will added to the pending order!',
                                            type: 'green',
                                            typeAnimated: true,
                                            //autoClose: 'cancel|8000',
                                            icon: 'fa fa-check fa-2x',
                                            buttons: {
                                                tryAgain: {
                                                    text: 'OK',
                                                    btnClass: 'btn-green',
                                                    action: function () {
                                                        if (qqty == '0') {
                                                            autodelalert(row);
                                                        }
                                                    }
                                                }
                                                //cancel: function () {

                                                //    //$.alert('Quantity Will not added to the pending order');
                                                //    //alert('Time Expired!');
                                                //}
                                            }

                                        });

                                    }


                                },

                                cancel: function () {
                                    row.find('.setting').attr('sett', 'No');
                                    // $.alert('Quantity Will not added to the pending order');
                                    if (qqty == '0') {
                                        autodelalert($(this).closest('tr'));
                                    }
                                    //alert('Time Expired!');
                                }
                            }
                        });

                        //r = confirm("Do you want to Add the balance quantity to pending order");
                        //if (r == true) {

                        //} else {
                        //    row.find('.setting').attr('sett', 'No');
                        //    alert('Quantity Will not added to the pending order');
                        //}
                    }
                    // calc_stock($(this).closest("tr"), 0);

                    if (!jQuery.trim($(this).val()) == '') {
                        if (!isNaN(parseFloat($(this).val()))) {

                            qty_fun(row);
                            multivalue[idx].Product_Code = row.find('.autoc').val();
                            multivalue[idx].Product_Name = row.find('.autoc').attr('t');
                            multivalue[idx].Sale_qt = $(this).val();
                            multivalue[idx].Quantity = $(this).val();
                            multivalue[idx].Free = row.find('[id*=Free]').val();
                            multivalue[idx].Discount = row.find('[id*=Dis]').val();
                            multivalue[idx].Off_Pro_Code = row.find('.of_pro_code').val();
                            multivalue[idx].Off_Pro_Name = row.find('.of_pro_name').val();
                            multivalue[idx].Off_Pro_Unit = row.find('.fre').attr('freecqty');

                            var ceon = row.find('.lbl_con_fac').text();
                            multivalue[idx].qty_in_sale_unit = $(this).val() * ceon;

                        }
                    } else {
                        $(this).val('');
                        qty_fun(row);

                        row.find('#Dis').val('');
                        row.find('#hid_dis').text('');
                        row.find('#Free').text('');
                        row.find('[id*=total]').val('');
                    }
                    get()
                    if (e.keyCode == 13) {
                        if ($(this).val() != '' || isNaN($(this).val())) {
                            AddRow();
                        }
                    }

                });

                //dis_count
                $(document).on('keyup', '[id*=Dis]', function () {
                    var row = $(this).closest("tr");
                    if (!jQuery.trim($(this).val()) == '') {
                        if (!isNaN(parseFloat($(this).val()))) {

                            var amd = row.find('[id*=Price_]').val();
                            var dis = row.find('[id*=Dis]').val();
                            var Val = 0;
                            if (dis != '' && amd != '') {

                                Val = (row.find('[id*=Price_]').val() * row.find('[id*=english]').val()) * (dis / 100);
                                row.find('#hid_dis').text(Val.toFixed(2));
                                row.find('[id*=total]').val(((row.find('[id*=Price_]').val() * row.find('[id*=english]').val()) - Val).toFixed(2));
                                var tot_value = row.find('[id*=total]').val();
                                var tot = Math.abs(tot_value / 100 * row.find('[id*=hd_tax_value]').val()).toFixed(2);

                            }
                        }
                    } else {
                        $(this).val('');
                        if (isNaN(dis)) dis = 0;
                        Val = (row.find('[id*=Price_]').val() * row.find('[id*=english]').val()) * (dis / 100);
                        row.find('#hid_dis').text(Val.toFixed(2));
                        row.find('[id*=total]').val(((row.find('[id*=Price_]').val() * row.find('[id*=english]').val()) - Val).toFixed(2));
                        var tot_value = row.find('[id*=total]').val();
                        var tot = Math.abs(tot_value / 100 * row.find('[id*=hd_tax_value]').val()).toFixed(2);
                        var Divided_value = tot / 2;
                    }
                    all();
                });


                $(document).on("click", ".category", function (e) {
                    e.preventDefault();
                    $('.category').removeClass('active');
                    $('.category').css('color', 'black');
                    $(this).addClass('active');
                    $(this).css('color', 'white');
                });

                function AddRow() {
                    if (Cust_Price.length > 0) {
                        itm = {}
                        itm.Trans_Order_No = '';
                        itm.Product_Code = '';
                        itm.Sale_Code = '';
                        itm.Product_Name = '';
                        itm.Rate = "0";
                        itm.Rate_in_peice = "0";
                        itm.Unit = '0';
                        itm.Unit_Code = '0';
                        itm.Quantity = "0";
                        //itm.Quantity = "0";
                        itm.Sale_qt = "0";
                        itm.qty_in_sale_unit = "0";
                        itm.Free = "0";
                        itm.Discount = "0";
                        itm.date = "0";
                        itm.Free = "0";
                        itm.Off_Pro_Code = "";
                        itm.Off_Pro_Name = "";
                        itm.Off_Pro_Unit = '';
                        itm.Tax_Id = '';
                        itm.Tax_Name = '';
                        itm.Tax_Val = '';
                        itm.E_Code = '';
                        itm.Type = 'Add';
                        itm.Sl_No = '0';
                        itm.Stock = '0';
                        itm.Con_fac_qty = '0';
                        itm.Avail_Stock = '0';
                        multivalue.push(itm);

                        var co = $('#tblCustomers tbody tr').length + 1;

                        var data = $("<tr class='tr_class' style='position:relative;'></tr>");
                        data.html("<td><input type='checkbox' class='case'/></td><td>" + co + "</td> " +
                            "<td><input class='form-control autoc' type='text' style='display:none' data-validation='required' name='item_name[]' ><select class='form-control item' name='item_name[]' style='width: 100%;'><option value='0'>---Select Product Name---</option></select><input type ='hidden' id ='Hid_Pro_code' class='rrr' name ='Hid_Pro_code'><input type='hidden' id='pro_sale_code' class='pro_sale_code'><div class='second_row_div'></div></td> " +
                            "<td id='sel_unit'><select id='unit' class='form-control unit'><option value='0'>Select</option></select></td>" +
                            "<td><input class='form-control' data-validation='required' autocomplete='off' type='text' id='Price_' readonly='' name='price[]' data-format='0.00' /><input class='form-control' data-validation='required' autocomplete='off' type='hidden' id='Rate_in_peice' readonly='' name='Rate_in_peice[]' data-format='0.00' /></td> " +
                            "<td><input class='form-control ' type='text' data-validation='required' autocomplete='off' readonly='' id='Order_qty' autocomplete='off'  name='Order_quantity[]' data-format='0' /></td>" +
                            "<td><input class='form-control validate' data-validation='required' autocomplete='off' type='text' id='english' name='quantity[]' data-format='0' /></td> " +
                            "<td><input class='form-control ' data-validation='required' autocomplete='off' type='text' id='Dis' readonly name='dis[]' data-format='0' /><input class='form-control hid' type='hidden' id='hid_dis'></td> " +
                            "<td><input class='form-control ' data-validation='required' autocomplete='off' type='text' id='Free' readonly name='free[]' data-format='0' /></td><td style='display:none' ><input type='hidden' class='of_pro_name'  name='of_pro_name' ></td><td style='display:none' ><input type='hidden' class='of_pro_code'  name='of_pro_code' ></td><td style='display:none;'<label name='free' class='fre'></lable></td><td style='display:none' ><input type='hidden' class='fre1' name='fre1' ></td><td style='display:none' ><input type='hidden' id='erp_code' class='erp_code' ></td>" +
                            "<td><input class='form-control' id='total' autocomplete='off' readonly name='total[]' type='text' data-format='0.00' /><input type ='hidden' id ='row_tot' class='row_tot' name ='row_tot'><input type ='hidden' id ='type' class='type' name ='type' value='Add'><input type ='hidden' id ='setting' class='setting' name ='setting'></td> " +
                            "<td><input class='form-control tax_val' type='text' data-cell='E1' data-format='0.00' readonly /></td>" +
                            "<td style='display:none;'><input type='hidden' class='tot_tax_per'/></td>" +
                            "<td><input class='form-control' type='text' id='grs_tot' class='grs_tot' data-cell='E1' data-format='0.00' readonly /></td>");
                        $('#tblCustomers').append(data);
                        d(data);
                        $(data).find('.item').chosen();

                        $('.item').on('chosen:showing_dropdown', function () {

                            var selected_cat_code = $('.btn.active').val();
                            var selected_cat_Name = $('.btn.active').text();
                            var filtered_prd = [];

                            filtered_prd = All_Product.filter(function (k) {
                                return (k.Product_Cat_Code == selected_cat_code);
                            });

                            $(this).html('');
                            filtered_prd = selected_cat_code == -1 ? All_Product : filtered_prd;

                            let str = '<option value=-1>--select--</option>';
                            for (var h = 0; h < filtered_prd.length; h++) {
                                str += `<option value="${filtered_prd[h].Product_Detail_Code}">${filtered_prd[h].Product_Detail_Name}</option>`;
                            }
                            $(this).html(str);
                            $(this).trigger("chosen:updated");
                            var row = $(this).closest('tr');
                            row.find("#Price_").val('');
                            row.find("#Rate_in_peice").val('');
                            row.find("[id*=english]").val('');
                            row.find("[id*=Dis]").val('');
                            row.find("[id*=Free]").val('0');
                            row.find("[id*=total]").val('');
                            row.find("[id*=sub_tot]").val('');
                            row.find("[id*=in_Tot]").val('');
                            row.find("[id*=Amt_Tot]").val('');
                            row.find("[id*=Txt_Dis_tot]").val('');
                            row.find('.innumber').val('');
                            row.find('#Tax_tot').val('');
                            row.find('#Ad_Paid').val('');
                            row.find('#Tax_GST').val('');
                            row.find('#Tax_CGST').val('');
                            row.find('#Tax_SGST').val('');
                            row.find('.unit').html("<option value='0'>Select</option>");
                            get();
                        });

                        event.stopPropagation();
                        $('#tblCustomers tbody tr:last').find(".item").trigger('chosen:open');
                        $('#tblCustomers tbody tr:last').addClass('addeffect').delay(600).queue(function () {
                            $('#tblCustomers tbody tr:last').removeClass('addeffect');
                        });

                    }
                    else {
                        rjalert('Attention!', "It's currently not possible to add products for this retailer. For further assistance, kindly contact the administrator", 'error');
                        //var rtname = $('#route-name').val();
                        // $("#route-name").val(rtname).trigger('change');
                        return false;
                    }
                }

                $('[id*=Add]').on('click', function () {
                    AddRow();
                });

                function d(CountValue) {
                    for (var b = 0; All_Product.length > b; b++) {
                        $(CountValue).find('.item').append($("<option></option>").val(All_Product[b].Product_Detail_Code).html(All_Product[b].Product_Detail_Name)).trigger('chosen:updated').css("width", "100%");;;
                    }
                }

                $(document).on("change", ".item", function (e) {

                    var selected_cust_Code = $('#recipient-name option:selected').val();
                    var selected_cust_Name = $('#recipient-name option:selected').text();

                    var selected_route_code = $('#route-name-name option:selected').val();
                    var selected_route_Name = $('#route-name option:selected').text();

                    if (selected_route_code == '' || selected_route_code == 0) {
                        rjalert('Error!', 'Select Route..', 'error');
                        // alert('Please select Route');
                        $('.item').val('');
                        $('.item ').chosen("destroy");
                        $('.item').chosen();
                        return false;
                    }

                    if (selected_cust_Code == '' || selected_cust_Code == 0) {
                        rjalert('Error!', 'Please select Customer..', 'error');
                        //alert('Please select Customer');
                        $('.item').val('');
                        $('.item ').chosen("destroy");
                        $('.item').chosen();
                        return false;
                    }

                    var product_name = $(this).children("option:selected").text();
                    var product_code = $(this).children("option:selected").val();
                    var row = $(this).closest("tr");
                    var indx = $(row).index();

                    var Pro_filter = []; var units1 = ''; var filter_unit = []; var batch_filter = []; var filter_tax_details = [];

                    //Pro_filter = Allrate.filter(function (t) {
                    //    return (t.Product_Detail_Code == product_code);
                    //});

                    Pro_filter = Cust_Price.filter(function (a) {
                        return (a.Product_Detail_Code == product_code /*&& a.Price_list_Sl_No == Rate_List_Code*/);
                    })

                    var ddlUnit = row.find('.unit');
                    ddlUnit.empty();
                    units1 += "<option value='0'>Select</option>";

                    filter_unit = All_unit.filter(function (w) {
                        return (product_code == w.Product_Detail_Code);
                    });
                    if (filter_unit.length > 0) {

                        //for (var z = 0; z < Pro_filter.length; z++) {

                        //    if (Pro_filter[z].Unit_code == Pro_filter[z].Move_MailFolder_Id) {
                        //        units1 += "<option selected value='" + Pro_filter[z].Move_MailFolder_Id + "'>" + Pro_filter[z].Move_MailFolder_Name + "</option>";
                        //    }
                        //    else {
                        //        units1 += "<option value='" + Pro_filter[z].Move_MailFolder_Id + "'>" + Pro_filter[z].Move_MailFolder_Name + "</option>";
                        //    }
                        //}
                        if (Pro_filter.length > 0) {
                            var unitrate = 0;
                            var Rate_in_peice = 0;
                            for (var z = 0; z < filter_unit.length; z++) {
                                if (filter_unit[z].Move_MailFolder_Id == Pro_filter[0].Base_Unit_code) {
                                    unitrate = Pro_filter[0].RP_Base_Rate;
                                    Rate_in_peice = Pro_filter[0].RP_Base_Rate;
                                    //  units += "<li id=" + unitrate + " name='itms' value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</li>";
                                    //if (Pro_filter[0].Unit_code == filter_unit[z].Move_MailFolder_Id) {
                                    //    units1 += "<option selected id=" + unitrate + " value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";
                                    //}
                                    //else
                                    units1 += "<option  id=" + unitrate + " Rate_in_peice=" + Rate_in_peice+" value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";
                                }
                                //else if (filter_unit[z].Move_MailFolder_Id != Pro_filter[0].UnitCode && filter_unit[z].Move_MailFolder_Id == Pro_filter[0].Base_Unit_code) {
                                //    unitrate = Pro_filter[0].RP_Base_Rate / Pro_filter[0].Sample_Erp_Code

                                //    //  units += "<li id=" + unitrate + " name='itms' value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</li>";

                                //    units1 += "<option  id=" + unitrate + " value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";
                                //}

                                else {
                                    unitrate = Pro_filter[0].RP_Base_Rate * filter_unit[z].Sample_Erp_Code;
                                    Rate_in_peice = Pro_filter[0].RP_Base_Rate;
                                    // units += "<li id=" + unitrate + " name='itms' value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</li>";
                                    //if (Pro_filter[0].Unit_code == filter_unit[z].Move_MailFolder_Id) {
                                    //    units1 += "<option selected id=" + unitrate + " value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";
                                    //}
                                    //else
                                    units1 += "<option  id=" + unitrate + " Rate_in_peice=" + Rate_in_peice +" value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";

                                }

                                //else if (filter_unit[z].Move_MailFolder_Id == Prod[0].Base_Unit_code) {
                                //    unitrate = Prod[0].PTS / Prod[0].Sample_Erp_Code
                                //    units += "<li id=" + unitrate + " name='itms' value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</li>";
                                //    units1 += "<option  id=" + unitrate + " value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";

                                //}

                            }
                        }
                    }

                    get_tax_Details(product_code, row, indx);

                    if (Pro_filter.length > 0) {

                        $(this).closest("tr").attr('class', Pro_filter[0].Product_Detail_Code);

                        row.find('.autoc').attr('t', product_name);
                        row.find('#pro_sale_code').val(Pro_filter[0].Sale_Erp_Code);
                        if (parseInt(Pro_filter[0].TotalStock) > 0) {
                            row.find('.lbl_stock').css("color", "rgb(39 194 76)");
                        }
                        else {
                            row.find('.lbl_stock').css("color", "red");
                        }
                        row.find('.lbl_stock').text(Pro_filter[0].TotalStock);
                        row.find('.autoc').val(Pro_filter[0].Product_Detail_Code);
                        row.find('.autoc').attr('t', Pro_filter[0].Product_Detail_Name);
                        row.find('.lbl_dis_per').text(0);


                        $(document).find('.' + product_code).each(function () {

                            var rowww = $(this).closest('tr');
                            var indxx = $(rowww).index();

                            var Unit_length = Pro_filter.length;
                            var row_length = $('#tblCustomers tbody').find('.' + product_code).length;

                            var Pre_pro = $(this).closest("tr").attr('Prev_Prod_Code');

                            if (Pre_pro == product_code) {
                                var row_unit = rowww.find('.Spinner-Value').text();
                                var row_unit_Code = rowww.find('.spinner-Value_val').attr('value');
                            }
                            else {
                                var row_unit = "";
                                var row_unit_Code = "";
                            }
                            row.closest("tr").attr('Prev_Prod_Code', product_code);


                            if (filter_unit.length >= row_length) {

                                var row_unit = rowww.find('.unit option:selected').text();
                                var row_unit_Code = rowww.find('.unit option:selected').val();

                                if (row_unit_Code == "" || row_unit_Code == undefined) {

                                    row.find('.unit').append(units1);

                                    var unt_rate = Pro_filter[0].RP_Base_Rate * Pro_filter[0].Sample_Erp_Code;
                                    if (unt_rate == 'null' || typeof unt_rate == "undefined" || unt_rate == "" || isNaN(unt_rate)) { unt_rate = 0; }

                                    //row.find('#Price_').val(unt_rate.toFixed(2));
                                    //multivalue[indx].Rate = unt_rate;
                                    row.find('.lbl_con_fac').text(Pro_filter[0].Sample_Erp_Code);
                                    row.find('#erp_code').val(Pro_filter[0].Sample_Erp_Code);
                                    //multivalue[indx].Con_fac_qty = Pro_filter[0].Sample_Erp_Code;
                                    //multivalue[indx].Unit = Pro_filter[0].product_unit;
                                    //multivalue[indx].Unit_Code = Pro_filter[0].Unit_code;

                                }
                                //else if (Pro_filter[0].Unit_code == row_unit_Code) {

                                //    if (indxx != indx) {

                                //        row.find('.unit').append(units1);
                                //        row.find('.unit option[value=' + Pro_filter[0].Base_Unit_code + ']').attr('selected', 'selected');

                                //        var unt_rate = Pro_filter[0].RP_Base_Rate;
                                //        if (unt_rate == 'null' || typeof unt_rate == "undefined" || unt_rate == "" || isNaN(unt_rate)) { unt_rate = 0; }

                                //        row.find('#Price_').val(parseFloat(unt_rate).toFixed(2));
                                //        multivalue[indx].Rate = unt_rate;
                                //        row.find('.lbl_con_fac').text(1);
                                //        row.find('#erp_code').val(1);
                                //        multivalue[indx].Con_fac_qty = 1;
                                //        multivalue[indx].Unit = Pro_filter[0].Product_Sale_Unit;
                                //        multivalue[indx].Unit_Code = Pro_filter[0].Base_Unit_code;

                                //    }
                                //    else {

                                //        var unt_rate = Pro_filter[0].RP_Base_Rate * Pro_filter[0].Sample_Erp_Code;
                                //        if (unt_rate == 'null' || typeof unt_rate == "undefined" || unt_rate == "" || isNaN(unt_rate)) { unt_rate = 0; }

                                //        row.find('#Price_').val(unt_rate.toFixed(2));
                                //        multivalue[indx].Rate = unt_rate;
                                //    }
                                //}
                                //else if (Pro_filter[0].Base_Unit_code == row_unit_Code) {

                                //    if (indxx != indx) {

                                //        row.find('.unit').append(units1);
                                //        row.find('.unit option[value=' + Pro_filter[0].Unit_code + ']').attr('selected', 'selected');

                                //        var unt_rate = Pro_filter[0].RP_Base_Rate * Pro_filter[0].Sample_Erp_Code;
                                //        if (unt_rate == 'null' || typeof unt_rate == "undefined" || unt_rate == "" || isNaN(unt_rate)) { unt_rate = 0; }

                                //        row.find('#Price_').val(unt_rate.toFixed(2));
                                //        multivalue[indx].Rate = unt_rate;
                                //        row.find('.lbl_con_fac').text(Pro_filter[0].Sample_Erp_Code);
                                //        row.find('#erp_code').val(Pro_filter[0].Sample_Erp_Code);
                                //        multivalue[indx].Con_fac_qty = Pro_filter[0].Sample_Erp_Code;
                                //        multivalue[indx].Unit = Pro_filter[0].product_unit;
                                //        multivalue[indx].Unit_Code = Pro_filter[0].Unit_code;
                                //    }
                                //    else {

                                //        var unt_rate = Pro_filter[0].RP_Base_Rate;
                                //        if (unt_rate == 'null' || typeof unt_rate == "undefined" || unt_rate == "" || isNaN(unt_rate)) { unt_rate = 0; }

                                //        row.find('#Price_').val(parseFloat(unt_rate).toFixed(2));
                                //        multivalue[indx].Rate = unt_rate;
                                //    }
                                //}
                                set = 0;
                                setTimeout(function () { row.find('#english').focus() }, 100);
                            }
                            else {
                                row.find('.item').val('');
                                row.find('.item ').chosen("destroy");
                                row.find('.item').chosen();
                                row.find('#Price_').val('');
                                row.find('#english').val('');
                                row.find('.unit').append("<option value='0'>Select</option>");
                                row.find('.second_row_div').text('');
                                multivalue[indx].Unit = 'Select';
                                multivalue[indx].Unit_Code = 'Select';
                                row.find('.lbl_stock').text(0);
                                get();
                                rjalert('Error!', 'Product Units Already Selected..', 'error');
                                //alert('Product Units Already Selected');
                                set = 1;
                                return false;
                            }

                        });

                       // getscheme(row.find('#pro_sale_code').val(), row.find('#english').val(), '', row.find('.unit :selected').val(), row, row.find(".fre1").text(), row.find('#Hid_Pro_code').val(), indx, product_name);
                        row.find('#Hid_Pro_code').val(Pro_filter[0].Product_Detail_Code);

                        multivalue[indx].Trans_Order_No = 0;
                        multivalue[indx].Product_Code = Pro_filter[0].Product_Detail_Code;
                        multivalue[indx].Product_Name = Pro_filter[0].Product_Detail_Name;
                        multivalue[indx].Sale_Code = Pro_filter[0].Sale_Erp_Code;
                        multivalue[indx].Tax_Id = Pro_filter[0].Tax_Id;
                        multivalue[indx].Tax_Val = Pro_filter[0].Tax_Val;
                        multivalue[indx].E_Code = 0;
                        multivalue[indx].Tax_Name = Pro_filter[0].Tax_Name;
                        multivalue[indx].Stock = Pro_filter[0].TotalStock;


                        multivalue[indx].Unit = '';
                        multivalue[indx].Unit_Code = '';
                        row.find("#Price_").val('');
                        row.find("[id*=english]").val('');
                        row.find("[id*=Dis]").val('');
                        row.find("[id*=Free]").val('');
                        row.find("[id*=total]").val('');
                        row.find("[id*=sub_tot]").val('');
                        row.find("[id*=in_Tot]").val('');
                        row.find("[id*=Amt_Tot]").val('');
                        row.find("[id*=Txt_Dis_tot]").val('');
                        row.find('.innumber').val('');
                        row.find('#Tax_tot').val('');
                        row.find('#Ad_Paid').val('');
                        row.find('#Tax_GST').val('');
                        row.find('#Tax_CGST').val('');
                        row.find('#Tax_SGST').val('');
                        get();
                    }
                    else {

                        $(this).closest("tr").attr('class', 0);
                        $(this).closest("tr").css('background-color', ' #9fdfbe');
                        $(this).closest("tr").find('.validate ').removeClass('focus');
                        $(this).closest("tr").find('.validate').css('background-color', '');
                        row.find('.unit').append(units1);
                        row.find('#pro_sale_code').val(0);
                        row.find('#Price_').val('');
                        row.find('#Dis').val('');
                        row.find('#Free').val('');
                        row.find('#total').val('');
                        row.find('#english').val('');
                        row.find('#Order_qty').val('');
                        row.find('#grs_tot').val('');
                        row.find('.lbl_stock').text(0);
                        row.find('#erp_code').val(0);
                        row.find('.autoc').val(0);
                        row.find('.autoc').attr('t', 0);
                        row.find('.lbl_dis_per').text(0);
                        row.find('.lbl_con_fac').text(0);


                        //getscheme(row.find('#pro_sale_code').val(), row.find('#english').val(), '', row.find('.unit :selected').val(), row, row.find(".fre1").text(), row.find('#Hid_Pro_code').val(), indx, product_name);
                        row.find('#Hid_Pro_code').val(0);

                        multivalue[indx].Trans_Order_No = 0;
                        multivalue[indx].Product_Code = 0;
                        multivalue[indx].Product_Name = 0;
                        multivalue[indx].Sale_Code = 0;
                        multivalue[indx].Tax_Id = 0;
                        multivalue[indx].Tax_Val = 0;
                        multivalue[indx].E_Code = 0;
                        multivalue[indx].Tax_Name = 0;
                        multivalue[indx].Stock = 0;
                        multivalue[indx].Unit = 0;
                        multivalue[indx].Unit_Code = 0;
                        get();
                    }
                });

                $(document).on('change', '.unit', function () {

                    var row = $(this).closest("tr");
                    var indx = $(row).index();
                    var Selected_Unit = row.find('.unit option:selected').text();
                    var Selected_Unit_Code = row.find('.unit option:selected').val();
                    var erp_c = row.find('.erp_code').val();

                    if (Selected_Unit_Code == '0' || Selected_Unit_Code == "") {
                        rjalert('Error!', 'select Unit..', 'error');
                        // alert('select Unit');
                        multivalue[indx].Unit = '';
                        multivalue[indx].Unit_Code = '';
                        row.find("#Price_").val('');
                        row.find("[id*=english]").val('');
                        row.find("[id*=Dis]").val('');
                        row.find("[id*=Free]").val('');
                        row.find("[id*=total]").val('');
                        row.find("[id*=sub_tot]").val('');
                        row.find("[id*=in_Tot]").val('');
                        row.find("[id*=Amt_Tot]").val('');
                        row.find("[id*=Txt_Dis_tot]").val('');
                        row.find('.innumber').val('');
                        row.find('#Tax_tot').val('');
                        row.find('#Ad_Paid').val('');
                        row.find('#Tax_GST').val('');
                        row.find('#Tax_CGST').val('');
                        row.find('#Tax_SGST').val('');
                        get();
                        return false;
                    }

                    var selected_pro_code = row.find('#Hid_Pro_code').val();

                    var pro_filter = [];

                    pro_filter = multivalue.filter(function (s) {
                        //return (s.Product_Code == selected_pro_code && s.Unit == Selected_Unit);
                        return (s.Product_Code == selected_pro_code && s.Unit_Code == Selected_Unit_Code);
                    });

                    var ans2 = [];
                    //ans2 = Allrate.filter(function (t) {
                    //    //return (t.Product_Detail_Code == selected_pro_code && t.Move_MailFolder_Name == Selected_Unit);
                    //    return (t.Product_Detail_Code == selected_pro_code && t.Move_MailFolder_Id == Selected_Unit_Code);
                    //})

                    ans2 = Cust_Price.filter(function (a) {
                        return (a.Product_Detail_Code == selected_pro_code);
                    })


                    if (pro_filter.length > 0) {
                        rjalert('Error!', 'Product Unit Already Selected..', 'error');
                        //alert('Product Unit Already Selected');

                        if (multivalue[indx].Unit == '0') {
                            row.find('.unit').val('0')
                        }
                        else {

                            row.find('.unit option').each(function () {
                                if ($(this).val() == multivalue[indx].Unit_Code) {
                                    $(this).prop("selected", true);
                                }
                            });
                        }
                        return false;
                    }
                    else {

                        multivalue[indx].Product_Code = selected_pro_code;
                        multivalue[indx].Unit = Selected_Unit;
                        multivalue[indx].E_Code = ans2[0].Sample_Erp_Code;

                        if (ans2.length > 0) {
                            filter_unit = All_unit.filter(function (w) {
                                return (selected_pro_code == w.Product_Detail_Code && Selected_Unit_Code == w.Move_MailFolder_Id);
                            });
                            row.find('#id_con_fac').text(filter_unit[0].Sample_Erp_Code);
                            row.find('#erp_code').val(filter_unit[0].Sample_Erp_Code);
                            if (Selected_Unit_Code == ans2[0].Base_Unit_code) {
                                row.find('#Price_').val(parseFloat(ans2[0].RP_Base_Rate).toFixed(2));
                                row.find('#Rate_in_peice').val(parseFloat(ans2[0].RP_Base_Rate).toFixed(2));
                                multivalue[indx].Rate = ans2[0].RP_Base_Rate;
                                multivalue[indx].Rate_in_peice = ans2[0].RP_Base_Rate;
                                multivalue[indx].Unit = Selected_Unit;
                            }
                            else {
                                var r = parseFloat(ans2[0].RP_Base_Rate) * parseFloat(filter_unit[0].Sample_Erp_Code);
                                row.find('#Price_').val(r.toFixed(2));
                                row.find('#Rate_in_peice').val(ans2[0].RP_Base_Rate);
                                multivalue[indx].Rate = r
                                multivalue[indx].Rate_in_peice = ans2[0].RP_Base_Rate;
                                multivalue[indx].Unit = Selected_Unit;
                            }

                            multivalue[indx].Unit_Code = Selected_Unit_Code;
                            qty_fun(row);
                            get();
                            //if (ans2[0].Move_MailFolder_Name == ans2[0].Product_Sale_Unit) {
                            //if (Selected_Unit_Code == ans2[0].UnitCode) {
                            //    if (Selected_Unit_Code == ans2[0].Base_Unit_code) {
                            //        row.find('#id_con_fac').text(1);
                            //        row.find('#erp_code').val(1);
                            //    }
                            //    else {
                            //        row.find('#id_con_fac').text(ans2[0].Sample_Erp_Code);
                            //        row.find('#erp_code').val(ans2[0].Sample_Erp_Code);
                            //    }

                            //    row.find('#Price_').val(parseFloat(ans2[0].RP_Base_Rate).toFixed(2));
                            //    multivalue[indx].Rate = ans2[0].RP_Base_Rate;
                            //    multivalue[indx].Unit = Selected_Unit;
                            //    multivalue[indx].Unit_Code = Selected_Unit_Code;
                            //    qty_fun(row);
                            //    get();
                            //}
                            //else if (Selected_Unit_Code == ans2[0].Base_Unit_code) {
                            //    row.find('#id_con_fac').text(1);
                            //    row.find('#erp_code').val(1);
                            //    row.find('#Price_').val(parseFloat(ans2[0].RP_Base_Rate / ans2[0].Sample_Erp_Code).toFixed(2));
                            //    multivalue[indx].Rate = ans2[0].RP_Base_Rate / ans2[0].Sample_Erp_Code;
                            //    multivalue[indx].Unit = Selected_Unit;
                            //    multivalue[indx].Unit_Code = Selected_Unit_Code;
                            //    qty_fun(row);
                            //    get();
                            //}
                            //else {
                            //    if (Selected_Unit_Code == ans2[0].Base_Unit_code) {
                            //        row.find('#id_con_fac').text(1);
                            //        row.find('#erp_code').val(1);
                            //    }
                            //    else {
                            //        row.find('#id_con_fac').text(ans2[0].Sample_Erp_Code);
                            //        row.find('#erp_code').val(ans2[0].Sample_Erp_Code);
                            //    }
                            //    var cal_rate = ans2[0].RP_Base_Rate * ans2[0].Sample_Erp_Code;
                            //    if (cal_rate == "" || cal_rate == undefined) { cal_rate = 0; }
                            //    row.find('#Price_').val(parseFloat(cal_rate).toFixed(2));

                            //    multivalue[indx].Rate = cal_rate;
                            //    multivalue[indx].Unit = Selected_Unit;
                            //    multivalue[indx].Unit_Code = Selected_Unit_Code;
                            //    qty_fun(row);
                            //    get();
                            //}
                        }
                    }
                    // calc_stock($(this).closest("tr"), 0);
                });

                $(".delete").on('click', function () {
                    var ct = $(".case:checked").length;
                    if (ct == 0) {
                        rjalert('Error!', 'Please Select Any Product To Delete.', 'error');
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
                               
                             $(".case:checked").each(function () {

                        row = $(this).closest('tr');
                        var order_id = row.find('#Hid_order_code').val();
                        var prod = row.find('#Hid_Pro_code').val();
                        idx = $(row).index();
                       // getscheme(row.find('#pro_sale_code').val(), row.find('#english').val(), row.find('.unit :selected').val(), row, row.find(".fre1").text(), row.find('#Hid_Pro_code').val());
                        var pending = $(row).find('.setting').attr('sett') || 0;
                        $(row).find('#type').val('remove');
                        if (pending == 'yes') {
                            if (ct == 1) {
                                //row.addClass('deleffect').delay(200).queue(function () {
                                row.css("display", "none");
                                var k = 1;
                                $("#tblCustomers >tbody >tr").each(function () {
                                    console.log(k);
                                    $(this).children('td').eq(1).html(k++);
                                });
                                //});


                            }
                            else {
                                row.css("display", "none");
                            }

                        }
                        else {
                            multivalue.splice(idx, 1);
                            if (ct == 1) {
                                //row.css("position", "relative");
                                //row.addClass('deleffect').delay(200).queue(function () {
                                row.remove();
                                var k = 1;
                                $("#tblCustomers >tbody >tr").each(function () {
                                    console.log(k);
                                    $(this).children('td').eq(1).html(k++);
                                });
                                //});
                            }
                            else {
                                row.remove();
                            }
                        }

                        if (prod != "") {
                            $(document).find('.' + prod).each(function () {
                                //calc_stock($(this).closest('tr'), 0);
                            });
                        }
                        check();
                        get();
                    });
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
                   
                });
                function check() {
                    var k = 1;
                    $("#tblCustomers >tbody >tr").each(function () {
                        console.log(k);
                        $(this).children('td').eq(1).html(k++);
                    });
                }

                function select_all() {
                    $('input[class=case]:checkbox').each(function () {
                        if ($('input[class=check_all]:checkbox:checked').length == 0) {
                            $(this).prop("checked", false);
                        } else {
                            $(this).prop("checked", true);
                        }
                    });
                }

                function isValidDate(s) {
                    var bits = s.split('/');
                    var d = new Date(bits[2] + '/' + bits[1] + '/' + bits[0]);
                    return !!(d && (d.getMonth() + 1) == bits[1] && d.getDate() == Number(bits[0]));
                }

                $(function () {
                    Date.prototype.ddmmyyyy = function () {
                        var dd = this.getDate().toString();
                        var mm = (this.getMonth() + 1).toString();
                        var yyyy = this.getFullYear().toString();
                        return (mm[1] ? mm : "0" + mm[0]) + "-" + (dd[1] ? dd : "0" + dd[0]) + "-" + yyyy;
                    };
                    $("#datepicker1").datepicker({ dateFormat: "dd-mm-yy" });

                });

                $("#datepicker1").on('change', function () {
                    var selectedDate = $(this).val().split("/").join("-");
                    var todaysDate = new Date().ddmmyyyy();
                    if (selectedDate < todaysDate) {
                        rjalert('Error!', 'Selected date must be greater than today date..', 'error');
                        //alert('Selected date must be greater than today date');
                        $(this).val('');
                    }
                });

                $("#datepicker").on('change', function () {
                    var selectedDate = $(this).val().split("/").join("-");
                    var todaysDate = new Date().ddmmyyyy();
                    if (selectedDate < todaysDate) {
                        rjalert('Error!', 'Selected date must be greater than today date..', 'error');
                        //alert('Selected date must be greater than today date');
                        $(this).val('');
                    }
                });

                $(document).ready(function () {
                    $(document).on('click', '.pleasewait', function () {
                        window.location = "../Stockist/Invoice_Print.aspx?Or_Date=" + encodeURIComponent($('#<%=InvoiceNumber.ClientID%>').val()) + "&Dis_code=" + encodeURIComponent(stockistcode) + "&Cus_code=" + encodeURIComponent($("#recipient-name option:selected").val());
                    });
                });

                var approve = 0;

                function svorder() {

                    if (approve == "1") {

                        var today1 = new Date();
                        var dd = today1.getDate();
                        var mm = today1.getMonth() + 1;
                        var yyyy = today1.getFullYear();
                        var hh = today1.getHours();
                        var mmm = today1.getMinutes();
                        var sec = today1.getSeconds();

                        if (dd < 10) {
                            dd = '0' + dd
                        }
                        if (mm < 10) {
                            mm = '0' + mm
                        }
                        today1 = yyyy + '-' + mm + '-' + dd + ' ' + hh + ':' + mmm + ':' + sec;

                        var Customer_Code = $('#recipient-name :selected').val();
                        var Customer_Name = $('#recipient-name :selected').text();
                        Customer_Name = Customer_Name.replace(/&/g, "&amp;");
                        var ret = $('#recipient-name :selected').val();

                        if (Customer_Code == "0") {
                            $('.spinnner_div').hide();
                            approve = 0;
                            rjalert('Error!', 'Please Select Customer..', 'error');
                            //alert("Please Select Customer.");
                            return false;
                        }
                        var Pay_Due = $('#datepicker').val(); //if (Pay_Due.length <= 0) { approve = 0;//alert('Enter Payment Due.!!'); $('#datepicker').focus(); return false; }
                        var Del_Date = $('#datepicker1').val(); //if (Del_Date.length <= 0) {approve = 0; //alert('Enter Delivery Date.!!'); $('#datepicker1').focus(); return false; }
                        //  if ($('input[type=checkbox]:checked').length == 0) { approve = 0; alert("Please select Order ID"); return false; }
                        var ship_met = $('#Sel_Shi_Med option:selected').text(); //if (ship_met == "Select") { approve = 0; alert('Select Shipping Mode!!'); $('#Sel_Shi_Med').focus(); return false; }
                        var pay_term = $('#Sel_Pay_Term option:selected').text(); //if (pay_term == "Select") { approve = 0; alert('Select Payment Mode'); $('#Sel_Pay_Term').focus(); return false; }

                        if (pay_term == "Select" || pay_term == "Cash") {
                            var ref = '0';
                        }
                        else {
                            //var ref = $('#txt_ref_no').val();
                            var ref = '0';
                            //if ($('#txt_ref_no').val() == "") {
                            //    approve = 0;
                            //    alert('Enter Reference No');
                            //    $('#txt_ref_no').focus();
                            //    //chk = false;
                            //    return false;
                            //}
                        }

                        var sub_tot = $('#sub_tot').val();
                        var Gst_Value = $('#Tax_GST').val();
                        var tcs_val = $('#txt_tcs').val() || 0;
                        var tds_val = $('#txt_tds').val() || 0;
                        var total = $('#in_Tot').val();

                        var chck = true;

                        $('#tblCustomers > tbody > tr').each(function () {

                            var pcode = $(this).find('.item option:selected').val();
                            var un = $(this).find('.unit option:selected').val();
                            var qt = $(this).find('#english').val();
							if(qt=='')
							  qt=0;
                            if (pcode == "0") {
                                $('.spinnner_div').hide();
                                approve = 0;
                                chck = false;
                                rjalert('Error!', 'Please Select Product!!.', 'error');
                                //alert('Please Select Product!!');
                                return false;
                            }
                            if (un == "0") {
                                $('.spinnner_div').hide();
                                approve = 0;
                                chck = false;
                                rjalert('Error!', 'Please Select UOM!!.', 'error');
                                //alert('Please Select UOM!!');
                                return false;
                            }
                            if (parseInt(qt) <= 0 && $(this).find('#type').val() != 'remove') {
                                $('.spinnner_div').hide();
                                approve = 0;
                                chck = false;
                                rjalert('Error!', 'Enter Quantity OR Remove Product!!.', 'error');
                                //alert('Enter Quantity!!');
                                $('#quan').focus();
                                return false;
                            }

                        });


                        var ad_paid = $('#Ad_Paid').val() || 0; //if (ad_paid.length <= 0 && chck == true) { approve = 0; alert('Enter Advanced Paid!!'); $('#Ad_Paid').focus(); return false; }

                        var adj_amt = $('#tot_adju').val();
                        var amt_due = $('#Amt_Tot').val();
                        var remark = $('#notes').val();
                        var Dis_Tot = $('#Txt_Dis_tot').val();
                        var Order_No = $('#<%=Hid_orderno.ClientID%>').val();
                        var Advance_amt = $('#extra_amt').val();

                        var orderid = $('input[type=checkbox]:checked').map(function () {
                            return $(this).val();
                        }).get().join();

                        var Stk = []; dayarr = []; mainarr = []; taxArr = []; penarr = [];
                        $('.example').show();

                        var stock_chck = $('#tblCustomers tbody tr').find('.focus').length

                        if (stock_chck == 0) {

                            setTimeout(function () {

                                $.ajax({
                                    type: "POST",
                                    contentType: "application/json; charset=utf-8",
                                    async: false,
                                    url: "Invoice_Entry_2.aspx/Get_AllValues",
                                    data: "{'Cus_code':'" + $("#recipient-name option:selected").val() + "'}",
                                    dataType: "json",
                                    success: function (data) {

                                        var Credit_Limit = 0;
                                        var obj = data.d;
                                        console.log(data.d);

                                        if (obj.length >= 0) {
                                            var Ob = obj.split(',');
                                            var out_sta = Ob[0];
                                            Credit_Limit = Ob[1];
                                            Credit_amt = Ob[2];
                                            var ou_sta_tot = Number(out_sta) + Number(amt_due) - Number(Credit_amt);

                                            /* if (Number(Credit_Limit) >= Number(ou_sta_tot) || Number(Credit_Limit) == "0") {*/

                                            if (orderid != "") {
                                                od = $('#example-multiple-selected option').html().split('_');
                                                var dss = od[1];
                                                ar = dss.split('/');
                                                da = ar[1] + '/' + ar[0] + '/' + ar[2];
                                            }
                                            else {
                                                da = today1;
                                                orderid = 'Direct';
                                            }

                                            mainarr.push({

                                                sf_code: stockistcode,
                                                sf_name: stockistname,
                                                dis_code: stockistcode,
                                                dis_name: stockistname,
                                                cus_code: retailer_ID,
                                                cus_name: Customer_Name,
                                                order_date: da,
                                                inv_date: today1,
                                                del_date: $(document).find(".dd").val(),
                                                pay_term: pay_term,
                                                pay_due: $(document).find(".pd").val(),
                                                ship_mtd: ship_met,
                                                ship_term: 0,
                                                sub_total: sub_tot,
                                                tax_total: Gst_Value,
                                                total: total,
                                                adv_paid: ad_paid,
                                                Amt_due: amt_due,
                                                remark: remark,
                                                Dis_total: Dis_Tot,
                                                order_no: orderid,
                                                Div_Code: div_code,
                                                Advance_amount: Advance_amt,
                                                adjust_amt: adj_amt,
                                                Reference_no: ref,
                                                TCS: tcs_val,
                                                TDS: tds_val

                                            });
                                            var flagstk = 0;
                                            var flagsalesqty = 0;
                                            $('#tblCustomers > tbody > tr').each(function () {

                                                var Prd_code = $(this).find('[id*=Hid_Pro_code]').val() || 0;
                                                var un = $(this).find('.unit option:selected').text() || 0;
                                                var un_code = $(this).find('.unit option:selected').val() || 0;
                                                var conf = parseFloat($(this).find('.lbl_con_fac').text()) || 0;

                                                var res = [];
                                                res = Cust_Price.filter(function (t) {
                                                    return (t.Product_Detail_Code == Prd_code);
                                                });
                                                //res = Allrate.filter(function (t) {
                                                //    return (t.Product_Detail_Code == Prd_code && t.Move_MailFolder_Id == un_code);
                                                //});

                                               // if (res.length > 0) {

                                                    //if (un_code == res[0].UnitCode) {

                                                    //    var Order_qty = $(this).find('#Order_qty').val();
                                                    //    var sales_qty = $(this).find('[id*=english]').val();
                                                    //    var Conversion_qty = parseFloat($(this).find('[id*=english]').val());
                                                    //    var Original_qty = $(this).find('[id*=english]').val() || 0;
                                                    //}

                                                    //else {
                                                    //    var Order_qty = $(this).find('#Order_qty').val() || 0;
                                                    //    var sales_qty = $(this).find('[id*=english]').val() || 0;
                                                    //    var Conversion_qty = parseFloat($(this).find('[id*=english]').val()) * res[0].Sample_Erp_Code;
                                                    //    var Original_qty = $(this).find('[id*=english]').val() || 0;
                                                    //}
                                                    var Order_qty = $(this).find('#Order_qty').val() || 0;
                                                    var sales_qty = $(this).find('[id*=english]').val() || 0;
                                                    var Conversion_qty = parseFloat($(this).find('[id*=english]').val()) * conf;
                                                    var Original_qty = $(this).find('[id*=english]').val() || 0;
                                                    var lblstk = $(this).find('[id*=id_stock]').text() || 0;
													if (res.length > 0) {
                                                    if (res[0].TotalStock > 0 && res[0].TotalStock >= Conversion_qty) {
                                                        $(this).css('background-color', '');
                                                        $(this).find('.txtQty').removeClass('focus');
                                                    }
													}
                                                    if (sales_qty == "0"  || $(this).find('#type').val() == 'remove') {
                                                        flagsalesqty += 1;
                                                        $(this).css('background-color', 'pink');
                                                        $(this).find('.txtQty').addClass('focus');
                                                        
                                                    }
													else if(lblstk=='0'){
													 flagstk += 1;
                                                        $(this).find('.validate').css('background-color', 'pink');
                                                        $(this).attr('stk', '0');
                                                        $(this).find('.txtQty').addClass('focus');
													}
													else if (parseInt(lblstk) > 0 && parseInt(lblstk) >= Conversion_qty) {
                                                        $(this).css('background-color', '');
                                                        $(this).find('.txtQty').removeClass('focus');
                                                    }
                                                    else {
                                                        flagstk += 1;
                                                        $(this).find('.validate').css('background-color', 'pink');
                                                        $(this).attr('stk', '0');
                                                        $(this).find('.txtQty').addClass('focus');
                                                    }
                                                    var sno = $(this).find('[id*=Hid_order_code]').val() || 0;
                                                    var Prd_name = $(this).find('.autoc').attr('t').replace(/&/g, "&amp;") || 0;
                                                    var price = $(this).find('[id*=Price_]').val() || 0;
                                                    var Rate_in_peice = $(this).find('[id*=Rate_in_peice]').val() || 0;
                                                    var dis = $(this).find('[id*=Dis]').val() || 0;
                                                    var dis_val = $(this).find('#hid_dis').val() || 0;
                                                    var free = $(this).find('[id*=Free]').val() || 0;
                                                    var tot = $(this).find('[id*=total]').val() || 0;
                                                    var over_all_tot = $(this).find('[id*=row_tot]').val() || 0;
                                                    var Ut = $(this).find('.unit option:selected').text() || 0;
                                                    var unt_code = $(this).find('.unit option:selected').val() || 0;
                                                    var offer_unit = $(this).find('.fre').attr('unit') || 0;
                                                    var offer_code = $(this).find('.of_pro_code').val() || 0;
                                                    var offer_name = $(this).find('.of_pro_name').val() || 0;
                                                    var ty = $(this).find('#type').val() || 0;
                                                    var erp = $(this).find('.erp_code').val();
                                                    var settg = $(this).find('.setting').attr('sett') || 0;
                                                    var conversion_factor = $(this).find('.lbl_con_fac').text();
                                                    var tax_val = $(this).find('.tax_val').val();

                                                    var tax_len = $(this).find('.each_tax_per').length;

                                                    for (var g = 0; g < tax_len; g++) {

                                                        taxArr.push({
                                                            pro_code: $(this).find('[id*=Hid_Pro_code]').val() || 0,
                                                            Tax_Code: $(this).find('.each_tax_per').eq(g).attr('tax_id'),
                                                            Tax_Name: $(this).find('.each_tax_per').eq(g).attr('text'),
                                                            Tax_Amt: $(this).find('.each_tax_per').eq(g).attr('tax_value'),
                                                            Tax_Per: $(this).find('.each_tax_per').eq(g).attr('value') || 0,
                                                            umo_code: $(this).find('.unit option:selected').val() || 0
                                                        });
                                                    }

                                                    console.log(taxArr);
                                                    dayarr.push({
                                                        Trans_order_no: sno,
                                                        Product_code: Prd_code,
                                                        Product_name: Prd_name,
                                                        Price: price,
                                                        Rate_in_peice: Rate_in_peice,
                                                        Discount: dis,
                                                        Dis_v: dis_val,
                                                        Free: free,
                                                        Non_Cov_Quantity: Original_qty,
                                                        Cov_Quantity: Conversion_qty,
                                                        Order_quantity: Order_qty,
                                                        Amount: tot,
                                                        Over_Tot: over_all_tot,
                                                        Product_Unit: Ut,
                                                        Unit_Code: unt_code,
                                                        Offer_Product_Code: offer_code,
                                                        Offer_Product_Name: offer_name,
                                                        Offer_Product_Unit: offer_unit,
                                                        Tye: ty,
                                                        Sales_quantity: sales_qty,
                                                        Tax: tax_val || 0,
                                                        Erp_code: erp,
                                                        settig: settg,
                                                        Con_Fav: conversion_factor,
                                                    });
                                               // }
                                            });
                                             if (flagsalesqty > 0) {
											 approve=0;
                                                rjalert('Error!', 'Enter Sale Quantity!!.', 'error');
                                                $('.spinnner_div').hide();
                                                //alert('Stock is not available.');
                                                return false;
                                            }
                                            if (flagstk > 0) {
											approve=0;
                                                rjalert('Error!', 'Stock is not available!!.', 'error');
                                                $('.spinnner_div').hide();
                                                //alert('Stock is not available.');
                                                return false;
                                            }
                                           
                                            if (orderid != "" && orderid != "Direct") {

                                                var Add_order = [];
                                                var add_order_orderno = '';

                                                Add_order = dayarr.filter(function (a) {
                                                    return (a.Tye == 'Add');
                                                });

                                                if (Add_order.length > 0) {

                                                    if (chck == true) {

                                                        var Add_orderval = 0; var Add_tot_net_weight = 0; var add_new_order_array = []; var Add_taxvalue = 0; var Add_disValue = 0; var Add_subtotal = 0;

                                                        for (var t = 0; t < Add_order.length; t++) {

                                                            Add_subtotal = parseFloat(Add_order[t].Amount) + parseFloat(Add_subtotal);
                                                            Add_taxvalue = parseFloat(Add_order[t].Tax) + parseFloat(Add_taxvalue);
                                                            Add_disValue = parseFloat(Add_order[t].Dis_v) + parseFloat(Add_disValue);
                                                            Add_tot_net_weight = parseFloat(Add_order[t].Cov_Quantity) + parseFloat(Add_tot_net_weight);
                                                            Add_orderval = parseFloat(Add_subtotal) + parseFloat(Add_taxvalue);

                                                            add_new_order_array.push({

                                                                PCd: Add_order[t].Product_code,
                                                                PName: Add_order[t].Product_name,
                                                                Unit: Add_order[t].Product_Unit,
                                                                Rate: Add_order[t].Price,
                                                                Rate_in_peice: Add_order[t].Rate_in_peice,
                                                                Qty: Add_order[t].Cov_Quantity,
                                                                Qty_c: Add_order[t].Non_Cov_Quantity,
                                                                Sub_Total: Add_order[t].Amount,
                                                                Free: Add_order[t].Free,
                                                                Dis_value: Add_order[t].Dis_v,
                                                                Discount: Add_order[t].Discount,
                                                                of_Pro_Code: Add_order[t].Offer_Product_Code,
                                                                of_Pro_Name: Add_order[t].Offer_Product_Name,
                                                                of_Pro_Unit: Add_order[t].Offer_Product_Unit,
                                                                umo_unit: Add_order[t].Unit_Code,
                                                                Tax_value: Add_order[t].Tax,
                                                                con_fac: Add_order[t].Con_Fav

                                                            });

                                                            tax_filter = All_Tax.filter(function (r) {
                                                                return (r.Product_Detail_Code == add_new_order_array.PCd)
                                                            });
                                                        }

                                                        if (mainarr[0].TCS == "" || mainarr[0].TCS == 0 || mainarr[0].TCS == undefined || mainarr[0].TCS == '0.00') { var Extra_Tax_Type = 'TDS'; var Extra_Tax_Value = mainarr[0].TDS } else { var Extra_Tax_Type = 'TCS'; var Extra_Tax_Value = mainarr[0].TCS }
                                                        $('.spinnner_div').show();
                                                        $.ajax({
                                                            type: "POST",
                                                            contentType: "application/json; charset=utf-8",
                                                            async: false,
                                                            url: "myOrders.aspx/saveorders",
                                                            data: "{'NewOrd':'" + JSON.stringify(add_new_order_array) + "','NewOrd_Tax_Details':'" + JSON.stringify(taxArr) + "','Remark':'','Ordrval':'" + Add_orderval + "','RetCode':'" + Customer_Code + "','RecDate':'" + today1 + "','Ntwt':'" + Add_tot_net_weight + "','retnm':'" + Customer_Name + "','Type':'1','ref_order':'','sub_total':'" + Add_subtotal + "','dis_total':'" + Add_disValue + "','tax_total':'" + Add_taxvalue + "','Extra_Tax_type':'" + Extra_Tax_Type + "','Extra_Tax_value':'" + Extra_Tax_Value + "'}",
                                                            dataType: "json",
                                                            success: function (data) {
                                                                add_order_orderno = data.d;
                                                                if (add_order_orderno.length > 0) {
                                                                    // var Add_Order_ID = data.d;
                                                                    orderid = orderid + ',' + add_order_orderno;
                                                                    mainarr[0]["order_no"] = orderid;
                                                                }
                                                                else {
                                                                    rjalert('Error!', 'Order Not Saved!!.', 'error');
                                                                    //alert('Order Not Saved')
                                                                    approve = 0;
                                                                    $('.spinnner_div').hide();
                                                                    return false;
                                                                }
                                                            },
                                                            error: function (result) {
                                                                approve = 0;
                                                                alert(JSON.stringify(result));
                                                            }
                                                        });
                                                    }
                                                }

                                                var Pen_order_ids = []; var Pen_orderval = 0; var Pen_tot_net_weight = 0;
                                                var pen_new_order_array = []; var pending_qty = ''; var pen_id = '';
                                                var pen_tax_total = 0; var Pen_disValue = 0; var Pen_subtotal = 0; var pen_cgst_total = 0;
                                                var pen_sgst_total = 0; var pen_igst_total = 0; var pen_extra_tax_val = 0; var pen_tax_val = 0;

                                                Pen_order_ids = dayarr.filter(function (a) {
                                                    return (a.settig == 'yes');
                                                });

                                                for (var c = 0; Pen_order_ids.length > c; c++) {

                                                    if (pen_id.indexOf(Pen_order_ids[c].Trans_order_no) < 0) {
                                                        pen_id += ',' + Pen_order_ids[c].Trans_order_no;
                                                    }

                                                    var pen_qty = parseFloat(Pen_order_ids[c].Order_quantity) - parseFloat(Pen_order_ids[c].Sales_quantity);
                                                    var pen_pro_price = parseFloat(Pen_order_ids[c].Price);

                                                    var dis2 = parseFloat(Pen_order_ids[c].Discount);
                                                    var dis_tot = pen_pro_price * pen_qty * (dis2 / 100);

                                                    var cal2 = ((pen_pro_price * pen_qty) - ((pen_pro_price * pen_qty) * (dis2 / 100)));

                                                    pen_tax_val = Pen_order_ids[c].Tax;

                                                    var tot_Con_qty = parseFloat(Pen_order_ids[c].Erp_code) * pen_qty;

                                                    Pen_subtotal = parseFloat(cal2) + parseFloat(Pen_subtotal);
                                                    pen_tax_total = parseFloat(pen_tax_val) + parseFloat(pen_tax_total);
                                                    Pen_disValue = parseFloat(dis_tot) + parseFloat(Pen_disValue);
                                                    Pen_tot_net_weight = parseFloat(tot_Con_qty) + parseFloat(Pen_tot_net_weight);
                                                    if (pen_tax_total > 0)
                                                        Pen_orderval = parseFloat(Pen_subtotal) + parseFloat(pen_tax_total);
                                                    else
                                                        Pen_orderval = parseFloat(Pen_subtotal);

                                                    pen_new_order_array.push({

                                                        PCd: Pen_order_ids[c].Product_code,
                                                        PName: Pen_order_ids[c].Product_name,
                                                        Unit: Pen_order_ids[c].Product_Unit,
                                                        Unitcode: Pen_order_ids[c].Unit_Code,
                                                        Rate: Pen_order_ids[c].Price,
                                                        Rate_in_peice: Pen_order_ids[c].Rate_in_peice,
                                                        Qty: tot_Con_qty,
                                                        Qty_c: pen_qty,
                                                        Sub_Total: cal2,
                                                        Free: Pen_order_ids[c].Free,
                                                        Dis_value: dis_tot,
                                                        Discount: Pen_order_ids[c].Discount,
                                                        of_Pro_Code: Pen_order_ids[c].Offer_Product_Code,
                                                        of_Pro_Name: Pen_order_ids[c].Offer_Product_Name,
                                                        of_Pro_Unit: Pen_order_ids[c].Offer_Product_Unit,
                                                        Tax_value: pen_tax_val,
                                                        con_fac: Pen_order_ids[c].Con_Fav
                                                    });
                                                }

                                                if (Extra_Tax_Type == 'TCS') {

                                                    var Pen_extra_tax = Extra_Tax;
                                                    var Pen_Calc_extra_tax = Pen_extra_tax / 100 * Pen_subtotal;
                                                    var Pen_Calc_extra_total_tax = Pen_Calc_extra_tax + Pen_orderval;
                                                    pen_extra_tax_val = Math.round((Pen_Calc_extra_tax + Number.EPSILON) * 100 / 100);
                                                    //Pen_orderval = Math.round((Pen_Calc_extra_total_tax + Number.EPSILON) * 100 / 100);

                                                }
                                                else {
                                                    var Pen_extra_tax = Extra_Tax;
                                                    var Pen_Calc_extra_tax = Pen_extra_tax / 100 * Pen_subtotal;
                                                    var Pen_Calc_extra_total_tax = Pen_Calc_extra_tax + Pen_orderval;
                                                    pen_extra_tax_val = Math.round((Pen_Calc_extra_tax + Number.EPSILON) * 100 / 100);
                                                    //Pen_orderval = Math.round((Pen_Calc_extra_total_tax + Number.EPSILON) * 100 / 100);
                                                }

                                                if (Pen_order_ids.length > 0) {
                                                    $('.spinnner_div').show();
                                                    if (chck == true) {
                                                        dayarr = dayarr.filter(function (a) {
                                                            return (a.Tye != 'remove');
                                                        });
                                                        $.ajax({
                                                            type: "POST",
                                                            contentType: "application/json; charset=utf-8",
                                                            async: false,
                                                            url: "myOrders.aspx/PendingOrders",
                                                            data: "{'NewOrd':'" + JSON.stringify(pen_new_order_array) + "','Remark':'','Ordrval':'" + Pen_orderval + "','RetCode':'" + Customer_Code + "','RecDate':'" + today1 + "','Ntwt':'" + Pen_tot_net_weight + "','retnm':'" + Customer_Name + "','Type':'2','ref_order':'" + pen_id + "','sub_total':'" + Pen_subtotal + "','dis_total':'" + Pen_disValue + "','tax_total':'" + pen_tax_total + "','Extra_Tax_type':'" + Extra_Tax_Type + "','Extra_Tax_value':'" + pen_extra_tax_val + "'}",
                                                            dataType: "json",
                                                            success: function (data) {

                                                            },
                                                            error: function (result) {
                                                                $('.spinnner_div').hide();
                                                                approve = 0;
                                                                alert(JSON.stringify(result));
                                                            }
                                                        });
                                                    }
                                                }
                                                if (chck == true) {
                                                    $('.spinnner_div').show();
                                                    $.ajax({
                                                        type: "POST",
                                                        contentType: "application/json; charset=utf-8",
                                                        url: "Invoice_Entry_2.aspx/SaveInvoice",
                                                        data: "{'General_details':'" + JSON.stringify(mainarr) + "','Pro_details':'" + JSON.stringify(dayarr) + "','Tax_details':'" + JSON.stringify(taxArr) + "','new_order_no':'" + add_order_orderno + "'}",
                                                        dataType: "json",
                                                        success: function (data) {
                                                            var Invoice_no = data.d;
                                                            $('.example').hide();
                                                            $(".btnsave").attr("disabled", true);
                                                            //var Respomse = confirm('Do You Want To Print Invoice Order');
                                                            //if (Respomse)
                                                            //    popup(Invoice_no, stockistcode, div_code, ret)
                                                            //else
                                                            $.confirm({
                                                                title: 'success!',
                                                                content: 'Invoice Generated Successfully..!!',
                                                                type: 'green',
                                                                typeAnimated: true,
                                                                autoClose: 'cancel|8000',
                                                                icon: 'fa fa-check',
                                                                buttons: {
                                                                    tryAgain: {
                                                                        text: 'OK',
                                                                        btnClass: 'btn-green',
                                                                        action: function () {
                                                                            $(".btnsave").attr("disabled", false);
                                                                            window.location.href = "../Stockist/Invoice_Order_List.aspx"
                                                                        }
                                                                    }
                                                                }
                                                            });
                                                            //alert('Invoice Generated Successfully')
                                                            //window.location.href = "../Stockist/Invoice_Order_List.aspx";
                                                            //if (Respomse) { window.location.href = "../Stockist/GstInvoice.aspx?Order_id=" + Invoice_no + "&Stockist_Code=" + stockistcode + "&Div_Code=" + div_code + "&Cust_Code=" + ret + ""; } else { window.location.href = "../Stockist/Invoice_Order_List.aspx"; }

                                                        },
                                                        error: function (data) {
                                                            approve = 0;
                                                            alert(JSON.stringify(data));
                                                        }
                                                    });
                                                }
                                            }
                                            else {
                                                rjalert('Error!', 'Please Select Order Id!..', 'error');
                                                //alert("Please Select Order Id ");
                                                $("#example-multiple-selected").trigger('chosen:open');
                                                approve = 0;
                                                $('.spinnner_div').hide();
                                                return false;
                                            }

                                            //}
                                            //else {
                                            //    alert("Your Outstanding Amount is High");
                                            //    approve = 0;
                                            //    return false;
                                            //}
                                        }
                                        else {
                                            confirm('your largest credit limit is low');
                                            $('.spinnner_div').hide();
                                            approve = 0;
                                        }
                                    },
                                    error: function (data) {
                                        alert(JSON.stringify(data));
                                    }
                                });
                            }, 30);
                        }
                        else {
                            approve = 0;
                            $('.spinnner_div').hide();
                            rjalert('Error!', 'Please Correct the Stock and Save!..', 'error');
                            //alert("Please Correct the Stock and Save");
                            return false;
                        }
                    }
                    else {
                        rjalert('Attention!', 'Please wait we are processing invoice!..', 'error');
                    }
                    $('.spinnner_div').hide();
                }
                var StkDetail = [];
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Invoice_Entry_2.aspx/GetCurrStock",
                    dataType: "json",
                    success: function (data) {
                        StkDetail = data.d;
                        $('#tblCustomers  >tbody > tr').each(function () {

                            for (var i = 0; i < data.d.length; i++) {
                                if ($(this).find('[id*=Hid_Pro_code]').val() == data.d[i].Prod_Code) {
                                    $(this).find("input[name=stkval]").attr('stkgood', data.d[i].GStock);
                                    //$(this).find("input[name=stkval]").attr('stkdamage', data.d[i].DStock);
                                    if (Number($(this).find('[id*=english]').val() || 0) > Number(data.d[i].GStock)) {
                                        console.log('low' + data.d[i].GStock);
                                    }
                                }

                            }
                        });
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });


                $.ajax({
                    type: "Post",
                    contentType: "application/json; charset=utf-8",
                    url: "Invoice_Entry_2.aspx/getscheme",
                    data: "{'date':'" + today + "','Div_Code':'" + div_code + "','Stockist_Code':'" + stockistcode + "'}",
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

                function qty_fun(row) {

                    idx = $(row).index();
                    un = row.find('.unit option:selected').text();
                    //  un = row.find('.unit option:selected').val();
                    var CQ = row.find('[id*=english]').val();
                    opcode = row.find('#Hid_Pro_code').val();
                    var CQvalue = '';

                    var out = [];

                    out = Allrate.filter(function (t) {
                        return (t.Product_Detail_Code == opcode);
                    });

                    if (out.length > 0) {

                        if (out[0].product_unit == un) {

                            CQvalue = CQ * parseFloat(row.find(".erp_code").val());
                        }
                        else {
                            CQvalue = CQ;
                            CQ = CQ / parseFloat(row.find(".erp_code").val());
                        }
                    }

                    pCode = row.find('#pro_sale_code').val();

                    pname = row.find(".autoc").attr('t');
                    ff = row.find(".fre1").text();

                    //getscheme(pCode, CQ, CQvalue, un, row, ff, opcode, idx, pname);
                }


                function getscheme(pCode, cq, cqvalue, un, tr, ff, opcode, index, pname) {

                    var rer = [];

                    var rer = scheme.filter(function (y) {
                        return (y.Sale_Erp_Code == pCode)

                    });
                    var res = rer.filter(function (a) {
                        return ((Number(cqvalue) >= Number(a.erp_val)))
                    });
                    //&& a.Scheme_Unit == un
                    ans = [];
                    if (res.length > 0) {

                        if (res[0].Offer_Product == 0) {

                            ans = Allrate.filter(function (t) {
                                return (t.Product_Detail_Code == opcode);
                            });
                        }

                        else {
                            ans = Allrate.filter(function (t) {
                                return (t.Product_Detail_Code == res[0].Offer_Product);

                            });
                        }
                    }

                    $(tr).find('.tddis').text(0);
                    $(tr).find('input[name="disc_value"]').val(0);

                    var hfree = '';

                    if ($(tr).find('.fre').attr('cqty') > 0) {

                        qqq = Allrate.filter(function (t) {
                            return (t.Product_Detail_Code == $(tr).find('.fre').attr('freepro'));
                        });

                        for (var c = 0; c < arr.length; c++) {
                            if (qqq.length > 0) {
                                if ($(tr).find('.fre').attr('unit') == qqq[0].product_unit) {
                                    if (qqq[0].product_unit == arr[c].C_fre_uni) {
                                        if (arr[c].pppcode.indexOf(opcode) >= 0 && arr[c].units == $(tr).find('.fre').attr('unit')) {

                                            arr[c].Free = arr[c].Free - ff;
                                            $(tr).find('#Free').val('');
                                            $(tr).find('#Dis').val('');
                                            //  arr[c].C_join = arr[c].Free + qqq[0].product_unit;
                                            $(tr).find('.fre').text('#' + opcode).text('0');
                                            $(tr).find('.fre1').text('#' + opcode).text('0');
                                            $(tr).find('.fre').attr('unit', '');
                                            tr.find('.of_pro_name').val(0);
                                            tr.find('.of_pro_code').val(0);
                                            $(tr).find('.disc_value').val($(tr).find('.disc_value').val() - $(tr).find('.disc_value').val());
                                        }
                                    }
                                }
                            }
                        }

                        for (var c = 0; c < arr.length; c++) {
                            if (qqq.length > 0) {
                                if ($(tr).find('.fre').attr('unit') == qqq[0].Product_Sale_Unit) {
                                    if (qqq[0].Product_Sale_Unit == arr[c].P_fre_uni) {
                                        if (arr[c].pppcode.indexOf(opcode) >= 0 && arr[c].units == $(tr).find('.fre').attr('unit')) {


                                            arr[c].P_Free = arr[c].P_Free - ff;
                                            $(tr).find('#Free').val('');
                                            $(tr).find('#Dis').val('');
                                            // arr[c].P_join = arr[c].Free + qqq[0].Product_Sale_Unit;
                                            $(tr).find('.fre').text('#' + opcode).text('0');
                                            $(tr).find('.fre1').text('#' + opcode).text('0');
                                            $(tr).find('.fre').attr('unit', '');
                                            tr.find('.of_pro_name').val(0);
                                            tr.find('.of_pro_code').val(0);
                                            $(tr).find('.disc_value').val($(tr).find('.disc_value').val() - $(tr).find('.disc_value').val());
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (res.length > 0) {

                        schemedefinewithoutpackage(res[0], tr, ff, ans, index, cq, cqvalue, pname);
                    }

                    bind_free(arr);
                    // CalcGTot();
                    //var l = 0;
                    //$('#free_table').find('tbody tr').remove();
                    //for (var r = 0; r < arr.length; r++) {
                    //    if (arr[r].Free != '0' || arr[r].P_Free != '0') {
                    //        var l = l + 1;
                    //        if (arr[r].Free != '0' && arr[r].P_Free != '0') {
                    //            var str = "<tr><td style='width: 14%;' class='td_id'>" + l + "</td><td style='width:21%;'><input type='hidden' value=" + arr[r].Product_Code + " id='apc'/>" + arr[r].Product_Code + "</td><td style='left: 201px;width: 38%;' class='td_name'><input type='hidden' value=" + arr[r].Product_Name + " id='apn'/>" + arr[r].Product_Name + "</td><td style='left: 454px;' class='td_free'><input type='hidden' value=" + arr[r].Free + " id='fr'/>" + (arr[r].Free + ' ' + arr[r].C_join) + "</td><td style='display:none'><input type='hidden' id='opc' value=" + arr[r].Product_Code1 + " />" + arr[r].Product_sale_Code + "</td><td>" + (arr[r].P_Free + ' ' + arr[r].P_join) + "</td></tr>";
                    //            $('#free_table tbody').append(str);
                    //        }

                    //        if (arr[r].Free != '0' && arr[r].P_Free == '0') {
                    //            var str = "<tr><td style='width: 14%;' class='td_id'>" + l + "</td><td style='width:21%;'><input type='hidden' value=" + arr[r].Product_Code + " id='apc'/>" + arr[r].Product_Code + "</td><td style='left: 201px;width: 38%;' class='td_name'><input type='hidden' value=" + arr[r].Product_Name + " id='apn'/>" + arr[r].Product_Name + "</td><td style='left: 454px;' class='td_free'><input type='hidden' value=" + arr[r].Free + " id='fr'/>" + (arr[r].Free + ' ' + arr[r].C_join) + "</td><td style='display:none'><input type='hidden' id='opc' value=" + arr[r].Product_Code1 + " />" + arr[r].Product_sale_Code + "</td></tr>";
                    //            $('#free_table tbody').append(str);
                    //            //<td>" + (arr[r].P_Free + ' ' + arr[r].P_join) + "</td>
                    //        }

                    //        if (arr[r].P_Free != '0' && arr[r].Free == '0') {
                    //            var str = "<tr><td style='width: 14%;' class='td_id'>" + l + "</td><td style='width:21%;'><input type='hidden' value=" + arr[r].Product_Code + " id='apc'/>" + arr[r].Product_Code + "</td><td style='left: 201px;width: 38%;' class='td_name'><input type='hidden' value=" + arr[r].Product_Name + " id='apn'/>" + arr[r].Product_Name + "</td><td style='display:none'><input type='hidden' id='opc' value=" + arr[r].Product_Code1 + " />" + arr[r].Product_sale_Code + "</td><td>" + (arr[r].P_Free + ' ' + arr[r].P_join) + "</td></tr>";
                    //            $('#free_table tbody').append(str);
                    //            //<td style='left: 454px;' class='td_free'><input type='hidden' value=" + arr[r].Free + " id='fr'/>" + (arr[r].Free + ' ' + arr[r].C_join) + "</td>
                    //        }

                    //    }
                    //}
                }

                function schemedefinewithoutpackage(res, tr, ff, ans, index, CQ, cqvalue, pname) {

                    if (res.Discount == '0') {

                        if (res.Package != "Y") {

                            if (CQ > 0) {

                                if (res.Scheme_Unit == ans[0].product_unit) {

                                    var free = parseInt(parseFloat((CQ / parseInt(res.Scheme))) * parseInt(res.Free))
                                }
                                else {
                                    var free = parseInt(parseFloat((CQ / parseInt(res.Scheme))) * parseInt(res.Free))
                                }

                                if (res.Scheme_Unit == ans[0].Product_Sale_Unit) {

                                    var free = parseInt(parseFloat((cqvalue / parseInt(res.Scheme))) * parseInt(res.Free))
                                }
                                if (res.Offer_Product != 0) {
                                    var x = arr.filter(function (b) {
                                        return (b.Product_Code == res.Offer_Product)
                                    })
                                }
                                else {
                                    var x = arr.filter(function (b) {
                                        return (b.Product_Code == res.Product_Code)
                                    })
                                }
                                if (res.Against == "N") {

                                    if (ans[0].product_unit == res.Free_Unit) {
                                        if (x.length > 0) {
                                            idx = arr.indexOf(x[0]);
                                            arr[idx].Free += free;
                                            arr[idx].C_fre_uni = res.Free_Unit;
                                            arr[idx].P_join = ans[0].Product_Sale_Unit;
                                            arr[idx].units = res.Free_Unit;
                                            arr[idx].pppcode = (arr[idx].pppcode.indexOf(opcode) >= 0) ? arr[idx].pppcode : arr[idx].pppcode + ',' + opcode;
                                            $(tr).find('.fre').text(free + '' + (ans[0].product_unit));
                                            $(tr).find('.fre').attr('unit', ans[0].product_unit);
                                            $(tr).find('.fre').attr('freepro', res.Product_Code);
                                            $(tr).find('.fre').attr('cqty', CQ);
                                            $(tr).find('.fre').attr('freecqty', free);
                                            $(tr).find('.fre1').text(free);
                                            $(tr).find('#Free').val(free);

                                            if (free != "") {
                                                tr.find('.of_pro_name').val(pname);
                                                tr.find('.of_pro_code').val(res.Product_Code);
                                            }
                                        }
                                        else {
                                            $(tr).find('.fre').text(free + ' ' + ((ans[0].product_unit)));
                                            $(tr).find('.fre').attr('unit', ans[0].product_unit);
                                            $(tr).find('.fre').attr('freepro', res.Product_Code);
                                            $(tr).find('.fre').attr('cqty', CQ);
                                            $(tr).find('.fre').attr('freecqty', free);
                                            $(tr).find('.fre1').text(free);
                                            $(tr).find('#Free').val(free);

                                            if (free != "") {
                                                tr.find('.of_pro_name').val(pname);
                                                tr.find('.of_pro_code').val(res.Product_Code);
                                            }
                                            arr.push({
                                                Product_Code: res.Product_Code,
                                                Product_Name: pname,
                                                pppcode: res.Product_Code,
                                                pppname: res.Product_Name,
                                                Free: free,
                                                P_Free: 0,
                                                C_fre_uni: res.Free_Unit,
                                                P_fre_uni: 0,
                                                C_join: ans[0].product_unit,
                                                P_join: ans[0].Product_Sale_Unit,
                                                units: res.Free_Unit

                                            })
                                        }
                                    }
                                    else {
                                        if (x.length > 0) {
                                            idx = arr.indexOf(x[0]);
                                            arr[idx].P_fre_uni = res.Free_Unit;
                                            arr[idx].P_Free += free;
                                            arr[idx].pppcode = (arr[idx].pppcode.indexOf(opcode) >= 0) ? arr[idx].pppcode : arr[idx].pppcode + ',' + opcode;
                                            arr[idx].C_join = ans[0].product_unit;

                                            arr[idx].units = res.Free_Unit;

                                            $(tr).find('.fre').text(free + ' ' + (ans[0].Product_Sale_Unit));
                                            $(tr).find('.fre').attr('unit', ans[0].Product_Sale_Unit);
                                            $(tr).find('.fre').attr('freepro', res.Product_Code);
                                            $(tr).find('.fre').attr('cqty', CQ);
                                            $(tr).find('.fre').attr('freecqty', free);
                                            $(tr).find('.fre1').text(free);
                                            $(tr).find('#Free').val(free);
                                            if (free != "") {
                                                tr.find('.of_pro_name').val(pname);
                                                tr.find('.of_pro_code').val(res.Product_Code);
                                            }
                                        }
                                        else {
                                            $(tr).find('.fre').text(free + ' ' + (ans[0].Product_Sale_Unit));
                                            $(tr).find('.fre').attr('unit', ans[0].Product_Sale_Unit);
                                            $(tr).find('.fre').attr('unit', ans[0].Product_Sale_Unit);
                                            $(tr).find('.fre').attr('freepro', res.Product_Code);
                                            $(tr).find('.fre').attr('cqty', CQ);
                                            $(tr).find('.fre').attr('freecqty', free);
                                            $(tr).find('.fre1').text(free);
                                            $(tr).find('#Free').val(free);

                                            if (free != "") {
                                                tr.find('.of_pro_name').val(pname);
                                                tr.find('.of_pro_code').val(res.Product_Code);
                                            }
                                            arr.push({
                                                Product_Code: res.Product_Code,
                                                Product_Name: pname,
                                                pppcode: res.Product_Code,
                                                pppname: res.Product_Name,
                                                Free: 0,
                                                P_Free: free,
                                                C_fre_uni: 0,
                                                P_fre_uni: res.Free_Unit,
                                                P_join: ans[0].Product_Sale_Unit,
                                                C_join: ans[0].product_unit,
                                                units: res.Free_Unit


                                            })
                                        }
                                    }
                                }
                                else {

                                    if (ans[0].product_unit == res.offer_product_unit) {
                                        if (parseInt(CQ) >= parseInt(res.Scheme)) {

                                            if (x.length > 0) {
                                                idx = arr.indexOf(x[0]);
                                                arr[idx].C_fre_uni = res.offer_product_unit;
                                                arr[idx].pppcode = (arr[idx].pppcode.indexOf(opcode) >= 0) ? arr[idx].pppcode : arr[idx].pppcode + ',' + opcode;
                                                arr[idx].Free += free;
                                                arr[idx].P_join = ans[0].Product_Sale_Unit;
                                                arr[idx].units = res.offer_product_unit;
                                                $(tr).find('.fre').text(free + ' ' + (ans[0].product_unit));
                                                $(tr).find('.fre').attr('unit', ans[0].product_unit);
                                                $(tr).find('.fre').attr('freepro', res.Offer_Product);
                                                $(tr).find('.fre').attr('cqty', CQ);
                                                $(tr).find('.fre').attr('freecqty', free);
                                                $(tr).find('.fre1').text(free);
                                                $(tr).find('#Free').val(free);
                                                tr.find('.of_pro_name').val(res.Offer_Product_Name);
                                                tr.find('.of_pro_code').val(res.Offer_Product);

                                            }
                                            else {
                                                $(tr).find('.fre').text(free + ' ' + ((ans[0].product_unit)));
                                                $(tr).find('.fre').attr('unit', ans[0].product_unit);
                                                $(tr).find('.fre').attr('freepro', res.Offer_Product);
                                                $(tr).find('.fre').attr('cqty', CQ);
                                                $(tr).find('.fre').attr('freecqty', free);
                                                $(tr).find('.fre1').text(free);
                                                $(tr).find('#Free').val(free);
                                                tr.find('.of_pro_name').val(res.Offer_Product_Name);
                                                tr.find('.of_pro_code').val(res.Offer_Product);
                                                arr.push({

                                                    Product_Code: res.Offer_Product,
                                                    Product_Name: res.Offer_Product_Name,
                                                    pppcode: res.Product_Code,
                                                    pppname: res.Product_Name,
                                                    Free: free,
                                                    P_Free: 0,
                                                    C_fre_uni: res.offer_product_unit,
                                                    P_fre_uni: 0,
                                                    C_join: ans[0].product_unit,
                                                    P_join: ans[0].Product_Sale_Unit,
                                                    units: res.offer_product_unit

                                                })
                                            }
                                        }
                                    }
                                    else {
                                        if (x.length > 0) {
                                            idx = arr.indexOf(x[0]);
                                            arr[idx].P_fre_uni = res.offer_product_unit;
                                            arr[idx].pppcode = (arr[idx].pppcode.indexOf(opcode) >= 0) ? arr[idx].pppcode : arr[idx].pppcode + ',' + opcode;
                                            arr[idx].P_Free += free;
                                            arr[idx].C_join = ans[0].product_unit;
                                            arr[idx].units = res.offer_product_unit;

                                            $(tr).find('.fre').text(free + ' ' + (ans[0].Product_Sale_Unit));
                                            $(tr).find('.fre').attr('unit', ans[0].Product_Sale_Unit);
                                            $(tr).find('.fre').attr('freepro', res.Offer_Product);
                                            $(tr).find('.fre').attr('cqty', CQ);
                                            $(tr).find('.fre').attr('freecqty', free);
                                            $(tr).find('.fre1').text(free);
                                            $(tr).find('#Free').val(free);
                                            tr.find('.of_pro_name').val(res.Offer_Product_Name);
                                            tr.find('.of_pro_code').val(res.Offer_Product);

                                        }
                                        else {
                                            $(tr).find('.fre').text(free + ' ' + ((ans[0].Product_Sale_Unit)));
                                            $(tr).find('.fre').attr('unit', ans[0].Product_Sale_Unit);
                                            $(tr).find('.fre').attr('freepro', res.Offer_Product);
                                            $(tr).find('.fre').attr('cqty', CQ);
                                            $(tr).find('.fre').attr('freecqty', free);
                                            $(tr).find('.fre1').text(free);
                                            $(tr).find('#Free').val(free);
                                            tr.find('.of_pro_name').val(res.Offer_Product_Name);
                                            tr.find('.of_pro_code').val(res.Offer_Product);
                                            arr.push({
                                                Product_Code: res.Offer_Product,
                                                Product_Name: res.Offer_Product_Name,
                                                pppcode: res.Product_Code,
                                                pppname: res.Product_Name,
                                                Free: 0,
                                                P_Free: free,
                                                C_fre_uni: 0,
                                                P_fre_uni: res.offer_product_unit,
                                                P_join: ans[0].Product_Sale_Unit,
                                                C_join: ans[0].product_unit,
                                                units: res.offer_product_unit

                                            })
                                        }
                                    }
                                }
                            }
                            else {

                                var z = arr.filter(function (b) {
                                    return (b.Product_sale_Code == pCode)
                                })

                                if (z.length > 0) {
                                    idx = arr.indexOf(z[0]);
                                    arr.splice(idx, 1);

                                }
                                $(tr).find('.dis').text(0);
                            }
                        }
                        else {

                            var TotalQuantity = CQ / res.Scheme;
                            var free = parseInt(TotalQuantity) * res.Free;

                            if (res.Offer_Product != 0) {
                                var x = arr.filter(function (b) {
                                    return (b.Product_Code == res.Offer_Product)
                                })
                            }
                            else {
                                var x = arr.filter(function (b) {
                                    return (b.Product_Code == res.Product_Code)
                                })
                            }

                            if (res.Against == "N") {

                                if (ans[0].product_unit == res.Free_Unit) {

                                    if (x.length > 0) {
                                        idx = arr.indexOf(x[0]);
                                        arr[idx].Free += free;
                                        arr[idx].pppcode = (arr[idx].pppcode.indexOf(opcode) >= 0) ? arr[idx].pppcode : arr[idx].pppcode + ',' + opcode;
                                        arr[idx].C_fre_uni = res.Free_Unit;
                                        arr[idx].P_join = ans[0].Product_Sale_Unit;
                                        arr[idx].units = res.Free_Uni;

                                        $(tr).find('.fre').text(free + ' ' + (ans[0].product_unit));
                                        $(tr).find('.fre').attr('unit', ans[0].product_unit);
                                        $(tr).find('.fre').attr('freepro', res.Product_Code);
                                        $(tr).find('.fre').attr('cqty', CQ);
                                        $(tr).find('.fre').attr('freecqty', free);
                                        $(tr).find('#Free').val(free);

                                        $(tr).find('.fre1').text(free);
                                        if (free != "") {
                                            tr.find('.of_pro_name').val(pname);
                                            tr.find('.of_pro_code').val(res.Product_Code);
                                        }
                                    }
                                    else {
                                        $(tr).find('.fre').text(free + ' ' + ((ans[0].product_unit)));
                                        $(tr).find('.fre').attr('unit', ans[0].product_unit);
                                        $(tr).find('.fre').attr('freepro', res.Product_Code);
                                        $(tr).find('.fre').attr('cqty', CQ);
                                        $(tr).find('.fre').attr('freecqty', free);
                                        $(tr).find('.fre1').text(free);
                                        $(tr).find('#Free').val(free);
                                        if (free != "") {
                                            tr.find('.of_pro_name').val(pname);
                                            tr.find('.of_pro_code').val(res.Product_Code);
                                        }

                                        arr.push({
                                            Product_Code: res.Product_Code,
                                            Product_Name: pname,
                                            pppcode: res.Product_Code,
                                            pppname: res.Product_Name,
                                            Free: free,
                                            P_Free: 0,
                                            C_fre_uni: res.Free_Unit,
                                            P_fre_uni: 0,
                                            C_join: ans[0].product_unit,
                                            P_join: ans[0].Product_Sale_Unit,
                                            units: res.Free_Unit
                                        })
                                    }
                                }
                                else {
                                    if (x.length > 0) {
                                        idx = arr.indexOf(x[0]);
                                        arr[idx].P_Free += free;
                                        arr[idx].pppcode = (arr[idx].pppcode.indexOf(opcode) >= 0) ? arr[idx].pppcode : arr[idx].pppcode + ',' + opcode;
                                        arr[idx].P_fre_uni = res.Free_Unit;
                                        arr[idx].C_join = ans[0].product_unit;
                                        arr[idx].units = res.Free_Unit;
                                        $(tr).find('.fre').text(free + ' ' + (ans[0].Product_Sale_Unit));
                                        $(tr).find('.fre').attr('freepro', res.Product_Code);
                                        $(tr).find('.fre').attr('cqty', CQ);
                                        $(tr).find('.fre').attr('freecqty', free);
                                        $(tr).find('.fre1').text(free);
                                        $(tr).find('#Free').val(free);
                                        if (free != "") {
                                            tr.find('.of_pro_name').val(pname);
                                            tr.find('.of_pro_code').val(res.Product_Code);
                                        }
                                    }
                                    else {
                                        $(tr).find('.fre').text(free + ' ' + ((ans[0].Product_Sale_Unit)));
                                        $(tr).find('.fre').attr('unit', ans[0].Product_Sale_Unit);
                                        $(tr).find('.fre').attr('freepro', res.Product_Code);
                                        $(tr).find('.fre').attr('cqty', CQ);
                                        $(tr).find('.fre').attr('freecqty', free);
                                        $(tr).find('.fre1').text(free);
                                        $(tr).find('#Free').val(free);
                                        if (free != "") {
                                            tr.find('.of_pro_name').val(pname);
                                            tr.find('.of_pro_code').val(res.Product_Code);
                                        }

                                        arr.push({
                                            Product_Code: res.Product_Code,
                                            Product_Name: pname,
                                            pppcode: res.Product_Code,
                                            pppname: res.Product_Name,
                                            Free: 0,
                                            P_Free: free,
                                            C_fre_uni: 0,
                                            P_fre_uni: res.Free_Unit,
                                            P_join: ans[0].Product_Sale_Unit,
                                            C_join: ans[0].product_unit,
                                            units: res.Free_Unit
                                        })
                                    }
                                }
                            }
                            else {
                                if (ans[0].product_unit == res.offer_product_unit) {
                                    if (x.length > 0) {
                                        idx = arr.indexOf(x[0]);
                                        arr[idx].C_fre_uni = res.offer_product_unit;
                                        arr[idx].pppcode = (arr[idx].pppcode.indexOf(opcode) >= 0) ? arr[idx].pppcode : arr[idx].pppcode + ',' + opcode;
                                        arr[idx].Free += free;
                                        arr[idx].P_join = ans[0].Product_Sale_Unit;
                                        arr[idx].units = res.offer_product_unit;
                                        $(tr).find('.fre').text(free + ' ' + (ans[0].product_unit));
                                        $(tr).find('.fre').attr('unit', ans[0].product_unit);
                                        $(tr).find('.fre').attr('freepro', res.Offer_Product);
                                        $(tr).find('.fre').attr('cqty', CQ);
                                        $(tr).find('.fre').attr('freecqty', free);
                                        $(tr).find('.fre1').text(free);
                                        $(tr).find('#Free').val(free);
                                        tr.find('.of_pro_name').val(res.Offer_Product_Name);
                                        tr.find('.of_pro_code').val(res.Offer_Product);
                                    }
                                    else {
                                        $(tr).find('.fre').text(free + ' ' + ((ans[0].product_unit)));
                                        $(tr).find('.fre').attr('unit', ans[0].product_unit);
                                        $(tr).find('.fre').attr('freepro', res.Offer_Product);
                                        $(tr).find('.fre').attr('cqty', CQ);
                                        $(tr).find('.fre').attr('freecqty', free);
                                        $(tr).find('.fre1').text(free);
                                        $(tr).find('#Free').val(free);
                                        tr.find('.of_pro_name').val(res.Offer_Product_Name);
                                        tr.find('.of_pro_code').val(res.Offer_Product);

                                        arr.push({
                                            Product_Code: res.Offer_Product,
                                            Product_Name: res.Offer_Product_Name,
                                            pppcode: res.Product_Code,
                                            pppname: res.Product_Name,
                                            C_fre_uni: res.offer_product_unit,
                                            P_fre_uni: 0,
                                            Free: free,
                                            P_Free: 0,
                                            C_join: ans[0].product_unit,
                                            P_join: ans[0].Product_Sale_Unit,
                                            units: res.offer_product_unit
                                        })
                                    }
                                } else {
                                    if (x.length > 0) {
                                        idx = arr.indexOf(x[0]);
                                        arr[idx].P_fre_uni = res.offer_product_unit;
                                        arr[idx].pppcode = (arr[idx].pppcode.indexOf(opcode) >= 0) ? arr[idx].pppcode : arr[idx].pppcode + ',' + opcode;
                                        arr[idx].P_Free += free;
                                        arr[idx].C_join = ans[0].product_unit;
                                        arr[idx].units = res.offer_product_unit;

                                        $(tr).find('.fre').text(free + ' ' + (ans[0].Product_Sale_Unit));
                                        $(tr).find('.fre').attr('unit', ans[0].Product_Sale_Unit);
                                        $(tr).find('.fre').attr('freepro', res.Offer_Product);
                                        $(tr).find('.fre').attr('cqty', CQ);
                                        $(tr).find('.fre').attr('freecqty', free);
                                        $(tr).find('.fre1').text(free);
                                        $(tr).find('#Free').val(free);
                                        tr.find('.of_pro_name').val(res.Offer_Product_Name);
                                        tr.find('.of_pro_code').val(res.Offer_Product);
                                    }
                                    else {
                                        $(tr).find('.fre').text(free + ' ' + ((ans[0].Product_Sale_Unit)));
                                        $(tr).find('.fre').attr('unit', ans[0].Product_Sale_Unit);
                                        $(tr).find('.fre').attr('freepro', res.Offer_Product);
                                        $(tr).find('.fre').attr('cqty', CQ);
                                        $(tr).find('.fre').attr('freecqty', free);
                                        $(tr).find('.fre1').text(free);
                                        $(tr).find('#Free').val(free);

                                        tr.find('.of_pro_name').val(res.Offer_Product_Name);
                                        tr.find('.of_pro_code').val(res.Offer_Product);
                                        arr.push({
                                            Product_Code: res.Offer_Product,
                                            Product_Name: res.Offer_Product_Name,
                                            pppcode: res.Product_Code,
                                            pppname: res.Product_Name,
                                            Free: 0,
                                            P_Free: free,
                                            C_fre_uni: 0,
                                            P_fre_uni: res.offer_product_unit,
                                            P_join: ans[0].Product_Sale_Unit,
                                            C_join: ans[0].product_unit,
                                            units: res.offer_product_unit
                                        })
                                    }
                                }
                            }
                        }
                    }

                    $(tr).find('#Dis').val(res.Discount);
                    var d = $(tr).find('#Price_').val() * $(tr).find('#english').val();
                    var discalc = parseInt(res.Discount) / 100;
                    var distotal = d * discalc;
                    var finaltotal = d - distotal;
                    $(tr).find('#hid_dis').text(distotal.toFixed(2));
                }
            });

            $("#tblCustomers >tbody >tr").each(function () {
                var row = $(this).closest("tr");
                var val = row.find('input[name=stkval]').attr('stkgood');
                var qty = row.find('[id*=english]').val();
                console.log(val, qty);
                if (Number(val) >= qty) {
                    $("#btndaysave").show();
                    $("#stk_chk").hide();
                }
                else {
                    $("#btndaysave").show();
                    $("#btnprint").show();
                    $("#stk_chk").hide();
                }
            });
            //print function 
            function popup(order_id, stk, div, cust_code) {

                PrintData(order_id, stk, div, cust_code);
                //}
                //else {
                //    var type = 1;
                //    slipfun(order_id, type);
                //    $('#divid').hide();
                //}
            }

            function PrintData(order_id, stk, div, cust_code) {

                $('#div_Print').html('');
                netval = 0; cgst = 0; sgst = 0; totlcase = 0; totalpie = 0; cashdis = 0.00; grossvalue = 0; totalGst = 0.00; roundoff = 0.00; netvalue = 0.00;
                rndoff = 0.00; netwrd = ''; netwrd1 = ''; netwrd2 = ''; cntu = 10; pagenumber = 1; amt_without_tax = 0;
                loadDataPrint(order_id, stk, div, cust_code);
                print();
            }

            function loadDataPrint(order_id, stk, div, cust_code) {

                $.ajax({
                    type: "Post",
                    contentType: "application/json; charset=utf-8",
                    url: "Invoice_Order_List.aspx/GetProductdetails",
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

                $('#div').empty(); var tot_gross = 0; var rowcount = 0; pgNo = 1;
                Multipage = '<div class="fullpage" style="padding-right: 10px;padding-top:40px;background-color:#f1f2f7;"><page size="A4" layout="portrait" class="Printarea" ><div  class="page" style="font-family: "Times New Roman", Times, serif;page-break-before:always;">';
                singlepg = '<div class="fullpage" style="padding-right: 10px;padding-top:40px;background-color:#f1f2f7;"><div  class="page" style="font-family: "Times New Roman", Times, serif;background-color:#f1f2f7;">';

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
                            var amtbyCase = 0.00;

                            var tax = Orders[$i].Tax / 2;
                            var qty_in_case = Orders[$i].Quantity / Orders[$i].Sample_Erp_Code

                            var amount = Orders[$i].Amount;
                            var discnt = Orders[$i].Discount;
                            var cgstv = tax;
                            var sgstv = tax;

                            //   var grossamnt = amount + Orders[$i].Tax;
                            // if (Orders[$i].qty == 0) {
                            //   amtbyCase = grossamnt;
                            //} else {
                            //  amtbyCase = grossamnt / Orders[$i].qty;
                            //}                  
                            rowcount = rowcount + 1;

                            str += "<tr><td class='Sh' align='center' style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'><b>" + ($i + 1) + "</b></td>";
                            str += "<td class='Sh' align='left'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'><b>" + Orders[$i].Product_Name + "</b></td>";
                            str += "<td class='Sh' align='center' style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + Orders[$i].HSN_Code + "</td>";
                            str += "<td class='Sh' align='right' style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + qty_in_case + "</td>";
                            str += "<td class='Sh' align='right' style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + Orders[$i].Quantity + "</td>";
                            /* str += "<td class='Sh' align='right'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + Orders[$i].MRP_Price + "</td>";*/
                            str += "<td class='Sh' align='right'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + Orders[$i].Rate + "</td>";
                            str += "<td class='Sh' align='right' style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + Orders[$i].Free + "</td>";
                            str += "<td class='Sh' align='right'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + discnt.toFixed(2) + "</td>";
                            str += "<td class='Sh' align='right'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + cgstv.toFixed(2) + "</td>";
                            str += "<td class='Sh' align='right'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + sgstv.toFixed(2) + "</td>";
                            str += "<td class='Sh' align='right'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + (amount + Orders[$i].Tax).toFixed(2) + "</td>";
                            str += "<td class='Sh' align='right'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;display:none;'>" + (amount + Orders[$i].Tax).toFixed(2) + "</td>";
                            str += "<td class='Sh' align='right'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + (amount + Orders[$i].Tax).toFixed(2) + "</td></tr>";

                            cashdis += Orders[$i].Discount;
                            totlcase += qty_in_case;
                            totalpie += Orders[$i].Quantity;
                            cgst += cgstv;
                            sgst += sgstv;
                            grossvalue += (Orders[$i].Amount + Orders[$i].Tax);
                            /*amt_without_tax += (Orders[$i].Amount - Orders[$i].Tax);*/
                            /* amt_without_tax += (Orders[$i].Amount - Orders[$i].Tax);*/
                            amt_without_tax += (grossvalue - Orders[$i].Tax);
                            totalGst += Orders[$i].Tax;
                        }
                    }
                    pagenumber = pgNo;
                    if (Orders.length > PgRecords_print) {
                        str += "<tr><td class='Sh' align='right' colspan='13' style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'><b>Continue</b></td></tr>";
                        str += '</tbody></table><table class="pageno"><tr style="font:12px TimesNewRomen;"><td colspan="8" style="padding-left:350px;float:right">' + pagenumber + '</td><td colspan="3" style="float:right"></td></tr></table></div><div style="break-after:page"></div>';
                        str += '</div></page></div>';
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
                    url: "Invoice_Order_List.aspx/GetDistributor",
                    data: "{'Order_Id':'" + order_id + "','Stockist':'" + stk + "','Division':'" + div + "','Cust_code':'" + cust_code + "'}",
                    dataType: "json",
                    success: function (data) {
                        AllOrdersdetails = JSON.parse(data.d) || [];

                        var str1 = '<div class="head" style="font-family: "Times New Roman", Times, serif;"><table border="0" class="headtable" align="left" style="width: 35%; border-collapse: collapse;"><tbody>';
                        var str2 = '<table border="0" align="left" class="middletable" style="width: 35%; border-collapse: collapse;font-size:12px"><tbody>';
                        var str3 = '<table border="0" align="left" class="lasttable" style="width: 30%; border-collapse: collapse;font-size:12px"><tbody>';

                        var a = 0;
                  <%--  $('#<%=hid_stockist_name.ClientID%>').val(AllOrdersdetails[a].Stockist_Name);--%>


                        str1 += '<tr style="font-size:12px"><td  align="left" style="padding-right:0px; padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen; TimesNewRomen;padding-left:10px;">Hatsun Toll Free Support: 18001237355</td></tr>';
                        str1 += '<tr><td  align="left" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;padding-left:10px; ">RET State Name:-' + AllOrdersdetails[a].Retstate + '</td></tr>';
                        str1 += '<tr><td  align="left" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;padding-left:10px;"><b>RET GST Tin No:</b>&nbsp;&nbsp;' + AllOrdersdetails[a].RetgstTin + '</td></tr>';
                        str1 += '<tr><td  align="left" style="padding-top: 0px;padding-bottom: 0px;font: bold 14px TimesNewRomen;padding-left:10px;"><b>' + AllOrdersdetails[a].ListedDr_Name + '</b></td></tr>';
                        str1 += '<tr><td  align="left" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;padding-left:10px;width:275px;">' + AllOrdersdetails[a].ListedDr_Address1 + '</td></tr>';
                        str1 += '<tr><td  align="left" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;padding-left:10px;">Retailer FSSI No :-' + AllOrdersdetails[a].RetFssi_No + '</td></tr>';
                        str1 += '<tr><td  align="left" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;padding-left:10px;">Retailer Ph NO:- ' + AllOrdersdetails[a].RetMobile + '</td></tr></tbody></table>';

                        str2 += '<tr><td align="left" style="font: bold 16px TimesNewRomen;padding-top: 0px;padding-bottom: 0px;"><b>' + AllOrdersdetails[a].Stockist_Name + '</b></td></tr>';
                        str2 += '<tr><td align="left" style="width:250px;padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;">' + AllOrdersdetails[a].Stockist_Address + '</td></tr>';
                        str2 += '<tr><td align="left" style="padding-top:0px;padding-bottom:0px;font:12px TimesNewRomen;">Dist FSSI No :-' + AllOrdersdetails[a].DistFssi_No + '</td></tr>';
                        str2 += '<tr><td align="center" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;">PH NO:&nbsp;&nbsp;' + AllOrdersdetails[a].Stockist_Mobile + '</b></td></tr>';
                        str2 += '<tr><td colspan="5" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;"></td></tr>';
                        str2 += '<tr><td colspan="5" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;"></td></tr></tbody></table>';

                        str3 += '<tr><td align="left" style="font: bold 16px TimesNewRomen;padding-top: 0px;padding-bottom: 0px;"><b>GST INVOICE</b></td></tr>';
                        str3 += '<tr><td align="left" style="padding-top:0px;padding-bottom:0px;font:12px TimesNewRomen;">Dist GSTIN:-' + AllOrdersdetails[a].DistgstTin + '</td></tr >';
                        str3 += '<tr><td align="left" style="padding-top:0px;padding-bottom:0px;font:12px TimesNewRomen;">Bill Date :-' + AllOrdersdetails[a].billdate + '</td></tr>';
                        str3 += '<tr><td align="left" style="padding-top:0px;padding-bottom:0px;font:12px TimesNewRomen;">Bill No:-' + AllOrdersdetails[a].billno + '</td></tr>';
                        str3 += '<tr><td align="left" style="padding-top:0px;padding-bottom:0px;font:12px TimesNewRomen;">D.State Name:-' + AllOrdersdetails[a].Diststate + '</td></tr> ';
                        str3 += '<tr><td align="left" style="padding-top:0px;padding-bottom:0px;font:12px TimesNewRomen;">D.State Code:-' + AllOrdersdetails[a].DistStatecd + '</td></tr></tbody></table></div>';

                        str += str1 + str2 + str3;

                    }
                });
            }

            function productheader() {
                str += '<div class="product" style="font:12px TimesNewRomen;"><table class="rptOrders" align="center" style="width: 100%; border-collapse: collapse; bordercolor:blue;">';
                str += '<thead><tr style="border-top:thin dashed;border-bottom:thin dashed;font:12px TimesNewRomen;"><td class="Sh1" align="center" rowspan="2"><font face="calibri"/><b>Sl.No</b></td><td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>DESCRIPTION</b></td>';
                str += '<td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>HSN Code</b></td><td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>Case</b></td><td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>Pc</b></td>';
                /*str += '<td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>MRP</b></td>*/
                str += ' <td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>Rate</b></td>';
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
                netval = amt_without_tax + totalGst - cashdis;
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

                str = '<div style="padding-bottom: -5px;background-color:#f1f2f7;bottom:0px;" class="total"><table border="0" class="rpttotal" align="center" style="width: 100%; border-collapse: collapse; bordercolor:blue;"><tbody>';
                str += '<tr style="border-top:thin dashed;font:12px TimesNewRomen;"> <td></td><td></td> <td colspan="12" ><b>** GST Summary **</b></td></tr>';
                str += '<tr style="font:12px TimesNewRomen;"><td style="padding-top:0px;padding-bottom:0px;padding-left:10px;" colspan="2"><b>GST Desc</b></td><td style="padding-top: 0px;padding-bottom: 0px;"><b>GST%</b></td><td style="padding-top: 0px;padding-bottom: 0px;"><b>GST Amount</b></td><td style="padding-top: 0px;padding-bottom: 0px;" colspan="2"><b>Gross Value</b></td><td  colspan="4" style="padding-top:0px;padding-bottom:0px;"><b>Total Case:</b>&nbsp;&nbsp;' + totlcase + '</td><td style="padding-top:0px;padding-bottom:0px;"><b>Gross Value :</b>&nbsp;&nbsp;</td><td style="float:right;padding-top:0px;padding-bottom:0px;"><b>' + amt_without_tax.toFixed(2) + '</b></td><td></td></tr>';
                str += '<tr style="font:12px TimesNewRomen;"><td style="padding-top:0px;padding-bottom:0px;padding-left:10px;" colspan="2"><b>CGST</b></td><td style="padding-top: 0px;padding-bottom: 0px;">9</td><td style="padding-top:0px;padding-bottom:0px;">' + cgst.toFixed(2) + '</td><td style="padding-top:0px;padding-bottom:0px;" colspan="3">' + amt_without_tax.toFixed(2) + '</td><td  colspan="3" style="padding-top:0px;padding-bottom:0px;"><b>Total Piece:</b>&nbsp;&nbsp;' + totalpie + '</td><td style="padding-top:0px;padding-bottom:0px;"><b>Scheme Amt :</b></td><td style="float:right;padding-top:0px;padding-bottom:0px;"><b>0.00</b></td><td></td></tr>';
                str += '<tr style="font:12px TimesNewRomen;"><td style="padding-top:0px;padding-bottom:0px;padding-left:10px;" colspan="2"><b>SGST</b></td><td style="padding-top: 0px;padding-bottom: 0px;">9</td><td style="padding-top:0px;padding-bottom:0px;">' + sgst.toFixed(2) + '</td><td style="padding-top:0px;padding-bottom:0px;" colspan="6">' + amt_without_tax.toFixed(2) + '</td><td style="padding-top:0px;padding-bottom:0px;"><b>Cash Disc :</b>&nbsp;&nbsp;</td><td style="float:right;padding-top:0px;padding-bottom:0px;"><b>' + cashdis.toFixed(2) + '</b></td><td></td></tr>';
                str += '<tr style="font:12px TimesNewRomen;"><td colspan="10" style="padding-top: 0px;padding-bottom: 0px;"></td><td style="padding-top: 0px;padding-bottom: 0px;"><b>GST Value :</b>&nbsp;&nbsp;</td><td style="float:right;padding-bottom: 0px;padding-top: 0px;"><b>' + totalGst.toFixed(2) + '</b></td><td></td></tr>';
                str += '<tr style="border-bottom:thin dashed;font:12px TimesNewRomen;"><td colspan="10" style="padding-top: 0px;padding-bottom: 0px;"></td><td style="padding-top: 0px;padding-bottom: 0px;"><b>Round Off(+/-) :</b>&nbsp;&nbsp;</td><td style="float:right;padding-top: 0px;padding-bottom: 0px;"><b>' + rndoff.toFixed(2) + '</b></td><td></td></tr>';
                str += '<tr style="border-bottom:thin dashed; padding-left:20px;font-family:monospace;font-size:14px;padding-left:10px;"><td  colspan="8">' + netwrd + ' Rupees  and ' + netwrd1 + ' Paise</td> </td><td></td><td></td><td><b>Net Value :</b>&nbsp;&nbsp;</td><td style="float:right; padding-bottom: 0px;"><b>' + netvalue.toFixed(2) + '</b></td><td></td></tr></tbody></table ></div>';
                str += '<table border="0" class="rpttotal1" style="width: 100%; border-collapse: collapse;background-color:#f1f2f7;"><tbody><tr style="font:12px TimesNewRomen;"><td style="padding-left:10px;width:450px;">We hereby Ceritify that Goods mentioned in this invoice are warranted to be of nature quality which these are purposed to be. </td><td style="padding-top: 20px;font:bold 12px TimesNewRomen;float:right;margin-right: 10px;"><b>For ' + distributor + '</b></td></tr>';
                str += '<tr style="font:12px TimesNewRomen;"><td style="padding-left:20px;padding-top: 10px;">Buyers Sign</td><td style="float:right;margin-right: 10px;padding-top: 10px;">Authorized Signatory</td></tr></tbody></table><table class="pageno"><tr style="font:12px TimesNewRomen;"><td colspan="8" style="padding-left:350px;float:center">' + pagenumber + '</td><td colspan="3" style="float:right"></td></tr></table></div></div><div style="break-after:page"></div>';

                $('#div_Print').append(str);
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
                    //autoClose: 'action|8000',
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
            function print() {
                var contents = $("#div_Print").html();
                var frame1 = $('<iframe />');
                frame1[0].name = "frame1";
                //  frame1.css({ "position": "absolute", "top": "-1000000px" });
                $("body").append(frame1);
                var frameDoc = frame1[0].contentWindow ? frame1[0].contentWindow : frame1[0].contentDocument.document ? frame1[0].contentDocument.document : frame1[0].contentDocument;
                frameDoc.document.open();
                frameDoc.document.write('<html><head><title>Invoice Print</title>');
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

            function autodelalert(rs) {
                $.confirm({
                    title: 'Alert!',
                    content: 'Do you Want to remove product ?',
                    type: 'green',
                    typeAnimated: true,
                    backgroundDismiss: 'tryAgain',
                    autoClose: 'tryAgain|2000',
                    icon: 'fa fa-check fa-2x',
                    buttons: {
                        tryAgain: {
                            text: 'OK',
                            btnClass: 'btn-green',
                            action: function () {
                                autodel(rs);
                            }
                        },

                        cancel: function () {

                            //$.alert('Quantity Will not added to the pending order');
                            //alert('Time Expired!');
                        }
                    }
                });
            }
            function autodel(r) {
                row = r;
                var order_id = row.find('#Hid_order_code').val();
                var prod = row.find('#Hid_Pro_code').val();
                idx = $(row).index();
                //getscheme(row.find('#pro_sale_code').val(), row.find('#english').val(), row.find('.unit :selected').val(), row, row.find(".fre1").text(), row.find('#Hid_Pro_code').val());
                var pending = $(row).find('.setting').attr('sett') || 0;
                $(row).find('#type').val('remove');
                if (pending == 'yes') {
                    row.css("position", "relative");
                    row.addClass('deleffect').delay(200).queue(function () {
                        row.css("display", "none");
                    });

                }
                else {
                    multivalue.splice(idx, 1);
                    row.css("position", "relative");
                    row.addClass('deleffect').delay(200).queue(function () {
                        row.remove();
                    });

                }


                var k = 1;
                $("#tblCustomers >tbody >tr").each(function () {
                    console.log(k);
                    $(this).children('td').eq(1).html(k++);
                });
                $("#tblCustomers >tbody >tr").each(function () {

                    var row = $(this).closest("tr");
                    idx = $(row).index();

                    var price = $(this).find('[id*=Price_]').val();
                    if (price == 'null' || typeof price == "undefined" || price == "" || isNaN(price)) { price = 0; }

                    var qty = $(this).find('[id*=english]').val();
                    if (qty == 'null' || typeof qty == "undefined" || qty == "" || isNaN(qty)) { qty = 0; }

                    var dis = $(this).find('.lbl_dis_per').text();
                    if (dis == 'null' || typeof dis == "undefined" || dis == "" || isNaN(dis)) { dis = 0; }

                    var dis_cal = parseFloat(price) * parseFloat(qty) * (parseFloat(dis) / 100);
                    $(this).find('[id*=hid_dis]').text(dis_cal.toFixed(2));

                    var cal = ((parseFloat(price) * parseFloat(qty)) - ((parseFloat(price) * parseFloat(qty)) * (dis / 100)));
                    $(this).find('[id*=total]').val(cal.toFixed(2));

                    var tots_tax_perc = $(this).find('.tot_tax_per').val();
                    if (isNaN(tots_tax_perc) || tots_tax_perc == "" || typeof tots_tax_perc == "undefined") { tots_tax_perc = 0; }
                    var cal_tax_perc = tots_tax_perc / 100 * $(this).find('[id*=total]').val();
                    $(this).find('.tax_val').val(parseFloat(cal_tax_perc).toFixed(2));

                    var tax_len = $(this).find('.each_tax_per').length;

                    for (var g = 0; g < tax_len; g++) {

                        var each_tax_per = $(this).find('.each_tax_per').eq(g).attr('value') || 0;
                        if (isNaN(each_tax_per) || each_tax_per == "" || typeof each_tax_per == "undefined") { each_tax_per = 0; }
                        var cal_each_tax_perc = each_tax_per / 100 * $(this).find('[id*=total]').val();
                        $(this).find('.each_tax_per').eq(g).attr('tax_value', parseFloat(cal_each_tax_perc).toFixed(2));
                    }

                    var gos = parseFloat(cal) + parseFloat(cal_tax_perc);
                    $(this).find('[id*=grs_tot]').val(gos.toFixed(2));

                });
                allqty();
            }
            function allqty() {

                var myorder_qty = 0;
                $("[id*=Order_qty]").each(function () {
                    var rr = $(this).closest('tr').find('.setting').attr('sett');
                    var rs = $(this).closest('tr').find('#english').val();
                    var myq = $(this).val();
                    if (rs != '0') {
                        if (myq != "") {
                            myorder_qty = myorder_qty + parseFloat($(this).val());
                        }
                    }
                });

                var sale_order_qty = 0;
                $("[id*=english]").each(function () {

                    var soq = $(this).val();

                    if (soq != "") {
                        sale_order_qty = sale_order_qty + parseFloat($(this).val());
                    }

                });

                var sale_dis = 0;
                $("[id*=Dis]").each(function () {

                    var sd = $(this).val();
                    if (sd != "") {
                        sale_dis = sale_dis + parseFloat($(this).val());
                    }
                });

                var sale_free = 0;
                $("[id*=Free]").each(function () {

                    var sf = $(this).val();
                    if (sf != "") {
                        sale_free = sale_free + parseFloat($(this).val());
                    }
                });

                var TaxTotal = 0;
                $(".tax_val").each(function () {
                    var ta = $(this).val();
                    if (ta != "") {
                        TaxTotal = TaxTotal + parseFloat($(this).val());
                    }
                });

                $("[id*=Tax_GST]").val(TaxTotal.toFixed(2));

                var dis_Tot = 0;
                $("[id*=hid_dis]").each(function () {
                    var x = $(this).text();
                    if (isNaN(x) || x == "") x = 0;
                    dis_Tot = (dis_Tot + parseFloat(x));
                });
                $("[id*=Txt_Dis_tot]").val(dis_Tot.toFixed(2));

                var grandTotal = 0;
                $("[id*=total]").each(function () {

                    var gt = $(this).val();
                    if (gt != "") {
                        grandTotal = grandTotal + parseFloat($(this).val());
                    }
                });
                $("[id*=sub_tot]").val(grandTotal.toFixed(2));

                var gs_tot = 0;
                $("[id*=grs_tot]").each(function () {
                    var gt = $(this).val();
                    if (gt != "") {
                        gs_tot = gs_tot + parseFloat($(this).val());
                    }
                });

                //if (Extra_Tax_Type == 'TDS') {

                //    var extra_tax = Extra_Tax;
                //    var Calc_extra_tax = extra_tax / 100 * grandTotal;
                //    var Calc_extra_total_tax = Calc_extra_tax + gs_tot;
                //    $('#txt_tds').val(Math.round((Calc_extra_tax + Number.EPSILON) * 100) / 100);
                //    var ffff = Math.round((Calc_extra_total_tax + Number.EPSILON) * 100 / 100);
                //    $("[id*=in_Tot]").val(ffff.toFixed(2));

                //}
                //else {

                //    var extra_tax = Extra_Tax;
                //    var Calc_extra_tax = extra_tax / 100 * grandTotal;
                //    var Calc_extra_total_tax = Calc_extra_tax + gs_tot;
                //    $('#txt_tcs').val(Math.round((Calc_extra_tax + Number.EPSILON) * 100) / 100);
                //    var ffff = Math.round((Calc_extra_total_tax + Number.EPSILON) * 100 / 100);
                //    $("[id*=in_Tot]").val(ffff.toFixed(2));
                //}
                $("[id*=in_Tot]").val(gs_tot.toFixed(2));
                $("[id*=Amt_Tot]").val(Math.round(gs_tot).toFixed(2));

                var tbl = $('#tblCustomers');
                $(tbl).find('tfoot tr').remove();

                if (myorder_qty != '0' || sale_order_qty != '0' || sale_dis != '0' || sale_free != '0' || grandTotal != '0') {
                    $("#tblCustomers").append('<tfoot><tr><th></th><th></th><th></th><th></th><th></th><th id="Order_q">' + myorder_qty + '</th><th id="sal_q">' + sale_order_qty + '</th><th id="dis_t">' + sale_dis + '</th><th id="free_t">' + sale_free + '</th><th id="tot_sub_amt">' + grandTotal.toFixed(2) + '</th><th id="tax_th">' + TaxTotal.toFixed(2) + '</th><th id="tot_gs_tot">' + gs_tot.toFixed(2) + '</th></tr></tfoot>');
                }
            }


        </script>

    </head>
    <body>
        <form id="form1" runat="server">
            <div class="spinnner_div" style="display: none;">
                <div class="spinner" style="position: absolute; left: 525px; top: 133px;">
                    <div class="rect1" style="background: #1a60d3;"></div>
                    <div class="rect2" style="background: #DB4437;"></div>
                    <div class="rect3" style="background: #F4B400;"></div>
                    <div class="rect4" style="background: #0F9D58;"></div>
                    <div class="rect5" style="background: orangered;"></div>
                </div>
            </div>
            <div class="container">
            </div>
            <div class="container-fluid">
                <div class="container" id="itemlist2">
                    <div class="row card" style="margin-top: -12px; max-width: 1094px;">

                        <div class="row">

                            <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="control-label " for="focusedInput">
                                        Route Name</label>
                                    <span style="color: Red"></span>
                                    <select class="form-control" id="route-name" style="width: 100%;" data-size="5">
                                        <option value="0">Select Route Name</option>
                                    </select>
                                </div>
                            </div>

                            <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="control-label " for="focusedInput">
                                        Customer Name</label>
                                    <span style="color: Red">*</span>
                                    <select class="form-control " id="recipient-name" data-rel="chosen" style="width: 100%;" data-size="5">
                                        <option value="0">Select Customer Name</option>
                                    </select>
                                </div>
                            </div>

                            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12" style="display: none;">
                                <div class="form-group">
                                    <label class="control-label" for="focusedInput">
                                        Payment Due</label>
                                    <span style="color: Red">*</span>
                                    <input class="form-control pd" id="datepicker" name="datepicker" data-validation="required"
                                        type="text" autocomplete="off" />
                                </div>
                            </div>

                            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12" style="display: none;">
                                <div class="form-group">
                                    <label class="control-label" for="focusedInput">
                                        Delivery Date</label>
                                    <span style="color: Red">*</span>
                                    <input class="form-control dd" id="datepicker1" name="datepicker1" data-validation="required"
                                        type="text" autocomplete="off" />

                                    <span id="Span2"></span>
                                </div>
                            </div>

                            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12" style="display: none;">
                                <div class="form-group">
                                    <label class="control-label" for="focusedInput">
                                        Invoice Date</label>
                                    <input name="Txt_Orderdate" type="text" id="Txt_in_date" runat="server" class="form-control invoice"
                                        data-validation="required" readonly="" value="" />
                                    <span id="Span1"></span>
                                    <input type="hidden" id="Hid_orderno" runat="server" name="Hid_orderno" />
                                </div>
                            </div>

                            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12" style="display: none;">
                                <div class="form-group">
                                    <label class="control-label" for="focusedInput">
                                        Payment Mode</label>
                                    <span style="color: Red">*</span>
                                    <select class="form-control" id="Sel_Pay_Term" name="Sel_Pay_Term" data-validation="required">
                                    </select>
                                </div>
                            </div>

                            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="control-group " for="focusedInput">
                                        Order ID</label>
                                    </br>
                                <select class="form-control poiinter" id="example-multiple-selected" multiple>
                                    example-multiple-selected         
                                </select>
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="form-group">
                                    <label class="control-label" for="focusedInput">
                                        Shipping Address</label>
                                    <textarea style="width: 330px;" rows="2" class="form-control txtblue" id="txt_Ship_add" disabled></textarea>
                                </div>
                            </div>
                        </div>
                    </div>



                    <div class="row">

                        <div style="display: none" class="col-md-3 col-lg-4">
                            <div class="form-group">
                                <label class="control-label" for="focusedInput">
                                    Shipping Term</label>
                                <span style="color: Red">*</span>
                                <input class="form-control" id="Txt_Shp_Term" runat="server" name="Shp_Team" data-validation="required"
                                    type="text" autocomplete="off" />
                            </div>
                        </div>

                        <div style="display: none" class="col-md-3">
                            <div class="form-group">
                                <label class="control-label lblorderdate" for="focusedInput">
                                    Order Date</label>
                                <input name="Txt_Orderdate" type="text" id="InvoiceNumber" runat="server" class="form-control innumber"
                                    data-validation="required" readonly="" />
                                <span id="status"></span>
                            </div>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-6" style="display: none;">
                            <div class="form-group">
                                <label class="control-label" for="focusedInput">
                                    Shipping Mode</label>
                                <span style="color: Red">*</span>
                                <select class="form-control" id="Sel_Shi_Med" name="Shi_Med" data-validation="required">
                                </select>
                            </div>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-6 hid" style="display: none;">
                            <div class="form-group">
                                <label class="control-label" for="focusedInput">
                                    Reference No</label>
                                <span style="color: Red">*</span>
                                <input type="text" id="txt_ref_no" autocomplete="off" class="form-control" />
                            </div>
                        </div>

                    </div>

                    <div class="row" style="text-align: center;">
                        <div id="myDIV">
                        </div>
                    </div>
                    <br />
                    <div class="row card">
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <table id="tblCustomers" class="table  table">
                                <thead class="ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix" style="white-space: nowrap">
                                    <tr>
                                        <th width="2%"></th>
                                        <th width="5%">Sl.No.
                                        </th>
                                        <th width="25%">Product Name
                                        </th>
                                        <th width="10%">Unit
                                        </th>
                                        <th width="8%">Price
                                        </th>
                                        <th width="5%">Order Qty
                                        </th>
                                        <th width="5%">Sales Qty
                                        </th>
                                        <th width="3%">Dis val
                                        </th>
                                        <th width="5%">Free
                                        </th>
                                        <th width="7%">Amount
                                        </th>
                                        <th width="7%">Tax
                                        </th>
                                        <th width="7%">Gross
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <button id="Del" type="button" class='btn btn-danger delete'>
                                - Delete</button>
                            <button id="Add" type="button" class='btn btn-success addmore'>
                                + Add More</button>&nbsp;&nbsp;
                           <label style="display: none;">Advance Amount :</label>&nbsp;&nbsp;
                            <input style="display: none;" type="text" class="form-control-plaintext tot_ex_amt" id="tot_ex_amt" readonly />&nbsp;&nbsp;
                            <label style="display: none;">Credit Limit :</label>&nbsp;&nbsp;
                            <input style="display: none;" type="text" class="form-control-plaintext tot_Credit_amt" id="tot_Credit_amt" readonly />&nbsp;&nbsp;
                            <label style="display: none;">Amount Due :</label>&nbsp;&nbsp;
                            <input style="display: none;" type="text" class="form-control-plaintext tot_due_amt" id="tot_due_amt" readonly />
                            <label style="display: none;" id="lbl_pending_tbl_amt"></label>
                            &nbsp;&nbsp;
                            <label style="display: none;" id="lbl_credit_amt"></label>
                            &nbsp;&nbsp;
                        </div>
                    </div>
                    <div class="row" style="margin-top: 0px;">
                        <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12" style="padding: 0px 0px 0px 0px;">
                            <div class="card freecard">
                                <div class="card-body table-responsive">
                                    <div style="white-space: nowrap; padding: 0px 0px 7px 7px; font-weight: 900;">View Offer Products</div>
                                    <div class="tddd">
                                        <table id="free_table" class="table table-hover">
                                            <thead class="text-warning">
                                                <tr>
                                                    <th style="width: 14%;">Sl No.</th>
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
                            <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                                <div class="row card">
                                    <div class="col-sm-12">
                                        <h3>Notes:
                                        </h3>
                                        <div class="form-group">
                                            <textarea placeholder="Your Notes" id="notes" name="notes" rows="5" class="form-control txt txtblue"></textarea>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>

                        <div class="col-lg-offset-8 col-md-offset-8 col-sm-offset-0 col-xs-offset-0 form-horizontal" style="display: none;">
                            <label class="col-lg-3 col-md-4 col-sm-2 col-xs-3 control-label">
                                Subtotal :
                            </label>
                            <div class="col-lg-9 col-md-8 col-sm-10 col-xs-7">
                                <div class="input-group">
                                    <div class="input-group-addon currency">
                                        <i class="fa fa-inr"></i>
                                    </div>
                                    <input data-cell="G1" id="sub_tot" data-format="0,0.00" class="form-control" readonly />
                                </div>
                            </div>
                        </div>

                        <div class="col-lg-offset-8 col-md-offset-8 col-sm-offset-0 col-xs-offset-0 form-horizontal" style="display: none;">
                            <label class="col-lg-3 col-md-4 col-sm-2 col-xs-3 control-label">
                                Discount :
                            </label>
                            <div class="col-lg-9 col-md-8 col-sm-10 col-xs-7">
                                <div class="input-group">
                                    <div class="input-group-addon currency">
                                        <i class="fa fa-inr"></i>
                                    </div>
                                    <input data-cell="G1" id="Txt_Dis_tot" data-format="0,0.00" class="form-control" readonly />
                                </div>
                            </div>
                        </div>

                        <div class="col-lg-offset-8 col-md-offset-8 col-sm-offset-0 col-xs-offset-0 form-horizontal" style="margin-top: 36px">
                            <label class="col-lg-3 col-md-4 col-sm-2 col-xs-3 control-label">
                                Tax :
                            </label>
                            <div class="col-lg-9 col-md-8 col-sm-10 col-xs-7">
                                <div class="input-group">
                                    <div class="input-group-addon currency">
                                        <i class="fa fa-inr"></i>
                                    </div>
                                    <input data-cell="G1" id="Tax_GST" data-format="0,0.00" class="form-control" readonly />
                                </div>
                            </div>
                        </div>

                        <%--                        <div class="col-lg-offset-8 col-md-offset-8 col-sm-offset-0 col-xs-offset-0 form-horizontal" style="margin-top: 40px;">
                            <div class="col-lg-12">
                                <div class="col-lg-6">
                                    <label class="col-lg-6 control-label" style="margin-left: -8px;">
                                        CGST : 
                                    </label>
                                    <div class="col-lg-6">
                                        <div class="input-group">
                                            <div class="input-group-addon currency">
                                                <i class="fa fa-inr"></i>
                                            </div>
                                            <input data-cell="G1" id="Tax_CGST" data-format="0,0.00" class="form-control" readonly="" style="width: 72px;">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <label class="col-lg-6 control-label" style="margin-left: -10px;">
                                        SGST :</label>
                                    <div class="col-lg-6" style="margin-left: -14px;">
                                        <div class="input-group">
                                            <div class="input-group-addon currency">
                                                <i class="fa fa-inr"></i>
                                            </div>
                                            <input data-cell="G1" id="Tax_SGST" data-format="0,0.00" class="form-control" readonly="" style="width: 347%;">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>--%>

                        <%--                      <div class="col-lg-offset-8 col-md-offset-8 col-sm-offset-0 col-xs-offset-0 form-horizontal" style="margin-top: 40px;">
                            <label class="col-lg-3 col-md-4 col-sm-2 col-xs-3 control-label">
                                CGST :
                            </label>
                            <div class="col-lg-9 col-md-8 col-sm-10 col-xs-7">
                                <div class="input-group">
                                    <div class="input-group-addon currency">
                                        <i class="fa fa-inr"></i>
                                    </div>
                                    <input data-cell="G1" id="Tax_CGST" data-format="0,0.00" class="form-control" readonly />
                                </div>
                            </div>
                        </div>

                        <div class="col-lg-offset-8 col-md-offset-8 col-sm-offset-0 col-xs-offset-0 form-horizontal" style="margin-top: 80px;">
                            <label class="col-lg-3 col-md-4 col-sm-2 col-xs-3 control-label">
                                SGST :
                            </label>
                            <div class="col-lg-9 col-md-8 col-sm-10 col-xs-7">
                                <div class="input-group">
                                    <div class="input-group-addon currency">
                                        <i class="fa fa-inr"></i>
                                    </div>
                                    <input data-cell="G1" id="Tax_SGST" data-format="0,0.00" class="form-control" readonly />
                                </div>
                            </div>
                        </div>

                        <div class="col-lg-offset-8 col-md-offset-8 col-sm-offset-0 col-xs-offset-0 form-horizontal" style="margin-top: 120px;">
                            <label class="col-lg-3 col-md-4 col-sm-2 col-xs-3 control-label">
                                IGST :
                            </label>
                            <div class="col-lg-9 col-md-8 col-sm-10 col-xs-7">
                                <div class="input-group">
                                    <div class="input-group-addon currency">
                                        <i class="fa fa-inr"></i>
                                    </div>
                                    <input data-cell="G1" id="Tax_IGST" data-format="0,0.00" class="form-control" readonly />
                                </div>
                            </div>
                        </div>--%>

                        <%--   <div class="col-lg-offset-8 col-md-offset-8 col-sm-offset-0 col-xs-offset-0 form-horizontal" style="margin-top: 40px;">
                            <label class="col-lg-3 col-md-4 col-sm-2 col-xs-3 control-label">
                                TCS :
                            </label>
                            <div class="col-lg-9 col-md-8 col-sm-10 col-xs-7">
                                <div class="input-group">
                                    <div class="input-group-addon currency">
                                        <i class="fa fa-inr"></i>
                                    </div>
                                    <input data-cell="G1" id="txt_tcs" data-format="0,0.00" class="form-control" readonly />
                                </div>
                            </div>
                        </div>--%>

                        <%--   <div class="col-lg-offset-8 col-md-offset-8 col-sm-offset-0 col-xs-offset-0 form-horizontal" style="margin-top: 80px;">
                            <label class="col-lg-3 col-md-4 col-sm-2 col-xs-3 control-label">
                                TDS :
                            </label>
                            <div class="col-lg-9 col-md-8 col-sm-10 col-xs-7">
                                <div class="input-group">
                                    <div class="input-group-addon currency">
                                        <i class="fa fa-inr"></i>
                                    </div>
                                    <input data-cell="G1" id="txt_tds" data-format="0,0.00" class="form-control" readonly />
                                </div>
                            </div>
                        </div>--%>

                        <div class="col-lg-offset-8 col-md-offset-8 col-sm-offset-0 col-xs-offset-0 form-horizontal" style="margin-top: 96px;">
                            <label class="col-lg-3 col-md-4 col-sm-2 col-xs-3 control-label">
                                Total (R) :
                            </label>
                            <div class="col-lg-9 col-md-8 col-sm-10 col-xs-7">
                                <div class="input-group">
                                    <div class="input-group-addon currency">
                                        <i class="fa fa-inr"></i>
                                    </div>
                                    <input data-cell="G1" id="in_Tot" data-format="0,0.00" class="form-control" readonly />
                                </div>
                            </div>
                        </div>

                        <div class="col-lg-offset-8 col-md-offset-8 col-sm-offset-0 col-xs-offset-0 form-horizontal" style="margin-top: 240px; display: none;">
                            <label class="col-lg-3 col-md-4 col-sm-2 col-xs-3 control-label">
                                R Total :
                            </label>
                            <div class="col-lg-9 col-md-8 col-sm-10 col-xs-7">
                                <div class="input-group">
                                    <div class="input-group-addon currency">
                                        <i class="fa fa-inr"></i>
                                    </div>
                                    <input data-cell="G1" id="tot_round" data-format="0,0.00" class="form-control" readonly />
                                </div>
                            </div>
                        </div>

                        <div class="col-lg-offset-8 col-md-offset-8 col-sm-offset-0 col-xs-offset-0 form-horizontal" style="margin-top: 120px; display: none;">
                            <label class="col-lg-3 col-md-4 col-sm-2 col-xs-3 control-label">
                                Adjust: 
                            </label>
                            <div class="col-lg-9 col-md-8 col-sm-10 col-xs-7">
                                <div class="input-group">
                                    <div class="input-group-addon currency">
                                        <i class="fa fa-inr"></i>
                                    </div>
                                    <input data-cell="P1" id="tot_adju" data-format="0,0.00" class="form-control" autocomplete="off" readonly
                                        name="amountdue" />
                                </div>
                            </div>
                        </div>

                        <div class="col-lg-offset-8 col-md-offset-8 col-sm-offset-0 col-xs-offset-0 form-horizontal" style="margin-top: 160px; display: none;">
                            <label class="col-lg-3 col-md-4 col-sm-2 col-xs-3 control-label">
                                Amt Paid: <span style="color: Red">*</span>
                            </label>
                            <div class="col-lg-9 col-md-8 col-sm-10 col-xs-7">
                                <div class="input-group">
                                    <div class="input-group-addon currency">
                                        <i class="fa fa-inr"></i>
                                    </div>
                                    <input data-cell="K1" id="Ad_Paid" autocomplete="off" data-format="0,0.00" class="form-control Ad_Paid" data-validation="required"
                                        name="amountpaid" />
                                </div>
                            </div>
                        </div>

                        <div class="col-lg-offset-8 col-md-offset-8 col-sm-offset-0 col-xs-offset-0 form-horizontal" style="margin-top: 200px; display: none;">
                            <label class="col-lg-3 col-md-4 col-sm-2 col-xs-3 control-label">
                                Amt Due: 
                            </label>
                            <div class="col-lg-8 col-md-8 col-sm-10 col-xs-7">
                                <div class="input-group">
                                    <div class="input-group-addon currency">
                                        <i class="fa fa-inr"></i>
                                    </div>
                                    <input data-cell="V1" id="Amt_Tot" data-format="0,0.00" class="form-control" readonly
                                        name="amountdue" />
                                </div>
                            </div>
                            <a href='#' class='example-popover' data-placement='left'><i style="padding: 7px 0px 0px 0px;" class="fa fa-info-circle" aria-hidden="true"></i></a>
                        </div>

                        <div id="popover-content" class="hide" style="display: none;">
                            <table style="font-size: 10px;">
                                <tbody>
                                    <tr>
                                        <td>Cur Inv Bal </td>
                                        <td>
                                            <label style="float: right;" id="lbl_cu_inv_amt"></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Pen (+) </td>
                                        <td>
                                            <label style="float: right;" id="lbl_out_amt"></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <label style="float: right;" id="lbl_before_creit_amt"></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Crd Amt (-) </td>
                                        <td>
                                            <label style="float: right;" id="lbl_creit_amt"></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <label style="float: right;" id="lbl_tot_amt"></label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>

                        <div class="col-lg-offset-8 col-md-offset-8 col-sm-offset-0 col-xs-offset-0 form-horizontal" style="margin-top: 19px; display: none;">
                            <label class="col-lg-3 col-md-4 col-sm-2 col-xs-3 control-label">
                                Adv Amt: 
                            </label>
                            <div class="col-lg-9 col-md-8 col-sm-10 col-xs-7">
                                <div class="input-group">
                                    <div class="input-group-addon currency">
                                        <i class="fa fa-inr"></i>
                                    </div>
                                    <input data-cell="P1" id="extra_amt" data-format="0,0.00" class="form-control " autocomplete="off" readonly
                                        name="amountdue" />
                                </div>
                            </div>
                        </div>

                    </div>

                    <div class="row" style="text-align: center">
                        <div class="fixed">
                            <a id="btndaysave"
                                class="btn btn-primary btnsave" style="vertical-align: middle; font-size: 17px;">
                                <span>Save Invoice</span></a>
                        </div>
                    </div>
                </div>
                <div id="div_Print" style="display: none; padding-right: 10px; padding-top: 10px; padding-left: 10px; min-height: 842px;">
                </div>
            </div>

            <script type="text/javascript" src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
            <script type="text/javascript" src="../js/bootstrap.min.js"></script>


            <script type="text/javascript" src="../js/lib/bootstrap-select.min.js"></script>
            <%-- <script type="text/javascript" src="../js/moment-2.24.0.min.js"></script>
         
<script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js" integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49" crossorigin="anonymous"></script>--%>
        </form>
    </body>
    </html>
</asp:Content>
