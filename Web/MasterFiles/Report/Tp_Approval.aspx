<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Tp_Approval.aspx.cs" Inherits="MasterFiles_Report_Tp_Approval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="../../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type="text/css" />
    <link href="../../css/jquery.multiselect.css" rel="stylesheet" />
<style>
    #txtfilter_chzn
    {
        width: 328px !important;
        position: absolute;
    }
    li
    {
        font-weight: 100;
    }
    .txtover
    {
        text-overflow: ellipsis;
    }
    .tpremove{
        color:red;
    }
    .tpedit{
        color:#1a73e8;
    }
    div > .dropdown.bootstrap-select
    {
        margin-right: 5px;
        float: right;
    }
    [data-val='Rejected']
    {
        color: #dc3545;
    }
    
    [data-val='Approved']
    {
        color: #28a745;
    }
    [data-val='Reject']
    {
        color: #dc3545;
    }
    
    [data-val='Approve']
    {
        color: #28a745;
    }
    
    [data-val='Pending']
    {
        color: #ffc107;
    }
    span.b {
        white-space: nowrap; 
        width: 150px; 
        overflow: hidden;
        text-overflow: ellipsis;
        display: block; 
    }
    .tooltip {
        position: relative;
        display: inline-block;
        color: #006080;
        opacity:1;
    }
    .tooltiptext {
        visibility: hidden;
        background-color: black;
        color: #fff;
        text-align: center;
        border-radius: 6px;
        padding: 5px 0;
        position: absolute;
        z-index: 100000;
    }

    td:hover .tooltiptext {
        visibility: visible;
    }
