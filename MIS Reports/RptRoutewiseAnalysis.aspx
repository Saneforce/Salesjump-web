<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RptRoutewiseAnalysis.aspx.cs" Inherits="MIS_Reports_RptRoutewiseAnalysis" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Routewise Distributor Performance</title>
    <link type="text/css" rel="Stylesheet" href="../../css/Report.css" />
    <link href="../../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../../css/style.css" rel="stylesheet" />
    <style type="text/css">
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

        .tr_det_head {
            font-family: Verdana;
            color: White;
            font-size: 9pt;
            height: 22px;
            font-weight: bold;
            font-family: Calibri;
            background: #0097AC;
            border-color: Black;
        }

        .tbldetail_main {
            font-family: Verdana;
            font-size: 7.8pt;
            height: 17px;
            border: 1px solid;
            border-color: #999999;
        }

        .tbldetail_Data {
            height: 18px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container" style="max-width: 100%; width: 95%; text-align: right">
            <img style="cursor: pointer; float: right;" src="/img/excel.png" alt="" width="40" height="40" id="btnExport">
        </div>

        <asp:HiddenField ID="hidn_sf_code" runat="server" />
        <asp:HiddenField ID="hFYear" runat="server" />
        <asp:HiddenField ID="hTYear" runat="server" />
        <asp:HiddenField ID="hFMonth" runat="server" />
        <asp:HiddenField ID="hTMonth" runat="server" />
        <asp:HiddenField ID="subDiv" runat="server" />
        <div class="row">
            <div class="col-sm-8">
                <asp:Label ID="Label1" runat="server" Text="Routewise Distributor Performance" Style="margin-left: 10px; font-size: x-large"></asp:Label>
            </div>
        </div>
        <div class="row" style="margin: 0px 0px 0px 5px;">
            <br />
            <br />
            <div id="content">
                <div class="row" style="margin: 6px 0px 0px 11px;">
                    <asp:Label ID="Label2" Text="Field Force Name :" runat="server" Style="font-size: larger"></asp:Label>
                    <span id="lblsf_name"></span>
                </div>

                <br />
                <br />
                <table id="FFTbl" class="newStly" style="border-collapse: collapse;">
                    <thead>
                    </thead>
                    <tbody>
                    </tbody>
                    <tfoot>
                    </tfoot>
                </table>
            </div>
        </div>
        <div class="overlay" id="loadover" style="display: none;">
            <div id="loader"></div>
        </div>
    </form>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="../js/xlsx.full.min.js"></script>
    <script type="text/javascript">
        let RouteData = [];
        let DcrData = [];
        let brandwiseSales = [];
        let brands = [];
        function getRoutes() {
            return $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "RptRoutewiseAnalysis.aspx/getRoutes",
                dataType: "json",
                success: function (data) {
                    RouteData = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function getDCRCount() {
            return $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "RptRoutewiseAnalysis.aspx/getDCRCountDetails",
                dataType: "json",
                success: function (data) {
                    DcrData = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function getBrandwiseSales() {
            return $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "RptRoutewiseAnalysis.aspx/getBrandwiseSales",
                dataType: "json",
                success: function (data) {
                    brandwiseSales = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function getBrands() {
            return $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "RptRoutewiseAnalysis.aspx/getProductBrandlist",
                dataType: "json",
                success: function (data) {
                    brands = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function ReloadData() {
            $('#FFTbl thead').html('');
            $('#FFTbl tbody').html('');
            $('#FFTbl tfoot').html('');

            let headTr = `<tr><th rowspan="2">Employee Name</th><th rowspan="2">Employee ID</th><th rowspan="2">Distributor Name</th><th rowspan="2">Route Name</th><th rowspan="2">Total Retailers</th><th rowspan="2">No Of Visit</th><th rowspan="2">Total Visited</th><th rowspan="2">Billed Retailers</th><th rowspan="2">Unbilled Retailers</th>`;
            let secondHeadTr = '<tr>';
            for (let i = 0; i < brands.length; i++) {
                headTr += `<th colspan="2">${brands[i]["Product_Brd_Name"]}</th>`;
                secondHeadTr += '<th>Quantity</th><th>Value</th>'
            }
            headTr += '<th colspan="4">Total</th></tr>'
            secondHeadTr += '<th>QTY</th><th>SKU Count</th><th>Net Weight</th><th>Values</th></tr>';
            $('#FFTbl thead').append(headTr);
            $('#FFTbl thead').append(secondHeadTr);

            let htmlarr = [];
            let totarr = [];
            for (let i = 0; i < RouteData.length; i++) {
                let overallQTY = 0;
                let overallValue = 0;
                let ar = 0;
                let OutletCount = parseInt(RouteData[i]["Cnt"]);
                let str = `<tr><td>${RouteData[i]["Sf_Name"]}</td><td>${RouteData[i]["sf_emp_id"]}</td><td>${RouteData[i]["Stockist_Name"]}</td><td>${RouteData[i]["Territory_Name"]}</td><td>${OutletCount}</td>`;

                totarr[ar] = (totarr[ar] != undefined) ? Number(totarr[ar]) + OutletCount : OutletCount;
                ar++;
                let filtArrD = [];
                filtArrD = DcrData.filter(function (a) { return a.SDP == RouteData[i]["Territory_Code"] && a.Sf_Code == RouteData[i]["Sf_Code"] });
                if (filtArrD.length > 0) {
                    let unBilled = OutletCount - parseInt(filtArrD[0]["Billed"]);
                    str += `<td>${filtArrD[0]["Visit"]}</td><td>${filtArrD[0]["TC"]}</td><td>${filtArrD[0]["Billed"]}</td><td>${unBilled}</td>`;
                    totarr[ar] = (totarr[ar] != undefined) ? Number(totarr[ar]) + parseInt(filtArrD[0]["Visit"]) : parseInt(filtArrD[0]["Visit"]);
                    ar++;
					totarr[ar] = (totarr[ar] != undefined) ? Number(totarr[ar]) + parseInt(filtArrD[0]["TC"]) : parseInt(filtArrD[0]["TC"]);
                    ar++;
                    totarr[ar] = (totarr[ar] != undefined) ? Number(totarr[ar]) + parseInt(filtArrD[0]["Billed"]) : parseInt(filtArrD[0]["Billed"]);
                    ar++;
                    totarr[ar] = (totarr[ar] != undefined) ? Number(totarr[ar]) + parseInt(unBilled) : parseInt(unBilled);
                    ar++;
                }
                else {
                    str += `<td></td><td></td><td></td>`;
                    totarr[ar] = (totarr[ar] != undefined) ? Number(totarr[ar]) + 0 : 0;
                    ar++;
                    totarr[ar] = (totarr[ar] != undefined) ? Number(totarr[ar]) + 0 : 0;
                    ar++;
                    totarr[ar] = (totarr[ar] != undefined) ? Number(totarr[ar]) + 0 : 0;
                    ar++;
                }
                for (let j = 0; j < brands.length; j++) {
                    let filterBrand = [];

                    filterBrand = brandwiseSales.filter(function (a) { return a.Route == RouteData[i]["Territory_Code"] && a.Sf_Code == RouteData[i]["Sf_Code"] && a.Product_Brd_Code == brands[j]["Product_Brd_Code"] });

                    totarr[ar] = ((filterBrand[0] != undefined) ? ((isNaN(totarr[ar]) ? 0 : totarr[ar]) + filterBrand[0].Qty) : ((isNaN(totarr[ar]) ? 0 : totarr[ar]) + 0));
                    ar++;

                    totarr[ar] = ((filterBrand[0] != undefined) ? ((isNaN(totarr[ar]) ? 0 : totarr[ar]) + parseFloat(filterBrand[0].OrderVal)) : ((isNaN(totarr[ar]) ? 0 : totarr[ar]) + 0));
                    ar++;

                    if (filterBrand.length > 0) {
                        overallQTY += parseFloat(filterBrand[0]["Qty"]);
                        overallValue += parseFloat(filterBrand[0]["OrderVal"]);
                        str += `<td>${filterBrand[0]["Qty"]}</td><td>${parseFloat(filterBrand[0]["OrderVal"]).toFixed(2)}</td>`;
                    }
                    else {
                        str += `<td></td><td></td>`;
                    }
                }
                let skuCount = brandwiseSales.filter(function (a) { return a.Route == RouteData[i]["Territory_Code"] && a.Sf_Code == RouteData[i]["Sf_Code"] }).reduce(function (prev, cur) {
                    return prev + cur.SKUCount;
                }, 0);
                totarr[ar] = (totarr[ar] != undefined) ? Number(totarr[ar]) + overallQTY : overallQTY;
                ar++;

                totarr[ar] = (totarr[ar] != undefined) ? Number(totarr[ar]) + skuCount : skuCount;
                ar++;

                totarr[ar] = (totarr[ar] != undefined) ? Number(totarr[ar]) + 0 : 0;
                ar++;

                totarr[ar] = (totarr[ar] != undefined) ? Number(totarr[ar]) + overallValue : overallValue;
                ar++;

                str += `<td>${overallQTY}</td><td>${skuCount}</td><td>0</td><td>${parseFloat(overallValue).toFixed(2)}</td>`;

                //totarr[ar] = ((filtArrD[0] != undefined) ? ((isNaN(totarr[ar]) ? 0 : totarr[ar]) + filtArrD[0]["TC"]) : ((isNaN(totarr[ar]) ? 0 : totarr[ar]) + 0));
                //ar++;

                //totarr[ar] = ((filtArrD[0] != undefined) ? ((isNaN(totarr[ar]) ? 0 : totarr[ar]) + filtArrD[0]["EC"]) : ((isNaN(totarr[ar]) ? 0 : totarr[ar]) + 0));
                //ar++;

                //if (filtArrD.length > 0) {
                //    str += `<td>${filtArrD[0]["TC"]}</td><td>${filtArrD[0]["EC"]}</td>`;
                //}
                //else {
                //    str += `<td></td><td></td><td></td>`;
                //}
                htmlarr[i] = str;
            }
            $('#FFTbl tbody').append(htmlarr.join(''));
            let footer = '<tr><th colspan="4">Total</th>';
            for (let i = 0; i < totarr.length; i++) {
                footer += `<th>${parseFloat(totarr[i]).toFixed(2)}</th>`;
            }
            footer += '</tr>';
            $('#FFTbl tfoot').append(footer);
        }
        function loadData() {
            $.when(getBrands(), getBrandwiseSales(), getDCRCount(), getRoutes()).then(function () {
                ReloadData();
            });
        }
        function downloadExcel(type, fn, dl) {
            var elt = document.getElementById('content');
            var wb = XLSX.utils.table_to_book(elt, { sheet: "Routewise Performance" });
            return dl ?
                XLSX.write(wb, { bookType: type, bookSST: true, type: 'base64' }) :
                XLSX.writeFile(wb, fn || ((type || 'xlsx')));
        }
        $(document).ready(function () {
            $('#lblsf_name').text('<%=sfname%>');
            $('#loadover').show();
            setTimeout(function () {
                loadData();
            }, 1000);
            $(document).ajaxStop(function () {
                $('#loadover').hide();
            });
            $('#btnExport').on('click', function (a) {
                downloadExcel('biff8', 'Routewise Performance.xls');
            })
        });
    </script>
</body>
</html>
