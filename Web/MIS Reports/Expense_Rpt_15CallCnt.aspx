<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Expense_Rpt_15CallCnt.aspx.cs" Inherits="MIS_Reports_Expense_Rpt_15CallCnt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
    <script src="../js/jquery-ui-1.10.3.min.js"></script>
    <script src="../js/popper-1.16.1.min.js"></script>
    <style type="text/css">
        .spinner {
            margin: 100px auto;
            width: 50px;
            height: 40px;
            text-align: center;
            font-size: 10px;
        }

            .spinner > div {
                background-color: #333;
                height: 100%;
                width: 6px;
                display: inline-block;
                -webkit-animation: sk-stretchdelay 1.2s infinite ease-in-out;
                animation: sk-stretchdelay 1.2s infinite ease-in-out;
            }

            .spinner .rect2 {
                -webkit-animation-delay: -1.1s;
                animation-delay: -1.1s;
            }

            .spinner .rect3 {
                -webkit-animation-delay: -1.0s;
                animation-delay: -1.0s;
            }

            .spinner .rect4 {
                -webkit-animation-delay: -0.9s;
                animation-delay: -0.9s;
            }

            .spinner .rect5 {
                -webkit-animation-delay: -0.8s;
                animation-delay: -0.8s;
            }

        @-webkit-keyframes sk-stretchdelay {
            0%, 40%, 100% {
                -webkit-transform: scaleY(0.4)
            }

            20% {
                -webkit-transform: scaleY(1.0)
            }
        }

        @keyframes sk-stretchdelay {
            0%, 40%, 100% {
                transform: scaleY(0.4);
                -webkit-transform: scaleY(0.4);
            }

            20% {
                transform: scaleY(1.0);
                -webkit-transform: scaleY(1.0);
            }
        }

        .spinnner_div {
            width: 1200px;
            height: 1000px;
            background: rgba(255, 255, 255, 0.1);
            backdrop-filter: blur(2px);
            position: absolute;
            z-index: 100;
            overflow-y: hidden;
        }

        #loader {
            position: absolute;
            left: 50%;
            top: 50%;
            z-index: 1;
            width: 120px;
            height: 120px;
            margin: -76px 0 0 -76px;
            border: 16px solid #f3f3f3;
            border-radius: 50%;
            border-top: 16px solid #3498db;
            -webkit-animation: spin 2s linear infinite;
            animation: spin 2s linear infinite;
        }

        .overlay {
            background-color: #EFEFEF;
            position: fixed;
            width: 100%;
            height: 100%;
            z-index: 1000;
            top: 0px;
            left: 0px;
            opacity: .5; /* in FireFox */
            filter: alpha(opacity=50); /* in IE */
            @keyframes shake {
                0% {
                    transform: translate(1px, 1px) rotate(0deg);
                }

                10% {
                    transform: translate(-1px, -2px) rotate(-1deg);
                }

                20% {
                    transform: translate(-3px, 0px) rotate(1deg);
                }

                30% {
                    transform: translate(3px, 2px) rotate(0deg);
                }

                40% {
                    transform: translate(1px, -1px) rotate(1deg);
                }

                50% {
                    transform: translate(-1px, 2px) rotate(-1deg);
                }

                60% {
                    transform: translate(-3px, 1px) rotate(0deg);
                }

                70% {
                    transform: translate(3px, 1px) rotate(-1deg);
                }

                80% {
                    transform: translate(-1px, -1px) rotate(1deg);
                }

                90% {
                    transform: translate(1px, 2px) rotate(0deg);
                }

                100% {
                    transform: translate(1px, -2px) rotate(-1deg);
                }
            }

            .shaking-image

        {
            animation: shake 1s cubic-bezier(.36,.07,.19,.97) both;
            transform: translate3d(0, 0, 0);
            backface-visibility: hidden;
            /*perspective: 1000px;*/
        }

        .shaking-image:hover {
            animation: shake 1s cubic-bezier(.36,.07,.19,.97) both;
            transform: translate3d(0, 0, 0);
            backface-visibility: hidden;
        }

        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        var FDT = '';
        var TDT = ''; var AllOrders = [], Orders = [], Sts = [], SFDets = [];
        var pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = 'Activity_Date,WorkType_Name,AllowanceValue,Calls_Cnt,SDP_Name,ListedDr_Name,';
        $(document).ready(function () {
            $("#reportrange").on("DOMSubtreeModified", function () {
                id = $('#ordDate').text();
                id = id.split('-');
                FDT = id[2].trim() + '-' + id[1] + '-' + id[0];
                TDT = id[5] + '-' + id[4] + '-' + id[3].trim();
                $('#loadover').show();
                setTimeout(function () {
                    setTimeout(loadData(), 500);
                }, 500);
            });
            $('#nodata').hover(function () {
                $('#imgshake').removeClass('shaking-image');
                $('#imgshake').addClass('shaking-image');
            }, function () {
                $('#imgshake').removeClass('shaking-image');
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Expense_Rpt_15CallCnt.aspx/state",
                dataType: "json",
                success: function (data) {
                    Sts = JSON.parse(data.d) || [];
                    if (Sts.length > 0) {
                        str = '';
                        str += "<option value=''>Nothing Select</option>";
                        for (var i = 0; i < Sts.length; i++) {
                            str += "<option value=" + Sts[i].State_Code + ">" + Sts[i].StateName + "</option>";
                        }
                        $("#ddlState").append(str);
                        $("#ddlState").selectpicker({
                            liveSearch: true
                        });
                    }

                },
                error: function (res) {
                    alert(res);
                }
            });

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Expense_Rpt_15CallCnt.aspx/SalesForceList",
                dataType: "json",
                success: function (data) {
                    SFDets = JSON.parse(data.d) || [];
                    if (SFDets.length > 0) {
                        str = '';
                        str += "<option value=''>Nothing Select</option>";
                        for (var i = 0; i < SFDets.length; i++) {
                            str += "<option value=" + SFDets[i].Sf_Code + ">" + SFDets[i].Sf_Name + "</option>";
                        }
                        $("#ddlSF").append(str);
                        $("#ddlSF").selectpicker({
                            liveSearch: true
                        });
                    }
                },
                error: function (res) {
                    alert(res);
                }
            });
            $(".data-table-basic_length").on("change",
                function () {
                    pgNo = 1;
                    PgRecords = $(this).val();
                    ReloadTable();
                }
            );
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
            $("#ddlState").on('change', function () {
                var State_Code = $("#ddlState").val();
                var FilterSt_ord = AllOrders.filter(function (a) {
                    return a.State_Code == State_Code;
                })
                var SfFilter = SFDets.filter(function (b) {
                    return b.State_Code == State_Code;
                })
                if (SfFilter.length > 0) {
                    $("#ddlSF").empty();
                    $("#ddlSF").selectpicker("refresh");
                    str = '';
                    str += "<option value=''>Nothing Select</option>";
                    for (var i = 0; i < SfFilter.length; i++) {
                        str += "<option value=" + SfFilter[i].Sf_Code + ">" + SfFilter[i].Sf_Name + "</option>";
                    }
                    $("#ddlSF").append(str);
                    $("#ddlSF").selectpicker("refresh");
                }
                Orders = FilterSt_ord;
                ReloadTable();
            })
            $("#ddlSF").on('change', function () {
                var Sf = $("#ddlSF").val();
                var FilterSF_ord = AllOrders.filter(function (a) {
                    return a.Sf_Code == Sf;
                })
                Orders = FilterSF_ord;
                ReloadTable();
            })
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
                if ('<%=Session["div_code"]%>' == '4' ||'<%=Session["div_code"].ToString()%>' == '106' ||'<%=Session["div_code"].ToString()%>' == '107' ||'<%=Session["div_code"].ToString()%>' == '109') {
                    $('.hrts').show();
                    $('#OrderList>tbody>tr>.rts').show();
                }
            }
            );
        }
        function ReloadTable() {
            $('#nodata').hide();
            $("#CallCnttbl").show();
            $("#CallCnttbl TBODY").html("");
            slno = 0;
            st = PgRecords * (pgNo - 1); slno = 0;
            if (Orders.length > 0) {
                for ($i = st; $i < st + PgRecords; $i++) {
                    if ($i < Orders.length) {
                        tr = $("<tr></tr>");
                        slno = $i + 1;
                        $(tr).html("<td >" + slno + "</td><td >" + Orders[$i].Activity_Date + "</td><td>" + Orders[$i].Sf_Name + "</td>" + Orders[$i].sf_emp_id + "<td></td><td>" + Orders[$i].subdivision_name + "</td><td >" + Orders[$i].WorkType_Name + "</td><td >" + Orders[$i].SDP_Name + "</td><td >" + Orders[$i].Stockist_Name + "</td><td>" + Orders[$i].TotCallCnt + "</td><td >" + Orders[$i].Calls_Cnt + "</td><td >" + Orders[$i].AllowanceValue + "</td>");
                        $("#CallCnttbl TBODY").append(tr);
                    }
                }
                $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")
                loadPgNos();
            }
            else {
                $('#nodata').show();
                $("#CallCnttbl").hide();
            }

            $('#loadover').hide();
        }
        function loadData() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Expense_Rpt_15CallCnt.aspx/GetCallCntRpt",
                data: "{'Frmdt':'" + FDT + "','Todt':'" + TDT +"'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders; ReloadTable();
                },
                error: function (result) {
                    alert(result);
                }
            });
        }
        $(function () {

            var start = moment();
            var end = moment();

            function cb(start, end) {
                $('#reportrange span').html(start.format('DD-MM-YYYY') + ' - ' + end.format('DD-MM-YYYY'));
                //$('#date_details').text(' From ' + start.format('DD/MM/YYYY') + ' To ' + end.format('DD/MM/YYYY'));

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
    <div class="overlay" id="loadover" style="display: none;">
        <div id="loader"></div>
    </div>
    <div>
        <div class="row">
            <div class="col-lg-5">
                <label style="padding-right: 10px;">State</label>
                <select id="ddlState">
                </select>
            </div>
            <div class="col-lg-5">
                <label style="padding-right: 10px;">Field Force</label>
                <select id="ddlSF">
                </select>
            </div>
            <div class="col-lg-2">
                <div id="reportrange" class="form-control mt-5 mb-5" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc;">
                    <i class="fa fa-calendar"></i>&nbsp;
               
                        <span id="ordDate"></span><i class="fa fa-caret-down"></i>
                </div>
            </div>
        </div>

        <div class="card table table-responsive">
            <div style="white-space: nowrap; padding: 15px;">
                Search&nbsp;&nbsp;<input type="text" id="tSearchOrd" style="width: 250px;" />
                <label style="float: right">
                    Show
                   
                            <select class="data-table-basic_length" aria-controls="data-table-basic">
                                <option value="10">10</option>
                                <option value="25">25</option>
                                <option value="50">50</option>
                                <option value="100">100</option>
                            </select>
                    entries</label>
            </div>
            <div class="card" id="nodata" style="/*overflow: scroll; */height: 82px; /*margin: auto; */margin-bottom: 16px; /*max-height: 500px; */display: none;">
                <span style="margin: auto; font-size: 22px; color: #998075;">
                    <img id="imgshake" src="data:image/png;base64,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" style="width: 39px; margin: auto;">
                    No Data Found !.</span>
            </div>
            <table class="table" id="CallCnttbl">
                <thead>
                    <tr>
                        <th>Sl No.</th>
                        <th>Date</th>
                        <th>Employee Name</th>
                        <th>Employee ID</th>
                        <th>Division Name</th>
                        <th>Work Type</th>
                        <th>Place of Work</th>
                        <th>Distributor Name</th>
                        <th>Total Count</th>
                        <th>Productive  Call Count</th>
                        <th>Allowance Value</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
            <div class="row">
                <div class="col-sm-5" style="padding: 25px;">
                    <div class="dataTables_info" id="orders_info" role="status" aria-live="polite">Showing 1 to 10 of nth entries</div>
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
</asp:Content>

