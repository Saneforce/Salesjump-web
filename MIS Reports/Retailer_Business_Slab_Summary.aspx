<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Retailer_Business_Slab_Summary.aspx.cs" Inherits="MIS_Reports_Retailer_Business_Slab_Summary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<style>
        th{
            white-space:nowrap;
        }
        table#summarydets>thead>tr>th, table#summarydets>tbody>tr>td {
    font-size: 12px;
}
    </style>    
    <form runat="server" id="frm1">
        <asp:HiddenField ID="hFF" runat="server" />
        <div class="row">
        <div class="col-lg-12 sub-header">Retailer Business Slab Summary 
            </div>
        </div>
        <div class="card">
            <div class="card-body table-responsive">
                <div style="white-space: nowrap">
                    Search&nbsp;&nbsp;<input type="text" id="tSearchOrd" style="width: 250px;" />
                    <%--<label style="float: right">
                        Show
                        <select class="data-table-basic_length" aria-controls="data-table-basic">
                            <option value="10">10</option>
                            <option value="25">25</option>
                            <option value="50">50</option>
                            <option value="100">100</option>
                        </select>
                        entries</label>--%>
                </div>
                <table class="table table-hover" id="OrderList" style="font-size: 12px">
                    <thead class="text-warning">
                        <tr>
                            <th style="text-align: left">SlNo</th>
                            <th style="text-align: left">Employee Name</th>
                            <th style="text-align: left">Marked Retailers</th>
                            <th style="text-align: left">Unmarked Retailers</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
                <%--<div class="row" style="padding: 5px 0px">
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
                </div>--%>
            </div>
        </div>
        <div class="modal fade" id="summaryModal" style="z-index: 10000000;background-color: transparent; overflow-y: scroll;" tabindex="0" aria-hidden="true">
            <div class="modal-dialog" role="document" style="width: 68% !important">
                <div class="modal-content">
                    <div class="modal-header">
                            <h5 class="modal-title" style="font-size: 13px;"  id="summaryModalLabel"></h5>
                            <asp:imagebutton id="ImageButton2" runat="server" align="right" imageurl="~/img/Excel-icon.png"
                                style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 39px; top: 8px;" onclick="ImageButton2_Click" />
                            <button type="button" class="close" style="margin-top: -20px;" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    </div>
                    <div class="modal-body">
                        <div class="row" style="padding:0px;">
                            <div style="padding: 0px 15px">
                                <div style="white-space: nowrap">
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
                                <table id="summarydets" class="table table-hover" style="width: 100%;">
                                <thead>
                                        <tr>
                                            <th style="text-align: left">SlNO</th>
                                            <th style="text-align: left">Retailer Name</th>
                                            <th style="text-align: left">Retailer Address</th>
                                            <th style="text-align: left">Route</th>
                                            <th style="text-align: left">Retailer Mobile</th>
                                            <th style="text-align: left">Retailer Business Slab Name</th>
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
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
        </form>
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
    <script language="javascript" type="text/javascript">
        var Orders = [];
        var Arrsum = [];
        var AllOrders2 = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Sf_Code,Sf_Name,";
        $(".data-table-basic_length").on("change",
        function () {
            pgNo = 1;
            PgRecords = $(this).val();
            fillsummary();
        }
        );

        function loadData2() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Retailer_Business_Slab_Summary.aspx/GetDetails_Summary",
                data: "{'SF':'<%=Session["Sf_Code"]%>','Div':'<%=Session["Division_Code"]%>'}",
                dataType: "json",
                success: function (data) {
                    AllOrders2 = JSON.parse(data.d) || [];
                    Orders = AllOrders2; ReloadTable2();
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
        }        
        function loadPgNos() {
            prepg = parseInt($(".paginate_button.previous>a").attr("data-dt-idx"));
            Nxtpg = parseInt($(".paginate_button.next>a").attr("data-dt-idx"));
            $(".pagination").html("");
            TotalPg = parseInt(parseInt(Orders.length / PgRecords) + ((Orders.length % PgRecords) ? 1 : 0)); selpg = 1;
            if (isNaN(prepg)) prepg = 0;
            if (isNaN(Nxtpg)) Nxtpg = 2;
          //  if ((prepg + 1) == pgNo && pgNo > 1) selpg = (parseInt(pgNo) - 1);
            selpg =(pgNo > 7)? (parseInt(pgNo) + 1) - 7:1;
            if ((Nxtpg) == pgNo){
                 selpg = (parseInt(TotalPg)) - 7;
                 selpg =(selpg>1)? selpg:1;
            }
            spg = '<li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">First</a></li>';
            for (il = selpg - 1; il < selpg + 7; il++) {
                if (il < TotalPg)
                    spg += '<li class="paginate_button' + ((pgNo == (il + 1)) ? " active" : "") + '"><a href="#" aria-controls="example2" data-dt-idx="' + (il + 1) + '" tabindex="0">' + (il + 1) + '</a></li>';
            }
            spg += '<li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="' + TotalPg + '" tabindex="0">Last</a></li>';
            $(".pagination").html(spg);

            $(".paginate_button > a").on("click", function () {
                pgNo = parseInt($(this).attr("data-dt-idx")); fillsummary();
                /* $(".paginate_button").removeClass("active");
                 $(this).closest(".paginate_button").addClass("active");*/
            }
           );
        }
        function ReloadTable2() {
            $("#OrderList TBODY").html("");
            var mlist = 0;
            var nmlist = 0;
            st = PgRecords * (pgNo - 1);
            for ($i = st; $i < Orders.length; $i++) {
                mlist += Orders[$i].Marked;
                nmlist += Orders[$i].NotMarked;
                if ($i < Orders.length) {
                    tr = $("<tr></tr>");
                    $(tr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].Sf_Name + "</td><td class='tdclick' id='" + Orders[$i].Sf_Code + ":M'><a href='#'>" + Orders[$i].Marked + "</a></td><td class='tdclick' id='" + Orders[$i].Sf_Code + ":NM'><a href='#'>" + Orders[$i].NotMarked + "</a></td>"); //<td>" + Orders[$i].Rmks+"</td>
                    $("#OrderList TBODY").append(tr);
                }
            }
            tr = $("<tr></tr>");
            $(tr).html("<td></td><td>Total</td><td class='tdclick' id=':AllM'><a href='#'>" + mlist + "</a></td><td class='tdclick' id=':AllNM'><a href='#'>" + nmlist + "</a></td>"); //<td>" + Orders[$i].Rmks+"</td>
            $("#OrderList TBODY").append(tr);
           // $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")
            //loadPgNos();
        }		
        $("#tSearchOrd").on("keyup", function () {
            if ($(this).val() != "") {
                shText = $(this).val().toLowerCase();
                Orders = AllOrders2.filter(function (a) {
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
                Orders = AllOrders2
            ReloadTable2();
        });
        function fillsummary() {
            $('#summarydets TBODY').html("");
            var slno = 0;
            st = PgRecords * (pgNo - 1);
            for ($i = st; $i < st + PgRecords; $i++) {
                slno += 1;
                if (Arrsum.length > $i) {
                    tr = $("<tr></tr>");
                    $(tr).html("<td>" + slno + "</td><td>" + Arrsum[$i].Retailer_Name + "</td><td>" + Arrsum[$i].Retailer_Address + "</td><td>" + Arrsum[$i].Territory_Name + "</td><td>" + Arrsum[$i].Mobile + "</td><td>" + Arrsum[$i].SlabName + "</td>");
                    $("#summarydets TBODY").append(tr);
                }
            }
            $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Arrsum.length) ? (st + PgRecords) : Arrsum.length) + " of " + Arrsum.length + " entries")
            loadPgNos();
        }
        $(document).ready(function () {
            sf = '<%=Session["Sf_Code"]%>';
            loadData2();

            $(document).on('click', '.tdclick', function () {
                tdid = this.id;
                $('#<%=hFF.ClientID%>').val(tdid);
                var arrtd = tdid.split(':');
                var FFNF = '';
                FFNF = Orders.filter(function (a) {
                    return a.Sf_Code = arrtd[0];
                });
                if (FFNF.length > 0) {
                    FFNF = FFNF[0].Sf_Name;
                }
                else {
                    FFNF='Total'
                }
                $("#summaryModal .modal-title").html("Retailer Details -  " + FFNF + "");

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Retailer_Business_Slab_Summary.aspx/GetRetailerDetails",
                    data: "{'SF':'" + arrtd[0] + "','Div':'<%=Session["Division_Code"]%>','typ':'" + arrtd[1] + "'}",
                    dataType: "json",
                    success: function (data) {
                        Arrsum = JSON.parse(data.d) || [];
                    },
                    error: function (result) {
                        //alert(JSON.stringify(result));
                    }
                });
                $('#summaryModal').modal('toggle');
                fillsummary();
            });
        });

    </script>
</asp:Content>

