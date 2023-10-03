<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Attendance_Performance.aspx.cs" Inherits="MIS_Reports_Attendance_Performance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    </style>
    <form id="Form1" runat="server">
      <div class="row">
        <div class="row">
            <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                Style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 15px; top: 43px;" OnClick="lnkDownload_Click" />
        </div>
        <div class="col-lg-12 sub-header">
            Attendance Performance <span style="float: right; margin-right: 15px;">
                <div id="reportrange" class="form-control mt-5 mb-5" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc;">
                    <i class="fa fa-calendar"></i>&nbsp;
        <span id="ordDate"></span><i class="fa fa-caret-down"></i>
                </div>
            </span>
            <div style="float: right;padding-right: 3px">
                <select id="ddlsf"></select>
            </div>
        </div>
    </div>
    <div class="card">
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
                <thead class="text-warning">
                    <tr>
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
    <script language="javascript" type="text/javascript">
        var AllOrders = [];
        var SFDivisions = [];
        var SFDepts = [];
        var SFHQs = [];
        var sf;
        var fdt = '';
        var tdt = '';
        var filtrkey = 'All';
        var sortid = '';
        var asc = true;
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Employee_Name,Department,Division,Employee_ID,HQ_Name,Shift_Code,Work_Date,Sf_Code,";
        $(".data-table-basic_length").on("change",
        function () {
            pgNo = 1;
            PgRecords = $(this).val();
            ReloadTable();
        }
        );
        $('th').on('click', function () {
            sortid = this.id;
            asc = $(this).attr('asc');
            if (asc == undefined) asc = 'true';
            Orders.sort(function (a, b) {
                if (a[sortid].toLowerCase() < b[sortid].toLowerCase() && asc == 'true') return -1;
                if (a[sortid].toLowerCase() > b[sortid].toLowerCase() && asc == 'true') return 1;

                if (b[sortid].toLowerCase() < a[sortid].toLowerCase() && asc == 'false') return -1;
                if (b[sortid].toLowerCase() > a[sortid].toLowerCase() && asc == 'false') return 1;
                return 0;
            });

            $(this).attr('asc', ((asc == 'true') ? 'false' : 'true'));
            ReloadTable();
        });
        $("#reportrange").on("DOMSubtreeModified", function () {
            id = $('#ordDate').text();
            id = id.split('-');
            fdt = id[2].trim() + '-' + id[1] + '-' + id[0];
            tdt = id[5] + '-' + id[4] + '-' + id[3].trim();
            $('#ddlsf').val(0).trigger('chosen:updated').css("width", "100%");
            loadData(sf);
        });
        function fillsf() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Attendance_Performance.aspx/GetSF",
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
        function ReloadTable() {
            $("#OrderList TBODY").html("");
            st = PgRecords * (pgNo - 1);
            for ($i = st; $i < st + PgRecords; $i++) {
                if ($i < Orders.length) {
                    var pres = '';
                    if (Number(Orders[$i].Total_Hours) > 6 || Orders[$i].WrkType == 1) {
                        pres = 'P';
                    }
                    else if (Number(Orders[$i].Total_Hours) < 6 && Number(Orders[$i].Total_Hours) > 4) {
                        pres = 'HA';
                    }
                    else {
                        pres = 'A';
                    }
                    tr = $("<tr></tr>");
                    $(tr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].Employee_ID + "</td><td>" + Orders[$i].Employee_Name + "</td><td>" + Orders[$i].Department + "</td><td>" + Orders[$i].Division + "</td><td>" + Orders[$i].HQ_Name + "</td><td>" + Orders[$i].Work_Date + "</td><td>" + Orders[$i].Shift_Code + "</td><td>" + Orders[$i].In_time + "</td><td>" + Orders[$i].Out_time + "</td><td>" + Orders[$i].Total_Hours + "</td><td>" + Orders[$i].Late_In + "</td><td>" + Orders[$i].Early_out + "</td><td></td><td>" + pres + "</td>"); //<td>" + Orders[$i].Rmks+"</td>
                    $("#OrderList TBODY").append(tr);
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
        function loadData(sf) {
            dt = new Date();
            $m = dt.getMonth() + 1, $y = dt.getFullYear();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Attendance_Performance.aspx/GetDetails",
                data: "{'SF':'" + sf + "','Div':'<%=Session["Division_Code"]%>','Mn':'" + fdt + "','Yr':'" + tdt + "'}",
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
            fillsf();
            loadData(sf);
            $('#ddlsf').on('change', function () {
                sf = $(this).val();
                Orders = AllOrders;
                if (sf == 'admin' || sf == '0') {
                    Orders = AllOrders;
                }
                else {
                    Orders = Orders.filter(function (a) {
                        return a.SF_Code == sf;
                    });
                }
                ReloadTable();
            })
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