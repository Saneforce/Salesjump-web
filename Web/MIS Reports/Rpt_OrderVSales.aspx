<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Rpt_OrderVSales.aspx.cs" Inherits="MIS_Reports_Rpt_OrderVSales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link type="text/css" href="../css/SalesForce_New/bootstrap-select.min.css" rel="stylesheet" />
    <style>
        [data-val='Pending'] {
            color: red;
        }

        [data-val='Dispatched'] {
            color: green;
        }

        [data-val='On-Duty'] {
            color: blue;
        }

        th {
            white-space: nowrap;
            cursor: pointer;
			position: sticky;
			top: 0;
        }
		tr.bgrow th{
			background:rgba(247, 247, 247, 0.8)
		}
        .sub-header {
            font-family: Verdana;
            font-size: large;
            font-weight: bold;
        }
		::-webkit-scrollbar {
		  width: 10px;
		}
		::-webkit-scrollbar-track {
		  background: #f1f1f1; 
		}
		::-webkit-scrollbar-thumb {
		  background: #888; 
		}
		::-webkit-scrollbar-thumb:hover {
		  background: #555; 
		}		
    </style>
    <form runat="server" id="frm1" style="overflow: hidden">
        <div class="modal fade" id="exampleModal" style="z-index: 10000000; overflow-y: auto; background-color: rgba(0, 0, 0, 0.06);" tabindex="0" aria-hidden="true">
            <div class="modal-dialog" role="document" style="width: 1000px !important">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Invoice Upload Summary</h5>
                        <button type="button" class="close" onclick="clearOrder()" style="margin-top: -20px;" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-sm-12" style="padding: 15px">
                                <table class="table table-hover orden" id="OrderEntry">
                                    <thead class="text-warning">
                                        <tr>
                                            <th style="text-align: left">SlNo</th>
                                            <th style="text-align: left">Field Force</th>
                                            <th style="text-align: left">Orders</th>
                                            <th style="text-align: left">Order Amount</th>
                                            <th style="text-align: left">Pending</th>
                                            <th style="text-align: left">Pending Amount</th>
                                            <th style="text-align: left">Invoiced</th>
                                            <th style="text-align: left">Invoiced Amount</th>
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
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" onclick="clearOrder()" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="row" style="margin-bottom: 2rem;">
                <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                    Style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 15px; top: 43px;" OnClick="lnkDownload_Click" />
            </div>
            <div class="row">
                <div class="col-sm-6 sub-header" style="padding-left: 4rem;">
                </div>
                <div class="card col-sm-6" style="margin: 0px 25px 0px 0px; width: 450px; float: right">
                    <div class="card-body table-responsive">
                        <table>
                            <tbody>
                                <tr>
                                    <td><span style="font-weight: bold;">Total Orders:</span><span id="totalord"></span></td>
                                    <span class="glyphicon glyphicon-eye-open qqq" style="float: right;"></span>
                                    <td><span style="font-weight: bold;">Order Amount :</span><span id="totamt"></span></td>
                                </tr>
                                <tr>
                                    <td><span style="font-weight: bold;">Pending Orders : </span><span id="totpend"></span></td>
                                    <td><span style="font-weight: bold;">Pending Amount : </span><span id="totpenamt"></span></td>
                                </tr>
                                <tr>
                                    <td><span style="font-weight: bold;">Invoiced : </span><span id="totinv"></span></td>
                                    <td><span style="font-weight: bold;">Invoiced Amount : </span><span id="totinvamt"></span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div class="card">
            <div class="card-body table-responsive" style="overflow-x: auto;max-height:100vh;">
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
                    <div style="float: right; padding-top: 4px;">
                        <ul class="segment">
                            <li data-va='All' class="active">ALL</li>
                            <li data-va='Pending'>Pending</li>
                            <li data-va="Dispatched">Dispatched</li>
                        </ul>
                    </div>
                </div>
                <table class="table table-hover" id="OrderList" style="font-size: 12px">
                    <thead class="text-warning">
                        <tr class="bgrow">
                            <th style="text-align: left">Sl.No</th>
                            <th style="text-align: left">State</th>
                            <th style="text-align: left">Field Force HQ</th>
                            <th style="text-align: left">Field Force Name</th>
                            <th style="text-align: left">Distributor Code</th>
                            <th style="text-align: left">Distributor Name</th>
                            <th style="text-align: left">Route</th>
                            <th style="text-align: left">OrderID</th>
                            <th style="text-align: left">Order Date</th>
                            <th style="text-align: left">Retailer Name</th>
                            <th style="text-align: left">Area Name</th>
                            <th style="text-align: left">Order Amt</th>
                            <th style="text-align: left">Invoice Amt</th>
                            <th style="text-align: left">Bill Upload Date</th>
                            <th style="text-align: left">Remarks</th>
                            <th style="text-align: left">Image</th>
                            <th style="text-align: left">Status</th>
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
                                <li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">Previous</a></li>
                                <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="7" tabindex="0">Next</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        var orderDets = [];
        var AllOrders = [];
        var filtrkey = 'All';
        var sortid = '';
        var asc = true;
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Trans_Sl_No,RetailerCode,RetailerName,Order_Amount,OrderDate,RouteDistributorName,OrderTakenByInvoice_Amount,Bill_Upload_Date,RemarksStatus,";
        $(".data-table-basic_length").on("change",
        function () {
            pgNo = 1;
            PgRecords = $(this).val();
            ReloadTable();
        }
        );
        //$('th').on('click', function () {
        //    sortid = this.id;
        //    asc = $(this).attr('asc');
        //    if (asc == undefined) asc = 'true';
        //    Orders.sort(function (a, b) {
        //        if (a[sortid].toLowerCase() < b[sortid].toLowerCase() && asc == 'true') return -1;
        //        if (a[sortid].toLowerCase() > b[sortid].toLowerCase() && asc == 'true') return 1;

        //        if (b[sortid].toLowerCase() < a[sortid].toLowerCase() && asc == 'false') return -1;
        //        if (b[sortid].toLowerCase() > a[sortid].toLowerCase() && asc == 'false') return 1;
        //        return 0;
        //    });

        //    $(this).attr('asc', ((asc == 'true') ? 'false' : 'true'));
        //    ReloadTable();
        //});
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
            spg = '<li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">First</a></li>';
            for (il = selpg - 1; il < selpg + 7; il++) {
                if (il < TotalPg)
                    spg += '<li class="paginate_button' + ((pgNo == (il + 1)) ? " active" : "") + '"><a href="#" aria-controls="example2" data-dt-idx="' + (il + 1) + '" tabindex="0">' + (il + 1) + '</a></li>';
            }
            spg += '<li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="' + TotalPg + '" tabindex="0">Last</a></li>';
            $(".pagination").html(spg);

            $(".paginate_button > a").on("click", function () {
                pgNo = parseInt($(this).attr("data-dt-idx")); ReloadTable();
            }
           );
        }
        function getCusdets() {
            $("#OrderList TBODY").html("<tr><td colspan=10>Loading please wait...</td></tr>");
            setTimeout(function () {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    url: "Rpt_OrderVSales.aspx/getDetails",
                    dataType: "json",
                    success: function (data) {
                        AllOrders = JSON.parse(data.d) || [];
                        Orders = AllOrders;
                        $('#totalord').html(AllOrders.length);
                        let ordval = AllOrders.reduce(function (prev, cur) {
                            return prev + cur.Order_Amount;
                        }, 0);
                        let pordval = AllOrders.filter(function (a) {
                            return a.Status == "Pending";
                        }).reduce(function (prev, cur) {
                            return prev + cur.Order_Amount;
                        }, 0);
                        let invordval = AllOrders.filter(function (a) {
                            return a.Status == "Dispatched";
                        }).reduce(function (prev, cur) {
                            return prev + cur.Invoice_Amount;
                        }, 0);
                        $('#totamt').html(parseFloat(ordval).toFixed(2));
                        $('#totpend').html(AllOrders.filter(function (a) {
                            return a.Status == "Pending";
                        }).length);
                        $('#totinv').html(AllOrders.filter(function (a) {
                            return a.Status == "Dispatched";
                        }).length);
                        $('#totpenamt').html(parseFloat(pordval).toFixed(2));
                        $('#totinvamt').html(parseFloat(invordval).toFixed(2));
                        ReloadTable();
                    },
                    error: function (result) {
                        $("#OrderList TBODY").html("<tr><td colspan=14>Something went wrong. Try again.</td></tr>");
                    }
                })
            }, 500);
        }
        $(".segment>li").on("click", function () {
            $(".segment>li").removeClass('active');
            $(this).addClass('active');
            filtrkey = $(this).attr('data-va');
            Orders = AllOrders;
            $("#tSearchOrd").val('');
            ReloadTable();
        });
        function ReloadTable() {
            $("#OrderList TBODY").html("");
            if (filtrkey != "All") {
                Orders = Orders.filter(function (a) {
                    return a.Status == filtrkey;
                });
            }
            st = PgRecords * (pgNo - 1);
            for ($i = st; $i < st + Number(PgRecords) ; $i++) {
                if ($i < Orders.length) {
                    tr = $("<tr class='trclick' id='" + Orders[$i].OrderID + "'></tr>");
                    $(tr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].StateName + "</td><td>" + Orders[$i].HQ + "</td><td>" + Orders[$i].FieldForce_Name + "</td><td>" + Orders[$i].Distributor_Code + "</td><td>" + Orders[$i].DistributorName + "</td><td>" + Orders[$i].Route + "</td><td>" + Orders[$i].OrderID + "</td><td>" + Orders[$i].OrderDate + "</td><td>" + Orders[$i].RetailerName + "</td><td></td><td>" + Orders[$i].Order_Amount + "</td><td>" + Orders[$i].Invoice_Amount + "</td><td>" + Orders[$i].Bill_Update_Date + "</td><td>" + Orders[$i].Remarks + "</td><td>" + ((Orders[$i].Imgurl != "") ? ("<img class='picc' style='width: 29px;' src='" + Orders[$i].Imgurl + "' />") : '') + "</td><td><span class='aState' data-val='" + Orders[$i].Status + "'>" + Orders[$i].Status + "</span></td>");
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
                        // if (val != null && val.toString().toLowerCase().indexOf(shText) > -1 && (',' + searchKeys).indexOf(',' + key + ',') > -1) {
                        if (val != null && val.toString().toLowerCase().substring(0, shText.length) == shText && (',' + searchKeys).indexOf(',' + key + ',') > -1) {
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
        function closew() {
            $('#photo1').css("width", "329px");
            $('#photo1').css("height", "585px");
            $('#cphoto1').css("display", 'none');
        }
        function clearOrder() {
            $("#OrderEntry > TBODY").html("");
        }
        function loadsf() {
            $("#OrderEntry > TBODY").html("");
            let ordsf = [];
            let mymap = new Map();
            ordsf = AllOrders.filter(function (el) {
                const val = mymap.get(el.FieldForce_Name);
                if (val) {
                    return false;
                }
                mymap.set(el.FieldForce_Name, el.FieldForce_Name);
                return true;
            }).map(function (a) { return a.FieldForce_Name; }).sort();

            for (var $k = 0; $k < ordsf.length; $k++) {
                let totords = AllOrders.filter(function (a) {
                    return ((ordsf[$k] == a.FieldForce_Name));
                });
                let penords = AllOrders.filter(function (a) {
                    return ((a.Status == "Pending") && (ordsf[$k] == a.FieldForce_Name));
                });
                let disords = AllOrders.filter(function (a) {
                    return ((a.Status == "Dispatched") && (ordsf[$k] == a.FieldForce_Name));
                });
                let ordval = totords.reduce(function (prev, cur) {
                    return prev + cur.Order_Amount;
                }, 0);
                let pordval = penords.filter(function (a) {
                    return a.Status == "Pending";
                }).reduce(function (prev, cur) {
                    return prev + cur.Order_Amount;
                }, 0);
                let invordval = disords.filter(function (a) {
                    return a.Status == "Dispatched";
                }).reduce(function (prev, cur) {
                    return prev + cur.Invoice_Amount;
                }, 0);
                tr = $("<tr></tr>");
                $(tr).html("<td>" + ($k + 1) + "</td><td>" + ordsf[$k] + "</td><td>" + totords.length + "</td><td>" + ordval.toFixed(2) + "</td><td>" + penords.length + "</td><td>" + pordval.toFixed(2) + "</td><td>" + disords.length + "</td><td>" + invordval.toFixed(2) + "</td>");
                $("#OrderEntry > TBODY").append(tr);
            }
        }
        $(document).on('click', '.picc', function () {
            var photo = $(this).attr("src");
            $('#photo1').attr("src", $(this).attr("src"));
            $('#cphoto1').css("display", 'block');
        });
        $(document).ready(function () {
            getCusdets();
            fdt = new Date('<%=fdt%>').toLocaleString("en-IN").split(',')[0];
            tdt = new Date('<%=tdt%>').toLocaleString("en-IN").split(',')[0];
            $('.sub-header').html(`Invoice Upload Report - <%=sfname%> - From ${fdt} To ${tdt}`);
            $('#headg').css('display', 'none');
            dv = $('<div style="z-index: 10000000;overflow:auto;position:fixed;left:50%;top:50%;width:99%;height:99%;transform: translate(-50%, -50%);border-radius: 15px;background:#ececec;display:none" id="cphoto1"></div>');
            $(dv).html('<span style="position: absolute;z-index: 10000000;padding: 5px;cursor: default;background: #a5a0a0;border-radius: 50%;width: 20px;height: 20px;line-height: 6px;text-align: center;border: solid 1px gray;top: 6px;right: 6px;" onclick="closew()">x</span><img style="width:98%;height:98%;border-radius: 15px;object-fit: contain;transform:translate(1%, 1%)" id="photo1" />')
            $("body").append(dv);
            $('#photo1').on('click', function () {
                $('#photo1').css("width", "1080px");
                $('#photo1').css("height", "1920px");
            });
        });
        $(".qqq").on("click", function () {
            $('#exampleModal').modal('toggle');
            loadsf();
        });
    </script>
</asp:Content>
