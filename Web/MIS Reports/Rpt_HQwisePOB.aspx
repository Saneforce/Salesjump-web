<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Rpt_HQwisePOB.aspx.cs" Inherits="MIS_Reports_Rpt_HQwisePOB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form runat="server">
        <div class="row" style="margin-bottom: 1rem;">
            <div class="row">
                <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                    Style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 15px; top: 43px;" OnClick="lnkDownload_Click" />
            </div>
            <div class="col-lg-12 sub-header">
                HQ Wise POB Report 
				<a href="POB_Report.aspx" class="btn" style="float: right; background-color: #1a73e8; color: white;">Back</a>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-6">
                <label style="font-weight: 100;">State Name</label><label> : <%=stname%></label>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-6">
                <label style="font-weight: 100;">Month</label><label> : <%=mnthname%></label>
            </div>
            <div class="col-sm-6">
                <label style="font-weight: 100;">Year</label><label> : <%=Yr%></label>
            </div>
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
                            <th style="text-align: left; color: #fff">S.No</th>
                            <th style="text-align: left; color: #fff;">HQ</th>
                            <th style="text-align: left; color: #fff">Today POB</th>
                            <th style="text-align: left; color: #fff">Monthly POB</th>
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
    <script type="text/javascript">
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
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "HQ,Monthly_POB,Today_POB,";
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
            }
            );
        }
        function ReloadTable() {
            $("#OrderList TBODY").html("");
            st = PgRecords * (pgNo - 1);
            for ($i = st; $i < st + Number(PgRecords) ; $i++) {
                if ($i < Orders.length) {
                    tr = $("<tr></tr>");
                    $(tr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].HQ + "</td><td>" + Orders[$i].Today_POB + "</td><td>" + Orders[$i].Monthly_POB + "</td>");
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
        function loadData() {
            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "Rpt_HQwisePOB.aspx/pobdata",
                data: "{'div_code':'<%=Session["Division_Code"]%>'}",
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
        $(document).ready(function () {
            loadData();
        });
    </script>
</asp:Content>
