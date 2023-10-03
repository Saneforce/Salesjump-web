<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Master.master" CodeFile="New_TerritoryList.aspx.cs" Inherits="MasterFiles_New_TerritoryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form runat="server">
        <div class="row" style="margin-bottom: 1rem;">
            <div class="row" style="margin-bottom: 1rem;">
                <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                    Style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 15px; top: 43px;" OnClick="ExportToExcel" />
            </div>
           <div class="col-lg-12 sub-header">Territory  Master<span style="float: right"><a href="TerritoryCreation.aspx" class="btn btn-primary btn-update" id="newsf">Add Territory </a></span></div>
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
                        entries</label><div style="float: right;padding-top: 3px;">
                    <ul class="segment">
                        <li data-va='All'>ALL</li>
                        <li data-va='0' class="active">Active</li>
                    </ul>
                </div>
                </div>
                <table class="table table-hover" id="OrderList" style="font-size: 12px">
                    <thead class="text-warning">
                        <tr style="background-color: #37a4c6; color: #fff; white-space: nowrap;">
                            <th style="text-align: left; color: #fff">Sl.No</th>
                            <th style="text-align: left; color: #fff;">Territory Code</th>
                            <th style="text-align: left; color: #fff">Territory Name</th>
                            <th style="text-align: left; color: #fff">Zone</th>
                            <th style="text-align: left; color: #fff">Route Count</th>
                            <th style="text-align: left; color: #fff">Edit</th>
                            <th style="text-align: left; color: #fff">Deactivate</th>
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
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Territory_sname,Territory_name,Zone,";
        var routes = [];
        var filtrkey = '0';
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
            if (filtrkey != "All") {
                Orders = Orders.filter(function (a) {
                    return a.Territory_Active_Flag == filtrkey;
                })
            }
            st = PgRecords * (pgNo - 1);
            for ($i = st; $i < st + Number(PgRecords); $i++) {
                if ($i < Orders.length) {
                  
                    tr = $("<tr rname='" + Orders[$i].Territory_name + "' rocode='" + Orders[$i].Territory_code + "'></tr>");
                    $(tr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].Territory_sname + "</td><td>" + Orders[$i].Territory_name + "</td><td>" + Orders[$i].Zone + "</td><td class='rocount'><a href='#'>" + Orders[$i].FO_Coun + "</a></td><td class='roedit'><a href='#'>Edit</a></td><td class='rodeact'><a href='#'>" + Orders[$i].Status + "</a></td>");
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

        $(".segment>li").on("click", function () {
            $(".segment>li").removeClass('active');
            $(this).addClass('active');
            filtrkey = $(this).attr('data-va');
            Orders = AllOrders;
            $("#tSearchOrd").val('');
            ReloadTable();
        });
       
       
        function fillterritory() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "New_TerritoryList.aspx/getterritory",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders; ReloadTable();
                }
            });
        }
        
        $(document).ready(function () {
           
            fillterritory();
          
          
            $(document).on('click', '.rocount', function () {
                $('#RouteModal').modal('toggle');
                var route_C = $(this).closest('tr').attr('rocode');
                var route_N = $(this).closest('tr').attr('rname');
                $('#RouteModalLabel').text("Routes for " + route_N);
                fillRoutes(route_C);
            });
            $(document).on('click', '.roedit', function () {
                var route_C = parseInt($(this).closest('tr').attr('rocode'));
                window.location.href = "TerritoryCreation.aspx?Subdivision_Code=" + route_C + "";
            });
            $(document).on("click", ".rodeact", function () {
                var route_C = $(this).closest('tr').attr('rocode');
                let oindex = Orders.findIndex(x => x.Territory_code == route_C);
                let disstat = Orders[oindex]["Status"].toString();
                let flg = (parseInt(Orders[oindex]["Territory_Active_Flag"]) == 0) ? 1 : 0;
                var rocnt = isNaN(parseInt($(this).closest('tr').find('.rocount>a').html())) ? 0 : parseInt($(this).closest('tr').find('.rocount>a').html());
              
                if ((rocnt < 1 && flg == 1) || (flg == 0)) {
                    if (confirm("Do you want " + disstat + " the Distributor ?")) {
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            url: "New_TerritoryList.aspx/deactivateterritory",
                            data: "{'tercode':'" + route_C + "','stat':'" + flg + "'}",
                            dataType: "json",
                            success: function (data) {
                                if (data.d == 'Success') {
                                    Orders[oindex]["Territory_Active_Flag"] = flg;
                                    Orders[oindex]["Status"] = (flg == 0) ? "Deactivate" : "Activate";
                                    ReloadTable();
                                    alert('Status Changed Successfully...');
                                }
                                else {
                                    alert("Status Can't be Changed");
                                }
                            },
                            error: function (result) {
                            }
                        });
                    }
                }
                else {
                    alert("Status Can't be Changed");
                }
            });
        });
    </script>
</asp:Content>
