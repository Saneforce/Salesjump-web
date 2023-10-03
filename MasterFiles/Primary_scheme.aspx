<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Primary_scheme.aspx.cs" Inherits="MasterFiles_Primary_scheme" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link rel="stylesheet" type="text/css" media="all" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/smoothness/jquery-ui.css" />

    <link rel="stylesheet" href="https://netdna.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css" />

    <link href="https://netdna.bootstrapcdn.com/bootstrap/3.0.3/css/bootstrap.min.css" rel="stylesheet" id="bootstrap-css" />



    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css" />

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
                console.log($(this).val());

                //  if (insertType == '1') {
                if ($(this).val().length > 0) {
                    if (isValidDate($(this).val())) {
                        var fit_start_time = $(this).val();
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
                // }
            });

            $(document).on('change', '#<%=txtName.ClientID %>', function () {

                var sch = $(this).val();

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Primary_scheme.aspx/Validate_Scheme_Name",
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
                url: "Primary_scheme.aspx/getreate",
                dataType: "json",
                success: function (data) {
                    Allrate = JSON.parse(data.d) || [];
                },
                error: function (result) {
                }
            });

            $('#ddlType').val(2);
            genTable = function () {
                type = $('#ddlType').val();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Primary_scheme.aspx/getProduct",
                    data: "{'Type':'" + type + "'}",
                    dataType: "json",
                    success: function (data) {
                        ProDtl = data.d;
                        str = '';
                        tbl = $('#tbl');
                        tbl1 = $('#tbl1');
                        $(tbl).find('thead tr').remove();
                        $(tbl).find('tbody tr').remove();
                        $(tbl1).find('thead tr').remove();
                        $(tbl1).find('tbody tr').remove();
                        if (ProDtl.length > 0) {
                            if (type == "2") {

                                var ofPro = ProDtl.filter(item => item.pTypes == "O");
                                // ProDtl = ProDtl.filter(item => item.pTypes != "O");
                                //$("#tbl1").css("width", "100")
                                str = '<th style="width: 24%;">Product Name</th><th style="width: 14%;">Scheme Unit</th><th style="width: 0%;">Scheme </th><th style="width: 13%;">Free_Unit</th><th>Free</th><th>Discount%</th><th>Package Offer</th><th>Against Product</th><th>Offer Product</th><th>Add</th>';
                                $(tbl1).find('thead').append('<tr>' + str + '</tr>');

                                var ofStr = '<option value="0">Select</option>';
                                if (ProDtl.length > 0) {
                                    ProDtl.forEach((obj) => ofStr += `<option value=${obj.pCode}>${obj.pName}</option>`);
                                }
                                bindunit = {}
                                var bindunit1 = '<option value="0">Select Unit</option>';

                                for (var i = 0; i < ProDtl.length; i++) {
                                    bindunit1 = {};
                                    bindunit1 = '<option selected="selected" value="0">Select Unit</option>';
                                    bindunit1 += `<option value=${ProDtl[i].product_unit}>${ProDtl[i].product_unit}</option>`;
                                    bindunit1 += `<option  value=${ProDtl[i].Product_Sale_Unit}>${ProDtl[i].Product_Sale_Unit}</option>`;


                                    str = '<td style="vertical-align: middle;padding: 2px 2px;"><input type="hidden" class="AutoID" name="AutoID" value="0"} /> <input type="hidden" name="pCode" value="' + ProDtl[i].pCode + '"/>  <span>' + ProDtl[i].pName + '</span></td><td style="display:none"><input type="hidden" class="erp_scheme_value" value="0"></td><td style="display:none"><input type="hidden" class="erp_code" value=' + ProDtl[i].Sample_Erp_Code + '></td>';
                                    str += `<td><select class="form-control unit" name="unit" >${bindunit1}</select></td>`;
                                    str += '<td style="padding: 2px 2px; vertical-align: middle;"><input type="text" style="width: 100%;" autocomplete="off" id="txtScheme" name="txtScheme" ></td>';
                                    str += `<td><select class="form-control unit" name="free_unit" >${bindunit1}</select></td>`;
                                    str += '<td style="padding: 2px 2px;vertical-align: middle;"><input type="text" style="width: 100%;" id="txtFree" name="txtFree" ></td><td style="display:none"><input type="hidden" class="erp_free_value" value="0"></td><td style="padding: 2px 2px;    vertical-align: middle;"><input type="text" style="width: 100%;" id="txtDiscount" name="txtDiscount" /></td> <td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" class="packages" value="1" id="checkboxID' + (i + 1) + '" /><label class="packageslbl" for="checkboxID' + (i + 1) + '"></label></p></td>';
                                    str += `<td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" class="against"  value="1" id="against${(i + 1)}"/><label for="against${(i + 1)}" class="againstlbl"></label></p></td>`;
                                    str += `<td style="padding: 2px 2px; vertical-align: middle;"><select class="form-control" disabled="disabled" name="allType" >${ofStr}</select></td><td style="padding: 2px 2px; vertical-align: middle;""><button type="button"  class="btn btn-default addpro btn-md"><span class="glyphicon glyphicon-plus" style="color:#378b2c"></span></button></td>`;
                                    $(tbl1).find('tbody').append('<tr id=' + (i + 1) + ' class=' + ProDtl[i].pCode + '>' + str + '</tr>');

                                }
                            }
                            else {

                                str = '<th>Category Name</th><th>Scheme </th><th>Free</th><th>Discount%</th><th>Package Offer</th>';
                                $(tbl1).find('thead').append('<tr>' + str + '</tr>');
                                for (var i = 0; i < ProDtl.length; i++) {
                                    str = '<tr><td style="vertical-align: middle;padding: 2px 2px;"> <input type="hidden" name="pCode" value="' + ProDtl[i].pCode + '"/>  <span>' + ProDtl[i].pName + '</span></td>';
                                    str += '<td style="padding: 2px 2px;    vertical-align: middle;"><input style="width: 96%;" type="text" autocomplete="off" id="txtScheme" name="txtScheme" /></td><td style="padding: 2px 2px; vertical-align: middle;"><input style="width: 97%;" type="text" id="txtFree" name="txtFree" ></td><td style="padding: 2px 2px; vertical-align: middle;"><input style="width: 89%;" type="text"  id="txtDiscount" name="txtDiscount" /></td><td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" class="packages" value="1" id="checkboxID' + (i + 1) + '"/><label class="packageslbl" for="checkboxID' + (i + 1) + '"></label></p></td>';
                                    $(tbl1).find('tbody').append('<tr>' + str + '</tr>');
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
            }
            genTable();

            genSchemTbale = function () {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Primary_scheme.aspx/getScheme",
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
                                str = '<td style="vertical-align: middle;" ><span>' + (i + 1) + '</span></td><td style="vertical-align: middle;"><span>' + SchDtl[i].sName + '</span></td>';
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
                url: "Primary_scheme.aspx/getDistributor_for_primary",
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
                url: "Primary_scheme.aspx/getState",
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
                    $('.disOnly .all').prop("checked", false);
                }
                //$('#<%=txtTo.ClientID%>').val('');
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
                e.stopPropagation();
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
                genDist();
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
                        alert('Please Select Offer Product');
                        return false;
                    }
                    else {
                        clonedRow.attr('id', Math.random())
                        clonedRow.find('input[type="text"]').val('');
                        var selected_unit = $(this).closest('tr').find('td').find('select[name="unit"]').val();
                        clonedRow.find('select[name="unit"]').val(selected_unit);

                        clonedRow.find('.erp_scheme_value').val('0');
                        clonedRow.find('.erp_free_value').val('0');
                        clonedRow.find('select[name = "unit"]').prop('disabled', 'disabled');
                        clonedRow.find('.AutoID').val(clCount);
                        clonedRow.find('td').eq(0).find('span').text('');
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
                    }
                }
                else {

                    clonedRow.attr('id', Math.random())
                    clonedRow.find('input[type="text"]').val('');
                    var selected_unit = $(this).closest('tr').find('td').find('select[name="unit"]').val();
                    clonedRow.find('select[name="unit"]').val(selected_unit);
                    clonedRow.find('select[name="unit"]').prop('disabled', 'disabled');

                    clonedRow.find('.erp_scheme_value').val('0');
                    clonedRow.find('.erp_free_value').val('0');
                    clonedRow.find('select[name = "unit"]').prop('disabled', 'disabled');
                    clonedRow.find('.AutoID').val(clCount);
                    clonedRow.find('td').eq(0).find('span').text('');
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

            fillData = function (scData) {

                $('#<%=txtName.ClientID %>').val(scData[0].schemeName);
                $('#<%=txtFrom.ClientID %>').val(scData[0].FDate);
                $('#<%=txtTo.ClientID %>').val(scData[0].TDate);

                $('#<%=txtName.ClientID %>').prop('disabled', 'disabled');

                //$('#state').find('a').remove();
                //for (var i = 0; i < sDt.length; i++) {
                //    str = '<a href="#" class="list-group-item">' + sDt[i].stName + '<input type="checkbox" name=' + sDt[i].stCode + ' class="chk pull-right"/></a>';
                //    $('#state').append(str);
                //}

              //  let uState = [...new Set(scData.map(item => item.State_Code))];
             //   var st = uState[0].split(',')
             //   if (uState.length > 0) {
             //       for (var i = 0; i < st.length; i++) {
             //           $('#state a').each(function () {
             //               if ($(this).find('input[type=checkbox]').attr('name') == st[i]) {
             //                   $(this).find('input[type=checkbox]').prop("checked", true);
             //               }
             //           });

             //       }
             //       genDist();
             //       let uStockist = [...new Set(scData.map(item => item.Stockist_Code))];
             //       var stk = uStockist[0].split(',')
             //       if (uStockist.length > 0) {
              //          for (var i = 0; i < stk.length; i++) {
              //              $('#distributor a').each(function () {
              //                  if ($(this).find('input[type=checkbox]').attr('name') == stk[i]) {
              //                      $(this).find('input[type=checkbox]').prop("checked", true);
              //                  }
              //              });

              //          }
             //       }
             //   }


                type = $('#ddlType').val();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Primary_scheme.aspx/getProduct",
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
                str = '<th style="width: 24%;">Product Name</th><th style="width: 14%;">Scheme Unit</th><th style="width: 0%;">Scheme </th><th style="width: 13%;">Free_Unit</th><th>Free</th><th>Discount%</th><th>Package Offer</th><th>Against Product</th><th>Offer Product</th><th>Add</th>';
                $(tbl1).find('thead').append('<tr>' + str + '</tr>');

                var ofStr = `<option value="0">Select</option>`;
                if (ProDtl.length > 0) {
                    ProDtl.forEach((obj) => ofStr += `<option value=${obj.pCode}>${obj.pName}</option>`);
                }
                var prdCodeex = '';

                var stkCode = scData[0].Stockist_Code;
                var stateCode = scData[0].State_Code;
                console.log(stkCode);
                console.log(stateCode);
                console.log(scData);
                var stkDt = scData.filter(lst => (lst.Stockist_Code == stkCode || lst.Stockist_Code == '') && (lst.State_Code == stateCode || lst.State_Code == ''));
                console.log(stkDt);
                var maids = 0, mid = 0;
                stkDt.forEach((element, index, array) => {

                    var bindunit1 = '<option value="0">Select Unit</option>';
                    bindunit1 = {};
                    bindunit1 = '<option selected="selected" value="0">Select Unit</option>';
                    bindunit1 += `<option value=${element.product_unit}>${element.product_unit}</option>`;
                    bindunit1 += `<option  value=${element.Product_Sale_Unit}>${element.Product_Sale_Unit}</option>`;

                    var pkg = '';
                    var agp = '';
                    var agPro = element.AgProduct;
                    var select_sche_unit = element.Scheme_Unit;
                    var select_free_unit = element.Free_Unit;
                    var ppcod = element.pCode;
                    var ppanem = element.pName;

                    var cal_sche = element.scheme;
                    var cal_fre = element.free;
                    var erp = element.Sample_Erp_Code;

                    var edit_sche = [];

                    edit_sche = Allrate.filter(function (y) {
                        return (y.Product_Detail_Code == ppcod);
                    });

                    if (edit_sche.length > 0) {

                        if (edit_sche[0].product_unit == select_sche_unit) {
                            var calc_erp = cal_sche * erp;
                            var cal_free_erp = cal_fre * erp;
                        }
                        else {
                            var calc_erp = cal_sche;
                            var cal_free_erp = cal_fre;
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
                    console.log(maids);
                    if (element.Package == 'Y') {
                        pkg = 'checked';
                    }
                    if (element.Against == 'Y') {
                        agp = 'checked';
                    }

                    if (prdCodeex != element.pCode) {
                        str = `<td style="vertical-align: middle;padding: 2px 2px;"> <input type="hidden" class="AutoID" name="AutoID" value=${maids} /><input type="hidden" name="pCode" value=${element.pCode} /><input type="hidden" name="actFlg" value=${element.actFlg} />   <span>${element.pName} </span></td><td style="display:none"><input type="hidden" class="erp_scheme_value" value=${calc_erp}></td><td style="display:none"><input type="hidden" class="erp_code" value=${element.Sample_Erp_Code}></td><td><select class="form-control unit" name="unit" >${bindunit1}</select></td>`;
                        str += `<td style="padding: 2px 2px; vertical-align: middle;"><input type="text" class="form-control " id="txtScheme" name="txtScheme" value="${element.scheme}" /></td><td><select class="form-control unit" name="free_unit" >${bindunit1}</select></td><td style="padding: 2px 2px;    vertical-align: middle;"><input type="text" class="form-control " id="txtFree" name="txtFree"  value=${element.free} /></td><td style="display:none"><input type="hidden" class="erp_free_value" value=${cal_free_erp}></td><td style="padding: 2px 2px;    vertical-align: middle;"><input type="text" id="txtDiscount" name="txtDiscount"  value=${element.discount} /></td><td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" class="packages" ${pkg} value="1" id="checkboxID${index + 1}"/><label class="packageslbl" for="checkboxID${index + 1}"></label></p></td>`;
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
                        $(tbl1).find('tr').eq(index + 1).find('select[name="unit"]').val(select_sche_unit);
                        $(tbl1).find('tr').eq(index + 1).find('select[name="free_unit"]').val(select_free_unit);
                        prdCodeex = element.pCode;
                    }
                    else {

                        str = `<td style="vertical-align: middle;padding: 2px 2px; text-align:center"><input type="hidden" class="AutoID" name="AutoID" value=${maids} /> <input type="hidden" name="pCode" value=${element.pCode} /><input type="hidden" name="actFlg" value=${element.actFlg} />   <span></span></td><td style="display:none"><input type="hidden" class="erp_scheme_value" value=${calc_erp}></td><td style="display:none"><input type="hidden" class="erp_code" value=${element.Sample_Erp_Code}></td>`;
                        str += `<td><select class="form-control unit" name="unit" >${bindunit1}</select></td><td style="padding: 2px 2px; vertical-align: middle;"><input type="text" class="form-control " id="txtScheme" name="txtScheme" value=${element.scheme} /></td><td><select class="form-control unit" name="free_unit" >${bindunit1}</select></td><td style="display:none"><input type="hidden" class="erp_free_value" value=${cal_free_erp}></td><td style="padding: 2px 2px;    vertical-align: middle;"><input type="text" class="form-control " id="txtFree" name="txtFree"  value=${element.free} /></td><td style="padding: 2px 2px;    vertical-align: middle;"><input type="text" class="form-control " id="txtDiscount" name="txtDiscount"  value=${element.discount} /></td><td style="text-align: center;padding: 2px 2px;"><p class="onoff"><input type="checkbox" class="packages" ${pkg} value="1" id="checkboxID${index + 1}"/><label class="packageslbl" for="checkboxID${index + 1}"></label></p></td>`;
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
                        $(tbl1).find('tr').eq(index + 1).find('select[name="unit"]').val(select_sche_unit);
                        $(tbl1).find('tr').eq(index + 1).find('select[name="free_unit"]').val(select_free_unit);
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

                var tbl1 = $('#tbl1');
                var arr = []; var prdList = ''; var of_pro = ''; var pNm = ''; var Scheme_count = 0; var free_count = 0; var batch = '';

                $(tbl1).find('tbody').find('tr').each(function () {
                    prdList = $(this).find('input[name="pCode"]').val();
                    of_pro = $(this).find('select[name="allType"]').val();
                    ans = [];
                    ans1 = [];

                    if ($(this).find("input[name=txtScheme]").val() != '') {
                        Scheme_count++;
                    }

                    else if ($(this).find("input[name=txtFree]").val() != '') {
                        free_count++;
                    }

                    else if ($(this).find("input[name=txtDiscount]").val() != '') {
                        free_count++;
                    }

                    var ofer_unit = '';
                    if (of_pro != '0') {

                        ans = Allrate.filter(function (y) {
                            return (y.Product_Detail_Code == prdList);
                        });

                        ans1 = Allrate.filter(function (y) {
                            return (y.Product_Detail_Code == of_pro);
                        });

                        if (ans.length > 0) {

                            if (ans[0].product_unit == $(this).find('select[name="free_unit"]').val()) {

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
                        pName: $(this).find('input[name="pCode"], span').text(),
                        scheme: $(this).find('input[name="txtScheme"]').val() || 0,
                        scheme_unit: $(this).find('select[name="unit"]').val(),
                        free: $(this).find('input[name="txtFree"]').val() || 0,
                        free_Unit: $(this).find('select[name="free_unit"]').val(),
                        discount: $(this).find('input[name="txtDiscount"]').val() || 0,
                        Package: $(this).find('.packages').attr("checked") ? 'Y' : 'N',
                        Against: $(this).find('.against').attr("checked") ? 'Y' : 'N' || '',
                        AgProduct: $(this).find('select[name="allType"]').val() || '',
                        actFlg: $(this).find('input[name="actFlg"]').val() || '0',
                        Offer_Product_Name: pNm,
                        offer_unit: ofer_unit || '0'
                    });
                });

                if (Scheme_count <= 0) {
                    buttoncount = 0;
                    alert('Enter Atleast One Row!!!');
                    $('#ProductTable').focus();
                    return false;
                }

                if (free_count > 0) {
                    buttoncount = 0;
                    alert('Enter Atleast One Row!!!');
                    $('#ProductTable').focus();
                    return false;
                }

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
                    url: "Primary_scheme.aspx/SaveScheme",
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
                            $('#<%=txtTo.ClientID%>').val(today);
                            $('.datetimepicker').datepicker({ dateFormat: 'dd/mm/yy', startDate: '-3d', minDate: 0 });
                            $('.datetimepicker1').datepicker({ dateFormat: 'dd/mm/yy', startDate: '-3d', minDate: 0 });
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
            });

            //$(document).on('keypress', '#tbl input[type = "text"]', function (event) {
            //    if (event.which != 8 && isNaN(String.fromCharCode(event.which))) {
            //        event.preventDefault();
            //    }
            //});
            $(document).on('change', '#tbl1 select[name = "unit"]', function (event) {

                var selected_unit = $(this).val();
                var pcode = $(this).closest('tr').find('[name="pCode"]').val();
                var erp = $(this).closest('tr').find('.erp_code').val();
                // var scheme_val = $(this).closest('tr').find('input[name = "txtScheme"]').val();

                var save = [];
                save = Allrate.filter(function (y) {
                    return (y.Product_Detail_Code == pcode);
                });

                $(document).find('#tbl1').find('tr').each(function () {

                    var all_pcode = $(this).closest('tr').find('[name="pCode"]').val();
                    var all_selected_unit = $(this).closest('tr').find('select[name="unit"]').val();
                    var scheme_val = $(this).closest('tr').find('input[name = "txtScheme"]').val();
                    if (all_pcode == pcode) {

                        $(this).closest('tr').find('select[name="unit"]').val(selected_unit);
                    }

                    if (save.length > 0) {

                        if (selected_unit == save[0].product_unit) {

                            $(this).closest('tr').find('.erp_scheme_value').val(erp * scheme_val);

                        }
                        else {

                            $(this).closest('tr').find('.erp_scheme_value').val(scheme_val);
                        }

                    }
                });
            });


            $(document).on('change', '#tbl1 input[name = "txtScheme"]', function (event) {
                var row = $(this).closest("tr");
                var Given_scheme = $(this).val();
                var pro_code = row.find('[name="pCode"]').val();
                var tis_id = row.attr('id');

                var sel_unit = row.find('select[name="unit"]').val();

                if (sel_unit == '0') {
                    alert('Please Select Scheme Umo');
                    $(this).closest('tr').find('[name="txtScheme"]').val('');
                    return false;
                }

                var save = [];
                save = Allrate.filter(function (y) {
                    return (y.Product_Detail_Code == pro_code);
                });
                if (save.length > 0) {

                    if (sel_unit == save[0].product_unit) {

                        var erp = row.find('.erp_code').val();
                        calc_erp = Given_scheme * erp;
                        row.find('.erp_scheme_value').val(calc_erp);
                    }
                    else {

                        row.find('.erp_scheme_value').val(Given_scheme);

                    }
                }

                $(document).find('.' + pro_code).each(function () {

                    var le = $(document).find('.' + pro_code).length;
                    var id = $(this).attr('id');
                    var ppode = $(this).closest('tr').find('[name="pCode"]').val();
                    var sch = $(this).closest('tr').find('[name="txtScheme"]').val();


                    if (le > 1) {
                        if (tis_id != id) {
                            if (ppode == pro_code) {
                                if (sch == Given_scheme) {
                                    alert('Select Different Schemes');
                                    row.find('[name="txtScheme"]').val('');
                                    return false;
                                }
                            }
                        }
                    }
                });
            });

            $(document).on('change', '#tbl1 select[name = "free_unit"]', function (event) {

                $(this).closest('tr').find('#txtFree').val('')
                var selected_unit = $(this).val();
                var pcode = $(this).closest('tr').find('[name="pCode"]').val();
                var erp = $(this).closest('tr').find('.erp_code').val();
                var scheme_val = $(this).closest('tr').find('input[name = "txtScheme"]').val();

                var save = [];
                save = Allrate.filter(function (y) {
                    return (y.Product_Detail_Code == pcode);
                });

                if (save.length > 0) {

                    if (selected_unit == save[0].product_unit) {

                        $(this).closest('tr').find('.erp_free_value').val(erp * scheme_val);

                    }
                    else {

                        $(this).closest('tr').find('.erp_free_value').val(scheme_val);
                    }
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
                    $(this).closest('tr').find('input[name = "txtDiscount"]').prop("disabled", false);
                    return false;
                }
                else {
                    if ($(this).val() == '') {
                        $(this).closest('tr').find('input[name = "txtDiscount"]').prop("disabled", false);
                    } else {
                        $(this).closest('tr').find('input[name = "txtDiscount"]').prop("disabled", true);
                    }
                }

                var Given_free = $(this).val();
                var pro_code = row.find('[name="pCode"]').val();
                var sel_unit = row.find('select[name="free_unit"]').val();
                var Schem_unit = row.find('select[name="unit"]').val();
                var sch_erp = row.find('.erp_scheme_value').val();

                if (sel_unit == '0') {
                    alert('Please Select Free Umo');
                    row.find('[name="txtFree"]').val('');
                    $(this).closest('tr').find('input[name = "txtDiscount"]').prop("disabled", false);
                    return false;
                }

                var save = [];
                save = Allrate.filter(function (y) {
                    return (y.Product_Detail_Code == pro_code);
                });

                if (save.length > 0) {
                    if (sel_unit == save[0].product_unit) {
                        var erp = row.find('.erp_code').val();
                        calc_erp = Given_free * erp;
                        row.find('.erp_free_value').val(calc_erp);
                    }
                    else {
                        row.find('.erp_free_value').val(Given_free);
                    }
                }
                var given_free_erp = row.find('.erp_free_value').val();

                $(document).find('.' + pro_code).each(function () {

                    var len = $(document).find('.' + pro_code).length;
                    var all_pcode = $(this).closest('tr').find('[name="pCode"]').val();
                    var id = $(this).attr('id');

                    if (len > 1) {

                        if (tis_id != id) {

                            if (all_pcode == pro_code) {

                                var all_selected_unit = $(this).closest('tr').find('select[name="unit"]').val();
                                var scheds = $(this).closest('tr').find('input[name = "txtScheme"]').val();
                                var scheds_erp = $(this).closest('tr').find('.erp_scheme_value').val();
                                var fr = $(this).closest('tr').find('input[name = "txtFree"]').val();
                                var fr_erp = $(this).closest('tr').find('.erp_free_value').val();
                                var fre_uni = $(this).closest('tr').find('select[name="free_unit"]').val();

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
                var freeuniiit = $(this).closest('tr').find('select[name="free_unit"] option:selected').val();
                var frq = $(this).closest('tr').find('input[name = "txtFree"]').val();

                var cNum = Number(sche || 0);

                if (cNum < 1) {
                    $(this).val('');
                    alert('Enter Scheme..!');
                    $(this).closest('tr').find('input[name = "txtScheme"]').focus();
                    $(this).closest('tr').find('input[name = "txtFree"]').prop("disabled", false);
                    return false;
                }
                else {
                    if ($(this).val() == '') {
                        $(this).closest('tr').find('input[name = "txtFree"]').prop("disabled", false);
                        $(this).closest('tr').find('select[name = "free_unit"]').prop("disabled", false);
                    } else {
                        $(this).closest('tr').find('input[name = "txtFree"]').prop("disabled", true);
                        $(this).closest('tr').find('select[name = "free_unit"] option[value="0"]').attr("selected", true);
                        $(this).closest('tr').find('select[name = "free_unit"]').prop("disabled", true)
                    }
                }
            });

            $(document).on('click', '.btnImport', function () {
                ExportXLToTable(
                    function (data) {
                        xldata = data;
                        $('#ddlType').val(2);
                        genTable();
                        fillData(xldata);
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

                var SchemeName = $('#<%=txtName.ClientID%>').val();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Primary_scheme.aspx/DeleteScheme",
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


            $(document).on('click', '.btnview', function () {
                $('#ddlType').val(2);
                insertType = '2';
                genTable();
                var scName = $(this).closest('tr').find('td').eq('1').text();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Primary_scheme.aspx/getSchemeVAl",
                    data: "{'schemeName':'" + scName + "'}",
                    dataType: "json",
                    success: function (data) {
                        scData = data.d;
                        fillData(scData);

                        var ef = scData[0].FDate;
                        var et = scData[0].TDate;

                        if (et >= today && ef <= today) {
                            $('#ctl00_ContentPlaceHolder1_txtTo').prop("disabled", true);
                            $('#ctl00_ContentPlaceHolder1_txtFrom').prop("disabled", true);
                           // $("#tbl1").find("input,button,textarea,select").attr("disabled", true);
                        }
                        else {
                            $('#ctl00_ContentPlaceHolder1_txtTo').prop("disabled", false);
                            $('#ctl00_ContentPlaceHolder1_txtFrom').prop("disabled", false);
                          //  $("#tbl1").find("input,button,textarea,select").attr("disabled", false);
                        }
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

                stC = '';
                state.each(function (idx, item) {
                    stC += $(item).attr('name') + ',';
                });

                var dist = $("#distributor input:checked");
                //if (dist.length == 0) {
                //    alert('Select Distributor..!');
                //    return false;
                //}
                dsC = '';
                ditributor = [];
                dist.each(function (idx, item) {
                    dsC += $(item).attr('name') + ',';
                    ditributor.push({
                        id: $(item).attr('name')
                    });
                });

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Primary_scheme.aspx/validateScheme",
                    data: "{'From_date':'" + from_date + "','State_Code':'" + stC + "','InsertType':'" + insertType + "','To_date':'" + to_date + "','Scheme_name':'" + schename + "'}",

                    dataType: "json",
                    success: function (data) {
                        Schme_stockist = JSON.parse(data.d) || [];

                        if (Schme_stockist.length > 0) {

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
                                //return false;
                            }
                            alert("Stockist Scheme Already Available From Date");
                            // alert("Stockist Scheme Already Available");
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
        <div class="container" style="min-width: 95%; width: 95%">
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
                                    <input type="text" runat="server" class="form-control  datetimepicker" autocomplete="off" id="txtFrom" name="txtFrom" />
                                </div>
                                <div class="form-group">
                                    <label for="txtTo">To:</label>
                                    <input type="text" runat="server" class="form-control  datetimepicker1" autocomplete="off" id="txtTo" name="txtTo" />
                                </div>
                                <div class="form-inline">
                                    <div class="form-group">
                                        <label for="txtName">Scheme Name</label>
                                        <input type="text" runat="server" class="form-control " id="txtName" name="txtName" />
                                    </div>

                                    <div class="form-group">
                                    </div>
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
        <%--        <table id="tbl" style="width: 50%" class="table table-bordered newStly">
            <thead></thead>
            <tbody></tbody>
        </table>--%>

        <table id="tbl1" class="table table-bordered newStly">
            <thead></thead>
            <tbody></tbody>
        </table>


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

