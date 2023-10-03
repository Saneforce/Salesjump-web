<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Route_Master.aspx.cs" Inherits="MasterFiles_Route_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form runat="server">
        <div class="card" style="margin-top:-5px !important">
            <div class="card-header">
                <div class="row" style="margin-bottom: 1rem;">
                    <div class="col-md-12" style="margin-bottom: 1rem;">
                        <div class="col-md-6 sub-header" style="float:left">
                            Route Master
                        </div>
                        <div class="col-md-6"  style="float: right">                               
                            <div class="col-md-3 sub-header" style="float: right">
                                <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png" 
                                    Style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 15px; " OnClick="ExportToExcel" />
                            </div>
                            <div class="col-md-3 sub-header" style="float: right;width:10%;">                                  
                                <span style="float: right;border-width: 0px; position: absolute; top: 5px;height: 40px; width: 40px;">
                                    <%-- <a href="New_Territory_Detail.aspx" class="btn btn-primary btn-update" id="newsf">--%>
                                         <a href="MR/Territory/Territory_Detail.aspx" class="btn btn-primary btn-update" id="newsf">
                                        Add Route
                                    </a>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>    
                
                <div class="hrow row">
                    <div class="col-md-12">
                        <div class="col-sm-4" style="margin-bottom: 1rem;">
                            <select id="ddlstate" class="form-control"></select>
                        </div>
                        <div class="col-sm-4" style="margin-bottom: 1rem;">
                            <select id="ddlhq" class="form-control">
                                <option value="">Select HQ</option>
                            </select>
                        </div>
                        <div class="col-sm-4" style="margin-bottom: 1rem;">
                            <select id="ddlsf" class="form-control">
                                <option value="">Select FieldForce</option>
                            </select>
                        </div>       
                    </div>    
                </div>
            </div>    
            <div class="card-body table-responsive" style="overflow-x: auto;">
                <div style="white-space: nowrap">
                    <div class="fieldsetting" style="width:30px;height:30px;float: right;padding-bottom:10px;padding-left:10px;padding-right:10px;padding-top:10px;">
                        <button type="button" class="btnsettings" id="btnsettings"><i class="fa fa-cog"></i></button>
                    </div>
                    Search&nbsp;&nbsp;<input type="text" id="tSearchOrd" style="width: 250px;" />
                    <label style="float: right">
                        Show
                        <select class="data-table-basic_length" aria-controls="data-table-basic">
                            <option value="10">10</option>
                            <option value="25">25</option>
                            <option value="50">50</option>
                            <option value="100">100</option>
                        </select>entries</label>
                    <div style="float: right;padding-top: 3px;">
                        <ul class="segment">
                            <li data-va='All'>ALL</li>
                            <li data-va='0' class="active">Active</li>                                    
                        </ul>
                    </div>
                </div>  
                <br />
                <table class="table table-hover" id="OrderList" style="font-size: 12px">
                    <thead class="text-warning">
                        <tr style="background-color: #37a4c6; color: #fff; white-space: nowrap;">
                            <th style="text-align: left; color: #fff">Sl.No</th>
                            <th style="text-align: left; color: #fff;">Route Code</th>
                            <th style="text-align: left; color: #fff">Route Name</th>
                            <th style="text-align: left; color: #fff">Distributor Name</th>
                            <th style="text-align: left; color: #fff">FieldForce Name</th>
                            <th style="text-align: left; color: #fff">Retailer Count</th>
                            <th style="text-align: left; color: #fff">Target</th>
                            <th style="text-align: left; color: #fff">Edit</th>
                            <th style="text-align: left; color: #fff">Status</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>

                <div class="row" style="padding: 5px 0px">
                    <div class="col-sm-5">
                        <div class="dataTables_info" id="orders_info" role="status" aria-live="polite">Showing 0 to 0 of 0 entries</div>
                    </div>
                    <div class="col-sm-7">
                        <div class="dataTables_paginate paging_simple_numbers" id="example2_paginate">
                            <ul class="pagination" style="float: right; margin: -11px 0px">
                                <li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">Previous</a></li>
                                <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="7" tabindex="0">Next</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            
            <div class="modal fade" id="RetailerModal" style="z-index: 10000000; background: transparent; overflow-y: scroll;" tabindex="0" aria-hidden="true">
                <div class="modal-dialog" role="document" style="width: 30% !important">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="RouteModalLabel"></h5>
                        </div>
                        <div class="modal-body" style="padding-top: 10px">
                            <div class="row">
                                <div class="col-sm-12">
                                    <table id="retailerdets" style="width: 100%; font-size: 12px;">
                                        <thead class="text-warning">
                                            <tr>
                                                <th style="text-align: left">SlNO</th>
                                                <th style="text-align: left">Retailer Name</th>
                                                <th style="text-align: left">Status</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
            
            <div class="modal fade" id="CustomFieldModal" style="z-index: 10000000; background: transparent; overflow-y: scroll;" tabindex="0" aria-hidden="true">
                <div class="modal-dialog" role="document" style="width: 30% !important">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="CustomFieldModalLabel"></h5>
                        </div>
                        <div class="modal-body" style="padding-top: 10px">
                            <div class="row">
                                <div class="col-sm-12">
                                    <table id="CustoFielddets" cellpadding="0" cellspacing="0" class="table" style="width: 100%; font-size: 12px;">
                                        <thead class="text-warning">
                                            <tr>
                                                <th style="text-align: left">S.No</th>
                                                <th style="text-align: left">Field Name</th>
                                                <th class="hide" style="text-align: left">Field Column</th>
                                                <th class="hide" style="text-align: left">Status</th>
                                                <th style="text-align: left"><input type="checkbox" name="checkAll" id="checkAll" class="checkAll" /></th>
                                            </tr>
                                        </thead>
                                        <tbody style="height:150px !important;overflow-x:scroll;"></tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-primary" onclick="ApplyFields()" data-dismiss="modal">Apply</button>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
    <script language="javascript" type="text/javascript">
        var AllOrders = []; var Allsf = []; var hqsf = []; var Allstate = []; var Ostate = []; var AllHQ = []; var SFHQ = [];
        var stkdetails = [], sfdets = [];
        var filtrkey = '0';
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Territory_Code,Territory_Name,Status,";
        $(".data-table-basic_length").on("change", function () {
            pgNo = 1;
            PgRecords = $(this).val();
            ReloadTable();
        });

        function ApplyFields() {

        }

        function loadPgNos() {
            prepg = parseInt($(".paginate_button.previous>a").attr("data-dt-idx"));
            Nxtpg = parseInt($(".paginate_button.next>a").attr("data-dt-idx"));
            $(".pagination").html("");
            TotalPg = parseInt(parseInt(Orders.length / PgRecords) + ((Orders.length % PgRecords) ? 1 : 0)); selpg = 1;
            if (isNaN(prepg)) prepg = 0;
            if (isNaN(Nxtpg)) Nxtpg = 2;
            //  if ((prepg + 1) == pgNo && pgNo > 1) selpg = (parseInt(pgNo) - 1);
            selpg = (pgNo > 7) ? (parseInt(pgNo) + 1) - 7 : 1;
            if ((Nxtpg) == pgNo) {
                selpg = (parseInt(TotalPg)) - 7;
                selpg = (selpg > 1) ? selpg : 1;
            }
            spg = '<li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">First</a></li>';
            for (il = selpg - 1; il < selpg + 7; il++) {
                if (il < TotalPg)
                    spg += '<li class="paginate_button' + ((pgNo == (il + 1)) ? " active" : "") + '"><a href="#" aria-controls="example2" data-dt-idx="' + (il + 1) + '" tabindex="0">' + (il + 1) + '</a></li>';
            }
            spg += '<li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="' + TotalPg + '" tabindex="0">Last</a></li>';
            $(".pagination").html(spg);

            $(".paginate_button > a").on("click", function () {
                pgNo = parseInt($(this).attr("data-dt-idx")); ReloadTable();
                /* $(".paginate_button").removeClass("active");
                 $(this).closest(".paginate_button").addClass("active");*/
            }
            );
        }

        function ReloadTable() {
            $("#OrderList TBODY").html("");
            if (filtrkey != "All") {
                Orders = Orders.filter(function (a) {
                    return a.Territory_Active_Flag == filtrkey;
                })
            }
            st = PgRecords * (pgNo - 1);
            for ($i = st; $i < st + Number(PgRecords); $i++) {
                if ($i < Orders.length) {
                    let stknm = stkdetails.filter(function (a) {
                        return (',' + Orders[$i].Stockist + ',').indexOf(',' + a.Stockist_Code + ',') > -1;
                    }).map(function (el) {
                        return el.Stockist_Name
                    }).join(',').toString();
                    let sfname = sfdets.filter(function (a) {
                        return (',' + Orders[$i].SF_Code + ',').indexOf(',' + a.Sf_Code + ',') > -1;
                    }).map(function (el) {
                        return el.Sf_Name
                    }).join(',').toString();
                    tr = $("<tr rname='" + Orders[$i].Territory_Name + "' rocode='" + Orders[$i].Territory_Code + "'></tr>");
                    $(tr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].Territory_Code + "</td><td>" + Orders[$i].Territory_Name + "</td><td>" + stknm + "</td><td>" + sfname + "</td><td class='rocount' tcode='" + Orders[$i].Territory_SName + "'><a href='#'>" + Orders[$i].Reatailer_Count + "</a></td><td>" + Orders[$i].Target + "</td><td class='roedit'><a href='#'>Edit</a></td><td class='rodeact'><a href='#'>" + Orders[$i].Status + "</a></td>");
                    $("#OrderList TBODY").append(tr);
                }
            }
            $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")
            loadPgNos();
        }

        $(".segment>li").on("click", function () {
            $(".segment>li").removeClass('active');
            $(this).addClass('active');
            filtrkey = $(this).attr('data-va');
            Orders = AllOrders;
            $("#tSearchOrd").val('');
            ReloadTable();
        });

        $("#tSearchOrd").on("keyup", function () {
            if ($(this).val() != "") {
                shText = $(this).val().toLowerCase();
                Orders = AllOrders.filter(function (a) {
                    chk = false;
                    $.each(a, function (key, val) {
                        // if (val != null && val.toString().toLowerCase().indexOf(shText) > -1 && (',' + searchKeys).indexOf(',' + key + ',') > -1) {
                        if (val != null && val.toString().toLowerCase().substring(0, shText.length) == shText && (',' + searchKeys).indexOf(',' + key + ',') > -1) {
                            chk = true;
                        }
                    })
                    return chk;
                })
            }
            else
                Orders = AllOrders
            ReloadTable();
        });

        function fillstate() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Route_Master.aspx/getStates",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    Allstate = JSON.parse(data.d) || [];
                    var st = $("#ddlstate");
                    st.empty().append('<option selected="selected" value="">Select State</option>');
                    for (var i = 0; i < Allstate.length; i++) {
                        st.append($('<option value="' + Allstate[i].State_Code + '">' + Allstate[i].StateName + '</option>'));
                    };
                }
            });
        }

        function getSalesforce() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Route_Master.aspx/getSalesforce",
                dataType: "json",
                success: function (data) {
                    sfdets = JSON.parse(data.d);
                }
            });
        }

        function getStockistDetails() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Route_Master.aspx/getStockist",
                dataType: "json",
                success: function (data) {
                    stkdetails = JSON.parse(data.d);
                }
            });
        }

        function fillHQ(sst) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Route_Master.aspx/getHQ",
                data: "{'divcode':'<%=Session["div_code"]%>','Sstate':'" + sst + "'}",
                dataType: "json",
                success: function (data) {
                    Allsf = JSON.parse(data.d);
                    hqsf = Allsf;
                    var hq = $("#ddlhq");
                    hq.empty().append('<option selected="selected" value="">Select HQ</option>');
                    for (var i = 0; i < hqsf.length; i++) {
                        hq.append($('<option value="' + hqsf[i].Hq_Code + '">' + hqsf[i].Sf_HQ + '</option>'));
                    };
                }
            });
        }

        function fillSF(shq) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Route_Master.aspx/getSF",
                data: "{'divcode':'<%=Session["div_code"]%>','Hq':'" + shq + "'}",
                dataType: "json",
                success: function (data) {
                    AllHQ = JSON.parse(data.d);
                    SFHQ = AllHQ;
                    var ddsf = $("#ddlsf");
                    ddsf.empty().append('<option selected="selected" value="">Select Fieldforce</option>');
                    for (var i = 0; i < SFHQ.length; i++) {
                        ddsf.append($('<option value="' + SFHQ[i].Sf_Code + '">' + SFHQ[i].Sf_Name + '</option>'));
                    };
                }
            });
        }

        function fillRoutes(sfs) {
            $('#OrderList TBODY').html("<tr><td colspan=8>Loading please wait...</td></tr>");
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "Route_Master.aspx/getRoutes",
                data: "{'divcode':'<%=Session["div_code"]%>','sf':'" + sfs + "'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders; ReloadTable();
                },
                error: function (result) {
                    $('#OrderList TBODY').html("<tr><td colspan=8>Something Went Wrong...</td></tr>");
                }
            });
        }

        function fillRetailers(sfs) {
            $('#retailerdets tbody').html('');
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Route_Master.aspx/getRoute_Retailers",
                data: "{'divcode':'<%=Session["div_code"]%>','route_code':'" + sfs + "'}",
                dataType: "json",
                success: function (data) {
                    var AllOrders2 = JSON.parse(data.d) || [];
                    $('#retailerdets TBODY').html("");
                    var slno = 0;
                    for ($i = 0; $i < AllOrders2.length; $i++) {
                        if (AllOrders2.length > 0) {
                            slno += 1;
                            tr = $("<tr></tr>");
                            var stat = (AllOrders2[$i].ListedDr_Active_Flag == 0) ? 'Active' : 'Inactive';
                            $(tr).html("<td>" + slno + "</td><td>" + AllOrders2[$i].ListedDr_Name + "</td><td>" + stat + "</td>");
                            $("#retailerdets TBODY").append(tr);
                        }
                    }
                }
            });
        }

        function deactRoute($routeCode,$deactFlag,$indx) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Route_Master.aspx/deactivateRoutes",
                data: "{'Territory_Code':'" + $routeCode + "','stat':'" + $deactFlag + "'}",
                dataType: "json",
                success: function (data) {
                    if (data.d == 'Success') {
                        if ($deactFlag == '2') {
                            $deactFlag = 0;
                        }
                        else if($deactFlag=='3'){
                            $deactFlag = 1;
                        }
                        Orders[$indx]["Territory_Active_Flag"] = $deactFlag;
                        Orders[$indx]["Status"] = ($deactFlag == 0) ? "Deactivate" : "Activate";
                        ReloadTable();
                        alert('Status Changed Successfully...');
                    }
                    else {
                        alert("Status Can't be Changed");
                    }
                },
                error: function (result) {
                    alert(result.responseText);
                }
            });
        }

        $(document).ready(function () {
			if ('<%=sf_type%>' != '3') {
                $('.pad').css('display', 'none');
				$('.hrow').css('display', 'none');
                $('#<%=ImageButton1.ClientID%>').hide();
            }
            
            $('.btnsettings').hide();

            fillRoutes('admin');

            fillstate();

            getSalesforce();

            getStockistDetails();

            $('#ddlstate').on('change', function () {
                var stt = $(this).val();
                if (stt != '') {
                    fillHQ(stt);
                }
                else {
                    alert('Select State');
                    $('#ddlhq').empty();
                    $('#ddlsf').empty();
                    return false;
                }
            });
            $('#ddlhq').on('change', function () {
                var hqn = $('#ddlhq :selected').text();
                if (hqn != 'Select HQ') {
                    fillSF(hqn);
                }
                else {
                    alert('Select HQ');
                    $('#ddlsf').empty();
                    return false;
                }
            });

            $('#ddlsf').on('change', function () {
                var sfco = $('#ddlsf').val();
                if (sfco != '') {
                    fillRoutes(sfco);
                }
                else {
                    alert('Select Fieldforce');
                    return false;
                }
            });

            $(document).on('click', '.rocount', function () {
                $('#RetailerModal').modal('toggle');
                var route_C = $(this).closest('tr').attr('rocode');
                var route_N = $(this).closest('tr').attr('rname');
                $('#RouteModalLabel').text("Retailers for Route -" + route_N);
                fillRetailers(route_C);
            });

            $(document).on('click', '.roedit', function () {
                var route_C = parseInt($(this).closest('tr').attr('rocode'));
                window.location.href = "MR/Territory/Territory_Detail.aspx?&Territory_Code=" + route_C + "";
            });

            $(document).on("click", ".rodeact", function () {
                var route_C = $(this).closest('tr').attr('rocode');
                var sfco = $('#ddlsf').val();
                let oindex = Orders.findIndex(x => x.Territory_Code == route_C);
                let disstat = Orders[oindex]["Status"].toString();
                let flg = (parseInt(Orders[oindex]["Territory_Active_Flag"]) == 0) ? 1 : 0;
                var rocnt = isNaN(parseInt($(this).closest('tr').find('.rocount>a').html())) ? 0 : parseInt($(this).closest('tr').find('.rocount>a').html());
                if ((rocnt > 0 && flg == 1) || (flg == 0)) {
                    if (confirm("Do you want " + disstat + " the Route and it's Outlets ?")) {
                        if (disstat == "Activate") {
                            deactRoute(route_C, 2, oindex);
                        }
                        else if (disstat == "Deactivate") {
                            deactRoute(route_C, 3, oindex);
                        }
                    }
                }
                else if ((rocnt < 1 && flg == 1) || (flg == 0)) {
                    if (confirm("Do you want " + disstat + " the Route ?")) {
                        deactRoute(route_C, flg, oindex);
                    }
                }
                else {
                    alert("Status Can't be Changed");
                }
            });

            $(document).on('click', '.btnsettings', function () {
                $('#CustomFieldModal').modal('toggle');
                loadCustomFields();
                $('#CustomFieldModalLabel').text("Addtional Fields for Route" );                
            });
        });

        function loadCustomFields() {
            var ModuleId = "4";
            var Sf = 'admin';
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Route_Master.aspx/GetCustomFormsFieldsColumns",
                data: "{'divcode':'<%=Session["div_code"]%>','ModuleId':'" + ModuleId + "','Sf':'" + Sf + "'}",
                dataType: "json",
                success: function (data) {
                    $('#CustoFielddets TBODY').html("");
                    //Orders = JSON.parse(data.d) || [];
                    MasFrms = JSON.parse(data.d) || [];
                    console.log(MasFrms);
                    if (MasFrms.length > 0) {
                        for (var i = 0; i < MasFrms.length; i++) {

                            tr = $("<tr class='" + MasFrms[i].Field_Col + " row-select' id='" + MasFrms[i].Field_Col + "'></tr>");
                            //var hq = filtered[$i].Sf_Name.split('-');
                            slno = i + 1;
                            var fldtype = MasFrms[i].Field_Type;
                            //alert(fldtype);
                            var checkbox = "";
                            if (fldtype == "M") {
                                checkbox = "<input type='checkbox' checked='checked' disabled='disabled' class='" + MasFrms[i].Field_Type + "' id='" + MasFrms[i].Field_Col + "' name='" + MasFrms[i].Field_Type + "' value='" + MasFrms[i].Field_Type + "' />";
                            }
                            else {
                                checkbox = "<input type='checkbox' class='" + MasFrms[i].Field_Type + "' id='" + MasFrms[i].Field_Col + "' name='" + MasFrms[i].Field_Type + "' value='" + MasFrms[i].Field_Type + "' />";
                            }
                            $(tr).html('<td><i class="fa fa-ellipsis-v tbltrmove"></i></td><td class="fldName">' + MasFrms[i].Field_Name +
                                '</td><td class="hide fldcol">' + MasFrms[i].Field_Col +
                                '</td><td class="hide fldtype"> ' + MasFrms[i].Field_Type +
                                '</td><td class="hide fldvb"> ' + MasFrms[i].Field_Visible +
                                '</td><td class="hide fldorder"> ' + MasFrms[i].Field_Order +
                                '</td><td class="frmedit">' + checkbox + '</td>');
                            $("#CustoFielddets TBODY").append(tr);

                        }
                    }
                },
                error: function (data) {
                    alert(JSON.stringify(data.d));
                }
            });

        }

        $('#checkAll').click(function () {
            isChecked = $(this).prop("checked");
            /*$('#CustoFielddets tbody').find('input[type="checkbox"]').prop('checked', isChecked);*/
            //MasFrms[0].Field_Visible = isChecked

            $('#CustoFielddets tbody').find('input[type="checkbox"]').each(function () {
                if ($(this).prop("checked") == false) {
                    $(this).prop("checked", isChecked);
                    var tblIndex = $(this).closest('tr').index();
                    if (MasFrms.length > 0) {
                        MasFrms[tblIndex].Field_Visible = $(this).prop("checked");
                    }
                }
                else {
                    var tblIndex = $(this).closest('tr').index();
                    if (MasFrms.length > 0) {
                        MasFrms[tblIndex].Field_Visible = $(this).prop("checked");
                    }
                }
            });
        });

        $('#CustoFielddets tbody').on('click', 'input[type="checkbox"]', function (e) {

            var isChecked = $(this).prop("checked");

            var tblIndex = $(this).closest('tr').index();

            //MasFrms[tblIndex].Field_Visible = $(this).prop("checked");
            if (MasFrms.length > 0) {
                MasFrms[tblIndex].Field_Visible = isChecked;

                var isHeaderChecked = $("#checkAll").prop("checked");

                if (isChecked == false && isHeaderChecked) {
                    $("#checkAll").prop('checked', isChecked);
                }
                else {
                    $('#CustoFielddets tbody').find('input[type="checkbox"]').each(function () {
                        if ($(this).prop("checked") == false) {
                            isChecked = false;
                        }
                    });
                    console.log(isChecked);
                    $("#checkAll").prop('checked', isChecked);
                }
            }

            //console.log(MasFrms);
        });

    </script>
</asp:Content>
