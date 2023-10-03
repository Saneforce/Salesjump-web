<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptFFWiseUOMReport.aspx.cs" Inherits="MIS_Reports_rptFFWiseUOMReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="https://code.jquery.com/jquery-3.3.1.js"></script>
    <script type="text/javascript">
        jQuery(document).ready(function ($) {
            var tcEC = [];
            var ordDt = [];
            var UOM = [];
            var Category = [];
            var UData = [];

            var Fyear = $("#<%=hFYear.ClientID%>").val();
            var FMonth = $("#<%=hFMonth.ClientID%>").val();
            var SubDivCode = $("#<%=hSubDiv.ClientID%>").val();
            var SfCode = $("#<%=hsfCode.ClientID%>").val();
            var mgronly = $("#<%=mgronly.ClientID%>").val();


            if (mgronly == "0") {


                getTable = function () {
                    var tbl = $('#FFTable');
                    $(tbl).find('thead tr').remove();
                    $(tbl).find('tbody tr').remove();
                    if (ordDt.length > 0 && Category.length > 0 && UOM.length > 0) {

                        var stcat = '';
                        var stUOM = '';
                        for (var c = 0; c < Category.length; c++) {
                            cF = UOM.filter(function (obj) {
                                return obj.catCode == Category[c].pcatCode;
                            });
                            var cls = 1;
                            if (cF.length > 0) {
                                cls = cF.length;
                                for (var u = 0; u < cF.length; u++) {
                                    stUOM += '<th style="min-width: 70px;">' + cF[u].uomName + '</th>'
                                }
                                stcat += '<th colspan="' + cls + '">' + Category[c].pcatName + '</th>'
                            }
                        }
                        var str = '<th rowspan="2" style="min-width: 150px;">Order Date</th><th rowspan="2" style="min-width: 150px;">Manager Name</th><th rowspan="2" style="min-width: 150px;"> Field Force Name</th><th rowspan="2" style="min-width: 100px;">H.Q</th><th rowspan="2" style="min-width: 150px;">Route Name</th><th rowspan="2" style="min-width: 150px;">Distributor</th><th rowspan="2" style="min-width: 150px;">Joint Work</th> <th rowspan="2" style="min-width: 70px;">Calls Made</th><th rowspan="2" style="min-width: 70px;">Effective Calls</th><th rowspan="2" style="min-width: 70px;">% Pro.</th> <th rowspan="2">New Outlets</th>' + stcat + '<th rowspan="2" style="min-width: 100px;">Value Sales @ PTR</th><%--<th rowspan="2" style="min-width: 100px;">Value Sales @ NSV</th>--%><th rowspan="2" style="min-width: 100px;">Net Weight</th>';
                        $(tbl).find('thead').append('<tr>' + str + '</tr>');
                        $(tbl).find('thead').append('<tr>' + stUOM + '</tr>');
                        for (var i = 0; i < ordDt.length; i++) {

                            var cL = tcEC.filter(function (obj) {
                                return obj.sf_Code == ordDt[i].sfCode && obj.Order_Date == ordDt[i].Order_Date;
                            });
                            tC = 0;
                            eC = 0;
                            cPer = 0;
                            if (cL.length > 0) {
                                tC = cL[0].TC_Count;
                                eC = cL[0].EC_Count;
                                cc = eC / tC;
                                cPer = cc * 100;

                            }



                            str = '<td>' + ordDt[i].Order_Date + '</td><td>' + ordDt[i].sfManager + '</td><td>' + ordDt[i].sfName + '</td><td>' + ordDt[i].sfHQ + '</td><td>' + ordDt[i].RouteName + '</td><td>' + ordDt[i].Stockist + '</td><td>' + ordDt[i].WorkedWith + '</td><td>' + tC + '</td><td>' + eC + '</td> <td>' + cPer.toFixed(2) + '</td> <td>' + ordDt[i].newRTCount + '</td>';

                            for (var c = 0; c < Category.length; c++) {
                                cF = UOM.filter(function (obj) {
                                    return obj.catCode == Category[c].pcatCode;
                                });
                                var cls = 1;
                                if (cF.length > 0) {
                                    cls = cF.length;
                                    for (var u = 0; u < cF.length; u++) {

                                        uF = UData.filter(function (obj) {
                                            return (obj.sf_code == ordDt[i].sfCode && obj.Order_Date == ordDt[i].Order_Date && obj.Product_Cat_Code == Category[c].pcatCode && obj.UOM_Weight == cF[u].uomName)
                                        });
                                        var d = '';
                                        if (uF.length > 0) {
                                            d = uF[0].Quantity;
                                        }
                                        str += '<td>' + d + '</td>'
                                    }
                                }
                            }
                            str += '<td>' + ordDt[i].OrderVal_Rt + '</td><%--<td>' + ordDt[i].OrderVal_Di + '</td>--%><td>' + ordDt[i].net_weight + '</td> ';
                            $(tbl).find('tbody').append('<tr>' + str + '</tr>');
                        }

                        var totArr = [];
                        $(tbl).find('tbody tr').each(function () {
                            $(this).find('td').each(function () {
                                idx = $(this).index();
                                totArr[idx] = Number(totArr[idx] || 0) + 0;
                                if ($(this).index() > 4) {
                                    totArr[idx] = Number(totArr[idx] || 0) + Number($(this).text() || 0);
                                }
                            });

                        });
                        // console.log(totArr);
                        str = '';



                        for (var i = 0; i < totArr.length; i++) {
                            console.log(i + ' : ' + totArr[i]);
                            if (i > 4) {
                                if (i == 9) {
                                    tA = totArr[8] / totArr[7];
                                    str += '<td>' + Number(tA * 100).toFixed(2) + '</td>';
                                }
                                else if (i == totArr.length - 3 || i == totArr.length - 1 || i == totArr.length - 2) {

                                    str += '<td>' + Number(totArr[i]).toFixed(2) + '</td>';
                                }
                                else {
                                    str += '<td>' + totArr[i] + '</td>';
                                }

                            }
                        }
                        //str += '<td>' + Number(totArr[totArr.length - 4] / totArr[7]).toFixed(2) + '</td>';
                        $(tbl).find('tbody').append('<tr style="text-align:right;font-weight:bold;background-color: #496a9a;color: white;" ><td colspan="5" style="text-align:center;" >Total</td>' + str + '</tr>');

                    }

                }

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "rptFFWiseUOMReport.aspx/getTcEc",
                    data: "{'SF_Code':'" + SfCode + "', 'FDate':'" + Fyear + "', 'TDate':'" + FMonth + "', 'SubDiv':'" + SubDivCode + "'}",
                    dataType: "json",
                    success: function (data, status) {
                        tcEC = data.d;
                        // console.log(data.d);
                    },
                    error: function (jqXHR, exception) {
                        //alert(JSON.stringify(result));
                        var msg = '';
                        if (jqXHR.status === 0) {
                            msg = 'Not connect.\n Verify Network.';
                        } else if (jqXHR.status == 404) {
                            msg = 'Requested page not found. [404]';
                        } else if (jqXHR.status == 500) {
                            msg = 'Internal Server Error [500].';
                        } else if (exception === 'parsererror') {
                            msg = 'Requested JSON parse failed.';
                        } else if (exception === 'timeout') {
                            msg = 'Time out error.';
                        } else if (exception === 'abort') {
                            msg = 'Ajax request aborted.';
                        } else {
                            msg = 'Uncaught Error.\n' + jqXHR.responseText;
                        }
                        alert(msg);
                    }
                });
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "rptFFWiseUOMReport.aspx/getUOMProductData",
                    data: "{'FDate':'" + Fyear + "','TDate':'" + FMonth + "'}",
                    dataType: "json",
                    success: function (data, status) {
                        UData = data.d;
                    },
                    error: function (jqXHR, exception) {
                        //alert(JSON.stringify(result));
                        var msg = '';
                        if (jqXHR.status === 0) {
                            msg = 'Not connect.\n Verify Network.';
                        } else if (jqXHR.status == 404) {
                            msg = 'Requested page not found. [404]';
                        } else if (jqXHR.status == 500) {
                            msg = 'Internal Server Error [500].';
                        } else if (exception === 'parsererror') {
                            msg = 'Requested JSON parse failed.';
                        } else if (exception === 'timeout') {
                            msg = 'Time out error.';
                        } else if (exception === 'abort') {
                            msg = 'Ajax request aborted.';
                        } else {
                            msg = 'Uncaught Error.\n' + jqXHR.responseText;
                        }
                        alert(msg);
                    }
                });

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "rptFFWiseUOMReport.aspx/getProductCategory",
                    data: "{'SubDiv':'" + SubDivCode + "'}",
                    dataType: "json",
                    success: function (data, status) {
                        //  console.log(data.d);
                        Category = data.d;
                        getTable();
                    },
                    error: function (jqXHR, exception) {
                        //alert(JSON.stringify(result));
                        var msg = '';
                        if (jqXHR.status === 0) {
                            msg = 'Not connect.\n Verify Network.';
                        } else if (jqXHR.status == 404) {
                            msg = 'Requested page not found. [404]';
                        } else if (jqXHR.status == 500) {
                            msg = 'Internal Server Error [500].';
                        } else if (exception === 'parsererror') {
                            msg = 'Requested JSON parse failed.';
                        } else if (exception === 'timeout') {
                            msg = 'Time out error.';
                        } else if (exception === 'abort') {
                            msg = 'Ajax request aborted.';
                        } else {
                            msg = 'Uncaught Error.\n' + jqXHR.responseText;
                        }
                        alert(msg);
                    }
                });

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "rptFFWiseUOMReport.aspx/getUOMData",
                    data: "{'SubDiv':'" + SubDivCode + "'}",
                    dataType: "json",
                    success: function (data, status) {
                        //console.log(data.d);
                        UOM = data.d;
                        getTable();
                    },
                    error: function (jqXHR, exception) {
                        //alert(JSON.stringify(result));
                        var msg = '';
                        if (jqXHR.status === 0) {
                            msg = 'Not connect.\n Verify Network.';
                        } else if (jqXHR.status == 404) {
                            msg = 'Requested page not found. [404]';
                        } else if (jqXHR.status == 500) {
                            msg = 'Internal Server Error [500].';
                        } else if (exception === 'parsererror') {
                            msg = 'Requested JSON parse failed.';
                        } else if (exception === 'timeout') {
                            msg = 'Time out error.';
                        } else if (exception === 'abort') {
                            msg = 'Ajax request aborted.';
                        } else {
                            msg = 'Uncaught Error.\n' + jqXHR.responseText;
                        }
                        alert(msg);
                    }
                });


                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "rptFFWiseUOMReport.aspx/GetOrderValues",
                    data: "{'sfCode':'" + SfCode + "','FDate':'" + Fyear + "','TDate':'" + FMonth + "','SubDiv':'" + SubDivCode + "'}",
                    dataType: "json",
                    success: function (data) {
                        ordDt = data.d;
                        if (ordDt.length > 0) {
                            getTable();
                        }
                        else {
                            alert('No Records Fround..!');
                            return false;
                        }
                    },
                    error: function (jqXHR, exception) {
                        console.log(jqXHR);
                        console.log(exception);
                    }
                });

            }
            else {
                 getTable = function () {
                    var tbl = $('#FFTable');
                    $(tbl).find('thead tr').remove();
                    $(tbl).find('tbody tr').remove();
                    if (ordDt.length > 0 && Category.length > 0 && UOM.length > 0) {

                        var stcat = '';
                        var stUOM = '';
                        for (var c = 0; c < Category.length; c++) {
                            cF = UOM.filter(function (obj) {
                                return obj.catCode == Category[c].pcatCode;
                            });
                            var cls = 1;
                            if (cF.length > 0) {
                                cls = cF.length;
                                for (var u = 0; u < cF.length; u++) {
                                    stUOM += '<th style="min-width: 70px;">' + cF[u].uomName + '</th>'
                                }
                                stcat += '<th colspan="' + cls + '">' + Category[c].pcatName + '</th>'
                            }
                        }
                        var str = '<th rowspan="2" style="min-width: 150px;">Order Date</th><th rowspan="2" style="min-width: 150px;">Manager Name</th><th rowspan="2" style="min-width: 150px;"> Field Force Name</th><th rowspan="2" style="min-width: 100px;">H.Q</th><th rowspan="2" style="min-width: 150px;">Route Name</th><th rowspan="2" style="min-width: 150px;">Distributor</th><th rowspan="2" style="min-width: 150px;">Joint Work</th> <th rowspan="2" style="min-width: 70px;">Calls Made</th><th rowspan="2" style="min-width: 70px;">Effective Calls</th><th rowspan="2" style="min-width: 70px;">% Pro.</th> <th rowspan="2">New Outlets</th>' + stcat + '<th rowspan="2" style="min-width: 100px;">Value Sales @ PTR</th><%--<th rowspan="2" style="min-width: 100px;">Value Sales @ NSV</th>--%><th rowspan="2" style="min-width: 100px;">Net Weight</th>';
                        $(tbl).find('thead').append('<tr>' + str + '</tr>');
                        $(tbl).find('thead').append('<tr>' + stUOM + '</tr>');
                        for (var i = 0; i < ordDt.length; i++) {

                            var cL = tcEC.filter(function (obj) {
                                return obj.sf_Code == ordDt[i].sfCode && obj.Order_Date == ordDt[i].Order_Date;
                            });
                            tC = 0;
                            eC = 0;
                            cPer = 0;
                            if (cL.length > 0) {
                                tC = cL[0].TC_Count;
                                eC = cL[0].EC_Count;
                                cc = eC / tC;
                                cPer = cc * 100;

                            }



                            str = '<td>' + ordDt[i].Order_Date + '</td><td>' + ordDt[i].sfManager + '</td><td>' + ordDt[i].sfName + '</td><td>' + ordDt[i].sfHQ + '</td><td>' + ordDt[i].RouteName + '</td><td>' + ordDt[i].Stockist + '</td><td>' + ordDt[i].WorkedWith + '</td><td>' + tC + '</td><td>' + eC + '</td> <td>' + cPer.toFixed(2) + '</td> <td>' + ordDt[i].newRTCount + '</td>';

                            for (var c = 0; c < Category.length; c++) {
                                cF = UOM.filter(function (obj) {
                                    return obj.catCode == Category[c].pcatCode;
                                });
                                var cls = 1;
                                if (cF.length > 0) {
                                    cls = cF.length;
                                    for (var u = 0; u < cF.length; u++) {

                                        uF = UData.filter(function (obj) {
                                            return (obj.sf_code == ordDt[i].sfCode && obj.Order_Date == ordDt[i].Order_Date && obj.Product_Cat_Code == Category[c].pcatCode && obj.UOM_Weight == cF[u].uomName)
                                        });
                                        var d = '';
                                        if (uF.length > 0) {
                                            d = uF[0].Quantity;
                                        }
                                        str += '<td>' + d + '</td>'
                                    }
                                }
                            }
                            str += '<td>' + ordDt[i].OrderVal_Rt + '</td><%--<td>' + ordDt[i].OrderVal_Di + '</td>--%><td>' + ordDt[i].net_weight + '</td> ';
                            $(tbl).find('tbody').append('<tr>' + str + '</tr>');
                        }

                        var totArr = [];
                        $(tbl).find('tbody tr').each(function () {
                            $(this).find('td').each(function () {
                                idx = $(this).index();
                                totArr[idx] = Number(totArr[idx] || 0) + 0;
                                if ($(this).index() > 4) {
                                    totArr[idx] = Number(totArr[idx] || 0) + Number($(this).text() || 0);
                                }
                            });

                        });
                        // console.log(totArr);
                        str = '';



                        for (var i = 0; i < totArr.length; i++) {
                            console.log(i + ' : ' + totArr[i]);
                            if (i > 4) {
                                if (i == 9) {
                                    tA = totArr[8] / totArr[7];
                                    str += '<td>' + Number(tA * 100).toFixed(2) + '</td>';
                                }
                                else if (i == totArr.length - 3 || i == totArr.length - 1 || i == totArr.length - 2) {

                                    str += '<td>' + Number(totArr[i]).toFixed(2) + '</td>';
                                }
                                else {
                                    str += '<td>' + totArr[i] + '</td>';
                                }

                            }
                        }
                        //str += '<td>' + Number(totArr[totArr.length - 4] / totArr[7]).toFixed(2) + '</td>';
                        $(tbl).find('tbody').append('<tr style="text-align:right;font-weight:bold;background-color: #496a9a;color: white;" ><td colspan="5" style="text-align:center;" >Total</td>' + str + '</tr>');

                    }

                }

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "rptFFWiseUOMReport.aspx/getTcEc",
                    data: "{'SF_Code':'" + SfCode + "', 'FDate':'" + Fyear + "', 'TDate':'" + FMonth + "', 'SubDiv':'" + SubDivCode + "'}",
                    dataType: "json",
                    success: function (data, status) {
                        tcEC = data.d;
                        // console.log(data.d);
                    },
                    error: function (jqXHR, exception) {
                        //alert(JSON.stringify(result));
                        var msg = '';
                        if (jqXHR.status === 0) {
                            msg = 'Not connect.\n Verify Network.';
                        } else if (jqXHR.status == 404) {
                            msg = 'Requested page not found. [404]';
                        } else if (jqXHR.status == 500) {
                            msg = 'Internal Server Error [500].';
                        } else if (exception === 'parsererror') {
                            msg = 'Requested JSON parse failed.';
                        } else if (exception === 'timeout') {
                            msg = 'Time out error.';
                        } else if (exception === 'abort') {
                            msg = 'Ajax request aborted.';
                        } else {
                            msg = 'Uncaught Error.\n' + jqXHR.responseText;
                        }
                        alert(msg);
                    }
                });
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "rptFFWiseUOMReport.aspx/getUOMProductDatamgr",
                    data: "{'FDate':'" + Fyear + "','TDate':'" + FMonth + "'}",
                    dataType: "json",
                    success: function (data, status) {
                        UData = data.d;
                    },
                    error: function (jqXHR, exception) {
                        //alert(JSON.stringify(result));
                        var msg = '';
                        if (jqXHR.status === 0) {
                            msg = 'Not connect.\n Verify Network.';
                        } else if (jqXHR.status == 404) {
                            msg = 'Requested page not found. [404]';
                        } else if (jqXHR.status == 500) {
                            msg = 'Internal Server Error [500].';
                        } else if (exception === 'parsererror') {
                            msg = 'Requested JSON parse failed.';
                        } else if (exception === 'timeout') {
                            msg = 'Time out error.';
                        } else if (exception === 'abort') {
                            msg = 'Ajax request aborted.';
                        } else {
                            msg = 'Uncaught Error.\n' + jqXHR.responseText;
                        }
                        alert(msg);
                    }
                });

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "rptFFWiseUOMReport.aspx/getProductCategory",
                    data: "{'SubDiv':'" + SubDivCode + "'}",
                    dataType: "json",
                    success: function (data, status) {
                        //  console.log(data.d);
                        Category = data.d;
                        getTable();
                    },
                    error: function (jqXHR, exception) {
                        //alert(JSON.stringify(result));
                        var msg = '';
                        if (jqXHR.status === 0) {
                            msg = 'Not connect.\n Verify Network.';
                        } else if (jqXHR.status == 404) {
                            msg = 'Requested page not found. [404]';
                        } else if (jqXHR.status == 500) {
                            msg = 'Internal Server Error [500].';
                        } else if (exception === 'parsererror') {
                            msg = 'Requested JSON parse failed.';
                        } else if (exception === 'timeout') {
                            msg = 'Time out error.';
                        } else if (exception === 'abort') {
                            msg = 'Ajax request aborted.';
                        } else {
                            msg = 'Uncaught Error.\n' + jqXHR.responseText;
                        }
                        alert(msg);
                    }
                });

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "rptFFWiseUOMReport.aspx/getUOMData",
                    data: "{'SubDiv':'" + SubDivCode + "'}",
                    dataType: "json",
                    success: function (data, status) {
                        //console.log(data.d);
                        UOM = data.d;
                        getTable();
                    },
                    error: function (jqXHR, exception) {
                        //alert(JSON.stringify(result));
                        var msg = '';
                        if (jqXHR.status === 0) {
                            msg = 'Not connect.\n Verify Network.';
                        } else if (jqXHR.status == 404) {
                            msg = 'Requested page not found. [404]';
                        } else if (jqXHR.status == 500) {
                            msg = 'Internal Server Error [500].';
                        } else if (exception === 'parsererror') {
                            msg = 'Requested JSON parse failed.';
                        } else if (exception === 'timeout') {
                            msg = 'Time out error.';
                        } else if (exception === 'abort') {
                            msg = 'Ajax request aborted.';
                        } else {
                            msg = 'Uncaught Error.\n' + jqXHR.responseText;
                        }
                        alert(msg);
                    }
                });


                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "rptFFWiseUOMReport.aspx/GetOrderValuesmgr",
                    data: "{'sfCode':'" + SfCode + "','FDate':'" + Fyear + "','TDate':'" + FMonth + "','SubDiv':'" + SubDivCode + "'}",
                    dataType: "json",
                    success: function (data) {
                        ordDt = data.d;
                        if (ordDt.length > 0) {
                            getTable();
                        }
                        else {
                            alert('No Records Fround..!');
                            return false;
                        }
                    },
                    error: function (jqXHR, exception) {
                        console.log(jqXHR);
                        console.log(exception);
                    }
                });

            }

            

            $(document).on('click', '#btnExcel', function (e) {
                var a = document.createElement('a');

                var fileName = 'Test file.xls';
                var blob = new Blob([$('div[id$=exlDiv]').html()], {
                    type: "application/csv;charset=utf-8;"
                });
                document.body.appendChild(a);
                a.href = URL.createObjectURL(blob);
                a.download = 'Field_Force_Work_Analysis.xls';
                a.click();
                e.preventDefault();

            });

        });
    </script>
    <style type="text/css">
        table tr td:not(:nth-child(1)):not(:nth-child(2)):not(:nth-child(3)):not(:nth-child(4)):not(:nth-child(5)) {
            text-align: right;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <div class="container" style="max-width: 100%; width: 100%">
            <div class="row">
                <div class="col-sm-8">
                </div>
                <div class="col-sm-4" style="text-align: right">
                    <a name="btnExcel" id="btnPrint" type="button" style="padding: 0px 20px; display: none" href="#" class="btn btnPrint"></a>
                    <a name="btnExcel" id="btnPdf" type="button" style="padding: 0px 20px; display: none" href="#" class="btn btnPdf"></a>
                    <a name="btnExcel" id="btnExcel" type="button" style="padding: 0px 20px;" href="#" class="btn btnExcel"></a>
                    <a name="btnClose" id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();" class="btn btnClose"></a>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hFYear" runat="server" />
        <asp:HiddenField ID="hFMonth" runat="server" />
        <asp:HiddenField ID="hSubDiv" runat="server" />
        <asp:HiddenField ID="hsfCode" runat="server" />
        <asp:HiddenField ID="hdivCode" runat="server" />
        <asp:HiddenField ID="mgronly" runat="server" />
        <div id="exlDiv" class="container" style="max-width: 100%; width: 100%">
            <div class="row">
                <div class="col-sm-8">
                    <asp:Label ID="Label1" runat="server" Text="SOB POB Orders" Style="font-weight: bold; font-size: x-large"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-8">
                    <asp:Label ID="Label2" runat="server"></asp:Label>
                </div>
            </div>
            <table id="FFTable" class="newStly" border="1" style="width: 100%; border-collapse: collapse;">
                <thead>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>
