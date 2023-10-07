﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="OutletReview.aspx.cs" Inherits="MIS_Reports_OutletReview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form runat="server">
        <asp:HiddenField runat="server" ID="mgrhqn" />
        <div class="row">
            <div class="row">
                <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                    Style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 15px; top: 43px;" OnClick="lnkDownload_Click" />
            </div>
            <div class="col-lg-12 sub-header">
                Outlet Review Summary <span style="float: right; margin-right: 15px;">
                    <div id="reportrange" class="form-control mt-5 mb-5" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc;"><i class="fa fa-calendar"></i>&nbsp;<span id="ordDate"></span><i class="fa fa-caret-down"></i></div>
                </span>
            </div>
        </div>
        <div class="card">
            <div class="card-body table-responsive" style="overflow-x: auto;">
                <div style="white-space: nowrap">
                    Search&nbsp;&nbsp;<input type="text" id="tSearchOrd" style="width: 250px;" />
                    <span id="filspan" style="margin-left: 10px; display: none">Filter By&nbsp;&nbsp;            
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
                        <select class="form-control" id="txtfilter" name="ddfilter" style="min-width: 315px !important; max-width: 318px !important; display: inline"></select>

                    </span>
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
                <table class="table table-hover" id="OrderList" style="font-size: 12px">
                    <thead class="text-warning">
                        <tr style="background-color: #37a4c6; color: #fff; white-space: nowrap;">
                            <th style="text-align: left; color: #fff">Sl.No</th>
                            <th style="text-align: left; color: #fff">Date</th>
                            <th style="text-align: left; color: #fff">Employee Name</th>
                            <th style="text-align: left; color: #fff">HQ</th>
                            <th style="text-align: left; color: #fff">State</th>
                            <th style="text-align: left; color: #fff">Total Calls</th>
                            <th style="text-align: left; color: #fff">Productive Calls</th>
                            <th style="text-align: left; color: #fff">Non Productive Calls</th>
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
        </div>
        <div class="modal fade" id="leaveModal" style="z-index: 10000000; overflow-y: scroll;background:transparent;" tabindex="0" aria-hidden="true">
            <div class="modal-dialog" role="document" style="width: 98% !important">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="leaveModalLabel"></h5>
                        <asp:imagebutton id="ImageButton2" runat="server" align="right" imageurl="~/img/Excel-icon.png"
                                style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 39px; top: 8px;" onclick="ImageButton1_Click" />
                        <button type="button" class="close" style="margin-top: -20px;" data-dismiss="modal" data-toggle="modal" data-target="#summaryModal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    </div>
                    <div class="modal-body" style="padding-top: 10px">
                        <div class="row">
                            <div class="col-sm-12">
                                <table id="leavedets" style="width: 100%; font-size: 12px;">
                                    <thead class="text-warning">
                                        <tr>
                                            <th style="text-align: left">SlNO</th>
                                            <th style="text-align: left">Date</th>
                                            <th style="text-align: left">Outlet Code</th>
                                            <th style="text-align: left">Outlet Name</th>
                                            <th style="text-align: left">Route</th>
                                            <th style="text-align: left">Outlet Type</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal" data-toggle="modal" data-target="#summaryModal">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script language="javascript" type="text/javascript">
        var AllOrders = [];
        var sftyp = '<%=Session["sf_type"]%>';
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
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Sf_Code,Name,HQ,Date,StateName,";
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

        $(".segment>li").on("click", function () {
            $(".segment>li").removeClass('active');
            $(this).addClass('active');
            filtrkey = $(this).attr('data-va');
            Orders = AllOrders;
            $("#tSearchOrd").val('');
            ReloadTable();
        });
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
            for ($i = st; $i < st + Number(PgRecords) ; $i++) {
                if ($i < Orders.length) {
                    tr = $(`<tr id="${Orders[$i].Sf_Code}:${Orders[$i].Date}" mgrhq="${Orders[$i].HQ}"></tr>`);
                    $(tr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].Date + "</td><td>" + Orders[$i].Name + "</td><td>" + Orders[$i].HQ + "</td><td>" + Orders[$i].StateName + "</td><td class='calls' typ='TC'><a style='cursor: pointer;'>" + Orders[$i].TotalCalls + "</a></td><td class='calls' typ='PC'><a style='cursor: pointer;'>" + Orders[$i].ProductiveCalls + "</a></td><td class='calls' typ='AC'><a style='cursor: pointer;'>" + Orders[$i].NonProductiveCalls + "</a></td>");
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
        function loadCallData($SF, $dt, $callTyp) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "OutletReview.aspx/GetOutletDetails",
                data: "{'SF':'" + $SF + "','dt':'" + $dt + "','calltyp':'" + $callTyp + "'}",
                dataType: "json",
                success: function (data) {
                    AllOutlets = JSON.parse(data.d) || [];
                    $('#leavedets TBODY').html("");
                    var slno = 0;
                    for ($i = 0; $i < AllOutlets.length; $i++) {
                        if (AllOutlets.length > 0) {
                            slno += 1;
                            tr = $("<tr></tr>");
                            $(tr).html("<td>" + slno + "</td><td>" + AllOutlets[$i].Date + "</td><td>" + AllOutlets[$i].OutletCode + "</td><td>" + AllOutlets[$i].OutletName + "</td><td>" + AllOutlets[$i].Route + "</td><td>" + AllOutlets[$i].OutletType + "</td>");
                            $("#leavedets TBODY").append(tr);
                        }
                    }
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
        }
        function loadData() {
            if (fdt !== "" && tdt !== "") {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "OutletReview.aspx/GetDetails",
                    data: "{'SF':'<%=Session["Sf_Code"]%>','Div':'<%=Session["Division_Code"]%>','Mn':'" + fdt + "','Yr':'" + tdt + "'}",
                    dataType: "json",
                    success: function (data) {
                        AllOrders = JSON.parse(data.d) || [];
                        Orders = AllOrders;
                        ReloadTable();
                    },
                    error: function (result) {
                        $('#leavedets TBODY').html("<tr><td colspan='4'>Something went wrong. Try again.</td></tr>");
                    }
                });
            }
        }
        $(document).ready(function () {
            sf = '<%=Session["Sf_Code"]%>';
            loadData();
            $("#reportrange").on("DOMSubtreeModified", function () {
                id = $('#ordDate').text();
                if (id != "") {
                    id = id.split('-');
                    fdt = id[2].trim() + '-' + id[1] + '-' + id[0];
                    tdt = id[5] + '-' + id[4] + '-' + id[3].trim();
                    loadData();
                }
            });
            $(document).on('click', '.calls', function () {
                let trdata = $(this).closest('tr').attr('id').split(':');
                let mgrhqname = $(this).closest('tr').attr('mgrhq');
                $("#<%=mgrhqn.ClientID%>").val(mgrhqname);
                let mgrCode = trdata[0];
                let wrkdate = trdata[1];                
                var dts = wrkdate.split('/');
                wrkdate = dts[2] + '-' + dts[1] + '-' + dts[0]
                let callType = $(this).closest('td').attr('typ');
                console.log(callType);
                $('#leaveModal').modal('toggle');
                $('#leavedets TBODY').html("<tr><td colspan='4'>Loading please wait...</td></tr>");
                $("#leaveModalLabel").html(`Outlet Details`);
                setTimeout(function () { loadCallData(mgrCode, wrkdate, callType) }, 500);
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
