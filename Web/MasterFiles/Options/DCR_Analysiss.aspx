<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="DCR_Analysiss.aspx.cs" Inherits="MasterFiles_DCR_Analysiss" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link type="text/css" href="../css/SalesForce_New/bootstrap-select.min.css" rel="stylesheet" />
    <link href="https://code.jquery.com/ui/1.10.4/themes/ui-lightness/jquery-ui.css" rel="stylesheet">
    <style>
        .row {
            /*width: 1280px;
            height: 475px;*/
            resize: both;
    
        }

        .tab {
            float: left;
            border: 1px solid #ccc;
            background-color: #f1f1f1;
            width: 25%;
            height: 475px;
            font-size:12px;
           /* resize: horizontal;
            overflow: auto;*/
        }

        .tab1 {
            float: left;
            border: 1px solid #ccc;
            background-color: #f1f1f1;
            width: 75%;
            height: 475px;
           /* resize: horizontal;
            *overflow: auto;*/
            /*-moz-tab-size: 4; /* Firefox */
        }
        #div1 {
            float: left;
            border: 1px solid #ccc;
            background-color: #f1f1f1;
            width: 25%;
            height: 475px;
            font-size:12px;
            /*resize: horizontal;
            overflow: auto;*/
        }

        #div2 {
            float: left;
            border: 1px solid #ccc;
            background-color: #f1f1f1;
            width: 75%;
            height: 475px;
            /*-moz-tab-size: 4; /* Firefox */
        }

        #dragbar {
            position: absolute;
            cursor: col-resize;
            z-index: 3;
            padding: 5px;
        }

        .dropdown-menu {
            /* height: 300px;*/
            overflow: auto;
            max-height: 210px;
        }

        #sortable1 {
            list-style-type: none;
            margin: 0;
            padding: 0;
            width: 100%;
        }

        #sortable1 li {
            margin: 0 3px 3px 3px;
            padding: 0.4em;
            padding-left: 1.5em;
            font-size: 0.9em;
            height: 35px;
        }

        #sortable1 li span {
            position: absolute;
            margin-left: -1.3em;
        }

        table {
            border: 1px solid #666;
            width: 100%;
            text-align: left;
            white-space: nowrap;
            vertical-align: middle;
        }

        th {
            background: #f8f8f8;
            font-weight: bold;
            padding: 2px;
            vertical-align: middle;
        }

    </style>
    <form runat="server" id="frm1">
        <div class="row" id="parent">
            <div class="col-lg-12 sub-header">
                DCR Analysis Report            
            </div>
            <button id="btntemp" class="btn btn-default dropdown-toggle" aria-label="settings" data-toggle="dropdown" type="button" title="DCR Analysis" style="padding-left: 5px; display: none;">
                <i class="fa fa-th-list"></i>
            </button>
            <%-- <a href="javascript:void(0)" id="dragbar" style="width: 5px; top: 138px; left: 707px; height: 461px; cursor: col-resize;"></a>--%>
            <div class="tab" id ="div1">
                <div class="col-xs-12 col-sm-10">
                    <div class="col-xs-12 col-sm-10">
                        <label style="padding-top: 5px;">Templates List</label>
                    </div>
                    <div class="col-xs-12 col-sm-2">
                        <button id="btnSettings" class="btn btn-default dropdown-toggle" aria-label="settings" data-toggle="dropdown" type="button" title="Template settings" style="padding-left: 5px;">
                            <i class="fa fa-th-list"></i>
                        </button>
                    </div>
                    <div class="col-xs-12 col-sm-12" style="padding-left: 0px;">
                        <div class="col-xs-12 col-sm-12">
                            <select id="mtemp" style="padding-right: 115px;" required>
                                <option value="select">--select--</option>
                            </select>
                        </div>
                    </div>
                </div>
                <div class="col-xs-12 col-sm-10">
                    <div class="col-xs-12 col-sm-10">
                        <label style="padding-top: 5px;">Fieldforce Name</label>
                    </div>
                    <select id="mforms" multiple data-actions-box="true" style="padding-left: 0px;" required>
                    </select>
                </div>
                <div class="row hiderow" id="Ftablerow1" style="margin-bottom: 1rem!important;">
                    <div class="col-xs-12 col-sm-12">
                        <div class="col-xs-12 col-sm-7">
                            <div class="col-xs-12 col-sm-10">
                                <label style="padding-top: 5px;">Date Selection </label>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-10">
                            <div class="col-xs-12 col-sm-12">
                                <span style="float: left; margin-right: 15px;">
                                    <div id="reportrange" class="form-control mt-5 mb-5" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc;">
                                        <i class="fa fa-calendar"></i>&nbsp;
                                        <span id="ordDate"></span>
                                        <i class="fa fa-caret-down"></i>
                                    </div>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
                <div>
                    <div class="col-xs-12 col-sm-10">
                        <div class="col-xs-12 col-sm-5">
                            <button type="button" style="background-color: #1a73e8; float: right;" class="btn btn-primary" id="viewfields">View</button>
                        </div>
                        <div class="col-xs-12 col-sm-5">
                            <button type="button" class="btn btn-primary" style="background-color: #1a73e8; float: right;" id="clearfields">Clear</button>
                        </div>
                    </div>
                </div>
                
            </div>
            <div class="tab1" id ="div2" style="float: center; overflow-y: scroll; overflow-x: scroll;">
                <div id="loading-image" style="display:none"><div id="sample" style="left: 50%;height: 100%;position: absolute;top: 50%;"><img src="../Images/loading/loading17.gif" /></div></div>
                <div class="tableclass">
                    <table class="table table-hover" id="OrderList" style="font-size: 10px;white-space: nowrap; display: none">
                        <thead class="text-warning">
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
            <div class="modal fade" id="AddFieldsModal" tabindex="-1" style="z-index: 1000000; background: transparent; overflow: auto;" role="dialog" aria-labelledby="AddFieldsModal" aria-hidden="true">
                <div class="modal-dialog" role="document" style="width: 60%;">
                    <div class="modal-content" style="height: 400px; overflow-y: scroll">
                        <div class="modal-header" style="background-color: #bfefff;">
                            <h4>Template setting</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row" style="margin-bottom: 1rem!important;">
                                <label style="padding-top: 5px; color: #3c8dbc; padding-left: 20px;">AddTemplateName</label>
                                <input class='tgl tgl-skewed' id="txtName" type='textbox' />
                                <div class="col-xs-12 col-sm-12">
                                    <ul id="sortable1" style="padding-left: 20px;">
                                        <li class="ui-state-default"><span class="ui-icon ui-icon-arrowthick-2-n-s"></span>
                                            <input class='tgl tgl-skewed' id="Sf_Name" type='checkbox' value="Sf_Name" />
                                            <label class='tgl' for="Sf_Name" style="height: 1px;">Fieldforce Name</label>
                                        </li>
                                        <li class="ui-state-default"><span class="ui-icon ui-icon-arrowthick-2-n-s"></span>
                                            <input class='tgl tgl-skewed' id="Sf_Joining_Date" type='checkbox' value="Sf_Joining_Date" />
                                            <label class='tgl' for="Sf_Joining_Date" style="height: 1px;">Joining Date</label>
                                        </li>
                                        <li class="ui-state-default"><span class="ui-icon ui-icon-arrowthick-2-n-s"></span>
                                            <input class='tgl tgl-skewed' id="Designation" type='checkbox' value="Designation" />
                                            <label class='tgl' for="Designation" style="height: 1px;">Designation</label>
                                        </li>
                                        <li class="ui-state-default"><span class="ui-icon ui-icon-arrowthick-2-n-s"></span>
                                            <input class='tgl tgl-skewed' id="dt" type='checkbox' value="dt" />
                                            <label class='tgl' for="dt" style="height: 1px;">Date</label>
                                        </li>
                                        <li class="ui-state-default"><span class="ui-icon ui-icon-arrowthick-2-n-s"></span>
                                            <input class='tgl tgl-skewed' id="Wtype" type='checkbox' value="Wtype" />
                                            <label class='tgl-btn' for="Wtype" style="height: 1px;">Worktype</label>
                                        </li>
                                        <li class="ui-state-default"><span class="ui-icon ui-icon-arrowthick-2-n-s"></span>
                                            <input class='tgl tgl-skewed' id="ClstrName" type='checkbox' value="ClstrName" />
                                            <label class='tgl-btn' for="ClstrName" style="height: 1px;">Route Worked</label>
                                        </li>
                                        <li class="ui-state-default"><span class="ui-icon ui-icon-arrowthick-2-n-s"></span>
                                            <input class='tgl tgl-skewed' id="Reporting_Sf" type='checkbox' value="Reporting_Sf" />
                                            <label class='tgl-btn' for="Reporting_Sf" style="height: 1px;">Reporting Manager 1</label>
                                        </li>
                                        <li class="ui-state-default"><span class="ui-icon ui-icon-arrowthick-2-n-s"></span>
                                            <input class='tgl tgl-skewed' id="Reporting" type='checkbox' value="Reporting" />
                                            <label class='tgl-btn' for="Reporting" style="height: 1px;">Reporting Manager 2</label>
                                        </li>
                                        <li class="ui-state-default"><span class="ui-icon ui-icon-arrowthick-2-n-s"></span>
                                            <input class='tgl tgl-skewed' id="Route" type='checkbox' value="Route" />
                                            <label class='tgl-btn' for="Route" style="height: 1px;">Route as per Tour Plan</label>
                                        </li>
                                        <li class="ui-state-default"><span class="ui-icon ui-icon-arrowthick-2-n-s"></span>
                                            <input class='tgl tgl-skewed' id="remarks" type='checkbox' value="remarks" />
                                            <label class='tgl-btn' for="remarks" style="height: 1px;">Daywise Remark</label>
                                        </li>
                                        <li class="ui-state-default"><span class="ui-icon ui-icon-arrowthick-2-n-s"></span>
                                            <input class='tgl tgl-skewed' id="Cust_Name" type='checkbox' value="Cust_Name" />
                                            <label class='tgl-btn' for="Cust_Name" style="height: 1px;">Customer Name</label>
                                        </li>
                                        <li class="ui-state-default"><span class="ui-icon ui-icon-arrowthick-2-n-s"></span>
                                            <input class='tgl tgl-skewed' id="Cust_Addr" type='checkbox' value="Cust_Addr" />
                                            <label class='tgl-btn' for="Cust_Addr" style="height: 1px;">Address</label>
                                        </li>
                                        <li class="ui-state-default"><span class="ui-icon ui-icon-arrowthick-2-n-s"></span>
                                            <input class='tgl tgl-skewed' id="Session" type='checkbox' value="Session" />
                                            <label class='tgl-btn' for="Session" style="height: 1px;">Session</label>
                                        </li>
                                        <li class="ui-state-default"><span class="ui-icon ui-icon-arrowthick-2-n-s"></span>
                                            <input class='tgl tgl-skewed' id="Cust_Code" type='checkbox' value="Cust_Code" />
                                            <label class='tgl-btn' for="Cust_Code" style="height: 1px;">Retailer Code</label>
                                        </li>
                                        <li class="ui-state-default"><span class="ui-icon ui-icon-arrowthick-2-n-s"></span>
                                            <input class='tgl tgl-skewed' id="Cust_Spec" type='checkbox' value="Cust_Spec" />
                                            <label class='tgl-btn' for="Cust_Spec" style="height: 1px;">Retailer Channel</label>
                                        </li>
                                        <li class="ui-state-default"><span class="ui-icon ui-icon-arrowthick-2-n-s"></span>
                                            <input class='tgl tgl-skewed' id="Cust_Cls" type='checkbox' value="Cust_Cls" />
                                            <label class='tgl-btn' for="Cust_Cls" style="height: 1px;">Retailer Class</label>
                                        </li>
                                        <li class="ui-state-default"><span class="ui-icon ui-icon-arrowthick-2-n-s"></span>
                                            <input class='tgl tgl-skewed' id="Activity_Date" type='checkbox' value="Activity_Date" />
                                            <label class='tgl-btn' for="Activity_Date" style="height: 1px;">Visit Date & Time</label>
                                        </li>
                                        <li class="ui-state-default"><span class="ui-icon ui-icon-arrowthick-2-n-s"></span>
                                            <input class='tgl tgl-skewed' id="POB_Value" type='checkbox' value="POB_Value" />
                                            <label class='tgl-btn' for="POB_Value" style="height: 1px;">Order Value</label>
                                        </li>
                                        <li class="ui-state-default"><span class="ui-icon ui-icon-arrowthick-2-n-s"></span>
                                            <input class='tgl tgl-skewed' id="OrderTyp" type='checkbox' value="OrderTyp" />
                                            <label class='tgl-btn' for="OrderTyp" style="height: 1px;">Order Type</label>
                                        </li>
                                        <li class="ui-state-default"><span class="ui-icon ui-icon-arrowthick-2-n-s"></span>
                                            <input class='tgl tgl-skewed' id="Product" type='checkbox' value="Product" />
                                            <label class='tgl-btn' for="Product" style="height: 1px;">Product</label>
                                        </li>
                                        <li class="ui-state-default"><span class="ui-icon ui-icon-arrowthick-2-n-s"></span>
                                            <input class='tgl tgl-skewed' id="Quantity" type='checkbox' value="Quantity" style="display: none" />
                                            <label class='tgl-btn' for="Quantity" style="height: 1px;">Product Quantity</label>
                                        </li>
                                        <li class="ui-state-default"><span class="ui-icon ui-icon-arrowthick-2-n-s"></span>
                                            <input class='tgl tgl-skewed' id="Prod_SKU" type='checkbox' value="Prod_SKU" style="display: none" />
                                            <label class='tgl-btn' for="Prod_SKU" style="height: 1px;">Product SKU</label>
                                        </li>
                                        <li class="ui-state-default"><span class="ui-icon ui-icon-arrowthick-2-n-s"></span>
                                            <input class='tgl tgl-skewed' id="value" type='checkbox' value="value" style="display: none" />
                                            <label class='tgl-btn' for="value" style="height: 1px;">Product Value</label>
                                        </li>
                                        <li class="ui-state-default"><span class="ui-icon ui-icon-arrowthick-2-n-s"></span>
                                            <input class='tgl tgl-skewed' id="net_weight_value" type='checkbox' value="net_weight_value" />
                                            <label class='tgl-btn' for="net_weight_value" style="height: 1px;">Nett Weight</label>
                                        </li>
                                        <li class="ui-state-default" style="display: none"><span class="ui-icon ui-icon-arrowthick-2-n-s"></span>
                                            <input class='tgl tgl-skewed' id="Att_Status" type='checkbox' value="Att_Status" />
                                            <label class='tgl-btn' for="Att_Status" style="height: 1px;">Attendance Status</label>
                                        </li>
                                        <li class="ui-state-default" style="display: none"><span class="ui-icon ui-icon-arrowthick-2-n-s"></span>
                                            <input class='tgl tgl-skewed' id="Sub_time" type='checkbox' value="Sub_time" />
                                            <label class='tgl-btn' for="Sub_time" style="height: 1px;">Day Plan Submission Time</label>
                                        </li>
                                        <li class="ui-state-default"><span class="ui-icon ui-icon-arrowthick-2-n-s"></span>
                                            <input class='tgl tgl-skewed' id="stockist_name" type='checkbox' value="stockist_name" />
                                            <label class='tgl-btn' for="stockist_name" style="height: 1px;">Distributor Name</label>
                                        </li>
                                        <li class="ui-state-default"><span class="ui-icon ui-icon-arrowthick-2-n-s"></span>
                                            <input class='tgl tgl-skewed' id="filters" type='checkbox' value="filters" />
                                            <label class='tgl-btn' for="filters" style="height: 1px;">Visited Outlets for day</label>
                                        </li>
                                        <li class="ui-state-default" style="display: none"><span class="ui-icon ui-icon-arrowthick-2-n-s"></span>
                                            <input class='tgl tgl-skewed' id="Prod_Catgry" type='checkbox' value="PProd_Catgry" />
                                            <label class='tgl-btn' for="Prod_Catgry" style="height: 1px;">Product Category</label>
                                        </li>
                                        <li class="ui-state-default"><span class="ui-icon ui-icon-arrowthick-2-n-s"></span>
                                            <input class='tgl tgl-skewed' id="latlong" type='checkbox' value="latlong" />
                                            <label class='tgl-btn' for="latlong" style="height: 1px;">Lat & Long</label>
                                        </li>
                                        <li class="ui-state-default"><span class="ui-icon ui-icon-arrowthick-2-n-s"></span>
                                            <input class='tgl tgl-skewed' id="Activity_Remarks" type='checkbox' value="Activity_Remarks" />
                                            <label class='tgl-btn' for="Activity_Remarks" style="height: 1px;">Remark</label>
                                        </li>
                                        <li class="ui-state-default"><span class="ui-icon ui-icon-arrowthick-2-n-s"></span>
                                            <input class='tgl tgl-skewed' id="Imgurl" type='checkbox' value="Imgurl" />
                                            <label class='tgl-btn' for="Imgurl" style="height: 1px;">Event Capture</label>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-dismiss="modal" id="closefields1">Cancel</button>
                                <button type="button" style="background-color: #1a73e8;" class="btn btn-primary" data-dismiss="modal" id="svfields1">Apply</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">    
        
        var MasFrms = [], dropdownval = '', rowkey = [], fnamematch = [], tempName = [], tempList = [], listobj = [], sfCodematch = [], rowtempl = [];
        var DCR_Products = [], DCR_ProdDts = [], DCR_OrderDts = [], DCR_EventCap = [], DCR_FForce = [], DCR_Dayplan = [], DCR_Customer = [], DCR_tourplan = [];
        var templatelist = '', templateName = '', fdt = '', tdt = '', selectsfcode = '', total = '';
        ResultArr = [], maxProd = [], CountProd = 0, prodcd = [];
        rwStr = ""; samplearr1 = ""; samplearr = [], samplearr2 = []; result1 = []; result2 = [];
        Sesion = ""; actdate = ""; pobVal = ""; netwtVal = ""; stkName = ""; latlg = ""; actRmk = ""; ordTyp = ""; custCd = "";
        custName = ""; custAddr = ""; custSpec = ""; custCls = ""; mycust = [];
        prdQnty = ""; visitout = "";
        function loadSfDetails() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "DCR_Analysiss.aspx/getForms",
                data: "{'divcode':'<%=Session["div_code"]%>','sfcode':'<%=Session["Sf_Code"]%>'}",
                dataType: "json",
                success: function (data) {
                    MasFrms = JSON.parse(data.d) || [];
                    var fillfrms = MasFrms;
                    $('#mforms').empty();

                    for ($i = 0; $i < fillfrms.length; $i++) {
                        if (fillfrms[$i].Sf_Code != 'admin')
                            $('#mforms').append('<option value="' + fillfrms[$i].Sf_Code + '">' + fillfrms[$i].Sf_Name + '</option>');
                    }

                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
            $('#mforms').selectpicker({
                liveSearch: true
            });
            $('#mforms').addClass('col-xs-12').selectpicker('setStyle');
        }

        $('#btnSettings').on('click', function () {
            $('#AddFieldsModal').modal('toggle');
        });


        $(document).ready(function () {
            loadSfDetails();
            retrieveTemplateList();
            $("#parent").splitter();
        });       

        $('#closefields').on('click', function () {
            clearFields();
        });
        $('#svfields1').on('click', function () {
            templateName = $('#txtName').val();
            alert(templateName);
            if (templateName == '')
                alert("Please Give Template Name");
            else {
                $('.tgl').each(function () {
                    if ($(this).prop("checked") == true)
                        templatelist += '"' + $(this).val() + '":"' + $(this).next('label').text() + '",';
                });
                templatelist = '{' + templatelist.substring(0, templatelist.length - 1) + '}';
                saveTemplateList(templateName, templatelist);
            }
        });
        $('.btnTemp').on('click', function () {
            $('#Ftablerow').show();
        });
        $('#closefields1').on('click', function () {
            clearFields();
        });

        $('#mtemp').on('change', function () {
            //showTabledata();            
        })

        $('#clearfields').on('click', function () {
            clearFields();
            alert('fields cleared');
        })
        $('#viewfields').on('click', function () {
            $("#OrderList thead").html("");
            $("#OrderList tbody").html("");
            if (($('#mtemp :selected').val() == 'select') || ($('#mforms').val() == null)) {
                alert("Please select Name");
            }
            else {
                $('#loading-image').show();
                setTimeout(function () { getViewFields(); }, 3000);
            }            
        })

        
        function showLoadingImage() {
        }
        
        function hideLoadingImage() {
            $('#loading-image').hide();
        }
        var tempData = [];
        function retrieveTemplateList() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "DCR_Analysiss.aspx/getTemplate",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    $('#mtemp').empty().append('<option value=select>--select--</option>');
                    tempData = JSON.parse(data.d) || [];
                    for (var i = 0; i < tempData.length; i++) {
                        $('#mtemp').append('<option value="' + i + '">' + tempData[i].template_name + '</option>');
                    }
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function saveTemplateList(templateName, templatelist) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "DCR_Analysiss.aspx/savetemplate",
                data: "{'divcode':'<%=Session["div_code"]%>','tpname':'" + templateName + "','tplist':'" + templatelist + "'}",
                dataType: "json",
                success: function (data) {
                    alert("success");
                    $('#Ftablerow').hide();
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
            clearFields();
            retrieveTemplateList();
        }

        function getViewFields() {
            var id = '';
            var id = $('#ordDate').text();
            selectsfcode = '';
            sfCodematch = [];
                $('#mforms :selected').each(function () {
                selectsfcode += $(this).val() + ',';
                sfCodematch.push($(this).val());
                });
                alert(selectsfcode);

                id = id.split('-');
                fdt = id[2].trim() + '-' + id[1] + '-' + id[0];
                tdt = id[5] + '-' + id[4] + '-' + id[3].trim();
                loadData();

                showTabledata();
                $('#OrderList').show();
            
        }
        result = [];
        function joinObjects(ar1, ar2, keys1, keys2, vstout) {
            result = []
            for (il = 0; il < ar1.length; il++) {
                jArr = ar2.filter(function (a) {
                    iflg = 0; k = ar1[il];
                    for (ij = 0; ij < keys1.length; ij++) {
                        if (a[keys1[ij]] != k[keys1[ij]]) iflg = 1;
                    }
                    return (iflg == 0);
                });

                if (jArr.length < 1) {
                    ritem = {}
                    ritem = JSON.parse(JSON.stringify(ar1[il]));
                    result.push(ritem);
                }
                else {
                    for (ij = 0; ij < jArr.length; ij++) {
                        ritem = {}
                        ritem = JSON.parse(JSON.stringify(ar1[il]));
                        if (vstout == 'Product')
                            prodcd.push(jArr[ij]['Product_Code']);
                        else {
                            for (var key in jArr[ij]) {
                                if (vstout == 'Customer') {
                                    ritem['filters'] = jArr.length;
                                }
                                ritem[key] = jArr[ij][key];                            
                            }                            
                        }
                        result.push(ritem);                     
                    }
                }
            }
            return result;
        }
        function showTabledata() {
            loadTableData();
            var head = $("<tr/>");
            var prodh = $("<tr/>");
            var qSV = $("<tr/>");
            var indx = $('#mtemp :selected').val();

            uniqueNames = [];
            $.each(prodcd, function (i, el) {
                if ($.inArray(el, uniqueNames) === -1)
                    uniqueNames.push(el);
            });

            $("#OrderList thead").html("");
            $("#OrderList tbody").html("");
            listobj = JSON.parse(tempData[indx].template_list) || [];
            head.append($("<th rowspan=3 style=background-color:grey;vertical-align:middle;>S.No</th>"));
            $("#OrderList thead").append(head);
            for (var key in listobj) {
                if (key != 'Product') {
                    if (key == 'Quantity' || key == 'value' || key == 'Prod_SKU') { }
                    else {
                        head.append($("<th rowspan=3 style=background-color:grey;vertical-align:middle;>" + listobj[key] + "</th>"));
                        $("#OrderList thead").append(head);
                        }                
                }
                else {
                    prodhead = (uniqueNames.length) * 2;
                    head.append($("<th  colspan='" + prodhead + "' style=background-color:grey;text-align:center;> Product</th>"));
                    for (j = 0; j < uniqueNames.length; j++) {
                        prodname = DCR_Products.filter(function (a) {
                            return a.Product_Code == uniqueNames[j];
                        });
                        if (prodname.length > 0) {
                            prodh.append($("<th colspan=2 style=background-color:grey;>" + prodname[0].Product_Name + "</th>"));
                            a = ['Quantity', 'value'];
                            for (k = 0; k < a.length; k++) {
                                qSV.append($("<th style=background-color:grey;>" + a[k] + "</th>"));
                            }
                        } 
                    }
                    $("#OrderList thead").append(head);
                }
                $("#OrderList thead").append(prodh);
                $("#OrderList thead").append(qSV);

            }
            tablebind();
        }

        function loadTableData() {
            rs1 = joinObjects(DCR_FForce, DCR_Dayplan, ["sf_code"], ["sf_code"], '');
            rs2 = joinObjects(rs1, DCR_Customer, ["sf_code", "dt"], ["sf_code"], 'Customer');
            rs3 = joinObjects(rs2, DCR_tourplan, ["sf_code", "dt"], ["sf_code"], '');
           rs4 = joinObjects(rs3, DCR_OrderDts, ["sf_code", "dt", "Cust_Code"], ["sf_code"], '');
           ResultArr1 = joinObjects(rs4, DCR_EventCap, ["sf_code", "dt"], ["sf_code"], '');
           rs5 = joinObjects(DCR_FForceDayjoin, DCR_ProdDts, ["sf_code", "dt", "Cust_Code"], ["sf_code"], 'Product');
           // ResultArr = joinObjects(rs6, DCR_Products, ["sf_code", "dt", "Cust_Code","Product_Code"], ["sf_code"], 'Product');
            ResultArr = DCR_FForceDayjoin;
        }

        function tablebind() {
            var rwStr = "";
            //var rowcount = 1;
            for (il = 0; il < ResultArr.length; il++) {
                rwStr += "<tr>";
                rowcount = il + 1;
                rwStr += "<td>" + rowcount + "</td>";
                for (var key in listobj) {
                    if (key != 'Product') {
                        if (key == 'Quantity' || key == 'value' || key == 'Prod_SKU') {}
                        else if (ResultArr[il].hasOwnProperty(key))
                            rwStr += "<td>" + ResultArr[il][key] + "</td>";
                        else
                            rwStr += "<td></td>";
                    }
                    else {
                        keys1 = ["sf_code", "dt", "Cust_Code"];
                        jArr = DCR_ProdDts.filter(function (a) {
                            iflg = 0; k = ResultArr[il];
                            for (ij = 0; ij < keys1.length; ij++) {
                                if (a[keys1[ij]] != k[keys1[ij]]) iflg = 1;
                            }
                            return (iflg == 0);
                        });
                        for (jk = 0; jk < uniqueNames.length; jk++) {
                            if (jArr.length < 1) {
                                rwStr += "<td></td><td></td>";
                            }
                            else {
                                var newProd = jArr.filter(function (a) {
                                    return a.Product_Code == uniqueNames[jk];
                                });
                                if (newProd.length > 0) {
                                    a = ['Quantity', 'value'];
                                    for (k = 0; k < a.length; k++) {
                                        rwStr += "<td>" + newProd[0][a[k]] + "</td>";
                                    }
                                }
                                else {
                                    rwStr += "<td></td><td></td>";
                                }

                            }
                        }
                    }
                }
                rwStr += "</tr>";
            }
            $("#OrderList tbody").html(rwStr);
            $('#loading-image').hide();        
        }      

        $('#Product').on('change', function () {
            if ($(this).prop("checked") == true) {
                $('#Quantity').show();
                $('#Prod_SKU').show();
                $('#value').show();
            }
            else {
                $('#Quantity').hide();
                $('#Prod_SKU').hide();
                $('#value').hide();
            }
        })

        function clearFields() {
            $('#Ftablerow').hide();
            $('#mforms').val('');
            $('#mforms').selectpicker('refresh');
            $('#txtName').val('');
            $('#mtemp').val('');
            sfCodematch = [];
            $('#sortable1').find('input[type="checkbox"]').prop('checked', false);
        }

        $(function () {
            var start = moment();
            var end = moment();
            function cb(start, end) {
                $('#reportrange span').html(start.format('DD-MM-YYYY') + ' - ' + end.format('DD-MM-YYYY'));
            }
            $('#reportrange').daterangepicker({
                startDate: start,
                endDate: end,
                ranges: {
                    'Today': [moment(), moment()],
                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
                }
            }, cb);
            cb(start, end);
        });



        function loadData() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "DCR_Analysiss.aspx/GetDCR_Products",
                data: "{'SF':'" + selectsfcode +"','Div':'<%=Session["Division_Code"]%>','Mn':'" + fdt + "','Yr':'" + tdt + "'}",
                dataType: "json",
                success: function (data) {
                    DCR_Products = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "DCR_Analysiss.aspx/GetDCR_ProdDts",
                data: "{'SF':'" + selectsfcode +"','Div':'<%=Session["Division_Code"]%>','Mn':'" + fdt + "','Yr':'" + tdt + "'}",
                dataType: "json",
                success: function (data) {
                    DCR_ProdDts = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "DCR_Analysiss.aspx/GetDCR_OrderDts",
                data: "{'SF':'" + selectsfcode +"','Div':'<%=Session["Division_Code"]%>','Mn':'" + fdt + "','Yr':'" + tdt + "'}",
                dataType: "json",
                success: function (data) {
                    DCR_OrderDts = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "DCR_Analysiss.aspx/GetDCR_EventCap",
                data: "{'SF':'" + selectsfcode +"','Div':'<%=Session["Division_Code"]%>','Mn':'" + fdt + "','Yr':'" + tdt + "'}",
                dataType: "json",
                success: function (data) {
                    DCR_EventCap = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "DCR_Analysiss.aspx/GetDCR_FForce",
                data: "{'SF':'" + selectsfcode +"','Div':'<%=Session["Division_Code"]%>'}",
                dataType: "json",
                success: function (data) {
                    DCR_FForce = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "DCR_Analysiss.aspx/GetDCR_Dayplan",
                data: "{'SF':'" + selectsfcode +"','Div':'<%=Session["Division_Code"]%>','Mn':'" + fdt + "','Yr':'" + tdt + "'}",
                dataType: "json",
                success: function (data) {
                    DCR_Dayplan = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "DCR_Analysiss.aspx/GetDCR_Customer",
                data: "{'SF':'" + selectsfcode +"','Div':'<%=Session["Division_Code"]%>','Mn':'" + fdt + "','Yr':'" + tdt + "'}",
                dataType: "json",
                success: function (data) {
                    DCR_Customer = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "DCR_Analysiss.aspx/GetDCR_tourplan",
                data: "{'SF':'" + selectsfcode +"','Div':'<%=Session["Division_Code"]%>','Mn':'" + fdt + "','Yr':'" + tdt + "'}",
                dataType: "json",
                success: function (data) {
                    DCR_tourplan = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "DCR_Analysiss.aspx/joinFForceDayPlan",
                data: "{'SF':'" + selectsfcode +"','Div':'<%=Session["Division_Code"]%>','Mn':'" + fdt + "','Yr':'" + tdt + "'}",
                dataType: "json",
                success: function (data) {
                    DCR_FForceDayjoin = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });

        }

    </script>
    <script type="text/javascript">
        $(function () {
            $("#sortable1").sortable();
            $("#sortable1").disableSelection();
        });
    </script>
    <%--<script type="text/javascript">
     $( function() {
         $(".tab").resizable();
         $( ".tab1" ).resizable();
     } );
  </script>--%>
</asp:Content>
 