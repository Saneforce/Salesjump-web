<%@ Page Title="" Language="C#" MasterPageFile="~/Master_DIS.master" AutoEventWireup="true" CodeFile="Billing.aspx.cs" Inherits="Stockist_Billing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <html>
    <head>
        <title>Billing</title>
       
       
        <link href="../css/jquery-ui.min.css" rel="stylesheet" />
    
        <script src="../js/bootstrap.min.js"></script>
        <script src="../js/jQuery-2.2.0.min.js"></script>
        <script src="../js/jquery.multiselect.js"></script>
        <link href="../css/daterangepicker-3.0.css" rel="stylesheet" />
        <link href="../css/multiselect/multi-select.css" rel="stylesheet" />
        <script src="../js/daterangepicker-3.0.5.min.js"></script>
      <%--<script src="../js/lib/bootstrap-select.min.js"></script>--%>
        <script src="../js/plugins/multiselect/jquery.multi-select.js"></script>
        <link href="../css/jquery.multiselect.css" rel="stylesheet" />
        <style type="text/css">
            .tab-content {
                margin-top: -1px;
                padding: 20px 8px 0px 20px;
            }
        </style>
        <script type="text/javascript">
            var AllOrderIDs = []; var InvNo = []; var Orders = []; var popUpObj; pgNo = 1; PgRecords = 35; TotalPg = 0; PgRecordscnt = PgRecords;
            var dllRoute = [];
            var subdiv = ("<%=Session["subdivision_code"]%>");


            $(document).ready(function () {

                $(function () {
                    var dtToday = new Date();
                    var month = dtToday.getMonth() + 1;
                    var day = dtToday.getDate();
                    var year = dtToday.getFullYear();
                    if (month < 10)
                        month = '0' + month.toString();
                    if (day < 10) {
                        day = '0' + day.toString();
                    }
                    var maxDate = year + '-' + month + '-' + day;

                });

                $('#ddl_route').multiselect('reload');
                $('#ddl_ret').multiselect('reload');
                $('#ddlInvoice').multiselect('reload');
                $('.divIn').hide();
                $('#btnPrintView').hide();

                $(document).on("click", '#btnPrintView', function () {

                    var selected_length = $('.selected').find('input[type="checkbox"]').length

                    if (selected_length == 0 || selected_length == "") { alert("Please select Atleat one order to print preview"); return false; }

                    var order_ids = '';
                    $('#ddlInvoice > option:selected').each(function () {
                        order_ids += $(this).val() + ',';
                    });

                    /*if (subdiv != "41") {
                        showModalPopUp(order_ids);
                    }
                    else {*/
                    var check = $('input[name="rad"]:checked').val();
                    if (check == "2") {
                        showModalPopUp(order_ids);
                    }
                    //window.open("slip.aspx?&BillFrom=" + txt1 + "&BillTO=" + txt2 + "&Type=" + check, null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
                    else {
                        window.open("slip.aspx?&Order_id=" + order_ids + "&Type=" + check, null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
                    }
                    //  }
                });

                function showModalPopUp(order_ids) {
                    //popUpObj = window.open("../Stockist/GstInvoiceWithoutMaster.aspx?Order_id=" + order_ids + "",
                    popUpObj = window.open("../Stockist/GstInvoiceWithoutMaster.aspx?Order_id=" + order_ids + "",
                        "ModalPopUp",
                        "toolbar=no," +
                        "scrollbars=yes," +
                        "location=no," +
                        "statusbar=no," +
                        "menubar=no," +
                        "addressbar=no," +
                        "resizable=yes," +
                        "width=900," +
                        "height=600," +
                        "left = 0," +
                        "top=0"
                    );
                    popUpObj.focus();
                }

                $(document).on("click", "#btnprint", function () {

                    var txt1 = $("#txt_1").val();
                    var txt2 = $("#txt_2").val();

                    if (txt1 == "") { alert("Please Enter value 1"); return false; }
                    if (txt2 == "") { alert("Please Enter value 2"); return false; }
                    var check = $('input[name="rad"]:checked').val();

                    if ($('input[name="rad"]:checked').length == 0) {
                        alert("Please select Type"); return false;
                    }
                    window.open("slip.aspx?&BillFrom=" + txt1 + "&BillTO=" + txt2 + "&Type=" + check, null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
                });

                $('#txtFrom_Date').on('change', function () {

                    var selected_Date = $('#txtFrom_Date').val();
                    $('#txtTo_Date').val('');
                    $('#txtTo_Date').attr('min', selected_Date);
                });

                $('#txtTo_Date').on('change', function () {
                    var Bill_Type = $('input[type=radio]:checked').val();
                    if (Bill_Type == "" || Bill_Type == undefined) { alert("Please Select Bill Type"); $('#txtTo_Date').val(''); return false; }

                    var FromDate = $('#txtFrom_Date').val();
                    if (FromDate == "") { alert('Please select From Date'); $('#txtTo_Date').val(''); return false; }

                    var ToDate = $(this).val();
                    retrieveRoute(FromDate, ToDate, Bill_Type);
                    $('#divRoute').show();
                    $('.ms-options').css("width", "500px");
                    $('.ms-options').css("height", "200px");
                    $('.ms-options-wrap').css("width", "500px");

                    $('#divRet').hide();
                    $('#ddlInvoice').hide();
                });

                $('#ddl_route').on('change', function () {
                    var Bill_Type = $('input[type=radio]:checked').val();
                    var FromDate = $('#txtFrom_Date').val();
                    var ToDate = $('#txtTo_Date').val();
                    var Terri = '';
                    $('#ddl_route > option:selected').each(function () {
                        Terri += ',' + $(this).val();
                    });
                    if (Terri == '') {
                        alert("select Route");
                        $('#ddl_ret').html('');
                        $('#ddl_ret').multiselect('refresh');
                        $('#ddl_ret').multiselect({
                            columns: 3,
                            placeholder: 'Select Route',
                            search: true,
                            searchOptions: {
                                'default': 'Search Route'
                            },
                            selectAll: true
                        }).multiselect('reload');

                        $('.ms-options').css("width", "500px");
                        $('.ms-options').css("height", "200px");
                        $('.ms-options-wrap').css("width", "500px");
                        return false;
                    }
                    retrieveRetailer(FromDate, ToDate, Bill_Type, Terri);
                    $('.ms-options').css("width", "500px");
                    $('.ms-options').css("height", "200px");
                    $('.ms-options-wrap').css("width", "500px");
                    $('#divRet').show();
                });

                $('#ddl_ret').on('change', function () {
                    $('.divIn').show();
                    $('#btnPrintView').show();
                    var Bill_Type = $('input[type=radio]:checked').val();
                    var FromDate = $('#txtFrom_Date').val();
                    var ToDate = $('#txtTo_Date').val();
                    var retailer = '';
                    $('#ddl_ret > option:selected').each(function () {
                        retailer += ',' + $(this).val();
                    });
                    if (retailer == '') {
                        alert("Select Retailer");
                        $('#ddlInvoice').html('');
                        $('#ddlInvoice').multiselect('refresh');
                        $('#ddlInvoice').multiselect({
                            columns: 3,
                            placeholder: 'Select Retailer',
                            search: true,
                            searchOptions: {
                                'default': 'Search Retailer'
                            },
                            selectAll: true
                        }).multiselect('reload');
                        $('.ms-options').css("width", "500px");
                        $('.ms-options').css("height", "200px");
                        $('.ms-options-wrap').css("width", "500px");
                        return false;
                    }

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Billing.aspx/GetInvoice",
                        data: "{'FDt':'" + FromDate + "','TDt':'" + ToDate + "','Type':'" + Bill_Type + "','Retailer':'" + retailer + "'}",
                        dataType: "json",
                        success: function (data) {
                            $('#ddlInvoice').html('');
                            var AllInv = JSON.parse(data.d) || [];

                            data_bind($('#ddlInvoice'), AllInv, 'Trans_Inv_Slno', 'Trans_Inv_Slno', 'No');

                            $('#ddlInvoice').multiselect({
                                columns: 4,
                                placeholder: 'Select Invoice No',
                                searchOptions: {
                                    'default': 'Search Order IDs'
                                },
                                selectAll: true,
                                maxHeight: 20,
                            }).multiselect("reload");
                        },
                        error: function (result) {
                            alert(JSON.stringify(result));
                        }
                    });

                    $('.ms-options').css("width", "500px");
                    $('.ms-options').css("height", "200px");
                    $('.ms-options-wrap').css("width", "500px");
                });
            });

            function retrieveRoute(FromDate, ToDate, Bill_Type) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Billing.aspx/getRoute",
                    data: "{'FDt':'" + FromDate + "','TDt':'" + ToDate + "','Type':'" + Bill_Type + "'}",
                    dataType: "json",
                    success: function (data) {
                        $('#ddl_route').html('');
                        $('.ms-options').html("");
                        dllRoute = JSON.parse(data.d) || [];
                        data_bind($('#ddl_route'), dllRoute, 'Territory_Code', 'Territory_Name', 'No');
                        $('#ddl_route').multiselect({
                            columns: 3,
                            placeholder: 'Select Route',
                            search: true,
                            searchOptions: {
                                'default': 'Search Route'
                            },
                            selectAll: true
                        }).multiselect('reload');
                        $('.ms-options').css('width', '200px');
                        $('#ms-list-2').css('width', '190px');
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            }
            function retrieveRetailer(FromDate, ToDate, Bill_Type, Terri) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Billing.aspx/getRetailer",
                    data: "{'FDt':'" + FromDate + "','TDt':'" + ToDate + "','Type':'" + Bill_Type + "','Territory':'" + Terri + "'}",
                    dataType: "json",
                    success: function (data) {

                        $('#ddl_ret').html('');
                        ddlretailer = JSON.parse(data.d) || [];

                        //var filtered_retailer = ddlretailer.filter(function (p) {
                        //    return (p.Delivery_Mode == Bill_Type);
                        //});

                        data_bind($('#ddl_ret'), ddlretailer, 'Cus_Code', 'Cus_Name', 'No');
                        $('#ddl_ret').multiselect({
                            columns: 3,
                            placeholder: 'Select Retailer',
                            search: true,
                            searchOptions: {
                                'default': 'Search Retailer'
                            },
                            selectAll: true
                        }).multiselect('reload');
                        $('#ddl_ret-options ul').css('column-count', '3');
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            }
            function data_bind(id, dataArray, optionVal, optionText, allselectoption) {
                if (allselectoption == "yes") {
                    for (var i = 0; i < dataArray.length; i++) {
                        id.append('<option selected value="' + dataArray[i][optionVal] + '">' + dataArray[i][optionText] + '</option>');
                    }
                }
                else {
                    for (var i = 0; i < dataArray.length; i++) {
                        id.append('<option value="' + dataArray[i][optionVal] + '">' + dataArray[i][optionText] + '</option>');
                    }
                }
            }
        </script>

    </head>
    <body>
        <form id="frm" runat="server">
            <div class="container" style="max-width: 100%">
                <ul class="nav nav-tabs">
                    <li class="active"><a data-toggle="tab" href="#home">Billing</a></li>
                    <li><a data-toggle="tab" href="#menu1" style="display:none;">Delivery Challan</a></li>
                </ul>
                <div class="tab-content">
                    <div id="home" class="tab-pane fade in active" style="min-height:700px;">
                        <%--<div style="text-align: center; margin-left: -231px;">
                            <input type="radio" id="radio1" name="bill_select" value="0" />
                            <label for="Sales_Invoice" class="form-group">Sales Invoice</label>
                        </div>--%>

                         <div class="row">

                            <div class="col-sm-4" style="text-align: center;">
                                <label for="lbl_type" class="form-group">Bill Type</label>
                            </div>
                            <div class="col-sm-2">
                                <input type="radio" id="radio1" name="rad" value="2" />
                                <label for="Sales_Invoice" class="form-group">Print Invoice</label>
                              <%--  <label for="Sales_Invoice" class="form-group">Sales Invoice</label>--%>
                            </div>
                             <%-- <div class="col-sm-2">
                                <input type="radio" id="radioAll" name="rad" value="All" />
                                <label for="Sales_Invoice" class="form-group">ALL</label>
                            </div>
                            <div class="col-sm-2">
                                <input type="radio" id="radioDc" name="rad" value="1" />
                                <label for="lbl_type1" class="form-group">DC</label>
                            </div>
                            <div class="col-sm-2">
                                <input type="radio" id="radioInvoice" name="rad" value="0" />
                                <label for="lbl_type2" class="form-group">Invoice</label>
                            </div>--%>

                        </div>
                        
                        <div class="row" style="margin-top: 1rem;margin-right: 500px;">
                            <label id="Label4" class="col-md-3  col-md-offset-3  control-label">From Date</label>
                            <div class="col-md-3 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                    <input type="date" name="From_Date" class="form-control" id="txtFrom_Date" placeholder="Date">
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin-top: 1rem; margin-right: 500px;"">
                            <label id="Label5" class="col-md-3  col-md-offset-3  control-label">To Date</label>
                            <div class="col-md-3 inputGroupContainer">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                    <input type="date" name="To_Date" class="form-control" id="txtTo_Date" placeholder="Date">
                                </div>
                            </div>
                        </div>
                       
                        <div class="row" id="divRoute" style="margin-top: 1rem; display: none;margin-right: 500px;">
                            <label id="lblroute" class="col-md-3  col-md-offset-3  control-label">Route</label>
                            <div class="col-md-3 inputGroupContainer">
                                <div>
                                    <select id="ddl_route" class="form-control" multiple></select>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="divRet" style="margin-top: 1rem; display: none;margin-right: 500px;">
                            <label id="lblret" class="col-md-3  col-md-offset-3  control-label">Retailer</label>
                            <div class="col-md-3 inputGroupContainer">
                                <div>
                                    <select id="ddl_ret" multiple></select>
                                </div>
                            </div>
                        </div>
                        <br />

                        <div class="row divIn" style="display: block;margin-top: 1rem;margin-right: 500px;">
                            <label id="lblInvoice" class="col-md-3  col-md-offset-3  control-label">Invoice No</label>
                            <div class="col-md-3 inputGroupContainer">
                                <div>
                                    <select name="ddlInv" id="ddlInvoice" multiple></select>
                                </div>
                            </div>
                        </div>
                        <div style="margin-top: 17px; margin-left: -61px; text-align: center;">
                            <span>
                                <input type="button" class="btn btn-primary btn" id="btnPrintView" value="Print PreView"></span>
                        </div>
                    </div>
                    <div id="menu1" class="tab-pane fade">
                        <div class="table-responsive">
                            <div class="row">
                                <div class="col-sm-1">
                                </div>
                                <div class="col-sm-2" style="text-align: center;">
                                    <label for="lbl_bilno" class="form-group">Bill No</label>
                                </div>
                                <div class="col-sm-3">
                                    <input type="text" class="form-control" id="txt_1" />
                                </div>
                                <%--   <div class="col-sm-2" style="text-align:center;">
                               <label for="lbl_to" class="form-group" >To</label>
                               </div>--%>
                                <div class="col-sm-3">
                                    <input type="text" class="form-control" id="txt_2" />
                                </div>
                                <div class="col-sm-1">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-1">
                                </div>
                                <div class="col-sm-2" style="text-align: center;">
                                    <label for="lbl_type" class="form-group">Bill Type</label>
                                </div>
                                <div class="col-sm-1">
                                    <input type="radio" id="rd1" name="rad" value="1" />
                                    <label for="lbl_type1" class="form-group">DC</label>
                                </div>
                                <div class="col-sm-2">
                                    <input type="radio" id="rd2" name="rad" value="0" />
                                    <label for="lbl_type2" class="form-group">Invoice</label>
                                </div>
                                <div class="col-sm-6">
                                </div>

                            </div>
                            <br />
                            <div class="col-md-6 col-md-offset-5">
                                <button type="button" id="btnprint" class="btn btn-success">Print</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </body>
    </html>
</asp:Content>
