<%@ Page Title="" Language="C#" MasterPageFile="~/Master_DIS.master" AutoEventWireup="true" CodeFile="Purchase_Order_Edit.aspx.cs" Inherits="Stockist_Purchase_Order_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <link href="../css/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
        <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.2/js/bootstrap-select.min.js"></script>
        <link href="../alertstyle/jquery-confirm.min.css" rel="stylesheet" />
        <script src="../alertstyle/jquery-confirm.min.js"></script>
        <style type="text/css">
            .green {
                color: #28a745;
            }

            .red {
                color: #f10b0b;
            }

            .edit_class {
                pointer-events: none;
                cursor: default;
                text-decoration: line-through;
                color: black;
            }

            .category {
                border: none;
                outline: none;
                padding: 10px 16px;
                background-color: #f1f1f1;
                cursor: pointer;
                font-size: 15px;
            }

            .active, .btn:hover {
                background-color: #666;
                color: white;
            }

            .chosen-container {
                width: 100% !important;
            }

            .modal {
                background-color: #0000000f;
            }

            #recipient-name {
                display: block !important;
            }

            .Spinner-Input {
                width: auto !important;
            }

            .chosen-container {
                width: 320px !important;
            }

            input[type='text'], select {
                border: 1.5px solid #19a4c6a3 !important;
                background: aliceblue;
            }

            .table > thead > tr > th {
                vertical-align: bottom;
                border-bottom: 2px solid #19a4c6a3;
            }

            .txtblue {
                border: 1.5px solid #19a4c6a3 !important;
                background: aliceblue;
                /*border: 1px solid #19a4c6!important;*/
            }

            .chosen-container-single .chosen-single {
                border: 1.5px solid #19a4c6a3 !important;
                height: 30px;
                background: aliceblue !important;
            }

            .card {
                border: 1.5px solid #b2ebf9b5 !important;
            }

            .select2-container--default .select2-selection--single {
                border: 1.5px solid #19a4c6a3 !important;
                background: aliceblue !important;
            }

            .ms-options-wrap > button:focus, .ms-options-wrap > button {
                border: 1.5px solid #19a4c6a3 !important;
            }

            .stockClass {
                font-size: 12px;
                font-weight: bolder;
            }

            .fa-stack-overflow:before {
                content: "\f16c";
                margin-right: 6px;
                margin-left: 6px;
                color: #00bcd4;
            }

            .fa-tags:before {
                content: "\f02c";
                color: #ef1b1b;
                margin-right: 6px;
            }

            .fa-code:before {
                content: "\f121";
                margin-left: 6px;
                margin-right: 6px;
                color: #0075ff;
                font-size: 13px;
                font-weight: bolder;
            }

            .spinner {
                margin: 100px auto;
                width: 50px;
                height: 40px;
                text-align: center;
                font-size: 10px;
            }

                .spinner > div {
                    background-color: #333;
                    height: 100%;
                    width: 6px;
                    display: inline-block;
                    -webkit-animation: sk-stretchdelay 1.2s infinite ease-in-out;
                    animation: sk-stretchdelay 1.2s infinite ease-in-out;
                }

                .spinner .rect2 {
                    -webkit-animation-delay: -1.1s;
                    animation-delay: -1.1s;
                }

                .spinner .rect3 {
                    -webkit-animation-delay: -1.0s;
                    animation-delay: -1.0s;
                }

                .spinner .rect4 {
                    -webkit-animation-delay: -0.9s;
                    animation-delay: -0.9s;
                }

                .spinner .rect5 {
                    -webkit-animation-delay: -0.8s;
                    animation-delay: -0.8s;
                }

            @-webkit-keyframes sk-stretchdelay {
                0%, 40%, 100% {
                    -webkit-transform: scaleY(0.4);
                }

                20% {
                    -webkit-transform: scaleY(1.0);
                }
            }

            @keyframes sk-stretchdelay {
                0%, 40%, 100% {
                    transform: scaleY(0.4);
                    -webkit-transform: scaleY(0.4);
                }

                20% {
                    transform: scaleY(1.0);
                    -webkit-transform: scaleY(1.0);
                }
            }

            .spinnner_div {
                width: 1200px;
                height: 1000px;
                background: rgba(255, 255, 255, 0.1);
                backdrop-filter: blur(2px);
                position: absolute;
                z-index: 100;
                overflow-y: hidden;
            }
        </style>
        <%--<script src="../js/lib/bootstrap-select.min.js"></script>--%>
        <%-- <link href="../css/SpinnerInput.css" rel="stylesheet" type="text/css" />
        <link href="../css/SpinnerInput.css" rel="stylesheet" type="text/css" />
       
        <link href="../css/select2.min.css" rel="stylesheet" />
        <script  type="text/javascript" src="../js/select2.min.js"></script>--%>
        <%-- <script  type="text/javascript" src="../js/daterangepicker-3.0.5.min.js"></script>
        <script src="../js/jquery.multiselect.js"></script>
        <script  type="text/javascript" src="../js/moment-2.24.0.min.js"></script>
         <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
        <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>--%>

        <link href="../css/SpinnerInput.css" rel="stylesheet" type="text/css" />
        <link href="../css/select2.min.css" rel="stylesheet" />
        <script src="../js/select2.full.min.js"></script>
        <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
        <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
        <link href="../fontawesome-free-6.2.1-web/css/all.min.css" rel="stylesheet" />
        <script src="../fontawesome-free-6.2.1-web/js/all.min.js"></script>
        <script language="javascript" type="text/javascript">

            var AllOrders = []; var Orders = []; edt = ""; Prds = ""; var Product_array = []; NewOrd = []; var filter_unit = []; units = ""; units1 = "";
            var scheme = []; var today = ''; var arr = []; var headdatas = []; var namesArr = []; var flag = 0; var CQ = ''; var All_Tax = []; var Retailer_State = 0;
            var Product_Details = []; var All_Product_Details = []; var Cust_Price = []; var Rate_List_Code = '';
            var All_Retailer = []; var new_height = 0; var currentheight = 0; var scrollhightchnage = 0; var scrollhightchnageDel = 0; var tax_arr = [];
            var sf = '';
            Dt = new Date(); sDt = Dt.getFullYear() + '-' + ((Dt.getMonth() < 9) ? "0" : "") + (Dt.getMonth() + 1) + '-' + ((Dt.getDate() < 10) ? "0" : "") + Dt.getDate() + ' 00:00:00';

            var Dist_state_code = ("<%=Session["State"].ToString()%>");
            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1;
            var yyyy = today.getFullYear();
            if (dd < 10) { dd = '0' + dd }
            if (mm < 10) { mm = '0' + mm }
            today = yyyy + '-' + mm + '-' + dd;

            var Order_id = "<%=Order_ID%>";
            var division_Code = "<%=Div%>";
            var Stockist_Code = "<%=stk%>";
            var Cust_Code = "<%=Cust_Code%>";
            var order_date = "<%=Order_date%>";
            var type = "<%=type%>";

            $(document).ready(function () {

                resetSL = function () {
                    $(".rwsl").each(function () {
                        $(this).text($(this).closest('tr').index() + 1);
                    });
                }

                $("#full_div").click(function (e) {
                    e.stopPropagation();
                    return false;
                });

                var stockist_Code = ("<%=Session["Sf_Code"].ToString()%>");
                var Div_Code = ("<%=Session["div_code"].ToString()%>");

                //$("#txt_Purchase_Date").attr('readonly', true);
                $('#chck').prop('checked', true);

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
                today = yyyy + '-' + mm + '-' + dd;

                $("#txt_Purchase_Date").val(today);
                $('#txt_Expected_Date').attr('min', today);
                //  $('#txt_Purchase_Date').attr('readonly', true);

                //$(document).on("click", ".Spinner-Input", function (e) {
                //    e.stopPropagation();
                //    $ix = $(this).find(".Spinner-Modal");
                //    $dsp = $($ix).hasClass("open");
                //    $(".Spinner-Modal").removeClass("open");
                //    if ($dsp == false)
                //        $($ix).addClass("open");
                //})

                $(document).on('keypress', '.txtQty ', function (e) {
                    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                        return false;
                    }
                });

                $(document).on("click", "#btnAdd", function () {
                    AddRow(1);
                })

                $(document).on("click", "#btnDelRow", function () {
                    DelRow();
                })

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Purchase_Order_Edit.aspx/DisplayCompanyDetails",
                    dataType: "json",
                    success: function (data) {
                        ComDetails = data.d;
                        if (data.d.length > 0) {
                            //$.each(data.d, function () {
                            //    $('#To_Address').append($("<option></option>").val(this['HO_ID']).html(this['Name'])).trigger('chosen:updated').css("width", "100%");;;

                            //});
                        }
                    },
                    error: function (result) {
                    }
                });
                // $('#To_Address').chosen();

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Purchase_Order_Edit.aspx/Dis_stk_sstk_Details",
                    dataType: "json",
                    success: function (data) {
                        add = JSON.parse(data.d);
                        for (var a = 0; a < add.length; a++) {
                            $('#lbl_Bill_Address').text(add[a].Address);
                            $('#txt_Ship_add').text(add[a].Address);
                            $('#txt_Ship_add').attr('readonly', true);
                        }
                    },
                    error: function (result) {
                    }
                });
                function CalcAmt(x) {

                    itr = $(x).closest('tr');
                    idx = $(itr).index();
                    rt = parseFloat($(itr).find('.tdRate').text()); if (isNaN(rt)) rt = 0; qt = parseFloat($(itr).find('.txtQty').val()); if (isNaN(qt)) qt = 0;
                    tax = parseFloat($(itr).find('.tdtax').text()); if (isNaN(tax)) tax = 0;

                    NewOrd[idx].Sub_Total = (qt * rt).toFixed(2);

                    if ($(itr).find('.dis_per').text() == "") { var dis_per = 0; }
                    if (NewOrd[idx].Total_Tax == '' || isNaN(NewOrd[idx].Total_Tax)) { NewOrd[idx].Total_Tax = 0; }

                    NewOrd[idx].Dis_value = (parseFloat(dis_per) / 100 * parseFloat(NewOrd[idx].Sub_Total)).toFixed(2);
                    NewOrd[idx].Sub_Total = (parseFloat(NewOrd[idx].Sub_Total) - parseFloat(NewOrd[idx].Dis_value)).toFixed(2);

                    for (var g = 0; g < NewOrd[idx].Tax_details.length; g++) {
                        NewOrd[idx].Tax_details[g].Tax_Amt = parseFloat(NewOrd[idx].Tax_details[g].Tax_Per / 100 * parseFloat(NewOrd[idx].Sub_Total)).toFixed(2);
                        if (NewOrd[idx].Tax_details[g].Tax_Name.toLowerCase() == 'tcs') { $(itr).find('.tdtcs').html(NewOrd[idx].Tax_details[g].Tax_Amt); }
                    }

                    $(itr).find('.tdtotal').html(NewOrd[idx].Sub_Total);
                    $(itr).find('.disc_value').html(NewOrd[idx].Dis_value);
                    $(itr).find('.tddis_val').html(NewOrd[idx].Dis_value);


                    $(itr).find('.tdtax').html((parseFloat(NewOrd[idx].Total_Tax) / 100 * parseFloat(NewOrd[idx].Sub_Total)).toFixed(2));
                    NewOrd[idx].Gross_Amt = (parseFloat(NewOrd[idx].Total_Tax) / 100 * parseFloat(NewOrd[idx].Sub_Total) + parseFloat(NewOrd[idx].Sub_Total)).toFixed(2);
                    NewOrd[idx].Tax_value = (parseFloat(NewOrd[idx].Total_Tax) / 100 * parseFloat(NewOrd[idx].Sub_Total)).toFixed(2);
                    $(itr).find('.tdAmt').html(NewOrd[idx].Gross_Amt);
                    ReCal();
                    //ReCalc();
                }
                function bind_category() {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Purchase_Order_Edit.aspx/Get_Category",
                        dataType: "json",
                        success: function (data) {
                            cat = JSON.parse(data.d);
                            $.each(cat, function () {
                                $('#txtfilter').append($("<option></option>").val(this['Product_Cat_Code']).html(this['Product_Cat_Name'])).trigger('chosen:updated');
                            });
                        },
                    });
                    $("#txtfilter").chosen({ allow_single_deselect: true, width: '50%' });
                }
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Purchase_Order_Edit.aspx/Get_Product_unit",
                    dataType: "json",
                    success: function (data) {
                        All_unit = JSON.parse(data.d) || [];
                    },
                    error: function (result) {
                    }
                });
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Purchase_Order_Edit.aspx/GetProducts",
                    data: "{'div':'" + Div_Code + "'}",
                    dataType: "json",
                    success: function (data) {
                        itms = JSON.parse(data.d) || [];
                        for ($k = 0; $k < itms.length; $k++) {
                            Prds += "<option value='" + itms[$k].Product_Detail_Code + "'>" + itms[$k].Product_Detail_Name + "</option>";
                        }
                        // AddRow(0);
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });

                $(document).on("change", "#To_Address", function () {

                    var comp = [];
                    var compVal = $('#To_Address option:selected').val();

                    comp = ComDetails.filter(function (w) {
                        return (w.HO_ID == compVal && w.State_Code.indexOf(Dist_state_code) > -1);
                    });

                    if (comp.length <= 0) {
                        clear();
                    }

                    Selected_Com_Name = $(this).val();
                    if (Selected_Com_Name != "0") {
                        var h = ComDetails.filter(function (a) {
                            return (a.HO_ID == Selected_Com_Name)
                        });
                        $('#lbl_To_Address').text(h[0].Division_Add1);
                    }
                    else {
                        $('#lbl_To_Address').html('');
                        alert("Please Select")
                    }
                });

                function clear() {
                    $('#OrderEntry').find('tbody tr').remove();
                    $('#free_table').find('tbody tr').remove();
                    $("[id*=sub_tot]").val('');
                    $("[id*=txt_cgst]").val('');
                    $("[id*=txt_sgst]").val('');
                    $("[id*=Txt_Dis_tot]").val('');
                    $("[id*=gross").val('');
                    NewOrd = [];
                }

                $(document).on("click", "#chck", function () {

                    if ($("#chck").is(":checked")) {
                        $("#txt_Ship_add").attr("readonly", true);

                        var d = [];

                        var d = add.filter(function (a) {
                            return (a.Code == "<%=Session["Sf_Code"].ToString()%>")
                        });

                        $("#txt_Ship_add").val(d[0].Address);

                    } else {
                        $("#txt_Ship_add").attr("readonly", false);
                    }
                });

                $.ajax({
                    type: "Post",
                    contentType: "application/json; charset=utf-8",
                    url: "Purchase_Order_Edit.aspx/getscheme",
                    data: "{'date':'" + today + "','Div_Code':'" + Div_Code + "','Stockist_Code':'" + stockist_Code + "'}",
                    dataType: "json",
                    async: false,
                    success: function (data) {
                        scheme = JSON.parse(data.d) || [];
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });

                function loadData() {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Purchase_Order_Edit.aspx/DisplayProduct",
                        dataType: "json",
                        success: function (data) {
                            AllOrders = JSON.parse(data.d) || [];
                            Orders = AllOrders;
                            //AddRow(0);
                        },
                        error: function (result) {
                        }
                    });
                }

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Purchase_Order_Edit.aspx/GetProducts",
                    data: "{'div':'<%=Session["Division_Code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        itms = JSON.parse(data.d) || [];
                        for ($k = 0; $k < itms.length; $k++) {
                            Prds += "<option value='" + itms[$k].Product_Detail_Code + "'>" + itms[$k].Product_Detail_Name + "</option>";
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
                    data: "{'Div_Code':'" + Div_Code + "','Stockist_Code':'" + stockist_Code + "'}",
                    url: "Purchase_Order_Edit.aspx/getratenew",
                    dataType: "json",
                    success: function (data) {
                        Allrate = JSON.parse(data.d) || [];
                        //Allrate = [];
                        if (Allrate.length < 1) {
                            //rjalert('Error!', 'Rate is not available for this distributor. Kindly contact the administrator!', 'error');
                            $.confirm({
                                title: 'Attention!',
                                content: 'Rate is not available for this distributor. Kindly contact the administrator!',
                                type: 'red',
                                typeAnimated: true,
                                autoClose: 'action|8000',
                                icon: 'fa fa-check fa-2x',
                                buttons: {
                                    tryAgain: {
                                        text: 'OK',
                                        btnClass: 'btn-green',
                                        action: function () {
                                            $('.spinnner_div').hide();
                                            window.location.href = "../Stockist/Purchase_Order_List.aspx";
                                        }
                                    }
                                }
                            });
                            return false;
                        }
                    },
                    error: function (result) {
                    }
                });

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Purchase_Order_Edit.aspx/Get_Product_unit",
                    dataType: "json",
                    success: function (data) {
                        All_unit = JSON.parse(data.d) || [];
                    },
                    error: function (result) {
                    }
                });

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Purchase_Order_Edit.aspx/Get_Product_Tax",
                    dataType: "json",
                    success: function (data) {
                        All_Tax = JSON.parse(data.d) || [];
                    },
                    error: function (result) {
                    }
                });

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Purchase_Order_Edit.aspx/Get_Product_Cat_Details",
                    dataType: "json",
                    success: function (data) {
                        Cat_Details = JSON.parse(data.d) || [];
                        $('#myDIV').html('');
                        for (var g = 0; g < Cat_Details.length; g++) {

                            if (Cat_Details[g].Product_Cat_Code == -1) {
                                $('#myDIV').append('<button class="btn category active" value="' + Cat_Details[g].Product_Cat_Code + '" >' + Cat_Details[g].Product_Cat_Name + '</button>');
                            }
                            else {
                                $('#myDIV').append('<button class="btn category" value="' + Cat_Details[g].Product_Cat_Code + '" >' + Cat_Details[g].Product_Cat_Name + '</button>');
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
                    url: "Purchase_Order_Edit.aspx/GetSecOrderDetails",
                    data: "{'Order_No':'" + Order_id + "'}",
                    dataType: "json",
                    success: function (data) {
                        AllOrders = JSON.parse(data.d) || [];
                        Orders = AllOrders;
                        sf = Orders[0].sf;
                        var tsr = "<option value=" + Orders[0].Sf_Code + ">" + Orders[0].Sup_Name + "</option>";
                        $('#To_Address').append(tsr).prop('disabled', true);
                        $('#To_Address').chosen();
                        $("#lbl_Bill_Address").html(Orders[0].Billing_Address);
                        $("#txt_Ship_add").html(Orders[0].Shipping_Address);
                        //$('#txt_Purchase_Date').val(Orders[0].Order_Date);
                        var today = new Date(Orders[0].DOP);
                        var dd = today.getDate();
                        var mm = today.getMonth() + 1;
                        var yyyy = today.getFullYear();
                        if (dd < 10) {
                            dd = '0' + dd
                        }
                        if (mm < 10) {
                            mm = '0' + mm
                        }
                        today = yyyy + '-' + mm + '-' + dd;
                        $("#txt_Expected_Date").val(today);
                        var todayord = new Date(Orders[0].Order_Date);
                        var ddord = todayord.getDate();
                        var mmord = todayord.getMonth() + 1;
                        var yyyyord = todayord.getFullYear();
                        if (ddord < 10) {
                            ddord = '0' + ddord
                        }
                        if (mmord < 10) {
                            mmord = '0' + mmord
                        }
                        todayord = yyyyord + '-' + mmord + '-' + ddord;
                        $('#txt_Purchase_Date').val(todayord);
                        ReloadTable();

                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
                function Get_unit(code) {

                    var filter_unit = []; units = ""; units1 = "";

                    filter_unit = All_unit.filter(function (w) {
                        return (code == w.Product_Detail_Code);
                    });

                    if (filter_unit.length > 0) {

                        for (var z = 0; z < filter_unit.length; z++) {
                            units += "<li id='s' name='itms' value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</li>";
                        }
                    }
                    return units
                }

                function Get_Tax(pro_code, itr) {

                    var tax_filter = [];

                    tax_filter = All_Tax.filter(function (r) {
                        return (r.Product_Detail_Code == pro_code)
                    });

                    for (var z = 0; z < tax_filter.length; z++) {

                        if (tax_filter[z].Tax_Type == 'SGST') {

                            $(itr).find('.sgst_taxname').text(tax_filter[z].Tax_name);
                            $(itr).find('.hid_sgst').val(tax_filter[z].Tax_Val);

                        }
                        else if (tax_filter[z].Tax_Type == 'CGST') {

                            $(itr).find('.cgst_taxname').text(tax_filter[z].Tax_name);
                            $(itr).find('.hid_cgst').val(tax_filter[z].Tax_Val);
                        }
                        else if (tax_filter[z].Tax_Type == 'IGST') {

                        }
                        else {

                            $(itr).find('.sgst_taxname').text(tax_filter[z].Tax_name);
                            $(itr).find('.hid_sgst').val(tax_filter[z].Tax_Val);
                            $(itr).find('.cgst_taxname').text(tax_filter[z].Tax_name);
                            $(itr).find('.hid_cgst').val(tax_filter[z].Tax_Val);
                        }
                    }

                }


                $(document).on("click", ".category", function (e) {
                    e.preventDefault();
                    $('.category').removeClass('active');
                    $('.category').css('color', 'black');
                    $(this).addClass('active');
                    $(this).css('color', 'white');

                });
                $(document).on("click", ".Spinner-Input", function () {
                    $ix = $(this).find(".Spinner-Modal");
                    $dsp = $($ix).hasClass("open");
                    $(".Spinner-Modal").removeClass("open");
                    if ($dsp == false)
                        $($ix).addClass("open");
                })

                $(document).on("click", ".Spinner-Modal>ul>li", function () {
                    $vl = $(this).attr('val');
                    $(this).closest(".Spinner-Input").find(".Spinner-Value").html($(this).html())
                    itr = $(this).closest('tr');
                    idx = $(itr).index();
                    $(itr).find('.txtQty').val(0);
                    $(itr).find('.tdAmt').text(0.00);
                    $(itr).find('.tdtotal').text(0.00);
                    //CalcAmt(itr);
                    cal(itr);
                    var prdcode = $(this).closest('tr').attr('class');
                    var unitval = $(this).attr('id');
                    var conf = $(this).attr('conf');
                    var umoval = $(this).val();
                    var filterunit = [];
                    //filterunit=NewOrd.filter(function (s, key) {
                    //        return (s.PCd == prdcode && key != idx && s.umo_unit==umoval);
                    //});
                    //if (filterunit.length > 0) {
                    //    $(itr).find('.tdRate').text(0);
                    //    alert('Product Units Already Selected');
                    //    return false;
                    //}
                    ans = Allrate.filter(function (t) {
                        return (t.Product_Detail_Code == prdcode && t.Unit_code == umoval.toString());
                    });
                    if (ans.length > 0) {
                        $(itr).find('.Con_fac').text(conf);
                    }
                    else {
                        $(itr).find('.Con_fac').text(conf);
                    }
                    NewOrd[idx].umo_unit = umoval;
                    NewOrd[idx].Rate = parseFloat(unitval).toFixed(2);
                    $(itr).find('.tdRate').text(parseFloat(unitval).toFixed(2));

                })

                $(document).on('keypress', '.txtQty', function (e) {
                    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                        return false;
                    }
                });
                function AddRow(type) {

                    itm = {}
                    itm.PCd = ''; itm.sPCd = ''; itm.s_pcode = ''; itm.PName = ''; itm.Unit = ''; itm.Rate = "0"; itm.Qty = 1; itm.Qty_c = 1; itm.Free = "0"; itm.Discount = "0"; itm.Dis_value = "0"; itm.Total_Tax = 0; itm.produnit = 0; itm.eQty = 0;
                    itm.Tax_details = ''; itm.Tax = "0"; itm.Tax_value = "0"; itm.Total = "0"; itm.Gross_Amt = "0"; itm.Sub_Total = 1; itm.of_Pro_Code = ''; itm.of_Pro_Name = ''; itm.of_Pro_Unit = ''; itm.umo_unit = ''; itm.con_fac = ''; itm.TCS = ''; itm.TDS = '';
                    NewOrd.push(itm);

                    tr = $("<tr class='subRow'></tr>");
                    $(tr).html("<td class='rowno'><label style='margin-bottom:0px;'><input type='checkbox' class='slitm'> <span class='rwsl'>" + ($("#OrderEntry > TBODY > tr").length + 1) + "</span></label></td><td class='pro_td' style='width:27%;padding: 9px 0px 0px 0px;'><select class='ddlProd' style='margin-top:-3px;height:25px;width:250px!important'><option value='0'>--select--</option>" + Prds + "</select><div class='second_row_div' style='display:none; font-size:11px;padding: 5px 0px 0px 3px;margin-left: -13px;'></div></td><td style='display:none;' ><input type='hidden' class='sale_code' /></td><td id='Td1' style='width: 18%;padding: 8px 0px 0px 30px;'><select class='cbAlwTyp ispinner'></select><div class='Spinner-Input'><div class='Spinner-Value'>Select</div><div class='Spinner-Modal'><ul>Select</ul></div></div><input type='text' name='txqty' id='txqty' class='txtQty validate' pval='0.00' value   style='text-align:right;width: 42%;'></td><td class='tdRate' style='text-align:right; padding: 17px 0px 0px 0px;'>0.00</td><td class='fre' style='text-align:right; padding: 17px 9px 0px 0px;'>0</td><td style='display:none;'<label name='free' class='fre'></lable></td><td style='display:none' ><input type='hidden' class='fre1' name='fre1' ></td><td style='display:none' ><input type='hidden' class='of_pro_name'  name='of_pro_name' ></td><td style='display:none' ><input type='hidden' class='of_pro_code'  name='of_pro_code' ></td><td class='tddis_val' style='text-align:right; padding: 17px 11px 0px 0px;'>0</td><td style='display:none'><input type='hidden' class='disc_value' name='disc_value'></td><td style='display:none'><input type='hidden' class='disc_value'  name='disc_value' ></td><td class='tdtotal' style='text-align:right;padding: 17px 7px 0px 0px;'>0.00</td><td class='tdtax' style='text-align:right;padding: 17px 0px 0px 0px;'>0.00</td><td style='display:none;'><input type='hidden' class='tdcgst' id='tdcgst' /></td><td style='display:none;'><input type='hidden' class='tdsgst' id='tsgst' ></td><td style='display:none;'><input type='hidden' class='tdigst' id='tigst' ></td><td style='display:none;'><input type='hidden' class='tdtcs' id='tdtcs' ></td><td class='tdAmt' style='text-align:right;padding: 17px 6px 0px 0px;width: 10%;'>0.00</td><td style='display:none;'><label id='pcode' class='pcode'></label></td><td style='display:none;' ><input type='hidden' class='erp_code' /></td>");
                    $("#OrderEntry > TBODY").append(tr); //OrderEntry
                    resetSL();
                    //$('.ddlProd').chosen();
                    $('.ddlProd').select2();

                    $('.ddlProd').on('select2:open', function () {
                        var selected_cat_code = $('.btn.active').val();
                        var selected_cat_Name = $('.btn.active').text();
                        var filtered_prd = [];

                        filtered_prd = itms.filter(function (k) {
                            return (k.Product_Cat_Code == selected_cat_code);
                        });

                        $(this).html('');
                        filtered_prd = selected_cat_code == -1 ? itms : filtered_prd;


                        let str = '<option value=-1>--select--</option>';
                        for (var h = 0; h < filtered_prd.length; h++) {
                            str += `<option value="${filtered_prd[h].Product_Detail_Code}">${filtered_prd[h].Product_Detail_Name}</option>`;
                        }
                        $(this).html(str);
                        $(this).trigger("select2:updated");

                    });

                    if (type == 1) {
                        event.stopPropagation();
                        $('#OrderEntry tr:last').find(".ddlProd").trigger('select2:open');
                    }

                    $(".ddlProd").on("change", function () {

                        itr = $(this).closest('tr');
                        idx = $(itr).index();
                        $(itr).find('.txtQty').val(0);
                        var pro_filter = [];

                        var selected_company_code = $('#To_Address option:selected').val();

                        if (selected_company_code == '' || selected_company_code == 0) {
                            $.confirm({
                                title: 'Confirm!',
                                content: 'Please select company',
                                type: 'red',
                                typeAnimated: true,
                                autoClose: 'action|8000',
                                icon: 'fa fa-warning ',
                                buttons: {
                                    tryAgain: {
                                        text: 'OK',
                                        btnClass: 'btn-red',
                                        action: function () {

                                        }
                                    }
                                }
                            });

                            $('.ddlProd').val('');
                            $('.ddlProd ').select2("destroy");
                            $('.ddlProd').select2();
                            return false;
                        }

                        $('.second_row_div').show();
                        sPCd = $(this).val();
                        $(this).closest("tr").attr('class', $(this).val());
                        var P_Name = itr.find('.ddlProd').find('option:selected').text();

                        //pro_filter = NewOrd.filter(function (s, key) {
                        //    return (s.PCd == sPCd && key != idx );
                        //});

                        if (pro_filter.length > 0) {

                            $(itr).find('.ddlProd').val('');
                            $(itr).find('.ddlProd ').select2("destroy");
                            $(itr).find('.ddlProd').select2();
                            $(itr).find('.Spinner-Value').text('Select');
                            $(itr).find('.tdRate').text("0.00");
                            $(itr).find('.txtQty').val('');
                            NewOrd[idx].Unit = 'Select';
                            NewOrd[idx].umo_unit = '';
                            $(itr).find('.second_row_div').text('');
                            cal(itr);
                            //CalcAmt(itr);
                            $.confirm({
                                title: 'Confirm!',
                                content: 'Product Already Selected',
                                type: 'red',
                                typeAnimated: true,
                                autoClose: 'action|8000',
                                icon: 'fa fa-warning ',
                                buttons: {
                                    tryAgain: {
                                        text: 'OK',
                                        btnClass: 'btn-red',
                                        action: function () {

                                        }
                                    }
                                }
                            });

                            return false;
                        }

                        Prod = Allrate.filter(function (a) {
                            return (a.Product_Detail_Code == sPCd);
                        })

                        rt = parseFloat($(itr).find('.tdRate').text()); if (isNaN(rt)) rt = 0; qt = parseFloat($(itr).find('.txtQty').val()); if (isNaN(qt)) qt = 0; //tax = parseFloat(Prod[0].Tax_Val || 0);

                        NewOrd[idx].PCd = sPCd;
                        NewOrd[idx].sPCd = $(this).val();
                        NewOrd[idx].s_pcode = Prod[0].Sale_Erp_Code;
                        NewOrd[idx].Unit = Prod[0].product_unit;
                        NewOrd[idx].PName = P_Name;
                        NewOrd[idx].umo_unit = Prod[0].Unit_code;
                        NewOrd[idx].Qty = qt;
                        NewOrd[idx].Free = 0;
                        NewOrd[idx].con_fac = Prod[0].Sample_Erp_Code;

                        //var unt_rate = Prod[0].PTS;
                        var unt_rate = 0;
                        //var unt_rate = Prod[0].RP_Base_Rate * Prod[0].Sample_Erp_Code;
                        if (unt_rate == 'null' || typeof unt_rate == "undefined" || unt_rate == "" || isNaN(unt_rate)) { unt_rate = 0; }
                        $(itr).find('.tdRate').text(0);
                        //$(itr).find('.tdRate').text(parseFloat(unt_rate).toFixed(2));
                        NewOrd[idx].Rate = unt_rate;


                        var tax_filter = []; var tax_arr = []; var filter_unit = []; units = ""; units1 = "";
                        filter_unit = All_unit.filter(function (w) {
                            return (sPCd == w.Product_Detail_Code);
                        });
                        //var row_length = $('#OrderEntry tbody').find('.' + sPCd).length;
                        //if (row_length == filter_unit.length) {
                        //    alert('product already exist');
                        //    return false;
                        //}
                        if (filter_unit.length > 0) {
                            //var unitrate = 0;
                            //for (var z = 0; z < filter_unit.length; z++) {
                            //    if (filter_unit[z].Move_MailFolder_Id == Prod[0].Unit_code) {
                            //        unitrate = Prod[0].PTS;
                            //        units += "<li id=" + unitrate + " name='itms' value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</li>";
                            //        units1 += "<option  id=" + unitrate + " value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";

                            //    }
                            //    else if (filter_unit[z].Move_MailFolder_Id == Prod[0].Base_Unit_code) {
                            //        unitrate = Prod[0].PTS / Prod[0].Sample_Erp_Code
                            //        units += "<li id=" + unitrate + " name='itms' value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</li>";
                            //        units1 += "<option  id=" + unitrate + " value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";

                            //    }
                            //}
                            if (Prod.length > 0) {
                                var unitrate = 0;
                                for (var z = 0; z < filter_unit.length; z++) {
                                    if (filter_unit[z].Move_MailFolder_Id == Prod[0].UnitCode) {
                                        unitrate = Prod[0].PTS;
                                        unitconfac = filter_unit[z].Sample_Erp_Code;
                                        units += "<li id=" + unitrate + " name='itms' value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</li>";
                                        units1 += "<option  id=" + unitrate + " value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";
                                    }
                                    else if (filter_unit[z].Move_MailFolder_Id != Prod[0].UnitCode && filter_unit[z].Move_MailFolder_Id == Prod[0].Base_Unit_code) {
                                        unitrate = Prod[0].PTS / Prod[0].Sample_Erp_Code
                                        unitconfac = filter_unit[z].Sample_Erp_Code;
                                        units += "<li id=" + unitrate + " name='itms' value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</li>";
                                        units1 += "<option  id=" + unitrate + " value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";
                                    }

                                    else {
                                        unitrate = Prod[0].PTS * filter_unit[z].Sample_Erp_Code
                                        unitconfac = filter_unit[z].Sample_Erp_Code;
                                        units += "<li id=" + unitrate + " name='itms' value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</li>";
                                        units1 += "<option  id=" + unitrate + " value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";

                                    }

                                    //else if (filter_unit[z].Move_MailFolder_Id == Prod[0].Base_Unit_code) {
                                    //    unitrate = Prod[0].PTS / Prod[0].Sample_Erp_Code
                                    //    units += "<li id=" + unitrate + " name='itms' value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</li>";
                                    //    units1 += "<option  id=" + unitrate + " value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";

                                    //}

                                }
                            }
                            else {
                                for (var z = 0; z < filter_unit.length; z++) {
                                    units += "<li id=" + unitrate + " name='itms' value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</li>";
                                    units1 += "<option  id=" + unitrate + " value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";


                                }
                            }
                        }
                        var ddlUnit = itr.find('.ispinner');
                        ddlUnit.empty();

                        // $(itr).find('#Td1').html("<select class='cbAlwTyp ispinner'><option value='1'>Select</option>" + Prod[0].product_unit + "</select><div class='Spinner-Input'><div style='display:none;' class='spinner-Value_val' value=" + Prod[0].Unit_code + "></div><div class='Spinner-Value'>" + Prod[0].product_unit + "</div><div class='Spinner-Modal'><ul>" + Prod[0].product_unit + "</ul></div></div><input type='text' autocomplete='off' id='txtQty' class='txtQty validate' pval='0.00' value='" + ((qt == 0) ? '' : qt) + "' style='text-align:right;width: 42%;'>");
                        $(itr).find('#Td1').html("<select class='cbAlwTyp ispinner'><option value='1'>Select</option>" + units1 + "</select><div class='Spinner-Input'><div style='display:none;' class='spinner-Value_val' value='0'></div><div class='Spinner-Value'>Select</div><div class='Spinner-Modal'><ul>" + units + "</ul></div></div><input type='text' autocomplete='off' id='txtQty' class='txtQty validate' pval='0.00' value='" + ((qt == 0) ? '' : qt) + "' style='text-align:right;width: 42%;'>");
                        setTimeout(function () { $(itr).find('#Td1').find('.txtQty').focus() }, 100);
                        tax_filter = All_Tax.filter(function (r) {
                            return (r.Product_Detail_Code == sPCd)
                        });

                        var comp = [];
                        var compVal = $('#To_Address option:selected').val();

                        comp = ComDetails.filter(function (w) {
                            return (Dist_state_code == w.State_Code && w.HO_ID == compVal);
                        });

                        if (comp.length <= 0) { var Retailer_State = undefined; } else { var Retailer_State = comp[0].State_Code; }
                        if (Dist_state_code == Retailer_State || Retailer_State == 0) { var type = 0; } else { var type = 1; }

                        tax_filter = All_Tax.filter(function (r) {
                            return (r.Product_Detail_Code == sPCd)
                            // && (r.Tax_Method_Id == type || r.Tax_Method_Id == 2))
                        });
                        $(itr).find('.second_row_div').text('');
                        $(itr).find('.second_row_div').append("<span style='color: #27c24c;padding: 5px;'><img style='width:16px;' src='data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABQAAAAUCAYAAACNiR0NAAAACXBIWXMAAAsTAAALEwEAmpwYAAABgklEQVR4nN3S20+CcBTAcf6Scm0tbWmZGqJc5CKgECjS+jPrDyj/hXrOHPMhBZQfF+vRnUbLLYcX7K2+z2ef7ewcDPsX+dJtJmyZ/Ug2UST1UCgaKGwaKBS6yBf0V8Rq2l5Y1DKtSDYhlHoQigaEzS74vL7wOf0l4DRArDpIj8k96ycUCB0IeB0CVruLZ1BDGSBGddOtKfasBMRp4LPX4DPqu08r9x7dXnhUezuIhJuDUOwO10INNcYA0Qogqg1evQUzQnS3Y83OcDckg1eTwCNEmF0J60Ff0jMB37FSQ3gzxmBa4ZIgYo3DgNesVUjZAfEwLXPgFhv9BBhw2uN+EAvTywa4RdoaF9nD5LqM+pwKKi0hBtwLypqcM5n1x6AVClGtj3QQDU6e3IwtQ3WZ9KrifCtUoGJsZOfJIyxNCBfIaYWfb4DAOSPSY8ucEkO6RXq+CtXAzlVH9nF5P2yZU6iTdr4+/4JOCbCz+O8x7Lu3XI2aZPGn8Qn+MM4Rydf4k30CsD+ms2XBE8sAAAAASUVORK5CYII='></span><label>Dis % :</label>&nbsp;&nbsp;<label class='dis_val_class' id='dis_val_class'></label>&nbsp;&nbsp;<span style='color: Dodgerblue;padding: 5px;'><img style='width: 16px;' src='data:image/png;base64,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'></span><label>Stock :</label>&nbsp;&nbsp;<label class='stockClass' id='stock_id'></label>&nbsp;&nbsp;<span style='color: #f29900;padding: 5px;'><img style='width:16px;' src='data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABQAAAAUCAYAAACNiR0NAAAACXBIWXMAAAsTAAALEwEAmpwYAAACaklEQVR4nK2UUUhTYRTHZ6WRb0FRVqzn6HFIzt3c7vk+N2WDHLF6WS1fWiQ+VFbEij1kRD0UPjjqBvXki0tR06K2+92ZKSsK3JMErSywLYUtTcfa3bpxNkZrcxhbBw7c853v+/3Pxzn3U6mKzGYbrON58aDqfxkAuw3Avuj10o6qYTwvGgmw1HGzN0YIe+p2uzdVDGtre7kTgH31OLrDi9calPbWZz8AmKvc/pyYUlMmrdQAsCd2y8CnzPWtitJbq7w73yhTKsoAgdbSmzANIZICwIR1cQDsgpH6VqKuPTLC8v6g07lMibgE4N9buL+paWab0cg0lE6pVeupAUg/zReDMfujmYXZPn0WdvbuyGrHvVCcWKdlQqRJvV7a8k8V8rzUAyDd54ToXLM/qYwIp7NAk/fzGsYGZ2gI8wYD02nGlHqtL9WoG05w3M15O+eJWDDWion9JWCtPykUAo+Ov1/CmGMpTX4PfuNasWv9SaEqYMuLZfnUaDDdMfEhsyHwyEQ44xh9k8ZD5YAohqIo3rwRsNgrBup8CTUeONwfMXM3wnbu8YoOY2wEIQFttjHO0BACsGGFQE6IzmEegF0uAeMo4EjgaOTXbLbBzQBMItZXaatnNt51Z3gVgaG+FuXEw+kFc89r/FVTBgM7VAKk1KdGmMUyVv+3kH8XJWJE6DwTLxz+xau7ZSP1fwdgl0pg5Sr8I8b0WEnwnC6FsF+9dcpJy0CYEMYqfkgIka600edrEdc+pd/RPQ8gfTOZJhsqguUMHxJp/Fi7N0aJKFPKTFXAcsZxU9sB2EdCpFtVw/JGaeCARvO2tjjxG2fCvWlN6gSbAAAAAElFTkSuQmCC'></span><label>Con_Fac :</label>&nbsp;&nbsp;<label id='Con_fac' class='Con_fac'></label>&nbsp;&nbsp;");

                        var append = ''; var total_tax_per = 0;

                        if (tax_filter.length > 0) {

                            for (var z = 0; z < tax_filter.length; z++) {
                                append += "<span style='color: red;padding: 5px;'><i class='fa-solid fa-percent'></i></span><label class='lbl_tax_type'>" + tax_filter[z].Tax_Type + "</label>:&nbsp;&nbsp;<label class='Tax_name' id='Tax_name'>" + tax_filter[z].Tax_name + "</label>&nbsp;&nbsp;";
                                var Push_data = tax_filter[z].Tax_Type;
                                total_tax_per = total_tax_per + parseFloat(tax_filter[z].Tax_Val);
                                NewOrd[idx][Push_data] = tax_filter[z].Tax_Val;

                                tax_arr.push({
                                    pro_code: sPCd,
                                    Tax_Code: tax_filter[z].Tax_Id,
                                    Tax_Name: tax_filter[z].Tax_Type,
                                    Tax_Amt: 0,
                                    Tax_Per: tax_filter[z].Tax_Val,
                                    umo_code: 0
                                });
                            }
                            NewOrd[idx].Tax_details = tax_arr;
                            NewOrd[idx].Total_Tax = total_tax_per;
                            $(itr).find('.second_row_div').append(append);
                        }
                        $(itr).find('.Con_fac').text(Prod[0].Sample_Erp_Code);
                        if (parseInt(Prod[0].TotalStock) > 0) {
                            $(itr).find('.stockClass').css("color", "rgb(39 194 76)");
                        }
                        else {
                            $(itr).find('.stockClass').css("color", "red");
                        }
                        $(itr).find('.stockClass').text(Prod[0].TotalStock);
                        $(itr).find('.stockClass').val(Prod[0].TotalStock);
                        $(itr).find('.dis_per').text("0");
                        $(itr).find('.sale_code').html(Prod[0].Sale_Erp_Code);
                        $(itr).find('.tdAmt').html((qt * rt).toFixed(2));
                        $(itr).find('.erp_code').val(Prod[0].Sample_Erp_Code);
                        $(itr).find('.dis_val_class').text(0);
                        $(itr).find('.fre').attr("id", sPCd);
                        $(itr).find('.fre1').attr("id", sPCd);
                        $(itr).find('.tddis_val').text('0.00');



                        getscheme(Prod[0].Sale_Erp_Code, '', '', $(itr).find('.Spinner-Value').text(), itr, $(itr).find('.fre1').text(), $(itr).find('.pcode').text(), idx, $(this).find('option:selected').text());
                        $(itr).find('.pcode').html(sPCd);
                        NewOrd[idx].Discount = $(itr).find('.dis_per').text() || 0;
                        cal(itr);
                        //CalcAmt(itr);

                    })
                }

                function DelRow() {

                    $(".slitm:checked").each(function () {

                        itr = $(this).closest('tr');
                        idx = $(itr).index();
                        var prod = itr.find('.ddlProd').val();
                        getscheme($(itr).find('.sale_code').text(), 0, $(itr).find('.Spinner-Value').text(), itr, $(itr).find('.fre1').text(), '', $(itr).find('.pcode').text());
                        $(this).closest('tr').remove();
                        NewOrd.splice(idx, 1);
                        if (prod != "") {
                            $(document).find('.' + prod).each(function () {
                                // calc_stock($(this).closest('tr'));
                            });
                        }

                        //}); resetSL(); ReCalc();
                    }); resetSL(); ReCal();
                }

                $(document).on('keyup', '.txtQty', function (e) {

                    tr = $(this).closest("tr");
                    idx = $(tr).index();
                    NewOrd[idx].PCd = tr.closest('tr').find('.ddlProd option:selected ').val();
                    NewOrd[idx].PName = tr.closest('tr').find('.ddlProd option:selected ').text();
                    NewOrd[idx].Unit = tr.closest('tr').find('.Spinner-Value').text();

                    if (e.keyCode == 13) {
                        if ($(this).val() != '' || isNaN($(this).val())) {
                            AddRow(1);
                            return false;
                        }
                    }

                    un = $(tr).find('.Spinner-Value').text();

                    if (un == 'Select') {
                        $.confirm({
                            title: 'Error!',
                            content: 'Please Select UOM',
                            type: 'red',
                            typeAnimated: true,
                            autoClose: 'action|8000',
                            icon: 'fa fa-warning ',
                            buttons: {
                                tryAgain: {
                                    text: 'OK',
                                    btnClass: 'btn-red',
                                    action: function () {

                                    }
                                }
                            }
                        });

                        $(tr).find('.txtQty').val('');
                        return false;
                    }

                    var erpcode = $(tr).find('.erp_code').val();

                    CQ = $(this).val()* $(tr).find('.Con_fac').text();
                    opcode = $(tr).find(".ddlProd :selected").val();

                    var disrate = $(this).closest("tr").find('.tdRate').text();
                    result = (CQ * disrate);

                    pCode = $(tr).find(".sale_code").text();
                    pname = $(tr).find(".ddlProd :selected").text();
                    ff = $(tr).find(".fre1").text();

                    getscheme(pCode, CQ, un, tr, ff, '', opcode);

                    NewOrd[idx].Free = $(tr).find('.fre1').text() || 0;
                    NewOrd[idx].Qty_c = $(this).val();
                    NewOrd[idx].Qty = parseFloat($(this).val()) * $(tr).find('.Con_fac').text();
                    NewOrd[idx].Discount = $(tr).find(".dis_val_class").text() || 0;
                    NewOrd[idx].of_Pro_Code = $(tr).find('.of_pro_code').val();
                    NewOrd[idx].of_Pro_Name = $(tr).find('.of_pro_name').val();
                    NewOrd[idx].of_Pro_Unit = $(tr).find('.fre').attr('unit') || 0;
                    NewOrd[idx].eQty = $(this).val();
                    NewOrd[idx].produnit = $(tr).find('.Con_fac').text();
                    cal(tr);
                    //CalcAmt(tr);

                });



                function ReCalc() {

                    tv = 0;
                    $('.tdtotal').each(function () {
                        v = parseFloat($(this).text()); if (isNaN(v)) v = 0;
                        tv += v;
                    })
                    $('#sub_tot').val(tv.toFixed(2));

                    tac = 0;
                    $('.tdtax').each(function () {
                        k = parseFloat($(this).text()); if (isNaN(k)) k = 0;
                        tac += k;
                    })
                    $('#Tax_GST').val(tac.toFixed(2));


                    tcss = 0;
                    $('.tdtcs').each(function () {
                        k = parseFloat($(this).text()); if (isNaN(k)) k = 0;
                        tcss += k;
                    })
                    $('#tot_tcs').val(tcss.toFixed(2));

                    /*
                    cgst_tax = 0;
                    $('.tdcgst').each(function () {
                        k = parseFloat($(this).text()); if (isNaN(k)) k = 0;
                        cgst_tax += k;
                    })
                    $('#txt_cgst').val(cgst_tax.toFixed(2));
     
                    sgst_tax = 0;
                    $('.tdsgst').each(function () {
                        k = parseFloat($(this).text()); if (isNaN(k)) k = 0;
                        sgst_tax += k;
                    })
                    $('#txt_sgst').val(sgst_tax.toFixed(2));
                    */

                    //igst_tax = 0;
                    //$('.tdigst').each(function () {
                    //    k = parseFloat($(this).text()); if (isNaN(k)) k = 0;
                    //    igst_tax += k;
                    //})
                    //$('#txt_igst').val(igst_tax.toFixed(2));

                    dis = 0;
                    $('.tddis').each(function () {
                        z = parseFloat($(this).text()); if (isNaN(z)) z = 0;
                        dis += z;
                    })
                    $('#Txt_Dis_tot').val(dis.toFixed(2));

                    gross = 0;
                    $('.tdAmt').each(function () {
                        i = parseFloat($(this).text()); if (isNaN(i)) i = 0;
                        gross += i;
                    })
                    $('#gross').val(gross.toFixed(2));

                }

                $(document).on('keypress', 'input[name=CQ]', function (e) {
                    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                        return false;
                    }
                });

                $(document).on('click', '#svorders', function () {
                    $('body,html').animate({
                        scrollTop: 0
                    }, 800);
                    $('.spinnner_div').show();
                    setTimeout(svOrder, 2000);
                    //// svOrder();
                });

                var approve = 0;
                svOrder = function () {

                    approve += 1; var Tax_Array = [];
                    if (approve == "1") {

                        var selectedvalue = $('#To_Address').val();
                        var selectdtext = $('#To_Address :selected').text();
                        if (selectedvalue == "0") {
                            button_Click = 0;

                            $.confirm({
                                title: 'Error!',
                                content: 'Please Select To',
                                type: 'red',
                                typeAnimated: true,
                                autoClose: 'action|8000',
                                icon: 'fa fa-warning ',
                                buttons: {
                                    tryAgain: {
                                        text: 'OK',
                                        btnClass: 'btn-red',
                                        action: function () {

                                        }
                                    }
                                }
                            });
                            $('.spinnner_div').hide();
                            return false
                        }

                        itm1 = {}

                        itm1.bill_add = $('#lbl_Bill_Address').text();
                        itm1.ship_add = $('#txt_Ship_add').val();
                        itm1.order_date = $('#txt_Purchase_Date').val();
                        if ($('#txt_Expected_Date').val() == "") {
                            itm1.exp_date = $('#txt_Purchase_Date').val();
                        }
                        else {
                            itm1.exp_date = $('#txt_Expected_Date').val();
                        }
                        itm1.div_code = Div_Code;

                        itm1.sup_no = selectedvalue;
                        itm1.sup_name = selectdtext;
                        itm1.sf_code = sf;
                        itm1.stk_code = stockist_Code;
                        itm1.com_add = $('#lbl_To_Address').text();
                        itm1.sub_tot = $(document).find('#sub_tot').val();
                        itm1.Tax_Total = $(document).find('#Tax_GST').val();
                        itm1.Gross_tot = $(document).find('#gross').val();
                        headdatas.push(itm1);

                        var netwt = 0;
                        for (var i = 0; i < NewOrd.length; i++) {
                            netwt += parseFloat(NewOrd[i].Qty);
                        }

                        var orderval = $('#gross').val();

                        if (NewOrd.length == 0) {
                            approve = 0;
                            $.confirm({
                                title: 'Error!',
                                content: 'Atleast select a Product',
                                type: 'red',
                                typeAnimated: true,
                                autoClose: 'action|8000',
                                icon: 'fa fa-warning ',
                                buttons: {
                                    tryAgain: {
                                        text: 'OK',
                                        btnClass: 'btn-red',
                                        action: function () {

                                        }
                                    }
                                }
                            });

                            headdatas = [];
                            $('.spinnner_div').hide();
                            return false;
                        }
                        for (var i = 0; i < NewOrd.length; i++) {

                            if (NewOrd[i].PName.indexOf('&') > -1) {
                                NewOrd[i].PName = NewOrd[i].PName.replace(/&/g, "&amp;");
                            }

                            if (NewOrd[i].sPCd == '') {
                                approve = 0;
                                $.confirm({
                                    title: 'Error!',
                                    content: 'Select a Product',
                                    type: 'red',
                                    typeAnimated: true,
                                    autoClose: 'action|8000',
                                    icon: 'fa fa-warning ',
                                    buttons: {
                                        tryAgain: {
                                            text: 'OK',
                                            btnClass: 'btn-red',
                                            action: function () {

                                            }
                                        }
                                    }
                                });

                                headdatas = [];
                                $('.spinnner_div').hide();
                                return false;
                            }
                            if (NewOrd[i].Qty == '') {
                                approve = 0;
                                $.confirm({
                                    title: 'Error!',
                                    content: 'Remove the Product or Enter the Quantity',
                                    type: 'red',
                                    typeAnimated: true,
                                    autoClose: 'action|8000',
                                    icon: 'fa fa-warning ',
                                    buttons: {
                                        tryAgain: {
                                            text: 'OK',
                                            btnClass: 'btn-red',
                                            action: function () {

                                            }
                                        }
                                    }
                                });

                                headdatas = [];
                                $('.spinnner_div').hide();
                                return false;
                            }

                            for (var f = 0; f < NewOrd[i].Tax_details.length; f++) {
                                NewOrd[i].Tax_details[f]["umo_code"] = NewOrd[i].produnit;
                                Tax_Array.push(NewOrd[i].Tax_details[f]);
                            }
                        }

                        if (orderval > 0) {

                            $.ajax({
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                async: false,
                                url: "Purchase_Order_Edit.aspx/EditPurchaseOrder",
                                data: "{'HeadData':'" + JSON.stringify(headdatas) + "','DetailsData':'" + JSON.stringify(NewOrd) + "','TaxData':'" + JSON.stringify(Tax_Array) + "','orderid':'" + Order_id + "'}",
                                dataType: "json",
                                success: function (data) {
                                    if (data.d.length > 0) {

                                        $.confirm({
                                            title: 'Success!',
                                            content: 'Purchase Ordered Successfully!',
                                            type: 'green',
                                            typeAnimated: true,
                                            autoClose: 'action|8000',
                                            icon: 'fa fa-check fa-2x',
                                            buttons: {
                                                tryAgain: {
                                                    text: 'OK',
                                                    btnClass: 'btn-green',
                                                    action: function () {
                                                        $('.spinnner_div').hide();
                                                        window.location.href = "../Stockist/Purchase_Order_List.aspx";
                                                    }
                                                }
                                            }
                                        });
                                        // Pri_order_id1 = data.d;
                                        //Pri_order_id = "'" + data.d + "'" + ',';

                                        //$.ajax({
                                        //    type: "POST",
                                        //    contentType: "application/json; charset=utf-8",
                                        //    async: false,
                                        //    data: '{"Order_id":"' + Pri_order_id + '"}',
                                        //    url: "Purchase_Order3.aspx/getorder",
                                        //    dataType: "json",
                                        //    success: function (data) {
                                        //        var PriOrdered_Data = data.d;
                                        //        call_api(PriOrdered_Data);
                                        //    },
                                        //    error: function (result) {
                                        //        button_Click = 0
                                        //        alert(JSON.stringify(result));
                                        //    }
                                        //});
                                        // window.location.href = "../Stockist/Purchase_Order_List.aspx";
                                    }
                                    else {
                                        $.confirm({
                                            title: 'Error!',
                                            content: 'Something Went Wrong',
                                            type: 'red',
                                            typeAnimated: true,
                                            autoClose: 'action|8000',
                                            icon: 'fa fa-warning ',
                                            buttons: {
                                                tryAgain: {
                                                    text: 'OK',
                                                    btnClass: 'btn-red',
                                                    action: function () {
                                                        $('.spinnner_div').hide();
                                                        window.location.href = "../Stockist/Purchase_Order_List.aspx";
                                                    }
                                                }
                                            }
                                        });
                                    }
                                },
                                error: function (result) {
                                    $('.spinnner_div').hide();
                                }
                            });
                        } else {
                            approve = 0;
                            $.confirm({
                                title: 'Error!',
                                content: 'Order Minimum Value to create a Order',
                                type: 'red',
                                typeAnimated: true,
                                autoClose: 'action|8000',
                                icon: 'fa fa-warning ',
                                buttons: {
                                    tryAgain: {
                                        text: 'OK',
                                        btnClass: 'btn-red',
                                        action: function () {

                                        }
                                    }
                                }
                            });
                            $('.spinnner_div').hide();
                            return false;
                        }
                    }
                }

                function call_api(PriOrdered_Data) {
                    var url = JSON.parse(PriOrdered_Data);
                    var trans_sl_no = url.mainOrder[0].Trans_sl_no;
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        data: "{'urlparam':'" + JSON.stringify(url) + "','trans_sl_no':'" + trans_sl_no + "'}",
                        url: "Purchase_Order_Edit.aspx/getPrimaryOrderValue",
                        dataType: "json",
                        success: function (data) {
                            var PriOrdered_Data = JSON.parse(data.d);
                            sap_code = PriOrdered_Data[0].result;
                            if ($.isNumeric(sap_code) == true)
                                alert("success");
                            else
                                alert(sap_code);
                        },
                        error: function (result) {
                            //alert(PriOrdered_Data);                        
                            alert(JSON.stringify(result));
                        }
                    });
                }
            });
            var editord = [];
            function ReloadTable() {

                for ($j = 0; $j < Orders.length; $j++) {

                    itm = {}
                    itm.PCd = ''; itm.sPCd = ''; itm.s_pcode = ''; itm.PName = ''; itm.Unit = ''; itm.Rate = "0"; itm.Qty = 1; itm.Qty_c = 1; itm.Free = "0"; itm.Discount = "0"; itm.Dis_value = "0"; itm.Total_Tax = 0; itm.produnit = 0; itm.eQty = 0;
                    itm.Tax_details = ''; itm.Tax = "0"; itm.Tax_value = "0"; itm.Total = "0"; itm.Gross_Amt = "0"; itm.Sub_Total = 1; itm.of_Pro_Code = ''; itm.of_Pro_Name = ''; itm.of_Pro_Unit = ''; itm.umo_unit = ''; itm.con_fac = ''; itm.TCS = ''; itm.TDS = '';
                    NewOrd.push(itm);
                    for ($k = 0; $k < itms.length; $k++) {
                        if (itms[$k].Product_Detail_Code == Orders[$j].Product_Code)
                            edt += "<option selected value='" + itms[$k].Product_Detail_Code + "'>" + itms[$k].Product_Detail_Name + "</option>";
                        else
                            edt += "<option  value='" + itms[$k].Product_Detail_Code + "'>" + itms[$k].Product_Detail_Name + "</option>";
                    }
                    tr = $("<tr class='subRow'></tr>");
                    $(tr).html("<td class='rowno'><label style='margin-bottom:0px;'><input type='checkbox' class='slitm'> <span class='rwsl'>" + ($("#OrderEntry > TBODY > tr").length + 1) + "</span></label></td><td class='pro_td' style='width:27%;padding: 9px 0px 0px 0px;'><select class='ddlProd' style='margin-top:-3px;height:25px;width:250px!important'><option value='0'>--select--</option>" + edt + "</select><div class='second_row_div' style='display:none; font-size:11px;padding: 5px 0px 0px 3px;margin-left: -8px;'></div></td><td style='display:none;' ><input type='hidden' class='sale_code' /></td><td id='Td1' style='width: 18%;padding: 8px 0px 0px 30px;'><select class='cbAlwTyp ispinner'></select><div class='Spinner-Input'><div class='Spinner-Value'>Select</div><div class='Spinner-Modal'><ul>Select</ul></div></div><input type='text' name='txqty' id='txqty' class='txtQty validate' pval='0.00' value=" + Orders[$j].qty / Orders[$j].con_fact + "   style='text-align:right;width: 42%;'></td><td class='tdRate' style='text-align:right; padding: 17px 0px 0px 0px;'>" + Orders[$j].Rate + "</td><td class='fre' style='text-align:right; padding: 17px 9px 0px 0px;'>0</td><td style='display:none;'<label name='free' class='fre'></lable></td><td style='display:none' ><input type='hidden' class='fre1' name='fre1' ></td><td style='display:none' ><input type='hidden' class='of_pro_name'  name='of_pro_name' ></td><td style='display:none' ><input type='hidden' class='of_pro_code'  name='of_pro_code' ></td><td class='tddis_val' style='text-align:right; padding: 17px 11px 0px 0px;'>0</td><td style='display:none'><input type='hidden' class='disc_value' name='disc_value'></td><td style='display:none'><input type='hidden' class='disc_value'  name='disc_value' ></td><td class='tdtotal' style='text-align:right;padding: 17px 7px 0px 0px;'>" + Orders[$j].value + "</td><td class='tdtax' style='text-align:right;padding: 17px 0px 0px 0px;'>0.00</td><td style='display:none;'><input type='hidden' class='tdcgst' id='tdcgst' /></td><td style='display:none;'><input type='hidden' class='tdsgst' id='tsgst' ></td><td style='display:none;'><input type='hidden' class='tdigst' id='tigst' ></td><td style='display:none;'><input type='hidden' class='tdtcs' id='tdtcs' ></td><td class='tdAmt' style='text-align:right;padding: 17px 6px 0px 0px;width: 10%;'>0.00</td><td style='display:none;'><label id='pcode' class='pcode'></label></td><td style='display:none;' ><input type='hidden' class='erp_code' /></td>");
                    $("#OrderEntry > TBODY").append(tr); //OrderEntry
                    $('.ddlProd').select2();
                    $('.ddlProd').on('select2:open', function () {
                        var selected_cat_code = $('.btn.active').val();
                        var selected_cat_Name = $('.btn.active').text();
                        var filtered_prd = [];

                        filtered_prd = itms.filter(function (k) {
                            return (k.Product_Cat_Code == selected_cat_code);
                        });

                        $(this).html('');
                        filtered_prd = selected_cat_code == -1 ? itms : filtered_prd;


                        let str = '<option value=-1>--select--</option>';
                        for (var h = 0; h < filtered_prd.length; h++) {
                            str += `<option value="${filtered_prd[h].Product_Detail_Code}">${filtered_prd[h].Product_Detail_Name}</option>`;
                        }
                        $(this).html(str);
                        $(this).trigger("select2:updated");

                    });
                    $(".ddlProd").on("change", function () {

                        itr = $(this).closest('tr');
                        idx = $(itr).index();
                        $(itr).find('.txtQty').val(0);
                        var pro_filter = [];

                        var selected_company_code = $('#To_Address option:selected').val();

                        if (selected_company_code == '' || selected_company_code == 0) {
                            alert('Please select company');
                            $('.ddlProd').val('');
                            $('.ddlProd ').select2("destroy");
                            $('.ddlProd').select2();
                            return false;
                        }

                        $('.second_row_div').show();
                        sPCd = $(this).val();
                        $(this).closest("tr").attr('class', $(this).val());
                        var P_Name = itr.find('.ddlProd').find('option:selected').text();

                        //pro_filter = NewOrd.filter(function (s, key) {
                        //    return (s.PCd == sPCd && key != idx );
                        //});

                        if (pro_filter.length > 0) {

                            $(itr).find('.ddlProd').val('');
                            $(itr).find('.ddlProd ').select2("destroy");
                            $(itr).find('.ddlProd').select2();
                            $(itr).find('.Spinner-Value').text('Select');
                            $(itr).find('.tdRate').text("0.00");
                            $(itr).find('.txtQty').val('');
                            NewOrd[idx].Unit = 'Select';
                            NewOrd[idx].umo_unit = '';
                            $(itr).find('.second_row_div').text('');
                            cal(itr);
                            //CalcAmt(itr);
                            alert('Product Already Selected');
                            return false;
                        }

                        Prod = Allrate.filter(function (a) {
                            return (a.Product_Detail_Code == sPCd);
                        })

                        rt = parseFloat($(itr).find('.tdRate').text()); if (isNaN(rt)) rt = 0; qt = parseFloat($(itr).find('.txtQty').val()); if (isNaN(qt)) qt = 0; //tax = parseFloat(Prod[0].Tax_Val || 0);

                        NewOrd[idx].PCd = sPCd;
                        NewOrd[idx].sPCd = $(this).val();
                        NewOrd[idx].s_pcode = Prod[0].Sale_Erp_Code;
                        NewOrd[idx].Unit = Prod[0].product_unit;
                        NewOrd[idx].PName = P_Name;
                        NewOrd[idx].umo_unit = Prod[0].Unit_code;
                        NewOrd[idx].Qty = qt;
                        NewOrd[idx].Free = 0;
                        NewOrd[idx].con_fac = Prod[0].Sample_Erp_Code;

                        //var unt_rate = Prod[0].PTS;
                        var unt_rate = 0;
                        //var unt_rate = Prod[0].RP_Base_Rate * Prod[0].Sample_Erp_Code;
                        if (unt_rate == 'null' || typeof unt_rate == "undefined" || unt_rate == "" || isNaN(unt_rate)) { unt_rate = 0; }
                        $(itr).find('.tdRate').text(0);
                        //$(itr).find('.tdRate').text(parseFloat(unt_rate).toFixed(2));
                        NewOrd[idx].Rate = unt_rate;


                        var tax_filter = []; var tax_arr = []; var filter_unit = []; units = ""; units1 = "";
                        filter_unit = All_unit.filter(function (w) {
                            return (sPCd == w.Product_Detail_Code);
                        });
                        //var row_length = $('#OrderEntry tbody').find('.' + sPCd).length;
                        //if (row_length == filter_unit.length) {
                        //    alert('product already exist');
                        //    return false;
                        //}
                        if (filter_unit.length > 0) {
                            var unitrate = 0;
                            for (var z = 0; z < filter_unit.length; z++) {
                                if (filter_unit[z].Move_MailFolder_Id == Prod[0].Unit_code) {
                                    unitrate = Prod[0].PTS;
                                    units += "<li id=" + unitrate + " name='itms' value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</li>";
                                    units1 += "<option  id=" + unitrate + " value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";
                                }

                                else if (filter_unit[z].Move_MailFolder_Id == Prod[0].Base_Unit_code) {
                                    unitrate = Prod[0].PTS / Prod[0].Sample_Erp_Code
                                    units += "<li id=" + unitrate + " name='itms' value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</li>";
                                    units1 += "<option  id=" + unitrate + " value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";
                                }



                            }
                        }
                        var ddlUnit = itr.find('.ispinner');
                        ddlUnit.empty();

                        // $(itr).find('#Td1').html("<select class='cbAlwTyp ispinner'><option value='1'>Select</option>" + Prod[0].product_unit + "</select><div class='Spinner-Input'><div style='display:none;' class='spinner-Value_val' value=" + Prod[0].Unit_code + "></div><div class='Spinner-Value'>" + Prod[0].product_unit + "</div><div class='Spinner-Modal'><ul>" + Prod[0].product_unit + "</ul></div></div><input type='text' autocomplete='off' id='txtQty' class='txtQty validate' pval='0.00' value='" + ((qt == 0) ? '' : qt) + "' style='text-align:right;width: 42%;'>");
                        $(itr).find('#Td1').html("<select class='cbAlwTyp ispinner'><option value='1'>Select</option>" + units1 + "</select><div class='Spinner-Input'><div style='display:none;' class='spinner-Value_val' value='0'></div><div class='Spinner-Value'>Select</div><div class='Spinner-Modal'><ul>" + units + "</ul></div></div><input type='text' autocomplete='off' id='txtQty' class='txtQty validate' pval='0.00' value='" + ((qt == 0) ? '' : qt) + "' style='text-align:right;width: 42%;'>");
                        setTimeout(function () { $(itr).find('#Td1').find('.txtQty').focus() }, 100);
                        tax_filter = All_Tax.filter(function (r) {
                            return (r.Product_Detail_Code == sPCd)
                        });

                        var comp = [];
                        var compVal = $('#To_Address option:selected').val();

                        comp = ComDetails.filter(function (w) {
                            return (Dist_state_code == w.State_Code && w.HO_ID == compVal);
                        });

                        if (comp.length <= 0) { var Retailer_State = undefined; } else { var Retailer_State = comp[0].State_Code; }
                        if (Dist_state_code == Retailer_State || Retailer_State == 0) { var type = 0; } else { var type = 1; }

                        tax_filter = All_Tax.filter(function (r) {
                            return (r.Product_Detail_Code == sPCd)// && (r.Tax_Method_Id == type || r.Tax_Method_Id == 2))
                        });
                        $(itr).find('.second_row_div').text('');
                        $(itr).find('.second_row_div').append("<span style='color: #27c24c;padding: 5px;'><img style='width:16px;' src='data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABQAAAAUCAYAAACNiR0NAAAACXBIWXMAAAsTAAALEwEAmpwYAAABgklEQVR4nN3S20+CcBTAcf6Scm0tbWmZGqJc5CKgECjS+jPrDyj/hXrOHPMhBZQfF+vRnUbLLYcX7K2+z2ef7ewcDPsX+dJtJmyZ/Ug2UST1UCgaKGwaKBS6yBf0V8Rq2l5Y1DKtSDYhlHoQigaEzS74vL7wOf0l4DRArDpIj8k96ycUCB0IeB0CVruLZ1BDGSBGddOtKfasBMRp4LPX4DPqu08r9x7dXnhUezuIhJuDUOwO10INNcYA0Qogqg1evQUzQnS3Y83OcDckg1eTwCNEmF0J60Ff0jMB37FSQ3gzxmBa4ZIgYo3DgNesVUjZAfEwLXPgFhv9BBhw2uN+EAvTywa4RdoaF9nD5LqM+pwKKi0hBtwLypqcM5n1x6AVClGtj3QQDU6e3IwtQ3WZ9KrifCtUoGJsZOfJIyxNCBfIaYWfb4DAOSPSY8ucEkO6RXq+CtXAzlVH9nF5P2yZU6iTdr4+/4JOCbCz+O8x7Lu3XI2aZPGn8Qn+MM4Rydf4k30CsD+ms2XBE8sAAAAASUVORK5CYII='></i></span><label>Dis % :</label>&nbsp;&nbsp;<label class='dis_val_class' id='dis_val_class'></label>&nbsp;&nbsp;<span style='color: Dodgerblue;padding: 5px;'><img style='width: 16px;' src='data:image/png;base64,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'></span><label>Stock :</label>&nbsp;&nbsp;<label class='stockClass' id='stock_id'></label>&nbsp;&nbsp;<span style='color: #f29900;padding: 5px;'><img style='width:16px;' src='data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABQAAAAUCAYAAACNiR0NAAAACXBIWXMAAAsTAAALEwEAmpwYAAACaklEQVR4nK2UUUhTYRTHZ6WRb0FRVqzn6HFIzt3c7vk+N2WDHLF6WS1fWiQ+VFbEij1kRD0UPjjqBvXki0tR06K2+92ZKSsK3JMErSywLYUtTcfa3bpxNkZrcxhbBw7c853v+/3Pxzn3U6mKzGYbrON58aDqfxkAuw3Avuj10o6qYTwvGgmw1HGzN0YIe+p2uzdVDGtre7kTgH31OLrDi9calPbWZz8AmKvc/pyYUlMmrdQAsCd2y8CnzPWtitJbq7w73yhTKsoAgdbSmzANIZICwIR1cQDsgpH6VqKuPTLC8v6g07lMibgE4N9buL+paWab0cg0lE6pVeupAUg/zReDMfujmYXZPn0WdvbuyGrHvVCcWKdlQqRJvV7a8k8V8rzUAyDd54ToXLM/qYwIp7NAk/fzGsYGZ2gI8wYD02nGlHqtL9WoG05w3M15O+eJWDDWion9JWCtPykUAo+Ov1/CmGMpTX4PfuNasWv9SaEqYMuLZfnUaDDdMfEhsyHwyEQ44xh9k8ZD5YAohqIo3rwRsNgrBup8CTUeONwfMXM3wnbu8YoOY2wEIQFttjHO0BACsGGFQE6IzmEegF0uAeMo4EjgaOTXbLbBzQBMItZXaatnNt51Z3gVgaG+FuXEw+kFc89r/FVTBgM7VAKk1KdGmMUyVv+3kH8XJWJE6DwTLxz+xau7ZSP1fwdgl0pg5Sr8I8b0WEnwnC6FsF+9dcpJy0CYEMYqfkgIka600edrEdc+pd/RPQ8gfTOZJhsqguUMHxJp/Fi7N0aJKFPKTFXAcsZxU9sB2EdCpFtVw/JGaeCARvO2tjjxG2fCvWlN6gSbAAAAAElFTkSuQmCC'></span><label>Con_Fac :</label>&nbsp;&nbsp;<label id='Con_fac' class='Con_fac'></label>&nbsp;&nbsp;");

                        var append = ''; var total_tax_per = 0;

                        if (tax_filter.length > 0) {

                            for (var z = 0; z < tax_filter.length; z++) {
                                append += "<span style='color: red;padding: 5px;'><i class='fa-solid fa-percent'></i></span><label class='lbl_tax_type'>" + tax_filter[z].Tax_Type + "</label>:&nbsp;&nbsp;<label class='Tax_name' id='Tax_name'>" + tax_filter[z].Tax_name + "</label>&nbsp;&nbsp;";
                                var Push_data = tax_filter[z].Tax_Type;
                                total_tax_per = total_tax_per + parseFloat(tax_filter[z].Tax_Val);
                                NewOrd[idx][Push_data] = tax_filter[z].Tax_Val;

                                tax_arr.push({
                                    pro_code: sPCd,
                                    Tax_Code: tax_filter[z].Tax_Id,
                                    Tax_Name: tax_filter[z].Tax_Type,
                                    Tax_Amt: 0,
                                    Tax_Per: tax_filter[z].Tax_Val,
                                    umo_code: 0
                                });
                            }
                            NewOrd[idx].Tax_details = tax_arr;
                            NewOrd[idx].Total_Tax = total_tax_per;
                            $(itr).find('.second_row_div').append(append);
                        }
                        $(itr).find('.Con_fac').text(Prod[0].Sample_Erp_Code);
                        if (parseInt(Prod[0].TotalStock) > 0) {
                            $(itr).find('.stockClass').css("color", "rgb(39 194 76)");
                        }
                        else {
                            $(itr).find('.stockClass').css("color", "red");
                        }
                        $(itr).find('.stockClass').text(Prod[0].TotalStock);
                        $(itr).find('.stockClass').val(Prod[0].TotalStock);
                        $(itr).find('.dis_per').text("0");
                        $(itr).find('.sale_code').html(Prod[0].Sale_Erp_Code);
                        $(itr).find('.tdAmt').html((qt * rt).toFixed(2));
                        $(itr).find('.erp_code').val(Prod[0].Sample_Erp_Code);
                        $(itr).find('.dis_val_class').text(0);
                        $(itr).find('.fre').attr("id", sPCd);
                        $(itr).find('.fre1').attr("id", sPCd);
                        $(itr).find('.tddis_val').text('0.00');



                        getscheme(Prod[0].Sale_Erp_Code, '', '', $(itr).find('.Spinner-Value').text(), itr, $(itr).find('.fre1').text(), $(itr).find('.pcode').text(), idx, $(this).find('option:selected').text());
                        $(itr).find('.pcode').html(sPCd);
                        NewOrd[idx].Discount = $(itr).find('.dis_per').text() || 0;
                        cal(itr);
                        //CalcAmt(itr);
                    })
                    itr = $('#OrderEntry > TBODY tr:last');
                    idx = $(itr).index();
                    var pro_filter = [];

                    var selected_company_code = $('#To_Address option:selected').val();



                    $('.second_row_div').show();
                    // sPCd = $(this).val();
                    itr.attr('class', Orders[$j].Product_Code);
                    var P_Name = itr.find('.ddlProd').find('option:selected').text();

                    //pro_filter = NewOrd.filter(function (s, key) {
                    //    return (s.PCd == sPCd && key != idx );
                    //});

                    //if (pro_filter.length > 0) {

                    //    $(itr).find('.ddlProd').val('');
                    //    $(itr).find('.ddlProd ').select2("destroy");
                    //    $(itr).find('.ddlProd').select2();
                    //    $(itr).find('.Spinner-Value').text('Select');
                    //    $(itr).find('.tdRate').text("0.00");
                    //    $(itr).find('.txtQty').val('');
                    //    NewOrd[idx].Unit = 'Select';
                    //    NewOrd[idx].umo_unit = '';
                    //    $(itr).find('.second_row_div').text('');
                    //    CalcAmt(itr);
                    //    alert('Product Already Selected');
                    //    return false;
                    //}

                    Prod = Allrate.filter(function (a) {
                        return (a.Product_Detail_Code == Orders[$j].Product_Code);
                    })

                    rt = parseFloat($(itr).find('.tdRate').text()); if (isNaN(rt)) rt = 0; qt = parseFloat($(itr).find('.txtQty').val()); if (isNaN(qt)) qt = 0; //tax = parseFloat(Prod[0].Tax_Val || 0);

                    NewOrd[idx].PCd = Orders[$j].Product_Code;
                    NewOrd[idx].sPCd = Orders[$j].Product_Code;
                    NewOrd[idx].s_pcode = Prod[0].Sale_Erp_Code;
                    NewOrd[idx].Unit = Orders[$j].unit_name;
                    NewOrd[idx].PName = Orders[$j].Product_Name;
                    NewOrd[idx].umo_unit = Orders[$j].Unit;
                    NewOrd[idx].Qty = Orders[$j].qty;
                    NewOrd[idx].Qty_c = Orders[$j].qty;
                    NewOrd[idx].Free = 0;
                    NewOrd[idx].con_fac = Prod[0].Sample_Erp_Code;
                    NewOrd[idx].con_fac = Prod[0].Sample_Erp_Code;
                    NewOrd[idx].produnit = Orders[$j].Product_Unit_Value;

                    //var unt_rate = Prod[0].PTS;
                    var unt_rate = 0;
                    //var unt_rate = Prod[0].RP_Base_Rate * Prod[0].Sample_Erp_Code;
                    if (unt_rate == 'null' || typeof unt_rate == "undefined" || unt_rate == "" || isNaN(unt_rate)) { unt_rate = 0; }
                    $(itr).find('.tdRate').text(Orders[$j].Rate);
                    //$(itr).find('.tdRate').text(parseFloat(unt_rate).toFixed(2));
                    NewOrd[idx].Rate = Orders[$j].Rate;


                    var tax_filter = []; var tax_arr = []; var filter_unit = []; units = ""; units1 = "";
                    filter_unit = All_unit.filter(function (w) {
                        return (Orders[$j].Product_Code == w.Product_Detail_Code);
                    });
                    //var row_length = $('#OrderEntry tbody').find('.' + sPCd).length;
                    //if (row_length == filter_unit.length) {
                    //    alert('product already exist');
                    //    return false;
                    //}
                    if (filter_unit.length > 0) {
                        var unitrate = 0; var uniy = 0
                        for (var z = 0; z < filter_unit.length; z++) {
                            if (filter_unit[z].Move_MailFolder_Id == Orders[$j].Unit)
                                uniy = filter_unit[z].Move_MailFolder_Name;
                            if (uniy == 0) {
                                if (filter_unit[z].Move_MailFolder_Name == Orders[$j].Unit)
                                    uniy = filter_unit[z].Move_MailFolder_Name;
                            }
                            if (filter_unit[z].Move_MailFolder_Id == Prod[0].Unit_code) {
                                unitrate = Prod[0].PTS;
                                units += "<li id=" + unitrate + " name='itms' value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</li>";
                                units1 += "<option  id=" + unitrate + " value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";
                            }

                            else if (filter_unit[z].Move_MailFolder_Id == Prod[0].Base_Unit_code) {
                                unitrate = Prod[0].PTS / Prod[0].Sample_Erp_Code
                                units += "<li id=" + unitrate + " name='itms' value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</li>";
                                units1 += "<option  id=" + unitrate + " value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";
                            }



                        }
                    }
                    var ddlUnit = itr.find('.ispinner');
                    ddlUnit.empty();

                    // $(itr).find('#Td1').html("<select class='cbAlwTyp ispinner'><option value='1'>Select</option>" + Prod[0].product_unit + "</select><div class='Spinner-Input'><div style='display:none;' class='spinner-Value_val' value=" + Prod[0].Unit_code + "></div><div class='Spinner-Value'>" + Prod[0].product_unit + "</div><div class='Spinner-Modal'><ul>" + Prod[0].product_unit + "</ul></div></div><input type='text' autocomplete='off' id='txtQty' class='txtQty validate' pval='0.00' value='" + ((qt == 0) ? '' : qt) + "' style='text-align:right;width: 42%;'>");
                    $(itr).find('#Td1').html("<select class='cbAlwTyp ispinner'><option value='1'>Select</option>" + units1 + "</select><div class='Spinner-Input'><div style='display:none;' class='spinner-Value_val' value='0'></div><div class='Spinner-Value'>" + uniy + "</div><div class='Spinner-Modal'><ul>" + units + "</ul></div></div><input type='text' autocomplete='off' id='txtQty' class='txtQty validate' pval='0.00' value='" + ((qt == 0) ? '' : qt) + "' style='text-align:right;width: 42%;'>");
                    setTimeout(function () { $(itr).find('#Td1').find('.txtQty').focus() }, 100);
                    tax_filter = All_Tax.filter(function (r) {
                        return (r.Product_Detail_Code == Orders[$j].Product_Code)
                    });

                    var comp = [];
                    var compVal = $('#To_Address option:selected').val();

                    comp = ComDetails.filter(function (w) {
                        return (Dist_state_code == w.State_Code && w.HO_ID == compVal);
                    });

                    if (comp.length <= 0) { var Retailer_State = undefined; } else { var Retailer_State = comp[0].State_Code; }
                    if (Dist_state_code == Retailer_State || Retailer_State == 0) { var type = 0; } else { var type = 1; }

                    tax_filter = All_Tax.filter(function (r) {
                        return (r.Product_Detail_Code == Orders[$j].Product_Code)// && (r.Tax_Method_Id == type || r.Tax_Method_Id == 2))
                    });
                    $(itr).find('.second_row_div').text('');
                    $(itr).find('.second_row_div').append("<span style='color: #27c24c;padding: 5px;'><img style='width:16px;' src='data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABQAAAAUCAYAAACNiR0NAAAACXBIWXMAAAsTAAALEwEAmpwYAAABgklEQVR4nN3S20+CcBTAcf6Scm0tbWmZGqJc5CKgECjS+jPrDyj/hXrOHPMhBZQfF+vRnUbLLYcX7K2+z2ef7ewcDPsX+dJtJmyZ/Ug2UST1UCgaKGwaKBS6yBf0V8Rq2l5Y1DKtSDYhlHoQigaEzS74vL7wOf0l4DRArDpIj8k96ycUCB0IeB0CVruLZ1BDGSBGddOtKfasBMRp4LPX4DPqu08r9x7dXnhUezuIhJuDUOwO10INNcYA0Qogqg1evQUzQnS3Y83OcDckg1eTwCNEmF0J60Ff0jMB37FSQ3gzxmBa4ZIgYo3DgNesVUjZAfEwLXPgFhv9BBhw2uN+EAvTywa4RdoaF9nD5LqM+pwKKi0hBtwLypqcM5n1x6AVClGtj3QQDU6e3IwtQ3WZ9KrifCtUoGJsZOfJIyxNCBfIaYWfb4DAOSPSY8ucEkO6RXq+CtXAzlVH9nF5P2yZU6iTdr4+/4JOCbCz+O8x7Lu3XI2aZPGn8Qn+MM4Rydf4k30CsD+ms2XBE8sAAAAASUVORK5CYII='></span><label>Dis % :</label>&nbsp;&nbsp;<label class='dis_val_class' id='dis_val_class'></label>&nbsp;&nbsp;<span style='color: Dodgerblue;padding: 5px;'><img style='width: 16px;' src='data:image/png;base64,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'></span><label>Stock :</label>&nbsp;&nbsp;<label class='stockClass' id='stock_id'></label>&nbsp;&nbsp;<span style='color: #f29900;padding: 5px;'><img style='width:16px;' src='data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABQAAAAUCAYAAACNiR0NAAAACXBIWXMAAAsTAAALEwEAmpwYAAACaklEQVR4nK2UUUhTYRTHZ6WRb0FRVqzn6HFIzt3c7vk+N2WDHLF6WS1fWiQ+VFbEij1kRD0UPjjqBvXki0tR06K2+92ZKSsK3JMErSywLYUtTcfa3bpxNkZrcxhbBw7c853v+/3Pxzn3U6mKzGYbrON58aDqfxkAuw3Avuj10o6qYTwvGgmw1HGzN0YIe+p2uzdVDGtre7kTgH31OLrDi9calPbWZz8AmKvc/pyYUlMmrdQAsCd2y8CnzPWtitJbq7w73yhTKsoAgdbSmzANIZICwIR1cQDsgpH6VqKuPTLC8v6g07lMibgE4N9buL+paWab0cg0lE6pVeupAUg/zReDMfujmYXZPn0WdvbuyGrHvVCcWKdlQqRJvV7a8k8V8rzUAyDd54ToXLM/qYwIp7NAk/fzGsYGZ2gI8wYD02nGlHqtL9WoG05w3M15O+eJWDDWion9JWCtPykUAo+Ov1/CmGMpTX4PfuNasWv9SaEqYMuLZfnUaDDdMfEhsyHwyEQ44xh9k8ZD5YAohqIo3rwRsNgrBup8CTUeONwfMXM3wnbu8YoOY2wEIQFttjHO0BACsGGFQE6IzmEegF0uAeMo4EjgaOTXbLbBzQBMItZXaatnNt51Z3gVgaG+FuXEw+kFc89r/FVTBgM7VAKk1KdGmMUyVv+3kH8XJWJE6DwTLxz+xau7ZSP1fwdgl0pg5Sr8I8b0WEnwnC6FsF+9dcpJy0CYEMYqfkgIka600edrEdc+pd/RPQ8gfTOZJhsqguUMHxJp/Fi7N0aJKFPKTFXAcsZxU9sB2EdCpFtVw/JGaeCARvO2tjjxG2fCvWlN6gSbAAAAAElFTkSuQmCC'></span><label>Con_Fac :</label>&nbsp;&nbsp;<label id='Con_fac' class='Con_fac'></label>&nbsp;&nbsp;");

                    var append = ''; var total_tax_per = 0;

                    if (tax_filter.length > 0) {

                        for (var z = 0; z < tax_filter.length; z++) {
                            append += "<span style='color: red;padding: 5px;'><i class='fa-solid fa-percent'></i></span><label class='lbl_tax_type'>" + tax_filter[z].Tax_Type + "</label>:&nbsp;&nbsp;<label class='Tax_name' id='Tax_name'>" + tax_filter[z].Tax_name + "</label>&nbsp;&nbsp;";
                            var Push_data = tax_filter[z].Tax_Type;
                            total_tax_per = total_tax_per + parseFloat(tax_filter[z].Tax_Val);
                            NewOrd[idx][Push_data] = tax_filter[z].Tax_Val;

                            tax_arr.push({
                                pro_code: Orders[$j].Product_Code,
                                Tax_Code: tax_filter[z].Tax_Id,
                                Tax_Name: tax_filter[z].Tax_Type,
                                Tax_Amt: 0,
                                Tax_Per: tax_filter[z].Tax_Val,
                                umo_code: 0
                            });
                        }
                        NewOrd[idx].Tax_details = tax_arr;
                        NewOrd[idx].Total_Tax = total_tax_per;
                        $(itr).find('.second_row_div').append(append);
                    }
                    $(itr).find('.Con_fac').text(Orders[$j].con_fact);
                    if (parseInt(Prod[0].TotalStock) > 0) {
                        $(itr).find('.stockClass').css("color", "rgb(39 194 76)");
                    }
                    else {
                        $(itr).find('.stockClass').css("color", "red");
                    }
                    $(itr).find('.stockClass').text(Prod[0].TotalStock);
                    $(itr).find('.stockClass').val(Prod[0].TotalStock);
                    $(itr).find('.dis_per').text("0");
                    $(itr).find('.sale_code').html(Prod[0].Sale_Erp_Code);
                    $(itr).find('.tdAmt').html((qt * rt).toFixed(2));
                    $(itr).find('.erp_code').val(Prod[0].Sample_Erp_Code);
                    $(itr).find('.dis_val_class').text(0);
                    $(itr).find('.fre').attr("id", Orders[$j].Product_Code);
                    $(itr).find('.fre1').attr("id", Orders[$j].Product_Code);
                    $(itr).find('.tddis_val').text('0.00');


                    CQ = Orders[$j].qty;//*Orders[$j].con_fact;
                    getscheme(Prod[0].Sale_Erp_Code, Orders[$j].qty, Orders[$j].unit_name,  itr, '', $(itr).find('.fre1').text(), Orders[$j].Product_Code, idx, Orders[$j].Product_Code);
                    $(itr).find('.pcode').html(Orders[$j].Product_Code);
                    NewOrd[idx].Discount = $(itr).find('.dis_per').text() || 0;
                    itr = $(itr).closest('tr');
                    idx = $(itr).index();
                    rt = parseFloat($(itr).find('.tdRate').text()); if (isNaN(rt)) rt = 0; qt = parseFloat($(itr).find('.txtQty').val()); if (isNaN(qt)) qt = 0;
                    tax = parseFloat($(itr).find('.tdtax').text()); if (isNaN(tax)) tax = 0;

                    NewOrd[idx].Sub_Total = (qt * rt).toFixed(2);

                    if ($(itr).find('.dis_val_class').text() == "") { var dis_per = 0; } else {var dis_per=parseFloat($(itr).find('.dis_val_class').text())}
                    if (NewOrd[idx].Total_Tax == '' || isNaN(NewOrd[idx].Total_Tax)) { NewOrd[idx].Total_Tax = 0; }

                    NewOrd[idx].Dis_value = (parseFloat(dis_per) / 100 * parseFloat(NewOrd[idx].Sub_Total)).toFixed(2);
                    NewOrd[idx].Sub_Total = (parseFloat(NewOrd[idx].Sub_Total) - parseFloat(NewOrd[idx].Dis_value)).toFixed(2);

                    for (var g = 0; g < NewOrd[idx].Tax_details.length; g++) {
                        NewOrd[idx].Tax_details[g].Tax_Amt = parseFloat(NewOrd[idx].Tax_details[g].Tax_Per / 100 * parseFloat(NewOrd[idx].Sub_Total)).toFixed(2);
                        if (NewOrd[idx].Tax_details[g].Tax_Name.toLowerCase() == 'tcs') { $(itr).find('.tdtcs').html(NewOrd[idx].Tax_details[g].Tax_Amt); }
                    }

                    $(itr).find('.tdtotal').html(NewOrd[idx].Sub_Total);
                    $(itr).find('.disc_value').html(NewOrd[idx].Dis_value);
                    $(itr).find('.tddis_val').html(NewOrd[idx].Dis_value);


                    $(itr).find('.tdtax').html((parseFloat(NewOrd[idx].Total_Tax) / 100 * parseFloat(NewOrd[idx].Sub_Total)).toFixed(2));
                    NewOrd[idx].Gross_Amt = (parseFloat(NewOrd[idx].Total_Tax) / 100 * parseFloat(NewOrd[idx].Sub_Total) + parseFloat(NewOrd[idx].Sub_Total)).toFixed(2);
                    NewOrd[idx].Tax_value = (parseFloat(NewOrd[idx].Total_Tax) / 100 * parseFloat(NewOrd[idx].Sub_Total)).toFixed(2);
                    $(itr).find('.tdAmt').html(NewOrd[idx].Gross_Amt);
                    //ReCalc();

                    tv = 0;
                    $('.tdtotal').each(function () {
                        v = parseFloat($(this).text()); if (isNaN(v)) v = 0;
                        tv += v;
                    })
                    $('#sub_tot').val(tv.toFixed(2));

                    tac = 0;
                    $('.tdtax').each(function () {
                        k = parseFloat($(this).text()); if (isNaN(k)) k = 0;
                        tac += k;
                    })
                    $('#Tax_GST').val(tac.toFixed(2));


                    tcss = 0;
                    $('.tdtcs').each(function () {
                        k = parseFloat($(this).text()); if (isNaN(k)) k = 0;
                        tcss += k;
                    })
                    $('#tot_tcs').val(tcss.toFixed(2));

                    /*
                    cgst_tax = 0;
                    $('.tdcgst').each(function () {
                        k = parseFloat($(this).text()); if (isNaN(k)) k = 0;
                        cgst_tax += k;
                    })
                    $('#txt_cgst').val(cgst_tax.toFixed(2));
     
                    sgst_tax = 0;
                    $('.tdsgst').each(function () {
                        k = parseFloat($(this).text()); if (isNaN(k)) k = 0;
                        sgst_tax += k;
                    })
                    $('#txt_sgst').val(sgst_tax.toFixed(2));
                    */

                    //igst_tax = 0;
                    //$('.tdigst').each(function () {
                    //    k = parseFloat($(this).text()); if (isNaN(k)) k = 0;
                    //    igst_tax += k;
                    //})
                    //$('#txt_igst').val(igst_tax.toFixed(2));

                    dis = 0;
                    $('.tddis').each(function () {
                        z = parseFloat($(this).text()); if (isNaN(z)) z = 0;
                        dis += z;
                    })
                    $('#Txt_Dis_tot').val(dis.toFixed(2));

                    gross = 0;
                    $('.tdAmt').each(function () {
                        i = parseFloat($(this).text()); if (isNaN(i)) i = 0;
                        gross += i;
                    })
                    $('#gross').val(gross.toFixed(2));

                }


                resetSL(); //ReCalc();
                //resetSL();
            }
            function cal(x) {

                itr = $(x).closest('tr');
                idx = $(itr).index();
                rt = parseFloat($(itr).find('.tdRate').text()); if (isNaN(rt)) rt = 0; qt = parseFloat($(itr).find('.txtQty').val()); if (isNaN(qt)) qt = 0;
                tax = parseFloat($(itr).find('.tdtax').text()); if (isNaN(tax)) tax = 0;

                NewOrd[idx].Sub_Total = (qt * rt).toFixed(2);

                if ($(itr).find('.dis_val_class').text() == "") { var dis_per = 0; } else {var dis_per=parseFloat($(itr).find('.dis_val_class').text())}
                if (NewOrd[idx].Total_Tax == '' || isNaN(NewOrd[idx].Total_Tax)) { NewOrd[idx].Total_Tax = 0; }

                NewOrd[idx].Dis_value = (parseFloat(dis_per) / 100 * parseFloat(NewOrd[idx].Sub_Total)).toFixed(2);
                NewOrd[idx].Sub_Total = (parseFloat(NewOrd[idx].Sub_Total) - parseFloat(NewOrd[idx].Dis_value)).toFixed(2);

                for (var g = 0; g < NewOrd[idx].Tax_details.length; g++) {
                    NewOrd[idx].Tax_details[g].Tax_Amt = parseFloat(NewOrd[idx].Tax_details[g].Tax_Per / 100 * parseFloat(NewOrd[idx].Sub_Total)).toFixed(2);
                    if (NewOrd[idx].Tax_details[g].Tax_Name.toLowerCase() == 'tcs') { $(itr).find('.tdtcs').html(NewOrd[idx].Tax_details[g].Tax_Amt); }
                }

                $(itr).find('.tdtotal').html(NewOrd[idx].Sub_Total);
                $(itr).find('.disc_value').html(NewOrd[idx].Dis_value);
                $(itr).find('.tddis_val').html(NewOrd[idx].Dis_value);


                $(itr).find('.tdtax').html((parseFloat(NewOrd[idx].Total_Tax) / 100 * parseFloat(NewOrd[idx].Sub_Total)).toFixed(2));
                NewOrd[idx].Gross_Amt = (parseFloat(NewOrd[idx].Total_Tax) / 100 * parseFloat(NewOrd[idx].Sub_Total) + parseFloat(NewOrd[idx].Sub_Total)).toFixed(2);
                NewOrd[idx].Tax_value = (parseFloat(NewOrd[idx].Total_Tax) / 100 * parseFloat(NewOrd[idx].Sub_Total)).toFixed(2);
                $(itr).find('.tdAmt').html(NewOrd[idx].Gross_Amt);
                ReCal();
                //ReCalc();
            }

            function ReCal() {

                tv = 0;
                $('.tdtotal').each(function () {
                    v = parseFloat($(this).text()); if (isNaN(v)) v = 0;
                    tv += v;
                })
                $('#sub_tot').val(tv.toFixed(2));

                tac = 0;
                $('.tdtax').each(function () {
                    k = parseFloat($(this).text()); if (isNaN(k)) k = 0;
                    tac += k;
                })
                $('#Tax_GST').val(tac.toFixed(2));


                tcss = 0;
                $('.tdtcs').each(function () {
                    k = parseFloat($(this).text()); if (isNaN(k)) k = 0;
                    tcss += k;
                })
                $('#tot_tcs').val(tcss.toFixed(2));

                /*
                cgst_tax = 0;
                $('.tdcgst').each(function () {
                    k = parseFloat($(this).text()); if (isNaN(k)) k = 0;
                    cgst_tax += k;
                })
                $('#txt_cgst').val(cgst_tax.toFixed(2));
 
                sgst_tax = 0;
                $('.tdsgst').each(function () {
                    k = parseFloat($(this).text()); if (isNaN(k)) k = 0;
                    sgst_tax += k;
                })
                $('#txt_sgst').val(sgst_tax.toFixed(2));
                */

                //igst_tax = 0;
                //$('.tdigst').each(function () {
                //    k = parseFloat($(this).text()); if (isNaN(k)) k = 0;
                //    igst_tax += k;
                //})
                //$('#txt_igst').val(igst_tax.toFixed(2));

                dis = 0;
                $('.tddis').each(function () {
                    z = parseFloat($(this).text()); if (isNaN(z)) z = 0;
                    dis += z;
                })
                $('#Txt_Dis_tot').val(dis.toFixed(2));

                gross = 0;
                $('.tdAmt').each(function () {
                    i = parseFloat($(this).text()); if (isNaN(i)) i = 0;
                    gross += i;
                })
                $('#gross').val(gross.toFixed(2));

            }
            function getscheme(pCode, cq, un, tr, ff, disss, opcode) {

                var res = scheme.filter(function (a) {
                    return (a.Sale_Erp_Code == pCode && (Number(cq) >= Number(a.Scheme)) /*&& a.Scheme_Unit == un*/)
                });

                ans = [];

                $(tr).find('.dis_val_class').text(0);
                $(tr).find('.disc_value').text(0);

                for (var c = 0; c < arr.length; c++) {

                    if (arr[c].Product_Code.indexOf(opcode) >= 0) {

                        if (arr[c].Free > 0) {

                            arr[c].Free = arr[c].Free - ff;
                            $(tr).find('.fre').text('#' + opcode).text('0');
                            $(tr).find('.fre1').text('#' + opcode).text('0');
                            $(tr).find('.fre').attr('freecqty', arr[c].Free);
                            $(tr).find('.fre').attr('unit', '');
                            $(tr).find('.of_pro_name').val(0);
                            $(tr).find('.of_pro_code').val(0);
                            $(tr).find('.disc_value').text($(tr).find('.disc_value').text() - $(tr).find('.disc_value').text());
                        }
                    }
                }

                if (res.length > 0) {

                    schemedefinewithoutpackage(res[0], tr, ff, ans);
                }

                //$('#free_table').find('tbody tr').remove();
                //for (var r = 0; r < arr.length; r++) {

                //    if (arr[r].Free != '0') {
                //        var str = "<tr><td style='width: 14%;' class='td_id'>" + (r + 1) + "</td><td style='width:21%;'><input type='hidden' value=" + arr[r].Offer_Product_Code + " id='apc'/>" + arr[r].Offer_Product_Code + "</td><td style='left: 201px;width: 38%;' class='td_name'><input type='hidden' value=" + arr[r].Offer_Product_Name + " id='apn'/>" + arr[r].Offer_Product_Name + "</td><td style='left: 454px;' class='td_free'><input type='hidden' value=" + arr[r].Free + " id='fr'/>" + (arr[r].Free + ' ' + arr[r].Product_Free_Unit) + "</td></tr>";//arr[r].Offer_Product_Free_Unit
                //        $('#free_table tbody').append(str);
                //    }
                //}

                $('#free_table').find('tbody tr').remove();
                    //for (var r = 0; r < arr.length; r++) {

                    //    if (arr[r].Free != '0') {
                    //        var str = "<tr><td style='width: 14%;' class='td_id'>" + (r + 1) + "</td><td style='width:21%;'><input type='hidden' value=" + arr[r].Offer_Product_Code + " id='apc'/>" + arr[r].Offer_Product_Code + "</td><td style='left: 201px;width: 38%;' class='td_name'><input type='hidden' value=" + arr[r].Offer_Product_Name + " id='apn'/>" + arr[r].Offer_Product_Name + "</td><td style='left: 454px;' class='td_free'><input type='hidden' value=" + arr[r].Free + " id='fr'/>" + (arr[r].Free + ' ' + arr[r].Offer_Product_Free_Unit) + "</td></tr>";//arr[r].Offer_Product_Free_Unit
                    //        $('#free_table tbody').append(str);
                    //    }
                    //}
                    var freecqtySum = {};
                    var existingData = [];

                    // Get the table element
                    var table = document.getElementById("OrderEntry");

                    // Get the table body rows
                    var rows = table.getElementsByTagName("tbody")[0].getElementsByTagName("tr");

                    // Loop through each row
                    for (var i = 0; i < rows.length; i++) {
                        var row = rows[i];

                        // Get the freecqty, ofer_product_na, ofer_product_unit, and ofer_product_code values
                        var freecqtyAttr = row.querySelector(".fre").getAttribute("freecqty");
                        var freecqty = freecqtyAttr ? parseInt(freecqtyAttr) : parseInt(row.querySelector(".fre").getAttribute("value"));
                        var oferProductNa = row.querySelector(".of_pro_name").value;
                        var oferProductUnit = row.querySelector(".fre").getAttribute("unit");
                        var oferProductCode = row.querySelector(".of_pro_code").value;

                        // Check if the data already exists in the array
                        var existingIndex = existingData.findIndex(function (data) {
                            return data.product_code == oferProductCode;
                        });

                        if (existingIndex !== -1) {
                            // Data already exists, update the freecqty
                            existingData[existingIndex].freecqty += freecqty;
                        } else {
                            // Data doesn't exist, push a new object to the array
                            existingData.push({
                                product_code: oferProductCode,
                                product_name: oferProductNa,
                                unit: oferProductUnit,
                                freecqty: freecqty
                            });
                        }
                    }

                    // Append the data from the array to the table
                    existingData.forEach(function (data, index) {
                        var str = "<tr><td style='width: 14%;' class='td_id'>" + (index + 1) + "</td><td style='width:21%;'><input type='hidden' value=" + data.product_code + " id='apc'/>" + data.product_code + "</td><td style='left: 201px;width: 38%;' class='td_name'><input type='hidden' value=" + data.product_name + " id='apn'/>" + data.product_name + "</td><td style='left: 454px;' class='td_free'><input type='hidden' value=" + data.freecqty + " id='fr'/>" + (data.freecqty + ' ' + data.unit) + "</td></tr>";
                        $('#free_table tbody').append(str);
                    });
            }
            function schemedefinewithoutpackage(res, tr, ff) {

               // if (res.Discount == '0') {

                    if (res.Package != "Y") {

                        if (CQ > 0) {

                            var free = parseInt(parseFloat((CQ / parseInt(res.Scheme))) * parseInt(res.Free))

                            var x = arr.filter(function (b) {
                                return (b.Offer_Product_Code == res.Offer_Product && b.Offer_Product_Free_Unit == res.Free_Unit)
                            })

                            if (res.Against == "N") {

                                if (x.length > 0) {

                                    idx = arr.indexOf(x[0]);
                                    arr[idx].Free += free;
                                    arr[idx].Product_Free_Unit = res.Scheme_Unit;
                                    arr[idx].Product_Code = (arr[idx].Product_Code.indexOf(opcode) >= 0) ? arr[idx].Product_Code : arr[idx].Product_Code + ',' + opcode;
                                    $(tr).find('.fre').text(free + ' ' + (res.Free_Unit));
                                    $(tr).find('.fre').attr('unit', res.Free_Unit);
                                    $(tr).find('.fre').attr('freepro', res.Product_Code);
                                    $(tr).find('.fre').attr('cqty', CQ);
                                    $(tr).find('.fre').attr('freecqty', free);

                                    $(tr).find('.fre1').text(free);

                                    if (free != "") {
                                        tr.find('.of_pro_name').val(pname);
                                        tr.find('.of_pro_code').val(res.Product_Code);
                                    }
                                }
                                else {

                                    $(tr).find('.fre').text(free + ' ' + ((res.Free_Unit)));
                                    $(tr).find('.fre').attr('unit', res.Free_Unit);
                                    $(tr).find('.fre').attr('freepro', res.Product_Code);
                                    $(tr).find('.fre').attr('cqty', CQ);
                                    $(tr).find('.fre').attr('freecqty', free);

                                    $(tr).find('.fre1').text(free);

                                    if (free != "") {
                                        tr.find('.of_pro_name').val(res.Offer_Product_Name);
                                        tr.find('.of_pro_code').val(res.Offer_Product);
                                    }
                                    arr.push({

                                        Product_Code: res.Product_Code,
                                        Product_Name: res.Product_Name,
                                        Offer_Product_Code: res.Offer_Product,
                                        Offer_Product_Name: res.Offer_Product_Name,
                                        Free: free,
                                        Product_Free_Unit: res.Scheme_Unit,
                                        Offer_Product_Free_Unit: res.Free_Unit

                                    })
                                }
                            }
                            else {

                                if (parseInt(CQ) >= parseInt(res.Scheme)) {

                                    if (x.length > 0) {
                                        idx = arr.indexOf(x[0]);
                                        arr[idx].Free += free;
                                        arr[idx].Product_Free_Unit = res.Scheme_Unit;
                                        arr[idx].Product_Code = (arr[idx].Product_Code.indexOf(opcode) >= 0) ? arr[idx].Product_Code : arr[idx].Product_Code + ',' + opcode;
                                        $(tr).find('.fre').text(free + ' ' + (res.Free_Unit));
                                        $(tr).find('.fre').attr('unit', res.Free_Unit);
                                        $(tr).find('.fre').attr('freepro', res.Offer_Product);
                                        $(tr).find('.fre').attr('cqty', CQ);
                                        $(tr).find('.fre').attr('freecqty', free);
                                        $(tr).find('.fre1').text(free);
                                        tr.find('.of_pro_name').val(res.Offer_Product_Name);
                                        tr.find('.of_pro_code').val(res.Offer_Product);
                                    }
                                    else {
                                        $(tr).find('.fre').text(free + ' ' + ((res.Free_Unit)));
                                        $(tr).find('.fre').attr('unit', res.Free_Unit);
                                        $(tr).find('.fre').attr('freepro', res.Offer_Product);
                                        $(tr).find('.fre').attr('cqty', CQ);
                                        $(tr).find('.fre').attr('freecqty', free);
                                        $(tr).find('.fre1').text(free);

                                        tr.find('.of_pro_name').val(res.Offer_Product_Name);
                                        tr.find('.of_pro_code').val(res.Offer_Product);
                                        arr.push({

                                            Product_Code: res.Product_Code,
                                            Product_Name: res.Product_Name,
                                            Offer_Product_Code: res.Offer_Product,
                                            Offer_Product_Name: res.Offer_Product_Name,
                                            Free: free,
                                            Product_Free_Unit: res.Scheme_Unit,
                                            Offer_Product_Free_Unit: res.Free_Unit
                                        })
                                    }
                                }
                            }
                        }
                    }
                    else {

                        var TotalQuantity = CQ / res.Scheme;
                        var free = parseInt(TotalQuantity) * res.Free;

                        var x = arr.filter(function (b) {
                            return (b.Offer_Product_Code == res.Offer_Product && b.Offer_Product_Free_Unit == res.Free_Unit)
                        })

                        if (res.Against == "N") {

                            if (x.length > 0) {
                                idx = arr.indexOf(x[0]);
                                arr[idx].Free += free;
                                arr[idx].Product_Free_Unit = res.Scheme_Unit;
                                arr[idx].Product_Code = (arr[idx].Product_Code.indexOf(opcode) >= 0) ? arr[idx].Product_Code : arr[idx].Product_Code + ',' + opcode;
                                $(tr).find('.fre').text(free + ' ' + (res.Free_Unit));
                                $(tr).find('.fre').attr('unit', res.Free_Unit);
                                $(tr).find('.fre').attr('freepro', res.Product_Code);
                                $(tr).find('.fre').attr('cqty', CQ);
                                $(tr).find('.fre').attr('freecqty', free);

                                $(tr).find('.fre1').text(free);
                                if (free != "") {
                                    tr.find('.of_pro_name').val(res.Offer_Product_Name);
                                    tr.find('.of_pro_code').val(res.Offer_Product);
                                }
                            }
                            else {
                                $(tr).find('.fre').text(free + ' ' + ((res.Free_Unit)));
                                $(tr).find('.fre').attr('unit', res.Free_Unit);
                                $(tr).find('.fre').attr('freepro', res.Product_Code);
                                $(tr).find('.fre').attr('cqty', CQ);
                                $(tr).find('.fre').attr('freecqty', free);
                                $(tr).find('.fre1').text(free);

                                if (free != "") {
                                    tr.find('.of_pro_name').val(res.Offer_Product_Name);
                                    tr.find('.of_pro_code').val(res.Offer_Product);
                                }

                                arr.push({

                                    Product_Code: res.Product_Code,
                                    Product_Name: res.Product_Name,
                                    Offer_Product_Code: res.Offer_Product,
                                    Offer_Product_Name: res.Offer_Product_Name,
                                    Free: free,
                                    Product_Free_Unit: res.Scheme_Unit,
                                    Offer_Product_Free_Unit: res.Free_Unit

                                })
                            }
                        }
                        else {
                            if (x.length > 0) {

                                idx = arr.indexOf(x[0]);
                                arr[idx].Free += free;
                                arr[idx].Product_Code = (arr[idx].Product_Code.indexOf(opcode) >= 0) ? arr[idx].Product_Code : arr[idx].Product_Code + ',' + opcode;
                                $(tr).find('.fre').text(free + ' ' + (res.Free_Unit));
                                $(tr).find('.fre').attr('unit', res.Free_Unit);
                                $(tr).find('.fre').attr('freepro', res.Offer_Product);
                                $(tr).find('.fre').attr('cqty', CQ);
                                $(tr).find('.fre').attr('freecqty', free);

                                $(tr).find('.fre1').text(free);
                                tr.find('.of_pro_name').val(res.Offer_Product_Name);
                                tr.find('.of_pro_code').val(res.Offer_Product);
                            }
                            else {
                                $(tr).find('.fre').text(free + ' ' + ((res.Free_Unit)));
                                $(tr).find('.fre').attr('unit', res.Free_Unit);
                                $(tr).find('.fre').attr('freepro', res.Offer_Product);
                                $(tr).find('.fre').attr('cqty', CQ);
                                $(tr).find('.fre').attr('freecqty', free);
                                $(tr).find('.fre1').text(free);

                                tr.find('.of_pro_name').val(res.Offer_Product_Name);
                                tr.find('.of_pro_code').val(res.Offer_Product);

                                arr.push({

                                    Product_Code: res.Product_Code,
                                    Product_Name: res.Product_Name,
                                    Offer_Product_Code: res.Offer_Product,
                                    Offer_Product_Name: res.Offer_Product_Name,
                                    Free: free,
                                    Product_Free_Unit: res.Scheme_Unit,
                                    Offer_Product_Free_Unit: res.Free_Unit

                                })
                            }
                        }
                    }
               // }

                $(tr).find('.dis_val_class').text(res.Discount);
                var d = $(tr).find('.tdRate').text() * $(tr).find('.txtQty ').val();
                var discalc = parseInt(res.Discount) / 100;
                var distotal = d * discalc;
                var finaltotal = d - distotal;

                if (distotal != '0') {
                    $(tr).find('.tdtotal').text(finaltotal.toFixed(2));
                    $(tr).find('.tddis_val').text(distotal - finaltotal.toFixed(2));
                    $(tr).find('.tdtotal').text(distotal.toFixed(2));
                }

                if (finaltotal != "0") {
                    var after_cal = $(tr).find('.tax_val').val() / 100 * $(tr).find('.tdtotal').text()
                    var fin = after_cal + parseFloat($(tr).find('.tdtotal').text());
                    $(tr).find('.tdtax').text(after_cal.toFixed(2));
                    $(tr).find('.tdAmt').text(fin.toFixed(2));
                }
            }
        </script>

        <form id="frm1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <div class="spinnner_div" style="display: none;">
                <div class="spinner" style="position: absolute; left: 525px; top: 133px;">
                    <div class="rect1" style="background: #1a60d3;"></div>
                    <div class="rect2" style="background: #DB4437;"></div>
                    <div class="rect3" style="background: #F4B400;"></div>
                    <div class="rect4" style="background: #0F9D58;"></div>
                    <div class="rect5" style="background: orangered;"></div>
                </div>
            </div>
            <div style="padding: 8px; margin-top: 0;" class="card">
                <div class="row">
                    <div class="col-md-4">
                        <label class="control-label">Company Name</label>
                        <select style="width: 265px; margin-left: 13px;" class="form-group" id="To_Address">
                        </select>
                        <label style="margin-left: 12px; font-weight: 100; padding-top: 20px;" id="lbl_To_Address" class="control-label"></label>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">Billing Address</label>
                        <div style="font-weight: 100;" id="lbl_Bill_Address" class="control-label"></div>
                    </div>
                    <div class="col-md-1">
                        <div style="width: 300px;">
                            <label style="width: 131px;" class="control-label">Shipping Address</label>&nbsp;&nbsp;
                        <input type="checkbox" id="chck" />Same As Billing Address
                        </div>
                        <textarea style="width: 330px; background-color: aliceblue" rows="4" class="form-control txtblue" id="txt_Ship_add"></textarea>
                    </div>
                </div>
                <div class="row" style="margin-top: 22px; padding-right: 4px;">
                    <div class="col-sm-6">
                        <label>Purchase Date</label>
                        <input type="date" style="width: 270px; margin-left: 20px;" disabled class="control-group txtblue" id="txt_Purchase_Date" />
                    </div>
                    <div class="col-sm-6" style="padding: 0px 0px 0px 143px;">
                        <label>Expected Date</label>

                        <input type="date" style="width: 270px; margin-left: 20px;" disabled class="control-group txtblue" id="txt_Expected_Date" />
                    </div>
                </div>

            </div>


            <div class="row" style="text-align: center;">
                <div id="myDIV">
                </div>
            </div>
            <br />
            <div class="row card " style="background: #fff; margin: 0;">
                <div class="col-sm-12" style="padding: 15px">
                    <div class="tableBodyScroll">
                        <table class="table table-hover" id="OrderEntry">
                            <thead class="text-warning">
                                <tr>
                                    <th style="text-align: left">S.No</th>
                                    <th style="text-align: left">Product</th>
                                    <th style="text-align: left; word-spacing: 41px; padding: 0px 0px 8px 50px;">Unit Qty</th>
                                    <th style="text-align: right">Rate</th>
                                    <th style="text-align: right">Free</th>
                                    <th style="text-align: right">Dis Val</th>
                                    <th style="text-align: right">Total</th>
                                    <th style="text-align: right">Tax</th>
                                    <th style="text-align: right">Gross_Total</th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </div>
                </div>

            </div>
            <br />
            <div class="row" style="margin-left: 0%;">
                <div>
                    <div style="text-align: left" colspan="9">
                        <button type="button" class="btn btn-success" id="btnAdd" style="font-size: 12px">+ Add Product </button>
                        <button type="button" class="btn btn-danger" id="btnDelRow" style="font-size: 12px">- Remove Product</button>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-sm-8">
                    <div class="card freecard">
                        <div class="card-body table-responsive">
                            <div style="white-space: nowrap; padding: 0px 0px 7px 7px; font-weight: 900;">View Offer Products</div>
                            <div class="tddd">
                                <table id="free_table" class="table table-hover">
                                    <thead class="text-warning">
                                        <tr>
                                            <th style="width: 14%;">SlNo</th>
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
                </div>
                <div class="col-sm-offset-8 form-horizontal">
                    <label class=" col-xs-2 control-label" style="font-size: 12px; padding: 0px">
                        Subtotal :
                    </label>
                    <div class="col-sm-9">
                        <div class="input-group">
                            <div class="input-group-addon currency" style="background: #00bcd4; color: white; border-color: #00bcd4;">
                                <i class="fa fa-inr"></i>
                            </div>
                            <input data-cell="G1" id="sub_tot" data-format="0,0.00" class="form-control txtblue" readonly />
                        </div>
                    </div>
                </div>

                <div class="col-sm-offset-8 form-horizontal" style="margin-top: 40px;">
                    <label class=" col-xs-2 control-label" style="font-size: 12px; padding: 0px">
                        Dis Total :
                    </label>
                    <div class="col-sm-9">
                        <div class="input-group">
                            <div class="input-group-addon currency" style="background: #00bcd4; color: white; border-color: #00bcd4;">
                                <i class="fa fa-inr"></i>
                            </div>
                            <input data-cell="G1" id="Txt_Dis_tot" data-format="0,0.00" class="form-control txtblue" readonly />
                        </div>
                    </div>
                </div>

                <div class="col-sm-offset-8 form-horizontal" style="margin-top: 80px;">
                    <label class=" col-xs-2 control-label" style="font-size: 12px; padding: 0px">
                        Tax Total :
                    </label>
                    <div class="col-sm-9">
                        <div class="input-group">
                            <div class="input-group-addon currency" style="background: #00bcd4; color: white; border-color: #00bcd4;">
                                <i class="fa fa-inr"></i>
                            </div>
                            <input data-cell="G1" id="Tax_GST" data-format="0,0.00" class="form-control txtblue" readonly />
                        </div>
                    </div>
                </div>

                <div class="col-sm-offset-8 form-horizontal" style="margin-top: 120px;">
                    <label class=" col-xs-2 control-label" style="font-size: 12px; padding: 0px">
                        TCS Total :
                    </label>
                    <div class="col-sm-9">
                        <div class="input-group">
                            <div class="input-group-addon currency" style="background: #00bcd4; color: white; border-color: #00bcd4;">
                                <i class="fa fa-inr"></i>
                            </div>
                            <input data-cell="G1" id="tot_tcs" data-format="0,0.00" class="form-control txtblue" readonly />
                        </div>
                    </div>
                </div>

                <div class="col-sm-offset-8 form-horizontal div_cgst" style="margin-top: 80px; display: none;">
                    <label class=" col-xs-2 control-label" style="font-size: 12px; padding: 0px">
                        CGST Total :
                    </label>
                    <div class="col-sm-9">
                        <div class="input-group">
                            <div class="input-group-addon currency" style="background: #00bcd4; color: white; border-color: #00bcd4;">
                                <i class="fa fa-inr"></i>
                            </div>
                            <%--    <input data-cell="G1" id="Tax_GST" data-format="0,0.00" class="form-control" readonly />--%>
                            <input data-cell="G1" id="txt_cgst" data-format="0,0.00" class="form-control txtblue" readonly />
                        </div>
                    </div>
                </div>

                <div class="col-sm-offset-8 form-horizontal div_sgst" style="margin-top: 123px; display: none;">
                    <label class=" col-xs-2 control-label" style="font-size: 12px; padding: 0px">
                        SGST Total :
                    </label>
                    <div class="col-sm-9">
                        <div class="input-group">
                            <div class="input-group-addon currency" style="background: #00bcd4; color: white; border-color: #00bcd4;">
                                <i class="fa fa-inr"></i>
                            </div>
                            <input data-cell="G1" id="txt_sgst" data-format="0,0.00" class="form-control txtblue" readonly />
                        </div>
                    </div>
                </div>

                <div class="col-sm-offset-8 form-horizontal div_gross" style="margin-top: 160px;">
                    <label class=" col-xs-2 control-label" style="font-size: 12px; padding: 0px">
                        Gross Total :
                    </label>
                    <div class="col-sm-9">
                        <div class="input-group">
                            <div class="input-group-addon currency" style="background: #00bcd4; color: white; border-color: #00bcd4;">
                                <i class="fa fa-inr"></i>
                            </div>
                            <input data-cell="G1" id="gross" data-format="0,0.00" class="form-control txtblue" readonly />
                        </div>
                    </div>
                </div>
                <div style="left: 0; position: absolute; padding: 61px 0px 0px 1009px;">
                    <input type="button" id="svorders" class="btn btn-lg btn-primary ews" value="Save" style="float: right; width: 160%;">
                </div>
            </div>

        </form>
    </div>
</asp:Content>

