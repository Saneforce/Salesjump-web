<%@ Page Title="Route List" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master_DIS.master" CodeFile="Territory.aspx.cs" Inherits="MasterFiles_MR_Territory" EnableEventValidation="false" %>

<asp:Content ID="Content1" class=".content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-lg-12 sub-header">Route List <span style="float: right;"><a href="Territory_Detail.aspx" class="btn btn-primary btn-update">Add New Route</a></span></div>
    </div>

    <div class="card">
        <div class="card-body table-responsive">
            <div style="white-space: nowrap">
                Search&nbsp;&nbsp;<input type="text" autocomplete="off" id="tSearchOrd" style="width: 250px;" />
                <label style="float: right">Show
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
                        <th style="text-align: left">Sl.No</th>
                        <th style="text-align: left">Code</th>
                        <th style="text-align: left">Name</th>
                        <th style="text-align: right">Target</th>
                        <th style="text-align: right">Customer Count</th>
                        <th style="text-align: right">Edit</th>
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
                            <li class="paginate_button previous" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">Previous</a></li>
                            <li class="paginate_button active"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">1</a></li>
                            <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="7" tabindex="0">Next</a></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
    <script language="javascript" type="text/javascript">
        function popup(routecode,TerName) {
            //var url = "../MIS Reports/DMSRetailersDetailsSTwise.aspx?Route=" + routecode
            //window.open(url, '_self');
			
			var url = "../MIS Reports/DMSRetailersDetailsSTwise.aspx?Route=" + routecode+"&Territory_Name="+TerName+""
            window.open(url, '_self');
			
        }
        var AllOrders = [];
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Route_Code,Territory_Name,Target,ListedDR_Count,";
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
            spg = '<li class="paginate_button previous" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">First</a></li>';
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
            for ($i = st; $i < st + PgRecords; $i++) {
                if ($i < Orders.length) {
                    tr = $("<tr></tr>"); Trgt = parseFloat(Orders[$i].Target); if (isNaN(Trgt)) Trgt = 0;
                    //$(tr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].Route_Code + "</td><td>" + Orders[$i].Territory_Name + "</td><td align=\"right\">" + Trgt.toFixed(2) + "</td><td style='text-align:right;cursor:pointer'; onClick='popup(\"" + Orders[$i].Territory_Code + "\")'><a>" + Orders[$i].ListedDR_Count + "</a></td><td style='text-align:right'; id=" + Orders[$i].Territory_Code + " class='sfedit'><a href='#'>Edit</a></td>");
					$(tr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].Route_Code + "</td><td>" + Orders[$i].Territory_Name + "</td><td align=\"right\">" + Trgt.toFixed(2) + "</td><td style='text-align:right;cursor:pointer'; onClick='popup(\"" + Orders[$i].Territory_Code + "\",\""+Orders[$i].Territory_Name+"\")'><a>" + Orders[$i].ListedDR_Count + "</a></td><td style='text-align:right'; id=" + Orders[$i].Territory_Code + " class='sfedit'><a href='#'>Edit</a></td>");
                    $("#OrderList TBODY").append(tr);
                }
            }
            $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")
            loadPgNos();
        }

        $("#tSearchOrd").on("keyup", function () {
            if ($(this).val() != "") {
                shText = $(this).val();
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
				 pgNo = 1;
            ReloadTable();
        })
        function loadData() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                //   async: false,
                url: "Territory.aspx/GetRoutes",
                data: "{'stk':'<%=Session["Sf_Code"]%>'}",
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
        $(document).ready(function () {
            loadData();

            var sfcode = (<%=Session["Sf_Code"]%>);
        });

        $(document).on('click', '.sfedit', function () {
            x = this.id;
            window.location.href = "Territory_Detail.aspx?Route_Code=" + x;
        });


    </script>
</asp:Content>
