<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="New_Distributor_Master.aspx.cs" Inherits="MasterFiles_New_Distributor_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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

        @-webkit-keyframes spin {
            0% {
                -webkit-transform: rotate(0deg);
            }

            100% {
                -webkit-transform: rotate(360deg);
            }
        }

        @keyframes spin {
            0% {
                transform: rotate(0deg);
            }

            100% {
                transform: rotate(360deg);
            }
        }
    </style>
	<form runat="server">
        <div class="overlay" id="loadover" style="display: none;">
            <div id="loader"></div>
        </div>
        <div class="row" style="margin-bottom: 1rem;">
            <div class="row" style="margin-bottom: 1rem;">
                <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                    Style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 15px; top: 43px;" OnClick="ExportToExcel" />
            </div>
            <div class="col-lg-12 sub-header">Distributor Master<span style="float: right"><a href="new_distributor_creation.aspx" class="btn btn-primary btn-update" id="newsf">Add Distributor</a></span></div>
        </div>
        <div class="modal fade" id="RouteModal" style="z-index: 10000000; background: transparent; overflow-y: scroll;" tabindex="0" aria-hidden="true">
            <div class="modal-dialog" role="document" style="width: 80% !important">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="RouteModalLabel"></h5>
                    </div>
                    <div class="modal-body" style="padding-top: 10px">
                        <div class="row">
                            <div class="col-sm-12">
                                <table id="routedets" style="width: 100%; font-size: 12px;">
                                    <thead class="text-warning">
                                        <tr>
                                            <th style="text-align: left">SlNO</th>
                                            <th style="text-align: left">Route Name</th>
                                            <th style="text-align: left">Retailer Count</th>
                                            <th style="text-align: left">FieldForce</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
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
        <div class="hrow row">
            <div class="col-sm-4" style="margin-bottom: 1rem;">
                <select id="ddlstate" class="form-control"></select>
            </div>
            <div class="col-sm-4" style="margin-bottom: 1rem;">
                <select id="ddlhq" class="form-control">
                    <option value="">Select HQ</option>
                </select>
            </div>
            <div class="col-sm-4" style="margin-bottom: 1rem;">
                <select id="ddlsf" class="form-control">
                    <option value="">Select FieldForce</option>
                </select>
            </div>
        </div>
        <div class="card">
            <div class="card-body table-responsive" style="overflow-x: auto;">
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
                        entries</label><div style="float: right;padding-top: 3px;">
                    <ul class="segment">
                        <li data-va='All'>ALL</li>
                        <li data-va='0' class="active">Active</li>
                    </ul>
					<input type="hidden" id="selectedTextHiddenField" name="selectedTextHiddenField" />
                </div>
                </div>
                <table class="table table-hover" id="OrderList" style="font-size: 12px">
                    <thead class="text-warning">
                        <tr style="background-color: #37a4c6; color: #fff; white-space: nowrap;">
                            <th style="text-align: left; color: #fff">Sl.No</th>
                            <th style="text-align: left; color: #fff;">Distributor Code</th>
                            <th style="text-align: left; color: #fff;">ERP Code</th>
                            <th style="text-align: left; color: #fff">Distributor Name</th>
							<th style="text-align: left; color: #fff">FieldForce Name</th>
                            <th style="text-align: left; color: #fff">Territory Name</th>
                            <th style="text-align: left; color: #fff">Route Count</th>
							<th style="text-align: left; color: #fff">Retailers</th>
							<th style="text-align: left; color: #fff">Rate Card</th>
                            <th style="text-align: left; color: #fff">Edit</th>
                            <th style="text-align: left; color: #fff">Deactivate</th>
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
	<script src="../js/xlsx.full.min.js"></script>
    <script language="javascript" type="text/javascript">
        var AllOrders = []; var Allsf = []; var hqsf = []; var Allstate = []; var Ostate = []; var AllHQ = []; var SFHQ = [];
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Stockist_Code,Stockist_Name,Territory_name,ERP_Code,";
		var routes = [];
        var filtrkey = '0';
		var state, HQ, HQNm, SF;
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
            }
           );
        }
        function ReloadTable() {
            $("#OrderList TBODY").html("");
            if (filtrkey != "All") {
                Orders = Orders.filter(function (a) {
                    return a.Stockist_Active_Flag == filtrkey;
                })
            }
            st = PgRecords * (pgNo - 1);
            for ($i = st; $i < st + Number(PgRecords) ; $i++) {
                if ($i < Orders.length) {				
                    var filt = routes.filter(function (a) {
                        return (a.territory_code == Orders[$i].Territory_Code) && (a.Dist_Name.indexOf(',' + Orders[$i].Stockist_Code + ',') > -1);
                    });
                    tr = $("<tr rname='" + Orders[$i].Stockist_Name + "' rocode='" + Orders[$i].Stockist_Code + "' roPrice='" + Orders[$i].Price_list_Name + "' roPriceCode='" + Orders[$i].Price_list_Code + "'></tr>");
                    $(tr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].Stockist_Code + "</td><td>" + Orders[$i].ERP_Code + "</td><td>" + Orders[$i].Stockist_Name + "</td><td>" + Orders[$i].FieldForce_Name + "</td><td>" + Orders[$i].Territory_name + "</td><td class='rocount'><a href='#'>" + filt.length + "</a></td><td class='dwnldrets'><a href='#'>Download Retailers</a></td><td class='roPrice'><a href='#'>" + Orders[$i].Price_list_Name+"</a></td><td class='roedit'><a href='#'>Edit</a></td><td class='rodeact'><a href='#'>" + Orders[$i].Status + "</a></td>");
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

        $(".segment>li").on("click", function () {
            $(".segment>li").removeClass('active');
            $(this).addClass('active');
            filtrkey = $(this).attr('data-va');
            Orders = AllOrders;
            $("#tSearchOrd").val('');
            ReloadTable();
			getSelectedText(filtrkey);
        });
        function fillstate() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Distributor_Master.aspx/getStates",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    Allstate = JSON.parse(data.d) || [];
                    var st = $("#ddlstate");
                    st.empty().append('<option selected="selected" value="">Select State</option>');
                    for (var i = 0; i < Allstate.length; i++) {
                        st.append($('<option value="' + Allstate[i].State_Code + '">' + Allstate[i].StateName + '</option>'));
                    };
                }
            });
        }
        function fillroute() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Distributor_Master.aspx/getRouCount",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    routes = JSON.parse(data.d) || [];
                }
            });
        }
		function getSelectedText(key) {
            document.getElementById("selectedTextHiddenField").value = key;
        }
		
        function DownloadDistwiseOutlets($DistId) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "Distributor_Master.aspx/DownloadDistwiseOutlets",
                data: "{'divcode':'<%=Session["div_code"]%>','DistId':'" + $DistId + "'}",
                dataType: "json",
                success: function (data) {
                    let errexl = JSON.parse(data.d) || [];
                    let droptblname = "Outlets";
                    let ws = XLSX.utils.json_to_sheet(errexl);
                    let wb = XLSX.utils.book_new();
                    XLSX.utils.book_append_sheet(wb, ws, droptblname);
                    XLSX.writeFile(wb, "RetailersDownload.xlsx");
                    $('#loadover').hide();
                },
                error: function (res) {
                    alert(JSON.stringify(res));
                    $('#loadover').hide();
                }
            });
        }

        function fillHQ(sst) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Distributor_Master.aspx/getHQ",
                data: "{'divcode':'<%=Session["div_code"]%>','Sstate':'" + sst + "'}",
                dataType: "json",
                success: function (data) {
                    Allsf = JSON.parse(data.d);
                    hqsf = Allsf;
                    var hq = $("#ddlhq");
                    hq.empty().append('<option selected="selected" value="">Select HQ</option>');
                    for (var i = 0; i < hqsf.length; i++) {
                        hq.append($('<option value="' + hqsf[i].Hq_Code + '">' + hqsf[i].Sf_HQ + '</option>'));
                    };
                }
            });
        }

        function fillSF(shq) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Distributor_Master.aspx/getSF",
                data: "{'divcode':'<%=Session["div_code"]%>','Hq':'" + shq + "'}",
                dataType: "json",
                success: function (data) {
                    AllHQ = JSON.parse(data.d);
                    SFHQ = AllHQ;
                    var ddsf = $("#ddlsf");
                    ddsf.empty().append('<option selected="selected" value="">Select Fieldforce</option>');
                    for (var i = 0; i < SFHQ.length; i++) {
                        ddsf.append($('<option value="' + SFHQ[i].Sf_Code + '">' + SFHQ[i].Sf_Name + '</option>'));
                    };
                }
            });
        }
        function fillStockist(sfs) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Distributor_Master.aspx/getStockist",
                data: "{'divcode':'<%=Session["div_code"]%>','sf':'" + sfs + "'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders; ReloadTable();
                }
            });
        }
        function fillRoutes(sfs) {
            $('#routedets tbody').html('');
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Distributor_Master.aspx/getRoute_Retailers",
                data: "{'divcode':'<%=Session["div_code"]%>','stockist_code':'" + sfs + "'}",
                dataType: "json",
                success: function (data) {
                    var AllOrders2 = JSON.parse(data.d) || [];
                    $('#routedets TBODY').html("");
                    var slno = 0;
                    for ($i = 0; $i < AllOrders2.length; $i++) {
                        if (AllOrders2.length > 0) {
                            slno += 1;
                            tr = $("<tr></tr>");
                            $(tr).html("<td>" + slno + "</td><td>" + AllOrders2[$i].Territory_Name + "</td><td>" + AllOrders2[$i].Retailer_Count + "</td><td>" + AllOrders2[$i].Sf_Name + "</td>");
                            $("#routedets TBODY").append(tr);
                        }
                    }
                }
            });
        }
        $(document).ready(function () {
		getSelectedText('0');
            if ('<%=sf_type%>' != '3') {
                $('.pad').css('display', 'none');
				$('.hrow').css('display', 'none');
                $('#<%=ImageButton1.ClientID%>').hide();
            }
            fillstate();
            var local = localStorage.getItem(window.location.pathname.replace(/^.*[\\\/]/, ''));
            if (localStorage.getItem(window.location.pathname.replace(/^.*[\\\/]/, '')) && ('<%=Session["div_code"]%>' == '100' || '<%=Session["div_code"]%>' == '29')) {
                var loc = JSON.parse(localStorage.getItem(window.location.pathname.replace(/^.*[\\\/]/, '')));
                if (loc.length > 0) {
                    fillHQ(loc[0].state);
                    fillSF(loc[0].hqnm);
                    fillroute();
                    $('#ddlstate > option[value="' + loc[0].state + '"]').prop('selected', true);
                    $('#ddlhq > option[value="' + loc[0].hq + '"]').prop('selected', true);
                    $('#ddlsf > option[value="' + loc[0].sfcode + '"]').prop('selected', true);
                    if (loc[0].sfcode!='') {
                        fillStockist(loc[0].sfcode);
                    }
                    else {
                        fillStockist('<%=sf_code%>');
                    }
                }
            }
            else {
                fillroute();
                fillStockist('<%=sf_code%>');
            }
            $('#ddlstate').on('change', function () {
			 state = $(this).val();
                var stt = $(this).val();
                if (stt != '') {
                    fillHQ(stt);
                }
                else {
                    alert('Select State');
                    $('#ddlhq').empty();
                    $('#ddlsf').empty();
                    return false;
                }
            });
            $('#ddlhq').on('change', function () {
			 HQ = $('#ddlhq').val();
                HQNm = $('#ddlhq :selected').text();
                var hqn = $('#ddlhq :selected').text();
                if (hqn != 'Select HQ') {
                    fillSF(hqn);
                }
                else {
                    alert('Select HQ');
                    $('#ddlsf').empty();
                    return false;
                }
            });
            $('#ddlsf').on('change', function () {
			SF = $('#ddlsf').val();
                var sfco = $('#ddlsf').val();
                if (sfco != '') {
                    fillStockist(sfco);
                }
                else {
                    alert('Select Fieldforce');
                    return false;
                }
				HQ = $('#ddlhq').val();
                HQNm = $('#ddlhq :selected').text();
                state = $('#ddlstate').val();
                var item = { div: '<%=Session["div_code"]%>', sfcode: SF, state: state, hq: HQ, hqnm: HQNm };

                namesArr = [];
                namesArr.push(item);
                window.localStorage.setItem(window.location.pathname.replace(/^.*[\\\/]/, ''), JSON.stringify(namesArr));
            });
            $(document).on('click', '.dwnldrets', function () {
                $('#loadover').show();
                let distcode = $(this).closest('tr').attr('rocode');
                setTimeout(function () {
                    DownloadDistwiseOutlets(distcode);
                }, 1000);
            });
            $(document).on('click', '.rocount', function () {
                $('#RouteModal').modal('toggle');
                var route_C = $(this).closest('tr').attr('rocode');
                var route_N = $(this).closest('tr').attr('rname');
                $('#RouteModalLabel').text("Routes for " + route_N);
                fillRoutes(route_C);
            });
            $(document).on('click', '.roedit', function () {
                var route_C = parseInt($(this).closest('tr').attr('rocode'));
				state = $('#ddlstate').val();
                SF = $('#ddlsf').val();
                HQ = $('#ddlhq').val();
                HQNm = $('#ddlhq :selected').text();
                if (state != '' || SF != '' || HQ!='') {
                    window.location.href = "new_distributor_creation.aspx?stockist_code=" + route_C + "&state=" + state + "&HQ=" + HQ + "&SF=" + SF + "&HQNm=" + HQNm + "";
                }
                else {
                    window.location.href = "new_distributor_creation.aspx?stockist_code=" + route_C + "";
                }
            });
			
			 $(document).on('click', '.roPrice', function () {
                var stockist_code = parseInt($(this).closest('tr').attr('rocode'));
                var price_name = parseInt($(this).closest('tr').attr('roPrice'));
                var price_code = parseInt($(this).closest('tr').attr('roPriceCode')); 
                window.open("Price_List_view.aspx?stockist_code=" + stockist_code + "&price_code=" + price_code,"ModalPopUp","null," +"toolbar=no," +"scrollbars=yes," +"location=no," +"statusbar=no," +"menubar=no," +"addressbar=no," +"resizable=yes," +"width=800," +"height=600," +"left = 0," +"top=0" );
                //window.location.href = "Price_List_view.aspx?stockist_code=" + stockist_code + "&price_code=" + price_code ;
            });
			
            $(document).on("click", ".rodeact", function () {
                var route_C = $(this).closest('tr').attr('rocode');
                let oindex = Orders.findIndex(x => x.Stockist_Code == route_C);
                let disstat = Orders[oindex]["Status"].toString();
                let flg = (parseInt(Orders[oindex]["Stockist_Active_Flag"]) == 0) ? 1 : 0;
				var rocnt = isNaN(parseInt($(this).closest('tr').find('.rocount>a').html())) ? 0 : parseInt($(this).closest('tr').find('.rocount>a').html());
                var sfco = $('#ddlsf').val();
                if ((rocnt < 1 && flg == 1) || (flg == 0)) {
                    if (confirm("Do you want " + disstat + " the Distributor ?")) {
						$.ajax({
							type: "POST",
							contentType: "application/json; charset=utf-8",
							async: false,
							url: "Distributor_Master.aspx/deactivateStockist",
							data: "{'stockist_code':'" + route_C + "','stat':'" + flg + "'}",
							dataType: "json",
							success: function (data) {
                                if (data.d == 'Success') {
                                    Orders[oindex]["Stockist_Active_Flag"] = flg;
                                    Orders[oindex]["Status"] = (flg == 0) ? "Deactivate" : "Activate";
                                    ReloadTable();
									alert('Status Changed Successfully...');
								}
								else {
									alert("Status Can't be Changed");
								}
							},
							error: function (result) {
							}
						});
					}
				}
                else {
                    alert("Status Can't be Changed");
                }
            });
        });
    </script>
</asp:Content>

