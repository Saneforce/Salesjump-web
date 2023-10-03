<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="DailyAttendanceDetail.aspx.cs" Inherits="MIS_Reports_DailyAttendanceDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datetimepicker/4.17.47/css/bootstrap-datetimepicker.min.css" />--%>
    <link rel="Stylesheet" href="../css/kendo.common.min.css" />
    <link rel="Stylesheet" href="../css/kendo.default.min.css" />
    <style>
        
        .segment1 {
            display: inline-block;
            padding-left: 0;
            margin: -2px 22px;
            border-radius: 4px;
            font-size: 13px;
            font-family: "Poppins";
            /* border: 1px solid  #66a454   #d3252a  #f59303;*/
        }

            .segment1 > li {
                display: inline-block;
                background: #fafafa;
                color: #666;
                margin-left: -4px;
                padding: 5px 10px;
                min-width: 50px;
                border: 1px solid #ddd;
            }

                .segment1 > li:first-child {
                    border-radius: 4px 0px 0px 4px;
                }

                .segment1 > li:last-child {
                    border-radius: 0px 4px 4px 0px;
                }

                .segment1 > li.active {
                    color: #fff;
                    cursor: default;
                    background-color: #428bca;
                    border-color: #428bca;
                }
        [data-val='Late']{
            color:red;
        }
        [data-val='On-Time']{
            color:green;
        }
        th
        {
            white-space:nowrap;   
            cursor:pointer;
        }
    </style>
    <form runat="server" id="frm1">
        <asp:HiddenField ID="hfilter" runat="server" />
        <asp:HiddenField ID="hffilter" runat="server" />
        <asp:HiddenField ID="hsfhq" runat="server" />
    <div class="row">
        <div class="row">
            <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                Style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 15px; top: 43px;" OnClick="lnkDownload_Click" />
        </div>
        <div class="col-lg-12 sub-header">Attendance Report <span style="float: right; margin-right: 15px;"><div id="reportrange" class="form-control mt-5 mb-5" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc;">
                <i class="fa fa-calendar"></i>&nbsp;
        <span id="ordDate"></span><i class="fa fa-caret-down"></i>
            </div>
            </span>
            <div style="float: right; padding-top: 4px;">
                <ul class="segment">
                    <li data-va='All' class="active">ALL</li>
                    <li data-va='On-Time'>On-Time</li>
                    <li data-va="Late">Late</li>
                </ul>
            </div>
                <div style="float: right; padding-top: 4px;">
                    <ul class="segment1">
                        <li data-va='AllFF' class="active">ALL</li>
                        <li data-va='FF'>FieldForce</li>
                        <li data-va="NF">Non-FieldForce</li>
                    </ul>
                </div>
    </div></div>

    <div class="card">
        <div class="card-body table-responsive">
            <div style="white-space:nowrap">Search&nbsp;&nbsp;<input type="text" id="tSearchOrd" style="width:250px;" />
            <span id="filspan" style="margin-left: 10px;">Filter By&nbsp;&nbsp;            
                    <div class="export btn-group" style="display:none;">
                        <button class="btn btn-default dropdown-toggle" aria-label="Export" data-toggle="dropdown" type="button" title="Export data">
                            <i class="fa fa-th-list"></i>
                            <span class="caret"></span>
                        </button>
                        <ul class="dropdown-menu" role="menu">
                            <li role="menuitem" class="" onclick="radiochange(this)" value="0">
                                <a href="#">HQ</a>
                            </li>
                            <li role="menuitem" class="" onclick="radiochange(this)" value="1">
                                <a href="#">Manager</a>
                            </li>
                        </ul>
                    </div>
            <select class="form-control" id="txtfilter" name="ddfilter" style="min-width: 315px !important;max-width:318px !important;display:inline"></select></span>
            <label style="float:right">Show <select class="data-table-basic_length" aria-controls="data-table-basic"><option value="10">10</option><option value="25">25</option><option value="50">50</option><option value="100">100</option></select> entries</label>
            </div>
             <table class="table table-hover" id="OrderList" style="font-size:12px">
                <thead class="text-warning">
                    <tr>                          
                        <th style="text-align:left">Sl.No</th>
                        <th id="sf_emp_id" style="text-align:left">Staff ID</th>
                        <th id="SF_Name" style="text-align:left">Staff Name</th>
                        <th id="Division" style="text-align:left">Division</th>
                        <th id="HQ_Name" style="text-align:left">HQ</th>
                        <th id="RPT" style="text-align:left">Reporting To</th>
                        <th id="WrkDate" style="text-align:left">Date</th>
                        <th id="Sft_Name" style="text-align:left">Shift Name</th>
                        <th id="Start_Time" style="text-align:left">Logged In</th>
                        <th id="End_Time" style="text-align:left">Logged Out</th>
                        <th id="TotHrs" style="text-align:left">Working Hours</th>
                        <th id="TimeDvat" style="text-align:left">Time Deviate</th>
                        <th id="dvMin" style="text-align:left">Status</th>
                        <th id="AppV" style="text-align:left">App Version</th>
                        <th class="Addrss" style="text-align:left;display:none;">Address</th>
                        <%--<th style="text-align:left">Remarks</th>--%>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
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
    </div>
    <div class="modal fade" id="exampleModal" style="z-index: 10000000;background-color: transparent; overflow-y: auto;" tabindex="0" aria-hidden="true">
        <div class="modal-dialog" role="document" style="width: 800px !important">
            <div class="modal-content">
                <div class="modal-header">
                    <div class="row" style="display: flex;">
                        <table style="width: 25%;white-space:nowrap;">
                            <tbody>
                                <tr>
                                    <td>Employee Name</td>
                                    <td id="empname"></td>
                                </tr>
                                <tr>
                                    <td>Employee ID</td>
                                    <td id="empid"></td>
                                </tr>
                                <tr>
                                    <td>HeadQuarters</td>
                                    <td id="emphq"></td>
                                </tr>
                            </tbody>
                        </table>                        
                        <img src="https://www.w3schools.com/howto/img_avatar.png" style="width: 106px; height: 98px; position: absolute; right: 13px;">
                    </div>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-sm-6">
                            <label for="tdt">Date:</label>
                            <span id="tdt"></span>
                        </div>
                        <div class="col-sm-6">
                            <label>Shift</label>
                            <span id="shftname"></span>
                            <select id="sftname"></select><span id="eedit"><a href="#" style="padding-left:15px">Edit</a></span><button style="margin-left:8px" class="btn btn-primary" id="btnupdate" type="button">Update</button>
                        </div>
                    </div>
                    <div class="row">
                            <div class="col-sm-4">
                                <div id="calendar" style="width: 102%"></div>
                            </div>
                        <div class="col-sm-8" style="padding: 15px">
                            <table id="logdets" style="width: 100%;">
                                <thead class="text-warning">
                                    <tr>
                                        <th></th>
                                        <th style="text-align: center;">IN</th>
                                        <th></th>
                                        <th style="text-align: center;">OUT</th>
                                        <th style="text-align: center;">Hours Worked</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td style="width: 39px;">
                                            <img src="https://www.w3schools.com/howto/img_avatar.png" style="width: 29px;">
                                        </td>
                                        <td style="text-align: center;">								
                                            <img src="https://cdn0.iconfinder.com/data/icons/small-n-flat/24/678111-map-marker-512.png" style="width: 20px; margin-left: 9px;">
                                        </td>
                                        <td style="width: 39px;">
                                            <img src="https://www.w3schools.com/howto/img_avatar.png" style="width: 29px;">
                                        </td>
                                        <td style="text-align: center;">
											<img src="https://cdn0.iconfinder.com/data/icons/small-n-flat/24/678111-map-marker-512.png" style="width: 20px; margin-left: 9px;">
                                        </td>
                                        <td style="text-align: center;"></td>
                                    </tr>
                                </tbody>
                                <tfoot style="">
                                    <tr>
                                        <th style="text-align: left" colspan="4">Status
										    <span id="stat"></span>
                                        </th>
                                        <th class="onTotAmt" style="text-align: center;"></th>
                                    </tr>
                                </tfoot>
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
    </form>
        <%--<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.24.0/moment.min.js"></script>--%>
