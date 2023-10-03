<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="New_RateCardAllocation.aspx.cs" Inherits="MasterFiles_New_RateCardAllocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <meta charset="utf-8" />
        <title>Rate card Allocation</title>
        <style>
            .container {
                margin-top: -5px;
            }

            .nav-tabs {
                border-bottom: 0;
            }

                .nav-tabs .nav-item {
                    margin-bottom: 0;
                }

                .nav-tabs .nav-link {
                    border: 0;
                    color: #333;
                }

            /*.nav-tabs .nav-link.active {
                        color: #f00;
                        border: 0;
                        border-bottom: 2px solid #f00;
                    }*/

            .tab-content {
                padding: 20px;
                /*border: 1px solid #f00;*/
                border-top: 0;
            }

            .nav-tabs > li.active > a, .nav-tabs > li.active > a:hover, .nav-tabs > li.active > a:focus {
                color: #007491;
                font-weight: bold;
                cursor: default;
                background-color: #f0f3f4;
                border: 2px solid #f0f3f4;
                border-bottom-color: #19a4c6;
                height: 41px;
            }

            .cardcus {
                background-color: #fff;
                border-radius: 8px;
                box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.2);
            }


            .form-group {
                /*margin-bottom: 45px;*/
            }

            .input-group {
                position: relative;
                width: 100%;
            }

                .input-group input {
                    height: 45px;
                    border: none;
                    border-radius: 0;
                    box-shadow: none;
                    border-bottom: 2px solid #ccc;
                    padding: 8px 12px;
                    font-size: 16px;
                }

                .input-group .chosen-container {
                    height: 45px;
                    border: none;
                    border-radius: 0;
                    box-shadow: none;
                    border-bottom: 2px solid #ccc;
                    padding: 8px 12px;
                    font-size: 16px;
                }

                .input-group input:focus {
                    border-color: #007bff;
                    box-shadow: none;
                    outline: none;
                }

                .input-group .select2-container:focus {
                    border-color: #007bff;
                    box-shadow: none;
                    outline: none;
                }

            .control-label {
                position: absolute;
                top: 12px;
                left: 12px;
                font-size: 16px;
                color: #aaa;
                transition: all 0.2s ease-in-out;
                pointer-events: none;
            }

            .input-group input:focus + .control-label,
            .input-group input:not(:placeholder-shown) + .control-label {
                top: -21px;
                font-size: 12px;
                color: #007bff;
            }

            .input-group .select2-container:focus + .control-label,
            .input-group .select2-container:not(:placeholder-shown) + .control-label {
                top: -21px;
                font-size: 12px;
                color: #007bff;
            }

            .bar {
                position: absolute;
                bottom: 0;
                left: 0;
                width: 100%;
                height: 2px;
                background-color: #007bff;
                transform-origin: left;
                transform: scaleX(0);
                transition: transform 0.2s ease-in-out;
            }

            .input-group input:focus ~ .bar,
            .input-group input:not(:placeholder-shown) ~ .bar {
                transform: scaleX(1);
            }

            .chosen-container-active .chosen-choices {
                border: 0;
            }

            .chosen-container-multi .chosen-choices {
                border: 0
            }

            .chosen-container-single .chosen-single {
                border: 0
            }

            .chosen-container .chosen-choices {
                background-image: linear-gradient(#fff 1%, #fff 15%);
            }

            .chosen-container-multi .chosen-choices {
                background-image: linear-gradient(#fff 1%, #fff 15%);
            }

            .btnstyle {
                display: flex;
                flex-direction: row-reverse;
                margin-right: 20px;
                margin-bottom: 20px;
            }

            .select2-container {
                width: 100% !important;
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
        <link href="../css/select2.min.css" rel="stylesheet" />
        <script src="../js/select2.min.js"></script>
    </head>
    <body>

        <script type="text/javascript">
            $(document).ready(function () {
                $('.select2').select2();
                $('#ddl_rate').select2();
                $('#sddl_rate').select2();
                $('#ddl_state').select2();
                $('#sddl_state').select2();
                $('#ddl_District').select2();
                $('#ddl_distributor').select2();
                retrieveRate();
                sretrieveRate();
                $('#ddl_mode').on('change', function () {
                    clearfields();
                    $('#ddl_rate').empty().trigger('select2:updated');
                    $('#ddlSubdivision').val('');
                    $('#rddlSubdivision').val('');
                    $('#ddl_state').empty().trigger('select2:updated');
                    $('#ddl_District').empty().trigger('select2:updated');
                    $('#ddl_distributor').empty().trigger('select2:updated');
                    $('#ddl_retailer').empty().trigger('select2:updated');
                    retrieveRate();

                });
                $('#sddl_mode').on('change', function () {
                    clearfields();
                    $('#sddl_rate').empty().trigger('select2:updated');
                    $('#sddlSubdivision').val('');
                    $('#srddlSubdivision').val('');
                    $('#sddl_state').empty().trigger('select2:updated');
                    $('#sddl_District').empty().trigger('select2:updated');
                    $('#sddl_distributor').empty().trigger('select2:updated');
                    $('#ddl_superst').empty().trigger('select2:updated');
                    $('#ddl_distcat').empty().trigger('select2:updated');
                    sretrieveRate();
                });
                //distributor checkbox change
                $('#distchk').on('change', function () {
                    var distchk = $('#distchk').is(":checked");

                    if (distchk == true) {
                        var selectedOptionsCount = $('#ddl_District > option:selected').length;
                        var selectedstateCount = $('#ddl_state > option:selected').length;
                        if (selectedstateCount > 0) {
                            if (selectedOptionsCount > 0) {
                                $('#ddl_distributor option').prop('selected', true);
                                $('#ddl_distributor').trigger('change');
                            }
                            else {
                                alert('select district');
                                return false;
                            }
                        }
                        else {
                            alert('select state');
                            return false;
                        }
                    }
                    else {
                        $('#ddl_distributor option').prop('selected', false);
                        $('#ddl_distributor').trigger('change');
                    }
                });
                //state checkbox change
                $('#statechk').on('change', function () {
                    var distchk = $('#statechk').is(":checked");
                    if (distchk == true) {
                        $('#ddl_state option').prop('selected', true);
                        $('#ddl_state').trigger('change');
                    }
                    else {
                        $('#ddl_state option').prop('selected', false);
                        $('#ddl_state').trigger('change');
                    }
                });
                //sstate checkbox change
                $('#sstatechk').on('change', function () {
                    var distchk = $('#sstatechk').is(":checked");
                    if (distchk == true) {
                        $('#sddl_state option').prop('selected', true);
                        $('#sddl_state').trigger('change');
                    }
                    else {
                        $('#sddl_state option').prop('selected', false);
                        $('#sddl_state').trigger('change');
                    }
                });
                //superstockist checkbox change
                $('#stkchk').on('change', function () {
                    var distchk = $('#stkchk').is(":checked");
                    var selectedOptionsCount = $('#sddl_state > option:selected').length;
                    if (selectedOptionsCount > 0) {
                        if (distchk == true) {
                            $('#ddl_superst option').prop('selected', true);
                            $('#ddl_superst').trigger('change');
                        }
                        else {
                            $('#ddl_superst option').prop('selected', false);
                            $('#ddl_superst').trigger('change');
                        }
                    }
                    else {
                        alert('select State first');
                        return false;
                    }
                });
                //district checkbox change
                $('#districtChk').on('change', function () {
                    var distchk = $('#districtChk').is(":checked");
                    var selectedOptionsCount = $('#ddl_state > option:selected').length;
                    if (selectedOptionsCount > 0) {
                        if (distchk == true) {
                            $('#ddl_District option').prop('selected', true);
                            $('#ddl_District').trigger('change');
                        }
                        else {
                            $('#ddl_District option').prop('selected', false);
                            $('#ddl_District').trigger('change');
                        }
                    }
                    else {
                        alert('select State first');
                        return false;
                    }
                });
                //district checkbox change
                $('#rchk').on('change', function () {
                    var distchk = $('#rchk').is(":checked");
                    var selecteddistrictCount = $('#ddl_District > option:selected').length;
                    var selectedstateCount = $('#ddl_state > option:selected').length;
                    var selectedOptionsCount = $('#ddl_distributor > option:selected').length;
                    if (distchk == true) {

                        if (selectedstateCount > 0) {
                            if (selecteddistrictCount > 0) {
                                if (selectedOptionsCount > 0) {
                                    $('#ddl_retailer option').prop('selected', true);
                                    $('#ddl_retailer').trigger('change');


                                }
                                else {
                                    alert('select distributor');
                                    return false;

                                }
                            }
                            else {
                                alert('select district');
                                return false;
                            }
                        }
                        else {
                            alert('select state');
                            return false;
                        }



                    }
                    else {
                        $('#ddl_retailer option').prop('selected', false);
                        $('#ddl_retailer').trigger('change');
                    }
                });
                //all category clcik
                //district checkbox change
                $('#dchk').on('change', function () {
                    var distchk = $('#dchk').is(":checked");

                    var selectedstateCount = $('#sddl_state > option:selected').length;
                    var selectedOptionsCount = $('#ddl_superst > option:selected').length;
                    if (distchk == true) {

                        if (selectedstateCount > 0) {

                            if (selectedOptionsCount > 0) {
                                $('#ddl_distcat option').prop('selected', true);
                                $('#ddl_distcat').trigger('change');


                            }
                            else {
                                alert('select SuperStockist');
                                return false;

                            }

                        }
                        else {
                            alert('select state');
                            return false;
                        }



                    }
                    else {
                        $('#ddl_distcat option').prop('selected', false);
                        $('#ddl_distcat').trigger('change');
                    }
                });
                $('#ddl_rate').on('change', function () {
                    var rate = $('#ddl_rate').val();
                    if (rate == 'select') {
                        $("#rddlSubdivision").val('');
                        $("#ddlSubdivision").val('');
                        //  alert("Select Rate");
                        $('#ddl_rate').focus();
                        return false;
                    }

                    var subdiv = $('#rddlSubdivision').text();
                    //$('#divstate').show();
                    //$('.chkstate').show();
                    //$('#divterr').show();  
                    var mode = $('#ddl_mode option:selected').val();
                    clearfields();
                    if (mode == 'select') {
                        alert("Select Mode");
                        $('#ddl_mode').focus();
                        $('#ddl_rate').val($('#ddl_rate option:first').val()).trigger('change');
                        return false;
                    }
                    else {
                        $("#chkalldiv").show;
                        retrieveDivision(rate);
                        subdiv = $('#rddlSubdivision').text();
                        retrieveState(subdiv, 0, mode);
                    }
                    retrieveDivision(rate);
                    // statechng();
                   $('.spinnner_div').show();
                    setTimeout(chk_ratecard, 1000);
                    
                    //chk_ratecard();

                    $('#statechk').prop('checked', false);
                    $('#terrchk').prop('checked', false);
                    $('#distchk').prop('checked', false);
                    $('#catechk').prop('checked', false);

                });
                $('#sddl_rate').on('change', function () {
                    var rate = $('#sddl_rate').val();
                    if (rate == 'select') {
                        // alert("Select Rate");
                        $("#srddlSubdivision").val('');
                        $("#sddlSubdivision").val('');
                        $('#sddl_rate').focus();
                        return false;
                    }

                    var subdiv = $('#srddlSubdivision').text();
                    //$('#divstate').show();
                    //$('.chkstate').show();
                    //$('#divterr').show();  
                    var mode = $('#sddl_mode option:selected').val();
                    if (mode == 'select') {
                        alert("Select Mode");
                        $('#sddl_rate').val($('#sddl_rate option:first').val()).trigger('change');
                        $('#sddl_mode').focus();
                        return false;
                    }
                    else {
                        $("#schkalldiv").show;
                        sretrieveDivision(rate);
                        subdiv = $('#srddlSubdivision').text();
                        sretrieveState(subdiv, 0, mode);
                    }

                    //sstatechng();
                    // sclearfields();
                     $('.spinnner_div').show();
                    setTimeout(schk_ratecard, 1000);
                   // schk_ratecard();
                    $('#statechk').prop('checked', false);
                    $('#terrchk').prop('checked', false);
                    $('#distchk').prop('checked', false);
                    $('#catechk').prop('checked', false);

                });
                $('#ddl_state').on('change', function () {
                    //$('#divterr').show();
                    //$('.chkterr').show();
                    //$('#divdist').hide();
                    statechng();
                });
                $('#sddl_state').on('change', function () {
                    //$('#divterr').show();
                    //$('.chkterr').show();
                    //$('#divdist').hide();
                    sstatechng();
                });

                $('#ddl_District').on('change', function () {
                    $('#divdist').show();
                    //$('.chkdist').show();
                    District();
                });

                $('#ddl_distributor').on('change', function () {

                    //$('.chkdist').show();
                    changedistributor();
                });
                //superstockist change
                $('#ddl_superst').on('change', function () {

                    //$('.chkdist').show();
                    changedistributorcat();
                });

                $('#btnsave').on('click', function () {
                    var mode = $('#ddl_mode option:selected').val();
                    if (mode == 'select') {
                        alert("Select Mode");
                        $('#ddl_mode').focus();
                        return false;
                    }

                    var rate = $('#ddl_rate option:selected').val();
                    if (rate == 'select') {
                        alert("Select Rate");
                        $('#ddl_rate').focus();
                        return false;
                    }
                    var Subdivcode = $('#rddlSubdivision').text();
                    /*var Subdivcode = $('#ddl_division option:selected').val();
                    if (Subdivcode == 'select') {
                        alert("Select Division");
                        $('#ddl_division').focus();
                        return false;
                    }*/
                    //var plant = $('#ddl_plant option:selected').val();
                    /*if (plant == 'select') {
                        alert("Select Plant");
                        $('#ddl_plant').focus();
                        return false;
                    }*/
                    var state = '';
                    $('#ddl_state > option:selected').each(function () {
                        state += ',' + $(this).val();
                    });

                    //var territory = '';
                    //$('#ddl_territory > option:selected').each(function () {
                    //    territory += ',' + $(this).val();
                    //});
                    var distributor = '';
                    $('#ddl_distributor > option:selected').each(function () {
                        distributor += ',' + $(this).val();
                    });
                    var category = '';
                    $('#ddl_retailer > option:selected').each(function () {
                        category += ',' + $(this).val();
                    });
                    if (mode == '2') {
                        if (category == '') {
                            alert("select category");
                            return false;
                        }
                    }
                    if (state == '') {
                        alert("select state");
                        return false;
                    }
                    var ddl_District = '';
                    ddl_District = $('#ddl_District option:selected').val() || '';
                    if (ddl_District == '') {
                        alert("Select district");
                        $('#ddl_District').focus();
                        return false;
                    }
                    //if (territory == '') {
                    //    alert("select territory");
                    //    return false;
                    //}
                    if (distributor == '') {
                        alert("select distributor");
                        return false;
                    }

                    splrate = $('#splRatechk').is(":checked");
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "New_RateCardAllocation.aspx/saveRate",
                //data: "{'DivCode':'<%=Session["div_code"]%>','SubDivCode':'" + Subdivcode + "','State':'" + state + "','Territory':'" + territory + "','Distributor':'" + distributor + "','Rate':'" + rate + "','Mode':'" + mode + "','Plant_id':'" + plant+ "','Splrate':'" + splrate + "'}",
                        data: "{'DivCode':'<%=Session["div_code"]%>','SubDivCode':'" + Subdivcode + "','State':'" + state + "','Distributor':'" + distributor + "','Rate':'" + rate + "','Mode':'" + mode + "','Splrate':'" + splrate + "','category':'" + category + "'}",
                        dataType: "json",
                        success: function (data) {
                            status = data.d;
                            alert("saved successfully");
                            //$('#ddl_mode [value=select]').attr('selected', 'true');
                            //clearfields();
                            //$('#ddl_territory').empty();
                            //$('#ddlSubdivision').val('');
                            //retrieveRate();
                            //// retrieveDivision();
                            //retrievePlant();
                            location.reload();
                        },
                        error: function (result) {
                            alert(JSON.stringify(result));
                        }
                    });
                });

                //ss btn
                $('#sbtnsave').on('click', function () {
                    var mode = $('#sddl_mode option:selected').val();
                    if (mode == 'select') {
                        alert("Select Mode");
                        $('#sddl_mode').focus();
                        return false;
                    }

                    var rate = $('#sddl_rate option:selected').val();
                    if (rate == 'select') {
                        alert("Select Rate");
                        $('#sddl_rate').focus();
                        return false;
                    }

                    var Subdivcode = $('#srddlSubdivision').text();

                    var state = '';
                    $('#sddl_state > option:selected').each(function () {
                        state += ',' + $(this).val();
                    });
                    //var territory = '';
                    //$('#ddl_territory > option:selected').each(function () {
                    //    territory += ',' + $(this).val();
                    //});
                    var ddl_superst = '';
                    $('#ddl_superst > option:selected').each(function () {
                        ddl_superst += ',' + $(this).val();
                    });

                    if (state == '') {
                        alert("select state");
                        return false;
                    }
                    //if (territory == '') {
                    //    alert("select territory");
                    //    return false;
                    //}
                    if (state == '') {
                        alert("select state");
                        return false;
                    }
                    if (ddl_superst == '') {
                        alert("select SuperStockist");
                        return false;
                    }
                    var category = '';
                    $('#ddl_distcat > option:selected').each(function () {
                        category += ',' + $(this).val();
                    });
                    if (mode == '2') {
                        if (category == '') {
                            alert("select category");
                            return false;
                        }
                    }

                    splrate = $('#splRatechk').is(":checked");
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "New_RateCardAllocation.aspx/ssaveRate",
                //data: "{'DivCode':'<%=Session["div_code"]%>','SubDivCode':'" + Subdivcode + "','State':'" + state + "','Territory':'" + territory + "','Distributor':'" + distributor + "','Rate':'" + rate + "','Mode':'" + mode + "','Plant_id':'" + plant+ "','Splrate':'" + splrate + "'}",
                        data: "{'DivCode':'<%=Session["div_code"]%>','SubDivCode':'" + Subdivcode + "','State':'" + state + "','sstockist':'" + ddl_superst + "','Rate':'" + rate + "','Mode':'" + mode + "','Splrate':'" + splrate + "','category':'" + category + "'}",
                        dataType: "json",
                        success: function (data) {
                            status = data.d;
                            alert("saved successfully");
                            location.reload();
                            //$('#ddl_mode [value=select]').attr('selected', 'true');
                            //clearfields();
                            //$('#ddl_territory').empty();
                            //$('#ddlSubdivision').val('');
                            //retrieveRate();
                            //// retrieveDivision();
                            //retrievePlant();
                        },
                        error: function (result) {
                            alert(JSON.stringify(result));
                        }
                    });
                });


            });
            $(function () {
                $('.nav-tabs a').click(function (e) {
                    e.preventDefault();
                    $(this).tab('show');
                });
            });
            function retrieveRate() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "New_RateCardAllocation.aspx/getRate",
                    data: "{'divcode':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        $('#ddl_rate').empty();
                        ddlrate = JSON.parse(data.d) || [];
                        data_bind($('#ddl_rate'), ddlrate, 'Price_list_Sl_No', 'Price_list_Name', 'No');
                        $('#ddl_rate').trigger('select2:updated');
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            }
            function sretrieveRate() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "New_RateCardAllocation.aspx/getRate",
                    data: "{'divcode':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        $('#sddl_rate').empty();
                        ddlrate = JSON.parse(data.d) || [];
                        data_bind($('#sddl_rate'), ddlrate, 'Price_list_Sl_No', 'Price_list_Name', 'No');
                        $('#sddl_rate').trigger('select2:updated');
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            }
            function data_bind(id, dataArray, optionVal, optionText, allselectoption) {
                if (dataArray.length > 0) {
                    if (optionVal == "Price_list_Sl_No") {
                        id.empty().append('<option selected value=select>Select Rate</option>');
                    }
                    else if (optionVal == "subdivision_code") {
                        id.empty().append('<option selected value=select>Select Division</option>');
                    }

                    else if (optionVal == "Plant_Id") {
                        id.empty().append('<option selected value=0>Select Plant</option>');
                    }
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
            }
            function retrieveDivision(ratecard) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "New_RateCardAllocation.aspx/getDivision",
                    data: "{'divcode':'<%=Session["div_code"]%>','ratecard':'" + ratecard + "'}",
                    dataType: "json",
                    success: function (data) {
                        // $('#ddl_division').empty();
                        ddldivision = JSON.parse(data.d) || [];
                        if (ddldivision.length > 0) {
                            $('#rddlSubdivision').text(ddldivision[0].sub_div_code);
                            $('#ddlSubdivision').val(ddldivision[0].subdivision_name);
                        }

                        //data_bind($('#ddl_division'), ddldivision, 'subdivision_code', 'subdivision_name', 'No');
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            }
            //superstockist
            function sretrieveDivision(ratecard) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "New_RateCardAllocation.aspx/getDivision",
                    data: "{'divcode':'<%=Session["div_code"]%>','ratecard':'" + ratecard + "'}",
                    dataType: "json",
                    success: function (data) {
                        // $('#ddl_division').empty();
                        ddldivision = JSON.parse(data.d) || [];
                        if (ddldivision.length > 0) {
                            $('#srddlSubdivision').text(ddldivision[0].sub_div_code);
                            $('#sddlSubdivision').val(ddldivision[0].subdivision_name);
                        }

                        //data_bind($('#ddl_division'), ddldivision, 'subdivision_code', 'subdivision_name', 'No');
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            }
            function retrieveState(subdiv, plant_id, mode) {
                //if (mode == '1') {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "New_RateCardAllocation.aspx/getState",
                    data: "{'subdivcode':'" + subdiv + "','plant_id':'" + plant_id + "'}",
                    dataType: "json",
                    success: function (data) {
                        $('#ddl_state').empty();
                        ddlstate = JSON.parse(data.d) || [];

                        data_bind($('#ddl_state'), ddlstate, 'State_Code', 'StateName', 'No');
                        $('#ddl_state').trigger('select2:updated');
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
                //}
            }
            function sretrieveState(subdiv, plant_id, mode) {
                //if (mode == '1') {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "New_RateCardAllocation.aspx/getState",
                    data: "{'subdivcode':'" + subdiv + "','plant_id':'" + plant_id + "'}",
                    dataType: "json",
                    success: function (data) {
                        $('#sddl_state').empty();
                        ddlstate = JSON.parse(data.d) || [];

                        data_bind($('#sddl_state'), ddlstate, 'State_Code', 'StateName', 'No');
                        $('#sddl_state').trigger('select2:updated');
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
                //}
            }
            function statechng() {
                // clearfields()
                $('.chkalldiv').show();
                $('#rchk').prop('checked', false);
                $('#distchk').prop('checked', false);
                // $('#statechk').prop('checked', false);
                $('#districtChk').prop('checked', false);
                statemulti = [];
                statemultip = '';
                //$('.chkterr').show();            
                //$('#divterr').show();           
                $('.chkDistrict').show();
                $('#divDistrict').show();
                $('#ddl_District').empty().trigger('select2:updated');
                $('#ddl_distributor').empty().trigger('select2:updated');
                $('#ddl_retailer').empty().trigger('select2:updated');
                //$('.chkdist').hide();
                //$('#divdist').hide();
                //  $('.chkcat').hide();
                //  $('#divcate').hide();
                //var Subdivcode = $('#ddl_division option:selected').val();
                var Subdivcode = $('#rddlSubdivision').text();

                //var plant = $('#ddl_plant option:selected').val();

                //if (isNaN(plant)) {
                //    plant = '';
                //}

                $('#ddl_state > option:selected').each(function () {
                    statemultip += ',' + $(this).val();
                    statemulti.push($(this).val());
                });

                //var districtCheck = $('#districtChk').is(":checked");
                var districtCheck = '';
                $('#ddl_District').empty();
                retrieveDistrict(statemultip, districtCheck);
                //$('#ddl_District').multiselect('reload');


            }
            function sstatechng() {
                // clearfields()
                $('.schkalldiv').show();
                $('#srchk').prop('checked', false);
                $('#sdistchk').prop('checked', false);
                // $('#statechk').prop('checked', false);

                sstatemulti = [];
                sstatemultip = '';
                //$('.chkterr').show();            
                //$('#divterr').show();           
                var Subdivcode = $('#srddlSubdivision').text();

                //var plant = $('#ddl_plant option:selected').val();

                //if (isNaN(plant)) {
                //    plant = '';
                //}

                $('#sddl_state > option:selected').each(function () {
                    sstatemultip += ',' + $(this).val();
                    sstatemulti.push($(this).val());
                });

                //var districtCheck = $('#districtChk').is(":checked");
                var districtCheck = '';
                $('#ddl_superst').empty();
                retrievestockist(sstatemultip, Subdivcode);
                //$('#ddl_District').multiselect('reload');


            }
            function retrievestockist(statemultip, Subdivcode) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "New_RateCardAllocation.aspx/get_SS",
                    //data: "{'subdivcode':'" + subdiv + "','plant_id':'" + plant_id + "'}",
                    data: "{'state_code':'" + statemultip + "','subdivcode':'" + Subdivcode + "'}",
                    dataType: "json",
                    success: function (data) {
                        $('#ddl_superst').empty();
                        ddl_superst = JSON.parse(data.d) || [];

                        //if (districtCheck == true) {
                        //    data_bind($('#ddl_District'), ddlDistrict, 'Dist_code', 'Dist_name', 'yes');

                        //}
                        //else
                        data_bind($('#ddl_superst'), ddl_superst, 'Stockist_Code', 'Stockist_Name', 'No');
                        $('#ddl_superst').trigger('select2:updated');
                        //data_bind($('#ddl_District'), ddlDistrict, 'Dist_code', 'Dist_name', 'No');
                        //$('#ddl_District').multiselect({
                        //    columns: 3,
                        //    placeholder: 'Select District',
                        //    search: true,
                        //    searchOptions: {
                        //        'default': 'Search District'
                        //    },
                        //    selectAll: true
                        //}).multiselect('reload');
                        //$('#ddl_District-options ul').css('column-count', '3');
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            }
            function chk_ratecard() {
                var mode = $('#ddl_mode option:selected').val();
                var rate = $('#ddl_rate').val();
                var type = 'distributor';
                var ddlchk = [];
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "New_RateCardAllocation.aspx/chkratecard",
                    //data: "{'subdivcode':'" + subdiv + "','plant_id':'" + plant_id + "'}",
                    data: "{'rate':'" + rate + "','mode':'" + mode + "','type':'" + type + "'}",
                    dataType: "json",
                    success: function (data) {
                        ddlchk = JSON.parse(data.d) || [];
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                        $('.spinnner_div').hide();
                    }
                });
                if (ddlchk.length > 0) {

                    var st = ddlchk[0].State_Codes || '';
                    var dt = ddlchk[0].Dist_Codes || '';
                    var stk = ddlchk[0].Stockist_Codes || '';
                    if (st != '') {
                        var stArray = st.split(',');
                        stArray = stArray.map(function (element) {
                            return element.trim();
                        });
                        $('#ddl_state option').each(function () {
                            var optionValue = $(this).val();
                            if (stArray.includes(optionValue)) {
                                $(this).prop('selected', true);
                            }
                        });
                        $('#ddl_state').trigger('change');
                        if (dt != '') {
                            var dtArray = dt.split(',');
                            dtArray = dtArray.map(function (element) {
                                return element.trim();
                            });
                            $('#ddl_District option').each(function () {
                                var optionValue = $(this).val();
                                if (dtArray.includes(optionValue)) {
                                    $(this).prop('selected', true);
                                }
                            });
                            $('#ddl_District').trigger('change');
                        }
                        if (stk != '') {
                            var stkArray = stk.split(',');
                            stkArray = stkArray.map(function (element) {
                                return element.trim();
                            });
                            $('#ddl_distributor option').each(function () {
                                var optionValue = $(this).val();
                                if (stkArray.includes(optionValue)) {
                                    $(this).prop('selected', true);
                                }
                            });
                            $('#ddl_distributor').trigger('change');
                            if (mode == '2') {
                                var stkcat = ddlchk[0].cat_codes || '';
                                var stkcatArray = stkcat.split(',');
                                stkcatArray = stkcatArray.map(function (element) {
                                    return element.trim();
                                });
                                $('#ddl_retailer option').each(function () {
                                    var optionValue = $(this).val();
                                    if (stkcatArray.includes(optionValue)) {
                                        $(this).prop('selected', true);
                                    }
                                });
                                $('#ddl_retailer').trigger('change');
                            }
                        }
                    }


                }
                $('.spinnner_div').hide();
            }
            function schk_ratecard() {
                var mode = $('#sddl_mode option:selected').val();
                var rate = $('#sddl_rate').val();
                var type = 'superstockist';
                var ddlchk = [];
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "New_RateCardAllocation.aspx/chkratecard",
                    //data: "{'subdivcode':'" + subdiv + "','plant_id':'" + plant_id + "'}",
                    data: "{'rate':'" + rate + "','mode':'" + mode + "','type':'" + type + "'}",
                    dataType: "json",
                    success: function (data) {
                        ddlchk = JSON.parse(data.d) || [];
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                         $('.spinnner_div').hide();
                    }
                });
                if (ddlchk.length > 0) {

                    var st = ddlchk[0].State_Codes || '';
                    var stk = ddlchk[0].Stockist_Codes || '';
                    if (st != '') {
                        var stArray = st.split(',');
                        stArray = stArray.map(function (element) {
                            return element.trim();
                        });
                        $('#sddl_state option').each(function () {
                            var optionValue = $(this).val();
                            if (stArray.includes(optionValue)) {
                                $(this).prop('selected', true);
                            }
                        });
                        $('#sddl_state').trigger('change');

                        if (stk != '') {
                            var stkArray = stk.split(',');
                            stkArray = stkArray.map(function (element) {
                                return element.trim();
                            });
                            $('#ddl_superst option').each(function () {
                                var optionValue = $(this).val();
                                if (stkArray.includes(optionValue)) {
                                    $(this).prop('selected', true);
                                }
                            });
                            $('#ddl_superst').trigger('change');
                            if (mode == '2') {
                                var stkcat = ddlchk[0].cat_codes || '';
                                var stkcatArray = stkcat.split(',');
                                stkcatArray = stkcatArray.map(function (element) {
                                    return element.trim();
                                });
                                $('#ddl_distcat option').each(function () {
                                    var optionValue = $(this).val();
                                    if (stkcatArray.includes(optionValue)) {
                                        $(this).prop('selected', true);
                                    }
                                });
                                $('#ddl_distcat').trigger('change');
                            }
                        }
                    }


                }
                 $('.spinnner_div').hide();
            }
            function retrieveDistrict(statemultip, districtCheck) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "New_RateCardAllocation.aspx/getDistrict",
                    //data: "{'subdivcode':'" + subdiv + "','plant_id':'" + plant_id + "'}",
                    data: "{'state_code':'" + statemultip + "'}",
                    dataType: "json",
                    success: function (data) {
                        $('#ddl_District').empty();
                        ddlDistrict = JSON.parse(data.d) || [];

                        //if (districtCheck == true) {
                        //    data_bind($('#ddl_District'), ddlDistrict, 'Dist_code', 'Dist_name', 'yes');

                        //}
                        //else
                        data_bind($('#ddl_District'), ddlDistrict, 'Dist_code', 'Dist_name', 'No');
                        $('#ddl_District').trigger('select2:updated');
                        //data_bind($('#ddl_District'), ddlDistrict, 'Dist_code', 'Dist_name', 'No');
                        //$('#ddl_District').multiselect({
                        //    columns: 3,
                        //    placeholder: 'Select District',
                        //    search: true,
                        //    searchOptions: {
                        //        'default': 'Search District'
                        //    },
                        //    selectAll: true
                        //}).multiselect('reload');
                        //$('#ddl_District-options ul').css('column-count', '3');
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            }

            function District() {
                Districtmultip = [];
                DistrictCode = '';
                var selectedOptionsCount = $('#ddl_District > option:selected').length;
                if (selectedOptionsCount == 0) {
                    $("#distributordiv").hide();
                    $("#retailerdiv").hide();
                }
                $('#ddl_District > option:selected').each(function () {
                    Districtmultip.push($(this).val());
                    DistrictCode += $(this).val() + ',';
                });
                var subdiv = $('#rddlSubdivision').text();
                var plant = $('#ddl_plant option:selected').val();
                if (isNaN(plant)) {
                    plant = '';
                }
                var mode = $('#ddl_mode option:selected').val();
                if (mode == 'select') {
                    alert("Select Mode");
                    $('#ddl_mode').focus();
                    return false;
                }
                retrieveDistributor(subdiv, DistrictCode, plant, mode);
            }
            function retrieveDistributor(subdiv, terricode, plant, mode) {

                $("#retailerdiv").hide();
                $("#distributordiv").show();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "New_RateCardAllocation.aspx/getDistributor",
                    //data: "{'subdivcode':'" + subdiv + "','terricode':'" + terricode + "','plant_id':'" + plant + "'}",
                    data: "{'subdivcode':'" + subdiv + "','terricode':'" + terricode + "'}",
                    dataType: "json",
                    success: function (data) {
                        $('#ddl_distributor').empty();
                        ddldistributor = JSON.parse(data.d) || [];
                        //if ($('#distchk').is(":checked")) {
                        //    data_bind($('#ddl_distributor'), ddldistributor, 'Stockist_Code', 'Stockist_Name', 'yes');
                        //    //$('.chkcat').show();
                        //}
                        //else {
                        data_bind($('#ddl_distributor'), ddldistributor, 'Stockist_Code', 'Stockist_Name', 'No');
                        $('#ddl_distributor').trigger('select2:updated');

                        //}

                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });


            }

            function changedistributor() {
                var mode = $('#ddl_mode option:selected').val();
                if (mode == '2') {
                    var selectedOptionsCount = $('#ddl_distributor > option:selected').length;
                    if (selectedOptionsCount > 0) {
                        $("#retailerdiv").show();
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            url: "New_RateCardAllocation.aspx/getcategory",
                            //data: "{'subdivcode':'" + subdiv + "','terricode':'" + terricode + "','plant_id':'" + plant + "'}",
                            //data: "{'subdivcode':'" + subdiv + "','terricode':'" + terricode + "'}",
                            dataType: "json",
                            success: function (data) {
                                $('#ddl_retailer').empty();
                                ddl_retailer = JSON.parse(data.d) || [];
                                //if ($('#distchk').is(":checked")) {
                                //    data_bind($('#ddl_distributor'), ddldistributor, 'Stockist_Code', 'Stockist_Name', 'yes');
                                //    //$('.chkcat').show();
                                //}
                                //else {
                                data_bind($('#ddl_retailer'), ddl_retailer, 'Doc_Cat_Code', 'Doc_Cat_Name', 'No');
                                $('#ddl_retailer').trigger('select2:updated');
                                //}
                                //$('#ddl_distributor').multiselect({
                                //    columns: 3,
                                //    placeholder: 'Select Distributor',
                                //    search: true,
                                //    searchOptions: {
                                //        'default': 'Search Distributor'
                                //    },
                                //    selectAll: true
                                //}).multiselect('reload');
                                //$('#ddl_distributor-options ul').css('column-count', '3');
                            },
                            error: function (result) {
                                alert(JSON.stringify(result));
                            }
                        });
                    }
                    else {
                        $("#retailerdiv").hide();
                    }

                }
            }
            function changedistributorcat() {
                var mode = $('#sddl_mode option:selected').val();
                if (mode == '2') {
                    var selectedOptionsCount = $('#ddl_superst > option:selected').length;
                    if (selectedOptionsCount > 0) {
                        $("#disdiv").show();
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            url: "New_RateCardAllocation.aspx/getcategory",
                            //data: "{'subdivcode':'" + subdiv + "','terricode':'" + terricode + "','plant_id':'" + plant + "'}",
                            //data: "{'subdivcode':'" + subdiv + "','terricode':'" + terricode + "'}",
                            dataType: "json",
                            success: function (data) {
                                $('#ddl_distcat').empty();
                                ddl_retailer = JSON.parse(data.d) || [];
                                //if ($('#distchk').is(":checked")) {
                                //    data_bind($('#ddl_distributor'), ddldistributor, 'Stockist_Code', 'Stockist_Name', 'yes');
                                //    //$('.chkcat').show();
                                //}
                                //else {
                                data_bind($('#ddl_distcat'), ddl_retailer, 'Doc_Cat_Code', 'Doc_Cat_Name', 'No');
                                $('#ddl_distcat').trigger('select2:updated');
                                //}
                                //$('#ddl_distributor').multiselect({
                                //    columns: 3,
                                //    placeholder: 'Select Distributor',
                                //    search: true,
                                //    searchOptions: {
                                //        'default': 'Search Distributor'
                                //    },
                                //    selectAll: true
                                //}).multiselect('reload');
                                //$('#ddl_distributor-options ul').css('column-count', '3');
                            },
                            error: function (result) {
                                alert(JSON.stringify(result));
                            }
                        });
                    }
                    else {
                        $("#disdiv").hide();
                    }

                }
            }
            function clearfields() {
                $('.chkalldiv').show();
                $('#rchk').prop('checked', false);
                $('#distchk').prop('checked', false);
                $('#statechk').prop('checked', false);
                $('#districtChk').prop('checked', false);

            }
        </script>
        <form id="form1" runat="server">
            <div class="spinnner_div" style="display: none;">
                <div class="spinner" style="position: absolute; left: 525px; top: 133px;">
                    <div class="rect1" style="background: #1a60d3;"></div>
                    <div class="rect2" style="background: #DB4437;"></div>
                    <div class="rect3" style="background: #F4B400;"></div>
                    <div class="rect4" style="background: #0F9D58;"></div>
                    <div class="rect5" style="background: orangered;"></div>
                </div>
            </div>
            <div class="container">
                <ul class="nav nav-tabs">
                    <li class="nav-item active ">
                        <a class="nav-link active" href="#dist-tab">Distributor </a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="#superst-tab">Super Stockist</a>
                    </li>
                </ul>
                <div class="tab-content">
                    <%-- first tab --%>
                    <div id="dist-tab" class="tab-pane active">
                        <div class="row m-0" style="max-width: 1100px">
                            <%-- left div --%>
                            <div class="col-lg-6 col-sm-12 col-md-6 ">
                                <div class="card" style="padding: 10px; margin: 0">

                                    <%--<div class="form-group">
                                        <label for="textbox" class="control-label">Enter text:</label>
                                        <div class="input-group">
                                            <input type="text" class="form-control" id="textbox" name="textbox" required>
                                            <div class="bar"></div>
                                        </div>
                                    </div>--%>
                                    <div class="form-group" style="margin-top: 9px;">
                                        <div class="input-group">
                                            <%--<input type="text" class="form-control" name="name" required>--%>
                                            <select class="form-control select2" name="name" id="ddl_mode">
                                                <option value="select">Select Mode</option>
                                                <option value="1">Distributor</option>
                                                <option value="2">Retailer Category</option>
                                            </select>
                                            <label for="name" class="control-label">Mode</label>
                                            <%--<span class="bar"></span>--%>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="input-group">
                                            <%--<input type="text" class="form-control" name="name" required>--%>
                                            <select class="form-control select2" name="name" id="ddl_rate">
                                                <option value="select">Select Rate</option>

                                            </select>
                                            <label for="name" class="control-label">Rate</label>
                                            <%--<span class="bar"></span>--%>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="input-group">
                                            <input type="text" class="form-control" id="rddlSubdivision" style="display: none;" name="name" required>
                                            <input type="text" class="form-control" id="ddlSubdivision" name="name" required disabled autocomplete="off">

                                            <label for="name" class="control-label">Division</label>
                                            <%--<span class="bar"></span>--%>
                                        </div>
                                    </div>
                                    <div class="chkalldiv" style="margin: auto; display: none;">
                                        <input type="checkbox" id="statechk" class="chkstate" name="dstatechk" value="1" />
                                        <label for="vehicle1" class="chkstate" style="padding-right: 20px;">All state</label>

                                        <input type="checkbox" class="chkDistrict" id="districtChk" name="districtchk" value="2" />
                                        <label for="district" class="chkDistrict" style="padding-right: 20px;">All District</label>

                                        <input type="checkbox" class="chkdist" id="distchk" name="ddistchk" value="3" />
                                        <label for="vehicle3" class="chkdist" id="dischk">All Distributor</label>

                                        <input type="checkbox" class="chkret" id="rchk" name="ddistchk" value="3" />
                                        <label for="vehicle3" class="chkdist" id="retchk">All Category</label>
                                    </div>
                                    <div class="form-group">
                                        <div class="input-group">
                                            <%--<input type="text" class="form-control" name="name" required>--%>
                                            <select class="form-control select2" name="name" id="ddl_state" multiple>
                                                <option value="" disabled>Select State</option>

                                            </select>
                                            <label for="name" class="control-label">State</label>
                                            <%--<span class="bar"></span>--%>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="input-group">
                                            <%--<input type="text" class="form-control" name="name" required>--%>
                                            <select class="form-control select2" name="name" id="ddl_District" multiple>
                                                <option value="select" disabled>Select District</option>

                                            </select>
                                            <label for="name" class="control-label">District</label>
                                            <%--<span class="bar"></span>--%>
                                        </div>
                                    </div>
                                    <div class="form-group" id="distributordiv" style="display: none;">
                                        <div class="input-group">
                                            <%--<input type="text" class="form-control" name="name" required>--%>
                                            <select class="form-control select2" name="name" id="ddl_distributor" multiple>
                                                <option value="" disabled>Select Distributor</option>

                                            </select>
                                            <label for="name" class="control-label">Distributor</label>
                                            <%--<span class="bar"></span>--%>
                                        </div>
                                    </div>
                                    <div class="form-group" id="retailerdiv" style="display: none;">
                                        <div class="input-group">
                                            <%--<input type="text" class="form-control" name="name" required>--%>
                                            <select class="form-control select2" name="name" id="ddl_retailer" multiple>
                                                <option value="" disabled>Select Category</option>

                                            </select>
                                            <label for="name" class="control-label">Retailer Category</label>
                                            <%--<span class="bar"></span>--%>
                                        </div>
                                    </div>
                                    <div class="row btnstyle">
                                        <button type="button" class="btn btn-primary col-md-offset-2" style="width: 120px;" id="btnsave">Save</button>
                                    </div>
                                </div>

                            </div>
                            <%-- left div --%>
                            <%-- right div --%>
                            <div class="col-lg-6 col-sm-12 col-md-6 ">
                                <div class="card" style="display: none;">
                                </div>
                            </div>
                            <%-- right div --%>
                        </div>
                    </div>
                    <%-- first tab --%>
                    <%-- Second tab --%>
                    <div id="superst-tab" class="tab-pane">
                        <div class="row m-0" style="max-width: 1100px">
                            <%-- left div --%>
                            <div class="col-lg-6 col-sm-12 col-md-6 ">
                                <div class="card" style="padding: 10px; margin: 0">

                                    <%--<div class="form-group">
                                        <label for="textbox" class="control-label">Enter text:</label>
                                        <div class="input-group">
                                            <input type="text" class="form-control" id="textbox" name="textbox" required>
                                            <div class="bar"></div>
                                        </div>
                                    </div>--%>
                                    <div class="form-group" style="margin-top: 9px;">
                                        <div class="input-group">
                                            <%--<input type="text" class="form-control" name="name" required>--%>
                                            <select class="form-control select2" name="name" id="sddl_mode">
                                                <option value="">Select Mode</option>
                                                <option value="1">Superstockist</option>
                                                <option value="2">Distributor Category</option>
                                            </select>
                                            <label for="name" class="control-label">Mode</label>
                                            <%--<span class="bar"></span>--%>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="input-group">
                                            <%--<input type="text" class="form-control" name="name" required>--%>
                                            <select class="form-control select2" name="name" id="sddl_rate">
                                                <option value="">Select rate</option>

                                            </select>
                                            <label for="name" class="control-label">Rate</label>
                                            <%--<span class="bar"></span>--%>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="input-group">
                                            <input type="text" class="form-control" id="srddlSubdivision" style="display: none;" name="name" required>
                                            <input type="text" class="form-control" id="sddlSubdivision" name="name" disabled required autocomplete="off">

                                            <label for="name" class="control-label">Division</label>
                                            <%--<span class="bar"></span>--%>
                                        </div>
                                    </div>
                                    <div class="chkalldiv" style="margin: auto; display: none;">
                                        <input type="checkbox" id="sstatechk" class="chkstate" name="dstatechk" value="1" />
                                        <label for="vehicle1" class="chkstate" style="padding-right: 20px;">All state</label>

                                        <%-- <input type="checkbox" class="chkDistrict" id="sdistrictChk" name="districtchk" value="2" />
                                        <label for="district" class="chkDistrict" style="padding-right: 20px;">All District</label>--%>

                                        <input type="checkbox" class="chkdist" id="stkchk" name="ddistchk" value="3" />
                                        <label for="vehicle3" class="chkdist" id="sstkchk">All Superstockist</label>

                                        <input type="checkbox" class="chkret" id="dchk" name="ddistchk" value="3" />
                                        <label for="vehicle3" class="chkdist" id="dischk">All Category</label>
                                    </div>
                                    <div class="form-group">
                                        <div class="input-group">
                                            <%--<input type="text" class="form-control" name="name" required>--%>
                                            <select class="form-control select2" name="name" id="sddl_state" multiple>
                                                <option value="" disabled>Select State</option>

                                            </select>
                                            <label for="name" class="control-label">State</label>
                                            <%--<span class="bar"></span>--%>
                                        </div>
                                    </div>
                                    <%--<div class="form-group">
                                        <div class="input-group">
                                            <%--<input type="text" class="form-control" name="name" required>--%>
                                    <%--<select class="form-control select2" name="name" id="ddl_District" multiple>
                                                <option value="">Select Mode</option>
                                                <option value="1">chennai</option>
                                                <option value="2">tambaram</option>
                                            </select>
                                            <label for="name" class="control-label">District</label>
                                            <%--<span class="bar"></span>
                                        </div>
                                    </div>--%>
                                    <div class="form-group" id="superstkdiv">
                                        <div class="input-group">
                                            <%--<input type="text" class="form-control" name="name" required>--%>
                                            <select class="form-control select2" name="name" id="ddl_superst" multiple>
                                                <option value="" disabled>Select Superstockist</option>

                                            </select>
                                            <label for="name" class="control-label">Super Stockist</label>
                                            <%--<span class="bar"></span>--%>
                                        </div>
                                    </div>
                                    <div class="form-group" id="disdiv" style="display: none;">
                                        <div class="input-group">
                                            <%--<input type="text" class="form-control" name="name" required>--%>
                                            <select class="form-control select2" name="name" id="ddl_distcat" multiple>
                                                <option value="">Select Mode</option>
                                            </select>
                                            <label for="name" class="control-label">Distributor Category</label>
                                            <span class="bar"></span>
                                        </div>
                                    </div>
                                    <div class="row btnstyle">
                                        <button type="button" class="btn btn-primary col-md-offset-2" style="width: 120px;" id="sbtnsave">Save</button>
                                    </div>
                                </div>

                            </div>
                            <%-- left div --%>
                            <%-- right div --%>
                            <div class="col-lg-6 col-sm-12 col-md-6 ">
                                <div class="card" style="display: none">
                                </div>
                            </div>
                            <%-- right div --%>
                        </div>
                    </div>
                    <%-- Second tab --%>
                </div>
            </div>

        </form>

    </body>
    </html>
</asp:Content>

