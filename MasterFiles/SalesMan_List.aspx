<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="SalesMan_List.aspx.cs" Inherits="MasterFiles_SalesMan_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="frm1" runat="server">
        <%--<div class="row">            
                <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                        style="height:40px;width:40px;border-width:0px;position: absolute;right: 15px;top: 55px;" OnClick="ExportToExcel" />
        </div>--%>
        <div class="row">
            <div class="col-lg-12 sub-header">SalesMan<span style="float: right"><a href="#" class="btn btn-primary btn-update" id="newsf">Add New</a></span></div>
        </div>

        <div class="card">
            <div class="card-body table-responsive">
                <div style="white-space: nowrap">
                    Search&nbsp;&nbsp;<input type="text" autocomplete="off" id="tSearchOrd" style="width: 250px;" />
                    <label style="white-space: nowrap; margin-left: 57px; display: none;">Filter By&nbsp;&nbsp;<select id="txtfilter" name="ddfilter" style="width: 250px; display: none;"></select></label>
                    <label style="float: right">Shows
                        <select class="data-table-basic_length" aria-controls="data-table-basic">
                            <option value="10">10</option>
                            <option value="25">25</option>
                            <option value="50">50</option>
                            <option value="100">100</option>
                        </select>
                        entries</label>
                    <div style="float: right">
                        <ul class="segment">
                            <li data-va='All'>ALL</li>
                            <li data-va='0' class="active">Active</li>
                        </ul>
                    </div>
                </div>
                <table class="table table-hover" id="OrderList">
                    <thead class="text-warning">
                        <tr>
                            <th style="text-align: left">Sl NO.</th>
                            <th style="text-align: left">DSM Name</th>
                            <th style="text-align: left">Designation</th>
                            <th style="text-align: left">Distributor</th>
                            <th style="text-align: left">Sales Type</th>
                            <th style="text-align: left">Username</th>
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
                                <li class="paginate_button previous" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">Previous</a></li>
                                <li class="paginate_button active"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">1</a></li>
                                <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="2" tabindex="0">Next</a></li>
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

     //   $(document).ready(function () {

            var AllOrders = [];
            var divcode; filtrkey = '0';
            optStatus = "<li><a href='#' v='0'>Active</a><a href='#' v='1'>Vacant / Block</a><a href='#' v='2'>Deactivate</a></li>"
            var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "DSM_code,DSM_name,Desig_type,Distributor_Name,Salestype,UserName,Status";
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
                if (filtrkey != "All") {
                    Orders = Orders.filter(function (a) {
                        return a.DSM_Active_Flag == filtrkey;
                    })
                }
                st = PgRecords * (pgNo - 1); slno = 0;
                for ($i = st; $i < st + PgRecords; $i++) {
                    //var filtered = Orders.filter(function (x) {
                    //    return x.Sf_Code != 'admin';
                    //})
                    if ($i < Orders.length) {
                        tr = $("<tr></tr>");
                        //var hq = filtered[$i].Sf_Name.split('-');
                        slno = $i + 1;
                        $(tr).html('<td>' + slno + '</td><td>' + Orders[$i].DSM_name + '</td><td>' + Orders[$i].Desig_type + '</td><td>' + Orders[$i].Distributor_Name +
                            '</td><td>' + Orders[$i].Salestype + '</td><td>' + Orders[$i].UserName + '</td><td id=' + Orders[$i].DSM_code +
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
                        sf = Orders[$indx].DSM_code;
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            url: "SalesMan_List.aspx/SetNewStatus",
                            data: "{'SF':'" + sf + "','stus':'" + stus + "'}",
                            dataType: "json",
                            success: function (data) {
                                Orders[$indx].DSM_Active_Flag = stus;
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
                pgNo = 1;
                ReloadTable();
            })
            function loadData(divcode, sf) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SalesMan_List.aspx/getSalesMan",
                    data: "{'divcode':'" + divcode + "','SF':'" + sf + "'}",
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
      //  });
        $(document).ready(function () {
            var sf;

            if (<%=Session["sf_type"]%>== 4 || <%=Session["sf_type"]%>== 2 || <%=Session["sf_type"]%>== 5) {

                sf ='<%=Session["Sf_Code"]%>';
                divcode ='<%=Session["div_code"]%>,';
                var divcode1 = divcode.split(',');
                divcode = divcode1[0];
            }
            else {
                sf = 'admin';
                divcode = Number(<%=Session["div_code"]%>);
            }
            loadData(divcode, sf);
            $(document).on('click', '#newsf', function () {
                window.location.href = "Sales_Man_Creation.aspx";
            });

            $(document).on('click', '.sfedit', function () {
                x = this.id;
                window.location.href = "Sales_Man_Creation.aspx?sfcode=" + x;
            });

            //$('select[name="ddfilter"]').change(function () {
            //    var r = $(this).val();
            //    loadData(r);
            //});
        });

    </script>
</asp:Content>

