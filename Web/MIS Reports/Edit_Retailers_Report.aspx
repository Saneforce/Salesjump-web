<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Edit_Retailers_Report.aspx.cs" Inherits="MIS_Reports_Edit_Retailers_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <form runat="server">
        <div class="row">
            <div class="row">
                <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                    Style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 15px; top: 43px;" OnClick="lnkDownload_Click" />
            </div>
            <div class="col-lg-12 sub-header">
                Edit Retailers Report <span style="float: right; margin-right: 15px;">
                    <div id="reportrange" class="form-control mt-5 mb-5" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc;"><i class="fa fa-calendar"></i>&nbsp;<span id="ordDate"></span><i class="fa fa-caret-down"></i></div>
                </span>
            </div>
        </div>
        <div class="card">
            <div class="card-body table-responsive" style="overflow-x: auto;">
                <div style="white-space: nowrap">
                    Search&nbsp;&nbsp;<input type="text" id="tSearchOrd" style="width: 250px;" />
                    <span id="filspan" style="margin-left: 10px;">Filter By&nbsp;&nbsp;            
                        <select class="form-control" id="txtfilter" name="ddfilter" style="min-width: 315px !important; max-width: 318px !important; display: inline"></select>
                    </span>
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
                        <tr style="background-color: #37a4c6; color: #fff;white-space:nowrap;">
                            <th style="text-align: left; color: #fff">Sl.No</th>
                            <th style="text-align: left; color: #fff">Edited Date</th>
                            <th style="text-align: left; color: #fff;">Code</th>
                            <th style="text-align: left; color: #fff">State</th>
                            <th style="text-align: left; color: #fff">HQ</th>
                            <th style="text-align: left; color: #fff">Manager Name</th>
                            <th style="text-align: left; color: #fff">SR Name</th>
                            <th style="text-align: left; color: #fff">Retailer Name</th>
                            <th style="text-align: left; color: #fff">Route</th>
                            <th style="text-align: left; color: #fff">Address</th>
                            <th style="text-align: left; color: #fff">City</th>
                            <th style="text-align: left; color: #fff">Pin Code</th>
                            <th style="text-align: left; color: #fff">Mobile</th>
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
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
    <script type="text/javascript">
        var AllOrders = [];
        var sftyp = '<%=Session["sf_type"]%>';
        var SFHQs = [];
        var SFMGRs = [];
        var Arrsum = [];
        var AllOrders2 = [];
        var sf;
        var sf1;
        var fdt = '';
        var tdt = '';
        var filtrkey = 'All';
        var fffilterkey = 'AllFF';
        var sortid = '';
        var asc = true;//
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "ListedDrCode,ListedDr_Name,sf_Code,City,Created_Date,Territory_Name,ListedDr_Phone,";
        $(".data-table-basic_length").on("change",
            function () {
                pgNo = 1;
                PgRecords = $(this).val();
                ReloadTable();
            }
        );
        $('th').on('click', function () {
            sortid = this.id;
            asc = $(this).attr('asc');
            if (asc == undefined) asc = 'true';
            Orders.sort(function (a, b) {
                if (a[sortid].toLowerCase() < b[sortid].toLowerCase() && asc == 'true') return -1;
                if (a[sortid].toLowerCase() > b[sortid].toLowerCase() && asc == 'true') return 1;

                if (b[sortid].toLowerCase() < a[sortid].toLowerCase() && asc == 'false') return -1;
                if (b[sortid].toLowerCase() > a[sortid].toLowerCase() && asc == 'false') return 1;
                return 0;
            });

            $(this).attr('asc', ((asc == 'true') ? 'false' : 'true'));
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
            }
            );
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
            st = PgRecords * (pgNo - 1);
            for ($i = st; $i < st + Number(PgRecords); $i++) {
                if ($i < Orders.length) {
                    tr = $("<tr></tr>");
                    var drsf = Orders[$i].sf_Code;
                    drsf = drsf.split(',');
                    var sf_name = '';
                    for ($j = 0; $j < drsf.length; $j++) {
                        var drsfn = SFMGRs.filter(function (a) {
                            return a.Sf_Code == drsf[$j];
                        });
                        sf_name += drsfn[0].SFNA+',';
                    }
                    $(tr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].Created_Date + "</td><td>" + Orders[$i].ListedDrCode + "</td><td>" + drsfn[0].StateName + "</td><td>" + drsfn[0].sf_hq + "</td><td>" + drsfn[0].Reporting_To + "</td><td>" + sf_name + "</td><td>" + Orders[$i].ListedDr_Name + "</td><td>" + Orders[$i].Territory_Name + "</td><td>" + Orders[$i].Address + "</td><td>" + Orders[$i].City + "</td><td>" + Orders[$i].PinCode + "</td><td>" + Orders[$i].ListedDr_Phone + "</td>");
                    //$(tr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].ListedDrCode + "</td><td>" + Orders[$i].ListedDr_Name + "</td><td>" + sf_name + "</td><td>" + stk_name + "</td><td>" + Orders[$i].Created_Date + "</td><td>" + Orders[$i].Territory_Name + "</td><td>" + Orders[$i].Address + "</td><td>" + Orders[$i].ListedDr_Phone + "</td>");
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
        });
        function loadData1() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Edit_Retailers_Report.aspx/GetSFDetails",
                data: "{'SF':'<%=Session["Sf_Code"]%>','Div':'<%=Session["Division_Code"]%>'}",
                dataType: "json",
                success: function (data) {
                    SFMGRs = JSON.parse(data.d) || [];
                    var sft = $('#txtfilter').empty().append('<option selected="selected" value="0">Select Fieldforce</option>');
                    for ($i = 0; $i < SFMGRs.length; $i++) {
                        sft.append($('<option value="' + SFMGRs[$i].Sf_Code + '">' + SFMGRs[$i].SFNA + '</option>'));
                    }
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
        }
        $('#txtfilter').on('change', function () {
            $xsf = $(this).val();
            Orders = AllOrders;
			if($xsf!='0'){
				Orders = Orders.filter(function (a) {
					return (a.sf_Code).indexOf($xsf) > -1;
				});
			}
            ReloadTable();
        })
        function loadData() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Edit_Retailers_Report.aspx/GetDetails",
                data: "{'SF':'<%=Session["Sf_Code"]%>','Div':'<%=Session["Division_Code"]%>','Mn':'" + fdt + "','Yr':'" + tdt + "'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders;
                    loadData1(); 
                    ReloadTable();
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
        }
        $(document).ready(function () {
            sf = '<%=Session["Sf_Code"]%>';
            loadData();
            $("#reportrange").on("DOMSubtreeModified", function () {
                id = $('#ordDate').text();
                id = id.split('-');
                fdt = id[2].trim() + '-' + id[1] + '-' + id[0];
                tdt = id[5] + '-' + id[4] + '-' + id[3].trim();
                loadData();
            });
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