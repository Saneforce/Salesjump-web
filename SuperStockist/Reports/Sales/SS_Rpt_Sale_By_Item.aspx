<%@ Page Title="" Language="C#" MasterPageFile="~/Master_SS.master" AutoEventWireup="true" CodeFile="SS_Rpt_Sale_By_Item.aspx.cs" Inherits="SuperStockist_Reports_Sales_SS_Rpt_Sale_By_Item" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <html>
    <title></title>
    <%--<link href="../../datatable/DataTables-1.13.1/css/dataTables.dataTables.min.css" rel="stylesheet" />--%>
    <link href="../../../../datatable/DataTables-1.13.1/css/jquery.dataTables.min.css" rel="stylesheet" />
    <link href="../../../../datatable/DataTables-1.13.1/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="../../../../datatable/Responsive-2.4.0/css/responsive.bootstrap.min.css" rel="stylesheet" />
    <link href="../../../../datatable/Responsive-2.4.0/css/responsive.dataTables.min.css" rel="stylesheet" />
    <link href="../../../../datatable/Responsive-2.4.0/css/responsive.jqueryui.min.css" rel="stylesheet" />
    <link href="../../../../datatable/SearchPanes-2.1.0/css/searchPanes.bootstrap.css" rel="stylesheet" />
    <link href="../../../../datatable/SearchPanes-2.1.0/css/searchPanes.dataTables.min.css" rel="stylesheet" />
    <link href="../../../../datatable/SearchPanes-2.1.0/css/searchPanes.jqueryui.min.css" rel="stylesheet" />
    <script src="../../../../datatable/DataTables-1.13.1/js/dataTables.bootstrap.min.js"></script>
    <script src="../../../../datatable/DataTables-1.13.1/js/dataTables.dataTables.js"></script>
    <script src="../../../../datatable/DataTables-1.13.1/js/jquery.dataTables.min.js"></script>
    <script src="../../../../datatable/JSZip-2.5.0/jszip.min.js"></script>
    <script src="../../../../datatable/pdfmake-0.1.36/pdfmake.min.js"></script>
    <script src="../../../../datatable/Responsive-2.4.0/js/dataTables.responsive.min.js"></script>
    <script src="../../../../datatable/Responsive-2.4.0/js/responsive.dataTables.min.js"></script>
    <script src="../../../../datatable/Responsive-2.4.0/js/responsive.jqueryui.min.js"></script>
    <script src="../../../../datatable/SearchPanes-2.1.0/js/dataTables.searchPanes.min.js"></script>
    <script src="../../../../datatable/SearchPanes-2.1.0/js/searchPanes.dataTables.min.js"></script>
    <script src="../../../../datatable/SearchPanes-2.1.0/js/searchPanes.jqueryui.min.js"></script>
    <style>
       .chosen-container {
                width: 320px !important;
            }

            input[type='text'], select {
                border: 1.5px solid #19a4c6a3 !important;
                background: aliceblue!important;
            }
            input[type='search'], select {
                border: 1.5px solid #19a4c6a3 !important;
                background: aliceblue!important;
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

            table.dataTable {
                border-collapse: collapse !important;
            }

                table.dataTable thead th, table.dataTable thead td {
                    border-bottom: 1px solid #19a4c6a3 !important;
                }

            .dataTables_wrapper .dataTables_paginate .paginate_button.current, .dataTables_wrapper .dataTables_paginate .paginate_button.current:hover {
                background-color: #428bca !important;
                border-color: #428bca !important;
                color: white !important;
            }

            .dataTables_wrapper .dataTables_paginate .paginate_button:hover {
                background-color: #428bca !important;
                border-color: #428bca !important;
                background: linear-gradient(to bottom, #428bca 0%, #428bca 100%);
                color: white !important;
                border: 1px solid #428bca !important;
            }

            div.dataTables_wrapper div.dataTables_filter input {
                margin-left: 0.5em;
                display: inline-block;
                width: 230px !important;
            }

            .dataTables_wrapper .dataTables_length, .dataTables_wrapper .dataTables_filter, .dataTables_wrapper .dataTables_info, .dataTables_wrapper .dataTables_processing, .dataTables_wrapper .dataTables_paginate {
                color: #428bca !important;
                font-weight: bold !important;
            }

            table.dataTable.table-hover > tbody > tr:hover > * {
                box-shadow: inset 0 0 0 9999px rgb(152 221 215 / 8%) !important;
            }

            .table-hover > tbody > tr:hover > td, .table-hover > tbody > tr:hover > th {
                background-color: aliceblue !important;
            }

            .card {
                border: 1.5px solid #b2ebf9b5 !important;
            }
    </style>
    <head>
        <body>
            <form id="form1" runat="server">
                <div class="row">
                    <div class="col-md-offset-4 col-md-4 sub-header">
                        Sales by Item
                    </div>
                    <div class="col-md-4 sub-header">
                        <span style="float: right"></span><span style="float: right; margin-right: 15px;">
                            <div id="reportrange" class="form-control mt-5 mb-5" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc;">
                                <i class="fa fa-calendar"></i>&nbsp;<span id="ordDate"></span><i class="fa fa-caret-down"></i>
                            </div>
                        </span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4 col-md-offset-4">
                        <h5 style="padding: 0px 0px 0px 0px;" id="date_details"></h5>
                    </div>
                </div>
                <div class="card">
                    <div class="card-body table-responsive">
                        <table class="table table-hover" id="ItemList">
                            <thead class="text-warning">
                                <tr>
                                    <th style="text-align: left" scope="col">Item Code</th>
                                    <th style="text-align: left">Item Name</th>
									<th style="text-align: left">Quantity Sold</th>
                                    <th style="text-align: right">Amount</th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                            <tfoot>
                            </tfoot>
                        </table>
                    </div>
                </div>
            </form>
        </body>

        <script type="text/javascript">
			var fdt ;
			var tdt;
            $(document).ready(function () {
                //$('#ItemList').DataTable();
                var AllOrders = []; var Orders = []; var Invoice_details = [];
                // loadData();
				const urlParams = new URLSearchParams(window.location.search);
     fdt = urlParams.get('FDate');
     tdt = urlParams.get('TDate');
const ordDateElement = $('#ordDate');
	function addLeadingZero(dateString) {
    const parts = dateString.split('-');
    if (parts.length === 3) {
        const day = parts[0];
        if (day.length === 1) {
            parts[0] = '0' + day;
        }
    }
    return parts.join('-');
}
    function updateOrdDateDisplay(fdt, tdt) {
        const formattedFdt = new Date(fdt).toLocaleDateString();
        const formattedTdt = new Date(tdt).toLocaleDateString();
        //ordDateElement.text(`${formattedFdt} - ${formattedTdt}`);
		//cb(formattedFdt, formattedTdt);
		const startDate = moment(addLeadingZero(formattedFdt), 'DD-MM-YYYY');
		
        const endDate = moment(addLeadingZero(formattedTdt), 'DD-MM-YYYY');
	 cb(startDate, endDate);
		//cb(moment(formattedFdt, 'DD-MM-YYYY'), moment(formattedTdt, 'DD-MM-YYYY'));
    }
    if (fdt && tdt) {
        loadData(fdt, tdt);
		 updateOrdDateDisplay(fdt, tdt);
    }
                $("#reportrange").on("DOMSubtreeModified", function () {
                    id = $('#ordDate').text(); 
                    id = id.split('-');
                     fdt = id[2].trim() + '-' + id[1] + '-' + id[0] + ' 00:00:00';
                     tdt = id[5] + '-' + id[4] + '-' + id[3].trim() + ' 00:00:00';
                    //loadData();
					loadData(fdt, tdt);
                });
            });
			function cb(start, end) {
                    $('#reportrange span').html(start.format('DD-MM-YYYY') + ' - ' + end.format('DD-MM-YYYY'));
                    $('#date_details').text(' From ' + start.format('DD/MM/YYYY') + ' To ' + end.format('DD/MM/YYYY'));

                }

               
            function ReloadTable() {
                var quanty = 0;
                var amtt = 0;
                $("#ItemList TBODY").html("");
                //$('#ItemList').DataTable().clear().destroy();
                for ($i = 0; Orders.length > $i; $i++) {
                    if ($i < Orders.length) {
                        tr = $("<tr></tr>");
                        //<td style='display:none';>" + Orders[$i].Product_Code + "</td>
                        $(tr).html("<td >" + Orders[$i].Product_Code + "</td><td>" + Orders[$i].Product_Name + "</td><td><a id='clc' href='SS_Rpt_Sales_By_Item_Detail.aspx?Pro_Code=" + Orders[$i].Product_Code + "&FDate=" + fdt + "&TDate=" + tdt + "&Pro_Name=" + Orders[$i].Product_Name + "'>" + Orders[$i].Qty + "</td><td align=\"right\">" + Orders[$i].Amt.toFixed() + "</td>");

                        quanty = parseFloat(Orders[$i].Qty || 0) + parseFloat(quanty)
                        amtt = parseFloat(Orders[$i].Amt) + parseFloat(amtt);
                        $("#ItemList TBODY").append(tr);
                    }
                }
                if (Orders.length > 0) {

                    $("#ItemList TFOOT").html("<tr><td colspan='2' style='font-weight: bold;'>Total</td><td>" + quanty + "</td><td style='align-content: right;text-align: right;font-weight: bold;'>" + amtt.toFixed(2) + "</td></tr>");
                }
                else {
                    $("#ItemList TFOOT").html("<h5>There were no sales during the selected date range.</h5>");
                }
                //$('#ItemList').DataTable({
                //    retrieve: true,
                //    scrollY: 200,
                //    scroller: true
                //});
            }

           function loadData(fdt, tdt) {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SS_Rpt_Sale_By_Item.aspx/Get_salebyitem_Count",
                    data: "{'FDT':'" + fdt + "','TDT':'" + tdt + "'}",
                    dataType: "json",
                    success: function (data) {
                        AllOrders = JSON.parse(data.d) || [];
                        Orders = AllOrders; ReloadTable();
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            }

        </script>

        <script type="text/javascript">
            $(function () {

                var start = moment();
                var end = moment();

                function cb(start, end) {
                    $('#reportrange span').html(start.format('DD-MM-YYYY') + ' - ' + end.format('DD-MM-YYYY'));
                    $('#date_details').text(' From ' + start.format('DD/MM/YYYY') + ' To ' + end.format('DD/MM/YYYY'));

                }

                $('#reportrange').daterangepicker({
                    startDate: start,
                    endDate: end,
                    ranges: {
                        'Today': [moment(), moment()],
                        'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                        'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                        'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                        'This Month': [moment().startOf('month'), moment().endOf('month')],
                        'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
                    }
                }, cb);

                cb(start, end);
            });
        </script>

    </head>
    </html>
</asp:Content>

