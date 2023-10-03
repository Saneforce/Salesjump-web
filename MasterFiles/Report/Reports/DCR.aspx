<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DCR.aspx.cs" Inherits="MasterFiles_Reports_DCR" %>

<!DOCTYPE html>

<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DCR Detailed View</title>
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

        .Holiday {
            color: Red;
            font-size: 9pt;
            font-family: Calibri;
        }

        .NoRecord {
            font-size: 10pt;
            font-weight: bold;
            color: Red;
            background-color: AliceBlue;
        }

        .table td {
            padding: 2px 5px;
            white-space: nowrap;
        }

        .gridviewStyle td {
            border: thin solid #000000;
            text-align: right;
            padding-right: 3px;
            max-width: 300px;
        }

        .gridpager td {
            padding-left: 13px;
            color: cornsilk;
            font-weight: bold;
            text-decoration: none;
            width: 100%;
        }

        [data-val='HELPLINE REQUIRED'] {
            background-color: yellow;
        }

        [data-val='RESOLVED'] {
            background-color: #18eb18;
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
                <asp:Label ID="Label1" runat="server" Text="Brandwise Sales" Style="margin-left: 10px; font-size: x-large"></asp:Label>

            </div>

        </div>
        <div class="row" style="margin: 6px 0px 0px 11px;">
            <asp:Label ID="Label2" Text="Field Force Name :" runat="server" Style="font-size: larger"></asp:Label>
            <asp:Label ID="lblsf_name" runat="server" Style="font-size: larger"></asp:Label>
        </div>
        <div class="row" style="margin: 0px 0px 0px 5px;">
            <br />
            <br />
            <div id="content">
            </div>
        </div>
        <div class="overlay" id="loadover" style="display: none;">
            <div id="loader"></div>
        </div>
    </form>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        let sfusers = [], daydets = [], orderdets = [];
        let div_code = '<%=Session["div_code"]%>';
        function getUsers() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "DCR.aspx/getSFdets",
                data: "{'Div':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    sfusers = JSON.parse(data.d) || [];
                    $('#content').empty();
                    $.when(getSFdets(), getOrders()).then(function () {
                        sfusers.forEach(function (el, i) {
                            let ldaydets = daydets.filter(function (a) { return a.Sf_Code == el.SF_Code });
                            let lorderdets = orderdets.filter(function (a) { return a.Sf_Code == el.SF_Code });
                            ReloadTable(ldaydets, lorderdets, el.SF_Name, el.sf_hq);
                        });
                    });
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function getSFdets() {
            return $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "DCR.aspx/getDaywisePlan",
                data: "{'Div':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    daydets = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function getOrders() {
            return $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "DCR.aspx/getDaywiseOrder",
                data: "{'Div':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    orderdets = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function ReloadTable(sfdaydets, proddets, sfnm, sfhq) {
            if (sfdaydets.length > 0) {
                let sftbl = '<table><tbody style="font-size: 11pt;"><tr><td>Name</td><td>:' + sfnm + '</td></tr><tr><td>HQ</td><td>:' + sfhq + '</td></tr></tbody></table>';
                let ptbl = $('<table border="1" class="newStly" style="border-collapse: collapse;margin:1rem 0rem 4rem 0rem;"></table>');
                ptbl.append('<thead></thead>');
                ptbl.append('<tbody></tbody>');
                ptbl.append('<tfoot></tfoot>');
                let hstr = '<tr><th>Slno</th><th>Route</th><th>Sub.Date</th><th>Work Type</th><th>Retailer(s) Visited</th><th>Joint Work</th>';
                let htr4 = '<tr><td colspan="6">Total</td>';
                let oprods = [];
                let mymap = new Map();
                oprods = proddets.filter(function (el) {
                    const val = mymap.get(el.Product_Code);
                    if (val) {
                        return false;
                    }
                    mymap.set(el.Product_Code, el.Product_Name);
                    return true;
                }).map(function (a) { return a; }).sort();
                for (var i = 0; i < oprods.length; i++) {
                    hstr += '<th>' + oprods[i].Product_Name + '</th>';
                }
                hstr += '<th>Order Value</th><th>Net Value</th><th>TC</th><th>EC</th><th>Stock</th><th>Achieved Value</th></tr>';
                $(ptbl).find('thead').append(hstr);
                let tr = '';
                var totarr = [];
                let totordval = 0;
                let totachval = 0;
                let tottc = 0;
                let totec = 0;
                let totstock = 0;
                for ($i = 0; $i < sfdaydets.length; $i++) {
                    let ar = 0;
                    tr += "<tr class='" + sfdaydets[$i].ActDate + ',' + sfdaydets[$i].Sf_Code + "'><td><a class='dtclick' href='#' onclick=dtclick(this)>" + ($i + 1) + "</a></td><td>" + sfdaydets[$i].rout + "</td><td>" + sfdaydets[$i].Dt + "</td><td>" + sfdaydets[$i].Wtype + "</td><td><a class='rtclick' href='#' onclick=rtclick(this)>" + sfdaydets[$i].Retailer + "</a></td><td>" + sfdaydets[$i].Worked_with_Name + "</td>";
                    let todayarr = proddets.filter(function (a) { return (a.Odt == sfdaydets[$i].Dt) });
                    for (var $j = 0; $j < oprods.length; $j++) {
                        let ordarr = todayarr.filter(function (a) { return (a.Product_Code == oprods[$j].Product_Code) });
                        tr += "<td>" + (ordarr.length > 0 ? ordarr[0].Quantity : '') + "</td>";
                        totarr[ar] = ((ordarr[0] != undefined) ? ((isNaN(totarr[ar]) ? 0 : totarr[ar]) + ordarr[0].Quantity) : ((isNaN(totarr[ar]) ? 0 : totarr[ar]) + 0));
                        ar++;
                    }
                    let ordval = todayarr.reduce(function (prev, cur) {
                        return prev + cur.Oval;
                    }, 0);
                    let achvl = todayarr.reduce(function (prev, cur) {
                        return prev + cur.Achval;
                    }, 0);
                    totordval += parseFloat(ordval);
                    totachval += parseFloat(achvl);
                    tottc += Number(sfdaydets[$i].Retailer);
                    totec += Number(sfdaydets[$i].efc);
                    totstock += Number(todayarr.length);
                    tr += "<td align=right'>" + ordval.toFixed(2) + "</td><td>0</td><td>" + sfdaydets[$i].Retailer + "</td><td>" + sfdaydets[$i].efc + "</td><td>" + todayarr.length + "</td><td align=right'>" + achvl.toFixed(2) + "</td></tr>";
                }
                for (var i = 0; i < totarr.length; i++) {
                    htr4 += '<td>' + totarr[i] + '</td>';
                }
                htr4 += '<td>' + totordval.toFixed(2) + '</td><td>0</td><td>' + tottc + '</td><td>' + totec + '</td><td>' + totstock + '</td><td>' + totachval.toFixed(2) + '</td></tr>'
                $(ptbl).find('tfoot').append(htr4);
                $(ptbl).find('tbody').append(tr);
                $('#content').append(sftbl);
                $('#content').append(ptbl);
            }
        }
        function dtclick($x) {
            let itm = ($($x).closest('td').closest('tr').attr('class')).toString().split(',');
            let sfcd = itm[1];
            let sdt = (itm[0]).split('-');
            let sURL = "rptDCRViewApprovedDetails.aspx?sf_Name=" + "&sf_code=" + sfcd + "&Year=" + sdt[0] + "&Month=" + sdt[1] + "&div_code=" + div_code + " &Day=" + sdt[2] + "";
            window.open(sURL, '_blank', 'statusbar=1,scrollbar=1,locator=0,width=1000,height=500,menubar=1,menubar=0,resizable=1,top=0,bottom=0');
        }
        function rtclick($x) {
            let itm = ($($x).closest('td').closest('tr').attr('class')).toString().split(',');
            let sfcd = itm[1];
            let sdt = (itm[0]).split('-');
            let sURL = "rptDCRViewCompleted.aspx?sf_Name=" + "&sf_code=" + sfcd + "&Year=" + sdt[0] + "&Month=" + sdt[1] + "&Day=" + sdt[2] + "&Type=1";
            window.open(sURL, '_blank', 'statusbar=1,scrollbar=1,locator=0,width=1000,height=500,menubar=1,menubar=0,resizable=1,top=0,bottom=0');
        }
         function getmgrUsers() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "DCR.aspx/getmgrdets",
                data: "{'Div':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    sfusers = JSON.parse(data.d) || [];
                    $('#content').empty();
                    $.when(getmgrdets(), getmgrOrders()).then(function () {
                        sfusers.forEach(function (el, i) {
                            let ldaydets = daydets.filter(function (a) { return a.Sf_Code == el.SF_Code });
                            let lorderdets = orderdets.filter(function (a) { return a.Sf_Code == el.SF_Code });
                            ReloadTable(ldaydets, lorderdets, el.SF_Name, el.sf_hq);
                        });
                    });
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function getmgrdets() {
            return $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "DCR.aspx/getmgrDaywisePlan",
                data: "{'Div':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    daydets = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function getmgrOrders() {
            return $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "DCR.aspx/getmgrDaywiseOrder",
                data: "{'Div':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    orderdets = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        $(document).ready(function () {
            var type = "<%=type%>";
            if (type == 0) {
                $('#loadover').show();
                setTimeout(function () {
                    getUsers();
                }, 1000);
                $(document).ajaxStop(function () {
                    $('#loadover').hide();
                });
            }
            else {
                 $('#loadover').show();
                setTimeout(function () {
                    getmgrUsers();
                }, 1000);
                $(document).ajaxStop(function () {
                    $('#loadover').hide();
                });
            }
           
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
                var tets = 'DCR Detailed View' + '.xls';   //create fname

                link.download = tets;
                link.href = uri + base64(format(template, ctx));
                link.click();
            });
        });
    </script>
</body>
</html>
