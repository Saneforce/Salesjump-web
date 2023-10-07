<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mgr_routeDetailsSFwise.aspx.cs" Inherits="MIS_Reports_mgr_routeDetailsSFwise" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>RetailersDetailsSFwise</title>
 <script src='https://cdn.rawgit.com/simonbengtsson/jsPDF/requirejs-fix-dist/dist/jspdf.debug.js'></script>
<script src='https://unpkg.com/jspdf-autotable@2.3.2'></script>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
  <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
  </head>
    
<body>
    <form id="form1" runat="server">
        <div>
      <asp:Panel ID="pnlbutton" runat="server">
            <table width="100%" >
                <tr>
               <td width="70%" align="center" >
                    <asp:Label ID="lblHead" Text="" SkinID="lblMand" Font-Bold="true"  Font-Underline="true"
                runat="server" Font-Size="Medium"></asp:Label>
                    </td>
              </tr>
            </table> 
			 <div class="row" style="margin: 6px 0px 0px 11px;">
            <asp:Label ID="Label2" Text="Manager Name :" runat="server" Style="font-size: larger"></asp:Label>
            <asp:Label ID="lblsf_name" runat="server" Style="font-size: larger"></asp:Label>
        </div>	
    
         </asp:Panel><br /><br />
                        <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                    Style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 15px; top: 43px;" OnClick="ExportToExcel" />
                    <div class="overlay" id="loadover" style="display: none;">
            <div id="loader">Please Wait...</div>
        </div>
                     <div class="card">
            <div class="card-body table-responsive" style="overflow-x: auto;">
                <div style="white-space: nowrap">
                    
                    <label style="float: right">
                        Show
                            <select class="data-table-basic_length" id="pglim" aria-controls="data-table-basic">
                            <option value="50">50</option>
                            <option value="100">100</option>
                            <option value="150">150</option>
                            <option value="200">200</option>
                            <option value="250">250</option>
                            </select>
                        entries</label>
                </div>
                <table class="table table-hover" id="OrderList" style="font-size: 12px">
                    <thead class="text-warning">
                        <tr style="background-color: #37a4c6; color: #fff; white-space: nowrap;">
                        <th style="text-align: left; color: #fff">S.No</th>
                        <th style="text-align: left; color: #fff">State</th>
                        <th style="text-align: left; color: #fff">Reporting Manager</th>
                        <th style="text-align: left; color: #fff">Manager Des</th>
                        <th style="text-align: left; color: #fff">SO Emp_Id</th>
                        <th style="text-align: left; color: #fff">SO Name</th>
                        <th style="text-align: left; color: #fff">HQ</th>
                         <th style="text-align: left; color: #fff">Route Name</th>
                        <th style="text-align: left; color: #fff">Route Type</th>
                        <th style="text-align: left; color: #fff">No Of Outlets</th>
                      </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
                            <div class="row">
                    
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
    </div>
       
    </form>
  <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
     <script language="javascript" type="text/javascript">
         var AllOrders = []; var Allsf = []; var hqsf = []; var Allstate = []; var Ostate = []; var AllHQ = []; var SFHQ = [];
         var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Area_sname,Area_name,state,";
         var rout = [];
         var filtrkey = '0';
         $(document).ready(function () {
             $('#loadover').show();
             setTimeout(function () {
                 $.when(getrouteName(),filldtl()).then(function () {
                     ReloadTable();
                 });
             }, 1000);
             $(document).ajaxStop(function () {
                 $('#loadover').hide();
             });
         });
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
             TotalPg = parseInt(parseInt(rout.length / PgRecords) + ((rout.length % PgRecords) ? 1 : 0)); selpg = 1;
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
             
             st = PgRecords * (pgNo - 1);
             for ($i = st; $i < st + Number(PgRecords); $i++) {
                 if ($i < rout.length) {
                     let sfname = Orders.filter(function (a) {
                         return ((',' + rout[$i].Territory_Code + ',').indexOf(a.Territory_Code) > 0)
                     }).map(function (el) {
                         return el.SF_Name
                     }).join(',');
                     let stname = Orders.filter(function (a) {
                         return ((',' + rout[$i].Territory_Code + ',').indexOf(a.Territory_Code) > 0)
                     }).map(function (el) {
                         return el.StateName
                     }).join(',');
                     let rsf = Orders.filter(function (a) {
                         return ((',' + rout[$i].Territory_Code + ',').indexOf(a.Territory_Code) > 0)
                     }).map(function (el) {
                         return el.rsf
                     }).join(',');
                     let rsfd = Orders.filter(function (a) {
                         return ((',' + rout[$i].Territory_Code + ',').indexOf(a.Territory_Code) > 0)
                     }).map(function (el) {
                         return el.sf_Designation_Short_Name
                     }).join(',');
                     let sfemp = Orders.filter(function (a) {
                         return ((',' + rout[$i].Territory_Code + ',').indexOf(a.Territory_Code) > 0)
                     }).map(function (el) {
                         return el.sf_emp_id
                     }).join(',');
                     let hq = Orders.filter(function (a) {
                         return ((',' + rout[$i].Territory_Code + ',').indexOf(a.Territory_Code) > 0)
                     }).map(function (el) {
                         return el.sf_hq
                     }).join(',');
                     let alty = Orders.filter(function (a) {
                         return ((',' + rout[$i].Territory_Code + ',').indexOf(a.Territory_Code) > 0)
                     }).map(function (el) {
                         return el.Allowance_Type
                     }).join(',');
                     let cnt = Orders.filter(function (a) {
                         return ((',' + rout[$i].Territory_Code + ',').indexOf(a.Territory_Code) > 0)
                     }).map(function (el) {
                         return el.Reatailer_Count
                     }).join(',');
                     str = "<tr><td>" + ($i + 1) + "</td><td>" + stname + "</td><td>" + rsf + "</td><td>" + rsfd + "</td><td>" + sfemp + "</td><td>" + sfname + "</td><td>" + hq + "</td>";
                     str += "<td>" + rout[$i].Territory_Name + "</td><td>" + alty + "</td><td>" + cnt + "</td></tr>";
                     $("#OrderList TBODY").append(str); 
                 }
             }0
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
         });
         function getrouteName() {
             return $.ajax({
                 type: "POST",
                 contentType: "application/json; charset=utf-8",
                 async: true,
                 url: "mgr_routeDetailsSFwise.aspx/getroute",
                 dataType: "json",
                 success: function (data) {
                     rout = JSON.parse(data.d);
                 },
                 error: function (jqXHR, exception) {
                     alert(JSON.stringify(result));
                 }
             });
         }

         function filldtl() {
             $.ajax({
                 type: "POST",
                 contentType: "application/json; charset=utf-8",
                 async: true,
                 url: "mgr_routeDetailsSFwise.aspx/getfilldtl",
                 dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders; ReloadTable();
                }
            });
         }

     
    </script>
</body>
</html>