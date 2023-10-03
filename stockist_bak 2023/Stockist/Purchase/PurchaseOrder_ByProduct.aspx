<%@ Page Title="" Language="C#" MasterPageFile="~/Master_DIS.master" AutoEventWireup="true" CodeFile="PurchaseOrder_ByProduct.aspx.cs" Inherits="Stockist_Purchase_PurchaseOrder_ByProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <h2>Product Wise Purchase Report</h2>

    <div class="form-group">
        <div class="row" style="padding-top: 10px;">
            <label id="Label6" class="col-md-2 col-md-offset-3  control-label">
                From Date
                   
            </label>
            <div class="col-md-4 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                    <input type="date" id="txtfrdate" class="form-control txtFrom">
                </div>
            </div>
        </div>
        <div class="row" style="margin-top: 10px;">
            <label id="Label5" class="col-md-2 col-md-offset-3  control-label">
                To Date                   
            </label>
            <div class="col-md-4 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                    <input type="date" id="ttxtodate" class="form-control todate">
                </div>
            </div>
        </div>
        <div class="row" style="margin-top: 11px;">
            <div class="col-md-6 col-md-offset-5">
                <a id="btnGo" class="btn btn-primary btnview" style="vertical-align: middle; width: 120px;font-size: 16px">
                    <span>Import Excel</span>
                </a>
            </div>
        </div>

        <div id="div_id" style="display: none;">
            <table id="OrderList">
                <thead>
                    <tr>
                        <th>Sl no</th>
                        <th>Order ID</th>
                        <th>Order Date</th>
                        <th>Company</th>
                        <th>Distributor</th>                        
                        <th>Product Code</th>
                        <th>Product Name</th>
                        <th>Unit</th>
                        <th>Price</th>
                        <th>Quantity</th>
                        <th>Gross</th>
                        <th>Discount</th>
                        <th>Tax</th>
                        <th>Net</th>

                    </tr>
                </thead>
                <tbody></tbody>
            </table>
        </div>
    </div>

    <script type="text/javascript">

        var Details_array = [];

        $(document).ready(function () {

            function ReloadTable() {
                $("#OrderList TBODY").html("");
                if (Details_array.length > 0) {
                    for (var $i = 0; $i < Details_array.length; $i++) {
                        slno = $i + 1;
                        var rwStr = "";
                        rwStr += "<tr><td>" + slno + "</td><td>" + Details_array[$i].Trans_Sl_No + "</td><td>" + Details_array[$i].Order_Date + "</td><td>" + Details_array[$i].Sup_Name + "</td><td>" + Details_array[$i].CustomerName + "</td><td>" + Details_array[$i].Product_Code + "</td><td>" + Details_array[$i].Product_Name + "</td><td>" + Details_array[$i].Unit + "</td><td>" + parseFloat(Details_array[$i].Rate).toFixed(2) + "</td><td>" + Details_array[$i].CQty + "</td><td>" + parseFloat(Details_array[$i].gross).toFixed(2) + "</td><td>" + Details_array[$i].discount + "</td><td>" + Details_array[$i].tax + "</td><td>" + Details_array[$i].value + "</td></tr>";
                        $("#OrderList tbody").append(rwStr);
                    }
                }
            }

            $(document).on("click", ".btnview", function () {

                var From_Date = $('#txtfrdate').val();
                var To_Date = $('#ttxtodate').val();

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "PurchaseOrder_ByProduct.aspx/Get_Primary_Details",
                    data: "{'FromDate':'" + From_Date + "','ToDate':'" + To_Date + "'}",
                    dataType: "json",
                    success: function (data) {
                        Details_array = JSON.parse(data.d) || [];
                        ReloadTable();
                        if (Details_array.length > 0) {
                            exportToExcel();
                        }
                        else {
                            alert('No Data Available');
                            return false;
                        }

                    },
                    error: function (result) {
                    }
                });
            });


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
            htmls = document.getElementById("div_id").innerHTML;

            var ctx = {
                worksheet: 'Worksheet',
                table: htmls
            }
            var link = document.createElement("a");
            var tets = 'Product_Wise_Purchase_Report' + '.xls';   //create fname

            link.download = tets;
            link.href = uri + base64(format(template, ctx));
            link.click();
        }


    </script>



</asp:Content>

