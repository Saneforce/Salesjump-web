<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="DistributorClaim.aspx.cs" Inherits="MIS_Reports_DistributorClaim" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <link rel="Stylesheet" href="../css/kendo.common.min.css" />
    <link rel="Stylesheet" href="../css/kendo.default.min.css" />
    <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />  
     <style>
        [data-val='Late']{
            color:red;
        }
        [data-val='On-Time']{
            color:green;
        }
        th
        {
            white-space:nowrap;   
            cursor:pointer;
        }
    </style>
    <form runat="server">           
         <div class="row">            
         <div class="col-lg-12 sub-header">Distributor Claim Report <span style="float: right; margin-right: 15px;"><div id="reportrange" class="form-control mt-5 mb-5" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc;">
                <i class="fa fa-calendar"></i>&nbsp;
        <span id="ordDate"></span><i class="fa fa-caret-down"></i>
            </div>              
            </span>
             
            <div style="float: right; padding-top: 5px;">               
                <div class="segment">Distributor Name<%--<asp:DropDownList ID="ddlDistributor" runat="server"></asp:DropDownList>--%><select id="ddlDistributor" data-live-search="true" data-dropup-auto="false"></select></div>
            </div>
    </div>
             </div>
         <div class="card">            
             <div class="card-body table-responsive">
                 <table>
                     <tr>
                         <td><div style="white-space:nowrap"><b>Contact no :</b></div></td>
                         <td><label id="lblContactNo" style="all:unset"></label></td>                         
                     </tr>                    
                 </table>           
                 </div>

        </div>
        <div class="card">
        <div class="card-body table-responsive">
            <div style="white-space:nowrap">Search&nbsp;&nbsp;<input type="text" id="tSearchOrd" style="width:250px;" />          
            <label style="float:right">Show <select class="data-table-basic_length" aria-controls="data-table-basic"><option value="10">10</option><option value="25">25</option><option value="50">50</option><option value="100">100</option></select> entries</label>
            </div>             
             <table class="table table-hover" id="OrderList" style="font-size:12px">
                <thead class="text-warning">
                    <tr>                          
                        <th id="Ean_Code" style="text-align:left">EAN Code</th>
                        <th id="Brand_Name" style="text-align:left">Brand Name</th>
                        <th id="Product_Name" style="text-align:left">Product Name</th>
                        <th id="MRP" style="text-align:left">MRP</th>
                        <th id="Claim_Qty" style="text-align:left">Claim Qty</th>
                        <th id="Claim_Type" style="text-align:left">Claim Type</th>                                              
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
                        <ul class="pagination" style="float:right;margin:-11px 0px">
                            <li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">Previous</a></li>
                            <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="7" tabindex="0">Next</a></li>
                        </ul>
                    </div>
                </div>
            </div>            
        </div>
    </div>
    </form>
     <script type="text/javascript" src="../js/kendo.all.min.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>    
    <script type="text/javascript">
        var AllOrders = [];
        var sf;
        var sf1;
        var fdt = '';
        var tdt = '';
        //var filtrkey = 'All';
        var sortid = '';
        var asc = true;        
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "SF,Sale_Erp_Code,Product_Brd_Name,Product_Detail_Name,MRP,Claim_Qty,Claim_Type";
        $(".data-table-basic_length").on("change",
        function () {
            pgNo = 1;
            PgRecords = $(this).val();
            ReloadTable();
        }
        );

        $("#reportrange").on("DOMSubtreeModified", function () {
            id = $('#ordDate').text();
            id = id.split('-');
            fdt = id[2].trim() + '-' + id[1] + '-' + id[0];
            tdt = id[5] + '-' + id[4] + '-' + id[3].trim();
            var ab = $('#ddlDistributor').val();
            if (ab != "0")
            {
                loadData1(ab);
            }
        });
        $('#ddlDistributor').on('change', function () {
            var ab = $('#ddlDistributor').val();
            if (ab == "0") {
                alert('Select the distributor');
                return false;
            }
            loadData1(ab);
        })
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
        function loadData() {
            var ab = $('#ddlDistributor').val();
            dt=new Date();
            $m=dt.getMonth()+1,$y=dt.getFullYear();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "DistributorClaim.aspx/GetData",
                data: "{'SF':'" + ab + "','Div':'<%=Session["Division_Code"]%>','Mn':'" + fdt + "','Yr':'" + tdt + "'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders; ReloadTable();
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
        }
        function loadData1(ab) {
            dt=new Date();
            $m=dt.getMonth()+1,$y=dt.getFullYear();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "DistributorClaim.aspx/GetData",
                data: "{'SF':'" + ab + "','Div':'<%=Session["Division_Code"]%>','Mn':'" + fdt + "','Yr':'" + tdt + "'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders; ReloadTable();
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
         }
        function ReloadTable() {
            $("#OrderList TBODY").html("");
            st = PgRecords * (pgNo - 1);
            for ($i = st; $i < st + PgRecords; $i++) {
                if ($i < Orders.length) {
                    tr = $("<tr></tr>");
                    $(tr).html("<td>" + Orders[$i].Sale_Erp_Code + "</td><td>" + Orders[$i].Product_Brd_Name + "</td><td>" + Orders[$i].Product_Detail_Name + "</td><td>" + Orders[$i].MRP + "</td><td>" + Orders[$i].ClaimQty + "</td><td>" + Orders[$i].ClaimType + "</td>");
                    $("#OrderList TBODY").append(tr);
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
            ReloadTable();
        });
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
            }
           );
        }
         function radiochange() {            
            //$('#dropdistributor').html("");
              $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "DistributorClaim.aspx/getMGR",
                    data: "{'divcode':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        SFStates = JSON.parse(data.d) || [];
                        if (SFStates.length > 0) {
                            var states = $("#ddlDistributor");
                            states.empty().append('<option selected="selected" value="0">Select distributor</option>');
                            for (var i = 0; i < SFStates.length; i++) {
                                states.append($('<option value="' + SFStates[i].Stockist_Code + '">' + SFStates[i].Stockist_Name + '</option>'))
                            }
                        }
                    }
              });                       
             $('#ddlDistributor').selectpicker({
                liveSearch: true
            });
        }
         $(document).ready(function () {
             sf = '<%=Session["Sf_Code"]%>';
             radiochange();
             $('#ddlDistributor').on('change', function () {
                var name = $(this).val();
                $.ajax({
                    type: "POST",
                    url: "DistributorClaim.aspx/GetContactDetails",
                    data: '{Stockist_Code: "' + name + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response.d != '') {
                            var user = JSON.parse(response.d);
                            $('#lblContactNo').html(user[0].Stockist_Mobile);                           
                        }
                        else {
                            $('#lblContactNo').html('no datafound');                           
                        }
                    },
                    error: function (response) {
                        alert(response.responseText);
                    }
                });
            });
        });
    </script>
   <%--  </span>--%>
</asp:Content>

