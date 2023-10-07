<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_Distributor_stock_details.aspx.cs" Inherits="MIS_Reports_Rpt_Distributor_stock_details" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <style>
        .table, td, th {
            border: 1px solid;
        }

        .table {
            width: 100%;
            border-collapse: collapse;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <tr>

                <td width="60%" align="center">
                    <asp:Label ID="lblHead" Font-Bold="true" Style="color: #3F51B5; padding-left: 150px;" Font-Size="25px" Font-Underline="true"
                        runat="server"></asp:Label>
                     <span style="float: right; margin-right: 80px;">
                    <img src="../img/Excel-icon.png" style="height: 50px; width: 50px; border-width: 0px; position: absolute; right: 15px; top: 10px;" onclick="exportToExcel()" />
                </span>
                </td>
            </tr>
            <br />
            <br />
            <br />
             
            
        </div>
        <div id="Stock_table">
            <div><tr>
                <td>Distributor Name :
                    <asp:Label ID="lblsf_name" runat="server" Style="font-size: medium"></asp:Label></td>
               </tr>

            </div>

            <table class="table table-bordered newStly" >
               
                <thead class="text-warning">
                    <tr>
                        <th rowspan="2" style="text-align: left">Sl NO</th>
                        <th rowspan="2" style="text-align: left;background-color: #f7ff78;">Product Code</th>
                        <th rowspan="2" style="text-align: left;background-color: #f7ff78;">Product Name</th>                        
                        <th colspan="1" style="text-align: center; background-color: #ffd800;">OPENING</th>
                        <th colspan="1" style="text-align: center; background-color: #ffd800;">INCOMMING</th>
                        <th colspan="1" style="text-align: center; background-color: #4fae04;">CUREENT</th>
                       <%-- <th colspan="1" style="text-align: center; background-color: #9bd6ff;">VAN - SALES</th>--%>
                        <th colspan="1" style="text-align: center; background-color: #9bd6ff;">SECONDARY</th>
                        <th colspan="1" style="text-align: center; background-color: #9bd6ff;">COUNTER</th>
                        <th colspan="1" style="text-align: center; background-color: #d1c305;">CLOSING</th>
                    </tr>
                    <tr>
                        
                        <%--<th style="background-color: #fffbe3;">Qty</th>
                        <th style="background-color: #fffbe3;display:none">UNT/CRT</th>
                        <th style="background-color: #fffbe3;">Qty</th>
                        <th style="background-color: #fffbe3;display:none">UNT/CRT</th>
                        <th style="background-color: #dfecd5;">Qty</th>
                        <th style="background-color: #dfecd5;display:none">UNT/CRT</th>
                        <th style="background-color: #cee2f3;">Qty</th>
                        <th style="background-color: #cee2f3;display:none">UNT/CRT</th>
                        <th style="background-color: #cee2f3;">Qty</th>
                        <th style="background-color: #cee2f3;display:none">UNT/CRT</th>
                        <th style="background-color: #cee2f3;">Qty</th>
                        <th style="background-color: #cee2f3;display:none">UNT/CRT</th>
                        <th style="background-color: #ede8aa;">Qty</th>
                        <th style="background-color: #ede8aa;display:none">UNT/CRT</th>--%>

                        <th style="background-color: #fffbe3;">Qty</th>
                       
                        <th style="background-color: #fffbe3;">Qty</th>
                      
                        <th style="background-color: #dfecd5;">Qty</th>
                        
                      <%--  <th style="background-color: #cee2f3;">Qty</th>--%>
                       
                        <th style="background-color: #cee2f3;">Qty</th>
                       
                        <th style="background-color: #cee2f3;">Qty</th>
                       
                        <th style="background-color: #ede8aa;">Qty</th>
                       
                    </tr>
                </thead>
                <tbody>
                </tbody>
                <tfoot></tfoot>
            </table>


        </div>
    </form>

    <link href="../../css/jquery.multiselect.css" rel="stylesheet" />
    <link href="../../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
    <script type="text/javascript">

        var AllOrders = []; var sf_type = ''; var sfCode = ''; var All_stock_details = []; var txt = '';
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Product_Detail_Code,Product_Detail_Name,";


        $(document).ready(function () {
            loadData();
            var sf_type = '<%=Session["sf_type"]%>';


            function loadData() {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    data: "{'divcode':'<%=Session["div_code"]%>'}",
                    url: "Rpt_Distributor_stock_details.aspx/Get_Stock",
                    dataType: "json",
                    success: function (data) {
                        AllOrders = JSON.parse(data.d) || [];
                        Orders = AllOrders; ReloadTable();
                    },
                    error: function (result) {
                    }
                });

            }




            function ReloadTable() {

                var total_opening = 0; var tot_purchase = 0; var tot_sales = 0; var tot_closing = 0;
                var tot_Inv = 0, tot_POS = 0, tot_van = 0;
                $("#Stock_table TBODY").html("");
                st = PgRecords * (pgNo - 1); slno = 0;
                //for ($i = st; $i < st + PgRecords; $i++) {

                if (0 < Orders.length) {
                    for ($i = 0; $i < Orders.length; $i++) {
                        tr = $("<tr></tr>");
                        slno = $i + 1;
                        //$(tr).html("<td style='text-align:left'>" + slno + "</td><td class='pro_code' style='text-align:left'>" + Orders[$i].ProdCode + "</td><td class='pro_name' style='text-align:left'> " + Orders[$i].Product_Detail_Name + "</td>"
                        //    + "<td class='op_stock' style='background-color:#fffbe3;'>" + Orders[$i].pBal + "</td><td class='op_stock' style='background-color:#fffbe3;'>" + Orders[$i].pBalAmt.toFixed(2) + "</td>"
                        //    + "<td class='purchase_order' style='background-color:#fffbe3;'>" + Orders[$i].Credit + "</td><td class='purchase_order' style='background-color:#fffbe3;'>" + Orders[$i].CreditAmt.toFixed(2) + "</td>"
                        //    + "<td class='CurrStock' style='background-color:#dfecd5;'>" + (Orders[$i].pBal+Orders[$i].Credit) + "</td><td class='CurrStock' style='background-color:#dfecd5;'>" + (Orders[$i].pBalAmt+Orders[$i].CreditAmt).toFixed(2) + "</td>"
                        //    + "<td class='sales' style='background-color:#cee2f3;'>" + "0" + "</td><td class='sales' style='background-color:#cee2f3;'>" + "0.00" + "</td>"
                        //    + "<td class='sales' style='background-color:#cee2f3;'>" + Orders[$i].Inv + "</td><td class='sales' style='background-color:#cee2f3;'>" + Orders[$i].InvAmt.toFixed(2) + "</td>"
                        //    + "<td class='sales' style='background-color:#cee2f3;'>" + Orders[$i].POS + "</td><td class='sales' style='background-color:#cee2f3;'>" + Orders[$i].POSAmt.toFixed(2) + "</td>"
                        //    //+ "<td class='sales'>" + Orders[$i].Debit + "</td><td class='sales'>" + Orders[$i].DebitAmt.toFixed(2) + "</td>"
                        //    + "<td class='Closing_stock' style='background-color:#ede8aa;'>" + Orders[$i].Balance + "</td><td class='Closing_stock' style='background-color:#ede8aa;'>" + Orders[$i].BalanceAmt.toFixed(2) + "</td>");
                        // $(tr).html("<td>" + slno + "</td><td class='pro_code'>" + Orders[$i].Product_Detail_Code + "</td><td class='pro_name'>" + Orders[$i].Product_Detail_Name + "</td><td class='op_stock'>" + Orders[$i].Opening_Stock + "</td><td class='purchase_order'>" + Orders[$i].Purchase + "</td>><td class='sales'>" + Orders[$i].Sales + "</td><td class='Closing_stock'>" + Orders[$i].Closing + "</td>");
                        //$(tr).html("<td style='text-align:left'>" + slno + "</td><td class='pro_code' style='text-align:left;background-color: #f7ff78;'>" + Orders[$i].ProdCode + "</td><td class='pro_name' style='text-align:left;background-color: #f7ff78;'> " + Orders[$i].Product_Detail_Name + "</td>"
                        //    + "<td class='op_stock' style='background-color:#fffbe3;'>" + Orders[$i].pBal + "</td><td class='op_stock' style='background-color:#fffbe3;display:none'>" + Orders[$i].UNTOb + "</td>"
                        //    + "<td class='purchase_order' style='background-color:#fffbe3;'>" + Orders[$i].Credit + "</td><td class='purchase_order' style='background-color:#fffbe3;display:none'>" + Orders[$i].UNTCr + "</td>"
                        //    + "<td class='CurrStock' style='background-color:#dfecd5;'>" + (Orders[$i].pBal + Orders[$i].Credit) + "</td><td class='CurrStock' style='background-color:#dfecd5;display:none'>" + (Orders[$i].pBal + Orders[$i].Credit) / Orders[$i].Conv + "</td>"
                        //    + "<td class='sales' style='background-color:#cee2f3;'>" + "0" + "</td><td class='sales' style='background-color:#cee2f3;display:none'>" + "0" + "</td>"
                        //    + "<td class='sales' style='background-color:#cee2f3;'>" + Orders[$i].Inv + "</td><td class='sales' style='background-color:#cee2f3;display:none'>" + Orders[$i].Inv / Orders[$i].Conv + "</td>"
                        //    + "<td class='sales' style='background-color:#cee2f3;'>" + Orders[$i].POS + "</td><td class='sales' style='background-color:#cee2f3;display:none'>" + Orders[$i].POS / Orders[$i].Conv + "</td>"
                        //    //+ "<td class='sales'>" + Orders[$i].Debit + "</td><td class='sales'>" + Orders[$i].DebitAmt.toFixed(2) + "</td>"
                        //    + "<td class='Closing_stock' style='background-color:#ede8aa;'>" + Orders[$i].Balance + "</td><td class='Closing_stock' style='background-color:#ede8aa;display:none'>" + Orders[$i].UNTBal + "</td>");
                        //// $(tr).html("<td>" + slno + "</td><td class='pro_code'>" + Orders[$i].Product_Detail_Code + "</td><td class='pro_name'>" + Orders[$i].Product_Detail_Name + "</td><td class='op_stock'>" + Orders[$i].Opening_Stock + "</td><td class='purchase_order'>" + Orders[$i].Purchase + "</td>><td class='sales'>" + Orders[$i].Sales + "</td><td class='Closing_stock'>" + Orders[$i].Closing + "</td>");


                        
                        $(tr).html("<td style='text-align:left'>" + slno + "</td><td class='pro_code' style='text-align:left;background-color: #f7ff78;'>" + Orders[$i].ProdCode + "</td><td class='pro_name' style='text-align:left;background-color: #f7ff78;'> " + Orders[$i].Product_Detail_Name + "</td>"
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
                        tot_POS += Orders[$i].POSAmt;
                        tot_van += 0;
                        tot_closing = Orders[$i].BalanceAmt + tot_closing;
                    }
                }
                // }
                $("#Stock_table TFOOT").html("<td colspan='3' style='font-weight: bold;padding: 0px 0px 0px 8px;'>Total (Values are In Prices)</td></td><td style='font-weight: bold;' colspan=1>" + total_opening.toFixed(2) + "</td><td style='font-weight: bold;' colspan=1>" + tot_purchase.toFixed(2) + "</td><td style='font-weight: bold;' colspan=1>" + (total_opening + tot_purchase).toFixed(2) + "</td>" +
                    //"<td style='font-weight: bold;' colspan=1>" + tot_van.toFixed(2) + "</td>" +
                    "<td style='font-weight: bold;' colspan=1 > " + tot_Inv.toFixed(2) + "</td > <td style='font-weight: bold;' colspan=1 > " + tot_POS.toFixed(2) + "</td > <td style='font-weight: bold;' colspan=1 > " + tot_closing.toFixed(2) + "</td > ");
                //$("#Stock_table TFOOT").html("<td colspan='3' style='font-weight: bold;padding: 0px 0px 0px 8px;'>Total</td></td><td style='font-weight: bold;'>" + total_opening.toFixed(2) + "</td><td style='font-weight: bold;'>" + tot_purchase.toFixed(2) + "</td><td style='font-weight: bold;'>" + tot_sales.toFixed(2) + "</td><td style='font-weight: bold;'>" + tot_closing.toFixed(2) + "</td>");
                //$("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")

            }




        });
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
            htmls = document.getElementById("Stock_table").innerHTML;

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
</body>
</html>
