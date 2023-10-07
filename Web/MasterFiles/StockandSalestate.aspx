<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="StockandSalestate.aspx.cs" Inherits="MasterFiles_StockandSalestate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <html>
    <head>
        <title></title>
        <body>
            <form runat="server">
                <div style="width: 100%">
                    <div>
                        <label style="font-size: 26px">Stock and Sale Statement</label>
                        <span style="float: right; margin-right: 80px;">
                            <img src="../img/Excel-icon.png" style="height: 50px; width: 50px; border-width: 0px; position: absolute; right: 15px;" onclick="exportToExcel()" />
                        </span>
                    </div>
                    <div style="height: 60px; margin-left: 77px;">
                        <div class="col-lg-4">
                            <label>Distributor</label><select id="stockist" style="width: 250px"></select>
                        </div>
                        <div class="col-lg-4">
                            <label>Town</label><input id="route" style="width: 250px" readonly />
                        </div>
                        <div class="col-lg-4">
                            <label>Date</label><input type="date" id="Date" onchange="loaddata()" />
                        </div>
                        <%-- <div class="col-lg-3"><label>Date</label><input type="date"  id="date"/></div>--%>
                    </div>
                    <div class="card">
                        <table id="stocktable" border="2px">
                            <thead>
                                <tr>
                                    <th style="text-align: center;" rowspan="2">S.No</th>
                                    <th style="text-align: center;" rowspan="2">Product Name</th>
                                    <th style="text-align: center;" rowspan="2">opening Stock</th>
                                    <th style="text-align: center;" rowspan="2">Receipts During the Month</th>
                                    <th style="text-align: center;" rowspan="2">Total Stock</th>
                                    <th style="text-align: center;" colspan="2">Sales</th>
                                    <th style="text-align: center;" rowspan="2">Total Sales</th>
                                    <th style="text-align: center;" rowspan="2">Closing Balance Stock</th>
                                    <th style="text-align: center;" colspan="3">Fresh Primary</th>
                                </tr>
                                <tr>
                                    <th style="text-align: center;">Distributor Personal</th>
                                    <th style="text-align: center;">Co-Sales Officer</th>
                                    <th style="text-align: center;">Dispatch Date</th>
                                    <th style="text-align: center;">Dispatch Date</th>
                                    <th style="text-align: center;">Value</th>
                                </tr>
                            </thead>
                            <tbody></tbody>
                        </table>
                    </div>
                </div>
            </form>
            <script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
            <link href="../css/jquery.multiselect.css" rel="stylesheet" />
            <link href="../css/SalesForce_New/bootstrap-select.min.css" rel="stylesheet" />
            <script type="text/javascript">
                $(document).ready(function () {
                    loadDist();
                    $('#stockist').on('change', function () {
                        var town = $('#stockist option:selected').attr('addr');
                        $('#route').val(town); loaddata();
                    });
                });
                function getCurrentDate() {
                    const now = new Date();
                    const year = now.getFullYear();
                    const month = (now.getMonth() + 1).toString().padStart(2, '0'); // Month is 0-based
                    const day = now.getDate().toString().padStart(2, '0');
                    return `${year}-${month}-${day}`;
                }

                // Set the maximum date for the input element to the current date
                document.getElementById("Date").max = getCurrentDate();
                function loaddata() {
                    var dist = $('#stockist').val();
                    var date = $('#Date').val();
                    if (date == "") {
                        document.getElementById('Date').valueAsDate = new Date();
                        var ddt = new Date;
                        date = ddt.getFullYear() + '-' + (ddt.getMonth() + 1) + '-' + ddt.getDate();
                        //ddate = "2023-02-24";
                    }
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        data: "{'dist':'" + dist + "','date':'" + date +"','divcode':'<%=Session["div_code"]%>'}",
                        url: "StockandSalestate.aspx/loaddetails",
                        dataType: "json",
                        success: function (data) {
                            Orders = JSON.parse(data.d) || [];
                            ReloadTable();
                        },
                        error: function (result) {
                        }
                    });
                }
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
                    htmls = document.getElementById("stocktable").innerHTML;

                    var ctx = {
                        worksheet: 'Stock_and_SaleReport',
                        table: htmls
                    }
                    var link = document.createElement("a");
                    var tets = 'Stock_and_SaleReport' + '.xls';

                    link.download = tets;
                    link.href = uri + base64(format(template, ctx));
                    link.click();
                }
                function loadDist() {

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        data: "{'divcode':'<%=Session["div_code"]%>'}",
                        url: "StockandSalestate.aspx/loadstockist",
                        dataType: "json",
                        success: function (data) {
                            Aallsto = JSON.parse(data.d) || [];
                            if (Aallsto.length > 0) {
                                Stockist_Name = "";
                                Stockist_Name = $("#stockist").empty();
                                Stockist_Name.append($('<option value="" addr="">--- Select --- </option>'))
                                for (var i = 0; i < Aallsto.length; i++) {
                                    if (Aallsto[i].Dist_Name == "Select Town" || Aallsto[i].Dist_Name == "Select Taluk") {
                                        var addr = '';
                                    } else {
                                         var addr = Aallsto[i].Dist_Name ;
                                    }
                                    Stockist_Name.append($('<option value="' + Aallsto[i].Stockist_Code + '" addr="' + addr + '">' + Aallsto[i].Stockist_Name + '</option>'))
                                }

                            }
                        },
                        error: function (result) {
                        }
                    });
                    $("#stockist").chosen();
                }

                function ReloadTable() {
                    $("#stocktable TBODY").html("");
                    if (Orders.length > 0) {
                        for (var $i = 0; $i < Orders.length; $i++) {
                            tr = $("<tr></tr>");
                            slno = $i + 1;
                            if (Orders[$i].total == null) { var total = ''; } else { var total = Orders[$i].total }
                            if (Orders[$i].qty == null) { var qty = ''; } else { var qty = Orders[$i].qty }
                            if (Orders[$i].total == null && Orders[$i].qty == null) { var closestock = ''; } else { var closestock = total - qty; }
                            $(tr).html("<td>" + slno + "</td><td>" + Orders[$i].Product_Detail_Name + "</td><td></td><td></td><td></td><td>" + total + "</td><td>" + qty + "</td><td>" + closestock + "</td><td></td><td></td><td></td><td></td>");
                            $("#stocktable TBODY").append(tr);
                        }

                    }
                }
            </script>
        </body>
    </head>

    </html>

</asp:Content>

