<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Shift_Master.aspx.cs" Inherits="MasterFiles_Shift_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="../css/timepicker/bootstrap-clockpicker.min.css" />
    <link href="../css/jquery.multiselect.css" rel="stylesheet" />
    <style>
        .popover {
            z-index: 10000000;
        }
    </style>
    <div class="row">
        <div class="col-lg-12 sub-header">
            Shift Master<span style="float: right"><a href="#" class="btn btn-primary btn-update" id="newsf">Add New</a></span>
            <div style="float: right">
                <ul class="segment">
                    <li data-va='All'>ALL</li>
                    <li data-va='0' class="active">Active</li>
                </ul>
            </div>
        </div>
    </div>
    <div id="MyPopup" class="modal fade" role="dialog" style="z-index: 10000000; background-color: #0000000f">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title"></h4>
                </div>
                <div class="modal-body">
                    <input type="hidden" id="hscode" />
                    <div class="row">
                        <div class="col-sm-2">
                            <label>Shift Name</label>
                        </div>
                        <div class="col-sm-10">
                            <input type="text" id="txtsname" class="form-control" autocomplete="off" />
                        </div>
                    </div>
                    <div class="row" style="margin-top: 10px;">
                        <div class="col-sm-2">
                            <label>Shift ID</label>
                        </div>
                        <div class="col-sm-10">
                            <input type="text" id="txtshtcode" class="form-control" autocomplete="off" />
                        </div>
                    </div>
                    <div class="row" style="margin-top: 10px;">
                        <div class="col-sm-2">
                            <label>Start Time</label>
                        </div>
                        <div class="col-sm-10">
                            <div class="input-group clockpicker" data-placement="top" data-align="left" data-donetext="Done">
                                <input type="text" class="form-control" id="txtstime" />
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-time"></span>
                                </span>
                            </div>

                        </div>
                    </div>
                    <div class="row" style="margin-top: 10px;">
                        <div class="col-sm-2">
                            <label>End Time</label>
                        </div>
                        <div class="col-sm-10">
                            <div class="input-group clockpicker" data-placement="top" data-align="left" data-donetext="Done">
                                <input type="text" class="form-control" id="txtetime" />
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-time"></span>
                                </span>
                            </div>

                        </div>
                    </div>
                    <div class="row" style="margin-top: 10px;display:none;">
                        <div class="col-sm-2">
                            <label>Department</label>
                        </div>
                        <div class="col-sm-10">
                            <select class="selectpicker" id="mselectdept" multiple>
                            </select>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 10px;display:none;">
                        <div class="col-sm-2">
                            <label>HQ</label>
                        </div>
                        <div class="col-sm-10">
                            <select class="selectpicker" id="mselect" multiple>
                            </select>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 10px;">
                        <div class="col-sm-2">
                            <label style="white-space:nowrap;">Auto Checkout</label>
                        </div>
                        <div class="col-sm-10" style="display: flex;">
                            <div class="col-xs-6" style="margin-left: -15px;">
                                <select class="msl form-control" id="ddlsl">
                                    <option value="2">--Select--</option>
                                    <option value="0">End of the Day</option>
                                    <option value="1">Time</option>
                                </select>
                            </div>
                            <div class="col-xs-4 cselect" style="display: flex;">
                                <span style="margin-top: 4px;">Hours</span>
                                <select class="form-control hsl" id="tselect" style="width: 100px; margin-left: 4px;">
                                    <option value="0">--Select--</option>
                                    <option value="1">1</option>
                                    <option value="2">2</option>
                                    <option value="3">3</option>
                                    <option value="4">4</option>
                                    <option value="5">5</option>
                                    <option value="6">6</option>
                                    <option value="7">7</option>
                                    <option value="8">8</option>
                                </select>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnsave" class="btn btn-success">Save</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-body table-responsive">
            <div style="white-space: nowrap">
                Search&nbsp;&nbsp;<input type="text" autocomplete="off" id="tSearchOrd" style="width: 250px;" />
                <label style="white-space: nowrap; margin-left: 57px; display: none;">Filter By&nbsp;&nbsp;<select id="txtfilter" name="ddfilter" style="width: 250px; display: none;"></select></label>
                <label style="float: right">
                    Shows
                    <select class="data-table-basic_length" aria-controls="data-table-basic">
                        <option value="10">10</option>
                        <option value="25">25</option>
                        <option value="50">50</option>
                        <option value="100">100</option>
                    </select>
                    entries</label>
            </div>
            <table class="table table-hover" id="OrderList">
                <thead class="text-warning">
                    <tr>
                        <th style="text-align: left">SlNO</th>
                        <th style="text-align: left">Shift ID</th>
                        <th style="text-align: left">Shift Name</th>
                        <th style="text-align: left">Start Time</th>
                        <th style="text-align: left">End Time</th>
                        <th style="text-align: left">Edit</th>
                        <th style="text-align: left">Status</th>
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
                        <ul class="pagination">
                            <li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">Previous</a></li>
                            <li class="paginate_button active"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">1</a></li>
                            <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="2" tabindex="0">Next</a></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="../js/plugins/timepicker/bootstrap-clockpicker.min.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
    <script language="javascript" type="text/javascript">
        var AllOrders = [];
        var divcode; filtrkey = '0';
        optStatus = "<li><a href='#' v='0'>Active</a><a href='#' v='1'>Deactivate</a></li>"
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Sft_ID,Sft_Name,Sft_STime,sft_ETime,Status";

        $('.clockpicker').clockpicker({
            placement: 'bottom',
            align: 'left',
            donetext: 'Done',
            default: 'now',
            vibrate: true,
        });
        function fillshift(scode) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Shift_Master.aspx/getshift1",
                data: "{'divcode':" + divcode + ",'scode':" + scode + "}",
                dataType: "json",
                success: function (data) {
                    var dts = JSON.parse(data.d) || [];
                    $('#hscode').val(dts[0].Sft_ID);
                    $('#txtsname').val(dts[0].Sft_Name);
                    var ar = (dts[0].Sft_STime).split('T');
                    var aarr = (dts[0].sft_ETime).split('T');
                    $('#txtstime').val(ar[1]);
                    $('#txtetime').val(aarr[1]);
                    $('#txtshtcode').val(dts[0].Sft_Code);
                    //var hq1 = dts[0].HQ_Code;
                    //var dept2 = dts[0].dept_code;
                    //hq1 = hq1.split(',');
                    //dept2 = dept2.split(',');
                    //$('#mselect  > option').each(function () {
                    //    for (var i = 0; i < hq1.length; i++) {
                    //        if (hq1[i] == $(this).val()) { $(this).prop('selected', true); $('#mselect').multiselect('reload'); $('.ms-options ul').css('column-count', '3') }
                    //    }
                    //});
                    //$('#mselectdept  > option').each(function () {
                    //    for (var i = 0; i < dept2.length; i++) {
                    //        if (dept2[i] == $(this).val()) { $(this).prop('selected', true); $('#mselectdept').multiselect('reload'); $('.ms-options ul').css('column-count', '3') }
                    //    }
                    //});
                    $('.msl').val(dts[0].ACtOffTyp);
                    if (dts[0].ACtOffTyp == 1) {
                        $('.cselect').show();
                    }
                    $('.hsl').val(dts[0].ACtOffHrs);
                },
                error: function (data) {
                    alert(JSON.stringify(data));
                }
            });
        }
        function fillshiftID(divcode) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Shift_Master.aspx/getShiftID",
                data: "{'divcode':" + divcode + "}",
                dataType: "json",
                success: function (data) {
                    var dts = JSON.parse(data.d) || [];
                    $('#hscode').val(dts[0].ID);
                },
                error: function (data) {
                    alert(JSON.stringify(data));
                }
            });
        }
        $(".data-table-basic_length").on("change",
        function () {
            pgNo = 1;
            PgRecords = $(this).val();
            ReloadTable();
        }
        );

        function loadPgNos() {
            $(".pagination").html("");
            TotalPg = (Orders.length / PgRecords).toFixed(0) //+ ((Orders.length % PgRecords) ? 1 : 0)
            spg = '<li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">Previous</a></li>';
            for (il = 0; il < TotalPg; il++) {
                spg += '<li class="paginate_button' + ((pgNo == (il + 1)) ? " active" : "") + '"><a href="#" aria-controls="example2" data-dt-idx="' + (il + 1) + '" tabindex="0">' + (il + 1) + '</a></li>';
            }
            spg += '<li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="' + (il + 1) + '" tabindex="0">Next</a></li>';
            $(".pagination").html(spg);

            $(".paginate_button > a").on("click", function () {
                pgNo = $(this).text(); ReloadTable();
                /* $(".paginate_button").removeClass("active");
                 $(this).closest(".paginate_button").addClass("active");*/
            }
           );
        }
        function clear() {
            $('#txtsname').val('');
            $('#txtstime').val('');
            $('#txtetime').val('');
            $('#txtshtcode').val('');
            $('.msl').val(2);
            $('.cselect').hide();
            $('.hsl').val(0);
        }
        function ShowPopup(title) {
            $("#MyPopup .modal-title").html(title);
            $("#MyPopup").modal("show");
            clear();
        }
        function ReloadTable() {
            $("#OrderList TBODY").html("");
            if (filtrkey != "All") {
                Orders = Orders.filter(function (a) {
                    return a.Active_Flag == filtrkey;
                })
            }
            st = PgRecords * (pgNo - 1); slno = 0;
            for ($i = st; $i < st + PgRecords ; $i++) {
                //var filtered = Orders.filter(function (x) {
                //    return x.Sf_Code != 'admin';
                //})
                if ($i < Orders.length) {
                    tr = $("<tr></tr>");
                    //var hq = filtered[$i].Sf_Name.split('-');
                    slno = $i + 1;
                    var ar = (Orders[$i].STime).split('T');
                    var aarr = (Orders[$i].ETime).split('T');
                    $(tr).html('<td>' + slno + '</td><td>' + Orders[$i].ID + '</td><td>' + Orders[$i].SName + '</td><td>' + ar[1] +
                    '</td><td>' + aarr[1] + '</td><td id=' + Orders[$i].ID +
                    ' class="sfedit"><a href="#">Edit</a></td>' +
                    '<td><ul class="nav" style="margin:0px"><li class="dropdown"><a href="#" style="padding:0px" class="dropdown-toggle" data-toggle="dropdown">'
                        + '<span><span class="aState" data-val="' + Orders[$i].Status + '">' + Orders[$i].Status + '</span><i class="caret" style="float:right;margin-top:8px;margin-right:0px"></i></span></a>' +
                        '<ul class="dropdown-menu dropdown-custom dropdown-menu-right ddlStatus" style="right:0;left:auto;">' + optStatus + '</ul></li></ul></td>');

                    $("#OrderList TBODY").append(tr);
                    hq = [];
                }
            }
            $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")

            $(".ddlStatus>li>a").on("click", function () {
                cStus = $(this).closest("td").find(".aState");
                stus = $(this).attr("v");
                $indx = $(this).closest("tr").index();
                cStusNm = $(this).text();
                if (confirm("Do you want change status " + $(cStus).text() + " to " + cStusNm + " ?")) {
                    sf = Orders[$indx].ID;
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Shift_Master.aspx/SetNewStatus",
                        data: "{'SF':'" + sf + "','stus':'" + stus + "'}",
                        dataType: "json",
                        success: function (data) {
                            Orders[$indx].Active_Flag = stus;
                            Orders[$indx].Status = cStusNm;
                            $(cStus).html(cStusNm);

                            ReloadTable();
                            alert('Status Changed Successfully...');

                        },
                        error: function (result) {
                        }
                    });
                }
            });
            loadPgNos();
        }
        <%--function filldept() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Shift_Master.aspx/GetDeptDetails",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    var Dep = JSON.parse(data.d) || [];
                    var ms = $("[id*=mselectdept]");
                    ms.empty();
                    for (var i = 0; i < Dep.length; i++) {
                        ms.append($('<option value="' + Dep[i].subdivision_code + '">' + Dep[i].subdivision_name + '</option>'));
                    }
                    $('#mselectdept').multiselect({
                        columns: 3,
                        placeholder: 'Select Department',
                        search: true,
                        searchOptions: {
                            'default': 'Search Department'
                        },
                        selectAll: true
                    }).multiselect('reload');
                    $('.ms-options ul').css('column-count', '3');
                }
            });
        }--%>
        <%--function fillhq() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Shift_Master.aspx/GetHQDetails",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    var HQ = JSON.parse(data.d) || [];
                    var sf = $("[id*=mselect]");
                    sf.empty();
                    for (var i = 0; i < HQ.length; i++) {
                        sf.append($('<option value="' + HQ[i].HQ_ID + '">' + HQ[i].HQ_Name + '</option>'));
                    }
                    $('#mselect').multiselect({
                        columns: 3,
                        placeholder: 'Select HQ',
                        search: true,
                        searchOptions: {
                            'default': 'Search HQ'
                        },
                        selectAll: true
                    }).multiselect('reload');
                    $('.ms-options ul').css('column-count', '3');
                }
            });
        }--%>
        $('.msl').on('change', function () {
            if ($(this).val() == 1) {
                $('.cselect').show();
            }
        });
        $(".segment>li").on("click", function () {
            $(".segment>li").removeClass('active');
            $(this).addClass('active');
            filtrkey = $(this).attr('data-va');
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
        })
        function loadData(divcode, sf) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Shift_Master.aspx/getShift",
                data: "{'divcode':'" + divcode + "'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders;
                    ReloadTable();
                },
                error: function (result) {
                }
            });
        }

        $(document).ready(function () {
            $('.cselect').hide();
            var sf;
            sf = 'admin';
            divcode = Number(<%=Session["div_code"]%>);
            loadData(divcode, sf);
            $(document).on('click', '#newsf', function () {
                var title = 'Shift Details';
                var scode1 = '';
                ShowPopup(title, scode1);
                fillshiftID(divcode);
                //filldept();
                //fillhq();
            });
            $('#btnsave').on('click', function () {
                var scode = $('#hscode').val();
                var sname = $('#txtsname').val();
                if (sname == '' || sname == null) {
                    alert('Enter the Shift name');
                    return false;
                }
                var shtscode = $('#txtshtcode').val();
                if (shtscode == '' || shtscode == null) {
                    alert('Enter the Shift ID');
                    return false;
                }

                var stime = $('#txtstime').val();
                if (stime == '' || stime == null) {
                    alert('Enter the Start Time');
                    return false;
                }
                var etime = $('#txtetime').val();
                if (etime == '' || etime == null) {
                    alert('Enter the End Time');
                    return false;
                }
                var from = etime.split(':');
                var to = stime.split(':');
                var duration = (from[0] + '.' + from[1]) - (to[0] + '.' + to[1]);
                var shq = '';
                //$('#mselect  > option:selected').each(function () {
                //    shq += ',' + $(this).val();
                //});
                //if (shq == '') {
                //    alert('Select Atleast one HQ');
                //    return false;
                //}
                var mdpt = '';
                //$('#mselectdept  > option:selected').each(function () {
                //    mdpt += ',' + $(this).val();
                //});
                //if (mdpt == '') {
                //    alert('Select Atleast one Department ');
                //    return false;
                //}
                var etype = $('.msl :selected').val();
                if (etype == '2') {
                    alert('Select the End Type');
                    return false;
                }
                var ehours = $('.hsl :selected').val();
                data = { "Divcode": divcode, "SCode": scode, "SName": sname, "STime": stime, "ETime": etime, "HQCode": shq, "ShtCode": shtscode, "Duration": duration, "Endtype": etype, "Endhour": ehours, 'Depart': mdpt }
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Shift_Master.aspx/saveshift",
                    data: "{'data':'" + JSON.stringify(data) + "'}",
                    dataType: "json",
                    success: function (data) {
                        alert(data.d);
                        window.location.href = "Shift_Master.aspx";
                    },
                    error: function (data) {
                        alert(JSON.stringify(data));
                    }
                });
            });
            $(document).on('click', '.sfedit', function () {
                var title = 'Shift Details';
                x = this.id;
                ShowPopup(title);
                //fillhq();
                //filldept();
                fillshift(x);
            });
        });
    </script>
</asp:Content>

