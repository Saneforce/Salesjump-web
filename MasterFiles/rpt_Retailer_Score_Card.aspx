<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="rpt_Retailer_Score_Card.aspx.cs" Inherits="rpt_Retailer_Score_Card" %>

<!DOCTYPE html>
<html lang="en" xmlns="https://www.w3.org/1999/xhtml">
    <head id="Head1">
        <title></title>
        
        <link href="css/font-awesome.min.css" rel="stylesheet" type="text/css" />       
        <link href="../css/style1.css" rel="stylesheet" type="text/css" />
        <link href="../css/bootstrap.min.css" type="text/css" rel="stylesheet" />

        <style type="text/css">
            #ddlsf_chzn {
                width: 300px !important;
                font-weight: 500;
            }

            #txtfilter_chzn {
                width: 300px !important;
                top: 10px;
            }

            .table {
                width: 100%;
                max-width: 100%;
                margin-bottom: 1rem;
            }

            .table th, 
            .table td {
                padding: 0.75rem;
                vertical-align: top;
                border-top: 1px solid #eceeef;
            }
            
            .table thead th {
                vertical-align: bottom;
                border-bottom: 2px solid #eceeef;
            }
            
            .table tbody + tbody {
                border-top: 2px solid #eceeef;
            }
            
            .table .table {
                background-color: #fff;
            }
            
            .table-sm th,
            .table-sm td {
                padding: 0.3rem;
            }

            .table-bordered {
                border: 1px solid #eceeef;
            }

            .table-bordered th,
            .table-bordered td {
                border: 1px solid #eceeef;
            }
            
            .table-bordered thead th,
            .table-bordered thead td {
                border-bottom-width: 2px;
            }

            .table-striped tbody tr:nth-of-type(odd) {
                background-color: rgba(0, 0, 0, 0.05);
            }

            .table-hover tbody tr:hover {
                background-color: rgba(0, 0, 0, 0.075);
            }

            .table-active,
            .table-active > th,
            .table-active > td {
                background-color: rgba(0, 0, 0, 0.075);
            }

            .table-hover .table-active:hover {
                background-color: rgba(0, 0, 0, 0.075);
            }
            .table-hover .table-active:hover > td,
            .table-hover .table-active:hover > th {
                background-color: rgba(0, 0, 0, 0.075);
            }

            .table-success,
            .table-success > th,
            .table-success > td {
                background-color: #dff0d8;
            }

            .table-hover .table-success:hover {
                background-color: #d0e9c6;
            }
            .table-hover .table-success:hover > td,
            .table-hover .table-success:hover > th {
                background-color: #d0e9c6;
            }

            .table-info,
            .table-info > th,
            .table-info > td {
                background-color: #d9edf7;
            }

            .table-hover .table-info:hover {
                background-color: #c4e3f3;
            }
            
            .table-hover .table-info:hover > td,
            .table-hover .table-info:hover > th {
                background-color: #c4e3f3;
            }

            .table-warning,
            .table-warning > th,
            .table-warning > td {
                background-color: #fcf8e3;
            }

            .table-hover .table-warning:hover {
                background-color: #faf2cc;
            }

            .table-hover .table-warning:hover > td,
            .table-hover .table-warning:hover > th {
                background-color: #faf2cc;
            }

            .table-danger,
            .table-danger > th,
            .table-danger > td {
                background-color: #f2dede;
            }

            .table-hover .table-danger:hover {
                background-color: #ebcccc;
            }
            
            .table-hover .table-danger:hover > td,
            .table-hover .table-danger:hover > th {
                background-color: #ebcccc;
            }

            .thead-inverse th {
                color: #fff;
                background-color: #292b2c;
            }

            .thead-default th {
                color: #464a4c;
                background-color: #eceeef;
            }

            .table-inverse {
                color: #fff;
                background-color: #292b2c;
            }

            .table-inverse th,
            .table-inverse td,
            .table-inverse thead th {
                border-color: #fff;
            }
            
            .table-inverse.table-bordered {
                border: 0;
            }

            .table-responsive {
                display: block;
                width: 100%;
                overflow-x: auto;
                -ms-overflow-style: -ms-autohiding-scrollbar;
            }
            
            .table-responsive.table-bordered {
                border: 0;
            }
            
            th {
                white-space: nowrap;
                cursor: pointer;
            }

            .yourclass{
                font-size:50px;
            }
        </style>
        
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.0.2/jquery.min.js"></script>
        <script src="../js/lib/xls.core.min.js" type="text/javascript"></script>
        <script src="../js/lib/xlsx.core.min.js" type="text/javascript"></script>
        <script src="../js/lib/import_data.js" type="text/javascript"></script>

        <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
        <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>   
    </head>
    <body>
        <form id="frm1" runat="server">
            <br />
            <div>
                <asp:Label ID="lblHead" Text="Retailer Score Card Detail" SkinID="lblMand" Font-Bold="true" Font-Underline="true" CssClass="yourclass"
                    runat="server"></asp:Label>
            </div>
            <br />            
            <div class="card" style="margin-left:1% !important;border:1px solid black;">
                <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                    Style=" height: 10%; float:right;  width: 5%;padding:15px;  margin-right: 15px;" OnClick="lnkDownload_Click" />
                <div class="card-body table-responsive">
                    <br />
                    <div style="white-space: nowrap">Search&nbsp;&nbsp;<input type="text" id="tSearchOrd" style="width: 250px;" />
                        <label style="float: right">
                            Show
                            <select class="data-table-basic_length" aria-controls="data-table-basic">
                                <option value="1000">1000</option>
                                <option value="1500">1500</option>
                                <option value="2000">2000</option>
                                <option value="2500">2500</option>
                            </select>entries</label>
                    </div>
                    <br />
                    <table class="table table-hover table-responsive" id="OrderList" style="font-size: 12px">
                        <thead class="text-warning">
                            <tr>
                                <th style="text-align: left">S.No</th>
                                <th id="Sf_Name" style="text-align: left">Field Force Name</th>
                                <th id="DistributorName" style="text-align: left">Distributor Name</th>
                                <th id="Retailer_Name" style="text-align: left">Outlet Name</th>
                                <th id="Month_sales" style="text-align: left">Month Sales Value</th>
                                <th id="sku" style="text-align: left">No of Sku Sold</th>
                                <th id="Visit" style="text-align: left">No of Visit</th>                                    
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                    <div class="row">
                        <div class="col-md-5">
                            <div class="dataTables_info" id="orders_info" role="status" aria-live="polite">Showing 0 to 0 of 0 entries</div>
                        </div>
                        <div class="col-md-7">
                            <div class="dataTables_paginate paging_simple_numbers" id="example2_paginate">
                                <ul class="pagination" style="float: right;">
                                    <li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">First</a></li>
                                    <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="7" tabindex="0">Last</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>            
        </form>
        <script type="text/javascript">

            var Retailer_Score_Card_Details = []; var Retailer_Score_Card_Details_Visit = [];
            var AllOrders = [];
            var sortid = '';
            var asc = true;
            var Orders = []; pgNo = 1; PgRecords = 1000; TotalPg = 0; searchKeys = "Sf_Name,DistributorName,Retailer_Name,Month_sales,sku,Visit";
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
                spg = '<li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">First</a></li>';
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
                });
            }

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
                ReloadTable();
            });

            function loadData() {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "rpt_Retailer_Score_Card.aspx/Retailer_Score_Card",
                    data: "",
                    dataType: "json",
                    success: function (data) {
                        AllOrders = JSON.parse(data.d) || [];
                        console.log(AllOrders);
                        Retailer_Score_Card_Details = JSON.parse(data.d) || [];
                        Orders = AllOrders;
                        ReloadTable();
                    },
                    error: function (result) {
                        //alert(JSON.stringify(result));
                    }
                });
            }

            function getRScoreCardDetails() {
                return $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    url: "rpt_Retailer_Score_Card.aspx/Retailer_Score_Card",
                    dataType: "json",
                    success: function (data) {
                        Retailer_Score_Card_Details = JSON.parse(data.d) || [];
                    },
                    error: function (result) {
                        //alert(JSON.stringify(result));
                    }
                });
            }

            function ReloadTable() {

                var tr = ''; let total = 0;
                $("#OrderList TBODY").html("");

                st = PgRecords * (pgNo - 1);
                for ($i = st; $i < st + Number(PgRecords); $i++) {
                    if ($i < Orders.length) {

                        tr = $("<tr rname='" + Orders[$i].Sf_Name + "'  rocode='" + Orders[$i].DistributorName + "'></tr>");


                        $(tr).html("<td>" + ($i + 1) + "</td>" +
                            "<td>" + Orders[$i].Sf_Name + "</td>" +
                            "<td class='rocount'><a href='#'>" + Orders[$i].DistributorName + "</a></td>" +
                            "<td class='rodeact'><a href='#'>" + Orders[$i].Retailer_Name + "</a></td>" +
                            "<td>" + Orders[$i].Month_sales + "</td><td>" + Orders[$i].sku + "</td>" +
                            
                            "<td>" + Orders[$i].Visit + "</td>");

                        total += Orders[$i].Month_sales;
                        $("#OrderList TBODY").append(tr);
                    }
                }
                //$('#OrderList tfoot th').append('');
                //$('#OrderList tfoot th').append("Grand Total: " + total.toFixed(3) + "  ");
                $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")
                loadPgNos();
            }

            $(document).ready(function () {

                loadData(); getRScoreCardDetails();

                $('#txtfilter').on('change', function () {
                    var sfs = $(this).val();
                    var hqn = $('#txtfilter_chosen a').text().trim();
                    if (sfs != 0) {
                        Orders = AllOrders;
                        Orders = Orders.filter(function (a) {
                            return a.Territory_name == sfs || a.Sf_Name == sfs || a.DistributorName == sfs || a.Retailer_Name == sfs;
                        });
                    }
                    else {
                        Orders = AllOrders;
                    }
                    ReloadTable();
                });
            });

            //$.ajax({
            //    type: "POST",
            //    contentType: "application/json; charset=utf-8",
            //    async: false,
            //    url: "rpt_Retailer_Score_Card.aspx/Retailer_Score_Card_Visit",
            //    dataType: "json",
            //    success: function (data) {
            //        Retailer_Score_Card_Details_Visit = JSON.parse(data.d);
            //    },
            //    error: function (result) {
            //    }
            //});


            //$.ajax({
            //    type: "POST",
            //    contentType: "application/json; charset=utf-8",
            //    async: false,
            //    url: "rpt_Retailer_Score_Card.aspx/Retailer_Score_Card",
            //    dataType: "json",
            //    success: function (data) {
            //        Retailer_Score_Card_Details = JSON.parse(data.d);
            //        if (Retailer_Score_Card_Details.length > 0) {
            //            $('#Retailer_score_card tbody').html('');
            //            for (var t = 0; t < Retailer_Score_Card_Details.length; t++) {

            //                var filtered_Visit = Retailer_Score_Card_Details_Visit.filter(function (u) {
            //                    return (u.Trans_Detail_Info_Code == Retailer_Score_Card_Details[t].Cust_Code);
            //                });
            //                slno = t + 1;
            //                $("#Retailer_score_card tbody").append("<tr><td>" + slno + "</td><td>" + Retailer_Score_Card_Details[t].Sf_Name + "</td><td>" + Retailer_Score_Card_Details[t].DistributorName + "</td><td>" + Retailer_Score_Card_Details[t].Retailer_Name + "</td><td>" + parseFloat(Retailer_Score_Card_Details[t].Month_sales).toFixed(2) + "</td><td>" + Retailer_Score_Card_Details[t].sku + "</td><td>" + ((filtered_Visit.length > 0) ? filtered_Visit[0].Visit : '0') + "</td></tr>");
            //            }
            //        }
            //        else {
            //            $("#Retailer_score_card").append('<tfoot><tr><th colspan="5"></th></tr></tfoot>');
            //        }
            //    },
            //    error: function (result) {
            //    }
            //});
            </script>
    </body>
</html>

