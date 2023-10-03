<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="DailyLeaveDetails.aspx.cs" Inherits="MIS_Reports_DailyLeaveDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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

        [data-val='Pending']{
            color:#FFC107;
        }
        [data-val='Approved']{
            color:green;
        }
        [data-val='Rejected'] {
            color: Red;
        }

        th
        {
            white-space:nowrap;  
            cursor:pointer; 
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
        <div class="row">
            <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                Style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 15px; top: 43px;" OnClick="lnkDownload_Click" />
        </div>
        <div class="col-lg-12 sub-header">Leave Status Report 
            <div style="float: right; margin-right: 15px;">
                <div id="reportrange" class="form-control mt-5 mb-5" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc;">
                    <i class="fa fa-calendar"></i>&nbsp;<span id="ordDate"></span><i class="fa fa-caret-down"></i>
                </div>
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-body table-responsive" style="overflow-x:auto;">
            <div style="white-space:nowrap">Search&nbsp;&nbsp;<input type="text" id="tSearchOrd" style="width:250px;" />
                <div id="filspan" style="margin-left: 10px;display:none">Filter By&nbsp;&nbsp;            
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
                                <a href="#">Manager</a>
                            </li>
                        </ul>
                    </div>
                    <select class="form-control" id="txtfilter" name="ddfilter" style="min-width: 315px !important;max-width:318px !important;display:inline"></select>

                </div>
                <label style="float:right">Show <select class="data-table-basic_length" aria-controls="data-table-basic"><option value="10">10</option><option value="25">25</option><option value="50">50</option><option value="100">100</option></select> entries</label>
             </div>
             <table class="table table-hover" id="OrderList" style="font-size:12px">
                <thead class="text-warning">
                    <tr style="background-color: #37a4c6;color:#fff;">                          
                        <th style="text-align:left;color:#fff">Sl.No</th>
                        <th id="Emp_ID" style="text-align:left;color:#fff;">Staff ID</th>
                        <th id="Name" style="text-align:left;color:#fff">Staff Name</th>
                       <%-- <th id="DeptName" style="text-align:left;color:#fff">Department</th>--%>
                        <th id="HQ_Name" style="text-align:left;color:#fff">HQ</th>
                        <th id="Reporting" style="text-align:left;color:#fff">Reporting To</th>
                        <th id="lDate" style="text-align:left;color:#fff">Apply Date</th>
                        <th id="FDate" style="text-align:left;color:#fff">From Date</th>
                        <th id="TDate" style="text-align:left;color:#fff">To Date</th>
                        <th id="lvTyp" style="text-align:left;color:#fff">Type</th>
<%--                        <th id="Eligible" style="text-align:left">Eligible</th>
                        <th id="PTkn" style="text-align:left">Taken</th>--%>
                        <th id="No_of_Days" style="text-align:left;color:#fff">Apply Days</th>
<%--                        <th id="Avail" style="text-align:left">Avail</th>--%>
                        <th id="lvReason" style="text-align:left;color:#fff">Reason For Leave</th>
                        <th id="Status" style="text-align:left;color:#fff">Status</th>
                        <th id="Approved_By" style="text-align:left;color:#fff">Approved By</th>
                        <th id="Approved_Date" style="text-align:left;color:#fff">Approved Date</th>
                        <th id="Rejected_Reason" style="text-align:left;color:#fff">Reject Reason</th>
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
    </form>
    <script type="text/javascript" src="../js/kendo.all.min.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
    <script language="javascript" type="text/javascript">
        var AllOrders = [];
        var sftyp ='<%=Session["sf_type"]%>';
        var SFHQs = [];
        var SFMGRs = [];
        var Arrsum = [];
        var AllOrders2 = [];
        var sf;
        var sf1;
        var fdt = '';
        var tdt = '';
        var filtrkey = 'All';
        var fffilterkey = 'AllFF';
        var sortid = '';
        var asc = true;//
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Emp_ID,Name,DeptName,HQ_Name,Reporting,lDate,Start_Time,End_Time,St_Loc,End_Loc,OD_Type,OD_Loc,vstPurp,Status,Approved_By,Approved_Date,Rejected_Reason,App_Version,";
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
                sfmgs.empty().append('<option value="0">Select Manager</option>');
                for (var i = 0; i < SFMGRs.length; i++) {
                    sfmgs.append($('<option value="' + SFMGRs[i].sfcode + '">' + SFMGRs[i].sfname + '</option>')).trigger('chosen:updated');
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
                url: "DailyAttendanceDetail.aspx/GetHQDetails",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    SFHQs = JSON.parse(data.d) || [];
                    var sfhq = $("[id*=txtfilter]");
                    sfhq.empty().append('<option selected="selected" value="0">Select HQ</option>');
                    for (var i = 0; i < SFHQs.length; i++) {
                        sfhq.append($('<option value="' + SFHQs[i].HQ_ID + '">' + SFHQs[i].HQ_Name + '</option>'));
                    };
                }
            });
            $('#txtfilter').chosen();
        }
        function loadmgrs(sf) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "DailyAttendanceDetail.aspx/getMGR",
                data: "{'divcode':'<%=Session["div_code"]%>','sfcode':'" + sf + "'}",
                dataType: "json",
                success: function (data) {
                    SFMGRs = data.d;
                }
            });
        }
        $('#eedit').on('click', function () {
            $('#shftname').hide();
            $('#sftname').show();
            $('#btnupdate').show();
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
            $("#<%=hfilter.ClientID%>").val(filtrkey);
            Orders = AllOrders;
            $("#tSearchOrd").val('');
            ReloadTable();
        });


        function fillshift(hqx, dept) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "DailyAttendanceDetail.aspx/getShift",
                data: "{'divcode':'<%=Session["Division_Code"]%>','HQ':'" + hqx + "','deptcode':'" + dept + "'}",
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
            for ($i = st; $i < st + Number(PgRecords); $i++) {
                if ($i < Orders.length) {
                    tr = $("<tr class='trclick' id='" + Orders[$i].SF_Code + ":" + Orders[$i].WrkDate + "'></tr>");
                    $(tr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].Emp_ID + "</td><td>" + Orders[$i].Name + "</td><td>" + Orders[$i].HQ_Name + "</td><td>" + Orders[$i].Reporting + "</td><td>" + Orders[$i].Created_Date + "</td><td style='white-space:nowrap'>" + Orders[$i].From_Date + "</td><td style='white-space:nowrap'>" + Orders[$i].To_Date + "</td><td>" + Orders[$i].Leave_Name + "</td><td>" + Orders[$i].No_of_Days + "</td><td>" + Orders[$i].lvReason + "</td><td><span class='aState' data-val='" + Orders[$i].Status + "'>" + Orders[$i].Status + "</span></td><td>" + Orders[$i].Approved_By + "</td><td>" + Orders[$i].Approved_Date + "</td><td>" + Orders[$i].Rejected_Reason + "</td>"); //<td>" + Orders[$i].Rmks+"</td>
                    $("#OrderList TBODY").append(tr);//<td>" + Orders[$i].Eligible + "</td><td>" + Orders[$i].PTkn + "</td><td>" + Orders[$i].No_of_Days + "</td><td>" + Orders[$i].Avail + "</td>
                    //tr = $("<tr><td style='background:#dcdcdc;'></td><td style='white-space:nowrap;background:#dcdcdc;color:green;' colspan='14'>" + Orders[$i].lvReason + "</td></tr>")
                    //$("#OrderList TBODY").append(tr);
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
        })
        function loadData() {
            dt=new Date();
            $m=dt.getMonth()+1,$y=dt.getFullYear();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "DailyLeaveDetails.aspx/GetDetails",
                data: "{'SF':'<%=Session["Sf_Code"]%>','Div':'<%=Session["Division_Code"]%>','Mn':'"+fdt+"','Yr':'"+tdt+"'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders; 
                    ReloadTable();
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
        /*$("#calendar").kendoCalendar();
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
        });*/
        $(document).ready(function () {
            sf = '<%=Session["Sf_Code"]%>';
            loadData();
            //loadData2();
            loadhq();
            loadmgrs(sf);
            dv = $('<div style="z-index: 10000000;position:fixed;left:50%;top:50%;width:99%;height:99%;transform: translate(-50%, -50%);border-radius: 15px;background:#ececec;display:none" id="cphoto1"></div>');
            $(dv).html('<span style="position: absolute;z-index: 10000000;padding: 5px;cursor: default;background: #a5a0a0;border-radius: 50%;width: 20px;height: 20px;line-height: 6px;text-align: center;border: solid 1px gray;top: 6px;right: 6px;" onclick="closew()">x</span><img style="width:98%;height:98%;border-radius: 15px;object-fit: contain;transform:translate(1%, 1%)" id="photo1" />')
            $("body").append(dv);
            $('#txtfilter').on('change', function () {
                var sfs = $(this).val();
                var hqn = $('#txtfilter :selected').text();
                $("#<%=hsfhq.ClientID%>").val(hqn);
                if (sfs != 0) {
                    Orders = AllOrders;
                    Orders = Orders.filter(function (a) {
                        return a.SF_Code == sfs || a.HQ_Name == hqn;
                    });
                }
                else {
                    Orders = AllOrders;
                }
                ReloadTable();
            });
            $("#reportrange").on("DOMSubtreeModified", function () {
                id = $('#ordDate').text();
                id = id.split('-');
                fdt = id[2].trim() + '-' + id[1] + '-' + id[0];
                tdt = id[5] + '-' + id[4] + '-' + id[3].trim();
                loadData();
            });
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
            fillshift(fhq[0].Sf_HQ, fhq[0].deptID);
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
//            $('#sftname option').each(function () {
//                if ($(this).text().toLowerCase() == filt[0].Sft_Name.toLowerCase()) {
//                    this.selected = true;
//                    return;
//                }
//            });
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
                    }
                    $('#logdets tbody').append(str);
                    $('.onTotAmt').html(whours + 'Hrs.');
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

