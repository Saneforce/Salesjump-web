<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Scheme_Master.aspx.cs" Inherits="MIS_Reports_Scheme_Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link rel="stylesheet" type="text/css" media="all" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/smoothness/jquery-ui.css" />

    <link rel="stylesheet" href="https://netdna.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css" />
    <link href="https://netdna.bootstrapcdn.com/bootstrap/3.0.3/css/bootstrap.min.css" rel="stylesheet" id="bootstrap-css" />



    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css">

    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="../js/lib/filldata.js"></script>

    <%--    <script type="text/javascript" src="https://netdna.bootstrapcdn.com/bootstrap/3.0.3/js/bootstrap.min.js"></script>

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.11.1/moment.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.0/jquery.min.js"></script>
	 
    
    --%>


    <script type="text/javascript">
        var xldata = []; var View_unit_details = [];
        $(document).ready(function () {
            $('.btnDelete').prop('disabled', 'disabled');
            var insertType = '1';
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
            $('#<%=txtFrom.ClientID%>').val(today);
            $('#<%=txtTo.ClientID%>').val(today);
            $('#ddlType').val(1);

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Scheme_Master.aspx/getDivision",
                dataType: "json",
                success: function (data) {
                    Div_Details = JSON.parse(data.d);
                    if (Div_Details.length > 0) {
                        $('#ddlDiv').empty();
                        var div = $('#ddlDiv');
                        div.empty().append('<option selected="selected" value="0">Select Division</option>');
                        for (var i = 0; i < Div_Details.length; i++) {
                            str = '<option value=' + Div_Details[i].subdivision_code + '>' + Div_Details[i].subdivision_name + ' </option>';
                            $(div).append(str);
                        }
                    }
                },
                error: function (jqXHR, exception) {
                    console.log(jqXHR);
                    console.log(exception);
                }
            });

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Primary_scheme.aspx/get_view_unit",
                dataType: "json",
                success: function (data) {
                    View_unit_details = JSON.parse(data.d) || [];
                },
                error: function (result) {
                }
            });

            $(document).on("change", "#ddlDiv", function () {
                var div = $(this).val();
                var subdiv = [];
                if (div == '4') {
                    div = '4,';
                }
                document.getElementById("<%= hidden_div.ClientID %>").value = div;                

                /*ProDtl = data.d;*/

                //filter_unit = View_unit_details.filter(function (w) {
                //    return (code == w.PCode);
                //});

                subdiv = ProDtl.filter(function (p) {
                    return (div == p.subdivcode);
                });
                //ProDtl = subdiv;
                str = '';
                tbl = $('#tbl');
                $(tbl).find('thead tr').remove();
                $(tbl).find('tbody tr').remove();                                

                genTable(div);                
            });

            $('#ddlType').val(2);
            function genTable(subdiv) {
                //genTable = function () {
                    type = $('#ddlType').val();
                    if (subdiv == undefined) {
                        subdiv = '0';
                    }
                    else {
                        subdiv = subdiv;
                    }
                    //var subdiv = '0';

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Scheme_Master.aspx/getProduct",
                        //data: "{'Type':'" + type + "'}",
                        data: "{'Type':'" + type + "','SubdivCode':'" + subdiv + "'}",
                        dataType: "json",
                        success: function (data) {
                            ProDtl = data.d;
                            str = '';
                            tbl = $('#tbl');
                            $(tbl).find('thead tr').remove();
                            $(tbl).find('tbody tr').remove();
                            if (ProDtl.length > 0) {
                                if (type == "2") {

                                    var ofPro = ProDtl;//.filter(item => item.pTypes == "O");
                                    ProDtl = ProDtl;//.filter(item => item.pTypes != "O");
                                    str = '<th>Product Name</th><th>Scheme (Qty)</th><th>Free (Qty)</th><th>Discount%</th><th>Package Offer</th><th>Against Product</th><th>Offer Product</th><th>Add</th>';
                                    $(tbl).find('thead').append('<tr>' + str + '</tr>');

                                    var ofStr = '<option value="0">Select</option>';
                                    if (ofPro.length > 0) {
                                        ofPro.forEach((obj) => ofStr += `<option value=${obj.pCode}>${obj.pName}</option>`);
                                    }

                                    for (var i = 0; i < ProDtl.length; i++) {
                                        str = '<td style="vertical-align: middle;padding: 2px 2px;"><input type="hidden" class="AutoID" name="AutoID" value="0"} /> <input type="hidden" name="pCode" value="' + ProDtl[i].pCode + '"/>  <span>' + ProDtl[i].pName + '</span></td>';
                                        str += '<td style="padding: 2px 2px;    vertical-align: middle;"><input type="text" class="form-control " id="txtScheme" name="txtScheme" /></td><td style="padding: 2px 2px;    vertical-align: middle;"><input type="text" class="form-control " id="txtFree" name="txtFree" /></td><td style="padding: 2px 2px;    vertical-align: middle;"><input type="text" class="form-control " id="txtDiscount" name="txtDiscount" /></td><td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" class="packages" name="' + ProDtl[i].pCode + '" value="1" id="checkboxID' + (i + 1) + '"/><label class="packageslbl" for="checkboxID' + (i + 1) + '"></label></p></td>';
                                        str += `<td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" name="a' + ProDtl[i].pCode + '" class="against"  value="1" id="against${(i + 1)}"/><label for="against${(i + 1)}" class="againstlbl"></label></p></td>`;
                                        str += `<td style="padding: 2px 2px; vertical-align: middle;"><select class="form-control" disabled="disabled" name="allType" >${ofStr}</select></td><td style="padding: 2px 2px; vertical-align: middle;""><button type="button"  class="btn btn-default addpro btn-md"><span class="glyphicon glyphicon-plus" style="color:#378b2c"></span></button></td>`;
                                        $(tbl).find('tbody').append('<tr id="' + ProDtl[i].pCode + '">' + str + '</tr>');
                                    }
                                }
                                else {
                                    str = '<th>Category Name</th><th>Scheme (Qty)</th><th>Free (Qty)</th><th>Discount%</th><th>Package Offer</th>';
                                    $(tbl).find('thead').append('<tr>' + str + '</tr>');
                                    for (var i = 0; i < ProDtl.length; i++) {
                                        str = '<td style="vertical-align: middle;padding: 2px 2px;"> <input type="hidden" name="pCode" value="' + ProDtl[i].pCode + '"/>  <span>' + ProDtl[i].pName + '</span></td>';
                                        str += '<td style="padding: 2px 2px;    vertical-align: middle;"><input type="text" class="form-control " id="txtScheme" name="txtScheme" /></td><td style="padding: 2px 2px;    vertical-align: middle;"><input type="text" class="form-control " id="txtFree" name="txtFree" /></td><td style="padding: 2px 2px;    vertical-align: middle;"><input type="text" class="form-control " id="txtDiscount" name="txtDiscount" /></td><td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" class="packages" value="1" id="checkboxID' + (i + 1) + '"/><label class="packageslbl" for="checkboxID' + (i + 1) + '"></label></p></td>';
                                        $(tbl).find('tbody').append('<tr>' + str + '</tr>');
                                    }
                                }
                            }
                            else {
                                alert('No Product This Division..!');
                            }                            
                        },
                        error: function (jqXHR, exception) {
                            console.log(jqXHR);
                            console.log(exception);
                        }
                    });
                //}
            }                       
           
            genTable();

            function Get_unit(code) {

                var filter_unit = []; units = ""; units1 = "";
                units += "<option value='0'>Select Unit</option>";

                filter_unit = View_unit_details.filter(function (w) {
                    return (code == w.PCode);
                });

                if (filter_unit.length > 0) {

                    for (var z = 0; z < filter_unit.length; z++) {
                        units += "<option value='" + filter_unit[z].UOM_Id + "'>" + filter_unit[z].UOM_Nm + "</option>";
                        //units += "<option value='" + filter_unit[z].UOM_Id + "'>" + filter_unit[z].UOM_Nm + "</option>";

                    }
                }
                return units
            }

            genSchemTbale = function () {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Scheme_Master.aspx/getScheme",
                    dataType: "json",
                    success: function (data) {
                        SchDtl = data.d;
                        //  console.log(SchDtl);
                        str = '';
                        tbl = $('#schmTbl');
                        $(tbl).find('thead tr').remove();
                        $(tbl).find('tbody tr').remove();
                        if (SchDtl.length > 0) {

                            str = '<th>SlNo.</th><th>Scheme Name</th><th>View</th>';
                            $(tbl).find('thead').append('<tr>' + str + '</tr>');
                            for (var i = 0; i < SchDtl.length; i++) {
                                //str = '<td style="vertical-align: middle;" ><span>' + (i + 1) + '</span></td><td style="vertical-align: middle;"><span>' + SchDtl[i].sName + '</span></td>';
                                str = '<td style="vertical-align: middle;" ><span>' + (i + 1) + '</span></td><input type="hidden" class="hiddSubDivCode" value=' + SchDtl[i].subdivCode+' ><td style="vertical-align: middle;"><span>' + SchDtl[i].sName + '</span></td>';
                                str += '<td><a name="btnview" type="button" class="btn btn-primary btnview" style="width: 100px">Edit</a></td>';
                                $(tbl).find('tbody').append('<tr>' + str + '</tr>');
                            }
                        }
                        else {
                            str = '<th>SlNo.</th><th>Scheme Name</th><th>Edit</th>';
                            $(tbl).find('thead').append('<tr>' + str + '</tr>');
                            str = '<td colspan="3" style="color:red">No Records Found..!</td>';
                            $(tbl).find('tbody').append('<tr>' + str + '</tr>');
                        }
                    },
                    error: function (jqXHR, exception) {
                        console.log(jqXHR);
                        console.log(exception);
                    }
                });
            }
            //schmTbl
            genSchemTbale();
            var stkDtl = [];
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Scheme_Master.aspx/getDistributor",
                dataType: "json",
                success: function (data) {
                    stkDtl = data.d;
                    //  console.log(stkDtl);
                },
                error: function (jqXHR, exception) {
                    console.log(jqXHR);
                    console.log(exception);
                }
            });

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Scheme_Master.aspx/getState",
                dataType: "json",
                success: function (data) {
                    sDt = data.d;
                    //  console.log(sDt);
                    if (sDt.length > 0) {
                        var div = $('#state');
                        for (var i = 0; i < sDt.length; i++) {
                            str = '<a href="#" class="list-group-item">' + sDt[i].stName + '<input type="checkbox" name=' + sDt[i].stCode + ' class="chk pull-right"/></a>';
                            $(div).append(str);
                        }
                    }
                },
                error: function (jqXHR, exception) {
                    console.log(jqXHR);
                    console.log(exception);
                }
            });

            $('.stOnly .all').click(function (e) {
                e.stopPropagation();
                var $this = $(this);
                if ($this.is(":checked")) {
                    $('#state').find("[type=checkbox]").prop("checked", true);
                }
                else {
                    $('#state').find("[type=checkbox]").prop("checked", false);
                    $this.prop("checked", false);
                }
                genDist();
            });

            $('.disOnly .all').click(function (e) {
                e.stopPropagation();
                var $this = $(this);
                if ($this.is(":checked")) {
                    $('#distributor').find("[type=checkbox]").prop("checked", true);
                }
                else {
                    $('#distributor').find("[type=checkbox]").prop("checked", false);
                    $this.prop("checked", false);
                }
            });


            $('[type=checkbox]').click(function (e) {
                e.stopPropagation();
            });



            genDist = function () {
                if (stkDtl.length > 0) {
                    var div = $('#distributor');
                    $(div).find('a').remove();
                    var items = $("#state input:checked");
                    items.each(function (idx, item) {
                        Dis = stkDtl.filter(function (obj) {
                            return obj.stCode == $(item).attr('name');
                        });
                        if (Dis.length > 0) {
                            for (var i = 0; i < Dis.length; i++) {
                                str = '<a href="#" class="list-group-item">' + Dis[i].DistName + '<input type="checkbox" name=' + Dis[i].DistCode + ' class="chk pull-right"/></a>';
                                $(div).append(str);
                            }
                        }
                    });


                    if ($("#distributor input").length <= 0) {
                        str = '<a href="#" class="list-group-item">No Distributor</a>';
                        $(div).append(str);

                    }
                }
            }

            $('.stOnly .list-group a .chk').click(function (e) {
                e.stopPropagation();
                var $this = $(this);
                if ($this.is(":checked")) {
                    $this.prop("checked", true);
                }
                else {
                    $this.prop("checked", false);
                }
                if ($this.hasClass("all")) {
                    $this.trigger('click');
                }
                genDist();
            });

            $(document).on('change', '.against', function (e) {
                var c = $(this).closest('td').find('[type=checkbox]').attr("checked") ? 'Y' : 'N';
                if (c == 'Y') {
                    $(this).closest('tr').find('select[name="allType"]').prop('disabled', '');

                }
                else {
                    $(this).closest('tr').find('select[name="allType"]').val(0);
                    $(this).closest('tr').find('select[name="allType"]').prop('disabled', 'disabled');
                }
            });

            var clCount = 1;
            $(document).on('click', '.addpro', function (e) {
                var clonedRow = $(this).closest('tr').clone();
                clonedRow.find('input[type="text"]').val('');

                clonedRow.find('.AutoID').val(clCount);


                clonedRow.find('td').eq(0).find('span').text('"');
                clonedRow.find('td').eq(0).css('text-align', 'center');
                clonedRow.find('[type=checkbox]').prop("checked", false);
                clonedRow.find('select[name = "allType"]').prop('disabled', 'disabled');
                var agId = clonedRow.find('.against').attr('id') + clCount;
                clonedRow.find('.against').attr('id', agId);
                clonedRow.find('.againstlbl').attr('for', agId);
                var pkId = clonedRow.find('.packages').attr('id') + clCount;
                clonedRow.find('.packages').attr('id', pkId);
                clonedRow.find('.packageslbl').attr('for', pkId);
                clonedRow.find('.addpro').find('.glyphicon').removeClass('glyphicon-plus');
                clonedRow.find('.addpro').find('.glyphicon').addClass('glyphicon-minus');
                clonedRow.find('.addpro').addClass('delpro');
                clonedRow.find('.delpro').removeClass('addpro');
                $(this).closest('tr').after(clonedRow);
                clCount++;
            });
            $(document).on('click', '.delpro', function (e) {


                if (insertType == 2) {
                    if ($(this).closest('tr').attr('class') == 'oldrow') {
                        $(this).closest('tr').find('input[name="actFlg"]').val("1");
                        $(this).closest('tr').hide();
                    }
                    else {
                        $(this).closest('tr').remove();
                    }

                }
                else {
                    $(this).closest('tr').remove();
                }

            });



            $(document).on('click', '.disOnly .list-group a .chk', function (e) {
                e.stopPropagation();
                var $this = $(this);
                if ($this.is(":checked")) {
                    $this.prop("checked", true);
                }
                else {
                    $this.prop("checked", false);
                }
                if ($this.hasClass("all")) {
                    $this.trigger('click');
                }
            });


            fillData = function (scData) {

                $('#<%=txtName.ClientID %>').val(scData[0].schemeName);
                $('#<%=txtFrom.ClientID %>').val(scData[0].FDate);
                $('#<%=txtTo.ClientID %>').val(scData[0].TDate);

                $('#<%=txtName.ClientID %>').prop('disabled', 'disabled');

                let uState = [...new Set(scData.map(item => item.State_Code))];
                var st = uState[0].split(',')
                if (uState.length > 0) {
                    for (var i = 0; i < st.length; i++) {
                        $('#state a').each(function () {
                            if ($(this).find('input[type=checkbox]').attr('name') == st[i]) {
                                $(this).find('input[type=checkbox]').prop("checked", true);
                            }
                        });

                    }
                    genDist();
                    let uStockist = [...new Set(scData.map(item => item.Stockist_Code))];
                    var stk = uStockist[0].split(',')
                    if (uStockist.length > 0) {
                        for (var i = 0; i < stk.length; i++) {
                            $('#distributor a').each(function () {
                                if ($(this).find('input[type=checkbox]').attr('name') == stk[i]) {
                                    $(this).find('input[type=checkbox]').prop("checked", true);
                                }
                            });

                        }
                    }
                }

                if (scData[0].sub_div_code.length > 0) {
                    $('#ddlDiv').val(scData[0].sub_div_code);
                }                

                tbl = $('#tbl');
                $(tbl).find('thead tr').remove();
                $(tbl).find('tbody tr').remove();
                
                var ofPro = scData//.filter(item => item.pTypes == "O");
                scData = scData//.filter(item => item.pTypes != "O");
                str = '<th>Product Name</th><th>Scheme </th><th>Free</th><th>Discount%</th><th>Package Offer</th><th>Against Product</th><th>Offer Product</th><th>Add</th>';
                $(tbl).find('thead').append('<tr>' + str + '</tr>');

                var ofStr = `<option value="0">Select</option>`;
                if (ofPro.length > 0) {
                    ofPro.forEach((obj) => ofStr += `<option value=${obj.pCode}>${obj.pName}</option>`);
                }
                var prdCodeex = '';

                var stkCode = scData[0].Stockist_Code;
                var stateCode = scData[0].State_Code;
                var subdiv = scData[0].sub_div_code;
                console.log(stkCode);
                console.log(stateCode);
                console.log(scData);
                var stkDt = scData.filter(lst => (lst.Stockist_Code == stkCode || lst.Stockist_Code == '') && (lst.State_Code == stateCode || lst.State_Code == ''));
                //var stkDt = scData.filter(lst => (lst.Stockist_Code == stkCode || lst.Stockist_Code == '') && (lst.State_Code == stateCode || lst.State_Code == '') && (lst.sub_div_code == subdiv || lst.sub_div_code == ''));
                //var stkDt = ProDtl.filter(lst => (lst.Stockist_Code == stkCode || lst.Stockist_Code == '') && (lst.State_Code == stateCode || lst.State_Code == '') && (lst.sub_div_code == subdiv || lst.sub_div_code == ''));
                console.log(stkDt);
                var maids = 0, mid = 0;
                stkDt.forEach((element, index, array) => {


                    var pkg = '';
                    var agp = '';
                    var agPro = element.AgProduct;


                    if (Number(element.AutoID) > 0) {
                        maids = element.AutoID;
                    }
                    else {
                        maids = Number(mid) + 1;
                        mid++;
                    }
                    console.log(maids);
                    if (element.Package == 'Y') {
                        pkg = 'checked';
                    }
                    if (element.Against == 'Y') {
                        agp = 'checked';
                    }

                    if (prdCodeex != element.pCode) {
                        str = `<td style="vertical-align: middle;padding: 2px 2px;"> <input type="hidden" class="AutoID" name="AutoID" value=${maids} /><input type="hidden" name="pCode" value=${element.pCode} /><input type="hidden" name="actFlg" value=${element.actFlg} />   <span>${element.pName} </span></td>`;
                        str += `<td style="padding: 2px 2px; vertical-align: middle;"><input type="text" class="form-control " id="txtScheme" name="txtScheme" value="${element.scheme}" /></td><td style="padding: 2px 2px;    vertical-align: middle;"><input type="text" class="form-control " id="txtFree" name="txtFree"  value=${element.free} /></td><td style="padding: 2px 2px;    vertical-align: middle;"><input type="text" class="form-control " id="txtDiscount" name="txtDiscount"  value=${element.discount} /></td><td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" class="packages" ${pkg} value="1" id="checkboxID${index + 1}"/><label class="packageslbl" for="checkboxID${index + 1}"></label></p></td>`;
                        str += `<td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" ${agp} class="against"  value="1" id="against${(index + 1)}"/><label for="against${(index + 1)}" class="againstlbl"></label></p></td>`;
                        str += `<td style="padding: 2px 2px; vertical-align: middle;"><select class="form-control" disabled="disabled" name="allType" >${ofStr}</select></td><td style="padding: 2px 2px; vertical-align: middle;""><button type="button"  class="btn btn-default addpro btn-md"><span class="glyphicon glyphicon-plus  " style="color:#378b2c"></span></button></td>`;
                        $(tbl).find('tbody').append('<tr>' + str + '</tr>');

                        if (element.Against == 'Y') {
                            $(tbl).find('tr').eq(index + 1).find('select[name="allType"]').prop('disabled', '');
                        }
                        else {
                            $(tbl).find('tr').eq(index + 1).find('select[name="allType"]').prop('disabled', 'disabled');
                        }
                        $(tbl).find('tr').eq(index + 1).find('select[name="allType"]').val(agPro);
                        prdCodeex = element.pCode;
                    }
                    else {

                        str = `<td style="vertical-align: middle;padding: 2px 2px; text-align:center"><input type="hidden" class="AutoID" name="AutoID" value=${maids} /> <input type="hidden" name="pCode" value=${element.pCode} /><input type="hidden" name="actFlg" value=${element.actFlg} />   <span>"</span></td>`;
                        str += `<td style="padding: 2px 2px; vertical-align: middle;"><input type="text" class="form-control " id="txtScheme" name="txtScheme" value=${element.scheme} /></td><td style="padding: 2px 2px;    vertical-align: middle;"><input type="text" class="form-control " id="txtFree" name="txtFree"  value=${element.free} /></td><td style="padding: 2px 2px;    vertical-align: middle;"><input type="text" class="form-control " id="txtDiscount" name="txtDiscount"  value=${element.discount} /></td><td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" class="packages" ${pkg} value="1" id="checkboxID${index + 1}"/><label class="packageslbl" for="checkboxID${index + 1}"></label></p></td>`;
                        str += `<td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" ${agp} class="against"  value="1" id="against${(index + 1)}"/><label for="against${(index + 1)}" class="againstlbl"></label></p></td>`;
                        str += `<td style="padding: 2px 2px; vertical-align: middle;"><select class="form-control" disabled="disabled" name="allType" >${ofStr}</select></td><td style="padding: 2px 2px; vertical-align: middle;""><button type="button"  class="btn btn-default delpro btn-md"><span class="glyphicon glyphicon-minus  " style="color:#378b2c"></span></button></td>`;
                        $(tbl).find('tbody').append('<tr class="oldrow">' + str + '</tr>');

                        if (element.Against == 'Y') {
                            $(tbl).find('tr').eq(index + 1).find('select[name="allType"]').prop('disabled', '');
                        }
                        else {
                            $(tbl).find('tr').eq(index + 1).find('select[name="allType"]').prop('disabled', 'disabled');
                        }
                        $(tbl).find('tr').eq(index + 1).find('select[name="allType"]').val(agPro);
                        prdCodeex = element.pCode;
                    }

                });
                $('.btnDelete').prop('disabled', '');
            }

            $(document).on('click', '.btnsave', function () {
                if ($('#ddlDiv').val() == '0') {
                    alert('Select Division');
                    return false;
                }
                var state = $("#state input:checked");
                if (state.length == 0) {
                    alert('Select State..!');
                    return false;
                }
                var dist = $("#distributor input:checked");
                if (dist.length == 0) {
                    alert('Select Distributor..!');
                    return false;
                }
                var fDate = $("#txtFrom").val();
                var tDate = $("#txtTo").val();
                if (fDate == "") {
                    alert('Enter From Date..!');
                    $("#txtFrom").focus();
                    return false;
                }
                if (tDate == "") {
                    alert('Enter To Date..!');
                    $("#txtFrom").focus();
                    return false;
                }

                var schemeName = $("#<%=txtName.ClientID%>").val();
                if (schemeName == "" || schemeName.length == '') {
                    alert('Enter To Scheme..!');
                    $("#<%=txtName.ClientID%>").focus();
                    return false;
                }

                //if ($(tbl).find('tbody').find('tr').length == 0) {
                //    alert('No Row Found..!');
                //    return false;
                //}

                var tbl = $('#tbl');
                var arr = [];
                var prdList = '';
                var pNm = '';
                $(tbl).find('tbody').find('tr').each(function () {
                    prdList = $(this).find('input[name="pCode"]').val();
                    pNm = '';
                    if ($(this).find('select[name="allType"]').val() != "0") {
                        pNm = $(this).find('select[name="allType"] :selected').text();
                    }


                    arr.push({
                        AutoId: $(this).find('input[name="AutoID"]').val() || 0,
                        pCode: $(this).find('input[name="pCode"]').val(),
                        scheme: $(this).find('input[name="txtScheme"]').val() || 0,
                        free: $(this).find('input[name="txtFree"]').val() || 0,
                        discount: $(this).find('input[name="txtDiscount"]').val() || 0,
                        Package: $(this).find('.packages').attr("checked") ? 'Y' : 'N',
                        Against: $(this).find('.against').attr("checked") ? 'Y' : 'N' || '',
                        AgProduct: $(this).find('select[name="allType"]').val() || '',
                        actFlg: $(this).find('input[name="actFlg"]').val() || '0',
                        Offer_Product_Name: pNm
                    });
                });

                stC = '';
                state.each(function (idx, item) {
                    stC += $(item).attr('name') + ',';
                });

                //console.log(stC);
                dsC = '';
                dist.each(function (idx, item) {
                    dsC += $(item).attr('name') + ',';
                });
                //console.log(dsC);
                console.log(JSON.stringify(arr));
                var FDate = $('#<%=txtFrom.ClientID%>').val();
                var TDate = $('#<%=txtTo.ClientID%>').val();
                var Types = $('#ddlType').val();
                var SchemeName = $('#<%=txtName.ClientID%>').val();
                var subdivCode = $('#ddlDiv').val();


                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Scheme_Master.aspx/SaveScheme",
                    //data: "{'Data':'" + JSON.stringify(arr) + "','DistCode':'" + dsC + "','StateCode':'" + stC + "','FDate':'" + FDate + "','TDate':'" + TDate + "','SchemeName':'" + SchemeName + "','Types':'" + Types + "','insertType':'" + insertType + "'}",
                    data: "{'Data':'" + JSON.stringify(arr) + "','DistCode':'" + dsC + "','StateCode':'" + stC + "','FDate':'" + FDate + "','TDate':'" + TDate + "','SchemeName':'" + SchemeName + "','Types':'" + Types + "','insertType':'" + insertType + "','subdiv':'" + subdivCode+"'}",
                    dataType: "json",
                    success: function (data) {
                        if (data.d == 'Success') {

                            alert('SCHEME INSERT OR UPDATE SUCCESSFULLY');
                            insertType = '2';
                            $('#<%=txtName.ClientID %>').prop('disabled', '');
                            $('#<%=txtName.ClientID %>').val('');
                            $('.btnDelete').prop('disabled', 'disabled');
                            $('#ddlType').val(0);
                            $('.stOnly').find("[type=checkbox]").prop("checked", false);
                            genDist();
                            genTable();
                            genSchemTbale();
                            //$('#ddlType').val(2);
                        }
                        else {

                            alert(data.d);
                        }

                    },
                    error: function (exception) {
                        console.log(exception);
                    }
                });

            });

            $(document).on('keypress', '#tbl input[type = "text"]', function (event) {
                if (event.which != 8 && isNaN(String.fromCharCode(event.which))) {
                    event.preventDefault();
                }
            });



            $(document).on('focus', '#tbl input[name = "txtFree"]', function (event) {
                var sche = $(this).closest('tr').find('input[name = "txtScheme"]').val();
                var cNum = Number(sche || 0);
                console.log(cNum);
                if (cNum < 1) {
                    alert('Enter Scheme..!');
                    $(this).closest('tr').find('input[name = "txtScheme"]').focus();
                }
            });

            $(document).on('focus', '#tbl input[name = "txtDiscount"]', function (event) {
                var sche = $(this).closest('tr').find('input[name = "txtScheme"]').val();
                var cNum = Number(sche || 0);
                console.log(cNum);
                if (cNum < 1) {
                    alert('Enter Scheme..!');
                    $(this).closest('tr').find('input[name = "txtScheme"]').focus();
                }
            });





            $(document).on('click', '.btnImport', function () {
                if ($('#ddlDiv').val() == "0") { alert("Select Division"); return false; }
                ExportXLToTable(
                    function (data) {
                        xldata = data;
                        $('#ddlType').val(2);
                        //genTable();
                        var subdiv=$('#ddlDiv').val();
                        //fillData1(xldata);
                        fillData1(xldata, subdiv);

                    },
                    function (Msg) {
                        console.log(Msg);
                    }
                )
            });

            function fillData1(xldata, subdiv) {
                type = $('#ddlType').val();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Scheme_Master.aspx/getProduct",
                    //data: "{'Type':'" + type + "'}",
                    data: "{'Type':'" + type + "','SubdivCode':'" + subdiv + "'}",
                    dataType: "json",
                    success: function (data) {
                        ProDtl = data.d;
                    },
                    error: function (jqXHR, exception) {
                        console.log(jqXHR);
                        console.log(exception);
                    }
                });
                tbl = $('#tbl');
                $(tbl).find('thead tr').remove();
                $(tbl).find('tbody tr').remove();

                var stkCode = ProDtl[0].Stockist_Code;
                var stateCode = ProDtl[0].State_Code;
                var subdiv = ProDtl[0].sub_div_code;

               
                //var ofPro = ProDtl.filter(lst => (lst.Stockist_Code == stkCode || lst.Stockist_Code == '') && (lst.State_Code == stateCode || lst.State_Code == ''));

                //var ofPro = ProDtl//.filter(item => item.pTypes == "O");
                //ProDtl = ProDtl//.filter(item => item.pTypes != "O");

                var ofPro = xldata//.filter(item => item.pTypes == "O");
                xldata = xldata//.filter(item => item.pTypes != "O");


                str = '<th>Product Name</th><th>Scheme </th><th>Free</th><th>Discount%</th><th>Package Offer</th><th>Against Product</th><th>Offer Product</th><th>Add</th>';
                $(tbl).find('thead').append('<tr>' + str + '</tr>');

                var ofStr = `<option value="0">Select</option>`;
                if (ofPro.length > 0) {
                    //ofPro.forEach((obj) => ofStr += `<option value=${obj.pCode}>${obj.pName}</option>`);
                    ofPro.forEach((obj) => ofStr += `<option value=${obj.ProductCode}>${obj.ProductName}</option>`);
                }
                var prdCodeex = '';
                var maids = 0, mid = 0;
                xldata.forEach((element, index, array) => {
                    var pkg = '';
                    var agp = '';
                    var agPro = element.OfferProduct;
                    maids = Number(mid) + 1;
                    mid++;
                    console.log(maids);
                    if (element.Package == 'Y') {
                        pkg = 'checked';
                    }
                    if (element.Against == 'Y') {
                        agp = 'checked';
                    }
                    //(isNaN(totarr1[ar1]) ? 0 : totarr1[ar1]
                    if (prdCodeex != element.ProductCode) {
					
                        str = `<td style="vertical-align: middle;padding: 2px 2px;"> <input type="hidden" class="AutoID" name="AutoID" value=${maids} /><input type="hidden" name="pCode" value=${element.ProductCode} />   <span>${element.ProductName} </span></td>`;
                        str += `<td style="padding: 2px 2px; vertical-align: middle;"><input type="text" class="form-control " id="txtScheme" name="txtScheme" value="${isNaN(element.Scheme) ? 0 : element.Scheme}" /></td><td style="padding: 2px 2px;    vertical-align: middle;"><input type="text" class="form-control " id="txtFree" name="txtFree"  value=${isNaN(element.Free) ? 0 : element.Free} /></td><td style="padding: 2px 2px;    vertical-align: middle;"><input type="text" class="form-control " id="txtDiscount" name="txtDiscount"  value=${isNaN(element.Discount) ? 0 : element.Discount} /></td><td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" class="packages" ${pkg} value="1" id="checkboxID${index + 1}"/><label class="packageslbl" for="checkboxID${index + 1}"></label></p></td>`;
                        str += `<td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" ${agp} class="against"  value="1" id="against${(index + 1)}"/><label for="against${(index + 1)}" class="againstlbl"></label></p></td>`;
                        str += `<td style="padding: 2px 2px; vertical-align: middle;"><select class="form-control" disabled="disabled" name="allType" >${ofStr}</select></td><td style="padding: 2px 2px; vertical-align: middle;""><button type="button"  class="btn btn-default addpro btn-md"><span class="glyphicon glyphicon-plus  " style="color:#378b2c"></span></button></td>`;
                        $(tbl).find('tbody').append('<tr>' + str + '</tr>');

                        if (element.Against == 'Y') {
                            $(tbl).find('tr').eq(index + 1).find('select[name="allType"]').prop('disabled', '');
                        }
                        else {
                            $(tbl).find('tr').eq(index + 1).find('select[name="allType"]').prop('disabled', 'disabled');
                        }
                        //$(tbl).find('tr').eq(index+1).find('select[name="allType"] :selected').text(agPro);

                        $(tbl).find('tr').eq(index + 1).find('select[name="allType"] option').each(function () {
                            
                            if ($(this).text() == agPro) {
                                this.selected = true;
                                return;
                            }
                        });
                        //$(tbl).find('tr').eq(index + 1).find('select[name="allType"]').val(agPro);                       
                        prdCodeex = element.ProductCode;
                    }
                    else {

                        str = `<td style="vertical-align: middle;padding: 2px 2px; text-align:center"><input type="hidden" class="AutoID" name="AutoID" value=${maids} /> <input type="hidden" name="pCode" value=${element.ProductCode} />   <span>"</span></td>`;
                        str += `<td style="padding: 2px 2px; vertical-align: middle;"><input type="text" class="form-control " id="txtScheme" name="txtScheme" value=${element.Scheme} /></td><td style="padding: 2px 2px;    vertical-align: middle;"><input type="text" class="form-control " id="txtFree" name="txtFree"  value=${element.Free} /></td><td style="padding: 2px 2px;    vertical-align: middle;"><input type="text" class="form-control " id="txtDiscount" name="txtDiscount"  value=${element.Discount} /></td><td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" class="packages" ${pkg} value="1" id="checkboxID${index + 1}"/><label class="packageslbl" for="checkboxID${index + 1}"></label></p></td>`;
                        str += `<td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" ${agp} class="against"  value="1" id="against${(index + 1)}"/><label for="against${(index + 1)}" class="againstlbl"></label></p></td>`;
                        str += `<td style="padding: 2px 2px; vertical-align: middle;"><select class="form-control" disabled="disabled" name="allType" >${ofStr}</select></td><td style="padding: 2px 2px; vertical-align: middle;""><button type="button"  class="btn btn-default delpro btn-md"><span class="glyphicon glyphicon-minus  " style="color:#378b2c"></span></button></td>`;
                        $(tbl).find('tbody').append('<tr class="oldrow">' + str + '</tr>');

                        if (element.Against == 'Y') {
                            $(tbl).find('tr').eq(index + 1).find('select[name="allType"]').prop('disabled', '');
                        }
                        else {
                            $(tbl).find('tr').eq(index + 1).find('select[name="allType"]').prop('disabled', 'disabled');
                        }
                        // $(tbl).find('tr').eq(index+1).find('select[name="allType"] :selected').text(agPro);

                        $(tbl).find('tr').eq(index + 1).find('select[name="allType"] option').each(function () {
                            //if ($(this).text().toLowerCase() == agPro.toLowerCase()) {
                            //    this.selected = true;
                            //    return;
                            //}
                            if ($(this).text().toLowerCase() == agPro) {
                                this.selected = true;
                                return;
                            }
                        });
                        //prdCodeex = element.pCode;

                        //$(tbl).find('tr').eq(index + 1).find('select[name="allType"]').val(agPro);
                        prdCodeex = element.ProductCode;
                    }

                });
                $('.btnDelete').prop('disabled', '');
            }

            $(document).on('click', '.btnClear', function () {
                insertType = '1';
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
                $('#<%=txtFrom.ClientID%>').val(today);
                $('#<%=txtTo.ClientID%>').val(today);
                $('#<%=txtName.ClientID %>').prop('disabled', '');
                $('#<%=txtName.ClientID %>').val('');
                $('.btnDelete').prop('disabled', 'disabled');
                $('#ddlType').val(2);
                $('.stOnly').find("[type=checkbox]").prop("checked", false);
                $('#ddlDiv').val(0);
                //$('.disOnly').find("[type=checkbox]").prop("checked", false);
                genDist();
                genTable();
                genSchemTbale();

            });

            $(document).on('click', '.btnDelete', function () {

                var SchemeName = $('#<%=txtName.ClientID%>').val();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Scheme_Master.aspx/DeleteScheme",
                    data: "{'schemeName':'" + SchemeName + "'}",
                    dataType: "json",
                    success: function (data) {
                        if (data.d == 'Success') {
                            alert('Successfully Delete Scheme');
                            insertType = '1';
                            $('#<%=txtName.ClientID %>').prop('disabled', '');
                            $('#<%=txtName.ClientID %>').val('');
                            $('.btnDelete').prop('disabled', 'disabled');
                            $('#ddlType').val(0);
                            $('.stOnly').find("[type=checkbox]").prop("checked", false);
                            genDist();
                            genTable();
                            genSchemTbale();
                        }
                        else {
                            alert('Error Delete Scheme');
                        }
                    },
                    error: function (exception) {
                        console.log(exception);
                    }
                });
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
                $('#<%=txtFrom.ClientID%>').val(today);
                $('#<%=txtTo.ClientID%>').val(today);
            });

            $('#<%=Upldbt.ClientID%>').click(function () {
                var subdiv = $('#ddlDiv').val();
                if (subdiv == "0") {
                    alert('Select Division');
                    return false;
                }                
            });

            $(document).on('click', '.btnview', function () {
                $('#ddlType').val(2);
                insertType = '2';               
                var hiddSubDivCode = $(this).closest('tr').find('.hiddSubDivCode').val();
                var scName = $(this).closest('tr').find('td').eq('1').text();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Scheme_Master.aspx/getSchemeVAl",
                    //data: "{'schemeName':'" + scName + "'}",
                    data: "{'schemeName':'" + scName + "','subdivCode':'" + hiddSubDivCode+"'}",
                    dataType: "json",
                    success: function (data) {
                        scData = data.d;                       
                        fillData(scData);
                    },
                    error: function (exception) {                        
                        console.log(exception);
                    }                   
                });

            });

            $(document).on('change', '#ddlType', function () {               
                $("#Exlupd").hide()
                if ($(this).val() == "2") $("#Exlupd").show()
                genTable();

            });


            function ExportXLToTable(succes, fail) {
                var regex = /^([a-zA-Z0-9\s_\\.\-:(0-9)])+(.xlsx|.xls)$/;
                /*Checks whether the file is a valid excel file*/
                if (regex.test($("#excelfile").val().toLowerCase())) {
                    var xlsxflag = false; /*Flag for checking whether excel is .xls format or .xlsx format*/
                    if ($("#excelfile").val().toLowerCase().indexOf(".xlsx") > 0) {
                        xlsxflag = true;
                    }
                    /*Checks whether the browser supports HTML5*/
                    if (typeof (FileReader) != "undefined") {
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            var data = e.target.result;
                            /*Converts the excel data in to object*/
                            if (xlsxflag) {
                                var workbook = XLSX.read(data, { type: 'binary' });
                            }
                            else {
                                var workbook = XLS.read(data, { type: 'binary' });
                            }
                            /*Gets all the sheetnames of excel in to a variable*/
                            var sheet_name_list = workbook.SheetNames;

                            var cnt = 0; /*This is used for restricting the script to consider only first sheet of excel*/
                            sheet_name_list.forEach(function (y) { /*Iterate through all sheets*/
                                /*Convert the cell value to Json*/
                                if (xlsxflag) {
                                    var exceljson = XLSX.utils.sheet_to_json(workbook.Sheets[y]);
                                }
                                else {
                                    var exceljson = XLS.utils.sheet_to_row_object_array(workbook.Sheets[y]);
                                }
                                if (exceljson.length > 0 && cnt == 0) {
                                    succes(exceljson);
                                    cnt++;
                                }
                            });
                            $('#exceltable').show();
                        }
                        if (xlsxflag) {/*If excel file is .xlsx extension than creates a Array Buffer from excel*/
                            reader.readAsArrayBuffer($("#excelfile")[0].files[0]);
                        }
                        else {
                            reader.readAsBinaryString($("#excelfile")[0].files[0]);
                        }
                    }
                    else {
                        fail("Sorry! Your browser does not support HTML5!");
                        // alert("Sorry! Your browser does not support HTML5!");  
                    }
                }
                else {
                    alert("Please upload a valid Excel file!");
                }
            }


            function BindTable(jsondata, tableid) {/*Function used to convert the JSON array to Html Table*/
                var columns = BindTableHeader(jsondata, tableid); /*Gets all the column headings of Excel*/
                for (var i = 0; i < jsondata.length; i++) {
                    var row$ = $('<tr/>');
                    for (var colIndex = 0; colIndex < columns.length; colIndex++) {
                        var cellValue = jsondata[i][columns[colIndex]];
                        if (cellValue == null)
                            cellValue = "";
                        row$.append($('<td/>').html(cellValue));
                    }
                    $(tableid).append(row$);
                }
            }
            function BindTableHeader(jsondata, tableid) {/*Function used to get all column names from JSON and bind the html table header*/
                var columnSet = [];
                var headerTr$ = $('<tr/>');
                for (var i = 0; i < jsondata.length; i++) {
                    var rowHash = jsondata[i];
                    for (var key in rowHash) {
                        if (rowHash.hasOwnProperty(key)) {
                            if ($.inArray(key, columnSet) == -1) {/*Adding each unique column names to a variable array*/
                                columnSet.push(key);
                                headerTr$.append($('<th/>').html(key));
                            }
                        }
                    }
                }
                $(tableid).append(headerTr$);
                return columnSet;
            }
        });
    </script>
    <style type="text/css">
        body {
            overflow-x: visible !important;
        }

        .div_fixed {
            position: fixed;
            right: 1px;
            top: 50%;
            transform: translate(-50%, -50%);
        }

        .list-group {
            max-height: 250px;
            min-height: 250px;
            width: 95%;
            margin-bottom: 10px;
            overflow: scroll;
            -webkit-overflow-scrolling: touch;
        }


        .onoff {
            margin-left: -7px;
            display: -moz-inline-stack;
            display: inline-block;
            vertical-align: middle;
            *vertical-align: auto;
            zoom: 1;
            *display: inline;
            position: relative;
            cursor: pointer;
            width: 55px;
            height: 30px;
            line-height: 30px;
            font-size: 14px;
            font-family: "Helvetica Neue", Helvetica, Arial, sans-serif;
        }

            .onoff label {
                position: absolute;
                top: 0px;
                left: 0px;
                width: 100%;
                height: 100%;
                cursor: pointer;
                background: #cd3c3c;
                border-radius: 5px;
                font-weight: bold;
                color: #FFF;
                -webkit-transition: background 0.3s, text-indent 0.3s;
                -moz-transition: background 0.3s, text-indent 0.3s;
                -o-transition: background 0.3s, text-indent 0.3s;
                transition: background 0.3s, text-indent 0.3s;
                text-indent: 27px;
                -webkit-box-shadow: 0px 0px 5px rgba(0, 0, 0, 0.4) inset;
                -moz-box-shadow: 0px 0px 5px rgba(0, 0, 0, 0.4) inset;
                box-shadow: 0px 0px 5px rgba(0, 0, 0, 0.4) inset;
            }

                .onoff label:after {
                    content: 'NO';
                    display: block;
                    position: absolute;
                    top: 0px;
                    left: 0px;
                    width: 100%;
                    font-size: 12px;
                    color: #591717;
                    text-shadow: 0px 1px 0px rgba(255, 255, 255, 0.35);
                    z-index: 1;
                }

                .onoff label:before {
                    content: '';
                    width: 15px;
                    height: 24px;
                    border-radius: 3px;
                    background: #FFF;
                    position: absolute;
                    z-index: 2;
                    top: 3px;
                    left: 3px;
                    display: block;
                    -webkit-transition: left 0.3s;
                    -moz-transition: left 0.3s;
                    -o-transition: left 0.3s;
                    transition: left 0.3s;
                    -webkit-box-shadow: 0px 0px 5px rgba(0, 0, 0, 0.4);
                    -moz-box-shadow: 0px 0px 5px rgba(0, 0, 0, 0.4);
                    box-shadow: 0px 0px 5px rgba(0, 0, 0, 0.4);
                }

            .onoff input:checked + label {
                background: #378b2c;
                text-indent: 8px;
            }

                .onoff input:checked + label:after {
                    content: 'YES';
                    color: #091707;
                    text-align: left;
                }

                .onoff input:checked + label:before {
                    left: 37px;
                }
    </style>
    <form id="form1" runat="server">
        <div class="container" style="min-width: 100%; width: 100%">
            <asp:HiddenField ID="hidden_div" runat="server" />
            <div class="row">
                <div class="col-md-8">
                             <div class="row">
                        <div class="col-md-12">                            
                            <div class="form-inline">
                                <div class="row">

                                    <div class="form-group">
                                        <label for="lbl_divison">Division:</label><span style="color: Red">*</span>
                                        <select id="ddlDiv" class="form-control">
                                        </select>
                                    </div>

                                    <div class="form-group">
                                        <label for="txtName">Scheme Name</label><span style="color: Red">*</span>
