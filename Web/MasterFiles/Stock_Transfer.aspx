<%@ Page Title="Stock Transfer" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="Stock_Transfer.aspx.cs" Inherits="MasterFiles_Stock_Transfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        input[type='text'], select, label
        {
            line-height: 22px;
            padding: 4px 6px;
            font-size: medium;
            border-radius: 7px;
            width: 100%;
        }
        #stockTable input[name='txtQty']
        {
            text-align: right;
        }
        .row
        {
            padding: 2px 2px;
        }
        #stockTable
        {
            font-family: "Trebuchet MS" , Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }
        
        #stockTable td, #stockTable th
        {
            border: 1px solid #ddd;
            padding: 2px 4px;
        }
        
        #stockTable td
        {
            vertical-align: middle;
        }
        
        #stockTable tr:nth-child(even)
        {
            background-color: #f2f2f2;
        }
        
        #stockTable tr:hover
        {
            background-color: #ddd;
        }
        
        #stockTable th
        {
            padding-top: 12px;
            padding-bottom: 12px;
            padding-left: 5px;
            padding-right: 5px;
            text-align: center;
            background-color: #475677;
            color: white;
        }
        
        #stockTable td:nth-child(10), #stockTable td:nth-child(12)
        {
            text-align: right;
        }
        .control-label
        {
            font-weight: normal;
        }
        body
        {
            overflow-x: initial !important;
        }
    </style>
    <link rel="stylesheet" type="text/css" media="all" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var StkDetail = [];
            $('.datetimepicker').datepicker({ dateFormat: 'dd/mm/yy' });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Stock_Transfer.aspx/GetDistributor",
                dataType: "json",
                success: function (data) {
                    console.log(data.d);
                    var ddlCustomers = $("#stockistFrom");
                    var ddlCustomersTo = $("#stockistTo");

                    ddlCustomers.empty().append('<option selected="selected" value="0">--- Select ---</option>');
                    ddlCustomersTo.empty().append('<option selected="selected" value="0">--- Select ---</option>');
                    $.each(data.d, function () {
                        if (this['wType'] == 'Warehouse') {
                            ddlCustomers.append($("<option></option>").val(this['disCode']).html(this['disName']));
                        }
                        else {
                            ddlCustomersTo.append($("<option></option>").val(this['disCode']).html(this['disName']));
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
                async: false,
                url: "Stock_Transfer.aspx/GetProduct",
                dataType: "json",
                success: function (data) {
                    var str = "";
                    for (var i = 0; i < data.d.length; i++) {
                        str = "<td>" + (i + 1) + "</td><td> <input type='hidden'name='pCode' value='" + data.d[i].pCode + "'/>" + data.d[i].pName + "</td><td><select class='ptype' name='selType' id='selType'><option value='0'>--Select--</option><option value='Good'>Good</option><option value='Damage'>Damage</option></select><input type='hidden' name='stkval' stkgood='0' stkdamage='0' /></td><td>0</td><td><input type='text' name='txtQty' id='txtQty' maxlength='10' style='min-width: 115px;' /></td><td><input type='text' name='txtReason' /></td>";
                        $('#stockTable >tbody').append('<tr>' + str + ' </tr>');
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
                url: "Stock_Transfer.aspx/GetCurrStock",
                dataType: "json",
                success: function (data) {
                    StkDetail = data.d;

                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
            $(document).on('change', '#stockistFrom', function () {
                loadval();
            });


            function loadval() {
                dtd = StkDetail.filter(function (a) {
                    return (a.Dist_Code == $("#stockistFrom").val());
                });

                $('#stockTable > tbody > tr').each(function () {
                    if (dtd.length > 0) {
                        for (var i = 0; i < dtd.length; i++) {
                            if ($(this).find("input[name=pCode]").val() == dtd[i].Prod_Code) {
                                console.log(dtd[i].GStock);
                                console.log(dtd[i].DStock);
                                $(this).find("input[name=stkval]").attr('stkgood', dtd[i].GStock);
                                $(this).find("input[name=stkval]").attr('stkdamage', dtd[i].DStock);
                            }
                        }
                    }
                    else {
                        $(this).find("input[name=stkval]").attr('stkgood', "0");
                        $(this).find("input[name=stkval]").attr('stkdamage', "0");

                    }
                });
            }

            $(document).on('change', '.ptype', function () {

                if ($(this).val() != "0") {
                    if ($(this).val() == "Good") {
                        $(this).closest('tr').find('td:eq(3)').text($(this).closest('tr').find('input[name=stkval]').attr('stkgood'));
                    }
                    else if ($(this).val() == "Damage") {
                        $(this).closest('tr').find('td:eq(3)').text($(this).closest('tr').find('input[name=stkval]').attr('stkdamage'));
                    }
                }
                else {
                    $(this).closest('tr').find('td:eq(3)').text('0');

                }

            });


            $(document).on('focus', '.ptype', function () {
                console.log($("#stockistFrom").val());
                if (Number($("#stockistFrom").val()) <= 0) {
                    alert('Select From..!');
                    $("#stockistFrom").focus();
                    return false;
                }


            });


            $('#stockTable input[name=txtQty]').focus(function (event) {
                if ($(this).closest('tr').find('.ptype').val() == "0") {
                    alert('Select Type');
                    $(this).closest('tr').find('.ptype').focus();
                    return false;
                }
            })

            $('#stockTable input[name=txtQty]').blur(function (event) {
                if (Number($(this).closest('tr').find('td:eq(3)').text()) < Number($(this).val())) {
                    alert('Value Must Below Stock Value : ' + $(this).closest('tr').find('td:eq(3)').text());
                    $(this).val("0")
                    $(this).focus();
                    return false;
                }
            })


            if ($('#<%=hdnmode.ClientID %>').val() == "1") {
                $('#lbltransNo').css('display', 'inline-block');
                $('#transNo').css('display', 'inline-block');
                $('#transNo').attr('disabled', true);
                $('#stockistFrom').attr('disabled', true);
                $('#stockistTo').attr('disabled', true);

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Stock_Transfer.aspx/Get_AllValues",
                    data: "{'TransSlNo':'" + $('#<%=hdntransno.ClientID %>').val() + "'}",
                    dataType: "json",
                    success: function (data) {
                        console.log(JSON.parse(data.d));

                        var obj = JSON.parse(data.d);
                        if (obj.TransH.length > 0) {
                            $('#transNo').val(obj.TransH[0].transNo);
                            $('#transDate').val(obj.TransH[0].transDate);
                            $('#stockistFrom').val(obj.TransH[0].stockistFrom);
                            $('#stockistTo').val(obj.TransH[0].stockistTo);
                        }
                        loadval();
                        $('#stockTable > tbody > tr').each(function () {
                            for (var i = 0; i < obj.TransD.length; i++) {
                                if ($(this).find("input[name=pCode]").val() == obj.TransD[i].pCode) {
                                    $(this).find(".ptype").val(obj.TransD[i].pType);
                                    $(this).find(".ptype").attr('disabled', true);
                                    $(this).find("input[name=txtQty]").val(obj.TransD[i].pqty);
                                    $(this).find("input[name=txtQty]").attr('pv_Val', obj.TransD[i].pqty);

                                    $(this).find("input[name=txtReason]").val(obj.TransD[i].preason);
                                    if (obj.TransD[i].pType == "Good") {
                                        $(this).closest('tr').find('input[name=stkval]').attr('stkgood', Number($(this).closest('tr').find('input[name=stkval]').attr('stkgood')) + Number(obj.TransD[i].pqty));
                                        $(this).closest('tr').find('td:eq(3)').text($(this).closest('tr').find('input[name=stkval]').attr('stkgood'));
                                    }
                                    else if (obj.TransD[i].pType == "Damage") {
                                        console.log($(this).closest('tr').find('input[name=stkval]').attr('stkdamage'));
                                        console.log(obj.TransD[i].pqty);
                                        var tot = Number($(this).closest('tr').find('input[name=stkval]').attr('stkdamage')) + Number(obj.TransD[i].pqty);
                                        $(this).closest('tr').find('input[name=stkval]').attr('stkdamage', tot);
                                        $(this).closest('tr').find('td:eq(3)').text($(this).closest('tr').find('input[name=stkval]').attr('stkdamage'));
                                    }

                                }
                            }
                        });

                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });


            }





            var objMain = {};
            $(document).on('click', '#btnSave', function () {

                var transNo = $('#transNo').val();
                //if (transNo.length <= 0) { alert('Enter Transfer  No.!!'); $('#transNo').focus(); return false; }

                var transDate = $('#transDate').val();
                if (transDate.length <= 0) { alert('Enter Transfer  Date!!'); $('#transDate').focus(); return false; }

                var stockistFrom = $('#stockistFrom').val();
                if (stockistFrom == 0) { alert('Select Warehouse Name!!'); $('#stockistFrom').focus(); return false; }

                var stockistTo = $('#stockistTo').val();
                if (stockistTo == 0) { alert('Select Distributor Name!!'); $('#stockistTo').focus(); return false; }


                var HeadArr = [];

                HeadArr.push({
                    transNo: transNo,
                    transDate: transDate,
                    stockistFrom: stockistFrom,
                    stockistFrom_Nm: $('#stockistFrom option:selected').text(),
                    stockistTo: stockistTo,
                    stockistTo_Nm: $('#stockistTo option:selected').text()
                });


                var ch = false;
                $('#stockTable >tbody >tr').each(function () {
                    console.log('enter');
                    if ($(this).find("input[name=txtQty]").val() > 0) {
                        if ($(this).find('.ptype').val() == 0) {
                            alert('Select Type');
                            $(this).focus();
                            ch = true;
                            return false;
                        }
                    }
                    if ($(this).find('.ptype').val() != 0) {
                        if ($(this).find("input[name=txtQty]").val() <= 0 || $(this).find("input[name=txtQty]").val() == "") {
                            alert('Enter Qty');
                            $(this).focus();
                            ch = true;
                            return false;
                        }
                    }
                });


                if (ch) {
                    ch = false;
                    return false;
                }


                var countr = 0;

                var productArr = [];
                $('#stockTable >tbody >tr').each(function () {
                    if ($(this).find("input[name=txtQty]").val() > 0) {
                        countr++;
                    }
                    var oqty = $(this).find("input[name=txtQty]").attr('pv_Val') || 0;

                    productArr.push({
                        pCode: $(this).find("input[name=pCode]").val(),
                        pName: $(this).find('td').eq(1).text(),
                        pType: $(this).find('.ptype').val(),
                        pType_Name: $(this).find('.ptype').find('option:selected').text(),
                        pqty: $(this).find("input[name=txtQty]").val(),
                        oqty: oqty,
                        preason: $(this).find("input[name=txtReason]").val()
                    });
                });


                if (countr <= 0) {
                    alert('Enter Atleast One Row!!!');
                    $('#stockTable').focus();
                    return false;
                }

                objMain['TransH'] = HeadArr;
                objMain['TransD'] = productArr;
                console.log(objMain);
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Stock_Transfer.aspx/SaveDate",
                    data: "{'data':'" + JSON.stringify(objMain) + "'}",
                    dataType: "json",
                    success: function (data) {
                        alert(data.d);
                        //  alert("Week Master has been updated successfully!!!");  

                        var url = "Stock_Transfer_List.aspx";
                        window.location = url;
                    },
                    error: function (data) {
                        alert(JSON.stringify(data));
                    }
                });
            });

            $('#stockTable input[name=txtQty').keypress(function (event) {
                return isNumber(event, this)
            });

            $('#transNo').keypress(function (event) {
                return isNumber(event, this)
            });
            function isNumber(evt, element) {

                var charCode = (evt.which) ? evt.which : event.keyCode
                if ((charCode < 48 || charCode > 57))
                    return false;
                return true;
            }
        });
    </script>
    <form id="stockfrm" runat="server">
    <asp:HiddenField ID="hdnmode" runat="server" />
    <asp:HiddenField ID="hdntransno" runat="server" />
    <div class="container" style="max-width: 100%; width: 100%;">
        <div class="row">
            <div class="form-horizontal">
                <div class="form-group" style="text-align: left">
                    <label for="transNo" id="lbltransNo" class="control-label col-sm-1 " style="text-align: left;
                        display: none;">
                        Transfer No.</label>
                    <div class="col-sm-2 inputGroupContainer">
                        <div class="input-group">
                            <input type="text" id="transNo" style="display: none;" class="form-control" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label for="transDate" class="control-label col-sm-1" style="text-align: left">
                        Date</label>
                    <div class="col-sm-2 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                            <input type="text" id="transDate" class="form-control datetimepicker" />
                        </div>
                    </div>
                    <label for="stockistFrom" class="control-label col-sm-1 " style="text-align: left">
                        From</label>
                    <div class="col-sm-3 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                            <select id="stockistFrom" class="form-control">
                            </select>
                        </div>
                    </div>
                    <label for="stockistTo" class="control-label col-sm-1" style="text-align: left">
                        To</label>
                    <div class="col-sm-3 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                            <select id="stockistTo" class="form-control">
                            </select>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div>
                <table id="stockTable" class="table table-responsive">
                    <thead>
                        <tr>
                            <th style="width: 50px">
                                Sl.No.
                            </th>
                            <th>
                                Product Name
                            </th>
                            <th style="width: 150px">
                                Stock Type
                            </th>
                            <th>
                                Stock
                            </th>
                            <th style="width: 150px">
                                QTY
                            </th>
                            <th style="width: 250px">
                                Reason
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
        <br />
        <div class="form-group">
            <div class="col-sm-12" style="text-align: center">
                <a id="btnSave" class="btn btn-primary" style="vertical-align: middle; font-size: 17px;">
                    <span>Save</span></a></div>
        </div>
    </div>
    </form>
</asp:Content>
