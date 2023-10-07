<%@ Page Language="C#" AutoEventWireup="true"   MasterPageFile="~/Master_DIS.master"  CodeFile="DMSRetailersDetailsSTwise.aspx.cs" Inherits="MIS_Reports_DMSRetailersDetailsSTwise" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<!DOCTYPE html>
<html xmlns="https://www.w3.org/1999/xhtml">

   <head >

    <title>Retailers</title>
	 <style>
        th{
            white-space:nowrap;
        }
    </style>
</head>
<body>
   <form id="frm1" runat="server" >
  
     <div class="row">    
             <div class="col-lg-12 sub-header" >
         <b>   Distributor  Name :</b><asp:Label ID="Stname" runat="server" Text=""  ForeColor="blue"></asp:Label>                  
                <asp:ImageButton ID="ImageButton1" runat="server"  ImageUrl="~/img/Excel-icon.png"
                        style="height:40px;width:40px;border-width:0px;position: absolute;right: 55px;top: 0px;" OnClick="ExportToExcel" /><span style="float:right;" >
                
                                                                                                           </span>                        
                          
       </div></div>
   
  <div class="row"  style="padding-top:50px;">       
	 <div   class="col-md-12" >
		<div class="card">
			<div class="card-body table-responsive">
            <div style="white-space:nowrap">Search&nbsp;&nbsp;<input type="text" id="tSearchOrd" style="width:250px;" />
              
            <label style="float:right">Show <select class="data-table-basic_length" aria-controls="data-table-basic"><option value="10">10</option><option value="25">25</option><option value="50">50</option><option value="100">100</option></select> entries</label>
            </div>
             <table class="table table-hover" id="OrderList">
                <thead class="text-warning">
                    <tr>                          
                        <th>SlNo</th>
                        <th>Retailer Name</th>
                        <th>Address</th>
                        <th>Mobile Number</th>  
                        <th>Terrritory Name</th>                          
                        <th>Latitude</th>
                        <th>Longitude</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
           
            <div class="row">
                <div class="col-sm-5">
                    <div class="dataTables_info" id="orders_info" role="status" aria-live="polite"></div>
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
                </div>  </div>  
            </div>            
        </div>
    </div>
    </form>
	
      <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script> 
    <script type="text/javascript" src="/js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="/js/plugins/datatables/dataTables.bootstrap.js"></script>
    <script language="javascript" type="text/javascript">



        var AllOrders = [];
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "RetailerName,Address,Territory_Name,";
        $(document).ready(function () {
            loadData();

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
        }

        function ReloadTable() {
            $("#OrderList TBODY").html("");
            st = PgRecords * (pgNo - 1); slno = 0;
            for ($i = st; $i < st + PgRecords; $i++) {


                if ($i < Orders.length) {
                    tr = $("<tr></tr>");
                    slno = $i + 1;
                    $(tr).html('<td>' + slno + '</td><td>' + Orders[$i].RetailerName + '</td><td>' + Orders[$i].Address + '</td><td>' + Orders[$i].MobileNumber + '</td><td>' + Orders[$i].TerrritoryName + '</td><td>' + Orders[$i].Latitude + '</td><td>' + Orders[$i].Longitude + '</td>')

                    $("#OrderList TBODY").append(tr);
                    hq = [];
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
        })
        function loadData() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "DMSRetailersDetailsSTwise.aspx/GetList",
                data: "{'divcode':'<%=Session["div_code"]%>','SF': '<%=Session["Sf_Code"]%>'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders; ReloadTable();
                    /*for ($i = 0; $i < ordList.length; $i++) {
                    tr = $("<tr></tr>");
                    $(tr).html("<td>" + ordList[$i].OrderNo + "</td><td>" + ordList[$i].Order_Date_Disp + "</td><td>" + ordList[$i].Retail + "</td><td>" + ordList[$i].Order_Value.toFixed(2) + "</td><td>" + ordList[$i].Sf_Name + "</td><td>" + ordList[$i].Status + "</td>");
                    $("TBODY").append(tr);
                    }*/
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
        }



    </script>

</body>

</html>

    </asp:Content>