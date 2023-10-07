<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SS_Sales_Invoice_Products.aspx.cs" Inherits="SuperStockist_Reports_Sales_SS_Sales_Invoice_Products" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/plain; charset=UTF-8" />
    <title></title>
    <link href="../../../../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../../../../css/style.css" rel="stylesheet" />
    <link href="../../../../css/chosen.css" rel="stylesheet" />
    <style type="text/css">
        .newStly td {
            border-collapse: collapse;
            padding-top: 4px;
            padding-bottom: 4px;
            padding-left: 4px;
            padding-right: 4px;
            font-size: 12px;
        }

        .newStly th {
            background-color: #428bca;
            color: white;
        }

        table {
            border-collapse: collapse;
            text-indent: initial;
            border-spacing: 2px;
        }

        .num2 {
            text-align: right;
        }
    </style>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../../js/jquery.table2excel.js"></script>
    <script src="../../../js/jquery.table2excel.min.js"></script>
    <script type="text/javascript">
        var cashdis = 0.00;
        var totlcase = 0;
        var totalpie = 0;
        var grossvalue = 0.00;
        var totalGst = 0.00;
        var taxedtotal = 0.00;
        var subtotal = 0; var total_tax_per = 0; var gsttotal = 0; var total = 0; var grosstotal = 0; var netTotal = 0;

        bindArray = []; var All_Tax = []; var tax_arr = []; var subDivision_Code = '';
        $i = 0;
        $(document).ready(function () {
            var ordid = $('#<%=hid_order_id.ClientID%>').val();

             subDivision_Code = "<%=Session["Sub_Div"]%>";

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "SS_Sales_Invoice_Products.aspx/Get_Product_Tax",
                dataType: "json",
                success: function (data) {
                    All_Tax = JSON.parse(data.d) || [];
                },
                error: function (result) {
                }
            });

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "SS_Sales_Invoice_Products.aspx/Get_sales_products",
                data: "{'order_iid':'" + ordid + "'}",
                dataType: "json",
                success: function (data) {
                    DCR_ProdDts = JSON.parse(data.d) || [];
                    bindArray = DCR_ProdDts;
                    ReloadTable();
                },
                error: function (result) {
                }
            });
        });
        function ReloadTable() {
            var orderlbl = $('#<%=hid_order_id.ClientID%>').val();
            $('#invoicelbl').text(orderlbl);
            $("#OrderList TBODY").html("");
            if ($i < bindArray.length) {
                for ($i; $i < bindArray.length; $i++) {

                    if (subDivision_Code == '41') {

                        if (bindArray[$i].Default_UOM == 15) {
                            var price = bindArray[$i].Price / bindArray[$i].Sample_Erp_Code
                            var gross = bindArray[$i].Quantity * price;
                            var mrpprice = bindArray[$i].MRP;
                        }
                        else if (bindArray[$i].Default_UOM == 14) {
                            var price = bindArray[$i].Price * bindArray[$i].Sample_Erp_Code
                            var MRp = bindArray[$i].MRP / bindArray[$i].Sample_Erp_Code
                            var gross = bindArray[$i].Quantity * price;
                            var mrpprice1 = bindArray[$i].Default_UOMQty * bindArray[$i].Sample_Erp_Code;
                            var mrpprice = bindArray[$i].MRP / mrpprice1;

                        }
                        else if (bindArray[$i].Default_UOM != 14 && bindArray[$i].Default_UOM != 15) {

                            var price = bindArray[$i].Price * bindArray[$i].Default_UOMQty
                            var mrpprice1 = bindArray[$i].Default_UOMQty * bindArray[$i].Sample_Erp_Code;
                            var mrpprice = bindArray[$i].MRP / mrpprice1;
                            var gross = bindArray[$i].Quantity * price;
                        }
                    }
                    else {

                        var price = bindArray[$i].Price;
                        var mrpprice = bindArray[$i].MRP;
                        var gross = bindArray[$i].Quantity * price;
                    }

                    var tax_cal = 0;

                    var tax_filter = All_Tax.filter(function (b) {
                        return (b.Product_Detail_Code == bindArray[$i].Product_Code && b.Tax_Method_Id == '0');
                    });

                    if (tax_filter.length > 0) {
                        for (var z = 0; z < tax_filter.length; z++) {
                            total_tax_per = total_tax_per + parseFloat(tax_filter[z].Tax_Val);
                            tax_cal = (tax_filter[z].Tax_Val / 100) * gross;
                            tax_arr.push({
                                pro_code: bindArray[$i].Product_Code,
                                Tax_Code: tax_filter[z].Tax_Id,
                                Tax_Name: tax_filter[z].Tax_Type,
                                Tax_Amt: 0,
                                Tax_Per: tax_filter[z].Tax_Val,
                                umo_code: bindArray[$i].UOM
                            });
                        }
                    }
                    var tax = 2 * ((tax_cal == 0 || tax_cal == NaN || tax_cal == undefined) ? 0 : tax_cal);
                    var Net = gross + tax;

                    var amount = bindArray[$i].Amount;
                    var discnt = bindArray[$i].Discount;
                    var grossamnt = amount - discnt;

                    slno = $i + 1;
                    var rwStr = "";
                    rwStr += "<tr><td>" + slno + "</td><td>" + bindArray[$i].Product_Name + "</td><td>" + bindArray[$i].HSN_Code + "</td><td>" + bindArray[$i].qty + "</td><td>" + bindArray[$i].Quantity + "</td>";
                    rwStr +="<td>" + price + "</td><td>" + bindArray[$i].Free + "</td><td>" + discnt.toFixed(2) + "</td><td>" + tax.toFixed(2) + "</td><td>" + Net.toFixed(2) + "</td></tr > ";
                    /*<td>" + mrpprice + "</td>*/
                    $("#OrderList tbody").append(rwStr);

                    cashdis += bindArray[$i].Discount;
                    totlcase += bindArray[$i].qty;
                    totalpie += bindArray[$i].Quantity;
                    grossvalue += bindArray[$i].Amount - tax;
                    totalGst += tax;
                    taxedtotal += bindArray[$i].Amount - bindArray[$i].Discount;
                }
                var total = "<tr><td></td><td><b>Total</b></td><td></td><td>" + totlcase + "</td><td>" + totalpie + "</td><td></td><td></td><td>" + cashdis.toFixed(2) + "</td><td>" + totalGst.toFixed(2) + "</td><td>" + parseFloat(Math.round(taxedtotal)).toFixed(2) + "</td></tr>";
                $("#OrderList tbody").append(total);
            }
        }
        $(document).on('click', '#btnExcel', function (e) {
            var data_type = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#divtable').html());
            var a = document.createElement('a');
            a.href = data_type;

            a.download = 'SalesInvoiceProducts.xls';
            a.click();
            e.preventDefault();
        });

    </script>
    <script type="text/javascript">
        var array1 = new Array();
        var array2 = new Array();
        var n = 2; //Total table
        for (var x = 1; x <= n; x++) {
            array1[x - 1] = x;
            array2[x - 1] = x + 'th';
        }

        var tablesToExcel = (function () {
            var uri = 'data:application/vnd.ms-excel;base64,'
                , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets>'
                , templateend = '</x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head>'
                , body = '<body>'
                , tablevar = '<table>{table'
                , tablevarend = '}</table>'
                , bodyend = '</body></html>'
                , worksheet = '<x:ExcelWorksheet><x:Name>'
                , worksheetend = '</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet>'
                , worksheetvar = '{worksheet'
                , worksheetvarend = '}'
                , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
                , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
                , wstemplate = ''
                , tabletemplate = '';

            return function (table, name, filename) {
                var tables = table;

                for (var i = 0; i < tables.length; ++i) {
                    wstemplate += worksheet + worksheetvar + i + worksheetvarend + worksheetend;
                    tabletemplate += tablevar + i + tablevarend;
                }

                var allTemplate = template + wstemplate + templateend;
                var allWorksheet = body + tabletemplate + bodyend;
                var allOfIt = allTemplate + allWorksheet;

                var ctx = {};
                for (var j = 0; j < tables.length; ++j) {
                    ctx['worksheet' + j] = name[j];
                }

                for (var k = 0; k < tables.length; ++k) {
                    var exceltable;
                    if (!tables[k].nodeType) exceltable = document.getElementById(tables[k]);
                    ctx['table' + k] = exceltable.innerHTML;
                }


                window.location.href = uri + base64(format(allOfIt, ctx));

            }
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="row" style="max-width: 100%; width: 98%">
            <br />
            <div class="col-lg-12 sub-header" style="font-size: 16px; text-align: center; font-weight: bolder;">
                SalesInvoice Product List           
            </div>
            <asp:HiddenField ID="hid_order_id" runat="server" />
            <button id="btnExcel" runat="server" align="right" style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 105px; top: 43px;" onclick="lnkDownload_Click">
                <img src="../../../../img/excel.png" />
            </button>
        </div>
        <div id="divtable" style="padding-top: 50px;">
            <div class="col-lg-12 " style="font-size: 16px; text-align: left; font-weight: bolder; float: left; padding-bottom: 10px; padding-left: 20px; color: red;">
                <label style="float: left">Invoice #:</label>
                <label style="float: left" id="invoicelbl"></label>
            </div>

            <table id="OrderList" border="1" class="newStly" style="margin-left: 15px; width: 95%;">
                <thead>
                    <tr>
                        <th>Sl.No</th>
                        <th>DESCRIPTION</th>
                        <th>HSN Code</th>
                        <th>QTY</th>
                        <th>QTY(Pc)</th>
                       <%-- <th>MRP</th>--%>
                        <th>Rate</th>
                        <th>FrQty</th>
                        <th>Discount</th>
                        <th>Tax</th>
                        <th>Amount</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>
