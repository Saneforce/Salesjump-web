<%@ Page Title="" Language="C#" MasterPageFile="~/Master_SS.master" AutoEventWireup="true" CodeFile="SS_Credit_Note_List.aspx.cs" Inherits="SuperStockist_Sales_CreditNote_SS_Credit_Note_List_aspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <style type="text/css">
            .ui-menu-item > a {
                display: block;
            }

            .fixed {
                position: fixed;
                width: 80%;
                bottom: 10px;
            }

            .example {
                display: contents;
                position: fixed;
                margin-left: 156px;
            }

            @media print {
                .hidden-print,
                .hidden-print * {
                    display: none !important;
                }
            }

            .spinner {
                margin: 100px auto;
                width: 50px;
                height: 40px;
                text-align: center;
                font-size: 10px;
            }

                .spinner > div {
                    background-color: #333;
                    height: 100%;
                    width: 6px;
                    display: inline-block;
                    -webkit-animation: sk-stretchdelay 1.2s infinite ease-in-out;
                    animation: sk-stretchdelay 1.2s infinite ease-in-out;
                }

                .spinner .rect2 {
                    -webkit-animation-delay: -1.1s;
                    animation-delay: -1.1s;
                }

                .spinner .rect3 {
                    -webkit-animation-delay: -1.0s;
                    animation-delay: -1.0s;
                }

                .spinner .rect4 {
                    -webkit-animation-delay: -0.9s;
                    animation-delay: -0.9s;
                }

                .spinner .rect5 {
                    -webkit-animation-delay: -0.8s;
                    animation-delay: -0.8s;
                }

            @-webkit-keyframes sk-stretchdelay {
                0%, 40%, 100% {
                    -webkit-transform: scaleY(0.4);
                }

                20% {
                    -webkit-transform: scaleY(1.0);
                }
            }

            @keyframes sk-stretchdelay {
                0%, 40%, 100% {
                    transform: scaleY(0.4);
                    -webkit-transform: scaleY(0.4);
                }

                20% {
                    transform: scaleY(1.0);
                    -webkit-transform: scaleY(1.0);
                }
            }

            .spinnner_div {
                width: 1200px;
                height: 1000px;
                background: rgba(255, 255, 255, 0.1);
                backdrop-filter: blur(2px);
                position: absolute;
                z-index: 100;
                overflow-y: hidden;
            }
        </style>
    <%-- loading div --%>
            <div class="spinnner_div" style="display: none;">
                <div class="spinner" style="position: absolute; left: 525px; top: 133px;">
                    <div class="rect1" style="background: #1a60d3;"></div>
                    <div class="rect2" style="background: #DB4437;"></div>
                    <div class="rect3" style="background: #F4B400;"></div>
                    <div class="rect4" style="background: #0F9D58;"></div>
                    <div class="rect5" style="background: orangered;"></div>
                </div>
            </div>
            <%-- loading div --%>
    <div class="row">
        <div class="col-lg-12 sub-header">
            Credit Note List <span style="float: right"><a href="#" class="btn btn-primary btn-update" id="newsorder">New Credit Note</a></span><span style="float: right; margin-right: 15px;"><div id="reportrange" class="form-control mt-5 mb-5" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc;">
                <i class="fa fa-calendar"></i>&nbsp;
        <span id="ordDate"></span><i class="fa fa-caret-down"></i>
            </div>
            </span>
        </div>
    </div>

    <div class="card">
        <div class="card-body table-responsive">
            <div style="white-space: nowrap">
                Search&nbsp;&nbsp;<input type="text" autocomplete="off" id="tSearchOrd" style="width: 250px;" />
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
                        <th>Sl No</th>
                        <th>Credit Note No</th>
                        <th>Credit Note Date</th>
                        <th>Invoice No</th>
                        <th>Customer Name</th>
                        <th style="text-align: right">Amount</th>
                        <th style="text-align: right">View</th>
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

    <script language="javascript" type="text/javascript">

        var stockist_Code = ("<%=Session["Sf_Code"]%>");
        var Div_Code = ("<%=Session["div_code"]%>");

        var AllOrders = []; NewOrd = []; RateDets = []; var arr = []; var fdt = ''; var tdt = ''; Prds = ""; var path = ''; var page = '';
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Credit_note_no1,Inv_No,Cus_Name,Amount,";
        var flag = 0;

        path = window.location.pathname;
        page = path.split("/").pop();

        $(document).on('click', '#newsorder', function () {
            window.location.href = "SS_Credit_Note.aspx";
        });

        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1;
        var yyyy = today.getFullYear();
        if (dd < 10) { dd = '0' + dd }
        if (mm < 10) { mm = '0' + mm }
        today = yyyy + '-' + mm + '-' + dd;



        $(".data-table-basic_length").on("change",
            function () {
                pgNo = 1;
                PgRecords = parseFloat($(this).val());
                ReloadTable();
            }
        );

        $("#reportrange").on("DOMSubtreeModified", function () {
            id = $('#ordDate').text();
            id = id.split('-');
            fdt = id[2].trim() + '-' + id[1] + '-' + id[0] + ' 00:00:00';
            tdt = id[5] + '-' + id[4] + '-' + id[3].trim() + ' 00:00:00';
            $(".data-table-basic_length").val(PgRecords);
            loadData();

            var Get_localData1 = localStorage.getItem("Date_Details");
            Get_localData1 = JSON.parse(Get_localData1);
            if (Get_localData1 != null) { if (Get_localData1[3] != '') { $('#tSearchOrd').val(Get_localData1[3]); search_fun(Get_localData1[3]); } else { $('#tSearchOrd').val(''); } }
        });

        function loadPgNos() {
             $('.spinnner_div').show();
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
             $('.spinnner_div').hide();
        }

        function ReloadTable() {
             $('.spinnner_div').show();
            var tota = 0;
            $("#OrderList TBODY").html("");
            st = PgRecords * (pgNo - 1); slno = 0;
            for ($i = st; $i < st + PgRecords; $i++) {
                if ($i < Orders.length) {
                    tr = $("<tr></tr>");
                    slno = $i + 1;
                    //$(tr).html("<td style='display:none'>" + Orders[$i].Cr_No + "</td><td>" + slno + "</td><td>" + Orders[$i].Credit_note_no1 + "</td><td>" + Orders[$i].cr_dat + "</td><td>" + Orders[$i].Inv_No + "</td><td>" + Orders[$i].Cus_Name + "</td><td align='right'>" + parseFloat(Orders[$i].Amount).toFixed(2) + "</td><td style='text-align:right;'><a href='Credit_Note_View.aspx?Order_No=" + Orders[$i].Cr_No + "&Stockist_Code=" + stockist_Code + "&Div_Code=" + Div_Code + "'><span class='glyphicon glyphicon-eye-open'></span></a></td>");
                    $(tr).html("<td style='display:none'>" + Orders[$i].Cr_No + "</td><td>" + slno + "</td><td>" + Orders[$i].Credit_note_no1 + "</td><td>" + Orders[$i].cr_dat + "</td><td>" + Orders[$i].Inv_No + "</td><td>" + Orders[$i].Cus_Name + "</td><td align='right'>" + parseFloat(Orders[$i].Amount).toFixed(2) + "</td><td style='text-align:right;'><a id='myButton' href='#' onClick='popup(\"" + Orders[$i].Cr_No + "\",\"" + stockist_Code + "\",\"" + Div_Code + "\")'><span class='glyphicon glyphicon-eye-open'></span></a></td>");
                    tota = parseFloat(Orders[$i].Order_Value || 0) + parseFloat(tota)
                    $("#OrderList TBODY").append(tr);
                }
            }
            // $("#OrderList TFOOT").html("<td></td><td style='font-weight: bold;'>Total</td><td></td><td style='align-content: right;text-align: right;font-weight: bold;'>" + tota.toFixed(2) + "</td><td></td><td></td><td></td>");
            $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")
            loadPgNos();
             $('.spinnner_div').hide();
        }

        function popup(orderid, stk, div) {

            namesArr = [];
            namesArr.push($('#reportrange span').text());
            namesArr.push(pgNo);
            namesArr.push($(".data-table-basic_length option:selected").text());
            namesArr.push($('#tSearchOrd').val());
            namesArr.push(page);
            window.localStorage.setItem("Date_Details", JSON.stringify(namesArr));
            var url = "../CreditNote/SS_Credit_Note_View.aspx?Order_No=" + orderid + "&Stockist_Code=" + stk + "&Div_Code=" + div + ""
            window.open(url, '_self');
        }

        $("#tSearchOrd").on("keyup", function () {
             $('.spinnner_div').show();
            if ($(this).val() != '') {
                search_fun($(this).val());
                namesArr = [];
                namesArr.push($('#reportrange span').text());
                namesArr.push(pgNo);
                namesArr.push($(".data-table-basic_length option:selected").text());
                namesArr.push($('#tSearchOrd').val());
                namesArr.push(page);
                window.localStorage.setItem("Date_Details", JSON.stringify(namesArr));
                pgNo = 1;
            }
            else {
                search_fun($(this).val());
                pgNo = 1;
                namesArr = [];
                namesArr.push($('#reportrange span').text());
                namesArr.push(pgNo);
                namesArr.push($(".data-table-basic_length option:selected").text());
                namesArr.push($('#tSearchOrd').val());
                window.localStorage.setItem("Date_Details", JSON.stringify(namesArr));

            }
             $('.spinnner_div').hide();
        })

        function search_fun(Search_text) {

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
        }

        function loadData() {
             $('.spinnner_div').show();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "SS_Credit_Note_List.aspx/Get_Credit_note",
                data: "{'FDt':'" + fdt + "','TDt':'" + tdt + "'}",
                dataType: "json",
                success: function (data) {
                     $('.spinnner_div').show();
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders; ReloadTable();
                     $('.spinnner_div').hide();
                },
                error: function (result) {
                     $('.spinnner_div').hide();
                    alert(JSON.stringify(result));
                }
            });
        }

    </script>

    <script type="text/javascript">
        $(function () {

            var Get_localData = localStorage.getItem("Date_Details");

            Get_localData = JSON.parse(Get_localData);

            if (Get_localData != "" && Get_localData != null && Get_localData[4] == page) {

                var Dates = Get_localData[0].split('-');

                var fdj = Dates[2].trim() + '-' + Dates[1] + '-' + Dates[0] + ' ' + ' 00:00:00';
                var nfgresd = Dates[5] + '-' + Dates[4] + '-' + Dates[3].trim() + ' ' + ' 00:00:00';

                pgNo = parseFloat(Get_localData[1]); PgRecords = parseFloat(Get_localData[2]); flag = '1';

                const utcDate = new Date(fdj);
                const utcDate1 = new Date(nfgresd);

                var start = utcDate;
                var end = utcDate1;

            }
            else {
                var start = moment();
                var end = moment();
            }

            function cb(start, end, flag) {

                if (flag == '1') {

                    var F_dat = start.getDate();
                    var F_dat1 = start.getMonth() + 1;
                    var F_dat2 = start.getFullYear();
                    var f_date3 = F_dat + '-' + F_dat1 + '-' + F_dat2;

                    var E_dat = end.getDate();
                    var E_dat1 = end.getMonth() + 1;
                    var E_dat2 = end.getFullYear();
                    var E_date3 = E_dat + '-' + E_dat1 + '-' + E_dat2;

                    $('#reportrange span').html(f_date3 + ' - ' + E_date3);
                }
                else {
                    pgNo = 1;
                    $('#reportrange span').html(start.format('DD-MM-YYYY') + ' - ' + end.format('DD-MM-YYYY'));
                }

                namesArr = [];
                namesArr.push($('#reportrange span').text());
                namesArr.push(pgNo);
                namesArr.push($(".data-table-basic_length option:selected").text());
                namesArr.push($('#tSearchOrd').val());
                namesArr.push(page);
                window.localStorage.setItem("Date_Details", JSON.stringify(namesArr));
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
            cb(start, end, flag);
        });
    </script>

</asp:Content>

