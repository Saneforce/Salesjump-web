<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mgr_RetailersDetailsSFwise.aspx.cs" Inherits="MIS_Reports_mgr_RetailersDetailsSFwise" %>

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
                     <asp:HiddenField ID="cnt" runat="server" />
                  <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                    Style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 15px; top: 43px;" OnClick="ExportToExcel" />
                     <div class="card">
            <div class="card-body table-responsive" style="overflow-x: auto;">
                <div style="white-space: nowrap">
                   
                    <label style="float: right">
                        Show
                            <select class="data-table-basic_length" id="pglim" aria-controls="data-table-basic">
                            <option value="1000">1000</option>
                            <option value="2000">2000</option>
                            <option value="3000">3000</option>
                            <option value="4000">4000</option>
                            <option value="5000">5000</option>
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
                        <th style="text-align: left; color: #fff">DOJ</th>
                        <th style="text-align: left; color: #fff">Route Name</th>
                        <th style="text-align: left; color: #fff">Route Type</th>
                        <th style="text-align: left; color: #fff">RETAILERS NAME</th>
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
             <div class="overlay" id="loadover" style="display: none;">
            <div id="loader"></div>
        </div>
        </div>
    </div>
       
    </form>
  <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
      <s type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>   
	  <script language="javascript" type="text/javascript">
        var AllOrders = []; var Allsf = []; var hqsf = []; var Allstate = []; var Ostate = []; var AllHQ = []; var SFHQ = [];
        var Orders = []; pgNo = 1; PgRecords = 500; TotalPg = 0; searchKeys = "StateName,RSf,rsfdes,SF_Name,HQ,Retailer,";
      var routes = []; var user = []; TotalPg = 0; var ordlen = [];
        var filtrkey = '0';
          var totcnt = $('#<%=cnt.ClientID%>').val();
        $(document).ready(function () {
            //Filllength();
            Filluser();
            Filldetail();
            Fillroutes();
            ReloadTable();
       
            $('#pglim').on('change', function () {
                Filluser();
                Filldetail();
                //Fillroutes();
                ReloadTable();
            });
          
            function loadPgNos() {
                prepg = parseInt($(".paginate_button.previous>a").attr("data-dt-idx"));
                Nxtpg = parseInt($(".paginate_button.next>a").attr("data-dt-idx"));
                $(".pagination").html("");
                TotalPg = parseInt(parseInt(totcnt / PgRecords) + ((totcnt % PgRecords) ? 1 : 0)); selpg = 1;
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
                    pgNo = parseInt($(this).attr("data-dt-idx"));
                    Filluser();
                    Filldetail();
                    //Fillroutes();
                    ReloadTable();
                }
                );
            }
        
        function ReloadTable() {
            $("#OrderList TBODY").html("");
            var point = 0;
            var str = '';
            PgRecords = $("#pglim").val();
            st = PgRecords * (pgNo - 1); slno = 0;
            slno = 0;
            for ($i = 0; $i <= st + PgRecords; $i++) {
                if ($i < Orders.length) {
                    slno = st + $i + 1;
                    var usr = user.filter(function (a) {
                        return a.SF_Code = Orders[$i].Sf_Code
                    });
                    var rts = routes.filter(function (a) {
                        return a.Territory_Code = Orders[$i].Territory_Code
                    });
                
                   
                    str = "<tr><td>" + slno + "</td><td>" + usr[0].StateName + "</td><td>" + usr[0].rsf + "</td><td>" + usr[0].rsfdes + "</td><td>" + usr[0].sf_emp_id + "</td><td>" + usr[0].SF_Name + "</td><td>" + usr[0].sf_hq + "</td><td>" + usr[0].doj + "</td>";
                    str += "<td>" + rts[0].Territory_Name + "</td><td>" + rts[0].Allowance_Type + "</td><td>" + Orders[$i].ListedDr_Name + "</td></tr>"
                    $("#OrderList TBODY").append(str); 
                }
              }
        
            $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")
            loadPgNos();
        }

     
            //function Filllength() {
            //    $.ajax({
            //        type: "Post",
            //        contentType: "application/json; charset=utf-8",
            //        url: "mgr_RetailersDetailsSFwise.aspx/Filllength",
            //        async: false,
            //        dataType: "json",
            //        success: function (data) {
            //            len = JSON.parse(data.d) || [];
            //            ordlen = len[0].cnt;
            //        }
            //    });
            //}


         function Filluser() {
            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "mgr_RetailersDetailsSFwise.aspx/Filluser",
                async: false,
                dataType: "json",
                success: function (data) {
                    userdtl = JSON.parse(data.d) || [];
                    user = userdtl;
                }
            });
        
                }
       function Filldetail() {
        var plim = $("#pglim option:selected").val();
        var plimt = ((pgNo - 1) * plim);
        $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "mgr_RetailersDetailsSFwise.aspx/Filldtl",
                data: "{'pageindex':'" + plimt + "','plimt':'" + plim + "'}",
                async: false,
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders; 
                }
            });
        }
        function Fillroutes() {

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "mgr_RetailersDetailsSFwise.aspx/Fillroute",
                async: false,
                dataType: "json",
                success: function (data) {
                    rt = JSON.parse(data.d) || [];
                    routes = rt; 
                }
            });
        }
        });
    </script>
</body>
</html>