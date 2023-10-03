<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_SKU_Analysis.aspx.cs" Inherits="MIS_Reports_Rpt_SKU_Analysis" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SKU Analysis</title>
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
        <asp:HiddenField ID="hidn_sf_code" runat="server" />
        <asp:HiddenField ID="hFDT" runat="server" />
        <asp:HiddenField ID="hTDT" runat="server" />
        <asp:HiddenField ID="hsubDiv" runat="server" />
        <div class="container" style="max-width: 100%; width: 95%; text-align: right">
            <img style="cursor: pointer; float: right;" src="/img/excel.png" alt="" width="40" height="40" id="btnExport">
        </div>

        <div class="row" style="margin: 0px 0px 0px 5px;">
            <div class="row">
                <div class="col-sm-8">
                    <asp:Label ID="Label1" runat="server" Text="SKU wise Outlet Coverage" Style="margin-left: 10px; font-size: x-large"></asp:Label>
                </div>
            </div>
            <div class="row" style="margin: 6px 0px 0px 11px;">
                <asp:Label ID="Label2" Text="Field Force Name :" runat="server" Style="font-size: larger"></asp:Label>
                <asp:Label ID="lblsf_name" runat="server" Style="font-size: larger"></asp:Label>
            </div>
            <br />
            <br />
            <div id="content">
                <table id="FFTbl" class="newStly" style="border-collapse: collapse;">
                    <thead>
                        <tr>
                            <th>SLNo.</th>
                            <th>FieldForce Name</th>
                            <th>Route</th>
                            <th>Retailer Count</th>
                            <th>Product Name</th>
                            <th>Billed Outlet Count</th>
                            <th>Repeat Billed Outlet Count</th>
                        </tr>
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
    <script type="text/javascript">
        let Products = [];
        function getSKU() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Rpt_SKU_Analysis.aspx/getDetails",
                data: "{'Div':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    Products = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function loadData() {
            $.when(getSKU()).then(function () {
                ReloadData();
            });
        }
        function ReloadData() {
            $("#FFTbl>tbody").html('');
            let strArr = [];
            for (let i = 0; i < Products.length; i++) {
                let str = `<tr id="${Products[i]["ProductCode"]}" Pname="${Products[i]["ProductName"]}" SfCode="${Products[i]["Sf_Code"]}" SfName="${Products[i]["SF_Name"]}" Territory_Code="${Products[i]["Territory_Code"]}"><td align="right">${i + 1}</td><td>${Products[i]["SF_Name"]}</td><td>${Products[i]["Route"]}</td><td>${Products[i]["RetailerCount"]}</td><td>${Products[i]["ProductName"]}</td><td class="billedoutlets"><a href="#">${Products[i]["Billed"]}</a></td><td>${Products[i]["Repeated"]}</td></tr>`;
                strArr[i] = str;
            }
            $("#FFTbl>tbody").append(strArr.join(''));
        }
        $(document).ready(function () {
            $('#loadover').show();
            setTimeout(function () {
                loadData();
            }, 1000);
            $(document).ajaxStop(function () {
                $('#loadover').hide();
            });
            $(document).on("click", ".billedoutlets", function () {
                let Pcode = $(this).closest('tr').attr("id");
                let Pname = $(this).closest('tr').attr("Pname");
                let Sf_code = $(this).closest('tr').attr("SfCode");
                let Sf_Name = $(this).closest('tr').attr("SfName");
                let Territory_Code = $(this).closest('tr').attr("Territory_Code");
                NewWindow(Pcode, Pname, Sf_code, Sf_Name,Territory_Code);
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
                htmls = document.getElementById("content").innerHTML;


                var ctx = {
                    worksheet: 'Worksheet',
                    table: htmls
                }
                var link = document.createElement("a");
                var tets = 'SKU Wise Outlet Coverage' + '.xls';   //create fname

                link.download = tets;
                link.href = uri + base64(format(template, ctx));
                link.click();
            });
        });
        function NewWindow(Pcode, Pname, OSfCode, OSfName,OTerritory_Code) {
            window.open("Rpt_SKU_BilledOutlets.aspx?&Fdate=<%=FDT%>&Tdate=<%=TDT%>&SF_code=<%=sfcode%>&SF_Name=<%=sfname%>&Sub_Div=<%=subdiv%>&ProductCode=" + Pcode + "&ProductName=" + Pname + "&Territory_Code=" + OTerritory_Code + "&OSfCode=" + OSfCode + "&OSfName=" + OSfName, 'Billed Outlet View', '_blank', 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=950,height=650,left=0,top=0');
        }
    </script>
</body>
</html>
