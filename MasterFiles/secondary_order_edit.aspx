<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="secondary_order_edit.aspx.cs" Inherits="MasterFiles_secondary_order_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="../css/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.2/js/bootstrap-select.min.js"></script>
   <%--<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-confirm/3.3.2/jquery-confirm.min.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-confirm/3.3.2/jquery-confirm.min.js"></script>--%>
    <link href="../alertstyle/jquery-confirm.min.css" rel="stylesheet" />
    <script src="../alertstyle/jquery-confirm.min.js"></script>
   
         <style>
        .daterangepicker {
                z-index: 10000000;
            }
			  #disabled {
  pointer-events: none;
  cursor: default;
  opacity: 0.6;
}
        </style>
     </br>
     <div class="row">
	 <div class="col-sm-4 sub-header">
             <span style="float: left; margin-right: 15px;"><div id="reportrange" class="form-control mt-5 mb-5" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc;">
                <i class="fa fa-calendar"></i>&nbsp;
        <span id="ordDate"></span><i class="fa fa-caret-down"></i>
            </div>
            </span>
        </div>
       <div class="col-sm-4" style="margin-bottom: 1rem;">
            <label for="ftname">FieldForce:  </label>&nbsp
                <select id="ftname"> 
                    <option value="">Select FieldForce</option>
                </select>
        </div>
         <div class="col-sm-4" style="margin-bottom: 1rem;">
                <label for="rtname">Stockist Name:  </label>&nbsp
                <select id="rtname">
                    <option value="">Select Stockist</option>
                </select>
            </div>
    </div>
    <div class="modal fade" id="EditModal" style="z-index: 10000000; background: transparent; overflow-y: scroll;" tabindex="0" aria-hidden="true">
            <div class="modal-dialog" role="document" style="width: 80% !important">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="orderedit"></h5>
                    </div>
                    <div class="modal-body" style="padding-top: 10px">
                         <div class="row">
                        <div class="col-sm-12" style="padding: 15px">
                                <table class="table table-hover" id="editdets">
                                    <thead class="text-warning">
                                        <tr>
                                            <th>S.No</th>
                                            <th>Product</th>
                                            <th>Unit</th>
                                            <th>Qty</th>
                                            <th>Rate</th>
                                            <th>Free</th>
                                            <th>Dis Val</th>
                                            <th>Total</th>
                                            <th>Tax</th>
                                            <th>Gross_Total</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
									 <tfoot></tfoot>
                                </table>
                        </div>
                    </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
    <div class="card">
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
                <div style="float: right; padding: 4px 0px 0px 0px;">
                    <ul class="segment">
                        <li data-va='All' class="active">ALL</li>
                        <li data-va='0'>Pending</li>
                        <li data-va='1'>Invoiced</li>
                        <li data-va='2'>Cancelled</li>
                    </ul>
                </div>
            </div>
            <table class="table table-hover" id="OrderList">
                <thead class="text-warning">
                    <tr>
                        <th>Sl.No</th>
                        <th>Order ID</th>
                        <th>Order Date</th>
                        <th>FieldForce</th>
						<th>Retailer</th>
                        <th>Route</th>
                        <th>Amount</th>
                        <th>Status</th>
                        <th>View</th>
                        <th>Edit</th>
                        <th>Cancel</th>
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
                            <li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">Previous</a></li>
                            <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="7" tabindex="0">Next</a></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <link href="../css/SpinnerInput.css" rel="stylesheet" type="text/css" />
    <link href="../css/select2.min.css" rel="stylesheet" />
    <script src="../js/select2.full.min.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
    <script language="javascript" type="text/javascript">
        var AllOrders = [], Orders = [], pgNo = 1, PgRecords = 10, TotalPg = 0, searchKeys = "Trans_Sl_No,Sf_Name,Order_Date,Order_Value,ListedDr_Name,Territory_Name,", All_unit=[];
        var div_code = ("<%=Session["div_code"]%>");
        var fdt = ''; var tdt = ''; var filtrkey = 'All'
        $('#tSearchOrd').focus();
        $(document).on('keypress', '#message-text', function (e) {
            if (e.keyCode == 34 || e.keyCode == 39 || e.keyCode == 38 || e.keyCode == 60 || e.keyCode == 62 || e.keyCode == 92) return false;
        });

        $(".data-table-basic_length").on("change",
            function () {
                pgNo = 1;
                PgRecords = parseFloat($(this).val());
                ReloadTable();
            }
        );

        $(".segment>li").on("click", function () {
            $(".segment>li").removeClass('active');
            $(this).addClass('active');
            filtrkey = $(this).attr('data-va');
            $("#tSearchOrd").val('');
            Orders = AllOrders;
            ReloadTable();
        });
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
                Orders = AllOrders;
            ReloadTable();
        });
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
                pgNo = parseInt($(this).attr("data-dt-idx"));
                ReloadTable();

            }
            );
        }
        function fillproducts(orderno, sfname) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "secondary_order_edit.aspx/Get_Product_unit",
                data: "{'divcode':'<%=Session["div_code"]%>','sfcode':'" + sfname + "'}",
                success: function (data) {
                    All_unit = JSON.parse(data.d) || [];
                },
                error: function (result) {
                }
            });
            $('#editdets tbody').html('');
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "secondary_order_edit.aspx/get_product_fedit",
                data: "{'divcode':'<%=Session["div_code"]%>','trans_slno':'" + orderno + "','sfcode':'" + sfname + "'}",
                        dataType: "json",
                        success: function (data) {
                            var AllOrders2 = JSON.parse(data.d) || [];
                            $('#editdets TBODY').html("");
                            var slno = 0,totamt = 0;
                            for ($i = 0; $i < AllOrders2.length; $i++) {
                                var filter_unit = []; units = ""; units1 = "";

                                filter_unit = All_unit.filter(function (w) {
                                    return (AllOrders2[$i].Product_Code == w.Product_Detail_Code);
                                });

                                if (filter_unit.length > 0) {

                                    for (var z = 0; z < filter_unit.length; z++) {
                                        units += "<li id='s' name='itms' value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</li>";
                                        units1 += "<option value='" + filter_unit[z].Move_MailFolder_Id + "'>" + filter_unit[z].Move_MailFolder_Name + "</option>";
                                    }
                                }
                                if (AllOrders2.length > 0) {
                                    slno += 1;
                                    tr = $("<tr></tr>");
                                    $(tr).html("<td>" + slno + "</td><td>" + AllOrders2[$i].Product_Name + "</td><td>" + AllOrders2[$i].Unit_Name + "</td><td>" + AllOrders2[$i].Quantity + "</td>" +
                                        "<td>" + AllOrders2[$i].Rate + "</td > <td>" + AllOrders2[$i].free + "</td> <td>" + AllOrders2[$i].discount_price + "</td> <td>" + AllOrders2[$i].total +
                                        "</td><td>" + AllOrders2[$i].Tax_value + "</td><td>" + (AllOrders2[$i].value).toFixed(2)+ "</td>");
                                    $("#editdets TBODY").append(tr);
									totamt = totamt + parseFloat((AllOrders2[$i].value).toFixed(2));
                                }
                            }
							$('#editdets tfoot').html("");
                            var ftr = $("<tr></tr>");
                            $(ftr).html("<td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td><b>Total</b></td><td>" + totamt.toFixed(2) + "</td>");
                            $("#editdets tfoot").append(ftr);
                        }
                    });
        }
        function ReloadTable() {
            var tr = '';
            $("#OrderList TBODY").html("");
            if (filtrkey != "All") {
                Orders = Orders.filter(function (a) {
                    return a.Order_Flag == filtrkey;
                })
            }
            st = PgRecords * (pgNo - 1); slno = 0;
            
            for ($i = st; $i < st + PgRecords; $i++) {
                if ($i < Orders.length) {
                    tr = $("<tr sfname='" + Orders[$i].Sf_Code + "'  ordno='" + Orders[$i].Trans_Sl_No + "' orddate='" + Orders[$i].Order_Date + "' stkcode='" + Orders[$i].Stockist_Code+"'></tr>");
                    slno = $i + 1;
                    $(tr).html('<td>' + slno + "</td><td>" + Orders[$i].Trans_Sl_No + '</td><td>' + Orders[$i].Order_Date + "</td><td>" + Orders[$i].Sf_Name +
                        "</td><td>" + Orders[$i].ListedDr_Name+"</td><td>" + Orders[$i].Territory_Name +"</td><td>" + Orders[$i].Order_Value.toFixed(2) + "</td><td class= 'stus' >" + Orders[$i].status + "</td ><td id=" + Orders[$i].Trans_Sl_No + " class= 'roview' > <a href='#'>View</a></td ><td id=" + ((Orders[$i].status == "Cancelled" || Orders[$i].status == "invoiced") ? 'disabled' : '0') + " class= 'roedit' > <a href='#'>Edit</a></td ><td id="+ ((Orders[$i].status == "Cancelled" || Orders[$i].status == "invoiced") ? 'disabled' : '0') +" class='rocanle'><a href='#'>Cancel</a></td>");//
                    $("#OrderList TBODY").append(tr);
                }
            }
            $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries");

            loadPgNos();
        }
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
        $("#reportrange").on("DOMSubtreeModified", function () {
            id = $('#ordDate').text();
            id = id.split('-');
            fdt = id[2].trim() + '-' + id[1] + '-' + id[0] + ' 00:00:00';
            tdt = id[5] + '-' + id[4] + '-' + id[3].trim() + ' 00:00:00';
            var Fdate = new Date(fdt);
            var fyear = Fdate.getFullYear();
            var Tdate = new Date(tdt);
            var tyear = Tdate.getFullYear();
            loadData();
        });
        function loadData() {
            var stkcd=$("#rtname").val();
			var focode = $("#ftname").val();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "secondary_order_edit.aspx/GetOrders",
                data: "{'stk':'" + stkcd + "','FDt':'" + fdt + "','TDt':'" + tdt + "','divcode':'" + div_code+"','sfcode':'" + focode +"'}",
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
        $("#rtname").on("change", function () {
            loadData();
            });
			$("#ftname").on("change", function () {
            loadData();
        });
        $(document).ready(function () {
            
            $(document).on('click', '.roview', function () {
                $('#EditModal').modal('toggle');
                var orderno = $(this).closest('tr').attr('ordno');
                var sfname = $(this).closest('tr').attr('sfname');
                $('#orderedit').text("Orders View : " + sfname);
                fillproducts(orderno, sfname);
            });
            $(document).on('click', '.roedit', function () {
                var orderno = $(this).closest('tr').attr('ordno');
                var sfname = $(this).closest('tr').attr('sfname');
                var stkcd = $(this).closest('tr').attr('stkcode');
                var orddate = $(this).closest('tr').attr('orddate');
                var stus = $(this).context.id;
                if (stus == "Cancelled") {
                    alert("Cancelled Orders can't Editable...");
                }
                else if (stus == "invoiced")
                {
                    alert("invoiced Orders can't Editable...");
                }
                else {
                    window.location.href = "order_edit.aspx?odcode=" + orderno + "&sfcode=" + sfname + "&stckde=" + stkcd + "&ordete=" + orddate;
                }
            });
            $(document).on('click', '.rocanle', function () {
                var order_no = $(this).closest('tr').attr('ordno');
                if (confirm("Are you sure cancel this Order: " + order_no + " ?")) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "secondary_order_edit.aspx/order_cancel",
                        data: "{'orderno':'" + order_no + "','divcode':'<%=Session["div_code"]%>'}",
                        dataType: "json",
                        success: function (data) {
                            if (data.d == "Success") {
                                alert("order Cancelled successfully...");
                            }
                            else {
                                alert("Error occured,try again...");
                            }
                            loadData();
                        },
                        error: function (result) {
                            alert(JSON.stringify(result));
                        }
                    });
                }
            });
           
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "secondary_order_edit.aspx/Get_Distributor",
                dataType: "json",
                success: function (data) {
                    All_stk = JSON.parse(data.d) || [];
                    var stk = $("#rtname"); 
                    stk.empty().append('<option selected="selected" value="">Select Stockist</option>');
                    for (var i = 0; i < All_stk.length; i++) {
                        stk.append($('<option value="' + All_stk[i].stockist_code + '">' + All_stk[i].stockist_name + '</option>'));
                    }
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
			$.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "secondary_order_edit.aspx/Get_Fieldforce",
                dataType: "json",
                success: function (data) {
                    All_fldre = JSON.parse(data.d) || [];
                    var fo = $("#ftname");
                    fo.empty().append('<option selected="selected" value="">Select FieldForce</option>');
                    for (var i = 0; i < All_fldre.length; i++) {
                        fo.append($('<option value="' + All_fldre[i].Sf_Code + '">' + All_fldre[i].Sf_Name + '</option>'));
                    }
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
            loadData();
        });
        </script>
</asp:Content>

