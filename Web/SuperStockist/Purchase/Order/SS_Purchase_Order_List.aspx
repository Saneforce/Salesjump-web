<%@ Page Title="" Language="C#" MasterPageFile="~/Master_SS.master" AutoEventWireup="true" CodeFile="SS_Purchase_Order_List.aspx.cs" Inherits="SuperStockist_Purchase_Order_SS_Purchase_Order_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <form id="frm1" runat="server">
        <%--<div class="row">            
                <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                        style="height:40px;width:40px;border-width:0px;position: absolute;right: 15px;top: 55px;" OnClick="ExportToExcel" />
        </div>--%>
        <div class="row">
            <style type="text/css">
                .green {
                    color: #28a745;
                }

                .red {
                    color: #f10b0b;
                }

                .edit_class {
                    pointer-events: none;
                    cursor: default;
                    text-decoration: line-through;
                    color: black;
                }

                .category {
                    border: none;
                    outline: none;
                    padding: 10px 16px;
                    background-color: #f1f1f1;
                    cursor: pointer;
                    font-size: 15px;
                }

                .active, .btn:hover {
                    background-color: #666;
                    color: white;
                }
            </style>

            <div class="col-lg-12 sub-header">
                Purchase Request<span style="float: right; margin-right: 53px;"><a href="../Order/SS_Purchase_Order.aspx" class="btn btn-primary btn" id="newsf">Add New</a></span><span style="float: right; margin-right: 15px;"><div id="reportrange" class="form-control mt-5 mb-5" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc;">
                    <i class="fa fa-calendar"></i>&nbsp;
        <span id="ordDate"></span><i class="fa fa-caret-down"></i>
                </div>
                </span>
                <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                    Style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 24px; top: -5px;" OnClick="ImageButton1_Click" />
            </div>
        </div>

        <div class="card">
            <div class="card-body table-responsive">
                <div style="white-space: nowrap">
                    Search&nbsp;&nbsp;<input type="text" autocomplete="off" id="tSearchOrd" style="width: 250px;" />
                    <label style="white-space: nowrap; margin-left: 57px; display: none;">Filter By&nbsp;&nbsp;<select id="txtfilter" name="ddfilter" style="width: 250px; display: none;"></select></label>
                    <label style="float: right">
                        Shows
                        <select class="data-table-basic_length" aria-controls="data-table-basic">
                            <option value="10">10</option>
                            <option value="25">25</option>
                            <option value="50">50</option>
                            <option value="100">100</option>
                        </select>
                        entries</label>
                   <%-- <div style="float: right; padding: 4px 0px 0px 0px;">
                        <ul class="segment">
                            <li data-va='All' class="active">ALL</li>
                            <li data-va='0'>Pending</li>
                            <li data-va='1'>Invioced</li>
                            <li data-va='2'>Cancelled</li>
                        </ul>
                    </div>--%>
                </div>
                <div id="excel_div">
                    <table class="table table-hover" id="OrderList">
                        <thead class="text-warning" style="white-space: nowrap;">
                            <tr>
                                <th>
                                    <input type="checkbox" name="checkAll" id="select_all" /></th>
                                <th style="text-align: left;">Sl NO</th>
                                <th style="text-align: left;">Order ID</th>
                                <th style="text-align: left;">Company Name</th>
                                <th style="text-align: left">Order Date</th>
                                <th>Status</th>
                                <th style="text-align: right">Total Amount</th>
                                <th style="text-align: right">Edit</th>
                                <th style="text-align: right">View</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                        <tfoot></tfoot>
                    </table>
                </div>
                <div class="row" style="padding: 5px 0px">
                    <div class="col-sm-5">
                        <div class="dataTables_info" id="orders_info" role="status" aria-live="polite">Showing 0 to 0 of 0 entries</div>
                    </div>
                    <div class="col-sm-7">
                        <div class="dataTables_paginate paging_simple_numbers" id="example2_paginate">
                            <ul class="pagination">
                                <li class="paginate_button previous" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">Previous</a></li>
                                <li class="paginate_button active"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">1</a></li>
                                <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="2" tabindex="0">Next</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript" src="../../../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../../../js/plugins/datatables/dataTables.bootstrap.js"></script>
    <script language="javascript" type="text/javascript">
        var stockist_Code = ("<%=Session["Sf_Code"]%>");
        var Div_Code = ("<%=Session["div_code"]%>");

        var AllOrders = [];
        var fdt = '';
        var tdt = '';
        var divcode; filtrkey = 'All';
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Order_No,Order_Value,CompanyName,Order_Date,";
        var flag = 0; var path = ''; var page = '';

        path = window.location.pathname;
        page = path.split("/").pop();


        $(".data-table-basic_length").on("change",
            function () {
                pgNo = 1;
                PgRecords = parseFloat($(this).val());
                ReloadTable(0);
            }
        );

        if (<%=Session["sf_type"]%>== 3) {
            $('#newsf').hide();
        }
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
            htmls = document.getElementById("excel_div").innerHTML;

            var ctx = {
                worksheet: 'Worksheet',
                table: htmls
            }
            var link = document.createElement("a");
            var tets = 'Primary_Order' + '.xls';   //create fname

            link.download = tets;
            link.href = uri + base64(format(template, ctx));
            link.click();
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
                pgNo = parseInt($(this).attr("data-dt-idx")); ReloadTable(0);
                /* $(".paginate_button").removeClass("active");
                 $(this).closest(".paginate_button").addClass("active");*/
            }
            );
        }
        function Edit_Screen(id, stk, div, flag, cust, order_date) {

            if (flag == 1) {
                alert('Order Invoiced');
                return false;
            }
            else {
                var url = "../Order/Purchase_Order_Edit.aspx?Order_No=" + id + "&Stockist_Code=" + stk + "&Div_Code=" + div + "&Cust_Code=" + cust + "&Order_Date=" + order_date + ""
                window.open(url, '_self');
            }
        }

        function ReloadTable(typ) {
            var tota = 0;
            $("#OrderList TBODY").html("");
            if (filtrkey != "All") {
                Orders = Orders.filter(function (a) {
                    return a.Order_Flag == filtrkey;
                })
            }
            if (typ == 1) { st = 0; } else { st = PgRecords * (pgNo - 1); slno = 0; }
            //st = PgRecords * (pgNo - 1); slno = 0;
            for ($i = st; $i < st + PgRecords; $i++) {

                if ($i < Orders.length) {
                    tr = $("<tr></tr>");
                    slno = $i + 1;
                    // $(tr).html("<td>" + slno + "</td><td>" + Orders[$i].Order_No + "</td><td>" + Orders[$i].CompanyName + "</td><td>" + Orders[$i].Order_Date_Disp +
                    //"</td><td>" + Orders[$i].free +
                    //"</td><td>" + Orders[$i].Dis +
                    //  "</td><td style='text-align: right;'>" + Orders[$i].Order_Value.toFixed(2) +
                    // "</td><td style='text-align: right;'><a href='Puchase_Order_View.aspx?Order_No=" + Orders[$i].Order_No + "&Stockist_Code=" + Orders[$i].stockist_Code + "&Div_Code=" + Div_Code + "'><span class='glyphicon glyphicon-eye-open'></span></a></td>");
                    //< td > <a href='Purchase_Order_Report_Details_View.aspx?Stockist_Code=" + Orders[$i].stockist_Code + "&Order_No=" + Orders[$i].Order_No + "' value=" + Orders[$i].total + " id='hplink'>" + Orders[$i].total + "</a> <input type='hidden' id='hid' value='" + Orders[$i].total + "'</ td>");

                    $(tr).html("<td><input type='hidden' class='Flag' value=" + Orders[$i].Order_Flag + "><input " + ((Orders[$i].Chk == '1') ? 'checked' : '') + " type='checkbox' class='rowCheckbox' id='SelectedCheckbox'/></td><td>" + slno + "</td><td>" + Orders[$i].Order_No + "</td><td>" + Orders[$i].CompanyName + "</td><td>" + Orders[$i].Order_Date_Disp +
                        "</td><td class='status'>" + Orders[$i].Status + "</td><td style='text-align: right;'>" + Orders[$i].Order_Value.toFixed(2) +"</td>"+
                        "<td style='text-align:right; cursor: pointer;'><a href='#' class ='" + ((Orders[$i].Order_Flag == 1 || Orders[$i].Order_Flag == 2 || Orders[$i].Order_Flag == 3) ? 'edit_class' : 'edit_class1') + "' id='Btn_Edit' onclick='Edit_Screen(\"" + Orders[$i].Order_No + "\",\"" + stockist_Code + "\",\"" + Div_Code + "\",\"" + Orders[$i].Order_Flag + "\" ,\"" + Orders[$i].Cust_Code + "\" ,\"" + Orders[$i].Order_Date + "\" )'</a>Edit</td>" +
                        "<td style = 'text-align: right;' > <a id='myButton' href='#' onClick='popup(\"" + Orders[$i].Order_No + "\",\"" + stockist_Code + "\",\"" + Div_Code + "\")'><span class='glyphicon glyphicon-eye-open'></span></a></td> ");

                    tota = parseFloat(Orders[$i].Order_Value || 0) + parseFloat(tota)
                    $("#OrderList TBODY").append(tr);
                    if (Orders[$i].Order_Flag == '1') { $('#OrderList tbody tr').find('.rowCheckbox').attr("disabled", true); }

                    if (Orders[$i].Status == 'Pending') {
                        $("td:contains('Pending')").addClass('red');
                    }
                    else if (Orders[$i].Status == 'Invoiced') {
                        $("td:contains('Invoiced')").addClass('green');
                    }
                    hq = [];
                }
            }
            $("#OrderList TFOOT").html("<td></td><td style='font-weight: bold;padding: 0px 0px 0px 8px;'>Total</td><td></td><td></td><td style='font-weight: bold;text-align: right;'>" + tota.toFixed(2) + "</td><td></td><td></td>");
            $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")
            $(".ddlStatus>li>a").on("click", function () {

                cStus = $(this).closest("td").find(".aState");
                stus = $(this).attr("v");
                order_id = $(this).closest("tr").find(".class_orderid").text();

                var fi_idx = Orders.findIndex(function (u) {
                    return (u.OrderNo == order_id);
                })
                cStusNm = $(this).text();
                cnfrmmsg = '';
                if (stus == 0) { cnfrmmsg = "Do you want to active the order?"; }
                else if (stus == 2) { cnfrmmsg = "Do you want to cancel the order ?"; }
                if (Orders[fi_idx].Status == "Invoiced") {
                    alert("Already Order Invoiced Cannot Change the Status");
                }
                else if (Orders[fi_idx].Status == "Pending" && stus == 0) {
                    alert("Already order is in pending");
                }
                else if (Orders[fi_idx].Status == "Cancelled" && stus == 2) {
                    alert("Already order is Cancelled");
                }
                else if (confirm(cnfrmmsg)) {
                    sf = Orders[fi_idx].OrderNo;
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "myOrders.aspx/SetNewStatus",
                        data: "{'SF':'" + sf + "','stus':'" + stus + "'}",
                        dataType: "json",
                        success: function (data) {

                            if (data.d == 1) {

                                if (stus == 0) {
                                    cStusNm = "Pending";
                                }
                                else if (stus == 2) {
                                    cStusNm = "Cancelled";
                                }
                                Orders[fi_idx].Status = cStusNm;
                                Orders[fi_idx].Order_Flag = stus;
                                $(cStus).html(cStusNm);

                                ReloadTable(0);
                                alert('Status Changed Successfully...');
                            }
                            else {
                                alert('Status Not Changed Successfully...');
                            }
                        },
                        error: function (result) {
                        }
                    });
                }
            });
            loadPgNos();
        }

        function popup(orderid, stk, div) {

            namesArr = [];
            namesArr.push($('#reportrange span').text());
            namesArr.push(pgNo);
            namesArr.push($(".data-table-basic_length option:selected").text());
            namesArr.push($('#tSearchOrd').val());
            namesArr.push(page);
            window.localStorage.setItem("Date_Details", JSON.stringify(namesArr));
            var url = "../Order/SS_Puchase_Order_View.aspx?Order_No=" + orderid + "&Stockist_Code=" + stk + "&Div_Code=" + div + ""
            window.open(url, '_self');
        }
        //$(".segment>li").on("click", function () {
        //    $(".segment>li").removeClass('active');
        //    $(this).addClass('active');
        //    filtrkey = $(this).attr('data-va');
        //    set_order();
        //    $("#tSearchOrd").val('');
        //    ReloadTable(0);
        //});
        $("#tSearchOrd").on("keyup", function () {
            if ($(this).val() != '') {
                pgNo = 1;
                search_fun($(this).val());
                namesArr = [];
                namesArr.push($('#reportrange span').text());
                namesArr.push(pgNo);
                namesArr.push($(".data-table-basic_length option:selected").text());
                namesArr.push($('#tSearchOrd').val());
                namesArr.push(page);
                window.localStorage.setItem("Date_Details", JSON.stringify(namesArr));
            }
            else {
                pgNo = 1;
                search_fun($(this).val());
                namesArr = [];
                namesArr.push($('#reportrange span').text());
                namesArr.push(pgNo);
                namesArr.push($(".data-table-basic_length option:selected").text());
                namesArr.push($('#tSearchOrd').val());
                namesArr.push(page);
                window.localStorage.setItem("Date_Details", JSON.stringify(namesArr));

            }
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
            ReloadTable(0);
        }

        function loadData() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "SS_Purchase_Order_List.aspx/GetOrders",
                data: "{'Stockist_Code':'" + stockist_Code + "','FDT':'" + fdt + "','TDT':'" + tdt + "'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders; ReloadTable(0);

                },
                error: function (result) {
                }
            });
        }


        var stockist_Code = ("<%=Session["Sf_Code"].ToString()%>");
        var Div_Code = ("<%=Session["div_code"].ToString()%>");
        loadData();


    </script>
    <script type="text/javascript">
        $(function () {

            var Get_localData = localStorage.getItem("Date_Details");

            Get_localData = JSON.parse(Get_localData);

            //if (Get_localData != "" && Get_localData != null && Get_localData[4] == page) {

            if (Get_localData != "" && Get_localData != null) {

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

