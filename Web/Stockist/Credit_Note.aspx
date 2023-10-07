<%@ Page Title="" Language="C#" MasterPageFile="~/Master_DIS.master" AutoEventWireup="true" CodeFile="Credit_Note.aspx.cs" Inherits="Stockist_Credit_Note" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <html>
    <head>
        <title>Credit Note</title>
        <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
        <link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" />
        <script type="text/javascript" src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    </head>
    <body>
        <style type="text/css">
            .fixed {
                position: fixed;
                width: 71%;
                bottom: 10px;
            }
        </style>
        <script type="text/javascript">

            $(document).ready(function () {

                $('.address').hide();

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
                $("#datepicker").val(today);

                AllCustomer = []; Inv_Order_no = []; var multivalue = []; var All_Product = []; var Allrate = []; var All_Sales_man = []; var mainarr = []; var detailsdata = []; var Pre_product = [];

                var div_code = ("<%=Session["div_code"].ToString()%>");
                var stockistcode = ("<%=Session["Sf_Code"].ToString()%>");
                var stockistname = ("<%=Session["sf_name"].ToString()%>");

                $(document).on('keypress', '#Credit_remarks', function (e) {
                    if (e.keyCode == 34 || e.keyCode == 39 || e.keyCode == 38 || e.keyCode == 60 || e.keyCode == 62 || e.keyCode == 92) return false;
                });

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Credit_Note.aspx/GetProductDetails",
                    dataType: "json",
                    success: function (data) {
                        All_Product = JSON.parse(data.d);
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Credit_Note.aspx/Get_SalesMan",
                    dataType: "json",
                    success: function (data) {
                        All_Sales_man = JSON.parse(data.d);
                        $('#sales_person').empty();
                        for (var b = 0; All_Sales_man.length > b; b++) {
                            if (All_Sales_man[b].Code == "<%=Session["Sf_Code"].ToString()%>") {

                                $('#sales_person').append($("<option selected></option>").val(All_Sales_man[b].Code).html(All_Sales_man[b].Name)).trigger('chosen:updated').css("width", "100%");;;
                            }
                            else {
                                $('#sales_person').append($("<option></option>").val(All_Sales_man[b].Code).html(All_Sales_man[b].Name)).trigger('chosen:updated').css("width", "100%");;;
                            }
                        }
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
                $('#sales_person').chosen();

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    data: "{'Div_Code':'" + div_code + "','Stockist_Code':'" + stockistcode + "'}",
                    url: "Credit_Note.aspx/getreate",
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
                    url: "Credit_Note.aspx/Get_Inv_customer",
                    dataType: "json",
                    success: function (data) {
                        AllCustomer = JSON.parse(data.d);
                        for (var b = 0; AllCustomer.length > b; b++) {
                            $('#Cust_id').append($("<option></option>").val(AllCustomer[b].Code).html(AllCustomer[b].Name)).trigger('chosen:updated').css("width", "100%");;;
                        }
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
                $('#Cust_id').chosen();

                $('#Cust_id').change(function () {

                    var tbl = $('#tblCustomers');
                    $(tbl).find('tbody tr').remove();

                    var tbl2 = $('#tbl_previous_order');
                    $(tbl2).find('tbody tr').remove();

                    $('#credit_no').val('');
                    $('#Credit_remarks').val('');
                    $('#sub_tot').val('');


                    retailer_ID = $(this).children("option:selected").val();

                    var address_filter = [];

                    address_filter = AllCustomer.filter(function (f) {
                        return (retailer_ID == f.Code)
                    })

                    // var address_split = address_filter[0].ListedDr_Address1
                    $('.address').show();
                    // $(document).find('#lbl_bind_bill_add').text(address_filter[0].ListedDr_Address1);
                    //$(document).find('#lbl_bind_ship_add').text(address_filter[0].ListedDr_Address1);

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        data: "{'Retailer_ID':'" + retailer_ID + "'}",
                        url: "Credit_Note.aspx/Get_cust_inv_no",
                        dataType: "json",
                        success: function (data) {
                            Inv_Order_no = JSON.parse(data.d);
                            $('#Inv_no_chzn').remove();
                            $('#Inv_no').removeClass('chzn-done');
                            $('#Inv_no').empty();
                            $('#Inv_no').append('<option value="0">Select Reference No</option>');
                            for (var b = 0; Inv_Order_no.length > b; b++) {
                                $('#Inv_no').append($("<option></option>").val(Inv_Order_no[b].Trans_Inv_Slno).html(Inv_Order_no[b].Trans_Inv_Slno_date)).trigger('chosen:updated').css("width", "100%");;;
                            }
                        },
                        error: function (result) {
                            alert(JSON.stringify(result));
                        }
                    });
                    $('#Inv_no').chosen({ max_selected_options: 5 });


                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        data: "{'Cust_ID':'" + retailer_ID + "'}",
                        url: "Credit_Note.aspx/Get_Pre_credit_details",
                        dataType: "json",
                        success: function (data) {
                            var Pre_product = JSON.parse(data.d);
                            var tbl = $('#tbl_previous_order');
                            $(tbl).find('tbody tr').remove();
                            for (var i = 0; i < Pre_product.length; i++) {
                                var str = "<tr><td style='display:none;'>" + Pre_product[i].Cr_No + "</td><td id='pre_id'>" + Pre_product[i].Credit_note_no + "</td>";
                                str += "<td>" + Pre_product[i].Inv_No + "</td>";
                                str += "<td>" + Pre_product[i].dat + "</td>";
                                str += "<td>" + parseFloat(Pre_product[i].Amount).toFixed(2) + "</td><td class='Click_view' style='cursor: pointer;' id=" + Pre_product[i].Cr_No + " >View</td></tr>";
                                $('#tbl_previous_order tbody').append(str);
                            }
                        },
                        error: function (result) {
                            alert(JSON.stringify(result));
                        }
                    });
                });

                $(document).on('click', '.Click_view', function () {

                    var cr_no = this.id;

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        data: "{'Credit_no':'" + cr_no + "'}",
                        url: "Credit_Note.aspx/View_credit_details",
                        dataType: "json",
                        success: function (data) {
                            var view_credit = JSON.parse(data.d);
                            $('#exampleModal').modal('toggle');
                            var tbl = $('#Credit_View');
                            $(tbl).find('tbody tr').remove();

                            for (var i = 0; i < view_credit.length; i++) {

                                if (view_credit[i].product_unit == view_credit[i].Unit) {

                                    var res = view_credit[i].Price / view_credit[i].Sample_Erp_Code;
                                }
                                else {
                                    var res = view_credit[i].Price
                                }

                                var str = "<tr><td style='display:none;'>" + view_credit[i].Cr_No + "</td><td>" + (i + 1) + "</td>";
                                str += "<td>" + view_credit[i].Product_Code + "</td>";
                                str += "<td>" + view_credit[i].Product_Name + "</td>";
                                str += "<td>" + view_credit[i].Unit + "</td><td>" + parseFloat(view_credit[i].Price).toFixed(2) + "</td><td>" + parseFloat(res).toFixed(2) + "</td><td>" + view_credit[i].qty + "</td><td>" + parseFloat(view_credit[i].Amount).toFixed(2) + "</td></tr>";
                                $('#Credit_View tbody').append(str);
                            }
                        },
                        error: function (result) {
                            alert(JSON.stringify(result));
                        }
                    });
                });



                $('#Inv_no').change(function () {

                    var selected_inv_no = $(this).children("option:selected").val();
                    var selected_cust = $('#Cust_id option:selected').val();

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        data: "{'inv_no':'" + selected_inv_no + "'}",
                        url: "Credit_Note.aspx/Get_invoice_pro_details",
                        dataType: "json",
                        success: function (data) {
                            var inv_product = JSON.parse(data.d);
                            var tbl = $('#tblCustomers');
                            $(tbl).find('tbody tr').remove();
                            $('#sub_tot').val('');

                            // bindunit1 = {};
                            // bindunit1 = '<option value="0">Select Unit</option>';
                            // bindunit1 += `<option  value=${"Broken"}>${"Broken"}</option>`;
                            //  bindunit1 += `<option  value=${"Damage"}>${"Damage"}</option>`;



                            for (var i = 0; i < inv_product.length; i++) {


                                var ans7 = [];
                                ans7 = Allrate.filter(function (t) {
                                    return (t.Product_Detail_Code == inv_product[i].Product_Code);
                                })

                                if (ans7[0].product_unit == inv_product[i].Unit) {

                                    var conv_cal_qty = inv_product[i].Order_qty * inv_product[i].Sample_Erp_Code;
                                }
                                else {

                                    var conv_cal_qty = inv_product[i].Order_qty;

                                }

                                var str = "<tr id='Cust_tr'><td>" + (i + 1) + "</td>";
                                str += "<td><input class='form-control autoc' type='text' style='display:none' data-validation='required' id='Item_1' t='" + inv_product[i].Product_Name + "'  value='" + inv_product[i].Product_Code + "' name='item_name[]' >" + inv_product[i].Product_Name + "<input type ='hidden' id ='Hid_Pro_code' class='rrr' name ='Hid_Pro_code' value=" + inv_product[i].Product_Code + "><input type='hidden' id='pro_sale_code' value=" + inv_product[i].Sale_Erp_Code + "><input type='hidden' id='Hid_order_code'  name='Hid_order_code' value=" + inv_product[i].Trans_Inv_Slno + " ><input type='hidden' id='hd_tax_value' class='hd_tax_value' value=" + inv_product[i].Tax_Val + "><br /><label style='padding-top: 8px;' class='lbl_Tax' id='id_tax'>Tax : " + inv_product[i].Tax_Name + "</label><label style='float:right; padding-top:10px; font-size: 12px;' class='lbl_con_value' value=" + inv_product[i].Sample_Erp_Code + " id='lbl_con_value'>Conv_Qty : " + inv_product[i].Sample_Erp_Code + "</label><input type='hidden' id='tax_div_value' /><input type='hidden' id='tax_id' /></td >";
                                str += "<td id='un'>" + inv_product[i].Unit + "</td>";
                                str += "<td><input class='form-control' type='text' id='Price_' name='price[]' data-cell='C1' data-format='0.00' data-validation='required' readonly=''  value=" + inv_product[i].Price + " /><input type='hidden' name='stkval' stkgood='0' stkdamage='0' /></td>";
                                str += "<td><input class='form-control validate' type='text' data-validation='required' id='english' autocomplete='off' readonly='' name='quantity[]' data-cell='D1' data-format='0' value=" + inv_product[i].Order_qty + " /></td>";
                                str += "<td style='display:none';><input type='hidden' id='tot_con_qty' value=" + conv_cal_qty + " />" + conv_cal_qty + "</td>";
                                str += "<td><input class='form-control' type='text' id='total' value=" + inv_product[i].Amount + " data-cell='E1' data-format='0.00' readonly  /><input type ='hidden' id ='row_tot' class='row_tot' name ='row_tot' ><input type ='hidden' id ='type' class='type' name ='type' value='Order'></td>";
                                str += "<td><input class='form-control type='text' data-validation='required' id='credit_qty' autocomplete='off' readonly='' value=" + ((inv_product[i].Credit_qty != '') ? inv_product[i].Credit_qty : 0) + " /></td>";
                                //  str += "<td><input class='form-control validate' type='text' autocomplete='off' id='Dis' name='dis[]' readonly='' data-cell='Y1' data-format='0' data-validation='required' value=" + inv_product[i].Discount + " >";
                                // <input class='form-control hid' type='hidden' id='hid_dis' value=" + dis_val.toFixed(2) + "> </td>
                                //str += "<td><input class='form-control validate' type='text' id='Free' autocomplete='off' name='free[]' readonly='' data-cell='I1' data-format='0'  data-validation='required' value=" + inv_product[i].Free + " ></td><td style='display:none' ><input type='hidden' class='of_pro_name'  name='of_pro_name' value='" + inv_product[i].Off_Pro_Name + "' ></td><td style='display:none' ><input type='hidden' class='of_pro_code'  name='of_pro_code' value=" + inv_product[i].Off_Pro_Code + " ></td><td style='display:none;'<label id=" + inv_product[i].Product_Code + " name='free' class='fre' freecqty=" + inv_product[i].Free + " cqty=" + inv_product[i].Quantity + " freepro=" + inv_product[i].Product_Code + " unit=" + inv_product[i].Off_Pro_Unit + ">" + inv_product[i].Free + '' + inv_product[i].Off_Pro_Unit + "</lable></td> <td style='display:none' ><input type='hidden' id=" + inv_product[i].Product_Code + " class='fre1' name='fre1' >" + inv_product[i].Free + "</td><td style='display:none' ><input type='hidden' id='erp_code' class='erp_code' value=" + inv_product[i].Sample_Erp_Code + " </td>";                              
                                str += "<td><input class='form-control' type='text' autocomplete='off' readonly='' id='credit_amt' name='credit_amt' value='" + inv_product[i].Credit_amt + "'></td>";
                                str += "<td><input class='form-control rec_qty' type='text' autocomplete='off' id='Ret_qty' data-validation='required' name='rec_qty'</td>";
                                str += "<td><input class='form-control rec_amt' type='text' autocomplete='off' readonly='' id='Ret_amt' name='rec_amt'</td><td style='display:none';><input type='hidden' id='txt_value'></td><td id='erp_code' style='display:none;'>" + inv_product[i].Sample_Erp_Code + "</td></tr>";

                                // str += `<td><select class='form-control Reason' name='Reason' >${bindunit1}</select></td>`;
                                $('#tblCustomers tbody').append(str);
                            }
                        },
                        error: function (result) {
                            alert(JSON.stringify(result));
                        }
                    });
                });


                //$('[id*=Add]').on('click', function () {

                //    itm = {}
                //    itm.Product_Code = '';
                //    itm.Product_Name = '';
                //    itm.Unit = '0';
                //    multivalue.push(itm);

                //    count = $("#tblCustomers >tbody >tr").length + 1;
                //    var data = "<tr class='Cust_tr'><td><input type='checkbox' class='case' /></td><td><span id='snum'>" + count + ".</span></td>";
                //    // < td > 
                //    // data += "<td><input class='form-control autoc' type='text' style='display:none' data-validation='required' id='Item_1' name='item_name[]' ><select class='form-control item' id='Item' name='item_name[]' style='width: 100%;'><option value='0'>---Select Product Name---</option></select><input type ='hidden' id ='Hid_Pro_code' class='rrr' name ='Hid_Pro_code'><input type='hidden' id='pro_sale_code' class='pro_sale_code'><input type='hidden' id='hd_tax_value' class='hd_tax_value' /><label style='padding-top:10px; font-size: 12px;' class='lbl_Tax' id='id_tax'></label><label style='float:right; padding-top:10px; font-size: 12px;' class='lbl_tax_value' id='lbl_tax_value'></label><input type='hidden' id='tax_div_value' /><input type='hidden' id='tax_id' /></td> " +
                //    data += "<td><input class='form-control autoc' type='text' style='display:none' data-validation='required' id='Item_1' name='item_name[]' ><select class='form-control item' id='Item" + count + "' name='item_name[]' style='width: 100%;'><option value='0'>---Select Product Name---</option></select><input type ='hidden' id ='Hid_Pro_code' class='rrr' name ='Hid_Pro_code'><input type='hidden' id='pro_sale_code' class='pro_sale_code'><input type='hidden' id='hd_tax_value' class='hd_tax_value' /><label style='padding-top:10px; font-size: 12px;' class='lbl_Tax' id='id_tax'></label><label style='float:right; padding-top:10px; font-size: 12px;' class='lbl_tax_value' id='lbl_tax_value'></label><input type='hidden' id='tax_div_value' /><input type='hidden' id='tax_id' /></td> " +
                //        // < input type = 'hidden' id = 'hide' class='hide' /> 
                //        "<td id='sel_unit'><select id='unit' class='form-control unit'><option value='0'>Select</option></select></td>" +
                //        "<td><input class='form-control' data-validation='required' autocomplete='off' type='text' id='Price_' readonly='' name='price[]'  data-cell='C" + count + "' data-format='0.00' /></td> " +
                //        // "<td></td>" +
                //        //  "<td><input class='form-control validate' type='text' data-validation='required' autocomplete='off' id='Order_qty' autocomplete='off'  name='Order_quantity[]' data-cell='K" + count + "' data-format='0' /></td>" +
                //        "<td><input class='form-control validate' data-validation='required' autocomplete='off' type='text' id='english' name='quantity[]'  data-cell='D" + count + "' data-format='0' /></td> " +
                //        //  "<td><input class='form-control validate' data-validation='required' autocomplete='off' type='text' id='Dis' name='dis[]'  data-cell='Y" + count + "' data-format='0' /><input class='form-control hid' type='hidden' id='hid_dis'></td> " +
                //        //   "<td><input class='form-control validate' data-validation='required' autocomplete='off' type='text' id='Free' name='free[]'  data-cell='I" + count + "' data-format='0' /></td><td style='display:none' ><input type='hidden' class='of_pro_name'  name='of_pro_name' ></td><td style='display:none' ><input type='hidden' class='of_pro_code'  name='of_pro_code' ></td><td style='display:none;'<label name='free' class='fre'></lable></td><td style='display:none' ><input type='hidden' class='fre1' name='fre1' ></td><td style='display:none' ><input type='hidden' id='erp_code' class='erp_code' ></td>" +
                //        // "<td><input class='form-control validate' data-validation='required' autocomplete='off' type='text' id='Free' name='free[]'  data-cell='I" + i + "' data-format='0' /></td><td><input type='hidden' id='of_pro_code' class='of_pro_code' ></td><td><input type='hidden' id='of_pro_unit' class='of_pro_unit'></td><td><input type='hidden' id='of_pro_name' class='of_pro_name' ></td> " +
                //        "<td><input class='form-control' id='total' autocomplete='off' readonly name='total[]' type='text' data-format='0.00' data-cell='E" + count + "'   /></td><td style='display:none;' ><input type='hidden' class='erp_code' /></td>" +
                //        "</tr>";
                //    $('#tblCustomers').append(data);
                //    d(count);
                //    $('#Item' + count + '').chosen();
                //    //$('#unit').chosen();
                //});

                function d(CountValue) {

                    for (var b = 0; All_Product.length > b; b++) {
                        $('#Item' + CountValue + '').append($("<option></option>").val(All_Product[b].Product_Detail_Code).html(All_Product[b].Product_Detail_Name)).trigger('chosen:updated').css("width", "100%");;;
                    }
                }

                //$("#tblCustomers >tbody").each(function () {

                //    var tis_val = $(this).length;

                //    itm = {}
                //    itm.Product_Code = '';
                //    itm.Product_Name = '';
                //    itm.Unit = '0';
                //    multivalue.push(itm);

                //    var data = "<tr class='Cust_tr'><td><input type='checkbox' class='case'/></td><td><span id='snum'>" + tis_val + ".</span></td>";
                //    // "<tr class='Cust_tr'>
                //    //  data += "<td><select class='form-control item' id='Item" + tis_val + "' name='item_name[]' style='width: 100%;'><option value='0'>---Select Product Name---</option></select><input type='hidden' id='hide' class='hide' /><input type='hidden' id='hd_tax_value' class='hd_tax_value' /><label style='padding-top:10px; font-size: 12px;' class='lbl_Tax' id='id_tax'></label><label  style='float:right; padding-top:10px; font-size: 12px;' class='lbl_tax_value' id='lbl_tax_value'></label><input type='hidden' id='tax_div_value' /><input type='hidden' id='tax_id' /></td> " +
                //    data += "<td><input class='form-control autoc' type='text' style='display:none' data-validation='required' id='Item_1' name='item_name[]' ><select class='form-control item' id='Item" + tis_val + "' name='item_name[]' style='width: 100%;'><option value='0'>---Select Product Name---</option></select><input type ='hidden' id ='Hid_Pro_code' class='rrr' name ='Hid_Pro_code'><input type='hidden' id='pro_sale_code' class='pro_sale_code'><input type='hidden' id='hd_tax_value' class='hd_tax_value' /><label style='padding-top:10px; font-size: 12px;' class='lbl_Tax' id='id_tax'></label><label style='float:right; padding-top:10px; font-size: 12px;' class='lbl_tax_value' id='lbl_tax_value'></label><input type='hidden' id='tax_div_value' /><input type='hidden' id='tax_id' /></td> " +
                //        "<td style='display:none;' ><input type='hidden' class='sale_code' /></td><td style='display:none;'><label id='pcode' class='pcode'></label></td><td style='display:none;'><input type='hidden' class='erp_code'></td>" +
                //        "<td id='sel_unit'><select id='unit' class='form-control unit'><option>Select</option></select></td>" +
                //        // "<td id='td_unit'><input class='form-control' data-validation='required' autocomplete='off' type='text' id='Unit' name='Unit[]'  data-cell='U' data-format='0.00' /></td> " +
                //        "<td><input class='form-control' data-validation='required' autocomplete='off' readonly type='text' id='Price_' name='price[]'  data-cell='C' data-format='0.00' /></td> " +
                //        "<td><input class='form-control' data-validation='required'  autocomplete='off' type='text' id='english' name='quantity[]'  data-cell='D' data-format='0' /></td> " +
                //        // "<td><input class='form-control' data-validation='required'  autocomplete='off' type='text' id='Dis' name='dis[]'  data-cell='Y' data-format='0' /><input class='form-control hid' type='hidden' id='hid_dis'></td> " +
                //        // "<td><input class='form-control' data-validation='required'  autocomplete='off' type='text' id='Free' name='free[]'  data-cell='I' data-format='0' /></td> " +
                //        //  "<td style='display: none;'><label name='free' class='fre'></td><td style='display:none;'><input type='hidden' class='fre1' name='fre1'></td>" +
                //        //  "<td style='display:none'><input type='hidden' class='of_pro_name' name='of_pro_name'></td><td style='display:none'><input type='hidden' class='of_pro_code' name='of_pro_code'></td>" +
                //        "<td><input class='form-control' id='total' name='total[]' readonly  autocomplete='off' type='text' data-format='0.00' data-cell='E'   /></td><td style='display:none;' ><input type='hidden' class='erp_code' /></td> " +
                //        "</tr> ";
                //    $('#tblCustomers').append(data);
                //    d(tis_val);
                //    $('#Item' + tis_val + '').chosen();
                //});

                $(document).on("change", ".item", function () {

                    var product_name = $(this).children("option:selected").text();
                    var product_code = $(this).children("option:selected").val();
                    var row = $(this).closest("tr");
                    var indx = $(row).index();

                    var Pro_filter = [];

                    Pro_filter = All_Product.filter(function (t) {
                        return (t.Product_Detail_Code == product_code);
                    });

                    if (Pro_filter.length > 0) {

                        var ddlUnit = row.find('.unit');
                        ddlUnit.empty();

                        bindunit1 = {};
                        bindunit1 = '<option value="0">Select Unit</option>';
                        bindunit1 += '<option  value=' + Pro_filter[0].product_unit + '>' + Pro_filter[0].product_unit + '</option>';
                        bindunit1 += '<option value=' + Pro_filter[0].Product_Sale_Unit + '>' + Pro_filter[0].Product_Sale_Unit + '</option>';

                        row.find('.unit').append(bindunit1);
                        row.find('#pro_sale_code').val(Pro_filter[0].Sale_Erp_Code);
                        row.find('#Price_').val('');
                        row.find('#Dis').val('');
                        row.find('#Free').val('');
                        row.find('#total').val('');
                        row.find('#english').val('');
                        row.find('#Order_qty').val('');
                        row.find('#hd_tax_value').val(Pro_filter[0].Tax_Val);
                        row.find('#spn_tax_details').val(Pro_filter[0].Tax_Name);
                        row.find('#tax_id').val(Pro_filter[0].Tax_Id);
                        row.find('#id_tax').text('Tax :' + ' ' + (Pro_filter[0].Tax_Name || 0));
                        row.find('#hd_tax_value').val(Pro_filter[0].Tax_Val);
                        row.find('#erp_code').val(Pro_filter[0].Sample_Erp_Code);
                        row.find('.autoc').val(Pro_filter[0].Product_Detail_Code);
                        row.find('.autoc').attr('t', Pro_filter[0].Product_Detail_Name);

                        // getscheme(row.find('#pro_sale_code').val(), row.find('#english').val(), '', row.find('.unit :selected').val(), row, row.find(".fre1").text(), row.find('#Hid_Pro_code').val(), indx, product_name);
                        row.find('#Hid_Pro_code').val(Pro_filter[0].Product_Detail_Code);

                        //  multivalue[indx].Trans_Order_No = 0;
                        //  multivalue[indx].Product_Code = data.d[0].product_Detail_Code;
                        //  multivalue[indx].Sale_Code = Pro_filter[0].Sale_Erp_Code;
                        //  multivalue[indx].Product_Name = data.d[0].Product_Detail_Name;
                        //  multivalue[indx].Tax_Id = Pro_filter[0].Tax_Id;
                        //  multivalue[indx].Tax_Val = Pro_filter[0].Tax_Val;
                        // multivalue[indx].E_Code = Pro_filter[0].Sample_Erp_Code;
                        // multivalue[indx].Tax_Name = Pro_filter[0].Tax_Name;
                        // qty_fun(row);
                        //get();
                    }

                });


                $(document).on('change', '.unit', function () {

                    var Selected_Unit = $(this).val();
                    var row = $(this).closest("tr");
                    var indx = $(row).index();

                    if (Selected_Unit == '0') {

                        alert('select Unit');

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
                        return (s.Product_Code == selected_pro_code && s.Unit == Selected_Unit);
                    });

                    var ans2 = [];
                    ans2 = Allrate.filter(function (t) {
                        return (t.Product_Detail_Code == selected_pro_code);
                    })

                    if (pro_filter.length > 0) {

                        alert('Product Unit Already Selected');

                        if (multivalue[indx].Unit == '0') {
                            row.find('.unit').val('0')
                        }
                        else {
                            row.find('.unit').val(multivalue[indx].Unit);
                        }
                        return false;
                    }
                    else {

                        multivalue[indx].Product_Code = selected_pro_code;
                        // multivalue[indx].Product_Name = selected_pro_name;
                        multivalue[indx].Unit = Selected_Unit;

                        if (ans2.length > 0) {

                            if (ans2[0].product_unit == Selected_Unit) {

                                row.find('#Price_').val(ans2[0].Distributor_Price);
                                multivalue[indx].Rate = ans2[0].Distributor_Price;
                                multivalue[indx].Unit = Selected_Unit;
                                //   qty_fun(row);
                                //   get();
                            }
                            else {
                                row.find('#Price_').val(ans2[0].MRP_Price);
                                multivalue[indx].Rate = ans2[0].MRP_Price;
                                multivalue[indx].Unit = Selected_Unit;
                                //  qty_fun(row);
                                //  get();
                            }
                        }
                    }
                    all();
                });

                $(".delete").on('click', function () {
                    $(".case:checked").each(function () {
                        row = $(this).closest('tr');

                        var order_id = row.find('#Hid_order_code').val();
                        var prod = row.find('#Hid_Pro_code').val();

                        idx = $(row).index();
                        multivalue.splice(idx, 1);
                        //  getscheme(row.find('#pro_sale_code').val(), '', '', row.find('.unit :selected').val(), row, row.find('.fre').attr('freecqty'), row.find('#Hid_Pro_code').val(), idx, row.find(".autoc").text());

                        $('.case:checkbox:checked').parents("tr").remove();
                        //$('.case:checkbox:checked').remove();
                        $('.check_all').prop("checked", false);
                        //check();
                    });
                });
                //$(document).on('keyup', '[id*=english]', function () {

                //    var row = $(this).closest("tr");
                //    idx = $(row).index();
                //    var total = row.find('[id*=Price_]').val() * row.find('[id*=english]').val();
                //    row.find('[id*=total]').val(total.toFixed(2));
                //    all();
                //});

                $(document).on('keyup paste', '[id*=Ret_qty]', function (e) {
                    this.value = this.value.replace(/[^0-9]/g, '');
                    var row = $(this).closest("tr");
                    idx = $(row).index();

                    //if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    //  row.find('[id*=Ret_qty]').val('');
                    //   return false;
                    // }


                    var qty = parseFloat(row.find('[id*=Ret_qty]').val());
                    var tax = row.find('[id*=hd_tax_value]').val();
                    // var tot_con = row.find('[id*=tot_con_qty]').val();
                    var credit_qty = parseFloat(row.find('#credit_qty').val());
                    var p_code = row.find('#Hid_Pro_code').val();
                    var p_u = row.find('#un').text();



                    if (isNaN(qty) || qty == '') { qty = 0; }

                    var cal_val = qty + parseFloat(credit_qty);


                    //var cal_val = isNaN(parseFloat(qty)) == true ? 0 : parseFloat(qty) + isNaN(parseFloat(credit_qty)) == true ? 0 : parseFloat(credit_qty);



                    var ans5 = [];
                    ans5 = Allrate.filter(function (t) {
                        return (t.Product_Detail_Code == p_code);
                    })



                    // var order_qty = row.find('#english').val();
                    var order_qty = row.find('[id*=tot_con_qty]').val();

                    //  if (qty != "0") {

                    //  if (parseFloat(order_qty) >= parseFloat(cal_val)) {

                    if (parseFloat(cal_val) > parseFloat(order_qty)) {

                        row.find('[id*=Ret_qty]').val('');
                        row.find('[id*=Ret_amt]').val('');
                        all()
                        alert('Please Check the Quantity');
                        return false;
                    }
                    else {

                        if (ans5[0].product_unit == p_u) {

                            var total = parseFloat(row.find('[id*=Price_]').val()) / parseFloat(row.find('#erp_code').text()) * parseFloat(qty);
                            // row.find('[id*=txt_value]').val(total * tax / 100);
                            row.find('[id*=Ret_amt]').val(total.toFixed(2));
                            all();
                        }
                        else {

                            var total = parseFloat(row.find('[id*=Price_]').val()) * parseFloat(qty);
                            // row.find('[id*=txt_value]').val(total * tax / 100);
                            row.find('[id*=Ret_amt]').val(total.toFixed(2));
                            all();

                        }

                    }
                    //   }

                });

                function all() {

                    var Total = 0;
                    $("[id*=Ret_amt]").each(function () {

                        var row = $(this).closest("tr");
                        var this_val = $(this).val() || 0;
                        //    var tax_val = row.find('[id*=txt_value]').val() || 0;
                        //  var ta = parseFloat(this_val) + parseFloat(tax_val);

                        if (this_val != "") {
                            Total += parseFloat(this_val);
                        }
                    });
                    if (Total == '0') {

                        $("[id*=sub_tot]").val('');
                        $('[id*=Ret_amt]').val('');
                    }
                    else {
                        $("[id*=sub_tot]").val(parseFloat(Total).toFixed(2));
                    }
                }

                $(document).on('click', '.btnsave', function (e) {

                    //$(".btnsave").attr("disabled", true);

                    var Customer_Code = $('#Cust_id :selected').val();
                    var Customer_Name = $('#Cust_id :selected').text();

                    if (Customer_Code == "0") {
                        buttoncount = 0;
                        alert("Please Select Customer.");
                        return false;
                    }

                    var Bill_Address = $('#lbl_bind_bill_add').text();
                    var Ship_Address = $('#lbl_bind_ship_add').text();


                    var credit_no = $('#credit_no').val();
                    if (credit_no == "" || credit_no == null) {
                        buttoncount = 0;
                        alert("Enter Credit Num.");
                        return false;
                    }

                    var Credit_date = $('#datepicker').val();
                    if (Credit_date.length <= 0) {
                        buttoncount = 0;
                        approve = 0;
                        alert('Select Credit_date.!!');
                        $('#datepicker').focus();
                        return false;
                    }

                    var Inv_num = $('#Inv_no :selected').val();

                    if (Inv_num == "0") {
                        buttoncount = 0;
                        alert("Please Select Invoice Number.");
                        return false;
                    }

                    var Sales_man = $('#sales_person :selected').val();

                    if (Sales_man == "0") {
                        buttoncount = 0;
                        alert("Please Select Sales Man.");
                        return false;
                    }
                    var fff = [];
                    $(document).find("#tblCustomers tbody").find("tr").each(function () {

                        var d = $(this).closest("tr").find('#Ret_qty').val();

                        if (d != "" && d != '0' && d != 'undefined') {

                            fff.push({
                                qqty: d
                            });

                        }
                    });

                    if (fff.length < 1) {
                        alert('Please Enter Damage Quantity');
                        return false;
                    }

                    var comment = $('#Credit_remarks').val();
                    var total_amt = $('#sub_tot').val();

                    mainarr.push({
                        Inv_no: Inv_num,
                        Date: Credit_date,
                        Cust_id: Customer_Code,
                        Cust_Name: Customer_Name,
                        Amount: total_amt,
                        Remarks: comment,
                        Div_Code: div_code,
                        Sales_Man_ID: <%=Session["Sf_Code"].ToString()%>,
                        Sf_Code: <%=Session["Sf_Code"].ToString()%>,
                        Credit_note_no: credit_no
                    });

                    $(document).find("#tblCustomers tbody").find("tr").each(function () {

                        var pcode = $(this).closest("tr").find('#Hid_Pro_code').val();
                        var units = $(this).closest("tr").find('#un').text();
                        var erp_code = $(this).closest("tr").find('#erp_code').text();
                        var d_qty = $(this).closest("tr").find('#Ret_qty').val() || 0;
                        var Dam_qty = $(this).closest("tr").find('#Ret_qty').val();

                        if (Dam_qty != '' && typeof Dam_qty != 'undefined') {

                            //var ans2 = [];
                            //ans2 = Allrate.filter(function (t) {
                            //    return (t.Product_Detail_Code == pcode);
                            //})

                            //if (ans2.length > 0) {

                            //    if (ans2[0].product_unit == units) {
                            //        var conversion_qty = parseFloat(d_qty) * parseFloat(erp_code);
                            //        var non_conversion_qty = d_qty;
                            //    }
                            //    else {
                            //        var conversion_qty = d_qty;
                            //        var non_conversion_qty = d_qty;
                            //    }
                            //}

                            itm2 = {}
                            itm2.ProductCode = $(this).closest("tr").find('#Hid_Pro_code').val();
                            itm2.Productname = $(this).closest("tr").find('.autoc').attr('t') || 0;
                            itm2.Productunit = $(this).closest("tr").find('#un').text();
                            itm2.Conv_qty = d_qty;
                            itm2.Non_conv_qty = d_qty;
                            itm2.Price = $(this).closest("tr").find('#Price_').val();
                            itm2.Amt = $(this).closest("tr").find('#Ret_amt').val();
                            itm2.Inv_no = Inv_num;
                            detailsdata.push(itm2);
                        }
                    });

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        data: "{'Credit_Main':'" + JSON.stringify(mainarr) + "','Credit_Details':'" + JSON.stringify(detailsdata) + "'}",
                        url: "Credit_Note.aspx/Save_Credit",
                        dataType: "json",
                        success: function (data) {
                            $(".btnsave").attr("disabled", false);
                            if (data.d = "success") {
                                alert('Credit Note Added Successfully');
                                window.location.href = "../Stockist/Credit_Note_List.aspx";
                            }
                        },
                        error: function (result) {
                            buttoncount = 0;
                            alert(JSON.stringify(result));
                        }
                    });
                });
            });

        </script>
        <form id="form1" runat="server">
            <div class="container">
                <div class="row">
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                        <div class="form-group">
                            <label id="lbl_cust_name" class="control-label">Customer Name :</label><span style="color: Red">*</span>
                            <div class="input-group">
                                <div class="input-group-addon">
                                    <a href="#">
                                        <span class="glyphicon glyphicon-user"></span>
                                    </a>
                                </div>

                                <select id="Cust_id" class="form-control">
                                    <option value="0">Select Customer Name</option>
                                </select>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                        <div class="form-group">
                            <label id="lbl_Inv" class="control-label">Reference No :</label><span style="color: Red">*</span>
                            <select id="Inv_no" class="form-control">
                                <option value="0">Select Reference No</option>
                            </select>
                        </div>
                    </div>

                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                        <div class="form-group">
                            <label class="control-label">Sales Person :</label><span style="color: Red">*</span>
                            <div class="input-group">
                                <div class="input-group-addon">
                                    <a href="#">
                                        <span class="glyphicon glyphicon-user"></span>
                                    </a>
                                </div>
                                <select id="sales_person" class="form-control">
                                </select>
                            </div>
                        </div>
                    </div>
                </div>

                <%--                <div class="row address">
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                        <div class="form-group">
                            <label id="lbl_Bill_add" class="control-label">Billing Address :</label>
                            <address id="lbl_bind_bill_add"></address>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                        <div class="form-group">
                            <label id="lbl_ship_add" class="control-label">Shipping Address :</label>
                            <label id="lbl_bind_ship_add" style="font-weight: 100"></label>
                        </div>
                    </div>
                </div>--%>

                <div class="row">
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                        <div class="form-group">
                            <label class="control-label">Credit Note No :</label><span style="color: Red">*</span>
                            <input class="form-control" type="text" id="credit_no" style="font-weight: 100" autocomplete="off" />
                        </div>
                    </div>

                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                        <div class="form-group">
                            <label class="control-label">Credit Note Date :</label><span style="color: Red">*</span>
                            <div class="input-group">
                                <div class="input-group-addon">
                                    <a href="#">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </a>
                                </div>
                                <input class="form-control" id="datepicker" data-validation="required" type="date" autocomplete="off" />
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                        <div class="form-group">
                            <label class="control-label">Remarks :</label>
                            <div class="input-group">
                                <div class="input-group-addon">
                                    <a href="#">
                                        <span class="glyphicon glyphicon-pencil"></span>
                                    </a>
                                </div>
                                <textarea rows="1" maxlength="280" id="Credit_remarks" class="form-control"></textarea>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div style="display: none;" class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                        <div class="form-group">
                            <label class="control-label">Reason :</label>
                            <select id="reason" class="form-control">
                                <option value="0">Select Reason</option>
                                <option value="1">Damage</option>
                                <option value="2">Return</option>
                            </select>
                        </div>
                    </div>

                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12" style="display: none;">
                        <div class="form-group">
                            <label class="control-label">Amount :</label>
                            <div class="input-group">
                                <div class="input-group-addon">
                                    <a href="#">
                                        <span class="fa fa-inr"></span>
                                    </a>
                                </div>
                                <input class="form-control" type="text" id="Credit_amt" autocomplete="off" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="container">
                        <div class="panel-group">
                            <div class="panel panel-default" style="overflow: inherit;">
                                <div class="panel-heading" style="background-color: #d9d9d9;">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" href="#collapse1">View Previous Credit</a>
                                    </h4>
                                </div>
                                <div id="collapse1" class="panel-collapse collapse">
                                    <table style="width: 100%;" id="tbl_previous_order" class="table table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th>Credit Note No</th>
                                                <th>Invoice No</th>
                                                <th>Credit Date</th>
                                                <th>Amount</th>
                                                <th>View</th>
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

                <div class="row" style="margin-top: 0px;">
                </div>
                <div class="row" style="display: none;">
                    <div class="col-md-2">
                        <h5>Add Items:</h5>
                    </div>
                </div>
                <div class="row" style="padding: 14px 0px 0px 0px;">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                        <table id="tblCustomers" class="table table-bordered table-hover">
                            <thead class="ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix">
                                <tr>
                                    <th width="5%">Sl.No.
                                    </th>
                                    <th width="25%">Product Name
                                    </th>
                                    <th width="5%">Unit
                                    </th>
                                    <th width="5%">Price
                                    </th>
                                    <th width="5%">Inv_Qty
                                    </th>
                                    <th width="5%">Inv_Amt
                                    </th>
                                    <th width="5%">Cre_Qty
                                    </th>
                                    <th width="5%">Cre_Amt
                                    </th>
                                    <th width="7%">D Qty(In P)
                                    </th>
                                    <th width="5%">D Amount
                                    </th>

                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </div>
                </div>
                <%--           <div class="row">
                    <div class="col-xs-12 col-sm-4 col-md-3 col-lg-3">
                        <button id="Del" type="button" class='btn btn-danger delete'>
                            - Delete</button>
                        <button id="Add" type="button" class='btn btn-success addmore'>
                            + Add More</button>
                    </div>
                </div>--%>

                <div class="col-sm-offset-8 form-horizontal">
                    <label class=" col-xs-3 control-label">
                        Subtotal :
                    </label>
                    <div class="col-sm-9">
                        <div class="input-group">
                            <div class="input-group-addon currency">
                                <i class="fa fa-inr"></i>
                            </div>
                            <input data-cell="G1" id="sub_tot" data-format="0,0.00" class="form-control" readonly />
                        </div>
                    </div>
                </div>

                <div class="row" style="text-align: center">
                    <div class="fixed">
                        <a id="btndaysave"
                            class="btn btn-primary btnsave" style="vertical-align: middle; font-size: 17px;">
                            <span>Save Credit Note</span></a>
                    </div>
                </div>

                <div class="modal fade" id="exampleModal" style="z-index: 10000000; overflow-y: auto;" tabindex="0" aria-hidden="true">
                    <div class="modal-dialog" role="document" style="width: 1200px !important">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="exampleModalLabel">Credit Note Details View</h5>
                                <button type="button" class="close" style="margin-top: -20px;" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-sm-12" style="padding: 15px">
                                        <table class="table table-hover" id="Credit_View">
                                            <thead class="text-warning">
                                                <tr>
                                                    <th style="text-align: left">S.No</th>
                                                    <th style="text-align: left">Product Code</th>
                                                    <th style="text-align: left">Product Name</th>
                                                    <th style="text-align: left">Unit</th>
                                                    <th style="text-align: left">Original Price</th>
                                                    <th style="text-align: left">Conv Price</th>
                                                    <th style="text-align: left">qty(In p)</th>
                                                    <th style="text-align: left">Amount</th>
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
                </div>

            </div>
        </form>
    </body>
    </html>

</asp:Content>