<%--    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datetimepicker/4.17.47/js/bootstrap-datetimepicker.min.js"></script>--%>
    <script type="text/javascript" src="../js/kendo.all.min.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
    <script language="javascript" type="text/javascript">
        var AllOrders = [];
        var sf;
        var sf1;
        var fdt = '';
        var tdt = '';
        var filtrkey = 'All';
        var fffilterkey = 'AllFF';
        var sortid = '';
        var asc = true;
        var zk = 0;
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "SF_Code,SF_Name,WrkDate,RSF,subdivision_name,App_Version,Start_Time,End_Time,TotHrs,TimeDvat,HQ_Name,Sft_Name,sf_emp_id,";
        $(".data-table-basic_length").on("change",
        function () {
            pgNo = 1;
            PgRecords = $(this).val();
            ReloadTable();
        }
        );
        //$('#calendar').datetimepicker({
        //    format: 'L',
        //    inline: true
        //})
        $('th').on('click', function () {
            sortid = this.id;
            if (sortid == 'SF_Name' && asc == true) {
                Orders.sort(function (a, b) {
                    if (a.SF_Name.toLowerCase() < b.SF_Name.toLowerCase()) return -1;
                    if (a.SF_Name.toLowerCase() > b.SF_Name.toLowerCase()) return 1;
                    return 0;
                });
                asc = false;
            }
            else if (sortid == 'SF_Name') {
                Orders.sort(function (a, b) {
                    if (b.SF_Name.toLowerCase() < a.SF_Name.toLowerCase()) return -1;
                    if (b.SF_Name.toLowerCase() > a.SF_Name.toLowerCase()) return 1;
                    return 0;
                });
                asc = true;
            }
            if (sortid == 'sf_emp_id' && asc == true) {
                Orders.sort(function (a, b) {
                    return a.sf_emp_id - b.sf_emp_id;
                });
                asc = false;
            }
            else if (sortid == 'sf_emp_id') {
                Orders.sort(function (a, b) {
                    return b.sf_emp_id - a.sf_emp_id;
                });
                asc = true;
            }
            if (sortid == 'HQ_Name' && asc == true) {
                Orders.sort(function (a, b) {
                if (a.HQ_Name.toLowerCase() < b.HQ_Name.toLowerCase()) return -1;
                if (a.HQ_Name.toLowerCase() > b.HQ_Name.toLowerCase()) return 1;
                return 0;
                });
                asc = false;
            }
            else if (sortid == 'HQ_Name') {
                    Orders.sort(function (a, b) {
                        if (b.HQ_Name.toLowerCase() < a.HQ_Name.toLowerCase()) return -1;
                        if (b.HQ_Name.toLowerCase() > a.HQ_Name.toLowerCase()) return 1;
                        return 0;
                });
                asc = true;
            }
            if (sortid == 'Sft_Name' && asc == true) {
                Orders.sort(function (a, b) {
                if (a.Sft_Name.toLowerCase() < b.Sft_Name.toLowerCase()) return -1;
                if (a.Sft_Name.toLowerCase() > b.Sft_Name.toLowerCase()) return 1;
                return 0;
                });
                asc = false;
            }
            else if (sortid == 'Sft_Name') {
                Orders.sort(function (a, b) {
                    if (b.Sft_Name.toLowerCase() < a.Sft_Name.toLowerCase()) return -1;
                    if (b.Sft_Name.toLowerCase() > a.Sft_Name.toLowerCase()) return 1;
                    return 0;
                });
                asc = true;
            }
            if (sortid == 'Start_Time' && asc == true) {
                Orders.sort(function (a, b) {
                if (a.Start_Time.toLowerCase() < b.Start_Time.toLowerCase()) return -1;
                if (a.Start_Time.toLowerCase() > b.Start_Time.toLowerCase()) return 1;
                return 0;
                });
                asc = false;
            }
            else if (sortid == 'Start_Time') {
                    Orders.sort(function (a, b) {
                        if (b.Start_Time.toLowerCase() < a.Start_Time.toLowerCase()) return -1;
                        if (b.Start_Time.toLowerCase() > a.Start_Time.toLowerCase()) return 1;
                        return 0;
                    });
                asc = true;
            }
            if (sortid == 'TotHrs' && asc == true) {
                Orders.sort(function (a, b) {
                    return a.TotHrs - b.TotHrs;
                });
                asc = false;
            }
            else if (sortid == 'TotHrs') {
                Orders.sort(function (a, b) {
                    return b.TotHrs - a.TotHrs;
                });
                asc = true;
            }
            if (sortid == 'TimeDvat' && asc == true) {
                Orders.sort(function (a, b) {
                if (a.TimeDvat.toLowerCase() < b.TimeDvat.toLowerCase()) return -1;
                if (a.TimeDvat.toLowerCase() > b.TimeDvat.toLowerCase()) return 1;
                return 0;
                });
                asc = false;
            }
            else if (sortid == 'TimeDvat') {
                Orders.sort(function (a, b) {
                    if (b.TimeDvat.toLowerCase() < a.TimeDvat.toLowerCase()) return -1;
                    if (b.TimeDvat.toLowerCase() > a.TimeDvat.toLowerCase()) return 1;
                    return 0;
                });
                asc = true;
            }
            if (sortid == 'End_Time' && asc == true) {
                Orders.sort(function (a, b) {
                    if (a.End_Time.toLowerCase() < b.End_Time.toLowerCase()) return -1;
                    if (a.End_Time.toLowerCase() > b.End_Time.toLowerCase()) return 1;
                    return 0;
                });
                asc = false;
            }
            else {
                if (sortid == 'End_Time') {
                    Orders.sort(function (a, b) {
                        if (b.End_Time.toLowerCase() < a.End_Time.toLowerCase()) return -1;
                        if (b.End_Time.toLowerCase() > a.End_Time.toLowerCase()) return 1;
                        return 0;
                    });
                    asc = true;
                }
            }
            console.log(Orders);
            ReloadTable();
        });
        $("#reportrange").on("DOMSubtreeModified", function () {
            id = $('#ordDate').text();
            id = id.split('-');
            fdt = id[2].trim() + '-' + id[1] + '-' + id[0];
            tdt = id[5] + '-' + id[4] + '-' + id[3].trim();
            loadData();
        });
        function loadaddrs($tr) {
            var Long = parseFloat($($tr).attr('lng'));
            var Lat = parseFloat($($tr).attr('lat'));
            var addrs = '';
            var url = "//nominatim.openstreetmap.org/reverse?format=json&lon=" + Long + '&lat=' + Lat + "";
            $.ajax({
                url: url,
                async: false,
                dataType: 'json',
                success: function (data) {
                    addrs = data.display_name;
                }
            });
            $($tr).find('td').eq(14).text(addrs);
            zk++;
			if(zk<$('#OrderList tbody tr').length)
				setTimeout(function () { loadaddrs($('#OrderList tbody tr')[zk]) }, 10);
        }
         function loadPgNos() {
            prepg = parseInt($(".paginate_button.previous>a").attr("data-dt-idx"));
            Nxtpg = parseInt($(".paginate_button.next>a").attr("data-dt-idx"));
            $(".pagination").html("");
            TotalPg = parseInt(parseInt(Orders.length / PgRecords) + ((Orders.length % PgRecords) ? 1 : 0)); selpg = 1;
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
            }
           );
         }
         //$('#calendar').on('changeDate', function (event) {
         //    alert(event.format());
         //});
         //$('.datepicker-days tr .active')
        function radiochange() {
            $('#txtfilter').html("");
           // if (x.value == 1) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "DailyAttendanceDetail.aspx/getMGR",
                    data: "{'divcode':'<%=Session["div_code"]%>','sfcode':'" + sf + "'}",
                    dataType: "json",
                    success: function (data) {
                        var txf = $("[id*=txtfilter]");
                        txf.empty().append('<option selected="selected" value="0">Select Manager</option>');
                        for (var i = 0; i < data.d.length; i++) {
                            txf.append($('<option value="' + data.d[i].sfcode + '">' + data.d[i].sfname + '</option>'));
                        };
                    }
                });
            //}
