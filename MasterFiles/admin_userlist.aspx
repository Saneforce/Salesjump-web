<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Billing.master" CodeFile="admin_userlist.aspx.cs" Inherits="MasterFiles_admin_userlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<body>
    <form id="form1" runat="server">
        <div class="col-lg-12 sub-header" style="margin-bottom:16px;">Billing Details</div>
         <div class="row" style="margin-top: 1rem;display:none;">
                    <label class="col-md-2  col-md-offset-3  control-label">Company Name</label>
                   <div class="col-md-5 inputGroupContainer">
                <div class="input-group">
                        <select id="ddlcomname">
                            <option value="0">--Select Company Name--</option>
                        </select>
                    </div></div>
                </div>
        <div class="row" style="margin-top: 1rem;display:none;">
          <label class="col-md-2  col-md-offset-3  control-label">Month</label>
            <div class="col-md-5 inputGroupContainer">
                <div class="input-group">
                    <select id="fltpmnth" data-size="5" data-dropup-auto="false">
                        <option value="0">Select Month</option>
                        <option value="1">January</option>
                        <option value="2">February</option>
                        <option value="3">March</option>
                        <option value="4">April</option>
                        <option value="5">May</option>
                        <option value="6">June</option>
                        <option value="7">July</option>
                        <option value="8">August</option>
                        <option value="9">September</option>
                        <option value="10">October</option>
                        <option value="11">November</option>
                        <option value="12">December</option>
                    </select>
                </div></div>
            </div>
        <div class="row" style="margin-top: 1rem;display:none;">
                    <label class="col-md-2  col-md-offset-3  control-label">Year</label>
            <div class="col-md-5 inputGroupContainer">
                  <div class="input-group">
                    <select id="fltpyr1" data-size="5" data-dropup-auto="false">
                    </select>
                </div></div>
            </div>
         <div class="row" style="margin-top: 5px;display:none;">
                <div class="col-md-6  col-md-offset-5">
         <button type="button" class="btn" id="btnview" style="background-color: #1a73e8; color: white;">View</button>
                    </div></div>
        
        <div class="card">

            <div class="card-body table-responsive">

		<div class="row">
            		<div class="col-md-12">
                <div style="white-space: nowrap;margin-top:5px;margin-bottom:10px;">
                    Search&nbsp;&nbsp;<input type="text" autocomplete="off" id="tSearchOrd" style="width: 250px;" />
                    <label style="white-space: nowrap; margin-left: 57px; display: none;">Filter By&nbsp;&nbsp;<select id="txtfilter" name="ddfilter" style="width: 250px; display: none;"></select></label>
                    <label style="float: right;display:none;">
                        Shows
                        <select class="data-table-basic_length" aria-controls="data-table-basic">
                            <option value="10">10</option>
                            <option value="25">25</option>
                            <option value="50">50</option>
                            <option value="100">100</option>
                        </select>
                        entries</label>
                    <select id="fltpyr" data-size="5" data-dropup-auto="false" style="float:right;">
                    </select>
						<ul class="segment" style="float: right;">
                        <li data-va="" class="active">ALL</li>
                            <li data-va="0">Active</li><li data-va="1">Block</li><li data-va="2">Demo</li><li data-va="3">Pilot</li><li data-va="4">Deactivate</li>
                    </ul>
                </div>
</div>
</div>
		<div class="row">
            		<div class="col-md-12">
                <div style="display:block;height:60vh;overflow:auto">
                <table class="table table-hover" id="OrderList" style="font-size: 12px">
                    <thead class="text-warning" style="position:sticky;top:0;background:#fff">
                        <tr style="white-space: nowrap;"><!--background-color: #37a4c6; color: #fff;-->
                            <th style="text-align: left;">Sl.No</th>
                            <th style="text-align: left;">Company Name</th>
                            <th style="text-align: left;">Code</th>
                            <th style="text-align: left;">Jan</th>
                            <th style="text-align: left;">Feb</th>
                            <th style="text-align: left;">Mar</th>
                             <th style="text-align: left;">Apr</th>
                             <th style="text-align: left;">May</th>
                             <th style="text-align: left;">Jun</th>
                             <th style="text-align: left;">Jul</th>
                             <th style="text-align: left;">Aug</th>
                             <th style="text-align: left;">Sep</th>
                             <th style="text-align: left;">Oct</th>
                             <th style="text-align: left;">Nov</th>
                             <th style="text-align: left;">Dec</th>
                            <th style="text-align: left;">Status</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
                </div>
            </div>
