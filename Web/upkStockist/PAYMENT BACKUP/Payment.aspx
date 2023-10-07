﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master_DIS.master" AutoEventWireup="true" CodeFile="Payment.aspx.cs" Inherits="Stockist_Payment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        /*.tableBodyScroll tbody {
            display: block;
            max-height: 624px;
            overflow-y: scroll;
            width: 104%;
        }

        .tableBodyScroll thead,
        tbody tr {
            display: table;
            width: 100%;
            table-layout: fixed;
        }*/
        .nav-tabs > li.active > a, .nav-tabs > li.active > a:hover, .nav-tabs > li.active > a:focus {
            color: #19a4c6;
            cursor: default;
            background-color: #f0f3f4;
            border: 1px solid #f0f3f4;
            border-bottom: 2px solid #19a4c6;
        }

        .chosen-container {
            min-width: 245px;
        }

        .bootstrap-select > select {
            position: absolute !important;
            bottom: 0;
            left: -300% !important;
            display: block;
            width: 0.5px !important;
            height: 100% !important;
            padding: 0 !important;
            opacity: 0 !important;
            border: none;
            z-index: 0 !important;
        }

        .bootstrap-select .dropdown-menu.inner {
            position: static;
            float: none;
            border: 0;
            padding: 0;
            margin: 0;
            border-radius: 0;
            -webkit-box-shadow: none;
            box-shadow: none;
            max-height: 267px !important;
            overflow-y: auto;
            min-height: 68px;
        }

        .pd .chosen-container {
            width: 100% !important;
        }

        /*.chosen-container-single .chosen-single {
            background: #edf6ff;
            border: 1.5px solid #91e1ff;
        }*/
    </style>
    <link href="../css/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
    <link href="../alertstyle/jquery-confirm.min.css" rel="stylesheet" />
    <script src="../alertstyle/jquery-confirm.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.2/js/bootstrap-select.min.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {

            get_retailer = []; Invoice_detail = []; var total_pending_amount = 0; get_dsm = []; var pre_val = 0; var Advance_amount = 0; var detailsdata = []; var avail_amt = 0; var total_avail_amt = 0;
            var credit_Details = []; var calender_year_type = 0; var RetcusCode = ''; var Adv_Detail = []; var From_month = ''; var To_month = '';

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
            today = yyyy + '-' + mm + '-' + dd;
            $("#datepicker").val(today); $('#adv_date').val(today); $("#datepicker2").val(today);
            $('.ref_row').hide();
            $('.adjust').prop("disabled", false).css("display", "none");
            $('#lbl_tot_amt').text($('#lbl_adv_amt').text());
            $(document).on('keypress', '.amt, #txt_rec_amt', function (e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });

            $(document).on('keypress', '#txt_remark', function (e) {
                if (e.keyCode == 34 || e.keyCode == 39 || e.keyCode == 38 || e.keyCode == 60 || e.keyCode == 62 || e.keyCode == 92) return false;
            });

            var get_access_master = localStorage.getItem("Access_Details");
            get_access_master = JSON.parse(get_access_master);
            var access_month = get_access_master[0].Year_value;
            var access_type = get_access_master[0].Year_setting;
            access_month = access_month.split('-');
            From_month = access_month[0];
            To_month = access_month[1];

            if (access_type == '0') {

                $('.Selyear').text('Calender Year');
                var this_year = new Date().getFullYear();
                $('#Year_id').val(this_year);

            }
            else {
                call_year_setting(From_month, To_month, access_type);
            }

            function call_year_setting(from_mnth, to_mnth, type) {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    data: "{'FM':'" + from_mnth + "','TM':'" + to_mnth + "','Type':'" + type + "'}",
                    url: "Payment.aspx/Get_Year_Data",
                    dataType: "json",
                    success: function (data) {

                        Year_Array = JSON.parse(data.d);
                        var bind_Year_Array = Year_Array.length - 1;
                        $('.Selyear').text('Financial Year');
                        $('#Year_id').val(Year_Array[bind_Year_Array].Get_years);

                        // for (var t = 0; t < Financial_Year.length; t++) {
                        //     $('.Year_selected').append("<option value=" + Financial_Year[t].Financial_Year + " selected>" + Financial_Year[t].Financial_Year + "</option>");
                        // }

                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            }

            //function call_calender_year() {

            //    $.ajax({
            //        type: "POST",
            //        contentType: "application/json; charset=utf-8",
            //        async: false,
            //        url: "Payment.aspx/Get_Calender_Year",
            //        dataType: "json",
            //        success: function (data) {
            //            Calender_Year = JSON.parse(data.d);
            //            var this_year = new Date().getFullYear();
            //            //    for (var t = 0; t < Calender_Year.length; t++) {
            //            //        if (this_year == Calender_Year[t].Year) {
            //            //           $('.Selyear').text('Calender Year');
            //            //           $('.Year_selected').append("<option value=" + Calender_Year[t].Year + " selected>" + Calender_Year[t].Year + "</option>");
            //            //       }
            //            //       else {
            //            //           $('.Year_selected').append("<option value=" + Calender_Year[t].Year + ">" + Calender_Year[t].Year + "</option>");
            //            //    }
            //            // }
            //            $('#finid').val(this_year);

            //        },
            //        error: function (result) {
            //            alert(JSON.stringify(result));
            //        }
            //    });
            //}

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Payment.aspx/bindretailer",
                data: "{'stk':'<%=Session["Sf_Code"]%>','Div':'<%=Session["Division_Code"]%>'}",
                dataType: "json",
                success: function (data) {
                    itms = JSON.parse(data.d) || [];
                    for (var i = 0; i < itms.length; i++) {
                        $('#Customer_Name_for_adv').append($("<option></option>").val(itms[i].ListedDrCode).html(itms[i].ListedDr_Name1)).trigger('chosen:updated').css("width", "100%");
                    }
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });

            $('#Customer_Name_for_adv').chosen();

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "Payment.aspx/bind_invoiced_retailer",
                dataType: "json",
                async: false,
                success: function (data) {
                    get_retailer = JSON.parse(data.d);
                    if (get_retailer.length > 0) {
                        $.each(get_retailer, function (data, value) {
                            $('#Customer_Name').append($("<option></option>").val(this['Code']).html(this['Name'])).trigger('chosen:updated').css("width", "100%");;;
                        });
                    }
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }

            });
            $('#Customer_Name').chosen();

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "Payment.aspx/bind_DSM_Details",
                dataType: "json",
                async: false,
                success: function (data) {
                    get_dsm = JSON.parse(data.d);
                    if (get_dsm.length > 0) {
                        $.each(get_dsm, function (data, value) {
                            $('#colled_by').append($("<option></option>").val(this['Code']).html(this['Name'])).trigger('chosen:updated').css("width", "100%");;;
                        });
                    }
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
            $('#colled_by').chosen();

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Payment.aspx/GetPayType",
                dataType: "json",
                success: function (data) {
                    if (data.d.length > 0) {
                        var ddlCustomers = $("#pay_mode");
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


            $(document).on("change", "#Customer_Name", function (e) {

                var selected_customer_Code = $('#Customer_Name option:selected').val();
                var selected_customer_nme = $('#Customer_Name option:selected').text();
                get_finanacial_year = $('.Year_selected').val();

                if (get_access_master[0].Year_setting == '0') {

                    bind_order(selected_customer_Code, get_finanacial_year, get_finanacial_year, From_month, To_month, access_type);

                }
                else {

                    get_finanacial_year = get_finanacial_year.split('-');
                    splited_year = get_finanacial_year[1];
                    splited_year1 = get_finanacial_year[3];
                    bind_order(selected_customer_Code, splited_year, splited_year1, From_month, To_month, access_type);
                }
                clear();
                get_credit_details(selected_customer_Code);
            });


            function bind_order(selected_customer_Code, F_year, T_year, from_month, to_month, typ) {

                $.ajax({
                    type: "Post",
                    contentType: "application/json; charset=utf-8",
                    url: "Payment.aspx/Get_invoiced_Order_details",
                    data: "{'Customer_Code':'" + selected_customer_Code + "','From_Year':'" + F_year + "','To_Year':'" + T_year + "','From_Month':'" + from_month + "','To_Month':'" + to_month + "','Type':'" + typ + "'}",
                    dataType: "json",
                    async: false,
                    success: function (data) {
                        Invoice_detail = JSON.parse(data.d);
                        Advance_amount = '';
                        bind_pending_order(Invoice_detail);
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            }

            function get_credit_details(cust) {

                $.ajax({
                    type: "Post",
                    contentType: "application/json; charset=utf-8",
                    url: "Payment.aspx/Get_customerWise_credit_note_details",
                    data: "{'Customer_Code':'" + cust + "'}",
                    dataType: "json",
                    async: false,
                    success: function (data) {
                        credit_Details = JSON.parse(data.d);
                        $('#credit_tbl tbody').html("");
                        $('#credit_tbl tfoot').html("");
                        for (var a = 0; a < credit_Details.length; a++) {
                            if (credit_Details[a].Credit_amt > 0) {
                                var C_amout = credit_Details[a].amt == "" ? credit_Details[a].amt : parseFloat(credit_Details[a].amt).toFixed(2);
                                str = '<tr><td style="width: 11%;"><input type="checkbox" name="cre_chk" class="Credit_check" id="Credit_check" ></td>';
                                str += '<td class="C_inv">' + parseFloat(credit_Details[a].Credit_amt).toFixed(2) + '</td>';
                                str += '<td class="C_dt">' + credit_Details[a].Adjust_credit + '</td>';
                                str += '<td><input type="text" id="C_amt" style="width:70px;" autocomplete="off" class="amt" value="' + credit_Details[a].Bal + '"</ td><td style="display:none;"><input type="hidden" id="hid_amt" /></td></tr>';
                                $('#credit_tbl tbody').append(str);
                            }
                            else {
                                $("#credit_tbl tfoot").html('<tr><td colspan="3">No Credit Amount is Available</td></tr>');
                            }
                        };
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            }

            $(document).on("click", ".Credit_check", function (e) {

                var row = $(this).closest('tr');

                if ($('input[name="cre_chk"]:checked').length > 0) {

                    $('#txt_rec_amt').attr('readonly', true);
                    $('.save').prop("disabled", true).css("display", "none");
                    $('.adjust').prop("disabled", false).css("display", "block");
                    $('#pay_mode').attr('readonly', true);
                    $('#colled_by_chzn').prop('disabled', true).trigger("chosen:updated");
                    $('#txt_remark').attr('readonly', true);
                }
                else {
                    $('#txt_rec_amt').attr('readonly', false);
                    $('.save').prop("disabled", false).css("display", "block");
                    $('.adjust').prop("disabled", true).css("display", "none");
                    $('#pay_mode').attr('readonly', false);
                    $('#txt_remark').attr('readonly', false);
                    $('#colled_by_chzn').prop('disabled', false).trigger("chosen:updated");
                }

                var ch = $(this).attr('checked') ? true : false;

                if (ch == true) {
                    var check_inv = parseFloat(row.find('.C_inv').text());
                    var checked_inv = parseFloat(row.find('.C_inv').text());
                }
                else {
                    var check_inv = 0;
                    var checked_inv = 0;
                }

                $('#pending_bill_table tbody tr').each(function () {

                    var row = $(this).closest('tr');
                    var indx = $(row).index();

                    var pending_amt = parseFloat(row.find('.pending_amt').text());

                    if (check_inv > pending_amt) {
                        var bal_rec_amt = check_inv - pending_amt;
                        check_inv = bal_rec_amt;
                        row.find('.amt').val(parseFloat(pending_amt).toFixed(2));
                    }
                    else {
                        var bal_rec_amt = pending_amt - check_inv;
                        row.find('.amt').val(parseFloat(check_inv).toFixed(2));
                        check_inv = 0;
                    }

                });
                //else {
                //    var check_inv = 0;
                //    bind_order($('#Customer_Name option:selected').val(), splited_year, splited_year1, calender_year_type);
                //    //bind_order(selected_customer_Code, splited_year, splited_year1);
                //}

                //    bind_pending_order(Invoice_detail);
                var show = 0;

                if (total_pending_amount > checked_inv) {
                    row.find('#C_amt').val(show.toFixed(2));
                    //row.find('.C_dt').text(parseFloat(row.find('.C_inv').text()).toFixed(2));
                    row.find('.C_dt').text(checked_inv.toFixed(2));
                }
                else {
                    var che_inv = checked_inv - total_pending_amount;
                    row.find('#C_amt').val(parseFloat(che_inv).toFixed(2));
                    row.find('.C_dt').text(parseFloat(parseFloat(checked_inv) - parseFloat(che_inv)).toFixed(2));
                }
            });


            $(document).on("change", "#pay_mode", function (e) {

                var selected_type = $('#pay_mode option:selected').text();

                if (selected_type == "CASH" || selected_type == "Select") {

                    $('.ref_row').hide();
                    $('#ref_no').val('');
                    $('#bank_name').val('');

                }
                else {
                    $('.ref_row').show();
                    $('#ref_no').val('');
                    $('#bank_name').val('');
                }

            });


            function clear() {
                $('#txt_rec_amt').val('');
                $('#adv_amt').val('');
                $('#ref_no').val('');
                $('#pay_mode').val(0)
                $('#colled_by').val(0);
                $('#bank_name').val('');
            }

            function bind_pending_order(Invoice_detail) {

                $('#pending_bill_table tbody').html("");
                var str = ''; var le = 0; var total_amount = 0; var collected_amount = 0; var pending_amount = 0; total_pending_amount = 0; avail_amt = 0; total_avail_amt = 0;

                for (var a = 0; a < Invoice_detail.length; a++) {

                    le = le + 1;
                    total_amount = parseFloat(total_amount) + parseFloat(Invoice_detail[a].BillAmt);
                    collected_amount = parseFloat(collected_amount) + parseFloat(Invoice_detail[a].Coll_Amt);
                    pending_amount = parseFloat(pending_amount) + parseFloat(Invoice_detail[a].Bal_Amt);
                    avail_amt = parseFloat(avail_amt) + parseFloat(Invoice_detail[a].aval_bal);
                    total_pending_amount = pending_amount;
                    total_avail_amt = avail_amt;
                    Advance_amount = Invoice_detail[a].Advance_amount;
                    var amout = Invoice_detail[a].Amt == "" ? Invoice_detail[a].Amt : parseFloat(Invoice_detail[a].Amt).toFixed(2);

                    if (Invoice_detail[a].Bal_Amt > 0) {

                        str = '<tr><td style="width: 11%;">' + le + '</td>';
                        str += '<td style="width: 19%;white-space: nowrap;" class="bill">' + Invoice_detail[a].Invoice_No + '</td>';
                        str += '<td style="width: 18%; white-space: nowrap;" class="dtt">' + Invoice_detail[a].Invoice_Date1 + '</td>';
                        str += '<td class="b_amt">' + Invoice_detail[a].BillAmt.toFixed(2); + '</td>';
                        str += '<td class="pending_amt">' + Invoice_detail[a].Bal_Amt.toFixed(2); + '</td>';
                        str += '<td style="display:none;" class="bill_no">' + Invoice_detail[a].BillNo; + '</td>';
                        str += '<td style="display:none;" class="invoice_date">' + Invoice_detail[a].Inv_Dt; + '</td>';
                        str += '<td><input type="text" readonly id="amt" style="width:70px;" autocomplete="off" class="amt" value="' + amout + '"</td><td style="display:none;"><input type="hidden"  id="hid_amt" /></td></tr>';
                        $('#pending_bill_table tbody').append(str);
                    }
                };

                $(document).find('#lbl_billed_amt').text(total_amount.toFixed(2));
                $(document).find('#lbl_paid_amt').text(collected_amount.toFixed(2));
                $(document).find('#lbl_pending_amt').text(pending_amount.toFixed(2));
                $(document).find('#lbl_adv_amt').text(Advance_amount || 0.00);

            }
            $(document).on("keyup", "#txt_rec_amt", function (e) {

                var Over_Adv_Amt = $('#lbl_adv_amt').text();

                if ($(this).val() == "") {

                    var rec_amt = 0;
                    var tot_value = 0;

                    $(".Credit_check").prop("disabled", false);
                    $('#lbl_tot_amt').text(Over_Adv_Amt);
                    // $('.adjust').prop("disabled", false);
                }
                else {
                    $(".Credit_check").prop("disabled", true);
                    $('.adjust').prop("disabled", true).css("display", "none");;
                    var rec_amt = parseFloat($(this).val()) + parseFloat(Over_Adv_Amt);
                    var tot_value = parseFloat($(this).val()) + parseFloat(Over_Adv_Amt);
                    $('#lbl_tot_amt').text(tot_value);
                }

                $('#pending_bill_table tbody tr').each(function () {

                    var row = $(this).closest('tr');
                    var indx = $(row).index();

                    var pending_amt = parseFloat(row.find('.pending_amt').text());

                    if (rec_amt > pending_amt) {
                        var bal_rec_amt = rec_amt - pending_amt;
                        rec_amt = bal_rec_amt;
                        row.find('.amt').val(parseFloat(pending_amt).toFixed(2));
                    }
                    else {
                        var bal_rec_amt = pending_amt - rec_amt;
                        row.find('.amt').val(parseFloat(rec_amt).toFixed(2));
                        rec_amt = 0;
                    }

                });

                if (rec_amt == 0) {
                    $(document).find('#adv_amt').val('');
                }
                else {

                    var after_adv = parseFloat(tot_value) - parseFloat(total_pending_amount);
                    $(document).find('#adv_amt').val(after_adv.toFixed(2));
                }

                //if ((tot_value + Advance_amount) > total_pending_amount) {
                //    $(document).find('#adv_amt').val((tot_value + Advance_amount) - total_pending_amount);
                //}
                //else {
                //    //  rec_amt = (tot_value + Advance_amount) - total_pending_amount;
                //    //  $(document).find('#adv_amt').val(parseFloat(rec_amt).toFixed(2));
                //}

                //if ($(this).val() == "") {
                //    $(".Credit_check").prop("disabled", false);
                //    $('.adjust').prop("disabled", false);
                //    calc($(this).val(), e, 0)
                //}
                //else {
                //    $(".Credit_check").prop("disabled", true);
                //    $('.adjust').prop("disabled", true);
                //    calc($(this).val(), e, 1)
                //}
            });

            //function calc(val, e, flag) {

            //    var rec_amt = val;

            //    if (rec_amt == "") {
            //        var rec_amt = 0;
            //    }
            //    else {
            //        var rec_amt = parseFloat(val);
            //    }

            //    if (flag == 0) {
            //        Invoice_detail = [];
            //        bind_order($('#Customer_Name option:selected').val(), splited_year, splited_year1, calender_year_type);

            //        cal(Invoice_detail, rec_amt, flag)
            //    }
            //    else {
            //        Invoice_detail = [];
            //        bind_order($('#Customer_Name option:selected').val(), splited_year, splited_year1, calender_year_type);
            //        cal(Invoice_detail, parseFloat(rec_amt || 0) + parseFloat(Advance_amount), flag)
            //    }
            //    bind_pending_order(Invoice_detail);

            //    if (total_pending_amount > (rec_amt + Advance_amount)) {
            //        $(document).find('#adv_amt').val('');
            //    }
            //    else {
            //        rec_amt = (rec_amt + Advance_amount) - total_pending_amount;
            //        $(document).find('#adv_amt').val(parseFloat(rec_amt).toFixed(2));
            //    }
            //}
            //function cal(Invoice_detail, rec_amt, flag) {

            //    if (flag == 0) {

            //        for (var s = 0; s < Invoice_detail.length; s++) {

            //            if (rec_amt > Invoice_detail[s].Bal_Amt) {
            //                var bal_rec_amt = parseFloat(rec_amt) - parseFloat(Invoice_detail[s].Bal_Amt);
            //                rec_amt = bal_rec_amt;
            //                Invoice_detail[s].Amt = Invoice_detail[s].Bal_Amt;
            //            }
            //            else {
            //                var bal_rec_amt = parseFloat(Invoice_detail[s].Bal_Amt) - parseFloat(rec_amt);
            //                Invoice_detail[s].Amt = rec_amt;
            //                rec_amt = 0;
            //            }
            //            if (rec_amt > 0) {
            //                continue;
            //            }
            //            else {
            //                pre_val = Invoice_detail[s].Amt;
            //                break;
            //            }

            //        }
            //    }
            //    else {

            //        for (var s = 0; s < Invoice_detail.length; s++) {

            //            if (rec_amt > Invoice_detail[s].Bal_Amt) {
            //                var bal_rec_amt = parseFloat(rec_amt) - parseFloat(Invoice_detail[s].Bal_Amt);
            //                rec_amt = bal_rec_amt;
            //                Invoice_detail[s].Amt = Invoice_detail[s].Bal_Amt;
            //            }
            //            else {
            //                var bal_rec_amt = parseFloat(Invoice_detail[s].Bal_Amt) - parseFloat(rec_amt);
            //                Invoice_detail[s].Amt = rec_amt;
            //                rec_amt = 0;
            //            }
            //            if (rec_amt > 0) {
            //                continue;
            //            }
            //            else {
            //                break;
            //            }
            //        }
            //    }
            //}

            var Credit_adjust_count = 0;
            $(document).on("click", ".adjust", function (e) {

                Credit_adjust_count += 1;
                if (Credit_adjust_count == "1") {

                    Credit_data = [];
                    var Customer_ID = $(document).find('#Customer_Name  option:selected').val();
                    var Customer_Name = $(document).find('#Customer_Name option:selected').text()

                    if (Customer_ID == '0' || Customer_ID == '---Select Customer Name---') {
                        Credit_adjust_count = 0;
                        rjalert('Error!', 'Select Customer Details.', 'error');
                        //alert('Select Customer Details'); 
                        return false;
                    }

                    var Pay_Date = $(document).find('#datepicker2').val(); //if (Pay_Date == '0' || Pay_Date == '') { Credit_adjust_count = 0; alert('Select Pay Date'); return false; }

                    var Credit_amt = $('.C_dt').text();

                    var adjs = $('.C_dt').text();
                    var addd = $('#C_amt').val();

                    var inv_no = "";

                    $('#pending_bill_table tbody tr').each(function () {

                        var table_amt = $(this).closest("tr").find('#amt').val();

                        if (table_amt != "" && table_amt != "0.00") {
                            itm2 = {}
                            itm2.bill_no = $(this).closest("tr").find('.bill').text();
                            itm2.bill_date = $(this).closest("tr").find('.invoice_date').text();
                            itm2.bill_amt = $(this).closest("tr").find('.b_amt').text();
                            itm2.Pen_amt = $(this).closest("tr").find('.pending_amt').text();
                            itm2.paid_amt = $(this).closest("tr").find('#amt').val();
                            itm2.order_no = $(this).closest("tr").find('.bill_no').text();
                            itm2.inv_no = $(this).closest("tr").find('.bill').text();
                            detailsdata.push(itm2);
                            inv_no += $(this).closest("tr").find('.bill').text() + ','
                        }
                    });

                    $.ajax({
                        type: "Post",
                        contentType: "application/json; charset=utf-8",
                        url: "Payment.aspx/Update_credit",
                        data: "{'Invoice_details':'" + JSON.stringify(detailsdata) + "','details':'" + JSON.stringify(detailsdata) + "','Cust_ID':'" + Customer_ID + "','Cust_Name':'" + Customer_Name + "','Total_amount':'" + Credit_amt + "','Advance_pay':'" + addd + "','Advance_Adj':'" + adjs + "','Type':'0','invoice_no':'" + inv_no + "','Pay_Date':'" + Pay_Date + "'}",
                        dataType: "json",
                        async: false,
                        success: function (data) {
                            if (data.d = "Success") {
                                $('#txt_rec_amt').attr('readonly', false);
                                $('.save').prop("disabled", false);
                                $('#pay_mode').attr('readonly', false);
                                $('#txt_remark').attr('readonly', false);
                                $('#colled_by_chzn').prop('disabled', false).trigger("chosen:updated");
                                get_credit_details(Customer_ID);
                                get_finanacial_year = $('.Year_selected').val();

                                if (get_access_master[0].Year_setting == '0') {
                                    bind_order($(document).find('#Customer_Name  option:selected').val(), get_finanacial_year, get_finanacial_year, From_month, To_month, access_type);
                                }
                                else {

                                    get_finanacial_year = get_finanacial_year.split('-');
                                    splited_year = get_finanacial_year[1];
                                    splited_year1 = get_finanacial_year[3];
                                    bind_order($(document).find('#Customer_Name  option:selected').val(), splited_year, splited_year1, From_month, To_month, access_type);
                                }
                                rjalert('Success!', 'Credit Note Adjusted Successfully.', 'success');
                                //alert('Credit Note Adjusted Successfully');
                            }
                            else {
                                rjalert('Error!', 'Credit Note Not Adjusted.', 'error');
                                // alert('Credit Note Not Adjusted');
                            }
                        },
                        error: function (result) {
                            Credit_adjust_count = 0;
                            alert(JSON.stringify(result));
                        }
                    });
                }
            });

            var buttoncount = 0;
            $(document).on("click", ".save", function () {

                buttoncount += 1;
                detailsdata = [];

                if (buttoncount == "1") {
                    var ref = '';

                    var Customer_ID = $(document).find('#Customer_Name  option:selected').val();
                    var Customer_Name = $(document).find('#Customer_Name option:selected').text()

                    if (Customer_ID == '0' || Customer_ID == '---Select Customer Name---') {
                        buttoncount = 0;
                        rjalert('Error!', 'Select Customer Details.', 'error');
                        //alert('Select Customer Details');
                        return false;
                    }

                    var recevied_amt = $(document).find('#txt_rec_amt').val();

                    if (recevied_amt == '') {
                        buttoncount = 0;
                        rjalert('Error!', 'Enter Amount.', 'error');
                        //alert('Enter Amount');
                        $(document).find('#txt_rec_amt').focus();
                        return false;
                    }

                    var payment_mode = $(document).find('#pay_mode option:selected').text();

                    if (payment_mode == 'Select') {
                        buttoncount = 0;
                        rjalert('Error!', 'Select Payment Mode.', 'error');
                        //alert('Select Payment Mode');
                        $(document).find('#pay_mode').focus();
                        return false;
                    }

                    if (payment_mode == "Select" || payment_mode == "CASH") { ref = ''; }

                    else {

                        ref = $(document).find('#ref_no').val();
                        if (ref_no == '') {
                            buttoncount = 0;
                            rjalert('Error!', 'Enter Reference Number.', 'error');
                            //alert('Enter Reference Number');
                            $(document).find('#ref_no').focus();
                            return false;
                        }
                        var Bank_Name = $(document).find('#bank_name').val();
                        if (Bank_Name == '') {
                            buttoncount = 0;
                            rjalert('Error!', 'Enter Bank Name.', 'error');
                            //alert('Enter Bank Name');
                            $(document).find('#bank_name').focus();
                            return false;
                        }
                    }

                    var Pay_Date = $(document).find('#datepicker2').val();
                    if (Pay_Date == '0' || Pay_Date == '') {
                        buttoncount = 0;
                        rjalert('Error!', 'Select Pay Date.', 'error');
                        //alert('Select Pay Date'); 
                        return false;
                    }

                    var collected_By = $(document).find('#colled_by option:selected').text();
                    if (collected_By == 'Select') {
                        buttoncount = 0;
                        rjalert('Error!', 'Select Collected By.', 'error');
                        //alert('Select Collected By'); 
                        return false;
                    }

                    var remark = $(document).find('#txt_remark').val();
                    var tot_adv_amt = $(document).find('#adv_amt').val();
                    var adv_adj = $(document).find('#lbl_adv_amt').text();

                    var inv_no = "";

                    $('#pending_bill_table tbody tr').each(function () {

                        var table_amt = $(this).closest("tr").find('#amt').val();

                        if (table_amt != "" && table_amt != "0.00") {
                            itm2 = {}
                            itm2.bill_no = $(this).closest("tr").find('.bill').text();
                            itm2.bill_date = $(this).closest("tr").find('.invoice_date').text();
                            itm2.bill_amt = $(this).closest("tr").find('.b_amt').text();
                            itm2.Pen_amt = $(this).closest("tr").find('.pending_amt').text();
                            itm2.paid_amt = $(this).closest("tr").find('#amt').val();
                            itm2.order_no = $(this).closest("tr").find('.bill_no').text();
                            itm2.inv_no = $(this).closest("tr").find('.bill').text();
                            detailsdata.push(itm2);
                            inv_no += $(this).closest("tr").find('.bill').text() + ','
                        }
                    });
                    console.log(Invoice_detail);
                    $.ajax({
                        type: "Post",
                        contentType: "application/json; charset=utf-8",
                        url: "Payment.aspx/Save_Billing",
                        data: "{'Paymend_Details':'" + JSON.stringify(detailsdata) + "','details':'" + JSON.stringify(detailsdata) + "','Cust_ID':'" + Customer_ID + "','Cust_Name':'" + Customer_Name + "','Total_amount':'" + recevied_amt + "','Mode':'" + payment_mode + "','Reference_No':'" + ref + "','Bk_name':'" + Bank_Name + "','Pay_Date':'" + Pay_Date + "','collect_by':'" + collected_By + "','Remark':'" + remark + "','Advance_pay':'" + tot_adv_amt + "','Advance_Adj':'" + adv_adj + "','Type':'1','invoice_no':'" + inv_no + "'}",
                        dataType: "json",
                        async: false,
                        success: function (data) {
                            if (data.d = "Success") {

                                // alert('Payment Paid Successfully');
                                $.confirm({
                                    title: 'Success!',
                                    content: 'Payment Paid Successfully!',
                                    type: 'green',
                                    typeAnimated: true,
                                    autoClose: 'action|8000',
                                    icon: 'fa fa-check fa-2x',
                                    buttons: {
                                        tryAgain: {
                                            text: 'OK',
                                            btnClass: 'btn-green',
                                            action: function () {
                                                window.location = "../Stockist/Payment_List.aspx";
                                            }
                                        }
                                    }
                                });

                            }
                        },
                        error: function (result) {
                            buttoncount = 0;
                            alert(JSON.stringify(result));
                        }
                    });
                }
            });

            var Adv_Count = 0;
            $(document).on("click", ".btn_adv_save", function () {

                Adv_Count += 1;
                detailsdata = [];

                if (Adv_Count == "1") {

                    var selected_cust_code = $(document).find('#Customer_Name_for_adv  option:selected').val();
                    var selected_cust_name = $(document).find('#Customer_Name_for_adv  option:selected').text();

                    if (selected_cust_code == '0' || selected_cust_code == 'Select Customer Name') {
                        Adv_Count = 0;
                        rjalert('Error!', 'Select Customer Name.', 'error');
                        //alert('Select Customer Name');
                        return false;
                    }

                    var received_advancea_amt = $('#given_adv_amt').val();

                    if (received_advancea_amt == "" || received_advancea_amt == 0) {
                        Adv_Count = 0;
                        rjalert('Error!', 'Enter Advance Amount.', 'error');
                        //alert('Enter Advance Amount');
                        return false;
                    }

                    var Advance_date = $('#adv_date').text();

                    $.ajax({
                        type: "Post",
                        contentType: "application/json; charset=utf-8",
                        url: "Payment.aspx/Advance_saving",
                        data: "{'Customer_ID':'" + selected_cust_code + "','Advance_amt':'" + received_advancea_amt + "'}",
                        dataType: "json",
                        async: false,
                        success: function (data) {
                            if (data.d = "Success") {
                                rjalert('Success!', 'Advance Amount Saved Successfully.', 'success');
                                //alert('Advance Amount Saved Successfully');
                                $('#given_adv_amt').val('');
                                //$('#Customer_Name_for_adv').val(0);
                                //$('#Customer_Name_for_adv_chzn').remove();
                                //$('#Customer_Name_for_adv').removeClass('chzn-done');
                                //$('#Customer_Name_for_adv').chosen();
                                retailer_adv($('#Customer_Name_for_adv option:selected').val());
                            }
                        },
                        error: function (result) {
                            Adv_Count = 0;
                            alert(JSON.stringify(result));
                        }
                    });
                }
            });


            $(document).on("change", "#Customer_Name_for_adv", function () {

                RetcusCode = $('#Customer_Name_for_adv option:selected').val();
                var cusName = $('#Customer_Name_for_adv option:selected').text();
                retailer_adv(RetcusCode)
            });

            function retailer_adv(RetcusCode) {
                $.ajax({
                    type: "Post",
                    contentType: "application/json; charset=utf-8",
                    url: "Payment.aspx/GetAdvanceDetails",
                    data: "{'Customer_Code':'" + RetcusCode + "'}",
                    dataType: "json",
                    async: false,
                    success: function (data) {
                        Adv_Detail = JSON.parse(data.d);
                        bind_adv_Details(Adv_Detail);
                        if (Adv_Detail.length > 0) { $('#tot_adv_amt_val').val(parseFloat(Adv_Detail[0].tot_adv_amt).toFixed(2)); } else { $('#tot_adv_amt_val').val(''); }
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            }

            function bind_adv_Details(Adv_Detail) {

                $('#Ret_adv_tbl tbody').html("");
                for ($i = 0; Adv_Detail.length > $i; $i++) {
                    if ($i < Adv_Detail.length) {
                        tr = $("<tr></tr>");
                        $(tr).html("<td style='display:none';>" + Adv_Detail[$i].ListedDrCode + "</td><td>" + Adv_Detail[$i].ListedDr_Name + "</td><td>" + Adv_Detail[$i].Advance + "</td><td>" + Adv_Detail[$i].eDate + "</td><td>" + Adv_Detail[$i].EntryFrom + "</td>");
                        $("#Ret_adv_tbl tbody").append(tr);
                    }
                }
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

    </script>

    <form id="form1" runat="server">

        <div class="container" style="margin-top: -19px; max-width: 1100px; min-height: 1000px;">

            <ul class="nav nav-tabs">
                <li class="active"><a data-toggle="tab" href="#home">Payment</a></li>
                <li><a data-toggle="tab" href="#Advance_given">Retailer Advance</a></li>
            </ul>

            <div class="tab-content">
                <div id="home" class=" card tab-pane fade active in" style="margin-top: 11px;">
                    <div class="table-responsive">
                        <table>
                            <tbody>
                                <tr>
                                    <td style="width: 50%; padding-left: 22px;">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label class="control-label" id="lbl_cust_name">Customer Name</label>
                                                    <span style="color: Red">*</span>
                                                    <select class="form-control" id="Customer_Name" style="width: 110%;">
                                                        <option value="0">Select Customer Name</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label class="control-label Selyear" for="focusedInput"></label>
                                                    <span style="color: Red">*</span>
                                                    <%--      <select id="Year_id" class="Year_selected" style="width: 100%;">
                                            <option value="0">Select Financial Year</option>
                                        </select>--%>
                                                    <input type="text" id="Year_id" class="form-control Year_selected" autocomplete="off" readonly />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="padding: 0px 0px 0px 19px;">
                                            <div class="card" style="width: 97%; border: 0.5px solid #00bfff63; background: #007eff12;">
                                                <div class="row">
                                                    <div class="col-md-5">
                                                        <div class="form-group">
                                                            <label style="float: right">Billed Amount    :</label>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-5" style="height: 50px;">
                                                        <div class="form-group">
                                                            <label style="float: right;" id="lbl_billed_amt"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-5">
                                                        <div class="form-group">
                                                            <label style="float: right;">Paid Amount  :</label>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-5" style="height: 50px;">
                                                        <div class="form-group">
                                                            <label style="float: right;" id="lbl_paid_amt"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-5">
                                                        <div class="form-group">
                                                            <label style="float: right;">Pending Amount :</label>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-5" style="height: 50px; border-top: 1px solid black; border-bottom: 1px solid black;">
                                                        <div class="form-group">
                                                            <label style="float: right;" id="lbl_pending_amt"></label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-5">
                                                        <div class="form-group">
                                                            <label style="float: right;">Previous Advance Amount:</label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-5" style="height: 50px;">
                                                        <div class="form-group">
                                                            <label style="float: right;" id="lbl_adv_amt"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-5">
                                                        <div class="form-group">
                                                            <label style="float: right;">Received Amount :</label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-5" style="height: 50px;">
                                                        <div class="form-group">
                                                            <input type="text" class="form-control" autocomplete="off" id="txt_rec_amt" style="text-align: right; padding-right: 6px;" />
                                                            <input type="hidden" id="hdn_val" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-5">
                                                        <div class="form-group">
                                                            <label style="float: right;">Total Amount :</label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-5" style="height: 50px;">
                                                        <div class="form-group">
                                                            <label style="float: right;" id="lbl_tot_amt"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="row" style="margin-top: -14px;">

                                            <%-- <div class="col-md-6">
                                                <div class="form-group">
                                                    <label class="control-label">Received Amount</label>
                                                    <span style="color: Red">*</span>
                                                    <input type="text" class="form-control" autocomplete="off" id="txt_rec_amt" />
                                                    <input type="hidden" id="hdn_val" />
                                                </div>
                                            </div>--%>

                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label class="control-label">Remaining Advance Amount </label>

                                                </div>
                                            </div>
                                            <div class="col-md-5">
                                                <div class="form-group">
                                                    <input type="text" id="adv_amt" readonly class="form-control" style="text-align: right; padding-right: 10px; margin-left: -45px" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label class="control-label">Payment Mode </label>
                                                    <span style="color: Red">*</span>
                                                    <select class="form-control" id="pay_mode" style="width: 110%;">
                                                    </select>
                                                </div>
                                            </div>

                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label class="control-label">Collected By </label>
                                                    <span style="color: Red">*</span>
                                                    <select class="form-control" id="colled_by" style="width: 110%;">
                                                        <option value="0">Select</option>
                                                    </select>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="row ref_row">

                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label class="control-label">Reference No : </label>
                                                    <span style="color: Red">*</span>
                                                    <input type="text" class="form-control" autocomplete="off" id="ref_no" />
                                                </div>
                                            </div>

                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label class="control-label">Bank Name : </label>
                                                    <span style="color: Red">*</span>
                                                    <input type="text" class="form-control" autocomplete="off" id="bank_name" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">


                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label class="control-label">Pay Date : </label>
                                                    <span style="color: Red">*</span>
                                                    <input type="date" class="form-control" autocomplete="off" id="datepicker2" />
                                                </div>
                                            </div>

                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label class="control-label">Remark : </label>
                                                    <textarea class="form-control" rows="1" cols="100" id="txt_remark"></textarea>
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                    <td style="vertical-align: top; padding-left: 22px;" rowspan="7">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td style="padding-top: 10px;">
                                                        <div class="card pending_bill" style="max-height: 400px; overflow-y: scroll; margin-top: 0px; max-width: 509px;">
                                                            <div class="card-body table-responsive" style="border: 0.5px solid #00bfff63; background: #007eff12;padding-left: 5px;">
                                                                <table id="pending_bill_table" class="table table-hover" style="font-size: 11px;">
                                                                    <thead class="text-warning" style="white-space: nowrap;">
                                                                        <tr>
                                                                            <th style="width: 11%;">Sl No.</th>
                                                                            <th style="width: 20%;">Bill No</th>
                                                                            <th style="width: 17%;">Date</th>
                                                                            <th>Billed Amt</th>
                                                                            <th>Pending Amt</th>
                                                                            <th style="text-align: center">Amt</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="card pending_bill" style="max-width: 509px;">
                                                            <div class="card-body table-responsive" style="border: 0.5px solid #00bfff63; background: #007eff12;">
                                                                <table id="credit_tbl" class="table table-hover">
                                                                    <thead>
                                                                        <tr>
                                                                            <th style="width: 11%;"></th>
                                                                            <th>Credit Amount</th>
                                                                            <th>Adjust Amount</th>
                                                                            <th>Balance Amount</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                    </tbody>
                                                                    <tfoot></tfoot>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>

                    <div class="row">
                        <div style="margin-bottom: 30px; padding-right: 50px; float: right;">
                            <input type="button" class="btn btn-primary save" value="Save" style="width: 100px;" />
                            <input type="button" class="btn btn-primary adjust" value="Adjust Credit" style="width: 150px;" />
                        </div>
                    </div>
                </div>

                <div id="Advance_given" class=" card tab-pane fade">
                    <div class="table-responsive">

                        <div class="row" style="padding-left: 22px;">
                            <div class="col-md-4">
                                <div class="form-group pd">
                                    <label class="control-label" id="lbl_cust_name_for_adv">Customer Name</label>
                                    <span style="color: Red">*</span>
                                    <select class="form-control" id="Customer_Name_for_adv" style="width: 110%;">
                                        <option value="0">Select Customer Name</option>
                                    </select>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class="control-label" id="lbl_given_adv_amt">Advance Amount</label>
                                    <span style="color: Red">*</span>
                                    <input type="text" id="given_adv_amt" class="form-control" autocomplete="off" />
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class="control-label" id="lbl_date">Date</label>
                                    <input type="date" readonly="readonly" class="form-control" id="adv_date" />
                                </div>
                            </div>
                        </div>

                        <div class="row" style="padding-left: 22px;">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class="control-label">Total Advance Amount</label>
                                    <span style="color: Red">*</span>
                                    <input type="text" id="tot_adv_amt_val" readonly="readonly" class="form-control" autocomplete="off" />
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="form-group" style="padding: 50px 0px 0px 0px;">
                                    <input type="button" id="btn_adv_save" class="btn btn-primary btn_adv_save" value="Save" style="width: 97px;" />
                                </div>
                            </div>
                        </div>

                        <div class="card" style="width: 95%; margin: auto; margin-bottom: 16px;">
                            <div class="card-body table-responsive" style="background: floralwhite;">
                                <table class="table table-hover" id="Ret_adv_tbl">
                                    <thead class="text-warning">
                                        <tr>
                                            <th id="Cust_Name">Customer Name</th>
                                            <th id="Advance_amount">Advance Amount</th>
                                            <th id="eDate">Date</th>
                                            <th id="Total">EntryFrom</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
    </form>

</asp:Content>

