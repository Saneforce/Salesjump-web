<%@ Page Title="Dashboard" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master_SS.master" CodeFile="Default5.aspx.cs" Inherits="Default5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />

    <style>
        th {
            white-space: nowrap;
        }
    </style>
    <form runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:HiddenField ID="hdndate" runat="server"></asp:HiddenField>
        <div class="row1" style="position: absolute; top: 50px; right: 20px;">
            <%--<input type="text" id="datepicker" class="form-control hasDatepicker" autocomplete="off" style="min-width: 110px !important; width: 110px; display: inline-block; border: none; background-color: transparent; color: #336277; font-family: Verdana; font-weight: bold; font-size: 14px;">--%>
            <%-- <input type="text" id="datepicker" class="form-control pd" autocomplete="off" name="datepicker" data-validation="required" style="min-width: 110px !important; width: 110px; display: inline-block; border: none; background-color: transparent; color: #336277; font-family: Verdana; font-weight: bold; font-size: 14px;" />
            <label class="caret" for="datepicker" style="position: relative; top: 3px;"></label>--%>
            <input type="date" id="datepic" style="min-width: 110px !important; width: 155px; display: inline-block; border: none; background-color: transparent; color: #336277; font-family: Verdana; font-weight: bold; font-size: 14px;" />
            <%--    <a href="#" ></a>--%>
        </div>
        <div class="row">
            <div class="col-md-3">
                <div class="sm-st clearfix retail">
                    <span class="sm-st-icon st-red"><i class="fa fa-user"></i></span>
                    <div class="sm-st-info">
                        <span>
                            <asp:Label ID="retailer" runat="server"></asp:Label></span> Customers
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="sm-st clearfix  DSMCnt">
                    <span class="sm-st-icon st-violet"><i class="fa fa-user"></i></span>
                    <div class="sm-st-info">
                        <span>
                            <asp:Label ID="DSMCnt" runat="server"></asp:Label></span> Sales Man
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="sm-st clearfix Pri_order_count">
                    <span class="sm-st-icon st-blue"><i class="glyphicon glyphicon-shopping-cart"></i></span>
                    <div class="sm-st-info">
                        <span>
                            <asp:Label ID="Pri_order_count" runat="server"></asp:Label></span>Purchase Orders
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="sm-st clearfix sec_order_count">
                    <span class="sm-st-icon st-green"><i class="fa fa-paperclip"></i></span>
                    <div class="sm-st-info">
                        <span>
                            <asp:Label ID="sec_order_count" runat="server"></asp:Label></span>Sale Orders
                    </div>
                </div>
            </div>
        </div>

        <div class="card" style="display:none;">
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
                <table class="table table-hover" id="Stock_table">
                    <thead class="text-warning">
                        <tr>
                            <th>Sl NO</th>
                            <th>Product Code</th>
                            <th>Product Name</th>
                            <th style="text-align: right;">Opening Stock</th>
                            <th style="text-align: right;">Purchase Order</th>
                            <th style="text-align: right;">Sales Order</th>
                            <th style="text-align: right;">Closing Stock</th>
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



    </form>
    <script language="javascript" type="text/javascript">

        var AllOrders = []; var sf_type = ''; var sfCode = ''; var All_stock_details = []; var txt = ''; var Access_data = [];
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Product_Detail_Code,Product_Detail_Name,";

        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1;
        var yyyy = today.getFullYear();
        if (dd < 10) { dd = '0' + dd }
        if (mm < 10) { mm = '0' + mm }
        today = yyyy + '-' + mm + '-' + dd;

        $("#datepic").val(today);

        $(document).ready(function () {

            sf_type = '<%=Session["sf_type"]%>';
            sfCode = '<%= Session["sf_code"] %>';

            var TDate = new Date();
            var TMonth = TDate.getMonth() + 1;
            var TDay = TDate.getDate();
            var TYear = TDate.getFullYear();
            var CDate = TYear + '-' + TMonth + '-' + TDay;
            var CDate1 = TDay + '-' + TMonth + '-' + TYear;

            var viewState = $('#<%=hdndate.ClientID %>').val();
            if (viewState != "") {
                $("#datepic").val(viewState);
                var st = viewState.split('-');
                TMonth = st[1];
                TDay = st[0];
                TYear = st[2];

                CDate = TYear + '-' + TMonth + '-' + TDay;
                CDate1 = TDay + '-' + TMonth + '-' + TYear;
            }
            else {
                $("#datepic").datepicker("setDate", new Date());
                viewState = TDay + '-' + TMonth + '-' + TYear;;
            }


            $(document).on('change', "#datepic", function (e) {
                var txt = $('#datepic').val();
                $('#<%=hdndate.ClientID %>').val(txt);
                __doPostBack(txt, e);
            });

            $(".data-table-basic_length").on("change",
                function () {
                    pgNo = 1;
                    PgRecords = parseFloat($(this).val());
                    ReloadTable();
                }
            );

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                data: "{'Date':'" + $('#datepic').val() + "'}",
                url: "Default5.aspx/Get_Stock",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders; ReloadTable();
                },
                error: function (result) {
                }
            });

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Default5.aspx/Get_access_master",
                dataType: "json",
                success: function (data) {
                    window.localStorage.removeItem("Access_Details");
                    Access_data = JSON.parse(data.d) || [];
                    localStorage.setItem('Access_Details', data.d);
                },
                error: function (result) {
                }
            });

            function ReloadTable() {

                var total_opening = 0; var tot_purchase = 0; var tot_sales = 0; var tot_closing = 0;
                $("#Stock_table TBODY").html("");
                st = PgRecords * (pgNo - 1); slno = 0;
                for ($i = st; $i < st + PgRecords; $i++) {

                    if ($i < Orders.length) {

                        tr = $("<tr></tr>");
                        slno = $i + 1;
                        $(tr).html("<td>" + slno + "</td><td class='pro_code'>" + Orders[$i].Product_Detail_Code + "</td><td class='pro_name'>" + Orders[$i].Product_Detail_Name + "</td><td style='text-align: right;' class='op_stock'>" + Orders[$i].Opening_Stock + "</td><td style='text-align: right;' class='purchase_order'>" + Orders[$i].Purchase + "</td>><td style='text-align: right;' class='sales'>" + Orders[$i].Sales + "</td><td style='text-align: right;' class='Closing_stock'>" + Orders[$i].Closing + "</td>");
                        $("#Stock_table TBODY").append(tr);

                        total_opening = Orders[$i].Opening_Stock + total_opening;
                        tot_purchase = Orders[$i].Purchase + tot_purchase;
                        tot_sales = Orders[$i].Sales + tot_sales;
                        tot_closing = Orders[$i].Closing + tot_closing;
                    }
                }
                $("#Stock_table TFOOT").html("<td colspan='3' style='font-weight: bold;padding: 0px 0px 0px 8px;'>Total</td></td><td style='font-weight: bold;text-align: right;'>" + total_opening + "</td><td style='font-weight: bold;text-align: right;'>" + tot_purchase + "</td><td style='font-weight: bold;text-align: right;'>" + tot_sales + "</td><td style='font-weight: bold;text-align: right;'>" + tot_closing + "</td>");
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
                }
                );
            }

            $("#tSearchOrd").on("keyup", function () {
                pgNo = 1;
                var Search_text = $(this).val();
                if (Search_text != "") {
                    shText = Search_text;
                    Orders = AllOrders.filter(function (a) {
                        chk = false;
                        $.each(a, function (key, val) {
                            if (val != null && val.toString().toLowerCase().indexOf(shText.toString().toLowerCase()) > -1 && (',' + searchKeys).indexOf(',' + key + ',') > -1) {
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
        });

    </script>

    <script type="text/javascript">
        $(document).ready(function () {

            $(".retail").on("click", function () {
                window.open('Stockist/Retailer_List.aspx?', '_self');
            });

            $(".DSMCnt").on("click", function () {
                window.open('MasterFiles/SalesMan_List.aspx?', '_self');
            });

            $(".sec_order_count").on("click", function () {

                if (sf_type == "5") {
                    window.open('Stockist/Supplier_syn_Page.aspx?sf_code=' + sfCode + '&Date=' + today + '', '_self');
                }
                else {
                    localStorage.clear();
                    window.localStorage.removeItem("Date_Details");
                    window.open('Stockist/myOrders.aspx?', '_self');
                }
            });

            $(".Pri_order_count").on("click", function () {
                localStorage.clear();
                window.localStorage.removeItem("Date_Details");

                var TDate = new Date();
                var TMonth = TDate.getMonth() + 1;
                var TDay = TDate.getDate();
                var TYear = TDate.getFullYear();
                var CDate = TYear + '-' + TMonth + '-' + TDay;
                var CDate1 = TDay + '-' + TMonth + '-' + TYear;

                var tdy = $('#datepic').val();
                var dfdf = tdy.split('-');
                var ruigior = dfdf[2] + '-' + dfdf[1] + '-' + dfdf[0];

                namesArr = [];
                namesArr.push(ruigior + '-' + ruigior);
                namesArr.push(1);
                namesArr.push(10);
                namesArr.push("");
                namesArr.push("");
                namesArr.push("0");
                window.localStorage.setItem("Date_Details", JSON.stringify(namesArr));
                window.open('Stockist/Purchase_Order_List.aspx?', '_self');
            });
        });
    </script>

</asp:Content>
