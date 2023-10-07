
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cus_sale_report_view.aspx.cs" Inherits="MIS_Reports_cus_sale_report_view" %>
<!DOCTYPE html>    
<html xmlns="http://www.w3.org/1999/xhtml">  
    <head runat="server">
        <meta charset="utf-8" />
        <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'>
        <meta name="description" content="Developed By M Abdur Rokib Promy">
        <meta name="keywords" content="Admin, Bootstrap 3, Template, Theme, Responsive">
        <title>Datatable Example</title>      

        <link href="../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <link href="../css/Stockist.css" rel="stylesheet" type="text/css" />
        
        <link href="../../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
        <link href="../../css/style.css" rel="stylesheet" />
        
        <%--<link href="../Content/DataTables/css/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />--%>

        <link href="https://cdn.datatables.net/1.13.1/css/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />
        <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.11.4/css/jquery.dataTables.min.css" />

</head>    
<body>    
    <form id="form1" runat="server">    
        <div class="container" style="max-width: 100%; width: 100%">
         
            <div class="row">
                <div class="col-md-12">
                    <div class="col-md-4" style="float:right;">
                        <asp:HiddenField ID="hfSfCode" runat="server" />
                        <asp:HiddenField ID="hfdivcode" runat="server" />
                        <asp:HiddenField ID="hfsfname" runat="server" />
                        <asp:HiddenField ID="hfsubdiv_code" runat="server" />
                        <asp:HiddenField ID="hfyear" runat="server" />
                         <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                         Style="height: 40px; width: 40px" OnClick="lnkDownload_Click" />
                    </div>                   
                </div>
            </div>
            
            <div class="row">
                <table id="example" class="table table-bordered table-striped table-hover table-heading table-datatable" 
                    style="width:100%; font-size:14px !important">
                    <thead>
                        <tr>
                            <th id="Id">S.No</th>
                            <th id="Territory_Name">Route</th>
                            <th id="HQ">HQ</th>
                            <th id="Outlet_Code">Customer Code</th>
                            <th id="Outlet_Name">Customer Name</th>
                            <th id="Category">Category</th>
                            <th id="Channel">Channel</th>
                            <th id="Phone">Phone</th>
                            <th id="Address">Address</th>
                            <th id="Janv">Jan Visit</th><th id="Jan">Jan</th>
                            <th id="Febv">Feb Visit</th><th id="Feb">Feb</th>
                            <th id="Marv">Mar Visit</th><th id="Mar">Mar</th>
                            <th id="Aprv">Apr Visit</th><th id="Apr">Apr</th>
                            <th id="Mayv">May Visit</th><th id="May">May</th>
                            <th id="Junv">Jun Visit</th><th id="Jun">Jun</th>
                            <th id="Julv">Jul Visit</th><th id="Jul">Jul</th>
                            <th id="Augv">Aug Visit</th><th id="Aug">Aug</th>
                            <th id="Sepv">Sep Visit</th><th id="Sep">Sep</th>
                            <th id="Octv">Oct Visit</th><th id="Oct">Oct</th>
                            <th id="Novv">Nov Visit</th><th id="Nov">Nov</th>
                            <th id="Decv">Dec Visit</th><th id="Dec">Dec</th>
                            <th id="Total">Total</th>
                        </tr>
                    </thead>
                </table>
            </div>
        </div>    
    </form>  
    
    <script src="../js/lib/xls.core.min.js" type="text/javascript"></script>
    <script src="../js/lib/xlsx.core.min.js" type="text/javascript"></script>
    <script src="../js/lib/import_data.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-3.6.0.js" type="text/javascript"></script>
    
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-json/2.6.0/jquery.json.min.js"></script>
   
    <script src="../Scripts/DataTables/jquery.dataTables.js"></script>
  
    <script src="https://cdn.datatables.net/1.13.1/js/jquery.dataTables.min.js"></script>

     <script type="text/javascript">    
         $(document).ready(function () {

             var sfcode = $("#<%=hfSfCode.ClientID%>").val();
             var divcode = $("#<%=hfdivcode.ClientID%>").val();
             var SubDiv = $("#<%=hfsubdiv_code.ClientID%>").val();
             var year = $("#<%=hfyear.ClientID%>").val();

             //var url = window.location.href;
             //var newurl = new URL(url)
             //var subdiv = newurl.searchParams.get('SubDiv');
             //var sfc = newurl.searchParams.get('SfCode');
             //var sfn = newurl.searchParams.get('sfName');
             //var year = newurl.searchParams.get('yr');


             BindData(divcode, sfcode, SubDiv, year)

         });

         function BindData(divcode, sfcode, SubDiv, year) {             
             // Report DataTables Init
             // ===========================================
             $('#example').DataTable({
                 processing: true,
                 serverSide: true,
                 lengthMenu: [
                     [500, 1000, 1500, -1],
                     [500, 1000, 1500, 'All'],
                 ],
                 scrollY: 600,
                 scrollX: true,
                 ajax: {

                     type: "POST",
                     url: "/WebServiceDataTable.asmx/GetDataForDataTable1",
                     data: { divcode: divcode, sfcode: sfcode, subdiv: SubDiv, years: year },

                     dataType: "Json"

                 },
                 "columns": [
                     { 'data': 'Id' },
                     { 'data': 'Route' },
                     { 'data': 'HQ' },
                     { 'data': 'Customer_Code' },
                     { 'data': 'Customer_Name' }, { 'data': 'Category' }, { 'data': 'Channel' }, { 'data': 'Phone' },
                     { 'data': 'Address' }, { 'data': 'Janv' }, { 'data': 'Jan' }, { 'data': 'Febv' }, { 'data': 'Feb' },
                     { 'data': 'Marv' }, { 'data': 'Mar' }, { 'data': 'Aprv' }, { 'data': 'Apr' }, { 'data': 'Mayv' },
                     { 'data': 'Mar' }, { 'data': 'Junv' }, { 'data': 'Jun' }, { 'data': 'Julv' }, { 'data': 'Jul' },
                     { 'data': 'Augv' }, { 'data': 'Aug' }, { 'data': 'Sepv' }, { 'data': 'Sep' }, { 'data': 'Octv' },
                     { 'data': 'Oct' }, { 'data': 'Novv' }, { 'data': 'Nov' }, { 'data': 'Decv' }, { 'data': 'Dec' },
                     { 'data': 'Total' }
                 ]
                 //,"order": [[1, "asc"]]
             });
         }
     </script>    
