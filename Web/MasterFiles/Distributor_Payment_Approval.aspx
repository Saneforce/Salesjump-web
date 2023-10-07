<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Distributor_Payment_Approval.aspx.cs" Inherits="MasterFiles_Distributor_Payment_Approval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <form id="frm" runat="server">

        <div class="container">

            <div class="card">
                <div class="card-body table-responsive">
                    <div style="white-space: nowrap">
                        Search&nbsp;&nbsp;<input type="text" id="tSearchOrd" autocomplete="off" style="width: 250px;" />
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
                    <table id="Primary_pending_bill" class="table table-hover">
                        <thead>
                            <tr>
                                <th>Sl No</th>
                                <th>Order ID</th>
                                <th>Distributor Name</th>
                                <th>Payment Date</th>
                                <th>Payment Mode</th>
                                <th>Amount</th>
                                <th>Attachement</th>
                                <th>View</th>
                                <th style="text-align: right;">Status</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>

                    <div class="row">
                        <div class="col-sm-5">
                            <div class="dataTables_info" id="orders_info" role="status" aria-live="polite">Showing 0 to 0 of 0 entries</div>
                        </div>
                        <div class="col-sm-7">
                            <div class="dataTables_paginate paging_simple_numbers" id="example2_paginate">
                                <ul class="pagination">
                                    <li class="paginate_button previous" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">Previous</a></li>
                                    <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="7" tabindex="0">Next</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </form>


    <script type="text/javascript">


        var dis_Details = []; var Pending_Payment = []; var AllOrders = []; var Orders = [];
        pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "OrderID,Stockist_Name,PaymentMode,Amount,";
        optStatus = '<li><a href="#" v="1">Approval</a></li><li><a href="#" v="-2">Reject</a></li>'

        $(document).ready(function () {

            load_Orders(); get_dis();

        });

        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1;
        var yyyy = today.getFullYear();
        if (dd < 10) {
            dd = '0' + dd
        }
        if (mm < 10) {
            mm = '0' + mm
        }
        today = yyyy + '-' + mm + '-' + dd;
        $("#datepicker").val(today);

        $(".data-table-basic_length").on("change",
            function () {
                pgNo = 1;
                PgRecords = parseFloat($(this).val());
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

        $("#tSearchOrd").on("keyup", function () {

            var Search_text = $(this).val();

            if (Search_text != "") {
                shText = Search_text;
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

        function get_dis() {

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Distributor_Payment_Approval.aspx/get_distributor",
                dataType: "json",
                success: function (data) {
                    dis_Details = JSON.parse(data.d) || [];

                    $('#distributor_Details').empty();
                    $('#distributor_Details').append('<option selected="selected" value="0">Select Distributor</option>');
                    for (var g = 0; g < dis_Details.length; g++) {
                        $('#distributor_Details').append("<option value=" + dis_Details[g].Stockist_code + " >" + dis_Details[g].Stockist_Name + "</option>");
                    }
                },
                error: function (result) {
                }
            });
        }

        function load_Orders() {

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Distributor_Payment_Approval.aspx/Get_Pending_Payment_Approval",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders; ReloadTable();
                },
                error: function (result) {
                }
            });
        }

        function ReloadTable() {
            var tota = 0;
            $('#Primary_pending_bill tbody').html("");
            st = PgRecords * (pgNo - 1); slno = 0;
            for ($i = st; $i < st + PgRecords; $i++) {
                if ($i < Orders.length) {
                    tr = $("<tr></tr>");
                    slno = $i + 1;
                    $(tr).html("<td>" + slno + "</td><td class='ord'>" + Orders[$i].OrderID + "</td><td style='display:none;' class='stk_code'>" + Orders[$i].Stockist_Code + "</td><td>" + Orders[$i].Stockist_Name + "</td><td>" + Orders[$i].PaymentDate + "</td><td>" + Orders[$i].PaymentMode + "</td><td>" + parseFloat(Orders[$i].Amount).toFixed(2) + "</td><td><img src='../photos/PrimaryOrderPayment/" + Orders[$i].simg + "' alt='img' width='50' height='60' class='img_class'></td><td><a href='#' class='picc'>View</a></td><td><ul class='nav' style='margin:0px'><li class='dropdown'><a href='#' style='padding:0px' class='dropdown-toggle' data-toggle='dropdown'><span><span class='aState' data-val=" + Orders[$i].PaymentMode + ">Select</span><i class='caret' style='float:right;margin-top:8px;margin-right:0px'></i></span></a><ul class='dropdown-menu dropdown-custom dropdown-menu-right ddlStatus' style='right:0;left:auto;'>" + optStatus + "</ul></li></ul></td>");
                    $("#Primary_pending_bill TBODY").append(tr);
                }
            }
            $("#OrderList TFOOT").html("<tr><td colspan='6' style='font-weight: bold;'>Total</td><td style='text-align: right;font-weight: bold;'>" + tota.toFixed(2) + "</td></tr>");
            $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")

            $(".ddlStatus>li>a").on("click", function () {

                cStus = $(this).closest("td").find(".aState");
                stus = $(this).attr("v");
                cStusNm = $(this).text();
                if (confirm("Do you want change status to " + cStusNm + " ?")) {
                    sf = $(this).closest("tr").find('.stk_code').text();
                    order = $(this).closest("tr").find('.ord').text();
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Distributor_Payment_Approval.aspx/SetNewStatus",
                        data: "{'SF':'" + sf + "','stus':'" + stus + "','Order_Id':'" + order + "'}",
                        dataType: "json",
                        success: function (data) {
                            alert('Status Changed Successfully...');
                            load_Orders();
                        },
                        error: function (result) {
                        }
                    });
                }
            });
            loadPgNos();
        }

        dv = $('<div style="z-index: 10000000;position:fixed;left:50%;top:50%;width:70%;height:70%;transform: translate(-50%, -50%);border-radius: 15px;display:none" id="cphoto1"></div>');
        $(dv).html('<span style="position: absolute;padding: 5px;cursor: default;background: #dcd6d652;border-radius: 50%;width: 20px;height: 20px;line-height: 6px;text-align: center;border: solid 1px gray;top: 6px;right: 6px;" onclick="closew()">x</span><img style="width:100%;height:100%;border-radius: 15px;" id="photo1" />')
        $("body").append(dv);

        function closew() {
            $('#cphoto1').css("display", 'none');
        }

        $(document).on('click', '.picc', function () {
            var photo = $(this).closest('tr').find('.img_class').attr('src');
            $('#photo1').attr("src", $(this).closest('tr').find('.img_class').attr('src'));
            $('#cphoto1').css("display", 'block');
        });

    </script>

</asp:Content>

