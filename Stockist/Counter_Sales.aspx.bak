<%@ Page Title="" Language="C#" MasterPageFile="~/Master_DIS.master" AutoEventWireup="true" CodeFile="Counter_Sales.aspx.cs" Inherits="Stockist_Counter_Sales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <html>
    <head>
        <title></title>
        <link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" />
        <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />

        <style type="text/css">
            .ui-menu-item > a {
                display: block;
            }

            .fixed {
                position: fixed;
                width: 80%;
                bottom: 10px;
            }

            .example {
                display: contents;
                position: fixed;
                margin-left: 156px;
            }

            @media print {
                .hidden-print,
                .hidden-print * {
                    display: none !important;
                }
            }
        </style>

        <%--       <style type="text/css">
            * {
                font-size: 12px;
                font-family: 'Times New Roman';
            }

            td.description,
            th.description {
                width: 75px;
                max-width: 75px;
            }

            td.quantity,
            th.quantity {
                width: 40px;
                max-width: 40px;
                word-break: break-all;
            }

            td.price,
            th.price {
                width: 40px;
                max-width: 40px;
                word-break: break-all;
            }

            .centered {
                text-align: center;
                align-content: center;
            }

            .ticket {
                width: 155px;
                max-width: 155px;
            }

            img {
                max-width: inherit;
                width: inherit;
            }

            @media print {
                .hidden-print,
                .hidden-print * {
                    display: none !important;
                }
            }
        </style>--%>

        <script type="text/javascript">

            var All_Product = []; tbl_val = []; var multivalue = []; var arr = []; var CQvalue = ''; var cq = ''; Prds = ""; var count = 0; var All_unit = []; var Allrate = [];
            var trClass = ''; var stockistcode = ''; var div_code = ''; var stockistname = '';
            var Alldetails = []; str = ""; totalQnty = 0.00; totalItem = 0; totalAmount = 0.00; totalTax = 0.00; balance = 0.00; Totalval = 0.00;

            $(document).ready(function () {

                $("#btnprint").hide();
                $("#txt_mob").focus();
                $('.example').hide();
                $('#txt_ref_no').hide();
                $('.lbl_ref').hide();
                $('#spn_refer').hide();

                div_code = ("<%=Session["div_code"].ToString()%>");
                stockistcode = ("<%=Session["Sf_Code"].ToString()%>");
                stockistname = ("<%=Session["sf_name"].ToString()%>");

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
                $("#txt_invoice_date").val(today);

                $('#txt_mob').keyup(function (e) {
                    if (/\D/g.test(this.value)) {
                        this.value = this.value.replace(/\D/g, '');
                    }
                });

                $(document).on('keypress', '#notes', function (e) {
                    if (e.keyCode == 34 || e.keyCode == 39 || e.keyCode == 38 || e.keyCode == 60 || e.keyCode == 62 || e.keyCode == 92) return false;
                });

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Counter_Sales.aspx/GetProductDetails",
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
                    data: "{'Div_Code':'" + div_code + "','Stockist_Code':'" + stockistcode + "'}",
                    url: "Counter_Sales.aspx/getratenew",
                    dataType: "json",
                    success: function (data) {
                        Allrate = JSON.parse(data.d) || [];
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Counter_Sales.aspx/Get_All_Stock",
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
                    url: "Counter_Sales.aspx/Get_Product_unit",
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
                    type: "Post",
                    contentType: "application/json; charset=utf-8",
                    url: "Counter_Sales.aspx/GetPayType",
                    dataType: "json",
                    success: function (data) {
                        if (data.d.length > 0) {
                            var ddlpayterm = $("#sel_pay-term");
                            ddlpayterm.empty().append('<option selected="selected" value="0">--- Select ---</option>');
                            $.each(data.d, function () {
                                ddlpayterm.append($("<option></option>").val(this['Pay_code']).html(this['Pay_name']));
                            });
                        }
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });

                $("#tblCustomers >tbody").each(function () {

                    var tis_val = $(this).length;
                    count++;
                    itm = {}; itm.Product_Code = ''; itm.Product_Name = ''; itm.Unit = '0'; multivalue.push(itm);

                    var data = $("<tr class='Cust_tr'></tr>")
                    data.html("<td><input type='checkbox' class='case'/></td><td class='row_no'>" + tis_val + "</td><td><select class='form-control item' name='item_name[]' style='width: 100%;'><option value='0'>---Select Product Name---</option></select><input type='hidden' id='hide' class='hide' /><label style='padding-top:10px; font-size: 12px;'>CGST :</label>&nbsp&nbsp<label style='padding-top:10px; font-size: 12px;' class='lbl_cgst' id='lbl_cgst'></label>&nbsp&nbsp<input type='hidden' class='hid_cgst_val /><label style='padding-top:10px; font-size: 12px;'>SGST : </label>&nbsp&nbsp<label style='padding-top:10px; font-size:12px;' class='lbl_sgst'></label>&nbsp&nbsp<input type='hidden' class='hid_sgst_val /><label style='padding-top:10px; font-size: 12px;' class='lbl_stock' id='id_stock'>Stock :</label>&nbsp&nbsp<label  style='padding-top:10px; font-size: 12px;' class='lbl_stock_value' id='lbl_stock_value'></label>&nbsp&nbsp<label style='padding-top:10px; font-size: 12px;' >Con_Fac  :</label>&nbsp&nbsp<label  style='padding-top:10px; font-size: 12px;' class='lbl_conv' id='lbl_convid'></label><input type='hidden' id='Total_Tax_value' class='Total_Tax_value' /></td> " +
                        "<td style='display:none;' ><input type='hidden' class='sale_code' /></td><td style='display:none;'><label id='pcode' class='pcode'></label></td><td style='display:none;'><input type='hidden' class='erp_code'></td>" +
                        "<td id='sel_unit'><select id='unit' class='form-control unit'><option>Select</option></select></td>" +
                        "<td><input class='form-control' data-validation='required' autocomplete='off' readonly type='text' id='Price_' name='price[]'  data-cell='C' data-format='0.00' /></td> " +
                        "<td><input class='form-control validate' data-validation='required'  autocomplete='off' type='text' id='english' name='quantity[]'  data-cell='D' data-format='0' /></td> " +
                        "<td><input class='form-control' data-validation='required'  autocomplete='off' type='text' id='Dis' name='dis[]'  data-cell='Y' data-format='0' /></td> " +
                        "<td><input class='form-control dis_val' data-validation='required' readonly  autocomplete='off' type='text' id='dis_val' name='dis_val'  data-cell='Y' data-format='0' /></td> " +
                        "<td><input class='form-control' data-validation='required'  autocomplete='off' type='text' id='Free' name='free[]'  data-cell='I' data-format='0' /></td> " +
                        "<td style='display: none;'><label name='free' class='fre'></td><td style='display:none;'><input type='hidden' class='fre1' name='fre1'></td>" +
                        "<td><input class='form-control' id='total' name='total[]' readonly  autocomplete='off' type='text' data-format='0.00' data-cell='E' /></td><td style='display:none;' ><input type='hidden' class='erp_code' /></td> " +
                        "<td style='display:none;'><input class='form-control cgst_tax_value'type='hidden' id='cgst_tax_value' name='cgst_tax_value' /></td>" +
                        "<td style='display:none;'><input class='form-control sgst_tax_value' data-validation='required' type='hidden' id='sgst_tax_value' name='sgst_tax_value' /></td>" +
                        "<td><input class='form-control tax_value' data-validation='required' readonly autocomplete='off' type='text' id='tax_value' name='tax_value'  data-format='0' /></td>" +
                        "<td style='display:none'><input type='hidden' class='of_pro_name' name='of_pro_name'></td><td style='display:none'><input type='hidden' class='of_pro_code' name='of_pro_code'></td>" +
                        "<td><input class='form-control grs_tot' data-validation='required' readonly autocomplete='off' type='text' id='grs_tot' name='grs_tot'  data-format='0' /></td>");

                    $('#tblCustomers').append(data);
                    d(data);
                    $(data).find('.item').chosen();
                });

                function d(CountValue) {
                    for (var b = 0; All_Product.length > b; b++) {
                        $(CountValue).find('.item').append($("<option></option>").val(All_Product[b].Product_Detail_Code).html(All_Product[b].Product_Detail_Name)).trigger('chosen:updated').css("width", "100%");;;
                    }
                }

                $('[id*=Add]').on('click', function () {
                    AddRow();
                });

                function AddRow() {

                    count++;
                    itm = {}; itm.Product_Code = ''; itm.Product_Name = ''; itm.Unit = '0'; multivalue.push(itm);

                    var data = $("<tr class='Cust_tr'></tr>")
                    data.html("<td><input type='checkbox' class='case'/></td><td class='row_no'>" + count + "</td><td><select class='form-control item' name='item_name[]' style='width: 100%;'><option value='0'>---Select Product Name---</option></select><input type='hidden' id='hide' class='hide' /><label style='padding-top:10px; font-size: 12px;'>CGST :</label>&nbsp&nbsp<label style='padding-top:10px; font-size: 12px;' class='lbl_cgst' id='lbl_cgst'></label><input type='hidden' class='hid_cgst_val />&nbsp&nbsp<label style='padding-top:10px; font-size:12px;'>SGST : </label>&nbsp&nbsp<label style='padding-top:10px; font-size:12px;' class='lbl_sgst'></label>&nbsp&nbsp<input type='hidden' class='hid_sgst_val /><label style='padding-top:10px; font-size: 12px;' class='lbl_stock' id='id_stock'>Stock :</label>&nbsp&nbsp<label  style='padding-top:10px; font-size: 12px;' class='lbl_stock_value' id='lbl_stock_value'></label>&nbsp&nbsp<label style='padding-top:10px; font-size: 12px;'>Con_Fac  :</label>&nbsp&nbsp<label  style='padding-top:10px; font-size: 12px;' class='lbl_conv' id='lbl_convid'></label><input type='hidden' class='Total_Tax_value' id='Total_Tax_value' /></td> " +
                        "<td style='display:none;' ><input type='hidden' class='sale_code' /></td><td style='display:none;'><label id='pcode' class='pcode'></label></td><td style='display:none;'><input type='hidden' class='erp_code'></td>" +
                        "<td id='sel_unit'><select id='unit' class='form-control unit'><option>Select</option></select></td>" +
                        "<td><input class='form-control' data-validation='required' autocomplete='off' readonly type='text' id='Price_' name='price[]'  data-cell='C' data-format='0.00' /></td> " +
                        "<td><input class='form-control validate' data-validation='required'  autocomplete='off' type='text' id='english' name='quantity[]'  data-cell='D' data-format='0' /></td> " +
                        "<td><input class='form-control' data-validation='required'  autocomplete='off' type='text' id='Dis' name='dis[]'  data-cell='Y' data-format='0' /></td> " +
                        "<td><input class='form-control dis_val' data-validation='required' readonly  autocomplete='off' type='text' id='dis_val' name='dis_val'  data-cell='Y' data-format='0' /></td> " +
                        "<td><input class='form-control' data-validation='required'  autocomplete='off' type='text' id='Free' name='free[]'  data-cell='I' data-format='0' /></td> " +
                        "<td style='display: none;'><label name='free' class='fre'></td><td style='display:none;'><input type='hidden' class='fre1' name='fre1'></td>" +
                        "<td><input class='form-control' id='total' name='total[]' readonly  autocomplete='off' type='text' data-format='0.00' data-cell='E'   /></td><td style='display:none;' ><input type='hidden' class='erp_code' /></td> " +
                        "<td style='display:none;'><input class='form-control cgst_tax_value' type='hidden' id='cgst_tax_value' name='cgst_tax_value' /></td>" +
                        "<td style='display:none;'><input class='form-control sgst_tax_value' type='hidden' id='sgst_tax_value' name='sgst_tax_value' /></td>" +
                        "<td><input class='form-control tax_value' data-validation='required' readonly autocomplete='off' type='text' id='tax_value' name='tax_value'  data-format='0' /></td>" +
                        "<td style='display:none'><input type='hidden' class='of_pro_name' name='of_pro_name'></td><td style='display:none'><input type='hidden' class='of_pro_code' name='of_pro_code'></td>" +
                        "<td><input class='form-control grs_tot' data-validation='required' readonly autocomplete='off' type='text' id='grs_tot' name='grs_tot'  data-format='0' /></td>");

                    $('#tblCustomers').append(data);
                    d(data);
                    $(data).find('.item').chosen();
                }

                $(document).on("change", "#txt_mob", function () {

                    var Mob_No = $(this).val();

                    if (Mob_No == "") {
                        $("#txt_name").val('');
                        $("#txt_add").val('');
                        return false;
                    }
                    else {
                        $.ajax({
                            type: "Post",
                            contentType: "application/json; charset=utf-8",
                            url: "Counter_Sales.aspx/GetCustomerDetails",
                            data: "{'Mobile_No':'" + Mob_No + "'}",
                            dataType: "json",
                            success: function (data) {
                                if (data.d.length > 0) {
                                    $("hid_Cust-Code").val(data.d[0].ListedDrCode);
                                    $("#txt_name").val(data.d[0].Cust_name);
                                    $("#txt_add").val(data.d[0].Cust_Address);
                                    $("#<%= hd_cust_code.ClientID %>").val(data.d[0].Cust_code);
                                }
                                else {
                                    $("#txt_name").val('');
                                    $("#txt_add").val('');
                                }
                            },
                            error: function (result) {
                                alert(JSON.stringify(result));
                            }
                        });
                    }
                });

                $(document).on("change", ".item", function () {

                    var product_name = $(this).children("option:selected").text();
                    var product_code = $(this).children("option:selected").val();
                    var row = $(this).closest("tr");
                    var indx = $(row).index();
                    trClass = $(this).closest("tr").find(".item").val();

                    var Pro_filter = []; var filter_unit = []; units1 = "";

                    Pro_filter = Allrate.filter(function (t) {
                        return (t.Product_Detail_Code == product_code);
                    });

                    var ddlUnit = row.find('.unit');
                    ddlUnit.empty();
                    units1 += "<option value='0'>Select</option>";

                    filter_unit = All_unit.filter(function (w) {
                        return (product_code == w.Product_Detail_Code);
                    });

                    if (filter_unit.length > 0) {

                        for (var z = 0; z < filter_unit.length; z++) {

                            if (Pro_filter[0].Unit_code == filter_unit[z].Move_MailFolder_Id) {
                                units1 += "<option selected value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";
                            }
                            else {
                                units1 += "<option value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";
                            }
                        }
                    }

                    tax_filter = All_Tax.filter(function (r) {
                        return (r.Product_Detail_Code == product_code)
                    });

                    var cgst_tax_val = ''; var sgst_tax_val = '';

                    for (var z = 0; z < tax_filter.length; z++) {

                        if (tax_filter[z].Tax_Type == 'SGST') {
                            row.find('.lbl_sgst').text(tax_filter[z].Tax_name);
                            row.find('.hid_sgst_val').val(tax_filter[z].Tax_Val);
                            cgst_tax_val = tax_filter[z].Tax_Val;

                        }
                        else if (tax_filter[z].Tax_Type == 'CGST') {
                            row.find('.lbl_cgst').text(tax_filter[z].Tax_name);
                            row.find('.hid_cgst_val').val(tax_filter[z].Tax_Val);
                            sgst_tax_val = tax_filter[z].Tax_Val;
                        }
                        else if (tax_filter[z].Tax_Type == 'IGST') {
                        }
                        else {
                            row.find('.lbl_sgst').text(tax_filter[z].Tax_name);
                            row.find('.hid_sgst_val').val(tax_filter[z].Tax_Val);
                            row.find('.lbl_cgst').text(tax_filter[z].Tax_name);
                            row.find('.hid_cgst_val').val(tax_filter[z].Tax_Val);
                        }
                        row.find('.Total_Tax_value').val(parseInt(cgst_tax_val) + parseInt(sgst_tax_val));
                    }

                    row.find('.validate').removeClass('focus');
                    row.find('.validate').css('background-color', '');

                    if (Pro_filter.length > 0) {

                        $(this).closest("tr").attr('class', Pro_filter[0].Product_Detail_Code);
                        row.find('.unit').append(units1);
                        getscheme(row.find('.sale_code').text(), row.find('#english').val(), row.find('.unit :selected').val(), row, row.find(".fre1").text(), row.find('.pcode').text());
                        var crunit = parseInt(row.find('.unit :selected').val());
                        if (crunit == Pro_filter[0].UnitCode)
                            var unt_rate = Pro_filter[0].MRP_Rate;
                        else if (crunit == Pro_filter[0].Unit_code && crunit != Pro_filter[0].Base_Unit_code)
                            var unt_rate = Pro_filter[0].MRP_Rate * Pro_filter[0].Sample_Erp_Code;
                        else if (crunit == Pro_filter[0].Base_Unit_code && Pro_filter[0].UnitCode == Pro_filter[0].Unit_code)
                            var unt_rate = Pro_filter[0].MRP_Rate / Pro_filter[0].Sample_Erp_Code;
                        else
                            var unt_rate = Pro_filter[0].MRP_Rate * Pro_filter[0].Sample_Erp_Code;
                        if (unt_rate == 'null' || typeof unt_rate == "undefined" || unt_rate == "" || isNaN(unt_rate)) { unt_rate = 0; }

                        row.find('#hide').val(Pro_filter[0].Product_Detail_Code);
                        row.find('#Price_').val(unt_rate.toFixed(2));
                        row.find('#Dis').val('');
                        row.find('#Free').val('');
                        row.find('#total').val('');
                        row.find('#english').val('');
                        row.find('.sale_code').text(Pro_filter[0].Sale_Erp_Code);
                        row.find('.erp_code').val(Pro_filter[0].Sample_Erp_Code);
                        row.find('#spn_tax_details').val(Pro_filter[0].Tax_name);
                        row.find('.lbl_stock_value').text(Pro_filter[0].TotalStock);
                        row.find('.lbl_conv').text(Pro_filter[0].Sample_Erp_Code);
                        row.find('.pcode').text(product_code);
                        get()
                    }

                    else {

                        getscheme(row.find('.sale_code').text(), row.find('#english').val(), row.find('.unit :selected').val(), row, row.find(".fre1").text(), row.find('.pcode').text());
                        $(this).closest("tr").attr('class', '');
                        row.find('.unit').append(units1);
                        row.find('#hide').val('');
                        row.find('#Price_').val('');
                        row.find('#Dis').val('');
                        row.find('#Free').val('');
                        row.find('#total').val('');
                        row.find('#english').val('');
                        row.find('.sale_code').text('');
                        row.find('.erp_code').val(0);
                        row.find('#spn_tax_details').val(0);
                        row.find('.lbl_stock_value').text(0);
                        row.find('.lbl_conv').text(0);
                        row.find('.pcode').text('');
                        get();

                    }
                    setTimeout(function () { row.find('.validate').focus() }, 100);

                });


                $(document).on('change', '.unit', function () {

                    var row = $(this).closest("tr");
                    var indx = $(row).index();

                    var Selected_Unit = $(this).children("option:selected").val();
                    var selected_pro_code = $(this).closest('tr').find('.item').children("option:selected").val();
                    var selected_pro_name = $(this).closest('tr').find('.item').children("option:selected").text();
                    var un = row.find('.unit').children("option:selected").text();

                    var pro_filter = []; var ans2 = [];

                    pro_filter = multivalue.filter(function (s, key) {
                        return (s.Product_Code == selected_pro_code && s.Unit == Selected_Unit && key != indx);
                    });

                    ans2 = Allrate.filter(function (t) {
                        return (t.Product_Detail_Code == selected_pro_code);
                    });

                    if (pro_filter.length > 0) {
                        row.find('.unit').val('0');
                        row.find('#Price_').text("0.00");
                        row.find('#english').val('');
                        multivalue[indx].Unit = 'Select';
                        get();
                        alert('Unit Already Selected');
                    }
                    else {

                        multivalue[indx].Product_Code = selected_pro_code;
                        multivalue[indx].Product_Name = selected_pro_name;
                        multivalue[indx].Unit = Selected_Unit;
                        
                        var priceField = row.find('#Price_');
                        var erpCodeField = row.find('.erp_code');
                        var englishField = row.find('#english');
                        var fre1Field = row.find('.fre1');
                        var lblConvField = row.find('.lbl_conv');
                        var saleCodeField = row.find('.sale_code');
                        var unitField = row.find('.unit :selected');
                        var pcodeField = row.find('.pcode');

                       
                        function updateFields(ar, erpCode) {
                            priceField.val(parseFloat(ar).toFixed(2));
                            erpCodeField.val(erpCode);
                            lblConvField.text(erpCode);
                            getscheme(saleCodeField.text(), englishField.val(), unitField.val(), row, fre1Field.text(), pcodeField.text());
                            get();
                        }

                    
                        switch (Selected_Unit) {
                            case ans2[0].UnitCode:
                                if (Selected_Unit == ans2[0].Unit_code) 
                                    updateFields(ans2[0].MRP_Rate, ans2[0].Sample_Erp_Code);
                                else
                                     updateFields(ans2[0].MRP_Rate, 1);
                                break;
                            case ans2[0].Unit_code:
                                if (Selected_Unit != ans2[0].Base_Unit_code) {
                                    var ar = ans2[0].MRP_Rate * ans2[0].Sample_Erp_Code;
                                    updateFields(ar,  ans2[0].Sample_Erp_Code);
                                }
                                break;
                            case ans2[0].Base_Unit_code:
                                if (ans2[0].UnitCode == ans2[0].Unit_code) {
                                    var ar = ans2[0].MRP_Rate / ans2[0].Sample_Erp_Code;
                                    updateFields(ar, 1);
                                }
                                break;
                            default:
                                var ar = ans2[0].MRP_Rate * ans2[0].Sample_Erp_Code;
                                lblConvField.text(ans2[0].Sample_Erp_Code);
                                updateFields(ar, ans2[0].Sample_Erp_Code);
                        }

                        //if (Selected_Unit == ans2[0].UnitCode) {

                        //    row.find('#Price_').val(parseFloat(ans2[0].MRP_Rate).toFixed(2));
                        //    row.find('.erp_code').val(1);
                        //    cq = row.find('#english').val();
                        //    fr = row.find('.fre1').text();
                        //    row.find('.lbl_conv').text(1);
                        //    getscheme(row.find('.sale_code').text(), cq, row.find('.unit :selected').val(), row, row.find(".fre1").text(), row.find('.pcode').text());
                        //    get();
                        //}
                        //else if (Selected_Unit == ans2[0].Unit_code && Selected_Unit != ans2[0].Base_Unit_code) {
                        //    var ar = ans2[0].MRP_Rate * ans2[0].Sample_Erp_Code
                        //    row.find('#Price_').val(parseFloat(ar).toFixed(2));
                        //    row.find('.erp_code').val(1);
                        //    cq = row.find('#english').val();
                        //    fr = row.find('.fre1').text();
                        //    row.find('.lbl_conv').text(1);
                        //    getscheme(row.find('.sale_code').text(), cq, row.find('.unit :selected').val(), row, row.find(".fre1").text(), row.find('.pcode').text());
                        //    get();
                        //}
                        //else if (Selected_Unit == ans2[0].Base_Unit_code && ans2[0].UnitCode == ans2[0].Unit_Code) {
                        //    var ar = ans2[0].MRP_Rate / ans2[0].Sample_Erp_Code
                        //    row.find('#Price_').val(parseFloat(ar).toFixed(2));
                        //    row.find('.erp_code').val(1);
                        //    cq = row.find('#english').val();
                        //    fr = row.find('.fre1').text();
                        //    row.find('.lbl_conv').text(1);
                        //    getscheme(row.find('.sale_code').text(), cq, row.find('.unit :selected').val(), row, row.find(".fre1").text(), row.find('.pcode').text());
                        //    get();
                        //}
                        //else {

                        //    var ar = ans2[0].MRP_Rate * ans2[0].Sample_Erp_Code
                        //    row.find('.lbl_conv').text(ans2[0].Sample_Erp_Code);
                        //    row.find('#Price_').val(parseFloat(ar).toFixed(2));
                        //    row.find('.erp_code').val(ans2[0].Sample_Erp_Code);
                        //    cq = row.find('#english').val();
                        //    getscheme(row.find('.sale_code').text(), cq, row.find('.unit :selected').val(), row, row.find(".fre1").text(), row.find('.pcode').text());
                        //    get();
                        //}
                    }
                    calc_stock($(this).closest("tr"));
                
                });

            function get() {

                $("#tblCustomers >tbody >tr").each(function () {

                    var price = $(this).find('[id*=Price_]').val();
                    if (isNaN(price) || price == "null" || typeof price == "undefined" || price == "") { price = 0; }

                    var qty = $(this).find('[id*=english]').val();
                    if (isNaN(qty) || qty == "null" || typeof qty == "undefined" || qty == "") { qty = 0; }

                    var dis = $(this).find('[id*=Dis]').val();
                    if (isNaN(dis) || dis == "null" || typeof dis == "undefined" || dis == "") { dis = 0; }

                    var dis_cal = price * qty * (dis / 100);
                    if (dis_cal == '0' || dis_cal == '0.00') { $(this).find('.dis_val').val(""); } else { $(this).find('.dis_val').val(dis_cal.toFixed(2)); }

                    var cal = ((price * qty) - ((price * qty) * (dis / 100)));
                    if (cal == '0' || cal == '0.00') { $(this).find('[id*=total]').val(""); } else { $(this).find('[id*=total]').val(cal.toFixed(2)); }

                    var cgst_val = $(this).find('.hid_cgst_val ').val();
                    if (isNaN(cgst_val) || cgst_val == null || typeof cgst_val == "undefined" || cgst_val == "") { cgst_val = 0; }

                    var cgst_cal = cgst_val / 100 * cal;
                    if (cgst_cal == '' || cgst_cal == null || isNaN(cgst_cal)) { cgst_cal = 0; }

                    var cgst_cal_val = parseFloat(cgst_cal).toFixed(2);

                    $(this).find('[id*=cgst_tax_value]').val(parseFloat(cgst_cal_val).toFixed(2));

                    var sgst_val = $(this).find('.hid_sgst_val').val();
                    if (isNaN(sgst_val) || sgst_val == null || typeof sgst_val == "undefined" || sgst_val == "") { sgst_val = 0; }

                    var sgst_cal = sgst_val / 100 * cal;
                    if (sgst_cal == '' || sgst_cal == null || isNaN(sgst_cal)) { sgst_cal = 0; }

                    var sgst_cal_val = parseFloat(sgst_cal).toFixed(2);

                    $(this).find('[id*=sgst_tax_value]').val(parseFloat(sgst_cal_val).toFixed(2));

                    var total_tax_val = parseFloat(cgst_cal_val) + parseFloat(sgst_cal_val);

                    if (total_tax_val == '0' || total_tax_val == '0.00') { $(this).find('.tax_value').val('') } else { $(this).find('.tax_value').val(parseFloat(total_tax_val).toFixed(2)); }

                    //$(this).find('[id*=tax_value]').val(cgst_cal_val + sgst_cal_val);

                    var total_gross_total = parseFloat(total_tax_val) + parseFloat(cal);

                    if (total_gross_total == '0' || total_gross_total == '0.00') { $(this).find('[id*=grs_tot]').val(''); } else { $(this).find('[id*=grs_tot]').val(parseFloat(total_gross_total).toFixed(2)); }


                    //var tax_value = $(this).find('[id*=Total_Tax_value]').val();
                    //if (isNaN(tax_value) || tax_value == "" || typeof tax_value == "undefined" || tax_value == "null") { tax_value = 0; }

                    //var tot = Math.abs(tax_value / 100 * $(this).find('[id*=total]').val());
                    //if (tot == '0' || tot == '0.00') { $(this).find('[id*=tax_value]').val(""); } else { $(this).find('[id*=tax_value]').val(tot.toFixed(2)); }

                    //var gos = parseFloat(cal) + parseFloat(tot);
                    //if (gos == '0' || gos == '0.00') { $(this).find('[id*=grs_tot]').val(""); } else { $(this).find('[id*=grs_tot]').val(gos.toFixed(2)); }

                });
                cal();
            }

            function cal() {

                var order_qty = 0;
                $("[id*=english]").each(function () {

                    var soq = $(this).val();
                    if (soq != "") {
                        order_qty = order_qty + parseFloat($(this).val());
                    }
                });

                var discount_Percentage = 0;
                $("[id*=Dis]").each(function () {
                    var x = $(this).val();
                    if (x != "")
                        discount_Percentage = (discount_Percentage + parseFloat(x));
                });

                var discount_val = 0;
                $("[id*=dis_val]").each(function () {
                    var x = $(this).val();
                    if (x != "")
                        discount_val = (discount_val + parseFloat(x));
                });

                var Free_count = 0;
                $("[id*=Free]").each(function () {
                    var x = $(this).val();
                    if (x != "")
                        Free_count = (Free_count + parseFloat(x));
                });

                var Total = 0;
                $("[id*=total]").each(function () {
                    var x = $(this).val();
                    if (x != "")
                        Total = Total + parseFloat(x);
                });

                //var TaxTotal = 0;
                //$(".tax_value").each(function () {
                //    var ta = $(this).val();
                //    if (ta != "") {
                //        TaxTotal = TaxTotal + parseFloat($(this).val());
                //    }
                //});

                //var CGSTTotal = 0;
                //$("[id*=tax_div_value]").each(function () {
                //    var ta = $(this).val();
                //    if (ta != "") {
                //        CGSTTotal = CGSTTotal + parseFloat($(this).val());
                //    }
                //});

                var CGST_Total = 0;
                $("[id*=cgst_tax_value]").each(function () {
                    var x = $(this).val();
                    if (x != "")
                        CGST_Total = CGST_Total + parseFloat(x);
                });

                var SGST_Total = 0;
                $("[id*=sgst_tax_value]").each(function () {
                    var x = $(this).val();
                    if (x != "")
                        SGST_Total = SGST_Total + parseFloat(x);
                });

                if (CGST_Total == "0" || CGST_Total == "0.00") { $("[id*=Tax_CGST]").val(""); } else { $("[id*=Tax_CGST]").val(CGST_Total.toFixed(2)); }
                if (SGST_Total == "0" || SGST_Total == "0.00") { $("[id*=Tax_SGST]").val(""); } else { $("[id*=Tax_SGST]").val(SGST_Total.toFixed(2)); }

                var grandTotal = 0;
                $("[id*=grs_tot]").each(function () {
                    var ta = $(this).val();
                    if (ta != "") {
                        grandTotal = grandTotal + parseFloat($(this).val());
                    }
                });

                if (grandTotal == "0" || grandTotal == "0.00" || isNaN(grandTotal)) { $("[id*=in_Tot]").val(""); } else { $("[id*=in_Tot]").val(Math.round(grandTotal).toFixed(2)) }

                var tbl = $('#tblCustomers');
                $(tbl).find('tfoot tr').remove();

                if (order_qty != '0' || discount_Percentage != '0' || discount_val != '0' || Free_count != '0' || grandTotal != '0' || Total != '0' || CGST_Total != '0' || SGST_Total != '0') {
                    $("#tblCustomers").append('<tfoot><tr><th></th><th></th><th></th><th></th><th></th><th id="Order_q">' + order_qty + '</th><th id="dis_t">' + discount_Percentage + '</th><th id="discount_v">' + discount_val.toFixed(2) + '</th><th id="free_t">' + Free_count + '</th><th id="tot_sub_amt">' + Total.toFixed(2) + '</th><th id="footer_tax_total">' + parseFloat(SGST_Total + CGST_Total).toFixed(2) + '</th><th id="tot_gs_tot">' + grandTotal.toFixed(2) + '</th></tr></tfoot>');
                }

            }

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "Counter_Sales.aspx/getscheme",
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


            $(document).on('change', '[id*=sel_pay-term]', function () {

                var selected_val = $(this).val();

                if (selected_val != '1') {
                    $('#txt_ref_no').show();
                    $('.lbl_ref').show();
                    $('#spn_refer').show();
                    $('#txt_ref_no').val('');
                }
                else {
                    $('#txt_ref_no').hide();
                    $('.lbl_ref').hide();
                    $('#spn_refer').hide();
                    $('#txt_ref_no').val('');
                }
            });


            $(document).on('keyup', '[id*=english]', function (e) {

                var row = $(this).closest("tr");
                idx = $(row).index();

                if (e.keyCode == 13) {
                    AddRow();
                    return false;
                }

                if (row.find('.chzn-done').children("option:selected").val() == "0") {
                    alert('Select Product');
                    $(this).val("");
                    return false;
                    $(this).closest('tr').find('.item').children("option:selected").focus();
                }

                if (row.find('.unit').children("option:selected").val() == "0") {
                    alert('Select Unit');
                    $(this).val("");
                    return false;
                    $(this).closest('tr').find('.unit').children("option:selected").focus();
                }

                calc_stock($(this).closest("tr"));

                if (!jQuery.trim($(this).val()) == '') {
                    qty_fun(row);
                } else {
                    qty_fun(row);

                    $(this).val('');
                    row.find('#Dis').val('');
                    row.find('.dis_val').val('');
                    row.find('#Free').text('');
                    row.find('[id*=total]').val('');
                    row.find('[id*=tax_value]').val('');
                    row.find('[id*=tax_div_value]').val('');
                }
                get();
            });

            $(document).on('keyup', '[id*=Free]', function () {

                var row = $(this).closest("tr");
                if (!jQuery.trim($(this).val()) == '') {
                    if (!isNaN(parseFloat($(this).val()))) {
                        row.find('#Dis').val('');
                        row.find('#dis_val').val('');
                        row.find("#Dis").prop("readonly", true);
                        row.find("#dis_val").prop("readonly", true);
                    }
                }
                else {
                    row.find('#Dis').val('');
                    row.find('#dis_val').val('');
                    row.find("#Dis").prop("readonly", false);
                    row.find("#dis_val").prop("readonly", false);
                }
            });

            $(document).on('keyup', '[id*=Dis]', function () {

                var row = $(this).closest("tr");
                if (!jQuery.trim($(this).val()) == '') {
                    row.find('#Free').val('');
                    row.find("#Free").prop("readonly", true);
                }
                else {
                    $(this).val('');
                    row.find('#Free').val('');
                    row.find("#Free").prop("readonly", false);
                }
                get();
            });

            function calc_stock(n) {

                var pduct_code = n.find('.item').val();
                var con_fc = n.find('.erp_code').val();
                var u_n = n.find('.unit option:selected').text();

                var sa_qty = 0;
                var ad_qty = 0;

                var sto_filter = [];

                sto_filter = All_stock.filter(function (y) {
                    return (y.Prod_Code == pduct_code);
                });

                $(document).find('.' + pduct_code).each(function () {

                    var producode = $(this).find('.item').val();
                    var uni = $(this).find('.unit option:selected').val();
                    var con_fc = $(this).find('.erp_code').val();
                    var qqty = $(this).find('#english').val(); if (qqty == "" || qqty == null) { qqty = 0; }

                    if (sto_filter.length > 0) {
                        ad_qty += parseFloat(qqty) * con_fc;

                        if (sto_filter[0].Total_Stock > 0 && sto_filter[0].Total_Stock >= ad_qty) {
                            $(this).find('.validate').css('background-color', '');
                            $(this).find('.validate').removeClass('focus');
                        }
                        else {
                            $(this).find('.validate').css('background-color', '#ff6666');
                            alert('Stock is not Available for the product');
                            $(this).find('.validate').addClass('focus');
                        }
                    }


                });
            }

            function qty_fun(row) {

                var un = row.find('.unit').children("option:selected").val();
                var CQ = row.find('#english').val();
                var opcode = row.find(".chzn-done :selected").val();
                var pCode = row.find(".sale_code").text();
                var pname = row.find(".chzn-done :selected").text();
                var ff = row.find(".fre1").text();
                getscheme(pCode, CQ, un, row, ff, opcode);

            }

            function getscheme(sale_code, CQ, un, tr, ff, pro_code) {

                var res = scheme.filter(function (a) {
                    return (a.Sale_Erp_Code == sale_code && (Number(CQ) >= Number(a.Scheme)) && a.Scheme_Unit == un)
                });

                ans = [];

                $(tr).find('[id*=Dis]').val('');
                $(tr).find('[id*=dis_val]').val('');


                for (var c = 0; c < arr.length; c++) {

                    if (arr[c].Product_Code.indexOf(pro_code) >= 0) {

                        if (arr[c].Free > 0) {

                            arr[c].Free = arr[c].Free - ff;
                            $(tr).find('.fre').text('#' + pro_code).text('0');
                            $(tr).find('.fre1').text('#' + pro_code).text('0');
                            $(tr).find('.fre').attr('freecqty', arr[c].Free);
                            $(tr).find('.fre').attr('unit', '');
                            $(tr).find('.of_pro_name').val(0);
                            $(tr).find('.of_pro_code').val(0);
                            $(tr).find('#Free').val('');
                            $(tr).find('.disc_value').text($(tr).find('.disc_value').text() - $(tr).find('.disc_value').text());
                        }
                    }
                }

                if (res.length > 0) {

                    schemedefinewithoutpackage(res[0], tr, ff, CQ, pro_code);
                }

                $('#free_table').find('tbody tr').remove();

                for (var r = 0; r < arr.length; r++) {

                    if (arr[r].Free != '0') {
                        var str = "<tr><td style='width: 14%;' class='td_id'>" + (r + 1) + "</td><td style='width:21%;'><input type='hidden' value=" + arr[r].Offer_Product_Code + " id='apc'/>" + arr[r].Offer_Product_Code + "</td><td style='left: 201px;width: 38%;' class='td_name'><input type='hidden' value=" + arr[r].Offer_Product_Name + " id='apn'/>" + arr[r].Offer_Product_Name + "</td><td style='left: 454px;' class='td_free'><input type='hidden' value=" + arr[r].Free + " id='fr'/>" + (arr[r].Free + ' ' + arr[r].Offer_Product_Free_Unit) + "</td></tr>";
                        $('#free_table tbody').append(str);
                    }
                }
            }

            function schemedefinewithoutpackage(res, tr, ff, CQ, pro_code) {

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
                                    arr[idx].Product_Code = (arr[idx].Product_Code.indexOf(pro_code) >= 0) ? arr[idx].Product_Code : arr[idx].Product_Code + ',' + pro_code;
                                    $(tr).find('.fre').text(free + ' ' + (res.Free_Unit));
                                    $(tr).find('.fre').attr('unit', res.Free_Unit);
                                    $(tr).find('.fre').attr('freepro', res.Product_Code);
                                    $(tr).find('.fre').attr('cqty', CQ);
                                    $(tr).find('.fre').attr('freecqty', free);
                                    $(tr).find('#Free').val(free);
                                    $(tr).find('.fre1').text(free);

                                    if (free != "") {
                                        tr.find('.of_pro_name').val(res.Offer_Product_Name);
                                        tr.find('.of_pro_code').val(res.Product_Code);
                                    }
                                }
                                else {

                                    $(tr).find('.fre').text(free + ' ' + ((res.Free_Unit)));
                                    $(tr).find('.fre').attr('unit', res.Free_Unit);
                                    $(tr).find('.fre').attr('freepro', res.Product_Code);
                                    $(tr).find('.fre').attr('cqty', CQ);
                                    $(tr).find('.fre').attr('freecqty', free);
                                    $(tr).find('#Free').val(free);

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
                                        arr[idx].Product_Code = (arr[idx].Product_Code.indexOf(pro_code) >= 0) ? arr[idx].Product_Code : arr[idx].Product_Code + ',' + pro_code;
                                        $(tr).find('.fre').text(free + ' ' + (res.Free_Unit));
                                        $(tr).find('.fre').attr('unit', res.Free_Unit);
                                        $(tr).find('.fre').attr('freepro', res.Offer_Product);
                                        $(tr).find('.fre').attr('cqty', CQ);
                                        $(tr).find('.fre').attr('freecqty', free);
                                        $(tr).find('.fre1').text(free);
                                        tr.find('.of_pro_name').val(res.Offer_Product_Name);
                                        tr.find('.of_pro_code').val(res.Offer_Product);
                                        $(tr).find('#Free').val(free);
                                    }
                                    else {
                                        $(tr).find('.fre').text(free + ' ' + ((res.Free_Unit)));
                                        $(tr).find('.fre').attr('unit', res.Free_Unit);
                                        $(tr).find('.fre').attr('freepro', res.Offer_Product);
                                        $(tr).find('.fre').attr('cqty', CQ);
                                        $(tr).find('.fre').attr('freecqty', free);
                                        $(tr).find('.fre1').text(free);
                                        $(tr).find('#Free').val(free);
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
                                arr[idx].Product_Code = (arr[idx].Product_Code.indexOf(pro_code) >= 0) ? arr[idx].Product_Code : arr[idx].Product_Code + ',' + pro_code;
                                $(tr).find('.fre').text(free + ' ' + (res.Free_Unit));
                                $(tr).find('.fre').attr('unit', res.Free_Unit);
                                $(tr).find('.fre').attr('freepro', res.Product_Code);
                                $(tr).find('.fre').attr('cqty', CQ);
                                $(tr).find('.fre').attr('freecqty', free);
                                $(tr).find('#Free').val(free);
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
                                $(tr).find('#Free').val(free);
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
                                arr[idx].Product_Code = (arr[idx].Product_Code.indexOf(pro_code) >= 0) ? arr[idx].Product_Code : arr[idx].Product_Code + ',' + pro_code;
                                $(tr).find('.fre').text(free + ' ' + (res.Free_Unit));
                                $(tr).find('.fre').attr('unit', res.Free_Unit);
                                $(tr).find('.fre').attr('freepro', res.Offer_Product);
                                $(tr).find('.fre').attr('cqty', CQ);
                                $(tr).find('.fre').attr('freecqty', free);
                                $(tr).find('#Free').val(free);
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
                                $(tr).find('#Free').val(free);
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

                $(tr).find('[id*=Dis]').val(res.Discount);
                var d = $(tr).find('[id*=Price_]').val() * $(tr).find('[id*=english] ').val();
                var discalc = parseInt(res.Discount) / 100;
                var distotal = d * discalc;
                var finaltotal = d - distotal;

                $(tr).find('.dis_val').val(distotal.toFixed(2));

                //if (distotal != '0') {
                //    $(tr).find('.tdtotal').text(finaltotal.toFixed(2));
                //    $(tr).find('.disc_value').val(distotal.toFixed(2));
                //    $(tr).find('.tddis_val').text(distotal.toFixed(2));
                //}

                //if (finaltotal != "0") {
                //    var after_cal = $(tr).find('.tax_val').val() / 100 * $(tr).find('.tdtotal').text()
                //    var fin = after_cal + parseFloat($(tr).find('.tdtotal').text());
                //    $(tr).find('.tdtax').text(after_cal.toFixed(2));
                //    $(tr).find('.tdAmt').text(fin.toFixed(2));
                //}
            }


            $(document).on('keyup', '[id*=Ad_Paid]', function () {

                if (!jQuery.trim($(this).val()) == '') {
                    if (!isNaN(parseFloat($(this).val()))) {

                    }
                } else {
                    $(this).val('');
                }
                var adTotal = 0;
                $("[id*=Ad_Paid]").each(function () {

                    var tal = parseInt($(this).val()) || 0;
                    var d = parseFloat($(this).val()) == NaN ? 0 : parseFloat($(this).val());
                    adTotal = (parseFloat($("[id*=in_Tot]").val()) - tal);
                });

                $("[id*=Amt_Tot]").val(adTotal.toFixed(2));
            });


            $(".delete").on('click', function () {
                $(".case:checked").each(function () {

                    count--;
                    row = $(this).closest('tr');
                    var order_id = row.find('#Hid_order_code').val();
                    var prod = row.find('.hide').val();
                    idx = $(row).index();
                    multivalue.splice(idx, 1);
                    getscheme(row.find('.sale_code').text(), '', row.find('.unit :selected').val(), row, row.find(".fre1").text(), row.find('.pcode').text());
                    row.remove();

                    if (prod != "") {
                        $(document).find('.' + prod).each(function () {
                            calc_stock($(this).closest('tr'));
                        });
                    }
                    check();
                    cal();
                });
            });

            function check() {
                var k = 1;
                $("#tblCustomers >tbody >tr").each(function () {
                    $(this).children('td').eq(1).html(k++);

                });
            }

            $(document).on("focus", "#Free", function () {

                var qua = $(this).closest("tr").find('#english').val();
                if (qua.length <= 0) {
                    alert('Enter Quantity');
                    $(this).closest('tr').find('#english').focus();
                }
            });

            $(document).on("focus", "#Dis", function () {

                var qua = $(this).closest("tr").find('#english').val();
                if (qua.length <= 0) {
                    alert('Enter Quantity');
                    $(this).closest('tr').find('#english').focus();
                }
            });

            $(document).on("focus", "#total", function () {

                var qua = $(this).closest("tr").find('#english').val();
                if (qua.length <= 0) {
                    alert('Enter Quantity');
                    $(this).closest('tr').find('#english').focus();
                }
            });

            $(document).ready(function () {
                $('#english').keypress(function (e) {
                    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                        return false;
                    }
                });
            });

            $(document).ready(function () {
                $('#Dis').keypress(function (e) {
                    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                        return false;
                    }
                });
            });

            $(document).ready(function () {
                $('#Free').keypress(function (e) {
                    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                        return false;
                    }
                });
            });


            var buttoncount = 0;

            $(document).on('click', '.btnsave', function () {
                var pcode = '';
                var qt = '';
                var chk = true; buttoncount += 1;

                if (buttoncount == "1") {

                    var approve = 0; var Alldata = []; var address1 = '';

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

                    var Customer_No = $('#txt_mob').val() || 0; //if (Customer_No == "") { buttoncount = 0; alert("Enter Customer Mobile No."); $('#txt_mob').focus(); return false; }

                    var in_date = $('#txt_invoice_date').val();

                    var Customer_Name = $('#txt_name').val(); //if (Customer_Name == "") { buttoncount = 0; alert('Please Enter Customer name'); $('#txt_name').focus(); return false; }

                    var Pay_Term_Text = $('#sel_pay-term :selected').text(); var Pay_Term = $('#sel_pay-term :selected').val(); if (Pay_Term == "0") { buttoncount = 0; alert("Please Enter Pay_Term"); $('#sel_pay-term').focus(); return false; }

                    if (Pay_Term == "1") {

                        var ref = '0';
                    }
                    else {

                        var ref = $('#txt_ref_no').val();
                        // if ($('#txt_ref_no').val() == "") { buttoncount = 0; alert('Enter Reference No'); $('#txt_ref_no').focus(); return false; }
                    }

                    var address = $('#txt_add').val();

                    // if (address == "") { buttoncount = 0; alert("Please Enter Customer Address"); $('#txt_add').focus(); return false; }

                    $("#tblCustomers >tbody >tr").each(function () {

                        pcode = $(this).find('.item option:selected').val();
                        var un = $(this).find('.unit option:selected').val();
                        qt = $(this).find('#english').val();

                        if (pcode == "0" && chk == true) { buttoncount = 0; alert('Please Select Product!!'); chk = false; }

                        if (un == "0" && chk == true) {
                            buttoncount = 0;
                            alert('Please Select UOM!!');
                            $(this).find('[id*=unit]').focus();
                            chk = false;
                        }
                        if (qt.length <= 0 && chk == true) { buttoncount = 0; alert('Enter Quantity!!'); $('#english').focus(); chk = false; }
                    });

                    var sub_tot = $('#tot_sub_amt').text();
                    var Gst = $('#footer_tax_total').text();
                    var Cgst = $('#Tax_CGST').val();
                    var Sgst = $('#Tax_SGST').val();
                    var total = $('#in_Tot').val();
                    var remark = $('#notes').val().replace(/'|"|/g, '');
                    var Dis_Tot = $('#discount_v').text() || 0;

                    var stock_chck = $('#tblCustomers tbody tr').find('.focus').length

                    if (stock_chck == 0) {

                        mainarr = []; dayarr = []; taxArr = [];

                        mainarr.push({

                            dis_code: stockistcode,
                            dis_name: stockistname,
                            cus_code: $("#<%= hd_cust_code.ClientID %>").val(),
                            cus_name: $("#txt_name").val(),
                            Cust_Address: address,
                            Customer_No: Customer_No,
                            order_date: today1,
                            pay_term: Pay_Term_Text,
                            sub_total: sub_tot || 0,
                            tax_total: Gst || 0,
                            cgst_total: Cgst || 0,
                            sgst_total: Sgst || 0,
                            total: total || 0,
                            remark: remark,
                            Dis_total: Dis_Tot,
                            Div_Code: div_code,
                            Ref_No: ref
                        });

                        $('#tblCustomers > tbody > tr').each(function () {
                            var Prd_code = $(this).find('[id*=hide]').val();
                            var unit = $(this).find('[id*=unit] Option:selected').text();
                            var res = [];
                            res = Allrate.filter(function (t) {
                                return (t.Product_Detail_Code == Prd_code );
                            });

                            if (res.length > 0) {

                                if (res[0].Base_Unit_code == unit) {

                                    var qty = $(this).find('[id*=english]').val();
                                    var Conversion_qty = parseFloat($(this).find('[id*=english]').val());
                                }
                                else {
                                    var qty = $(this).find('[id*=english]').val();
                                    var Conversion_qty = parseFloat($(this).find('[id*=english]').val()) * res[0].Sample_Erp_Code;
                                }

                                var Prd_name = $(this).find(".item option:selected").html();
                                var price = $(this).find('[id*=Price_]').val();
                                var dis = $(this).find('[id*=Dis]').val();
                                var dis_value = $(this).find('[id*=dis_val]').val();
                                var free = $(this).find('[id*=Free]').val();
                                var tot = $(this).find('[id*=total]').val();
                                var of_pro_code = $(this).find('.of_pro_code').val();
                                var of_pro_name = $(this).find('.of_pro_name').val();
                                var of_pro_unit = $(this).find('.fre').attr('unit') || 0;
                                var Tax_amt = $(this).find('.tax_value').val();
                                var Tax_name = $(this).find('[id*=id_tax]').text();
                                var cf = $(this).find('.lbl_conv').text();

                                taxArr.push({
                                    Product_code: $(this).find('[id*=hide]').val() || 0,
                                    taxCode: $(this).find('.tax_value').text() || 0,
                                    tax_Name: $(this).find('.Total_Tax_value').val() || 0,
                                    value: (Number($(this).find('.tax_value').val() || 0))
                                });

                                console.log(taxArr);
                                dayarr.push({
                                    Trans_order_no: Prd_code,
                                    Product_code: Prd_code,
                                    Product_name: Prd_name,
                                    Price: price,
                                    Discount: dis,
                                    Free: free,
                                    Quantity: qty,
                                    con_qty: Conversion_qty,
                                    Amount: tot,
                                    Unit: unit,
                                    Off_Pro_Code: of_pro_code,
                                    Off_Pro_Name: of_pro_name,
                                    Off_Pro_Unit: of_pro_unit,
                                    Tax: Tax_amt,
                                    Con_Fac: cf,
                                    Dis_val: dis_value
                                });
                            }
                        });

                        if (dayarr.length <= 0) {
                            buttoncount = 0;
                            alert('No Records Found');
                            chk = false;
                        }

                        $('.example').show();
                        if (chk != false) {

                            setTimeout(function () {
                                $.ajax({
                                    type: "POST",
                                    async: false,
                                    contentType: "application/json; charset=utf-8",
                                    url: "Counter_Sales.aspx/SaveDate",
                                    data: "{'Counter_head':'" + JSON.stringify(mainarr) + "','Counter_details':'" + JSON.stringify(dayarr) + "','Counter_tax':'" + JSON.stringify(taxArr) + "','no':'" + Customer_No + "'}",
                                    dataType: "json",
                                    success: function (data) {
                                        order = data.d;
                                        alert("Counter Sale Saved Successfully");
                                        //window.location.href = "../Stockist/Counter_Sales_List.aspx";
                                        var Respomse = confirm('Do You Want To Print Counter Sales')
                                        if (Respomse) { window.location.href = "../Stockist/CounterSales_Print.aspx?Order_id=" + order + "&Stockist_Code=" + stockistcode + "&Div_Code=" + div_code + ""; } else { window.location.href = "../Stockist/Counter_Sales_List.aspx"; }
                                        // if (Respomse) { window.location.href = "../Stockist/counter_sale_print.aspx?Order_id=" + order + "&Stockist_Code=" + stockistcode + "&Div_Code=" + div_code + ""; } else { window.location.href = "../Stockist/Counter_Sales_List.aspx"; }
                                        if (Respomse) { loadCountersale(); print(); } else { window.location.href = "../Stockist/Counter_Sales_List.aspx"; }
                                        loadCountersale();
                                        print();
                                    },
                                    error: function (data) {
                                        buttoncount = 0;
                                        alert(JSON.stringify(data));
                                    }
                                }, 30);
                            });
                        }
                    }
                    else {
                        buttoncount = 0;
                        alert("Stock value is lesser than qunatity");
                        return false;
                    }

                }
            });

            });

            //function print() {


            //    window.print();

            //    var originalContents = $("body").html();
            //    var printContents = $("#div").html();
            //    $("body").html(printContents);
            //    window.print($("#div").html());
            //    $("body").html("");
            //    $("body").html(originalContents);
            //    return false;
            //}

            function loadCountersale() {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    data: "{'Order_Id':'" + order + "'}",
                    url: "Counter_Sales.aspx/GetcountersaleDetail",
                    dataType: "json",
                    success: function (data) {
                        Alldetails = JSON.parse(data.d) || [];
                        str = "";
                        $("#div").html("");
                        var str1 = '<table border="0" align="left" class="middletable" style="width: 80%; border-collapse: collapse;"><tbody>';
                        var str2 = '<table border="0" align="left" class="datetable" style="width: 80%; border-collapse: collapse;"><tbody>';

                        str1 += '<tr><td  align="center" style="padding-right:0px;padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen; TimesNewRomen;"><b>' + Alldetails[0].Stockist_Name + '</b></td></tr>';
                        str1 += '<tr><td  align="center" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen; "><b>HAP DAILY</b></td></tr>';
                        str1 += '<tr><td  align="center" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;"><b>' + Alldetails[0].Stockist_Address + '</b></td></tr>';
                        str1 += '<tr><td  align="center" style ="font: bold 14px TimesNewRomen;padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;"><b>Ph:' + Alldetails[0].Stockist_Mobile + '</b></td></tr>';
                        str1 += '<tr><td  align="center" style="width:275px;padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;"><b>GSTIN:' + Alldetails[0].gstn + '</b></td></tr>';
                        str1 += '<tr><td  align="center" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;"><b>SAP CODE:' + Alldetails[0].ERP_Code + '</b></td></tr>';
                        str1 += '<tr><td  align="center" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;"><b>RETAIL INVOICE</b></td></tr></tbody></table>';

                        str2 += '<tr><td  align="left" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;">Bill#: ' + Alldetails[0].Trans_Count_Slno + '</td><td  align="right" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;">Date: ' + Alldetails[0].dt + '</td></tr></tbody></table>';

                        str = str1 + str2;

                        productheader();

                        for ($i = 0; $i < Alldetails.length; $i++) {
                            var price = Alldetails[$i].Price;
                            var amount = Alldetails[$i].Amount;
                            var qnty = Alldetails[$i].Quantity;
                            var tax = Alldetails[$i].Tax_Total;

                            str += "<tr><td align='left' style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + Alldetails[$i].Product_Name + "</td>";
                            str += "<td  align='left'   style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + price.toFixed(2) + "</td>";
                            str += "<td  align='left' style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + qnty.toFixed(2) + "</td>";
                            str += "<td  align='right'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + price.toFixed(2) + "</td>";
                            str += "<td  align='right'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + amount.toFixed(2) + "</td></tr>";

                            totalQnty += qnty;
                            totalAmount += amount;
                            totalTax += tax;
                        }
                        str += '</tbody>';
                        $('#div').append(str);
                        TotalCalculation();
                    }
                });
            }

            function TotalCalculation() {

                Totalval = totalAmount + totalTax;
                netvalue = Math.round(Totalval);
                rndoff = netvalue - Totalval;
                totalItem = Alldetails.length;

                var str3 = '<table border="0" align="left" class="datetable" style="width: 80%; border-collapse: collapse;"><tbody>';
                str3 += '<tr style="border-top:thin dashed;"><td  align="left" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;">Tot Qty :</td><td  align="left" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;"> ' + totalQnty.toFixed(2) + '</td>';
                str3 += '<td align="right" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;">Total :</td><td align="right" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;"> ' + totalAmount.toFixed(2) + '</td></tr>';
                str3 += '<tr><td  align="left" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;">Total Items :</td><td  align="left" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;"> ' + totalItem + '</td>';
                str3 += '<td align="right" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;">Tot GST: </td><td align="right" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;">' + totalTax.toFixed(2) + '</td></tr>';
                str3 += '<tr style="border-bottom:thin dashed;"><td></td><td></td><td  align="right" style="padding-top: 0px;padding-bottom: 5px;font:12px TimesNewRomen;">Coin Adj:</td><td  align="right" style="padding-top: 0px;padding-bottom: 5px;font:12px TimesNewRomen;"> ' + rndoff.toFixed(2) + '</td></tr>';
                str3 += '<tr style="border-bottom:thin dashed;"><td></td><td  align="right" style="padding-top: 5px;padding-bottom: 5px;font:14px TimesNewRomen;"><b>TOTAL: </b></td><td align="center" style="padding-top: 5px;padding-bottom: 5px;font:14px TimesNewRomen;"><b>' + Totalval.toFixed(2) + '</b></td><td></td></tr></tbody></table>';
                $('#div').append(str3);
            }
            function productheader() {
                str += '<table class="rptOrders" align="left" style="width: 80%; border-collapse: collapse; bordercolor:blue;">';
                str += '<thead><tr style="border-top:thin dashed;border-bottom:thin dashed;font:12px TimesNewRomen;"><td align="left"><b>Item Name</b></td>';
                str += '<td align="center" style="font:12px TimesNewRomen;"><b>MRP</b></td>';
                str += '<td align="center" style="font:12px TimesNewRomen;"><b>Qty</b></td>';
                str += '<td align="center" style="font:12px TimesNewRomen;"><b>Rate</b></td>';
                str += '<td align="center" style="font:12px TimesNewRomen;"><b>Amount</b></td></tr></thead>';
                str += '<tbody>';
            }



        </script>

    </head>
    <body>

        <form id="frm" runat="server">

            <asp:HiddenField ID="hd_cust_code" runat="server" />
            <asp:HiddenField ID="Hid_Pro_code" runat="server" />
            <asp:HiddenField ID="Hid_Cust" runat="server" />
            <input type="hidden" id="hid_order_Id" />

            <div class="container">

                <div class="row">

                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                        <div class="form-group">
                            <label class="control-label" for="lbl_mobileno">Customer Mobile No :</label><span style="color: Red">*</span>
                            <input type="text" autocomplete="off" class="form-control" id="txt_mob" />
                        </div>
                    </div>

                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                        <div class="form-group">
                            <label class="control-label" for="lbl_Cust_Name">Customer Name :</label><%--<span style="color: Red">*</span>--%>
                            <input type="text" autocomplete="off" class="form-control" id="txt_name" />
                            <input type="hidden" id="hid_Cust-Code" />
                        </div>
                    </div>

                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                        <div class="form-group">
                            <label class="control-label" for="lbl_Cust_Name">Date</label>
                            <input type="date" id="txt_invoice_date" readonly class="form-control datepicker" />
                        </div>
                    </div>

                    <div class="col-md-3 col-lg-3 col-sm-3 col-xs-12">
                        <div class="form-group">
                            <label class="control-label" for="lbl_Pay_term">Payment Term :</label><span style="color: Red">*</span>
                            <select id="sel_pay-term" class="form-control">
                            </select>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4 col-lg-4 col-sm-4 col-xs-12">
                        <label class="control-label" for="lbl_Cust_Name">Customer Address :</label><%--<span style="color: Red">*</span>--%>
                        <textarea rows="4" autocomplete="off" class="form-control" id="txt_add"></textarea>
                    </div>

                    <div class="col-md-4 col-lg-4 col-sm-4 col-xs-12">
                        <label class="control-label" for="lbl_notes">Notes:</label>
                        <textarea placeholder="Your Notes" id="notes" name="notes" rows="4" class="form-control txt"></textarea>
                    </div>
                    <div class="col-md-3 col-lg-3 col-sm-3 col-xs-12">
                        <div class="form-group">
                            <label class="control-label lbl_ref" for="lbl_ref_no">Reference No</label><%--<span id="spn_refer" style="color: Red">*</span>--%>
                            <input type="text" id="txt_ref_no" autocomplete="off" class="form-control" />
                        </div>
                    </div>
                </div>

            </div>
            <br />
            <div class="row">
                <div class="form-group">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                        <table id="tblCustomers" class="table table-bordered table-hover">
                            <thead class="ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix">
                                <tr align="center">
                                    <th width="2%"></th>
                                    <th width="2%">Sl No.
                                    </th>
                                    <th width="15%">Product Name
                                    </th>
                                    <th width="5%">Unit
                                    </th>
                                    <th width="3%">Price
                                    </th>
                                    <th width="3%">Quantity
                                    </th>
                                    <th width="3%">Dis(%)
                                    </th>
                                    <th width="3%">Dis val
                                    </th>
                                    <th width="3%">Free
                                    </th>
                                    <th width="3%">Total
                                    </th>
                                    <th width="3%">Tax Val
                                    </th>
                                    <th width="3%">Gross Total
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-3">
                    <button id="Add" tabindex="" type="button" class='btn btn-success addmore'>
                        + Add More</button>
                    <button id="Del" type="button" tabindex="3" class='btn btn-danger delete'>
                        - Delete</button>
                </div>
            </div>

            <div class="row" style="margin-top: 0px;">

                <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
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
                </div>

                <div class="col-sm-offset-8 form-horizontal" style="display: none;">
                    <label class=" col-xs-3 control-label">
                        Subtotal :
                    </label>
                    <div class=" col-md-9">
                        <div class="input-group">
                            <div class="input-group-addon currency">
                                <i class="fa fa-inr"></i>
                            </div>
                            <input data-cell="G1" id="sub_tot" data-format="0,0.00" class="form-control" readonly />
                        </div>
                    </div>
                </div>

                <div class="col-sm-offset-8 form-horizontal" style="margin-top: 40px; display: none;">
                    <label class=" col-xs-3 control-label">
                        Discount :
                    </label>
                    <div class="col-sm-9">
                        <div class="input-group">
                            <div class="input-group-addon currency">
                                <i class="fa fa-inr"></i>
                            </div>
                            <input data-cell="G1" id="Txt_Dis_tot" data-format="0,0.00" class="form-control" readonly />
                        </div>
                    </div>
                </div>

                <div class="col-sm-offset-8 form-horizontal" style="margin-top: 80px; display: none;">
                    <label class=" col-xs-3 control-label">
                        GST :
                    </label>
                    <div class="col-sm-9">
                        <div class="input-group">
                            <div class="input-group-addon currency">
                                <i class="fa fa-inr"></i>
                            </div>
                            <input data-cell="G1" id="Tax_GST" data-format="0,0.00" class="form-control" readonly />
                        </div>
                    </div>
                </div>

                <div class="col-sm-offset-8 form-horizontal" style="margin-top: 40px;">
                    <label class=" col-xs-3   control-label">
                        CGST :
                    </label>
                    <div class="col-sm-9">
                        <div class="input-group">
                            <div class="input-group-addon currency">
                                <i class="fa fa-inr"></i>
                            </div>
                            <input data-cell="G1" id="Tax_CGST" data-format="0,0.00" class="form-control" readonly />
                        </div>
                    </div>
                </div>


                <div class="col-sm-offset-8 form-horizontal" style="margin-top: 80px;">
                    <label class=" col-xs-3  control-label">
                        SGST :
                    </label>
                    <div class="col-sm-9">
                        <div class="input-group">
                            <div class="input-group-addon currency">
                                <i class="fa fa-inr"></i>
                            </div>
                            <input data-cell="G1" id="Tax_SGST" data-format="0,0.00" class="form-control" readonly />
                        </div>
                    </div>
                </div>


                <div class="col-sm-offset-8 form-horizontal" style="margin-top: 120px;">
                    <label class=" col-xs-3 control-label">
                        Total (R) :
                    </label>
                    <div class="col-sm-9">
                        <div class="input-group">
                            <div class="input-group-addon currency">
                                <i class="fa fa-inr"></i>
                            </div>
                            <input data-cell="G1" id="in_Tot" data-format="0,0.00" class="form-control" readonly />
                        </div>
                    </div>
                </div>
            </div>

            <br />
            <br />

            <div class="row" style="text-align: center">
                <div class="fixed">
                    <%--<a id="stk_chk" style="font-size: medium">Qty Value High on Stock Value</a> --%><a id="btndaysave"
                        class="btn btn-primary btnsave" style="vertical-align: middle; font-size: 17px;">
                        <span>Save Counter Sale</span></a>&nbsp&nbsp<a id="btnprint" class="btn btn-success pleasewait"
                            style="vertical-align: middle; font-size: 17px;"> <span>Print Invoice</span></a>
                </div>
            </div>


            <div class="ticket" id="div" style="display: none;">
            </div>


        </form>

    </body>

    </html>

</asp:Content>

