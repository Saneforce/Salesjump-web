<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_FieldPerformance_Aachi.aspx.cs" Inherits="MIS_Reports_Rpt_FieldPerformance_Aachi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title>FieldForce Summary View</title>
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
                <img style="cursor: pointer; float: right;" src="/img/excel.png" alt="" width="40" height="40" id="btnExport" />
            </div>
            <asp:HiddenField ID="hidn_sf_code" runat="server" />
            <asp:HiddenField ID="hFYear" runat="server" />
            <asp:HiddenField ID="hTYear" runat="server" />
            <asp:HiddenField ID="hFMonth" runat="server" />
            <asp:HiddenField ID="hTMonth" runat="server" />
            <asp:HiddenField ID="subDiv" runat="server" />

            <div>
                <div class="row">
                    <div class="col-sm-8">
                        <asp:Label ID="Label1" runat="server" Text="FieldForce Summary" Style="margin-left: 10px; font-size: x-large" />
                    </div>
                </div>
                <div class="row" style="margin: 6px 0px 0px 11px;">
                    <asp:Label ID="Label2" Text="Field Force Name :" runat="server" Style="font-size: larger" />
                    <asp:Label ID="lblsf_name" runat="server" Style="font-size: larger" />
                </div>
                <br />
                <div class="row" style="margin: 0px 0px 0px 5px;">
                    <div id="content">
                        <table id="FFTbl" class="newStly" style="border-collapse: collapse;">
                            <thead>
                                <tr>
                                    <th>SLNo.</th>
                                    <th>Date</th>
                                    <th>State</th>
                                    <th>HQ</th>
                                    <th>User Name</th>
                                    <th>User Rank</th>
                                    <th>SR Contact No</th>
                                    <th>Type</th>
                                    <th>Worked With Name</th>
                                    <th>Reason</th>
                                    <th>Distributors</th>
                                    <th>Distributors Address</th>
                                    <th>Selected Beats</th>
                                    <th>AC</th>
                                    <th>TC</th>
                                    <th>PC</th>
                                    <th>Productivity</th>
                                    <th>New Retailers</th>
                                    <th>New Retailers POB</th>
                                    <th>Telephonic Orders</th>
                                    <th>Login</th>
                                    <th>Log Out</th>
                                    <th>Total Time(HH:MM)</th>
                                    <th>Lat</th>
                                    <th>Long</th>
                                    <th>Address</th>
                                    <th>First Call</th>
                                    <th>Last Call</th>
                                    <th>Total Retail Time</th>
                                    <th>Start Km</th>
                                    <th>End Km</th>
                                    <th>Value</th>
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
            </div>
        </form>
        <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script type="text/javascript">
            var tbplan = [];
            var bDat = [];
            var sfDate = [];
            var sNum = 1;
            var eachrow = 0; var nerc = 0; var tc = 0; var cavg = 0; var tds = 0; var ths = 0;

            function getDayPlan() {
                return $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    url: "Rpt_FieldPerformance_Aachi.aspx/getDayPlan",
                    dataType: "json",
                    success: function (data) {
                        tbplan = JSON.parse(data.d) || [];

                        //console.log(tbplan);
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            }

            function loadData() {
               
                $.when(getDayPlan()).then(function () {
                    ReloadData();
                    setTimeout(function () { loadaddrs($('#FFTbl tbody tr')[0]) }, 29);
                });
            }

            function loadaddrs($tr) {
                var Long = parseFloat($($tr).find('td').eq(23).text());
                var Lat = parseFloat($($tr).find('td').eq(24).text());
                var addrs = '';
                var url = "//nominatim.openstreetmap.org/reverse?format=json&lon=" + Long + '&lat=' + Lat + "";
                $.ajax({
                    url: url,
                    async: false,
                    dataType: 'json',
                    success: function (data) {
                        addrs = data.display_name;
                    }
                });
                $($tr).find('td').eq(25).text(addrs);
                eachrow++;
                if (eachrow < $('#FFTbl tbody tr').length) {
                    setTimeout(function () { loadaddrs($('#FFTbl tbody tr')[eachrow]) }, 29);
                }
            }

            function ReloadData() {
                $("#FFTbl>tbody").html('');
                var str = "";
                let slno = 0;
                let gtotal = 0; 
                
                for (var j = 0; j < tbplan.length; j++) {

                    str += `<tr><td>${++slno}</td><td>${tbplan[j].Date}</td><td>${tbplan[j].State}</td><td>${tbplan[j].HQ}</td><td>${tbplan[j].UserName}</td><td>${tbplan[j].UserRank}</td><td>${tbplan[j].SRContactNo}</td>`;
                    str += `<td>${tbplan[j].WorkType}</td><td>${tbplan[j].WorkedWithName}</td><td>${tbplan[j].Reason}</td><td>${tbplan[j].Distributors}</td><td>${tbplan[j].DistributorsAddress}</td><td>${tbplan[j].SelectedBeats}</td><td>${tbplan[j].AC}</td>`;

                    
                    str += `<td>${tbplan[j].TC}</td><td>${tbplan[j].PC}</td><td>${(isNaN(tbplan[j].Productivity) == true ? 0 : tbplan[j].Productivity)}</td>`;
                    str += `<td>${tbplan[j].NewRetailers}</td><td>${tbplan[j].NewRetailersPOB}</td><td>${tbplan[j].TelephonicOrders}</td>`;

                    str += `<td>${tbplan[j].LoginTime}</td><td>${tbplan[j].LogoutTime}</td><td>${isNaN(tbplan[j].TotaTime) == true ? '' : tbplan[j].TotaTime}</td><td>${tbplan[j].Lat}</td><td>${tbplan[j].Long}</td><td class="place"></td>`;

                    str += `<td>${tbplan[j].FirstCall}</td><td>${tbplan[j].LastCall}</td><td>${tbplan[j].TotalRetailTime}</td><td>${tbplan[j].Start_Km}</td><td>${tbplan[j].End_Km}</td>`;

                    let todayOrderval = parseFloat(tbplan[j].OrderValue).toFixed(2);

                    let daytotal = parseFloat(isNaN(todayOrderval) == true ? 0 : todayOrderval);
                    gtotal = parseFloat(gtotal) + parseFloat(daytotal);
                    str += `<td>${daytotal.toFixed(2)}</td></tr>`;
                }

                let fstr = `<tr><th colspan="31">Total</th><th>${gtotal.toFixed(2)}</th></tr>`
                $("#FFTbl >tbody").append(str);
                $("#FFTbl >tfoot").append(fstr);
            }

            $(document).ready(function () {
                var divcode = '<%=Session["div_code"]%>';
               
                $('#loadover').show();
                getDayPlan(); 
                setTimeout(function () {
                    loadData();
                }, 1000);
                $(document).ajaxStop(function () {
                    $('#loadover').hide();
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
                    var tets = 'FieldForce Performance' + '.xls';   //create fname

                    link.download = tets;
                    link.href = uri + base64(format(template, ctx));
                    link.click();
                });
            });
        </script>
    </body>
</html>
