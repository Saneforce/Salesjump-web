<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Primary_Payment_Detail_Report.aspx.cs" Inherits="MIS_Reports_Primary_Payment_Detail_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

        var scheme = []; var GetProd = []; var Allorder = []; var state = ''; var DisCode = ''; var sub_div = ''; var slno = ''; var HeadsData = []; var TableData = [];
        var Payment_Details = []; var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "OrderID,Amount,Stockist_Name,PaymentMode,Status,PaymentDate,PaymentTypeName,Verified_by,Verify_Date,Dispatch_Date"; var AllOrders = [];
        var selected_dis_code = '';
        var Div_Code = '';


        $(document).ready(function () {

            $(".data-table-basic_length").on("change",
                function () {
                    pgNo = 1;
                    PgRecords = $(this).val();
                    //ReloadTable();
                    ReloadTable(Orders);
                }
            );

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Primary_Payment_Detail_Report.aspx/DivName",
                dataType: "json",
                success: function (data) {
                    var divName = JSON.parse(data.d) || [];
                    if (divName.length > 0) {
                        $.each(divName, function () {
                            $('#ddlDiv').append($("<option></option>").val(this['subdivision_code']).html(this['subdivision_name'])).trigger('chosen:updated').css("width", "100%");;;
                        });
                    }
                },
                error: function (result) {
                }
            });

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Primary_Payment_Detail_Report.aspx/GetState",
                dataType: "json",
                success: function (data) {
                    var divName = JSON.parse(data.d) || [];
                    if (divName.length > 0) {
                        $.each(divName, function () {
                            $('#ddlState').append($("<option></option>").val(this['state_code']).html(this['statename'])).trigger('chosen:updated').css("width", "100%");;;
                        });
                    }
                },
                error: function (result) {
                }
            });


            $('#ddlState').change(function () {
                state = $(this).val();
                $.ajax({
                    type: "Post",
                    contentType: "application/json; charset=utf-8",
                    url: "Primary_Payment_Detail_Report.aspx/getDistributor",
                    data: "{'StateCode':'" + state + "','sub_div':'" + sub_div + "'}",
                    dataType: "json",
                    async: false,
                    success: function (data) {
                        var disName = JSON.parse(data.d) || [];
                        if (disName.length > 0) {
                            $('#ddlDis').html('');
                            $('#ddlDis').append($("<option></option>").val(0).html("---Select---")).trigger('chosen:updated').css("width", "100%");;;
                            $.each(disName, function () {
                                $('#ddlDis').append($("<option></option>").val(this['Stockist_Code']).html(this['Stockist_Name'])).trigger('chosen:updated').css("width", "100%");;;
                            });
                        }
                        else {
                            $('#ddlDis').html('');
                            $('#ddlDis').append($("<option></option>").val(0).html("---Select---")).trigger('chosen:updated').css("width", "100%");;;
                        }
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            });

            $('#ddlDiv').change(function () {                
				sub_div = $(this).val();
                $('#ddlState').val(0);
                $('#ddlDis').html('');
                $('#ddlDis').append($("<option></option>").val(0).html("---Select---")).trigger('chosen:updated').css("width", "100%");;;
            });

            $('#btnView').click(function () {
                var selected_dis_name = $('#ddlDis option:selected').text();
                selected_dis_code = $('#ddlDis option:selected').val();
                var FromDate = $('#txtFrom_Date').val();
                var ToDate = $('#txtTo_Date').val();

                var div_code = $('#ddlDiv :selected').val();
                var State_Code = $('#ddlState :selected').val();

                if (div_code == "0") {
                    alert('select Division');
                    return false;
                }

                if (State_Code == "0") {
                    alert('select State');
                    return false;
                }

                if (selected_dis_code == "0") {
                    alert('select distributor');
                    return false;
                }

                if (FromDate == "") {
                    alert('select FromDate');
                    return false;
                }

                if (ToDate == "") {
                    alert('select ToDate');
                    return false;
                }

                $.ajax({
                    type: "Post",
                    contentType: "application/json; charset=utf-8",
                    url: "Primary_Payment_Detail_Report.aspx/Get_Primary_Payment_Details",
                    //data: "{'stockist_code':'" + selected_dis_code + "'}",
                    data: "{'stockist_code':'" + selected_dis_code + "','FromDate':'" + FromDate + "','ToDate':'" + ToDate + "'}",
                    dataType: "json",
                    async: false,
                    success: function (data) {
                        AllOrders = JSON.parse(data.d) || [];
                        Orders = AllOrders; ReloadTable(Orders);

                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });

                if (AllOrders.length > 0) {
                    var lookup = {};
                    var items = AllOrders;
                    var result = [];

                    for (var item, i = 0; item = items[i++];) {
                        var name = item.PaymentMode;
						
						if(name != ""){
                        if (!(name in lookup)) {
                            lookup[name] = 1;
                            result.push(name);
                        }
						}
                    }

                    $('#ddlPayment').html('');
                    $('#ddlPayment').append($("<option></option>").val(0).html("---Select---")).trigger('chosen:updated').css("width", "200px");;;
                    //$.each(result, function () {                        
                    //    $('#ddlPayment').append($("<option></option>").val(result[0]).html(result[0])).trigger('chosen:updated').css("width", "200px");;;
                    //});

                    for (var i = 0; i < result.length; i++) {
                        $('#ddlPayment').append($("<option></option>").val(result[i]).html(result[i])).trigger('chosen:updated').css("width", "200px");;;
                    }
                }

            });

            $(document).on("change", "#ddlPayment", function () {

                var selPaymentMode = $('#ddlPayment option:selected').val();
                var selected_txt = $('#tSearchOrd').val();
                var filter_unit = [];

                //filter_unit = AllOrders.filter(function (w) {
                //    return (selPaymentMode == w.PaymentMode);
                //});

                //Orders = filter_unit;
                ////filter_unit = Orders;

                //ReloadTable(Orders);

                search_function(selPaymentMode, selected_txt)


            });

            function ReloadTable(Orders) {
                var tota = 0;
                var AmountTotal = 0;
                $("#OrderList TBODY").html("");
                st = PgRecords * (pgNo - 1); slno = 0;
                for ($i = st; $i < st + PgRecords; $i++) {
                    if ($i < Orders.length) {
                        tr = $("<tr></tr>");
                        slno = $i + 1;
						$(tr).html("<td>" + slno + "</td><td>" + Orders[$i].OrderID + "</td><td>" + Orders[$i].Stockist_Name + "</td><td>" + Orders[$i].PaymentDate + "</td><td>" + Orders[$i].PaymentMode + "</td><td>" + Orders[$i].PaymentTypeName + "</td><td style='text-align:right;'>" + Orders[$i].Status + "</td><td style='text-align:right;'>" + Orders[$i].Verified_by + "</td><td style='text-align:right;'>" + Orders[$i].Verify_Date + "</td><td style='text-align:right;'>" + Orders[$i].Dispatch_Date + "</td><td style='text-align:right;'>" + Orders[$i].Amount.toFixed(2) + "</td>");
                        AmountTotal = parseFloat(Orders[$i].Amount || 0) + parseFloat(AmountTotal);
                        $("#OrderList TBODY").append(tr);
                    }
                }
				$("#OrderList TFOOT").html("<tr><td colspan='10' style='font-weight: bold;'>Total</td><td style='text-align: right;font-weight: bold;'>" + AmountTotal.toFixed(2) + "</td></tr>");
                $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")
                loadPgNos();
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
                spg = '<li class="paginate_button previous" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">First</a></li>';
                for (il = selpg - 1; il < selpg + 7; il++) {
                    if (il < TotalPg)
                        spg += '<li class="paginate_button' + ((pgNo == (il + 1)) ? " active" : "") + '"><a href="#" aria-controls="example2" data-dt-idx="' + (il + 1) + '" tabindex="0">' + (il + 1) + '</a></li>';
                }
                spg += '<li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="' + TotalPg + '" tabindex="0">Last</a></li>';
                $(".pagination").html(spg);

                $(".paginate_button > a").on("click", function () {
                    pgNo = parseInt($(this).attr("data-dt-idx")); ReloadTable(Orders);
                }
                );
            }

            $("#tSearchOrd").on("keyup", function () {

                var selected_filter = $('#ddlPayment :selected').val();
                var selected_txt = $(this).val();
                search_function(selected_filter, selected_txt)
            })

            function search_function(filter_txt, search_txt) {

                if (search_txt != "") {
                    shText = search_txt.toLowerCase();
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
                else {
                    Orders = AllOrders
                }
				
				if(filter_txt!="0")
				{
                Orders = Orders.filter(function (e) {
                    return (e.PaymentMode == filter_txt);
                });
				}
                ReloadTable(Orders);
            }
        });

    </script>


    <form id="form1" runat="server">

        <div class="container" style="width: 100%">

            <div class="row">
                <label id="Label1" class="col-md-2  col-md-offset-3  control-label">
                    Division</label>
                <div class="col-md-3 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <select name="ddlDiv" id="ddlDiv" class="form-control">
                            <option selected="selected" value="0">--Select--</option>
                        </select>
                    </div>
                </div>
            </div>

            <div class="row">
                <label id="Label2" class="col-md-2  col-md-offset-3  control-label">
                    State Name</label>
                <div class="col-md-3 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <select name="ddlState" id="ddlState" class="form-control" style="min-width: 100px;">
                            <option selected="selected" value="0">--Select--</option>
                        </select>
                    </div>
                </div>
            </div>

            <div class="row">
                <label id="Label3" class="col-md-2  col-md-offset-3  control-label">
                    Distributor Name</label>
                <div class="col-md-4 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <select name="ddlDis" id="ddlDis" class="form-control" style="min-width: 100px;">
                            <option selected="selected" value="0">--Select--</option>
                        </select>
                    </div>
                </div>
            </div>


            <div class="row">
                <label id="Label4" class="col-md-2  col-md-offset-3  control-label">
                    From Date</label>
                <div class="col-md-3 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <input type="date" name="From_Date" class="form-control" id="txtFrom_Date" placeholder="Date" />
                    </div>
                </div>
            </div>


            <div class="row">
                <label id="Label5" class="col-md-2  col-md-offset-3  control-label">
                    To Date</label>
                <div class="col-md-3 inputGroupContainer">
                    <div class="input-group">
                       <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <input type="date" name="To_Date" class="form-control" id="txtTo_Date" placeholder="Date" />
                    </div>
                </div>
            </div>

            <br />
            <center>
                 <button type="button" class="btn btn-primary btnsaveClass" id="btnView">View</button> 
            </center>


        </div>



        <div class="card">
            <div class="card-body table-responsive">
                <div style="white-space: nowrap">
                    Search&nbsp;&nbsp;<input type="text" id="tSearchOrd" autocomplete="off" style="width: 250px;" />

                    Filter By
                    <select style="width: 200px; margin-left: 13px;" id="ddlPayment">
                        <option value="0">---Select---</option>
                    </select>
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
                <table class="table table-hover" id="OrderList">
                    <thead class="text-warning">
                        <tr>
                            <th>Sl NO</th>
                            <th>Order ID</th>
                            <th>Stockist Name</th>
                            <th>Payment Date</th>
                            <th>Payment Mode</th>
                            <th>Payment Type</th>
                            <th style="text-align: right">Status</th>
                            <th style="text-align: right">Verified by</th>
                            <th style="text-align: right">Verify Date</th>
                            <th style="text-align: right">Dispatch Date</th>
							<th style="text-align: right">Amount</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                    <tfoot></tfoot>
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

        <br />

    </form>
</asp:Content>