</body>   
</html>     











<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cus_sale_report_view.aspx.cs" Inherits="MIS_Reports_cus_sale_report_view" %>

<!DOCTYPE html>
<html lang="en" xmlns="https://www.w3.org/1999/xhtml">
    <head id="Head1">
        <title></title>

        
        <link href="css/font-awesome.min.css" rel="stylesheet" type="text/css" />       
        <link href="../css/style1.css" rel="stylesheet" type="text/css" />
        <link href="../css/bootstrap.min.css" type="text/css" rel="stylesheet" />

        <style type="text/css">
            #ddlsf_chzn {
                width: 300px !important;
                font-weight: 500;
            }

            #txtfilter_chzn {
                width: 300px !important;
                top: 10px;
            }

           
            .table {
                width: 100%;
                max-width: 100%;
                margin-bottom: 1rem;
            }

                .table th,
                .table td {
                    padding: 0.75rem;
                    vertical-align: top;
                    border-top: 1px solid #eceeef;
                }

                .table thead th {
                    vertical-align: bottom;
                    border-bottom: 2px solid #eceeef;
                }

                .table tbody + tbody {
                    border-top: 2px solid #eceeef;
                }

                .table .table {
                    background-color: #fff;
                }

            .table-sm th,
            .table-sm td {
                padding: 0.3rem;
            }

            .table-bordered {
                border: 1px solid #eceeef;
            }

            .table-bordered th,
            .table-bordered td {
                border: 1px solid #eceeef;
            }

            .table-bordered thead th,
            .table-bordered thead td {
                border-bottom-width: 2px;
            }

            .table-striped tbody tr:nth-of-type(odd) {
                background-color: rgba(0, 0, 0, 0.05);
            }

            .table-hover tbody tr:hover {
                background-color: rgba(0, 0, 0, 0.075);
            }

            .table-active,
            .table-active > th,
            .table-active > td {
                background-color: rgba(0, 0, 0, 0.075);
            }

            .table-hover .table-active:hover {
                background-color: rgba(0, 0, 0, 0.075);
            }

            .table-hover .table-active:hover > td,
            .table-hover .table-active:hover > th {
                background-color: rgba(0, 0, 0, 0.075);
            }

            .table-success,
            .table-success > th,
            .table-success > td {
                background-color: #dff0d8;
            }

            .table-hover .table-success:hover {
                background-color: #d0e9c6;
            }

            .table-hover .table-success:hover > td,
            .table-hover .table-success:hover > th {
                background-color: #d0e9c6;
            }

            .table-info,
            .table-info > th,
            .table-info > td {
                background-color: #d9edf7;
            }

            .table-hover .table-info:hover {
                background-color: #c4e3f3;
            }

            .table-hover .table-info:hover > td,
            .table-hover .table-info:hover > th {
                background-color: #c4e3f3;
            }

            .table-warning,
            .table-warning > th,
            .table-warning > td {
                background-color: #fcf8e3;
            }

            .table-hover .table-warning:hover {
                background-color: #faf2cc;
            }

            .table-hover .table-warning:hover > td,
            .table-hover .table-warning:hover > th {
                background-color: #faf2cc;
            }

            .table-danger,
            .table-danger > th,
            .table-danger > td {
                background-color: #f2dede;
            }

            .table-hover .table-danger:hover {
                background-color: #ebcccc;
            }

            .table-hover .table-danger:hover > td,
            .table-hover .table-danger:hover > th {
                background-color: #ebcccc;
            }

            .thead-inverse th {
                color: #fff;
                background-color: #292b2c;
            }

            .thead-default th {
                color: #464a4c;
                background-color: #eceeef;
            }

            .table-inverse {
                color: #fff;
                background-color: #292b2c;
            }

            .table-inverse th,
            .table-inverse td,
            .table-inverse thead th {
                border-color: #fff;
            }

            .table-inverse.table-bordered {
                border: 0;
            }

            .table-responsive {
                display: block;
                width: 100%;
                overflow-x: auto;
                -ms-overflow-style: -ms-autohiding-scrollbar;
            }

            .table-responsive.table-bordered {
                border: 0;
            }

             th {
                white-space: nowrap;
                cursor: pointer;
            }


        </style>

         <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.0.2/jquery.min.js"></script>
        <script src="../js/lib/xls.core.min.js" type="text/javascript"></script>
        <script src="../js/lib/xlsx.core.min.js" type="text/javascript"></script>
        <script src="../js/lib/import_data.js" type="text/javascript"></script>

        <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
        <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
          
    
    </head>
    <body>
        <form id="form1" runat="server">
            
            <div class="row">
                       
                <div class="card" style="margin-left:1% !important;border:1px solid black;">
                    <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                         Style="height: 40px; float:right; width: 40px;padding:15px;margin-left:15px; " OnClick="lnkDownload_Click" />
                    <div class="card-body table-responsive">
                        <br />
                        <div style="white-space: nowrap">Search&nbsp;&nbsp;<input type="text" id="tSearchOrd" style="width: 250px;" />
                            <label style="float: right">
                                Show
                                <select class="data-table-basic_length" aria-controls="data-table-basic">
                                    <option value="1000">1000</option>
                                    <option value="1500">1500</option>
                                    <option value="2000">2000</option>
                                    <option value="2500">2500</option>
                                </select>entries</label>
                        </div>
                        <br />
                        <table class="table table-hover table-responsive" id="OrderList" style="font-size: 12px">
                            <thead class="text-warning">
                                <tr>
                                    <th style="text-align: left">S.No</th>
                                    <th id="Territory_Name" style="text-align: left">Route</th>
                                    <th id="HQ" style="text-align: left">HQ</th>
                                    <th id="Outlet_Code" style="text-align: left">Customer Code</th>
                                    <th id="Outlet_Name" style="text-align: left">Customer Name</th>
                                    <th id="Category" style="text-align: left">Category</th>
                                    <th id="Channel" style="text-align: left">Channel</th>
                                    <th id="Phone" style="text-align: left">Phone</th>
                                    <th id="Address" style="text-align: left">Address</th>
                                    <th id="Janv" style="text-align: left">Jan Visit</th>
                                    <th id="Jan" style="text-align: left">Jan</th>
                                    <th id="Febv" style="text-align: left">Feb Visit</th>
                                    <th id="Feb" style="text-align: left">Feb</th>
                                    <th id="Marv" style="text-align: left">Mar Visit</th>
                                    <th id="Mar" style="text-align: left">Mar</th>
                                    <th id="Aprv" style="text-align: left">Apr Visit</th>
                                    <th id="Apr" style="text-align: left">Apr</th>
                                    <th id="Mayv" style="text-align: left">May Visit</th>
                                    <th id="May" style="text-align: left">May</th>
                                    <th id="Junv" style="text-align: left">Jun Visit</th>
                                    <th id="Jun" style="text-align: left">Jun</th>
                                    <th id="Julv" style="text-align: left">Jul Visit</th>
                                    <th id="Jul" style="text-align: left">Jul</th>
                                    <th id="Augv" style="text-align: left">Aug Visit</th>
                                    <th id="Aug" style="text-align: left">Aug</th>
                                    <th id="Sepv" style="text-align: left">Sep Visit</th>
                                    <th id="Sep" style="text-align: left">Sep</th>
                                    <th id="Octv" style="text-align: left">Oct Visit</th>
                                    <th id="Oct" style="text-align: left">Oct</th>
                                    <th id="Novv" style="text-align: left">Nov Visit</th>
                                    <th id="Nov" style="text-align: left">Nov</th>
                                    <th id="Decv" style="text-align: left">Dec Visit</th>
                                    <th id="Dec" style="text-align: left">Dec</th>
                                    <th id="Total" style="text-align: left">Total</th>
                                </tr>
                            </thead>
                            <tbody>

                            </tbody>
                            
                        </table>

                        <div class="row" >
                            <div class="col-md-5">
                                <div class="dataTables_info" id="orders_info" role="status" aria-live="polite">Showing 0 to 0 of 0 entries</div>
                            </div>
                             <div class="col-md-7">
                                <div class="dataTables_paginate paging_simple_numbers" id="example2_paginate">
                                    <ul class="pagination" style="float: right;">
                                        <li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">First</a></li>
                                        <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="7" tabindex="0">Last</a></li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        
                    </div>
                </div>
            </div>
        </form>

        

        <script type="text/javascript">
            var routM = [];
            var AllOrders = [];
            var sortid = '';
            var asc = true;
            var Orders = []; pgNo = 1; PgRecords = 1000; TotalPg = 0; searchKeys = "Territory_Name,Outlet_Code,Outlet_Name,Category,Channel";
            $(".data-table-basic_length").on("change",
                function () {
                    pgNo = 1;
                    PgRecords = $(this).val();
                    ReloadTable();
                }
            );

            

            function getCusdets() {
                return $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    url: "cus_sale_report_view.aspx/getRetailerData",
                    dataType: "json",
                    success: function (data) {
                        routM = JSON.parse(data.d) || [];
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            }

            function ReloadTable() {

                var tr = '';  let total = 0;
                $("#OrderList TBODY").html("");

                st = PgRecords * (pgNo - 1);
                for ($i = st; $i < st + Number(PgRecords); $i++) {
                    if ($i < Orders.length) {
                                                                       
                        tr = $("<tr rname='" + Orders[$i].Route + "'></tr>");
                                               

                        $(tr).html("<td>" + ($i + 1) + "</td>" +
                            "<td>" + Orders[$i].Route + "</td>" +
							"<td>" + Orders[$i].HQ + "</td>" +
                            "<td class='rocount'><a href='#'>" + Orders[$i].Customer_Code + "</a></td>" +
                            "<td class='rodeact'><a href='#'>" + Orders[$i].Customer_Name + "</a></td>" +
                            "<td>" + Orders[$i].Category + "</td><td>" + Orders[$i].Channel + "</td>" +
                            "<td class='rocount'><a href='#'>" + Orders[$i].Phone + "</a></td>" +
                            "<td class='rodeact'><a href='#'>" + Orders[$i].Address + "</a></td>" +
                            "<td>" + Orders[$i].Janv + "</td><td>" + Orders[$i].Jan + "</td>" +
                            "<td>" + Orders[$i].Febv + "</td><td>" + Orders[$i].Feb + "</td>" +
                            "<td>" + Orders[$i].Marv + "</td><td>" + Orders[$i].Mar + "</td>" +
                            "<td>" + Orders[$i].Aprv + "</td><td>" + Orders[$i].Apr + "</td>" +
                            "<td>" + Orders[$i].Mayv + "</td><td>" + Orders[$i].May + "</td>" +
                            "<td>" + Orders[$i].Junv + "</td><td>" + Orders[$i].Jun + "</td>" +
                            "<td>" + Orders[$i].Julv + "</td><td>" + Orders[$i].Jul + "</td>" +
                            "<td>" + Orders[$i].Augv + "</td><td>" + Orders[$i].Aug + "</td>" +
                            "<td>" + Orders[$i].Sepv + "</td><td>" + Orders[$i].Sep + "</td>" +
                            "<td>" + Orders[$i].Octv + "</td><td>" + Orders[$i].Oct + "</td>" +
                            "<td>" + Orders[$i].Novv + "</td><td>" + Orders[$i].Nov + "</td>" +
                            "<td>" + Orders[$i].Decv + "</td><td>" + Orders[$i].Dec + "</td>" +
                            "<td>" + Orders[$i].Total + "</td>");

                        total += Orders[$i].Total;
                        $("#OrderList TBODY").append(tr);
                    }
                }
                //$('#OrderList tfoot th').append('');
                //$('#OrderList tfoot th').append("Grand Total: " + total.toFixed(3) + "  ");
                $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")
                loadPgNos();
            }

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
                });
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
            });

            function loadData() {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "cus_sale_report_view.aspx/getRetailerData",
                    data: "",
                    dataType: "json",
                    success: function (data) {
                        AllOrders = JSON.parse(data.d) || [];
                        console.log(AllOrders);
                        routM = JSON.parse(data.d) || [];
                        Orders = AllOrders; ReloadTable();
                    },
                    error: function (result) {
                        //alert(JSON.stringify(result));
                    }
                });
            }

            $(document).ready(function () {
               
                loadData(); getCusdets();
               
                $('#txtfilter').on('change', function () {
                    var sfs = $(this).val();
                    var hqn = $('#txtfilter_chosen a').text().trim();
                    if (sfs != 0) {
                        Orders = AllOrders;
                        Orders = Orders.filter(function (a) {
                            return a.Territory_name == sfs || a.Outlet_Code == sfs || a.Outlet_Code == sfs || a.Outlet_Name == sfs;
                        });
                    }
                    else {
                        Orders = AllOrders;
                    }
                    ReloadTable();
                });
            });

        </script>



    </body>

