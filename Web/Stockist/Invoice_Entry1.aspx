<%@ Page Title="" Language="C#" MasterPageFile="~/Master_DIS.master" AutoEventWireup="true"
    CodeFile="Invoice_Entry1.aspx.cs" Inherits="Invoice_Entry1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!doctype html>
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>

        <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
        <link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" />
        <link href="../css/jquery.multiselect.css" rel="stylesheet" type="text/css" />
        <script src="../js/jquery.multiselect.js" type="text/javascript"></script>

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
        </style>
        <script type="text/javascript">

            $(document).ready(function ($) {

                $('#datepicker').datepicker({ format: 'dd/mm/yyyy', startDate: '-3d', minDate: 0 }); $('#datepicker').attr('min', today);
                $('#datepicker1').datepicker({ format: 'mm/dd/yyyy', startDate: '-3d', minDate: 0 });

                var SingleSelValue = []; var FirstCheck = []; var multivalue = []; var Free_Data = []; var alldata = []; var arr = []; var retailer_ID; var retailer_name;
                var dts = []; var AllProduct = []; var s = []; var od = []; var ar = []; var da; var AllOrderIDs = [];

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


                var div_code = ("<%=Session["div_code"].ToString()%>");
                var stockistcode = ("<%=Session["Sf_Code"].ToString()%>");
                var stockistname = ("<%=Session["sf_name"].ToString()%>");


                $(document).on('keypress', '.validate', function (e) {
                    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                        return false;
                    }
                });

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Invoice_Entry1.aspx/GetProductDetails",
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
                    url: "Invoice_Entry1.aspx/bindretailer",
                    dataType: "json",
                    async: false,
                    success: function (data) {
                        if (data.d.length > 0) {
                            $.each(data.d, function (data, value) {
                                $('#recipient-name').append($("<option></option>").val(this['Value']).html(this['Text'])).trigger('chosen:updated').css("width", "100%");;;
                            });
                        }
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
                $('#recipient-name').chosen();

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    data: "{'Div_Code':'" + div_code + "','Stockist_Code':'" + stockistcode + "'}",
                    url: "Invoice_Entry1.aspx/getreate",
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
                    url: "Invoice_Entry1.aspx/GetTransType",
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


                $('#recipient-name').change(function () {

                    retailer_ID = $(this).children("option:selected").val();
                    $("#example-multiple-selected").html('');

                    $.ajax({
                        type: "Post",
                        contentType: "application/json; charset=utf-8",
                        url: "Invoice_Entry1.aspx/GetPendingOrder",
                        data: "{'Retailer_ID':'" + retailer_ID + "'}",
                        dataType: "json",
                        async: false,
                        success: function (data) {
                            var Datas = data.d;
                            if (Datas.length > 0) {

                                $.each(data.d, function () {
                                    $('#example-multiple-selected').append($("<option></option>").val(this['Trans_Sl_No']).html(this['Trans_sl_Order_Date']));
                                });

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
                        }
                    });
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
                    $('#Tax_SGST').val('');
                    AllProduct = [];
                    multivalue = [];
                    arr = [];
                }

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Invoice_Entry1.aspx/GetPayType",
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

                        idx = AllProduct.indexOf(exO[0]);
                        // idx = arr.indexOf(exO[0]);
                        //  arr.splice(idx, 1);
                        AllProduct.splice(idx, 1);
                        all();
                    }
                });

                function bindvalue(ordervalues) {

                    for (var r = 0; r < ordervalues.length; r++) {
                        var value = ordervalues[r].OrderIds;
                        $.ajax({
                            type: "Post",
                            contentType: "application/json; charset=utf-8",
                            url: "Invoice_Entry1.aspx/GetAllProductDetails",
                            data: "{'no':'" + ordervalues[r].OrderIds + "'}",
                            dataType: "json",
                            async: false,
                            success: function (data) {
                                FirstCheck = JSON.parse(data.d) || 0;
                                var Order = [];
                                AllProduct.push({
                                    Order_No: value,
                                    Order_Details: FirstCheck
                                });
                                reloadTable(FirstCheck, 0);
                                check_free(FirstCheck, 0);
                            },
                            error: function (result) {
                                alert(JSON.stringify(result));
                            }
                        });
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
                                // Free_Data[idx].Quantity = parseFloat(OrdP[0].Quantity) + parseFloat(items[k].Quantity);
                                arr[idx].Free = parseFloat(OrdP[0].Free) + parseFloat(items[k].Free);

                                console.log(JSON.stringify(AllProduct));
                            } else {
                                $vq = parseFloat(OrdP[0].Free) - parseFloat(items[k].Free)
                                if ($vq > 0) {
                                    arr[idx].Free = $vq;
                                    // Free_Data[idx].Free = parseFloat(OrdP[0].Free) - parseFloat(items[k].Free);

                                } else {
                                    arr.splice(idx, 1);
                                    //arr.remove();
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
                                    if (ans3[0].product_unit == items[k].unit) {
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
                                    //<td>" + (arr[r].P_Free + ' ' + arr[r].P_join) + "</td>
                                }

                                if (arr[r].P_Free != '0' && arr[r].Free == '0') {
                                    var str = "<tr><td style='width: 14%;' class='td_id'>" + l + "</td><td style='width:21%;'><input type='hidden' value=" + arr[r].Product_Code + " id='apc'/>" + arr[r].Product_Code + "</td><td style='left: 201px;width: 38%;' class='td_name'><input type='hidden' value=" + arr[r].Product_Name + " id='apn'/>" + arr[r].Product_Name + "</td><td style='display:none'><input type='hidden' id='opc' value=" + arr[r].Product_Code1 + " />" + arr[r].Product_sale_Code + "</td><td>" + (arr[r].P_Free + ' ' + arr[r].P_join) + "</td></tr>";
                                    $('#free_table tbody').append(str);
                                    //<td style='left: 454px;' class='td_free'><input type='hidden' value=" + arr[r].Free + " id='fr'/>" + (arr[r].Free + ' ' + arr[r].C_join) + "</td>
                                }

                            }
                        }
                    }
                }

                function reloadTable(items, flag) {

                    for (k = 0; k < items.length; k++) {
                        OrdP = multivalue.filter(function (a) {
                            return (items[k].Product_Code == a.Product_Code && items[k].unit == a.Unit)
                        })
                        if (OrdP.length > 0) {

                            idx = multivalue.indexOf(OrdP[0]);
                            if (flag == 0) {
                                multivalue[idx].Quantity = parseFloat(OrdP[0].Quantity) + parseFloat(items[k].Quantity);
                                multivalue[idx].Sale_qt = multivalue[idx].Quantity;
                                multivalue[idx].Free = parseFloat(OrdP[0].Free) + parseFloat(items[k].Free);
                                multivalue[idx].Discount = parseFloat(OrdP[0].Discount) + parseFloat(items[k].Discount);
                                //   multivalue[idx].Trans_Order_No = OrdP[0].Trans_Order_No + ',' + items[k].Trans_Order_No;


                                console.log(JSON.stringify(AllProduct));
                            } else {
                                $vq = parseFloat(OrdP[0].Quantity) - parseFloat(items[k].Quantity)
                                if ($vq > 0) {
                                    multivalue[idx].Quantity = $vq;
                                    multivalue[idx].Sale_qt = $vq;
                                    multivalue[idx].Free = parseFloat(OrdP[0].Free) - parseFloat(items[k].Free);
                                    //  multivalue[idx].Trans_Order_No = OrdP[0].Trans_Order_No + ',' + items[k].Trans_Order_No;
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
                                itm.Unit = items[k].unit || 0;
                                itm.Quantity = items[k].Quantity;
                                itm.Sale_qt = items[k].Quantity;
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

                                multivalue.push(itm);
                            }
                        }
                    }
                    var tbl = $('#tblCustomers');
                    $(tbl).find('tbody tr').remove();

                    if (multivalue.length > 0) {
                        for (var i = 0; i < multivalue.length; i++) {

                            var ans = [];
                            ans = Allrate.filter(function (t) {
                                return (t.Product_Detail_Code == multivalue[i].Product_Code);
                            });
                            bindunit1 = {};
                            if (ans[0].product_unit == multivalue[i].Unit) {

                                //  bindunit1 = '<option value="0">Select Unit</option>';
                                bindunit1 += `<option selected="selected" value=${ans[0].product_unit}>${ans[0].product_unit}</option>`;
                                bindunit1 += `<option  value=${ans[0].Product_Sale_Unit}>${ans[0].Product_Sale_Unit}</option>`;
                            }
                            else {
                                //  bindunit1 = '<option value="0">Select Unit</option>';
                                bindunit1 += `<option  value=${ans[0].product_unit}>${ans[0].product_unit}</option>`;
                                bindunit1 += `<option selected="selected" value=${ans[0].Product_Sale_Unit}>${ans[0].Product_Sale_Unit}</option>`;
                            }

                            if (multivalue[i].Type == "Order") {

                                var dis_val = parseFloat((multivalue[i].Rate * multivalue[i].Quantity)) * parseFloat((multivalue[i].Discount / 100));
                                var fr = multivalue[i].Free;
                                var str = "<td><input type='checkbox' class='case' /></td>";
                                str += '<td>' + (i + 1) + '</td>';
                                str += "<td><input class='form-control autoc' type='text' style='display:none' data-validation='required' id='Item_1' t='" + multivalue[i].Product_Name + "'  value='" + multivalue[i].Product_Code + "' name='item_name[]' >" + multivalue[i].Product_Name + "<input type ='hidden' id ='Hid_Pro_code' class='rrr' name ='Hid_Pro_code' value=" + multivalue[i].Product_Code + "><input type='hidden' id='pro_sale_code' value=" + multivalue[i].Sale_Code + "><input type='hidden' id='Hid_order_code'  name='Hid_order_code' value=" + multivalue[i].Trans_Order_No + " ><input type='hidden' id='hd_tax_value' class='hd_tax_value' value=" + multivalue[i].Tax_Val + "><br /><label style='padding-top: 8px;' class='lbl_Tax' id='id_tax'>Tax : " + multivalue[i].Tax_Name + "</label><label style='float:right; padding-top:10px; font-size: 12px;' class='lbl_tax_value' id='lbl_tax_value'></label><input type='hidden' id='tax_div_value' /><input type='hidden' id='tax_id' /></td >";
                                str += `<td><select class="form-control unit" name="unit" >${bindunit1}</select></td>`;
                                str += "<td><input class='form-control' type='text' id='Price_' name='price[]' data-cell='C1' data-format='0.00' data-validation='required' readonly=''  value=" + multivalue[i].Rate + " /><input type='hidden' name='stkval' stkgood='0' stkdamage='0' /></td>";
                                str += "<td><input class='form-control validate' type='text' data-validation='required' id='Order_qty' readonly autocomplete='off'  name='Order_quantity[]' data-cell='K1' data-format='0' value=" + multivalue[i].Quantity + " /></td>";
                                str += "<td><input class='form-control validate' type='text' data-validation='required' id='english' autocomplete='off'  name='quantity[]' data-cell='D1' data-format='0' value=" + multivalue[i].Quantity + " /></td>";
                                str += "<td><input class='form-control validate' type='text' autocomplete='off' id='Dis' name='dis[]' data-cell='Y1' data-format='0' data-validation='required' value=" + multivalue[i].Discount + " ><input class='form-control hid' type='hidden' id='hid_dis' value=" + dis_val.toFixed(2) + "> </td>";
                                str += "<td><input class='form-control validate' type='text' id='Free' autocomplete='off' name='free[]' data-cell='I1' data-format='0'  data-validation='required' value=" + multivalue[i].Free + " ></td><td style='display:none' ><input type='hidden' class='of_pro_name'  name='of_pro_name' value='" + multivalue[i].Off_Pro_Name + "' ></td><td style='display:none' ><input type='hidden' class='of_pro_code'  name='of_pro_code' value=" + multivalue[i].Off_Pro_Code + " ></td><td style='display:none;'<label id=" + multivalue[i].Product_Code + " name='free' class='fre' freecqty=" + multivalue[i].Free + " cqty=" + multivalue[i].Quantity + " freepro=" + multivalue[i].Product_Code + " unit=" + multivalue[i].Off_Pro_Unit + ">" + multivalue[i].Free + '' + multivalue[i].Off_Pro_Unit + "</lable></td> <td style='display:none' ><input type='hidden' id=" + multivalue[i].Product_Code + " class='fre1' name='fre1' >" + multivalue[i].Free + "</td><td style='display:none' ><input type='hidden' id='erp_code' class='erp_code' value=" + multivalue[i].E_Code + " </td>";
                                str += "<td><input class='form-control' type='text' id='total' value=" + multivalue[i].Amount + " data-cell='E1' data-format='0.00' readonly  /><input type ='hidden' id ='row_tot' class='row_tot' name ='row_tot' ><input type ='hidden' id ='type' class='type' name ='type' value='Order'></td>";
                                //  $x = $(tbl).find('tbody')[0];
                                $('#tblCustomers tbody').append('<tr>' + str + '</tr>');
                                AllOrderIDs = [];
                            }
                            else {

                                var str = "<td><input type='checkbox' class='case' /></td>";
                                str += '<td>' + (i + 1) + '</td>';
                                // str += "<td><input class='form-control autoc' type='text' style='display:none' data-validation='required' id='Item_1' name='item_name[]' ><select class='form-control item' id='Item' name='item_name[]' style='width: 100%;'><option value='0'>---Select Product Name---</option></select><input type ='hidden' id ='Hid_Pro_code' class='rrr' name ='Hid_Pro_code' value=" + multivalue[i].Product_Code + "><input type='hidden' id='pro_sale_code' value=" + multivalue[i].Sale_Code + "><input type='hidden' id='Hid_order_code'  name='Hid_order_code' value=" + multivalue[i].Trans_Order_No + " ><input type='hidden' id='hd_tax_value' class='hd_tax_value' value=" + multivalue[i].Tax_Val + "><br /><label style='padding-top: 8px;' class='lbl_Tax' id='id_tax'>Tax : " + multivalue[i].Tax_Name + "</label><label style='float:right; padding-top:10px; font-size: 12px;' class='lbl_tax_value' id='lbl_tax_value'></label><input type='hidden' id='tax_div_value' /><input type='hidden' id='tax_id' /></td >";
                                str += "<td><input class='form-control autoc' type='text' style='display:none' data-validation='required' id='Item_1' t='" + multivalue[i].Product_Name + "'  value='" + multivalue[i].Product_Code + "' name='item_name[]' ><select class='form-control item' id='Item" + i + "' name='item_name[]' style='width: 100%;'><option></option></select><input type ='hidden' id ='Hid_Pro_code' class='rrr' name ='Hid_Pro_code' value=" + multivalue[i].Product_Code + "><input type='hidden' id='pro_sale_code' value=" + multivalue[i].Sale_Code + "><input type='hidden' id='Hid_order_code'  name='Hid_order_code' value=" + multivalue[i].Trans_Order_No + " ><input type='hidden' id='hd_tax_value' class='hd_tax_value' value=" + multivalue[i].Tax_Val + "><br /><label style='padding-top: 8px;' class='lbl_Tax' id='id_tax'>Tax : " + multivalue[i].Tax_Name + "</label><label style='float:right; padding-top:10px; font-size: 12px;' class='lbl_tax_value' id='lbl_tax_value'></label><input type='hidden' id='tax_div_value' /><input type='hidden' id='tax_id' /></td >";
                                str += `<td><select class="form-control unit" name="unit" >${bindunit1}</select></td>`;
                                str += "<td><input class='form-control' type='text' id='Price_' name='price[]' data-cell='C1' data-format='0.00' data-validation='required' readonly=''  value=" + multivalue[i].Rate + " /><input type='hidden' name='stkval' stkgood='0' stkdamage='0' /></td>";
                                str += "<td><input class='form-control validate' type='text' data-validation='required' id='Order_qty' readonly autocomplete='off'  name='Order_quantity[]' data-cell='K1' data-format='0' value=" + multivalue[i].Sale_qt + " /></td>";
                                str += "<td><input class='form-control validate' type='text' data-validation='required' id='english' autocomplete='off'  name='quantity[]' data-cell='D1' data-format='0' value=" + multivalue[i].Sale_qt + " /></td>";
                                str += "<td><input class='form-control validate' type='text' autocomplete='off' id='Dis' name='dis[]' data-cell='Y1' data-format='0' data-validation='required' value=" + multivalue[i].Discount + " ><input class='form-control hid' type='hidden' id='hid_dis' value=" + dis_val + "> </td>";
                                str += "<td><input class='form-control validate' type='text' id='Free' autocomplete='off' name='free[]' data-cell='I1' data-format='0'  data-validation='required' value=" + multivalue[i].Free + " ></td><td style='display:none' ><input type='hidden' class='of_pro_name'  name='of_pro_name' value='" + multivalue[i].Off_Pro_Name + "' ></td><td style='display:none' ><input type='hidden' class='of_pro_code'  name='of_pro_code' value=" + multivalue[i].Off_Pro_Code + " ></td><td style='display:none;'<label id=" + multivalue[i].Product_Code + " name='free' class='fre' freecqty=" + multivalue[i].Free + " cqty=" + multivalue[i].Quantity + " freepro=" + multivalue[i].Product_Code + " unit=" + multivalue[i].Off_Pro_Unit + ">" + multivalue[i].Free + '' + multivalue[i].Off_Pro_Unit + "</lable></td> <td style='display:none' ><input type='hidden' id=" + multivalue[i].Product_Code + " class='fre1' name='fre1' >" + multivalue[i].Free + "</td><td style='display:none' ><input type='hidden' id='erp_code' class='erp_code' value=" + multivalue[i].E_Code + " </td>";
                                str += "<td><input class='form-control' type='text' id='total' value=" + multivalue[i].Amount + " data-cell='E1' data-format='0.00' readonly  /><input type ='hidden' id ='row_tot' class='row_tot' name ='row_tot'><input type ='hidden' id ='type' class='type' name ='type' value='Add'></td>";
                                $x = $(tbl).find('tbody')[0];
                                $($x).append('<tr>' + str + '</tr>');
                                d(i);
                                $('#Item' + i + '').val(multivalue[i].Product_Code);
                                $('#Item' + i + '').chosen();
                                AllOrderIDs = [];
                            }
                        }
                        get();
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

                function get() {

                    $("#tblCustomers >tbody >tr").each(function () {
                        // loadddl(arr);

                        var row = $(this).closest("tr");
                        idx = $(row).index();

                        var price = $(this).find('[id*=Price_]').val();
                        var qty = $(this).find('[id*=english]').val();
                        var dis = $(this).find('[id*=Dis]').val();
                        var dis_cal = price * qty * (dis / 100);

                        $(this).find('[id*=hid_dis]').text(dis_cal.toFixed(2));
                        var cal = ((price * qty) - ((price * qty) * (dis / 100)));
                        if (cal == "0") {
                            $(this).find('[id*=total]').val('');
                        }
                        else {
                            $(this).find('[id*=total]').val(cal.toFixed(2));
                        }

                        var tax_value = $(this).find('[id*=hd_tax_value]').val();
                        if (isNaN(tax_value)) { tax_value = 0; }
                        var tot = Math.abs(tax_value / 100 * $(this).find('[id*=total]').val()).toFixed(2);
                        //if (isNaN(tot)) { tot = 0;}
                        if (tot == "0" || tot == "0.00") {
                            $(this).find('[id*=lbl_tax_value]').text('');
                        }
                        else {
                            $(this).find('[id*=lbl_tax_value]').text(tot);
                        }
                        var Divided_value = tot / 2;
                        // $(this).find('[id*=lbl_tax_value]').text(tot);
                        $(this).find('[id*=tax_div_value]').val(Divided_value.toFixed(2));
                        all();
                    });

                };

                function all() {

                    var CGSTTotal = 0;
                    $("[id*=tax_div_value]").each(function () {

                        var ta = $(this).val();
                        if (ta != "") {
                            CGSTTotal = CGSTTotal + parseFloat($(this).val());
                        }
                    });

                    $("[id*=Tax_CGST]").val(CGSTTotal.toFixed(2));
                    $("[id*=Tax_SGST]").val(CGSTTotal.toFixed(2));

                    var TaxTotal = 0;
                    $("[id*=lbl_tax_value]").each(function () {
                        var ta = $(this).text();
                        if (ta != "") {
                            TaxTotal = TaxTotal + parseFloat($(this).text());
                        }
                    });

                    $("[id*=Tax_GST]").val(TaxTotal.toFixed(2));


                    var dis_Tot = 0;
                    $("[id*=hid_dis]").each(function () {
                        // $("#hid_dis").each(function () {
                        // var x = ($(this).closest("tr").find('[id*=Price_]').val() * $(this).closest("tr").find('[id*=english]').val()) * ($(this).text() / 100);
                        var x = $(this).text();
                        if (isNaN(x) || x == "") x = 0;

                        dis_Tot = (dis_Tot + parseFloat(x));

                        //dis_Tot = dis_Tot + parseFloat($(this).val() || 0);
                    });
                    $("[id*=Txt_Dis_tot]").val(dis_Tot.toFixed(2));



                    var grandTotal = 0;
                    $("[id*=total]").each(function () {

                        var row = $(this).closest("tr");
                        idx = $(row).index();

                        var gt = $(this).val();
                        if (gt != "") {
                            grandTotal = grandTotal + parseFloat($(this).val());
                            row.find('#row_tot').val(parseFloat(gt) + parseFloat(row.find('#lbl_tax_value').text()));
                        }
                    });
                    $("[id*=sub_tot]").val(grandTotal.toFixed(2));
                    $("[id*=in_Tot]").val(parseFloat(parseFloat(grandTotal.toFixed(2)) + parseFloat(TaxTotal.toFixed(2))).toFixed(2));
                    $("[id*=Amt_Tot]").val(parseFloat(parseFloat(grandTotal.toFixed(2)) + parseFloat(TaxTotal.toFixed(2))).toFixed(2));

                    //$("[id*=in_Tot]").val(parseFloat(grandTotal.toFixed(2)) + parseFloat(TaxTotal.toFixed(2)));
                    //$("[id*=Amt_Tot]").val(parseFloat(grandTotal.toFixed(2)) + parseFloat(TaxTotal.toFixed(2)));


                    var adv_amt = $(document).find('#Ad_Paid').val();
                    if (adv_amt != '') {
                        $(document).find('#Amt_Tot').val(parseFloat($(document).find('#Amt_Tot').val()) - parseFloat(adv_amt));
                    }

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

                $(document).on('keyup', '[id*=english]', function () {

                    var row = $(this).closest("tr");
                    idx = $(row).index();

                    if (row.find('.autoc').val() == "") {
                        // if (row.find('.chzn-done').children("option:selected").val() == "0") {
                        alert('Select Product');
                        $(this).val("");
                        return false;
                        $(this).closest('tr').find('.item').children("option:selected").focus();
                    }

                    if (parseFloat($(this).val()) > parseFloat(row.find('#Order_qty').val())) {
                        alert('Check the Ordered Quantity');
                        $(this).val(0);
                        $(this).focus();
                        return false;
                    }

                    if (!jQuery.trim($(this).val()) == '') {
                        if (!isNaN(parseFloat($(this).val()))) {

                            qty_fun(row);
                            multivalue[idx].Product_Code = row.find('.autoc').val();
                            multivalue[idx].Product_Name = row.find('.autoc').attr('t');
                            multivalue[idx].Sale_qt = $(this).val();
                            multivalue[idx].Free = row.find('[id*=Free]').val();
                            multivalue[idx].Discount = row.find('[id*=Dis]').val();
                            multivalue[idx].Off_Pro_Code = row.find('.of_pro_code').val();
                            multivalue[idx].Off_Pro_Name = row.find('.of_pro_name').val();
                            multivalue[idx].Off_Pro_Unit = row.find('.fre').attr('freecqty');

                            var dis = row.find('[id*=Dis]').val();
                            var dis_tot = row.find('[id*=Price_]').val() * row.find('[id*=english]').val() * (dis / 100);
                            row.find('[id*=hid_dis]').text(dis_tot.toFixed(2));
                            var ca = row.find('[id*=Price_]').val() * row.find('[id*=english]').val() - dis_tot;
                            row.find('[id*=total]').val(ca.toFixed(2));
                            multivalue[idx].Amount = ca.toFixed(2);
                            var tax_value = row.find('[id*=hd_tax_value]').val();
                            var tot = Math.abs(tax_value / 100 * row.find('[id*=total]').val()).toFixed(2);
                            var Divided_value = tot / 2;
                            row.find('[id*=lbl_tax_value]').text(tot);
                            // row.find('[id*=lbl_tax_value]').text(tot.toFixed(2));
                            row.find('[id*=tax_div_value]').val(Divided_value.toFixed(2));

                        }
                    } else {
                        $(this).val('');
                        qty_fun(row);

                        row.find('#Dis').val('');
                        row.find('#hid_dis').text('');
                        row.find('#Free').text('');

                        // if (ds == "") { ds = 0;}

                        //  var v = (row.find('[id*=Price_]').val() * row.find('[id*=english]').val()) * (ds / 100);

                        //var v = row.find('[id*=Price_]').val() * row.find('[id*=english]').val();
                        //  row.find('[id*=total]').val(v.toFixed(2));

                        row.find('[id*=total]').val('');

                        // var tax_value = row.find('[id*=hd_tax_value]').val();
                        //  var tot = Math.abs(tax_value / 100 * row.find('[id*=total]').val()).toFixed(2);
                        //  var div = row.find('[id*=tax_div_value]').val();
                        //  var xx = tot * div
                        row.find('[id*=lbl_tax_value]').text('');
                        row.find('[id*=tax_div_value]').val('');
                        //row.find('[id*=Dis]').val("");
                    }
                    // all();
                    get();

                });
                //dis_count
                $(document).on('keyup', '[id*=Dis]', function () {
                    // $("#tblCustomers >tbody >tr").each(function () {
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


                                var Divided_value = tot / 2;
                                row.find('[id*=lbl_tax_value]').text(tot);

                                row.find('[id*=tax_div_value]').val(Divided_value);

                                //var tax_value = row.find('[id*=addetype]').val();
                                //var tot = Math.abs(tax_value / 100 * row.find('[id*=total]').val()).toFixed(2);
                                //row.find('[id*=taxamt]').val(tot);
                            }

                        }
                    } else {
                        $(this).val('');

                        if (isNaN(dis)) dis = 0;

                        //  Val = (row.find('[id*=Price_]').val() * row.find('[id*=english]').val());
                        Val = (row.find('[id*=Price_]').val() * row.find('[id*=english]').val()) * (dis / 100);
                        row.find('#hid_dis').text(Val.toFixed(2));
                        row.find('[id*=total]').val(((row.find('[id*=Price_]').val() * row.find('[id*=english]').val()) - Val).toFixed(2));

                        var tot_value = row.find('[id*=total]').val();

                        var tot = Math.abs(tot_value / 100 * row.find('[id*=hd_tax_value]').val()).toFixed(2);


                        var Divided_value = tot / 2;
                        row.find('[id*=lbl_tax_value]').text(tot);

                        row.find('[id*=tax_div_value]').val(Divided_value);
                    }
                    //get();
                    all();

                });

                $('[id*=Add]').on('click', function () {

                    itm = {}
                    itm.Trans_Order_No = '';
                    itm.Product_Code = '';
                    itm.Sale_Code = '';
                    itm.Product_Name = '';
                    itm.Rate = "0";
                    itm.Unit = '0';
                    itm.Quantity = "0";
                    itm.Quantity = "0";
                    itm.Sale_qt = "0";
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
                    multivalue.push(itm);

                    count = $("#tblCustomers >tbody >tr").length + 1;
                    var data = "<tr><td><input type='checkbox' class='case'/></td><td><span id='snum'>" + count + ".</span></td>";
                    // data += "<td><input class='form-control autoc' type='text' style='display:none' data-validation='required' id='Item_1' name='item_name[]' ><select class='form-control item' id='Item' name='item_name[]' style='width: 100%;'><option value='0'>---Select Product Name---</option></select><input type ='hidden' id ='Hid_Pro_code' class='rrr' name ='Hid_Pro_code'><input type='hidden' id='pro_sale_code' class='pro_sale_code'><input type='hidden' id='hd_tax_value' class='hd_tax_value' /><label style='padding-top:10px; font-size: 12px;' class='lbl_Tax' id='id_tax'></label><label style='float:right; padding-top:10px; font-size: 12px;' class='lbl_tax_value' id='lbl_tax_value'></label><input type='hidden' id='tax_div_value' /><input type='hidden' id='tax_id' /></td> " +
                    data += "<td><input class='form-control autoc' type='text' style='display:none' data-validation='required' id='Item_1' name='item_name[]' ><select class='form-control item' id='Item" + count + "' name='item_name[]' style='width: 100%;'><option value='0'>---Select Product Name---</option></select><input type ='hidden' id ='Hid_Pro_code' class='rrr' name ='Hid_Pro_code'><input type='hidden' id='pro_sale_code' class='pro_sale_code'><input type='hidden' id='hd_tax_value' class='hd_tax_value' /><label style='padding-top:10px; font-size: 12px;' class='lbl_Tax' id='id_tax'></label><label style='float:right; padding-top:10px; font-size: 12px;' class='lbl_tax_value' id='lbl_tax_value'></label><input type='hidden' id='tax_div_value' /><input type='hidden' id='tax_id' /></td> " +
                        // < input type = 'hidden' id = 'hide' class='hide' /> 
                        "<td id='sel_unit'><select id='unit' class='form-control unit'><option value='0'>Select</option></select></td>" +
                        "<td><input class='form-control' data-validation='required' autocomplete='off' type='text' id='Price_' readonly='' name='price[]'  data-cell='C" + i + "' data-format='0.00' /></td> " +
                        // "<td></td>" +
                        "<td><input class='form-control validate' type='text' data-validation='required' autocomplete='off' id='Order_qty' autocomplete='off'  name='Order_quantity[]' data-cell='K" + i + "' data-format='0' /></td>" +
                        "<td><input class='form-control validate' data-validation='required' autocomplete='off' type='text' id='english' name='quantity[]'  data-cell='D" + i + "' data-format='0' /></td> " +
                        "<td><input class='form-control validate' data-validation='required' autocomplete='off' type='text' id='Dis' name='dis[]'  data-cell='Y" + i + "' data-format='0' /><input class='form-control hid' type='hidden' id='hid_dis'></td> " +
                        "<td><input class='form-control validate' data-validation='required' autocomplete='off' type='text' id='Free' name='free[]'  data-cell='I" + i + "' data-format='0' /></td><td style='display:none' ><input type='hidden' class='of_pro_name'  name='of_pro_name' ></td><td style='display:none' ><input type='hidden' class='of_pro_code'  name='of_pro_code' ></td><td style='display:none;'<label name='free' class='fre'></lable></td><td style='display:none' ><input type='hidden' class='fre1' name='fre1' ></td><td style='display:none' ><input type='hidden' id='erp_code' class='erp_code' ></td>" +
                        // "<td><input class='form-control validate' data-validation='required' autocomplete='off' type='text' id='Free' name='free[]'  data-cell='I" + i + "' data-format='0' /></td><td><input type='hidden' id='of_pro_code' class='of_pro_code' ></td><td><input type='hidden' id='of_pro_unit' class='of_pro_unit'></td><td><input type='hidden' id='of_pro_name' class='of_pro_name' ></td> " +
                        "<td><input class='form-control' id='total' autocomplete='off' readonly name='total[]' type='text' data-format='0.00' data-cell='E" + i + "'   /><input type ='hidden' id ='row_tot' class='row_tot' name ='row_tot'><input type ='hidden' id ='type' class='type' name ='type' value='Add'></td> " +
                        "</tr>";
                    $('#tblCustomers').append(data);
                    d(count);
                    $('#Item' + count + '').chosen();
                    //$('#unit').chosen();
                });


                function d(CountValue) {
                    for (var b = 0; All_Product.length > b; b++) {
                        $('#Item' + CountValue + '').append($("<option></option>").val(All_Product[b].Product_Detail_Code).html(All_Product[b].Product_Detail_Name)).trigger('chosen:updated').css("width", "100%");;;
                    }
                }


                //function d(CountValue) {
                //    $.ajax({
                //        type: "POST",
                //        contentType: "application/json; charset=utf-8",
                //        url: "Invoice_Entry1.aspx/GetProducts",
                //        dataType: "json",
                //        async: false,
                //        success: function (data) {
                //            if (data.d.length > 0) {
                //                $.each(data.d, function (data, value) {
                //                    $('#Item' + CountValue + '').append($("<option></option>").val(this['Value']).html(this['Text'])).trigger('chosen:updated').css("width", "100%");;;
                //                    // $('#Item').append($("<option></option>").val(this['Value']).html(this['Text'])).trigger('chosen:updated').css("width", "100%");;;
                //                });
                //            }
                //        },
                //        error: function (data) {
                //            alert(JSON.stringify(data));
                //        }
                //    });

                //}


                $(document).on("change", ".item", function () {

                    var product_name = $(this).children("option:selected").text();
                    var product_code = $(this).children("option:selected").val();
                    var row = $(this).closest("tr");
                    var indx = $(row).index();

                    var Pro_filter = [];

                    Pro_filter = All_Product.filter(function (t) {
                        return (t.Product_Detail_Code == product_code);
                    });

                    // for (var i = 0; i < data.d.length; i++) {
                    var ans1 = [];
                    ans1 = Allrate.filter(function (t) {
                        return (t.Product_Detail_Code == product_code);
                    });
                    var ddlUnit = row.find('.unit');
                    ddlUnit.empty();
                    //.append('<option selected="selected" value="0">Select</option>');

                    //var pro_filter = [];

                    //pro_filter = multivalue.filter(function (s) {
                    //    return (s.Product_Code == product_code);
                    //});

                    // if (pro_filter.length > 0) {
                    bindunit1 = {};
                    if (ans1[0].product_unit == ans1[0].Product_Sale_Unit) {

                        alert('Product Already Added');
                        row.remove();
                        multivalue.splice(indx, 1)
                        return false;
                    }
                    else {

                        bindunit1 = '<option value="0">Select Unit</option>';
                        bindunit1 += '<option  value=' + ans1[0].product_unit + '>' + ans1[0].product_unit + '</option>';
                        bindunit1 += '<option value=' + ans1[0].Product_Sale_Unit + '>' + ans1[0].Product_Sale_Unit + '</option>';
                    }

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
                    row.find('#tax_id').val(data.d[0].Tax_Id);
                    row.find('#id_tax').text('Tax :' + ' ' + (Pro_filter[0].Tax_Name || 0));
                    row.find('#hd_tax_value').val(data.d[0].Tax_Val);
                    row.find('#erp_code').val(Pro_filter[0].Sample_Erp_Code);
                    row.find('.autoc').val(Pro_filter[0].product_Detail_Code);
                    row.find('.autoc').attr('t', Pro_filter[0].Product_Detail_Name);

                    getscheme(row.find('#pro_sale_code').val(), row.find('#english').val(), '', row.find('.unit :selected').val(), row, row.find(".fre1").text(), row.find('#Hid_Pro_code').val(), indx, product_name);
                    row.find('#Hid_Pro_code').val(Pro_filter[0].product_Detail_Code);


                    multivalue[indx].Trans_Order_No = 0;
                    //  multivalue[indx].Product_Code = data.d[0].product_Detail_Code;
                    multivalue[indx].Sale_Code = data.d[0].Sale_Erp_Code;
                    //  multivalue[indx].Product_Name = data.d[0].Product_Detail_Name;
                    multivalue[indx].Tax_Id = data.d[0].Tax_Id;
                    multivalue[indx].Tax_Val = data.d[0].Tax_Val;
                    multivalue[indx].E_Code = data.d[0].Sample_Erp_Code;
                    multivalue[indx].Tax_Name = data.d[0].Tax_Name;
                    // qty_fun(row);
                    get();
                    //}
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

                    if (pro_filter.length > 0) {

                        alert('Product Unit already Added');
                        row.find('.unit').val('0')
                        return false;
                    }

                    else {

                        var ans2 = [];
                        ans2 = Allrate.filter(function (t) {
                            return (t.Product_Detail_Code == selected_pro_code);
                        });

                        if (ans2.length > 0) {

                            if (ans2[0].product_unit == Selected_Unit) {

                                row.find('#Price_').val(ans2[0].Distributor_Price);
                                multivalue[indx].Rate = ans2[0].Distributor_Price;
                                multivalue[indx].Unit = Selected_Unit;

                                //  cq = row.find('#english').val();
                                //  fr = row.find('.fre1').text();
                                // dis = row.find('.tddis').text();
                                //  CQvalue = cq * parseFloat(row.find(".erp_code").val());
                                //  getscheme(row.find('.erp_code').val(), cq, CQvalue, row.find('.unit :selected').val(), row, row.find(".fre1").text(), row.find('#Hid_Pro_code').text(), indx, row.find('.chzn-done :selected').text());
                                qty_fun(row);
                                get();
                            }
                            else {
                                row.find('#Price_').val(ans2[0].MRP_Price);
                                multivalue[indx].Rate = ans2[0].MRP_Price;
                                multivalue[indx].Unit = Selected_Unit;
                                //  cq = row.find('#english').val();
                                //  CQvalue = cq;
                                //  cq = cq / parseFloat(row.find(".erp_code").val());
                                //  getscheme(row.find('.erp_code').val(), cq, CQvalue, row.find('.unit :selected').val(), row, row.find(".fre1").text(), row.find('#Hid_Pro_code').text(), indx, row.find('.chzn-done :selected').text());
                                qty_fun(row);
                                get();
                            }
                        }
                    }

                });

                $(".delete").on('click', function () {
                    $(".case:checked").each(function () {
                        row = $(this).closest('tr');

                        var order_id = row.find('#Hid_order_code').val();
                        var prod = row.find('#Hid_Pro_code').val();

                        //var del_val = [];

                        //for (var z = 0; z < AllProduct.length; z++) {

                        //    del_val = AllProduct.filter(function (a) {
                        //        return (a.Order_No == order_id);
                        //    });

                        //}

                        //var sss = [];
                        //for (var c = 0; c < del_val[0].Order_Details.length; c++) {
                        //    sss = del_val[0].Order_Details.filter(function (p) {
                        //        return (p.Product_Code == prod);
                        //    });
                        //}


                        idx = $(row).index();
                        multivalue.splice(idx, 1);
                        getscheme(row.find('#pro_sale_code').val(), '', '', row.find('.unit :selected').val(), row, row.find('.fre').attr('freecqty'), row.find('#Hid_Pro_code').val(), idx, row.find(".autoc").text());

                        $('.case:checkbox:checked').parents("tr").remove();
                        //$('.case:checkbox:checked').remove();
                        $('.check_all').prop("checked", false);
                        check();

                        if ($("#tblCustomers >tbody >tr").length > 0) {
                            get();
                        }
                        else {
                            var grandTotal = 0;
                            $("[id*=total]").each(function () {

                                grandTotal = Math.abs(grandTotal + parseFloat($(this).val()));
                            });
                            $("[id*=sub_tot]").val(grandTotal.toFixed(2));
                            $("[id*=in_Tot]").val(grandTotal.toFixed(2));
                            $("[id*=Amt_Tot]").val(grandTotal.toFixed(2));
                            //var TaxTotal = 0;
                            //$("[id*=taxamt]").each(function () {

                            //    TaxTotal = TaxTotal + parseFloat($(this).val() == "" ? 0 : parseFloat($(this).val()));

                            //});
                            $("[id*=Txt_Dis_tot]").val(grandTotal.toFixed(2));
                            $("[id*=Tax_GST]").val(grandTotal.toFixed(2));
                            $("[id*=Tax_CGST]").val(grandTotal.toFixed(2));
                            $("[id*=Tax_SGST]").val(grandTotal.toFixed(2));
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

                        var totalvalue = $("[id*=in_Tot]").val();
                        if (totalvalue == "" || typeof totalvalue == "undefined") {
                            $("[id*=Ad_Paid]").val('');
                            alert("Please Select Order");
                            return false;
                        }
                        else {
                            adTotal = totalvalue - tal;
                            $("[id*=Amt_Tot]").val(adTotal.toFixed(2));
                        };
                    });
                });

                function isValidDate(s) {
                    var bits = s.split('/');
                    var d = new Date(bits[2] + '/' + bits[1] + '/' + bits[0]);
                    return !!(d && (d.getMonth() + 1) == bits[1] && d.getDate() == Number(bits[0]));
                }

                //$(document).on('change', '#datepicker', function () {
                //    console.log($(this).val());
                //    if ($(this).val().length > 0) {
                //        if (isValidDate($(this).val())) {
                //            var fit_start_time = $(this).val();
                //            var FromDate = fit_start_time.split("/").reverse().join("-");
                //            var dts = new Date(FromDate);
                //            var cr = dts.getDate() + '/' + (dts.getMonth() + 1) + '/' + dts.getFullYear();
                //            $('#datepicker').datepicker("destroy");
                //            $('#datepicker').datepicker({ dateFormat: 'dd/mm/yy', minDate: cr, defaultDate: cr });
                //            $('#datepicker').val('');
                //        }
                //        else {
                //            alert('Invalid Date Enter or Select Correct Date..!');
                //            $(this).val('');
                //            $(this).focus();

                //        }
                //    }
                //});


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
                        alert('Selected date must be greater than today date');
                        $(this).val('');
                    }
                });


                $("#datepicker").on('change', function () {
                    var selectedDate = $(this).val().split("/").join("-");
                    var todaysDate = new Date().ddmmyyyy();
                    if (selectedDate < todaysDate) {
                        alert('Selected date must be greater than today date');
                        $(this).val('');
                    }
                });


                //         $(document).on('change', '#datepicker1', function () {
                //    console.log($(this).val());
                //    if ($(this).val().length > 0) {
                //        if (isValidDate($(this).val())) {
                //            var fit_start_time = $(this).val();
                //            var FromDate = fit_start_time.split("/").reverse().join("-");
                //            var dts = new Date(FromDate);
                //            var cr = dts.getDate() + '/' + (dts.getMonth() + 1) + '/' + dts.getFullYear();
                //            $('#datepicker').datepicker("destroy");
                //            $('#datepicker').datepicker({ dateFormat: 'dd/mm/yy', minDate: cr, defaultDate: cr });
                //            $('#datepicker').val('');
                //        }
                //        else {
                //            alert('Invalid Date Enter or Select Correct Date..!');
                //            $(this).val('');
                //            $(this).focus();

                //        }
                //    }
                //});

                $(document).ready(function () {
                    $(document).on('click', '.pleasewait', function () {

                        window.location = "../Stockist/Invoice_Print.aspx?Or_Date=" + encodeURIComponent($('#<%=InvoiceNumber.ClientID%>').val()) + "&Dis_code=" + encodeURIComponent(stockistcode) + "&Cus_code=" + encodeURIComponent($("#recipient-name option:selected").val());

                    });
                });

                var approve = 0;
                $(document).on('click', '.btnsave', function (e) {
                    approve += 1;
                    if (approve == "1") {
                        var Customer_Code = $('#recipient-name :selected').val();
                        var Customer_Name = $('#recipient-name :selected').text();

                        if (Customer_Code == "0") {
                            approve = 0;
                            alert("Please Select Customer.");
                            return false;
                        }

                        //var pay_term = $('#Sel_Pay_Term option:selected').text();
                        //if (pay_term == "Select") {
                        //    approve = 0;
                        //    alert('Select Payment Mode'); $('#Sel_Pay_Term').focus(); return false;
                        //}


                        //if ($('input[type=checkbox]:checked').length == 0) {
                        //    approve = 0;
                        //    alert("Please select Order ID");
                        //    return false;
                        //}

                        //var Pay_Due = $('#datepicker').val();
                        //if (Pay_Due.length <= 0) {
                        //    approve = 0;
                        //    alert('Enter Payment Due.!!'); $('#datepicker').focus(); return false;
                        //}

                   <%-- var inv_Date = $('#<%=Txt_in_date.ClientID%>').val();
                    if (inv_Date.length <= 0) { alert('Enter Invoice Date.!!'); $('#Txt_in_date').focus(); return false; }--%>

                        //var ship_met = $('#Sel_Shi_Med option:selected').text();
                        //if (ship_met == "Select") {
                        //    approve = 0;
                        //    alert('Select Shipping Mode!!'); $('#Sel_Shi_Med').focus(); return false;
                        //}


                        //var Del_Date = $('#datepicker1').val();
                        //if (Del_Date.length <= 0) {
                        //    approve = 0;
                        //    alert('Enter Delivery Date.!!'); $('#datepicker1').focus(); return false;
                        //}



<%--                    var ship_term = $('#<%=Txt_Shp_Term.ClientID%>').val();
                    if (ship_term.length <= 0) { alert('Enter Shipping Term!!'); $('#Txt_Shp_Term').focus(); return false; }--%>
                        var sub_tot = $('#sub_tot').val();
                        if (sub_tot.length <= 0) {
                            approve = 0;
                            alert('Enter Subtotal!!'); $('#sub_tot').focus(); return false;
                        }

                        var Gst_Value = $('#Tax_GST').val();
                        if (Gst_Value.length <= 0) {
                            approve = 0;
                            alert('Enter Gst Total!!'); $('#Tax_GST').focus(); return false;
                        }


                        var tax_CGST = $('#Tax_CGST').val();
                        if (tax_CGST.length <= 0) {
                            approve = 0;
                            alert('Enter CGST Total!!'); $('#Tax_CGST').focus(); return false;
                        }

                        var tax_SGST = $('#Tax_SGST').val();
                        if (tax_SGST.length <= 0) {
                            approve = 0;
                            alert('Enter SGST Total!!'); $('#Tax_SGST').focus(); return false;
                        }

                        var total = $('#in_Tot').val();
                        if (total.length <= 0) {
                            approve = 0;
                            alert('Enter Total!!'); $('#in_Tot').focus(); return false;
                        }
                        var ad_paid = $('#Ad_Paid').val();
                        if (ad_paid.length <= 0) {
                            approve = 0;
                            alert('Enter Advanced Paid!!'); $('#Ad_Paid').focus(); return false;
                        }
                        var amt_due = $('#Amt_Tot').val();
                        if (amt_due.length <= 0) {
                            approve = 0;
                            alert('Enter Amount Due!!'); $('#Amt_Tot').focus(); return false;
                        }
                        var remark = $('#notes').val();
                        var Dis_Tot = $('#Txt_Dis_tot').val();
                        var Order_No = $('#<%=Hid_orderno.ClientID%>').val();
                        //check Out_standing_Amt

                        var customercode = $("#recipient-name option:selected").val();

                        var customername = $("#recipient-name option:selected").html();

                        var orderid = $('input[type=checkbox]:checked').map(function () {
                            return $(this).val();
                        }).get().join();


                        var Stk = [];
                        // if (amt_due > 0) {

                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            url: "Invoice_Entry1.aspx/Get_AllValues",
                            data: "{'Cus_code':'" + $("#recipient-name option:selected").val() + "'}",
                            dataType: "json",
                            success: function (data) {


                                var Credit_Limit = 0;
                                var obj = data.d;
                                console.log(data.d);

                                if (obj.length >= 0) {
                                    var Ob = obj.split(',');
                                    console.log(Ob);
                                    var out_sta = Ob[0];
                                    console.log(Ob[0]);
                                    Credit_Limit = Ob[1];
                                    console.log(Ob[1]);
                                    console.log(out_sta);
                                    var ou_sta_tot = Number(out_sta) + Number(amt_due);
                                    console.log(ou_sta_tot);
                                    // if (Number(ou_sta_tot) >= Number(Credit_Limit)) {
                                    if (Number(Credit_Limit) >= Number(ou_sta_tot)) {


                                        dayarr = [];
                                        mainarr = [];
                                        if (pay_term.length <= 0) {
                                            alert('select Payment Term');
                                            return false;
                                        }
                                        if (Sel_Shi_Med.length <= 0) {
                                            alert('select Shipping Method');
                                            return false;
                                        }
                                        var oqty = $(this).find("#english").attr('pv_Val') || 0;

                                        var customercode = $(document).find("#tblCustomers tr td").find("#recipient-name option:selected").val();
                                        //var custumorname = ("#Retailer_Details option:selected").text();

                                        od = $('#example-multiple-selected option').html().split('_');
                                        var dss = od[1];
                                        ar = dss.split('/');
                                        da = ar[1] + '/' + ar[0] + '/' + ar[2];

                                        mainarr.push({

                                            sf_code: stockistcode,
                                            sf_name: stockistname,
                                            dis_code: stockistcode,
                                            dis_name: stockistname,
                                            cus_code: retailer_ID,
                                            cus_name: customername,
                                            order_date: da,
                                            inv_date: $(document).find(".invoice").val(),
                                            del_date: $(document).find(".dd").val(),
                                            pay_term: pay_term,
                                            pay_due: $(document).find(".pd").val(),
                                            ship_mtd: ship_met,
                                            ship_term: 0,
                                            sub_total: sub_tot,
                                            tax_total: Gst_Value,
                                            cgst: tax_CGST,
                                            sgst: tax_SGST,
                                            total: total,
                                            adv_paid: ad_paid,
                                            Amt_due: amt_due,
                                            remark: remark,
                                            Dis_total: Dis_Tot,
                                            order_no: orderid,
                                            Div_Code: div_code
                                        });

                                        var dtls_tab = document.getElementById("tblCustomers");
                                        var nrows1 = dtls_tab.rows.length;
                                        var Ncols = dtls_tab.rows[0].cells.length;
                                        if (nrows1 > 1) {
                                            var ch = true;
                                            var arr = [];
                                            $('#tblCustomers > tbody > tr').each(function () {

                                                var Prd_code = $(this).find('[id*=Hid_Pro_code]').val() || 0;
                                                var un = $(this).find('.unit option:selected').text() || 0;
                                                var res = [];
                                                res = Allrate.filter(function (t) {
                                                    return (t.Product_Detail_Code == Prd_code);
                                                });

                                                if (res[0].product_unit == res[0].Product_Sale_Unit) {
                                                    var qty = $(this).find('#Order_qty').val();
                                                    qty1 = $(this).find('#Order_qty').val();
                                                    var sales_qty = $(this).find('[id*=english]').val();
                                                }
                                                else {
                                                    if (res[0].product_unit == un) {
                                                        var qty = parseFloat($(this).find('#Order_qty').val()) * res[0].Sample_Erp_Code;
                                                        var qty1 = $(this).find('#Order_qty').val() || 0;
                                                        var sales_qty = $(this).find('[id*=english]').val() || 0;
                                                    }
                                                    else {
                                                        var qty = $(this).find('#Order_qty').val() || 0;
                                                        qty1 = $(this).find('#Order_qty').val();
                                                        var sales_qty = $(this).find('[id*=english]').val() || 0;
                                                    }
                                                }

                                                var sno = $(this).find('[id*=Hid_order_code]').val() || 0;
                                                var Prd_name = $(this).find('.autoc').attr('t') || 0;
                                                var price = $(this).find('[id*=Price_]').val() || 0;
                                                var dis = $(this).find('[id*=Dis]').val() || 0;
                                                var dis_val = $(this).find('#hid_dis').val() || 0;
                                                var free = $(this).find('[id*=Free]').val() || 0;
                                                var tot = $(this).find('[id*=total]').val() || 0;
                                                var over_all_tot = $(this).find('[id*=row_tot]').val() || 0;
                                                var Ut = $(this).find('.unit option:selected').text() || 0;
                                                var offer_unit = $(this).find('.fre').attr('unit') || 0;
                                                var offer_code = $(this).find('.of_pro_code').val() || 0;
                                                var offer_name = $(this).find('.of_pro_name').val() || 0;
                                                var Tax_code = $(this).find('[id*=lbl_tax_value]').text() || 0;
                                                var Tax_name = $(this).find('[id*=id_tax]').text() || 0;
                                                var ty = $(this).find('#type').val() || 0;

                                                taxArr = [];
                                                taxArr.push({
                                                    Trans_order_no: $(this).find('[id*=Hid_order_code]').val() || 0,
                                                    taxCode: $(this).find('.hd_tax_value').val() || 0,
                                                    tax_Name: $(this).find('.lbl_Tax').text() || 0,
                                                    value: (Number($(this).find('.hd_tax_value').val()) * Number(tot) / 100)

                                                });

                                                console.log(taxArr);
                                                dayarr.push({
                                                    Trans_order_no: sno,
                                                    Product_code: Prd_code,
                                                    Product_name: Prd_name,
                                                    Price: price,
                                                    Discount: dis,
                                                    Dis_v: dis_val,
                                                    Free: free,
                                                    Quantity: qty,
                                                    sale_q: sales_qty,
                                                    Quat: qty1,
                                                    Amount: tot,
                                                    Over_Tot: over_all_tot,
                                                    Product_Unit: Ut,
                                                    Offer_Product_Code: offer_code,
                                                    Offer_Product_Name: offer_name,
                                                    Offer_Product_Unit: offer_unit,
                                                    Tye: ty,
                                                    taxDtls: taxArr
                                                });
                                            });
                                        }

                                        var Add_order = [];
                                        var add_order_orderno = '';

                                        Add_order = dayarr.filter(function (a) {
                                            return (a.Tye == 'Add');
                                        });

                                        if (Add_order.length > 0) {

                                            var Add_orderval = 0;
                                            var Add_tot_net_weight = 0;

                                            var add_new_order_array = [];


                                            for (var t = 0; t < Add_order.length; t++) {

                                                Add_orderval = parseFloat(Add_order[t].Amount) + parseFloat(Add_orderval);
                                                Add_tot_net_weight = parseFloat(Add_order[t].Quat) + parseFloat(Add_tot_net_weight);

                                                add_new_order_array.push({

                                                    PCd: Add_order[t].Product_code,
                                                    PName: Add_order[t].Product_name,
                                                    Unit: Add_order[t].Product_Unit,
                                                    Rate: Add_order[t].Price,
                                                    Qty: Add_order[t].Quat,
                                                    Sub_Total: Add_order[t].Amount,
                                                    Free: Add_order[t].Free,
                                                    Dis_value: Add_order[t].Dis_v,
                                                    Discount: Add_order[t].Discount,
                                                    of_Pro_Code: Add_order[t].Offer_Product_Code,
                                                    of_Pro_Name: Add_order[t].Offer_Product_Name,
                                                    of_Pro_Unit: Add_order[t].Offer_Product_Unit
                                                });
                                            }

                                            //$.ajax({
                                            //    type: "POST",
                                            //    contentType: "application/json; charset=utf-8",
                                            //    async: false,
                                            //    url: "myOrders.aspx/saveorders",
                                            //    data: "{'NewOrd':'" + JSON.stringify(add_new_order_array) + "','Remark':'','Ordrval':'" + Add_orderval + "','RetCode':'" + Customer_Code + "','RecDate':'" + today + "','Ntwt':'" + Add_tot_net_weight + "','retnm':'" + Customer_Name + "','Type':'1','ref_order':''}",
                                            //    dataType: "json",
                                            //    success: function (data) {
                                            //        add_order_orderno = data.d;
                                            //        if (add_order_orderno.length > 0) {
                                            //            // var Add_Order_ID = data.d;
                                            //            orderid = orderid + ',' + add_order_orderno;
                                            //            mainarr[0]["order_no"] = orderid;
                                            //        }
                                            //        else {
                                            //            alert('Order Not Saved')
                                            //            return false;
                                            //        }

                                            //    },
                                            //    error: function (result) {
                                            //        approve = 0;
                                            //        alert(JSON.stringify(result));
                                            //    }
                                            //});
                                        }

                                        var Pen_order_ids = [];
                                        var Pen_orderval = 0;
                                        var Pen_tot_net_weight = 0;
                                        var pen_new_order_array = [];
                                        var pending_qty = '';

                                        Pen_order_ids = dayarr.filter(function (a) {
                                            return (parseFloat(a.sale_q) < parseFloat(a.Quat));
                                        });
                                        var pen_id = '';
                                        for (var c = 0; Pen_order_ids.length > c; c++) {

                                            if (pen_id.indexOf(Pen_order_ids[c].Trans_order_no) < 0) {

                                                pen_id += ',' + Pen_order_ids[c].Trans_order_no;
                                                //pending_qty =Pen_order_ids[c].
                                            }

                                            Pen_orderval = parseFloat(Pen_order_ids[c].Amount) + parseFloat(Pen_orderval);
                                            Pen_tot_net_weight = parseFloat(Pen_order_ids[c].Quat) + parseFloat(Pen_tot_net_weight);

                                            pen_new_order_array.push({

                                                PCd: Pen_order_ids[c].Product_code,
                                                PName: Pen_order_ids[c].Product_name,
                                                Unit: Pen_order_ids[c].Product_Unit,
                                                Rate: Pen_order_ids[c].Price,
                                                Qty: Pen_order_ids[c].sale_q, // not converted qty
                                                Sub_Total: Pen_order_ids[c].Amount,
                                                Free: Pen_order_ids[c].Free,
                                                Dis_value: Pen_order_ids[c].Dis_v,
                                                Discount: Pen_order_ids[c].Discount,
                                                of_Pro_Code: Pen_order_ids[c].Offer_Product_Code,
                                                of_Pro_Name: Pen_order_ids[c].Offer_Product_Name,
                                                of_Pro_Unit: Pen_order_ids[c].Offer_Product_Unit
                                            });
                                        }

                                        if (Pen_order_ids.length > 0) {

                                            //$.ajax({
                                            //    type: "POST",
                                            //    contentType: "application/json; charset=utf-8",
                                            //    async: false,
                                            //    url: "myOrders.aspx/saveorders",
                                            //    data: "{'NewOrd':'" + JSON.stringify(pen_new_order_array) + "','Remark':'','Ordrval':'" + Pen_orderval + "','RetCode':'" + Customer_Code + "','RecDate':'" + today + "','Ntwt':'" + Pen_tot_net_weight + "','retnm':'" + Customer_Name + "','Type':'2','ref_order':'" + pen_id + "'}",
                                            //    dataType: "json",
                                            //    success: function (data) {

                                            //        //var Add_Order_ID = data.d;
                                            //        //orderid = orderid + ',' + Add_Order_ID;
                                            //        // mainarr[0]["order_no"] = orderid;

                                            //        //if (data.d.length <= 0) {
                                            //        //    alert('Pending Order Not Saved');
                                            //        //    return false;
                                            //        //}
                                            //    },
                                            //    error: function (result) {
                                            //        approve = 0;
                                            //        alert(JSON.stringify(result));
                                            //    }
                                            //});

                                        }

                                        var objmain = {};
                                        objmain['MTED'] = mainarr;
                                        objmain['TED'] = dayarr;
                                        // objmain['DED'] = dayarr;
                                        //objmain['MED'] = mon_arr;
                                        console.log(objmain);
                                        if (dayarr.length <= 0) {
                                            alert('No Records Fount');
                                            return false;
                                        }
                                        console.log(JSON.stringify(objmain));

                                        //$.ajax({
                                        //    type: "POST",
                                        //    contentType: "application/json; charset=utf-8",
                                        //    url: "Invoice_Entry1.aspx/SaveInvoice",
                                        //    data: "{'General_details':'" + JSON.stringify(mainarr) + "','Pro_details':'" + JSON.stringify(dayarr) + "','Tax_details':'" + JSON.stringify(taxArr) + "','new_order_no':'" + add_order_orderno + "'}",
                                        //    dataType: "json",
                                        //    success: function (data) {
                                        //        var Respomse = confirm('Do You Want To Print Invoice Order');
                                        //        if (Respomse) { window.location.href = "../Stockist/Invoice_Print.aspx?Order_id=" + orderid + "&Stockist_Code=" + stockistcode + "&Div_Code=" + div_code + "&Cust_Code=" + retailer_ID + ""; } else { window.location.href = "../Stockist/Invoice_Order_List.aspx"; }
                                        //    },
                                        //    error: function (data) {
                                        //        approve = 0;
                                        //        alert(JSON.stringify(data));
                                        //    }
                                        //});
                                    }
                                    else {
                                        // var answer = confirm('your largest credit limit is low');
                                        // if (answer) {
                                        dayarr = [];
                                        mainarr = [];
                                        if (pay_term.length <= 0) {
                                            alert('select Payment Term');
                                            return false;
                                        }
                                        if (Sel_Shi_Med.length <= 0) {
                                            alert('select Shipping Method');
                                            return false;
                                        }
                                        var oqty = $(this).find("#english").attr('pv_Val') || 0;
                                        var customercode = $(document).find("#tblCustomers tr td").find("#recipient-name option:selected").val();

                                        od = $('#example-multiple-selected option').html().split('_');
                                        var dss = od[1];
                                        ar = dss.split('/');
                                        da = ar[1] + '/' + ar[0] + '/' + ar[2];
                                        // var custumorname = ("#Retailer_Details option:selected").text();
                                        mainarr.push({
                                            sf_code: stockistcode,
                                            sf_name: stockistname,
                                            dis_code: stockistcode,
                                            dis_name: stockistname,
                                            cus_code: retailer_ID,
                                            cus_name: customername,
                                            order_date: da,
                                            inv_date: $(document).find(".invoice").val(),
                                            del_date: $(document).find(".dd").val(),
                                            pay_term: pay_term,
                                            pay_due: $(document).find(".pd").val(),
                                            ship_mtd: ship_met,
                                            ship_term: 0,
                                            sub_total: sub_tot,
                                            tax_total: Gst_Value,
                                            cgst: tax_CGST,
                                            sgst: tax_SGST,
                                            total: total,
                                            adv_paid: ad_paid,
                                            Amt_due: amt_due,
                                            remark: remark,
                                            Dis_total: Dis_Tot,
                                            order_no: orderid,
                                            Div_Code: div_code
                                        });

                                        var dtls_tab = document.getElementById("tblCustomers");
                                        var nrows1 = dtls_tab.rows.length;
                                        var Ncols = dtls_tab.rows[0].cells.length;
                                        if (nrows1 > 1) {
                                            var ch = true;
                                            var arr = [];
                                            $('#tblCustomers > tbody > tr').each(function () {

                                                var Prd_code = $(this).find('[id*=Hid_Pro_code]').val() || 0;
                                                var un = $(this).find('.unit option:selected').text() || 0;

                                                var res = [];

                                                res = Allrate.filter(function (t) {
                                                    return (t.Product_Detail_Code == Prd_code);
                                                });

                                                if (res[0].product_unit == res[0].Product_Sale_Unit) {
                                                    var qty = $(this).find('#Order_qty').val();
                                                    qty1 = $(this).find('#Order_qty').val();
                                                    var sales_qty = $(this).find('[id*=english]').val();
                                                }
                                                else {

                                                    if (res[0].product_unit == un) {
                                                        var qty = parseFloat($(this).find('#Order_qty').val()) * res[0].Sample_Erp_Code;
                                                        var qty1 = $(this).find('#Order_qty').val() || 0;
                                                        var sales_qty = $(this).find('[id*=english]').val() || 0;
                                                    }
                                                    else {
                                                        var qty = $(this).find('#Order_qty').val() || 0;
                                                        qty1 = $(this).find('#Order_qty').val() || 0;
                                                        var sales_qty = $(this).find('[id*=english]').val() || 0;
                                                    }
                                                }

                                                var sno = $(this).find('[id*=Hid_order_code]').val() || 0;
                                                var Prd_name = $(this).find('.autoc').attr('t');
                                                var price = $(this).find('[id*=Price_]').val() || 0;
                                                var dis = $(this).find('[id*=Dis]').val() || 0;
                                                var dis_val = $(this).find('#hid_dis').val() || 0;
                                                var free = $(this).find('[id*=Free]').val() || 0;
                                                var tot = $(this).find('[id*=total]').val() || 0;
                                                var Ut = $(this).find('.unit option:selected').text();
                                                var offer_unit = $(this).find('.fre').attr('unit') || 0;
                                                var offer_code = $(this).find('.of_pro_code').val() || 0;
                                                var offer_name = $(this).find('.of_pro_name').val() || 0;
                                                var Tax_code = $(this).find('[id*=lbl_tax_value]').text() || 0;
                                                var Tax_name = $(this).find('[id*=id_tax]').text() || 0;

                                                taxArr = [];

                                                taxArr.push({
                                                    Trans_order_no: $(this).find('[id*=Hid_order_code]').val() || 0,
                                                    taxCode: $(this).find('.hd_tax_value').val() || 0,
                                                    tax_Name: $(this).find('.lbl_Tax').text() || 0,
                                                    value: (Number($(this).find('.hd_tax_value').val()) * Number(tot) / 100)

                                                });

                                                console.log(taxArr);
                                                dayarr.push({
                                                    Trans_order_no: sno,
                                                    Product_code: Prd_code,
                                                    Product_name: Prd_name,
                                                    Price: price,
                                                    Discount: dis,
                                                    Dis_v: dis_val,
                                                    Free: free,
                                                    Quantity: qty,
                                                    sale_q: sales_qty,
                                                    Quat: qty1,
                                                    Amount: tot,
                                                    Product_Unit: Ut,
                                                    Offer_Product_Code: offer_code,
                                                    Offer_Product_Name: offer_name,
                                                    Offer_Product_Unit: offer_unit,
                                                    Tye: ty,
                                                    //taxDtls: taxArr
                                                });
                                            });

                                        }

                                        var Add_order = [];

                                        Add_order = dayarr.filter(function (a) {
                                            return (a.Tye == 'Add');
                                        });

                                        if (Add_order.length > 0) {

                                            var orderval = 0;
                                            var tot_net_weight = 0;
                                            var add_new_order_array = [];
                                            for (var t = 0; t < Add_order.length; t++) {

                                                orderval = parseFloat(Add_order[t].Amount) + parseFloat(orderval);
                                                tot_net_weight = parseFloat(Add_order[t].Quat) + parseFloat(tot_net_weight);

                                                add_new_order_array.push({

                                                    PCd: Add_order[t].Product_code,
                                                    PName: Add_order[t].Product_name,
                                                    Unit: Add_order[t].Product_Unit,
                                                    Rate: Add_order[t].Price,
                                                    Qty: Add_order[t].Quat,
                                                    Sub_Total: Add_order[t].Amount,
                                                    Free: Add_order[t].Free,
                                                    Dis_value: Add_order[t].Dis_v,
                                                    Discount: Add_order[t].Discount,
                                                    of_Pro_Code: Add_order[t].Offer_Product_Code,
                                                    of_Pro_Name: Add_order[t].Offer_Product_Name,
                                                    of_Pro_Unit: Add_order[t].Offer_Product_Unit

                                                });
                                            }

                                            //$.ajax({
                                            //    type: "POST",
                                            //    contentType: "application/json; charset=utf-8",
                                            //    async: false,
                                            //    url: "myOrders.aspx/saveorders",
                                            //    data: "{'NewOrd':'" + JSON.stringify(add_new_order_array) + "','Remark':'','Ordrval':'" + Add_orderval + "','RetCode':'" + Customer_Code + "','RecDate':'" + today + "','Ntwt':'" + Add_tot_net_weight + "','retnm':'" + Customer_Name + "','Type':'1','ref_order':''}",
                                            //    dataType: "json",
                                            //    success: function (data) {
                                            //        add_order_orderno = data.d;
                                            //        if (add_order_orderno.length > 0) {
                                            //            // var Add_Order_ID = data.d;
                                            //            orderid = orderid + ',' + add_order_orderno;
                                            //            mainarr[0]["order_no"] = orderid;
                                            //        }
                                            //        else {
                                            //            alert('Order Not Saved')
                                            //            return false;
                                            //        }

                                            //    },
                                            //    error: function (result) {
                                            //        approve = 0;
                                            //        alert(JSON.stringify(result));
                                            //    }
                                            //});
                                        }


                                        var Pen_order_ids = [];
                                        var Pen_orderval = 0;
                                        var Pen_tot_net_weight = 0;
                                        var pen_new_order_array = [];
                                        var pending_qty = '';

                                        Pen_order_ids = dayarr.filter(function (a) {
                                            return (parseFloat(a.sale_q) < parseFloat(a.Quat));
                                        });

                                        var pen_id = '';
                                        for (var c = 0; Pen_order_ids.length > c; c++) {

                                            if (pen_id.indexOf(Pen_order_ids[c].Trans_order_no) < 0) {

                                                pen_id += ',' + Pen_order_ids[c].Trans_order_no;
                                                //pending_qty =Pen_order_ids[c].
                                            }


                                            Pen_orderval = parseFloat(Add_order[t].Amount) + parseFloat(Pen_orderval).toString();
                                            Pen_tot_net_weight = parseFloat(Add_order[t].Quat) + parseFloat(tot_net_weight).toString();

                                            pen_new_order_array.push({

                                                PCd: Add_order[t].Product_code,
                                                PName: Add_order[t].Product_name,
                                                Unit: Add_order[t].Product_Unit,
                                                Rate: Add_order[t].Price,
                                                Qty: Add_order[t].sale_q,
                                                Sub_Total: Pen_order_ids[c].Amount,
                                                Free: Add_order[t].Free,
                                                Dis_value: Add_order[t].Dis_v,
                                                Discount: Add_order[t].Discount,
                                                of_Pro_Code: Add_order[t].Offer_Product_Code,
                                                of_Pro_Name: Add_order[t].Offer_Product_Name,
                                                of_Pro_Unit: Add_order[t].Offer_Product_Unit
                                            });
                                        }

                                        if (Pen_order_ids.length > 0) {

                                            //$.ajax({
                                            //    type: "POST",
                                            //    contentType: "application/json; charset=utf-8",
                                            //    async: false,
                                            //    url: "myOrders.aspx/saveorders",
                                            //    data: "{'NewOrd':'" + JSON.stringify(pen_new_order_array) + "','Remark':'','Ordrval':'" + Pen_orderval + "','RetCode':'" + Customer_Code + "','RecDate':'" + today + "','Ntwt':'" + Pen_tot_net_weight + "','retnm':'" + Customer_Name + "','Type':'2','ref_order':'" + pen_id + "'}",
                                            //    dataType: "json",
                                            //    success: function (data) {
                                            //        var Add_Order_ID = data.d;
                                            //        orderid = orderid + ',' + Add_Order_ID;
                                            //        mainarr[0]["order_no"] = orderid;
                                            //    },
                                            //    error: function (result) {
                                            //        approve = 0;
                                            //        alert(JSON.stringify(result));
                                            //    }
                                            //});

                                        }


                                        var objmain = {};
                                        objmain['MTED'] = mainarr;
                                        objmain['TED'] = dayarr;
                                        // objmain['DED'] = dayarr;
                                        //objmain['MED'] = mon_arr;
                                        console.log(objmain);
                                        if (dayarr.length <= 0) {
                                            alert('No Records Fount');
                                            return false;
                                        }
                                        console.log(JSON.stringify(objmain));

                                        //$.ajax({
                                        //    type: "post",
                                        //    contentType: "application/json; charset=utf-8",
                                        //    url: "invoice_entry1.aspx/SaveInvoice",
                                        //    data: '{"General_details":"' + json.stringify(mainarr) + '","Pro_details":"' + json.stringify(dayarr) + '","Tax_details":"' + json.stringify(taxArr) + '"}',
                                        //    datatype: "json",
                                        //    success: function (data) {
                                        //        var respomse = confirm('do you want to print invoice order');
                                        //        if (respomse) { window.location.href = "../stockist/invoice_print.aspx?order_id=" + orderid + "&stockist_code=" + stockistcode + "&div_code=" + div_code + "&cust_code=" + retailer_id + ""; } else { window.location.href = "../stockist/invoice_order_list.aspx"; }
                                        //    },
                                        //    error: function (data) {
                                        //        approve = 0;
                                        //        alert(json.stringify(data));
                                        //    }
                                        //});


                                        //  }
                                        //  else {
                                        //task for no

                                        //  }
                                    }
                                }
                                else {
                                    confirm('your largest credit limit is low');
                                }
                            },
                            error: function (data) {
                                alert(JSON.stringify(data));
                            }

                        });
                        //  }
                    }
                });


                var StkDetail = [];
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Invoice_Entry1.aspx/GetCurrStock",
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
                    url: "Invoice_Entry1.aspx/getscheme",
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

                    // CQ = (CQvalue == "") ? 0 : CQvalue;
                    //var disrate = $(this).closest("tr").find('.tdRate').text();
                    //result = (CQ * disrate);

                    pCode = row.find('#pro_sale_code').val();

                    pname = row.find(".autoc").text();
                    ff = row.find(".fre1").text();

                    getscheme(pCode, CQ, CQvalue, un, row, ff, opcode, idx, pname);
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

                    // $(tr).find('#Free').val(res.Free);
                    var d = $(tr).find('#Price_').val() * $(tr).find('#english').val();
                    var discalc = parseInt(res.Discount) / 100;
                    var distotal = d * discalc;
                    var finaltotal = d - distotal;

                    $(tr).find('#hid_dis').text(distotal.toFixed(2));
                    //    if (distotal != '0') {
                    //        $(tr).find('.tdtotal').text(finaltotal.toFixed(2));
                    //        $(tr).find('.disc_value').val(distotal.toFixed(2));
                    //    }

                    //    if (finaltotal != "0") {
                    //        var after_cal = $(tr).find('#tax_val').text() / 100 * $(tr).find('.tdtotal').text()
                    //        var fin = after_cal + parseFloat($(tr).find('.tdtotal').text());
                    //        $(tr).find('.tdtax').text(after_cal.toFixed(2));
                    //        $(tr).find('.tdAmt').text(fin.toFixed(2));
                    //    }
                }

            });

            //stock_check
            $("#tblCustomers >tbody >tr").each(function () {
                var row = $(this).closest("tr");
                var val = row.find('input[name=stkval]').attr('stkgood');
                var qty = row.find('[id*=english]').val();
                console.log(val, qty);
                if (Number(val) >= qty) {
                    //$(this).closest('tr').find('input[name=stkval]').attr('stkgood')
                    $("#btndaysave").show();
                    //$("#btnprint").show();

                    $("#stk_chk").hide();
                    //row.find('[id*=txtVal]').val(0);
                }
                else {
                    $("#btndaysave").show();
                    $("#btnprint").show();
                    $("#stk_chk").hide();
                    //alert('Qty Value High on Stock Value')
                    //row.find('[id*=english]').val(0);
                }
            });
        </script>
    </head>
    <body>
        <form id="form1" runat="server">
            <div class="container">
            </div>
            <div class="container-fluid">
                <div class="container" id="itemlist2">
                    <div class="row" style="margin-top: -12px;">
                        <div class="row">
                            <div class="col-md-3 col-lg-4">
                                <div class="form-group">
                                    <label class="control-label " for="focusedInput">
                                        Customer Name</label>
                                    <span style="color: Red">*</span>
                                    <select class="form-control" id="recipient-name" style="width: 100%;">
                                        <option value="0">Select Customer Name</option>
                                    </select>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label class="control-label" for="focusedInput">
                                        Payment Mode</label>
                                    <span style="color: Red">*</span>
                                    <select class="form-control" id="Sel_Pay_Term" name="Sel_Pay_Term" data-validation="required">
                                    </select>
                                </div>

                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label class="control-group " for="focusedInput">
                                        Order ID</label>
                                    <span style="color: Red">*</span>
                                    </br>
                                <select class="form-control poiinter" id="example-multiple-selected" multiple>
                                    <%--          <option value="0">---Select All--</option>--%>
                                </select>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-lg-2">
                            <div class="form-group">
                                <label class="control-label" for="focusedInput">
                                    Payment Due</label>
                                <span style="color: Red">*</span>
                                <input class="form-control pd" id="datepicker" name="datepicker" data-validation="required"
                                    type="text" autocomplete="off" />
                            </div>
                        </div>
                        <div class="col-md-3 col-lg-2">
                            <div class="form-group">
                                <label class="control-label" for="focusedInput">
                                    Shipping Mode</label>
                                <span style="color: Red">*</span>
                                <select class="form-control" id="Sel_Shi_Med" name="Shi_Med" data-validation="required">
                                </select>
                            </div>
                        </div>
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

                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label" for="focusedInput">
                                    Delivery Date</label>
                                <span style="color: Red">*</span>
                                <input class="form-control dd" id="datepicker1" name="datepicker1" data-validation="required"
                                    type="text" autocomplete="off" />

                                <span id="Span2"></span>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label" for="focusedInput">
                                    Invoice Date</label>
                                <input name="Txt_Orderdate" type="text" id="Txt_in_date" runat="server" class="form-control invoice"
                                    data-validation="required" readonly="" value="" />
                                <span id="Span1"></span>
                                <input type="hidden" id="Hid_orderno" runat="server" name="Hid_orderno" />
                            </div>
                        </div>
                    </div>

                    <div class="row" style="margin-top: 0px;">
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <h5>Add Items:</h5>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <table id="tblCustomers" class="table table-bordered table-hover">
                                <thead class="ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix">
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
                                        <th width="3%">Discount(%)
                                        </th>
                                        <th width="5%">Free
                                        </th>
                                        <th width="7%">Amount
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                            <button id="Del" type="button" class='btn btn-danger delete'>
                                - Delete</button>
                            <button id="Add" type="button" class='btn btn-success addmore'>
                                + Add More</button>
                        </div>
                    </div>

                    <div class="row" style="margin-top: 0px;">

                        <div class="col-sm-8" style="padding: 0px 0px 0px 0px;">
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
                            <div class="col-sm-6">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <h3>Notes:
                                        </h3>
                                        <div class="form-group">
                                            <textarea placeholder="Your Notes" id="notes" name="notes" rows="5" class="form-control txt"></textarea>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>



                        <%--          <div class="col-sm-4">
                            <div class="row">
                                <div class="col-sm-12">
                                    <h3>Notes:
                                    </h3>
                                    <div class="form-group">
                                        <textarea placeholder="Your Notes" id="notes" name="notes" rows="5" class="form-control txt"></textarea>
                                    </div>
                                </div>
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


                        <div class="col-sm-offset-8 form-horizontal" style="margin-top: 40px;">
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

                        <div class="col-sm-offset-8 form-horizontal" style="margin-top: 80px;">
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

                        <div class="col-sm-offset-8 form-horizontal" style="margin-top: 120px;">
                            <label class=" col-xs-3 control-label">
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


                        <div class="col-sm-offset-8 form-horizontal" style="margin-top: 160px;">
                            <label class=" col-xs-3 control-label">
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


                        <div class="col-sm-offset-8 form-horizontal" style="margin-top: 200px;">
                            <label class=" col-xs-3 control-label">
                                Total :
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

                        <div class="col-sm-offset-8 form-horizontal" style="margin-top: 240px;">
                            <label class=" col-xs-3 control-label">
                                Advanced Paid: 
                            </label>
                            <div class="col-sm-9">
                                <div class="input-group">
                                    <div class="input-group-addon currency">
                                        <i class="fa fa-inr"></i>
                                    </div>
                                    <input data-cell="K1" id="Ad_Paid" autocomplete="off" data-format="0,0.00" class="form-control validate" data-validation="required"
                                        name="amountpaid" />
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-offset-8 form-horizontal" style="margin-top: 287px;">
                            <label class=" col-xs-3 control-label">
                                Amount Due: 
                            </label>
                            <div class="col-sm-9">
                                <div class="input-group">
                                    <div class="input-group-addon currency">
                                        <i class="fa fa-inr"></i>
                                    </div>
                                    <input data-cell="V1" id="Amt_Tot" data-format="0,0.00" class="form-control" readonly
                                        name="amountdue" />
                                </div>
                            </div>
                        </div>

                    </div>

                    <div class="row" style="text-align: center">
                        <div class="fixed">
                            <%-- <a id="stk_chk" style="font-size: medium">Qty Value High on Stock Value</a>--%> <a id="btndaysave"
                                class="btn btn-primary btnsave" style="vertical-align: middle; font-size: 17px;">
                                <span>Save Invoice</span></a>&nbsp&nbsp<%--<a id="btnprint" class="btn btn-success pleasewait"
                                style="vertical-align: middle; font-size: 17px;"> <span>Print Invoice</span></a>--%>
                        </div>

                    </div>
                </div>

                <script type="text/javascript">
                    //  $form = $('#itemlist2').calx();
                    var i = 2;

                    //$(document).ready(function () {
                    //    $form.calx('update');

                    //    var mode = GetParameterValues('mode');

                    //    if (mode == "automatic") {
                    //        $('#InvoiceNumber').attr('readonly', true);
                    //    }
                    //    else if (mode == "manual") {
                    //        $('#InvoiceNumber').attr('readonly', false);
                    //    }

                    //});

                    function GetParameterValues(param) {
                        var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
                        for (var i = 0; i < url.length; i++) {
                            var urlparam = url[i].split('=');
                            if (urlparam[0] == param) {
                                return urlparam[1];
                            }
                        }
                    }



                </script>

                </script>

                <script type="text/javascript" src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
                <%--  <script type="text/javascript" src="//cdnjs.cloudflare.com/ajax/libs/jquery-form-validator/2.2.1/jquery.form-validator.min.js"></script>--%>

                <%-- <script type="text/javascript">
                    $(function () { 
                        $("#datepicker").datepicker({ dateFormat: 'dd/mm/yy' });
                        $("#datepicker1").datepicker({ dateFormat: 'dd/mm/yy' });
                    });
                </script>--%>
                <%-- <script type="text/javascript">                $.validate(); </script>--%>
                <script type="text/javascript">
                    $(document).ready(function () {
                        $("#Button1").click(function () {
                            var client = $("#ddlClients").val()

                            if (client == "<-- Select -->") {
                                alert("Please select a Client");
                                return false;
                            }


                            var PaymentTerms = $("#ddlPaymentTerms").val()
                            if (PaymentTerms == "<-- Select -->") {
                                alert("Please select Payment Terms");
                                return false;
                            }

                            var PONumber = $("#ddlPONumber").val()
                            if (PONumber == "<-- Select -->") {
                                alert("Please select PONumber");
                                return false;
                            }

                        });
                    });
                </script>
                <script type="text/jscript">
                    $(document).ready(function () {
                        $('#InvoiceNumber').change(function () {
                            var uname = $(this);
                            var msgbox1 = $('#datepicker');
                            var msgbox = $('#status');
                            if (uname.val().length > 3) {
                                $.ajax({
                                    type: "POST",
                                    url: '/Service.asmx/GetData',
                                    data: "{'username':'" + uname.val() + ", " + msgbox1.val() + "'}",
                                    contentType: "application/json;charset=utf-8",
                                    datatype: "json",
                                    success: function (data) {
                                        if (data.d == 'Available') {
                                            msgbox.html('<font color="green">Available</font>');
                                        }
                                        else {
                                            msgbox.html(data.d)
                                            uname.val('');
                                        }
                                    }
                                })
                            }
                            else {
                                msgbox.html('<font color="#cc0000">user name must be greater than 3 characters</font>')
                            }
                        });
                    });

                </script>
        </form>
    </body>
    </html>
</asp:Content>
