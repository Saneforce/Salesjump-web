<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Brandwise_Sales_Rpt.aspx.cs" Inherits="MIS_Reports_Brandwise_Sales_Rpt" %>

<!DOCTYPE html>

<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Brandwise POB</title>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
</head>
<body>

    <form id="form1" runat="server">
        <asp:HiddenField ID="hidn_sf_code" runat="server" />
        <asp:HiddenField ID="hFYear" runat="server" />
        <asp:HiddenField ID="hTYear" runat="server" />
        <asp:HiddenField ID="hFMonth" runat="server" />
        <asp:HiddenField ID="hTMonth" runat="server" />
        <asp:HiddenField ID="subDiv" runat="server" />
        <div class="row">
            <div class="col-sm-8" style="margin: 0px;">
                <asp:Label ID="Label1" runat="server" Text="Brandwise Sales" Style="margin-left: 10px; font-size: x-large"></asp:Label>

            </div>

            <div class="col-sm-4" style="text-align: right">
                <a name="btnExcel" id="btnExcel" type="button" href="#" class="btn btnExcel"></a>
                <a name="btnClose" id="btnClose" type="button" href="javascript:window.open('','_self').close();" class="btn btnClose"></a>
            </div>
        </div>
        <div class="row" style="margin: 6px 0px 0px 11px;">
            <asp:Label ID="Label2" Text="Field Force Name :" runat="server" Style="font-size: larger"></asp:Label>
            <asp:Label ID="lblsf_name" runat="server" Style="font-size: larger"></asp:Label>
        </div>
        <div class="row" style="margin:0px 0px 0px 5px;">
            <br />

            <br />
            <div id="content">
                <table id="Product_Table" border="1" class="newStly" style="border-collapse: collapse;">
                    <thead>
                    </thead>
                    <tbody>
                    </tbody>
                    <tfoot>
                    </tfoot>
                </table>
            </div>
        </div>
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <%-- <img src="../../../Images/loader.gif" alt="" />--%>
        </div>
    </form>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        var Pbrands = [], Gorders = [], Gusers = [];
        function getUsers() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Brandwise_Sales_Rpt.aspx/getBrandwiseSalesUsr",
                data: "{'Div':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    Gusers = JSON.parse(data.d) || [];
                    console.log(data.d);
                    getBrands();
                    getOrders();
                    ReloadTable();
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }

            });
        }
        function getOrders() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Brandwise_Sales_Rpt.aspx/getBrandwiseSales",
                data: "{'Div':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    Gorders = JSON.parse(data.d) || [];
                    console.log(data.d);
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function getBrands() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Brandwise_Sales_Rpt.aspx/getProductBrandlist",
                data: "{'Div':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    Pbrands = JSON.parse(data.d) || [];
                    Pbrands.sort(function (a, b) {
                        if (a["Product_Brd_Name"].toLowerCase() < b["Product_Brd_Name"].toLowerCase()) return -1;
                        if (a["Product_Brd_Name"].toLowerCase() > b["Product_Brd_Name"].toLowerCase()) return 1;

                        if (b["Product_Brd_Name"].toLowerCase() < a["Product_Brd_Name"].toLowerCase()) return -1;
                        if (b["Product_Brd_Name"].toLowerCase() > a["Product_Brd_Name"].toLowerCase()) return 1;
                        return 0;
                    });
                    console.log(data.d);
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }

            });
        }
        function ReloadTable() {
            $('#Product_Table thead').html('');
            $('#Product_Table tbody').html('');
            var hstr = '<tr><th rowspan="2">SR</th><th rowspan="2">State</th>';
            var hstr2 = '<tr>';
            for (var i = 0; i < Pbrands.length; i++) {
                hstr += '<th colspan="2">' + Pbrands[i].Product_Brd_Name + '</th>';
                hstr2 += '<th>UNIT</th><th>VALUE</th>';
            }
            hstr += '</tr>';
            hstr2 += '</tr>';
            $('#Product_Table thead').append(hstr);
            $('#Product_Table thead').append(hstr2);
            var bstr = '';
            for (var i = 0; i < Gusers.length; i++) {
                var csf = '';
                bstr += '<tr><td>' + Gusers[i].FieldForce + '</td><td>' + Gusers[i].State + '</td>';
                for (var j = 0; j < Pbrands.length; j++) {
                    var filt = Gorders.filter(function (a) {
                        return a.FieldForce == Gusers[i].Sf_Code && a.Product_Brd_Code == Pbrands[j].Product_Brd_Code;
                    });
                    bstr += '<td>' + ((filt.length > 0) ? filt[0].Quantity : 0) + '</td><td>' + ((filt.length > 0) ? filt[0].Value : 0) + '</td>';
                }
            }
            $('#Product_Table tbody').append(bstr);
        }
        $(document).ready(function () {
            getUsers();
            $('#btnExcel').click(function (event) {

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
                htmls = document.getElementById("Product_Table").innerHTML;


                var ctx = {
                    worksheet: 'Worksheet',
                    table: htmls
                }
                var link = document.createElement("a");
                var tets = /*'Expense_Report' + '.xls';   //create fname */"Brandwise_Sales.xls"

                link.download = tets;
                link.href = uri + base64(format(template, ctx));
                link.click();
                event.preventDefault();
            });
        })
    </script>
</body>
</html>
