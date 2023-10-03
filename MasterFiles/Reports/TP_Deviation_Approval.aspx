<%@ Page Language="C#" AutoEventWireup="true" Title="" MasterPageFile="~/Master.master" CodeFile="TP_Deviation_Approval.aspx.cs" Inherits="MasterFiles_Reports_TP_Deviation_Approval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type="text/css" />

    <style type="text/css">
        .tooltip {
            position: relative;
            display: inline-block;
            color: #006080;
            opacity: 1;
        }

        span.b {
            white-space: nowrap;
            width: 110px;
            overflow: hidden;
            text-overflow: ellipsis;
            display: block;
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
            right: 0px;
        }

        td:hover .tooltiptext {
            visibility: visible;
        }
    </style>
    <form id="frm1" runat="server">
        <div class="row">
            <img alt="" onclick="exceldwnld()" src="../../../img/Excel-icon.png" style="cursor: pointer; float: right;" id="btnExport" width="40" height="40" />
        </div>
        <div class="row">
            <div class="col-lg-12 sub-header">
                TP Deviation Dates
                <select id="flfield" data-size="6" data-dropup-auto="false" onchange="selectch()" style="float: right">
                </select>
                <label id="lbldt" class="control-label">
                    Date
                </label>
                <div class="col-xs-12 col-sm-8" id="ddate" style="padding-top: 5px; float: right">
                    <div style="float: left; margin-right: 15px;">
                        <div id="reportrange" class="form-control mt-5 mb-5" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc;">
                            <i class="fa fa-calendar"></i>&nbsp;
                            <span id="ordDate"></span>
                            <i class="fa fa-caret-down"></i>
                        </div>
                    </div>
                </div>

                <select id="fltpmnth" data-size="5" data-dropup-auto="false" onchange="selectch()" class="hidden" style="float: right">
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
                <select id="fltpyr" data-size="5" data-dropup-auto="false" onchange="selectch()" class="hidden" style="float: right">
                </select>
            </div>
        </div>
        <div class="row">
            <button class="btn btn-primary" onclick="saveDeviationApproval()" type="button" id="ApprBtn" style="float: right; margin-right: 2rem;">Approve</button>

        </div>
        <div class="card">
            <div class="card-body table-responsive">
                <div style="white-space: nowrap">
                    <input type="text" id="tSearchOrd" style="width: 250px;" placeholder="Search" />
                    <label style="float: right">
                        Show
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

                        </ul>
                    </div>
                </div>
                <table class="table table-hover" id="OrderList" style="font-size: 12px; margin-left: 26px;">
                    <thead class="text-warning">
                        <tr style="white-space: nowrap;">
                            <th style="text-align: left">SlNo</th>
                            <th style="text-align: left">
                                <input type="checkbox" id="selectAll" onclick="Achkall()">
                                <span>Select</span></th>
                            <th style="text-align: left">Deviation Date</th>
                            <th style="text-align: left">Employee ID</th>
                            <th style="text-align: left">Employee Name</th>
                            <th style="text-align: left">HQ</th>
                            <th style="text-align: left">Reporting To</th>
                            <th style="text-align: left">Reason for deviation</th>
                            <th style="text-align: left">Approve Status</th>
                            <th style="text-align: left">Reject</th>
                            <th style="text-align: left">Reject Reason</th>

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
        <div class="modal fade in" id="myModal" role="dialog" style="z-index: 10000000; overflow-y: auto; background-color: rgb(255 255 255 / 15%);">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Reason For Rejection</h4>
                    </div>
                    <div class="modal-body">
                        <textarea class="form-control" id="message-text" max="20" min="1" step="1"></textarea>

                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" id="Closebtn" data-dismiss="modal">Close</button>
                        <button type="button" class="btn btn-default" onclick="RejectDeviationApproval()">Reject</button>
                    </div>
                </div>
            </div>
        </div>
    </form>



    <script src="../../js/xlsx.full.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="dist/simple-notify.min.css" />
    <script src="dist/simple-notify.min.js"></script>
    <script type="text/javascript">
        var AllOrders = [], AllTP = [], CTP = [];
        let excelArr = [];
        var sf = '', sfty = '';
        var filtrkey = 'All';
        var mdsf = '';
        var dt = new Date();
        var pgYR = dt.getFullYear();
        var pgMNTH = dt.getMonth() + 1;
        var trsfcl = '';
        optStatus = "<li><a href='#' v='2'>Approve</a><a href='#' v='1'>Pending</a><a href='#' v='-1'>ALL</a></li>"
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Emplyee_ID,Employee_Name,HQ,Reporting_To,DeviationDate,ApproveStatus,";


        var startDate;
        var endDate;

        $(function () {
            $('#reportrange').daterangepicker({
                startDate: moment(),
                endDate: moment(),
                //minDate: '01/01/2012',
                //maxDate: '12/31/2014',
                //dateLimit: { days: 60 },
                showDropdowns: true,
                showWeekNumbers: true,
                //timePicker: false,
                //timePickerIncrement: 1,
                //timePicker12Hour: true,
                ranges: {
                    'Today': [moment(), moment()],
                    'Yesterday': [moment().subtract('days', 1), moment().subtract(1, 'days')],
                    'Last 7 Days': [moment().subtract('days', 6), moment()],
                    'Last 30 Days': [moment().subtract('days', 29), moment()],
                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
                },
                opens: 'left',
                buttonClasses: ['btn btn-primary'],
                applyClass: 'btn-small btn-primary',
                cancelClass: 'btn-small btn-danger',
                format: 'DD/MM/YYYY',
                separator: ' to ',
                locale: {
                    applyLabel: 'Apply',
                    fromLabel: 'From',
                    toLabel: 'To',
                    customRangeLabel: 'Custom Range',
                    daysOfWeek: ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'],
                    monthNames: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
                    firstDay: 1
                }
            },
                function (start, end) {
                    console.log("Callback has been called!");
                    $('#reportrange span').html(start.format('DD-MM-YYYY') + ' - ' + end.format('DD-MM-YYYY'));
                    startDate = start;
                    endDate = end;

                }
            );
            //Set the initial state of the picker label
            $('#reportrange span').html(moment().subtract('days', 0).format('DD-MM-YYYY') + ' - ' + moment().format('DD-MM-YYYY'));
        });

        $(".data-table-basic_length").on("change",
            function () {
                pgNo = 1;
                PgRecords = $(this).val();
                ReloadTable();
            }
        );

        function exceldwnld() {
            excelArr.forEach(object => {
                delete object['DDate'];
                delete object['isApproved'];
                delete object['Sf_Code'];
                delete object['Status'];
	          delete object['EDate'];
                delete object['Sl_No'];
                delete object['Apply_No'];
            });
            var droptblname = $("#sheetid option:selected").text();
            var ws = XLSX.utils.json_to_sheet(excelArr);
            var wb = XLSX.utils.book_new();
            XLSX.utils.book_append_sheet(wb, ws, droptblname);
            XLSX.writeFile(wb, "TP_Deviation_Dates.xlsx");
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

        function fillFieldForce() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "TP_Deviation_Approval.aspx/FillMRManagers",
                data: "{'div_code':'<%=Session["div_code"]%>','sf_code':'<%=Session["sf_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    var fillfdf = JSON.parse(data.d) || [];
                    var tpyr = $("#flfield");
                    tpyr.empty().append('<option value="0">Select FieldForce</option>');
                    for (var i = 0; i < fillfdf.length; i++) {
                        tpyr.append($('<option value="' + fillfdf[i].Sf_Code + '">' + fillfdf[i].Sf_Name + '</option>'));
                    };
                }
            });
            $('#flfield').selectpicker({
                liveSearch: true
            });
        }
        function fillTpYR() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "TP_Deviation_Approval.aspx/BindDate",
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
        function Chkclick($Chk) {
            let rowID = ($($Chk).closest("tr").attr("id"));

            let sfCode = rowID.split(":")[0];
            let dDate = rowID.split(":")[1];

            let rowIndex = Orders.findIndex(x => x.Sf_Code == sfCode && x.DDate == dDate)


            if ($($Chk).is(":checked")) {

                if (Orders[rowIndex].Status == 3) {
                    Orders[rowIndex].isApproved = "Checked";
                }
            }
            else {
                if (Orders[rowIndex].Status == 3) {
                    Orders[rowIndex].isApproved = "";
                }
            }
        }
        function ReloadTable() {
            $("#OrderList TBODY").html("");
            if (filtrkey != "All") {
                //alert(filtrkey);
                //Orders = Orders.filter(function (a) {
                //    return a.isApproved == filtrkey;
                //})

                Orders = Orders.filter(function (a) {
                    return a.ApproveStatus == filtrkey;
                })
            }
            var isappr = "";
            st = PgRecords * (pgNo - 1); slno = 0;
            for ($i = st; $i < st + PgRecords; $i++) {
                if ($i < Orders.length) {
                    slno = $i + 1;
                    $isapp = (Orders[$i].Status == 3) ? `<input type="checkbox" onclick="Chkclick(this)" name="chk" class="chktr" ${(Orders[$i].isApproved == "Checked") ? "checked" : ""}>` : ``;
                    $isapp1 = (Orders[$i].Status == 3) ? `<button class="btn btn-primary"  type="button" id="` + $i + `" onclick=\"Openrejectwindow(` + $i + `)\"  style="width:80px; height:30px">Reject</Button>` : ``;
                    tr = $(`<tr id="${Orders[$i].Sf_Code}:${Orders[$i].DDate}"></trs>`);
                    $(tr).html(`<td>${slno}</td><td>${$isapp}</td><td>${Orders[$i].DeviationDate}</td><td>${Orders[$i].Emplyee_ID}</td><td>${Orders[$i].Employee_Name}</td><td>${Orders[$i].HQ}</td><td>${Orders[$i].Reporting_To}</td><td>${Orders[$i].Deviation_Reason}</td><td>${Orders[$i].ApproveStatus}</td><td>${$isapp1}</td><td>${Orders[$i].Reason}</td>`);

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
        })

        $(".segment>li").on("click", function () {
            $(".segment>li").removeClass('active');
            $(this).addClass('active');
            filtrkey = $(this).attr('data-va');
            Orders = AllOrders;
            $("#tSearchOrd").val('');
            ReloadTable();
        });

        function loadData(sf, Mn, Yr) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "TP_Deviation_Approval.aspx/GetList",
                data: "{'divcode':'<%=Session["div_code"]%>','SF':'" + sf + "','Mn':'" + Mn + "','Yr':'" + Yr + "'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    excelArr = JSON.parse(data.d) || [];
                    Orders = AllOrders; ReloadTable();
                },
                error: function (result) {
                }
            });
        }

        function loadDataNew(sf, Fdt, Tdt) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "TP_Deviation_Approval.aspx/GetListNew",
                data: "{'divcode':'<%=Session["div_code"]%>','SF':'" + sf + "','Fdt':'" + Fdt + "','Tdt':'" + Tdt + "'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    excelArr = JSON.parse(data.d) || [];
                    Orders = AllOrders; ReloadTable();
                },
                error: function (result) {
                }
            });
        }



        function saveDeviationApproval() {


            var id = $('#ordDate').text();
            id = id.split('-');

            var Fdt = '';
            var Tdt = '';

            Fdt = id[2].trim() + '-' + id[1] + '-' + id[0];
            Tdt = id[5] + '-' + id[4] + '-' + id[3].trim();

            var sf = $('#flfield').val();
            let sxml = "<ROOT>";
            let filtArr = Orders.filter(a => a.Status == 3 && a.isApproved == "Checked");
            for (let i = 0; i < filtArr.length; i++) {
                sxml += `<TP SF="${filtArr[i]["Sf_Code"]}" Dt="${filtArr[i]["DDate"]}" RSF="<%=Session["sf_code"]%>" />`;
            }
            sxml += "</ROOT>";
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "TP_Deviation_Approval.aspx/saveApproval",
                data: "{'sXML':'" + sxml + "'}",
                dataType: "json",
                success: function (data) {
                    //alert(data.d);
                    //loadData('<=Session["sf_code"]%>', pgMNTH, pgYR);
                   <%-- loadDataNew('<%=Session["sf_code"]%>', Fdt, Tdt);--%>

                    loadDataNew(sf, Fdt, Tdt);
                    if (data.d != 'Failure') {
                        alert('Succesfully Approved');
                        return false;                       
                    }
                    return false;

                },
                error: function (result) {
                }
            });
        }




        //function checkboxcount() {         
        //       var number = $('.chktr option:selected').val();
        //        if ($('input:checkbox:checked').length > 1) {
        //            alert('Select any one checkbox');
        //            //$(this).prop('checked', false);//dont check the click checkbox
        //            return false;
        //        }

        //}
        function Openrejectwindow(id) {
            if ($('input:checkbox:checked').length <= 1) {
                let rowID = $('#' + id).closest("tr").attr("id");
                let sfCode = rowID.split(":")[0];
                let dDate = rowID.split(":")[1];
                let rowIndex = Orders.findIndex(x => x.Sf_Code == sfCode && x.DDate == dDate)
                if (Orders[rowIndex].Status == 3 && Orders[rowIndex].isApproved == "Checked") {
                    $('#myModal').modal('toggle');
                }
                else {
                    if (Orders[rowIndex].Status == 3) {
                        alert("Select the checkbox");
                        return false;
                    }
                }
            }
            else {
                alert('Select any one checkbox');
                return false;
            }
        }
        $('#Closebtn').on("click", function () {
            $('#message-text').val('');
        });
        function RejectDeviationApproval() {

            var reject = document.getElementById("message-text").value;
            if (reject == "") {
                alert('Enter the Reason for Rejection');
                return false;
            }

            
            


            var id = $('#ordDate').text();
            id = id.split('-');

            var Fdt = '';
            var Tdt = '';

            Fdt = id[2].trim() + '-' + id[1] + '-' + id[0];
            Tdt = id[5] + '-' + id[4] + '-' + id[3].trim();

            var sf = $('#flfield').val();
            let sxml = "<ROOT>";
            let filtArr = Orders.filter(a => a.Status == 3 && a.isApproved == "Checked");

            for (let i = 0; i < filtArr.length; i++) {
                filtArr[0].RSN = reject              

                sxml += `<TP SF="${filtArr[i]["Sf_Code"]}"  Dt="${filtArr[i]["DDate"]}" EDT="${filtArr[i]["EDate"]}" RSF="<%=Session["sf_code"]%>" RSN="${filtArr[i]["RSN"]}" Sl_No="${filtArr[i]["Sl_No"]}"/>`;
            }
            sxml += "</ROOT>";
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "TP_Deviation_Approval.aspx/RejectApproval",
                data: "{'sXML':'" + sxml + "'}",
                dataType: "json",
                success: function (data) {
                    //alert(data.d);
                    //loadData('<=Session["sf_code"]%>', pgMNTH, pgYR);
                   <%-- loadDataNew('<%=Session["sf_code"]%>', Fdt, Tdt);--%>
                    $('#myModal').modal('hide');
                    $('#message-text').val('');
                    loadDataNew(sf, Fdt, Tdt);
                },
                error: function (result) {
                }
            });
        }


        function selectch() {
            //var cmn = $('#fltpmnth').val();
            //var cyr = $('#fltpyr').val();
            //var csf = $('#flfield').val();
            //loadData(csf, cmn, cyr);           



            var id = $('#ordDate').text();

            var Fdt = '';
            var Tdt = '';


            id = id.split('-');

            Fdt = id[2].trim() + '-' + id[1] + '-' + id[0];
            Tdt = id[5] + '-' + id[4] + '-' + id[3].trim();

            //fillFieldForce();
            fillTpYR();
            //loadData(sf, pgMNTH, pgYR);

            var sf = $('#flfield').val();

            //alert(sf)
            if (sf == null || sf == "" || sf == "0" || sf == 0) {
                sf = 'admin';
                $('#flfield').val('admin');
            }

            $("#OrderList TBODY").html("");

            loadDataNew(sf, Fdt, Tdt);

            <%--loadDataNew('<%=Session["sf_code"]%>', Fdt, Tdt);--%>
        }

        function Achkall() {
            if ($('#selectAll').is(":checked")) {
                Orders = Orders.map(function (o) {
                    if (o.Status == 3) {
                        o["isApproved"] = "Checked";
                    }
                    return o;
                });
            }
            else {
                Orders = Orders.map(function (o) {
                    if (o.Status == 3) {
                        o["isApproved"] = "";
                    }
                    return o;
                });
            }
            ReloadTable();
        }



        //$(function () {
        //    var start = moment();
        //    var end = moment();
        //    function cb(start, end) {
        //        $('#reportrange span').html(start.format('DD-MM-YYYY') + ' - ' + end.format('DD-MM-YYYY'));

        //    }
        //    $('#reportrange').daterangepicker({
        //        startDate: start,
        //        endDate: end,
        //        ranges: {
        //            'Today': [moment(), moment()],
        //            'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
        //            'Last 7 Days': [moment().subtract(6, 'days'), moment()],
        //            'Last 30 Days': [moment().subtract(29, 'days'), moment()],
        //            'This Month': [moment().startOf('month'), moment().endOf('month')],
        //            'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
        //        },

        //    }, cb);
        //    cb(start, end);

        //});



        $("#reportrange").on("DOMSubtreeModified", function () {

            var id = $('#ordDate').text();
            id = id.split('-');

            var Fdt = '';
            var Tdt = '';

            Fdt = id[2].trim() + '-' + id[1] + '-' + id[0];
            Tdt = id[5] + '-' + id[4] + '-' + id[3].trim();

            //Fdt = id[2].trim() + '-' + id[1] + '-' + id[0] + ' 00:00:00';
            //Tdt = id[5] + '-' + id[4] + '-' + id[3].trim() + ' 00:00:00';

            $(".data-table-basic_length").val(PgRecords);
            //loadData();
            var sf = $('#flfield').val();

            if (sf == null || sf == "" || sf == "0" || sf == 0) {
                sf = 'admin';
                $('#flfield').val('admin');
            }

            loadDataNew(sf, Fdt, Tdt);

        });

        $(document).ready(function () {


            fillFieldForce();
            fillTpYR();

            var sf = $('#flfield').val();
            <%--sf = '<%=Session["sf_code"]%>';--%>
            sfty = '<%=Session["sf_type"]%>';

            var id = $('#ordDate').text();

            var Fdt = '';
            var Tdt = '';


            id = id.split('-');

            Fdt = id[2].trim() + '-' + id[1] + '-' + id[0];
            Tdt = id[5] + '-' + id[4] + '-' + id[3].trim();

            //loadData(sf, pgMNTH, pgYR);


            if (sf == null || sf == "" || sf == "0" || sf == 0) {
                sf = 'admin';
                $('#flfield').val('admin');
            }


            //alert(sf);
            loadDataNew(sf, Fdt, Tdt);

            $('#fltpmnth').selectpicker('val', pgMNTH);
            $('#fltpyr').selectpicker('val', pgYR);
            $('#flfield').selectpicker('val', sf);


        });
    </script>
</asp:Content>

