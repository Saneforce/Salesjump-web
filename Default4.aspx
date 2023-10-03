<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Master_DIS.master" AutoEventWireup="true"
    CodeFile="Default4.aspx.cs" Inherits="Default4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <script src="canvas/canvasjs.min.js"></script>
    <script src="canvas/canvasjs.stock.min.js"></script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />

    <style>
        th {
            white-space: nowrap;
        }

        .sm-st-icon {
            width: 49px;
            height: 44px;
            padding-top: 7px;
        }

        .clearfix {
            box-shadow: 0 4px 8px 0 rgb(0 0 0 / 20%);
        }

        .sm-st-icon {
            box-shadow: -6px 5px 9px 0px rgb(30 4 4 / 20%);
        }

        .nav.nav-pills > li.active > a, .nav.nav-pills > li.active > a:hover {
            background-color: #f0f3f4;
            border-bottom: 2px solid #19a4c6;
            border-top-color: #f0f3f4;
            color: #444;
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
                <div class="sm-st clearfix retail" style="border: 1px solid #ffc1c140;">
                    <span class="sm-st-icon st-red"><i class="fa fa-user"></i></span>
                    <div class="sm-st-info">
                        <span>
                            <asp:Label ID="retailer" runat="server"></asp:Label></span> Customers
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="sm-st clearfix  DSMCnt" style="border: 1px solid #9f95dd5c;">
                    <span class="sm-st-icon st-violet"><i class="fa fa-user"></i></span>
                    <div class="sm-st-info">
                        <span>
                            <asp:Label ID="DSMCnt" runat="server"></asp:Label></span> Sales Man
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="sm-st clearfix Pri_order_count" style="border: 1px solid #8ce4ff3d;">
                    <span class="sm-st-icon st-blue"><i class="glyphicon glyphicon-shopping-cart"></i></span>
                    <div class="sm-st-info">
                        <span>
                            <asp:Label ID="Pri_order_count" runat="server"></asp:Label></span>Purchase Orders
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="sm-st clearfix sec_order_count" style="border: 1px solid #b8f3c663;">
                    <span class="sm-st-icon st-green"><i class="fa fa-paperclip"></i></span>
                    <div class="sm-st-info">
                        <span>
                            <asp:Label ID="sec_order_count" runat="server"></asp:Label></span>Sale Orders
                    </div>
                </div>
            </div>
            <div class="col-md-12" style="padding: 0px;">
                <div id="exTab3" class="container" >
                    <ul class="nav nav-pills">
                        <li class="active"><a href="#1b" id="TabSec" data-toggle="tab">Secondary</a> </li>
                        <li><a href="#2b" id="TabPri" data-toggle="tab">Primary</a> </li>
			            <li ><a href="#3b" id="TabStock" data-toggle="tab" style="display:none">Stock</a> </li>
                    </ul>
                </div>
                <div class="tab-content " > 
                    <div class="tab-pane active" id="1b">
                        <div class="col-md-12" style="padding: 0px;">
                            <div class="row" style="position: relative; min-height: 600px">

                                <asp:HiddenField ID="HiddenField1" runat="server"></asp:HiddenField>
                                <div id="chartContainer" class="col-6 card" style="height: 250px; max-width: 45%; margin: 20px; position: absolute; top: 0; left: 26px; box-shadow: 0 4px 8px 0 rgb(0 0 0 / 20%);"></div>
                                <div id="chartContainer1" class="col-6 card" style="height: 250px; max-width: 45%; margin: 20px; position: absolute; top: 0px; right: 14px; box-shadow: 0 4px 8px 0 rgb(0 0 0 / 20%);"></div>
                                <div id="chartContainer2" class="col-12 card" style="height: 320px; max-width: 94%; margin: 20px; position: absolute; top: 262px; right: 14px; box-shadow: 0 4px 8px 0 rgb(0 0 0 / 20%);"></div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane" id="2b">
                        <div class="col-md-12" style="position: relative; min-height: 600px">
                            <input type="date" id="dtcat" style="min-width: 110px !important; width: 155px; display: inline-block; border: none; background-color: transparent; color: #336277; font-family: Verdana; font-weight: bold; font-size: 14px; margin-top: -13PX; margin-right: 20PX; float: RIGHT;" />
                            <div id="chartContainer3" class="col-6 card" style="height: 250px; max-width: 501px; margin: 20px; position: absolute; top: 0; left: 11px; box-shadow: 0 4px 8px 0 rgb(0 0 0 / 20%);"></div>
                            <div id="chartContainer4" class="col-3 card" style="height: 250px; max-width: 501px; margin: 20px; position: absolute; top: 0px; right: 14px; box-shadow: 0 4px 8px 0 rgb(0 0 0 / 20%);"></div>
                            <div id="chartContainer5" class="col-12 card" style="height: 420px; max-width: 1045px; margin: 20px; position: absolute; top: 262px; right: 14px; box-shadow: 0 4px 8px 0 rgb(0 0 0 / 20%);"></div>

                        </div>
                    </div>
		 <%-- stock --%>
                  <div class="tab-pane active" id="3b" style="display:none">
                    <div class="col-md-12"style="position:relative;min-height:500px">
                       <div style="margin-top: 5px;" class="card">
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
                   
                </div>
                </div>
                <%-- stock --%>
                </div>
            </div>
        </div>

        <div class="card" style="display: none;">
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
            var chart1 = new CanvasJS.Chart();
            var chart = new CanvasJS.Chart();
            var chart3 = new CanvasJS.Chart();
            var chart4 = new CanvasJS.Chart();
            var dts = $("#datepic").val();
            var datas = [];
            var datas1 = [];
            std2 = today;
            var d = $('#<%=HiddenField1.ClientID %>').val() || '';


            upd();
            updret();
            updtotalview()
            //setTimeout(upd, 1500);
            setInterval(function () { upd() }, 18500);
            setInterval(function () { updret() }, 39500);
            setInterval(function () { updtotalview() }, 59500);
            function addData() {
                var boilerColor, deltaY, yVal;
                var dps = chart1.options.data[0].dataPoints;
                var dps1 = chart1.options.data;
                for (var i = 0; i < datas.length; i++) {
                    deltaY = datas[i]["Product_Cat_Name"] + ' (Qty - ' + datas[i]["quantity"] + ')';
                    yVal = parseInt(datas[i]["value1"]);
                    dps.push({ label: datas[i]["Product_Cat_Name"], y: parseInt(datas[i]["value1"]), name: deltaY, showInLegend: true, legendText: deltaY, });

                }

                chart1.options.data[0].dataPoints = dps;
                showDefaultText(chart1, "No Data Found!");
                chart1.render();

            }
            function addretData() {
                var boilerColor, deltaY, yVal;
                var dps = [];
                var dps1 = [];

                for (var i = 0; i < datas1.length; i++) {
                    for (var j = 0; j < datas1.length; j++) {
                        dps = [];
                        if (datas1[i]["ListedDrCode"] == datas1[j]["ListedDrCode"]) {
                            dps.push({ x: new Date(parseInt(datas1[j]["yr"]), parseInt(datas1[j]["mth"]), parseInt(datas1[j]["dt"])), y: parseInt(datas1[i]["val"]) });
                        }
                    }
                    chart.options.data[i].dataPoints = dps;

                }
                showDefaultText(chart, "No Data Found!");
                chart.render();

            }
            function showDefaultText(chart, text) {
                var dataPoints = chart.options.data[0].dataPoints;
                var isEmpty = !(dataPoints && dataPoints.length > 0);

                if (!isEmpty) {
                    for (var i = 0; i < dataPoints.length; i++) {
                        isEmpty = !dataPoints[i].y;
                        if (!isEmpty)
                            break;
                    }
                }

                if (!chart.options.subtitles)
                    chart.options.subtitles = [];
                if (isEmpty) {
                    chart.options.subtitles.push({
                        text: text,
                        verticalAlign: 'center',
                    });
                    chart.options.title.text = "";
                }
                else
                    chart.options.subtitles = [];
            }
            function addDatapri() {
                var boilerColor, deltaY, yVal;
                var dps = chart3.options.data[0].dataPoints;
                var dps1 = chart3.options.data;
                for (var i = 0; i < datas.length; i++) {
                    deltaY = datas[i]["Product_Cat_Name"] + ' (Qty - ' + datas[i]["quantity"] + ')';
                    yVal = parseInt(datas[i]["value"]);
                    dps.push({ label: datas[i]["Product_Cat_Name"], y: parseInt(datas[i]["value"]), name: deltaY, showInLegend: true, legendText: deltaY, });

                }

                chart3.options.data[0].dataPoints = dps;
                showDefaultText(chart3, "No Data Found!");
                chart3.render();

            }
            function addDatapri_mon(typ) {
                var boilerColor, deltaY, yVal;
                var dps = chart4.options.data[0].dataPoints;
                var dps1 = chart4.options.data;
                var tt = 0;
                for (var i = 0; i < datas.length; i++) {
                    tt += parseInt(datas[i]["value"]);
                    deltaY = datas[i]["Product_Cat_Name"] + ' (Qty - ' + datas[i]["quantity"] + ')';
                    yVal = parseInt(datas[i]["value"]);
                    dps.push({ label: datas[i]["Product_Cat_Name"], y: parseInt(datas[i]["value"]), name: deltaY, legendText: deltaY, });

                }

                chart4.options.data[0].dataPoints = dps;
                chart4.options.title.text = typ + " " + tt;
                chart4.options.subtitles[0].text = tt;
                showDefaultText(chart4, "No Data Found!");
                chart4.render();

            }
            function upd() {
                var d1 = $('#<%=HiddenField1.ClientID %>').val() || '';
                if ($("#TabSec").parent().hasClass("active")) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Default4.aspx/GetCatSecondOrderDay",
                        data: "{'dt':'" + d1 + "'}",
                        dataType: "json",
                        success: function (data) {
                            datas = JSON.parse(data.d) || [];
                            var dataPoints = [];

                            chart1 = new CanvasJS.Chart("chartContainer", {
                                animationEnabled: true,
                                title: {
                                    text: "Category wise view",
                                    fontSize: 16,
                                    fontWeight: "lighter",
                                    fontColor: "#19a4c6"
                                },
                                axisY: {
                                    title: "Amount",
                                    suffix: "rs",
                                    labelFontSize: 10,
                                    labelFontColor: "dimGrey"
                                },
                                axisX: {
                                    labelFontSize: 10,
                                    labelFontColor: "dimGrey"
                                },
                                legend: {
                                    cursor: "pointer",
                                    fontSize: 8,
                                    itemclick: function (e) {
                                        if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
                                            e.dataSeries.visible = false;
                                        } else {
                                            e.dataSeries.visible = true;

                                        }
                                    }

                                },
                                toolTip: {
                                    shared: true,
                                    content: toolTipFormatter
                                },
                                data: [{
                                    type: "pie",
                                    yValueFormatString: "#,### rs",
                                    dataPoints: []
                                }]
                            });
                            addData();
                        },
                        error: function (result) {
                            //alert(JSON.stringify(result));
                        }
                    });
                }
            }
            function toolTipFormatter(e) {
                var str = "";
                var total = 0;
                var str3;
                var str2;
                for (var i = 0; i < e.entries.length; i++) {
                    var str1 = "<span style= 'color:" + e.entries[i].dataSeries.color + "'>" + e.entries[i].dataPoint.legendText + "</span>: <strong>" + e.entries[i].dataPoint.y + "</strong> <br/>";
                    total = e.entries[i].dataPoint.y + total;
                    str = str.concat(str1);
                }
                str2 = "<strong>" + e.entries[0].dataPoint.label + "</strong> <br/>";
                str3 = "<span style = 'color:Tomato'>Total: </span><strong>" + total + "</strong><br/>";
                return (str2.concat(str)).concat(str3);
            }
            function updret() {
                var d2 = $('#<%=HiddenField1.ClientID %>').val() || '';
                if ($("#TabSec").parent().hasClass("active")) {
                    var retdistinct = [];
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Default4.aspx/GetretSecondOrderhead",
                        data: "{'dt':'" + d2 + "'}",
                        dataType: "json",
                        success: function (data) {
                            retdistinct = JSON.parse(data.d) || [];
                            if (retdistinct.length > 0) {
                                $.ajax({
                                    type: "POST",
                                    contentType: "application/json; charset=utf-8",
                                    async: false,
                                    url: "Default4.aspx/GetretSecondOrderDay",
                                    data: "{'dt':'" + d2 + "'}",
                                    dataType: "json",
                                    success: function (data) {
                                        datas1 = JSON.parse(data.d) || [];

                                        var data = [];
                                        for (var i = 0; i <= retdistinct.length - 1; i++) {
                                            var dataPointss = [];
                                            for (var j = 0; j <= datas1.length - 1; j++) {
                                                var dataSeries = { type: "line" };
                                                //var dataPoints = [];
                                                if (retdistinct[i]["ListedDrCode"] == datas1[j]["ListedDrCode"]) {

                                                    dataPointss.push({
                                                        x: new Date(parseInt(datas1[j]["yr"]), parseInt(datas1[j]["mth"]), parseInt(datas1[j]["dt"])),
                                                        y: parseInt(datas1[j]["val"])
                                                    });

                                                }
                                                //dataPointss.push({ label: new Date(parseInt(datas1[j]["yr"]), parseInt(datas1[j]["mth"]), parseInt(datas1[j]["dt"])).x, y: parseInt(datas1[i]["val"]).y });
                                            }
                                            dataSeries.name = retdistinct[i]["ListedDr_Name"];
                                            dataSeries.dataPoints = dataPointss;
                                            dataSeries.type = "spline";
                                            dataSeries.yValueFormatString = "#0.## ₹";
                                            dataSeries.showInLegend = true;
                                            name: "Myrtle Beach",
                                                data.push(dataSeries);
                                        }
                                        var options = {

                                            zoomEnabled: true,
                                            animationEnabled: true,
                                            title: {
                                                text: "Weekly Summary Retailer Wise",
                                                fontSize: 18,
                                                fontColor: "#19a4c6"
                                            },
                                            axisY2: {
                                                title: "Amount",
                                                valueFormatString: "0.0 days",
                                                interlacedColor: "#F5F5F5",
                                                gridColor: "#D7D7D7",
                                                tickColor: "#D7D7D7",
                                                labelFontSize: 9,
                                                labelFontColor: "dimGrey"
                                            },
                                            axisX: {
                                                title: "Retailers",
                                                labelFontSize: 10,
                                                labelFontColor: "dimGrey"
                                            },
                                            theme: "theme2",
                                            toolTip: {
                                                shared: true
                                            },
                                            legend: {
                                                verticalAlign: "bottom",
                                                horizontalAlign: "center",
                                                fontSize: 8,
                                                fontFamily: "Lucida Sans Unicode",
                                                cursor: "pointer",
                                                indexLabelPlacement: "inside",
                                                itemclick: function (e) {
                                                    if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
                                                        e.dataSeries.visible = false;
                                                    }
                                                    else {
                                                        e.dataSeries.visible = true;

                                                    }

                                                }
                                            },
                                            data: data  // random data
                                        };
                                        var chart = new CanvasJS.Chart("chartContainer1", options);
                                        chart.render();

                                    },
                                    error: function (result) {
                                        //alert(JSON.stringify(result));
                                    }
                                });
                            }
                        },
                        error: function (result) {
                            //alert(JSON.stringify(result));
                        }
                    });
                }

            }
            function updtotalview() {
                var d2 = $('#<%=HiddenField1.ClientID %>').val() || '';
                if ($("#TabSec").parent().hasClass("active")) {
                    var future = new Date(d2);
                    let per30days = new Date(future.setDate(future.getDate() - 35))
                    let next30days = new Date(future.setDate(future.getDate() + 5))


                    var retdistinct = [];
                    var dataPoints1 = [];
                    var dataPoints2 = [];
                    var dataPoints3 = [];
                    var dataPoints4 = [];
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Default4.aspx/GetretSecondOrdertotalview",
                        data: "{'dt':'" + d2 + "'}",
                        dataType: "json",
                        success: function (data) {
                            rettotal = JSON.parse(data.d) || [];

                            for (var i = 0; i <= rettotal.length - 1; i++) {
                                dataPoints1.push({ x: new Date(rettotal[i].dt), y: Number(rettotal[i].bill) });
                                dataPoints2.push({ x: new Date(rettotal[i].dt), y: Number(rettotal[i].inv) });
                                dataPoints3.push({ x: new Date(rettotal[i].dt), y: Number(rettotal[i].py) });
                                dataPoints4.push({ x: new Date(rettotal[i].dt), y: Number(rettotal[i].cn) });
                            }
                            var stockChart = new CanvasJS.StockChart("chartContainer2", {

                                theme: "light2",
                                animationEnabled: true,
                                title: {
                                    fontColor: "#19a4c6",
                                    text: "Monthly Order Summary"
                                },
                                subtitles: [{
                                    text: "summary Order/Invoice/Payment/Creditnote"
                                }],
                                charts: [{
                                    axisY: {
                                        title: "Amount ₹"
                                    },
                                    toolTip: {
                                        shared: true
                                    },
                                    legend: {
                                        cursor: "pointer",
                                        itemclick: function (e) {
                                            if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible)
                                                e.dataSeries.visible = false;
                                            else
                                                e.dataSeries.visible = true;
                                            e.chart.render();
                                        }
                                    },
                                    data: [{
                                        showInLegend: true,
                                        name: "Order Amount ₹",
                                        yValueFormatString: "₹,##0",
                                        xValueType: "dateTime",
                                        dataPoints: dataPoints1
                                    }, {
                                        showInLegend: true,
                                        name: "Invoice Amount ₹",
                                        yValueFormatString: "₹,##0",
                                        dataPoints: dataPoints2
                                    },
                                    {
                                        showInLegend: true,
                                        name: "Paid Amount ₹",
                                        yValueFormatString: "₹,##0",
                                        dataPoints: dataPoints3
                                    },
                                    {
                                        showInLegend: true,
                                        name: "Credit Amount ₹",
                                        yValueFormatString: "#,##0",
                                        dataPoints: dataPoints4
                                    }]
                                }],
                                rangeSelector: {
                                    enabled: false
                                },
                                navigator: {
                                    data: [{
                                        dataPoints: dataPoints1
                                    }],
                                    slider: {
                                        minimum: new Date(per30days),
                                        maximum: new Date(next30days)
                                    }
                                }
                            });
                            stockChart.render();


                        },
                        error: function (result) {
                            //alert(JSON.stringify(result));
                        }
                    });
                }
            }
            function toggleDataSeries(e) {
                if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
                    e.dataSeries.visible = false;
                }
                else {
                    e.dataSeries.visible = true;
                }
                chart.render();
            }
            function updpri() {

                var d1 = $('#<%=HiddenField1.ClientID %>').val() || '';
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Default4.aspx/GetCatPrimaryOrderDay",
                    data: "{'dt':'" + d1 + "'}",
                    dataType: "json",
                    success: function (data) {
                        datas = JSON.parse(data.d) || [];
                        var dataPoints = [];

                        chart3 = new CanvasJS.Chart("chartContainer3", {
                            animationEnabled: true,
                            title: {
                                text: "Category wise view",
                                fontSize: 16,
                                fontWeight: "lighter",
                                fontColor: "#19a4c6"
                            },
                            axisY: {
                                title: "Amount",
                                suffix: "rs",
                                labelFontSize: 10,
                                labelFontColor: "dimGrey"
                            },
                            axisX: {
                                labelFontSize: 10,
                                labelFontColor: "dimGrey"
                            },
                            legend: {
                                cursor: "pointer",
                                fontSize: 8,
                                itemclick: function (e) {
                                    if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
                                        e.dataSeries.visible = false;
                                    } else {
                                        e.dataSeries.visible = true;

                                    }
                                }

                            },
                            toolTip: {
                                shared: true,
                                content: toolTipFormatter
                            },
                            data: [{
                                type: "pie",
                                yValueFormatString: "#,### rs",
                                dataPoints: []
                            }]
                        });
                        addDatapri();
                    },
                    error: function (result) {
                        //alert(JSON.stringify(result));
                    }
                });

            }
            function updpri_mon() {

                var d1 = $('#<%=HiddenField1.ClientID %>').val() || '';
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Default4.aspx/GetCatPrimaryOrderDaymon",
                    data: "{'dt':'" + d1 + "'}",
                    dataType: "json",
                    success: function (data) {
                        datas = JSON.parse(data.d) || [];
                        var dataPoints = [];

                        chart4 = new CanvasJS.Chart("chartContainer4", {
                            animationEnabled: true,
                            title: {
                                text: "",
                                verticalAlign: "center",
                                fontSize: 14,
                                fontWeight: "lighter"
                            },
                            subtitles: [
                                {
                                    text: "This is a Subtitle"
                                    //Uncomment properties below to see how they behave
                                    //fontColor: "red",
                                    //fontSize: 30
                                }
                            ],
                            axisY: {
                                title: "Amount",
                                suffix: "rs",
                                labelFontSize: 10,
                                labelFontColor: "dimGrey"
                            },
                            axisX: {
                                labelFontSize: 10,
                                labelFontColor: "dimGrey"
                            },
                            legend: {
                                cursor: "pointer",
                                fontSize: 8,
                                itemclick: function (e) {
                                    if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
                                        e.dataSeries.visible = false;
                                    } else {
                                        e.dataSeries.visible = true;

                                    }
                                }

                            },
                            toolTip: {
                                shared: true,
                                content: toolTipFormatter
                            },
                            data: [{
                                type: "doughnut",
                                yValueFormatString: "#,### rs",
                                dataPoints: []
                            }]
                        });
                        addDatapri_mon('Monthly');
                    },
                    error: function (result) {
                        //alert(JSON.stringify(result));
                    }
                });

            }
            function dystock() {
                var d1 = $('#<%=HiddenField1.ClientID %>').val() || '';
                var dataPoints1 = [], dataPoints2 = [], dataPoints3 = []; dataPoints4 = [];
                var stockChart1 = new CanvasJS.StockChart("chartContainer5", {
                    theme: "light2",
                    exportEnabled: true,
                    title: {
                        text: "Order VS invoice Summary",
                        fontSize: 16,
                        fontWeight: "lighter",
                        fontColor: "#19a4c6"
                    },
                    charts: [{
                        height: 150,
                        toolTip: {
                            shared: true
                        },
                        axisX: {
                            lineThickness: 5,
                            tickLength: 0,
                            labelFormatter: function (e) {
                                return "";
                            }
                        },

                        axisY: {
                            prefix: "₹"
                        },
                        legend: {
                            verticalAlign: "top"
                        },
                        data: [{
                            showInLegend: true,
                            name: "Order Details",
                            yValueFormatString: "₹#,###.##",
                            dataPoints: dataPoints1
                        }]
                    },
                    {
                        height: 150,
                        toolTip: {
                            shared: true
                        },
                        axisY: {
                            prefix: "₹"

                        },
                        legend: {
                            verticalAlign: "top"
                        },
                        data: [{
                            showInLegend: true,
                            name: "Invoice Details",
                            yValueFormatString: "₹#,###.##",
                            dataPoints: dataPoints2
                        }]
                    }],
                    navigator: {
                        data: [{
                            dataPoints: dataPoints3
                        }],
                        slider: {
                            minimum: new Date(2022, 01, 01),
                            maximum: new Date(2022, 12, 01)
                        }
                    }
                });

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Default4.aspx/GetCatPrimaryOrder_YEARLY",
                    data: "{'dt':'" + d1 + "'}",
                    dataType: "json",
                    success: function (data) {
                        var datayr = JSON.parse(data.d) || [];
                        for (var i = 0; i < datayr.length; i++) {
                            dataPoints1.push({ x: new Date(datayr[i].dt), y: Number(datayr[i]["value"]) });
                            dataPoints2.push({ x: new Date(datayr[i].dt), y: Number(datayr[i]["value1"]) });
                            dataPoints3.push({ x: new Date(datayr[i].dt), y: Number(datayr[i]["value"] = datayr[i]["value"]) });
                        }
                    },
                    error: function (result) {
                        //alert(JSON.stringify(result));
                    }
                });
                stockChart1.render();


            }
            var TDate = new Date();
            var TMonth = TDate.getMonth() + 1;
            var TDay = TDate.getDate();
            var TYear = TDate.getFullYear();
            var CDate = TYear + '-' + TMonth + '-' + TDay;
            var CDate1 = TDay + '-' + TMonth + '-' + TYear;
            $(function () {
                $('.clearfix').hover(function () {
                    $(this).find('.sm-st-icon').css({ "transition-duration": "0.5s", "margin-top": "-4px" });
                },
             function () {
                 $(this).find('.sm-st-icon').css({ "transition-duration": "0.5s", "margin-top": "0px" });
             })
            });
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
            $(document).on('click', "#TabPri", function (e) {
                updpri();
                updpri_mon();
                dystock();
            });
            $(document).on('click', "#TabSec", function (e) {
                upd();
                updret();
                updtotalview();
            });

            $(document).on('change', "#datepic", function (e) {
                var txt = $('#datepic').val();
                $('#<%=hdndate.ClientID %>').val(txt);
                __doPostBack(txt, e);
            });
            $(document).on('change', "#dtcat", function (e) {
                var txt = $('#dtcat').val();
                var d1 = $('#<%=HiddenField1.ClientID %>').val() || '';
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Default4.aspx/GetCatPrimaryOrderDaymon_CUSTOM",
                data: "{'dt':'" + txt + "'}",
                dataType: "json",
                success: function (data) {
                    datas = JSON.parse(data.d) || [];
                    var dataPoints = [];

                    chart4 = new CanvasJS.Chart("chartContainer4", {
                        animationEnabled: true,
                        title: {
                            text: "",
                            verticalAlign: "center",
                            fontSize: 14,
                            fontWeight: "lighter"
                        },
                        subtitles: [
                            {
                                text: "This is a Subtitle"
                                //Uncomment properties below to see how they behave
                                //fontColor: "red",
                                //fontSize: 30
                            }
                        ],
                        axisY: {
                            title: "Amount",
                            suffix: "rs",
                            labelFontSize: 10,
                            labelFontColor: "dimGrey"
                        },
                        axisX: {
                            labelFontSize: 10,
                            labelFontColor: "dimGrey"
                        },
                        legend: {
                            cursor: "pointer",
                            fontSize: 8,
                            itemclick: function (e) {
                                if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
                                    e.dataSeries.visible = false;
                                } else {
                                    e.dataSeries.visible = true;

                                }
                            }

                        },
                        toolTip: {
                            shared: true,
                            content: toolTipFormatter
                        },
                        data: [{
                            type: "doughnut",
                            yValueFormatString: "#,### rs",
                            dataPoints: []
                        }]
                    });
                    addDatapri_mon('Total');
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });

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
                url: "Default4.aspx/Get_Stock",
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
                url: "Default4.aspx/Get_access_master",
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
                    //localStorage.clear();
                   // window.localStorage.removeItem("Date_Details");
                    window.open('Stockist/myOrders.aspx?', '_self');
                }
            });

            $(".Pri_order_count").on("click", function () {
                //localStorage.clear();
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
