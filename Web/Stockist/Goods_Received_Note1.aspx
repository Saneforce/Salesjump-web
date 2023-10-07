<%@ Page Title="Goods Received Note" Language="C#" MasterPageFile="~/Master_DIS.master"
    AutoEventWireup="true" CodeFile="Goods_Received_Note1.aspx.cs" Inherits="MasterFiles_Goods_Received_Note" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!doctype html>
    <html xmlns="https://www.w3.org/1999/xhtml">
    <head>

        <meta charset="utf-8" content="width=device-width, initial-scale=1" runat="server" />
        <meta name="viewport" content="width=device-width, initial-scale=1" />
        <link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" />

        <script type="text/javascript" src='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js'></script>
        <link rel="stylesheet" href='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css'
            media="screen" />
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/css/bootstrap-datepicker.css" type="text/css" />
		 <%--<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-confirm/3.3.2/jquery-confirm.min.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-confirm/3.3.2/jquery-confirm.min.js"></script>--%>
        <link href="../alertstyle/jquery-confirm.min.css" rel="stylesheet" />
        <script src="../alertstyle/jquery-confirm.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/js/bootstrap-datepicker.js" type="text/javascript"></script>


        <style type="text/css">
            body {
                overflow-x: initial !important;
            }

            input[type='text'], select {
                line-height: 22px;
                padding: 4px 6px;
				border:1.5px solid #19a4c6a3!important;
                /*font-size: medium;*/
                border-radius: 7px;
                /*width: 100px;*/
            }

            label {
                font-weight: 500;
            }

            #ui-datepicker-div {
                z-index: 9999999 !important;
            }

            .auto-style1 {
                left: 0px;
                top: 0px;
            }

            /* .modules-table tr > *:nth-child(6) {
                padding-right: 80px;
            }
			*/

            /*.tableBodyScroll tbody {
                display: block;
                max-height: 218px;
                overflow-y: scroll;
            }

            .tableBodyScroll thead,
            tbody tr {
                display: table;
                width: 100%;
            }


            .tableBodyScroll1 tbody {
                display: block;
                max-height: 100px;
                overflow-y: scroll;
            }

            .tableBodyScroll1 thead,
            tbody tr {
                display: table;
                width: 100%;
            }

            table, thead, tbody {
                font-size: 13.5px;
            }*/
             
			 
            .table > thead > tr > th {
                vertical-align: bottom;
                border-bottom: 2px solid #19a4c6a3;
            }
            .txtblue{
                border:1.5px solid #19a4c6a3!important;
                /*border: 1px solid #19a4c6!important;*/
            }
            .chosen-container-single .chosen-single {
                border: 1.5px solid #19a4c6a3!important;
                height: 32px;
            }
            .card{
                border: 1.5px solid #b2ebf9b5!important;
            }
            .table-scroll {
                position: relative;
                width: 101.5%;
                z-index: 1;
                margin: auto;
                overflow: auto;
                /* height:250px; */
                max-height: 383px;
            }

                .table-scroll table {
                    width: 100%;
                    /*min-width: 1280px;*/
                    margin: auto;
                    border-collapse: separate;
                    border-spacing: 0;
                }

            .table-wrap {
                position: relative;
            }

            .table-scroll th,
            .table-scroll td {
                padding: 5px 10px;
                /*border: 1px solid #000;
            background: #fff;*/
                vertical-align: top;
            }

            .table-scroll thead th {
                background: white;
                color: black;
                position: -webkit-sticky;
                position: sticky;
                top: 0;
            }
            /* safari and ios need the tfoot itself to be position:sticky also */
            .table-scroll tfoot,
            .table-scroll tfoot th,
            .table-scroll tfoot td {
                position: -webkit-sticky;
                position: sticky;
                bottom: 0;
                /*background: #666;
            color: #fff;*/
                background: white;
                color: black;
                z-index: 4;
            }


            /* testing links*/

            th:first-child {
                position: -webkit-sticky;
                position: sticky;
                left: 0;
                z-index: 2;
                /*background: #ccc;*/
            }

            thead th:first-child,
            tfoot th:first-child {
                z-index: 5;
            }
        </style>

        <script type="text/javascript">

            searchKeys = "Product_Name";
            var discc = 0;
            var today = "";
            var pcode = '';
            var CQ = '';
            var un = '';
            var tr = '';
            var ff = '';
            var disss = '';
            var opcode = '';
            var scheme = [];
            var arr = [];
            var pname = '';

            $(document).ready(function () {

                var stockist_Code = ("<%=Session["Sf_Code"].ToString()%>");
                var stockist_Name = ("<%=Session["sf_name"].ToString()%>");
                var Div_Code = ("<%=Session["div_code"].ToString()%>");
                var tot_Net = 0;
                var now = new Date();
                var day = ("0" + now.getDate()).slice(-2);
                var month = ("0" + (now.getMonth() + 1)).slice(-2);
                var today = now.getFullYear() + "-" + (month) + "-" + (day);
                $('#txtEdate').val(today);
                $('.datetimepicker').attr("max", today);


                $(document).on('keypress', '#remarks', function (e) {
                    if (e.keyCode == 34 || e.keyCode == 39 || e.keyCode == 38 || e.keyCode == 60 || e.keyCode == 62 || e.keyCode == 92) return false;
                });


                var arrTAX = [];
                var StkDetail = [];
                var res = [];
                var result = [];
                var output = [];
                var pendord = [];
                var headdatas = [];
                $.ajax({
                    type: "Post",
                    contentType: "application/json; charset=utf-8",
                    url: "Goods_Received_Note1.aspx/getscheme",
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

                $(document).on('keypress', 'input[name=txtGood]', function (e) {
                    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                        return false;
                    }
                });


                if ($('#<%=hdnmode.ClientID %>').val() == "1") {
                    $('#lblgrnno').css('display', 'inline-block');
                    $('#grnNo').css('display', 'inline-block');
                    $('#grnNo').attr('disabled', true);
                    $('#grnDate').attr('disabled', true);
                    $('#PonoSelect').attr('disabled', true);
                    $('#grnDisDate').attr('disabled', true);
                    $('#ddlsupplier').attr('disabled', true);
                    $('#ddldistributor').attr('disabled', true);

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Goods_Received_Note1.aspx/Get_AllValues",
                        data: "{'grnNo':'" + $('#<%=hdngrn_no.ClientID %>').val() + "','grnDate':'" + $('#<%=hdngrn_date.ClientID %>').val() + "','grnSuppcode':'" + $('#<%=hdnsupp_code.ClientID %>').val() + "'}",
                        dataType: "json",
                        success: function (data) {
                            var obj = JSON.parse(data.d);
                            if (obj.TransH.length > 0) {

                                $('#grnNo').val(obj.TransH[0].GRN_No);

                                var dt = (obj.TransH[0].GRN_Date).split('/');
                                var dt1 = dt[2] + '-' + dt[1] + '-' + dt[0];
                                document.getElementById("grnDate").value = dt1;

                                var dtt = (obj.TransH[0].Dispatch_Date).split('/');
                                var dtt1 = dt[2] + '-' + dt[1] + '-' + dt[0];

                                document.getElementById("grnDisDate").value = dtt1;
                                $('#ddlsupplier').append('<option selected="selected" text="' + obj.TransH[0].Supp_Name + '" value="' + obj.TransH[0].Supp_Code + '">' + obj.TransH[0].Supp_Name + '</option>');
                                $('#txtEdate').val(obj.TransH[0].Entry_Date);
                                $('#grnPono').val(obj.TransH[0].Po_No);
                                $('#PonoSelect').append('<option value="' + obj.TransH[0].Po_No + '">' + obj.TransH[0].Po_No + '</option>');
                                $('#challenNo').val(obj.TransH[0].Challan_No);
                                $('#ddldistributor').val(obj.TransH[0].Received_Location);
                                $('#receivedby').val(obj.TransH[0].Received_By);
                                $('#authorised').val(obj.TransH[0].Authorized_By);
                                $('#remarks').val(obj.TransH[0].remarks);
                            }

                            $(tbl1).find('tbody tr').remove();

                            for (var i = 0; i < obj.TransD.length; i++) {

                                var tbl1 = $('#ProductTable');
                                var d = (obj.TransD[i].mfgDate).split('/');
                                var d1 = dt[2] + '-' + dt[1] + '-' + dt[0];
                                str = "<td>" + (i + 1) + "</td><td style='min-width: 115px;'>" + obj.TransD[i].PCode + "</td><td style='min-width: 300px;'>" + obj.TransD[i].PDetails + "</td><td><input type='hidden' name='Erp_Code' value='" + obj.TransD[i].Erp_Code + "'/><input type='hidden' name='UOM_Code' value='" + obj.TransD[i].UOM + "'/>" + obj.TransD[i].UOM_Name + "</td><td><input type='hidden' name='stkval'/><input type='text' value=" + obj.TransD[i].Batch_No + " name='txtBatch' autocomplete='off' maxlength='10' style='min-width: 115px;' /></td><td><input type='date' value=" + d1 + " name='txtDate' style='min-width: 115px;'/></td><td><input type='text' name='txtGood' value=" + obj.TransD[i].Good + " maxlength='7' class='textval' /></td><td><input type='hidden' class='hidcqty' value='" + obj.TransD[i].POQTY + "'/><input type='text' name='txtPoqty' readonly value='" + obj.TransD[i].POQTY + "'  maxlength='7' class='numberOnly'/></td><td><input type='text' name='txtPrice' maxlength='10' class='textval' value=" + obj.TransD[i].Price + "  /></td>";
                                str += "<td style='display:none'><input type='text' value=" + obj.TransD[i].Damaged + " name='txtDamaged' /><td><input type='text' style='width:250px;' id='rema' value='" + obj.TransD[i].Remarks + "'></td></td><td style='min-width: 150px;'><label name='grossVal'>0.00</label></td><td></td><td style='min-width: 150px;'><label name='netVal'>0.00</label></td>";
                                $('#ProductTable >tbody').append('<tr>' + str + ' </tr>')

                            }
                        },
                        error: function (result) {
                            alert(JSON.stringify(result));
                        }
                    });
                }
                else {
                    orderdetails();
                }
				function rjalertfn(titles, contents, types, func) {
                    if (types == 'error') {
                        var tp = 'red';
                        var icons = 'fa fa-warning';
                        var btn = 'btn-red';
                    }
                    else {
                        var tp = 'green';
                        var icons = 'fa fa-check fa-2x';
                        var btn = 'btn-green';
                    }
                    $.confirm({
                        title: '' + titles + '',
                        content: '' + contents + '',
                        type: '' + tp + '',
                        typeAnimated: true,
                        autoClose: 'action|8000',
                        icon: '' + icons + '',
                        buttons: {
                            tryAgain: {
                                text: 'OK',
                                btnClass: '' + btn + '',
                                action: function () {
                                    func();
                                }
                            }
                        }
                    });
                }
                function rjalert(titles, contents, types) {
                    if (types == 'error') {
                        var tp = 'red';
                        var icons = 'fa fa-warning';
                        var btn = 'btn-red';
                    }
                    else {
                        var tp = 'green';
                        var icons = 'fa fa-check fa-2x';
                        var btn = 'btn-green';
                    }
                    $.confirm({
                        title: '' + titles + '',
                        content: '' + contents + '',
                        type: '' + tp + '',
                        typeAnimated: true,
                        autoClose: 'action|8000',
                        icon: '' + icons + '',
                        buttons: {
                            tryAgain: {
                                text: 'OK',
                                btnClass: '' + btn + '',
                                action: function () {

                                }
                            }
                        }
                    });
                }

                function orderdetails() {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Goods_Received_Note1.aspx/GetOrderNo",
                        dataType: "json",
                        success: function (data) {
						var pononumber = JSON.parse(data.d) || [];
						for (var i = 0; pononumber.length > i; i++) {
						
						if(pononumber[i].Order_Flag=='0')
						  var sp="<span class='sp' id="+pononumber[i].Order_Flag+" style='color:red'> ( Pending )</span>";
						else if(pononumber[i].Order_Flag=='1')
						  var sp="<span class='sp' id="+pononumber[i].Order_Flag+" style='color:green'> ( Invoiced )</span>";
						else if(pononumber[i].Order_Flag=='2')
						  var sp="<span class='sp' id="+pononumber[i].Order_Flag+" style='color:orange'> ( Cancelled )</span>";
						else
						  var sp="<span class='sp' id="+pononumber[i].Order_Flag+" style='color:red'>( Pending )</span>";
						 
						 $('#PonoSelect').append($("<option></option>").val(pononumber[i].Trans_Sl_No).html(pononumber[i].Trans_Sl_No +' '+ sp));
						$('#PonoSelect').append(sp);
						$('#PonoSelect').trigger('chosen:updated').css("width", "100%");
                                    }
                           
                        },
                        error: function (result) {
                            alert(JSON.stringify(result));
                        }
                    });
                    $('#PonoSelect').chosen();
                }

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Goods_Received_Note1.aspx/GetSuppilerByOrder",
                    dataType: "json",
                    success: function (data) {
                        supp_array = JSON.parse(data.d);
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
                $(document).on('change', '#grnDate', function () {
                    var pono = $("#PonoSelect").val() || "";
                    if (pono == "" || pono == "0") {
						rjalert('Error!', 'choose PO NO First!..', 'error');
                        //alert('choose PO NO First!.');
						$('#grnDate,#grnDisDate').val('');
                        return false;
                    }
                    var now = new Date($('#grnDate').val());
                    var day = ("0" + now.getDate()).slice(-2);
                    var month = ("0" + (now.getMonth() + 1)).slice(-2);
                    var today = now.getFullYear() + "-" + (month) + "-" + (day);
                    $('#grnDisDate').attr("max", today);
                    $('#grnDisDate').val('');
                });
                $(document).on('change', '#grnDisDate', function () {
                    var pono = $("#PonoSelect").val() || "";
                    if (pono == "" || pono == "0") {
                        rjalert('Error!', 'Choose PO NO First!..', 'error');
                        //alert('choose PO NO First!.');
                        $('#grnDisDate').val('');
                        return false;
                    }
                    else if ($('#grnDate').val() == '') {
                        rjalert('Error!', 'Choose GRN Date First!..', 'error');
                        $('#grnDisDate').val('');
                        return false;
                    }
                    else if ($('#grnDisDate').val() > $('#grnDate').val()) {
                        rjalert('Error!', 'Dispatch date is not greater than GRN date ', 'error');
                        $('#grnDisDate').val('');
                        return false;
                    }
                });
                $(document).on("change", "#PonoSelect", function () {
                    $('#grnDate,#grnDisDate,#challenNo').val('');
		    var selectedvalue = $(this).children("option:selected").val();
			 var selectedOption = $(this).find("option:selected");
            var spanOption = selectedOption.find(".sp").attr("id");
			if ( spanOption==0){
			  var firstOption = $("#PonoSelect option:first-child");
            firstOption.prop("selected", true);
            $("#PonoSelect").trigger("chosen:updated");
			  rjalert('Error!', 'Updating the GRN is only possible after processing the Superstockist invoice', 'error');
			return false;}
			if ( spanOption==2){
			  var firstOption = $("#PonoSelect option:first-child");
            firstOption.prop("selected", true);
            $("#PonoSelect").trigger("chosen:updated");
			  rjalert('Error!', 'Superstockist Cancelled Order', 'error');
			return false;}
                    pendord = [];
                    headdatas = [];
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        data: "{'Orderid':'" + selectedvalue + "'}",
                        url: "Goods_Received_Note1.aspx/GetProduct",
                        dataType: "json",
                        success: function (data) {
                            output = JSON.parse(data.d);
                            result = output; var now = new Date(result[0].Order_Date);
                            var day = ("0" + now.getDate()).slice(-2);
                            var month = ("0" + (now.getMonth() + 1)).slice(-2);
                            var today = now.getFullYear() + "-" + (month) + "-" + (day);
                            $('.datetimepicker').attr("min", today);
                            loadtable(result);
                        },
                        error: function (result) {
                            alert(JSON.stringify(result));
                        }
                    });

                    $("#ddlsupplier").html('');
                    $('#ddlsupplier_chzn').remove();
                    $('#ddlsupplier').removeClass('chzn-done');
                    // $('#ddlsupplier').chosen();

                    for (var t = 0; t < supp_array.length; t++) {

                        var filter_supp = [];

                        filter_supp = output.filter(function (e) {
                            return (e.Sup_No == supp_array[t].HO_ID);
                        });

                        if (filter_supp.length > 0) {
                            $('#ddlsupplier').append($("<option selected></option>").val(supp_array[t].HO_ID).html(supp_array[t].Name)).trigger('chosen:updated').css("width", "100%");;;
                        }
                        else {
                            $('#ddlsupplier').append($("<option></option>").val(supp_array[t].HO_ID).html(supp_array[t].Name)).trigger('chosen:updated').css("width", "100%");;;
                        }
                    }
                    $('#ddlsupplier').chosen();
                });

                function loadtable(result) {

                    var tbl = $('#ProductTable');
                    $(tbl).find('tbody tr').remove();
                    $('#free_table >tbody').html('');
                    $("#spn").html("");
                    var str = ""; var pcode = ""; var count = 0;

                    for (var i = 0; i < result.length; i++) {
                        var free_unit = '';
                        var free = result[i].Free || 0;
                        if (free == '0') { free_unit = 0; } else { free_unit = (free + '-' + result[i].Offer_Product_Unit); }
                        //var discc = result[i].discount || 0;
                        discc = result[i].discount || 0;

                        if (pcode == result[i].Product_Code) {

                            //str = "<tr class=" + result[i].Product_Code + "><td style='width: 5%;'>" + (i + 1) + "</td><td style='display:none'><input type='hidden' id='pro_code' class='pro_code' value=" + result[i].Product_Code + "></td><td style='display:none'><input type='hidden' id='erp_code' class='erp_code' value=" + result[i].Sample_Erp_Code + "></td><td style='width: 16%;'>" + result[i].Sale_Erp_Code + "<br></td><td style='width: 22%;padding: 13px 0px 0px 0px;'><label class='pro_name'>" + result[i].Product_Name + "</label><br><div><label>Unit :</label><label id='uom'>" + result[i].Product_Unit_Name + "</label><label style='padding-left: 5px;'>Rs :</label><label>" + result[i].Rate + "</label></div></td><td style='width: 6%;padding: 26px 0px 0px 0px;'><label name='txtPoqty' maxlength='7' class='numberOnly'>" + result[i].CQty/result[i].con_fact + "</label></td><td style='width: 10%;padding: 20px 0px 0px 0px;'><input type='text' autocomplete='off' style='width:67%;' name='txtGood'maxlength='7' class='textval' value=" + result[i].CQty/result[i].con_fact + " ></td><td style='padding: 26px 10px 0px 9px;'><label id='free' class='pro_free'>" + free_unit + "</label></td><td style='display:none'><input type='hidden' id='ofer_product_na' class='ofer_product_na' value='" + result[i].Offer_ProductCd + "'></td><td style='display:none'><input type='hidden' id='ofer_product_unit' class='ofer_product_unit' value='" + result[i].Offer_Product_Unit + "'></td><td style='display:none'><input type='hidden' id='ofer_product_code' class='ofer_product_code' value=" + result[i].Offer_ProductNm + "></td><td style='width: 5%;padding: 26px 0px 0px 9px;'><label id='disc' class='pro_disc'>" + discc + "</label></td><td style='display:none'><input type='hidden' id='Dis_value' class='Dis_value'></td><td style='display:none'><label class='price' >" + result[i].Rate + "</label></td>";
                            //str += "<td style='padding: 26px 0px 0px 0px;text-align: right;'><label class='gross_value' id='gross_value'>" + 0 + "</label></td><td style='width: 10%;padding: 26px 0px 0px 0px;text-align: right;'><label class='tax_amt'>" + 0 + "</label></td><td style='display:none'><input type='hidden' id='tax_val' class='tax_val' value=" + result[i].Tax_Val + "></td><td style='display:none'><input type='hidden' id='tax_id' class='tax_id' value=" + result[i].Tax_Id + "></td><td style='display:none'><input type='hidden' id='tax_name' class='tax_name' value='" + result[i].Tax_Name + "'></td><td style='display:none'><input type='hidden' id='dam' class='dam'></td><td style='padding: 26px 15px 0px 0px; text-align: right;'><label class='netVal' name='netVal'>0.00</label></td><td style='display:none';><input type='hidden' id='con_fac' class='con_fac'></td><td style='display:none';><input type='hidden' id='pro_unit' class='pro_unit' value=" + result[i].Product_Unit_Name + "></td></tr>";
                            //str += "<td style='padding: 26px 0px 0px 0px;text-align: right;'><label class='gross_value' id='gross_value'>" + 0 + "</label></td><td style='width: 10%;padding: 26px 0px 0px 0px;text-align: right;'><label class='tax_amt'>" + 0 + "</label></td><td style='display:none'><input type='hidden' id='tax_val' class='tax_val' value=" + result[i].Tax_Val + "></td><td style='display:none'><input type='hidden' id='tax_id' class='tax_id' value=" + result[i].Tax_Id + "></td><td style='display:none'><input type='hidden' id='tax_name' class='tax_name' value='" + result[i].Tax_Name + "'></td><td style='display:none'><input type='hidden' id='dam' class='dam'></td><td style='padding: 26px 15px 0px 0px; text-align: right;'><label class='netVal' name='netVal'>0.00</label></td><td style='display:none';><input type='hidden' id='con_fac' class='con_fac'></td><td style='display:none';><input type='hidden' id='pro_unit' class='pro_unit' value=" + result[i].Product_Unit_Name + "></td></tr>";
                            //str = "<tr class=" + result[i].Product_Code + "><td>" + (i + 1) + "</td><td style='display:none'><input type='hidden' id='pro_code' class='pro_code' value=" + result[i].Product_Code + "></td><td style='display:none'><input type='hidden' id='erp_code' class='erp_code' value=" + result[i].Sample_Erp_Code + "></td><td style='width: 16%;'>" + result[i].Sale_Erp_Code + "<br></td><td style='width: 22%;padding: 13px 0px 0px 0px;'><label class='pro_name'>" + result[i].Product_Name + "</label><br><div><label>Unit :</label><label id='uom'>" + result[i].Product_Unit_Name + "</label><label> Rs :</label><label>" + result[i].Rate + "</label></div></td><td style='text-align:center'><label  name='txtPoqty' maxlength='7' class='numberOnly'>" + result[i].CQty + "</label></td><td><input type='text' autocomplete='off' style='width:67%;' name='txtGood'maxlength='7' class='textval' value=" + result[i].CQty + " ></td><td><label id='free' class='pro_free'>" + free_unit + "</label></td><td style='display:none'><input type='hidden' id='ofer_product_na' class='ofer_product_na' value='" + result[i].Offer_ProductCd + "'></td><td style='display:none'><input type='hidden' id='ofer_product_unit' class='ofer_product_unit' value='" + result[i].Offer_Product_Unit + "'></td><td style='display:none'><input type='hidden' id='ofer_product_code' class='ofer_product_code' value=" + result[i].Offer_ProductNm + "></td><td style='width: 5%;padding: 26px 0px 0px 9px;'><label id='disc' class='pro_disc'>" + discc + "</label></td><td style='display:none'><input type='hidden' id='Dis_value' class='Dis_value'></td><td style='display:none'><label class='price' >" + result[i].Rate + "</label></td>";                            
                            //str += "<td><label class='gross_value' id='gross_value'>" + 0 + "</label></td><td><label class='tax_amt'>" + 0 + "</label></td><td style='display:none'><input type='hidden' id='tax_val' class='tax_val' value=" + result[i].Tax_Val + "></td><td style='display:none'><input type='hidden' id='tax_id' class='tax_id' value=" + result[i].Tax_Id + "></td><td style='display:none'><input type='hidden' id='tax_name' class='tax_name' value='" + result[i].Tax_Name + "'></td><td style='display:none'><input type='hidden' id='dam' class='dam'></td><td><label class='netVal' name='netVal'>0.00</label></td><td style='display:none';><input type='hidden' id='con_fac' class='con_fac'></td><td style='display:none';><input type='hidden' id='pro_unit' class='pro_unit' value=" + result[i].Product_Unit_Name + "></td></tr>";

                            //str = "<tr><td>" + (i + 1) + "</td><td style='display:none'><input type='hidden' id='pro_code' class='pro_code' value=" + result[i].Product_Code + "></td><td style='display:none'><input type='hidden' id='erp_code' class='erp_code' value=" + result[i].Sample_Erp_Code + "></td><td style='width: 16%;'>" + result[i].Sale_Erp_Code + "<br></td><td><label class='pro_name'>" + result[i].Product_Name + "</label><br><div><label>Unit :</label><label id='uom'>" + result[i].Product_Unit_Name + "</label><label style='padding-left: 5px;'>Rs :</label><label>" + result[i].Rate + "</label></div></td><td style='text-align:center'><label name='txtPoqty' maxlength='7' class='numberOnly'>" + result[i].CQty + "</label></td><td><input type='text' autocomplete='off' value=" + result[i].CQty + " name='txtGood' maxlength='7'style='width:67%;' class='textval' ></td><td style='text-align:center'><label id='free' class='pro_free'>" + free_unit + "</label></td><td style='display:none'><input type='hidden' id='ofer_product_na' class='ofer_product_na' value='" + result[i].Offer_ProductCd + "'></td><td style='display:none'><input type='hidden' id='ofer_product_unit' class='ofer_product_unit' value='" + result[i].Offer_Product_Unit + "'></td><td style='display:none'><input type='hidden' id='ofer_product_code' class='ofer_product_code' value=" + result[i].Offer_ProductNm + "></td><td style='text-align:center'><label id='disc' class='pro_disc'>" + discc + "</label></td><td style='display:none'><input type='hidden' id='Dis_value' class='Dis_value'></td><td style='display:none'><label class='price' >" + result[i].Rate + "</label></td>";
                            //str += "<td style='text-align:center'><label class='gross_value' id='gross_value'>" + 0 + "</label></td><td style='text-align:center'><label class='tax_amt'>" + 0 + "</label></td><td style='display:none'><input type='hidden' id='tax_val' class='tax_val' value=" + result[i].Tax_Val + "></td><td style='display:none'><input type='hidden' id='tax_id' class='tax_id' value=" + result[i].Tax_Id + "></td><td style='display:none'><input type='hidden' id='tax_name' class='tax_name' value='" + result[i].Tax_Name + "'></td><td style='display:none'><input type='hidden' id='dam' class='dam'></td><td style='text-align:right'><label class='netVal' name='netVal'>0.00</label></td><td style='display:none';><input type='hidden' id='con_fac' class='con_fac'></td><td style='display:none';><input type='hidden' id='pro_unit' class='pro_unit' value=" + result[i].Product_Unit_Name + "></td></tr>";
                            //$('#ProductTable >tbody').append(str);

                            //str = "<tr><td>" + (i + 1) + "</td><td style='display:none'><input type='hidden' id='pro_code' class='pro_code' value=" + result[i].Product_Code + "></td><td style='display:none'><input type='hidden' id='erp_code' class='erp_code' value=" + result[i].Sample_Erp_Code + "></td><td style='width: 16%;'>" + result[i].Sale_Erp_Code + "<br></td><td><label class='pro_name'>" + result[i].Product_Name + "</label><br><div><label>Unit :</label><label id='uom'>" + result[i].Product_Unit_Name + "</label><label style='padding-left: 5px;'>Rs :</label><label>" + result[i].Rate + "</label><label style='padding-left:5px;'>Dis% :</label><label class='disclass' id='dis_id'>" + discc + "</label></div></td><td style='text-align:center'><label name='txtPoqty' maxlength='7' class='numberOnly'>" + result[i].CQty + "</label></td><td><input type='text' autocomplete='off' value=" + result[i].CQty + " name='txtGood' maxlength='7'style='width:67%;' class='textval' ></td><td><label id='free' class='pro_free'>" + free_unit + "</label></td><td style='display:none'><input type='hidden' id='ofer_product_na' class='ofer_product_na' value='" + result[i].Offer_ProductCd + "'></td><td style='display:none'><input type='hidden' id='ofer_product_unit' class='ofer_product_unit' value='" + result[i].Offer_Product_Unit + "'></td><td style='display:none'><input type='hidden' id='ofer_product_code' class='ofer_product_code' value=" + result[i].Offer_ProductNm + "></td><td style='text-align:center'><label id='disc' class='pro_disc'>" + 0 + "</label></td><td style='display:none'><input type='hidden' id='Dis_value' class='Dis_value'></td><td style='display:none'><label class='price' >" + result[i].Rate + "</label></td>";
                            //str += "<td style='text-align:center'><label class='gross_value' id='gross_value'>" + 0 + "</label></td><td style='text-align:center'><label class='tax_amt'>" + 0 + "</label></td><td style='display:none'><input type='hidden' id='tax_val' class='tax_val' value=" + result[i].Tax_Val + "></td><td style='display:none'><input type='hidden' id='tax_id' class='tax_id' value=" + result[i].Tax_Id + "></td><td style='display:none'><input type='hidden' id='tax_name' class='tax_name' value='" + result[i].Tax_Name + "'></td><td style='display:none'><input type='hidden' id='dam' class='dam'></td><td style='text-align:right'><label class='netVal' name='netVal'>0.00</label></td><td style='display:none';><input type='hidden' id='con_fac' class='con_fac'></td><td style='display:none';><input type='hidden' id='pro_unit' class='pro_unit' value=" + result[i].Product_Unit_Name + "></td></tr>";
                            //$('#ProductTable >tbody').append(str);

                            str = "<tr class=" + result[i].Product_Code + "><td>" + (i + 1) + "</td><td style='display:none'><input type='hidden' id='pro_code' class='pro_code' value=" + result[i].Product_Code + "></td><td style='display:none'><input type='hidden' id='erp_code' class='erp_code' value=" + result[i].Sample_Erp_Code + "></td><td style='width: 16%;'>" + result[i].Sale_Erp_Code + "<br></td><td><label class='pro_name'>" + result[i].Product_Name + "</label><br><div><label>Unit :</label><label id='uom'>" + result[i].Product_Unit_Name + "</label><label style='padding-left: 5px;'>Rs :</label><label>" + result[i].Rate + "</label><label style='padding-left:5px;'>Dis%</label><label class='disclass' id='dis_id'>" + discc + "</label></div></td><td style='text-align:center'><label name='txtPoqty' maxlength='7' class='numberOnly'>" + result[i].CQty / result[i].con_fact + "</label></td><td style='display:none'><input type='hidden' class='uom' id='" + result[i].Move_MailFolder_Id + "' value='" + result[i].Product_Unit_Name + "'></td><td><input type='text' autocomplete='off' value=" + result[i].CQty / result[i].con_fact + " name='txtGood' maxlength='7'style='width:85%;' class='textval' ></td><td><label id='free' class='fre' value=" + result[i].Free + ">" + free_unit + "</label></td><td style='display:none'><input type='hidden' id='ofer_product_na' class='ofer_product_na' value='" + result[i].Offer_ProductCd + "'></td><td style='display:none'><input type='hidden' id='ofer_product_unit' class='ofer_product_unit' value='" + result[i].Offer_Product_Unit + "'></td><td style='display:none'><input type='hidden' id='ofer_product_code' class='ofer_product_code' value=" + result[i].Offer_ProductNm + "></td><td><label id='disc' class='pro_disc'>" + 0 + "</label></td><td style='display:none'><input type='hidden' id='Dis_value' class='Dis_value'></td><td style='display:none'><label class='price' >" + result[i].Rate + "</label></td>";
                            str += "<td><label class='gross_value' id='gross_value'>" + 0 + "</label></td><td style='display:none'><label class='Rate_in_peice' >" + result[i].Rate_in_peice + "</label></td><td><label class='tax_amt'>" + 0 + "</label></td><td style='display:none'><input type='hidden' id='tax_val' class='tax_val' value=" + result[i].Tax_Val + "></td><td style='display:none'><input type='hidden' id='tax_id' class='tax_id' value=" + result[i].Tax_Id + "></td><td style='display:none'><input type='hidden' id='tax_name' class='tax_name' value='" + result[i].Tax_Name + "'></td><td style='display:none'><input type='hidden' id='dam' class='dam'></td><td><label class='netVal' name='netVal'>0.00</label></td><td style='display:none';><input type='hidden' id='con_fac' class='con_fac' value='" + result[i].con_fact + "'></td><td style='display:none';><input type='hidden' id='pro_unit' class='pro_unit' value=" + result[i].Product_Unit_Name + "></td></tr>";
                            $('#ProductTable >tbody').append(str);
                            //getscheme(result[i].Sale_Erp_Code, result[i].CQty, result[i].Product_Unit_Name, $('#ProductTable >tbody tr'), result[i].Free, discc, result[i].Product_Code, result[i].CQty)

                        }
                        else {
                            //str = "<tr><td style='width: 5%;'>" + (i + 1) + "</td><td style='display:none'><input type='hidden' id='pro_code' class='pro_code' value=" + result[i].Product_Code + "></td><td style='display:none'><input type='hidden' id='erp_code' class='erp_code' value=" + result[i].Sample_Erp_Code + "></td><td style='width: 16%;'>" + result[i].Sale_Erp_Code + "<br><div id='second_table' style=' padding: 5px 0px 0px 0px;'><label style='padding: 0px 5px 0px 0px;'>Batch No</label><span style='color: Red'>*</span><input autocomplete='off' type='text' id='batchtext' class='batch' style='width: 50%;'></div></td><td style='width: 22%;padding: 13px 0px 0px 0px;'><label class='pro_name'>" + result[i].Product_Name + "</label><br><div><label>Unit :</label><label id='uom'>" + result[i].Product_Unit_Name + "</label><label style='padding-left: 5px;'>Rs :</label><label>" + result[i].Rate + "</label></div></td><td><label name='txtPoqty' maxlength='7' class='numberOnly'>" + result[i].CQty + "</label></td><td><input type='text' autocomplete='off' value=" + result[i].CQty + " name='txtGood' maxlength='7'style='width:67%;' class='textval' ></td><td><label id='free' class='pro_free'>" + free_unit + "</label></td><td style='display:none'><input type='hidden' id='ofer_product_na' class='ofer_product_na' value='" + result[i].Offer_ProductCd + "'></td><td style='display:none'><input type='hidden' id='ofer_product_unit' class='ofer_product_unit' value='" + result[i].Offer_Product_Unit + "'></td><td style='display:none'><input type='hidden' id='ofer_product_code' class='ofer_product_code' value=" + result[i].Offer_ProductNm + "></td><td style='text-align:center'><label id='disc' class='pro_disc'>" + discc + "</label></td><td style='display:none'><input type='hidden' id='Dis_value' class='Dis_value'></td><td style='display:none'><label class='price' >" + result[i].Rate + "</label></td>";
                            //str += "<td style='text-align: right;'><label class='gross_value' id='gross_value'>" + 0 + "</label></td><td style='width: 10%;padding: 26px 0px 0px 0px;text-align: right;'><label class='tax_amt'>" + 0 + "</label></td><td style='display:none'><input type='hidden' id='tax_val' class='tax_val' value=" + result[i].Tax_Val + "></td><td style='display:none'><input type='hidden' id='tax_id' class='tax_id' value=" + result[i].Tax_Id + "></td><td style='display:none'><input type='hidden' id='tax_name' class='tax_name' value='" + result[i].Tax_Name + "'></td><td style='display:none'><input type='hidden' id='dam' class='dam'></td><td style='padding: 26px 15px 0px 0px; text-align: right;'><label class='netVal' name='netVal'>0.00</label></td><td style='display:none';><input type='hidden' id='con_fac' class='con_fac'></td><td style='display:none';><input type='hidden' id='pro_unit' class='pro_unit' value=" + result[i].Product_Unit_Name + "></td></tr>";
                            //str += "<td style='text-align: right;'><label class='gross_value' id='gross_value'>" + 0 + "</label></td><td style='width: 10%;padding: 26px 0px 0px 0px;text-align: right;'><label class='tax_amt'>" + 0 + "</label></td><td style='display:none'><input type='hidden' id='tax_val' class='tax_val' value=" + result[i].Tax_Val + "></td><td style='display:none'><input type='hidden' id='tax_id' class='tax_id' value=" + result[i].Tax_Id + "></td><td style='display:none'><input type='hidden' id='tax_name' class='tax_name' value='" + result[i].Tax_Name + "'></td><td style='display:none'><input type='hidden' id='dam' class='dam'></td><td style='padding: 26px 15px 0px 0px; text-align: right;'><label class='netVal' name='netVal'>0.00</label></td><td style='display:none';><input type='hidden' id='con_fac' class='con_fac'></td><td style='display:none';><input type='hidden' id='pro_unit' class='pro_unit' value=" + result[i].Product_Unit_Name + "></td></tr>";

                            //str = "<tr><td>" + (i + 1) + "</td><td style='display:none'><input type='hidden' id='pro_code' class='pro_code' value=" + result[i].Product_Code + "></td><td style='display:none'><input type='hidden' id='erp_code' class='erp_code' value=" + result[i].Sample_Erp_Code + "></td><td style='width: 16%;'>" + result[i].Sale_Erp_Code + "<br><div id='second_table' ><label style='padding: 0px 5px 0px 0px;'>Batch No</label><span style='color: Red'>*</span><input autocomplete='off' type='text' id='batchtext' class='batch' style='width: 50%;'></div></td><td><label class='pro_name'>" + result[i].Product_Name + "</label><br><div><label>Unit :</label><label id='uom'>" + result[i].Product_Unit_Name + "</label><label style='padding-left: 5px;'>Rs :</label><label>" + result[i].Rate + "</label></div></td><td style='text-align:center'><label name='txtPoqty' maxlength='7' class='numberOnly'>" + result[i].CQty + "</label></td><td><input type='text' autocomplete='off' value=" + result[i].CQty + " name='txtGood' maxlength='7'style='width:67%;' class='textval' ></td><td style='text-align:center'><label id='free' class='pro_free'>" + free_unit + "</label></td><td style='display:none'><input type='hidden' id='ofer_product_na' class='ofer_product_na' value='" + result[i].Offer_ProductCd + "'></td><td style='display:none'><input type='hidden' id='ofer_product_unit' class='ofer_product_unit' value='" + result[i].Offer_Product_Unit + "'></td><td style='display:none'><input type='hidden' id='ofer_product_code' class='ofer_product_code' value=" + result[i].Offer_ProductNm + "></td><td style='text-align:center'><label id='disc' class='pro_disc'>" + discc + "</label></td><td style='display:none'><input type='hidden' id='Dis_value' class='Dis_value'></td><td style='display:none'><label class='price' >" + result[i].Rate + "</label></td>";                            
                            //str += "<td style='text-align:center'><label class='gross_value' id='gross_value'>" + 0 + "</label></td><td style='text-align:center'><label class='tax_amt'>" + 0 + "</label></td><td style='display:none'><input type='hidden' id='tax_val' class='tax_val' value=" + result[i].Tax_Val + "></td><td style='display:none'><input type='hidden' id='tax_id' class='tax_id' value=" + result[i].Tax_Id + "></td><td style='display:none'><input type='hidden' id='tax_name' class='tax_name' value='" + result[i].Tax_Name + "'></td><td style='display:none'><input type='hidden' id='dam' class='dam'></td><td style='text-align:right'><label class='netVal' name='netVal'>0.00</label></td><td style='display:none';><input type='hidden' id='con_fac' class='con_fac'></td><td style='display:none';><input type='hidden' id='pro_unit' class='pro_unit' value=" + result[i].Product_Unit_Name + "></td></tr>";
                            //$('#ProductTable >tbody').append(str); pcode = result[i].Product_Code;

                            //str = "<tr><td>" + (i + 1) + "</td><td style='display:none'><input type='hidden' id='pro_code' class='pro_code' value=" + result[i].Product_Code + "></td><td style='display:none'><input type='hidden' id='erp_code' class='erp_code' value=" + result[i].Sample_Erp_Code + "></td><td style='width: 16%;'>" + result[i].Sale_Erp_Code + "<br><div id='second_table' ><label style='padding: 0px 5px 0px 0px;'>Batch No</label><span style='color: Red'>*</span><input autocomplete='off' type='text' id='batchtext' class='batch' style='width: 50%;'></div></td><td><label class='pro_name'>" + result[i].Product_Name + "</label><br><div><label>Unit :</label><label id='uom'>" + result[i].Product_Unit_Name + "</label><label style='padding-left: 5px;'>Rs :</label><label>" + result[i].Rate + "</label><label style='padding-left:5px;'>Dis% :</label><label class='disclass' id='dis_id'>" + discc + "</label></div></td><td style='text-align:center'><label name='txtPoqty' maxlength='7' class='numberOnly'>" + result[i].CQty + "</label></td><td><input type='text' autocomplete='off' value=" + result[i].CQty + " name='txtGood' maxlength='7'style='width:67%;' class='textval' ></td><td><label id='free' class='pro_free'>" + free_unit + "</label></td><td style='display:none'><input type='hidden' id='ofer_product_na' class='ofer_product_na' value='" + result[i].Offer_ProductCd + "'></td><td style='display:none'><input type='hidden' id='ofer_product_unit' class='ofer_product_unit' value='" + result[i].Offer_Product_Unit + "'></td><td style='display:none'><input type='hidden' id='ofer_product_code' class='ofer_product_code' value=" + result[i].Offer_ProductNm + "></td><td style='text-align:center'><label id='disc' class='pro_disc'>" + 0 + "</label></td><td style='display:none'><input type='hidden' id='Dis_value' class='Dis_value'></td><td style='display:none'><label class='price' >" + result[i].Rate + "</label></td>";
                            //str += "<td style='text-align:center'><label class='gross_value' id='gross_value'>" + 0 + "</label></td><td style='text-align:center'><label class='tax_amt'>" + 0 + "</label></td><td style='display:none'><input type='hidden' id='tax_val' class='tax_val' value=" + result[i].Tax_Val + "></td><td style='display:none'><input type='hidden' id='tax_id' class='tax_id' value=" + result[i].Tax_Id + "></td><td style='display:none'><input type='hidden' id='tax_name' class='tax_name' value='" + result[i].Tax_Name + "'></td><td style='display:none'><input type='hidden' id='dam' class='dam'></td><td style='text-align:right'><label class='netVal' name='netVal'>0.00</label></td><td style='display:none';><input type='hidden' id='con_fac' class='con_fac'></td><td style='display:none';><input type='hidden' id='pro_unit' class='pro_unit' value=" + result[i].Product_Unit_Name + "></td></tr>";
                            //$('#ProductTable >tbody').append(str); pcode = result[i].Product_Code;

                            str = "<tr><td>" + (i + 1) + "</td><td style='display:none'><input type='hidden' id='pro_code' class='pro_code' value=" + result[i].Product_Code + "></td><td style='display:none'><input type='hidden' id='erp_code' class='erp_code' value=" + result[i].Sample_Erp_Code + "></td><td style='width: 16%;'>" + result[i].Sale_Erp_Code + "<br><div id='second_table' ><label style='padding: 0px 5px 0px 0px;'>Batch No</label><span style='color: Red'>*</span><input autocomplete='off' type='text' id='batchtext' class='batch' style='width: 50%;'></div></td><td><label class='pro_name'>" + result[i].Product_Name + "</label><br><div><label>Unit :</label><label id='uom'>" + result[i].product_unit + "</label><label style='padding-left: 5px;'>Rs :</label><label>" + result[i].Rate + "</label><label style='padding-left:5px;'>Dis%</label><label class='disclass' id='dis_id'>" + discc + "</label></div></td><td style='text-align:center'><label name='txtPoqty' maxlength='7' class='numberOnly'>" + result[i].CQty / result[i].con_fact + "</label></td><td style='display:none'><input type='hidden' class='uom' id='" + result[i].Move_MailFolder_Id + "' value='" + result[i].product_unit + "'></td><td><input type='text' autocomplete='off' value=" + result[i].CQty / result[i].con_fact + " name='txtGood' maxlength='7'style='width:85%;' class='textval' ></td><td><label id='free' class='fre' value=" + result[i].Free + ">" + free_unit + "</label></td><td style='display:none'><input type='hidden' id='ofer_product_na' class='ofer_product_na' value='" + result[i].Offer_ProductCd + "'></td><td style='display:none'><input type='hidden' id='ofer_product_unit' class='ofer_product_unit' value='" + result[i].Offer_Product_Unit + "'></td><td style='display:none'><input type='hidden' id='ofer_product_code' class='ofer_product_code' value=" + result[i].Offer_ProductNm + "></td><td><label id='disc' class='pro_disc'>" + 0 + "</label></td><td style='display:none'><input type='hidden' id='Dis_value' class='Dis_value'></td><td style='display:none'><label class='price' >" + result[i].Rate + "</label></td>";
                            str += "<td><label class='gross_value' id='gross_value'>" + 0 + "</label></td><td style='display:none'><label class='Rate_in_peice' >" + result[i].Rate_in_peice + "</label></td><td><label class='tax_amt'>" + 0 + "</label></td><td style='display:none'><input type='hidden' id='tax_val' class='tax_val' value=" + result[i].Tax_Val + "></td><td style='display:none'><input type='hidden' id='tax_id' class='tax_id' value=" + result[i].Tax_Id + "></td><td style='display:none'><input type='hidden' id='tax_name' class='tax_name' value='" + result[i].Tax_Name + "'></td><td style='display:none'><input type='hidden' id='dam' class='dam'></td><td><label class='netVal' name='netVal'>0.00</label></td><td style='display:none';><input type='hidden' id='con_fac' class='con_fac' value='" + result[i].con_fact + "' ></td><td style='display:none';><input type='hidden' id='pro_unit' class='pro_unit' value=" + result[i].Product_Unit_Name + "></td></tr>";
                            $('#ProductTable >tbody').append(str); pcode = result[i].Product_Code;
                            //getscheme(result[i].Sale_Erp_Code, result[i].CQty, result[i].Product_Unit_Name, $('#ProductTable >tbody tr'), result[i].Free, discc, result[i].Product_Code, result[i].CQty)
                        }

                        if (result[i].Free != '0') {

                            str1 = "<tr><td style='width: 11%;'>" + (count + 1) + "</td><td style='width: 21%;'>" + result[i].Offer_ProductNm + "</td><td style='width: 47%;'>" + result[i].Offer_ProductCd + "</td><td>" + (result[i].Free + '-' + result[i].Offer_Product_Unit) + "<td></tr>";

                            $('#free_table >tbody').append(str1);
                            count++;
                        }
                    }
                    //$('#footer').html("");
                    //$('#footer').append('<table><tbody><tr><td style="width: 3%; font-weight: bold;">Total</td><td style="width: 14%;"><div class="input-group"><div class="input-group-addon currency"><i class="fa fa-inr"></i></div><input data-cell="G1" id="total_good_value"  data-format="0.00" class="form-control" readonly="" ></div></td><td style="width: 9%;padding: 0px 0px 0px 14px;font-weight: bold;">Gst_Total</td><td style="width: 14%;"><div class="input-group"><div class="input-group-addon currency"><i class="fa fa-inr"></i></div><input data-cell="G1" id="total_tax_value" data-format="0,0.00" class="form-control" readonly=""></div></td><td style="width: 9%;padding: 0px 0px 0px 14px;font-weight: bold;">Discount_total</td><td style="width: 14%;"><div class="input-group"><div class="input-group-addon currency"><i class="fa fa-inr"></i></div><input data-cell="G1" id="all_dis_tot"  data-format="0,0.00" class="form-control"  readonly=""></div></td><td style="width: 8%;padding: 0px 0px 0px 17px;font-weight: bold;">Gross_Total</td><td style="width: 14%;"><div class="input-group"><div class="input-group-addon currency"><i class="fa fa-inr"></i></div><input data-cell="G1" id="gnd_Net_tot" data-format="0,0.00" class="form-control total1" readonly="" ></div></td></tr></tbody></table>');
                    rowCal();

                }


                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Goods_Received_Note1.aspx/GetCurrStock",
                    dataType: "json",
                    success: function (data) {
                        StkDetail = data.d;
                        console.log(StkDetail);
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });

                function rowCal() {
                    var tot_Good = 0; var tax_total = 0; var totval = 0; var dis_total = 0; var totinvqty = 0; var totOrdQty = 0;
                    $('#ProductTable tbody tr').each(function () {

                        var price = $(this).find("td").find(".price").text();

                        //   if ($(this).find("input[name=txtGood]").val() == "") {

                        //      var good = $(this).find(".numberOnly").text();
                        //   }
                        //   else {
                        var good = $(this).find("input[name=txtGood]").val();
                        //   }

                        if (isNaN(price)) price = 0;
                        if (isNaN(good)) good = 0;


                        var tot_gross = price * good;
                        var dis = $(this).closest("tr").find('.disclass').text();
                        if (isNaN(dis)) dis = 0;

                        var dis_calc = dis / 100 * tot_gross;
                        var fin_dis_cal = tot_gross - dis_calc;

                        $(this).closest("tr").find('.pro_disc').text(dis_calc.toFixed(2));

                        $(this).find(".Dis_value").val(dis_calc.toFixed(2));

                        dis_total += parseFloat($(this).find(".Dis_value").val());

                        $(this).find(".gross_value").text(fin_dis_cal.toFixed(2));

                        tot_Good += parseFloat($(this).find(".gross_value").text());

                        var tax_cal = $(this).closest("tr").find('.tax_val').val() / 100 * $(this).find(".gross_value").text();
                        $(this).closest("tr").find('.tax_amt').text(tax_cal.toFixed(2));

                        var final_amt = parseFloat(tax_cal) + parseFloat($(this).find(".gross_value").text());
                        $(this).find('td').find('.netVal').text(final_amt.toFixed(2));
                        var each_total = $(this).find('td').find('.netVal').text();
                        if (each_total == "" || each_total == '0') each_total = 0;

                        tax_total += parseFloat(tax_cal);
                        totval += parseFloat(each_total);

                        var OrdQty = $(this).closest("tr").find('.numberOnly').text();
                        totOrdQty += parseFloat(OrdQty);

                        var invQty = $(this).closest("tr").find('.textval').val();
                        if (invQty == "" || invQty == '0') invQty = 0;
                        totinvqty += parseFloat(invQty);

                    });
                    $("#gnd_Net_tot").val(totval.toFixed(2));
                    $("#total_good_value").val(tot_Good.toFixed(2));
                    $("#total_tax_value").val(tax_total.toFixed(2));
                    $("#all_dis_tot").val(dis_total.toFixed(2));

                    var tbl = $('#ProductTable');
                    $(tbl).find('tfoot tr').remove();
                    $("#ProductTable").append('<tfoot id="ProductTablefoot"><tr><th colspan="3"></th><th style="text-align: center;" id="totOrdQty">' + totOrdQty + '</th><th id="totinvQty">' + totinvqty + '</th><th colspan="1"></th><th style="text-align: center;" id="totdis">' + dis_total.toFixed(2) + '</th><th style="text-align: center;" id="totGross">' + tot_Good.toFixed(2) + '</th><th style="text-align: center;" id="totTax">' + tax_total.toFixed(2) + '</th><th style="text-align: right;" id="totNet">' + totval.toFixed(2) + '</th></tr></tfoot>');


                }
                var ckrow = [];
                $(document).on('blur', 'input[name=txtGood]', function () {

                    tr = $(this).closest("tr");
                    var good = $(this).val();
                    CQ = (good == "") ? 0 : good;
                    un = $(tr).find('#uom').text();

                    if (good.length <= 0) {
                        $(this).val('');
                    }

                    var ans = [];

                    var pc = $(this).closest('tr').find('.pro_code').val();
                    var cv = $(this).closest('tr').find('.con_fac').val();

                    ans = Allrate.filter(function (t) {
                        return (t.Product_Detail_Code == pc);
                    });

                    if ($(this).closest('tr').find('.pro_unit').val() == ans[0].product_unit) {

                        $(this).closest('tr').find('.con_fac').val(cv);

                    }
                    else {
                        $(this).closest('tr').find('.con_fac').val(cv);
                    }


                    var currVal = $(this).closest('tr').find('input[name=stkval]').attr('stkgood') || 0;
                    var oldVal = $(this).closest('tr').find('input[name=txtGood]').attr('pv_good') || 0;

                    var currDmg = $(this).closest('tr').find('input[name=stkval]').attr('stkdamage') || 0;
                    var oldDmg = $(this).closest('tr').find('.dam').attr('pv_damage') || 0;


                    var qty = $(this).closest('tr').find('.numberOnly').text() || 0;

                    pcode = ans[0].Sale_Erp_Code
                    ff = $(tr).find(".fre").val();
                    disss = $(tr).find('.pro_disc').val();
                    opcode = $(tr).find(".pro_code").val();
                    pname = $(tr).find(".pro_name").text();

                    if (Number(qty) < Number($(this).val())) {
                        $(this).val('');
                        // $(this).val(oldVal);
						rjalert('Error!', 'Invoice qty Must Below Order qty..!', 'error');
                        //alert('Goods Value Must below ' + qty + '..!');
                        $(this).focus();
                        //getscheme(pcode, 0, un, tr, ff, disss, opcode, 0)
                        return false;
                    }
                    else if (Number(qty) > Number($(this).val()) && Number($(this).val()) >= 0) {
                        if ($(this).val() != '') {
                            var row = $(this).closest("tr");
                            idx = $(row).index();
                            var txt;
							$.confirm({
                                title: 'Alert!',
                                content: 'Do you want to Add the balance quantity to pending order!',
                                type: 'green',
                                typeAnimated: true,
                                autoClose: 'cancel|8000',
                                icon: 'fa fa-check fa-2x',
                                buttons: {
                                    tryAgain: {
                                        text: 'OK',
                                        btnClass: 'btn-green',
                                        action: function () {
										  var ckh = [];
                                var ro = row;
                                var pn = $(ro).find('.pro_code').val();
                                var sn = $(ro).find('td:first').text();
                                if (pendord.length > 0) {
                                    ckh = pendord.filter(function (t) {
                                        return (t.PCd == pn && t.sno == sn);
                                    });
                                    if (ckh.length > 0) {
                                        pendord = $.grep(pendord, function (n) {
                                            return (n.sno != sn);
                                        });
                                    }
                                }

                                ckrow.push({
                                    PCd: sn,
                                    PName: pn
                                });

                                var Qtypen = parseInt($(row).find('.numberOnly').text()) - parseInt($(row).find("input[name=txtGood]").val());
                                var price = parseInt($(row).find('.price').text());
                                            var Rate_in_peice = parseInt($(row).find('.Rate_in_peice').text());
                                pendord.push({
                                    sno: $(row).find('td:first').text(),
                                    PCd: $(row).find('.pro_code').val(),
                                    PName: $(row).find('.pro_name').text(),
                                    umo_unit: $(row).children('td').find('.uom').attr('id'),
                                    UOM_Name: $(row).children('td').find('.uom').val(),
                                    Erp_Code: $(row).find('.erp_code').val(),
                                    // POQTY: $(this).find('.numberOnly').text(),
                                    Qty_c: Qtypen * cv,
                                    Rate: price,
                                    Rate_in_peice: Rate_in_peice,
                                    Tax_value: $(row).find('.tax_amt').text() || 0,
                                    Gross_Value: price * Qtypen || 0,
                                    Net_Value: price * Qtypen + (parseInt($(row).find('.tax_amt').text()) || 0) || 0,
                                    free: $(row).find('.pro_free').text() || 0,
                                    dis: $(row).find('.pro_disc').text() || 0,
                                    dis_value: $(row).find('.pro_disc').text() || 0,
                                    Off_Pro_code: $(row).find('.ofer_product_code').val(),
                                    Off_Pro_name: $(row).find('.ofer_product_na').val(),
                                    Off_Pro_Unit: $(row).find('.ofer_product_unit').val(),
                                    con_fac: $(row).find('.con_fac').val(),

                                });


										  }
                                    },

                                    cancel: function () {
                                        row.find('.setting').attr('sett', 'No');
                                        $.alert('Quantity Will not added to the pending order');
                                        //alert('Time Expired!');
                                    }
                                }
                            });
                            
                        }
                        else {

                        }
                    }
                    else {
                        var damage = Number(qty) - Number($(this).val());
                        $(this).closest('tr').find('.dam').val(damage)

                    }

                    //var vqty = 0;
                    //console.log(currVal + ":" + oldVal);
                    //if (Number(currVal) < Number(oldVal)) {
                    //    console.log('en')
                    //    vqty = oldVal - currVal;
                    //}


                    //console.log(vqty + ":" + god);

                    //if (Number(vqty) > Number(god)) {
                    //    $(this).val(oldVal);
                    //    alert('Goods Value Must above ' + vqty + ' ..!');
                    //    $(this).focus();
                    //}

                    //var vDmg = 0;
                    //if (Number(currDmg) < Number(oldDmg)) {
                    //    vDmg = oldDmg - currDmg;
                    //}

                    //if (Number(vDmg) > Number(dmg)) {
                    //    $(this).val(oldVal);
                    //    var ent = (qty - vDmg)
                    //    alert('Goods Value Must Below ' + ent + ' ..!');
                    //    $(this).focus();
                    //}
                    //calcAddi($(this));

                    var dmg = $(this).closest('tr').find('.dam').val() || 0;
                    calcAddi(CQ);
                    //getscheme(pcode, CQ, un, tr, ff, disss, opcode, CQ)

                });

                function getscheme(pCode, cq, un, tr, ff, disss, opcode, gd) {

                    var res = scheme.filter(function (a) {
                        return (a.Sale_Erp_Code == pCode && (Number(cq) >= Number(a.Scheme)) && a.Scheme_Unit == un)
                    });
                    ans = [];
                    if (res.length > 0) {

                        if (res[0].Offer_Product == 0) {

                            ans = Allrate.filter(function (t) {
                                return (t.Product_Detail_Code == opcode);
                            });
                        }

                        else {
                            ans = Allrate.filter(function (t) {
                                return (t.Product_Detail_Code == res[0].Offer_Product);

                            });
                        }
                    }

                    $(tr).find('.dis').text(0);
                    $(tr).find('input[name="disc_value"]').val(0);

                    var hfree = '';


                    if ($(tr).find('.fre').attr('cqty') > 0) {

                        qqq = Allrate.filter(function (t) {
                            return (t.Product_Detail_Code == $(tr).find('.fre').attr('freepro'));
                        });

                        for (var c = 0; c < arr.length; c++) {
                            //if (isNaN(arr[c].Free)) arr[c].Free = 0;
                            if (qqq.length > 0) {
                                if ($(tr).find('.fre').attr('unit') == qqq[0].product_unit) {
                                    if (qqq[0].product_unit == arr[c].C_fre_uni) {
                                        if (arr[c].pppcode.indexOf(opcode) >= 0) {
                                            if (arr[c].Free > 0) {
                                                arr[c].Free = arr[c].Free - ff;
                                                $(tr).find('.fre').text('#' + opcode).text('0');
                                                $(tr).find('.fre1').text('#' + opcode).text('0');
                                                tr.find('.of_pro_name').val(0);
                                                tr.find('.of_pro_code').val(0);
                                                $(tr).find('.disc_value').val($(tr).find('.disc_value').val() - $(tr).find('.disc_value').val());
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        for (var c = 0; c < arr.length; c++) {
                            //if (isNaN(arr[c].P_Free)) arr[c].P_Free = 0;                          
                            if (qqq.length > 0) {
                                if ($(tr).find('.fre').attr('unit') == qqq[0].Product_Sale_Unit) {
                                    if (qqq[0].Product_Sale_Unit == arr[c].P_fre_uni) {
                                        if (arr[c].pppcode.indexOf(opcode) >= 0) {
                                            if (arr[c].P_Free > 0) {
                                                arr[c].P_Free = arr[c].P_Free - ff;
                                                $(tr).find('.fre').text('#' + opcode).text('0');
                                                $(tr).find('.fre1').text('#' + opcode).text('0');
                                                tr.find('.of_pro_name').val(0);
                                                tr.find('.of_pro_code').val(0);
                                                $(tr).find('.disc_value').val($(tr).find('.disc_value').val() - $(tr).find('.disc_value').val());
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (res.length > 0) {

                        schemedefinewithoutpackage(res[0], tr, ff, ans, gd);
                    }

                    //CalcGTot();
                    calcAddi(gd);
                    $('#free_table').find('tbody tr').remove();

                    for (var r = 0; r < arr.length; r++) {
                        if (arr[r].Free != '0' || arr[r].P_Free != '0') {
                            if (arr[r].Free != '0' && arr[r].P_Free != '0') {
                                var str = "<tr><td style='width: 14%;' class='td_id'>" + (r + 1) + "</td><td style='width:21%;'><input type='hidden' value=" + arr[r].Product_Code + " id='apc'/>" + arr[r].Product_Code + "</td><td style='left: 201px;width: 38%;' class='td_name'><input type='hidden' value=" + arr[r].Product_Name + " id='apn'/>" + arr[r].Product_Name + "</td><td style='left: 454px;' class='td_free'><input type='hidden' value=" + arr[r].Free + " id='fr'/>" + (arr[r].Free + ' ' + arr[r].C_join) + "</td><td style='display:none'><input type='hidden' id='opc' value=" + arr[r].Product_Code1 + " />" + arr[r].Product_sale_Code + "</td><td>" + (arr[r].P_Free + ' ' + arr[r].P_join) + "</td></tr>";
                                $('#free_table tbody').append(str);
                            }

                            if (arr[r].Free != '0' && arr[r].P_Free == '0') {
                                var str = "<tr><td style='width: 14%;' class='td_id'>" + (r + 1) + "</td><td style='width:21%;'><input type='hidden' value=" + arr[r].Product_Code + " id='apc'/>" + arr[r].Product_Code + "</td><td style='left: 201px;width: 38%;' class='td_name'><input type='hidden' value=" + arr[r].Product_Name + " id='apn'/>" + arr[r].Product_Name + "</td><td style='left: 454px;' class='td_free'><input type='hidden' value=" + arr[r].Free + " id='fr'/>" + (arr[r].Free + ' ' + arr[r].C_join) + "</td><td style='display:none'><input type='hidden' id='opc' value=" + arr[r].Product_Code1 + " />" + arr[r].Product_sale_Code + "</td></tr>";
                                $('#free_table tbody').append(str);
                            }

                            if (arr[r].P_Free != '0' && arr[r].Free == '0') {
                                var str = "<tr><td style='width: 14%;' class='td_id'>" + (r + 1) + "</td><td style='width:21%;'><input type='hidden' value=" + arr[r].Product_Code + " id='apc'/>" + arr[r].Product_Code + "</td><td style='left: 201px;width: 38%;' class='td_name'><input type='hidden' value=" + arr[r].Product_Name + " id='apn'/>" + arr[r].Product_Name + "</td><td style='display:none'><input type='hidden' id='opc' value=" + arr[r].Product_Code1 + " />" + arr[r].Product_sale_Code + "</td><td>" + (arr[r].P_Free + ' ' + arr[r].P_join) + "</td></tr>";
                                $('#free_table tbody').append(str);
                            }

                        }

                    }

                }

                function schemedefinewithoutpackage(res, tr, ff, kk, CQ) {

                    if (res.Discount == '0') {

                        if (res.Package != "Y") {

                            if (CQ > 0) {

                                var free = parseInt(parseFloat((CQ / parseInt(res.Scheme))) * parseInt(res.Free))

                                if (res.Offer_Product != 0) {
                                    var x = arr.filter(function (b) {
                                        return (b.Product_Code == res.Offer_Product)
                                    })
                                }
                                else {
                                    var x = arr.filter(function (b) {
                                        return (b.Product_Code == res.Product_Code)
                                    })
                                }
                                if (res.Against == "N") {

                                    if (ans[0].product_unit == res.Free_Unit) {
                                        if (x.length > 0) {
                                            idx = arr.indexOf(x[0]);
                                            arr[idx].Free += free;
                                            arr[idx].C_fre_uni = res.Free_Unit;
                                            arr[idx].P_join = ans[0].Product_Sale_Unit;
                                            arr[idx].pppcode = (arr[idx].pppcode.indexOf(opcode) >= 0) ? arr[idx].pppcode : arr[idx].pppcode + ',' + opcode;
                                            $(tr).find('.fre').text(free + '' + (ans[0].product_unit));
                                            $(tr).find('.fre').attr('unit', ans[0].product_unit);
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
                                            $(tr).find('.fre').text(free + ' ' + ((ans[0].product_unit)));
                                            $(tr).find('.fre').attr('unit', ans[0].product_unit);
                                            $(tr).find('.fre').attr('freepro', res.Product_Code);
                                            $(tr).find('.fre').attr('cqty', CQ);
                                            $(tr).find('.fre').attr('freecqty', free);

                                            $(tr).find('.fre1').text(free);

                                            if (free != "") {
                                                tr.find('.of_pro_name').val(pname);
                                                tr.find('.of_pro_code').val(res.Product_Code);
                                            }
                                            arr.push({
                                                Product_Code: res.Product_Code,
                                                Product_Name: pname,
                                                pppcode: res.Product_Code,
                                                pppname: res.Product_Name,
                                                Free: free,
                                                P_Free: 0,
                                                C_fre_uni: res.Free_Unit,
                                                P_fre_uni: 0,
                                                C_join: ans[0].product_unit,
                                                P_join: ans[0].Product_Sale_Unit
                                            })
                                        }
                                    }
                                    else {
                                        if (x.length > 0) {
                                            idx = arr.indexOf(x[0]);
                                            arr[idx].P_fre_uni = res.Free_Unit;
                                            arr[idx].P_Free += free;
                                            arr[idx].pppcode = (arr[idx].pppcode.indexOf(opcode) >= 0) ? arr[idx].pppcode : arr[idx].pppcode + ',' + opcode;
                                            arr[idx].C_join = ans[0].product_unit;
                                            $(tr).find('.fre').text(free + ' ' + (ans[0].Product_Sale_Unit));
                                            $(tr).find('.fre').attr('unit', ans[0].Product_Sale_Unit);
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
                                            $(tr).find('.fre').text(free + ' ' + (ans[0].Product_Sale_Unit));
                                            $(tr).find('.fre').attr('unit', ans[0].Product_Sale_Unit);
                                            $(tr).find('.fre').attr('unit', ans[0].Product_Sale_Unit);
                                            $(tr).find('.fre').attr('freepro', res.Product_Code);
                                            $(tr).find('.fre').attr('cqty', CQ);
                                            $(tr).find('.fre').attr('freecqty', free);
                                            $(tr).find('.fre1').text(free);

                                            if (free != "") {
                                                tr.find('.of_pro_name').val(pname);
                                                tr.find('.of_pro_code').val(res.Product_Code);
                                            }
                                            arr.push({
                                                Product_Code: res.Product_Code,
                                                Product_Name: pname,
                                                pppcode: res.Product_Code,
                                                pppname: res.Product_Name,
                                                Free: 0,
                                                P_Free: free,
                                                C_fre_uni: 0,
                                                P_fre_uni: res.Free_Unit,
                                                P_join: ans[0].Product_Sale_Unit,
                                                C_join: ans[0].product_unit

                                            })
                                        }
                                    }
                                }
                                else {

                                    if (ans[0].product_unit == res.offer_product_unit) {
                                        if (parseInt(CQ) >= parseInt(res.Scheme)) {

                                            if (x.length > 0) {
                                                idx = arr.indexOf(x[0]);
                                                arr[idx].C_fre_uni = res.offer_product_unit;
                                                arr[idx].pppcode = (arr[idx].pppcode.indexOf(opcode) >= 0) ? arr[idx].pppcode : arr[idx].pppcode + ',' + opcode;
                                                arr[idx].Free += free;
                                                arr[idx].P_join = ans[0].Product_Sale_Unit;
                                                $(tr).find('.fre').text(free + ' ' + (ans[0].product_unit));
                                                $(tr).find('.fre').attr('unit', ans[0].product_unit);
                                                $(tr).find('.fre').attr('freepro', res.Offer_Product);
                                                $(tr).find('.fre').attr('cqty', CQ);
                                                $(tr).find('.fre').attr('freecqty', free);
                                                $(tr).find('.fre1').text(free);
                                                tr.find('.of_pro_name').val(res.Offer_Product_Name);
                                                tr.find('.of_pro_code').val(res.Offer_Product);
                                            }
                                            else {
                                                $(tr).find('.fre').text(free + ' ' + ((ans[0].product_unit)));
                                                $(tr).find('.fre').attr('unit', ans[0].product_unit);
                                                $(tr).find('.fre').attr('freepro', res.Offer_Product);
                                                $(tr).find('.fre').attr('cqty', CQ);
                                                $(tr).find('.fre').attr('freecqty', free);
                                                $(tr).find('.fre1').text(free);

                                                tr.find('.of_pro_name').val(res.Offer_Product_Name);
                                                tr.find('.of_pro_code').val(res.Offer_Product);
                                                arr.push({

                                                    Product_Code: res.Offer_Product,
                                                    Product_Name: res.Offer_Product_Name,
                                                    pppcode: res.Product_Code,
                                                    pppname: res.Product_Name,
                                                    Free: free,
                                                    P_Free: 0,
                                                    C_fre_uni: res.offer_product_unit,
                                                    P_fre_uni: 0,
                                                    C_join: ans[0].product_unit,
                                                    P_join: ans[0].Product_Sale_Unit
                                                })
                                            }
                                        }
                                    }
                                    else {
                                        if (x.length > 0) {
                                            idx = arr.indexOf(x[0]);
                                            arr[idx].P_fre_uni = res.offer_product_unit;
                                            arr[idx].pppcode = (arr[idx].pppcode.indexOf(opcode) >= 0) ? arr[idx].pppcode : arr[idx].pppcode + ',' + opcode;
                                            arr[idx].P_Free += free;
                                            arr[idx].C_join = ans[0].product_unit;
                                            $(tr).find('.fre').text(free + ' ' + (ans[0].Product_Sale_Unit));
                                            $(tr).find('.fre').attr('unit', ans[0].Product_Sale_Unit);
                                            $(tr).find('.fre').attr('freepro', res.Offer_Product);
                                            $(tr).find('.fre').attr('cqty', CQ);
                                            $(tr).find('.fre').attr('freecqty', free);
                                            $(tr).find('.fre1').text(free);
                                            tr.find('.of_pro_name').val(res.Offer_Product_Name);
                                            tr.find('.of_pro_code').val(res.Offer_Product);
                                        }
                                        else {
                                            $(tr).find('.fre').text(free + ' ' + ((ans[0].Product_Sale_Unit)));
                                            $(tr).find('.fre').attr('unit', ans[0].Product_Sale_Unit);
                                            $(tr).find('.fre').attr('freepro', res.Offer_Product);
                                            $(tr).find('.fre').attr('cqty', CQ);
                                            $(tr).find('.fre').attr('freecqty', free);
                                            $(tr).find('.fre1').text(free);

                                            tr.find('.of_pro_name').val(res.Offer_Product_Name);
                                            tr.find('.of_pro_code').val(res.Offer_Product);
                                            arr.push({
                                                Product_Code: res.Offer_Product,
                                                Product_Name: res.Offer_Product_Name,
                                                pppcode: res.Product_Code,
                                                pppname: res.Product_Name,
                                                Free: 0,
                                                P_Free: free,
                                                C_fre_uni: 0,
                                                P_fre_uni: res.offer_product_unit,
                                                P_join: ans[0].Product_Sale_Unit,
                                                C_join: ans[0].product_unit
                                            })
                                        }
                                    }
                                }
                            }
                            else {

                                var z = arr.filter(function (b) {
                                    return (b.Product_sale_Code == pCode)
                                })

                                if (z.length > 0) {
                                    idx = arr.indexOf(z[0]);
                                    arr.splice(idx, 1);

                                }
                                $(tr).find('.dis').text(0);
                            }
                        }
                        else {

                            var TotalQuantity = CQ / res.Scheme;
                            var free = parseInt(TotalQuantity) * res.Free;

                            if (res.Offer_Product != 0) {
                                var x = arr.filter(function (b) {
                                    return (b.Product_Code == res.Offer_Product)
                                })

                            }
                            else {
                                var x = arr.filter(function (b) {
                                    return (b.Product_Code == res.Product_Code)
                                })
                            }

                            if (res.Against == "N") {

                                if (ans[0].product_unit == res.Free_Unit) {

                                    if (x.length > 0) {
                                        idx = arr.indexOf(x[0]);
                                        arr[idx].Free += free;
                                        arr[idx].pppcode = (arr[idx].pppcode.indexOf(opcode) >= 0) ? arr[idx].pppcode : arr[idx].pppcode + ',' + opcode;
                                        arr[idx].C_fre_uni = res.Free_Unit;
                                        arr[idx].P_join = ans[0].Product_Sale_Unit;
                                        $(tr).find('.fre').text(free + ' ' + (ans[0].product_unit));
                                        $(tr).find('.fre').attr('unit', ans[0].product_unit);
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
                                        $(tr).find('.fre').text(free + ' ' + ((ans[0].product_unit)));
                                        $(tr).find('.fre').attr('unit', ans[0].product_unit);
                                        $(tr).find('.fre').attr('freepro', res.Product_Code);
                                        $(tr).find('.fre').attr('cqty', CQ);
                                        $(tr).find('.fre').attr('freecqty', free);
                                        $(tr).find('.fre1').text(free);

                                        if (free != "") {
                                            tr.find('.of_pro_name').val(pname);
                                            tr.find('.of_pro_code').val(res.Product_Code);
                                        }

                                        arr.push({
                                            Product_Code: res.Product_Code,
                                            Product_Name: pname,
                                            pppcode: res.Product_Code,
                                            pppname: res.Product_Name,
                                            Free: free,
                                            P_Free: 0,
                                            C_fre_uni: res.Free_Unit,
                                            P_fre_uni: 0,
                                            C_join: ans[0].product_unit,
                                            P_join: ans[0].Product_Sale_Unit

                                        })
                                    }
                                }
                                else {
                                    if (x.length > 0) {
                                        idx = arr.indexOf(x[0]);
                                        arr[idx].P_Free += free;
                                        arr[idx].pppcode = (arr[idx].pppcode.indexOf(opcode) >= 0) ? arr[idx].pppcode : arr[idx].pppcode + ',' + opcode;
                                        arr[idx].P_fre_uni = res.Free_Unit;
                                        arr[idx].C_join = ans[0].product_unit;
                                        $(tr).find('.fre').text(free + ' ' + (ans[0].Product_Sale_Unit));
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
                                        $(tr).find('.fre').text(free + ' ' + ((ans[0].Product_Sale_Unit)));
                                        $(tr).find('.fre').attr('unit', ans[0].Product_Sale_Unit);
                                        $(tr).find('.fre').attr('freepro', res.Product_Code);
                                        $(tr).find('.fre').attr('cqty', CQ);
                                        $(tr).find('.fre').attr('freecqty', free);
                                        $(tr).find('.fre1').text(free);

                                        if (free != "") {
                                            tr.find('.of_pro_name').val(pname);
                                            tr.find('.of_pro_code').val(res.Product_Code);
                                        }

                                        arr.push({
                                            Product_Code: res.Product_Code,
                                            Product_Name: pname,
                                            pppcode: res.Product_Code,
                                            pppname: res.Product_Name,
                                            Free: 0,
                                            P_Free: free,
                                            C_fre_uni: 0,
                                            P_fre_uni: res.Free_Unit,
                                            P_join: ans[0].Product_Sale_Unit,
                                            C_join: ans[0].product_unit

                                        })
                                    }
                                }
                            }
                            else {
                                if (ans[0].product_unit == res.offer_product_unit) {
                                    if (x.length > 0) {
                                        idx = arr.indexOf(x[0]);
                                        arr[idx].C_fre_uni = res.offer_product_unit;
                                        arr[idx].pppcode = (arr[idx].pppcode.indexOf(opcode) >= 0) ? arr[idx].pppcode : arr[idx].pppcode + ',' + opcode;
                                        arr[idx].Free += free;
                                        arr[idx].P_join = ans[0].Product_Sale_Unit;
                                        $(tr).find('.fre').text(free + ' ' + (ans[0].product_unit));
                                        $(tr).find('.fre').attr('unit', ans[0].product_unit);
                                        $(tr).find('.fre').attr('freepro', res.Offer_Product);
                                        $(tr).find('.fre').attr('cqty', CQ);
                                        $(tr).find('.fre').attr('freecqty', free);

                                        $(tr).find('.fre1').text(free);
                                        tr.find('.of_pro_name').val(res.Offer_Product_Name);
                                        tr.find('.of_pro_code').val(res.Offer_Product);
                                    }
                                    else {
                                        $(tr).find('.fre').text(free + ' ' + ((ans[0].product_unit)));
                                        $(tr).find('.fre').attr('unit', ans[0].product_unit);
                                        $(tr).find('.fre').attr('freepro', res.Offer_Product);
                                        $(tr).find('.fre').attr('cqty', CQ);
                                        $(tr).find('.fre').attr('freecqty', free);
                                        $(tr).find('.fre1').text(free);

                                        tr.find('.of_pro_name').val(res.Offer_Product_Name);
                                        tr.find('.of_pro_code').val(res.Offer_Product);

                                        arr.push({
                                            Product_Code: res.Offer_Product,
                                            Product_Name: res.Offer_Product_Name,
                                            pppcode: res.Product_Code,
                                            pppname: res.Product_Name,
                                            C_fre_uni: res.offer_product_unit,
                                            P_fre_uni: 0,
                                            Free: free,
                                            P_Free: 0,
                                            C_join: ans[0].product_unit,
                                            P_join: ans[0].Product_Sale_Unit


                                        })
                                    }
                                } else {
                                    if (x.length > 0) {
                                        idx = arr.indexOf(x[0]);
                                        arr[idx].P_fre_uni = res.offer_product_unit;
                                        arr[idx].pppcode = (arr[idx].pppcode.indexOf(opcode) >= 0) ? arr[idx].pppcode : arr[idx].pppcode + ',' + opcode;
                                        arr[idx].P_Free += free;
                                        arr[idx].C_join = ans[0].product_unit;
                                        $(tr).find('.fre').text(free + ' ' + (ans[0].Product_Sale_Unit));
                                        $(tr).find('.fre').attr('unit', ans[0].Product_Sale_Unit);
                                        $(tr).find('.fre').attr('freepro', res.Offer_Product);
                                        $(tr).find('.fre').attr('cqty', CQ);
                                        $(tr).find('.fre').attr('freecqty', free);
                                        $(tr).find('.fre1').text(free);
                                        tr.find('.of_pro_name').val(res.Offer_Product_Name);
                                        tr.find('.of_pro_code').val(res.Offer_Product);
                                    }
                                    else {
                                        $(tr).find('.fre').text(free + ' ' + ((ans[0].Product_Sale_Unit)));
                                        $(tr).find('.fre').attr('unit', ans[0].Product_Sale_Unit);
                                        $(tr).find('.fre').attr('freepro', res.Offer_Product);
                                        $(tr).find('.fre').attr('cqty', CQ);
                                        $(tr).find('.fre').attr('freecqty', free);
                                        $(tr).find('.fre1').text(free);

                                        tr.find('.of_pro_name').val(res.Offer_Product_Name);
                                        tr.find('.of_pro_code').val(res.Offer_Product);

                                        arr.push({
                                            Product_Code: res.Offer_Product,
                                            Product_Name: res.Offer_Product_Name,
                                            pppcode: res.Product_Code,
                                            pppname: res.Product_Name,
                                            Free: 0,
                                            P_Free: free,
                                            C_fre_uni: 0,
                                            P_fre_uni: res.offer_product_unit,
                                            P_join: ans[0].Product_Sale_Unit,
                                            C_join: ans[0].product_unit

                                        })
                                    }
                                }
                            }
                        }
                    }

                    $(tr).find('.dis').text(res.Discount);
                    var d = $(tr).find('.code').text() * $(tr).find('.numberVal').val();
                    var discalc = parseInt(res.Discount) / 100;
                    var distotal = d * discalc;
                    var finaltotal = d - distotal;

                    if (distotal != '0') {
                        $(tr).find('.gross_value').text(finaltotal.toFixed(2));
                        $(tr).find('.disc_value').val(distotal.toFixed(2));
                    }

                    if (finaltotal != "0") {
                        var after_cal = $(tr).find('#tex_val').val() / 100 * $(tr).find('.gross_value').text()
                        var fin = after_cal + parseFloat($(tr).find('.gross_value').text());
                        $(tr).find('.tex').text(after_cal.toFixed(2));
                        $(tr).find('#txttotal').text(fin.toFixed(2));
                    }
                    //CalcGTot();
                    calcAddi(CQ);
                }

                $(document).on('keyup', 'input[name=txtPoqty]', function () {

                    var poq = $(this).val();
                    if (poq.length <= 0) {
                        $(this).val(0);
                    }

                    var poqty = $(this).closest('tr').find('input[name=txtPoqty]').val();

                    var fin = poqty - poq;

                    $(this).closest('tr').find('.dam').val(fin);

                    var currVal = $(this).closest('tr').find('input[name=stkval]').attr('stkgood') || 0;
                    var oldVal = $(this).closest('tr').find('input[name=txtGood]').attr('pv_good') || 0;

                    var currDmg = $(this).closest('tr').find('input[name=stkval]').attr('stkdamage') || 0;
                    var oldDmg = $(this).closest('tr').find('.dam').attr('pv_damage') || 0;
                    var god = $(this).closest('tr').find('input[name=txtGood]').val() || 0;
                    var dmg = $(this).closest('tr').find('.dam').val() || 0;

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
                        rjalert('Error!', 'PO QTY Value Must Above ' + TotCrrQty + ' ..!', 'error');
						//alert('PO QTY Value Must Above ' + TotCrrQty + ' ..!');
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
                //$(document).on('blur', 'input[name=txtBatch]', function () {
                //    var batchno = $(this).val();
                //    if (batchno.length > 0) {
                //        $(this).closest('tr').find('input[name=txtDate]').datepicker({ dateFormat: 'dd/mm/yy' });
                //    }
                //});

                //$(document).on('focus', 'input[name=txtDate]', function () {
                //    var batchs = $(this).closest('tr').find('input[name=txtBatch]').val();
                //    if (batchs.length <= 0) {
                //        alert('Enter Batch No.');
                //        $(this).closest('tr').find('input[name=txtBatch]').focus();
                //        return false;
                //    }
                //});

                //$(document).on('focus', 'input[name=txtGood]', function () {

                //    var date = $(this).closest('tr').find('input[name=txtDate]').val();
                //    if (date.length <= 0) {
                //        alert('Enter MFG Date');
                //        $(this).closest('tr').find('input[name=txtDate]').focus();
                //    }
                //});


                $(document).on('keyup', 'input[name=txtPrice]', function () {
                    //calcAddi($(this));
                    calcAddi($(this).val());
                });

                //$(document).on('keyup', 'input[name=txtGood]', function () {

                //});

                calcAddi = function (x) {

                    //var t = parseFloat($(x).val());
                    var t = x;
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
                        cTC = $(Rw).find('td').find('.price').text();
                        if (isNaN(cTC)) cTC = 0;
                    }


                    var Amt_calc = $(Rw).find('td').find('.price').text() * t;

                    var dis_value = $(Rw).find('td').find('.pro_disc').text();
                    if (isNaN(dis_value)) dis_value = 0;

                    var dis_calc = dis_value / 100 * Amt_calc;
                    var fin_dis_cal = Amt_calc - dis_calc;

                    $(Rw).find('td').find('.gross_value').text(fin_dis_cal.toFixed(2));

                    var tax = Rw.find('.tax_val').val();
                    if (isNaN(tax)) tax = 0;

                    var TaxAmt = ($(Rw).find('td').find('.gross_value').text() * tax) / 100;
                    $(Rw).find('td').find('.tax_amt').text(TaxAmt.toFixed(2));
                    tottax = Number($(Rw).find('td').find('.gross_value').text()) + Number(TaxAmt);
                    //$(Rw).find("input[name=taxVal]").val(TaxAmt.toFixed(2));
                    $(Rw).find(".netVal").text(tottax.toFixed(2));
                    console.log(tottax);

                    // $(Rw).find("label[name=netVal]").text(t * cTC);
                    var su = t * cTC;

                    //var sum = t * cTC + Number(tottax);
                    //$(Rw).find("label[name=netVal]").text(sum.toFixed(2));

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

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    data: "{'Div_Code':'" + Div_Code + "','Stockist_Code':'" + stockist_Code + "'}",
                    url: "Goods_Received_Note1.aspx/getratenew",
                    dataType: "json",
                    success: function (data) {
                        Allrate = JSON.parse(data.d) || [];
                    },
                    error: function (result) {
                    }
                });

                $("#tSearchOrd").on("keyup", function () {
                    if ($(this).val() != "") {
                        shText = $(this).val().toLowerCase();
                        Orders = result.filter(function (a) {
                            chk = false;
                            $.each(a, function (key, val) {
                                if (val != null && val.toString().toLowerCase().indexOf(shText) > -1 && (',' + searchKeys).indexOf(',' + key) > -1) {
                                    chk = true;
                                }
                            })
                            return chk;
                        })
                    }
                    else
                        Orders = result
                    loadtable(Orders);
                })

                var buttoncount = 0;

                $(document).on('click', '.btnsave', function () {

                    var chk = true; buttoncount += 1;
                    if (buttoncount == "1") {

			var poname = $('#PonoSelect :selected').val();
                        if (poname == "0") { buttoncount = 0; 
						rjalert('Error!', 'Select PO NO', 'error');
						//alert('Select PO NO'); 
						$('#PonoSelect').focus(); return false; }

                        var grnDate = $('#grnDate').val();
                        if (grnDate.length <= 0) { buttoncount = 0; 
						rjalert('Error!', 'Enter GRN Date!!', 'error');
						//alert('Enter GRN Date!!'); 
						$('#grnDate').focus(); return false; }

                        

                        var Supplier = $('#ddlsupplier').val();
                        if (Supplier == 0) { buttoncount = 0;
						rjalert('Error!', 'Select Supplier Name!!', 'error');
						//alert('Select Supplier Name!!'); 
						$('#ddlsupplier').focus(); return false; }

                        var grnChallan = $('#challenNo').val();
                        if (grnChallan.length <= 0) { buttoncount = 0; 
						rjalert('Error!', 'Enter PI No.!!', 'error');
						//alert('Enter PI No.!!');
						$('#challenNo').focus(); return false; }

                        var grnDisDt = $('#grnDisDate').val();
                        if (grnDisDt.length <= 0) { buttoncount = 0; 
						rjalert('Error!', 'Enter Dispatch  Date!!', 'error');
						//alert('Enter Dispatch  Date!!'); 
						$('#grnDisDate').focus(); return false; }
						$('#ProductTable >tbody >tr').each(function () {

                            if ($(this).find("#batchtext").val() == '') {
                                //batch_count++;
                                buttoncount = 0;
				               rjalert('Error!', 'Enter Batch No!!!', 'error');
                                //alert('Enter Batch No!!!');
                                che = 1;
                                $(this).find("input[id*=batchtext]").focus();
                                return false;
                            }
							});
							if(che==1){
							return false;
							}
							
                       	 var received = $('#receivedby').val();
                        if (received == "") { buttoncount = 0; 
						rjalert('Error!', 'Enter Received By', 'error');
						//alert('Enter Received By');
						$('#receivedby').focus(); return false; }

                        var auth = $('#authorised').val();
                        if (auth == "") { buttoncount = 0;
						rjalert('Error!', 'Enter Authorised By', 'error');
						//alert('Enter Authorised By'); 
						$('#authorised').focus(); return false; }

                        var remark = $('#remarks').val();

                        var mainArr = []; var proArr = [];

                        mainArr.push({
                            GRN_No: $('#challenNo').val() || 0,
                            GRN_Date: grnDate,
                            Entry_Date: new Date(),
                            Supp_Code: $('#ddlsupplier option:selected').val(),
                            Supp_Name: $('#ddlsupplier option:selected').text(),
                            Po_No: $('#PonoSelect option:selected').text(),
                            Challan_No: grnChallan,
                            Dispatch_Date: grnDisDt,
                            Received_Location: ("<%=Session["Sf_Code"].ToString()%>"),
                            Received_Name: ("<%=Session["sf_name"].ToString()%>"),
                            Received_By: received,
                            Authorized_By: auth,
                            //goodsTot: $("#total_good_value").val(),
                            //taxTot: $("#total_tax_value").val(),
                            goodsTot: $("#totGross").text(),
                            taxTot: $("#totTax").text(),
                            remarks: remark,
                            //netTot: $('#gnd_Net_tot').val()
                            netTot: $('#totNet').text()
                        });

                        var countr = 0; var batch_count = 0; var batch = ''; var che = 0;

                        $('#ProductTable >tbody >tr').each(function () {

                            if ($(this).find("#batchtext").val() == '') {
                                //batch_count++;
                                buttoncount = 0;
				rjalert('Error!', 'Enter Batch No!!!', 'error');
                                //alert('Enter Batch No!!!');
                                che = 1;
                                $(this).find("input[id*=batchtext]").focus();
                                return false;
                            }

                            if ($(this).find("input[name=txtGood]").val() == '') {
                                //countr++;
                                buttoncount = 0;
                                alert('Enter invoice quantity!!!');
                                che = 1;
                                $(this).find("input[name=txtGood]").focus();
                                return false;
                            }



                            taxArr = [];
                            taxArr.push({
                                Tax_Code: $(this).find('.tax_id').val(),
                                Tax_Name: $(this).find('.tax_name').val(),
                                Tax_Value: $(this).find('.tax_val').val()
                            });
                            //var sgood = Number($(this).children('td').eq(7).find('input[name=txtGood]').attr('pv_good')) || 0;
                            //var sdamaged = Number($(this).children('td').eq(8).find('input[name=txtDamaged]').attr('pv_damage')) || 0;

                            if ($(this).find('.pro_code').val() == $(this).attr('class')) {

                                batch = batch;
                            }
                            else {
                                batch = $(this).children('td').find('#batchtext').val();

                            }

                            var P_Code = $(this).find('.pro_code').val();
                            var factor = $(this).find('#uom').text();

                            var ans = [];

                            ans = Allrate.filter(function (t) {
                                return (t.Product_Detail_Code == P_Code && t.Move_MailFolder_Id == factor);
                            });
                            if (ans.length < 1) {
                                ans = Allrate.filter(function (t) {
                                    return (t.Product_Detail_Code == P_Code && t.Move_MailFolder_Name == factor);
                                });
                            }


                            if (ans.length > 0) {

                                if (ans[0].Move_MailFolder_Name == ans[0].Product_Sale_Unit) {

                                    var sgood = $(this).find('.textval').val() || 0;
                                    var good = $(this).find('.textval').val() || 0;
                                }
                                else {
                                    var sgood = $(this).find('.textval').val() * ans[0].Sample_Erp_Code;
                                    var good = $(this).find('.textval').val() * ans[0].Sample_Erp_Code;
                                }

                            }
                            //var sgood = $(this).find('.textval').val();
                            var sdamaged = $(this).find('.dam').val() || 0;

                            proArr.push({
                                PCode: $(this).find('.pro_code').val(),
                                PDetails: $(this).find('.pro_name').text(),
                                UOM: $(this).children('td').find('.uom').attr('id'),
                                UOM_Name: $(this).children('td').find('.uom').val(),
                                Batch_No: batch,
                                mfgDate: grnDate,
                                Erp_Code: $(this).find('.erp_code').val(),
                                // POQTY: $(this).find('.numberOnly').text(),
                                POQTY: $(this).find("input[name=txtGood]").val(),
                                Price: $(this).find('.price').text(),
                                Rate_in_peice: $(this).find('.Rate_in_peice').text(),
                                // Good: $(this).find('.textval').val(),
                                Good: good,
                                Tax: $(this).find('.tax_amt').text() || 0,
                                Damaged: $(this).find('.dam').val() || 0,
                                Gross_Value: $(this).find('.gross_value').text() || 0,
                                Net_Value: $(this).find('label[name=netVal]').text() || 0,
                                Remarks: 0,
                                free: $(this).find('.pro_free').text() || 0,
                                dis: $(this).find('.pro_disc').text() || 0,
                                dis_val: $(this).find('.pro_disc').text() || 0,
                                Offer_pro_code: $(this).find('.ofer_product_code').val(),
                                Offer_pro_name: $(this).find('.ofer_product_na').val(),
                                offer_pro_unit: $(this).find('.ofer_product_unit').val(),
                                con_factor: $(this).find('.con_fac').val(),
                                SGood: sgood,
                                SDamaged: sdamaged,
                            });

                        });

                        //  if (countr > 0) {
                        //   buttoncount = 0;
                        //  alert('Enter Quantity!!!');
                        //   $('#ProductTable').focus();
                        //   return false;
                        // }
                        // if (batch_count > 0) {
                        //     buttoncount = 0;
                        //     alert('Enter Batch No');
                        //     $('#ProductTable').focus();
                        //      return false;
                        //  } 

                        var saveType = "";
                        if ($('#<%=hdnmode.ClientID %>').val() == "1") {
                            saveType = "1";
                        }
                        else {
                            saveType = "0";
                        }

                        if (che == 0) {
                            if (pendord.length > 0) {
                                var Tax_Array = [];
                                var selectedvalue = $('#ddlsupplier option:selected').val();
                                var selectdtext = $('#ddlsupplier option:selected').text();
                                var PonoSelect = $('#PonoSelect option:selected').text();
                                var gross = 0, taxtot = 0, subtot = 0;
                                for (var t = 0; t < pendord.length; t++) {
                                    gross += pendord[t].Net_Value;
                                    taxtot += pendord[t].Tax || 0;
                                    subtot += pendord[t].Gross_Value;
                                }
                                itm1 = {}
                                itm1.bill_add = "";
                                itm1.ship_add = "";
                                itm1.order_date = "";
                                itm1.exp_date = "";
                                itm1.div_code = Div_Code || "";
                                itm1.sup_no = selectedvalue || "";
                                itm1.sup_name = selectdtext;
                                itm1.sf_code = stockist_Code || "";
                                itm1.stk_code = stockist_Code || "";
                                itm1.com_add = selectdtext || "";
                                itm1.sub_tot = subtot;
                                itm1.Tax_Total = taxtot || 0;
                                itm1.Gross_tot = gross;
                                headdatas.push(itm1);



                                $.ajax({
                                    type: "POST",
                                    contentType: "application/json; charset=utf-8",
                                    async: false,
                                    url: "Goods_Received_Note1.aspx/SavePurchaseOrder",
                                    data: "{'HeadData':'" + JSON.stringify(headdatas) + "','DetailsData':'" + JSON.stringify(pendord) + "','TaxData':'" + JSON.stringify(Tax_Array) + "','Pono_No':'" + PonoSelect + "'}",
                                    dataType: "json",
                                    success: function (data) {
                                        if (data.d.length > 0) {
                                        }
                                        else {
                                            alert('Error');
                                        }
                                    },
                                    error: function (result) {
                                    }
                                });
                            }
                            $.ajax({
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                url: "Goods_Received_Note1.aspx/SaveDate",
                                data: "{'Head':'" + JSON.stringify(mainArr) + "','Details':'" + JSON.stringify(proArr) + "','Tax':'" + JSON.stringify(taxArr) + "','SaveMode':'" + saveType + "'}",
                                dataType: "json",
                                success: function (data) {
                                    if (data.d == "Success") {
                                        //alert("Update Successfully..!!");
                                        //var url = "Goods_Received_List.aspx";
                                        //window.location = url;
										$.confirm({
                                            title: 'Confirm!',
                                            content: 'Update Successfully..!!',
                                            type: 'green',
                                            typeAnimated: true,
                                            autoClose: 'cancel|8000',
                                            icon: 'fa fa-check',
                                            buttons: {
                                                tryAgain: {
                                                    text: 'OK',
                                                    btnClass: 'btn-green',
                                                    action: function () {
                                                        var url = "Goods_Received_List.aspx";
                                                        window.location = url;
                                                    }
                                                }
                                            }
                                        });
                                    }
                                    else {
                                        alert(data.d);
                                    }
                                },
                                error: function (data) {
                                    buttoncount = 0;
                                    alert(JSON.stringify(data));
                                }
                            });



                        }
                    }
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
                //$(document).on('change', '#ddlsupplier', function () {
                //    $('#grnPono').val('');
                //    var ddlSubVal = $(this).val();
                //    if (ddlSubVal != 0) {
                //        $.ajax({
                //            type: "POST",
                //            contentType: "application/json; charset=utf-8",
                //            url: "Goods_Received_Note.aspx/GetPONumber",
                //            data: "{'suppCode':'" + ddlSubVal + "'}",
                //            dataType: "json",
                //            success: function (data) {
                //                console.log(data.d);
                //                var ddlCustomers = $("#PonoSelect");
                //                ddlCustomers.empty().append('<option selected="selected" value="0">-Select-</option>');
                //                for (var k = 0; k < data.d.length; k++) {
                //                    ddlCustomers.append($("<option></option>").val(data.d[k]).html(data.d[k]));
                //                }

                //            },
                //            error: function (data) {
                //                alert(JSON.stringify(data));
                //            }
                //        });
                //    }
                //});
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
					 rjalert('Error!', 'Select Supplier Name!', 'error');
                       // alert('Select Supplier Name');
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
                            url: "Goods_Received_Note1.aspx/GetPOQTY",
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
            });
        </script>
    </head>
    <body>
        <form id="goodsfrm" runat="server">
            <div class="container" style="width: 100%; max-width: 100%; margin-top: -17px;">
                <asp:HiddenField ID="hdnmode" runat="server" />
                <asp:HiddenField ID="hdngrn_no" runat="server" />
                <asp:HiddenField ID="hdngrn_date" runat="server" />
                <asp:HiddenField ID="hdnsupp_code" runat="server" />

                <%--   <div class="form-group">
                        <label for="grnno" id="lblgrnno" class="col-sm-2 col-sm-offset-1  control-label"
                            style="display: none; font-weight: normal">
                            GRN No.</label>
                        <div class="col-sm-3">
                            <input type="text" name="grnno" id="grnNo" class="form-control txtblue" value="0" style="display: none;" />
                        </div>
                    </div>--%>

				 <div class="card" style="padding: 15px; border: 1px solid #dff2fbe6;">
                <div class="row">
                    <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                        <div class="form-group">
                            <label class="control-label " for="focusedInput">GRN Date</label>
                            <span style="color: Red">*</span>
                            <input type="date" name="grnDate" id="grnDate" autocomplete="off" class="form-control txtblue datetimepicker" />
                        </div>
                    </div>

                    <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                        <div class="form-group">
                            <label class="control-label " for="focusedInput">PO No</label><span style="color: Red">*</span>
                            <select id="PonoSelect" style="width: 100%;">
                                <option value="0">Select PO No</option>
                            </select>
                        </div>
                    </div>

                    <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                        <div class="form-group">
                            <label class="control-label " for="focusedInput">Supplier</label><span style="color: Red">*</span>
                            <select name="ddlsupplier" id="ddlsupplier" style="width: 100%" class="txtblue">
                                <option value="0">Select Supplier</option>
                            </select>
                        </div>
                    </div>
                </div>


                <div class="row">
                    <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                        <div class="form-group">
                            <label class="control-label " for="focusedInput">Entry Date</label>
                            <input type="text" name="txtEdate" id="txtEdate" autocomplete="off" class="form-control txtblue" readonly />
                        </div>
                    </div>

                    <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                        <div class="form-group">
                            <label class="control-label " for="focusedInput">PI No</label><span style="color: Red">*</span>
                            <input type="text" name="challenNo" autocomplete="off" id="challenNo" class="form-control txtblue" />
                        </div>
                    </div>

                    <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                        <div class="form-group">
                            <label class="control-label " for="focusedInput">Dispatch Date</label><span style="color: Red">*</span>
                            <input type="date" name="grnDisDate" autocomplete="off" id="grnDisDate" min="2020-06-09" class="form-control datetimepicker txtblue" />
                        </div>
                    </div>


                </div>
			</div>


                <%--          <div class="row">
                    <div class="form-group">
                        <label for="grnDate" class="col-sm-2 col-sm-offset-1  control-label" style="font-weight: normal">
                            GRN Date</label>
                        <div class="col-sm-3">
                            <input type="date" name="grnDate" id="grnDate" autocomplete="off" class="form-control datetimepicker" />
                        </div>
                        <label for="txtEdate" class="col-sm-2 control-label" style="font-weight: normal">
                            Entry Date</label>
                        <div class="col-sm-3">
                            <input type="text" name="txtEdate" id="txtEdate" autocomplete="off" class="form-control" readonly />
                        </div>
                    </div>
                </div>--%>
                <%--                <div class="row">
                    <div class="form-group">

                        <label for="grnPono" class="col-sm-2 col-sm-offset-1  control-label" style="font-weight: normal">
                            PO No.</label>
                        <div class="col-sm-3">
                            <select id="PonoSelect" style="width: 100%; float: left; height: 32px">
                                <option value="0">Select PO No</option>
                            </select>
                            <input type="text" name="grnpono" id="grnPono" autocomplete="off" style="width: 180px; margin-left: -199px; margin-top: 1px; border: none; float: left;" />
                        </div>
                        <label for="ddlsupplier1" class="col-sm-2 control-label" style="font-weight: normal">
                            Supplier</label>
                        <div class="col-sm-3 ">
                            <select class="form-control" name="ddlsupplier" id="ddlsupplier" style="width: 100%">
                                <option value="0">Select Supplier</option>
                            </select>
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
                            <input type="date" name="grnDisDate" autocomplete="off" id="grnDisDate" min="2020-06-09" class="form-control datetimepicker" />
                        </div>
                    </div>
                </div>--%>
                <div class="card" style="margin-top: -10px;">
                    <div class="card-body table-responsive">
                        <div style="white-space: nowrap">
                            <%-- Search&nbsp;&nbsp;<input type="text" autocomplete="off" id="tSearchOrd" style="width: 250px;" /> --%>
                            <label style="white-space: nowrap; margin-left: 57px; display: none;">Filter By&nbsp;&nbsp;<select id="txtfilter" name="ddfilter" style="width: 250px; display: none;"></select></label>
                        </div>
                        <div class="table-scroll">
                            <table class="table table-hover modules-table" id="ProductTable">
                                <thead class="text-warning" style="white-space: nowrap">
                                    <tr>
                                        <%--        <th style="width: 1%;">SlNo.</th>
                                        <th style="width: 10%;">PCode</th>
                                        <th style="width: 12%;">Product Name</th>
                                        <th style="width: 6%;">Order Qty</th>
                                        <th style="width: 7%;">Invoice Qty</th>
                                        <th style="width: 3%;">Free</th>
                                        <th style="width: 1%;">Discount Value</th>
                                        <th style="width: 6%;">Gross Value</th>
                                        <th style="width: 6%; padding: 0px 0px 9px 29px;">Tax_Amt</th>
                                        <th style="width: 6%;">Net Value</th>--%>

                                        <th>SlNo.</th>
                                        <th>PCode</th>
                                        <th>Product Name</th>
                                        <th>Order Qty</th>
                                        <th>Invoice Qty</th>
                                        <th>Free</th>
                                        <th>Discount Value</th>
                                        <th>Gross Value</th>
                                        <th>Tax_Amt</th>
                                        <th>Net Value</th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                        </div>
                        <span id="spn">Please Select PO No To View Product</span>
                    </div>
                </div>

                <div id="footer" style="padding: -6px 0px 0px 0px;">
                    <%--                <table>
                    <tbody>
                        <tr>
                            <td style="width: 3%; font-weight: bold;">Total</td>
                            <td style="width: 14%;"><div class="input-group"><div class="input-group-addon currency"><i class="fa fa-inr"></i></div><input data-cell="G1" id="all_tot"  data-format="0.00" class="form-control" readonly=""></div></td>
                            <td style="width: 9%;padding: 0px 0px 0px 14px;font-weight: bold;">Gst_Total</td>
                            <td style="width: 14%;"><div class="input-group"><div class="input-group-addon currency"><i class="fa fa-inr"></i></div><input data-cell="G1" id="all_gst_tot" data-format="0,0.00" class="form-control" readonly=""></div></td>
                            <td style="width: 9%;padding: 0px 0px 0px 14px;font-weight: bold;">Discount_total</td>
                            <td style="width: 14%;"><div class="input-group"><div class="input-group-addon currency"><i class="fa fa-inr"></i></div><input data-cell="G1" id="all_dis_tot" data-format="0,0.00" class="form-control"  readonly=""></div></td>
                            <td style="width: 8%;padding: 0px 0px 0px 17px;font-weight: bold;">Gross_Total</td>
                            <td style="width: 14%;"><div class="input-group"><div class="input-group-addon currency"><i class="fa fa-inr"></i></div><input data-cell="G1" id="all_gross_tot" data-format="0,0.00" class="form-control total1" readonly="" ></div></td>
                        </tr>
                    </tbody>
                </table>--%>
                </div>


                <br />
                <div class="container" style="width: 100%; max-width: 100%;">
                    <div class="row">
                        <div class="col-md-7 inputGroupContainer" style="padding-left: 0;">
                            <div class="card">
                                <div class="card-body table-responsive">
                                    <div style="white-space: nowrap; padding: 0px 0px 7px 7px; font-weight: 900;">View Offer Products</div>
                                    <div class="tableBodyScroll1">
                                        <table id="free_table" class="table table-hover" style="border-radius: 4px; width: 570px;">
                                            <thead class="text-warning">
                                                <tr>
                                                    <th style="width: 3%;">SlNo</th>
                                                    <th style="width: 13%;">Product Code </th>
                                                    <th style="width: 28%;">Product Name</th>
                                                    <th style="width: 14%;">Free</th>
                                                </tr>
                                            </thead>
                                            <tbody></tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-5 card" style="padding: 20px;">
                            <div class="row" style="display: none;">
                                <label for="ddldistributor" class="col-md-3 control-label" style="font-weight: normal">
                                    Received Location</label>
                                <div class="col-md-9 inputGroupContainer">
                                    <select class="selectpicker" name="ddldistributor" id="ddldistributor" style="width: 100%">
                                    </select>
                                </div>
                            </div>
                            <div class="row">
                                <label for="receivedby" class="col-md-4" style="font-weight: normal">
                                    Received By<span style="color: Red">*</span></label>
                                <div class="col-md-8 inputGroupContainer">
                                    <input type="text" name="receivedby" autocomplete="off" id="receivedby" class="form-control txtblue">
                                </div>
                            </div>
                            <div class="row" style="padding-top: 2%;">
                                <label for="authorised" class="col-md-4" style="font-weight: normal">
                                    Authorised By<span style="color: Red">*</span></label>
                                <div class="col-md-8 inputGroupContainer">
                                    <input type="text" name="authorised" autocomplete="off" id="authorised" class="form-control txtblue">
                                </div>
                            </div>
                            <div class="row">
                                <label for="remarks" class="col-md-2" style="font-weight: normal; padding-top: 42px;">
                                    Remarks
                                </label>
                                <div class="col-md-8 inputGroupContainer" style="padding: 16px 0px 0px 0px;">
                                    <textarea rows="3" cols="150" class="txtblue" style="width: 99%; resize: none; margin-left: 74px;" id="remarks"></textarea>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="row" style="text-align: center" style="padding-top: 10px;">
                        <div class="col-md-12 inputGroupContainer">
                            <a id="btndaysave" class="btn btn-primary btnsave txtblue" style="vertical-align: middle; font-size: 17px;"><span>Save</span></a>
                        </div>
                    </div>

                </div>
            </div>
        </form>
    </body>
    </html>
</asp:Content>
