<%@ Page Title="Stock Adjustment Entry" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master"
    CodeFile="Stock_Adj_Entry.aspx.cs" Inherits="MasterFiles_Stock_Adj_Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!doctype html public "-//w3c//dtd xhtml 1.0 transitional//en" "http://www.w3.org/tr/xhtml1/dtd/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title>Stock Adjustment Entry</title>
        <%--<link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
        <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
        <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <link rel="stylesheet" type="text/css" media="all" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/smoothness/jquery-ui.css" />
        <script type="text/javascript">
            function ShowProgress() {
                setTimeout(function () {
                    var modal = $('<div />');
                    modal.addClass("modal");
                    $('body').append(modal);
                    var loading = $(".loading");
                    loading.show();
                    var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                    var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                    loading.css({ top: top, left: left });
                }, 200);
            }
            $('form').live("submit", function () {
                ShowProgress();
            });
        </script>
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
        <script type="text/javascript">
            $(document).ready(function () {
                $('.datetimepicker').datepicker({ dateFormat: 'dd/mm/yy' });
                $('input:text:first').focus();
                $('input:text').bind("keydown", function (e) {
                    var n = $("input:text").length;
                    if (e.which == 13) { //Enter key
                        e.preventDefault(); //to skip default behavior of the enter key
                        var curIndex = $('input:text').index(this);

                        if ($('input:text')[curIndex].value == '') {
                            $('input:text')[curIndex].focus();
                        }
                        else {
                            var nextIndex = $('input:text').index(this) + 1;

                            if (nextIndex < n) {
                                e.preventDefault();
                                $('input:text')[nextIndex].focus();
                            }
                            else {
                                $('input:text')[nextIndex - 1].blur();
                                $('#btnSubmit').focus();
                            }
                        }
                    }
                });
                $("input:text").on("keypress", function (e) {
                    if (e.which === 32 && !this.value.length)
                        e.preventDefault();
                });
                $('#btnSubmit').click(function () {
                    if ($("#txtEffFrom").val() == "") { alert("Please Enter Effective From Date."); $('#txtEffFrom').focus(); return false; }
                });

                $(document).on('keyup', '[id*=txtQty]', function () {
                    if (!jQuery.trim($(this).val()) == '') {
                        if (!isNaN(parseFloat($(this).val()))) {
                            var row = $(this).closest("tr");

                            if (row.find('[id*=txtQty]').val() > Number(row.find('td').eq(3).text())) {

                                alert('Qty Value High on Stock Value')
                                row.find('[id*=txtQty]').val(0);
                            }
                            else {
                            }
                        }
                    } else {
                        $(this).val('');
                    }
                });

                $(document).on('focus', '.ptype', function () {
                    console.log($('#<%=Stk_Loc.ClientID %>').val());
                    if (Number($('#<%=Stk_Loc.ClientID %>').val()) <= 0) {
                        alert('Select Stock Location..!');
                        $('#<%=Stk_Loc.ClientID %>').focus();
                        return false;
                    }


                });


                $(document).on('change', '.pbtype', function () {
                    var provinceId = $(this).val();
                    if (provinceId == "RETBATCH") {
                        $(this).closest('tr').find('.pbtype1').val(0);
                        $(this).closest('tr').find('.pbtype1').attr('disabled', false);
                        x = $('.ptype').val();

                        $(this).closest('tr').find('.ptype1').find('option').each(function () {


                            if (x == $(this).val()) {
                                $(this).attr('disabled', false);

                            }
                            else {
                                $(this).attr('disabled', false);
                            }
                        });
                        $(this).closest('tr').find('.ptype').find('option').each(function () {


                            if (x == $(this).val()) {
                                $(this).attr('disabled', false);

                            }
                            else {
                                $(this).attr('disabled', false);
                            }
                        });

                    }
                    else {
                        if (provinceId != 0) {
                            $(this).closest('tr').find('.pbtype1').val(provinceId);
                            $(this).closest('tr').find('.pbtype1').attr('disabled', true);
                        }
                        else {
                            $(this).closest('tr').find('.pbtype1').val(0);
                            $(this).closest('tr').find('.pbtype1').attr('disabled', false);
                        }
                    }
                });

            }); 

                $(document).on('change', '.ptype', function () {

                    x = $(this).val();

                    $(this).closest('tr').find('.ptype1').find('option').each(function () {

                    if ($(this).closest('tr').find('.pbtype').val() == "RETBATCH") {

                        if (x == $(this).val()) {
                            $(this).attr('disabled', false);

                        }
                        else {
                            $(this).attr('disabled', false);
                        }
                     
                    }
                    else
                    {

                  


                        if (x == $(this).val()) {
                            $(this).attr('disabled', true);

                        }
                        else {
                            $(this).attr('disabled', false);
                        }
                        }




                    });
                });

                $(document).on('change', '.ptype1', function () {
                    x = $(this).val();

                    $(this).closest('tr').find('.ptype').find('option').each(function () {


                        if ($(this).closest('tr').find('.pbtype').val() == "RETBATCH") {

                            if (x == $(this).val()) {
                                $(this).attr('disabled', false);

                            }
                            else {
                                $(this).attr('disabled', false);
                            }

                        }
                        else {




                            if (x == $(this).val()) {
                                $(this).attr('disabled', true);

                            }
                            else {
                                $(this).attr('disabled', false);
                            }
                        }




                    });
                });

               
        </script>
        <script language="javascript" type="text/javascript">
            $(document).ready(function () {
                var StkDetail = [];
                var BatchDtl = [];
                //For navigating using left and right arrow of the keyboard
                $("input[type='text'], select").keydown(
function (event) {
    if ((event.keyCode == 39) || (event.keyCode == 9 && event.shiftKey == false)) {
        var inputs = $(this).parents("form").eq(0).find("input[type='text'], select");
        var idx = inputs.index(this);
        if (idx == inputs.length - 1) {
            inputs[0].select()
        } else {
            $(this).parents("table").eq(0).find("tr").not(':first').each(function () {
                $(this).attr("style", "BACKGROUND-COLOR: white; ");
            });

            inputs[idx + 1].focus();
        }
        return false;
    }
    if ((event.keyCode == 37) || (event.keyCode == 9 && event.shiftKey == true)) {
        var inputs = $(this).parents("form").eq(0).find("input[type='text'], select");
        var idx = inputs.index(this);
        if (idx > 0) {
            $(this).parents("table").eq(0).find("tr").not(':first').each(function () {
                $(this).attr("style", "BACKGROUND-COLOR: white; ");
            });


            inputs[idx - 1].focus();
        }
        return false;
    }
});
                //For navigating using up and down arrow of the keyboard
                $("input[type='text'], select").keydown(
function (event) {
    if ((event.keyCode == 40)) {
        if ($(this).parents("tr").next() != null) {
            var nextTr = $(this).parents("tr").next();
            var inputs = $(this).parents("tr").eq(0).find("input[type='text'], select");
            var idx = inputs.index(this);
            nextTrinputs = nextTr.find("input[type='text'], select");
            if (nextTrinputs[idx] != null) {
                $(this).parents("table").eq(0).find("tr").not(':first').each(function () {
                    $(this).attr("style", "BACKGROUND-COLOR: white; ");
                });

                nextTrinputs[idx].focus();
            }
        }
        else {
            $(this).focus();
        }
    }
    if ((event.keyCode == 38)) {
        if ($(this).parents("tr").next() != null) {
            var nextTr = $(this).parents("tr").prev();
            var inputs = $(this).parents("tr").eq(0).find("input[type='text'], select");
            var idx = inputs.index(this);
            nextTrinputs = nextTr.find("input[type='text'], select");
            if (nextTrinputs[idx] != null) {
                $(this).parents("table").eq(0).find("tr").not(':first').each(function () {
                    $(this).attr("style", "BACKGROUND-COLOR: white;");
                });

                nextTrinputs[idx].focus();
            }
            return false;
        }
        else {
            $(this).focus();
        }
    }
});

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Stock_Adj_Entry.aspx/GetCurrbat",
                    dataType: "json",
                    success: function (data) {
                        BatchDtl = data.d;
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Stock_Adj_Entry.aspx/GetProduct",
                    dataType: "json",
                    success: function (data) {

                        var str = "";
                        for (var i = 0; i < data.d.length; i++) {
                            str = "<td>" + (i + 1) + "</td><td> <input type='hidden'name='pCode' value='" + data.d[i].pCode + "'/>" + data.d[i].pName + "</td><td><select class='ptype' name='selType' id='selType'><option value='0'>--Select--</option><option value='Good'>Good</option><option value='Damage'>Damage</option></select><input type='hidden' name='stkval' stkgood='0' stkdamage='0'/></td><td><select class='pbtype' name='selbType' id='selbType'><option value='0'>--Select--</option></select></td><td>" + 0 + "</td></label></td><td><select class='ptype1' name='selType1' id='selType1'><option value='0'>--Select--</option><option value='Good'>Good</option><option value='Damage'>Damage</option></select></td><td><select class='pbtype1' name='selbType1' id='selbType1'><option value='0'>--Select--</option></select></td><td><input type='text' name='txtQty' id='txtQty' /></td><td><input type='text' name='txtReason' /></td>";
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
                    url: "Stock_Adj_Entry.aspx/GetSno",
                    dataType: "json",
                    success: function (data) {

                        var str = "";
                        for (var i = 0; i < data.d.length; i++) {
                            $('#Txt_Adj_no').val(data.d[i].pUOM_Name);
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
                    url: "Stock_Adj_Entry.aspx/GetCurrStock",
                    dataType: "json",
                    success: function (data) {
                        StkDetail = data.d;
                        //                        $('#stockTable  >tbody > tr').each(function () {

                        //                            for (var i = 0; i < data.d.length; i++) {
                        //                                console.log($(this).find("input[name=pCode]").val() + ":" + data.d[i].Prod_Code);
                        //                                if ($(this).find("input[name=pCode]").val() == data.d[i].Prod_Code) {
                        //                                    $(this).find("input[name=stkval]").attr('stkgood', data.d[i].GStock);
                        //                                    $(this).find("input[name=stkval]").attr('stkdamage', data.d[i].DStock);
                        //                                }
                        //                            }
                        //                        });
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });

                var BDT = BatchDtl.filter(function (a) {
                    return (a.Dist_Code == $('#<%=Stk_Loc.ClientID %>').val());
                });

                $('#stockTable > tbody > tr').each(function () {

                    var pcodes = $(this).find("input[name=pCode]").val();
                    var PT = BDT.filter(function (a) {
                        return (a.Prod_Code == pcodes);
                    });
                    console.log(PT);
                    if (PT.length > 0) {
                        if ($(this).find("input[name=pCode]").val() == PT[0].Prod_Code) {
                            var ddlCustomers = $(this).find('.pbtype');
                            var ddlCustomers1 = $(this).find('.pbtype1');
                            for (var i = 0; i < PT.length; i++) {
                                ddlCustomers.append($("<option></option>").val(PT[i].Batch).html(PT[i].Batch));
                                ddlCustomers1.append($("<option></option>").val(PT[i].Batch).html(PT[i].Batch));
                            }
                        }
                    }
                });

                if ($('#<%=hdnmode.ClientID %>').val() == "1") {

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Stock_Adj_Entry.aspx/GetCurrbat",
                        dataType: "json",
                        success: function (data) {
                            BatchDtl = data.d;
                        },
                        error: function (result) {
                            alert(JSON.stringify(result));
                        }
                    });
                    $('#Txt_Adj_no').attr('disabled', true);
                    $('#transDate').attr('disabled', true);
                    $('#Stk_Loc').prop('disabled', true);
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Stock_Adj_Entry.aspx/Get_AllValues",
                        data: "{'TransSlNo':'" + $('#<%=hdntransno.ClientID %>').val() + "'}",
                        dataType: "json",
                        success: function (data) {
                            console.log(JSON.parse(data.d));

                            var obj = JSON.parse(data.d);
                            if (obj.TransH.length > 0) {
                                $('#Txt_Adj_no').val(obj.TransH[0].transNo);
                                $('#transDate').val(obj.TransH[0].transDate);
                                $('#Stk_Loc').val(obj.TransH[0].stkloc);
                                $('#authorised').val(obj.TransH[0].Auth_Nm);
                            }

                            $('#stockTable > tbody > tr').each(function () {

                                //                                for (var i = 0; i < obj.TransD.length; i++) {
                                //                                    if ($(this).find("input[name=pCode]").val() == obj.TransD[i].pCode) {
                                //                                        $(this).find(".ptype").val(obj.TransD[i].pType);
                                //                                        $(this).find(".ptype1").val(obj.TransD[i].pType1);
                                //                                        $(this).find("input[name=txtQty]").val(obj.TransD[i].pqty);
                                //                                        $(this).find("input[name=txtReason]").val(obj.TransD[i].preason);

                                for (var i = 0; i < obj.TransD.length; i++) {
                                    if ($(this).find("input[name=pCode]").val() == obj.TransD[i].pCode) {
                                        $(this).find(".ptype").val(obj.TransD[i].pType);
                                        $(this).find('.ptype').attr('disabled', true);
                                        $(this).find(".ptype1").val(obj.TransD[i].pType);
                                        $(this).find('.ptype1').attr('disabled', true);
                                        $(this).find(".ptype").val(obj.TransD[i].pType);
                                        $(this).find(".ptype1").val(obj.TransD[i].pType1);
                                        $(this).find(".pbtype").val(obj.TransD[i].pbtype);
                                        $(this).find(".pbtype1").val(obj.TransD[i].pbtype1);
                                        $(this).find("input[name=txtQty]").attr('pv_Val', obj.TransD[i].pqty);
                                        // $(this).find(".pbtype").val(obj.TransD[i].pType1);
                                        //$(this).find("input[name=txtRate]").val(obj.TransD[i].prate);
                                        $(this).find("input[name=txtQty]").val(obj.TransD[i].pqty);
                                        $(this).find("input[name=txtReason]").val(obj.TransD[i].preason);
                                        if (obj.TransD[i].pType == "Good") {
                                            $(this).closest('tr').find('input[name=stkval]').attr('stkgood', Number($(this).closest('tr').find('input[name=stkval]').attr('stkgood')) + Number(obj.TransD[i].pqty));
                                            $(this).closest('tr').find('td:eq(4)').text($(this).closest('tr').find('input[name=stkval]').attr('stkgood'));
                                        }
                                        else if (obj.TransD[i].pType == "Damage") {
                                            console.log($(this).closest('tr').find('input[name=stkval]').attr('stkdamage'));
                                            console.log(obj.TransD[i].pqty);
                                            var tot = Number($(this).closest('tr').find('input[name=stkval]').attr('stkdamage')) + Number(obj.TransD[i].pqty);
                                            $(this).closest('tr').find('input[name=stkval]').attr('stkdamage', tot);
                                            $(this).closest('tr').find('td:eq(4)').text($(this).closest('tr').find('input[name=stkval]').attr('stkdamage'));
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

                $('#<%=Stk_Loc.ClientID %>').change(function () {

                    dtd = StkDetail.filter(function (a) {
                        return (a.Dist_Code == $('#<%=Stk_Loc.ClientID %>').val());
                    });



                    $('#stockTable > tbody > tr').each(function () {
                        if (dtd.length > 0) {
                            for (var i = 0; i < dtd.length; i++) {
                                if ($(this).find("input[name=pCode]").val() == dtd[i].Prod_Code) {
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



                    var BDT = BatchDtl.filter(function (a) {
                        return (a.Dist_Code == $('#<%=Stk_Loc.ClientID %>').val());
                    });

                    $('#stockTable > tbody > tr').each(function () {

                        var pcodes = $(this).find("input[name=pCode]").val();
                        var PT = BDT.filter(function (a) {
                            return (a.Prod_Code == pcodes);
                        });
                        console.log(PT);
                        if (PT.length > 0) {
                            if ($(this).find("input[name=pCode]").val() == PT[0].Prod_Code) {
                                var ddlCustomers = $(this).find('.pbtype');
                                var ddlCustomers1 = $(this).find('.pbtype1');
                                for (var i = 0; i < PT.length; i++) {
                                    ddlCustomers.append($("<option></option>").val(PT[i].Batch).html(PT[i].Batch));
                                    ddlCustomers1.append($("<option></option>").val(PT[i].Batch).html(PT[i].Batch));
                                }
                            }
                        }
                    });

                });
                $(document).on('change', '.ptype', function () {


                    if ($(this).val() != "0") {
                        if ($(this).val() == "Good") {
                            $(this).closest('tr').find('td:eq(4)').text($(this).closest('tr').find('input[name=stkval]').attr('stkgood'));
                        }
                        else if ($(this).val() == "Damage") {
                            $(this).closest('tr').find('td:eq(4)').text($(this).closest('tr').find('input[name=stkval]').attr('stkdamage'));
                        }
                    }
                    else {
                        $(this).closest('tr').find('td:eq(4)').text('0');

                    }

                });

                $('#stockTable input[name=txtQty]').focus(function (event) {
                    if ($(this).closest('tr').find('.ptype').val() == "0") {
                        alert('Select Type');
                        $(this).closest('tr').find('.ptype').focus();
                        return false;
                    }
                })

                $('#stockTable input[name=txtQty]').focus(function (event) {
                    if ($(this).closest('tr').find('.ptype1').val() == "0") {
                        alert('Select Type');
                        $(this).closest('tr').find('.ptype1').focus();
                        return false;
                    }
                })

                $('#stockTable input[name=txtQty]').blur(function (event) {
                    if (Number($(this).closest('tr').find('td:eq(4)').text()) < Number($(this).val())) {
                        alert('Value Must Below Stock Value : ' + $(this).closest('tr').find('td:eq(4)').text());
                        $(this).val("0")
                        $(this).focus();
                        return false;
                    }
                })



                //save
                var objMain = {};
                $(document).on('click', '#btnSave', function () {



                    var transNo = $('#Txt_Adj_no').val();
                    if (transNo.length <= 0) { alert('Enter Adj.SlNo.!!'); $('#Txt_Adj_no').focus(); return false; }

                    var transDate = $('#transDate').val();
                    if (transDate.length <= 0) { alert('Enter Adj.Date!!'); $('#transDate').focus(); return false; }

                    var SName = $('#<%=Stk_Loc.ClientID%> :selected').text();
                    if (SName == "---Select---") { alert("Please Select Stock Location."); $('#<%=Stk_Loc.ClientID%>').focus(); return false; }

                    var Auth = $('#authorised').val();
                    if (Auth.length <= 0) { alert('Enter Authorised !!'); $('#authorised').focus(); return false; }



                    var HeadArr = [];

                    HeadArr.push({
                        transNo: transNo,
                        transDate: transDate,
                        stkloc: SName,
                        stkloc_Nm: $('#<%=Stk_Loc.ClientID%> option:selected').val(),
                        Auth_Nm: Auth

                    });


                    var ch = false;
                    $('#stockTable >tbody >tr').each(function () {


                        if ($(this).find($("input[name=txtQty]").val()) > 0) {
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
                        if ($(this).find('.ptype1').val() != 0) {
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
                        //alert($(this).find('.ptype').val());
                        var oqty = $(this).find("input[name=txtQty]").attr('pv_Val') || 0;
                        productArr.push({
                            pCode: $(this).find("input[name=pCode]").val(),
                            pName: $(this).find('td').eq(1).text(),
                            pType: $(this).find('.ptype').val(),
                            pType_Name: $(this).find('.ptype').find('option:selected').text(),
                            pbtype: $(this).find('.pbtype').val(),
                            pbtype_Name: $(this).find('.pbtype').find('option:selected').text(),
                            stock: $(this).find('td').eq(3).text(),
                            pType1: $(this).find('.ptype1').val(),
                            pType_Name1: $(this).find('.ptype1').find('option:selected').text(),

                            pbtype1: $(this).find('.pbtype1').val(),
                            pbtype_Name1: $(this).find('.pbtype1').find('option:selected').text(),

                            pqty: $(this).find("input[name=txtQty]").val(),
                            preason: $(this).find("input[name=txtReason]").val(),
                            oqty: oqty
                        });
                    });



                    if (countr <= 0) {
                        alert('Enter Atleast One Row!!!');
                        $('#stockTable').focus();
                        return false;
                    }

                    objMain['TransH'] = HeadArr;
                    objMain['TransD'] = productArr;
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "Stock_Adj_Entry.aspx/SaveDate",
                        data: "{'data':'" + JSON.stringify(objMain) + "'}",
                        dataType: "json",
                        success: function (data) {
                            alert(data.d);
                            var url = "Stock_Adj_List.aspx";
                            window.location = url;
                            //alert("Week Master has been updated successfully!!!");                        
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

				$('input').bind('cut copy paste', function (e) {
                 e.preventDefault(); //disable cut,copy,paste
                 });

            });   
      
        </script>
        <style type="text/css">
            .mGrid1
            {
                width: 100%;
                background-color: #fff;
                margin: 5px 0 10px 0;
                border: solid 1px #525252;
                border-collapse: collapse;
            }
            .mGrid1 td
            {
                padding: 2px;
                border: solid 1px #c1c1c1;
                color: #717171;
            }
            .mGrid1 th
            {
                padding: 4px 2px;
                color: #fff;
                background: #424242 url(grd_head.png) repeat-x top;
                border-left: solid 1px #525252;
                font-size: 0.9em;
            }
            .mGrid1 .alt
            {
                background: #fcfcfc url(grd_alt.png) repeat-x top;
            }
            .mGrid1 .pgr
            {
                background: #424242 url(grd_pgr.png) repeat-x top;
            }
            .mGrid1 .pgr table
            {
                margin: 5px 0;
            }
            .mGrid1 .pgr td
            {
                border-width: 0;
                padding: 0 6px;
                border-left: solid 1px #666;
                font-weight: bold;
                color: #fff;
                line-height: 12px;
            }
            .mGrid1 .pgr a
            {
                color: #666;
                text-decoration: none;
            }
            .mGrid1 .pgr a:hover
            {
                color: #000;
                text-decoration: none;
            }
        </style>
    </head>
    <body>
        <form id="form1" runat="server">
        <asp:HiddenField ID="hdnmode" runat="server" />
        <asp:HiddenField ID="hdntransno" runat="server" />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="container" id="itemlist2">
            <div class="row" style="margin-left: 70px;">
                <div class="col-md-5 col-lg-3">
                    <div class="form-group">
                        <label class="control-label" for="focusedInput">
                            Adj.SlNo</label>
                        <input class="form-control" id="Txt_Adj_no" name="Adj_no" data-validation="required"
                            type="text" autocomplete="off" readonly/>
                    </div>
                </div>
                <div class="col-md-3 col-lg-3">
                    <div class="form-group">
                        <label class="control-label" for="focusedInput">
                            Adj Date</label>
                        <div class="inputGroupContainer">
                            <div class="input-group date">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-th"></i></span>
                                <input type="text" id="transDate" class="form-control datetimepicker" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-5 col-lg-4">
                    <div class="form-group">
                        <label class="control-label" for="focusedInput">
                            Stock Location</label>
                        <div class="inputGroupContainer">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                <asp:DropDownList ID="Stk_Loc" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <asp:RequiredFieldValidator ID="RFieldValidator1" InitialValue="0" runat="server"
                            ErrorMessage="RequiredField" ControlToValidate="Stk_Loc"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-12 col-sm-offset-0">
                <table id="stockTable" class="table table-responsive">
                    <thead>
                        <tr>
                            <th style="width: 50px">
                                Sl.No.
                            </th>
                            <th style="width: 250px">
                                Product Name
                            </th>
                            <th style="width: 250px">
                                From
                            </th>
                            <th style="width: 250px">
                                From Batch.No
                            </th>
                            <th style="width: 50px">
                                Stock
                            </th>
                            <th style="width: 250px">
                                To
                            </th>
                            <th style="width: 250px">
                                To Batch.No
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
        <div class="row">
            <div class="row">
                <label for="authorised" class="col-md-2 control-label" style="font-weight: normal">
                    Authorised By</label>
                <div class="col-md-3 inputGroupContainer">
                    <input type="text" name="authorised" id="authorised" class="form-control" />
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-12" style="text-align: center">
                <a id="btnSave" class="btn btn-primary" style="vertical-align: middle; font-size: 17px;">
                    <span>Save</span></a></div>
        </div>
        </form>
    </body>
    </html>
</asp:Content>
