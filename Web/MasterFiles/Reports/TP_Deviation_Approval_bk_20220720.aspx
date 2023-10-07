<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="TP_Deviation_Approval.aspx.cs" Inherits="MasterFiles_Reports_TP_Deviation_Approval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type="text/css" />
    <style>
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
            <img onclick="exceldwnld()" src="../../img/Excel-icon.png" style="cursor: pointer; float: right;" id="btnExport" width="40" height="40" />
        </div>
        <div class="row">
            <div class="col-lg-12 sub-header">
                TP Deviation Dates
                <select id="flfield" data-size="6" data-dropup-auto="false" onchange="selectch()" style="float: right">
                </select>
                <select id="fltpmnth" data-size="5" data-dropup-auto="false" onchange="selectch()" style="float: right">
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
                <select id="fltpyr" data-size="5" data-dropup-auto="false" onchange="selectch()" style="float: right">
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
                </div>
                <table class="table table-hover" id="OrderList" style="font-size: 12px;">
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
                            <th style="text-align: left">Approve Status</th>
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
    </form>
    <script src="../../js/xlsx.full.min.js"></script>
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
        optStatus = "<li><a href='#' v='2'>Approve</a><a href='#' v='-1'>Reject</a></li>"
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Emplyee_ID,Employee_Name,HQ,Reporting_To,DeviationDate,ApproveStatus,";
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
            });
            var droptblname = $("#sheetid option:selected").text();
            var ws = XLSX.utils.json_to_sheet(excelArr);
            var wb = XLSX.utils.book_new();
            XLSX.utils.book_append_sheet(wb, ws, droptblname);
            XLSX.writeFile(wb, "TP Deviation Dates.xlsx");
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
                Orders = Orders.filter(function (a) {
                    return a.isApproved == filtrkey;
                })
            }
            var isappr = "";
            st = PgRecords * (pgNo - 1); slno = 0;
            for ($i = st; $i < st + PgRecords; $i++) {
                if ($i < Orders.length) {
                    slno = $i + 1;
                    $isapp = (Orders[$i].Status == 3) ? `<input type="checkbox" onclick="Chkclick(this)" class="chktr" ${(Orders[$i].isApproved == "Checked") ? "checked" : ""}>` : ``;
                    tr = $(`<tr id="${Orders[$i].Sf_Code}:${Orders[$i].DDate}"></tr>`);
                    $(tr).html(`<td>${slno}</td><td>${$isapp}</td><td>${Orders[$i].DeviationDate}</td><td>${Orders[$i].Emplyee_ID}</td><td>${Orders[$i].Employee_Name}</td><td>${Orders[$i].HQ}</td><td>${Orders[$i].Reporting_To}</td><td>${Orders[$i].ApproveStatus}</td>`);
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
        function saveDeviationApproval() {
            let sxml = "<ROOT>";
            let filtArr = Orders.filter(a =>  a.Status == 3 && a.isApproved == "Checked");
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
                    alert(data.d);
                    loadData('<%=Session["sf_code"]%>', pgMNTH, pgYR);
                },
                error: function (result) {
                }
            });
        }
        function selectch() {
            var cmn = $('#fltpmnth').val();
            var cyr = $('#fltpyr').val();
            var csf = $('#flfield').val();
            loadData(csf, cmn, cyr);
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
        $(document).ready(function () {
            sf = '<%=Session["sf_code"]%>';
            sfty = '<%=Session["sf_type"]%>';
            fillFieldForce();
            fillTpYR();
            loadData(sf, pgMNTH, pgYR);
            $('#fltpmnth').selectpicker('val', pgMNTH);
            $('#fltpyr').selectpicker('val', pgYR);
            $('#flfield').selectpicker('val', sf);
        });
    </script>
</asp:Content>
