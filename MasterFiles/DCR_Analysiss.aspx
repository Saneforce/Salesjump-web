<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="DCR_Analysiss.aspx.cs" Inherits="MasterFiles_DCR_Analysiss" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link type="text/css" href="../css/SalesForce_New/bootstrap-select.min.css" rel="stylesheet" />
    <link href="https://code.jquery.com/ui/1.10.4/themes/ui-lightness/jquery-ui.css" rel="stylesheet">
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css" rel="stylesheet" />
    <style>
        .row {
            /*width: 1280px;
            height: 475px;*/
            resize: both;
    
        }

        /*.tab {
            float: left;
            border: 1px solid #ccc;
            background-color: #f1f1f1;
            width: 25%;
            height: 475px;
            font-size:12px;
           /* resize: horizontal;
            overflow: auto;*
        }

        .tab1 {
            float: left;
            border: 1px solid #ccc;
            background-color: #f1f1f1;
            width: 75%;
            height: 475px;
           /* resize: horizontal;
            *overflow: auto;*/
            /*-moz-tab-size: 4; /* Firefox *
        }*/
        #div1 {
            position:relative;
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
            position:relative;
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
            /*overflow: auto;*/
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
                <div class="row">
                    <label style="float: right;padding-right: 50px;display:none;" id="shwcnt">Show
                        <select class="data-table-basic_length" aria-controls="data-table-basic">
                            <option value="10">10</option>
                            <option value="25">25</option>
                            <option value="50">50</option>
                            <option value="100">100</option>
                        </select>
                        entries</label>
                </div>
                <button type='button' class='btn btn-secondary'  id='filterclear' style="display:none">Clear</button>
                <%--<button type='button' style='background-color:#1a73e8;' class='btn btn-primary'  id='filtrapply'>Apply</button>--%>
                <div class="tableclass">
                    <table class="table table-hover" id="OrderList" style="font-size: 10px;white-space: nowrap; display: none">
                        <thead class="text-warning">
                            <%--<tr><th>
                                
                                 <button id="btndropdown" class="btn btn-default dropdown-toggle" aria-label="settings" data-toggle="dropdown" type="button" title="Template settings" style="padding-left: 5px;"></button>
                                 <a href='#' class="dropdown-toggle" data-toggle="dropdown"><span><i class="fa fa-filter" style="font-size:15px; padding-left: 5px;"></i ></span ></a>
                                </th></tr>--%>                           
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
                <div class="row" id ="pagination_main" style="display:none">
                    <div class="col-sm-5">
                        <div class="dataTables_info" id="orders_info" role="status" aria-live="polite">Showing 1 to 10 of 57 entries</div>
                    </div>
                    <div class="col-sm-7">
                        <div class="dataTables_paginate paging_simple_numbers" id="example2_paginate">
                            <ul class="pagination">
                                <li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">Previous</a></li>
                                <li class="paginate_button active"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">1</a></li>
                                <li class="paginate_button "><a href="#" aria-controls="example2" data-dt-idx="2" tabindex="0">2</a></li>
                                <li class="paginate_button "><a href="#" aria-controls="example2" data-dt-idx="3" tabindex="0">3</a></li>
                                <li class="paginate_button "><a href="#" aria-controls="example2" data-dt-idx="4" tabindex="0">4</a></li>
                                <li class="paginate_button "><a href="#" aria-controls="example2" data-dt-idx="5" tabindex="0">5</a></li>
                                <li class="paginate_button "><a href="#" aria-controls="example2" data-dt-idx="6" tabindex="0">6</a></li>
                                <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="7" tabindex="0">Next</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="AddFieldsModal" tabindex="-1" style="z-index: 1000000; background: transparent; overflow: auto;" role="dialog" aria-labelledby="AddFieldsModal" aria-hidden="true">
                <div class="modal-dialog" role="document" style="width: 30%;">
                    <div class="modal-content" style="height: 400px; overflow-y: scroll">
                        <div class="modal-header" style="background-color: #bfefff;">
                            <h4>Template setting</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row" style="margin-bottom: 1rem!important;">
                                <label style="padding-top: 5px; color: #3c8dbc; padding-left: 20px;">AddTemplateName</label>
                                <input class='tgl tgl-skewed' id="txtName" type='textbox'/>
                                <div class="col-xs-12 col-sm-12" style="padding-left: 35px;padding-bottom: 5px;">
                                    <button type="button" class="btn btn-secondary"   id="slct_all">select All</button>
                                    <button type="button" class="btn btn-secondary" id="dslct_all">Deselect All</button>
                                </div>
                                <div class="col-xs-12 col-sm-12">
                                    <ul id="sortable1" style="padding-left: 20px;">                                        
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
        
        var MasFrms = [], listobj = [], sfCodematch = [], ResultArr = [] , prodcd = [], tempkey = []; templabel = [];
        var DCR_Products = [],DCR_FForceDayjoin = [], DCR_ProdDts = [], DCR_OrderDts = [], DCR_EventCap = [], DCR_FForce = [], DCR_Dayplan = [], DCR_Customer = [], DCR_tourplan = [];
        var templatelist = '', templateName = '', fdt = '', tdt = '', selectsfcode = '' ,rwStr = "";
        var DCR_Prod_Name = [], DCR_Prod_Code = [],bindArray = []; tempData = [],filterdataa = [];;
        listArray = [];
        listdata = "";
        objs = {};
        checkarr = [];
        colmnkey = "";
        lblval = "";
        filtrval = [];

        tempkey = ["Sf_Name","Sf_Joining_Date","Designation","dt","Wtype","ClstrName","Reporting_Sf","Reporting","Route","remarks","Cust_Name","Cust_Addr","Session","Cust_Code","Cust_Spec","Cust_Cls","Activity_Date","POB_Value","OrderTyp","Product","net_weight_value","stockist_name","filters","latlong","Activity_Remarks","Imgurl"]
        templabel  = ["Fieldforce Name","Joining Date","Designation","Date","Worktype","Route Worked","Reporting Manager 1","Reporting Manager 2","Route as per Tour Plan","Daywise Remark","Customer Name","Address","Session","Retailer Code","Retailer Channel","Retailer Class","Visit Date & Time","Order Value","Order Type","Product","net_weight_value","Distributor Name","Visited Outlets for day","Lat & Long","Activity_Remarks","Event Capture"]

        $(document).ready(function () {
            loadSfDetails();
            retrieveTemplateList();
        });

        $('#btnSettings').on('click', function () {
            for (i = 0; i < tempkey.length; i++) {
             $("#sortable1").append("<li class='ui-state-default'><span class='ui-icon ui-icon-arrowthick-2-n-s'></span><input class='tgl tgl-skewed' id='" + tempkey[i] + "' type='checkbox' value='" + tempkey[i] + "'/><label class='tgl' for='" + tempkey[i] + "style='height:1px;'>" + templabel[i] + "</label></li>");
            }
            $('#AddFieldsModal').modal('toggle');
        });
        
        $('#slct_all').on('click', function () {
             $('#sortable1').find('input[type="checkbox"]').prop('checked', true);
        });

        $('#dslct_all').on('click', function () {
             $('#sortable1').find('input[type="checkbox"]').prop('checked', false);
        });

        $('#closefields').on('click', function () {
            clearFields();
        });

        $('#svfields1').on('click', function () {
            templateName = $('#txtName').val();
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
            $('#pagination_main').hide();
            if (($('#mtemp :selected').val() == 'select') || ($('#mforms').val() == null)) {
                alert("Please select Name");
            }
            else {
                $('#loading-image').show();
                setTimeout(function () { getViewFields(); }, 3000);
            }            
        })

        $("#filterclear").on("click", function () {
            $('#loading-image').show();
            filtrval = [];
            $('.nav').find('input[type="checkbox"]').prop('checked', false);
            bindArray = ResultArr;
            setTimeout(function () {  ReloadTable();}, 3000);           
        });

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
                        if (fillfrms[$i].Sf_Code != 'admin') {
                            $('#mforms').append('<option value="' + fillfrms[$i].Sf_Code + '">' + fillfrms[$i].Sf_Name + '</option>');
                        }                            
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
            pgNo = 1;
            var id = '';
            var id = $('#ordDate').text();
            selectsfcode = '';
            sfCodematch = [];

            $('#mforms :selected').each(function () {
            selectsfcode += $(this).val() + ',';
            sfCodematch.push($(this).val());
            });

            id = id.split('-');
            fdt = id[2].trim() + '-' + id[1] + '-' + id[0];
            tdt = id[5] + '-' + id[4] + '-' + id[3].trim();
            loadData();

            showTabledata();
            $('#OrderList').show();
            $('#filterclear').show();
            $('#shwcnt').show();
        }
        
        unique_visit = [];
        function btndropdown(key, value) {
            listdata =  "<button type='button' style='background-color:#1a73e8;float:right;' onclick = 'filtrapply("+ key +")' class='filtrapply12'>Apply</button>";
            listArray = [];
            if (key == "filters") {
                $.each(filtrdt, function (i, el) {
                    if ($.inArray(el, unique_visit) === -1)
                        unique_visit.push(el);
                });
                listArray = unique_visit;
            }
            else {
                listArray = filterdata(key);
            }         
            
            for (j = 0; j < listArray.length; j++) {
                if (listArray[j] != "") {
                    listdata += "<li class='ui-state-default'><input class='tgl tgl-skewed' id=" + key + " type='checkbox' value=" + key + " /><label class='tgl' for=" + key + " style='height:1px;'>" + listArray[j] + "</label></li>";
                }                
            }
            //listdata += "<button type='button' style='background-color:#1a73e8;float:right;' onclick = 'filtrapply("+ key +")' class='filtrapply12'>Apply</button>";
            objs[key] = listArray; 
           // objs.push(obj);
            return listdata;             
        }       
        
        function filtrapply() {
            checkarr = [];
             $(".tgl:checked ").each(function () {
                 colmnkey = ($(this).attr('id'));
                 lblval = $(this).next("label").html();
                 //var value = $(this).val();
                 checkarr.push($(this).next("label").html());                
             });
             bindArray = clikfil(colmnkey);
             ReloadTable();
        }  
        
        function clikfil(keys) {
            var eachfiltr;
            filtrval = [];
            for (j = 0; j < checkarr.length; j++) {
                eachfiltr = bindArray.filter(function (a) {                            
                    return a[keys] == checkarr[j];                          
                });
                filtrval = filtrval.concat(eachfiltr);
            }            
            return  filtrval;
        }        

        function filterdata(key) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "DCR_Analysiss.aspx/uniqueFilter",
                data: "{ 'key':'"+ key + "'}",
                dataType: "json",
                success: function (data) {
                    filterdataa = data.d;
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
            return filterdataa;                       
        }
         
        function showTabledata() {
            loadTableData();
            var head = $("<tr/>");
            var prodh = $("<tr/>");
            var qSV = $("<tr/>");
            var indx = $('#mtemp :selected').val();

            DCR_Prod_Name = filterdata("Product_Name");
            DCR_Prod_Code = filterdata("Product_Code");

            $("#OrderList thead").html("");
            $("#OrderList tbody").html("");
            listobj = JSON.parse(tempData[indx].template_list) || [];
            head.append($("<th rowspan=3 style=background-color:grey;vertical-align:middle;>S.No</th>"));
            $("#OrderList thead").append(head);
            for (var key in listobj) {
                if (key != 'Product') {
                    if (key == 'Quantity' || key == 'value' || key == 'Prod_SKU') { }
                    else {
                        head.append($("<th rowspan=3 style=background-color:grey;vertical-align:middle;>" + listobj[key] +                            
                            //old "<ul class='nav' style='margin:0px'><li class='dropdown'><a href='#' style='padding:0px' class='dropdown-toggle' data-toggle='dropdown'><span><i class='fa fa-filter' style='float:right;margin-top:0px;margin-right:0px;padding-right: 10px;font-size: 15px;color: black;' /></span></a><ul class='dropdown-menu dropdown-custom dropdown-menu-right ddlStatus' style='right:0;left:0;overflow: scroll;'>" + btndropdown(key, listobj[key]) + "</ul><ul class='dropdown-menu dropdown-custom dropdown-menu-right ddlStatus' style='right:0;left:0;overflow: scroll;'><button type='button' style='background-color:#1a73e8;float:right;' onclick = 'filtrapply(" + key + ")' class='filtrapply12'>Apply</button></ul></li></ul></th > "));
                            //recent "<ul class='nav' style='margin:0px'><li class='dropdown'><a href='#' style='padding:0px' class='dropdown-toggle' data-toggle='dropdown'><span><i class='fa fa-filter' style='float:right;margin-top:0px;margin-right:0px;padding-right: 10px;font-size: 15px;color: black;'/></span></a> <ul class='dropdown-menu dropdown-custom dropdown-menu-right ddlStatus' style='right:0;left:0;overflow: scroll;'><li class='dropdown'> <button type='button' style='background-color:#1a73e8;float:right;' onclick = 'filtrapply(" + key + ")' class='filtrapply12'>Apply</button></li><li class='dropdown'><ul class='dropdown-menu dropdown-custom dropdown-menu-right ddlStatus' style='right:0;left:0;overflow: scroll;'>" + btndropdown(key, listobj[key]) + "</ul></li></ul></li></ul></th>"));
                             "<ul class='nav' style='margin:0px'><li class='dropdown'><a href='#' style='padding:0px' class='dropdown-toggle' data-toggle='dropdown'><span><i class='fa fa-filter' style='float:right;margin-top:0px;margin-right:0px;padding-right: 10px;font-size: 15px;color: black;' /></span></a><ul class='dropdown-menu dropdown-custom dropdown-menu-right ddlStatus' style='right:0;left:0;overflow: scroll;'>" + btndropdown(key, listobj[key]) + "</ul></li></ul></th >"));
                            $("#OrderList thead").append(head);
                    }
                }
                else {
                    prodhead = (DCR_Prod_Name.length) * 2;
                    head.append($("<th  colspan='" + prodhead + "' style=background-color:grey;text-align:center;> Product</th>"));
                    for (j = 0; j < DCR_Prod_Name.length; j++) {
                        prodh.append($("<th colspan=2 style=background-color:grey;>" + DCR_Prod_Name[j] + "</th>"));
                        a = ['Quantity', 'value'];
                        for (k = 0; k < a.length; k++) {
                            qSV.append($("<th style=background-color:grey;>" + a[k] + "</th>"));
                        }
                    } 
                    $("#OrderList thead").append(head);
                }
                $("#OrderList thead").append(prodh);
                $("#OrderList thead").append(qSV);
            }
            ReloadTable();
        }

        var pgNo = 1; PgRecords = 10; TotalPg = 0;

        $(".data-table-basic_length").on("change",
            function () {
            pgNo = 1;
            PgRecords = $(this).val();
            ReloadTable();
        });

        function loadPgNos() {
            prepg = parseInt($(".paginate_button.previous>a").attr("data-dt-idx"));
            Nxtpg = parseInt($(".paginate_button.next>a").attr("data-dt-idx"));
            $(".pagination").html("");
            TotalPg = parseInt(parseInt(bindArray.length / PgRecords) + ((bindArray.length % PgRecords) ? 1 : 0)); selpg = 1;
            if (isNaN(prepg)) prepg = 0;
            if (isNaN(Nxtpg)) Nxtpg = 2;
          //  if ((prepg + 1) == pgNo && pgNo > 1) selpg = (parseInt(pgNo) - 1);
            selpg =(pgNo > 7)? (parseInt(pgNo) + 1) - 7:1;
            if ((Nxtpg) == pgNo){
                 selpg = (parseInt(TotalPg)) - 7;
                 selpg =(selpg>1)? selpg:1;
            }
            spg = '<li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">First</a></li>';
            for (il = selpg - 1; il < selpg + 7; il++) {
                if (il < TotalPg)
                    spg += '<li class="paginate_button' + ((pgNo == (il + 1)) ? " active" : "") + '"><a href="#" aria-controls="example2" data-dt-idx="' + (il + 1) + '" tabindex="0">' + (il + 1) + '</a></li>';
            }
            spg += '<li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="' + TotalPg + '" tabindex="0">Last</a></li>';
            $(".pagination").html(spg);

            $(".paginate_button > a").on("click", function () {
                pgNo = parseInt( $(this).attr("data-dt-idx")); ReloadTable();
                /* $(".paginate_button").removeClass("active");
                 $(this).closest(".paginate_button").addClass("active");*/
            });
        }
        
        function ReloadTable() {      
            $("#OrderList TBODY").html("");
           
            st = PgRecords * (pgNo - 1); slno = 0;            
            for ($i = st; $i < st + PgRecords; $i++) {
                if ($i < bindArray.length) {                   
                    slno = $i + 1;
                    var rwStr = "";
                    rwStr += "<tr><td>" + slno + "</td>";
                    for (var key in listobj) {
                        if (key != 'Product') {
                            if (key == 'Quantity' || key == 'value' || key == 'Prod_SKU') {
                            }
                            else if (bindArray[$i].hasOwnProperty(key)) {
                                rwStr += "<td>" + bindArray[$i][key] + "</td>";
                            }
                            else
                                rwStr += "<td></td>";
                        }
                        else {
                            keys1 = ["sf_code", "dt", "Cust_Code"];
                            jArr = DCR_ProdDts.filter(function (a) {
                                iflg = 0; k = bindArray[$i];
                                for (ij = 0; ij < keys1.length; ij++) {
                                    if (a[keys1[ij]] != k[keys1[ij]]) iflg = 1;
                                }
                                return (iflg == 0);
                            });
                            for (jk = 0; jk < DCR_Prod_Code.length; jk++) {
                                if (jArr.length < 1) {
                                    rwStr += "<td></td><td></td>";
                                }
                                else {
                                    var newProd = jArr.filter(function (a) {
                                        return a.Product_Code == DCR_Prod_Code[jk];
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
                    $("#OrderList tbody").append(rwStr);
                }               
            }
            $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < bindArray.length) ? (st + PgRecords) : bindArray.length) + " of " + bindArray.length + " entries")
            $('#pagination_main').show();
            loadPgNos();
            $('#loading-image').hide();
        } 

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

        function loadTableData() {
            rs1 = joinObjects(DCR_FForce, DCR_Dayplan, ["sf_code"], ["sf_code"], '');
            rs2 = joinObjects(rs1, DCR_OrderDts, ["sf_code", "dt"], ["sf_code"], '');
            rs3 = joinObjects(rs2, DCR_tourplan, ["sf_code", "dt"], ["sf_code"], '');           
            rs4 = joinObjects(rs3, DCR_EventCap, ["sf_code", "dt"], ["sf_code"], '');
            ResultArr = joinObjects(rs4, DCR_Customer, ["sf_code", "dt", "Cust_Code"], ["sf_code"], 'Customer');
            
            bindArray = ResultArr;
        }

        result = [];
        filtrdt = [];

        function joinObjects(ar1, ar2, keys1, keys2, vstout) {
            result = []
            if (ar2 != []) {
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
                            if (vstout == 'Customer') {
                                    filtrdt.push(jArr.length);
                                }
                            for (var key in jArr[ij]) {
                                if (vstout == 'Customer') {
                                    ritem['filters'] = jArr.length;
                                }
                                ritem[key] = jArr[ij][key];                            
                            }                            
                            result.push(ritem);                     
                        }
                    }
                }
                return result;
            }            
        }

        function loadData() {
            /*$.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "DCR_Analysiss.aspx/GetDCR_Products",
                data: "{'SF':'" + selectsfcode +"','Div':'Session["Division_Code"]%>','Mn':'" + fdt + "','Yr':'" + tdt + "'}",
                dataType: "json",
                success: function (data) {
                    DCR_Products = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });*/
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
        } 
    </script>
    <script type="text/javascript">
        $(function () {
            $("#sortable1").sortable();
            $("#sortable1").disableSelection();
        });
    </script>
</asp:Content>
                                                                                                                                                                                                                                                                                                                                                                                                                                               