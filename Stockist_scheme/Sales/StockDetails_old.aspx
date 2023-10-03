<%@ Page Title="" Language="C#" MasterPageFile="~/Master_DIS.master" AutoEventWireup="true" CodeFile="StockDetails.aspx.cs" Inherits="Stockist_Sales_StockDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <html>
    <head><title></title></head>
    <style>
        .chosen-container {
            width: 320px !important;
        }

        input[type='text'], select {
            border: 1.5px solid #19a4c6a3 !important;
            background: aliceblue;
        }

        .table > thead > tr > th {
            vertical-align: bottom;
            border-bottom: 2px solid #19a4c6a3;
        }

        .txtblue {
            border: 1.5px solid #19a4c6a3 !important;
            background-color: aliceblue !important;
            /*border: 1px solid #19a4c6!important;*/
        }

        .chosen-container-single .chosen-single {
            border: 1.5px solid #19a4c6a3 !important;
            height: 30px;
            background: aliceblue !important;
        }

        .card {
            border: 1.5px solid #b2ebf9b5 !important;
        }

        .select2-container--default .select2-selection--single {
            border: 1.5px solid #19a4c6a3 !important;
            background: aliceblue !important;
        }

        .ms-options-wrap > button:focus, .ms-options-wrap > button {
            border: 1.5px solid #19a4c6a3 !important;
        }

        .stockClass {
            font-size: 12px;
            font-weight: bolder;
        }

        .btn-group > .btn:first-child {
            border: 1.5px solid #19a4c6a3 !important;
            background: aliceblue !important;
        }

        .modal {
            background-color: transparent !important;
        }

        .btn .category .active {
            background-color: #00bcd4 !important;
            color: white !important;
        }

        .stockClass {
            font-size: 12px;
            font-weight: bolder;
        }

        .fa-stack-overflow:before {
            content: "\f16c";
            margin-right: 6px;
            margin-left: 15px;
            color: #00bcd4;
        }

        .fa-tags:before {
            content: "\f02c";
            color: #ef1b1b;
            margin-right: 6px;
        }

        .fa-code:before {
            content: "\f121";
            margin-left: 17px;
            margin-right: 6px;
            color: #0075ff;
            font-size: 13px;
            font-weight: bolder;
        }

        tbody {
            border-bottom: 1.5px solid #6cc5db !important;
        }

        tfoot :last-child {
            border-top: 1.5px solid #6cc5db !important;
        }

        .headcard {
            font-size: 23px;
            font-weight: 500;
            padding-bottom: 10px;
            color: #19a4c6;
        }


        #backButton {
            color: black;
            border-radius: 4px;
            /* padding: 8px; */
            border: none;
            font-size: 13px;
            background-color: #2eacd1;
            color: white;
            position: absolute;
            right: 613px;
            top: 810px;
            /* right: 10px; */
            cursor: pointer;
        }



        .invisible {
            display: none;
        }
    </style>
    <script src="../../canvas/canvasjs.min.js"></script>
    <script src="../../canvas/canvasjs.stock.min.js"></script>
    <script src="../../canvas/jquery.canvasjs.min.js"></script>
    <body>
        <form id="form1" runat="server">
            <asp:HiddenField ID="hdndate" runat="server"></asp:HiddenField>
            <div class="row">
                <div class="col-md-offset-4 col-md-4 sub-header" style="text-align: center">
                    Stock Details
                </div>
                <div class="col-md-4 sub-header">
                    <span style="float: right"></span><span style="float: right; margin-right: 50px;">
                        <div class="row1">
                            <%--<input type="date" id="datepic" style="min-width: 110px !important; width: 155px; display: inline-block; border: none; background-color: transparent; color: #336277; font-family: Verdana; font-weight: bold; font-size: 14px;" />--%>
                            <label style="padding-left: 63px; font-weight: 100; font-size: 14px;">Financial Year</label>
                            <select name="year_select" class="form-control year_select">
                        </div>
                    </span>
                    <span style="float: right; margin-top: -5px;">
                        <div>
                            <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                                Style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 15px; top: 26px;" OnClick="ImageButton1_Click" />
                        </div>
                    </span>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4 col-md-offset-4" style="text-align: center">
                    <h5 id="date_details"></h5>
                </div>
            </div>
            <div class="row" id="distselect" style="display: none;">
                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                    <div class="form-group">
                        <label class="control-label" for="focusedInput">
                            Distibutor Name</label><span style="color: red">*</span>
                        <select class="form-control" id="ddl_dist" style="width: 100%;">
                            <option value="0">SelectDistributor</option>
                        </select>
                    </div>
                </div>
            </div>
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
                    <table class="table table-hover" id="Stock_table">
                        <thead class="text-warning">
                            <tr>
                                <th>Sl NO</th>
                                <th>Product Code</th>
                                <th>Product Name</th>
                                <th>Opening </th>
                                <th>Purchase </th>
                                <th>Sales </th>
                                <th>Closing </th>
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
            <div id="chartContainer" style="height: 300px; width: 100%;"></div>
            <input type="button" class="btn invisible" id="backButton" value="Back">
        </form>
    </body>
    <link href="../../css/jquery.multiselect.css" rel="stylesheet" />
    <link href="../../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
    <script type="text/javascript">
        var totalVisitors = 883000;
        var visitorsDrilldownedChartOptions;
        var newVSReturningVisitorsOptions
        var visitorsData;
        function visitorsChartDrilldownHandler(e) {
            chart = new CanvasJS.Chart("chartContainer", visitorsDrilldownedChartOptions);
            chart.options.data = visitorsData[e.dataPoint.name];
            chart.options.title = { text: e.dataPoint.name }
            chart.render();
            $("#backButton").toggleClass("invisible");
        }

        $("#backButton").click(function () {
            $(this).toggleClass("invisible");
            chart = new CanvasJS.Chart("chartContainer", newVSReturningVisitorsOptions);
            chart.options.data = visitorsData["NewvsReturningVisitors"];
            chart.render();
        });


        var AllOrders = []; var sf_type = ''; var sfCode = ''; var All_stock_details = []; var txt = '';
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
            dist_code = '<%=Session["Sf_Code"]%>';
            var get_access_master = localStorage.getItem("Access_Details");
            get_access_master = JSON.parse(get_access_master);
            var access_month = get_access_master[0].Year_value;
            var access_type = get_access_master[0].Year_setting;
            access_month = access_month.split('-');
            From_month = access_month[0];
            To_month = access_month[1];

            get_Years(From_month, To_month, access_type);
            if (access_type == '0') {
                var get_finanacial_year = $('.year_select').val();
                splited_form_year = get_finanacial_year;
                splited_to_year = get_finanacial_year;
                loadData();
            }
            else {

                var get_finanacial_year = $('.year_select').val();
                get_finanacial_year = get_finanacial_year.split('-');
                splited_form_year = get_finanacial_year[1];
                splited_to_year = get_finanacial_year[3];
                loadData();

            }
            $(document).on("change", ".year_select", function () {

                var selected_year = $(this).val();

                if (access_type == '0') {

                    splited_form_year = selected_year;
                    splited_to_year = selected_year;
                    loadData();

                }
                else {

                    get_finanacial_year = selected_year.split('-');
                    splited_form_year = get_finanacial_year[1];
                    splited_to_year = get_finanacial_year[3];
                    loadData();

                }

            });
            function get_Years(from_mnth, to_mnth, type) {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    data: "{'FM':'" + from_mnth + "','TM':'" + to_mnth + "','Type':'" + type + "'}",
                    url: "Rpt_Retailer_Payment_Pending.aspx/Get_Year_Data",
                    dataType: "json",
                    success: function (data) {
                        Year_Data = JSON.parse(data.d);
                        for (var t = 0; t < Year_Data.length; t++) {
                            $('.year_select').append("<option selected value=" + Year_Data[t].Get_years + ">" + Year_Data[t].Get_years + "</option>");
                        }
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            }
            loadData();

            var sf_type = '<%=Session["sf_type"]%>';
            if (sf_type != '4') {
                $('#distselect').show();
                loaddist();
            }


            $(document).on('change', "#ddl_dist", function (e) {
                var dist_code = $('#ddl_dist').val();
                loadData();
            });
            $(".data-table-basic_length").on("change",
                function () {
                    pgNo = 1;
                    PgRecords = parseFloat($(this).val());
                    ReloadTable();
                });

            function loadData() {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    data: "{'dist_code':'" + dist_code + "','splited_form_year':'" + splited_form_year + "','splited_to_year':'" + splited_to_year + "','From_month':'" + From_month + "','To_month':'" + To_month + "'}",
                    url: "StockDetails.aspx/Get_Stock",
                    dataType: "json",
                    success: function (data) {
                        AllOrders = JSON.parse(data.d) || [];
                        Orders = AllOrders; ReloadTable();
                    },
                    error: function (result) {
                    }
                });

            }
            function loaddist() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "StockDetails.aspx/binddistributor",
                    data: "{'sf_code':'<%=Session["Sf_Code"]%>','Div':'<%=Session["Division_Code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        itms = JSON.parse(data.d) || [];
                        for (var i = 0; i < itms.length; i++) {
                            $('#ddl_dist').append($("<option></option>").val(itms[i].Stockist_Code).html(itms[i].Stockist_Name + '-' + itms[i].ERP_Code)).trigger('chosen:updated').css("width", "100%");
                        }
                        $('#ddl_dist').selectpicker({
                            liveSearch: true
                        });
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            }



            function ReloadTable() {

                var total_opening = 0; var tot_purchase = 0; var tot_sales = 0; var tot_closing = 0;
                var ttotal_opening = 0; var ttot_purchase = 0; var ttot_sales = 0; var ttot_closing = 0;
                $("#Stock_table TBODY").html("");
                st = PgRecords * (pgNo - 1); slno = 0;
                for ($j = 0; $j < Orders.length; $j++) {
                    ttotal_opening = Orders[$j].Opening_Stock + ttotal_opening;
                    ttot_purchase = Orders[$j].Purchase + ttot_purchase;
                    ttot_sales = Orders[$j].Sales + ttot_sales;
                    ttot_closing = Orders[$j].Closing + ttot_closing;
                }
                var dataPoints1 = []; var dataPoints2 = []; var dataPoints3 = []; var dataPoints4 = [];
                for ($i = st; $i < st + PgRecords; $i++) {

                    if ($i < Orders.length) {

                        tr = $("<tr></tr>");
                        slno = $i + 1;
                        $(tr).html("<td>" + slno + "</td><td class='pro_code'>" + Orders[$i].Product_Detail_Code + "</td><td class='pro_name'>" + Orders[$i].Product_Detail_Name + "</td><td class='op_stock'>" + Orders[$i].Opening_Stock + "</td><td class='purchase_order'>" + Orders[$i].Purchase + "</td>><td class='sales'>" + Orders[$i].Sales + "</td><td class='Closing_stock'>" + Orders[$i].Closing + "</td>");
                        $("#Stock_table TBODY").append(tr);

                        total_opening = Orders[$i].Opening_Stock + total_opening;
                        tot_purchase = Orders[$i].Purchase + tot_purchase;
                        tot_sales = Orders[$i].Sales + tot_sales;
                        tot_closing = Orders[$i].Closing + tot_closing;
                        dataPoints1.push({ label: Orders[$i].Product_Detail_Name, y: Number(Orders[$i].Purchase) });
                        dataPoints2.push({ label: Orders[$i].Product_Detail_Name, y: Number(Orders[$i].Sales) });

                    }

                }
                totalVisitors = tot_purchase + tot_sales;
                visitorsData = {
                    "NewvsReturningVisitors": [{
                        click: visitorsChartDrilldownHandler,
                        cursor: "pointer",
                        explodeOnClick: false,
                        innerRadius: "70%",
                        legendMarkerType: "square",
                        name: "Opening VS Closing Stock",
                        radius: "100%",
                        showInLegend: true,
                        startAngle: 90,
                        type: "doughnut",
                        dataPoints: [
                            { y: Number(tot_purchase), name: "Purchase", color: "#E7823A" },
                            { y: Number(tot_sales), name: "Sales", color: "#546BC1" }
                        ]
                    }],
                    "Sales": [{
                        color: "#546BC1",
                        name: "Sales",
                        showInLegend: true,
                        type: "column",
                        dataPoints: dataPoints2
                    }],
                    "Purchase": [{
                        color: "#E7823A",
                        name: "Purchase",
                        showInLegend: true,
                        type: "column",
                        dataPoints: dataPoints1
                    }]
                };

                newVSReturningVisitorsOptions = {
                    animationEnabled: true,
                    theme: "light2",
                    title: {
                        text: "Purchase vs Sales "
                    },
                    subtitles: [{
                        text: "Click on Any Segment to Drilldown",
                        backgroundColor: "#2eacd1",
                        fontSize: 12,
                        fontColor: "white",
                        padding: 5
                    }],
                    width: 500,
                    height: 310,
                    legend: {
                        fontFamily: "calibri",
                        fontSize: 14,
                        itemTextFormatter: function (e) {
                            return e.dataPoint.name + ": " + Math.round(e.dataPoint.y / totalVisitors * 100) + "%";
                        }
                    },
                    data: []
                };

                visitorsDrilldownedChartOptions = {
                    animationEnabled: true,
                    theme: "light2",
                    width: 500,
                    height: 310,
                    axisX: {
                        labelFontColor: "#717171",
                        lineColor: "#a2a2a2",
                        tickColor: "#a2a2a2"
                    },
                    axisY: {
                        gridThickness: 0,
                        includeZero: false,
                        labelFontColor: "#717171",
                        lineColor: "#a2a2a2",
                        tickColor: "#a2a2a2",
                        lineThickness: 1
                    },
                    
                    data: []
                };

                var chart = new CanvasJS.Chart("chartContainer", newVSReturningVisitorsOptions);
                chart.options.data = visitorsData["NewvsReturningVisitors"];
                chart.render();
                $("#Stock_table TFOOT").html("<tr><td colspan='3' style='font-weight: bold;padding-right: 35px;text-align:right;'>Total</td></td><td style='font-weight: bold;'>" + total_opening.toFixed(2) + "</td><td style='font-weight: bold;'>" + tot_purchase.toFixed(2) + "</td><td style='font-weight: bold;'>" + tot_sales.toFixed(2) + "</td><td style='font-weight: bold;'>" + tot_closing.toFixed(2) + "</td></tr><tr><td colspan='3' style='font-weight: bold;padding-right: 35px;text-align:right;'>Overall Total</td></td><td style='font-weight: bold;'>" + ttotal_opening.toFixed(2) + "</td><td style='font-weight: bold;'>" + ttot_purchase.toFixed(2) + "</td><td style='font-weight: bold;'>" + ttot_sales.toFixed(2) + "</td><td style='font-weight: bold;'>" + ttot_closing.toFixed(2) + "</td></tr>");
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
    </html>
</asp:Content>

