<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="rpt_Event_capture_Closing.aspx.cs" Inherits="MasterFiles_Reports_rpt_Event_capture_Closing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <form id="frm1" runat="server">
        <div class="row">            
                <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                        style="height:40px;width:40px;border-width:0px;position: absolute;right: 15px;top: 55px;" OnClick="ExportToExcel" />
        </div>
    <div class="row"> 
       <b>  <asp:Label ID="lblhead" runat="server"></asp:Label></b>
    
    <div class="card">
        <div class="card-body table-responsive">
            <div style="white-space:nowrap">Search&nbsp;&nbsp;<input type="text" id="tSearchOrd" style="width:250px;" />
              <%--   <label style="float:right">Show <select class="data-table-basic_length" aria-controls="data-table-basic"><option value="10">10</option><option value="25">25</option><option value="50">50</option><option value="100">100</option></select> entries</label>--%>
            </div>
             <table class="table table-hover" id="ClosingEvent">
                <thead class="text-warning">
                    <tr>                          
                        <th>SlNo</th>                        
                        <th>FieldForce Name</th>
                        <th>Distributor Name</th>  
                        <th>Date</th>                          
                        <th>Image</th>                       
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
       <%--     <div class="row">
                <div class="col-sm-5">
                    <div class="dataTables_info" id="orders_info" role="status" aria-live="polite">Showing 1 to 10 of 57 entries</div>
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
                </div>--%>
            </div>            
        </div>
    </div>
    </form>
    <style>
        .phimg img {
    border-radius: 5px;
    cursor: pointer;
    transition: 0.3s;
}

.phimg img:hover {opacity: 0.7;}

    </style>
     	<script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
      <script language="javascript" type="text/javascript">
        var AllOrders = [];
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "DistributorName,FieldForce";
        $(".data-table-basic_length").on("change",
        function () {
            pgNo = 1;
            PgRecords = $(this).val();
            ReloadTable();
        });
        
        function loadPgNos() {
            prepg = parseInt($(".paginate_button.previous>a").attr("data-dt-idx"));
            Nxtpg = parseInt($(".paginate_button.next>a").attr("data-dt-idx"));
            $(".pagination").html("");
            TotalPg = (Orders.length / PgRecords) + ((Orders.length % PgRecords) ? 1 : 0); selpg = 1;
            if (isNaN(prepg)) prepg = 0;
            if (isNaN(Nxtpg)) Nxtpg = 2;
            if ((prepg + 1) == pgNo && pgNo>1) selpg = (parseInt(pgNo) - 1);
            if ((Nxtpg - 1) == pgNo) selpg = (parseInt(pgNo) + 1) - 7;
            spg = '<li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="' + (selpg - 1) + '" tabindex="0">Previous</a></li>';
            for (il = selpg-1;il<selpg+7; il++) {
               if( il < TotalPg)
                spg += '<li class="paginate_button'+((pgNo==(il+1))?" active":"")+'"><a href="#" aria-controls="example2" data-dt-idx="' + (il+1) + '" tabindex="0">' + (il+1) + '</a></li>';
            }
            spg += '<li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="' + (il + 1) + '" tabindex="0">Next</a></li>';
            $(".pagination").html(spg);

            $(".paginate_button > a").on("click", function () {
                pgNo = $(this).text(); ReloadTable();
               /* $(".paginate_button").removeClass("active");
                $(this).closest(".paginate_button").addClass("active");*/
            }
           );
        }
        function loadData() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                //   async: false,
                url: "rpt_Event_capture_Closing.aspx/GetList",              
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
        function ReloadTable() {
           
            $("#ClosingEvent TBODY").html("");
            st = PgRecords * (pgNo - 1); slno = 0;
            if (Orders.length > 0) {
                for ($i = 0; $i < Orders.length; $i++) {

                    tr = $("<tr></tr>");
                    slno = $i + 1;
                    var img1 = '<img class="picc"  src = "http://fmcg.sanfmcg.com/photos/' + Orders[$i].Eventcapture + '" width="40" height="40"  />';
                    //var img1 = '<a href="http://fmcg.sanfmcg.com/photos/' + Orders[$i].Eventcapture + '"><img src = "http://fmcg.sanfmcg.com/photos/' + Orders[$i].Eventcapture + '" /></a>';
                    $(tr).html('<td>' + slno + '</td><td>' + Orders[$i].FieldForce + '</td><td>' + Orders[$i].DistributorName + '</td><td>' + Orders[$i].Insert_Date_Time + '</td><td>'+img1+'</td>');

                  
                    $("#ClosingEvent TBODY").append(tr);
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
                        if (val != null && val.toString().toLowerCase().indexOf(shText) > -1 && (',' + searchKeys).indexOf(',' + key + ',')>-1)
                        {
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
       
        $(document).ready(function () {
            dv = $('<div style="position:fixed;left:50%;top:50%;width:50%;height:50%;transform: translate(-50%, -50%);border-radius: 15px;display:none" id="cphoto1"></div>');
            $(dv).html('<span style="position: absolute;padding: 5px;cursor: default;background: #dcd6d652;border-radius: 50%;width: 20px;height: 20px;line-height: 6px;text-align: center;border: solid 1px gray;top: 6px;right: 6px;" onclick="closew()">x</span><img style="width:100%;height:100%;border-radius: 15px;" id="photo1" />')
            $("body").append(dv);
            loadData();      
            $(document).on('click', '.picc',function () {
                // alert("hi");
                var photo = $(this).attr("src");
                $('#photo1').attr("src", $(this).attr("src"));
                $('#cphoto1').css("display", 'block');
                // $(this).append('<div style="width: 100%" ><img src="' + photo + '"/></div>'

            });
           
        });
        function closew() {
            $('#cphoto1').css("display", 'none');
        }  
    </script>
</asp:Content>

