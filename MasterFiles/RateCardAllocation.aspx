<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="RateCardAllocation.aspx.cs" Inherits="MasterFiles_RateCardAllocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <meta charset="utf-8" />
        <title>Rate card Allocation</title>
        <%--<link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />--%>
        <link href="../css/jquery.multiselect.css" rel="stylesheet" />
    </head>
    <body>
        <script type="text/javascript">
            ddldivision = []; ddlstate = []; ddlterritory = []; ddldistributor = []; ddlrate = []; stateterr = []; statedist = []; district = [];
            var statesel = []; mdptt = ''; ddlplant = [];
            $(document).ready(function () {
                //$('#ddl_state').multiselect('reload');
                // $('#ddl_territory').multiselect('reload');
                //$('#ddl_distributor').multiselect('reload');
                retrieveRate();
                //retrieveDivision();
                retrievePlant();
                //retrieveTerritory();
                 $('#ddl_mode').on('change', function () {
                     clearfields();
                      $('#divdist').hide();
                });

                /*save onclick*/
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
                    var Subdivcode = $('#ddlSubdivision').text();
                    /*var Subdivcode = $('#ddl_division option:selected').val();
                    if (Subdivcode == 'select') {
                        alert("Select Division");
                        $('#ddl_division').focus();
                        return false;
                    }*/
                    var plant = $('#ddl_plant option:selected').val();
                    /*if (plant == 'select') {
                        alert("Select Plant");
                        $('#ddl_plant').focus();
                        return false;
                    }*/
                    var state = '';
                    $('#ddl_state > option:selected').each(function () {
                        state += ',' + $(this).val();
                    });
                    var territory = '';
                    $('#ddl_territory > option:selected').each(function () {
                        territory += ',' + $(this).val();
                    });
                    var distributor = '';
                    $('#ddl_distributor > option:selected').each(function () {
                        distributor += ',' + $(this).val();
                    });
                     var category = '';
                    //$('#ddl_category > option:selected').each(function () {
                    //    category += ',' + $(this).val();
                    //});
                    if (state == '') {
                        alert("select state");
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
                        url: "RateCardAllocation.aspx/saveRate",
                    //data: "{'DivCode':'<%=Session["div_code"]%>','SubDivCode':'" + Subdivcode + "','State':'" + state + "','Territory':'" + territory + "','Distributor':'" + distributor + "','Rate':'" + rate + "','Mode':'" + mode + "','Plant_id':'" + plant+ "','Splrate':'" + splrate + "'}",
                        data: "{'DivCode':'<%=Session["div_code"]%>','SubDivCode':'" + Subdivcode + "','State':'" + state + "','Territory':'" + territory + "','Distributor':'" + distributor + "','Rate':'" + rate + "','Mode':'" + mode + "','Splrate':'" + splrate + "','category':'" + category + "'}",
                        dataType: "json",
                        success: function (data) {
                            status = data.d;
                            alert("saved successfully");
                            $('#ddl_mode [value=select]').attr('selected', 'true');
                            clearfields();
                            $('#ddl_territory').empty();
                            $('#ddlSubdivision').val('');
                            retrieveRate();
                            // retrieveDivision();
                            retrievePlant();
                        },
                        error: function (result) {
                            alert(JSON.stringify(result));
                        }
                    });
                });

                /*Rate onchange*/
                $('#ddl_rate').on('change', function () {
                    var rate = $('#ddl_rate').val();
                    retrieveDivision(rate);
                    var subdiv = $('#ddlSubdivision').text();
                    $('#divstate').show();
                    $('.chkstate').show();
                    //$('#divterr').show();  
                    var mode = $('#ddl_mode option:selected').val();
                    if (mode == 'select') {
                        alert("Select Mode");
                        $('#ddl_mode').focus();
                        return false;
                    }
                    retrieveState(subdiv, 0, mode);
                    statechng();
                    //clearfields();

                    $('#statechk').prop('checked', false);
                    $('#terrchk').prop('checked', false);
                    $('#distchk').prop('checked', false);
                   // $('#catechk').prop('checked', false);

                });

                /*division onchange*/
                $('#ddl_division').on('change', function () {
                    var subdiv = $('#ddl_division').val();
                    $('#ddl_plant [value=select]').attr('selected', 'true');
                    clearfields();
                });
                $('#ddl_plant').on('change', function () {
                    var plant = $('#ddl_plant').val();
                    //var subdiv = $('#ddl_division').val(); 
                    var subdiv = $('#ddlSubdivision').text();
                    if (plant == 'select') {
                        alert("Select Plant");
                        clearfields();
                        $('#ddl_plant').focus();
                        return false;
                    }
                    clearfields();
                    $('#divstate').show();
                    $('.chkstate').show();
                    var mode = $('#ddl_mode option:selected').val();
                    if (mode == 'select') {
                        alert("Select Mode");
                        $('#ddl_mode').focus();
                        return false;
                    }
                    //$('#divterr').show();                
                    retrieveState(subdiv, plant, mode);
                    statechng();
                });

                /*onchange state*/
                $('#ddl_state').on('change', function () {
                    //$('#divterr').show();
                    //$('.chkterr').show();
                    $('#divdist').hide();
                    statechng();
                });


                /*onchange Territory*/
                $('#ddl_territory').on('change', function () {
                    $('#divdist').show();
                    $('.chkdist').show();
                    terrimulti();
                });

                /*onchange district*/
                $('#ddl_District').on('change', function () {
                    $('#divdist').show();
                    //$('.chkdist').show();
                    District();
                });

                /*onchange district*/
                $('#ddl_distributor').on('change', function () {
                    //$('#divcate').show();
                  //  $('.chkcat').show();
                   // retrieveCategory();
                });

                /*state checkbox onchange*/
                $('#statechk').on('change', function () {
                    $('#ddl_state').empty();
                    if ($('#statechk').is(":checked")) {
                        data_bind($('#ddl_state'), ddlstate, 'State_Code', 'StateName', 'yes');
                        $('#ddl_state').multiselect('reload');
                    } else {
                        data_bind($('#ddl_state'), ddlstate, 'State_Code', 'StateName', 'No');
                        $('#ddl_state').multiselect('reload');
                    }
                    statechng();
                });

                /*territory checkbox onchange*/
                $('#terrchk').on('change', function () {
                    if ($('#statechk').is(":checked") == true) {
                        statechng();
                        terrimulti();
                        //$('#divdist').show();
                        //$('.chkdist').show();
                    }
                    else {
                        alert("Check State");
                        $('#terrchk').prop('checked', false);
                        return false;
                    }

                });

                $('#districtChk').on('change', function () {
                    if ($('#statechk').is(":checked") == true) {
                        statechng();
                        District();
                        $('#divdist').show();
                        $('.chkdist').show();
                    }
                    else {
                        alert("Check State");
                        $('#districtChk').prop('checked', false);
                        return false;
                    }
                });

                /*distributor checkbox onchange*/
                $('#distchk').on('change', function () {
                    $('#divdist').show();
                    District();
                   // $('#divcate').show();
                   // retrieveCategory();
                    /*terrimulti();*/
                });

                 /*distributor checkbox onchange*/
                $('#catechk').on('change', function () {
                   // $('#divcate').show();
                   // retrieveCategory();
                    /*terrimulti();*/
                });

            });

            function statechng() {
                statemulti = [];
                statemultip = '';
                //$('.chkterr').show();            
                //$('#divterr').show();           
                $('.chkDistrict').show();
                $('#divDistrict').show();
                $('.chkdist').hide();
                $('#divdist').hide();
              //  $('.chkcat').hide();
              //  $('#divcate').hide();
                //var Subdivcode = $('#ddl_division option:selected').val();
                var Subdivcode = $('#ddlSubdivision').text();

                var plant = $('#ddl_plant option:selected').val();

                if (isNaN(plant)) {
                    plant = '';
                }

                $('#ddl_state > option:selected').each(function () {
                    statemultip += ',' + $(this).val();
                    statemulti.push($(this).val());
                });

                var districtCheck = $('#districtChk').is(":checked");
                $('#ddl_District').empty();
                retrieveDistrict(statemultip, districtCheck);
                $('#ddl_District').multiselect('reload');

                /*Territoty filter*/
                var terricheck = $('#terrchk').is(":checked");
                $('#ddl_territory').empty();
                retrieveTerritory(Subdivcode, statemultip, plant, terricheck);

                $('#ddl_territory').multiselect('reload');
            }

            //function district() {
            //    district = [];
            //    dist = '';
            //    $('#districtChk')
            //}

            function terrimulti() {
                terrimultip = [];
                terrcode = '';
                $('#ddl_territory > option:selected').each(function () {
                    terrimultip.push($(this).val());
                    terrcode += $(this).val() + ',';
                });
                //var subdiv = $('#ddl_division option:selected').val();
                var subdiv = $('#ddlSubdivision').text();
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
              
                retrieveDistributor(subdiv, terrcode, plant,mode);
            }

            function District() {
                Districtmultip = [];
                DistrictCode = '';
                $('#ddl_District > option:selected').each(function () {
                    Districtmultip.push($(this).val());
                    DistrictCode += $(this).val() + ',';
                });
                var subdiv = $('#ddlSubdivision').text();
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
                retrieveDistributor(subdiv, DistrictCode, plant,mode);
            }

            function clearfields() {
                $('#divstate').hide();
                $('#divterr').hide();
                $('#divDistrict').hide();
                $('#divdist').hide();
                $('#divDistrict').hide();
                $('#statechk').prop('checked', false);
                $('#terrchk').prop('checked', false);
                $('#districtChk').prop('checked', false);
                $('#distchk').prop('checked', false);
                $('#catechk').prop('checked', false);
                
            }

            function retrieveRate() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "RateCardAllocation.aspx/getRate",
                    data: "{'divcode':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        $('#ddl_rate').empty();
                        ddlrate = JSON.parse(data.d) || [];
                        data_bind($('#ddl_rate'), ddlrate, 'Price_list_Sl_No', 'Price_list_Name', 'No');
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            }
            function retrieveDivision(ratecard) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "RateCardAllocation.aspx/getDivision",
                    data: "{'divcode':'<%=Session["div_code"]%>','ratecard':'" + ratecard + "'}",
                    dataType: "json",
                    success: function (data) {
                        // $('#ddl_division').empty();
                        ddldivision = JSON.parse(data.d) || [];
                        if (ddldivision.length > 0) {
                            $('#ddlSubdivision').text(ddldivision[0].sub_div_code);
                            $('#ddlSubdivision').val(ddldivision[0].subdivision_name);
                        }

                        //data_bind($('#ddl_division'), ddldivision, 'subdivision_code', 'subdivision_name', 'No');
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            }

        <%--function retrieveDivision() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "RateCardAllocation.aspx/getDivision",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    $('#ddl_division').empty();
                    ddldivision = JSON.parse(data.d) || [];
                    data_bind($('#ddl_division'), ddldivision, 'subdivision_code', 'subdivision_name', 'No');
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }--%>
            function retrievePlant() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "RateCardAllocation.aspx/getPlant",
                    data: "{'divcode':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        $('#ddl_plant').empty();
                        ddlplant = JSON.parse(data.d) || [];
                        data_bind($('#ddl_plant'), ddlplant, 'Plant_Id', 'Plant_Name', 'No');
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            }
            function retrieveState(subdiv, plant_id, mode) {
                if (mode == '1') {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "RateCardAllocation.aspx/getState",
                        data: "{'subdivcode':'" + subdiv + "','plant_id':'" + plant_id + "'}",
                        dataType: "json",
                        success: function (data) {
                            $('#ddl_state').empty();
                            ddlstate = JSON.parse(data.d) || [];

                            data_bind($('#ddl_state'), ddlstate, 'State_Code', 'StateName', 'No');
                            $('#ddl_state').multiselect({
                                columns: 3,
                                placeholder: 'Select State',
                                search: true,
                                searchOptions: {
                                    'default': 'Search State'
                                },
                                selectAll: true
                            }).multiselect('reload');
                            $('#ddl_state-options ul').css('column-count', '3');
                        },
                        error: function (result) {
                            alert(JSON.stringify(result));
                        }
                    });
                }
                else {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "RateCardAllocation.aspx/get_SS_State",
                        data: "{'subdivcode':'" + subdiv + "','plant_id':'" + plant_id + "'}",
                        dataType: "json",
                        success: function (data) {
                            $('#ddl_state').empty();
                            ddlstate = JSON.parse(data.d) || [];

                            data_bind($('#ddl_state'), ddlstate, 'State_Code', 'StateName', 'No');
                            $('#ddl_state').multiselect({
                                columns: 3,
                                placeholder: 'Select State',
                                search: true,
                                searchOptions: {
                                    'default': 'Search State'
                                },
                                selectAll: true
                            }).multiselect('reload');
                            $('#ddl_state-options ul').css('column-count', '3');
                        },
                        error: function (result) {
                            alert(JSON.stringify(result));
                        }
                    });
                }

            }

            function retrieveDistrict(statemultip, districtCheck) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "RateCardAllocation.aspx/getDistrict",
                    //data: "{'subdivcode':'" + subdiv + "','plant_id':'" + plant_id + "'}",
                    data: "{'state_code':'" + statemultip + "'}",
                    dataType: "json",
                    success: function (data) {
                        $('#ddl_District').empty();
                        ddlDistrict = JSON.parse(data.d) || [];

                        if (districtCheck == true) {
                            data_bind($('#ddl_District'), ddlDistrict, 'Dist_code', 'Dist_name', 'yes');

                        }
                        else
                            data_bind($('#ddl_District'), ddlDistrict, 'Dist_code', 'Dist_name', 'No');

                        //data_bind($('#ddl_District'), ddlDistrict, 'Dist_code', 'Dist_name', 'No');
                        $('#ddl_District').multiselect({
                            columns: 3,
                            placeholder: 'Select District',
                            search: true,
                            searchOptions: {
                                'default': 'Search District'
                            },
                            selectAll: true
                        }).multiselect('reload');
                        $('#ddl_District-options ul').css('column-count', '3');
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            }
            //retrive category
             function retrieveCategory() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "RateCardAllocation.aspx/getcategory",
                    //data: "{'subdivcode':'" + subdiv + "'}",
                    dataType: "json",
                    success: function (data) {
                        $('#ddl_category').empty();
                        ddlcategory = JSON.parse(data.d) || [];
                       if ($('#catechk').is(":checked")) {
                            data_bind($('#ddl_category'), ddlcategory, 'Doc_Cat_Code', 'Doc_Cat_Name', 'yes');
                        }
                        else
                            data_bind($('#ddl_category'), ddlcategory, 'Doc_Cat_Code', 'Doc_Cat_Name', 'No');

                        $('#ddl_category').multiselect({
                            columns: 3,
                            placeholder: 'Select Category',
                            search: true,
                            searchOptions: {
                                'default': 'Search Category'
                            },
                            selectAll: true
                        }).multiselect('reload');
                        $('#ddl_category-options ul').css('column-count', '3');
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            }

            function retrieveTerritory(subdiv, statemultip, plant, terricheck) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "RateCardAllocation.aspx/getTerritory",
                    data: "{'subdivcode':'" + subdiv + "','state_code':'" + statemultip + "','plant_id':'" + plant + "'}",
                    dataType: "json",
                    success: function (data) {
                        $('#ddl_territory').empty();
                        ddlterritory = JSON.parse(data.d) || [];
                        if (terricheck == true) {
                            data_bind($('#ddl_territory'), ddlterritory, 'Territory_Code', 'Territory', 'yes');
                        }
                        else
                            data_bind($('#ddl_territory'), ddlterritory, 'Territory_Code', 'Territory', 'No');

                        $('#ddl_territory').multiselect({
                            columns: 3,
                            placeholder: 'Select Territory',
                            search: true,
                            searchOptions: {
                                'default': 'Search Territory'
                            },
                            selectAll: true
                        }).multiselect('reload');
                        $('#ddl_territory-options ul').css('column-count', '3');
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            }
            function retrieveDistributor(subdiv, terricode, plant, mode) {
                if (mode == '1') {
                    $("#dischk").text('ALL Distributor');
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "RateCardAllocation.aspx/getDistributor",
                        //data: "{'subdivcode':'" + subdiv + "','terricode':'" + terricode + "','plant_id':'" + plant + "'}",
                        data: "{'subdivcode':'" + subdiv + "','terricode':'" + terricode + "'}",
                        dataType: "json",
                        success: function (data) {
                            $('#ddl_distributor').empty();
                            ddldistributor = JSON.parse(data.d) || [];
                            if ($('#distchk').is(":checked")) {
                                data_bind($('#ddl_distributor'), ddldistributor, 'Stockist_Code', 'Stockist_Name', 'yes');
                                //$('.chkcat').show();
                            }
                            else {
                                data_bind($('#ddl_distributor'), ddldistributor, 'Stockist_Code', 'Stockist_Name', 'No');
                                
                            }
                            $('#ddl_distributor').multiselect({
                                columns: 3,
                                placeholder: 'Select Distributor',
                                search: true,
                                searchOptions: {
                                    'default': 'Search Distributor'
                                },
                                selectAll: true
                            }).multiselect('reload');
                            $('#ddl_distributor-options ul').css('column-count', '3');
                        },
                        error: function (result) {
                            alert(JSON.stringify(result));
                        }
                    });
                }
                else {
                    $("#dischk").text('ALL SuperStockist');
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "RateCardAllocation.aspx/get_SS_Distributor",
                        //data: "{'subdivcode':'" + subdiv + "','terricode':'" + terricode + "','plant_id':'" + plant + "'}",
                        data: "{'subdivcode':'" + subdiv + "','terricode':'" + terricode + "'}",
                        dataType: "json",
                        success: function (data) {
                            $('#ddl_distributor').empty();
                            ddldistributor = JSON.parse(data.d) || [];
                            if ($('#distchk').is(":checked")) {
                                data_bind($('#ddl_distributor'), ddldistributor, 'Stockist_Code', 'Stockist_Name', 'yes');
                            }
                            else {
                                data_bind($('#ddl_distributor'), ddldistributor, 'Stockist_Code', 'Stockist_Name', 'No');
                            }
                            $('#ddl_distributor').multiselect({
                                columns: 3,
                                placeholder: 'Select Super Stockist',
                                search: true,
                                searchOptions: {
                                    'default': 'Search Superstockist'
                                },
                                selectAll: true
                            }).multiselect('reload');
                            $("#dislbl").text('Super Stockist');
                            $('#ddl_distributor-options ul').css('column-count', '3');
                        },
                        error: function (result) {
                            alert(JSON.stringify(result));
                        }
                    });
                }
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

    </script>
        <form id="form1" runat="server">
            <div class="container col-md-offset-3">
                <div class="row" style="margin-top: 20px; /*border-bottom: thin dashed; */">
                    <div class="col-xs-1">
                        <label>Mode</label><span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3">
                        <select id="ddl_mode" style="width: 265px;">
                            <option selected value="select">Select Mode</option>
                            <option value="1">Distributor</option>
                            <option value="2" style="display: none">Retailer</option>
                            <option value="3">Super Stockict</option>
                        </select>
                    </div>
                </div>
                <div class="row" style="margin-top: 10px; margin-bottom: 10px; /*border-bottom: thin dashed; */">
                    <div class="col-xs-1">
                        <label>Rate</label><span style="color: red">*</span>
                    </div>
                    <div class="col-sm-3">
                        <select id="ddl_rate" style="width: 265px;">
                        </select>
                    </div>
                </div>
                <div class="row" style="align-items: center; margin-top: 10px;">
                    <div class="col-xs-1">
                        <label>Division</label><span style="color: red"></span>
                    </div>
                    <div class="col-xs-3">
                        <input type="text" name="Subdivision" class="form-control" id="ddlSubdivision" readonly />
                        <select id="ddl_division" name="ddldivision" style="width: 265px; display: none;"></select>
                    </div>
                </div>
                <%-- <div class="row" style="align-items: center;margin-top: 10px;display:none">
            <input type="checkbox" id="splRatechk"   name="splRatechk" value="3"/>
            <label for="splRatechk"> Special Rate</label>            
        </div>--%>

                <%--<div class="row" id="Allchk" style="margin-top: 10px;padding-left: 15px;">--%>
                <div class="row" id="Allchk" style="margin-top: 10px; padding-left: 113px;">
                    <input type="checkbox" id="splRatechk" name="splRatechk" value="3" style="display: none" />
                    <label for="splRatechk" style="padding-right: 20px; display: none">Special Rate</label>
                    <input type="checkbox" id="statechk" class="chkstate" name="dstatechk" value="1" style="display: none" />
                    <label for="vehicle1" class="chkstate" style="padding-right: 20px; display: none">All state</label>
                    <input type="checkbox" class="chkterr" id="terrchk" name="dterrchk" value="2" style="display: none" />
                    <label for="vehicle2" class="chkterr" style="padding-right: 20px; display: none">All Territory</label>

                    <input type="checkbox" class="chkDistrict" id="districtChk" name="districtchk" value="2" style="display: none" />
                    <label for="district" class="chkDistrict" style="padding-right: 20px; display: none">All District</label>

                    <input type="checkbox" class="chkdist" id="distchk" name="ddistchk" value="3" style="display: none" />
                    <label for="vehicle3" class="chkdist" id="dischk"  style="display: none">All Distributor</label>
                     <%--<input type="checkbox" class="chkcat" id="catechk" name="ddistchk" value="3" style="display: none" />
                    <label for="vehicle3" class="chkcat" id="catchk"  style="display: none">All Category</label>--%>
                </div>
                <div class="row" style="margin-top: 10px; display: none;">
                    <div class="col-xs-1">
                        <label>Plant</label><span style="color: red"></span>
                    </div>
                    <div class="col-sm-3">
                        <select id="ddl_plant" name="ddlplant" style="width: 265px;"></select>
                    </div>
                </div>

                <div class="row" id="divstate" style="margin-top: 10px; display: none;">
                    <div class="col-xs-1">
                        <label>State</label><span style="color: red"></span>
                    </div>
                    <div class="col-sm-3">
                        <select id="ddl_state" multiple>
                        </select>
                    </div>
                </div>

                <div class="row" id="divterr" style="margin-top: 10px; display: none">
                    <div class="col-xs-1">
                        <label>Territory</label>
                    </div>
                    <div class="col-sm-3">
                        <select id="ddl_territory" multiple>
                        </select>
                    </div>
                </div>

                <div class="row" id="divDistrict" style="margin-top: 10px; display: none;">
                    <div class="col-xs-1">
                        <label>District</label><span style="color: red"></span>
                    </div>
                    <div class="col-sm-3">
                        <select id="ddl_District" multiple>
                        </select>
                    </div>
                </div>

                <%--<div class="row" id="divterr" style="margin-top: 10px;display:none">
            <div class="col-xs-1"><label>Territory</label></div>
            <div class="col-sm-3">
                <select id="ddl_territory" multiple >
                </select>
            </div>
        </div>--%>
                <div class="row" id="divdist" style="margin-top: 10px; display: none">
                    <div class="col-xs-1">
                        <label id="dislbl">Distributor</label>
                    </div>
                    <div class="col-sm-3">
                        <select id="ddl_distributor" multiple>
                        </select>
                    </div>
                </div>
               <%--  <div class="row" id="divcate" style="margin-top: 10px; display: none">
                    <div class="col-xs-1">
                        <label id="lbl_Category">Category</label>
                    </div>
                    <div class="col-sm-3">
                        <select id="ddl_category" multiple>
                        </select>
                    </div>
                </div>--%>
                <button type="button" class="btn btn-primary col-md-offset-2" style="margin-top: 20px;" id="btnsave">Save</button>
            </div>
        </form>
    </body>
    </html>
</asp:Content>
