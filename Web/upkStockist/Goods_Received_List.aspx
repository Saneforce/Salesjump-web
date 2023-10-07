<%@ Page Title="Goods Received List" Language="C#" MasterPageFile="~/Master_DIS.master"
    AutoEventWireup="true" CodeFile="Goods_Received_List.aspx.cs" Inherits="MasterFiles_Goods_Received_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="frm1" runat="server">


        <div class="row">
            <div class="col-lg-12 sub-header">
                Purchase GRN<span style="float: right;"><a href="../Stockist/Goods_Received_Note1.aspx" class="btn btn-primary btn" id="newsf">Add New</a></span><span style="float: right; margin-right: 15px;"><div id="reportrange" class="form-control mt-5 mb-5" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc;">
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
                    <%--<div style="float:right"><ul class="segment"><li data-va='All'>ALL</li><li data-va='0' class="active">Active</li></ul></div>--%>
                </div>
                <table class="table table-hover" id="OrderList">
                    <thead class="text-warning">
                        <tr>
                            <th style="text-align: left;">Sl NO</th>
                            <th style="text-align: left">GRN No</th>
                            <th style="text-align: left">GRN Date</th>
                            <th style="text-align: left">Po_No</th>
                            <th style="text-align: left">Supp_Name</th>
                            <th style="text-align: left">Sub Total</th>
                            <th style="text-align: left">Tax Total</th>
                            <th style="text-align: left">Gross Total</th>
                            <th style="text-align: left">View</th>
                            <th style="text-align: left">Print</th>
                             <th style="text-align: left">DO</th>
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
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
    <script language="javascript" type="text/javascript">
        var AllOrders = [];
        var divcode; filtrkey = '0';
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "GRN_No,Supp_Name,slno,Po_No,GRN_Date,";

        $(".data-table-basic_length").on("change",
            function () {
                pgNo = 1;
                PgRecords = parseFloat($(this).val());
                ReloadTable();
            }
        );
        $("#reportrange").on("DOMSubtreeModified", function () {
            pgNo = 1;
            $('#tSearchOrd').val('');
            id = $('#ordDate').text();
            id = id.split('-');
            fdt = id[2].trim() + '-' + id[1] + '-' + id[0] + ' 00:00:00';
            tdt = id[5] + '-' + id[4] + '-' + id[3].trim() + ' 00:00:00';
            loadData(tdt,fdt);
        });

        function loadPgNos() {
            prepg = parseInt($(".paginate_button.previous>a").attr("data-dt-idx"));
            Nxtpg = parseInt($(".paginate_button.next>a").attr("data-dt-idx"));
            $(".pagination").html("");
            TotalPg = parseInt(parseInt(Orders.length / PgRecords) + ((Orders.length % PgRecords) ? 1 : 0)); selpg = 1;
            if (isNaN(prepg)) prepg = 0;
            if (isNaN(Nxtpg)) Nxtpg = 2;
          //  if ((prepg + 1) == pgNo && pgNo > 1) selpg = (parseInt(pgNo) - 1);
            selpg =(pgNo > 7)? (parseInt(pgNo) + 1) - 7:1;
            if ((Nxtpg) == pgNo){
                 selpg = (parseInt(TotalPg)) - 7;
                 selpg =(selpg>1)? selpg:1;
            }
            spg = '<li class="paginate_button previous" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">First</a></li>';
            for (il = selpg - 1; il < selpg + 7; il++) {
                if (il < TotalPg)
                    spg += '<li class="paginate_button' + ((pgNo == (il + 1)) ? " active" : "") + '"><a href="#" aria-controls="example2" data-dt-idx="' + (il + 1) + '" tabindex="0">' + (il + 1) + '</a></li>';
            }
            spg += '<li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="' + TotalPg + '" tabindex="0">Last</a></li>';
            $(".pagination").html(spg);

            $(".paginate_button > a").on("click", function () {
                pgNo = parseInt( $(this).attr("data-dt-idx")); ReloadTable();
                /* $(".paginate_button").removeClass("active");
                 $(this).closest(".paginate_button").addClass("active");*/
            }
           );
        }

        function ReloadTable() {
            $("#OrderList TBODY").html("");
            st = PgRecords * (pgNo - 1); slno = 0;
            for ($i = st; $i < st + PgRecords; $i++) {
                //var filtered = Orders.filter(function (x) {
                //    return x.Sf_Code != 'admin';
                //})
                if ($i < Orders.length) {
                    tr = $("<tr></tr>");
                    //var hq = filtered[$i].Sf_Name.split('-');
                    slno = $i + 1;
                    $(tr).html("<td>" + slno + "</td><td>" + Orders[$i].GRN_No + "</td><td>" + Orders[$i].GRN_Date +
                        "</td><td>" + Orders[$i].Po_No + "</td><td>" + Orders[$i].Supp_Name +
                        "</td><td>" + parseFloat(Orders[$i].Net_Tot_Goods).toFixed(2) + "</td><td>" + parseFloat(Orders[$i].Net_Tot_Tax).toFixed(2) + "</td><td>" + parseFloat(Orders[$i].Net_Tot_Value).toFixed(2) + "</td><td><a href='Good_Received_View.aspx?Trans_Sl_No=" + Orders[$i].Trans_Sl_No + "&Stockist_Code=" + Orders[$i].stockist_Code + "&Div_Code=" + Div_Code + "'><span class='glyphicon glyphicon-eye-open'></span></a></td><td><a href='Grn_Print.aspx?Trans_Sl_No="+Orders[$i].Po_No+"&Div_Code=" + Div_Code + "'><span class='glyphicon glyphicon-print'></span></a></td><td><a href='Delivery_Order.aspx?Trans_Sl_No="+Orders[$i].Po_No+"&Div_Code=" + Div_Code + "'><span class='glyphicon glyphicon-print'></span></a></td>");

                    $("#OrderList TBODY").append(tr);
                    hq = []
                }
            }
            $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")

            loadPgNos();
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
             pgNo = 1;
            ReloadTable();
        })
        function loadData(tdt,fdt) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Goods_Received_List.aspx/GetgrnDetails",
                data: "{'Stockist_Code':'"+stockist_Code+"','FDT':'" + fdt + "','TDT':'" + tdt + "'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders; ReloadTable();

                },
                error: function (result) {
                }
            });
        }

        var stockist_Code = ("<%=Session["Sf_Code"].ToString()%>");
        var Div_Code = ("<%=Session["div_code"].ToString()%>");
        loadData();

        $(document).on('click', '#btn', function () {

            window.location = "../Stockist/Counter_Sales.aspx";
        });


    </script>

        <script type="text/javascript">
        $(function () {

            var start = moment();
            var end = moment();

            function cb(start, end) {
                $('#reportrange span').html(start.format('DD-MM-YYYY') + ' - ' + end.format('DD-MM-YYYY'));
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


</asp:Content>
