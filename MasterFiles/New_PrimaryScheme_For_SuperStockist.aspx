<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="New_PrimaryScheme_For_SuperStockist.aspx.cs" Inherits="MasterFiles_New_PrimaryScheme_For_SuperStockist " %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link rel="stylesheet" type="text/css" media="all" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/smoothness/jquery-ui.css" />
    <link rel="stylesheet" href="https://netdna.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css" />
    <link href="https://netdna.bootstrapcdn.com/bootstrap/3.0.3/css/bootstrap.min.css" rel="stylesheet" id="bootstrap-css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css" />

    <link href="../css/bootstrap-slider/RsPer.css" rel="stylesheet" />
    <%--<link href="../css/bootstrap-slider/Switch.css" rel="stylesheet" type="text/css" />--%>

    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.0/jquery.min.js" type="text/javascript"></script>

    <script type="text/javascript" src="../js/lib/filldata.js"></script>
    -<script type="text/javascript" src="../js/lib/import_data.js"></script>
    <script type="text/javascript" src="../js/lib/xls.core.min.js"></script>
    <script type="text/javascript" src="../js/lib/xlsx.core.min.js"></script>

    <script type="text/javascript">
        $(document)
            .ready(function () { $('.dropdown').dropdown({ on: 'click' }); });
    </script>

    <script type="text/javascript">

        var save = []; var View_unit_details = [];

        $(document).ready(function () {

            var today = '';
            $('.btnDelete').prop('disabled', 'disabled');
            var insertType = '1';
            $('.datetimepicker').datepicker({ dateFormat: 'dd/mm/yy', startDate: '-3d', minDate: 0 });
            $('.datetimepicker1').datepicker({ dateFormat: 'dd/mm/yy', startDate: '-3d', minDate: 0 });
            var today1 = new Date();
            var dd = today1.getDate();
            var mm = today1.getMonth() + 1; //January is 0!

            var yyyy = today1.getFullYear();
            if (dd < 10) {
                dd = '0' + dd;
            }
            if (mm < 10) {
                mm = '0' + mm;
            }
            today = dd + '/' + mm + '/' + yyyy;
            $('#<%=txtFrom.ClientID%>').val(today);
           // $('#<%=txtTo.ClientID%>').val(today);

            var calc_erp = 0; Allrate = []; Schme_stockist = []; aaa = []; var Checked_stk = '';
            var Div_Code = ("<%=Session["div_code"].ToString()%>");

            $('#<%=Upldbt.ClientID%>').click(function () {
                var subdiv = $('#ddlDiv').val();
                if (subdiv == "0") {
                    alert('Select Division');
                    return false;
                }
            });


            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_PrimaryScheme_For_SuperStockist.aspx/getDivision",
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

            $(document).on('keypress', '#txtScheme, #txtFree, #txtDiscount', function (e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });

            function isValidDate(s) {
                var bits = s.split('/');
                var d = new Date(bits[2] + '/' + bits[1] + '/' + bits[0]);
                return !!(d && (d.getMonth() + 1) == bits[1] && d.getDate() == Number(bits[0]));
            }

            $(document).on('change', '.datetimepicker ', function () {
                date_calc($(this).val());
            });

            function date_calc(date) {

                if (date.length > 0) {
                    if (isValidDate(date)) {
                        var fit_start_time = date;
                        var FromDate = fit_start_time.split("/").reverse().join("-");
                        var dts = new Date(FromDate);
                        var cr = dts.getDate() + '/' + (dts.getMonth() + 1) + '/' + dts.getFullYear();
                        $('.datetimepicker1').datepicker("destroy");
                        $('.datetimepicker1').datepicker({ dateFormat: 'dd/mm/yy', minDate: fit_start_time, defaultDate: fit_start_time });
                        $('.datetimepicker1').val('');
                    }
                    else {
                        alert('Invalid Date Enter or Select Correct Date..!');
                        $(this).val('');
                        $(this).focus();

                    }
                }
            }

            $(document).on('change', '#<%=txtName.ClientID %>', function () {

                var sch = $(this).val();

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "New_PrimaryScheme_For_SuperStockist.aspx/Validate_Scheme_Name",
                    data: "{'Scheme_Name':'" + sch + "'}",
                    dataType: "json",
                    success: function (data) {
                        Scheme_Name = [];
                        Scheme_Name = JSON.parse(data.d);
                        if (Scheme_Name.length > 0) {
                            alert('Scheme Name Already Exists');
                            $('#<%=txtName.ClientID%>').val('');
                            return false;
                        }
                    },
                    error: function (jqXHR, exception) {
                        console.log(jqXHR);
                        console.log(exception);
                    }
                });
            });


            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                data: "{'Div_Code':'" + Div_Code + "'}",
                url: "New_PrimaryScheme_For_SuperStockist.aspx/getreate",
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
                url: "New_PrimaryScheme_For_SuperStockist.aspx/get_view_unit",
                dataType: "json",
                success: function (data) {
                    View_unit_details = JSON.parse(data.d) || [];
                },
                error: function (result) {
                }
            });

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_PrimaryScheme_For_SuperStockist.aspx/Get_Product_unit",
                dataType: "json",
                success: function (data) {
                    All_unit = JSON.parse(data.d) || [];
                },
                error: function (result) {
                }
            });

            function Get_unit(code) {

                var filter_unit = []; units = ""; units1 = "";
                units += "<option value='0'>Select Unit</option>";

                filter_unit = All_unit.filter(function (w) {
                    return (code == w.Product_Detail_Code);
                });

                if (filter_unit.length > 0) {

                    for (var z = 0; z < filter_unit.length; z++) {
                        units += "<option value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";

                    }
                }
                return units
            }

            $('#ddlType').val(2);
            //genTable = function () {
            //    type = $('#ddlType').val();
            //    $.ajax({
            //        type: "POST",
            //        contentType: "application/json; charset=utf-8",
            //        async: false,
            //        url: "New_PrimaryScheme_For_SuperStockist.aspx/getProduct",
            //        data: "{'Type':'" + type + "'}",
            //        dataType: "json",
            //        success: function (data) {
            //            ProDtl = data.d;
            //            str = '';
            //            tbl = $('#tbl');
            //            tbl1 = $('#tbl1');
            //            $(tbl).find('thead tr').remove();
            //            $(tbl).find('tbody tr').remove();
            //            $(tbl1).find('thead tr').remove();
            //            $(tbl1).find('tbody tr').remove();
            //            if (ProDtl.length > 0) {
            //                if (type == "2") {

            //                    var ofPro = ProDtl.filter(item => item.pTypes == "O");                                                                
            //                    str = `<th style="width: 24%;">Product Name</th><th style="width: 14%;">Scheme Unit</th><th style="width: 0%;">Scheme </th><th style="width: 13%;">Free_Unit</th><th>Free</th><th style="width: 10%;">Discount</th><th style="width: 10%;">Dis Value <div><p class="onoff"><input type="checkbox" class="ddlDis" value="1" id="ddlDis"/><label for="ddlDis" class="ddlDislbl"></label></p></div></th><th>Package Offer</th><th>Against Product</th><th>Offer Product</th><th>Add</th>`;
            //                    $(tbl1).find('thead').append('<tr>' + str + '</tr>');

            //                    var ofStr = '<option value="0">Select</option>';
            //                    if (ProDtl.length > 0) {
            //                        ProDtl.forEach((obj) => ofStr += `<option value=${obj.pCode}>${obj.pName}</option>`);
            //                    }

            //                    for (var i = 0; i < ProDtl.length; i++) {

            //                        Get_unit(ProDtl[i].pCode);                                    
            //                        str = '<td style="vertical-align: middle;padding: 2px 2px;"><input type="hidden" class="AutoID" name="AutoID" value="0"} /> <input type="hidden" name="pCode" value="' + ProDtl[i].pCode + '"/>  <span>' + ProDtl[i].pName + '</span></td><td style="display:none"><input type="hidden" class="erp_scheme_value" value="0"></td><td style="display:none"><input type="hidden" class="Scheme_erp_code" value="0" ></td>';
            //                        str += `<td><select id='Scheme_unit' class="form-control unit" name="unit" >${units}</select></td>`;
            //                        str += '<td style="padding: 2px 2px; vertical-align: middle;"><input type="text" style="width: 100%;" autocomplete="off" id="txtScheme" name="txtScheme" ></td>';
            //                        str += `<td><select id='free_unit' class="form-control unit" name="free_unit" >${units}</select></td>`;                                    
            //                        str += `<td style="padding: 2px 2px;vertical-align: middle;"><input type="text" style="width: 100%;" id="txtFree" name="txtFree" ></td><td style="display:none"><input type="hidden" class="erp_free_value" value="0"></td><td style="display:none;"><input type="hidden" class="Free_erp_code" value="3"></td><td style="padding: 2px 2px; vertical-align: middle;"><input type="text" style="width: 100%;" id="txtDiscount" name="txtDiscount" /></td><td style="padding: 2px 42px; vertical-align: middle;"><p class="onoff"><input type="checkbox" class="ddlDisClass" name="checked" value="1" id="ddlDisClass${(i + 1)}"/><label for="ddlDisClass${(i + 1)}" class="ddlDisClasslbl"></label></p></td><td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" class="packages" value="1" id="checkboxID` + (i + 1) + `" /><label class="packageslbl" for="checkboxID` + (i + 1) + `"></label></p></td>`;
            //                        str += `<td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" class="against"  value="1" id="against${(i + 1)}"/><label for="against${(i + 1)}" class="againstlbl"></label></p></td>`;
            //                        str += `<td style="padding: 2px 2px; vertical-align: middle;"><select class="form-control" disabled="disabled" name="allType" >${ofStr}</select></td><td style="padding: 2px 2px; vertical-align: middle;""><button type="button"  class="btn btn-default addpro btn-md"><span class="glyphicon glyphicon-plus" style="color:#378b2c"></span></button></td>`;
            //                        $(tbl1).find('tbody').append('<tr id=' + (i + 1) + ' class=' + ProDtl[i].pCode + '>' + str + '</tr>');

            //                    }
            //                }
            //                else {

            //                    str = '<th>Category Name</th><th>Scheme </th><th>Free</th><th>Discount%</th><th>Package Offer</th>';
            //                    $(tbl1).find('thead').append('<tr>' + str + '</tr>');
            //                    for (var i = 0; i < ProDtl.length; i++) {
            //                        str = '<td style="vertical-align: middle;padding: 2px 2px;"> <input type="hidden" name="pCode" value="' + ProDtl[i].pCode + '"/>  <span>' + ProDtl[i].pName + '</span></td>';
            //                        str += '<td style="padding: 2px 2px;    vertical-align: middle;"><input style="width: 96%;" type="text" autocomplete="off" id="txtScheme" name="txtScheme" /></td><td style="padding: 2px 2px; vertical-align: middle;"><input style="width: 97%;" type="text" id="txtFree" name="txtFree" ></td><td style="padding: 2px 2px; vertical-align: middle;"><input style="width: 89%;" type="text"  id="txtDiscount" name="txtDiscount" /></td><td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" class="packages" value="1" id="checkboxID' + (i + 1) + '"/><label class="packageslbl" for="checkboxID' + (i + 1) + '"></label></p></td>';
            //                        $(tbl1).find('tbody').append('<tr>' + str + '</tr>');
            //                    }
            //                }
            //            }
            //            else {
            //                alert('No Product This Division..!');
            //            }
            //        },
            //        error: function (jqXHR, exception) {
            //            console.log(jqXHR);
            //            console.log(exception);
            //        }
            //    });
            //}

            function genTable(ProDtl) {

                type = 2;
                str = ''; var Filter_view_unit = [];
                tbl1 = $('#tbl1');
                $(tbl1).find('thead tr').remove();
                $(tbl1).find('tbody tr').remove();
                if (ProDtl.length > 0) {
                    if (type == "2") {

                        var ofPro = ProDtl.filter(item => item.pTypes == "O");
                        str = `<th style="width: 24%;">Product Name</th><th style="width: 14%;">Scheme Unit</th><th style="width: 0%;">Scheme </th><th style="width: 13%;">Free_Unit</th><th>Free</th><th style="width: 10%;">Discount</th><th style="width: 10%;">Dis Value <div><p class="onoff"><input type="checkbox" class="ddlDis" value="1" id="ddlDis"/><label for="ddlDis" class="ddlDislbl"></label></p></div></th><th>Package Offer</th><th>Against Product</th><th>Offer Product</th><th>Add</th>`;
                        $(tbl1).find('thead').append('<tr>' + str + '</tr>');

                        var ofStr = '<option value="0">Select</option>';
                        if (ProDtl.length > 0) {
                            ProDtl.forEach((obj) => ofStr += `<option value=${obj.pCode}>${obj.pName}</option>`);
                        }

                        for (var i = 0; i < ProDtl.length; i++) {

                            Filter_view_unit = View_unit_details.filter(function (t) {
                                return (t.PCode == ProDtl[i].pCode && t.UOM_Id == ProDtl[i].product_Default_umo);
                            });

                            if (Filter_view_unit.length > 0) {

                                var con = Filter_view_unit[0].CnvQty;
                            }
                            else {
                                var con = 0;
                            }

                            Get_unit(ProDtl[i].pCode);
                            str = '<td style="vertical-align: middle;padding: 2px 2px;"><input type="hidden" class="AutoID" name="AutoID" value="0"} /><input type="hidden" name="pCode" value="' + ProDtl[i].pCode + '"/><span>' + ProDtl[i].pName + '</span></td><td style="display:none"><input type="hidden" class="prd_sale_unit" value=' + ProDtl[i].product_Sale_unit_Code + '></td><td style="display:none"><input type="hidden"  class="prd_unit" value=' + ProDtl[i].product_unit_Code + '></td><td style="display:none"><input type="hidden" class="prd_sample_erp_code" value=' + ProDtl[i].Sample_Erp_Code + '></td><td style="display:none"><input type="hidden" class="prd_umoweight" value=' + ProDtl[i].product_umo_weight + '></td><td style="display:none"><input type="hidden" class="Default_con_fac" value=' + con + '></td><td style="display:none"><input type="hidden" class="erp_scheme_value" value="0"></td><td style="display:none"><input type="hidden" class="Scheme_erp_code" value="0" ></td>';
                            str += `<td><select id='Scheme_unit' class="form-control unit" name="unit" >${units}</select></td>`;
                            str += '<td style="padding: 2px 2px; vertical-align: middle;"><input type="text" style="width: 100%;" autocomplete="off" id="txtScheme" name="txtScheme" ></td>';
                            str += `<td><select id='free_unit' class="form-control unit" name="free_unit" >${units}</select></td>`;
                            str += `<td style="padding: 2px 2px;vertical-align: middle;"><input type="text" style="width: 100%;" id="txtFree" name="txtFree" ></td><td style="display:none"><input type="hidden" class="erp_free_value" value="0"></td><td style="display:none;"><input type="hidden" class="Free_erp_code" value="3"></td><td style="padding: 2px 2px; vertical-align: middle;"><input type="text" style="width: 100%;" id="txtDiscount" name="txtDiscount" /></td><td style="padding: 2px 42px; vertical-align: middle;"><p class="onoff"><input type="checkbox" class="ddlDisClass" name="checked" value="1" id="ddlDisClass${(i + 1)}"/><label for="ddlDisClass${(i + 1)}" class="ddlDisClasslbl"></label></p></td><td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" class="packages" value="1" id="checkboxID` + (i + 1) + `" /><label class="packageslbl" for="checkboxID` + (i + 1) + `"></label></p></td>`;
                            str += `<td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" class="against"  value="1" id="against${(i + 1)}"/><label for="against${(i + 1)}" class="againstlbl"></label></p></td>`;
                            str += `<td style="padding: 2px 2px; vertical-align: middle;"><select class="form-control" disabled="disabled" name="allType" >${ofStr}</select></td><td style="padding: 2px 2px; vertical-align: middle;""><button type="button"  class="btn btn-default addpro btn-md"><span class="glyphicon glyphicon-plus" style="color:#378b2c"></span></button></td>`;
                            $(tbl1).find('tbody').append('<tr id=' + (i + 1) + ' class=' + ProDtl[i].pCode + '>' + str + '</tr>');
                        }
                    }
                    else {

                        str = '<th>Category Name</th><th>Scheme </th><th>Free</th><th>Discount%</th><th>Package Offer</th>';
                        $(tbl1).find('thead').append('<tr>' + str + '</tr>');
                        for (var i = 0; i < ProDtl.length; i++) {
                            str = '<td style="vertical-align: middle;padding: 2px 2px;"> <input type="hidden" name="pCode" value="' + ProDtl[i].pCode + '"/>  <span>' + ProDtl[i].pName + '</span></td>';
                            str += '<td style="padding: 2px 2px;    vertical-align: middle;"><input style="width: 96%;" type="text" autocomplete="off" id="txtScheme" name="txtScheme" /></td><td style="padding: 2px 2px; vertical-align: middle;"><input style="width: 97%;" type="text" id="txtFree" name="txtFree" ></td><td style="padding: 2px 2px; vertical-align: middle;"><input style="width: 89%;" type="text"  id="txtDiscount" name="txtDiscount" /></td><td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" class="packages" value="1" id="checkboxID' + (i + 1) + '"/><label class="packageslbl" for="checkboxID' + (i + 1) + '"></label></p></td>';
                            $(tbl1).find('tbody').append('<tr>' + str + '</tr>');
                        }
                    }
                }
                else {
                    alert('No Product This Division..!');
                }
            }
            //genTable();

            $(document).on('change', '.ddlDis ', function () {

                var c = $(this).closest('th').find('[type=checkbox]').attr("checked") ? 'Y' : 'N';
                if (c == 'Y') {
                    $('.ddlDisClass').closest('tr').find('input:checkbox[name="checked"]').attr("checked", true)
                }
                else {
                    $('.ddlDisClass').closest('tr').find('input:checkbox[name="checked"]').attr("checked", false)
                }

            });

            genSchemTbale = function () {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "New_PrimaryScheme_For_SuperStockist.aspx/getScheme",
                    dataType: "json",
                    success: function (data) {
                        SchDtl = data.d;
                        str = '';
                        tbl = $('#schmTbl');
                        $(tbl).find('thead tr').remove();
                        $(tbl).find('tbody tr').remove();
                        if (SchDtl.length > 0) {

                            str = '<th>SlNo.</th><th>Scheme Name</th><th>View</th>';
                            $(tbl).find('thead').append('<tr>' + str + '</tr>');
                            for (var i = 0; i < SchDtl.length; i++) {
                                //str = '<td style="vertical-align: middle;" ><span>' + (i + 1) + '</span></td><td style="vertical-align: middle;"><span>' + SchDtl[i].sName + '</span></td>';
                                str = '<td style="vertical-align: middle;" ><span>' + (i + 1) + '</span></td><td style="display:none;" class="scheme_sub_div" id="scheme_sub_div">' + SchDtl[i].sName + '</td><td style="vertical-align: middle;"><span>' + SchDtl[i].sName + '</span></td>';
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
            genSchemTbale();
            Get_Product_Details("0", 0);
            var stkDtl = [];
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_PrimaryScheme_For_SuperStockist.aspx/getDistributor_for_primary",
                dataType: "json",
                success: function (data) {
                    stkDtl = data.d;
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
                url: "New_PrimaryScheme_For_SuperStockist.aspx/getState",
                dataType: "json",
                success: function (data) {
                    sDt = data.d;
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

            var TerrDtl = [];
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_PrimaryScheme_For_SuperStockist.aspx/getTerritory_for_Secondary",
                dataType: "json",
                success: function (data) {
                    TerrDtl = JSON.parse(data.d);
                },
                error: function (jqXHR, exception) {
                    console.log(jqXHR);
                    console.log(exception);
                }
            });

            $('#ddlDiv').change(function () {

                var selected_div_code = $('#ddlDiv option:selected').val();
                var selected_div_Name = $('#ddlDiv option:selected').text();
                document.getElementById("<%= hidden_div.ClientID %>").value = selected_div_code;
                Get_Product_Details(selected_div_code, 0);
            });

            function Get_Product_Details(sub_div, type) {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "New_PrimaryScheme_For_SuperStockist.aspx/getProduct",
                    data: "{'Type':'" + 2 + "','SubdivCode':'" + sub_div + "'}",
                    dataType: "json",
                    success: function (data) {
                        ProDtl = data.d;
                        if (type == 0) { genTable(ProDtl); }
                    },
                    error: function (result) {
                    }
                });
            }

            $('.stOnly .all').click(function (e) {

                e.stopPropagation();
                var $this = $(this);
                if ($this.is(":checked")) {
                    $('#state').find("[type=checkbox]").prop("checked", true);
                }
                else {
                    $('#state').find("[type=checkbox]").prop("checked", false);
                    $this.prop("checked", false);
                    $('.disOnly .all').prop("checked", false);
                }
                $('#<%=txtTo.ClientID%>').val('');
                genTerr();
                genDist();

            });

            $('.disOnly .all').click(function (e) {
                e.stopPropagation();
                var $this = $(this);
                if ($this.is(":checked")) {
                    $('#distributor').find("[type=checkbox]").each(function (u) {
                        if ($(this).hasClass('dis')) {
                            $(this).prop("checked", false);
                        }
                        else {
                            $(this).prop("checked", true);
                        }
                    });
                }
                else {
                    $('#distributor').find("[type=checkbox]").prop("checked", false);
                    $this.prop("checked", false);
                }
            });


            $('[type=checkbox]').click(function (e) {
                e.stopPropagation();
            });

            $('.stOnly .list-group a .chk').click(function (e) {
               <%-- e.stopPropagation();
                var $this = $(this);
                if ($this.is(":checked")) {
                    $('#<%=txtTo.ClientID%>').val('');
                    Checked_stk = '';
                    $('.disOnly .list-group a .chk').each(function (t) {
                        var $this = $(this);
                        if ($this.is(":checked")) {
                            Checked_stk += $this.attr('name') + ',';
                        }
                    });

                    $this.prop("checked", true);
                }
                else {
                    Checked_stk = '';
                    $('.disOnly .list-group a .chk').each(function (t) {
                        var $this = $(this);
                        if ($this.is(":checked")) {
                            Checked_stk += $this.attr('name') + ',';
                        }
                    });
                    $this.prop("checked", false);
                    if (insertType == 1) {
                        $('#<%=txtTo.ClientID%>').val('');
                    }
                }
                if ($this.hasClass("all")) {
                    $this.trigger('click');
                }
                genDist();--%>


                e.stopPropagation();

                var $this = $(this);
                if ($this.is(":checked")) {
                    $this.prop("checked", true);
                }
                else {
                    $this.prop("checked", false);
                }

                $('.terrOnly .list-group a .chk').each(function (t) {
                    var $this = $(this);
                    if ($this.is(":checked")) {
                        checked_terr += $this.attr('name') + ',';
                    }
                });

                if ($this.hasClass("all")) {
                    $this.trigger('click');
                }
                //genTerr();
                genDist();
            });

            //$(document).on("click", ".stOnly .list-group a .chk", function (e) {

            //    e.stopPropagation();

            //    var $this = $(this);
            //    if ($this.is(":checked")) {
            //        $this.prop("checked", true);
            //    }
            //    else {
            //        $this.prop("checked", false);
            //    }

            //    $('.terrOnly .list-group a .chk').each(function (t) {
            //        var $this = $(this);
            //        if ($this.is(":checked")) {
            //            checked_terr += $this.attr('name') + ',';
            //        }
            //    });

            //    if ($this.hasClass("all")) {
            //        $this.trigger('click');
            //    }
            //    genTerr();
            //    genDist();
            //});

            $(document).on("click", ".terrOnly .list-group a .chk", function (e) {

                e.stopPropagation();
                var $this = $(this);
                if ($this.is(":checked")) {
                    $this.prop("checked", true);
                }
                else {
                    Checked_stk = '';
                    $this.prop("checked", false);
                }

                $('.disOnly .list-group a .chk').each(function (t) {
                    var $this = $(this);
                    if ($this.is(":checked")) {
                        Checked_stk += $this.attr('name') + ',';
                    }
                });

                if ($this.hasClass("all")) {
                    $this.trigger('click');
                }

                $('.disOnly .list-group a .chk').each(function (t) {
                    var $this = $(this);
                    if ($this.is(":disabled")) {
                        Given_distributor += $this.attr('name') + ',';
                    }
                });

                genDist(0);
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

            //genDist = function () {
            //    if (stkDtl.length > 0) {
            //        var div = $('#distributor');
            //        $(div).find('a').remove();
            //        var items = $("#state input:checked");
            //        items.each(function (idx, item) {
            //            Dis = stkDtl.filter(function (obj) {
            //                return obj.stCode == $(item).attr('name');
            //            });
            //            if (Dis.length > 0) {
            //                for (var i = 0; i < Dis.length; i++) {
            //                    if ((',' + Checked_stk).indexOf(',' + Dis[i].DistCode + ',') > -1) {
            //                        str = '<a href="#" class="list-group-item">' + Dis[i].DistName + '<input type="checkbox" name=' + Dis[i].DistCode + ' class="chk pull-right" checked/></a>';
            //                    }
            //                    else {
            //                        str = '<a href="#" class="list-group-item">' + Dis[i].DistName + '<input type="checkbox" name=' + Dis[i].DistCode + ' class="chk pull-right"/></a>';
            //                    }

            //                    $(div).append(str);
            //                }
            //            }
            //        });

            //        if ($("#distributor input").length <= 0) {
            //            str = '<a href="#" class="list-group-item">No Distributor</a>';
            //            $(div).append(str);

            //        }
            //        if (insertType == 2) {
            //            let uStockist = [...new Set(scData.map(item => item.Stockist_Code))];
            //            var stk = uStockist[0].split(',')
            //            if (uStockist.length > 0) {
            //                for (var i = 0; i < stk.length; i++) {
            //                    $('#distributor a').each(function () {
            //                        if ($(this).find('input[type=checkbox]').attr('name') == stk[i]) {
            //                            $(this).find('input[type=checkbox]').prop("checked", true);
            //                        }
            //                    });

            //                }
            //            }
            //        }
            //    }
            //}

            function genTerr() {
                if (TerrDtl.length > 0) {
                    var div = $('#territory');
                    $(div).find('a').remove();
                    var items = $("#state input:checked");
                    items.each(function (idx, item) {
                        Ter = TerrDtl.filter(function (obj) {
                            return obj.state_code == $(item).attr('name');
                        });
                        if (Ter.length > 0) {
                            for (var i = 0; i < Ter.length; i++) {
                                if ((',' + checked_terr).indexOf(',' + Ter[i].Territory_code + ',') > -1) {
                                    str = '<a href="#" class="list-group-item">' + Ter[i].Territory_name + '<input type="checkbox" name=' + Ter[i].Territory_code + ' class="chk pull-right" checked  /></a>';
                                }
                                else {
                                    str = '<a href="#" class="list-group-item">' + Ter[i].Territory_name + '<input type="checkbox" name=' + Ter[i].Territory_code + ' class="chk pull-right"/></a>';
                                }
                                $(div).append(str);
                            }
                        }
                    });

                    $('.terrOnly .list-group a .chk').each(function (idx, item) {
                        if ((',' + Given_territory).indexOf(',' + $(item).attr('name') + ',') > -1) {

                            $(item).prop("checked", true);
                            //  $(item).attr("disabled", "true");
                            $(item).addClass('Ter');
                        }
                    });

                    if ($("#territory input").length <= 0) {
                        str = '<a href="#" class="list-group-item">No Territory</a>';
                        $(div).append(str);
                    }
                }
            }

            function genDist(typ) {

                //if (stkDtl.length > 0) {
                //    var div = $('#distributor');
                //    $(div).find('a').remove();
                //    var items = $("#territory input:checked");
                //    items.each(function (idx, item) {
                //        Dis = stkDtl.filter(function (obj) {
                //            return obj.Terr_Code == $(item).attr('name');
                //        });
                //        if (Dis.length > 0) {
                //            for (var i = 0; i < Dis.length; i++) {
                //                if ((',' + Checked_stk).indexOf(',' + Dis[i].DistCode + ',') > -1) {
                //                    str = '<a href="#" class="list-group-item">' + Dis[i].DistName_Erp + '<input type="checkbox" name=' + Dis[i].DistCode + ' class="chk pull-right" checked/></a>';
                //                }
                //                else {
                //                    str = '<a href="#" class="list-group-item">' + Dis[i].DistName_Erp + '<input type="checkbox" name=' + Dis[i].DistCode + ' class="chk pull-right"/></a>';
                //                }

                //                $(div).append(str);
                //            }
                //        }
                //    });

                //    $('.disOnly .list-group a .chk').each(function (idx, item) {
                //        if ((',' + Given_distributor).indexOf(',' + $(item).attr('name') + ',') > -1) {

                //            $(item).prop("checked", true);
                //            $(item).attr("disabled", "true");
                //            $(item).addClass('dis');
                //        }
                //    })

                //    if ($("#distributor input").length <= 0) {
                //        str = '<a href="#" class="list-group-item">No Distributor</a>';
                //        $(div).append(str);

                //    }
                //}

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
                                if ((',' + Checked_stk).indexOf(',' + Dis[i].DistCode + ',') > -1) {
                                    str = '<a href="#" class="list-group-item">' + Dis[i].DistName + '<input type="checkbox" name=' + Dis[i].DistCode + ' class="chk pull-right" checked/></a>';
                                }
                                else {
                                    str = '<a href="#" class="list-group-item">' + Dis[i].DistName + '<input type="checkbox" name=' + Dis[i].DistCode + ' class="chk pull-right"/></a>';
                                }

                                $(div).append(str);
                            }
                        }
                    });

                    if ($("#distributor input").length <= 0) {
                        str = '<a href="#" class="list-group-item">No Distributor</a>';
                        $(div).append(str);

                    }
                    if (insertType == 2) {
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
                }
            }

            $(document).on('keyup', '#txt_state', function () {

                var search_txt = $(this).val();
                var plantt = $("#state input");

                $('#state .list-group-item').each(function (idx, val) {
                    if ($(this).text() != null && $(this).text().toString().toLowerCase().indexOf(search_txt) > -1) {
                        $(this).css('display', 'block');
                    }
                    else {
                        $(this).css('display', 'none');
                    }

                });
            });


            $(document).on('keyup', '#txt_dis', function () {

                var search_txt = $(this).val();
                var plantt = $("#distributor input");

                $('#distributor .list-group-item').each(function (idx, val) {
                    if ($(this).text() != null && $(this).text().toString().toLowerCase().indexOf(search_txt) > -1) {
                        $(this).css('display', 'block');
                    }
                    else {
                        $(this).css('display', 'none');
                    }

                });
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
                var selected_drop = $(this).closest('tr').find('select[name="allType"] :selected').val();
                var check = $(this).closest('tr').find('.against').attr("checked") ? 'Y' : 'N';

                if (check != 'N') {
                    if (selected_drop == "0") {
                        alert('Please Select Against Product');
                        return false;
                    }
                    else {
                        clonedRow.attr('id', Math.random())
                        clonedRow.find('input[type="text"]').val('');
                        //var selected_unit = $(this).closest('tr').find('td').find('select[name="unit"]').val();
                        var selected_unit = $(this).closest('tr').find('td').find('select[name="unit"] :selected').text();


                        clonedRow.find('select[name="unit"]').find('option').each(function () {
                            if ($(this).text().toLowerCase() == selected_unit.toLowerCase()) {
                                $(this).prop('selected', true);
                            }
                        });

                        //clonedRow.find('select[name="unit"]').val(selected_unit);

                        clonedRow.find('.erp_scheme_value').val('0');
                        clonedRow.find('.erp_free_value').val('0');
                        clonedRow.find('select[name = "unit"]').prop('disabled', 'disabled');
                        clonedRow.find('.AutoID').val(clCount);
                        clonedRow.find('td').eq(0).find('span').text('');
                        clonedRow.find('td').eq(0).css('text-align', 'center');
                        clonedRow.find('[type=checkbox]').prop("checked", false);
                        clonedRow.find('select[name = "allType"]').prop('disabled', 'disabled');
                        var disClassId = clonedRow.find('.ddlDisClass').attr('id') + clCount;
                        clonedRow.find('.ddlDisClass').attr('id', disClassId);
                        clonedRow.find('.ddlDisClasslbl').attr('for', disClassId);
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
                    }
                }
                else {

                    clonedRow.attr('id', Math.random())
                    clonedRow.find('input[type="text"]').val('');
                    //  var selected_unit = $(this).closest('tr').find('td').find('select[name="unit"]').val();
                    var selected_unit = $(this).closest('tr').find('td').find('select[name="unit"] :selected').text();

                    //clonedRow.find('select[name="unit"]').val(selected_unit);


                    //  clonedRow.find('select[name="unit"]').find('option').each(function () {
                    //      if ($(this).text().toLowerCase() == selected_unit.toLowerCase()) {
                    //          $(this).prop('selected', true);
                    //      }
                    //  });

                    // clonedRow.find('select[name="unit"]').prop('disabled', 'disabled');

                    clonedRow.find('.erp_scheme_value').val('0');
                    clonedRow.find('.erp_free_value').val('0');
                    clonedRow.find('.AutoID').val(clCount);
                    clonedRow.find('td').eq(0).find('span').text('');
                    clonedRow.find('td').eq(0).css('text-align', 'center');
                    clonedRow.find('[type=checkbox]').prop("checked", false);
                    clonedRow.find('select[name = "allType"]').prop('disabled', 'disabled');
                    var disClassId = clonedRow.find('.ddlDisClass').attr('id') + clCount;
                    clonedRow.find('.ddlDisClass').attr('id', disClassId);
                    clonedRow.find('.ddlDisClasslbl').attr('for', disClassId);
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

                }
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

           <%-- fillData = function (scData) {

                $('#<%=txtName.ClientID %>').val(scData[0].schemeName);
                $('#<%=txtFrom.ClientID %>').val(scData[0].FDate);
                date_calc(scData[0].FDate);
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


                type = $('#ddlType').val();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "New_PrimaryScheme_For_SuperStockist.aspx/getProduct",
                    data: "{'Type':'" + type + "'}",
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
                tbl1 = $('#tbl1');
                $(tbl).find('thead tr').remove();
                $(tbl).find('tbody tr').remove();
                $(tbl1).find('thead tr').remove();
                $(tbl1).find('tbody tr').remove();

                var ofPro = ProDtl.filter(item => item.pTypes == "O");
                //  ProDtl = ProDtl.filter(item => item.pTypes != "O");
                //str = '<th style="width: 24%;">Product Name</th><th style="width: 14%;">Scheme Unit</th><th style="width: 0%;">Scheme </th><th style="width: 13%;">Free_Unit</th><th>Free</th><th>Discount%</th><th>Package Offer</th><th>Against Product</th><th>Offer Product</th><th>Add</th>';
                //str = '<th style="width: 24%;">Product Name</th><th style="width: 14%;">Scheme Unit</th><th style="width: 0%;">Scheme </th><th style="width: 13%;">Free_Unit</th><th>Free</th><th>Discount%</th><th>Package Offer</th><th>Against Product</th><th>Offer Product</th><th>Add</th>';
                str = `<th style="width: 24%;">Product Name</th><th style="width: 14%;">Scheme Unit</th><th style="width: 0%;">Scheme </th><th style="width: 13%;">Free_Unit</th><th>Free</th><th style="width: 10%;">Discount</th><th style="width: 10%;">Dis Value <div><p class="onoff"><input type="checkbox" class="ddlDis" value="1" id="ddlDis"/><label for="ddlDis" class="ddlDislbl"></label></p></div></th><th>Package Offer</th><th>Against Product</th><th>Offer Product</th><th>Add</th>`;
                $(tbl1).find('thead').append('<tr>' + str + '</tr>');

                var ofStr = `<option value="0">Select</option>`;
                if (ProDtl.length > 0) {
                    ProDtl.forEach((obj) => ofStr += `<option value=${obj.pCode}>${obj.pName}</option>`);
                }
                var prdCodeex = '';

                var stkCode = scData[0].Stockist_Code;
                var stateCode = scData[0].State_Code;
                var stkDt = scData.filter(lst => (lst.Stockist_Code == stkCode || lst.Stockist_Code == '') && (lst.State_Code == stateCode || lst.State_Code == ''));
                var maids = 0, mid = 0;
                stkDt.forEach((element, index, array) => {

                    Get_unit(element.pCode);

                    var pkg = '';
                    var agp = '';
                    var agPro = element.AgProduct;
                    var select_sche_unit = element.Scheme_Unit;
                    var select_free_unit = element.Free_Unit;
                    var ppcod = element.pCode;
                    var ppanem = element.pName;

                    var disVal = '';

                    var cal_sche = element.scheme;
                    var cal_fre = element.free;
                    var erp = element.Sample_Erp_Code;

                    var edit_sche = []; var edit_free_unit = [];

                    edit_sche = Allrate.filter(function (y) {
                        return (y.Product_Detail_Code == ppcod && y.Move_MailFolder_Name == select_sche_unit);
                    });

                    if (edit_sche.length > 0) {

                        if (edit_sche[0].Product_Sale_Unit == select_sche_unit) {

                            var calc_Scheme_erp_value = cal_sche;
                            var calc_Scheme_erp = edit_sche[0].Sample_Erp_Code;

                        }
                        else {
                            var calc_Scheme_erp_value = cal_sche * edit_sche[0].Sample_Erp_Code;
                            var calc_Scheme_erp_code = edit_sche[0].Sample_Erp_Code;
                        }
                    }

                    edit_free_unit = Allrate.filter(function (y) {
                        return (y.Product_Detail_Code == ppcod && y.Move_MailFolder_Name == select_free_unit);
                    });

                    if (edit_free_unit.length > 0) {

                        if (edit_free_unit[0].Product_Sale_Unit == select_free_unit) {

                            var calc_free_erp_value = cal_fre;
                            var calc_free_erp = edit_free_unit[0].Sample_Erp_Code;

                        }
                        else {
                            var calc_free_erp_value = cal_fre * edit_free_unit[0].Sample_Erp_Code;
                            var calc_free_erp_code = edit_free_unit[0].Sample_Erp_Code;
                        }
                    }



                    if (Number(element.AutoID) > 0) {
                        maids = element.AutoID;
                    }
                    else {
                        //maids = Number(mid) + 1;
                        //mid++;

                        maids = Number(maids) + 1;
                        //maids++;
                    }

                    if (element.Package == 'Y') {
                        pkg = 'checked';
                    }
                    if (element.Against == 'Y') {
                        agp = 'checked';
                    }

                    if (element.DisType == '%') {
                        disVal = 'checked'
                    }

                    if (prdCodeex != element.pCode) {
                        str = `<td style="vertical-align: middle;padding: 2px 2px;"> <input type="hidden" class="AutoID" name="AutoID" value=${maids} /><input type="hidden" name="pCode" value=${element.pCode} /><input type="hidden" name="actFlg" value=${element.actFlg} />   <span>${element.pName} </span></td><td style="display:none"><input type="hidden" class="erp_scheme_value" value=${calc_Scheme_erp_value}></td><td style="display:none"><input type="hidden" class="Scheme_erp_code" value=${calc_Scheme_erp_code} ></td><td><select class="form-control unit" name="unit" >${units}</select></td>`;
                        //str += `<td style="padding: 2px 2px; vertical-align: middle;"><input type="text" class="form-control " id="txtScheme" name="txtScheme" value="${element.scheme}" /></td><td><select class="form-control unit" name="free_unit" >${units}</select></td><td style="padding: 2px 2px;    vertical-align: middle;"><input type="text" class="form-control " id="txtFree" name="txtFree"  value=${element.free} /></td><td style="display:none"><input type="hidden" class="erp_free_value" value=${calc_free_erp_value}></td><td style="display:none;"><input type="hidden" class="Free_erp_code" value=${calc_free_erp_code} ></td><td style="padding: 2px 2px; vertical-align: middle;"><input type="text" id="txtDiscount" name="txtDiscount"  value=${element.discount} /></td><td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" class="packages" ${pkg} value="1" id="checkboxID${index + 1}"/><label class="packageslbl" for="checkboxID${index + 1}"></label></p></td>`;
                        str += `<td style="padding: 2px 2px; vertical-align: middle;"><input type="text" class="form-control " id="txtScheme" name="txtScheme" value="${element.scheme}" /></td><td><select class="form-control unit" name="free_unit" >${units}</select></td><td style="padding: 2px 2px;    vertical-align: middle;"><input type="text" class="form-control " id="txtFree" name="txtFree"  value=${element.free} /></td><td style="display:none"><input type="hidden" class="erp_free_value" value=${calc_free_erp_value}></td><td style="display:none;"><input type="hidden" class="Free_erp_code" value=${calc_free_erp_code} ></td><td style="padding: 2px 2px; vertical-align: middle;"><input type="text" id="txtDiscount" name="txtDiscount"  value=${element.discount} /></td><td style="padding: 2px 42px; vertical-align: middle;"><p class="onoff"><input type="checkbox" class="ddlDisClass" name="checked" ${disVal} value="1" id="ddlDisClass${(index + 1)}"/><label for="ddlDisClass${(index + 1)}" class="ddlDisClasslbl"></label></p></td><td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" class="packages" ${pkg} value="1" id="checkboxID${index + 1}"/><label class="packageslbl" for="checkboxID${index + 1}"></label></p></td>`;
                        str += `<td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" ${agp} class="against"  value="1" id="against${(index + 1)}"/><label for="against${(index + 1)}" class="againstlbl"></label></p></td>`;
                        str += `<td style="padding: 2px 2px; vertical-align: middle;"><select class="form-control" disabled="disabled" name="allType" >${ofStr}</select></td><td style="padding: 2px 2px; vertical-align: middle;""><button type="button"  class="btn btn-default addpro btn-md"><span class="glyphicon glyphicon-plus  " style="color:#378b2c"></span></button></td>`;
                        $(tbl1).find('tbody').append('<tr id=' + maids + ' class=' + element.pCode + '>' + str + '</tr>');

                        if (element.Against == 'Y') {
                            $(tbl1).find('tr').eq(index + 1).find('select[name="allType"]').prop('disabled', '');
                        }
                        else {
                            $(tbl1).find('tr').eq(index + 1).find('select[name="allType"]').prop('disabled', 'disabled');
                        }
                        $(tbl1).find('tr').eq(index + 1).find('select[name="allType"]').val(agPro);

                        $(tbl1).find('tr').eq(index + 1).find('select[name="unit"] option').each(function () {
                            if ($(this).text() == select_sche_unit) {
                                $(this).prop("selected", true);
                            }
                        });

                        $(tbl1).find('tr').eq(index + 1).find('select[name="free_unit"] option').each(function () {
                            if ($(this).text() == select_free_unit) {
                                $(this).prop("selected", true);
                            }
                        });

                        // $(tbl1).find('tr').eq(index + 1).find('select[name="unit"] option:selected').text(select_sche_unit);
                        // $(tbl1).find('tr').eq(index + 1).find('select[name="free_unit"] option:selected').text(select_free_unit);
                        prdCodeex = element.pCode;
                    }
                    else {

                        str = `<td style="vertical-align: middle;padding: 2px 2px; text-align:center"><input type="hidden" class="AutoID" name="AutoID" value=${maids} /> <input type="hidden" name="pCode" value=${element.pCode} /><input type="hidden" name="actFlg" value=${element.actFlg} />   <span></span></td><td style="display:none"><input type="hidden" class="erp_scheme_value" value=${calc_Scheme_erp_value}></td><td style="display:none"><input type="hidden" class="Scheme_erp_code" value=${calc_Scheme_erp_code} ></td>`; //value=${element.Sample_Erp_Code}
                        //str += `<td><select class="form-control unit" name="unit" >${units}</select></td><td style="padding: 2px 2px; vertical-align: middle;"><input type="text" class="form-control " id="txtScheme" name="txtScheme" value=${element.scheme} /></td><td><select class="form-control unit" name="free_unit" >${units}</select></td><td style="display:none"><input type="hidden" class="erp_free_value" value=${calc_free_erp_value}></td><td style="display:none;"><input type="hidden" class="Free_erp_code" value=${calc_free_erp_code} ></td><td style="padding: 2px 2px; vertical-align: middle;"><input type="text" class="form-control " id="txtFree" name="txtFree"  value=${element.free} /></td><td style="padding: 2px 2px;    vertical-align: middle;"><input type="text" class="form-control " id="txtDiscount" name="txtDiscount"  value=${element.discount} /></td><td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" class="packages" ${pkg} value="1" id="checkboxID${index + 1}"/><label class="packageslbl" for="checkboxID${index + 1}"></label></p></td>`;
                        str += `<td><select class="form-control unit" name="unit" >${units}</select></td><td style="padding: 2px 2px; vertical-align: middle;"><input type="text" class="form-control " id="txtScheme" name="txtScheme" value=${element.scheme} /></td><td><select class="form-control unit" name="free_unit" >${units}</select></td><td style="display:none"><input type="hidden" class="erp_free_value" value=${calc_free_erp_value}></td><td style="display:none;"><input type="hidden" class="Free_erp_code" value=${calc_free_erp_code} ></td><td style="padding: 2px 2px; vertical-align: middle;"><input type="text" class="form-control " id="txtFree" name="txtFree"  value=${element.free} /></td><td style="padding: 2px 2px;    vertical-align: middle;"><input type="text" class="form-control " id="txtDiscount" name="txtDiscount"  value=${element.discount} /></td><td style="padding: 2px 42px; vertical-align: middle;"><p class="onoff"><input type="checkbox" class="ddlDisClass" name="checked" ${disVal} value="1" id="ddlDisClass${(index + 1)}"/><label for="ddlDisClass${(index + 1)}" class="ddlDisClasslbl"></label></p></td><td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" class="packages" ${pkg} value="1" id="checkboxID${index + 1}"/><label class="packageslbl" for="checkboxID${index + 1}"></label></p></td>`;
                        str += `<td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" ${agp} class="against"  value="1" id="against${(index + 1)}"/><label for="against${(index + 1)}" class="againstlbl"></label></p></td>`;
                        str += `<td style="padding: 2px 2px; vertical-align: middle;"><select class="form-control" disabled="disabled" name="allType" >${ofStr}</select></td><td style="padding: 2px 2px; vertical-align: middle;""><button type="button"  class="btn btn-default delpro btn-md"><span class="glyphicon glyphicon-minus  " style="color:#378b2c"></span></button></td>`;
                        $(tbl1).find('tbody').append('<tr id=' + Math.random() + ' class=' + element.pCode + '>' + str + '</tr>');

                        if (element.Against == 'Y') {
                            $(tbl1).find('tr').eq(index + 1).find('select[name="allType"]').prop('disabled', '');
                        }
                        else {
                            $(tbl1).find('tr').eq(index + 1).find('select[name="allType"]').prop('disabled', 'disabled');
                        }
                        $(tbl1).find('tr').eq(index + 1).find('select[name="allType"]').val(agPro);


                        $(tbl1).find('tr').eq(index + 1).find('select[name="unit"] option').each(function () {
                            if ($(this).text() == select_sche_unit) {
                                $(this).prop("selected", true);
                            }
                        });

                        $(tbl1).find('tr').eq(index + 1).find('select[name="free_unit"] option').each(function () {
                            if ($(this).text() == select_free_unit) {
                                $(this).prop("selected", true);
                            }
                        });

                        //  $(tbl1).find('tr').eq(index + 1).find('select[name="unit"] option:selected').text(select_sche_unit);
                        //$(tbl1).find('tr').eq(index + 1).find('select[name="free_unit"] option:selected').text(select_free_unit);
                        prdCodeex = element.pCode;
                    }

                });
                $('.btnDelete').prop('disabled', '');
            }--%>



            fillData = function (scData, sub_d) {

                Checked_stk = ''; checked_state = ''; checked_terr = '';

                $('#<%=txtName.ClientID %>').val(scData[0].schemeName);
                $('#<%=txtFrom.ClientID %>').val(scData[0].FDate);
                date_calc(scData[0].FDate);
                $('#<%=txtTo.ClientID %>').val(scData[0].TDate);

                //if (scData[0].sub_div_code.length > 0) {
                //    $('#ddlDiv').val(scData[0].sub_div_code);
                //}

                $('#<%=txtName.ClientID %>').prop('disabled', 'disabled');
                //$("#plant input").prop("checked", false);
                //let uplant = [...new Set(scData.map(item => item.Plant_Code))];
                //var plt = uplant[0].split(',')
                //if (uplant.length > 0) {
                //    for (var i = 0; i < plt.length; i++) {
                //        $('#plant a').each(function () {
                //            if ($(this).find('input[type=checkbox]').attr('name') == plt[i]) {
                //                $(this).find('input[type=checkbox]').prop("checked", true);
                //            }
                //        });

                //    }
                //}
                //genState();

                let uState = [...new Set(scData.map(item => item.State_Code))];
                var st = uState[0].split(',')
                if (uState.length > 0) {
                    for (var i = 0; i < st.length; i++) {
                        if (st[i] != "") { checked_state += st[i] + ','; }
                        $('#state a').each(function () {
                            if ($(this).find('input[type=checkbox]').attr('name') == st[i]) {
                                $(this).find('input[type=checkbox]').prop("checked", true);
                            }
                        });

                    }
                }
                //genTerr();

                //let uTerr = [...new Set(scData.map(item => item.Terr_Code))];
                //var trry = uTerr[0].split(',')
                //if (uTerr.length > 0) {
                //    for (var i = 0; i < trry.length; i++) {
                //        if (st[i] != "") { checked_terr += trry[i] + ','; }
                //        $('#territory a').each(function () {
                //            if ($(this).find('input[type=checkbox]').attr('name') == trry[i]) {
                //                $(this).find('input[type=checkbox]').prop("checked", true);
                //            }
                //        });

                //    }
                //}

                genDist(0);

                let uStockist = [...new Set(scData.map(item => item.Stockist_Code))];
                var stk = uStockist[0].split(',')
                if (uStockist.length > 0) {
                    for (var i = 0; i < stk.length; i++) {
                        if (st[i] != "") { Checked_stk += stk[i] + ','; }
                        $('#distributor a').each(function () {
                            if ($(this).find('input[type=checkbox]').attr('name') == stk[i]) {
                                $(this).find('input[type=checkbox]').prop("checked", true);
                            }
                        });

                    }
                }

                type = $('#ddlType').val();
                var sub_div = "";
                sub_div = $('#ddlDiv').val();
                if ((sub_div == "undefined" || sub_div == null || sub_div == "")) {
                    sub_div = "0";
                }
                Get_Product_Details(sub_div, 1);
                //Get_Product_Details(sub_d, 1);

                tbl = $('#tbl');
                tbl1 = $('#tbl1');
                $(tbl).find('thead tr').remove();
                $(tbl).find('tbody tr').remove();
                $(tbl1).find('thead tr').remove();
                $(tbl1).find('tbody tr').remove();

                var ofPro = ProDtl.filter(item => item.pTypes == "O");
                str = `<th style="width: 24%;">Product Name</th><th style="width: 14%;">Scheme Unit</th><th style="width: 0%;">Scheme </th><th style="width: 13%;">Free_Unit</th><th>Free</th><th style="width: 10%;">Discount</th><th style="width: 10%;">Dis Value <div><p class="onoff"><input type="checkbox" class="ddlDis" value="1" id="ddlDis"/><label for="ddlDis" class="ddlDislbl"></label></p></div></th><th>Package Offer</th><th>Against Product</th><th>Offer Product</th><th>Add</th>`;
                $(tbl1).find('thead').append('<tr>' + str + '</tr>');

                var ofStr = `<option value="0">Select</option>`;
                if (ProDtl.length > 0) {
                    ProDtl.forEach((obj) => ofStr += `<option value=${obj.pCode}>${obj.pName}</option>`);
                }
                var prdCodeex = '';

                var stkCode = scData[0].Stockist_Code;
                var stateCode = scData[0].State_Code;
                var stkDt = scData.filter(lst => (lst.Stockist_Code == stkCode || lst.Stockist_Code == '') && (lst.State_Code == stateCode || lst.State_Code == ''));
                var maids = 0, mid = 0; var Filter_edit_view = [];
                stkDt.forEach((element, index, array) => {

                    Get_unit(element.pCode);

                    var pkg = '';
                    var agp = '';
                    var agPro = element.AgProduct;
                    var select_sche_unit = element.Scheme_Unit;
                    var select_free_unit = element.Free_Unit;
                    var ppcod = element.pCode;
                    var ppanem = element.pName;

                    var disVal = '';

                    var cal_sche = element.scheme;
					var scheme_inpiece = element.sch_inp;
					var free_inpiece = element.Free_inp;
                    var cal_fre = element.free;
                    var erp = element.Sample_Erp_Code;

                    var edit_sche = []; var edit_free_unit = [];

                    edit_sche = Allrate.filter(function (y) {
                        return (y.Product_Detail_Code == ppcod && y.Move_MailFolder_Name == select_sche_unit);
                    });

                    if (edit_sche.length > 0) {

                        if (edit_sche[0].Product_Sale_Unit == select_sche_unit) {

                            var calc_Scheme_erp_value = scheme_inpiece;
                            var calc_Scheme_erp = edit_sche[0].Sample_Erp_Code;

                        }
                        else {
                            var calc_Scheme_erp_value = scheme_inpiece * edit_sche[0].Sample_Erp_Code;
                            var calc_Scheme_erp_code = edit_sche[0].Sample_Erp_Code;
                        }
                    }

                    edit_free_unit = Allrate.filter(function (y) {
                        return (y.Product_Detail_Code == ppcod && y.Move_MailFolder_Name == select_free_unit);
                    });

                    if (edit_free_unit.length > 0) {

                        if (edit_free_unit[0].Product_Sale_Unit == select_free_unit) {

                            var calc_free_erp_value = free_inpiece;
                            var calc_free_erp = edit_free_unit[0].Sample_Erp_Code;

                        }
                        else {
                            var calc_free_erp_value = free_inpiece * edit_free_unit[0].Sample_Erp_Code;
                            var calc_free_erp_code = edit_free_unit[0].Sample_Erp_Code;
                        }
                    }

                    Filter_edit_view = View_unit_details.filter(function (t) {
                        return (t.PCode == ppcod && t.UOM_Id == element.Default_umo);
                    });

                    if (Filter_edit_view.length > 0) {

                        var con = Filter_edit_view[0].CnvQty;
                    }
                    else {
                        var con = 0;
                    }

                    if (Number(element.AutoID) > 0) {
                        maids = element.AutoID;
                    }
                    else {
                        //maids = Number(mid) + 1;
                        //mid++;

                        maids = Number(maids) + 1;
                        //maids++;
                    }

                    if (element.Package == 'Y') {
                        pkg = 'checked';
                    }
                    if (element.Against == 'Y') {
                        agp = 'checked';
                    }

                    if (element.DisType == '%') {
                        disVal = 'checked'
                    }

                    if (prdCodeex != element.pCode) {
                        //str = `<td style="vertical-align: middle;padding: 2px 2px;"> <input type="hidden" class="AutoID" name="AutoID" value=${maids} /><input type="hidden" name="pCode" value=${element.pCode} /><input type="hidden" name="actFlg" value=${element.actFlg} /><span>${element.pName} </span></td><td style="display:none"><input class="prd_sale_unit" type="hidden" value=${element.Sale_unit_code}></td><td style="display:none"><input type="hidden" class="prd_unit" value=${element.unit_code}></td><td style="display:none"><input type="hidden" class="prd_sample_erp_code" value=${element.Sample_Erp_Code}></td><td style="display:none"><input type="hidden" class="prd_umoweight" value=${element.Umo_wegt}></td><td style="display:none"><input type="hidden" class="Default_con_fac" value=${con}></td><td style="display:none"><input type="hidden" class="erp_scheme_value" value=${calc_Scheme_erp_value}></td><td style="display:none"><input type="hidden" class="Scheme_erp_code" value=${calc_Scheme_erp_code} ></td><td><select class="form-control unit" name="unit" >${units}</select></td>`;
                        //str += `<td style="padding: 2px 2px; vertical-align: middle;"><input type="text" class="form-control " id="txtScheme" name="txtScheme" value="${element.scheme_in_piece}" /></td><td><select class="form-control unit" name="free_unit" >${units}</select></td><td style="padding: 2px 2px;    vertical-align: middle;"><input type="text" class="form-control " id="txtFree" name="txtFree"  value=${free_inpiece} ></td><td style="display:none"><input type="hidden" class="erp_free_value" value=${calc_free_erp_value}></td><td style="display:none;"><input type="hidden" class="Free_erp_code" value=${calc_free_erp_code} ></td><td style="padding: 2px 2px; vertical-align: middle;"><input type="text" id="txtDiscount" name="txtDiscount"  value=${element.discount_inP} /></td><td style="padding: 2px 42px; vertical-align: middle;"><p class="onoff"><input type="checkbox" class="ddlDisClass" name="checked" ${disVal} value="1" id="ddlDisClass${(index + 1)}"/><label for="ddlDisClass${(index + 1)}" class="ddlDisClasslbl"></label></p></td><td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" class="packages" ${pkg} value="1" id="checkboxID${index + 1}"/><label class="packageslbl" for="checkboxID${index + 1}"></label></p></td>`;
                        //str += `<td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" ${agp} class="against"  value="1" id="against${(index + 1)}"/><label for="against${(index + 1)}" class="againstlbl"></label></p></td>`;
                        //str += `<td style="padding: 2px 2px; vertical-align: middle;"><select class="form-control" disabled="disabled" name="allType" >${ofStr}</select></td><td style="padding: 2px 2px; vertical-align: middle;""><button type="button"  class="btn btn-default addpro btn-md"><span class="glyphicon glyphicon-plus  " style="color:#378b2c"></span></button></td>`;
                        //$(tbl1).find('tbody').append('<tr id=' + maids + ' class=' + element.pCode + '>' + str + '</tr>');

                        str = `<td style="vertical-align: middle;padding: 2px 2px;"> <input type="hidden" class="AutoID" name="AutoID" value=${maids} /><input type="hidden" name="pCode" value=${element.pCode} /><input type="hidden" name="actFlg" value=${element.actFlg} />   <span>${element.pName} </span></td><td style="display:none"><input type="hidden" class="erp_scheme_value" value=${calc_Scheme_erp_value}></td><td style="display:none"><input type="hidden" class="Scheme_erp_code" value=${calc_Scheme_erp_code} ></td><td><select class="form-control unit" name="unit" >${units}</select></td>`;
                        str += `<td style="padding: 2px 2px; vertical-align: middle;"><input type="text" class="form-control " id="txtScheme" name="txtScheme" value="${scheme_inpiece}" /></td><td><select class="form-control unit" name="free_unit" >${units}</select></td><td style="padding: 2px 2px;    vertical-align: middle;"><input type="text" class="form-control " id="txtFree" name="txtFree"  value=${free_inpiece} ></td><td style="display:none"><input type="hidden" class="erp_free_value" value=${calc_free_erp_value}></td><td style="display:none;"><input type="hidden" class="Free_erp_code" value=${calc_free_erp_code} ></td><td style="padding: 2px 2px; vertical-align: middle;"><input type="text" id="txtDiscount" name="txtDiscount"  value=${element.discount} /></td><td style="padding: 2px 42px; vertical-align: middle;"><p class="onoff"><input type="checkbox" class="ddlDisClass" name="checked" ${disVal} value="1" id="ddlDisClass${(index + 1)}"/><label for="ddlDisClass${(index + 1)}" class="ddlDisClasslbl"></label></p></td><td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" class="packages" ${pkg} value="1" id="checkboxID${index + 1}"/><label class="packageslbl" for="checkboxID${index + 1}"></label></p></td>`;
                        str += `<td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" ${agp} class="against"  value="1" id="against${(index + 1)}"/><label for="against${(index + 1)}" class="againstlbl"></label></p></td>`;
                        str += `<td style="padding: 2px 2px; vertical-align: middle;"><select class="form-control" disabled="disabled" name="allType" >${ofStr}</select></td><td style="padding: 2px 2px; vertical-align: middle;""><button type="button"  class="btn btn-default addpro btn-md"><span class="glyphicon glyphicon-plus  " style="color:#378b2c"></span></button></td>`;
                        $(tbl1).find('tbody').append('<tr id=' + maids + ' class=' + element.pCode + '>' + str + '</tr>');

                        if (element.Against == 'Y') {
                            $(tbl1).find('tr').eq(index + 1).find('select[name="allType"]').prop('disabled', '');
                        }
                        else {
                            $(tbl1).find('tr').eq(index + 1).find('select[name="allType"]').prop('disabled', 'disabled');
                        }
                        $(tbl1).find('tr').eq(index + 1).find('select[name="allType"]').val(agPro);

                        $(tbl1).find('tr').eq(index + 1).find('select[name="unit"] option').each(function () {
                            if ($(this).text() == select_sche_unit) {
                                $(this).prop("selected", true);
                            }
                        });

                        $(tbl1).find('tr').eq(index + 1).find('select[name="free_unit"] option').each(function () {
                            if ($(this).text() == select_free_unit) {
                                $(this).prop("selected", true);
                            }
                        });

                        prdCodeex = element.pCode;
                    }
                    else {

                        str = `<td style="vertical-align: middle;padding: 2px 2px; text-align:center"><input type="hidden" class="AutoID" name="AutoID" value=${maids} /> <input type="hidden" name="pCode" value=${element.pCode} /><input type="hidden" name="actFlg" value=${element.actFlg} /><span></span></td><td style="display:none"><input class="prd_sale_unit" type="hidden" value=${element.Sale_unit_code}></td><td style="display:none"><input type="hidden" class="prd_unit" value=${element.unit_code}></td><td style="display:none"><input type="hidden" class="prd_sample_erp_code" value=${element.Sample_Erp_Code}></td><td style="display:none"><input type="hidden" class="prd_umoweight" value=${element.Umo_wegt}></td><td style="display:none"><input type="hidden" class="Default_con_fac" value=${con}></td><td style="display:none"><input type="hidden" class="erp_scheme_value" value=${calc_Scheme_erp_value}></td><td style="display:none"><input type="hidden" class="Scheme_erp_code" value=${calc_Scheme_erp_code} ></td>`; //value=${element.Sample_Erp_Code}
                        str += `<td><select class="form-control unit" name="unit" >${units}</select></td><td style="padding: 2px 2px; vertical-align: middle;"><input type="text" class="form-control " id="txtScheme" name="txtScheme" value=${scheme_inpiece} /></td><td><select class="form-control unit" name="free_unit" >${units}</select></td><td style="display:none"><input type="hidden" class="erp_free_value" value=${calc_free_erp_value}></td><td style="display:none;"><input type="hidden" class="Free_erp_code" value=${calc_free_erp_code} ></td><td style="padding: 2px 2px; vertical-align: middle;"><input type="text" class="form-control " id="txtFree" name="txtFree"  value=${free_inpiece} ></td><td style="padding: 2px 2px;    vertical-align: middle;"><input type="text" class="form-control " id="txtDiscount" name="txtDiscount"  value=${element.discount_inP} /></td><td style="padding: 2px 42px; vertical-align: middle;"><p class="onoff"><input type="checkbox" class="ddlDisClass" name="checked" ${disVal} value="1" id="ddlDisClass${(index + 1)}"/><label for="ddlDisClass${(index + 1)}" class="ddlDisClasslbl"></label></p></td><td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" class="packages" ${pkg} value="1" id="checkboxID${index + 1}"/><label class="packageslbl" for="checkboxID${index + 1}"></label></p></td>`;
                        str += `<td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" ${agp} class="against"  value="1" id="against${(index + 1)}"/><label for="against${(index + 1)}" class="againstlbl"></label></p></td>`;
                        str += `<td style="padding: 2px 2px; vertical-align: middle;"><select class="form-control" disabled="disabled" name="allType" >${ofStr}</select></td><td style="padding: 2px 2px; vertical-align: middle;""><button type="button"  class="btn btn-default delpro btn-md"><span class="glyphicon glyphicon-minus  " style="color:#378b2c"></span></button></td>`;
                        $(tbl1).find('tbody').append('<tr id=' + Math.random() + ' class=' + element.pCode + '>' + str + '</tr>');

                        if (element.Against == 'Y') {
                            $(tbl1).find('tr').eq(index + 1).find('select[name="allType"]').prop('disabled', '');
                        }
                        else {
                            $(tbl1).find('tr').eq(index + 1).find('select[name="allType"]').prop('disabled', 'disabled');
                        }
                        $(tbl1).find('tr').eq(index + 1).find('select[name="allType"]').val(agPro);


                        $(tbl1).find('tr').eq(index + 1).find('select[name="unit"] option').each(function () {
                            if ($(this).text() == select_sche_unit) {
                                $(this).prop("selected", true);
                            }
                        });

                        $(tbl1).find('tr').eq(index + 1).find('select[name="free_unit"] option').each(function () {
                            if ($(this).text() == select_free_unit) {
                                $(this).prop("selected", true);
                            }
                        });

                        prdCodeex = element.pCode;
                    }

                });
                $('.btnDelete').prop('disabled', '');
            }

            var buttoncount = 0;
            $(document).on('click', '.btnsave', function () {

                var chk = true; buttoncount += 1;
                if (buttoncount == "1") { }

                var state = $("#state input:checked");
                if (state.length == 0) {
                    buttoncount = 0;
                    alert('Select State..!');
                    return false;
                }
                var dist = $("#distributor input:checked");
                if (dist.length == 0) {
                    buttoncount = 0;
                    alert('Select Distributor..!');
                    return false;
                }
                var fDate = $(".datetimepicker").val();
                var tDate = $(".datetimepicker1").val();
                if (fDate == "") {
                    buttoncount = 0;
                    alert('Enter From Date..!');
                    $("#txtFrom").focus();
                    return false;
                }
                if (tDate == "") {
                    buttoncount = 0;
                    alert('Enter To Date..!');
                    $("#txtFrom").focus();
                    return false;
                }

                var schemeName = $("#<%=txtName.ClientID%>").val();
                if (schemeName == "" || schemeName.length == '') {
                    buttoncount = 0;
                    alert('Enter Scheme Name..!');
                    $("#<%=txtName.ClientID%>").focus();
                    return false;
                }


                var arr = []; var prdList = ''; var of_pro = ''; var pNm = ''; var Scheme_count = 0; var free_count = 0; var batch = '';


                $('#tbl1').find('tbody').find('tr').each(function () {

                    var table_len = $('#tbl1').find('tbody').find('tr').length;

                    var tot_sche_len = 0;
                    $("[id*=txtScheme]").each(function () {

                        if ($(this).val() == "") {
                            tot_sche_len = tot_sche_len + 1;
                        }
                    });

                    var tot_sche_Unit_len = 0;
                    $('select[name = "unit"]').each(function () {

                        if ($(this).val() == "0") {
                            tot_sche_Unit_len = tot_sche_Unit_len + 1;
                        }
                    });

                    if (table_len == tot_sche_len && table_len == tot_sche_Unit_len) {
                        alert("Enter Atlease One Scheme");
                        chk = false;
                        return false;
                    }
                    if ($(this).find("input[name=txtScheme]").val() == '' && $(this).find('select[name = "unit"] option:selected').text() != 'Select Unit') {
                        chk = false;
                        buttoncount = 0;
                        alert('Enter Scheme!!!');
                        $('#ProductTable').focus();
                        return false;
                    }

                    if ($(this).find("input[name=txtFree]").val() == '' && $(this).find("input[name=txtDiscount]").val() == '' && $(this).find('select[name = "unit"] option:selected').text() != 'Select Unit') {
                        buttoncount = 0;
                        chk = false;
                        alert('Enter either Free or Discount');
                        $('#ProductTable').focus();
                        return false;


                        var ags = $(this).find('.against').attr("checked") ? 'Y' : 'N' == 'Y';

                        if (ags == 'Y' && $(this).find('select[name = "allType"]').val() == 0) {
                            chk = false;
                            alert("Select Againt Product");
                            return false;
                        }
                    }

                });
                if (chk != false) {

                    $('#tbl1').find('tbody').find('tr').each(function () {

                        prdList = $(this).find('input[name="pCode"]').val();
                        of_pro = $(this).find('select[name="allType"]').val();

                        ans = []; ans1 = [];

                        var ofer_unit = '';
                        if (of_pro != '0') {

                            ans = Allrate.filter(function (y) {
                                return (y.Product_Detail_Code == prdList);
                            });

                            ans1 = Allrate.filter(function (y) {
                                return (y.Product_Detail_Code == of_pro);
                            });

                            if (ans.length > 0) {

                                //    if (ans[0].product_unit == $(this).find('select[name="free_unit"]').val()) {

                                if (ans[0].product_unit == $(this).find('select[name="free_unit"] option:selected').text()) {

                                    ofer_unit = ans1[0].product_unit;
                                }
                                else {
                                    ofer_unit = ans1[0].Product_Sale_Unit;
                                }
                            }
                        }
                        pNm = '';
                        if ($(this).find('select[name="allType"]').val() != "0") {
                            pNm = $(this).find('select[name="allType"] :selected').text();
                        }
                        arr.push({
                            AutoId: $(this).find('input[name="AutoID"]').val() || 0,
                            pCode: $(this).find('input[name="pCode"]').val() || 0,
                            pName: $(this).find('input[name="pCode"], span').text().replace(/&/g, "&amp;"),
                            scheme_txt: $(this).find('input[name="txtScheme"]').val() || 0,
                            scheme: (isNaN($(this).find('.erp_scheme_value').val()) ? 0 : $(this).find('.erp_scheme_value').val()) || 0,
                            scheme_unit_code: $(this).find('select[name="unit"]').val(),
                            scheme_unit: $(this).find('select[name="unit"] option:selected').text(),
                            free: $(this).find('input[name="txtFree"]').val() || 0,
							free_in_piece: $(this).find('.erp_free_value').val() || 0,
                            free_Unit: ($(this).find('select[name="free_unit"] option:selected').text() == 'Select Unit') ? '' : $(this).find('select[name="free_unit"] option:selected').text(),
                            free_Unit_code: $(this).find('select[name="free_unit"]').val(),
                            discount: $(this).find('input[name="txtDiscount"]').val() || 0,
                            DisType: $(this).find('.ddlDisClass').attr("checked") ? '%' : 'Rs',
                            Package: $(this).find('.packages').attr("checked") ? 'Y' : 'N',
                            Against: $(this).find('.against').attr("checked") ? 'Y' : 'N' || '',
                            AgProduct: $(this).find('select[name="allType"]').val() || '',
                            offer_product_Unit_code: ($(this).find('.against').attr("checked") == "checked") ? $(this).find('select[name="free_unit"] option:selected').val() : '',
                            actFlg: '0',
                            Offer_Product_Name: pNm,
                            offer_unit: ofer_unit || '0'
                        });
                    });

                    stC = '';
                    state.each(function (idx, item) {
                        stC += $(item).attr('name') + ',';
                    });

                    dsC = '';
                    dist.each(function (idx, item) {
                        dsC += $(item).attr('name') + ',';
                    });

                    var FDate = $('#<%=txtFrom.ClientID%>').val();
                    var TDate = $('#<%=txtTo.ClientID%>').val();
                    var Types = $('#ddlType').val();
                    var SchemeName = $('#<%=txtName.ClientID%>').val();

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "New_PrimaryScheme_For_SuperStockist.aspx/SaveScheme",
                        data: "{'Data':'" + JSON.stringify(arr) + "','DistCode':'" + dsC + "','StateCode':'" + stC + "','FDate':'" + FDate + "','TDate':'" + TDate + "','SchemeName':'" + SchemeName + "','Types':'" + Types + "','insertType':'" + insertType + "'}",
                        dataType: "json",
                        success: function (data) {
                            if (data.d == 'Success') {

                                alert('Scheme Saved Successfully');
                                insertType = '1';
                                $('#<%=txtName.ClientID %>').prop('disabled', '');
                                $('#<%=txtName.ClientID %>').val('');
                                $('.btnDelete').prop('disabled', 'disabled');
                                $('#ddlType').val(0);
                                $('.stOnly').find("[type=checkbox]").prop("checked", false);
                                genDist();
                                genTable();
                                genSchemTbale();
                                $('#<%=txtFrom.ClientID%>').val(today);
                                $('#<%=txtTo.ClientID%>').val('');
                                $('.datetimepicker').datepicker({ dateFormat: 'dd/mm/yy', startDate: '-3d', minDate: 0 });
                            }
                            else {
                                alert(data.d);
                            }
                        },
                        error: function (exception) {
                            buttoncount = 0;
                            console.log(exception);
                        }
                    });
                }
            });

            //$(document).on('keypress', '#tbl input[type = "text"]', function (event) {
            //    if (event.which != 8 && isNaN(String.fromCharCode(event.which))) {
            //        event.preventDefault();
            //    }
            //});

            function filter(pcode, selected_unit) {

                save = Allrate.filter(function (y) {
                    return (y.Product_Detail_Code == pcode && y.Move_MailFolder_Name == selected_unit);
                });
                return save;
            }


            $(document).on('change', '#tbl1 select[name = "unit"]', function (event) {

                // var selected_unit = $(this).val();
                var selected_unit = $(this).closest('tr').find('#Scheme_unit option:selected').text();
                var pcode = $(this).closest('tr').find('[name="pCode"]').val();
                var scheme_val = $(this).closest('tr').find('input[name = "txtScheme"]').val();

                //if (selected_unit == 0) {
                //    alert('Please Select Correct Scheme Unit');
                //    $(this).closest('tr').find('#txtScheme').val('');
                //    return false;

                //}

                filter(pcode, selected_unit);

                if (save[0].Move_MailFolder_Name == save[0].Product_Sale_Unit) {

                    $(this).closest('tr').find('.Scheme_erp_code').val(1);
                    $(this).closest('tr').find('.erp_scheme_value').val(scheme_val);
                }
                else {

                    $(this).closest('tr').find('.Scheme_erp_code').val(save[0].Sample_Erp_Code);
                    $(this).closest('tr').find('.erp_scheme_value').val(((save.length > 0) ? save[0].Sample_Erp_Code : 0) * scheme_val);
                }

            });


            $(document).on('change', '#tbl1 input[name = "txtScheme"]', function (event) {

                var row = $(this).closest("tr");
                var Given_scheme = $(this).val();
                var pro_code = row.find('[name="pCode"]').val();
                var tis_id = row.attr('id');

                var sel_unit = row.find('select[name="unit"]').val();
                var sel_unit_name = row.find('select[name="unit"] option:selected').text();

                if (sel_unit == '0') {
                    alert('Please Select Scheme Umo');
                    $(this).closest('tr').find('[name="txtScheme"]').val('');
                    return false;
                }

                filter(pro_code, sel_unit_name);

                if (save[0].Move_MailFolder_Name == save[0].Product_Sale_Unit) {
                    calc_erp = Given_scheme;
                    $(this).closest('tr').find('.erp_scheme_value').val(calc_erp);
                }
                else {
                    $(this).closest('tr').find('.erp_scheme_value').val(save[0].Sample_Erp_Code * Given_scheme);
                }

                // $(document).find('.' + pro_code).each(function () {

                //var le = $(document).find('.' + pro_code).length;
                //var id = $(this).attr('id');
                // var ppode = $(this).closest('tr').find('[name="pCode"]').val();
                // var sch = $(this).closest('tr').find('[name="txtScheme"]').val();

                // if (le > 1) {
                //     if (tis_id != id) {
                //         if (ppode == pro_code) {
                //             if (sch == Given_scheme) {
                //                 alert('Select Different Schemes');
                //                 row.find('[name="txtScheme"]').val('');
                //                 return false;
                //             }
                //         }
                //     }
                // }
                // });
            });

            $(document).on('change', '#tbl1 select[name = "free_unit"]', function (event) {

                $(this).closest('tr').find('#txtFree').val('');
                // var selected_unit = $(this).val();
                var selected_unit = $(this).closest('tr').find('#free_unit option:selected').text();
                var pcode = $(this).closest('tr').find('[name="pCode"]').val();
                var erp = $(this).closest('tr').find('.Scheme_erp_code').val();
                var scheme_val = $(this).closest('tr').find('input[name = "txtScheme"]').val();
				var free_val = $(this).closest('tr').find('input[name = "txtFree"]').val();

                filter(pcode, selected_unit);

                if (save[0].Move_MailFolder_Name == save[0].Product_Sale_Unit) {

                    $(this).closest('tr').find('.Free_erp_code').val(1);
                    $(this).closest('tr').find('.erp_free_value').val(free_val);
                }
                else {

                    $(this).closest('tr').find('.Free_erp_code').val(save[0].Sample_Erp_Code);
                    $(this).closest('tr').find('.erp_free_value').val(((save.length > 0) ? save[0].Sample_Erp_Code : 0) * free_val);
                }
            });

            $(document).on('change', '#tbl1 input[name = "txtFree"]', function (event) {

                var row = $(this).closest("tr");

                var tis_id = row.attr('id');
                var sche = row.find('input[name = "txtScheme"]').val();

                var cNum = Number(sche || 0);
                if (cNum < 1) {
                    alert('Enter Scheme..!');
                    row.find('input[name = "txtScheme"]').focus();
                    row.find('input[name="txtFree"]').val('');
                    //$(this).closest('tr').find('input[name = "txtDiscount"]').prop("disabled", false);
                    return false;
                }
                //  else {
                //      if ($(this).val() == '') {
                //          $(this).closest('tr').find('input[name = "txtDiscount"]').prop("disabled", false);
                //      } else {
                //           $(this).closest('tr').find('input[name = "txtDiscount"]').prop("disabled", true);
                //       }
                //   }

                var Given_free = $(this).val();
                var pro_code = row.find('[name="pCode"]').val();
                var sel_unit = row.find('select[name="free_unit"]').val();
                var sel_unit_name = row.find('select[name="free_unit"] option:selected').text();
                var Schem_unit = row.find('select[name="unit"]').val();
                var sch_erp = row.find('.erp_scheme_value').val();

                if (sel_unit == '0') {
                    alert('Please Select Free Umo');
                    row.find('[name="txtFree"]').val('');
                    // $(this).closest('tr').find('input[name = "txtDiscount"]').prop("disabled", false);
                    return false;
                }

                filter(pro_code, sel_unit_name);


                if (save[0].Move_MailFolder_Name == save[0].Product_Sale_Unit) {
                    $(this).closest('tr').find('.erp_free_value').val(Given_free);
                }
                else {
                    var con = save[0].Sample_Erp_Code;
                    $(this).closest('tr').find('.erp_free_value').val(con * Given_free);
                }

                var given_free_erp = row.find('.erp_free_value').val();

                $(document).find('.' + pro_code).each(function () {
                    // $(document).find('#tbl1').find('tr').each(function () {

                    var len = $(document).find('.' + pro_code).length;
                    var all_pcode = $(this).closest('tr').find('[name="pCode"]').val();
                    var id = $(this).attr('id');

                    if (len > 1) {

                        if (tis_id != id) {

                            if (all_pcode == pro_code) {

                                //  var all_selected_unit = $(this).closest('tr').find('select[name="unit"]').val();
                                var all_selected_unit = $(this).closest('tr').find('select[name="unit"] option:selected').text();
                                var scheds = $(this).closest('tr').find('input[name = "txtScheme"]').val();
                                var scheds_erp = $(this).closest('tr').find('.erp_scheme_value').val();
                                var fr = $(this).closest('tr').find('input[name = "txtFree"]').val();
                                var fr_erp = $(this).closest('tr').find('.erp_free_value').val();
                                // var fre_uni = $(this).closest('tr').find('select[name="free_unit"]').val();
                                var fre_uni = $(this).closest('tr').find('select[name="free_unit"] option:selected').text();

                                if (Number(scheds_erp) > Number(sch_erp)) {
                                    if (fr_erp != '0') {
                                        if (Number(given_free_erp) > Number(fr_erp) || Number(given_free_erp) >= Number(fr_erp)) {
                                            alert('Please Check The Scheme');
                                            row.find('[name="txtFree"]').val('');
                                            return false;
                                        }
                                    }
                                }
                                else {
                                    if (Number(given_free_erp) == Number(fr_erp) || Number(given_free_erp) < Number(fr_erp)) {
                                        alert('Please Check The Scheme');
                                        row.find('[name="txtFree"]').val('');
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                });
            });

            $(document).on('keyup', '#tbl1 input[name = "txtDiscount"]', function (event) {

                var sche = $(this).closest('tr').find('input[name = "txtScheme"]').val();
                //  var freeuniiit = $(this).closest('tr').find('select[name="free_unit"] option:selected').val();
                var freeuniiit = $(this).closest('tr').find('select[name="free_unit"] option:selected').text();
                var frq = $(this).closest('tr').find('input[name = "txtFree"]').val();
                var sel_unit = $(this).closest('tr').find('select[name="free_unit"]').val();

                var cNum = Number(sche || 0);

                if (cNum < 1) {
                    $(this).val('');
                    alert('Enter Scheme..!');
                    $(this).closest('tr').find('input[name = "txtScheme"]').focus();
                    $(this).closest('tr').find('input[name = "txtFree"]').prop("disabled", false);
                    return false;
                }
                // else {
                //    if ($(this).val() == '') {
                //       $(this).closest('tr').find('input[name = "txtFree"]').prop("disabled", false);
                //        $(this).closest('tr').find('select[name = "free_unit"]').prop("disabled", false);
                //    } else {
                //        $(this).closest('tr').find('input[name = "txtFree"]').prop("disabled", true);
                //         $(this).closest('tr').find('select[name = "free_unit"] option[value="0"]').attr("selected", true);
                //         $(this).closest('tr').find('select[name = "free_unit"]').prop("disabled", true)
                //    }
                // }		
            });

            $(document).on('click', '.btnImport', function () {
                ExportXLToTable(
                    function (data) {
                        xldata = data;
                        $('#ddlType').val(2);
                        //genTable();
                        //fillData1(xldata);
                        var subdiv = $('#ddlDiv').val();
                        //fillData1(xldata, subdiv);
                        fillData1(xldata);
                    },
                    function (Msg) {
                        console.log(Msg);
                    }
                )
            });

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
                $('#ddlType').val(0);
                $('.stOnly').find("[type=checkbox]").prop("checked", false);
                //$('.disOnly').find("[type=checkbox]").prop("checked", false);
                genDist();
                genTable();
                genSchemTbale();

            });

            $(document).on('click', '.btnDelete', function () {

                var Ans = confirm("Do you want to Delete Scheme?");
                if (Ans == true) {
                    var SchemeName = $('#<%=txtName.ClientID%>').val();
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "New_PrimaryScheme_For_SuperStockist.aspx/DeleteScheme",
                        data: "{'schemeName':'" + SchemeName + "'}",
                        dataType: "json",
                        success: function (data) {
                            if (data.d == 'Success') {
                                alert('Scheme Deleted Successfully ');
                                insertType = '1';
                                $('#<%=txtName.ClientID %>').prop('disabled', '');
                                $('#<%=txtName.ClientID %>').val('');
                                $('.btnDelete').prop('disabled', 'disabled');
                                $('#ddlType').val(0);
                                $('.stOnly').find("[type=checkbox]").prop("checked", false);
                                $('#<%=txtTo.ClientID%>').val('');
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
                }
                else {
                    return false;
                }

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
               // $('#<%=txtTo.ClientID%>').val(today);
            });


            //$(document).on('click', '.btnview', function () {
            //    $('#ddlType').val(2);
            //    insertType = '2';
            //    //    genTable();
            //    var scName = $(this).closest('tr').find('td').eq('1').text();
            //    $.ajax({
            //        type: "POST",
            //        contentType: "application/json; charset=utf-8",
            //        async: false,
            //        url: "New_PrimaryScheme_For_SuperStockist.aspx/getSchemeVAl",
            //        data: "{'schemeName':'" + scName + "'}",
            //        dataType: "json",
            //        success: function (data) {
            //            scData = data.d;
            //            fillData(scData);

            //            var ef = scData[0].FDate;
            //            var et = scData[0].TDate;

            //            if (today >= ef) {
            //                $('#ctl00_ContentPlaceHolder1_txtTo').prop("disabled", true);
            //                $('#ctl00_ContentPlaceHolder1_txtFrom').prop("disabled", true);
            //                $("#tbl1").find("input,button,textarea,select").attr("disabled", true);
            //            }
            //            else {
            //                $('#ctl00_ContentPlaceHolder1_txtTo').prop("disabled", false);
            //                $('#ctl00_ContentPlaceHolder1_txtFrom').prop("disabled", false);
            //                $("#tbl1").find("input,button,textarea,select").attr("disabled", false);
            //            }
            //        },
            //        error: function (exception) {
            //            console.log(exception);
            //        }
            //    });
            //});

<%--            $(document).on('click', '.btnview', function () {
                $('#ddlType').val(2);
                insertType = '2';
                var scName = $(this).closest('tr').find('td').eq('2').text();
                var sc_sub_div = $(this).closest('tr').find('.scheme_sub_div').text();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "New_PrimaryScheme_For_SuperStockist.aspx/getSchemeVAl",
                    data: "{'schemeName':'" + scName + "','subDiv':'" + sc_sub_div + "'}",
                    dataType: "json",
                    success: function (data) {
                        scData = data.d;
                        fillData(scData, sc_sub_div);
                        //$('#ddlDiv').val(scData[0].Sub_div);
                        $('#ddlDiv').val(scData[0].sub_div_code);
                        document.getElementById("<%= hidden_div.ClientID %>").value = scData[0].sub_div_code;
                        //$("#ddlDiv").prop("disabled", true);
                        var ef = scData[0].FDate;
                        var et = scData[0].TDate;
                    },
                    error: function (exception) {
                        console.log(exception);
                    }
                });
            });--%>


            $(document).on('click', '.btnview', function () {
                var subdiv = $('#ddlDiv').val();
                $('#ddlType').val(2);
                insertType = '2';
                //    genTable();
                var scName = $(this).closest('tr').find('td').eq('1').text();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "New_PrimaryScheme_For_SuperStockist.aspx/getSchemeVAl",
                    data: "{'schemeName':'" + scName + "'}",
                    dataType: "json",
                    success: function (data) {
                        scData = data.d;
                        fillData(scData, subdiv);

                        var ef = scData[0].FDate;
                        var et = scData[0].TDate;
                    },
                    error: function (exception) {
                        console.log(exception);
                    }
                });
            });


            $(document).on('change', '#ddlType', function () {
                genTable();
            });

            $(document).on('change', '.datetimepicker1', function () {

                var to_date = $(this).val();
                var from_date = $('.datetimepicker').val();
                var schename = $('#ctl00_ContentPlaceHolder1_txtName').val();

                var state = $("#state input:checked");
                $('.datetimepicker1').val('');
                if (state.length == 0) {
                    alert('Select State..!');
                    return false;
                }

                var Dis = $("#distributor input:checked");

                if (Dis.length == 0) {
                    alert('Select Distributor..!');
                    return false;
                }

                stC = '';
                state.each(function (idx, item) {
                    stC += $(item).attr('name') + ',';
                });


                var DisC = '';
                Dis.each(function (idx, item) {
                    DisC += $(item).attr('name') + ',';
                });

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "New_PrimaryScheme_For_SuperStockist.aspx/validateScheme",
                    data: "{'From_date':'" + from_date + "','State_Code':'" + stC + "','InsertType':'" + insertType + "','To_date':'" + to_date + "','Scheme_name':'" + schename + "','Dist_Code':'" + DisC + "'}",

                    dataType: "json",
                    success: function (data) {
                        Schme_stockist = JSON.parse(data.d) || [];

                        if (Schme_stockist.length > 0) {

                            $("#distributor input").removeAttr('disabled');

                            for (var z = 0; z < Schme_stockist.length; z++) {

                                var dist = $("#distributor input");
                                dist.each(function (idx, item) {

                                    if ($(item).attr('name') == Schme_stockist[z].stcode) {
                                        $(item).attr("disabled", "true");
                                        $(item).prop("checked", false);
                                        $(item).addClass('dis');
                                        $(item).attr('name').strike();
                                        $('#ctl00_ContentPlaceHolder1_txtTo').val(to_date);
                                        $('.disOnly .all').prop("checked", false);
                                    }
                                });

                            }
                            alert("Stockist Scheme Already Available From Date");
                        }
                        else {
                            var dist = $("#distributor input");
                            dist.each(function (idx, item) {
                                $(item).attr("disabled", false);
                                $(item).removeClass('dis');
                                $('#ctl00_ContentPlaceHolder1_txtTo').val(to_date);

                            });
                        }
                    },
                    error: function (exception) {
                        console.log(exception);
                    }
                });
            });

            //function fillData1(xldata, subdiv) {
            //    Xcel_Date = [];
            //    type = $('#ddlType').val();
            //    $.ajax({
            //        type: "POST",
            //        contentType: "application/json; charset=utf-8",
            //        async: false,
            //        url: "New_PrimaryScheme_For_SuperStockist.aspx/getProduct",
            //        //data: "{'Type':'" + type + "'}",
            //        data: "{'Type':'" + type + "','SubdivCode':'" + subdiv + "'}",
            //        dataType: "json",
            //        success: function (data) {
            //            ProDtl = data.d;
            //        },
            //        error: function (jqXHR, exception) {
            //            console.log(jqXHR);
            //            console.log(exception);
            //        }
            //    });


            //    tbl = $('#tbl');
            //    tbl1 = $('#tbl1');
            //    $(tbl).find('thead tr').remove();
            //    $(tbl).find('tbody tr').remove();
            //    $(tbl1).find('thead tr').remove();
            //    $(tbl1).find('tbody tr').remove();

            //    //var ofPro = ProDtl.filter(item => item.pTypes == "O");                
            //    var ofPro = xldata//.filter(item => item.pTypes == "O");
            //    xldata = xldata

            //    // ProDtl = ProDtl.filter(item => item.pTypes != "O");
            //    str = '<th style="width: 24%;">Product Name</th><th style="width: 14%;">Scheme Unit</th><th style="width: 0%;">Scheme </th><th style="width: 13%;">Free_Unit</th><th>Free</th><th>Discount%</th><th>Package Offer</th><th>Against Product</th><th>Offer Product</th><th>Add</th>';
            //    $(tbl1).find('thead').append('<tr>' + str + '</tr>');

            //    var ofStr = `<option value="0">Select</option>`;
            //    //if (ProDtl.length > 0) {
            //    //    ProDtl.forEach((obj) => ofStr += `<option value=${obj.pCode}>${obj.pName}</option>`);
            //    //}
            //    if (ofPro.length > 0) {
            //        //ofPro.forEach((obj) => ofStr += `<option value=${obj.pCode}>${obj.pName}</option>`);
            //        ofPro.forEach((obj) => ofStr += `<option value=${obj.ProductCode}>${obj.ProductName}</option>`);
            //    }
            //    var prdCodeex = '';
            //    var maids = 0, mid = 0;

            //    var res = [];

            //    /* for (var a = 0; a < ProDtl.length; a++) {*/
            //    for (var a = 0; a < ProDtl.length; a++) {

            //        res = xldata.filter(function (c) {
            //            //return (c["Product_Code"] == ProDtl[a].pCode);
            //            return (c["ProductCode"] == ProDtl[a].pCode);
            //        });

            //        if (res.length > 0) {

            //            for (var f = 0; f < res.length; f++) {

            //                Xcel_Date.push({                                
            //                    //pCode: res[f].Product_Code || '0',
            //                    //pName: res[f].Product_Name || '0',
            //                    pCode: res[f].ProductCode || '0',
            //                    pName: res[f].ProductName || '0',
            //                    Scheme_Unit: res[f].Scheme_Unit || '0',
            //                    scheme: res[f].Scheme || '0',
            //                    Free_Unit: res[f].Free_Unit || '0',
            //                    free: res[f].Free || '0',
            //                    discount: res[f].Discount || '0',
            //                    Package: res[f].Package || '0',
            //                    Against: res[f].Against || '0',
            //                    AgProduct: res[f].Offer_Product_code || '0',
            //                    Offer_Product_Name: res[f].Offer_Product_Name || '0',
            //                    Product_Sale_Unit: ProDtl[a].Product_Sale_Unit || '0',
            //                    product_unit: ProDtl[a].product_unit || '0',
            //                    Sample_Erp_Code: ProDtl[a].Sample_Erp_Code || '0',
            //                    pTypes: ProDtl[a].pTypes || '0'

            //                });
            //            }
            //        }
            //        else {

            //            Xcel_Date.push({
            //                pCode: ProDtl[a].pCode,
            //                pName: ProDtl[a].pName,
            //                Scheme_Unit: '0',
            //                scheme: '0',
            //                Free_Unit: '0',
            //                free: '0',
            //                discount: '0',
            //                Package: '0',
            //                Against: '0',
            //                AgProduct: '0',
            //                Offer_Product_Name: '0',
            //                Product_Sale_Unit: ProDtl[a].Product_Sale_Unit,
            //                product_unit: ProDtl[a].product_unit,
            //                Sample_Erp_Code: ProDtl[a].Sample_Erp_Code,
            //                pTypes: ProDtl[a].pTypes
            //            });
            //        }
            //    }

            //    Xcel_Date.forEach((element, index, array) => {

            //        var bindunit1 = '<option value="0">Select Unit</option>';
            //        bindunit1 = {};
            //        bindunit1 = '<option selected="selected" value="0">Select Unit</option>';
            //        bindunit1 += `<option value=${element.product_unit}>${element.product_unit}</option>`;
            //        bindunit1 += `<option  value=${element.Product_Sale_Unit}>${element.Product_Sale_Unit}</option>`;

            //        var pkg = '';
            //        var agp = '';
            //        var agPro = element.AgProduct;
            //        var select_sche_unit = element.Scheme_Unit;
            //        var select_free_unit = element.Free_Unit;
            //        var ppcod = element.pCode;
            //        var ppanem = element.pName;

            //        var cal_sche = element.scheme;
            //        var cal_fre = element.free;
            //        var erp = element.Sample_Erp_Code;

            //        var edit_sche = []; var edit_free_unit = [];

            //        edit_sche = Allrate.filter(function (y) {
            //            return (y.Product_Detail_Code == ppcod && y.Move_MailFolder_Name == select_sche_unit);
            //        });

            //        if (edit_sche.length > 0) {

            //            if (edit_sche[0].Product_Sale_Unit == select_sche_unit) {

            //                var calc_Scheme_erp_value = cal_sche;
            //                var calc_Scheme_erp = edit_sche[0].Sample_Erp_Code;

            //            }
            //            else {
            //                var calc_Scheme_erp_value = cal_sche * edit_sche[0].Sample_Erp_Code;
            //                var calc_Scheme_erp_code = edit_sche[0].Sample_Erp_Code;
            //            }
            //        }

            //        edit_free_unit = Allrate.filter(function (y) {
            //            return (y.Product_Detail_Code == ppcod && y.Move_MailFolder_Name == select_free_unit);
            //        });

            //        if (edit_free_unit.length > 0) {

            //            if (edit_free_unit[0].Product_Sale_Unit == select_free_unit) {

            //                var calc_free_erp_value = cal_fre;
            //                var calc_free_erp = edit_free_unit[0].Sample_Erp_Code;

            //            }
            //            else {
            //                var calc_free_erp_value = cal_fre * edit_free_unit[0].Sample_Erp_Code;
            //                var calc_free_erp_code = edit_free_unit[0].Sample_Erp_Code;
            //            }
            //        }


            //        if (Number(element.AutoID) > 0) {
            //            maids = element.AutoID;
            //        }
            //        else {
            //            maids = Number(maids) + 1;
            //        }
            //        console.log(maids);
            //        if (element.Package == 'Y') {
            //            pkg = 'checked';
            //        }
            //        if (element.Against == 'Y') {
            //            agp = 'checked';
            //        }

            //        //if (element.DisType == '%')
            //        //{

            //        //}

            //        if (prdCodeex != element.pCode) {

            //            str = `<td style="vertical-align: middle;padding: 2px 2px;"> <input type="hidden" class="AutoID" name="AutoID" value=${maids} /><input type="hidden" name="pCode" value=${element.pCode} /><input type="hidden" name="actFlg" value=${element.actFlg} />   <span>${element.pName} </span></td><td style="display:none"><input type="hidden" class="erp_scheme_value" value=${calc_Scheme_erp_value}></td><td style="display:none"><input type="hidden" class="Scheme_erp_code" value=${calc_Scheme_erp_code} ></td><td><select class="form-control unit" name="unit" >${units}</select></td>`;
            //            str += `<td style="padding: 2px 2px; vertical-align: middle;"><input type="text" class="form-control " id="txtScheme" name="txtScheme" value="${element.scheme}" /></td><td><select class="form-control unit" name="free_unit" >${units}</select></td><td style="padding: 2px 2px;    vertical-align: middle;"><input type="text" class="form-control " id="txtFree" name="txtFree"  value=${element.free} /></td><td style="display:none"><input type="hidden" class="erp_free_value" value=${calc_free_erp_value}></td><td style="display:none;"><input type="hidden" class="Free_erp_code" value=${calc_free_erp_code} ></td><td style="padding: 2px 2px; vertical-align: middle;"><input type="text" id="txtDiscount" name="txtDiscount"  value=${element.discount} /></td><td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" class="packages" ${pkg} value="1" id="checkboxID${index + 1}"/><label class="packageslbl" for="checkboxID${index + 1}"></label></p></td>`;
            //            str += `<td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" ${agp} class="against"  value="1" id="against${(index + 1)}"/><label for="against${(index + 1)}" class="againstlbl"></label></p></td>`;
            //            str += `<td style="padding: 2px 2px; vertical-align: middle;"><select class="form-control" disabled="disabled" name="allType" >${ofStr}</select></td><td style="padding: 2px 2px; vertical-align: middle;""><button type="button"  class="btn btn-default addpro btn-md"><span class="glyphicon glyphicon-plus  " style="color:#378b2c"></span></button></td>`;
            //            $(tbl1).find('tbody').append('<tr id=' + maids + ' class=' + element.pCode + '>' + str + '</tr>');



            //            if (element.Against == 'Y') {
            //                $(tbl1).find('tr').eq(index + 1).find('select[name="allType"]').prop('disabled', '');
            //                $(tbl1).find('tr').eq(index + 1).find('select[name="allType"]').val(agPro);
            //            }
            //            else {
            //                $(tbl1).find('tr').eq(index + 1).find('select[name="allType"]').prop('disabled', 'disabled');
            //            }

            //            $(tbl1).find('tr').eq(index + 1).find('select[name="unit"]').find('option').each(function () {
            //                if ($(this).val().toLowerCase() == select_sche_unit.toLowerCase()) {
            //                    $(this).prop('selected', true);
            //                }
            //            })
            //            $(tbl1).find('tr').eq(index + 1).find('select[name="free_unit"]').find('option').each(function () {
            //                if ($(this).val().toLowerCase() == select_free_unit.toLowerCase()) {
            //                    $(this).prop('selected', true);
            //                }
            //            });
            //            prdCodeex = element.pCode;
            //        }
            //        else {

            //            str = `<td style="vertical-align: middle;padding: 2px 2px; text-align:center"><input type="hidden" class="AutoID" name="AutoID" value=${maids} /> <input type="hidden" name="pCode" value=${element.pCode} /><input type="hidden" name="actFlg" value=${element.actFlg} />   <span></span></td><td style="display:none"><input type="hidden" class="erp_scheme_value" value=${calc_Scheme_erp_value}></td><td style="display:none"><input type="hidden" class="Scheme_erp_code" value=${calc_Scheme_erp_code} ></td>`; //value=${element.Sample_Erp_Code}
            //            str += `<td><select class="form-control unit" name="unit" >${units}</select></td><td style="padding: 2px 2px; vertical-align: middle;"><input type="text" class="form-control " id="txtScheme" name="txtScheme" value=${element.scheme} /></td><td><select class="form-control unit" name="free_unit" >${units}</select></td><td style="display:none"><input type="hidden" class="erp_free_value" value=${calc_free_erp_value}></td><td style="display:none;"><input type="hidden" class="Free_erp_code" value=${calc_free_erp_code} ></td><td style="padding: 2px 2px; vertical-align: middle;"><input type="text" class="form-control " id="txtFree" name="txtFree"  value=${element.free} /></td><td style="padding: 2px 2px;    vertical-align: middle;"><input type="text" class="form-control " id="txtDiscount" name="txtDiscount"  value=${element.discount} /></td><td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" class="packages" ${pkg} value="1" id="checkboxID${index + 1}"/><label class="packageslbl" for="checkboxID${index + 1}"></label></p></td>`;
            //            str += `<td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" ${agp} class="against"  value="1" id="against${(index + 1)}"/><label for="against${(index + 1)}" class="againstlbl"></label></p></td>`;
            //            str += `<td style="padding: 2px 2px; vertical-align: middle;"><select class="form-control" disabled="disabled" name="allType" >${ofStr}</select></td><td style="padding: 2px 2px; vertical-align: middle;""><button type="button"  class="btn btn-default delpro btn-md"><span class="glyphicon glyphicon-minus  " style="color:#378b2c"></span></button></td>`;
            //            $(tbl1).find('tbody').append('<tr id=' + Math.random() + ' class=' + element.pCode + '>' + str + '</tr>');

            //            if (element.Against == 'Y') {
            //                $(tbl1).find('tr').eq(index + 1).find('select[name="allType"]').prop('disabled', '');
            //                $(tbl1).find('tr').eq(index + 1).find('select[name="allType"]').val(agPro);
            //            }
            //            else {
            //                $(tbl1).find('tr').eq(index + 1).find('select[name="allType"]').prop('disabled', 'disabled');
            //            }

            //            $(tbl1).find('tr').eq(index + 1).find('select[name="unit"]').find('option').each(function () {
            //                if ($(this).val().toLowerCase() == select_sche_unit.toLowerCase()) {
            //                    $(this).prop('selected', true);
            //                }
            //            })
            //            $(tbl1).find('tr').eq(index + 1).find('select[name="free_unit"]').find('option').each(function () {
            //                if ($(this).val().toLowerCase() == select_free_unit.toLowerCase()) {
            //                    $(this).prop('selected', true);
            //                }
            //            });
            //            prdCodeex = element.pCode;
            //        }

            //    });


            //    $('.btnDelete').prop('disabled', '');
            //}


            function fillData1(xldata) {
                Xcel_Date = [];
                type = $('#ddlType').val();

                // Get_Product_Details('', 1);

                //$.ajax({
                //    type: "POST",
                //    contentType: "application/json; charset=utf-8",
                //    async: false,
                //    url: "Primary_scheme.aspx/getProduct",
                //    data: "{'Type':'" + type + "'}",
                //    dataType: "json",
                //    success: function (data) {
                //        ProDtl = data.d;
                //    },
                //    error: function (jqXHR, exception) {
                //        console.log(jqXHR);
                //        console.log(exception);
                //    }
                //});
		    flg = 0;
                for (var a = 0; a < ProDtl.length; a++) {

                   var res = xldata.filter(function (c) {
                        //return (c["Product_Detail_Code"] == ProDtl[a].pCode);
                        return (c["ProductCode"] == ProDtl[a].pCode);
                    });
                    if (res.length > 0) flg = 1;
                }
                if (flg == 0) {
                    alert('Rows not found.Upload valid excel file..');
                    return;
                }	
                tbl = $('#tbl');
                tbl1 = $('#tbl1');
                $(tbl).find('thead tr').remove();
                $(tbl).find('tbody tr').remove();
                $(tbl1).find('thead tr').remove();
                $(tbl1).find('tbody tr').remove();

                //var ofPro = ProDtl.filter(item => item.pTypes == "O");
                // ProDtl = ProDtl.filter(item => item.pTypes != "O");
                var ofPro = xldata;
                str = '<th style="width: 24%;">Product Name</th><th style="width: 14%;">Scheme Unit</th><th style="width: 0%;">Scheme </th><th style="width: 13%;">Free_Unit</th><th>Free</th><th>Discount</th><th style="width: 10%;">Dis Value <div><p class="onoff"><input type="checkbox" class="ddlDis" value="1" id="ddlDis"/><label for="ddlDis" class="ddlDislbl"></label></p></div></th><th>Package Offer</th><th>Against Product</th><th>Offer Product</th><th>Add</th>';
                $(tbl1).find('thead').append('<tr>' + str + '</tr>');

                var ofStr = `<option value="0">Select</option>`;
                //if (ProDtl.length > 0) {
                //    ProDtl.forEach((obj) => ofStr += `<option value=${obj.pCode}>${obj.pName}</option>`);
                //}

                if (ofPro.length > 0) {
                    ofPro.forEach((obj) => ofStr += `<option value=${obj.ProductCode}>${obj.ProductName}</option>`);
                }
                var prdCodeex = '';
                var maids = 0, mid = 0;

                var res = [];

                for (var a = 0; a < ProDtl.length; a++) {

                    res = xldata.filter(function (c) {
                        //return (c["Product_Detail_Code"] == ProDtl[a].pCode);
                        return (c["ProductCode"] == ProDtl[a].pCode);

                    });

                    if (res.length > 0) {

                        Xcel_Date.push({

                            //pCode: res[0].Product_Detail_Code || '0',
                            //pName: res[0].Product_Detail_Name || '0',
                            pCode: res[0].ProductCode || '0',
                            pName: res[0].ProductName || '0',
                            Sale_unit_code: ProDtl[a].product_Sale_unit_Code || 0,
                            unit_code: ProDtl[a].product_unit_Code || 0,
                            Scheme_Unit: res[0].Scheme_Unit || '0',
                            Scheme_Unit_Code: res[0].Scheme_Unit_Code || '0',
                            scheme: res[0].Scheme || '0',
                            Free_Unit: res[0].Free_Unit || '0',
                            free: res[0].Free || '0',
                            discount: res[0].Discount || '0',
                            dis_type: res[0].Discount_Type,
                            Package: res[0].Package || '0',
                            Against: res[0].Against || '0',
                            AgProduct: res[0].Offer_Product_code || '0',
                            //Offer_Product_Name: res[0].Offer_Product_Name || '0',
                            Offer_Product_Name: res[0].OfferProduct || '0',
                            Product_Sale_Unit: ProDtl[a].Product_Sale_Unit || '0',
                            product_unit: ProDtl[a].product_unit || '0',
                            Sample_Erp_Code: ProDtl[a].Sample_Erp_Code || '0',
                            Umo_wegt: ProDtl[a].product_umo_weight || '0',
                            Defaul_uom: ProDtl[a].product_Default_umo || '0',
                            pTypes: ProDtl[a].pTypes || '0'

                        });

                    }

                }

                Xcel_Date.forEach((element, index, array) => {

                    var bindunit1 = '<option value="0">Select Unit</option>';
                    bindunit1 = {};
                    bindunit1 = '<option selected="selected" value="0">Select Unit</option>';
                    bindunit1 += `<option value=${element.product_unit}>${element.product_unit}</option>`;
                    bindunit1 += `<option  value=${element.Product_Sale_Unit}>${element.Product_Sale_Unit}</option>`;

                    var pkg = '';
                    var agp = ''; var disVal = '';
                    var agPro = element.AgProduct;
                    var select_sche_unit = element.Scheme_Unit;
                    var select_free_unit = element.Free_Unit;
                    var select_sche_unit_code = element.Scheme_Unit_Code;
                    var ppcod = element.pCode;
                    var ppanem = element.pName;

                    var cal_sche = element.scheme;
                    var cal_fre = element.free;
                    var erp = element.Sample_Erp_Code;

                    Get_unit(element.pCode);

                    var edit_sche = []; var edit_free_unit = []; var Filter_upload = [];

                    edit_sche = Allrate.filter(function (y) {
                        return (y.Product_Detail_Code == ppcod && y.Move_MailFolder_Name == select_sche_unit);
                    });

                    if (edit_sche.length > 0) {

                        if (edit_sche[0].Product_Sale_Unit == select_sche_unit) {

                            var calc_Scheme_erp_value = cal_sche;
                            var calc_Scheme_erp = edit_sche[0].Sample_Erp_Code;

                        }
                        else {
                            var calc_Scheme_erp_value = cal_sche * edit_sche[0].Sample_Erp_Code;
                            var calc_Scheme_erp_code = edit_sche[0].Sample_Erp_Code;
                        }
                    }

                    edit_free_unit = Allrate.filter(function (y) {
                        return (y.Product_Detail_Code == ppcod && y.Move_MailFolder_Name == select_free_unit);
                    });

                    if (edit_free_unit.length > 0) {

                        if (edit_free_unit[0].Product_Sale_Unit == select_free_unit) {

                            var calc_free_erp_value = cal_fre;
                            var calc_free_erp = edit_free_unit[0].Sample_Erp_Code;

                        }
                        else {
                            var calc_free_erp_value = cal_fre * edit_free_unit[0].Sample_Erp_Code;
                            var calc_free_erp_code = edit_free_unit[0].Sample_Erp_Code;
                        }
                    }


                    Filter_upload = View_unit_details.filter(function (t) {
                        return (t.PCode == ppcod && t.UOM_Id == element.Defaul_uom);
                    });

                    if (Filter_upload.length > 0) {

                        var con = Filter_upload[0].CnvQty;
                    }
                    else {
                        var con = 0;
                    }


                    if (Number(element.AutoID) > 0) {
                        maids = element.AutoID;
                    }
                    else {
                        maids = Number(maids) + 1;
                    }
                    console.log(maids);
                    if (element.Package == 'Y') {
                        pkg = 'checked';
                    }
                    if (element.Against == 'Y') {
                        agp = 'checked';
                    }

                    if (element.dis_type == '%') {
                        disVal = 'checked'
                    }

                    if (prdCodeex != element.pCode) {

                        str = `<td style="vertical-align: middle;padding: 2px 2px;"> <input type="hidden" class="AutoID" name="AutoID" value=${maids} /><input type="hidden" name="pCode" value=${element.pCode} /><input type="hidden" name="actFlg" value=${element.actFlg} />   <span>${element.pName} </span></td><td style="display:none"><input class="prd_sale_unit" type="hidden" value=${element.Sale_unit_code}></td><td style="display:none"><input type="hidden" class="prd_unit" value=${element.unit_code}></td><td style="display:none"><input type="hidden" class="prd_sample_erp_code" value=${element.Sample_Erp_Code}></td><td style="display:none"><input type="hidden" class="prd_umoweight" value=${element.Umo_wegt}></td><td style="display:none"><input type="hidden" class="Default_con_fac" value=${con}></td><td style="display:none"><input type="hidden" class="erp_scheme_value" value=${calc_Scheme_erp_value}></td><td style="display:none"><input type="hidden" class="Scheme_erp_code" value=${calc_Scheme_erp_code} ></td><td><select class="form-control unit" name="unit" >${units}</select></td>`;
                        str += `<td style="padding: 2px 2px; vertical-align: middle;"><input type="text" class="form-control " id="txtScheme" name="txtScheme" value="${element.scheme}" /></td><td><select class="form-control unit" name="free_unit" >${units}</select></td><td style="padding: 2px 2px;    vertical-align: middle;"><input type="text" class="form-control " id="txtFree" name="txtFree"  value=${element.free} /></td><td style="display:none"><input type="hidden" class="erp_free_value" value=${calc_free_erp_value}></td><td style="display:none;"><input type="hidden" class="Free_erp_code" value=${calc_free_erp_code} ></td><td style="padding: 2px 2px; vertical-align: middle;"><input type="text" id="txtDiscount" name="txtDiscount"  value=${element.discount} /></td><td style="padding: 2px 42px; vertical-align: middle;"><p class="onoff"><input type="checkbox" class="ddlDisClass" name="checked" ${disVal} value="1" id="ddlDisClass${(index + 1)}"/><label for="ddlDisClass${(index + 1)}" class="ddlDisClasslbl"></label></p></td><td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" class="packages" ${pkg} value="1" id="checkboxID${index + 1}"/><label class="packageslbl" for="checkboxID${index + 1}"></label></p></td>`;
                        str += `<td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" ${agp} class="against"  value="1" id="against${(index + 1)}"/><label for="against${(index + 1)}" class="againstlbl"></label></p></td>`;
                        str += `<td style="padding: 2px 2px; vertical-align: middle;"><select class="form-control" disabled="disabled" name="allType" >${ofStr}</select></td><td style="padding: 2px 2px; vertical-align: middle;""><button type="button"  class="btn btn-default addpro btn-md"><span class="glyphicon glyphicon-plus  " style="color:#378b2c"></span></button></td>`;
                        $(tbl1).find('tbody').append('<tr id=' + maids + ' class=' + element.pCode + '>' + str + '</tr>');

                        if (element.Against == 'Y') {
                            $(tbl1).find('tr').eq(index + 1).find('select[name="allType"]').prop('disabled', '');
                            $(tbl1).find('tr').eq(index + 1).find('select[name="allType"]').val(agPro);
                        }
                        else {
                            $(tbl1).find('tr').eq(index + 1).find('select[name="allType"]').prop('disabled', 'disabled');
                        }

                        $(tbl1).find('tr').eq(index + 1).find('select[name="unit"]').find('option').each(function () {
                            if ($(this).text().toLowerCase() == select_sche_unit.toLowerCase()) {
                                $(this).prop('selected', true);
                            }
                        })
                        $(tbl1).find('tr').eq(index + 1).find('select[name="free_unit"]').find('option').each(function () {
                            if ($(this).text().toLowerCase() == select_free_unit.toLowerCase()) {
                                $(this).prop('selected', true);
                            }
                        });
                        prdCodeex = element.pCode;
                    }
                    else {

                        str = `<td style="vertical-align: middle;padding: 2px 2px; text-align:center"><input type="hidden" class="AutoID" name="AutoID" value=${maids} /> <input type="hidden" name="pCode" value=${element.pCode} /><input type="hidden" name="actFlg" value=${element.actFlg} /><span></span></td><td style="display:none"><input class="prd_sale_unit" type="hidden" value=${element.Sale_unit_code}></td><td style="display:none"><input type="hidden" class="prd_unit" value=${element.unit_code}></td><td style="display:none"><input type="hidden" class="prd_sample_erp_code" value=${element.Sample_Erp_Code}></td><td style="display:none"><input type="hidden" class="prd_umoweight" value=${element.Umo_wegt}></td><td style="display:none"><input type="hidden" class="Default_con_fac" value=${con}></td><td style="display:none"><input type="hidden" class="erp_scheme_value" value=${calc_Scheme_erp_value}></td><td style="display:none"><input type="hidden" class="Scheme_erp_code" value=${calc_Scheme_erp_code} ></td>`; //value=${element.Sample_Erp_Code}
                        str += `<td><select class="form-control unit" name="unit" >${units}</select></td><td style="padding: 2px 2px; vertical-align: middle;"><input type="text" class="form-control " id="txtScheme" name="txtScheme" value=${element.scheme} /></td><td><select class="form-control unit" name="free_unit" >${units}</select></td><td style="display:none"><input type="hidden" class="erp_free_value" value=${calc_free_erp_value}></td><td style="display:none;"><input type="hidden" class="Free_erp_code" value=${calc_free_erp_code} ></td><td style="padding: 2px 2px; vertical-align: middle;"><input type="text" class="form-control " id="txtFree" name="txtFree"  value=${element.free} /></td><td style="padding: 2px 2px;    vertical-align: middle;"><input type="text" class="form-control " id="txtDiscount" name="txtDiscount"  value=${element.discount} /></td><td style="padding: 2px 42px; vertical-align: middle;"><p class="onoff"><input type="checkbox" class="ddlDisClass" name="checked" ${disVal} value="1" id="ddlDisClass${(index + 1)}"/><label for="ddlDisClass${(index + 1)}" class="ddlDisClasslbl"></label></p></td><td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" class="packages" ${pkg} value="1" id="checkboxID${index + 1}"/><label class="packageslbl" for="checkboxID${index + 1}"></label></p></td>`;
                        str += `<td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" ${agp} class="against"  value="1" id="against${(index + 1)}"/><label for="against${(index + 1)}" class="againstlbl"></label></p></td>`;
                        str += `<td style="padding: 2px 2px; vertical-align: middle;"><select class="form-control" disabled="disabled" name="allType" >${ofStr}</select></td><td style="padding: 2px 2px; vertical-align: middle;""><button type="button"  class="btn btn-default delpro btn-md"><span class="glyphicon glyphicon-minus  " style="color:#378b2c"></span></button></td>`;
                        $(tbl1).find('tbody').append('<tr id=' + Math.random() + ' class=' + element.pCode + '>' + str + '</tr>');

                        if (element.Against == 'Y') {
                            $(tbl1).find('tr').eq(index + 1).find('select[name="allType"]').prop('disabled', '');
                            $(tbl1).find('tr').eq(index + 1).find('select[name="allType"]').val(agPro);
                        }
                        else {
                            $(tbl1).find('tr').eq(index + 1).find('select[name="allType"]').prop('disabled', 'disabled');
                        }

                        $(tbl1).find('tr').eq(index + 1).find('select[name="unit"]').find('option').each(function () {
                            if ($(this).text().toLowerCase() == select_sche_unit.toLowerCase()) {
                                $(this).prop('selected', true);
                            }
                        })
                        $(tbl1).find('tr').eq(index + 1).find('select[name="free_unit"]').find('option').each(function () {
                            if ($(this).text().toLowerCase() == select_free_unit.toLowerCase()) {
                                $(this).prop('selected', true);
                            }
                        });
                        prdCodeex = element.pCode;
                    }

                });
                $('.btnDelete').prop('disabled', '');
            }


            function ExportXLToTable(succes, fail) {
                //var regex = /^([a-zA-Z0-9\s_\\.\-:()])+(.xlsx|.xls)$/;
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

        .ui-datepicker {
            z-index: 2 !important;
            width: 15em !important;
        }
    </style>
    <form id="form1" runat="server">
        <%--        <div class="container" style="min-width: 95%; width: 95%">
            <div class="row">
                <div class="col-md-8">
                    <div class="row">
                        <div class="col-md-5  stOnly">
                            <a href="#" class="list-group-item active state" style="width: 95%;">States<input title="toggle all" type="checkbox" class="all pull-right" /></a>
                            <div class="list-group" id="state" style="border: 1px solid #ddd">
                            </div>
                        </div>
                        <div class="col-md-5 disOnly">
                            <a href="#" class="list-group-item active distributor" style="width: 95%;">Distributor<input title="toggle all" type="checkbox" class="all pull-right" /></a>
                            <div class="list-group" id="distributor" style="border: 1px solid #ddd">
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-inline">
                                <div class="form-group" style="display: none">
                                    <label for="ddlSubDiv">Division</label>
                                    <asp:dropdownlist id="subdiv" runat="server" cssclass="form-control">
                                    </asp:dropdownlist>
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
                                    <input type="text" runat="server" class="form-control  datetimepicker" autocomplete="off" id="txtFrom" name="txtFrom" />
                                </div>
                                <div class="form-group">
                                    <label for="txtTo">To:</label>
                                    <input type="text" runat="server" class="form-control  datetimepicker1" autocomplete="off" id="txtTo" name="txtTo" />
                                </div>
                                <div class="form-inline">
                                    <div class="form-group">
                                        <label for="txtName">Scheme Name</label>
                                        <input type="text" runat="server" class="form-control" autocomplete="off" id="txtName" name="txtName" />
                                    </div>

                                    <div class="form-group">
                                    </div>
                                </div>

                                <div class="form-group">
                                    <button name="btnClear" type="button" class="btn btn-primary btnClear" style="width: 70px; margin-top: 34px;">Clear</button>
                                    <asp:button id="Upldbt" cssclass="btn btn-primary" runat="server" text="Excel File" style="margin-top: 33px; margin-left: 15px;" onclick="lnkDownload_Click" />
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


        </div>--%>


        <div class="container" style="min-width: 100%; width: 100%">
            <asp:HiddenField ID="hidden_div" runat="server" />
            <div class="row">
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-7">
                            <div class="form-inline">
                                <div class="row">

                                    <div class="form-group">
                                        <label for="lbl_divison">Division:</label><span style="color: Red">*</span>
                                        <select id="ddlDiv" class="form-control">
                                        </select>
                                    </div>

                                    <div class="form-group">
                                        <label for="txtName">Scheme Name</label><span style="color: Red">*</span>
                                        <input type="text" runat="server" class="form-control" autocomplete="off" id="txtName" name="txtName" />
                                    </div>
                                </div>

                                <div class="row">

                                    <div class="form-group">
                                        <label for="txtFrom">From:</label>
                                        <input type="text" runat="server" class="form-control  datetimepicker" autocomplete="off" id="txtFrom" name="txtFrom" />
                                    </div>

                                    <div class="form-group">
                                        <label for="txtTo">To:</label><span style="color: Red">*</span>
                                        <input type="text" runat="server" class="form-control  datetimepicker1" autocomplete="off" id="txtTo" name="txtTo" />
                                    </div>

                                </div>

                                <div class="form-group" style="display: none;">
                                    <label for="ddlType">Type:</label>
                                    <select id="ddlType" class="form-control">
                                        <option value="1">Categorywise</option>
                                        <option selected="selected" value="2">SKUwise</option>
                                    </select>
                                </div>
                                <button name="btnClear" type="button" class="btn btn-primary btnClear" style="width: 70px; margin-top: 34px;">Clear</button>
                                <asp:Button ID="Upldbt" CssClass="btn btn-primary" runat="server" Text="Excel File" Style="margin-top: 33px; margin-left: 15px;" OnClick="lnkDownload_Click" />
                            </div>


                            <div class="row" style="padding: 30px 0px 0px 0px;">
                                <div class="col-md-6" style="text-align: right; padding: 0px 42px 0px 0px;">
                                    <input type="file" id="excelfile" style="float: right; margin-top: 2px;" 
                                      accept=".xlsx, .xls, .csv"/>
                                </div>
                                <div class="col-md-3">
                                    <button name="btnImport" type="button" class="btn btn-primary btnImport" style="width: 170px; margin-bottom: 8px;">Fill Data From Excel </button>
                                </div>


                            </div>

                        </div>

                        <div class="col-md-5" style="padding: 20px 0px 0px 0px;">
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
                    <br />
                    <br />
                    <div class="row">

                        <div class="col-md-3  plantOnly" style="display: none;">
                            <a href="#" class="list-group-item active state" style="width: 95%;">Plant<input title="toggle all" type="checkbox" class="all pull-right" /></a>
                            <div class="list-group" id="plant" style="border: 1px solid #ddd">
                                <input type="text" class="form-control" id="txt_plant" placeholder="Search" style="border-radius: 0px; font-family: revert;">
                            </div>
                        </div>

                        <div class="col-md-3  stOnly">
                            <a href="#" class="list-group-item active state" style="width: 95%;">States<input title="toggle all" type="checkbox" class="all pull-right" /></a>
                            <div class="list-group" id="state" style="border: 1px solid #ddd">
                                <input type="text" class="form-control" id="txt_state" placeholder="Search" style="border-radius: 0px; font-family: revert;">
                            </div>
                        </div>

                        <div class="col-md-3  terrOnly" style="display: none;">
                            <a href="#" class="list-group-item active territory" style="width: 95%;">Territory<input title="toggle all" type="checkbox" class="all pull-right" /></a>
                            <div class="list-group" id="territory" style="border: 1px solid #ddd">
                                <input type="text" class="form-control" id="txt_terr" placeholder="Search" style="border-radius: 0px; font-family: revert;">
                            </div>
                        </div>
                        <div class="col-md-3 disOnly">
                            <a href="#" class="list-group-item active distributor" style="width: 95%;">Distributor<input title="toggle all" type="checkbox" class="all pull-right" /></a>
                            <div class="list-group" id="distributor" style="border: 1px solid #ddd">
                                <input type="text" class="form-control" id="txt_dis" placeholder="Search" style="border-radius: 0px; font-family: revert;">
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <br />
            </div>
        </div>

         <div class="row">
            <div class="table-responsive col-xs-12">
                <table id="tbl1" class="table table-bordered newStly">
                    <thead></thead>
                    <tbody></tbody>
                    <tfoot style="height:60px"></tfoot>
                </table>
            </div>            
        </div>


        <div class="form-inline footr">
            <div class="form-group" style="margin-left: 55px;">
                <button name="btnsave" type="button" class="btn btn-primary btnsave" style="width: 100px">Save</button>
            </div>
            <div class="form-group">
                <button name="btnDelete" type="button" class="btn btn-primary btnDelete" style="width: 100px">Delete</button>
            </div>
        </div>
    </form>
</asp:Content>

