<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Form_25.aspx.cs" Inherits="MIS_Reports_Form_25" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">  
    <style>
        th{
            white-space:nowrap;
        }
        #txtfilter_chzn{
            width:300px !important;
            top:10px;
        }
    </style> 
    <form runat="server" id="frm1">
        <asp:HiddenField ID="hfilter" runat="server" />
        <asp:HiddenField ID="hffilter" runat="server" />
        <asp:HiddenField ID="hsfhq" runat="server" />
        <div class="row">
            <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                Style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 15px; top: 43px;" OnClick="lnkDownload_Click" />
        </div>
    <div class="row">
        <div class="col-lg-12 sub-header">Monthly Attendance Report <span style="float: right; margin-right: 15px;"><div id="reportrange" class="form-control mt-5 mb-5" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc;">
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
    </div></div>
    <div class="card">
        <div class="card-body table-responsive" style="overflow-x:auto;">
            <div>Search&nbsp;&nbsp;<input type="text" id="tSearchOrd" style="width:250px;" />
            <span id="filspan" style="margin-left: 10px;">&nbsp;&nbsp;</span>    
                    <div class="export btn-group">
                        <button class="btn btn-default dropdown-toggle" aria-label="Export" data-toggle="dropdown" type="button" title="Export data">
                            <i class="fa fa-th-list"></i>
                            <span class="caret"></span>
                        </button>
                        <ul class="dropdown-menu" role="menu">
                            <li role="menuitem" class="" onclick="radiochange(this)" value="0">
                                <a href="#">HQ</a>
                            </li>
                            <li role="menuitem" class="" onclick="radiochange(this)" value="1">
                                <a href="#">Department</a>
                            </li>
                            <li role="menuitem" class="" onclick="radiochange(this)" value="2">
                                <a href="#">Division</a>
                            </li>
                        </ul>
                    </div>
                    <select class="form-control" id="txtfilter" name="ddfilter" style="min-width: 315px !important; max-width: 318px !important; display: inline"></select></span>
                <label style="float:right">Show <select class="data-table-basic_length" aria-controls="data-table-basic"><option value="10">10</option><option value="25">25</option><option value="50">50</option><option value="100">100</option><option value="All">All</option></select> entries</label>
            </div>
            <div style="overflow-x: auto;width: 100%;margin-bottom: 8px;">
             <table class="table table-hover" id="OrderList" style="font-size:12px">
                <thead class="text-warning av">
                </thead>
                <tbody>
                </tbody>
            </table></div>
            <div class="row" style="padding:5px 0px">
                <div class="col-sm-5">
                    <div class="dataTables_info" id="orders_info" role="status" aria-live="polite">Showing 0 to 0 of 0 entries</div>
                </div>
                <div class="col-sm-7">
                    <div class="dataTables_paginate paging_simple_numbers" id="example2_paginate">
                        <ul class="pagination" style="float:right;margin:-11px 0px">
                            <li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">Previous</a></li>
                            <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="7" tabindex="0">Next</a></li>
                        </ul>
                    </div>
                </div>
            </div>            
        </div>
    </div></form>
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
    <script language="javascript" type="text/javascript">
        var AllOrders = [], AllMonth = [], Allleave = [], filtmon = [], fillev = [];
        var SFDivisions = [];
        var SFDepts = [];
        var SFHQs = [];
        var sf;
        var sf1;
        var fdt = '';
        var tdt = '';
        var filtrkey = 'All';
        var fffilterkey = 'AllFF';
        var sortid = '';
        var asc = true;
        var Orders = [], MonOrders = [], LvOrders = [];
        pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "SF_Code,Employee_ID,Employee_Name,Department,Division,HQ";
        $(".data-table-basic_length").on("change",
        function () {
            pgNo = 1;
            PgRecords = $(this).val();
            if(PgRecords=='All'){
                PgRecords = AllOrders.length;
            }
            ReloadTable();
        }
        );
        $("#reportrange").on("DOMSubtreeModified", function () {
            id = $('#ordDate').text();
            id = id.split('-');
            fdt = id[2] + '-' + id[1] + '-' + id[0] + ' 00:00:00';
            tdt = id[5] + '-' + id[4] + '-' + id[3] + ' 00:00:00';
            loadData();
        });
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
        function radiochange(x) {
            Orders = AllOrders;
            ReloadTable();
            $('#txtfilter_chzn').remove();
            $('#txtfilter').removeClass('chzn-done');
            var sfmgs = $("#txtfilter");
            if (x.value == 1) {
                sfmgs.empty().append('<option value="0">Select Department</option>');
                for (var i = 0; i < SFDepts.length; i++) {
                    sfmgs.append($('<option value="' + SFDepts[i].DeptID + '">' + SFDepts[i].DeptName + '</option>')).trigger('chosen:updated');
                };
            }
            if (x.value == 2) {
                sfmgs.empty().append('<option value="0">Select Division</option>');
                for (var i = 0; i < SFDivisions.length; i++) {
                    sfmgs.append($('<option value="' + SFDivisions[i].subdivision_code + '">' + SFDivisions[i].subdivision_name + '</option>')).trigger('chosen:updated');
                };
            }
            if (x.value == 0) {
                sfmgs.empty().append('<option value="0">Select HQ</option>');
                for (var i = 0; i < SFHQs.length; i++) {
                    sfmgs.append($('<option value="' + SFHQs[i].HQ_ID + '">' + SFHQs[i].HQ_Name + '</option>')).trigger('chosen:updated');
                };
            }
            $('#txtfilter').chosen();
        }
        function loadhq() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Form_25.aspx/GetHQDetails",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    SFHQs = JSON.parse(data.d) || [];
                    var sfhq = $("#txtfilter");
                    sfhq.empty().append('<option selected="selected" value="0">Select HQ</option>');
                    for (var i = 0; i < SFHQs.length; i++) {
                        sfhq.append($('<option value="' + SFHQs[i].HQ_ID + '">' + SFHQs[i].HQ_Name + '</option>'));
                    };
                }
            });
            $('#txtfilter').chosen();
        }
        function loaddepts() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Form_25.aspx/getDepts",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    SFDepts = JSON.parse(data.d) || [];
                }
            });
        }
        function loaddivision() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Form_25.aspx/getDivisions",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    SFDivisions = JSON.parse(data.d) || [];
                }
            });
        }
        function addAllColumnHeaders() {
            var columnSet = [];
            var myList = '';
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Form_25.aspx/GetDetails_Headers",
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
                headerTr$.append($('<th/>').html('Sl.No'));
                headerTr$.append($('<th/>').html('Employee ID'));
                headerTr$.append($('<th/>').html('Employee Name'));
                headerTr$.append($('<th/>').html('Department'));
                headerTr$.append($('<th/>').html('Division'));
                headerTr$.append($('<th/>').html('HQ'));
            }
            for (var i = 0; i < arr.length; i++) {
                if ($.inArray(arr[i], columnSet) == -1) {
                    if (arr[i] !== '' && arr[i] != undefined) {
                        columnSet.push(arr[i]);
                        headerTr$.append($('<th/>').html(arr[i]));
                    }                        
                }
            }            
            if (arr.length > 0) {
                headerTr$.append($('<th/>').html('Total Present Days')); 
                headerTr$.append($('<th/>').html('Absent'));
                headerTr$.append($('<th/>').html('Leave'));
                headerTr$.append($('<th/>').html('LOP'));
                headerTr$.append($('<th/>').html('Total Days'));
                //headerTr$.append($('<th/>').html('WFH'));
                //headerTr$.append($('<th/>').html('CZ'));
                //headerTr$.append($('<th/>').html('CS'));
                //headerTr$.append($('<th/>').html('SQ'));
                //headerTr$.append($('<th/>').html('OL'));
            }
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
                        return a.SF == Orders[i].Sf_Code;
                    });
                    fillev = LvOrders.filter(function (a) {
                        return a.sf_code == Orders[i].Sf_Code;
                    });
                    row$ = $('<tr/>');
                    row$.append($('<td/>').html(i + 1));
                    row$.append($('<td/>').html(Orders[i]["Employee_ID"]));
                    row$.append($('<td/>').html(Orders[i]["Employee_Name"]));
                    row$.append($('<td/>').html(Orders[i]["Department"]));
                    row$.append($('<td/>').html(Orders[i]["Division"]));
                    row$.append($('<td/>').html(Orders[i]["HQ"]));
                    for (var colIndex = 0; colIndex < columns.length; colIndex++) {
                        if (columns[colIndex] != '' && columns[colIndex] != undefined) {
                            if (filtmon.length > 0) {
                                var cellValue1 = filtmon[0];
                                var cellValue = cellValue1[columns[colIndex]];
                                if (cellValue == null) { cellValue = "A"; }
                            }
                            else {
                                cellValue = 'A';
                            }
                            if (cellValue == 'P' ||cellValue == 'OD') 
                            {
                                pres = pres + 1;
                            }
                            else if (cellValue == 'HA') {
                                pres = Number(pres) + 0.5;
                                abs = Number(abs) + 0.5;
                            }
                            else if (cellValue == '/HAL'|| cellValue == 'A/HAL'|| cellValue == 'HA/HAL'|| cellValue == 'P/HAL'|| cellValue == 'OD/HAL') {
                                lev = parseFloat(lev) + 0.5;
                            }
                            else if (cellValue == '/L' || cellValue == 'A/L' || cellValue == 'HA/L' || cellValue == 'P/L'|| cellValue == 'OD/L') {
                                lev = Number(lev) + 1;
                            }
                            else if (cellValue == '/LOP' || cellValue == 'A/LOP' || cellValue == 'HA/LOP' || cellValue == 'P/LOP' || cellValue == 'OD/LOP') {
                                lop = Number(lev) + 1;
                            }
                            else if (cellValue == '/HALOP' || cellValue == 'A/HALOP' || cellValue == 'HA/HALOP' || cellValue == 'P/HALOP' || cellValue == 'OD/HALOP') {
                                lop = Number(lev) + 0.5;
                            }
                            else if (cellValue == 'A') {
                                abs = abs + 1;
							}
                            row$.append($('<td/>').html(cellValue));                            
                        }
                    }
                    row$.append($('<td/>').html(pres));
                    row$.append($('<td/>').html(abs));
                    row$.append($('<td/>').html(lev));
					row$.append($('<td/>').html(lop));
                    row$.append($('<td/>').html(Number(pres + abs + parseFloat(lev)+parseFloat(lop))));                    
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
        function loadData() {
            dt = new Date();
            $m = dt.getMonth() + 1, $y = dt.getFullYear();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Form_25.aspx/GetDetails",
                data: "{'SF':'<%=Session["Sf_Code"]%>','Div':'<%=Session["Division_Code"]%>','Mn':'" + fdt + "','Yr':'" + tdt + "'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders; loadData2(); loadData3(); ReloadTable();
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
        }
        function loadData2() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Form_25.aspx/GetMonthlyAttendance",
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
        function loadData3() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Form_25.aspx/GetMonthlyAttendanceLeave",
                data: "{'SF':'<%=Session["Sf_Code"]%>','Div':'<%=Session["Division_Code"]%>','Mn':'" + fdt + "','Yr':'" + tdt + "'}",
                dataType: "json",
                success: function (data) {
                    Allleave = JSON.parse(data.d) || [];
                    LvOrders = Allleave;
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
        }
        $(document).ready(function () {
            sf = '<%=Session["Sf_Code"]%>';
            loadhq();
            loadData();
            loaddepts();
            loaddivision();
                $("#btnExcel").click(function () {
                    $("#OrderList").table2excel({
                        filename: "Monthly_Attendance_Report.xls"
                    });
                });
            $('#txtfilter').on('change', function () {
                var sfs = $(this).val();
                var hqn = $('#txtfilter_chzn a').text();
                if (sfs != 0) {
                    Orders = AllOrders;
                    Orders = Orders.filter(function (a) {
                        return a.Department == hqn || a.HQ == hqn || a.Division == hqn;
                    });
                }
                else {
                    Orders = AllOrders;
                }
                ReloadTable();
            });
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
