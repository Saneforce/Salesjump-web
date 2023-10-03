<%@ Page Language="C#" AutoEventWireup="true" CodeFile="view_mgr_reporting.aspx.cs" Inherits="MIS_Reports_view_mgr_reporting" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>view point summary</title>
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
          <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                    Style="height: 31px; width: 40px; border-width: 0px; position: absolute; right: 15px; top: 15px;" OnClick="ExportToExcel"/>
         </asp:Panel><br /><br />
                   
                     
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
                        entries</label>
                </div>
                <table class="table table-hover" id="OrderList" style="font-size: 12px">
                    <thead class="text-warning">
                        <tr style="background-color: #37a4c6; color: #fff; white-space: nowrap;">
                        <th style="text-align: left; color: #fff">S.No</th>
                        <th style="text-align: left; color: #fff">STATE</th>
                        <th style="text-align: left; color: #fff">H.Q</th>
                        <th style="text-align: left; color: #fff">NAME OF MANAGER</th>
                        <th style="text-align: left; color: #fff">WORKING WITH</th> 
                        <th style="text-align: left; color: #fff">ATTENDANCE</th>
                        <th style="text-align: left; color: #fff">REASON</th>
                        <th style="                                text-align: left;
                                color: #fff">SELECTED ROUTE</th>
                        <th style="text-align: left; color: #fff">CALL DATE & TIME</th>
                        <th style="text-align: left; color: #fff">RETAILERS CODE</th>
                        <th style="text-align: left; color: #fff">RETAILERS NAME</th>
                        <th style="text-align: left; color: #fff">AVAILABLE PRODUCT</th>
                        <th style="text-align: left; color: #fff">PRODUCTIVE CALL PRODUCT</th>
                        <th style="text-align: left; color: #fff">REMARKS</th>
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
    </div>
       
    </form>
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
    <script language="javascript" type="text/javascript">
        var AllOrders = []; var Allsf = []; var hqsf = []; var Allstate = []; var Ostate = []; var AllHQ = []; var SFHQ = []; 
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "SF_Name,sf_hq,worked_with_name,Worktype_Name_M,ClstrName,ListedDr_Name,StateName,";
        var routes = [];
        var filtrkey = '0';
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
            var point = 0;
            var str = '';
            st = PgRecords * (pgNo - 1);
            for ($i = st; $i < st + Number(PgRecords); $i++) {
                if ($i < Orders.length) {
                    var arr = Orders[$i].BrndRevw.indexOf('#1#') > -1;
                    if (arr == false) {
                        arr2 = "";
                    }
                    else {
                        tst = Orders[$i].BrndRevw;
                        var arr1 = tst.split('$');
                        var arr2 = arr1.filter(function (a) { return (a).indexOf('#1#') > -1 }).map(function (a) { return a.substring(a.indexOf('#') + 1, a.length - 4) }).join(',');
                    }
                    var arrs = Orders[$i].BrndRevw.indexOf('#0#') > -1;
                    if (arrs == false) {
                        arr2s = "";
                    }
                    else {
                        tsts = Orders[$i].BrndRevw;
                        var arr1s = tsts.split('$');
                        var arr2s = arr1s.filter(function (a) { return (a).indexOf('#0#') > -1 }).map(function (a) { return a.substring(a.indexOf('#') + 1, a.length - 4) }).join(',');
                    }
                    str = "<tr><td>" + ($i + 1) + "</td><td>" + Orders[$i].StateName + "</td><td>" + Orders[$i].sf_hq + "</td><td>" + Orders[$i].SF_Name + "</td><td>" + Orders[$i].worked_with_name + "</td><td>" + Orders[$i].Worktype_Name_M + "</td>";
                    str += "<td>" + Orders[$i].remarks + "</td><td>" + Orders[$i].ClstrName + "</td><td>" + Orders[$i].Adt + "</td><td>" + Orders[$i].RetID + "</td><td>" + Orders[$i].ListedDr_Name + "</td><td>" + arr2 +"</td><td>" + arr2s +"</td><td>" + Orders[$i].Rmks + "</td></tr>"
                    $("#OrderList TBODY").append(str);
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
        });


        function Filldetail() {

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "view_mgr_reporting.aspx/Filldtl",
                async: true,
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders; ReloadTable();
                }
            });
        } 
       $(document).ready(function () {
           Filldetail();
         });
    </script>
</body>
</html>