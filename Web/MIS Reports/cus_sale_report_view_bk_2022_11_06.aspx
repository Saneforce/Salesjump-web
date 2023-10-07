<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cus_sale_report_view.aspx.cs" Inherits="MIS_Reports_cus_sale_report_view" %>

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
                    <img style="cursor: pointer; float: right;" src="/img/excel.png" alt="" width="40" height="40" id="btnExport">
                    <table id="grid" class="grids" style="display: none;">
                        <thead>
                            <tr>
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
            let total = 0;
            for (let i = 0; i < sfdata.length; i++) {
                let retArr = routM.filter(function (a) {
                    return a.Route == sfdata[i].Territory_Code
                });
                for (let j = 0; j < retArr.length; j++) {
                    total = 0;
                    let ordArr = orderdts.filter(function (a) {
                        return a.Cust_Code == retArr[j].Outlet_Code;
                    });
                    let vstArr = vstdts.filter(function (a) {
                        return a.Trans_Detail_Info_Code == retArr[j].Outlet_Code;
                    });
                    str += "<tr><td>" + sfdata[i].Territory_Name + "</td><td>" + sfdata[i].HQ + "</td><td>" + retArr[j].Outlet_Code + "</td><td>" + retArr[j].Outlet_Name + "</td><td>" + retArr[j]["Class"] + "</td>" +
                        "<td>" + retArr[j].Outlet_Type + "</td><td>" + retArr[j].Phone + "</td><td>" + retArr[j].Address + "</td>";
                    for (let x = 0; x < mnths.length; x++) {
                        let mnthO = 0;
                        let mnthV = "";
                        mnthO = Number(ordArr.filter(function (a) {
                            return a.Months == mnths[x];
                        }).map(function (el) {
                            return el.ord_val;
                        }).toString());
                        mnthV = ((vstArr.filter(function (a) {
                            return a.mnth == mnths[x];
                        }).length) > 0 ? "Yes" : "No");
                        total += mnthO;
                        str += `<td>${mnthV}</td><td>${mnthO}</td>`;
                    }
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
</html>
