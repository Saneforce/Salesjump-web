<%@ Page Title="" Language="C#" MasterPageFile="~/Master_SS.master" AutoEventWireup="true" CodeFile="SS_Counter_Sales_View.aspx.cs" Inherits="SuperStockist_Sales_CounterSales_SS_Counter_Sales_View" %>

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
    <form id="frm1" runat="server">
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
        <%--<div class="row">            
                <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                        style="height:40px;width:40px;border-width:0px;position: absolute;right: 15px;top: 55px;" OnClick="ExportToExcel" />
        </div>--%>
    <div class="row">
        <div class="col-lg-12 sub-header">Counter Sales<span style="float:right"><a href="../CounterSales/SS_Counter_Sales.aspx" class="btn btn-primary btn" id="newsf">Add New</a></span><span style="float: right; margin-right: 15px;"><div id="reportrange" class="form-control mt-5 mb-5" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc;">
                    <i class="fa fa-calendar"></i>&nbsp;
        <span id="ordDate"></span><i class="fa fa-caret-down"></i>
                </div>
                </span>
            </div>
    </div>        

    <div class="card">
        <div class="card-body table-responsive">
            <div style="white-space:nowrap">Search&nbsp;&nbsp;<input type="text" autocomplete="off" id="tSearchOrd" style="width:250px;" />
            <label style="white-space:nowrap;margin-left: 57px;display:none;">Filter By&nbsp;&nbsp;<select id="txtfilter" name="ddfilter" style="width:250px;display:none;"></select></label>
            <label style="float:right">Shows <select class="data-table-basic_length" aria-controls="data-table-basic"><option value="10">10</option><option value="25">25</option><option value="50">50</option><option value="100">100</option></select> entries</label>
            <%--<div style="float:right"><ul class="segment"><li data-va='All'>ALL</li><li data-va='0' class="active">Active</li></ul></div>--%>
            </div>
             <table class="table table-hover" id="OrderList">
                <thead class="text-warning">
                    <tr>   
                        <th style="text-align:left;">SlNO</th>                       
                        <th style="text-align:left">Order ID</th>
                        <th style="text-align:left">Cust_Name</th>                          
                        <th style="text-align:left">Order Date</th>
                        <th style="text-align:left">Pay Term</th>
                       <th style="text-align:left">Total Amount</th>              
                       <th style="text-align:left">Total Product</th>

          

                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
            <div class="row" style="padding:5px 0px">
                <div class="col-sm-5">
                    <div class="dataTables_info" id="orders_info" role="status" aria-live="polite">Showing 0 to 0 of 0 entries</div>
                </div>
                <div class="col-sm-7">
                    <div class="dataTables_paginate paging_simple_numbers" id="example2_paginate">
                        <ul class="pagination">
                            <li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">Previous</a></li>
                            <li class="paginate_button active"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">1</a></li>
                            <li class="paginate_button "><a href="#" aria-controls="example2" data-dt-idx="2" tabindex="0">2</a></li>
                            <li class="paginate_button "><a href="#" aria-controls="example2" data-dt-idx="3" tabindex="0">3</a></li>
                            <li class="paginate_button "><a href="#" aria-controls="example2" data-dt-idx="4" tabindex="0">4</a></li>
                            <li class="paginate_button "><a href="#" aria-controls="example2" data-dt-idx="5" tabindex="0">5</a></li>
                            <li class="paginate_button "><a href="#" aria-controls="example2" data-dt-idx="6" tabindex="0">6</a></li>
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
        var fdt='';
        var tdt='';    
        var divcode;filtrkey='0';
       // optStatus="<li><a href='#' v='0'>Active</a><a href='#' v='1'>Vacant / Block</a><a href='#' v='2'>Deactivate</a></li>"
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Order_No,Product_Name,Pay_Term,Price";
        $(".data-table-basic_length").on("change",
        function () {
            pgNo = 1;
            PgRecords = $(this).val();
            ReloadTable();
        }
        );
        $("#reportrange").on("DOMSubtreeModified",function(){
            id=$('#ordDate').text();
            id=id.split('-');
            fdt=id[2].trim()+'-'+id[1]+'-'+id[0]+' 00:00:00';
            tdt=id[5]+'-'+id[4]+'-'+id[3].trim()+' 00:00:00';
            loadData();
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
            selpg =(pgNo > 7)? (parseInt(pgNo) + 1) - 7:1;
            if ((Nxtpg) == pgNo){
                 selpg = (parseInt(TotalPg)) - 7;
                 selpg =(selpg>1)? selpg:1;
            }
            spg = '<li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">First</a></li>';
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
            $('.spinnner_div').hide();
        }
        
        function ReloadTable() {
            $('.spinnner_div').show();
            $("#OrderList TBODY").html("");
            st = PgRecords * (pgNo - 1);slno = 0;
            for ($i = st; $i < st + PgRecords ; $i++) {
                //var filtered = Orders.filter(function (x) {
                //    return x.Sf_Code != 'admin';
                //})
                if ($i < Orders.length) {
                    tr = $("<tr></tr>");
                    //var hq = filtered[$i].Sf_Name.split('-');
                    slno = $i + 1;
                    $(tr).html("<td>" + slno + "</td><td>" + Orders[$i].Order_No + "</td><td>" + Orders[$i].Cus_Name + 
                    "</td><td>" + Orders[$i].Order_Date + "</td><td>" + Orders[$i].Pay_Term + 
                        "</td><td>" + Orders[$i].Total + "</td><td><a id='myButton' href='#' onclick='popup("+Orders[$i].Order_No+")'> <span class='glyphicon glyphicon-eye-open'></span></a></td>");
                    
                    $("#OrderList TBODY").append(tr);
                    hq = [];
                }
            }
            $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")
            
            loadPgNos();
            $('.spinnner_div').hide();
        }

        $("#tSearchOrd").on("keyup", function () {
            if ($(this).val() != "") {
                shText = $(this).val().toLowerCase();
                Orders = AllOrders.filter(function (a) {
                    chk = false;
                    $.each(a, function (key, val) {                       
                        if (val != null && val.toString().toLowerCase().indexOf(shText) > -1 && (',' + searchKeys).indexOf(',' + key + ',')>-1)
                        {
                            chk = true;
                        }
                    })
                    return chk;
                })
            }
            else
                Orders = AllOrders
            ReloadTable();
        })
        function loadData() {
            $('.spinnner_div').show();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "SS_Counter_Sales_View.aspx/GetCounterDetails",  
                data: "{'FDT':'" + fdt + "','TDT':'" + tdt + "'}",           
                dataType: "json",
                success: function (data) {
                       $('.spinnner_div').show();
                    AllOrders = data.d || [];
                    Orders = AllOrders; ReloadTable();
                    $('.spinnner_div').hide();

                },
                error: function (result) {
                    $('.spinnner_div').hide();
                }
            });
        }

        
            var stockist_Code = ("<%=Session["Sf_Code"].ToString()%>");
        loadData();

          $(document).on('click', '#btn', function () {

                window.location = "../CounterSales/SS_Counter_Sales.aspx";
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