</div>
            </div>
        </div>
         <div class="modal fade" id="leaveModal" style="z-index: 10000000; overflow-y: scroll;" tabindex="0" aria-hidden="true">
            <div class="modal-dialog" role="document" style="width: 98% !important">
                <div class="modal-content">
                    <div class="modal-header">
                            <h5 class="modal-title" id="leaveModalLabel"></h5>
                           <button type="button" class="close" style="margin-top: -20px;" data-dismiss="modal"  data-toggle="modal" data-target="#summaryModal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    </div>
                   
                    <div class="modal-body" style="padding-top: 10px">
                        <div class="row">
                            <div class="col-sm-12">
                                  <img style="cursor: pointer;float:right;" src="/img/excel.png" alt="" width="40" height="40" id="btnExportmd">
                                <table id="leavedets" style="width: 100%;font-size:12px;">
                                    <thead class="text-warning" style="background-color:#19a4c6;color:#ffffff;">
                                        <tr>
                                            <th style="text-align: left">S.No</th>
                                            <th style="text-align: left">SF_Code</th>
                                            <th style="text-align: left">FieldForce Name</th>
                                          
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal" data-toggle="modal" data-target="#summaryModal">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
        <script type="text/javascript" src="http://code.jquery.com/jquery-3.3.1.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <script type="text/javascript">
        var filtrkey = ""; 
        $(document).ready(function () {
            bindComdrop();
            fillTpYR();
            
		    $(".segment>li").on("click", function () {
                $(".segment>li").removeClass('active');
                $(this).addClass('active');
                filtrkey = $(this).attr('data-va');
                $("#tSearchOrd").val('');
                Orders = AllOrders;
                ReloadTable();
            });
            
            $("#tSearchOrd").on("keyup", function () {
                ReloadTable();
            })
        });
        $("#fltpyr").on('change', function () {
            getfilldtl();
        });
        var AllOrders = []; var Allsf = []; var hqsf = []; var Allstate = []; var Ostate = []; var AllHQ = []; var SFHQ = [];
        var Orders = []; var comn = [];
      ;
		
        optrights = "<li><a href='#' v='0'>Active</a><a href='#' v='1'>Block</a><a href='#' v='2'>Demo</a><a href='#' v='3'>Pilot</a><a href='#' v='4'>Deactivate</a></li>"
       
        function ReloadTable() {
            $il = 1;
            $("#OrderList TBODY").html(""); var tt = [];var jan=0,feb=0,mar=0,apr=0,may=0,jun=0,jul=0,aug=0,sep=0,oct=0,nov=0,dec=0
            for ($i = 0; $i < comn.length; $i++) {
                tt = Orders.filter(function (a) {
                    return a.Cus_Id == comn[$i].ID; 
                });
                $sva = $("#tSearchOrd").val().toLowerCase();
                $st = comn[$i].Cust_Status;
                if ($st == filtrkey || filtrkey == '') {
                    if ($sva == '' || comn[$i].Cust_Name.toLowerCase().indexOf($sva) > -1) {
                        $surl = ((comn[$i].Cust_DBName.toLowerCase() == "fmcg_live") ? "fmcg.sanfmcg.com" : comn[$i].Cus_Url.toLowerCase() + "." + "salesjump.in");
                        tr = $("<tr></tr>");
                        $(tr).html("<td style='white-space: nowrap;'><span style='display:inline-block;width:8px;height:8px;" + ((($st > 3) ? "" : (($st > 0) ? "background:#f56954;" : (comn[$i].ex == 1) ? "background:#00a65a;" : "background:#f39c12;"))) + "margin-right:5px;'></span>" + $il + "</td><td>" + comn[$i].Cust_Name + "<span style='display: block;font-size: 10px;color: #333333a8;'>" + comn[$i].Cust_Add1 + "</span></td><td>" + comn[$i].ID + "<span style='display: block;font-size: 10px;color: #333333a8;'><a href='http://" + $surl+"' target='_blank'>"+ $surl + "</a></span></td><td style='text-align:right;'><a href='#' onclick='sfleave(\"" + comn[$i].ID + "\",1,\"" + comn[$i].Cust_Name + "\",\"Jan\")'>" + (tt.length > 0 ? tt[0].Jan : '') + "<a/></td><td style='text-align:right;'><a href='#' onclick='sfleave(\"" + comn[$i].ID + "\",2,\"" + comn[$i].Cust_Name + "\",\"Feb\")'>" + (tt.length > 0 ? tt[0].Feb : '') + "<a/></td> " +
                            "<td style='text-align:right;'><a href='#' onclick='sfleave(\"" + comn[$i].ID + "\",3,\"" + comn[$i].Cust_Name + "\",\"Mar\")'>" + (tt.length > 0 ? tt[0].Mar : '') + "<a/></td><td style='text-align:right;'><a href='#' onclick='sfleave(\"" + comn[$i].ID + "\",4,\"" + comn[$i].Cust_Name + "\",\"Apr\")'>" + (tt.length > 0 ? tt[0].Apr : '') + "<a/></td><td style='text-align:right;'><a href='#' onclick='sfleave(\"" + comn[$i].ID + "\",5,\"" + comn[$i].Cust_Name + "\",\"May\")'>" + (tt.length > 0 ? tt[0].May : '') + "<a/></td><td style='text-align:right;'><a href='#' onclick='sfleave(\"" + comn[$i].ID + "\",6,\"" + comn[$i].Cust_Name + "\",\"Jun\")'>" + (tt.length > 0 ? tt[0].Jun : '') + "<a/></td><td style='text-align:right;'><a href='#' onclick='sfleave(\"" + comn[$i].ID + "\",7,\"" + comn[$i].Cust_Name + "\",\"Jul\")'>" + (tt.length > 0 ? tt[0].Jul : '') + "<a/></td><td style='text-align:right;'><a href='#' onclick='sfleave(\"" + comn[$i].ID + "\",8,\"" + comn[$i].Cust_Name + "\",\"Aug\")'>" + (tt.length > 0 ? tt[0].Aug : '') + "<a/></td>  " +
                            "<td style='text-align:right;'><a href='#' onclick='sfleave(\"" + comn[$i].ID + "\",9,\"" + comn[$i].Cust_Name + "\",\"Sep\")'>" + (tt.length > 0 ? tt[0].Sep : '') + "<a/></td><td style='text-align:right;'><a href='#' onclick='sfleave(\"" + comn[$i].ID + "\",10,\"" + comn[$i].Cust_Name + "\",\"Oct\")'>" + (tt.length > 0 ? tt[0].Oct : '') + "<a/></td><td style='text-align:right;'><a href='#' onclick='sfleave(\"" + comn[$i].ID + "\",11,\"" + comn[$i].Cust_Name + "\",\"Nov\")'>" + (tt.length > 0 ? tt[0].Nov : '') + "<a/></td><td style='text-align:right;'><a href='#' onclick='sfleave(\"" + comn[$i].ID + "\",12,\"" + comn[$i].Cust_Name + "\",\"Dec\")'>" + (tt.length > 0 ? tt[0].Dec : '') + "<a/></td>" +
                            "<td menuids='" + comn[$i].ID + "' id='" + comn[$i].ID + "' class='rts' style='text-align:left;' align='center'><ul class='nav' style='margin:0px'><li class='dropdown'><a href='#' style='padding:0px' class='dropdown-toggle' data-toggle='dropdown'>"
                            + "<span><span class='aState' style='color:" + (($st == 1) ? 'red' : ($st == 2) ? 'lightblue' : ($st == 3) ? 'orange' : ($st == 4) ? 'red' : 'green') + "' data-val=" + (($st == 1) ? 'Block' : ($st == 2) ? 'Demo' : ($st == 3) ? 'Pilot' : ($st == 4) ? 'Deactive' : 'Active') + ">" + (($st == 1) ? 'Block' : ($st == 2) ? 'Demo' : ($st == 3) ? 'Pilot' : ($st == 4) ? 'Deactive' : 'Active') + "</span><i class='caret' style='float:right;margin-top:8px;margin-right:0px'></i></span></a>" + "<ul class='dropdown-menu dropdown-custom dropdown-menu-right ddlStatus' style='right:0;left:auto;'>" + optrights + "</ul></li></ul></td>");
                        $("#OrderList TBODY").append(tr);

                        $il++;
                        jan += (tt.length > 0 ? tt[0].Jan : 0);
                        feb += (tt.length > 0 ? tt[0].Feb : 0);
                        mar += (tt.length > 0 ? tt[0].Mar : 0);
                        apr += (tt.length > 0 ? tt[0].Apr : 0);

                        may += (tt.length > 0 ? tt[0].May : 0);
                        jun += (tt.length > 0 ? tt[0].Jun : 0);
                        jul += (tt.length > 0 ? tt[0].Jul : 0);
                        aug += (tt.length > 0 ? tt[0].Aug : 0);

                        sep += (tt.length > 0 ? tt[0].Sep : 0);
                        oct += (tt.length > 0 ? tt[0].Oct : 0);
                        nov += (tt.length > 0 ? tt[0].Nov : 0);
                        dec += (tt.length > 0 ? tt[0].Dec : 0);
                    }
                }
            }
        
            tr = $("<tr  style='position:sticky;bottom:0;background:#fff'></tr>");
            $(tr).html("<td style='white-space: nowrap;' colspan='3'> <b>Total Users</b></td><td>" + jan + "</td><td>" + feb + "</td><td>" + mar + "</td><td>" + apr + "</td><td>" + may + "</td><td>" + jun + "</td><td>" + jul + "</td><td>" + aug + "</td><td>" + sep + "</td><td>" + oct + "</td><td>" + nov + "</td><td>" + dec + "</td><td></td>");
            $("#OrderList TBODY").append(tr);

			$(".ddlStatus>li>a").on("click", function () {
				cStus = $(this).closest("td").find(".aState");
				stus = $(this).attr("v");
				$indx = $(this).closest("tr").index();
				cStusNm = $(this).text();
				if (confirm("Do you want change status " + $(cStus).text() + " to " + cStusNm + " ?")) {
					$(cStus).html(cStusNm);
					sf = comn[$indx].ID;
					$.ajax({
						type: "POST",
						contentType: "application/json; charset=utf-8",
						async: false,
						url: "admin_userlist.aspx/ChangeStatus",
						data: "{'id':'" + sf + "','status':'" + stus + "'}",
						dataType: "json",
						success: function (data) {
							comn[$indx].Active_Flag = stus;
							comn[$indx].Status = cStusNm;
							$(cStus).html(cStusNm);

							ReloadTable();
							alert('Status Changed Successfully...');

						},
						error: function (result) {
						}
					});
				}
			});
        }
		
		
        function sfleave(lsfc,at,com,mnn) {
            var  dtname = lsfc;
            var  mn = at;
            var comn = com;
            var  mnthn = mnn;
            var yr = $("#fltpyr").val();
            $('#summaryModal').modal('hide');

            $("#leaveModal .modal-title").html(com + ' User Details For ' + mnn +' '+ yr);
            $('#leaveModal').modal('toggle');
            $('#leavedets TBODY').html("<tr><td colspan=10>Loading please wait...</td></tr>");
            setTimeout(function () {
                    $.ajax({
                          type: "POST",
                          contentType: "application/json; charset=utf-8",
                          async: false,
                          url: "admin_userlist.aspx/fatdetails",
                        data: "{'divc':'" + dtname + "','month':'" + mn + "','year':'" + yr + "'}",
                          dataType: "json",
                          success: function (data) {
                              AllOrders3 = JSON.parse(data.d) || [];
                              $('#leavedets TBODY').html("");
                              var slno = 0;
                              for ($j = 0; $j < AllOrders3.length; $j++) {
                                  if (AllOrders3.length > 0) {
                                      slno += 1;
                                      tr = $("<tr></tr>");
                                      $(tr).html("<td>" + slno + "</td><td>" + AllOrders3[$j].SF_Code + "</td><td>" + AllOrders3[$j].SF_Name + "</td>");
                                      $("#leavedets TBODY").append(tr);

                                  }
                              }
                          },
                          error: function (result) {
                              $('#leavedets TBODY').html("<tr><td colspan=14>Something went wrong. Try again.</td></tr>");
                              //alert(JSON.stringify(result));
                          }
                      })
            }, 500);
          }


        function getfilldtl() {
           // var div = $("#ddlcomname").val();
          //  var mnth = $("#fltpmnth").val();
            var yr = $("#fltpyr").val();
            if (yr == '0') { yr = (new Date()).getFullYear(); $("#fltpyr").val(yr); }
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "admin_userlist.aspx/getfilldtl",
                data: "{'eyear':'" + yr + "'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders; ReloadTable();
                }
            });
        }

       function bindComdrop() {
          
                $.ajax({
                    type: "Post",
                    contentType: "application/json; charset=utf-8",
                    url: "admin_userlist.aspx/bindcomdrop",
                    dataType: "json",
                    success: function (data) {
                        comn = JSON.parse(data.d) || [];
                       
                        $.each(data.d, function (data, value) {

                            $('#ddlcomname').append($("<option></option>").val(this['Value']).html(this['Text']));
                        });
                        getfilldtl();
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
        };
        function fillTpYR() {
            
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "admin_userlist.aspx/BindDate",
                dataType: "json",
                success: function (data) {
                    var tpyr = $("#fltpyr");
                    tpyr.empty().append('<option value="0">Select Year</option>');
                    for (var i = 0; i < data.d.length; i++) {
                        tpyr.append($('<option value="' + data.d[i].value + '">' + data.d[i].text + '</option>'));
                    };
                }
            });
        };
        $('#btnExportmd').click(function () {

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
            htmls = document.getElementById("leavedets").innerHTML;


            var ctx = {
                worksheet: 'Worksheet',
                table: htmls
            }
            var link = document.createElement("a");
            var tets = 'BillingUserDetails' + '.xls';   //create fname

            link.download = tets;
            link.href = uri + base64(format(template, ctx));
            link.click();
        });
        </script>
</asp:Content>
