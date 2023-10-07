<%@ Page Title="" Language="C#" MasterPageFile="~/mail.master" AutoEventWireup="true" CodeFile="Instant_Notification_list.aspx.cs" Inherits="MIS_Reports_Instant_Notification_list" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <form id="frm1" runat="server">
        <%--<div class="row">            
                <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                        style="height:40px;width:40px;border-width:0px;position: absolute;right: 15px;top: 55px;" OnClick="ExportToExcel" />
        </div>--%>
    <div class="row">
        <div class="col-lg-12 sub-header">Notification<span style="float:right"><a href="#" class="btn btn-primary btn-update" id="newsf">Add New</a></span></div>
    </div>        

    <div class="card">
        <div class="card-body table-responsive">
            <div style="white-space:nowrap">Search&nbsp;&nbsp;<input type="text" autocomplete="off" id="tSearchOrd" style="width:250px;" />                
            <label style="float:right">Show <select class="data-table-basic_length" aria-controls="data-table-basic"><option value="10">10</option><option value="25">25</option><option value="50">50</option><option value="100">100</option></select> entries</label>
            </div>
             <table class="table table-hover" id="Notify">
                <thead class="text-warning">
                    <tr>   
                        <th>SlNO</th>                       
                        <th> Create Date</th>
                        <th>Field Force</th>
                        <th>Notification</th> 
                        <th>Notification Sent Date</th>                                                                    
                       <th style="text-align:center">Edit</th>
                        <th style="text-align:center">Delete</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
            <div class="row">
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
                </div>
            </div>            
        </div>
    </div>
        </form>
     <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
    <script language="javascript" type="text/javascript">
        var AllOrders = [];
        var divcode;
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys =  "SF_Name,Notification_Message,Created_Date,Start_Date,End_Date";
        $(".data-table-basic_length").on("change",
            function () {
                pgNo = 1;
                PgRecords = $(this).val();
                ReloadTable();
            }
        );


        function sfdeact(x) {
            if (confirm("do you want Delete the Notification??")) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Instant_Notification_list.aspx/notifyDelete",
                    data: "{'sl_no':'" + x + "','Divcode':'" + divcode + "'}",
                    dataType: "json",
                    success: function (data) {
                        var i = data.d;
                        if (i >= 1) {
                            alert('Deleted Successfully');
                            loadData();
                        }
                        else {
                            alert('Deleted Failed');
                        }
                    },
                    error: function (rs) {
                        alert(JSON.stringify(result));
                    }
                });
            }
        }
        function loadPgNos() {
            $(".pagination").html("");
            TotalPg = ((Orders.length / PgRecords) + ((Orders.length % PgRecords) ? 1 : 0)).toFixed(0);
            spg = '<li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">Previous</a></li>';
            for (il = 0; il < TotalPg; il++) {
                spg += '<li class="paginate_button' + ((pgNo == (il + 1)) ? " active" : "") + '"><a href="#" aria-controls="example2" data-dt-idx="' + (il + 1) + '" tabindex="0">' + (il + 1) + '</a></li>';
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

        function ReloadTable() {
            $("#Notify TBODY").html("");
            st = PgRecords * (pgNo - 1); slno = 0;
            for ($i = st; $i < st + PgRecords; $i++) {
                //var filtered = Orders.filter(function (x) {
                //    return x.Sf_Code != 'admin';
                //})
                if ($i < Orders.length) {
                    tr = $("<tr></tr>");
                    //var hq = filtered[$i].Sf_Name.split('-');
                    slno = $i + 1;    
                 
                       
                    $(tr).html('<td>' + slno + '</td><td>' + Orders[$i].start_Date + '</td><td>' + Orders[$i].SF_Name + '</td><td>' + Orders[$i].Notification_Message + '</td><td>' + Orders[$i].start_Date + '</td><td id=' + Orders[$i].Trans_Sl_No + ' class="sfedit" align="center"><a href="#">Edit</a></td><td align="center" onclick="sfdeact(' + Orders[$i].Trans_Sl_No + ')"><a href="#">Delete</a></td>');

                    $("#Notify TBODY").append(tr);
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
                url: "Instant_Notification_list.aspx/getnotice_board",               
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders; ReloadTable();
                   
                },
                error: function (result) {
                   // alert(JSON.stringify(result));
                }
            });
         }
        $(document).ready(function () {
            var sf;
            if (<%=Session["sf_type"]%>== 4) {
                sf =<%=Session["Sf_Code"]%>;
                divcode ='<%=Session["Division_Code"]%>';
                var divcode1 = divcode.split(',');
                divcode = divcode1[0];
            }
            else {
                sf = 'admin';
                divcode = Number(<%=Session["div_code"]%>);
            }
            loadData();
            $(document).on('click', '#newsf', function () {
                window.location.href = "Instant_notification.aspx";
            });

            $(document).on('click', '.sfedit', function () {
                x = this.id;
                window.location.href = "Instant_notification.aspx?Trans_sl_no=" + x;
            });

            //$('select[name="ddfilter"]').change(function () {
            //    var r = $(this).val();
            //    loadData(r);
            //});
        });
 </script>
</asp:Content>

