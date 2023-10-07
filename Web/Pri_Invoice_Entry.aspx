<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="Pri_Invoice_Entry.aspx.cs" Inherits="Pri_Invoice_Entry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!doctype html>
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" />
        <link href="css/bootstrap.css" rel="stylesheet" type="text/css" />
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/2.0.2/jquery.min.js"></script>
        <script src="Scripts/bootbox.min.js" type="text/javascript"></script>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script type="text/javascript" src="https://code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
        <script type="text/javascript">
            jQuery(document).ready(function ($) {
                var arr = [];
                $("#tblCustomers >tbody >tr").each(function () {
                    $(this).children('td').eq(8).append('<table name="addtable" style="width:100%"><tbody><tr class="sel_row"><td><select id="addetype" name="addetype" class="form-control selTax" style="width:120px;"/></td><td style="width:80px;" ></td><td style="width:40px;"><a name="btnadd" class="btn btn-primary"><span>+</span></a></td></tr></tbody><tfoot><tr><td></td><td class="css_sum"></td></tr></tfoot></table>');
                    $(this).children('td').eq(9).append('<table name="addtable1" style="width:100%"><tbody><tr class="sel_row1"><td><input id="taxamt" name="taxamt[]" data-cell="L" data-format="0,0.00" class="form-control" readonly /></td></tr></tbody></table>');

                    loadddl(arr);

                    var price = $(this).find('[id*=Price_]').val();
                    var qty = $(this).find('[id*=english]').val();
                    var dis = $(this).find('[id*=Dis]').val();
                    $(this).find('[id*=total]').val((price * qty) - ((price * qty) * (dis / 100)));
                    // alert($('#total').val);
                    var grandTotal = 0;

                    $("[id*=total]").each(function () {
                        grandTotal = grandTotal + parseFloat($(this).val());
                    });
                    $("[id*=sub_tot]").val(grandTotal.toFixed(2));
                    $("[id*=in_Tot]").val(grandTotal.toFixed(2));
                    $("[id*=Amt_Tot]").val(grandTotal.toFixed(2));

                    var dis_Tot = 0;
                    $("[id*=Dis]").each(function () {

                        dis_Tot = dis_Tot + parseFloat(dis);

                    });
                    $("[id*=Txt_Dis_tot]").val(dis_Tot.toFixed(2));

                });



                $(document).on('click', 'a[name=btnadd]', function () {
                    if ($(this).text().toString() == "+") {


                        if ($(this).closest('tr').find('.selTax').children('option').length - 1 > $(this).closest('table >tbody').find('tr').length) {
                            $(this).text("-");
                            $(this).closest('tr').after('<tr class="sel_row"><td style="width:100px;"><select id="addetype" name="addetype" class="form-control selTax" style="width:120px;"/></td><td style="width:80px;" ></td><td style="width:40px;"><a name="btnadd" class="btn btn-primary"><span>+</span></a></td></tr>');
                            console.log($(this).closest('table').closest('tr').children('td').eq(9));
                            $(this).closest('table').closest('tr').children('td').eq(9).find('tr:last').after('<tr class="sel_row1"><td><input id="taxamt" name="taxamt[]" data-cell="L" data-format="0,0.00" class="form-control" readonly /></td></tr>');
                            //$(this).find('td:nth-child(10)').closest('tr').after('<input id="taxamt" name="taxamt[]" data-cell="L" data-format="0,0.00" class="form-control" readonly />')
                            //$(this).closest('#tblCustomers >tbody >tr >td').after('<input id="taxamt" name="taxamt[]" data-cell="L" data-format="0,0.00" class="form-control" readonly />');
                            $('[id*=taxamt]').append("<div><br><input type='text'/><br></div>");
                            loadddl(arr, $(this).closest('tbody'))
                            var l = $(this).parent('#tblCustomers >tbody >tr').index();
                            i = $(this).find('td:nth-child(10)').closest('#tblCustomers >tbody ').closest('#tblCustomers >tbody >tr >td').index();
                            tds = $(this).closest('#tblCustomers >tbody >tr').closest('#tblCustomers >tbody >tr >td').closest('#tblCustomers >tbody >tr');
                            //  console.log(l);
                            // console.log(tds);
                            $(tds[i + 1]).after('<input id="taxamt" name="taxamt[]" data-cell="L" data-format="0,0.00" class="form-control" readonly />');
                        }
                    }

                    else {
                        //  x = $(this).closest('tr').find('input[name=addeamt]'); $(x).val('0');
                        var iDX = $(this).closest('tr').index();
                        //alert(iDX);
                        console.log($(this).closest('table').closest('tr').children('td').eq(9).find('tr'));
                        $(this).closest('table').closest('tr').children('td').eq(9).find('tr').eq(iDX).remove();
                        $(this).closest('tr').remove();
                        $('[id*=taxamt]').children().last().remove(); 0
                    }
                });



                $(document).on('keyup', '[id*=english]', function () {
                    //                    var row = $(this).closest("tr");
                    //                    var val = row.find('input[name=stkval]').attr('stkgood');
                    //                    var qty = row.find('[id*=english]').val();
                    //                    console.log(val, qty);
                    //                    if (Number(val) >= qty) {
                    //                        //$(this).closest('tr').find('input[name=stkval]').attr('stkgood')



                    //                        //row.find('[id*=txtVal]').val(0);
                    //                    }
                    //                    else {
                    //                        alert('Qty Value High on Stock Value')
                    //                        row.find('[id*=english]').val(0);
                    //                   }

                    if (!jQuery.trim($(this).val()) == '') {
                        if (!isNaN(parseFloat($(this).val()))) {
                            var row = $(this).closest("tr");
                            var dis = row.find('[id*=Dis]').val();
                            //$('#Price_').val() * $('#english').val()
                            row.find('[id*=total]').val(row.find('[id*=Price_]').val() * row.find('[id*=english]').val() - (parseInt(dis)));
                            //                           alert(row.find('[id*=addetype]').val());
                            row.find('[id*=taxamt]').val(row.find('[id*=addetype]').val() / 100 * row.find('[id*=total]').val());
                            // alert($('#total').val);
                        }
                    } else {
                        $(this).val('');
                    }
                    var grandTotal = 0;

                    $("[id*=total]").each(function () {
                        grandTotal = (grandTotal + parseFloat($(this).val()));
                    });
                    $("[id*=sub_tot]").val(grandTotal.toString());
                    $("[id*=in_Tot]").val(grandTotal.toString());
                    $("[id*=Amt_Tot]").val(grandTotal.toString());
                });



                $(document).on('keyup', '[id*=Price_]', function () {

                    if (!jQuery.trim($(this).val()) == '') {
                        if (!isNaN(parseFloat($(this).val()))) {
                            var row = $(this).closest("tr");
                            var dis = row.find('[id*=Dis]').val();
                            //$('#Price_').val() * $('#english').val()
                            row.find('[id*=total]').val(row.find('[id*=Price_]').val() * row.find('[id*=english]').val() - (parseInt(dis)));
                            row.find('[id*=taxamt]').val(row.find('[id*=addetype]').val() / 100 * row.find('[id*=total]').val());

                            // alert($('#total').val);
                        }
                    } else {
                        $(this).val('');
                    }
                    var grandTotal = 0;
                    $("[id*=total]").each(function () {
                        grandTotal = (grandTotal + parseFloat($(this).val()));
                    });
                    $("[id*=sub_tot]").val(grandTotal.toFixed(2));
                    $("[id*=in_Tot]").val(grandTotal.toFixed(2));
                    $("[id*=Amt_Tot]").val(grandTotal.toFixed(2));
                });


                //dis_count
                $(document).on('keyup', '[id*=Dis]', function () {

                    if (!jQuery.trim($(this).val()) == '') {
                        if (!isNaN(parseFloat($(this).val()))) {
                            var row = $(this).closest("tr");

                            var amd = row.find('[id*=Price_]').val();
                            var dis = row.find('[id*=Dis]').val();
                            if (dis != '' && amd != '') {

                                Val = (row.find('[id*=Price_]').val() * row.find('[id*=english]').val()) * (dis / 100);
                                row.find('[id*=total]').val(((row.find('[id*=Price_]').val() * row.find('[id*=english]').val()) - Val).toFixed(2));

                            }

                        }
                    } else {
                        $(this).val('');
                    }

                    var grandTotal = 0;
                    $("[id*=total]").each(function () {
                        grandTotal = (grandTotal + parseFloat($(this).val()));
                    });
                    $("[id*=sub_tot]").val(grandTotal.toFixed(2));
                    $("[id*=in_Tot]").val(grandTotal.toFixed(2));
                    $("[id*=Amt_Tot]").val(grandTotal.toFixed(2));
                    var dis_Tot = 0;
                    $("[id*=Dis]").each(function () {
                        dis_Tot = Number(dis_Tot) + Number(parseFloat($(this).find('[id*=Dis]').val() || 0));
                        $("[id*=Txt_Dis_tot]").val(dis_Tot.toFixed(2));
                    });

                });


                //                $(document).on('click', '.pleasewait', function () {

                //                    window.location = "../E-Report_DotNet/Invoice_Print.aspx";

                //                });




                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Pri_Invoice_Entry.aspx/GetAllType",
                    dataType: "json",
                    success: function (data) {

                        if (data.d.length > 0) {


                            for (var i = 0; i < data.d.length; i++) {


                                var str = "";
                                var str_foot = "";
                                // if (data.d[i].Exp_code > 0) {

                                $("select[name=addetype]").append($("<option></option>").val(data.d[i].Exp_code).html(data.d[i].Exp_name));
                                arr.push({ Exp_code: data.d[i].Exp_code, Exp_name: data.d[i].Exp_name });


                                // }




                            }
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
                    url: "Pri_Invoice_Entry.aspx/GetTransType",
                    dataType: "json",
                    success: function (data) {


                        if (data.d.length > 0) {
                            var ddlCustomers = $("#Sel_Shi_Med");
                            ddlCustomers.empty().append('<option selected="selected" value="0">--- Select ---</option>');

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
                    url: "Pri_Invoice_Entry.aspx/GetPayType",
                    dataType: "json",
                    success: function (data) {


                        if (data.d.length > 0) {
                            var ddlCustomers = $("#Sel_Pay_Term");
                            ddlCustomers.empty().append('<option selected="selected" value="0">--- Select ---</option>');

                            $.each(data.d, function () {
                                ddlCustomers.append($("<option></option>").val(this['Pay_code']).html(this['Pay_name']));
                            });

                        }

                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }

                });

                function loadddl(arr, tr) {

                    var ddlCustomers;
                    if (!tr) {
                        ddlCustomers = $("select[name=addetype]");
                        //console.log(tr);
                    } else {
                        ddlCustomers = $(tr).find("select[name=addetype]:last");
                    }
                    $(arr).each(function () {
                        var option = $("<option />");
                        //Set Customer Name in Text part.
                        option.html(this.Exp_name);
                        //Set Customer CustomerId in Value pat.
                        option.val(this.Exp_code);
                        //Add the Option element to DropDownList.
                        ddlCustomers.append(option);
                    });

                }





                $('[id*=Add]').on('click', function () {


                    count = $("#tblCustomers >tbody >tr").length + 1;

                    var data = "<tr><td><input type='checkbox' class='case'/></td><td><span id='snum'>" + count + ".</span></td>";
                    data += "<td><input class='form-control autoc' data-validation='required' type='text' id='Item_1' name='item_name[]'/></td> " +
                    "<td><input class='form-control' data-validation='required' type='text' id='Price_' name='price[]'  data-cell='C" + i + "' data-format='0.00' /></td> " +
                    "<td><input class='form-control' data-validation='required' type='text' id='Dis' name='dis[]'  data-cell='Y" + i + "' data-format='0' /></td> " +
                    "<td><input class='form-control' data-validation='required' type='text' id='Free' name='free[]'  data-cell='I" + i + "' data-format='0' /></td> " +
                    "<td><input class='form-control' data-validation='required' type='text' id='english' name='quantity[]'  data-cell='D" + i + "' data-format='0' /></td> " +
                    "<td><input class='form-control' id='total' name='total[]' type='text' data-format='0.00' data-cell='E" + i + "'   readonly/></td> " +
                    //"<td><input class='form-control' type='text' id='discount' name='discount[]' data-cell='X" + i + "' data-format='0.00' /></td>" +
                    "<td></td> " +
                    "<td></td></tr> ";

                    //$('#tblCustomers tr:last').find('td').eq(9).append('<table name="addtable1" style="width:100%"><tbody><tr><td><input id="taxamt" name="taxamt[]" data-cell="L" data-format="0,0.00" class="form-control" readonly /></td></tr></tbody></table>');
                    $('#tblCustomers').append(data);
                    $('#tblCustomers tr:last').find('td').eq(8).append('<table name="addtable" style="width:100%"><tbody><tr class="sel_row"><td><select id="addetype"  name="addetype" class="form-control selTax" style="width:120px;"/></td><td style="width:80px;" ></td><td style="width:40px;"><a name="btnadd" class="btn btn-primary"><span>+</span></a></td></tr></tbody></table>');
                    loadddl(arr, $("#tblCustomers  >tbody >tr:last").find('td:nth-child(9)').find('table[name=addtable]').closest('tbody'));
                    console.log($('#tblCustomers >tbody >tr:last').closest('tr').children('td').eq(9));
                    $('#tblCustomers >tbody >tr:last').closest('tr').children('td').eq(9).append('<table name="addtable1" style="width:100%"><tbody><tr class="sel_row1"><td><input id="taxamt" name="taxamt[]" data-cell="L" data-format="0,0.00" class="form-control" readonly /></td></tr></tbody></table>');
                    //console.log($(this).closest('#tblCustomers tr:last').find('td').eq(9));
                    i++;
                    filldata(dts);

                });
                $(".delete").on('click', function () {
                    $('.case:checkbox:checked').parents("tr").remove();
                    $('.check_all').prop("checked", false);
                    check();
                    var grandTotal = 0;
                    $("[id*=total]").each(function () {

                        grandTotal = Math.abs(grandTotal + parseFloat($(this).val()));

                    });
                    $("[id*=sub_tot]").val(grandTotal.toString());
                    $("[id*=in_Tot]").val(grandTotal.toString());
                    $("[id*=Amt_Tot]").val(grandTotal.toString());
                    var TaxTotal = 0;
                    $("[id*=taxamt]").each(function () {

                        TaxTotal = TaxTotal + parseFloat($(this).val());

                    });
                    $("[id*=Tax_tot]").val(TaxTotal.toString());

                });





                function select_all() {
                    $('input[class=case]:checkbox').each(function () {
                        if ($('input[class=check_all]:checkbox:checked').length == 0) {
                            $(this).prop("checked", false);
                        } else {
                            $(this).prop("checked", true);
                        }
                    });
                }

                function check() {
                    var k = 1;

                    $("#tblCustomers >tbody >tr").each(function () {
                        console.log(k);
                        $(this).children('td').eq(1).html(k++);

                    });
                }

                $("#tblCustomers >tbody >tr").each(function () {
                    var tot = $(this).find('[id*=total]').val();

                    var tax = $(this).find('[id*=addetype] option:selected').val();

                    $(this).find('[id*=taxamt]').val(tax / 100 * tot);

                    var TaxTotal = 0;


                    $("[id*=taxamt]").each(function () {

                        TaxTotal = TaxTotal + parseFloat($(this).val());

                    });
                    $("[id*=Tax_tot]").val(TaxTotal.toString());


                });


                $(document).on('keyup', '[id*=Ad_Paid]', function () {

                    if (!jQuery.trim($(this).val()) == '') {
                        if (!isNaN(parseFloat($(this).val()))) {

                        }
                    } else {
                        $(this).val('');
                    }
                    var adTotal = 0;
                    $("[id*=Ad_Paid]").each(function () {


                        adTotal = (parseFloat($("[id*=in_Tot]").val()) - parseFloat($(this).val()));
                    });

                    $("[id*=Amt_Tot]").val(adTotal.toString());
                });
                //g
                $(document).on('change', '[id*=addetype]', function () {
                    if (!jQuery.trim($(this).val()) == '') {
                        if (!isNaN(parseFloat($(this).val()))) {
                            var row = $(this).closest('#tblCustomers >tbody >tr');


                            var iDX = $(this).closest('tr').index();
                            var x = $(this).closest('tr');
                            //alert(iDX);
                            //tax
                            var tot = Math.abs($(this).val() / 100 * row.find('[id*=total]').val()).toFixed(2);
                            console.log(tot);
                            $(this).closest('table').closest('tr').children('td').eq(9).find('tr').eq(iDX).find('[id*=taxamt]').val(tot);
                            var amd = row.find('[id*=Price_]').val();
                            var dis = row.find('[id*=Dis]').val();
                            // if (dis != '' && amd != '')
                            //   row.find('[id*=total]').val((row.find('[id*=Price_]').val() * row.find('[id*=english]').val()) - (parseInt(dis)));

                            //row.find('[id*=taxamt]').val(row.find('[id*=addetype]').val() / 100 * row.find('[id*=total]').val());
                            var opt = $(this).find('option:selected').text();
                            var tottax = 0;

                            $(this).closest('table').find('.sel_row').each(function () {

                                if (iDX != $(this).index()) {
                                    console.log(iDX + ":" + $(this).index());
                                    if (opt == $(this).find('.selTax').find('option:selected').text()) {
                                        $(x).find('.selTax').val(0);
                                        alert('Already Select ' + opt + '');
                                        $(this).closest('table').closest('tr').children('td').eq(9).find('tr').eq(iDX).find('[id*=taxamt]').val(0);
                                    }
                                }

                            });

                        }
                    } else {
                        $(this).val('');
                    }


                    var TaxTotal = 0;
                    $("[id*=taxamt]").each(function () {

                        TaxTotal = TaxTotal + parseFloat($(this).val());

                    });
                    $("[id*=Tax_tot]").val(TaxTotal.toString());

                    var grandTotal = 0;
                    $("[id*=total]").each(function () {
                        grandTotal = (grandTotal + parseFloat($(this).val()));
                    });
                    $("[id*=sub_tot]").val(grandTotal.toString());
                    $("[id*=in_Tot]").val(parseFloat(grandTotal.toString()) + parseFloat(TaxTotal.toString()));
                    $("[id*=Amt_Tot]").val(parseFloat(grandTotal.toString()) + parseFloat(TaxTotal.toString()));

                });
                function isValidDate(s) {
                    var bits = s.split('/');
                    var d = new Date(bits[2] + '/' + bits[1] + '/' + bits[0]);
                    return !!(d && (d.getMonth() + 1) == bits[1] && d.getDate() == Number(bits[0]));
                }
                //val
                $(document).on('change', '#datepicker', function () {
                    console.log($(this).val());
                    if ($(this).val().length > 0) {
                        if (isValidDate($(this).val())) {
                            var fit_start_time = $(this).val();
                            var FromDate = fit_start_time.split("/").reverse().join("-");
                            var dts = new Date(FromDate);
                            var cr = dts.getDate() + '/' + (dts.getMonth() + 1) + '/' + dts.getFullYear();
                            //$('#datepicker').datepicker("destroy");
                            //$('#datepicker').datepicker({ dateFormat: 'dd/mm/yy', minDate: cr, defaultDate: cr });
                            //$('#datepicker').val('');
                        }
                        else {
                            alert('Invalid Date Enter or Select Correct Date..!');
                            $(this).val('');
                            $(this).focus();

                        }
                    }
                });

                $(document).on('change', '#datepicker1', function () {
                    console.log($(this).val());
                    if ($(this).val().length > 0) {
                        if (isValidDate($(this).val())) {
                            var fit_start_time = $(this).val();
                            var FromDate = fit_start_time.split("/").reverse().join("-");
                            var dts = new Date(FromDate);
                            var cr = dts.getDate() + '/' + (dts.getMonth() + 1) + '/' + dts.getFullYear();
                            //$('#datepicker').datepicker("destroy");
                            //$('#datepicker').datepicker({ dateFormat: 'dd/mm/yy', minDate: cr, defaultDate: cr });
                            //$('#datepicker').val('');
                        }
                        else {
                            alert('Invalid Date Enter or Select Correct Date..!');
                            $(this).val('');
                            $(this).focus();

                        }
                    }
                });

                $(document).ready(function () {
                    $(document).on('click', '.pleasewait', function () {

                        window.location = "../Pri_Invoice_Print.aspx?Or_Date=" + encodeURIComponent($('#<%=InvoiceNumber.ClientID%>').val()) + "&Dis_code=" + encodeURIComponent($('#<%=Hid_Dis_Name.ClientID%>').val()) + "&Cus_code=" + encodeURIComponent($('#<%=Hid_orderno.ClientID%>').val());

                    });
                });

                $(document).on('click', '.btnsave', function () {
                    var approve = 0;

                    var sf_code = $('#<%=Hid_Sf_Name.ClientID%>').val();
                    var sf_Name = $('#<%=Txt_fldForce.ClientID%>').val();
                    if (sf_Name.length <= 0) { alert('Enter Field Force Name.!!'); $('#Txt_fldForce').focus(); return false; }
                    var dis_code = $('#<%=Hid_Dis_Name.ClientID%>').val();
                    var dis_Name = $('#<%=Txt_dist.ClientID%>').val();
                    if (dis_Name.length <= 0) { alert('Enter Distributor Name.!!'); $('#Txt_dist').focus(); return false; }
                    var Cus_code = $('#<%=Hid_Cus_Name.ClientID%>').val();
                    var Cus_Name = $('#<%=Txt_Retailer.ClientID%>').val();
                    if (Cus_Name.length <= 0) { alert('Enter Supplier Name.!!'); $('#Txt_Retailer').focus(); return false; }
                    var order_date = $('#<%=InvoiceNumber.ClientID%>').val();
                    if (order_date.length <= 0) { alert('Enter Order Date.!!'); $('#InvoiceNumber').focus(); return false; }
                    var inv_Date = $('#<%=Txt_in_date.ClientID%>').val();
                    if (inv_Date.length <= 0) { alert('Enter Invoice Date.!!'); $('#Txt_in_date').focus(); return false; }
                    var Del_Date = $('#datepicker1').val();
                    if (Del_Date.length <= 0) { alert('Enter Delivery Date.!!'); $('#datepicker1').focus(); return false; }
                    var pay_term = $('#Sel_Pay_Term option:selected').text();
                    if (pay_term == "--- Select ---") { alert('Select Payment Term!!'); $('#Sel_Pay_Term').focus(); return false; }
                    var Pay_Due = $('#datepicker').val();
                    if (Pay_Due.length <= 0) { alert('Enter Payment Due.!!'); $('#datepicker').focus(); return false; }
                    var ship_met = $('#Sel_Shi_Med option:selected').text();
                    if (ship_met == "--- Select ---") { alert('Select Shipping Method!!'); $('#Sel_Shi_Med').focus(); return false; }
                    var ship_term = $('#<%=Txt_Shp_Term.ClientID%>').val();
                    if (ship_term.length <= 0) { alert('Enter Shipping Term!!'); $('#Txt_Shp_Term').focus(); return false; }
                    var sub_tot = $('#sub_tot').val();
                    if (sub_tot.length <= 0) { alert('Enter Subtotal!!'); $('#sub_tot').focus(); return false; }
                    var tax_tot = $('#Tax_tot').val();
                    if (tax_tot.length <= 0) { alert('Enter Tax Total!!'); $('#Tax_tot').focus(); return false; }
                    var total = $('#in_Tot').val();
                    if (total.length <= 0) { alert('Enter Total!!'); $('#in_Tot').focus(); return false; }
                    var ad_paid = $('#Ad_Paid').val();
                    if (ad_paid.length <= 0) { alert('Enter Advanced Paid!!'); $('#Ad_Paid').focus(); return false; }
                    var amt_due = $('#Amt_Tot').val();
                    if (amt_due.length <= 0) { alert('Enter Amount Due!!'); $('#Amt_Tot').focus(); return false; }
                    var remark = $('#notes').val();
                    var Dis_Tot = $('#Txt_Dis_tot').val();
                    var Order_No = $('#<%=Hid_orderno.ClientID%>').val();

                    var Stk = [];

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
                    mainarr.push({
                        order_no: Order_No,
                        sf_code: sf_code,
                        sf_name: sf_Name,
                        dis_code: dis_code,
                        dis_name: dis_Name,
                        cus_code: Cus_code,
                        cus_name: Cus_Name,
                        order_date: order_date,
                        inv_date: inv_Date,
                        del_date: Del_Date,
                        pay_term: pay_term,
                        pay_due: Pay_Due,
                        ship_mtd: ship_met,
                        ship_term: ship_term,
                        sub_total: sub_tot,
                        tax_total: tax_tot,
                        total: total,
                        adv_paid: ad_paid,
                        Amt_due: amt_due,
                        remark: remark,
                        Dis_total: Dis_Tot,
                        oqty: oqty
                    });

                    var dtls_tab = document.getElementById("tblCustomers");
                    var nrows1 = dtls_tab.rows.length;
                    var Ncols = dtls_tab.rows[0].cells.length;

                    if (nrows1 > 1) {
                        var ch = true;
                        var arr = [];
                        $('#tblCustomers > tbody > tr').each(function () {
                            var sno = $.trim($(this).find('[id*=snum]').html());
                            var Prd_code = $(this).find('[id*=Hid_Pro_code]').val();
                            var Prd_name = $(this).find('[id*=Item_1]').val();
                            var price = $(this).find('[id*=Price_]').val();
                            var dis = $(this).find('[id*=Dis]').val();
                            var free = $(this).find('[id*=Free]').val();
                            var qty = $(this).find('[id*=english]').val();
                            var tot = $(this).find('[id*=total]').val();
                            var Exp_code = $(this).find('option:selected').val();
                            var Exp_name = $(this).find('option:selected').text();
                            console.log(Exp_code + ":" + Exp_name);

                            taxArr = [];
                            $(this).find('.sel_row').each(function () {
                                taxArr.push({
                                    taxCode: $(this).find('.selTax').val(),
                                    tax_Name: $(this).find('.selTax').find('option:selected').text(),
                                    value: (numeral($(this).find('.selTax').val()) * numeral(tot) / 100)
                                });
                            });

                            console.log(taxArr);
                            dayarr.push({
                                Trans_order_no: sno,
                                Product_code: Prd_code,
                                Product_name: Prd_name,
                                Price: price,
                                Discount: dis,
                                Free: free,
                                Quantity: qty,
                                Amount: tot,
                                taxDtls: taxArr
                            });
                        });

                    }


                    var objmain = {};
                    objmain['MTED'] = mainarr;
                    objmain['TED'] = dayarr;
                    console.log(objmain);
                    if (dayarr.length <= 0) {
                        alert('No Records Fount');
                        return false;
                    }
                    console.log(JSON.stringify(objmain));

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "Pri_Invoice_Entry.aspx/SaveDate",
                        data: "{'data':'" + JSON.stringify(objmain) + "','savemode':'" + approve + "'}",
                        dataType: "json",
                        success: function (data) {
                            alert(data.d);
                        },
                        error: function (data) {
                            alert(JSON.stringify(data));
                        }
                    });

                });

                var StkDetail = [];
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Pri_Invoice_Entry.aspx/GetCurrStock",
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
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Pri_Invoice_Entry.aspx/GetHolidays",
                    dataType: "json",
                    success: function (data) {
                        dts = data.d;
                        if (dts.length > 0) {
                            filldata(dts);

                        }
                    },
                    error: function (data) {
                        alert(JSON.stringify(data));
                    }
                });


                $(document).on('change', '[id*=Item_1]', function () {
                    var kk = '';

                    var selProds = "";
                    var row = $(this).closest("tr");
                    var indx = $(row).index();
                    var $Prds = $('[id*=Item_1]');
                    for (il = 0; il < $Prds.length; il++) {
                        if (indx != il) {
                            selProds += $($Prds[il]).val() + ',';
                        }
                    }
                    if ((','+selProds).indexOf(','+$(this).val()+',') > -1) {
                        alert('Already your select this product');
                        return false;
                    }
                });



                //stock_check
                $("#tblCustomers >tbody >tr").each(function () {
                    var row = $(this).closest("tr");
                    var val = row.find('input[name=stkval]').attr('stkgood');
                    var qty = row.find('[id*=english]').val();

                    $("#btndaysave").show();
                    $("#btnprint").show();

                    $("#stk_chk").hide();
                    //row.find('[id*=txtVal]').val(0);

                });
                function filldata(dts) {
                    //console.log(dts);
                    $(".autoc").autocomplete({
                        source: dts
                    });
                }


            });

           
             
         
        </script>
    </head>
    <body>
        <form id="form1" runat="server">
        <input id="holiID" type="hidden" />
        <div class="container">
        </div>
        <div class="container-fluid">
            <div class="container" id="itemlist2">
                <div class="row" style="margin-top: 0px;">
                    <div class="col-md-3 col-lg-4">
                        <div class="form-group">
                            <label class="control-label" for="focusedInput">
                                Supplier Name</label>
                            <input class="form-control" id="Txt_Retailer" runat="server" name="Retailer" data-validation="required"
                                type="text" autocomplete="off" />
                            <input type="hidden" id="Hid_Cus_Name" runat="server" name="Hid_Cus_Name" />
                        </div>
                    </div>
                    <div class="col-md-3 col-lg-4">
                        <div class="form-group">
                            <label class="control-label" for="focusedInput">
                                Distributor Name</label>
                            <input class="form-control" id="Txt_dist" runat="server" name="dist" data-validation="required"
                                type="text" autocomplete="off" />
                            <input type="hidden" id="Hid_Dis_Name" runat="server" name="Hid_Dis_Name" />
                        </div>
                    </div>
                    <%--<div class="col-md-3 col-lg-2">
                        <div class="form-group">
                            <label class="control-label" for="focusedInput">
                                Field Force Name</label>
                            <input class="form-control" id="Txt_fldForce" runat="server" name="Fld_force" data-validation="required"
                                type="text" autocomplete="off" />
                        </div>
                    </div>--%>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label class="control-label" for="focusedInput">
                                Order Date</label>
                            <input name="Txt_Orderdate" type="text" id="InvoiceNumber" runat="server" class="form-control"
                                data-validation="required" readonly="" value="" />
                            <span id="status"></span>
                        </div>
                    </div>
                </div>
                <div class="row" style="margin-top: 12px;">
                    <div class="col-md-3 col-lg-2">
                        <div class="form-group">
                            <label class="control-label" for="focusedInput">
                                Payment Term</label>
                            <select class="form-control" id="Sel_Pay_Term" name="Sel_Pay_Term" data-validation="required">
                            </select>
                        </div>
                    </div>
                    <div class="col-md-3 col-lg-2">
                        <div class="form-group">
                            <label class="control-label" for="focusedInput">
                                Payment Due</label>
                            <input class="form-control" id="datepicker" name="datepicker" data-validation="required"
                                type="text" autocomplete="off" />
                        </div>
                    </div>
                    <div class="col-md-3 col-lg-2">
                        <div class="form-group">
                            <label class="control-label" for="focusedInput">
                                Shipping Method</label>
                            <select class="form-control" id="Sel_Shi_Med" name="Shi_Med" data-validation="required">
                            </select>
                        </div>
                    </div>
                    <div class="col-md-3 col-lg-4">
                        <div class="form-group">
                            <label class="control-label" for="focusedInput">
                                Shipping Term</label>
                            <input class="form-control" id="Txt_Shp_Term" runat="server" name="Shp_Team" data-validation="required"
                                type="text" autocomplete="off" />
                        </div>
                    </div>
                </div>
                <div class="row" style="margin-top: 12px;">
                    <div class="col-md-3 col-lg-4">
                        <div class="form-group">
                            <label class="control-label" for="focusedInput">
                                Field Force Name</label>
                            <input class="form-control" id="Txt_fldForce" runat="server" name="Fld_force" data-validation="required"
                                type="text" autocomplete="off" />
                            <input type="hidden" id="Hid_Sf_Name" runat="server" name="Hid_Sf_Name" />
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label class="control-label" for="focusedInput">
                                Delivery Date</label>
                            <input class="form-control" id="datepicker1" name="datepicker1" data-validation="required"
                                type="text" autocomplete="off" />
                            <%-- <input class="form-control hasDatepicker error" id="datepicker1" data-validation="required" name="datepicker1" type="text" autocomplete="off" style="border-color: red;" />--%>
                            <span id="Span2"></span>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label class="control-label" for="focusedInput">
                                Invoice Date</label>
                            <input name="Txt_Orderdate" type="text" id="Txt_in_date" runat="server" class="form-control"
                                data-validation="required" readonly="" value="" />
                            <span id="Span1"></span>
                            <input type="hidden" id="Hid_orderno" runat="server" name="Hid_orderno" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <h5>
                            Add Items:</h5>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                        <table id="tblCustomers" class="table table-bordered table-hover">
                            <asp:Repeater ID="rptOrders" runat="server">
                                <HeaderTemplate>
                                    <thead class="ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix">
                                        <tr>
                                            <th width="2%">
                                                <input type="checkbox" class="formcontrol" id="check_all">
                                            </th>
                                            <th width="5%">
                                                S.No.
                                            </th>
                                            <th width="20%">
                                                Product Name
                                            </th>
                                            <th width="10%">
                                                Price
                                            </th>
                                            <th width="10%">
                                                Discount(%)
                                            </th>
                                            <th width="10%">
                                                Free
                                            </th>
                                            <th width="5%">
                                                CQuantity
                                            </th>
                                            <th width="11%">
                                                Amount
                                            </th>
                                            <th width="8%">
                                                Tax %
                                            </th>
                                            <th width="7%">
                                                Tax Amt.
                                            </th>
                                        </tr>
                                    </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr id="player-row-973" class="row_sum">
                                        <td>
                                            <input type='checkbox' class='case' />
                                        </td>
                                        <td>
                                            <span id='snum'>
                                                <%# Container.ItemIndex + 1 %></span>
                                        </td>
                                        <td>
                                            <input class="form-control autoc" type='text' data-validation="required" id='Item_1'
                                                name='item_name[]' value='<%# Eval("Product_Name")%>' readonly />
                                            <input type="hidden" id="Hid_Pro_code" runat="server" name="Hid_Pro_code" value='<%# Eval("Product_Code")%>' />
                                        </td>
                                        <td>
                                            <input class="form-control" type='text' id='Price_' name='price[]' data-cell="C1"
                                                data-format="0.00" data-validation="required" value='<%# Eval("Distributor_Price")%>'
                                                readonly />
                                            <input type='hidden' name='stkval' stkgood='0' stkdamage='0' />
                                        </td>
                                        <td>
                                            <input class="form-control" type='text' id='Dis' name='dis[]' data-cell="Y1" data-format="0"
                                                data-validation="required" value='<%# Eval("PQty")%>' />
                                        </td>
                                        <td>
                                            <input class="form-control" type='text' id='Free' name='free[]' data-cell="I1" data-format="0"
                                                data-validation="required" value='<%# Eval("PQty")%>' />
                                        </td>
                                        <td>
                                            <input class="form-control" type='text' data-validation="required" id='english' name='quantity[]'
                                                data-cell="D1" data-format="0" value='<%# Eval("CQty")%>' />
                                        </td>
                                        <td>
                                            <input class="form-control" type='text' id='total' name='total[]' data-cell="E1"
                                                data-format="0.00" readonly />
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
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
                    <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                    </div>
                    <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                    </div>
                    <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                        <div class="form-group form-inline">
                            <label>
                                Subtotal: &nbsp;</label>
                            <div class="input-group">
                                <div class="input-group-addon currency">
                                    <i class="fa fa-inr"></i>
                                </div>
                                <input data-cell="G1" id="sub_tot" data-format="0,0.00" class="form-control" readonly />
                            </div>
                        </div>
                        <div class="form-group" style="display: none;">
                            <label style="display: none;">
                                Discount Total: &nbsp;</label>
                            <div class="input-group">
                                <div class="input-group-addon currency">
                                    <i class="fa fa-inr"></i>
                                </div>
                                <input style="display: none;" id="Txt_Dis_tot" data-cell="M2" data-format="0,0.00"
                                    class="form-control" readonly />
                            </div>
                        </div>
                        <div class="form-group">
                            <label>
                                Tax Total: &nbsp;</label>
                            <div class="input-group">
                                <div class="input-group-addon currency">
                                    <i class="fa fa-inr"></i>
                                </div>
                                <input id="Tax_tot" data-cell="M1" data-format="0,0.00" class="form-control" readonly />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-10 col-sm-8 col-md-2 col-lg-8">
                        <div class="row">
                            <div class="col-xs-8 col-sm-6 col-md-6 col-lg-6">
                                <h3>
                                    Notes:
                                </h3>
                                <div class="form-group">
                                    <textarea placeholder="Your Notes" id="notes" name="notes" rows="5" class="form-control txt"></textarea>
                                </div>
                                <%--  <div class="form-group text-center">
                                    <input type="submit" id="In_Save" name="Button1" value="Save Invoice" 
                                        class="btn btn-success pleasewait" />
                                </div>--%>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-4 col-md-2 col-lg-4">
                        <div class="form-group">
                            <label>
                                Total: &nbsp;</label>
                            <div class="input-group">
                                <div class="input-group-addon currency">
                                    <i class="fa fa-inr"></i>
                                </div>
                                <input data-cell="Y1" id="in_Tot" data-format="0,0.00" class="form-control" readonly />
                            </div>
                        </div>
                        <div class="form-group">
                            <label>
                                Advanced Paid: &nbsp;</label>
                            <div class="input-group">
                                <div class="input-group-addon currency">
                                    <i class="fa fa-inr"></i>
                                </div>
                                <input data-cell="K1" id="Ad_Paid" data-format="0,0.00" class="form-control" data-validation="required"
                                    name="amountpaid" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label>
                                Amount Due: &nbsp;</label>
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
            </div>
            <div class="row" style="text-align: center">
                <div class="col-sm-12 inputGroupContainer">
                    <a id="stk_chk" style="font-size: medium">Qty Value High on Stock Value</a> <a id="btndaysave"
                        class="btn btn-primary btnsave" style="vertical-align: middle; font-size: 17px;">
                        <span>Save Invoice</span></a>&nbsp&nbsp<a id="btnprint" class="btn btn-success pleasewait"
                            style="vertical-align: middle; font-size: 17px;"> <span>Print Invoice</span></a></div>
            </div>
            <script src="Scripts/bootbox.min.js" type="text/javascript"></script>
            <script src="Scripts/numeral.min.js" type="text/javascript"></script>
            <script src="Scripts/bootstrap.js" type="text/javascript"></script>
            <script type="text/javascript">
                $form = $('#itemlist2').calx();
                var i = 2;

                $(document).ready(function () {
                    $form.calx('update');

                    var mode = GetParameterValues('mode');

                    if (mode == "automatic") {
                        $('#InvoiceNumber').attr('readonly', true);
                    }
                    else if (mode == "manual") {
                        $('#InvoiceNumber').attr('readonly', false);
                    }

                });

                function GetParameterValues(param) {
                    var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
                    for (var i = 0; i < url.length; i++) {
                        var urlparam = url[i].split('=');
                        if (urlparam[0] == param) {
                            return urlparam[1];
                        }
                    }
                }


                $(document).ready(function () {
                    $(document).on('focus', '.autoc', function () {
                        $(".autoc").autocomplete({
                            source: function (request, response) {
                                $.ajax({
                                    url: '/Service.asmx/GetCustomers',
                                    data: "{ 'prefix': '" + request.term + "'}",
                                    dataType: "json",
                                    type: "POST",
                                    contentType: "application/json; charset=utf-8",
                                    success: function (data) {
                                        response($.map(data.d, function (item) {
                                            return {
                                                label: item.split('-')[0],
                                                val: item.split('-')[1]
                                            }
                                        }))
                                    },
                                    error: function (response) {
                                        alert(response.responseText);
                                    },
                                    failure: function (response) {
                                        alert(response.responseText);
                                    }
                                });
                            },
                            select: function (e, i) {
                                id_arr = $(this).attr('id');
                                id = id_arr.split("_");
                                $('#Price_1' + id[1]).val(i.item.val);
                                $form.calx('update');
                            },
                            minLength: 1
                        });

                    });
                });
            </script>
            <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
            <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
            <script src="//cdnjs.cloudflare.com/ajax/libs/jquery-form-validator/2.2.1/jquery.form-validator.min.js"></script>
            <style>
                #ui-datepicker-div
                {
                    z-index: 9999999 !important;
                }
            </style>
            <script>
                $(function () {
                    $("#datepicker").datepicker({ dateFormat: 'dd/mm/yy' });
                    $("#datepicker1").datepicker({ dateFormat: 'dd/mm/yy' });
                });
            </script>
            <script>                $.validate(); </script>
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
        </div>
        </form>
    </body>
    </html>
</asp:Content>
