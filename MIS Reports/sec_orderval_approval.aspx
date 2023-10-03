<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="sec_orderval_approval.aspx.cs" Inherits="MIS_Reports_sec_orderval_approval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form runat="server">
        <div class="row" style="margin-bottom: 1rem;">
     
           <div class="col-lg-12 sub-header">Secondary Order Value Approval</div>
        </div>
       
        <div class="card">
            <div class="card-body table-responsive" style="overflow-x: auto;">
                <div style="white-space: nowrap">
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
                <table class="table table-hover" id="OrderList" style="font-size: 12px">
                    <thead class="text-warning">
                        <tr style="background-color: #37a4c6; color: #fff; white-space: nowrap;">
                            <th style="text-align: left; color: #fff">Sl.No</th>
                            <th style="text-align: left; color: #fff;">STATE NAME</th>
                            <th style="text-align: left; color: #fff">REPORTING MANAGER</th>
                            <th style="text-align: left; color: #fff">SO NAME</th>
                            <th style="text-align: left; color: #fff">EMP ID</th>
                            <th style="text-align: left; color: #fff">MONTH</th>
                            <th style="text-align: left; color: #fff">Total Booked Value</th>
                            <th style="text-align: left; color: #fff">Total Supplied Value</th>
                            <th style="text-align: left; color: #fff">Difference</th>
                            <th style="text-align: left; color: #fff">Action</th>
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
    </form>
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
    <script language="javascript" type="text/javascript">
        var AllOrders = []; var Allsf = []; var hqsf = []; var Allstate = []; var Ostate = []; var AllHQ = []; var SFHQ = [];
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "StateName,SF_Name,sf_emp_id,rsf";
        var routes = [];

        $(".data-table-basic_length").on("change",
            function () {
                pgNo = 1;
                PgRecords = $(this).val();
                ReloadTable();
            }
        );
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
            for ($i = st; $i < st + Number(PgRecords); $i++) {
                if ($i < Orders.length) {

                    tr = $("<tr></tr>");
                    $(tr).html("<td style='white-space: nowrap;'><input type='hidden' name='hlid' value='" + Orders[$i].created_date + "'/>" + ($i + 1) + "</td><td style='white-space: nowrap;'>" + Orders[$i].StateName + "</td><td>" + Orders[$i].rsf + "</td><td><input type='hidden' name='hsf' value='" + Orders[$i].SF_Code + "'/>" + Orders[$i].SF_Name + "</td><td style='white-space: nowrap;'>" + Orders[$i].sf_emp_id + "</td><td style='white-space: nowrap;'>" + Orders[$i].mon + "-" + Orders[$i].year + "</td>" +
                        "<td style='white-space: nowrap;'>" + Orders[$i].tot_order_value + "</td><td style='white-space: nowrap;'>" + Orders[$i].acheived_value + "</td><td style='white-space: nowrap;'>" + Orders[$i].diff_value + "</td><td style='white-space: nowrap;'><a id='btnApp' class='btn btn-primary'>Approval</a> <a id='btnRej' class='btn btn-danger'>Reject</a></td>");
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
        });

        $(document).ready(function () {
            getfilldtl();
        });

        function getfilldtl() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "sec_orderval_approval.aspx/getfilldtl",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders; ReloadTable();
                }
            });
        }
        $(document).on('click', '[id*=btnApp]', function () {
            var mRow = $(this).closest('tr');
            if (confirm('Are you sure you want to Approval ?')) {
                date = $(mRow).find('input[name=hlid]').val();
                sfCode = $(mRow).find('input[name=hsf]').val();
                $.ajax({
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    url: 'sec_orderval_approval.aspx/Approvaldata',
                    type: "POST",
                    data: "{'SF_Code':'" + sfCode + "', 'dates':'" + date + "'}",
                    dataType: "json",
                    success: function (data) {
                        console.log(data.d);
                        getfilldtl();
                    },
                    error: function (erorr) {
                        console.log(erorr);
                    }
                });
            }
            else {
            }
        });
        $(document).on('click', '[id*=btnRej]', function () {
            var mRow = $(this).closest('tr');
            if (confirm('Are you sure you want to Reject ?')) {
            
                date = $(mRow).find('input[name=hlid]').val();
                sfCode = $(mRow).find('input[name=hsf]').val();
                $.ajax({
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    url: 'sec_orderval_approval.aspx/RejectData',
                    type: "POST",
                    data: "{'SF_Code':'" + sfCode + "', 'dates':'" + date + "'}",
                    dataType: "json",
                    success: function (data) {
                        console.log(data.d);
                        getfilldtl();
                    },
                    error: function (erorr) {
                        console.log(erorr);
                    }
                });
            }
            else {
            }


        });
      
    </script>
</asp:Content>
