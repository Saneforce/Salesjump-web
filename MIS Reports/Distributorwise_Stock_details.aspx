<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Distributorwise_Stock_details.aspx.cs" Inherits="MIS_Reports_Distributorwise_Stock_details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <html>
    <head><title></title>
        <style type="text/css">
            #Stock_table td,#Stock_table th {
            white-space:nowrap;
            text-align:right
            }
        </style>
    </head>
    <body>
        <form id="form1" runat="server">
            <asp:HiddenField ID="hdndate" runat="server"></asp:HiddenField>
            <div class="row">
                <div class="col-md-offset-4 col-md-4 sub-header" style="text-align: center">
                    ProductWise Stock Details
                </div>
                <div class="col-md-4 sub-header">
                    <span style="float: right"></span><span style="float: right; margin-right: 50px;">                       
                        <div class="row1" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc;">
                            <input type="date" id="datepic" style="min-width: 110px !important; width: 155px; display: inline-block; border: none; background-color: transparent; color: #336277; font-family: Verdana; font-weight: bold; font-size: 14px;" />
                        </div>
                    </span>
                    <span style="float: right; margin-top: -5px;">
                        <div>
                            <%--<asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                                Style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 15px;" OnclientClick="exportToExcel()" />--%>

                            <img src="../img/Excel-icon.png" style="height: 50px; width: 50px; border-width: 0px; position: absolute; right: 15px; top:-5px;" onclick="exportToExcel()" />
                        </div>
                    </span>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4 col-md-offset-4" style="text-align: center">
                    <h5 id="date_details"></h5>
                </div>
            </div>
            <div class="row" id="distselect" style="display:none;">
                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                    <div class="form-group">
                        <label class="control-label" for="focusedInput">
                            Distibutor Name</label><span style="color:red">*</span>
                        <select class="form-control" id="ddl_dist" style="width: 100%;">
                            <option value="0">SelectDistributor</option>
                        </select>
                    </div>
                </div>
            </div>
            <div class="card" style="width:1300px;">
                <div class="card-body table-responsive">
                    <div style="white-space: nowrap">
                        Search&nbsp;&nbsp;<input type="text" id="tSearchOrd" autocomplete="off" style="width: 250px;" />
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
                    <table class="table table-hover" id="Stock_table">
                        <thead class="text-warning">
                            <tr>
                                <th style="text-align:left">Sl NO</th>
                                <th style="text-align:left">Product Code</th>
                                <th style="text-align:left">Product Name</th>
                                <th colspan="1" style="text-align:center;background-color:#ffd800;">OPENING</th>
                                <th colspan="1" style="text-align:center;background-color:#ffd800;">INCOMING</th>
                                <th colspan="1" style="text-align:center;background-color:#4fae04;">CURRENT</th>
                                <%--<th colspan="2" style="text-align:center;background-color:#ff0056;">VAN - SALES</th>--%>
                                <th colspan="1" style="text-align:center;background-color:#ff0056;">SECONDARY</th>
                                <th colspan="1" style="text-align:center;background-color:#ff0056;">COUNTER</th>
                                <th colspan="1" style="text-align:center;background-color:#d1c305;">CLOSING</th>
                            </tr>
                           
                        </thead>
                        <tbody>
                        </tbody>
                        <tfoot></tfoot>
                    </table>
                     
                    <div class="row">
                        <div class="col-sm-5">
                            <div class="dataTables_info" id="orders_info" role="status" aria-live="polite">Showing 0 to 0 of 0 entries</div>
                        </div>
                        <div class="col-sm-7">
                            <div class="dataTables_paginate paging_simple_numbers" id="example2_paginate">
                                <ul class="pagination">
                                    <li class="paginate_button previous" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">Previous</a></li>
                                    <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="7" tabindex="0">Next</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <table class="table table-hover" style="display:none" id="excel_table">
                        <thead class="text-warning">
                            <tr>
                                <th style="text-align:left">Sl NO</th>
                                <th style="text-align:left">Product Code</th>
                                <th style="text-align:left">Product Name</th>
                                <th colspan="1" style="text-align:center;background-color:#ffd800;">OPENING</th>
                                <th colspan="1" style="text-align:center;background-color:#ffd800;">INCOMING</th>
                                <th colspan="1" style="text-align:center;background-color:#4fae04;">CURRENT</th>
                                <%--<th colspan="2" style="text-align:center;background-color:#ff0056;">VAN - SALES</th>--%>
                                <th colspan="1" style="text-align:center;background-color:#ff0056;">SECONDARY</th>
                                <th colspan="1" style="text-align:center;background-color:#ff0056;">COUNTER</th>
                                <th colspan="1" style="text-align:center;background-color:#d1c305;">CLOSING</th>
                            </tr>
                           
                        </thead>
                        <tbody>
                        </tbody>
                        <tfoot></tfoot>
                    </table>

                </div>
            </div>
        </form>
    </body>
	<link href="../../css/jquery.multiselect.css" rel="stylesheet" />     
    <link href="../../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
         <script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script type="text/javascript">
        
        var AllOrders = []; var sf_type = ''; var sfCode = ''; var All_stock_details = []; var txt = '';
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Product_Detail_Code,Product_Detail_Name,";

        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1;
        var yyyy = today.getFullYear();
        if (dd < 10) { dd = '0' + dd }
        if (mm < 10) { mm = '0' + mm }
        today = yyyy + '-' + mm + '-' + dd;

        $("#datepic").val(today);

        $(document).ready(function () {         
            //loadData();
            var sf_type = '<%=Session["sf_type"]%>';
            
            
            $(document).on('change', "#datepic", function (e) {  
                getDataByDist();
            });
            $(document).on('change', "#ddl_dist", function (e) {
                 var dist_code = $('#ddl_dist').val();
                 loadData(dist_code);                
            });
           function getDataByDist(){
            
                 var txt = $('#datepic').val();
                 var dist_code = $('#ddl_dist').val();
                 var sf_type = '<%=Session["sf_type"]%>';
                 if (sf_type != '4') {
                     if (dist_code == '0') { alert("Select Distributor"); focus($('#ddl_dist')); return false; }
                 } else {
                     dist_code = '<%=Session["Sf_Code"]%>';
                 }
                 //if (dist_code == '0') { alert("Select Distributor"); focus($('#ddl_dist')); return false;}
                loadData(dist_code);
            }
            if (sf_type != '4') {
                $('#distselect').show();
                loaddist();
            }else { getDataByDist(); }
             $(".data-table-basic_length").on("change",
                 function () {
                     pgNo = 1;
                     PgRecords = parseFloat($(this).val());
                     ReloadTable();
                 });

            function loadData(dist_code) {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    data: "{'Date':'" + $('#datepic').val() + "','dist_code':'" + dist_code + "'}",
                    url: "Distributorwise_Stock_Details.aspx/Get_Stock",
                    dataType: "json",
                    success: function (data) {
                        AllOrders = JSON.parse(data.d) || [];
                        Orders = AllOrders; ReloadTable();
                    },
                    error: function (result) {
                    }
                });

            }
            function loaddist() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Distributorwise_Stock_Details.aspx/binddistributor",
                    data: "{'sf_code':'<%=Session["Sf_Code"]%>','Div':'<%=Session["Division_Code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        itms = JSON.parse(data.d) || [];
                        for (var i = 0; i < itms.length; i++) {
                            $('#ddl_dist').append($("<option></option>").val(itms[i].Stockist_Code).html(itms[i].Stockist_Name+'-'+itms[i].ERP_Code)).trigger('chosen:updated').css("width", "100%");
                        }
						$('#ddl_dist').selectpicker({
                            liveSearch: true
                        });
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            }

             //$.ajax({
             //    type: "POST",
             //    contentType: "application/json; charset=utf-8",
             //    async: false,
             //    url: "Default4.aspx/Get_access_master",
             //    dataType: "json",
             //    success: function (data) {
             //        window.localStorage.removeItem("Access_Details");
             //        Access_data = JSON.parse(data.d) || [];
             //        localStorage.setItem('Access_Details', data.d);
             //    },
             //    error: function (result) {
             //    }
             //});


             function ReloadTable() {

                 var total_opening = 0; var tot_purchase = 0; var tot_sales = 0; var tot_closing = 0;
                 var tot_Inv = 0,tot_POS = 0,tot_van = 0 ;
                 $("#Stock_table TBODY").html("");
                 st = PgRecords * (pgNo - 1); slno = 0;
                 for ($i = st; $i < st + PgRecords; $i++) {

                     if ($i < Orders.length) {

                         tr = $("<tr></tr>");
                         slno = $i + 1;
                         //$(tr).html("<td style='text-align:left'>" + slno + "</td><td class='pro_code' style='text-align:left'>" + Orders[$i].ProdCode + "</td><td class='pro_name' style='text-align:left'> " + Orders[$i].Product_Detail_Name + "</td>"
                         //    + "<td class='op_stock' style='background-color:#fffbe3;'>" + Orders[$i].pBal + "</td><td class='op_stock' style='background-color:#fffbe3;'>" + Orders[$i].pBalAmt.toFixed(2) + "</td>"
                         //    + "<td class='purchase_order' style='background-color:#fffbe3;'>" + Orders[$i].Credit + "</td><td class='purchase_order' style='background-color:#fffbe3;'>" + Orders[$i].CreditAmt.toFixed(2) + "</td>"
                         //    + "<td class='CurrStock' style='background-color:#dfecd5;'>" + (Orders[$i].pBal+Orders[$i].Credit) + "</td><td class='CurrStock' style='background-color:#dfecd5;'>" + (Orders[$i].pBalAmt+Orders[$i].CreditAmt).toFixed(2) + "</td>"
                         //    + "<td class='sales' style='background-color:#f8e3ea;'>" + "0" + "</td><td class='sales' style='background-color:#f8e3ea;'>" + "0.00" + "</td>"
                         //    + "<td class='sales' style='background-color:#f8e3ea;'>" + Orders[$i].Inv + "</td><td class='sales' style='background-color:#f8e3ea;'>" + Orders[$i].InvAmt.toFixed(2) + "</td>"
                         //    + "<td class='sales' style='background-color:#f8e3ea;'>" + Orders[$i].POS + "</td><td class='sales' style='background-color:#f8e3ea;'>" + Orders[$i].POSAmt.toFixed(2) + "</td>"
                         //    //+ "<td class='sales'>" + Orders[$i].Debit + "</td><td class='sales'>" + Orders[$i].DebitAmt.toFixed(2) + "</td>"
                         //    + "<td class='Closing_stock' style='background-color:#ede8aa;'>" + Orders[$i].Balance + "</td><td class='Closing_stock' style='background-color:#ede8aa;'>" + Orders[$i].BalanceAmt.toFixed(2) + "</td>");
                        // $(tr).html("<td>" + slno + "</td><td class='pro_code'>" + Orders[$i].Product_Detail_Code + "</td><td class='pro_name'>" + Orders[$i].Product_Detail_Name + "</td><td class='op_stock'>" + Orders[$i].Opening_Stock + "</td><td class='purchase_order'>" + Orders[$i].Purchase + "</td>><td class='sales'>" + Orders[$i].Sales + "</td><td class='Closing_stock'>" + Orders[$i].Closing + "</td>");
                        //  $(tr).html("<td style='text-align:left'>" + slno + "</td><td class='pro_code' style='text-align:left'>" + Orders[$i].ProdCode + "</td><td class='pro_name' style='text-align:left'> " + Orders[$i].Product_Detail_Name + "</td>"
                        //     + "<td class='op_stock' style='background-color:#fffbe3;'>" + Orders[$i].pBal + "</td><td class='op_stock' style='background-color:#fffbe3;'>" + Orders[$i].UNTOb + "</td>"
                        //     + "<td class='purchase_order' style='background-color:#fffbe3;'>" + Orders[$i].Credit + "</td><td class='purchase_order' style='background-color:#fffbe3;'>" + Orders[$i].UNTCr + "</td>"
                        //     + "<td class='CurrStock' style='background-color:#dfecd5;'>" + (Orders[$i].pBal+Orders[$i].Credit) + "</td><td class='CurrStock' style='background-color:#dfecd5;'>" + (Orders[$i].pBal+Orders[$i].Credit)/Orders[$i].Conv + "</td>"
                        //     + "<td class='sales' style='background-color:#f8e3ea;'>" + "0" + "</td><td class='sales' style='background-color:#f8e3ea;'>" + "0" + "</td>"
                        //     + "<td class='sales' style='background-color:#f8e3ea;'>" + Orders[$i].Inv + "</td><td class='sales' style='background-color:#f8e3ea;'>" + Orders[$i].Inv/Orders[$i].Conv + "</td>"
                        //     + "<td class='sales' style='background-color:#f8e3ea;'>" + Orders[$i].POS + "</td><td class='sales' style='background-color:#f8e3ea;'>" + Orders[$i].POS/Orders[$i].Conv + "</td>"
                        //     //+ "<td class='sales'>" + Orders[$i].Debit + "</td><td class='sales'>" + Orders[$i].DebitAmt.toFixed(2) + "</td>"
                        //     + "<td class='Closing_stock' style='background-color:#ede8aa;'>" + Orders[$i].Balance + "</td><td class='Closing_stock' style='background-color:#ede8aa;'>" + Orders[$i].UNTBal + "</td>");
                        //// $(tr).html("<td>" + slno + "</td><td class='pro_code'>" + Orders[$i].Product_Detail_Code + "</td><td class='pro_name'>" + Orders[$i].Product_Detail_Name + "</td><td class='op_stock'>" + Orders[$i].Opening_Stock + "</td><td class='purchase_order'>" + Orders[$i].Purchase + "</td>><td class='sales'>" + Orders[$i].Sales + "</td><td class='Closing_stock'>" + Orders[$i].Closing + "</td>");
                         $(tr).html("<td style='text-align:left'>" + slno + "</td><td class='pro_code' style='text-align:left;'>" + Orders[$i].ProdCode + "</td><td class='pro_name' style='text-align:left;'> " + Orders[$i].Product_Detail_Name + "</td>"
                            + "<td class='op_stock' style='background-color:#fffbe3;'>" + Orders[$i].pBal + "</td>"
                            + "<td class='purchase_order' style='background-color:#fffbe3;'>" + Orders[$i].Credit + "</td>"
                            + "<td class='CurrStock' style='background-color:#dfecd5;'>" + (Orders[$i].pBal + Orders[$i].Credit) + "</td>"
                            //+ "<td class='sales' style='background-color:#cee2f3;'>" + "0" + "</td>"
                            + "<td class='sales' style='background-color:#cee2f3;'>" + Orders[$i].Inv + "</td>"
                            + "<td class='sales' style='background-color:#cee2f3;'>" + Orders[$i].POS + "</td>"                            
                            + "<td class='Closing_stock' style='background-color:#ede8aa;'>" + Orders[$i].Balance + "</td>");
                       
                         $("#Stock_table TBODY").append(tr);

                         total_opening = Orders[$i].pBalAmt + total_opening;
                         tot_purchase = Orders[$i].CreditAmt + tot_purchase;
                         tot_sales = Orders[$i].DebitAmt + tot_sales;
                         tot_Inv += Orders[$i].InvAmt;
                         tot_POS += Orders[$i].POSAmt ;
                         tot_van += 0 ;
                         tot_closing = Orders[$i].BalanceAmt + tot_closing;
                     }
                 }
                 //$("#Stock_table TFOOT").html("<td colspan='3' style='font-weight: bold;padding: 0px 0px 0px 8px;' >Total  (values in Price amounts)</td></td><td style='font-weight: bold;' colspan=1>" + total_opening.toFixed(2) + "</td><td style='font-weight: bold;' colspan=1>" + tot_purchase.toFixed(2) + "</td><td style='font-weight: bold;' colspan=1>" + (total_opening + tot_purchase).toFixed(2) + "</td>" +
                 //    "<td style='font-weight: bold;' colspan=1>" + tot_Inv.toFixed(2) + "</td><td style='font-weight: bold;' colspan=1>" + tot_POS.toFixed(2) + "</td><td style='font-weight: bold;' colspan=1>" + tot_closing.toFixed(2) + "</td>");
                 //$("#Stock_table TFOOT").html("<td colspan='3' style='font-weight: bold;padding: 0px 0px 0px 8px;'>Total</td></td><td style='font-weight: bold;'>" + total_opening.toFixed(2) + "</td><td style='font-weight: bold;'>" + tot_purchase.toFixed(2) + "</td><td style='font-weight: bold;'>" + tot_sales.toFixed(2) + "</td><td style='font-weight: bold;'>" + tot_closing.toFixed(2) + "</td>");
                 $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")
                 loadPgNos();
                 tableforexcel();
             }

             function loadPgNos() {
                 prepg = parseInt($(".paginate_button.previous>a").attr("data-dt-idx"));
                 Nxtpg = parseInt($(".paginate_button.next>a").attr("data-dt-idx"));
                 $(".pagination").html("");
                 TotalPg = parseInt(parseInt(Orders.length / PgRecords) + ((Orders.length % PgRecords) ? 1 : 0)); selpg = 1;
                 if (isNaN(prepg)) prepg = 0;
                 if (isNaN(Nxtpg)) Nxtpg = 2;
                 selpg = (pgNo > 7) ? (parseInt(pgNo) + 1) - 7 : 1;
                 if ((Nxtpg) == pgNo) {
                     selpg = (parseInt(TotalPg)) - 7;
                     selpg = (selpg > 1) ? selpg : 1;
                 }
                 spg = '<li class="paginate_button previous" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">First</a></li>';
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

             $("#tSearchOrd").on("keyup", function () {
                 pgNo = 1;
                 var Search_text = $(this).val();
                 if (Search_text != "") {
                     shText = Search_text;
                     Orders = AllOrders.filter(function (a) {
                         chk = false;
                         $.each(a, function (key, val) {
                             if (val != null && val.toString().toLowerCase().indexOf(shText.toString().toLowerCase()) > -1 && (',' + searchKeys).indexOf(',' + key + ',') > -1) {
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
        });

        function tableforexcel() {
            var total_opening = 0; var tot_purchase = 0; var tot_sales = 0; var tot_closing = 0;
                var tot_Inv = 0, tot_POS = 0, tot_van = 0;
                $("#excel_table TBODY").html("");
                st = PgRecords * (pgNo - 1); slno = 0;

                if (0 < Orders.length) {
                    for ($i = 0; $i < Orders.length; $i++) {
                        tr = $("<tr></tr>");
                        slno = $i + 1;
                       
                        $(tr).html("<td style='text-align:left'>" + slno + "</td><td class='pro_code' style='text-align:left;background-color: #f7ff78;'>" + Orders[$i].ProdCode + "</td><td class='pro_name' style='text-align:left;background-color: #f7ff78;'> " + Orders[$i].Product_Detail_Name + "</td>"
                            + "<td class='op_stock' style='background-color:#fffbe3;'>" + Orders[$i].pBal + "</td>"
                            + "<td class='purchase_order' style='background-color:#fffbe3;'>" + Orders[$i].Credit + "</td>"
                            + "<td class='CurrStock' style='background-color:#dfecd5;'>" + (Orders[$i].pBal + Orders[$i].Credit) + "</td>"
                            //+ "<td class='sales' style='background-color:#cee2f3;'>" + "0" + "</td>"
                            + "<td class='sales' style='background-color:#cee2f3;'>" + Orders[$i].Inv + "</td>"
                            + "<td class='sales' style='background-color:#cee2f3;'>" + Orders[$i].POS + "</td>"                            
                            + "<td class='Closing_stock' style='background-color:#ede8aa;'>" + Orders[$i].Balance + "</td>");
                        $("#excel_table TBODY").append(tr);

                        total_opening = Orders[$i].pBalAmt + total_opening;
                        tot_purchase = Orders[$i].CreditAmt + tot_purchase;
                        tot_sales = Orders[$i].DebitAmt + tot_sales;
                        tot_Inv += Orders[$i].InvAmt;
                        tot_POS += Orders[$i].POSAmt;
                        tot_van += 0;
                        tot_closing = Orders[$i].BalanceAmt + tot_closing;
                    }
                }
            
                $("#excel_table TFOOT").html("<td colspan='3' style='font-weight: bold;padding: 0px 0px 0px 8px;'>Total (Values are In Prices)</td></td><td style='font-weight: bold;' colspan=1>" + total_opening.toFixed(2) + "</td><td style='font-weight: bold;' colspan=1>" + tot_purchase.toFixed(2) + "</td><td style='font-weight: bold;' colspan=1>" + (total_opening + tot_purchase).toFixed(2) + "</td>" +
                    //"<td style='font-weight: bold;' colspan=1>" + tot_van.toFixed(2) + "</td>" +
                    "<td style='font-weight: bold;' colspan=1 > " + tot_Inv.toFixed(2) + "</td > <td style='font-weight: bold;' colspan=1 > " + tot_POS.toFixed(2) + "</td > <td style='font-weight: bold;' colspan=1 > " + tot_closing.toFixed(2) + "</td > ");
                
        }
         function exportToExcel() {
            var htmls = ""; 
            var uri = 'data:application/vnd.ms-excel;base64,';
            var template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table  style="border-collapse: collapse;">{table}</table></body></html>';
            var base64 = function (s) {
                return window.btoa(unescape(encodeURIComponent(s)))
            };
            var format = function (s, c) {
                return s.replace(/{(\w+)}/g, function (m, p) {
                    return c[p];
                })
            };
            htmls = document.getElementById("excel_table").innerHTML;

            var ctx = {
                worksheet: 'Stock details',
                table: htmls
            }
            var link = document.createElement("a");
            var tets = 'Rpt_Stock details' + '.xls';

            link.download = tets;
            link.href = uri + base64(format(template, ctx));
            link.click();
        }

    </script>    
    </html>
</asp:Content>

