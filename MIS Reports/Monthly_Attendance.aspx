<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Monthly_Attendance.aspx.cs" Inherits="MIS_Reports_Monthly_Attendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">   
    <style>
        th{
            white-space:nowrap;
        }
    </style> 
    <form runat="server" id="frm1">
    <div class="row">
        <div class="row">
            <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                Style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 15px; top: 43px;" OnClick="lnkDownload_Click" />
        </div>
        <div class="col-lg-12 sub-header">Monthly Attendance Report <span style="float: right; margin-right: 15px;"><div id="reportrange" class="form-control mt-5 mb-5" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc;">
                <i class="fa fa-calendar"></i>&nbsp;
        <span id="ordDate"></span><i class="fa fa-caret-down"></i>
            </div>
            </span>
    </div></div>
    

    <div class="card">
        <div class="card-body table-responsive" style="overflow-x:auto;">
            <div>Search&nbsp;&nbsp;<input type="text" id="tSearchOrd" style="width:250px;" />
            <span id="filspan" style="margin-left: 10px;">&nbsp;&nbsp;</span>
            <label style="float:right">Show <select class="data-table-basic_length" aria-controls="data-table-basic"><option value="10">10</option><option value="25">25</option><option value="50">50</option><option value="100">100</option></select> entries</label>
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
        var AllOrders = [];
        var sf;
        var sf1;
        var fdt = '';
        var tdt = '';
        var filtrkey = 'All';
        var sortid = '';
        var asc = true;
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "SF_Code,Employee_ID,Employee_Name,HQ";
        $(".data-table-basic_length").on("change",
        function () {
            pgNo = 1;
            PgRecords = $(this).val();
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
        function addAllColumnHeaders() {
            var columnSet = [];
            var myList = '';
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Monthly_Attendance.aspx/GetDetails_Headers",
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
            if (fcol[1] != tcol[1]) {
                monthheaders.append($('<th colspan=' + tcol[1] + '/>').html(tcol[0]));
            }
            $(".av").append(monthheaders);
            var arr = myList[0].Headers.split(',');
            if (arr.length > 0) {
                var headerTr$ = $('<tr/>');
                headerTr$.append($('<th/>').html('Sl.No'));
                headerTr$.append($('<th/>').html('Employee ID'));
                headerTr$.append($('<th/>').html('Employee Name'));
//                headerTr$.append($('<th/>').html('Department'));
//                headerTr$.append($('<th/>').html('Division'));
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
                headerTr$.append($('<th/>').html('Total Days'));
            }
            $(".av").append(headerTr$);

            return columnSet;
        }
        function tablebind() {

        }
        function ReloadTable() {
            $("#OrderList TBODY").html("");
            $("#OrderList THEAD").html("");
            Orders = Orders.filter(function (a) {
                return a.Employee_ID != null;
            });
            //hdr = $("<tr></tr>");
            //for (var j = 0; j < Orders.length; j++) {
            //    $(htr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].sf_emp_id + "</td><td>" + Orders[$i].SF_Name + "</td><td>" + Orders[$i].HQ_Name + "</td><td>" + Orders[$i].WrkDate + "</td><td>" + Orders[$i].Sft_Name + "</td><td>" + Orders[$i].Start_Time + "</td><td>" + Orders[$i].End_Time + "</td><td>" + Orders[$i].TotHrs + "</td><td>" + Orders[$i].TimeDvat + "</td><td><span class='aState' data-val='" + Orders[$i].dvMin + "'>" + Orders[$i].dvMin + "</span></td>"); //<td>" + Orders[$i].Rmks+"</td>
            //}
            //$("#OrderList TBODY").html("");
            st = PgRecords * (pgNo - 1);
            //for ($i = st; $i < st + PgRecords; $i++) {
            //    if ($i < Orders.length) {
            //        tr = $("<tr class='trclick' id='" + Orders[$i].SF_Code + ":" + Orders[$i].WrkDate + "'></tr>");
            //        $(tr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].sf_emp_id + "</td><td>" + Orders[$i].SF_Name + "</td><td>" + Orders[$i].HQ_Name + "</td><td>" + Orders[$i].WrkDate + "</td><td>" + Orders[$i].Sft_Name + "</td><td>" + Orders[$i].Start_Time + "</td><td>" + Orders[$i].End_Time + "</td><td>" + Orders[$i].TotHrs + "</td><td>" + Orders[$i].TimeDvat + "</td><td><span class='aState' data-val='" + Orders[$i].dvMin + "'>" + Orders[$i].dvMin + "</span></td>"); //<td>" + Orders[$i].Rmks+"</td>
            //        $("#OrderList TBODY").append(tr);
            //    }
            //}
            if (Orders.length > 0) {
                //var table = $("<table />");
                //table[0].border = "1";
                var row$;
                var columns = addAllColumnHeaders();
                for (var i = st; i < st + PgRecords; i++) {
                    var pres = 0;
                    var abs = 0;
                    row$ = $('<tr/>');
                    row$.append($('<td/>').html(i + 1));
                    row$.append($('<td/>').html(Orders[i]["Employee_ID"]));
                    row$.append($('<td/>').html(Orders[i]["Employee_Name"]));
//                    row$.append($('<td/>').html(Orders[i]["Department"]));
//                    row$.append($('<td/>').html(Orders[i]["Division"]));
                    row$.append($('<td/>').html(Orders[i]["HQ"]));
                    for (var colIndex = 0; colIndex < columns.length; colIndex++) {
                        if (columns[colIndex] != '' && columns[colIndex] != undefined){
                        var cellValue = Orders[i][columns[colIndex]];
                        if (cellValue == null) { cellValue = ""; }
                        (cellValue == 'P' || cellValue == 'HA') ? pres++ : abs++;
                        row$.append($('<td/>').html(cellValue));
                    }
                    }
                    row$.append($('<td/>').html(pres));
                    row$.append($('<td/>').html(abs));
                    row$.append($('<td/>').html(Number(pres + abs)));
                    $("#OrderList TBODY").append(row$);
                }
            }
            $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")
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
        });
        function loadData() {
            dt = new Date();
            $m = dt.getMonth() + 1, $y = dt.getFullYear();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Monthly_Attendance.aspx/GetDetails",
                data: "{'Div':'<%=Session["Division_Code"]%>','Mn':'" + fdt + "','Yr':'" + tdt + "'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders; ReloadTable();
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
        }
        $(document).ready(function () {
            sf = '<%=Session["Sf_Code"]%>';
            loadData();
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

