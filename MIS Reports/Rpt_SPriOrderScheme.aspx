<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Rpt_SPriOrderScheme.aspx.cs" Inherits="MIS_Reports_Rpt_SPriOrderScheme" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
    <form id="Form1" runat="server">
        <div class="overlay" id="loadover" style="display: none;">
            <div id="loader"></div>
        </div>
        <div class="row">
            <div class="col-lg-12 sub-header">
                Super Stockist Scheme<span style="float: right; margin-right: 15px;">
                    <div id="reportrange" class="form-control mt-5 mb-5" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc;">
                        <i class="fa fa-calendar"></i>&nbsp;
               
                        <span id="ordDate"></span><i class="fa fa-caret-down"></i>
                    </div>
                    <img style="cursor: pointer; width: 40px; height: 40px; float: right;" alt="" onclick="exportToExcel()" src="../img/Excel-icon.png" /></span>
            </div>
            <div class="col-lg-12">
                <label>Field Force  :</label>
                <select id="ddlsf" style="max-width: 150px; min-width: 150px;"></select>
            </div>
        </div>
        <div class="card">
            <div class="card-body table-responsive">
                <div style="white-space: nowrap">
                    Search&nbsp;&nbsp;<input type="text" id="tSearchOrd" style="width: 250px;" />
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
                <table class="table table-hover" id="OrderList" style="font-size: 12px">
                    <thead class="text-warning">
                        <tr>
                            <th style="text-align: left">Sl.No</th>
                            <th id="Date" style="text-align: left">Date</th>
                            <th id="Ret_Nm" style="text-align: left">Distributor Name</th>
                            <th id="Ord_No" style="text-align: left">Order No.</th>
                            <th id="Prd" style="text-align: left">Product</th>
                            <th id="Schm_Prd" style="text-align: left">Scheme Product</th>
                            <th id="Ord_Qty" style="text-align: left">Order Qty.</th>
                            <th id="Free_Qty" style="text-align: left">Free Qty.</th>
                            <th id="Discount" style="text-align: left">Discount</th>
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
                                <li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">First</a></li>
                                <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="7" tabindex="0">Last</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <style>
            #loader {
                position: absolute;
                left: 50%;
                top: 50%;
                z-index: 1;
                width: 120px;
                height: 120px;
                margin: -76px 0 0 -76px;
                border: 16px solid #f3f3f3;
                border-radius: 50%;
                border-top: 16px solid #3498db;
                -webkit-animation: spin 2s linear infinite;
                animation: spin 2s linear infinite;
            }

            .overlay {
                background-color: #EFEFEF;
                position: fixed;
                width: 100%;
                height: 100%;
                z-index: 1000;
                top: 0px;
                left: 0px;
                opacity: .5; /* in FireFox */
                filter: alpha(opacity=50); /* in IE */
            }
        </style>
        <script type="text/javascript">
            var AllOrders = []; var Orders = []; var FDT = ''; var TDT = ''; searchKeys = 'Order_Date,Order_No,Product_Name,Offer_ProductNm,Stockist_Name,';
            var pgNo = 1; PgRecords = 10; TotalPg = 0;
            $(document).ready(function () {
                $("#reportrange").on("DOMSubtreeModified", function () {
                    id = $('#ordDate').text();
                    id = id.split('-');
                    FDT = id[2].trim() + '-' + id[1] + '-' + id[0];
                    TDT = id[5] + '-' + id[4] + '-' + id[3].trim();
                    //$('#ddlsf').val(0).trigger('chosen:updated').css("width", "100%");
                    $('#loadover').show();
                    setTimeout(function () {
                        setTimeout(loadData(), 500);
                    }, 500);
                });
                fillsf()
                $('#ddlsf').on('change', function () {
                    sf = $(this).val();
                    Orders = AllOrders.filter(function (a) {
                        return a.Sf_Code == sf;
                    });
                    ReloadTable();
                });
            });

            $(".data-table-basic_length").on("change",
                function () {
                    pgNo = 1;
                    PgRecords = $(this).val();
                    ReloadTable();
                }
            );
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
                    data: "{'Fdt':'" + FDT + "','Tdt':'" + TDT + "'}",
                    url: "Rpt_SPriOrderScheme.aspx/GetSprStk_Schm",
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

            function exportToExcel() {
                var htmls = "";
                var uri = 'data:application/vnd.ms-excel;base64,';
                var template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>';
                var base64 = function (s) {
                    return window.btoa(unescape(encodeURIComponent(s)))
                };
                var format = function (s, c) {
                    return s.replace(/{(\w+)}/g, function (m, p) {
                        return c[p];
                    })
                };
                htmls = document.getElementById("OrderList").innerHTML;

                var ctx = {
                    worksheet: 'Worksheet',
                    table: htmls
                }
                var link = document.createElement("a");
                var tets = 'Report_Super_Stockist_Scheme' + '.xls';   //create fname

                link.download = tets;
                link.href = uri + base64(format(template, ctx));
                link.click();
            }
            function ReloadTable() {
                $("#OrderList TBODY").html("");
                st = PgRecords * (pgNo - 1); slno = 0;

                for ($i = st; $i < st + PgRecords; $i++) {
                    if ($i < Orders.length) {
                        tr = $("<tr></tr>");
                        $(tr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].Order_Date + "</td><td>" + Orders[$i].Stockist_Name + "</td><td>" + Orders[$i].Order_No + "</td><td>" + Orders[$i].Product_Name + "</td><td>" + Orders[$i].Offer_Product_Name + "</td><td>" + Orders[$i].CQty + "</td><td>" + Orders[$i].Free + "</td><td>" + Orders[$i].Discount + "</td>");
                        $("#OrderList TBODY").append(tr);
                    }
                }
                $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries");
                loadPgNos();
                $('#loadover').hide();
            }
            function fillsf() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Rpt_SPriOrderScheme.aspx/GetSF",
                    data: "{'Div':'<%=Session["div_Code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        var sfdets = JSON.parse(data.d) || [];
                        var ddsf = $('#ddlsf');
                        $('#ddlff').selectpicker('destroy');
                        ddsf.empty().append('<option value="0">Select Employee</option>');
                        ddsf.append('<option value="admin">All</option>');
                        for (var i = 0; i < sfdets.length; i++) {
                            ddsf.append($('<option value="' + sfdets[i].Sf_Code + '">' + sfdets[i].Sf_Name + '</option>'));
                        }
                    },
                    error: function (result) {
                    }
                });
                $('#ddlsf').selectpicker({
                    liveSearch: true
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
                    if ('<%=Session["div_code"]%>' == '4' || '<%=Session["div_code"].ToString()%>' == '106' || '<%=Session["div_code"].ToString()%>' == '107' || '<%=Session["div_code"].ToString()%>' == '109') {
                        $('.hrts').show();
                        $('#OrderList>tbody>tr>.rts').show();
                    }
                    /* $(".paginate_button").removeClass("active");
                     $(this).closest(".paginate_button").addClass("active");*/
                }
                );
            }
            $(function () {

                var start = moment();
                var end = moment();

                function cb(start, end) {
                    $('#reportrange span').html(start.format('DD-MM-YYYY') + ' - ' + end.format('DD-MM-YYYY'));
                    //$('#date_details').text(' From ' + start.format('DD/MM/YYYY') + ' To ' + end.format('DD/MM/YYYY')); 

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
    </form>
</asp:Content>