<%--                                        <input type="text" runat="server" class="form-control" autocomplete="off" id="Text1" name="txtName" />--%>
                                        <input type="text" runat="server" class="form-control " id="txtName" name="txtName" />
                                    </div>
                                </div>                                                              
                            </div>
                        </div>
                    </div>
                     <br />
                    <div class="row">
                        <div class="col-md-5  stOnly">
                            <a href="#" class="list-group-item active state" style="width: 95%;">States<input title="toggle all" type="checkbox" class="all pull-right" /></a>
                            <div class="list-group" id="state" style="border: 1px solid #ddd">
                            </div>
                        </div>
                        <div class="col-lg-offset-6 disOnly" style="max-width: 500px;">
                            <a href="#" class="list-group-item active distributor" style="width: 95%;">Distributor<input title="toggle all" type="checkbox" class="all pull-right" /></a>
                            <div class="list-group" id="distributor" style="border: 1px solid #ddd">
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-inline">
                                <%--<div class="form-group" style="display:none;">
                                    <label for="txtName">Scheme Name</label>
                                    <input type="text" runat="server" class="form-control " id="txtName" name="txtName" />
                                </div>--%>

                                <div class="form-group">
                                </div>
                            </div>
                            <div class="form-inline">
                                <div class="form-group" style="display: none">
                                    <label for="ddlSubDiv">Division</label>
                                    <asp:DropDownList ID="subdiv" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label for="ddlType">Type:</label>
                                    <select id="ddlType" class="form-control">
                                        <option selected="selected" value="1">Categorywise</option>
                                        <option value="2">SKUwise</option>
                                    </select>
                                </div>
                                <div class="form-group">
                                    <label for="txtFrom">From:</label>
                                    <input type="text" runat="server" class="form-control  datetimepicker" id="txtFrom" name="txtFrom" />
                                </div>
                                <div class="form-group">
                                    <label for="txtTo">To:</label>
                                    <input type="text" runat="server" class="form-control  datetimepicker" id="txtTo" name="txtTo" />
                                </div>
                                <div class="form-group">
                                    <button name="btnClear" type="button" class="btn btn-primary btnClear" style="width: 70px; margin-top: 34px;">Clear</button>
                                    <asp:Button ID="Upldbt" CssClass="btn btn-primary" runat="server" Text="Excel File" Style="margin-top: 33px; margin-left: 15px;" OnClick="lnkDownload_Click" />
                                </div>


                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row" id="Exlupd">
                        <div class="col-md-9" style="text-align: right">
                            <input type="file" id="excelfile" style="float: right; margin-top: 2px;" />
                        </div>
                        <div class="col-md-3">
                            <button name="btnImport" type="button" class="btn btn-primary btnImport" style="width: 170px; margin-bottom: 8px;">Fill Data From Excel </button>
                        </div>
                    </div>
                    <%--<div class="row">
                        <div class="col-md-12">
                            <table id="tbl" class="table table-bordered newStly">
                                <thead></thead>
                                <tbody></tbody>
                            </table>
                        </div>
                    </div>   --%>               

                    <div class="row">
                        <div class="col-md-12" style="text-align: center">
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-md-12">
                            <table id="schmTbl" class="table table-bordered newStly">
                                <thead></thead>
                                <tbody></tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>

        </div>


        <div class="row">
            <div class="table-responsive col-xs-12">
                <table id="tbl" class="table table-bordered newStly">
                    <thead></thead>
                    <tbody></tbody>
                    <tfoot style="height:60px"></tfoot>
                </table>
            </div>            
        </div>

        <%--<table id="tbl" class="table table-bordered newStly">
            <thead></thead>
            <tbody></tbody>
		  <tfoot style="height:60px"></tfoot>
        </table>--%>



        <div class="form-inline footr">
            <div class="form-group" style="margin-left: 55px;">
                <button name="btnsave" type="button" class="btn btn-primary btnsave" style="width: 100px">Save</button></div>
            <div class="form-group">
                <button name="btnDelete" type="button" class="btn btn-primary btnDelete" style="width: 100px">Delete</button></div>
        </div>
    </form>
</asp:Content>