</html>
    

<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cus_sale_report_view.aspx.cs" Inherits="MIS_Reports_cus_sale_report_view" %>

<!DOCTYPE html>
<html lang="en" xmlns="https://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>customer sales</title>
    <script src='https://cdn.rawgit.com/simonbengtsson/jsPDF/requirejs-fix-dist/dist/jspdf.debug.js'></script>
    <script src='https://unpkg.com/jspdf-autotable@2.3.2'></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />

    <style>
        #grid {
            border: 1px solid #ddd;
            border-collapse: collapse;
            width: 80%;
            overflow: scroll;
        }

        th {
            position: sticky;
            top: 0;
            background: #6c7ae0;
            font-weight: normal;
            font-size: 15px;
            color: white;
        }

        #grid td, table th {
            padding: 5px;
            border: 1px solid #ddd;
        }

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
</head>
<body>
    <form id="form1" runat="server">
        <div class="row" style="margin: 0px 0px 0px 5px;">
            <br />
            <br />
            <div id="content">
                <h4>Customer Wise Sales Analysis - <%=sfname%> - <%=year %></h4>
                <div style="margin-top: 12px;">
                    <img style="cursor: pointer; float: right;" src="/img/excel.png" alt="" width="40" height="40" id="btnExport" />
                    <table id="grid" class="grids" style="display: none;">
                        <thead>
                            <tr>
                                <th>S.No</th>
                                <th>Route</th>
                                <th>HQ</th>
                                <th>Code</th>
                                <th>Name</th>
                                <th>Category</th>
                                <th>Channel</th>
                                <th>Phone</th>
                                <th>Address</th>
                                <th>JAN Visit</th>
                                <th>JAN</th>
                                <th>FEB Visit</th>
                                <th>FEB</th>
                                <th>MAR Visit</th>
                                <th>MAR</th>
                                <th>APR Visit</th>
                                <th>APR</th>
                                <th>MAY Visit</th>
                                <th>MAY</th>
                                <th>JUN Visit</th>
                                <th>JUN</th>
                                <th>JUL Visit</th>
                                <th>JUL</th>
                                <th>AUG Visit</th>
                                <th>AUG</th>
                                <th>SEP Visit</th>
                                <th>SEP</th>
                                <th>OCT Visit</th>
                                <th>OCT</th>
                                <th>NOV Visit</th>
                                <th>NOV</th>
                                <th>DEC Visit</th>
                                <th>DEC</th>
                                <th>Total</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                        <tfoot></tfoot>
                    </table>
                </div>
            </div>
        </div>
        <div class="overlay" id="loadover" style="display: none;">
            <div id="loader"></div>
        </div>
    </form>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.3.3/jspdf.min.js"></script>
    <script type="text/javascript" src="https://html2canvas.hertzen.com/dist/html2canvas.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        var routM = [], orderdts = [], vstdts = [], sfdata = [];
        var mnths = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
        $(document).ready(function () {
            $('#loadover').show();
            $.when(loadRoute_Name(), getCusdets(), getCusODets(), getCusVDets()).then(function () {
                loaddata();
            });
            $(document).ajaxStop(function () {
                $('#loadover').hide();
            });
        });
        var url = window.location.href;
        var newurl = new URL(url)
        var subdiv = newurl.searchParams.get('SubDiv');
        var sfc = newurl.searchParams.get('SfCode');
        var sfn = newurl.searchParams.get('sfName');
        var year = newurl.searchParams.get('yr');
        function loadRoute_Name() {
            return $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "cus_sale_report_view.aspx/getRoute_Name",
                dataType: "json",
                success: function (data) {
                    sfdata = JSON.parse(data.d) || [];
                }
            });
        }
        function getCusdets() {
            return $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "cus_sale_report_view.aspx/getRetailerData",
                dataType: "json",
                success: function (data) {
                    routM = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function getCusVDets() {
            return $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "cus_sale_report_view.aspx/getVisitDetails",
                dataType: "json",
                success: function (data) {
                    vstdts = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function getCusODets() {
            return $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "cus_sale_report_view.aspx/getOrderValue",
                dataType: "json",
                success: function (data) {
                    orderdts = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function loaddata() {
            $('#grid').show();
            $('#grid tbody').html('');
            let str = '';
            let total = 0; //var vlis = [];
            for (let i = 0; i < sfdata.length; i++) {
                let retArr = routM.filter(function (a) {
                    return a.Route == sfdata[i].Territory_Code
                });
                for (let j = 0; j < retArr.length; j++) {
                    total = 0;
                    let ordArr = orderdts.filter(function (a) {
                        return a.Cust_Code == retArr[j].Outlet_Code;
                    });
                    let vlis = vstdts.filter(function (a) {
                        return a.Trans_Detail_Info_Code == retArr[j].Outlet_Code;
                    });

                    //vlis = vstdts.filter(function (a) {
                    //    return a.Trans_Detail_Info_Code == retArr[j].Outlet_Code;
                    //});

                    console.log(vlis);

                    str += "<tr><td>" + sfdata[i].Territory_Name + "</td><td>" + sfdata[i].HQ + "</td><td>" + retArr[j].Outlet_Code + "</td><td>" + retArr[j].Outlet_Name + "</td><td>" + retArr[j].Class + "</td>" +
                        "<td>" + retArr[j].Outlet_Type + "</td><td>" + retArr[j].Phone + "</td><td>" + retArr[j].Address + "</td>";


                    for (let x = 0; x < vlis.length; x++) {
                        let mnthO = 0;
                        let mnthV = "";

                        str += "<td>" + vlis[x].Janv + "</td>"; str += "<td>" + vlis[x].Jan + "</td>";

                        str += "<td>" + vlis[x].Febv + "</td>"; str += "<td>" + vlis[x].Feb + "</td>";

                        str += "<td>" + vlis[x].Marv + "</td>"; str += "<td>" + vlis[x].Mar + "</td>";

                        str += "<td>" + vlis[x].Aprv + "</td>"; str += "<td>" + vlis[x].Apr + "</td>";

                        str += "<td>" + vlis[x].Mayv + "</td>"; str += "<td>" + vlis[x].May + "</td>";

                        str += "<td>" + vlis[x].Junv + "</td>"; str += "<td>" + vlis[x].Jun + "</td>";

                        str += "<td>" + vlis[x].Julv + "</td>"; str += "<td>" + vlis[x].Jul + "</td>";

                        str += "<td>" + vlis[x].Augv + "</td>"; str += "<td>" + vlis[x].Aug + "</td>";

                        str += "<td>" + vlis[x].Sepv + "</td>"; str += "<td>" + vlis[x].Sep + "</td>";

                        str += "<td>" + vlis[x].Octv + "</td>"; str += "<td>" + vlis[x].Oct + "</td>";

                        str += "<td>" + vlis[x].Novv + "</td>"; str += "<td>" + vlis[x].Nov + "</td>";

                        str += "<td>" + vlis[x].Decv + "</td>"; str += "<td>" + vlis[x].Dec + "</td>";



                        //mnthO = Number(ordArr.filter(function (a) {
                        //    return a.Months == mnths[x];
                        //}).map(function (el) {
                        //    return el.ord_val;
                        //}).toString());
                        //mnthV = ((vstArr.filter(function (a) {
                        //    return a.mnth == mnths[x];
                        //}).length) > 0 ? "Yes" : "No");
                        total += mnthO;
                        //str += `<td>${mnthV}</td><td>${mnthO}</td>`;
                    }


                    //for (let x = 0; x < mnths.length; x++) {
                    //    let mnthO = 0;
                    //    let mnthV = "";
                    //    mnthO = Number(ordArr.filter(function (a) {
                    //        return a.Months == mnths[x];
                    //    }).map(function (el) {
                    //        return el.ord_val;
                    //    }).toString());
                    //    mnthV = ((vstArr.filter(function (a) {
                    //        return a.mnth == mnths[x];
                    //    }).length) > 0 ? "Yes" : "No");
                    //    total += mnthO;
                    //    str += `<td>${mnthV}</td><td>${mnthO}</td>`;
                    //}
                    str += `<td>${total.toFixed(2)}</td>`;
                }
            }
            $('#grid tbody').append(str);
        }
        $('#btnpdf').click(function () {

            var HTML_Width = $(".grids").width();
            var HTML_Height = $(".grids").height();
            var top_left_margin = 15;
            var PDF_Width = HTML_Width + (top_left_margin * 2);
            var PDF_Height = (PDF_Width * 1.5) + (top_left_margin * 2);
            var canvas_image_width = HTML_Width;
            var canvas_image_height = HTML_Height;

            var totalPDFPages = Math.ceil(HTML_Height / PDF_Height) - 1;


            html2canvas($(".grids")[0], { allowTaint: true }).then(function (canvas) {
                canvas.getContext('2d');

                console.log(canvas.height + "  " + canvas.width);


                var imgData = canvas.toDataURL("image/jpeg", 1.0);
                var pdf = new jsPDF('p', 'pt', [PDF_Width, PDF_Height]);
                pdf.addImage(imgData, 'JPG', top_left_margin, top_left_margin, canvas_image_width, canvas_image_height);


                for (var i = 1; i <= totalPDFPages; i++) {
                    pdf.addPage(PDF_Width, PDF_Height);
                    pdf.addImage(imgData, 'JPG', top_left_margin, -(PDF_Height * i) + (top_left_margin * 4), canvas_image_width, canvas_image_height);
                }

                pdf.save("customersalesanalysis.pdf");
            });
        });
        $('#btnExport').click(function () {

            var htmls = "";
            var uri = 'data:application/vnd.ms-excel;base64,';
            var template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="https://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>';
            var base64 = function (s) {
                return window.btoa(unescape(encodeURIComponent(s)))
            };
            var format = function (s, c) {
                return s.replace(/{(\w+)}/g, function (m, p) {
                    return c[p];
                })
            };
            htmls = document.getElementById("grid").innerHTML;


            var ctx = {
                worksheet: 'Worksheet',
                table: htmls
            }
            var link = document.createElement("a");
            var tets = 'customersalesanalysis' + '.xls';   //create fname

            link.download = tets;
            link.href = uri + base64(format(template, ctx));
            link.click();
        });
    </script>
</body>
</html>--%>
