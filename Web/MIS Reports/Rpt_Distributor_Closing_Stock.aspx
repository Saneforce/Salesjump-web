<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_Distributor_Closing_Stock.aspx.cs" Inherits="MIS_Reports_Rpt_Distributor_Closing_Stock" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Rpt_Distributor_Closing_Stock</title>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
</head>
<style>
    table {
        font-family: arial, sans-serif;
        border-collapse: collapse;
        width: 100%;
    }

    td, th {
        border: 1px solid #dddddd;
        text-align: left;
        padding: 10px;
    }

    tr:nth-child(even) {
        background-color: #dddddd;
    }
</style>
<body>
    <form runat="server" id="frm1">
        <tr>

            <td width="60%" align="center">
                <asp:Label ID="lblHead" Font-Bold="true" Style="color: #3F51B5; padding-left:150px;" Font-Size="25px" Font-Underline="true"
                    runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
        <div class="col-lg-12 sub-header" style="font-size: 25px;padding:10px">
            <asp:Label ID="Label2" Text="Field Force Name :" runat="server" Style="font-size: medium"></asp:Label>
            <asp:Label ID="lblsf_name" runat="server" Style="font-size: medium"></asp:Label>
            <span style="float: right; margin-right: 80px;">
                <img src="../img/Excel-icon.png" style="height: 50px; width: 50px; border-width: 0px; position: absolute; right: 15px; top: 10px;" onclick="exportToExcel()" />
            </span>
        </div>
            </tr>
        <div class="card" style="display: inline-block;">
            <div class="card-body table-responsive">

                <table class="table table-bordered table-hover grids " id="OrderList" style="font-size: 18px; margin-top: 15px">
                    <thead class="text-warning">
                        <tr style="background-color: #37a4c6; color: #fff;">
                            <th style="text-align: left">Sl.No</th>
                            <th id="Dt" style="text-align: left">Date</th>
                            <th id="DistName" style="text-align: left">Distributor Name</th>
                            <th id="ProNme" style="text-align: left">Product Name</th>
                            <th style="text-align: left">Opening Stock Qty</th>
                            <th id="Order" style="text-align: left">Order Qty</th>
                            <th id="Value" style="text-align: left;">Order Value (Rs)</th>
                            <th id="stock" style="text-align: left;">Closing stock Qty</th>
                        </tr>
                    </thead>
                    <tbody style="font-size: 15px; padding: 10px">
                         
                    </tbody>
                </table>
                
            </div>
        </div>
    </form>
    <script language="javascript" type="text/javascript">

        var AllOrders = [];
        var Orders = [];

        function loadDetails() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Rpt_Distributor_Closing_Stock.aspx/Get_Closing_Stock_Details",
                //data: "{'sfCode':'"+sfCode+"','Dist_Code':'"+Dist_Code+"','FDT':'" + FDT + "','TDT':'" + TDT + "'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders;
                    Append();
                    
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
        }
        function Append() {
            $("#OrderList TBODY").html("");
            for ($i = 0; $i < Orders.length; $i++) {
                if ($i < Orders.length) {
                    tr = $("<tr class='trclick'></tr>");
                    var Closing_stock = Orders[$i].Opening_Stock - Orders[$i].Order_Stock;
                    //let filtarr = Outlets.filter(function(a){ return a.Stockist_Name == Orders[$i]["Stockist_Name"] });
                    $(tr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].eDate + "</td><td>" + Orders[$i].Stockist_Name + "</td><td>" + Orders[$i].Product_Name + "</td><td>" + Orders[$i].Opening_Stock + "</td><td>" + Orders[$i].Order_Stock + "</td><td>" + Orders[$i].Order_value + "</td><td>" + Closing_stock + "</td>");
                    $("#OrderList TBODY").append(tr);
                }
            }
        }
        $(document).ready(function () {
            loadDetails();
        });
        function exportToExcel() {
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
            htmls = document.getElementById("OrderList").innerHTML;

            var ctx = {
                worksheet: 'Closing Stock',
                table: htmls
            }
            var link = document.createElement("a");
            var tets = 'Rpt_Distributor_Closing_stock' + '.xls';

            link.download = tets;
            link.href = uri + base64(format(template, ctx));
            link.click();
        }


    </script>
</body>
</html>


