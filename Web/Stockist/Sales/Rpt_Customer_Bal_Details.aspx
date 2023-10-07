<%@ Page Title="" Language="C#" MasterPageFile="~/Master_DIS.master" AutoEventWireup="true" CodeFile="Rpt_Customer_Bal_Details.aspx.cs" Inherits="Stockist_Sales_Rpt_Customer_Bal_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <html>

    <head>
        <title>Sales By Customer Details</title>
        <%--<link href="../../datatable/DataTables-1.13.1/css/dataTables.dataTables.min.css" rel="stylesheet" />--%>
        <link href="../../datatable/DataTables-1.13.1/css/jquery.dataTables.min.css" rel="stylesheet" />
        <link href="../../datatable/DataTables-1.13.1/css/dataTables.bootstrap.min.css" rel="stylesheet" />
        <link href="../../datatable/Responsive-2.4.0/css/responsive.bootstrap.min.css" rel="stylesheet" />
        <link href="../../datatable/Responsive-2.4.0/css/responsive.dataTables.min.css" rel="stylesheet" />
        <link href="../../datatable/Responsive-2.4.0/css/responsive.jqueryui.min.css" rel="stylesheet" />
        <link href="../../datatable/SearchPanes-2.1.0/css/searchPanes.bootstrap.css" rel="stylesheet" />
        <link href="../../datatable/SearchPanes-2.1.0/css/searchPanes.dataTables.min.css" rel="stylesheet" />
        <link href="../../datatable/SearchPanes-2.1.0/css/searchPanes.jqueryui.min.css" rel="stylesheet" />
        <script src="../../datatable/DataTables-1.13.1/js/dataTables.bootstrap.min.js"></script>
        <script src="../../datatable/DataTables-1.13.1/js/dataTables.dataTables.js"></script>
        <script src="../../datatable/DataTables-1.13.1/js/jquery.dataTables.min.js"></script>
        <script src="../../datatable/JSZip-2.5.0/jszip.min.js"></script>
        <script src="../../datatable/pdfmake-0.1.36/pdfmake.min.js"></script>
        <script src="../../datatable/Responsive-2.4.0/js/dataTables.responsive.min.js"></script>
        <script src="../../datatable/Responsive-2.4.0/js/responsive.dataTables.min.js"></script>
        <script src="../../datatable/Responsive-2.4.0/js/responsive.jqueryui.min.js"></script>
        <script src="../../datatable/SearchPanes-2.1.0/js/dataTables.searchPanes.min.js"></script>
        <script src="../../datatable/SearchPanes-2.1.0/js/searchPanes.dataTables.min.js"></script>
        <script src="../../datatable/SearchPanes-2.1.0/js/searchPanes.jqueryui.min.js"></script>
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
    </head>

    <body>
        <form id="form1" runat="server">

            <div class="row">
                <div class="col-md-4">
                    <span class="glyphicon glyphicon-chevron-left" style="color: #5a89e0;"></span>
                    <a href="Rpt_Customer_Bal.aspx" style="font-size: 17px;">Customer Balance </a>
                </div>

                <div class="col-md-4 sub-header" style="text-align: center">
                    <asp:Label Style="font-size: 14px;" ID="Tit" runat="server"></asp:Label>
                </div>

                <div class="col-md-4">
                    <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                        Style="height: 40px; width: 40px; border-width: 0px; right: 15px; top: 43px;" OnClick="ImageButton1_Click" />
                </div>

            </div>

            <div class="row">
                <div class="col-md-4 col-md-offset-4">
                    <asp:Label ID="date_details" runat="server"></asp:Label>
                </div>
            </div>

            <div class="card">
                <div class="card-body table-responsive">
                    <div class="table-scroll">
                        <table class="table table-hover" id="sales_man_Details">
                            <thead class="text-warning">
                                <tr>
                                    <th id="Dt" style="text-align: left">Date</th>
                                    <th id="Ty" style="text-align: left">Type</th>
                                    <th style="text-align: left">Invoice No</th>
                                    <%-- <th id="sts" style="text-align: left">Status</th>--%>
                                    <th style="text-align: left">Credit</th>
                                    <th style="text-align: left">Debit</th>
                                    <th style="text-align: right">Balance Due</th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                            <tfoot>
                            </tfoot>
                        </table>
                    </div>
                </div>
            </div>
        </form>
    </body>
    </html>

    <style type="text/css">
        .green {
            color: #28a745;
        }

        .yellow {
            color: #e8a11e;
        }

        .red {
            color: #ff8080;
        }

        .blue {
            color: #80bfff;
        }

        .table-scroll {
            position: relative;
            width: 100%;
            z-index: 1;
            margin: auto;
            overflow: auto;
            height: 550px;
        }

            .table-scroll table {
                width: 100%;
                /*min-width: 1280px;*/
                margin: auto;
                border-collapse: separate;
                border-spacing: 0;
            }

        .table-wrap {
            position: relative;
        }

        .table-scroll th,
        .table-scroll td {
            padding: 5px 10px;
            /*border: 1px solid #000;
            background: #fff;*/
            vertical-align: top;
        }

        .table-scroll thead th {
            background: white;
            color: black;
            position: -webkit-sticky;
            position: sticky;
            top: 0;
        }
        /* safari and ios need the tfoot itself to be position:sticky also */
        .table-scroll tfoot,
        .table-scroll tfoot th,
        .table-scroll tfoot td {
            position: -webkit-sticky;
            position: sticky;
            bottom: 0;
            /*background: #666;
            color: #fff;*/
            background: white;
            color: black;
            z-index: 4;
        }


        /* testing links*/

        th:first-child {
            position: -webkit-sticky;
            position: sticky;
            left: 0;
            z-index: 2;
            /*background: #ccc;*/
        }

        thead th:first-child,
        tfoot th:first-child {
            z-index: 5;
        }
    </style>

    <script type="text/javascript">

        $(document).ready(function () {
            var sortid = ''; var asc = true; var AllOrders = []; var Orders = []; var Item_details = [];
            $('#sales_man_Details').DataTable();
            loadData();

            function loadData() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Rpt_Customer_Bal_Details.aspx/Get_cust_bal_details",
                    dataType: "json",
                    success: function (data) {
                        Item_details = JSON.parse(data.d);
                        Orders = Item_details;
                        Bind_Details();
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            }

            function Bind_Details() {

                var Total_bal = 0; var Total_Credit = 0; var Total_Debit = 0;

                $("#sales_man_Details tbody").html('');
                $('#sales_man_Details').DataTable().clear().destroy();
                var tbalamt = 0;
                for ($i = 0; Item_details.length > $i; $i++) {

                    if ($i < Item_details.length) {

                        tr = $("<tr></tr>");

                        if (Item_details[$i].Ty == 'Opening') {
                            Total_bal += (parseFloat(Item_details[$i].Credit) - parseFloat(Item_details[$i].Debit)) * -1;
                            //<td style='display:none';>" + Item_details[$i].cust_id + "</td>
                            $(tr).html("<td>" + Item_details[$i].Dt + "</td><td>" + Item_details[$i].Ty + "</td><td>" + Item_details[$i].Num + "</td><td>" + parseFloat(Item_details[$i].Credit).toFixed(2) + "</td><td>" + parseFloat(Item_details[$i].Debit1).toFixed(2) + "</td><td style='text-align:right;'>" + parseFloat(Total_bal).toFixed(2) + "</td>");
                            $("#sales_man_Details tbody").append($(tr));
                            Total_Credit = parseFloat(Item_details[$i].Credit || 0) + parseFloat(Total_Credit)
                            Total_Debit = parseFloat(Item_details[$i].Debit1) + parseFloat(Total_Debit);
                        }
                        else {
                            Total_bal += (parseFloat(Item_details[$i].Credit) - parseFloat(Item_details[$i].Debit)) * -1;
                            //<td style='display:none';>" + Item_details[$i].cust_id + "</td>
                            $(tr).html("<td>" + Item_details[$i].Dt + "</td><td>" + Item_details[$i].Ty + "</td><td>" + Item_details[$i].Num + "</td><td>" + parseFloat(Item_details[$i].Credit).toFixed(2) + "</td><td>" + parseFloat(Item_details[$i].Debit).toFixed(2) + "</td><td style='text-align:right;'>" + parseFloat(Total_bal).toFixed(2) + "</td>");
                            $("#sales_man_Details tbody").append($(tr));
                            Total_Credit = parseFloat(Item_details[$i].Credit || 0) + parseFloat(Total_Credit)
                            Total_Debit = parseFloat(Item_details[$i].Debit) + parseFloat(Total_Debit);
                        }

                        //if (Item_details[$i].sts == 'Paid') {
                        //    $("td:contains('Paid')").addClass('green');
                        //}
                        //else if (Item_details[$i].sts == 'Partially Paid') {
                        //    $("td:contains('Partially Paid')").addClass('yellow');
                        //}
                        //else if (Item_details[$i].sts == 'Pending') {
                        //    $("td:contains('Pending')").addClass('red');
                        //}
                    }
                    $("#sales_man_Details TFOOT").html("<tr><td style='font-weight: bold;'>Total</td><td colspan='2'></td><td style='font-weight: bold;'>" + Total_Credit.toFixed(2) + "</td><td style='font-weight: bold;'>" + Total_Debit.toFixed(2) + "</td><td style='font-weight: bold; text-align:right'>" + Total_bal.toFixed(2) + "</td></tr>");
                    //<td style='font-weight: bold;'>" + tot_adv_amt.toFixed(2) + "</td><td style='font-weight: bold;'>" + tot_adj_amt.toFixed(2) + "</td>
                }
                $('#sales_man_Details').DataTable({
                    retrieve: true,
                    scrollY: 300,
                    scroller: true
                });
            }

            function Bind_Details1() {

                var Total_bal = 0; var Total_Credit = 0; var Total_Debit = 0;

                $("#sales_man_Details tbody").html('');
                $("#sales_man_Details tbody").append('<tr style="display:none"></tr>');
                var tbalamt = 0;
                for ($i = Item_details.length - 1; $i <= Item_details.length; $i--) {

                    if ($i < Item_details.length) {

                        var trs = $("#sales_man_Details tbody").find('tr');
                        tr = $("<tr></tr>");
                        Total_bal += (parseFloat(Item_details[$i].Credit) - parseFloat(Item_details[$i].Debit)) * -1;
                        //<td style='display:none';>" + Item_details[$i].cust_id + "</td>
                        $(tr).html("<td>" + Item_details[$i].Dt + "</td><td>" + Item_details[$i].Ty + "</td><td>" + Item_details[$i].Num + "</td><td>" + parseFloat(Item_details[$i].Credit).toFixed(2) + "</td><td>" + parseFloat(Item_details[$i].Debit).toFixed(2) + "</td><td style='text-align:right;'>" + parseFloat(Total_bal).toFixed(2) + "</td>");
                        $(trs[0]).before(tr);

                        Total_Credit = parseFloat(Item_details[$i].Credit || 0) + parseFloat(Total_Credit)
                        Total_Debit = parseFloat(Item_details[$i].Debit) + parseFloat(Total_Debit);

                        //if (Item_details[$i].sts == 'Paid') {
                        //    $("td:contains('Paid')").addClass('green');
                        //}
                        //else if (Item_details[$i].sts == 'Partially Paid') {
                        //    $("td:contains('Partially Paid')").addClass('yellow');
                        //}
                        //else if (Item_details[$i].sts == 'Pending') {
                        //    $("td:contains('Pending')").addClass('red');
                        //}
                    }
                    $("#sales_man_Details TFOOT").html("<tr><td style='font-weight: bold;'>Total</td><td colspan='2'></td><td style='font-weight: bold;'>" + Total_Credit.toFixed(2) + "</td><td style='font-weight: bold;'>" + Total_Debit.toFixed(2) + "</td><td style='font-weight: bold; text-align:right'>" + Total_bal.toFixed(2) + "</td></tr>");
                    //<td style='font-weight: bold;'>" + tot_adv_amt.toFixed(2) + "</td><td style='font-weight: bold;'>" + tot_adj_amt.toFixed(2) + "</td>
                }
                $('#sales_man_Details').DataTable({
                    retrieve: true,
                    scrollY: 300,
                    scroller: true
                });
            }

            $('th').on('click', function () {
                sortid = this.id;
                asc = $(this).attr('asc');
                if (asc == undefined) asc = 'true';
                Orders.sort(function (a, b) {
                    if (parseInt(a[sortid].toLowerCase()), 10 < parseInt(b[sortid].toLowerCase()), 10 && asc == 'true')
                        return -1;
                    if (parseInt(a[sortid].toLowerCase()), 10 > parseInt(b[sortid].toLowerCase()), 10 && asc == 'true')
                        return 1;

                    if (parseInt(b[sortid].toLowerCase()), 10 < parseInt(a[sortid].toLowerCase()), 10 && asc == 'false')
                        return -1;
                    if (parseInt(b[sortid].toLowerCase()), 10 > parseInt(a[sortid].toLowerCase()), 10 && asc == 'false')
                        return 1;
                    return 0;

                });

                $(this).attr('asc', ((asc == 'true') ? 'false' : 'true'));

                if ($(this).attr('asc') == 'false') { Bind_Details1(); } else { Bind_Details(); }
            });
        });

    </script>


</asp:Content>

