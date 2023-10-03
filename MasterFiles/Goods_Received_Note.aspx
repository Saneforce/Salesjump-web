<%@ Page Title="Goods Received Note" Language="C#" MasterPageFile="~/Master.master"
    AutoEventWireup="true" CodeFile="Goods_Received_Note.aspx.cs" Inherits="MasterFiles_Goods_Received_Note" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <style type="text/css">
        body
        {
            overflow-x: initial !important;
        }
        input[type='text'], select, label
        {
            line-height: 22px;
            padding: 4px 6px;
            font-size: medium;
            border-radius: 7px;
            width: 100%;
        }
        #ProductTable input[type='text']
        {
            text-align: right;
        }
        .row
        {
            padding: 2px 2px;
        }
        #ProductTable
        {
            font-family: "Trebuchet MS" , Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }
        
        #ProductTable td, #ProductTable th
        {
            border: 1px solid #ddd;
            padding: 2px 4px;
        }
        
        #ProductTable td
        {
            vertical-align: top;
        }
        
        #ProductTable tr:nth-child(even)
        {
            background-color: #f2f2f2;
        }
        
        #ProductTable tr:hover
        {
            background-color: #ddd;
        }
        
        #ProductTable th
        {
            padding-top: 12px;
            padding-bottom: 12px;
            padding-left: 5px;
            padding-right: 5px;
            text-align: center;
            background-color: #475677;
            color: white;
        }
        #ProductTable td:nth-child(10), #ProductTable th:nth-child(10)
        {
            display: none;
        }
        
        #ProductTable td:nth-child(11), #ProductTable td:nth-child(13)
        {
            text-align: right;
        }
    </style>
    <link rel="stylesheet" type="text/css" media="all" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var arrTAX = [];
            var StkDetail = [];
            $('.datetimepicker').datepicker({ dateFormat: 'dd/mm/yy' });
            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!

            var yyyy = today.getFullYear();
            if (dd < 10) {
                dd = '0' + dd;
            }
            if (mm < 10) {
                mm = '0' + mm;
            }
            var today = dd + '/' + mm + '/' + yyyy;
            $('#txtEdate').val(today);

            $(document).keydown(function (event) {
                if (event.ctrlKey == true && (event.which == '118' || event.which == '86')) {
                    alert('thou. shalt. not. PASTE!');
                    event.preventDefault();
                }
            });

            $('#grnDisDate').attr('disabled', true);
            $(document).on('change', '#grnDate', function () {
                if ($(this).val().length > 0) {
                    if (isValidDate($(this).val())) {
                        var fit_start_time = $(this).val();
                        var FromDate = fit_start_time.split("/").reverse().join("-");
                        var dts = new Date(FromDate);
                        var cr = dts.getDate() + '/' + (dts.getMonth() + 1) + '/' + dts.getFullYear();
                        $('#grnDisDate').datepicker("destroy");
                        $('#grnDisDate').datepicker({ dateFormat: 'dd/mm/yy', maxDate: cr, defaultDate: cr });
                        $('#grnDisDate').val('');
                        $('#grnDisDate').attr('disabled', false);
                    }
                    else {
                        alert('Invalid Date Enter or Select Correct Date..!');
                        $('#grnDisDate').attr('disabled', true);
                        $(this).val('');
                        $(this).focus();

                    }
                }
                else {
                    $('#grnDisDate').attr('disabled', true);
                }


            });

            //	$(document).on('focus','#grnDisDate',function() {               
            //  if($('#grnDate').val().length<=0)
            //  {
            //      alert('Enter GRN Date..!');
            //      $('#grnDate').focus();
            //  }
            //	});



            $(document).on('change', '#grnDisDate', function () {
                if ($(this).val().length > 0) {
                    if (isValidDate($(this).val())) {
                    }
                    else {
                        alert('Invalid Date Enter or Select Correct Date..!');
                        $(this).val('');
                        $(this).focus();
                    }
                }
            });

            function isValidDate(s) {
                var bits = s.split('/');
                var d = new Date(bits[2] + '/' + bits[1] + '/' + bits[0]);
                return !!(d && (d.getMonth() + 1) == bits[1] && d.getDate() == Number(bits[0]));
            }



            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Goods_Received_Note.aspx/GetSupplier",
                dataType: "json",
                success: function (data) {
                    var ddlCustomers = $("#ddlsupplier");
                    ddlCustomers.empty().append('<option selected="selected" value="0">--- Select ---</option>');
                    $.each(data.d, function () {
                        ddlCustomers.append($("<option></option>").val(this['disCode']).html(this['disName']));
                    });
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Goods_Received_Note.aspx/GetDistributor",
                dataType: "json",
                success: function (data) {
                    var ddlCustomers = $("#ddldistributor");
                    ddlCustomers.empty().append('<option selected="selected" value="0">--- Select ---</option>');
                    $.each(data.d, function () {
                        ddlCustomers.append($("<option></option>").val(this['disCode']).html(this['disName']));
                    });
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Goods_Received_Note.aspx/GetTAXType",
                dataType: "json",
                success: function (data) {
                    for (var i = 0; i < data.d.length; i++) {
                        arrTAX.push({ Tax_Val: data.d[i].Exp_code, alw_name: data.d[i].Exp_name, Tax_Id: data.d[i].Tax_Id });
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
                url: "Goods_Received_Note.aspx/GetCurrStock",
                dataType: "json",
                success: function (data) {
                    StkDetail = data.d;
                    console.log(StkDetail);
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Goods_Received_Note.aspx/GetProduct",
                dataType: "json",
                success: function (data) {
                    var str = "";
                    for (var i = 0; i < data.d.length; i++) {
                        str = "<td>" + (i + 1) + "</td><td style='min-width: 115px;'>" + data.d[i].pCode + "</td><td style='min-width: 300px;'>" + data.d[i].pName + "</td><td><input type='hidden' name='Erp_Code' value='" + data.d[i].Erp_Code + "'/> <input type='hidden' name='UOM_Code' value='" + data.d[i].pUOM + "'/>" + data.d[i].pUOM_Name + "</td><td><input type='hidden' name='stkval'/><input type='text' name='txtBatch' maxlength='10' style='min-width: 115px;' /></td><td><input type='text' name='txtDate' style='min-width: 115px;'/></td><td><input type='text' name='txtPoqty' value='0' maxlength='7' class='numberOnly'  style='min-width: 100px;'/></td><td><input type='text' name='txtPrice' value='0' maxlength='10' class='textval'  style='min-width: 100px;'/></td><td><input type='text' name='txtGood' value='0'maxlength='7' class='textval' readonly style='min-width: 100px;'/></td>";
                        str += "<td><input type='text' name='txtDamaged'  disabled='disabled' value='0' /></td><td style='min-width: 150px;'><label name='grossVal'>0.00</label></td><td></td><td style='min-width: 150px;'><label name='netVal'>0.00</label></td>";
                        $('#ProductTable >tbody').append('<tr>' + str + ' </tr>');
                    }
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });

            $('#ProductTable:first tbody tr').each(function () {
                $(this).children('td').eq(11).append('<table name="addtable" style="width:100%"><tbody><tr class="cssTax"><td style="min-width:125px;"><select name="addTax" class="form-control selTax"/></td><td style="min-width:80px;"><input type="text" name="taxVal" readonly /></td><td style="width:40px;"><a name="btnadd" class="btn btn-primary"><span>+</span></a></td></tr></tbody></table>');
                // $(this).children('td').eq(9).append('<table name="addtable1" style="width:100%"><tbody><tr><td><select name="addtax" class="form-control"/></td><td style="width:40px;"><a name="btnadd" class="btn btn-primary"><span>+</span></a></td></tr></tbody></table>');
            });

            loadddl(arrTAX);



            function loadval() {
                dtd = StkDetail.filter(function (a) {
                    return (a.Dist_Code == $("#ddldistributor").val());
                });
                $('#ProductTable > tbody > tr').each(function () {
                    if (dtd.length > 0) {
                        for (var i = 0; i < dtd.length; i++) {
                            if ($(this).find("td").eq(1).text() == dtd[i].Prod_Code) {
                                console.log($(this).find("input[name=txtBatch]").val());
                                console.log(dtd[i].BatchNo);
                                if ($(this).find("input[name=txtBatch]").val() == dtd[i].BatchNo) {

                                    $(this).find("input[name=stkval]").attr('stkgood', dtd[i].GStock);
                                    $(this).find("input[name=stkval]").attr('stkdamage', dtd[i].DStock);
                                }
                            }
                        }
                    }
                    else {
                        $(this).find("input[name=stkval]").attr('stkgood', "0");
                        $(this).find("input[name=stkval]").attr('stkdamage', "0");

                    }
                });
            }

            if ($('#<%=hdnmode.ClientID %>').val() == "1") {
                $('#lblgrnno').css('display', 'inline-block');
                $('#grnNo').css('display', 'inline-block');
                $('#grnNo').attr('disabled', true);
                $('#grnDate').attr('disabled', true);
                $('#ddlsupplier').attr('disabled', true);
                $('#ddldistributor').attr('disabled', true);

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Goods_Received_Note.aspx/Get_AllValues",
                    data: "{'grnNo':'" + $('#<%=hdngrn_no.ClientID %>').val() + "','grnDate':'" + $('#<%=hdngrn_date.ClientID %>').val() + "','grnSuppcode':'" + $('#<%=hdnsupp_code.ClientID %>').val() + "'}",
                    dataType: "json",
                    success: function (data) {
                        //  console.log(JSON.parse(data.d));
                        var obj = JSON.parse(data.d);

                        if (obj.TransH.length > 0) {
                            $('#grnNo').val(obj.TransH[0].GRN_No);
                            $('#grnDate').val(obj.TransH[0].GRN_Date);
                            $('#ddlsupplier').val(obj.TransH[0].Supp_Code);
                            $('#txtEdate').val(obj.TransH[0].Entry_Date);
                            $('#grnPono').val(obj.TransH[0].Po_No);
                            $('#challenNo').val(obj.TransH[0].Challan_No);
                            $('#grnDisDate').val(obj.TransH[0].Dispatch_Date);
                            $('#ddldistributor').val(obj.TransH[0].Received_Location);
                            $('#receivedby').val(obj.TransH[0].Received_By);
                            $('#authorised').val(obj.TransH[0].Authorized_By);
                            $('#remarks').val(obj.TransH[0].remarks);
                        }


                        var fit_start_time = obj.TransH[0].GRN_Date;
                        var FromDate = fit_start_time.split("/").reverse().join("-");
                        var dts = new Date(FromDate);
                        var cr = dts.getDate() + '/' + (dts.getMonth() + 1) + '/' + dts.getFullYear();
                        $('#grnDisDate').datepicker("destroy");
                        $('#grnDisDate').datepicker({ dateFormat: 'dd/mm/yy', maxDate: cr, defaultDate: cr });
                        $('#grnDisDate').val(obj.TransH[0].Dispatch_Date);




                        $('#ProductTable > tbody > tr').each(function () {
                            for (var i = 0; i < obj.TransD.length; i++) {
                                // console.log($(this).find("td").eq(1).text());
                                // console.log(obj.TransD[i].PCode);
                                if ($(this).find("td").eq(1).text() == obj.TransD[i].PCode) {
                                    $(this).find("input[name=txtBatch]").attr('disabled', true);
                                    $(this).find("input[name=txtBatch]").val(obj.TransD[i].Batch_No);
                                    $(this).find("input[name=txtPoqty]").val(obj.TransD[i].POQTY);
                                    $(this).find("input[name=txtPoqty]").attr('pv_poQty', obj.TransD[i].POQTY);
                                    $(this).find("input[name=txtDate]").val(obj.TransD[i].mfgDate);
                                    $(this).find("input[name=txtPrice]").val(obj.TransD[i].Price);
                                    $(this).find("input[name=txtGood]").val(obj.TransD[i].Good);
                                    $(this).find("input[name=txtGood]").attr('pv_good', obj.TransD[i].Good);
                                    $(this).find("input[name=txtDamaged]").val(obj.TransD[i].Damaged);
                                    $(this).find("input[name=txtDamaged]").attr('pv_damage', obj.TransD[i].Damaged);
                                    $(this).find("label[name=grossVal]").val(obj.TransD[i].Gross_Value);
                                    $(this).find("label[name=netVal]").val(obj.TransD[i].Net_Value);
                                    for (var j = 0; j < obj.TransD[i].taxDtls.length; j++) {
                                        $(this).find('table[name=addtable] tbody tr:last').find('.selTax').val(obj.TransD[i].taxDtls[j].Tax_Code);
                                        $(this).find('table[name=addtable] tbody tr:last').find('input[name=taxVal]').val(obj.TransD[i].taxDtls[j].Tax_Value);
                                    }

                                }
                            }
                        });
                        loadval();

                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });

            }

            var Ttot_Good = 0;
            var Ttot_Tax = 0;
            var Ttot_Net = 0;
            $('#ProductTable:first tbody tr').each(function () {
                var price = $(this).find("input[name=txtPrice]").val();
                var good = $(this).find("input[name=txtGood]").val();
                if (isNaN(price)) price = 0;
                if (isNaN(good)) good = 0;
                var tottax = 0;
                $(this).find('.cssTax').each(function () {
                    var tax = $(this).find('.selTax').find('option:selected').attr('tax_val');
                    if (isNaN(tax)) tax = 0;
                    var TaxAmt = ((price * good) * tax) / 100;
                    tottax = Number(tottax) + Number(TaxAmt);

                    $(this).find("input[name=taxVal]").val(TaxAmt.toFixed(2));
                });
                Ttot_Tax += tottax;
                $(this).find("label[name=grossVal]").text(price * good);
                Ttot_Good += (price * good);
                var sum = price * good + Number(tottax);
                Ttot_Net += sum;
                $(this).find("label[name=netVal]").text(sum.toFixed(2));
            });


            $("#gnd_Good_tot").text(Ttot_Good.toFixed(2));
            $("#gnd_Txt_tot").text(Ttot_Tax.toFixed(2));
            $("#gnd_Net_tot").text(Ttot_Net.toFixed(2));



            function rowCal() {
                var tot_Good = 0;
                var tot_Tax = 0;
                var tot_Net = 0;
                $('#ProductTable:first tbody tr').each(function () {
                    var price = $(this).find("input[name=txtPrice]").val();
                    var good = $(this).find("input[name=txtGood]").val();
                    if (isNaN(price)) price = 0;
                    if (isNaN(good)) good = 0;
                    var tottax = 0;
                    $(this).find('.cssTax').each(function () {
                        var tax = $(this).find('.selTax').find('option:selected').attr('tax_val');
                        if (isNaN(tax)) tax = 0;
                        var TaxAmt = ((price * good) * tax) / 100;
                        tottax = Number(tottax) + Number(TaxAmt);
                        $(this).find("input[name=taxVal]").val(TaxAmt.toFixed(2));
                    });
                    tot_Tax += tottax;
                    $(this).find("label[name=grossVal]").text(price * good);
                    tot_Good += price * good;
                    var sum = price * good + Number(tottax);
                    tot_Net += sum;
                    $(this).find("label[name=netVal]").text(sum.toFixed(2));
                });


                $("#gnd_Good_tot").text(tot_Good.toFixed(2));
                $("#gnd_Txt_tot").text(tot_Tax.toFixed(2));
                $("#gnd_Net_tot").text(tot_Net.toFixed(2));
            }

            $(document).on('click', 'a[name=btnadd]', function () {
                var good = $(this).closest('table').closest('tr').find("input[name=txtGood]").val();
                var price = $(this).closest('table').closest('tr').find("input[name=txtPrice]").val();
                var x = '';
                if ($(this).text().toString() == "+") {
                    if ($(this).closest('tr').find('.selTax').children('option').length - 1 > $(this).closest('table').find('tr').length) {
                        $(this).text("-");
                        $(this).closest('tr').after('<tr class="cssTax"><td style="min-width:125px;"><select name="addTax" class="form-control selTax"/></td><td style="min-width:80px;"><input type="text" name="taxVal" readonly /></td><td style="width:40px;"><a name="btnadd" class="btn btn-primary"><span>+</span></a></td></tr>');
                        loadddl(arrTAX, $(this).closest('tbody'));
                        var taxper = $(this).closest('table').find('tr:last').find('.selTax').find('option:selected').attr('tax_val');
                        var taxTot = (Number(good) * Number(price) * Number(taxper)) / 100;
                        $(this).closest('table').find('tr:last').find('input[name=taxVal]').val(taxTot.toFixed(2));
                        x = $(this).closest('table').closest('tr');
                    }
                }
                else {
                    x = $(this).closest('table').closest('tr');
                    $(this).closest('tr').remove();
                }
                var tottax = 0;
                $(x).find('.cssTax').each(function () {
                    var tax = $(this).find('.selTax').find('option:selected').attr('tax_val');
                    if (isNaN(tax)) tax = 0;
                    var TaxAmt = ((good * price) * tax) / 100;
                    tottax = Number(tottax) + Number(TaxAmt);
                });
                var sum = good * price + Number(tottax);
                $(x).find("label[name=netVal]").text(sum.toFixed(2));
                rowCal();
            });



            $(document).on('blur', 'input[name=txtGood]', function () {

                var god = $(this).val();
                if (god.length <= 0) {
                    $(this).val(0);
                }

                var currVal = $(this).closest('tr').find('input[name=stkval]').attr('stkgood') || 0;
                var oldVal = $(this).closest('tr').find('input[name=txtGood]').attr('pv_good') || 0;

                var currDmg = $(this).closest('tr').find('input[name=stkval]').attr('stkdamage') || 0;
                var oldDmg = $(this).closest('tr').find('input[name=txtDamaged]').attr('pv_damage') || 0;


                var qty = $(this).closest('tr').find('input[name=txtPoqty]').val() || 0;

                if (Number(qty) < Number($(this).val())) {
                    $(this).val(oldVal);
                    alert('Goods Value Must below ' + qty + '..!');
                    $(this).focus();
                }
                else {
                    var damage = Number(qty) - Number($(this).val());
                    $(this).closest('tr').find('input[name=txtDamaged]').val(damage)

                }

                var dmg = $(this).closest('tr').find('input[name=txtDamaged]').val() || 0;


                var vqty = 0;
                console.log(currVal + ":" + oldVal);
                if (Number(currVal) < Number(oldVal)) {
                    console.log('en')
                    vqty = oldVal - currVal;
                }


                console.log(vqty + ":" + god);

                if (Number(vqty) > Number(god)) {
                    $(this).val(oldVal);
                    alert('Goods Value Must above ' + vqty + ' ..!');
                    $(this).focus();
                }

                var vDmg = 0;
                if (Number(currDmg) < Number(oldDmg)) {
                    vDmg = oldDmg - currDmg;
                }

                if (Number(vDmg) > Number(dmg)) {
                    $(this).val(oldVal);
                    var ent = (qty - vDmg)
                    alert('Goods Value Must Below ' + ent + ' ..!');
                    $(this).focus();
                }



            });

            $(document).on('blur', 'input[name=txtPoqty]', function () {
                var poq = $(this).val();
                if (poq.length <= 0) {
                    $(this).val(0);
                }

                var currVal = $(this).closest('tr').find('input[name=stkval]').attr('stkgood') || 0;
                var oldVal = $(this).closest('tr').find('input[name=txtGood]').attr('pv_good') || 0;

                var currDmg = $(this).closest('tr').find('input[name=stkval]').attr('stkdamage') || 0;
                var oldDmg = $(this).closest('tr').find('input[name=txtDamaged]').attr('pv_damage') || 0;
                var god = $(this).closest('tr').find('input[name=txtGood]').val() || 0;
                var dmg = $(this).closest('tr').find('input[name=txtDamaged]').val() || 0;

                var vqty = 0;

                if (Number(currVal) < Number(oldVal)) {
                    vqty = oldVal - currVal;
                }
                var vDmg = 0;
                if (Number(currDmg) < Number(oldDmg)) {
                    vDmg = oldDmg - currDmg;
                }

                var TotCrrQty = Number(vqty);

                if (Number(TotCrrQty) > Number(poq)) {
                    $(this).val($(this).closest('tr').find('input[name=txtPoqty]').attr('pv_poQty') || $(this).closest('tr').find('input[name=txtGood]').val());
                    alert('PO QTY Value Must Above ' + TotCrrQty + ' ..!');
                    $(this).focus();
                }

                // $(this).closest('tr').find('input[name=txtGood]').val(0);
                var totDmg = Number(poq) - Number($(this).closest('tr').find('input[name=txtGood]').val() || 0);
                $(this).closest('tr').find('input[name=txtGood]').val(poq);
                //$(this).closest('tr').find('input[name=txtDamaged]').val(totDmg);
            });

            $(document).on('blur', 'input[name=txtPrice]', function () {
                var pri = $(this).val();
                if (pri.length <= 0) {
                    $(this).val(0);
                }
            });
            $(document).on('blur', 'input[name=txtBatch]', function () {
                var batchno = $(this).val();
                if (batchno.length > 0) {
                    $(this).closest('tr').find('input[name=txtDate]').datepicker({ dateFormat: 'dd/mm/yy' });
                }
            });

            $(document).on('focus', 'input[name=txtDate]', function () {
                var batchs = $(this).closest('tr').find('input[name=txtBatch]').val();
                if (batchs.length <= 0) {
                    alert('Enter Batch No.');
                    $(this).closest('tr').find('input[name=txtBatch]').focus();

                }
            });

            $(document).on('focus', 'input[name=txtPoqty]', function () {
                var date = $(this).closest('tr').find('input[name=txtDate]').val();
                if (date.length <= 0) {
                    alert('Enter MFG Date');
                    $(this).closest('tr').find('input[name=txtDate]').focus();
                }

            });


            $(document).on('keyup', 'input[name=txtPrice]', function () {
                calcAddi($(this));
            });

            $(document).on('keyup', 'input[name=txtGood]', function () {
                calcAddi($(this));
            });


            $(document).on('change', '.selTax', function () {
                calcTax($(this));
                rowCal();
            });

            calcTax = function (x) {
                var good = $(x).closest('table').closest('tr').find("input[name=txtGood]").val();
                var price = $(x).closest('table').closest('tr').find("input[name=txtPrice]").val();
                if (isNaN(good)) good = 0;
                if (isNaN(price)) price = 0;
                var tax = ((Number(good) * Number(price)) * $(x).val() / 100);

                $(x).closest('tr').find("input[name=taxVal]").val(tax.toFixed(2));

                var opt = $(x).find('option:selected').text();
                var tottax = 0;
                var idx = $(x).closest('tr').index();

                $(x).closest('table').find('.cssTax').each(function () {
                    if (idx != $(this).index()) {
                        if (opt == $(this).find('.selTax').find('option:selected').text()) {
                            $(x).closest('tr').find('.selTax').val(0);
                            alert('Already Select ' + opt + '');
                        }
                    }
                    var tax = $(this).find('.selTax').find('option:selected').attr('tax_val');
                    if (isNaN(tax)) tax = 0;
                    var TaxAmt = ((good * price) * tax) / 100;
                    tottax = Number(tottax) + Number(TaxAmt);
                    $(this).find("input[name=taxVal]").val(TaxAmt.toFixed(2));

                });
                var sum = good * price + Number(tottax);
                $(x).closest('table').closest('tr').find("label[name=netVal]").text(sum.toFixed(2));
            }

            calcAddi = function (x) {
                var t = parseFloat($(x).val());
                var tottax = 0;
                if (isNaN(t)) t = 0;
                Rw = $(x).closest('tr');
                var cTC = 0;

                var pv = parseFloat($(x).attr('pv'));
                if (isNaN(pv)) pv = 0;

                var pvv = parseFloat($(x).attr('pvv'));
                if (isNaN(pvv)) pvv = 0;


                var pvt = parseFloat($(x).attr('pvt'));
                if (isNaN(pvt)) pvt = 0;

                if ($(x).attr('name') == 'txtPrice') {
                    cTC = $(Rw).find("input[name=txtGood]").val();
                    if (isNaN(cTC)) cTC = 0;
                }
                if ($(x).attr('name') == 'txtGood') {
                    cTC = $(Rw).find("input[name=txtPrice]").val();
                    if (isNaN(cTC)) cTC = 0;
                }

                $(Rw).find('.cssTax').each(function () {
                    var tax = $(this).find('.selTax').find('option:selected').attr('tax_val');
                    if (isNaN(tax)) tax = 0;
                    var TaxAmt = ((t * cTC) * tax) / 100;
                    tottax = Number(tottax) + Number(TaxAmt);
                    $(this).find("input[name=taxVal]").val(TaxAmt.toFixed(2));
                });
                // console.log(tottax);
                $(Rw).find("label[name=grossVal]").text(t * cTC);
                var su = t * cTC;

                var sum = t * cTC + Number(tottax);
                $(Rw).find("label[name=netVal]").text(sum.toFixed(2));

                //total of goods

                var gntgtot = parseFloat($("#gnd_Good_tot").text());
                if (isNaN(gntgtot)) gntgtot = 0;
                gntgtot = (gntgtot - (pv * cTC)) + Number(su);
                $(x).attr('pv', t);
                $("#gnd_Good_tot").text(gntgtot);

                //                // total of netvalues
                //                var gntNtot = parseFloat($("#gnd_Net_tot").text());
                //                if (isNaN(gntNtot)) gntNtot = 0;
                //                gntNtot = (gntNtot - (pvv * cTC)) + Number(sum);
                //                $(x).attr('pvv', t);
                //                $("#gnd_Net_tot").text(gntNtot.toFixed(2));

                rowCal();
            }

            $(document).on('click', '.btnsave', function () {


                var grnDate = $('#grnDate').val();
                if (grnDate.length <= 0) { alert('Enter GRN Date!!'); $('#grnDate').focus(); return false; }

                var Supplier = $('#ddlsupplier').val();
                if (Supplier == 0) { alert('Select Supplier Name!!'); $('#ddlsupplier').focus(); return false; }

                var grnPono = $('#grnPono').val();
                if (grnPono.length <= 0) { alert('Enter PO No.!!'); $('#grnPono').focus(); return false; }

                var grnChallan = $('#challenNo').val();
                if (grnChallan.length <= 0) { alert('Enter PI No.!!'); $('#challenNo').focus(); return false; }

                var grnDisDt = $('#grnDisDate').val();
                if (grnDisDt.length <= 0) { alert('Enter Dispatch  Date!!'); $('#grnDisDate').focus(); return false; }


                var DllDistri = $('#ddldistributor').val();
                if (DllDistri == 0) { alert('Select Received Location!!'); $('#ddldistributor').focus(); return false; }




                var objMain = {};
                var mainArr = [];
                var proArr = [];

                mainArr.push({
                    GRN_No: $('#grnNo').val(),
                    GRN_Date: grnDate,
                    Entry_Date: $('#txtEdate').val(),
                    Supp_Code: Supplier,
                    Supp_Name: $('#ddlsupplier option:selected').text(),
                    Po_No: grnPono,
                    Challan_No: grnChallan,
                    Dispatch_Date: grnDisDt,
                    Received_Location: $('#ddldistributor').val(),
                    Received_Name: $('#ddldistributor option:selected').text(),
                    Received_By: $('#receivedby').val(),
                    Authorized_By: $('#authorised').val(),
                    remarks: $('#remarks').val(),
                    goodsTot: $('#gnd_Good_tot').text(),
                    taxTot: $('#gnd_Txt_tot').text(),
                    netTot: $('#gnd_Net_tot').text()
                });


                var countr = 0;
                $('#ProductTable >tbody >tr').each(function () {

                    if ($(this).find("input[name=txtPoqty]").val() > 0) {
                        countr++;
                    }


                    taxArr = [];
                    $(this).find('.cssTax').each(function () {
                        taxArr.push({
                            Tax_Code: $(this).find('.selTax').val(),
                            Tax_Name: $(this).find('.selTax').find('option:selected').text(),
                            Tax_Value: $(this).find("input[name=taxVal]").val()
                        });
                    });

                    var sgood = Number($(this).children('td').eq(7).find('input[name=txtGood]').attr('pv_good')) || 0;
                    var sdamaged = Number($(this).children('td').eq(8).find('input[name=txtDamaged]').attr('pv_damage')) || 0;

                    proArr.push({
                        PCode: $(this).children('td').eq(1).text(), //$(this).children('td').eq(1).find('input[name=Alw_Code]').val().toLowerCase().toString()                        
                        PDetails: $(this).children('td').eq(2).text(),
                        UOM: $(this).find('input[name=UOM_Code]').val(),
                        UOM_Name: $(this).children('td').eq(3).text(),
                        Batch_No: $(this).find('input[name=txtBatch]').val(),
                        mfgDate: $(this).find('input[name=txtDate]').val(),
                        Erp_Code: $(this).find('input[name=Erp_Code]').val(),
                        POQTY: $(this).find('input[name=txtPoqty]').val(),
                        Price: $(this).find('input[name=txtPrice]').val(),
                        Good: $(this).find('input[name=txtGood]').val(),
                        Damaged: $(this).find('input[name=txtDamaged]').val(),
                        Gross_Value: $(this).find('label[name=grossVal]').text(),
                        Net_Value: $(this).find('label[name=netVal]').text(),
                        SGood: sgood,
                        SDamaged: sdamaged,
                        taxDtls: taxArr
                    });

                });
                if (countr <= 0) {
                    alert('Enter Atleast One Row!!!');
                    $('#ProductTable').focus();
                    return false;
                }

                objMain['TransH'] = mainArr;
                objMain['TransD'] = proArr;

                //console.log(JSON.stringify(objMain));
                var saveType = "";
                if ($('#<%=hdnmode.ClientID %>').val() == "1") {
                    saveType = "1";
                }
                else {
                    saveType = "0";
                }
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Goods_Received_Note.aspx/SaveDate",
                    data: "{'data':'" + JSON.stringify(objMain) + "','SaveUpdate':'" + saveType + "'}",
                    dataType: "json",
                    success: function (data) {
                        if (data.d == "Success") {
                            alert("Update Successfully..!!");
                               var url = "Goods_Received_List.aspx";
                              window.location = url;
                        }
                        else {
                            alert(data.d);
                        }
                    },
                    error: function (data) {
                        alert(JSON.stringify(data));
                    }
                });


            });


            $('#ProductTable .textval').keypress(function (event) {
                return isNumber(event, this)
            });

            $('#ProductTable .numberOnly').keypress(function (event) {
                return isNumberOnly(event, this)
            });


            function isNumber(evt, element) {

                var charCode = (evt.which) ? evt.which : event.keyCode

                if (
            (charCode != 45 || $(element).val().indexOf('-') != -1) &&      // “-” CHECK MINUS, AND ONLY ONE.
            (charCode != 46 || $(element).val().indexOf('.') != -1) &&      // “.” CHECK DOT, AND ONLY ONE.
            (charCode < 48 || charCode > 57))
                    return false;

                return true;
            }

            function isNumberOnly(evt, element) {

                var charCode = (evt.which) ? evt.which : event.keyCode

                if (
            (charCode < 48 || charCode > 57))
                    return false;

                return true;
            }

            function loadddl(arr, tr) {
                var ddlCustomers;
                if (!tr) {
                    ddlCustomers = $("select[name=addTax]");
                } else {

                    ddlCustomers = $(tr).find("select[name=addTax]:last");
                }
                $(arr).each(function () {
                    var option = $("<option />");
                    //Set Customer Name in Text part.
                    option.html(this.alw_name);
                    //Set Customer CustomerId in Value pat.
                    option.val(this.Tax_Id);
                    option.attr('Tax_Val', this.Tax_Val);
                    //Add the Option element to DropDownList.
                    ddlCustomers.append(option);
                });
            }
            $(document).on('change', '#ddlsupplier', function () {
                $('#grnPono').val('');
                var ddlSubVal = $(this).val();
                if (ddlSubVal != 0) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "Goods_Received_Note.aspx/GetPONumber",
                        data: "{'suppCode':'" + ddlSubVal + "'}",
                        dataType: "json",
                        success: function (data) {
                            console.log(data.d);
                            var ddlCustomers = $("#PonoSelect");
                            ddlCustomers.empty().append('<option selected="selected" value="0">-Select-</option>');
                            for (var k = 0; k < data.d.length; k++) {
                                ddlCustomers.append($("<option></option>").val(data.d[k]).html(data.d[k]));
                            }

                        },
                        error: function (data) {
                            alert(JSON.stringify(data));
                        }
                    });
                }
            });
            $("#PonoSelect").change(function () {
                if ($("#PonoSelect option:selected").val() != 0) {
                    $("#grnPono").val($("#PonoSelect option:selected").val());
                    $("#grnPono").focus();
                }
                else {
                    $("#grnPono").val('');
                }
            });


            $(document).on('focus', '#grnPono', function () {
                var txt = $('#ddlsupplier').val();
                if (txt == 0) {
                    alert('Select Supplier Name');
                    $('#ddlsupplier').focus();
                    return false;
                }
            });
            $(document).on('blur', '#grnPono', function () {
                var txt = $(this).val();
                if (txt.length > 0) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "Goods_Received_Note.aspx/GetPOQTY",
                        data: "{'data':'" + txt + "'}",
                        dataType: "json",
                        success: function (data) {
                            // console.log(data.d);
                            $('#ProductTable > tbody > tr').each(function () {
                                for (var i = 0; i < data.d.length; i++) {
                                    if ($(this).find("td").eq(1).text() == data.d[i].Product_Code) {
                                        // console.log(data.d[i].PQty);
                                        $(this).find('input[name=txtPoqty]').val(data.d[i].CQty);
                                        $(this).find('input[name=txtPoqty]').attr('orpoqty', data.d[i].CQty);

                                        $(this).find('input[name=txtGood]').val(data.d[i].CQty);
                                        $(this).find('input[name=txtGood]').attr('orpoqty', data.d[i].CQty);
                                        
                                    }
                                }
                            });
                        },
                        error: function (data) {
                            alert(JSON.stringify(data));
                        }
                    });
                }
            });
        });

        function clears() {
            $('#grnDate').val('');
            $('#txtEdate').val('');
            $('#ddlsupplier').val(0);
            $('#grnPono').val('');
            $('#challenNo').val('');
            $('#grnDisDate').val('');
            $('#ddldistributor').val(0);
            $('#receivedby').val('');
            $('#receivedby').val('');
            $('#remarks').val('');
            $('#gnd_Good_tot').text('0.00');
            $('#gnd_Txt_tot').text('0.00');
            $('#gnd_Net_tot').text('0.00');
        }
    </script>
    <form id="goodsfrm" runat="server">
    <div class="container" style="width: 100%; max-width: 100%;">
        <asp:HiddenField ID="hdnmode" runat="server" />
        <asp:HiddenField ID="hdngrn_no" runat="server" />
        <asp:HiddenField ID="hdngrn_date" runat="server" />
        <asp:HiddenField ID="hdnsupp_code" runat="server" />
        <div class="row">
            <div class="form-group">
                <label for="grnno" id="lblgrnno" class="col-sm-2 col-sm-offset-1  control-label"
                    style="display: none; font-weight: normal">
                    GRN No.</label>
                <div class="col-sm-3">
                    <input type="text" name="grnno" id="grnNo" class="form-control" value="0" style="display: none;" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="form-group">
                <label for="grnDate" class="col-sm-2 col-sm-offset-1  control-label" style="font-weight: normal">
                    GRN Date</label>
                <div class="col-sm-3">
                    <input type="text" name="grnDate" id="grnDate" autocomplete="off" class="form-control datetimepicker" />
                </div>
                <label for="txtEdate" class="col-sm-2 control-label" style="font-weight: normal">
                    Entry Date</label>
                <div class="col-sm-3">
                    <input type="text" name="txtEdate" id="txtEdate" autocomplete="off" class="form-control" readonly />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="form-group">
                <label for="ddlsupplier" class="col-sm-2 col-sm-offset-1  control-label" style="font-weight: normal">
                    Supplier</label>
                <div class="col-sm-3 ">
                    <select name="ddlsupplier" id="ddlsupplier" style="width: 100%">
                    </select>
                </div>
                <label for="grnPono" class="col-sm-2 control-label" style="font-weight: normal">
                    PO No.</label>
                <div class="col-sm-3">
                    <select id="PonoSelect" style="width: 200px; float: left; height: 32px">
                        </select>
                        <input type="text" name="grnpono" id="grnPono" autocomplete="off" style="width: 180px; margin-left: -199px; margin-top: 1px; border: none; float: left;" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="form-group">
                <label for="challenNo" class="col-sm-2 col-sm-offset-1 control-label" style="font-weight: normal">
                    PI No.</label>
                <div class="col-sm-3 ">
                    <input type="text" name="challenNo" autocomplete="off" id="challenNo" class="form-control" />
                </div>
                <label for="grnDisDate" class="col-sm-2 control-label" style="font-weight: normal">
                    Dispatch Date</label>
                <div class="col-sm-3 ">
                    <input type="text" name="grnDisDate" autocomplete="off" id="grnDisDate" class="form-control datetimepicker" />
                </div>
            </div>
        </div>
    </div>
    <br />
    <div class="container" style="margin-left: 0px">
        <table id="ProductTable" class="gvHeader">
            <thead>
                <tr>
                    <th style="width: 40px">
                        SlNo.
                    </th>
                    <th style="width: 200px">
                        PCode
                    </th>
                    <th style="width: 350px">
                        Product Name
                    </th>
                    <th>
                        UOM
                    </th>
                    <th style="width: 150px">
                        Batch No.
                    </th>
                    <th style="width: 150px">
                        MFG Date.
                    </th>
                    <th style="width: 150px">
                        PO QTY
                    </th>
                    <th style="width: 150px">
                        Price
                    </th>
                    <th style="width: 150px">
                        Good
                    </th>
                    <th style="width: 150px">
                        Damaged
                    </th>
                    <th style="width: 150px">
                        Gross Value
                    </th>
                    <th style="width: 150px">
                        TAX
                    </th>
                    <th style="width: 150px">
                        Net Value
                    </th>
                </tr>
            </thead>
            <tbody>
            </tbody>
            <tfoot>
                <tr>
                    <th colspan="9">
                        Total
                    </th>
                    <th style="text-align: right">
                        <label name="gnd_Good_tot" id="gnd_Good_tot">
                            0.00
                        </label>
                    </th>
                    <th style="text-align: right">
                        <label name="gnd_Txt_tot" id="gnd_Txt_tot">
                            0.00
                        </label>
                    </th>
                    <th style="text-align: right">
                        <label name="gnd_Net_tot" id="gnd_Net_tot">
                            0.00
                        </label>
                    </th>
                </tr>
            </tfoot>
        </table>
    </div>
    <br />
    <div class="container" style="width: 100%; max-width: 100%;">
        <div class="row">
            <label for="remarks" class="col-md-1 control-label" style="font-weight: normal">
                Remarks
            </label>
            <div class="col-md-5 inputGroupContainer">
                <textarea rows="6" cols="150" style="width: 100%; resize: none;" id="remarks"></textarea>
            </div>
            <div class="col-md-6">
                <div class="row">
                    <label for="ddldistributor" class="col-md-3 control-label" style="font-weight: normal">
                        Received Location</label>
                    <div class="col-md-9 inputGroupContainer">
                        <select class="selectpicker" name="ddldistributor" id="ddldistributor" style="width: 100%">
                        </select>
                    </div>
                </div>
                <div class="row">
                    <label for="receivedby" class="col-md-3 control-label" style="font-weight: normal">
                        Received By</label>
                    <div class="col-md-9 inputGroupContainer">
                        <input type="text" name="receivedby" id="receivedby" class="form-control" />
                    </div>
                </div>
                <div class="row">
                    <label for="authorised" class="col-md-3 control-label" style="font-weight: normal">
                        Authorised By</label>
                    <div class="col-md-9 inputGroupContainer">
                        <input type="text" name="authorised" id="authorised" class="form-control" />
                    </div>
                </div>
            </div>
        </div>
        <div class="row" style="text-align: center">
            <div class="col-md-12 inputGroupContainer">
                <a id="btndaysave" class="btn btn-primary btnsave" style="vertical-align: middle;
                    font-size: 17px;"><span>Save</span></a></div>
        </div>
    </div>
    </form>
</asp:Content>
