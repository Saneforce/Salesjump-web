<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Daily_Attendance_Performance.aspx.cs" Inherits="MIS_Reports_Daily_Attendance_Performance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="../css/chosen.css" rel='stylesheet' type='text/css' />
    <style>
        #ddlsf_chzn{
            width:300px !important;
            font-weight:500;
        }
        #txtfilter_chzn{
            width:300px !important;
            top:10px;
        }
        th{
            white-space:nowrap; 
            cursor:pointer;
        }
        .alt1{
            background-color:#f5f5f5;
        }
        .alt2{
            background-color:#ffffff;
        }
    </style>
    <form id="Form1" runat="server">
        <asp:HiddenField ID="hfilter" runat="server" />
        <asp:HiddenField ID="hffilter" runat="server" />
        <asp:HiddenField ID="hsfhq" runat="server" />
      <div class="row">
        <div class="row">
            <asp:ImageButton ID="ImageButton1" runat="server" Visible="false" align="right" ImageUrl="~/img/Excel-icon.png"
                Style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 15px; top: 43px;" OnClick="lnkDownload_Click" />
        </div>
        <div class="col-lg-12 sub-header">
            Monthly Attendance Log <span style="float: right; margin-right: 15px;">
                <div id="reportrange" class="form-control mt-5 mb-5" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc;">
                    <i class="fa fa-calendar"></i>&nbsp;
        <span id="ordDate"></span><i class="fa fa-caret-down"></i>
                </div>
            </span>
                <div style="float: right; padding-top: 4px;">
                    <ul class="segment">
                        <li data-va='AllFF' class="active">ALL</li>
                        <li data-va='FF'>FieldForce</li>
                        <li data-va="NF">Non-FieldForce</li>
                    </ul>
                </div>
        </div>
    </div>
    <div class="card" style="overflow:auto">
        <div class="card-body table-responsive">
            <div style="white-space: nowrap">
                Search&nbsp;&nbsp;<input type="text" id="tSearchOrd" style="width: 250px;" />
                <label style="float: right">Show
                    <select class="data-table-basic_length" aria-controls="data-table-basic">
                        <option value="10">10</option>
                        <option value="25">25</option>
                        <option value="50">50</option>
                        <option value="100">100</option>
                    </select>
                    entries</label>
            </div>
            <table class="table table-hover" id="OrderList" style="font-size: 12px">
                <thead class="text-warning av">
                  <%--  <tr>
                        <th style="text-align: left">Sl.No</th>
                        <th id="Employee_ID" style="text-align: left">Employee ID</th>
                        <th id="Employee_Name" style="text-align: left">Employee Name</th>
                        <th id="Department" style="text-align: left">Department</th>
                        <th id="Division" style="text-align: left">Division</th>
                        <th id="HQ_Name" style="text-align: left">Location</th>
                        <th id="Work_Date" style="text-align: left">Date</th>
                        <th id="Shift_Code" style="text-align: left">Shift Code</th>
                        <th id="In_time" style="text-align: left">IN</th>
                        <th id="Out_time" style="text-align: left">Out</th>
                        <th id="Total_Hours" style="text-align: left">Total Hours</th>
                        <th id="Late_In" style="text-align: left">Late In</th>
                        <th id="Early_out" style="text-align: left">Early Out</th>
                        <th style="text-align: left">OT Hours</th>
                        <th style="text-align: left">Attendance Hours</th>
                    </tr>--%>
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
                            <li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">First</a></li>
                            <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="7" tabindex="0">Last</a></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
    <script type="text/javascript">
        var AllOrders = [], AllMonth = [], Allleave = [], filtmon = [], fillev = [];
        var SFDivisions = [];
        var SFDepts = [];
        var SFHQs = [];
        var sf;
        var fdt = '';
        var tdt = '';
        var filtrkey = 'All';
        var fffilterkey = 'AllFF';
        var sortid = '';
        var asc = true;
        var Orders = [], MonOrders = [], LvOrders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Employee_Name,Department,Division,Employee_ID,HQ_Name,Shift_Code,Work_Date,Sf_Code,";
        $(".data-table-basic_length").on("change",
        function () {
            pgNo = 1;
            PgRecords = $(this).val();
            ReloadTable();
        }
        );
        function addAllColumnHeaders() {
            var columnSet = [];
            var myList = '';
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Daily_Attendance_Performance.aspx/GetDetails_Headers",
                data: "{'Mn':'" + fdt + "','Yr':'" + tdt + "'}",
                dataType: "json",
                success: function (data) {
                    myList = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
            var monthheaders = $('<tr/>');
            var fcol = myList[0].Fmc.split(',');
            var tcol = myList[0].Tmc.split(',');
            monthheaders.append($('<th style="text-align: center;" colspan="6"/>').html(''));
            monthheaders.append($('<th style="text-align: center;" colspan=' + fcol[1] + '/>').html(fcol[0]));
            if (fcol[1] != tcol[1] && fcol[0] != tcol[0]) {
                monthheaders.append($('<th colspan=' + tcol[1] + '/>').html(tcol[0]));
            }
            $(".av").append(monthheaders);
            var arr = myList[0].Headers.split(',');
            if (arr.length > 0) {
                var headerTr$ = $('<tr/>');
                var headerTr1$ = $('<tr/>');
                headerTr1$.append($('<th style="text-align: center" colspan="6" />').html(''));
                headerTr$.append($('<th/>').html('Sl.No'));
                headerTr$.append($('<th/>').html('Employee ID'));
                headerTr$.append($('<th/>').html('Employee Name'));
                headerTr$.append($('<th/>').html('Department'));
                headerTr$.append($('<th/>').html('Division'));
                headerTr$.append($('<th/>').html('Location'));
                for (var i = 0; i < arr.length; i++) {
                    var adclass = ((i % 2) == 0) ? "alt1" : "alt2";
                    if ($.inArray(arr[i], columnSet) == -1) {
                        if (arr[i] !== '' && arr[i] != undefined) {
                            columnSet.push(arr[i]);
                            headerTr1$.append($('<th class="' + adclass + '"   style="text-align: center" colspan="3" />').html(arr[i]));
                            headerTr$.append($('<th class="' + adclass + '"  />').html('In'));
                            headerTr$.append($('<th class="' + adclass + '"  />').html('Out'));
                            headerTr$.append($('<th class="' + adclass + '"  />').html('Total Hours'));
                        }
                    }
                }
                //$(".rw3").append(headerTr$);
            }
            $(".av").append(headerTr1$);
            $(".av").append(headerTr$);

            return columnSet;
        }
        function ReloadTable() {
            $("#OrderList TBODY").html("");
            $("#OrderList THEAD").html("");
            Orders = Orders.filter(function (a) {
                return a.Employee_ID != null;
            });
            if (fffilterkey != "AllFF") {
                Orders = Orders.filter(function (a) {
                    return a.Approved_By == fffilterkey;
                })
            }
            st = PgRecords * (pgNo - 1);
            if (Orders.length > 0) {
                //var table = $("<table />");
                //table[0].border = "1";
                var row$;
                var columns = addAllColumnHeaders();
                for (var i = st; i < st + PgRecords; i++) {
                    var pres = 0;
                    var abs = 0;
                    var lev = 0;
                    var lop = 0;
                    if(Orders[i]!=undefined){
                    filtmon = MonOrders.filter(function (a) {
                        return a.SF_Code == Orders[i].Sf_Code;
                    });
                    row$ = $('<tr/>');
                    row$.append($('<td/>').html(i + 1));
                    row$.append($('<td/>').html(Orders[i]["Employee_ID"]));
                    row$.append($('<td/>').html(Orders[i]["Employee_Name"]));
                    row$.append($('<td/>').html(Orders[i]["Department"]));
                    row$.append($('<td/>').html(Orders[i]["Division"]));
                    row$.append($('<td/>').html(Orders[i]["HQ"]));
                    for (var $jk = 0; $jk < columns.length; $jk++) {
                        var adclass = (($jk % 2) == 0) ? "alt1" : "alt2";
                        var dayfilt=filtmon.filter(function(a){
                            return a.Ady==columns[$jk];
                        });
                        if (dayfilt[0]!=undefined) {
                            row$.append($('<td class="' + adclass + '" />').html(dayfilt[0]["In_time"]));
                            row$.append($('<td class="' + adclass + '" />').html(dayfilt[0]["Out_time"]));
                            row$.append($('<td class="' + adclass + '" />').html(dayfilt[0]["Total_Hours"]));
                        }
                        else {
                            row$.append($('<td class="' + adclass + '" />').html("-"));
                            row$.append($('<td class="' + adclass + '" />').html("-"));
                            row$.append($('<td class="' + adclass + '" />').html("0"));

                        }
                    }
                    //row$.append($('<td/>').html(pres));
                    //row$.append($('<td/>').html(abs));
                    //row$.append($('<td/>').html(lev));
                    //row$.append($('<td/>').html(lop));
                    //row$.append($('<td/>').html(Number(pres + abs + parseFloat(lev) + parseFloat(lop))));
                    //if (fillev.length > 0) {
                    //    var lvcell = fillev[0];
                    //    row$.append($('<td/>').html(lvcell.WFH));
                    //    row$.append($('<td/>').html(lvcell.CS));
                    //    row$.append($('<td/>').html(lvcell.CZ));
                    //    row$.append($('<td/>').html(lvcell.SQ));
                    //    row$.append($('<td/>').html(lvcell.OL));
                    //}
                    //else {
                    //    row$.append($('<td/>').html("0"));
                    //    row$.append($('<td/>').html("0"));
                    //    row$.append($('<td/>').html("0"));
                    //    row$.append($('<td/>').html("0"));
                    //    row$.append($('<td/>').html("0"));
                    //}
                    $("#OrderList TBODY").append(row$);
                    }
                }
            }
            $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")
            loadPgNos();
        }
        $("#reportrange").on("DOMSubtreeModified", function () {
            id = $('#ordDate').text();
            id = id.split('-');
            fdt = id[2].trim() + '-' + id[1] + '-' + id[0];
            tdt = id[5] + '-' + id[4] + '-' + id[3].trim();
            $('#ddlsf').val(0).trigger('chosen:updated').css("width", "100%");
            loadData();
        });
        function fillsf() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Daily_Attendance_Performance.aspx/GetSF",
                data: "{'Div':'<%=Session["Division_Code"]%>'}",
                dataType: "json",
                success: function (data) {
                    var sfdets = JSON.parse(data.d) || [];
                    var ddsf = $('#ddlsf');
                    ddsf.empty().append('<option value="0">Select Employee</option>');
                    ddsf.append('<option value="admin">All</option>');
                    for (var i = 0; i < sfdets.length; i++) {
                        ddsf.append($('<option value="' + sfdets[i].Sf_Code + '">' + sfdets[i].Sf_Name + '</option>')).trigger('chosen:updated').css("width", "100%");
                    }
                },
                error: function (result) {
                }
            });
            $('#ddlsf').chosen();
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
        $(".segment>li").on("click", function () {
            $(".segment>li").removeClass('active');
            $(this).addClass('active');
            fffilterkey = $(this).attr('data-va');
            $("#<%=hffilter.ClientID%>").val(fffilterkey);
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
        });
        function loadData2() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Daily_Attendance_Performance.aspx/GetAttendancePerformance",
                data: "{'SF':'<%=Session["Sf_Code"]%>','Div':'<%=Session["Division_Code"]%>','Mn':'" + fdt + "','Yr':'" + tdt + "'}",
                dataType: "json",
                success: function (data) {
                    AllMonth = JSON.parse(data.d) || [];
                    MonOrders = AllMonth;
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
        }
        function loadData() {
            dt = new Date();
            $m = dt.getMonth() + 1, $y = dt.getFullYear();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Daily_Attendance_Performance.aspx/GetDetails",
                data: "{'SF':'" + sf + "','Div':'<%=Session["Division_Code"]%>','Mn':'" + fdt + "','Yr':'" + tdt + "'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders; loadData2(); ReloadTable();
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
        }
        $(document).ready(function () {
            sf = '<%=Session["Sf_Code"]%>';
            loadData(sf);
        });
</script>
    <script type="text/javascript">
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
    </script>
</asp:Content>