//            if (x.value == 0) {
//                $.ajax({
//                    type: "POST",
//                    contentType: "application/json; charset=utf-8",
//                    async: false,
//                    url: "DailyAttendanceDetail.aspx/GetHQDetails",
//                    data: "{'divcode':'<%=Session["div_code"]%>'}",
//                    dataType: "json",
//                    success: function (data) {
//                        var HQ = JSON.parse(data.d) || [];
//                        var sf = $("[id*=txtfilter]");
//                        sf.empty().append('<option selected="selected" value="0">Select HQ</option>');
//                        for (var i = 0; i < HQ.length; i++) {
//                            sf.append($('<option value="' + HQ[i].HQ_ID + '">' + HQ[i].HQ_Name + '</option>'));
//                        };
//                    }
//                });
//            }
        }
        $('#eedit').on('click', function () {
            $('#shftname').hide();
            $('#sftname').show();
            $('#btnupdate').show();
        });
        $('#btnupdate').on('click', function () {
            var selsft = $('#sftname').val();
            var ssfttext = $('#sftname :selected').text();
            var presft = $('#ShiftSelected_Id').val();
            var logdt = $('#tdt').html();
            logdt = logdt.split('/');
            ldt = logdt[2] + '-' + logdt[1] + '-' + logdt[0];
            var hsf = $('#hidsf').val();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "DailyAttendanceDetail.aspx/shiftUpdate",
                data: "{'SF':'" + hsf + "','logdt':'" + ldt + "','nsftid':'" + selsft + "','psftid':'" + presft + "'}",
                dataType: "json",
                success: function (data) {
                    if (data.d = 'Success') {
                        alert('Shift Changed Successfully');
                        $('#shftname').show();
                        $('#sftname').hide();
                        $('#btnupdate').hide();
                        $('#shftname').html(':' + ssfttext);
                        $('#sftname').val(selsft);
                    }
                    else {
                        alert('Shift Change Unsuccessful');
                    }
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
        })
        $('#txtfilter').on('change', function () {
            var sfs = $(this).val();
            var hqn = $('#txtfilter :selected').text();
            if (sfs != 0) {
                Orders = AllOrders;
                Orders = Orders.filter(function (a) {
                    return a.RSF == sfs;
                    //|| a.HQ_Name == hqn;
                });
            }
            else {
                Orders = AllOrders;
            }
            ReloadTable();
        });

        $(".segment1>li").on("click", function () {
            $(".segment1>li").removeClass('active');
            $(this).addClass('active');
            fffilterkey = $(this).attr('data-va');
            $("#<%=hffilter.ClientID%>").val(fffilterkey);
            Orders = AllOrders;
            $("#tSearchOrd").val('');
            ReloadTable();
        });
        
        $(".segment>li").on("click", function () {
            $(".segment>li").removeClass('active');
            $(this).addClass('active');
            filtrkey = $(this).attr('data-va');
            Orders = AllOrders;
            $("#tSearchOrd").val('');
            ReloadTable();
        });
        function fillshift(hqx) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "DailyAttendanceDetail.aspx/getShift",
                data: "{'divcode':'<%=Session["Division_Code"]%>','HQ':'" + hqx + "'}",
                dataType: "json",
                success: function (data) {
                    var shft = JSON.parse(data.d) || [];
                    var sft = $("[id*=sftname]");
                    sft.empty();
                    for (var i = 0; i < shft.length; i++) {
                        sft.append($('<option value="' + shft[i].Sft_ID + '">' + shft[i].Sft_Name + '</option>'));
                    }
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
        }
        function ReloadTable() {
            $("#OrderList TBODY").html("");
            if (fffilterkey != "AllFF") {
                Orders = Orders.filter(function (a) {
                    return a.Approved_By == fffilterkey;
                })
            }
            if (filtrkey != "All") {
                Orders = Orders.filter(function (a) {
                    return a.dvMin == filtrkey;
                })
            }
            st = PgRecords * (pgNo - 1);
            for ($i = st; $i < st + PgRecords; $i++) {
                if ($i < Orders.length) {
                    tr = $("<tr class='trclick' lat='" + Orders[$i].Start_Lat + "' lng='" + Orders[$i].Start_Long + "'  id='" + Orders[$i].SF_Code + ":" + Orders[$i].WrkDate + "'></tr>");
                    $(tr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].sf_emp_id + "</td><td>" + Orders[$i].SF_Name + "</td><td>" + Orders[$i].subdivision_name + "</td><td>" + Orders[$i].HQ_Name + "</td><td>" + Orders[$i].RSF + "</td><td>" + Orders[$i].WrkDate + "</td><td>" + Orders[$i].Sft_Name + "</td><td>" + Orders[$i].Start_Time + "</td><td>" + Orders[$i].End_Time + "</td><td>" + Orders[$i].TotHrs + "</td><td>" + Orders[$i].TimeDvat + "</td><td><span class='aState' data-val='" + Orders[$i].dvMin + "'>" + Orders[$i].dvMin + "</span></td><td>" + Orders[$i].App_Version + "</td><td class='Addrss' style='display:none'></td>"); //<td>" + Orders[$i].Rmks+"</td>
                    $("#OrderList TBODY").append(tr);
                }
            }
            $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")            
			loadPgNos();
            if(('<%=Session["Division_Code"]%>').indexOf(1)>-1)
            {
                $('.Addrss').show();zk=0;
                setTimeout(function () { loadaddrs($('#OrderList tbody tr')[0]) }, 10);
            }
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
            dt=new Date();
            $m=dt.getMonth()+1,$y=dt.getFullYear();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "DailyAttendanceDetail.aspx/GetDetails",
                data: "{'SF':'<%=Session["Sf_Code"]%>','Div':'<%=Session["Division_Code"]%>','Mn':'"+fdt+"','Yr':'"+tdt+"'}",
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
        function closew() {
            $('#cphoto1').css("display", 'none');
        }
        $(document).on('click','.picc',function () {
            var photo = $(this).attr("src");
            $('#photo1').attr("src", $(this).attr("src"));
            $('#cphoto1').css("display", 'block');
        });
        function opdoc(lat,lng) {
            //var lat = $(this).attr("attr-lat");
            //var lng = $(this).attr("attr-lat");
            var popUpObj;
            popUpObj = window.open("SF_Modal_Map.aspx?lat=" + lat + "&lng=" + lng,
        "ModalPopUp",
        "toolbar=no," +
        "scrollbars=yes," +
        "location=no," +
        "statusbar=no," +
        "menubar=no," +
        "addressbar=no," +
        "resizable=yes," +
        "width=900," +
        "height=600," +
        "left = 0," +
        "top=0"
        );
                popUpObj.focus();
        }
        $("#calendar").kendoCalendar();
        $('.k-nav-prev span').removeClass();
        $('.k-nav-next span').removeClass(); 
        $('.k-nav-prev span').addClass('fa fa-arrow-left fa-1');
        $('.k-nav-next span').addClass('fa fa-arrow-right fa-1');
        var calendar = $("#calendar").data("kendoCalendar");
        curr = calendar.current();
        calendar.bind("change", function () {
            dte = new Date(this.value());
            selDt = dte.getFullYear() + '-0' + (dte.getMonth() + 1) + '-' + dte.getDate();
            fillchmodal(selDt);
        });
        $(document).ready(function () {
            sf = '<%=Session["Sf_Code"]%>';
            loadData();
            radiochange();
            dv = $('<div style="z-index: 10000000;position:fixed;left:50%;top:50%;width:50%;height:50%;transform: translate(-50%, -50%);border-radius: 15px;display:none" id="cphoto1"></div>');
            $(dv).html('<span style="position: absolute;padding: 5px;cursor: default;background: #dcd6d652;border-radius: 50%;width: 20px;height: 20px;line-height: 6px;text-align: center;border: solid 1px gray;top: 6px;right: 6px;" onclick="closew()">x</span><img style="width:100%;height:100%;border-radius: 15px;" id="photo1" />')
            $("body").append(dv);
        });
        $(document).on('click', '.trclick', function () {
            var cl = this.id;
            cl = cl.split(':');
            var x = cl[0];
            sf1 = x;
            var y = cl[1];
            var fhq = Orders.filter(function (a) {
                return a.SF_Code == x;
            });
            fillshift(fhq[0].Sf_HQ);
            $('#exampleModal').modal('toggle');
            fillModal(x,y);
        });
        function fillModal(x,y) {
            var filt = [];
            $("#sftname").hide();
            $('#shftname').show();
            $('#btnupdate').hide();
            filt = Orders.filter(function (a) {
                return a.SF_Code == x && a.WrkDate == y;
            });
            $("#empname").html(':' + filt[0].SF_Name);
            $("#empid").html(':' + filt[0].sf_emp_id);
            $("#emphq").html(':' + filt[0].HQ_Name);
            $("#tdt").html(filt[0].WrkDate);
            $("#shftname").html(':' + filt[0].Sft_Name);
            $("#sftname").val(filt[0].Sft_ID);
            //$('#sftname option').each(function () {
            //    if ($(this).text().toLowerCase() == filt[0].Sft_Name.toLowerCase()) {
            //        this.selected = true;
            //        return;
            //    }
            //});
            $('#stat').html(':' + filt[0].dvMin);
            
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "DailyAttendanceDetail.aspx/GetSFModal",
                data: "{'sfcode':'" + x + "','logdate':'" + y + "'}",
                dataType: "json",
                success: function (data) {
                    var sfdets = JSON.parse(data.d) || [];
                    var str = '';
                    var whours = 0;
                    $('#logdets tbody').empty();
                    for (var i = 0; i < sfdets.length; i++) {
                        whours += sfdets[i].Whours;
                        str += '<tr><td style="width: 39px;"><input type="hidden" id="hidsf" value="' + x + '" /><input type="hidden" id="ShiftSelected_Id" value="' + sfdets[0].ShiftSelected_Id + '" /><img class="picc" src="' + sfdets[i].simg + '" style="width: 29px;"></td>' +
                            '<td style="text-align: center;">' + sfdets[i].STime + '<img onclick="opdoc(' + sfdets[i].Start_Lat + ',' + sfdets[i].Start_Long + ')" attr-lat="' + sfdets[i].Start_Lat + '" attr-lng="' + sfdets[i].Start_Long + '" class="latlong" src="https://cdn0.iconfinder.com/data/icons/small-n-flat/24/678111-map-marker-512.png" style="width: 20px; margin-left: 9px;">' +
                            '</td><td style="width: 39px;"><img class="picc" src="' + sfdets[i].endimg + '" style="width: 29px;"></td><td style="text-align: center;">' + sfdets[i].ETime +
                            '<img class="latlong" onclick="opdoc(' + sfdets[i].End_Lat + ',' + sfdets[i].End_Long + ')" attr-lat="' + sfdets[i].End_Lat + '" attr-lng="' + sfdets[i].End_Long + '" src="https://cdn0.iconfinder.com/data/icons/small-n-flat/24/678111-map-marker-512.png" style="width: 20px; margin-left: 9px;"></td><td style="text-align: center;">' + sfdets[i].Whours + 'Hrs.</td></tr>';
                    }
                    $('#logdets tbody').append(str);
                    $('.onTotAmt').html(whours+'Hrs.');
                }
            });
        }
        function fillchmodal(sdt) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "DailyAttendanceDetail.aspx/GetCHSFModal",
                data: "{'sfcode':'" + sf1 + "','logdate':'" + sdt + "'}",
                dataType: "json",
                success: function (data) {
                    var sfdets = JSON.parse(data.d) || [];
                    var str = '';
                    var whours = 0;
                    if (sfdets.length > 0) {
                    $("#tdt").html(sfdets[0].login_date);
                    $("#shftname").html(':' + sfdets[0].Sft_Name);
                    $("#sftname").val(sfdets[0].ShiftSelected_Id);
                    $('#logdets tbody').empty();
                    for (var i = 0; i < sfdets.length; i++) {
                        whours += sfdets[i].Whours;
                        str += '<tr><td style="width: 39px;"><input type="hidden" id="hidsf" value="' + sf1 + '" /><input type="hidden" id="ShiftSelected_Id" value="' + sfdets[0].ShiftSelected_Id + '" /><img class="picc" src="' + sfdets[i].simg + '" style="width: 29px;"></td>' +
                            '<td style="text-align: center;">' + sfdets[i].STime + '<img onclick="opdoc(' + sfdets[i].Start_Lat + ',' + sfdets[i].Start_Long + ')" attr-lat="' + sfdets[i].Start_Lat + '" attr-lng="' + sfdets[i].Start_Long + '" class="latlong" src="https://cdn0.iconfinder.com/data/icons/small-n-flat/24/678111-map-marker-512.png" style="width: 20px; margin-left: 9px;">' +
                            '</td><td style="width: 39px;"><img class="picc" src="' + sfdets[i].endimg + '" style="width: 29px;"></td><td style="text-align: center;">' + sfdets[i].ETime +
                            '<img class="latlong" onclick="opdoc(' + sfdets[i].End_Lat + ',' + sfdets[i].End_Long + ')" attr-lat="' + sfdets[i].End_Lat + '" attr-lng="' + sfdets[i].End_Long + '" src="https://cdn0.iconfinder.com/data/icons/small-n-flat/24/678111-map-marker-512.png" style="width: 20px; margin-left: 9px;"></td><td style="text-align: center;">' + sfdets[i].Whours + 'Hrs.</td></tr>';
                    }
                    $('#logdets tbody').append(str);
                    $('.onTotAmt').html(whours + 'Hrs.');
                }
                }
            });
        }

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

