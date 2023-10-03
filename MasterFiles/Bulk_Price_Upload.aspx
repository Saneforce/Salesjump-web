<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Bulk_Price_Upload.aspx.cs" Inherits="MasterFiles_Bulk_Price_Upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #loader {
            position: absolute;
            left: 50%;
            top: 50%;
            z-index: 1;
            width: 120px;
            height: 120px;
            margin: -76px 0 0 -76px;
            border: 16px solid #f3f3f3;
            border-radius: 50%;
            border-top: 16px solid #3498db;
            -webkit-animation: spin 2s linear infinite;
            animation: spin 2s linear infinite;
        }

        .overlay {
            background-color: #EFEFEF;
            position: fixed;
            width: 100%;
            height: 100%;
            z-index: 1000;
            top: 0px;
            left: 0px;
            opacity: .5; /* in FireFox */
            filter: alpha(opacity=50); /* in IE */
        }
    </style>
    <script src="../js/lib/xls.core.min.js"></script>
    <script src="../js/lib/xlsx.core.min.js"></script>
    <script src="../js/lib/import_data.js"></script>
    <form id="form1" runat="server">
        <div class="container" style="width: 100%; margin-bottom: 30px;">
            <div class="row">
                <div class="col-md-5">
                    <div class="row" style="margin-top: 1rem;">
                        <label id="Label1" class="col-md-4">
                            Division</label>
                        <div>
                            <div class="col-xs-6">
                                <%--<select name="ddlDiv" id="ddlDiv" data-dropup-auto="false" data-size="5">
                                    <option selected="selected" value="0">--Select--</option>
                                </select>--%>
                                <select name="ddlDiv" id="ddlDiv" multiple>
                                    <option selected="selected" value="0">--Select--</option>
                                </select>
                            </div>
                        </div>
                    </div>

                    <div class="row" style="margin-top: 1rem;">
                        <label id="lblName" class="col-md-4">
                            Name</label>
                        <div>
                            <div class="col-xs-6">
                                <input  name="desig" class="form-control autoc ui-autocomplete-input" id="txt_name" style="width: 260px;" autocomplete="off" />
                                <datalist id="designation">
                                </datalist>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="cpyratee" style="margin-top: 1rem;">
                        <label id="lblrate" class="col-md-4">
                            Copy From</label>
                        <div>
                            <div class="col-xs-6">
                                <select name="ddlcpyrate" id="ddlcpyrate">
                                    <option selected="selected" value="0">--Select--</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="ratePeriod" style="margin-top: 1rem; display: none;">
                        <label id="lblperiod" class="col-md-4">
                            Period</label>
                        <div>
                            <div class="col-xs-6">
                                <select name="ddlPeriod" id="ddlPeriod">
                                    <option selected="selected" value="0">--Select--</option>
                                </select>
                            </div>

                        </div>
                    </div>
                    <div class="row" id="AddNew" style="margin-top: 1rem;">

                        <%--<button type="button" class="btn btn-success" id="btnAdd" style="font-size: 12px">+ AddDate </button>  --%>
                    </div>

                    <div class="row" id="EffFrom" style="margin-top: 1rem;">
                        <label id="Label4" class="col-md-4">
                            Effective From Date</label>
                        <div class="col-md-6 inputGroupContainer">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                <input type="text" name="From_Date" class="form-control" id="txtFrom_Date" placeholder="DD/MM/YYYY" />
                            </div>
                        </div>
                    </div>
                    <div class="row" id="EffTo" style="margin-top: 1rem;">
                        <label id="lbltodate" class="col-md-4">
                            Effective To Date</label>
                        <div class="col-md-6 inputGroupContainer">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                <input type="text" name="To_Date" class="form-control" id="txtTo_Date" placeholder="DD/MM/YYYY" />
                            </div>
                        </div>
                    </div>
                    <input type="text" name="Period_slno" class="form-control" id="txtPeriod_slno" style="display: none;" />
                    <div class="row" style="margin-top: 1rem; padding: 0px 0px 0px 215px;">
                        <button type="button" class="btn btn-primary btnAdd" id="btnAdd" style="display: none">+</button>
                        <button type="button" class="btn btn-primary btncancel" id="btncancel" style="display: none">-</button>
                        <button type="button" class="btn btn-primary btnsaveClass" id="btnView">View</button>
                        <asp:Button ID="Upldbt" CssClass="btn btn-primary" runat="server" Text="Excel File" OnClick="Upldbt_Click1" OnClientClick="return validateFunction();" />
                        <button type="button" class="btn btn-primary btnback" id="btnback" style="display: none;">Back</button>
                    </div>

                    <div class="row" style="margin-top: 2rem;">
                        <div class="col-md-4">
                            <label id="Label6" class="col-md-4">
                                Upload File</label>
                        </div>
                        <div class="col-md-6">
                            <input type="file" id="excelfile">
                        </div>
                    </div>

                    <div class="row" style="margin-top: 1rem; padding: 0px 0px 0px 215px;">
                        <button type="button" class="btn btn-primary btnUpload" id="btnUpload">Upload</button>
                    </div>
                </div>

                <div class="col-md-7">
                    <div class="row">
                        <table id="view_rate_card" border="1" style="width: 60%; border-collapse: collapse; inline-size: fit-content;">
                            <thead style="background-color: #496a9a"></thead>
                            <tbody></tbody>
                            <tfoot></tfoot>
                        </table>
                    </div>
                </div>
                <br />
            </div>
            <br />
            <div id="NoRecord"></div>
            <div class="overlay" id="loadover" style="display: none;">
                <div id="loader"></div>
            </div>
            <table id="Product_List" border="1" style="width: 100%; border-collapse: collapse;">
                <thead class="text-warning" style="background-color: #4697ce;"></thead>
                <tbody></tbody>
                <tfoot></tfoot>
            </table>
            <br />

            <div class="form-inline footr">
                <div class="form-group" style="margin-left: 55px;">
                    <button type="button" class="btn btn-primary btnsaveClass" id="btnSaveId">Save</button>
                </div>
            </div>
            <asp:HiddenField ID="hidden_div" runat="server" />

        </div>
    </form>
    <link href="../css/jquery.multiselect.css" rel="stylesheet" />
    <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
    <link rel="stylesheet" type="text/css" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.10.4/themes/redmond/jquery-ui.css">

    <style type="text/css">
        select {
            width: 100%;
            border: 1px solid #D5D5D5 !important;
            padding: 6px 6px 7px !important;
        }

            select:focus {
                outline: none;
                box-shadow: 0 0 0 3px rgba(21, 156, 228, 0.4);
            }

        input[type='text'], select {
            border-radius: 5px;
        }

        .ui-autocomplete {
            max-height: 300px;
            overflow-y: auto;
            overflow-x: hidden;
        }

        .ui-menu-item > a {
            display: block;
        }

        table.scroll {
            border-spacing: 0;
        }

            table.scroll tbody,
            table.scroll thead {
                display: block;
            }

            table.scroll tbody {
                height: 300px;
                overflow-y: auto;
                overflow-x: hidden;
            }

        tbody td:last-child, thead th:last-child {
            border-right: none;
        }
    </style>

    <script type="text/javascript">

        var divName = []; var PRoduct_details = []; var slno = ''; var HeadsData = []; var BodyData = []; var All_unit = []; var UnitCode = ''; var units = '';
        var xldata = []; var All_Product = [];
        //var All_unit_new = [];
        var divName = []; var ratePeriod = [];

        $(document).ready(function () {



            $('#txtPeriod_slno').val(0);

            $('#btnSaveId').hide();
            getdatalist();

            var now = new Date();
            var day = ("0" + now.getDate()).slice(-2);
            var month = ("0" + (now.getMonth() + 1)).slice(-2);
            var today = now.getFullYear() + "-" + (month) + "-" + (day);
            var datecheck = day + "/" + month + "/" + now.getFullYear();

            $("#txtFrom_Date").datepicker({
                dateFormat: "dd/mm/yy",
                minDate: now,
                onSelect: function (date) {
                    $("#txtTo_Date").datepicker('option', 'minDate', date);
                }
            });

            $("#txtTo_Date").datepicker({
                dateFormat: "dd/mm/yy",
                minDate: $("#txtFrom_Date").val(),
            });

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Bulk_Price_Upload.aspx/DivName",
                dataType: "json",
                success: function (data) {
                    divName = JSON.parse(data.d) || [];
                    getsubdivision(divName);
                },
                error: function (result) {
                }
            });
            //$('#ddlDiv').selectpicker({
            //    liveSearch: true
            //});
            function getsubdivision(divName) {
                $('#ddlDiv').empty();//.append('<option selected="selected" value="">Select Division</option>');
                if (divName.length > 0) {
                    $.each(divName, function () {
                        $('#ddlDiv').append($("<option></option>").val(this['subdivision_code']).html(this['subdivision_name'])).trigger('chosen:updated').css("width", "100%");;;
                    });
                }
                $('#ddlDiv').multiselect({
                    columns: 3,
                    placeholder: 'Select Division',
                    search: true,
                    searchOptions: {
                        'default': 'Search Division'
                    },
                    selectAll: true
                }).multiselect('reload');
                $('#ddlDiv-options ul').css('column-count', '3');
            }

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Bulk_Price_Upload.aspx/Get_Product_unit",
                dataType: "json",
                success: function (data) {
                    All_unit = JSON.parse(data.d) || [];
                },
                error: function (result) {
                }
            });
            //$.ajax({
            //    type: "POST",
            //    contentType: "application/json; charset=utf-8",
            //    async: false,
            //    url: "Bulk_Price_Upload.aspx/Get_Product_unit_new",
            //    dataType: "json",
            //    success: function (data) {
            //        All_unit_new = JSON.parse(data.d) || [];
            //    },
            //    error: function (result) {
            //    }
            //});

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Bulk_Price_Upload.aspx/Get_All_product",
                dataType: "json",
                success: function (data) {
                    All_Product = JSON.parse(data.d) || [];
                },
                error: function (result) {
                }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Bulk_Price_Upload.aspx/get_view_unit",
                dataType: "json",
                success: function (data) {
                    View_unit_details = JSON.parse(data.d) || [];
                },
                error: function (result) {
                }
            });

            function getdatalist() {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Bulk_Price_Upload.aspx/GetDesgnName",
                    dataType: "json",
                    success: function (data) {
                        ListName = JSON.parse(data.d) || [];
                        ratelist = [];
                        if (ListName.length > 0) {
                            var st = $('#designation');
                            var list_name = '';
                            for (var i = 0; i < ListName.length; i++) {
                                if (list_name != ListName[i].Price_list_Name) {
                                    st.append($('<option data-xyz=' + ListName[i].Price_list_Sl_No + ' value="' + ListName[i].Price_list_Name + '">'));
                                    list_name = ListName[i].Price_list_Name;
                                    // st.append($('<option value="' + ListName[i].Price_list_Name + '">'));
                                }
                            }
                        }
                        var str = '';
                        tbl = $('#view_rate_card');
                        $(tbl).find('thead tr').remove();
                        $(tbl).find('tbody tr').remove();
                        if (ListName.length > 0) {

                            //str = '<th style="width:5%;">SlNo.</th><th style="width:10%;">Rate Card Name</th><th style="width:10%;">Division</th><th style="width:10%;">Edit</th>';
                            str = '<th style="text-align:left;width:5%;color: white;">Sl.No</th><th style="color: white;">RateCard Name</th><th style="color: white;">Division</th><th style="color: white;">Effective From</th><th style="color: white;">Effective To</th><th style="color: white;">Dist Count</th><th style="color: white;">Superstk Count</th><th style="color: white;">Edit</th>';
                            $(tbl).find('thead').append('<tr>' + str + '</tr>');
                            var prdDate = 0;
                            var slno = 1;
                            $('#ddlcpyrate').empty().append('<option value="0">--select--</option>');
                            for (var i = 0; i < ListName.length; i++) {
                                var str1 = '';

                                if (prdDate != ListName[i].Price_list_Sl_No) {
                                    str1 += '<td style="text-align:left;width:5%;"><span>' + slno + '</span></td>';
                                    str1 += '<td style="text-align:left;"><span>' + ListName[i].Price_list_Name + '</span></td>';
                                    str1 += '<td style="text-align:left;"> <span>' + ListName[i].subdivision_name + '</span></td > ';
                                    str1 += '<td style="text-align:left;display:none;"><span>' + ListName[i].Division_Code + '</span></td>';
                                    str1 += '<td style="text-align:left;"><span>' + ListName[i].Effective_From_Date + '</span></td>';
                                    str1 += '<td style="text-align:left;"><span>' + ListName[i].Effective_To_Date + '</span></td>';
                                    str1 += '<td style="text-align:left;" class="rodist"><span><a href="#">' + ListName[i].Dist_count + '</a></span></td>';
                                    str1 += '<td style="text-align:left;" class="rosup"><span><a href="#">' + ListName[i].sup_count + '</a></span></td>';
                                    str1 += '<td style="text-align:left;" class="roedit"><span><a href="#">Edit</a></span></td>';
                                    $(tbl).find('tbody').append('<tr rosubdiv="' + ListName[i].sub_div_code + '" ropricename="' + ListName[i].Price_list_Name + '" ropriceno="' + ListName[i].Price_list_Sl_No + '" roEffFrom="' + ListName[i].Effective_From_Date + '" roEffTo="' + ListName[i].Effective_To_Date + '" roPeriodno="' + ListName[i].Period_Sl_No + '">' + str1 + '</tr>');
                                    slno += 1;
                                    //('#ddlcpyrate').
                                    $('#ddlcpyrate').append('<option value="' + ListName[i].Price_list_Sl_No + '">' + ListName[i].Price_list_Name + '</option>');
                                }
                                prdDate = ListName[i].Price_list_Sl_No;
                            }
                        }
                        else {
                            str = '<th style="width:10%;">SlNo.</th><th style="width:10%;">Rate Card Name</th><th style="width:10%;">Division</th>';
                            $(tbl).find('thead').append('<tr>' + str + '</tr>');
                            str = '<td colspan="3" style="color:red">No Records Found..!</td>';
                            $(tbl).find('tbody').append('<tr>' + str + '</tr>');
                        }
                    },
                    error: function (data) {
                        alert(JSON.stringify(data));
                    }
                });
            }
            $(document).on('keyup', '.ret_rate ,.Dis_price ,.MRP_Price', function (event) {
                // Allow backspace, delete, and arrow keys
                if (event.which == 8 || event.which == 46 || event.which == 37 || event.which == 39) {
                    return;
                }

                // Check if input is a valid decimal number with at most 2 decimal places
                var inputValue = $(this).val();
                var regex = /^\d+(\.\d{0,2})?$/;
                if (!regex.test(inputValue)) {
                    // If input is not valid, remove any decimal places beyond 2
                    var newValue = parseFloat(inputValue).toFixed(2);
                    if (isNaN(newValue)) {
                        $(this).val('');
                    } else {
                        $(this).val(newValue);
                    }
                }
            });
            function limitToTwoDecimal(input) {
                // Get the input value as a number with two decimal places
                let value = parseFloat(input.value).toFixed(2);

                // If the input is not a valid number, set the value to an empty string
                if (isNaN(value)) {
                    value = '';
                }

                // Update the input value with the limited decimal places
                input.value = value;
            }
            function getratePeriod(rowpriceNo) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Bulk_Price_Upload.aspx/GetRatePeriods",
                    dataType: "json",
                    data: "{'Price_list_Code':'" + rowpriceNo + "'}",
                    success: function (data) {
                        ratePeriod = JSON.parse(data.d) || [];
                        $('#ddlPeriod').empty().append('<option value="0">--select--</option>')
                        for (k = 0; k < ratePeriod.length; k++) {
                            if (ratePeriod[k].Effective_From_Date != '') {
                                var perioddata = ratePeriod[k].Effective_From_Date + "- " + ratePeriod[k].Effective_To_Date;
                                $('#ddlPeriod').append('<option value="' + ratePeriod[k].Period_Sl_No + '">' + perioddata + '</option>');
                            }
                        }
                    },
                    error: function (data) {
                        alert(JSON.stringify(data));
                    }
                });
            }
			
            function getratecpy(subdivision) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Bulk_Price_Upload.aspx/GetRateCopy",
                    dataType: "json",
                    data: "{'subdiv':'" + subdivision + "'}",
                    success: function (data) {
                        ratecpy = JSON.parse(data.d) || [];
                        $('#ddlcpyrate').empty().append('<option value="0">--select--</option>')
                        for (k = 0; k < ratecpy.length; k++) {

                            $('#ddlcpyrate').append('<option value="' + ratecpy[k].Price_list_Sl_No + '">' + Price_list_Name + '</option>');

                        }
                    },
                    error: function (data) {
                        alert(JSON.stringify(data));
                    }
                });
            }
            //$(document).on('click', '.rodist', function () {
            //    var rowpriceNo = parseInt($(this).closest('tr').attr('ropriceno'));
            //    var rowpriceName = $(this).closest('tr').attr('ropricename');
            //    window.open("Price_list_distributor.aspx?price_code=" + rowpriceNo + "&price_name=" + rowpriceName, "ModalPopUp", "null," + "toolbar=no," + "scrollbars=yes," + "location=no," + "statusbar=no," + "menubar=no," + "addressbar=no," + "resizable=yes," + "width=800," + "height=600," + "left = 0," + "top=0");
            //});
            $(document).on('click', '.rodist', function () {
                var rowpriceNo = parseInt($(this).closest('tr').attr('ropriceno'));
                var rowpriceName = $(this).closest('tr').attr('ropricename');
                var cttype = 'dist';
                window.open("Price_list_distributor.aspx?price_code=" + rowpriceNo + "&price_name=" + rowpriceName + "&cttype=" + cttype, "ModalPopUp", "null," + "toolbar=no," + "scrollbars=yes," + "location=no," + "statusbar=no," + "menubar=no," + "addressbar=no," + "resizable=yes," + "width=800," + "height=600," + "left = 0," + "top=0");
            });
            $(document).on('click', '.rosup', function () {
                var rowpriceNo = parseInt($(this).closest('tr').attr('ropriceno'));
                var rowpriceName = $(this).closest('tr').attr('ropricename');
                var cttype = 'supstk';
                window.open("Price_list_distributor.aspx?price_code=" + rowpriceNo + "&price_name=" + rowpriceName + "&cttype=" + cttype, "ModalPopUp", "null," + "toolbar=no," + "scrollbars=yes," + "location=no," + "statusbar=no," + "menubar=no," + "addressbar=no," + "resizable=yes," + "width=800," + "height=600," + "left = 0," + "top=0");
            });
            $(document).on('click', '.roedit', function () {
                var rowsubdivision = $(this).closest('tr').attr('rosubdiv');
                var rowpriceNo = parseInt($(this).closest('tr').attr('ropriceno'));
                var rowpriceName = $(this).closest('tr').attr('ropricename');
                var rowEffFrom = $(this).closest('tr').attr('roEffFrom');
                var rowEffTo = $(this).closest('tr').attr('roEffTo');
                var rowPeriodNo = $(this).closest('tr').attr('roPeriodno');

                $('#Product_List').hide();

                $("#ddlDiv option:selected").prop("selected", false);
                $('#txt_name').val(rowpriceName);
                $('#txt_name').text(rowpriceNo);
                $('#txtFrom_Date').val(rowEffFrom);
                $('#txtTo_Date').val(rowEffTo);
                $('#txtPeriod_slno').val(rowPeriodNo);

                //getratePeriod(rowpriceNo);

                $('#ratePeriod').hide();
                $('#EffFrom').show();
                $('#EffTo').show();
                $('#AddNew').hide();
                $('#cpyratee').hide();
                $('#txt_name').attr('readonly', 'true');

                subdivarr = rowsubdivision.split(',');
                for (j = 0; j < subdivarr.length; j++) {
                    if (subdivarr[j] != '') {
                        $('#ddlDiv option[value="' + subdivarr[j] + '"]').attr("selected", "selected");
                    }
                }
                $('#ddlDiv').multiselect('reload');
				$('#btnView').trigger('click');
            });

            $('#ddlPeriod').on('change', function () {
                var price_list_val = $('#txt_name').val();
                var price_list_text = $('#txt_name').text();
                var PeriodSl_No = $('#ddlPeriod option:selected').val();
                var getdata = ListName.filter(function (a) {
                    return a.Period_Sl_No == PeriodSl_No;
                });
                $('#txtFrom_Date').val(getdata[0].Effective_From_Date);
                $('#txtTo_Date').val(getdata[0].Effective_To_Date);
                var Sub_div_code = '';
                $('#ddlDiv > option:selected').each(function () {
                    Sub_div_code += $(this).val() + ',';
                });
                $('#btnback').show();
                $('#EffFrom').hide();
                $('#EffTo').hide();
                $('#btnAdd').hide();
                $('#btncancel').hide();

                if (PeriodSl_No == 0) {
                    getdataa(Sub_div_code, 0, 0, PeriodSl_No);
                } else
                    getdataa(Sub_div_code, price_list_val, price_list_text, PeriodSl_No);
            });
            $('#ddlcpyrate').on('change', function () {
                subdiv = $('#ddlDiv  option:selected').val();
                rateVal = $('#ddlcpyrate  option:selected').val();
                rateName = $('#ddlcpyrate  option:selected').text();
                if (rateVal != 0) {
                    var filtered_rt = ListName.filter(function (r) {
                        return (r.Price_list_Sl_No == rateVal);
                    });
                    getdataa(filtered_rt[0].sub_div_code, rateName, rateVal, 0);
                }
                else
                    getdataa(subdiv, 0, 0, 0);

            });

            $('#ddlDiv').change(function () {
                ddldivValue = '';
                $('#ddlDiv > option:selected').each(function () {
                    ddldivValue += $(this).val() + ',';
                });
                document.getElementById("<%= hidden_div.ClientID %>").value = ddldivValue;
                var filterfn = ListName.filter(function (r) {
                    return (r.sub_div_code == ddldivValue);
                });
                var list_name = '';
                if (filterfn.length > 0) {
                    $('#ddlcpyrate').empty().append('<option value="0">--select--</option>')
                    for (k = 0; k < filterfn.length; k++) {
                        if (list_name != filterfn[k].Price_list_Name) {
                            $('#ddlcpyrate').append('<option value="' + filterfn[k].Price_list_Sl_No + '">' + filterfn[k].Price_list_Name + '</option>');
                            list_name = filterfn[k].Price_list_Name;
                        }
                    }
                }

               <%-- var selected_div_code = $('#ddlDiv option:selected').val();
                var selected_div_Name = $('#ddlDiv option:selected').text();
                document.getElementById("<%= hidden_div.ClientID %>").value = selected_div_code;--%>

            });

            $(document).on('change', '.unit', function () {

                var selected_unit_code = $('#unit option:selected').val();
                var selected_unit_name = $('#unit option:selected').text();
                var pcode = $(this).closest('tr').find('.prodCode').val();
                var filter_data = [];

                filter_data = View_unit_details.filter(function (t) {
                    return (t.PCode == pcode && t.UOM_Id == selected_unit_code);
                });

                $(this).closest('tr').find('.Default_con_fac').val(filter_data[0].CnvQty);
                $(this).closest('tr').find('.Default_umo_code').val(filter_data[0].UOM_Id);

            });
            $('#btnAdd').click(function () {
                $('#ratePeriod').hide();
                $('#EffFrom').show();
                $('#EffTo').show();
            });
            $('#btncancel').click(function () {
                $('#EffFrom').hide();
                $('#EffTo').hide();
                $('#ratePeriod').hide();
            });
            $('#btnback').click(function () {
                window.location.href = 'Bulk_Price_Upload.aspx';
            })

            $('#btnUpload').click(function () {
				   var fileInput = document.getElementById('excelfile');
                var files = fileInput.files;

                if (files.length === 0) {
                    alert("Please select at least one file!");
                    return;
                }

                var validFileTypes = ["application/vnd.ms-excel", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"];
                var validFiles = [];

                for (var i = 0; i < files.length; i++) {
                    var file = files[i];
                    var fileType = file.type;

                    if (validFileTypes.indexOf(fileType) !== -1) {
                        var fileName = file.name;
                        var newFileName = fileName.replace(/\([^()]+\)/g, ""); // Remove parentheses and their contents from the file name
                        var renamedFile = new File([file], newFileName, { type: fileType });
                        validFiles.push(renamedFile);
                    }
                }

                if (validFiles.length === 0) {
                    alert("Please upload valid Excel files!");
                    $("#excelfile").val('');
                    return;
                }

                // Process each valid file
                for (var j = 0; j < validFiles.length; j++) {
                    var currentFile = validFiles[j];
                    ExportXLToTable(
                        function (data) {
                            xldata = data;
			     var xldata1 = [];                            
				var xl_Filter_view_unit = [];
			  
                            for (var f = 0; f < xldata.length; f++) {

                                var filtered_xl = All_Product.filter(function (r) {
                                    return (r.Sale_Erp_Code == xldata[f].Sap_Code);
                                });

                                //xl_Filter_view_unit = View_unit_details.filter(function (t) {
                                //    return (t.PCode == xldata[f].Sap_Code && t.UOM_Id == filtered_xl[0].Default_umo);
                                //});

                                //if (Filter_view_unit.length > 0) {

                                //    var con = Filter_view_unit[0].CnvQty;
                                //}
                                //else {
                                //    var con = 0;
                                //}

                                if (filtered_xl.length > 0) {
									 if (xldata[f]["Product_Sale_Unit"] !== filtered_xl[0].Product_Sale_Unit)
									 {
										alert("Product Unit not match with product sale unit in line  " + (f + 2) + " kindly Change and upload again ..");
										return; 
								     }
                                    xldata[f]["product_detail_code"] = filtered_xl[0].product_detail_code;
                                    xldata[f]["product_detail_name"] = filtered_xl[0].product_detail_name;
                                    xldata[f]["Product_Sale_Unit"] = filtered_xl[0].Product_Sale_Unit;
                                    xldata[f]["product_unit"] = filtered_xl[0].product_unit;
                                    xldata[f]["Base_Unit_code"] = filtered_xl[0].Base_Unit_code;
                                    xldata[f]["Unit_code"] = filtered_xl[0].Unit_code;
                                    xldata[f]["Sample_Erp_Code"] = filtered_xl[0].Sample_Erp_Code;
                                    xldata[f]["UnitCode"] = filtered_xl[0].Unit_code;
                                    xldata[f]["con_fac"] = filtered_xl[0].con_fac;
                                    xldata[f]["Default_umo"] = filtered_xl[0].Default_umo;
                                    xldata[f]["Default_con"] = filtered_xl[0].Default_con;
                                    xldata[f]["UOM_Weight"] = filtered_xl[0].UOM_Weight;
                                    xldata[f].Customer_Price == undefined || xldata[f].Customer_Price == "" ? xldata[f].Customer_Price = 0 : xldata[f].Customer_Price = xldata[f].Customer_Price;
                                    xldata[f].Purchase_Price == undefined || xldata[f].Purchase_Price == "" ? xldata[f].Purchase_Price = 0 : xldata[f].Purchase_Price = xldata[f].Purchase_Price;
                                    xldata[f].MRP_Price == undefined || xldata[f].MRP_Price == "" ? xldata[f].MRP_Price = 0 : xldata[f].MRP_Price = xldata[f].MRP_Price;
                                 xldata1.push(xldata[f]);
}
				
                            }
                            bind_Data_xcel(xldata1, 0);
                        },
                        function (Msg) {
                            console.log(Msg);
                        }
                    );
                }
                //ExportXLToTable(
                   // function (data) {
                    //    xldata = data;
                     //   var xl_Filter_view_unit = [];

                     //   for (var f = 0; f < xldata.length; f++) {

                       //     var filtered_xl = All_Product.filter(function (r) {
                       //         return (r.Sale_Erp_Code == xldata[f].Sap_Code);
                        //    });

                           // //xl_Filter_view_unit = View_unit_details.filter(function (t) {
                           // //    return (t.PCode == xldata[f].Sap_Code && t.UOM_Id == filtered_xl[0].Default_umo);
                           // //});

                           // //if (Filter_view_unit.length > 0) {

                           // //    var con = Filter_view_unit[0].CnvQty;
                           // //}
                           // //else {
                           // //    var con = 0;
                            ////}

                          //  if (filtered_xl.length > 0) {

                           //     xldata[f]["product_detail_code"] = filtered_xl[0].product_detail_code;
                           //     xldata[f]["product_detail_name"] = filtered_xl[0].product_detail_name;
                            //    xldata[f]["Product_Sale_Unit"] = filtered_xl[0].Product_Sale_Unit;
                            //    xldata[f]["product_unit"] = filtered_xl[0].product_unit;
                            //    xldata[f]["Base_Unit_code"] = filtered_xl[0].Base_Unit_code;
                            //    xldata[f]["Unit_code"] = filtered_xl[0].Unit_code;
                            //    xldata[f]["Sample_Erp_Code"] = filtered_xl[0].Sample_Erp_Code;
                            //    xldata[f]["UnitCode"] = filtered_xl[0].Unit_code;
                            //    xldata[f]["con_fac"] = filtered_xl[0].con_fac;
                            //    xldata[f]["Default_umo"] = filtered_xl[0].Default_umo;
                            //    xldata[f]["Default_con"] = filtered_xl[0].Default_con;
                            //    xldata[f]["UOM_Weight"] = filtered_xl[0].UOM_Weight;
                            //    xldata[f].Retailer_Price == undefined || xldata[f].Retailer_Price == "" ? xldata[f].Retailer_Price = 0 : xldata[f].Retailer_Price = xldata[f].Retailer_Price;
                            //    xldata[f].Distributor_Price == undefined || xldata[f].Distributor_Price == "" ? xldata[f].Distributor_Price = 0 : xldata[f].Distributor_Price = xldata[f].Distributor_Price;
                           //     xldata[f].MRP_Price == undefined || xldata[f].MRP_Price == "" ? xldata[f].MRP_Price = 0 : xldata[f].MRP_Price = xldata[f].MRP_Price;
                           // }
                        //}
                      //  bind_Data(xldata, 0);
                   // },
                   // function (Msg) {
                   //     console.log(Msg);
                   // }
               // )
            });

            $('#btnView').click(function () {

                $('#loadover').show();
                setTimeout(function () {
                    setTimeout(loadData(), 100);
                }, 100);
                function loadData() {


                    var FromDate = $('#txtFrom_Date').val();
                    var ToDate = $('#txtTo_Date').val();
                    //var Sub_div_code = $('#ddlDiv :selected').val() || 0;
                    var Sub_div_code = '';
                    $('#ddlDiv > option:selected').each(function () {
                        Sub_div_code += $(this).val() + ',';
                    });
                    var Name = $('#txt_name').val();

                    var xyz = $('#designation option').filter(function () {
                        return this.value == Name;
                    }).data('xyz');

                    if (Sub_div_code == "" || Sub_div_code == 0) { alert("Select Division"); $('#ddlDiv').focus(); return false; }
                    if (Name == "") { alert("Select Name"); $('#txt_name').focus(); return false; }
                    //if (FromDate == "") { alert("Select FromDate"); $('#txtFrom_Date').focus(); return false; }
                    // if (ToDate == "") { alert("Select ToDate"); $('#txtTo_Date').focus(); return false; }
                    var PeriodSl_No = 0;
                    PeriodSl_No = $('#ddlPeriod option:selected').val();
                    if (PeriodSl_No == 0) {
                        getdataa(Sub_div_code, Name, xyz, 0);
                    } else
                        getdataa(Sub_div_code, Name, xyz, PeriodSl_No);
                }

            });

            function getdataa(Sub_div_code, Name, Code, Date_period) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    data: "{'Subdiv_code':'" + Sub_div_code + "','Name': '" + Name + "','Code':'" + Code + "','DatePeriod':'" + Date_period + "'}",
                    url: "Bulk_Price_Upload.aspx/Get_rate_Details",
                    dataType: "json",
                    success: function (data) {
                        PRoduct_details = JSON.parse(data.d) || [];
                        bind_Data(PRoduct_details, 1);
                    },
                    error: function (result) {
                    }
                });
            }
            //function loadunitconv() {
            //    $.ajax({
            //        type: "POST",
            //        contentType: "application/json; charset=utf-8",
            //        async: false,
            //        data: "{'Subdiv_code':'" + Sub_div_code + "','Name': '" + Name + "','Code':'" + Code + "','DatePeriod':'" + Date_period + "'}",
            //        url: "Bulk_Price_Upload.aspx/Get_Product_unit_new",
            //        dataType: "json",
            //        success: function (data) {
            //            All_unit_new = JSON.parse(data.d) || [];
            //        },
            //        error: function (result) {
            //        }
            //    });
            //}
			function bind_Data_xcel(PRoduct_details, type) {

                if (PRoduct_details.length <= 0) {
                    $('#NoRecord').html("<h3 style='text-align:center;color:blue;'>No Records found</h1>");
                    $('#Product_List thead').html('');
                    $('#Product_List tbody').html('');
                    $('#btnSaveId').hide();
                }
                else {
                    $('#NoRecord').html('');
                    $('#Product_List thead').html('');
                    var thead = $("<tr style='text-align:center'><th>S.NO</th><th>Product Name</th><th>UOM/Per</th><th>Customer Price</th><th>Purchase Price</th>><th>MRP Price</th></tr>");
                    $('#Product_List thead').append(thead);
                    $('#Product_List tbody').html('');
                    var str = '';

                    for (var g = 0; g < PRoduct_details.length; g++) {
                        slno = g + 1;

                        var filter_unit = []; units1 = ""; var Pro_filter = []; units = ''; units += "<option value='0'>Select Unit</option>"
                        var Filter_view_unit = [];
                        var All_unit_new = [];
                        var pcode = PRoduct_details[g].product_detail_code;

                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            data: "{'pcode':'" + pcode + "','Div':'29'}",
                            url: "Bulk_Price_Upload.aspx/Get_Product_unitconv",
                            dataType: "json",
                            success: function (data) {
                                All_unit_new = JSON.parse(data.d) || [];
                            },
                            error: function (result) {
                            }
                        });

                        for (var z = 0; z < All_unit_new.length; z++) {
						 if ( All_unit_new[z].UOM_Id == PRoduct_details[g].Base_Unit_code) {
                                //units += "<option selected name='itms' value='" + PRoduct_details[g].Unit_code + "'>" + PRoduct_details[g].Unit + "</option>";                                
                                units += "<option  selected name='itms' value='" + All_unit_new[z].UOM_Id + "'>" + All_unit_new[z].UOM_Nm + "</option>";
                                //units += "<option selected name='itms' value='" + PRoduct_details[g].UOM + "'>" + PRoduct_details[g].Unit + "</option>";

                                //units += "<option selected name='itms' value='" + PRoduct_details[g].Unit_code + "'>" + PRoduct_details[g].product_unit + "</option>";

                            }
                           
                            else {
                                units += "<option name='itms' value='" + All_unit_new[z].UOM_Id + "'>" + All_unit_new[z].UOM_Nm + "</option>";
                            }
                        }

                        Filter_view_unit = View_unit_details.filter(function (t) {
                            return (t.PCode == PRoduct_details[g].product_detail_code && t.UOM_Id == PRoduct_details[g].Default_umo);
                        });

                        if (Filter_view_unit.length > 0) {

                            var con = Filter_view_unit[0].CnvQty;
                        }
                        else {
                            var con = 0;
                        }
						var ret_price= PRoduct_details[g].Retailer_Price==undefined ? PRoduct_details[g].Customer_Price : PRoduct_details[g].Retailer_Price ;
						var dis_price= PRoduct_details[g].Distributor_Price==undefined ? PRoduct_details[g].Purchase_Price : PRoduct_details[g].Distributor_Price ;
                        var tr = $("<tr><td style='text-align:center'>" + slno + "</td>" +
                            "<td style='display:none;'><input type='text' class='prodCode' style='text-align:center' value=" + PRoduct_details[g].product_detail_code + "></td>" +
                            "<td style='text-align:left' class='prodName' value='" + PRoduct_details[g].product_detail_name + "'>" + PRoduct_details[g].product_detail_name + "</td>" +
                            "<td><select id=unit class='unit'>" + units + "</select></td>" +
                            "<td style='display:none;'><input type='text' class='Product_Sale_Unit' style='text-align:center' value=" + PRoduct_details[g].Product_Sale_Unit + "></td>" +
                            "<td style='display:none;'><input type='text' class='Product_Unit' style='text-align:center' id='Product_Unit' value=" + PRoduct_details[g].product_unit + "></td>" +
                            "<td style='display:none;'><input type='text' style='text-align:center' class='Product_Sale_Unit_code' value=" + PRoduct_details[g].Base_Unit_code + "></td>" +
                            "<td style='display:none;'><input type='text' style='text-align:center' class='Product_Unit_code' value=" + PRoduct_details[g].Unit_code + "></td>" +
                            "<td style='display:none;'><input type='text' style='text-align:center' class='Product_sample_erp_code' value=" + PRoduct_details[g].Sample_Erp_Code + "></td>" +
                            "<td style='display:none;'><input type='text' class='UomWeight' style='text-align:center' value=" + PRoduct_details[g].UOM_Weight + "></td>" +
                            "<td style='text-align:center'><input type='text' class='ret_rate'  style='text-align:center' value=" +ret_price+ "></td>" +
                            "<td style='text-align:center'><input type='text' class='Dis_price'   style='text-align:center' value=" +dis_price + "></td>" +
                            "<td style='text-align:center'><input type='text' class='MRP_Price'  style='text-align:center' value=" + PRoduct_details[g].MRP_Price + "></td>" +
                            "<td style='display:none'><input type='hidden' class='Default_con_fac' value=" + con + "></td>" +
                            "<td style='display:none'><input type='hidden' class='Default_umo_code' value=" + PRoduct_details[g].Default_umo + "></td>" +
                            "<td style='display:none'><input type='hidden' class='Default_con_fac' value=" + PRoduct_details[g].Default_con + "></td>" +
                            "</tr>");
                        $('#Product_List tbody').append(tr);
                    }
                    $('#Product_List').css("display", "inline-table");
                    $('#btnSaveId').show();
                    $('#loadover').hide();
                }
            }

            function bind_Data(PRoduct_details, type) {

                if (PRoduct_details.length <= 0) {
                    $('#NoRecord').html("<h3 style='text-align:center;color:blue;'>No Records found</h1>");
                    $('#Product_List thead').html('');
                    $('#Product_List tbody').html('');
                    $('#btnSaveId').hide();
                }
                else {
                    $('#NoRecord').html('');
                    $('#Product_List thead').html('');
                    var thead = $("<tr style='text-align:center'><th>S.NO</th><th>Product Name</th><th>UOM/Per</th><th>Customer Price</th><th>Purchase Price</th>><th>MRP Price</th></tr>");
                    $('#Product_List thead').append(thead);
                    $('#Product_List tbody').html('');
                    var str = '';

                    for (var g = 0; g < PRoduct_details.length; g++) {
                        slno = g + 1;

                        var filter_unit = []; units1 = ""; var Pro_filter = []; units = ''; units += "<option value='0'>Select Unit</option>"
                        var Filter_view_unit = [];
                        var All_unit_new = [];
                        var pcode = PRoduct_details[g].product_detail_code;

                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            data: "{'pcode':'" + pcode + "','Div':'<%=Session["div_code"]%>'}",
                            url: "Bulk_Price_Upload.aspx/Get_Product_unitconv",
                            dataType: "json",
                            success: function (data) {
                                All_unit_new = JSON.parse(data.d) || [];
                            },
                            error: function (result) {
                            }
                        });

                        for (var z = 0; z < All_unit_new.length; z++) {
						if (PRoduct_details[g].Unit != '' && All_unit_new[z].UOM_Id == PRoduct_details[g].UnitCode) {
                                units += "<option  selected name='itms' value='" + All_unit_new[z].UOM_Id + "'>" + All_unit_new[z].UOM_Nm + "</option>";
                            }
			    else if (PRoduct_details[g].Unit != '' && All_unit_new[z].UOM_Id == PRoduct_details[g].Base_Unit_code) {
                                //units += "<option selected name='itms' value='" + PRoduct_details[g].Unit_code + "'>" + PRoduct_details[g].Unit + "</option>";                                
                                units += "<option  selected name='itms' value='" + All_unit_new[z].UOM_Id + "'>" + All_unit_new[z].UOM_Nm + "</option>";
                                //units += "<option selected name='itms' value='" + PRoduct_details[g].UOM + "'>" + PRoduct_details[g].Unit + "</option>";

                                //units += "<option selected name='itms' value='" + PRoduct_details[g].Unit_code + "'>" + PRoduct_details[g].product_unit + "</option>";

                            }
                            //else if (PRoduct_details[g].Unit != '' && All_unit_new[z].UOM_Id == PRoduct_details[g].UnitCode) {
                                //units += "<option selected name='itms' value='" + PRoduct_details[g].Unit_code + "'>" + PRoduct_details[g].Unit + "</option>";                                
                                //units += "<option  selected name='itms' value='" + All_unit_new[z].UOM_Id + "'>" + All_unit_new[z].UOM_Nm + "</option>";
                                //units += "<option selected name='itms' value='" + PRoduct_details[g].UOM + "'>" + PRoduct_details[g].Unit + "</option>";

                                //units += "<option selected name='itms' value='" + PRoduct_details[g].Unit_code + "'>" + PRoduct_details[g].product_unit + "</option>";

                            //}
                            else if (All_unit_new[z].UOM_Id == PRoduct_details[g].Default_umo) {
                                units += "<option selected name='itms' value='" + All_unit_new[z].UOM_Id + "'>" + All_unit_new[z].UOM_Nm + "</option>";
                            }
                            else {
                                units += "<option name='itms' value='" + All_unit_new[z].UOM_Id + "'>" + All_unit_new[z].UOM_Nm + "</option>";
                            }
                        }

                        Filter_view_unit = View_unit_details.filter(function (t) {
                            return (t.PCode == PRoduct_details[g].product_detail_code && t.UOM_Id == PRoduct_details[g].Default_umo);
                        });

                        if (Filter_view_unit.length > 0) {

                            var con = Filter_view_unit[0].CnvQty;
                        }
                        else {
                            var con = 0;
                        }
						var ret_price= PRoduct_details[g].Retailer_Price==undefined ? PRoduct_details[g].Customer_Price : PRoduct_details[g].Retailer_Price ;
						var dis_price= PRoduct_details[g].Distributor_Price==undefined ? PRoduct_details[g].Purchase_Price : PRoduct_details[g].Distributor_Price ;
                        var tr = $("<tr><td style='text-align:center'>" + slno + "</td>" +
                            "<td style='display:none;'><input type='text' class='prodCode' style='text-align:center' value=" + PRoduct_details[g].product_detail_code + "></td>" +
                            "<td style='text-align:left' class='prodName' value='" + PRoduct_details[g].product_detail_name + "'>" + PRoduct_details[g].product_detail_name + "</td>" +
                            "<td><select id=unit class='unit'>" + units + "</select></td>" +
                            "<td style='display:none;'><input type='text' class='Product_Sale_Unit' style='text-align:center' value=" + PRoduct_details[g].Product_Sale_Unit + "></td>" +
                            "<td style='display:none;'><input type='text' class='Product_Unit' style='text-align:center' id='Product_Unit' value=" + PRoduct_details[g].product_unit + "></td>" +
                            "<td style='display:none;'><input type='text' style='text-align:center' class='Product_Sale_Unit_code' value=" + PRoduct_details[g].Base_Unit_code + "></td>" +
                            "<td style='display:none;'><input type='text' style='text-align:center' class='Product_Unit_code' value=" + PRoduct_details[g].Unit_code + "></td>" +
                            "<td style='display:none;'><input type='text' style='text-align:center' class='Product_sample_erp_code' value=" + PRoduct_details[g].Sample_Erp_Code + "></td>" +
                            "<td style='display:none;'><input type='text' class='UomWeight' style='text-align:center' value=" + PRoduct_details[g].UOM_Weight + "></td>" +
                            "<td style='text-align:center'><input type='text' class='ret_rate'  style='text-align:center' value=" +ret_price+ "></td>" +
                            "<td style='text-align:center'><input type='text' class='Dis_price'   style='text-align:center' value=" +dis_price + "></td>" +
                            "<td style='text-align:center'><input type='text' class='MRP_Price'  style='text-align:center' value=" + PRoduct_details[g].MRP_Price + "></td>" +
                            "<td style='display:none'><input type='hidden' class='Default_con_fac' value=" + con + "></td>" +
                            "<td style='display:none'><input type='hidden' class='Default_umo_code' value=" + PRoduct_details[g].Default_umo + "></td>" +
                            "<td style='display:none'><input type='hidden' class='Default_con_fac' value=" + PRoduct_details[g].Default_con + "></td>" +
                            "</tr>");
                        $('#Product_List tbody').append(tr);
                    }
                    $('#Product_List').css("display", "inline-table");
                    $('#btnSaveId').show();
                    $('#loadover').hide();
                }
            }


            var button_Click = 0;
            $(document).on("click", "#btnSaveId", function () {
                //button_Click += 1;
                //if (button_Click == "1") {
                var SubdivName = '';
                var SubdivCode = '';
                $('#ddlDiv > option:selected').each(function () {
                    SubdivCode += $(this).val() + ',';
                    SubdivName += $(this).text() + ',';
                });

                if (SubdivCode == '' || SubdivCode == '0' || SubdivCode == undefined) { button_Click = 0; alert('Please Select Division'); return false; }
                SubdivName = SubdivName.substring(0, SubdivName.length - 1);
                var Name = $('#txt_name').val();
                if (Name == '' || Name == undefined) { button_Click = 0; alert('Please Give Rate Card Name'); return false; }
                /*var FromDate = $('#txtFrom_Date').val();
                var ToDate = $('#txtTo_Date').val();
                if (FromDate == "") { alert("Select FromDate"); $('#txtFrom_Date').focus(); return false; }
                if (ToDate == "") { alert("Select ToDate"); $('#txtTo_Date').focus(); return false; }*/

                var fdt = $('#txtFrom_Date').val();
                var tdt = $('#txtTo_Date').val();
                if (fdt == "") { alert("Select FromDate"); $('#txtFrom_Date').focus(); return false; }
                if (tdt == "") { alert("Select ToDate"); $('#txtTo_Date').focus(); return false; }

                id = fdt.split('/');
                id1 = tdt.split('/');
                var FromDate = id[2].trim() + '-' + id[1] + '-' + id[0];
                var ToDate = id1[2].trim() + '-' + id1[1] + '-' + id1[0];
                var periodSlno = $('#txtPeriod_slno').val();

                $(document).find("#Product_List tbody tr").each(function () {

                    var selected_unit_code = $(this).closest("tr").find("#unit option:Selected").val();
                    var selected_unit_Name = $(this).closest("tr").find("#unit option:Selected").text();
                    var mrp_price = $(this).closest("tr").find(".MRP_Price").val();

                    var Given_ret_rate = $(this).closest("tr").find(".ret_rate").val();
                    if (Given_ret_rate == "" || Given_ret_rate == undefined) { Given_ret_rate = 0; }

                    var Given_dis_rate = $(this).closest("tr").find(".Dis_price").val();
                    if (Given_dis_rate == "" || Given_dis_rate == undefined) { Given_dis_rate = 0; }

                    var sale_unt_code = $(this).closest("tr").find(".Product_Sale_Unit_code").val();
                    var Prd_unit_code = $(this).closest("tr").find(".Product_Unit_code").val();
                    var Prd_sale_erp_code = $(this).closest("tr").find(".Product_sample_erp_code").val();

                    var con_fac = $(this).closest("tr").find(".Default_con_fac").val();
                    var umoweg = $(this).closest("tr").find(".UomWeight").val();

                    if (sale_unt_code == selected_unit_code) {
                        var ret_con_rate = Given_ret_rate;
                        var dis_con_rate = Given_dis_rate;
                    }
                    else if (Prd_unit_code == selected_unit_code) {
                        var ret_con_rate = Given_ret_rate / Prd_sale_erp_code;
                        var dis_con_rate = Given_dis_rate / Prd_sale_erp_code;
                        if (ret_con_rate > 0)
                            ret_con_rate = ret_con_rate.toFixed(2);
                        if (dis_con_rate > 0)
                        dis_con_rate = dis_con_rate.toFixed(2);
                    }
                    else {
                        /*var Unit_conversion = (umoweg / con_fac);
                        if (Unit_conversion != 0) {
                            var ret_con_rate = umoweg / Unit_conversion * Given_ret_rate;
                        } else
                            var ret_con_rate = 0;

                        var Unit_con = (umoweg / con_fac);
                        if (Unit_con != 0) {
                            var dis_con_rate = umoweg / Unit_con * Given_dis_rate;
                        }
                        var dis_con_rate = 0;*/

                        if (con_fac == 0) {

                            var ret_con_rate = (umoweg / 1000) * Given_ret_rate
                            var dis_con_rate = (umoweg / 1000) * Given_dis_rate

                        }
                        else {
                            var ret_con_rate = Given_ret_rate / con_fac;
                            var dis_con_rate = Given_dis_rate / con_fac;
                        }
                    }

                    if (Given_ret_rate != 0 || mrp_price != 0 || Given_dis_rate != 0) {
                        itm1 = {};
                        itm1.product_detail_code = $(this).closest("tr").find(".prodCode").val();
                        itm1.product_detail_name = $(this).closest("tr").find(".prodName").text();
                        itm1.Unit = selected_unit_Name;
                        itm1.UnitCode = selected_unit_code;
                        itm1.Ret_Rate = Given_ret_rate;
                        itm1.Ret_Rate_in_piece = ret_con_rate;
                        itm1.Dis_Rate = Given_dis_rate;
                        itm1.Dis_Rate_in_piece = dis_con_rate;
                        itm1.MRP_Price = $(this).closest("tr").find('.MRP_Price').val();
                        BodyData.push(itm1);
                    }
                });
                console.log(BodyData);

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    data: "{'BodyBulkData':'" + JSON.stringify(BodyData) + "','divcode':'<%=Session["div_code"]%>','subdivCode':'" + SubdivCode + "','Name':'" + Name + "','EffectiveDate':'" + FromDate + "','EffectiveTo':'" + ToDate + "','subdivName':'" + SubdivName + "','PeriodSlno':'" + periodSlno + "'}",
                    url: "Bulk_Price_Upload.aspx/SaveBulkPriceOrder",
                    dataType: "json",
                    success: function (data) {
                        alert("Successfully updated");
                        window.location.href = 'Bulk_Price_Upload.aspx';
                    },
                    error: function (result) {
                        button_Click = 0
                        alert(JSON.stringify(result));
                    }
                });
                //}
            });

        });
		function validateFunction() {
                var SubdivName = '';
                var SubdivCode = '';
                var button_Click = 0;
                $('#ddlDiv > option:selected').each(function () {
                    SubdivCode += $(this).val() + ',';
                    SubdivName += $(this).text() + ',';
                });

                if (SubdivCode == '' || SubdivCode == '0' || SubdivCode == undefined) { button_Click = 1; alert('Please Select Division'); return false; }
                SubdivName = SubdivName.substring(0, SubdivName.length - 1);
                var Name = $('#txt_name').val();
                if (Name == '' || Name == undefined) { button_Click = 1; alert('Please Give Rate Card Name'); return false; }
                /*var FromDate = $('#txtFrom_Date').val();
                var ToDate = $('#txtTo_Date').val();
                if (FromDate == "") { alert("Select FromDate"); $('#txtFrom_Date').focus(); return false; }
                if (ToDate == "") { alert("Select ToDate"); $('#txtTo_Date').focus(); return false; }*/

                var fdt = $('#txtFrom_Date').val();
                var tdt = $('#txtTo_Date').val();
                if (fdt == "") { button_Click = 1; alert("Select FromDate"); $('#txtFrom_Date').focus(); return false; }
                if (tdt == "") { button_Click = 1; alert("Select ToDate"); $('#txtTo_Date').focus(); return false; }
               if(button_Click == 0)
                 return true; 
                
            }
    </script>
</asp:Content>