</style>
    <form id="frm1" runat="server">
        <div class="modal fade" id="editmodal" tabindex="-1" style="z-index: 2147483647;background-color: transparent;" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog" role="document" style="width:50%">
                    <div class="modal-content">
                        <div class="modal-header">
                            <div class="row">
                                <div class="col-sm-10">
                                    <h5 class="modal-title">TP Edit</h5>
                                </div>
                                <div class="col-sm-2"></div>
                            </div>
                        </div>
                    <div class="modal-body" style="padding-top: 10px">
                        <div class="row">
                            <div class="col-sm-12">
                               <table width="100%" style="table-layout:fixed">
                                   <tr>
                                       <td>Tour Date</td><td>Work Type</td>
                                   </tr>
                                   <tr>
                                       <td>
                                           <input class="form-control" type="date" id="eDate" />
                                       </td>
                                       <td>
                                           <select class="form-control" id="eWorktyp"></select>
                                       </td>
                                   </tr>
                                   <tr>
                                       <td colspan="2">Distributor</td>
                                   </tr>
                                   <tr>
                                       <td colspan="2">
                                           <select id="edist" multiple></select>
                                       </td>
                                   </tr>
                                   <tr>
                                       <td colspan="2">Route</td>
                                   </tr>
                                   <tr>
                                       <td colspan="2">
                                           <select id="eroute" data-dropup-auto="false"></select>
                                       </td>
                                   </tr>
                                   <tr>
                                       <td colspan="2">Retailer</td>
                                   </tr>
                                   <tr>
                                       <td colspan="2">
                                           <select id="eretailer" multiple></select>
                                       </td>
                                   </tr>
                                   <tr>
                                       <td colspan="2">Remarks</td>
                                   </tr>
                                   <tr>
                                       <td  colspan="2">
                                           <input class="form-control" type="text" id="eremarks" />
                                       </td>
                                   </tr>
                                   <tr>
                                       <td>POB</td><td>SOB</td>
                                   </tr>
                                   <tr>
                                       <td>
                                           <input class="form-control" type="number" id="epob" />
                                       </td>
                                       <td>
                                           <input class="form-control" type="number" id="esob" />
                                       </td>
                                   </tr>
                               </table>
                            </div>
                        </div>
                    </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal" data-toggle="modal" data-target="#exampleModal">Close</button>
                            <button type="button" class="btn btn-primary" id="btnupdate" onclick="updateTP()">Update</button>
                        </div>
                    </div>
                </div>
            </div>         
        <div class="modal fade" id="exampleModal" tabindex="-1" style="z-index: 1000000;background-color: transparent;" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog" role="document" style="width:90%">
                    <div class="modal-content">
                        <div class="modal-header">
                            <div class="row">
                                <div class="col-sm-10">
                                    <h5 class="modal-title" id="exampleModalLabel"></h5>
                                </div>
                                <div class="col-sm-2">
                                    <asp:imagebutton id="ImageButton1" runat="server" align="right" imageurl="~/img/Excel-icon.png"
                                        style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 39px; top: -9px;" onclick="ImageButton1_Click" /></div>
                            </div>
                        </div>
                    <div class="modal-body" style="padding-top: 10px">
                        <div class="row">
                            <div class="col-sm-12">
                                <table id="tpdets" class="table table-hover" style="width: 100%;font-size:12px;">
                                    <thead class="text-warning" style="white-space:nowrap">
                                        <tr>
                                            <th style="text-align: left">SlNO</th>
                                            <th style="text-align: left">Tour Date</th>
                                            <th style="text-align: left">Work Type</th>
                                            <th class="chcl" style="text-align: left">Worked With</th>
                                            <th style="text-align: left">Route</th>
                                            <th style="text-align: left">Remarks</th>
                                            <th class="hcl" style="text-align: left">Retailer Name</th>
                                            <th class="hcl" style="text-align: left">POB</th>
                                            <th class="hcl" style="text-align: left">SOB</th>
                                            <th class="hcl" style="text-align: left">Daywise Total</th>
                                            <th style="text-align: left">Actions</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                    <tfoot>
                                        <tr>
                                        <th style="text-align: right" colspan="7">Total</th>
                                        <th style="text-align: left" id="tpob"></th>
                                        <th style="text-align: left" id="tsob"></th>
                                        <th style="text-align: left" id="gtot"></th>
                                        <th></th>
                                        </tr>
                                    </tfoot>
                                </table>
                            </div>
                        </div>
                    </div>
                        <div class="modal-footer">
                            <button type="button" style="background-color: #6c757d;" class="btn btn-secondary" data-dismiss="modal">Close</button>
                            <button type="button" style="background-color: rgb(26 115 232 / 1);" class="btn btn-primary" id="apprtp">Approve</button>
                            <button type="button" style="background-color: #dc3545" class="btn btn-primary" id="rejtp">Reject</button>
                        </div>
                    </div>
                </div>
            </div>
        <div class="row">
            <div class="col-lg-12 sub-header">
                TP Approval
                <select id="fltpyr" data-size="5" data-dropup-auto="false" style="float:right">
                </select>
                <select id="fltpmnth" data-size="5" data-dropup-auto="false" style="float:right">
                    <option value="1">January</option>
                    <option value="2">February</option>
                    <option value="3">March</option>
                    <option value="4">April</option>
                    <option value="5">May</option>
                    <option value="6">June</option>
                    <option value="7">July</option>
                    <option value="8">August</option>
                    <option value="9">September</option>
                    <option value="10">October</option>
                    <option value="11">November</option>
                    <option value="12">December</option>
                </select>
            </div>
        </div>


        <div class="card">
            <div class="card-body table-responsive">
                <div style="white-space: nowrap"><input type="text" id="tSearchOrd" style="width: 250px;" placeholder="Search" />
                    <label style="float: right">Show
                        <select class="data-table-basic_length" aria-controls="data-table-basic">
                            <option value="10">10</option>
                            <option value="25">25</option>
                            <option value="50">50</option>
                            <option value="100">100</option>
                        </select>
                        entries</label>
                    <div style="float: right; padding-top: 4px;">
                        <ul class="segment">
                            <li data-va='All' class="active">ALL</li>
                            <li data-va='Pending'>Pending</li>
                            <li data-va="Approved">Approved</li>
                            <li data-va="Rejected">Rejected</li>
                            <li data-va="Not Submitted">Others</li>
                        </ul>
                    </div>
                </div>
                <table class="table table-hover" id="OrderList" style="font-size: 12px;">
                    <thead class="text-warning">
                        <tr style="white-space: nowrap;">
                            <th style="text-align:left">SlNo</th>
                            <th style="text-align:left"><input type="checkbox" id="sall" onclick="Achkall()" /> Select</th>
                            <th style="text-align:left">Is Approved</th>
                            <th style="text-align:left">Status</th>
                            <th style="text-align:left">Employee ID</th>
                            <th style="text-align:left">Employee Name</th>
                            <th style="text-align:left">Designation</th>
                            <th style="text-align:left">HQ</th>
                            <th style="text-align:left">Reporting To</th>
                            <th style="text-align:left">View</th
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
                <div class="row">
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
        </div>
        <center>
            <button type="button" class="btn btn-primary" style="background-color: rgb(26 115 232 / 1);" onclick="svMasTp()">Save</button>
        </center>
    </form>
    <script type="text/javascript" src="../../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../../js/plugins/datatables/dataTables.bootstrap.js"></script>
    <script type="text/javascript" src="../../js/plugins/table2excel.js"></script>
    <script language="javascript" type="text/javascript">
        var AllOrders = [], AllTP = [], CTP = [];
        var sf = '', sfty = '';
        var filtrkey = 'All';
        var mdsf = '';
        var dt = new Date();
        var pgYR = dt.getFullYear();
        var pgMNTH = dt.getMonth() + 2;
        var trsfcl = '';
        optStatus = "<li><a href='#' v='2'>Approve</a><a href='#' v='-1'>Reject</a></li>"
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Sf_Code,sf_emp_id,SFNA,DSN,sf_hq,Reporting_To,isApproved,";
        $(".data-table-basic_length").on("change",
            function () {
                pgNo = 1;
                PgRecords = $(this).val();
                ReloadTable();
            }
        );


        function getTPRetail(sfco, rco) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Tp_Approval.aspx/getSFTPRetailer",
                data: "{'divcode':'<%=Session["div_code"]%>','sfcode':'" + sfco + "','routecode':'" + rco + "'}",
                dataType: "json",
                success: function (data) {
                    var TPRetails = JSON.parse(data.d) || [];
                    var tpr = $("#eretailer");
                    tpr.empty();
                    for (var i = 0; i < TPRetails.length; i++) {
                        tpr.append($('<option value="' + TPRetails[i].ListedDrCode + '">' + TPRetails[i].ListedDr_Name + '</option>'));
                    }
                    $('#eretailer').multiselect({
                        columns: 3,
                        placeholder: 'Select Retailer',
                        search: true,
                        searchOptions: {
                            'default': 'Search Retailer'
                        },
                        selectAll: true
                    }).multiselect('reload');
                    $('.ms-options ul').css('column-count', '3');
                },
                error: function (rs) {

                }
            });
        }
        function getTPWtypes(x) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Tp_Approval.aspx/getSFTPWtypes",
                data: "{'divcode':'<%=Session["div_code"]%>','sfcode':'" + x + "'}",
                dataType: "json",
                success: function (data) {
                    var TPWtyps = JSON.parse(data.d) || [];
                    var tpr = $("#eWorktyp");
                    tpr.empty();
                    for (var i = 0; i < TPWtyps.length; i++) {
                        tpr.append($('<option value="' + TPWtyps[i].WCode + '">' + TPWtyps[i].WName + '</option>'));
                    }
                },
                error: function (rs) {

                }
            });
        }
        function getTPRoutes(x) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Tp_Approval.aspx/getSFTPRoute",
                data: "{'divcode':'<%=Session["div_code"]%>','sfcode':'" + x + "'}",
                dataType: "json",
                success: function (data) {
                    var TPRoutes = JSON.parse(data.d) || [];
                    var tpr = $("#eroute");
                    tpr.empty().append($('<option value="0">Select Route</option>'));
                    for (var i = 0; i < TPRoutes.length; i++) {
                        tpr.append($('<option value="' + TPRoutes[i].Territory_Code + '">' + TPRoutes[i].Territory_Name + '</option>'));
                    }
                    $('#eroute').selectpicker({
                        liveSearch: true
                    });
                    $('#eroute').selectpicker('refresh');
                },
                error: function (rs) {

                }
            });
        }
        function getTPDist(x) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Tp_Approval.aspx/getSFTPDist",
                data: "{'divcode':'<%=Session["div_code"]%>','sfcode':'" + x + "'}",
                dataType: "json",
                success: function (data) {
                    var TPDist = JSON.parse(data.d) || [];
                    var tpr = $("#edist");
                    tpr.empty();
                    for (var i = 0; i < TPDist.length; i++) {
                        tpr.append($('<option value="' + TPDist[i].Stockist_Code + '">' + TPDist[i].Stockist_Name + '</option>'));
                    }
                    $('#edist').multiselect({
                        columns: 3,
                        placeholder: 'Select Distributor',
                        search: true,
                        searchOptions: {
                            'default': 'Search Distributor'
                        },
                        selectAll: true
                    }).multiselect('reload');
                    $('.ms-options ul').css('column-count', '3');
                },
                error: function (rs) {

                }
            });
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
        function Achkall() {
            if ($('#sall').is(":checked")) {
                Orders = Orders.map(function (o) {
                    if (sfty == '3' && o.Stat == 3 || (o.Reporting_To=='admin' && o.Stat == 1)) {
                        o.isApproved = "Approve";
                    }
                    if (sfty == '2' && o.Stat == 1) {
                        o.isApproved = "Pending";
                    }
                    return o;
                });
            }
            else {
                Orders = Orders.map(function (o) {
                    if (sfty == '3' && o.Stat == 3 || (o.Reporting_To=='admin' && o.Stat == 1)) {
                        o.isApproved = "Approve";
                    }
                    if (sfty == '2' && o.Stat == 1) {
                        o.isApproved = "Pending";
                    }
                    if (o.Status == "Pending with Admin" || o.Status == "Pending with Manager") {
                        o.isApproved = "Pending";
                    }
                    return o;
                });
            }
            ReloadTable();
        }
        function Atdclick($Ack) {
            Chkclick(($($Ack).find('.Approve')));
        }
        function Chkclick($Chk) {
            var trsf = parseInt($($Chk).closest("tr").find('td').eq(0).text()) - 1;
            if (!$($Chk).is(":checked")) {
                $($Chk).prop('checked', true);
                $($Chk).closest('tr').find('.aState').text('Approve');
                $($Chk).closest('tr').find('.aState').attr('data-val', 'Approve');
                //if (sfty == '3' && Orders[trsf].Stat == 3) {
                //    Orders[trsf].isApproved = "Approve";
                //}
                if (Orders[trsf].Stat == 1) {//sfty == '2' && Orders[trsf].Stat == 1
                    Orders[trsf].isApproved = "Approve";
                }
            }
            else {
                $($Chk).prop('checked', false);
                $($Chk).closest('tr').find('.aState').text('Pending');
                $($Chk).closest('tr').find('.aState').attr('data-val', 'Pending');
                //if (sfty == '3' && Orders[trsf].Stat == 3) {
                //    Orders[trsf].isApproved = "Pending";
                //}
                if (Orders[trsf].Stat == 1) { //sfty == '2' && Orders[trsf].Stat == 1
                    Orders[trsf].isApproved = "Pending";
                }
            }
        }
        function fillTpYR() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Tp_Approval.aspx/BindDate",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    var tpyr = $("#fltpyr");
                    tpyr.empty().append('<option value="0">Select Year</option>');
                    for (var i = 0; i < data.d.length; i++) {
                        tpyr.append($('<option value="' + data.d[i].value + '">' + data.d[i].text + '</option>'));
                    };
                }
            });
            $('#fltpyr').selectpicker({
                liveSearch: true
            });
            $('#fltpmnth').selectpicker({
                liveSearch: true
            });
        }
        function ReloadModal() {
            $('#tpdets tbody').html('');
            $dtot = 0;
            $gtot = 0;
            $gpob = 0;
            $gsob = 0;
            for ($i = 0; $i < CTP.length; $i++) {
                if (CTP.length > 0) {
                    slno = $i + 1;
                    if (CTP[$i].TPOB == '' || CTP[$i].TPOB == null)
                        CTP[$i].TPOB = 0;
                    if (CTP[$i].TSOB == '' || CTP[$i].TSOB == null)
                        CTP[$i].TSOB = 0;
                    $gpob += parseFloat(CTP[$i].TPOB);
                    $gsob += parseFloat(CTP[$i].TSOB);
                    $dtot = (parseFloat(CTP[$i].TPOB) + parseFloat(CTP[$i].TSOB));
                    $gtot += $dtot;
                    tr = $("<tr></tr>");					
                    $(tr).html('<td>' + slno + '</td><td>' + CTP[$i].Tour_date + '</td><td>' + CTP[$i].Worktype + '<td><span class="tooltip b">' + CTP[$i].Worked_With.slice(0,-1) + '</span><div class="tooltiptext">' + CTP[$i].Worked_With.slice(0,-1) + '</div></td><td>' + CTP[$i].Tour_Schedule1 + '</td><td>' + CTP[$i].objective +
                        '</td><td><span class="tooltip b">' + CTP[$i].Retailer_Name + '</span><div class="tooltiptext">' + CTP[$i].Retailer_Name + '</div></td><td>' + CTP[$i].TPOB + '</td><td>' + CTP[$i].TSOB + '</td><td>' + $dtot + '</td><td class="tdaction"><a class="tpedit"><i class="fa fa-edit"></i></a></td>');
                    $("#tpdets TBODY").append(tr);
                }
            }
            $('#tpob').text($gpob);
            $('#tsob').text($gsob);
            $('#gtot').text($gtot);
        }
        function ReloadTable() {
            $("#OrderList TBODY").html("");
            if (filtrkey != "All") {
                Orders = Orders.filter(function (a) {
                    return a.isApproved == filtrkey;
                })
            }
            var isappr = "";
            st = PgRecords * (pgNo - 1); slno = 0;
            for ($i = st; $i < st + PgRecords; $i++) {
                if ($i < Orders.length) {
                    slno = $i + 1;
                    tr = $("<tr stat=\"" + Orders[$i].Status + "\" id=\"" + Orders[$i].Sf_Code + "\"></tr>");
                    if (sfty == '3' && (Orders[$i].Status == "Pending with Admin")) {
                        $isapp = (Orders[$i].isApproved != "Pending") ? 'checked' : "";
                        $(tr).html('<td>' + slno + '</td><td class="Approvetd" onclick="Atdclick(this)"><input type="checkbox" class="Approve" onclick="Chkclick(this)" ' + $isapp + ' /></td><td><ul class="nav" style="margin:0px"><li class="dropdown"><a href="#" style="padding:0px" class="dropdown-toggle" data-toggle="dropdown">'
                            + '<span><span class="aState" data-val="' + Orders[$i].isApproved + '">' + Orders[$i].isApproved + '</span><i class="caret" style="float:right;margin-top:8px;margin-right:0px"></i></span></a>' +
                            '<ul class="dropdown-menu dropdown-custom dropdown-menu-right ddlStatus" style="right:0;left:auto;">' + optStatus + '</ul></li></ul></td><td>' + Orders[$i].Status + '</td><td>' + Orders[$i].sf_emp_id + '</td><td>' + Orders[$i].SFNA + '</td><td>' + Orders[$i].DSN +
                            '</td><td>' + Orders[$i].sf_hq + '</td><td>' + Orders[$i].Reporting_To + '</td><td id=' + Orders[$i].Sf_Code + ' class="sfedit"><a href="#"><span class="glyphicon glyphicon-eye-open"></span></a></td>');

                    }
                    else if (sfty == '2' && (Orders[$i].Status == "Pending with Manager")) {
                        $isapp = (Orders[$i].isApproved != "Pending") ? 'checked' : "";
                        $(tr).html('<td>' + slno + '</td><td class="Approvetd" onclick="Atdclick(this)"><input type="checkbox" class="Approve" onclick="Chkclick(this)" ' + $isapp + ' /></td><td><ul class="nav" style="margin:0px"><li class="dropdown"><a href="#" style="padding:0px" class="dropdown-toggle" data-toggle="dropdown">'
                            + '<span><span class="aState" data-val="' + Orders[$i].isApproved + '">' + Orders[$i].isApproved + '</span><i class="caret" style="float:right;margin-top:8px;margin-right:0px"></i></span></a>' +
                            '<ul class="dropdown-menu dropdown-custom dropdown-menu-right ddlStatus" style="right:0;left:auto;">' + optStatus + '</ul></li></ul></td><td>' + Orders[$i].Status + '</td><td>' + Orders[$i].sf_emp_id + '</td><td>' + Orders[$i].SFNA + '</td><td>' + Orders[$i].DSN +
                            '</td><td>' + Orders[$i].sf_hq + '</td><td>' + Orders[$i].Reporting_To + '</td><td id=' + Orders[$i].Sf_Code + ' class="sfedit"><a href="#"><span class="glyphicon glyphicon-eye-open"></span></a></td>');
                    }
                    else {
                        $(tr).html('<td>' + slno + '</td><td></td><td data-val="' + Orders[$i].isApproved + '">' + Orders[$i].isApproved + '</td><td>' + Orders[$i].Status + '</td><td>' + Orders[$i].sf_emp_id + '</td><td>' + Orders[$i].SFNA + '</td><td>' + Orders[$i].DSN +
                            '</td><td>' + Orders[$i].sf_hq + '</td><td>' + Orders[$i].Reporting_To + '</td><td id=' + Orders[$i].Sf_Code + ' class="sfedit"><a href="#"><span class="glyphicon glyphicon-eye-open"></span></a></td>');
                    }
                    $("#OrderList TBODY").append(tr);
                }
            }
            $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")             
            $(".ddlStatus>li>a").on("click", function () {
                cStus = $(this).closest("td").find(".aState");
                stus = $(this).attr("v");
                $indx = parseInt($(this).closest("tr").find('td').eq(0).text()) - 1;
                if (!$(this).closest("tr").find('.Approve').is(':checked')) {
                    $(this).closest("tr").find('.Approve').prop('checked', true);
                }
                cStusNm = $(this).text();
                //Orders[$indx].Stat = parseInt(stus);
                if (sfty == '3' && Orders[$indx].Stat == 3) {
                    Orders[$indx].isApproved = cStusNm;
                }
                if (sfty == '2' && Orders[$indx].Stat == 1) {
                    Orders[$indx].isApproved = cStusNm;
                }
                $(cStus).html(cStusNm);
                ReloadTable();
            });
            loadPgNos();
        }

        $("#tSearchOrd").on("keyup", function () {
            if ($(this).val() != "") {
                shText = $(this).val().toLowerCase();
                Orders = AllOrders.filter(function (a) {
                    chk = false;
                    $.each(a, function (key, val) {
                        if (val != null && val.toString().toLowerCase().indexOf(shText) > -1 && (',' + searchKeys).indexOf(',' + key + ',') > -1) {
                            chk = true;
                        }
                    })
                    return chk;
                })
            }
            else
                Orders = AllOrders
            ReloadTable();
        })

        $(".segment>li").on("click", function () {
            $(".segment>li").removeClass('active');
            $(this).addClass('active');
            filtrkey = $(this).attr('data-va');
            Orders = AllOrders;
            $("#tSearchOrd").val('');
            ReloadTable();
        });
        function loadSFTP(sf, Mn, Yr) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Tp_Approval.aspx/getSFTP",
                data: "{'divcode':'<%=Session["div_code"]%>','SF':'" + sf + "','Mn':'" + Mn + "','Yr':'" + Yr + "'}",
                dataType: "json",
                success: function (data) {
                    AllTP = JSON.parse(data.d) || [];
                    CTP = AllTP; ReloadModal();
                },
                error: function (result) {
                }
            });
        }
        function clearfields() {
            $('#eDate').val('');
            $('#epob').val('');
            $('#esob').val('');
            $('#eremarks').val('');
            $('#eretailer').multiselect('reload');
            $('#edist').multiselect('reload');
        }
        function loadData(sf, Mn, Yr) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Tp_Approval.aspx/GetList",
                data: "{'divcode':'<%=Session["div_code"]%>','SF':'" + sf + "','Mn':'" + Mn + "','Yr':'" + Yr + "'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders; ReloadTable();
                },
                error: function (result) {
                }
            });
        }
        function getworkedwith($sffc) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Tp_Approval.aspx/getSFWorkedWith",
                data: "{'sfcode':'" + $sffc + "'}",
                dataType: "json",
                success: function (data) {
                    $('#selmgr').empty().append('<option value="">Select</option>');
                    $('#selmgr').append('<option value="admin">admin-Admin</option>');
                    var filtmgr = JSON.parse(data.d);
                    for ($i = 0; $i < filtmgr.length; $i++) {
                        $('#selmgr').append('<option value="' + filtmgr[$i].id + '">' + filtmgr[$i].name + '</option>');
                    }
                    $('#selmgr').selectpicker({
                        liveSearch: true
                    });
                    $('#selmgr').selectpicker('refresh');
                }
            });
        }         
        $(document).ready(function () {
            sf = '<%=Session["sf_code"]%>';
            sfty ='<%=Session["sf_type"]%>';
            loadData(sf, pgMNTH, pgYR);
            fillTpYR();
            $('#apprtp').hide();
            $('#rejtp').hide();
            $('#fltpmnth').selectpicker('val', pgMNTH);
            $('#fltpyr').selectpicker('val', pgYR);             
            $(document).on('click', '.panel-heading>i', function () {
                var addrsf = $(this).closest('.panel-heading').attr('tsf');
                var addrsfname = $(this).closest('.panel-heading').find('.apsf').text();
                $('#selmgr').append('<option value="' + addrsf + '">' + addrsfname + '</option>');
                $('#selmgr').selectpicker('refresh');
                $(this).parent().parent().remove();
                var panelList = $('#draggablePanelList');
                $('.panel', panelList).each(function (index, elem) {
                    var $listItem = $(elem),
                        newIndex = $listItem.index();
                    $(elem).find('div>.aplvl').html("Level - " + ($listItem.index() + 1))
                    // Persist the new indices.
                });
            });             
          
            $('#fltpyr').on('change', function () {
                var cmn = $('#fltpmnth').val();
                var cyr = $('#fltpyr').val();
                loadData(sf, cmn, cyr)
            });
            $('#apprtp').on('click', function () {
                modalApprove($(this).html())
            });
            $('#rejtp').on('click', function () {
                modalApprove($(this).html())
            });
            $('#fltpmnth').on('change', function () {
                var cmn = $('#fltpmnth').val();
                var cyr = $('#fltpyr').val();
                loadData(sf, cmn, cyr)
            });
            
            $(document).on('click', '.tpedit', function () {
                clearfields();
                getTPWtypes(trsfcl); getTPDist(trsfcl); getTPRoutes(trsfcl);
                getTPRetail(trsfcl, $('#eroute').val());
                $eTdate = $(this).closest('tr').find('td').eq(1).text();
                $eTdate = $eTdate.split('/');
                $eTdate = $eTdate[2] + '-' + $eTdate[1] + '-' + $eTdate[0];
                $('#eDate').val($eTdate);
                $('#eDate').prop('disabled', true);
                $('#editmodal').modal('toggle');
                $('#eroute').val(0);
                // $('#eroute').selectpicker('refresh');
            });
            $('#eroute').on('change', function () {
                getTPRetail(trsfcl, $('#eroute').val());
            });
            $(document).on('click', '.sfedit', function () {
                $x = this.id;
                $stat = $(this).closest('tr').attr("stat");
                mdsf = $x;
                trsfcl = $x;
                $('#apprtp').hide();
                $('#rejtp').hide();
                var sfname = $(this).closest('tr').find('td').eq(5).text();
                var mnth = $('#fltpmnth :selected').text();
                var year = $('#fltpyr :selected').text();
                var cmn = $('#fltpmnth').val();
                var cyr = $('#fltpyr').val();
                loadSFTP($x, cmn, cyr);
                $('#exampleModal').modal('toggle');
                $('.tdaction').hide();
                $('#exampleModalLabel').text('Tour Plan for ' + mnth + '-' + year + ' - ' + sfname);
                if (sfty == 3 && $stat == "Pending with Admin") {
                    $('#apprtp').show();
                    $('#rejtp').show();
                    $('.tdaction').show();
                }
                if (sfty == 2 && $stat == "Pending with Manager") {
                    $('#apprtp').show();
                    $('#rejtp').show();
                    $('.tdaction').show();
                }
            });
        });
        function svMasTp() {
            var filttp = [];
            var cmn = $('#fltpmnth').val();
            var cyr = $('#fltpyr').val();
            var sXMl = '';
             //if (sf == 'admin') {
            //    filttp = Orders.filter(function (a) {
            //        if (a.Stat == 1 || a.Stat == 3)
            //        return a.Stat;
            //    });
            //}
            //else {
                filttp = Orders.filter(function (a) {
                    return a.Stat == 1;
                });
            //}
            for (var i = 0; i < filttp.length; i++) {
                sXMl += "<TP sfc=\"" + filttp[i].Sf_Code + "\" ApStatus=\"" + filttp[i].isApproved + "\" />"
            }
            if (filttp.length > 0) {
                console.log(sXMl);
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Tp_Approval.aspx/svTpApprove",
                    data: "{'sxml':'" + sXMl + "','Mn':'" + cmn + "','Yr':'" + cyr + "'}",
                    dataType: "json",
                    success: function (data) {
                        alert(data.d);
                        window.location.reload();
                    },
                    error: function (result) {
                        alert(result.responseText);
                    }
                });
            }
        }
        function updateTP() {
            var upDt = $('#eDate').val();
            var wtypc = $('#eWorktyp').val();
            var wtypname = $('#eWorktyp :selected').text();
            var updist = '';
            var updistn = '';
            $('#edist  > option:selected').each(function () {
                updist += ',' + $(this).val();
                updistn += $(this).text() + ',';
            });
            var uproute = $('#eroute').val();
            var upretail = '';
            var upretailn = '';
            $('#eretailer  > option:selected').each(function () {
                upretail += ',' + $(this).val();
                upretailn += $(this).text() + ',';
            });
            var upremarks = $('#eremarks').val();
            var upPOB = $('#epob').val();
            var upSOB = $('#esob').val();
            if (wtypname == 'Field Work' && updist == '') {
                alert('Select the Distributor');
                return false;
            }
            if (wtypname == 'Field Work' && uproute == 0) {
                alert('Select the Route');
                return false;
            }
            if (wtypname == 'Field Work' && upretail == '') {
                alert('Select the Retailer');
                return false;
            }
            var Tpmn = $('#fltpmnth').val();
            var Tpyr = $('#fltpyr').val();
            if (confirm("Do you want change the TP Entry ?")) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Tp_Approval.aspx/updateSFTP",
                    data: "{'sf_Code':'" + trsfcl + "','Divcode':'<%=Session["div_code"]%>','TpDate':'" + upDt + "','TMn':'" + Tpmn + "','TYr':'" + Tpyr + "','Distc':'" + updist + "','Distname':'" + updistn + "','Retc':'" + upretail + "','Retn':'" + upretailn + "','Routec':'" + uproute + "','wtypc':'" + wtypc + "','wtypn':'" + wtypname + "','Remarks':'" + upremarks + "','POB':'" + upPOB + "','SOB':'" + upSOB + "','Styp':'" + sfty + "'}",
                    dataType: "json",
                    success: function (data) {
                        alert(data.d);
                        $('#editmodal').modal('hide');
                        loadSFTP(trsfcl, Tpmn, Tpyr);
                    },
                    error: function (result) {
                        alert(result.responseText);
                    }
                });
            }

        }
        function modalApprove($St) {
            var sXMl = "<TP sfc=\"" + trsfcl + "\" ApStatus=\"" + $St + "\" />";
            var Tpmn = $('#fltpmnth').val();
            var Tpyr = $('#fltpyr').val();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Tp_Approval.aspx/svTpApprove",
                data: "{'sxml':'" + sXMl + "','Mn':'" + Tpmn + "','Yr':'" + Tpyr + "'}",
                dataType: "json",
                success: function (data) {
                    alert("TP Status Changed");
                    $('#exampleModal').modal('hide');
                    loadData('<%=Session["sf_code"]%>', Tpmn, Tpyr);
                },
                error: function (result) {
                    alert(result.responseText);
                }
            });
        }
    </script>
</asp:Content>
