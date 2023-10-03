<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptOrderDetailedView.aspx.cs"
    Inherits="MIS_Reports_rptOrderDetailedView" %>

<!DOCTYPE html>
<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <style type="text/css">
        #lodimg {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 30%;
            width: 30%;
        }

        #OrderDtls tr td {
            text-align: left;
        }

          <%--  #OrderDtls tr td:nth-child(1), #OrderDtls tr td:nth-child(2), #OrderDtls tr td:nth-child(3), #OrderDtls tr td:nth-child(4) {
                text-align: left;
            }--%>
    </style>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var weekday = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
            var ordDtls = [];
            var rshq = [];
            var lvl = 0;
            var sf_code = $("#<%=ddlFieldForce.ClientID%>").val();
            var Fyear = $("#<%=ddlFYear.ClientID%>").val();
            var FMonth = $("#<%=ddlFMonth.ClientID%>").val();
            var SubDivCode = $("#<%=SubDivCode.ClientID%>").val();

            var left = (window.innerWidth / 4);
            var top = (window.innerHeight / 4);
            $('#lodimg').css({ 'display': 'block', top: top, left: left });



            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptOrderDetailedView.aspx/GetReportingToSF",
                dataType: "json",
                success: function (data) {
                    dRSF = data.d;
                },
                error: function (jqXHR, exception) {
                    console.log(jqXHR);
                    console.log(exception);
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


            var leng = 0;
            if (dRSF.length > 0) {
                leng = dRSF.length;
                Rf = dRSF.filter(function (a) { return (a.RSF_Code == 'admin'); });
                if (Rf.length > 0) {
                    var Rf1;
                    var strk = '';
                    leng = leng - Rf.length;

                    for (var l = 0; l < Rf.length; l++) {
                        rshq.push({ sfCode: Rf[l].Sf_Code, RSFCode: Rf[l].RSF_Code, Desig: Rf[l].Designation, sfName: Rf[l].sf_Name, level: '1' });
                        lvl = lvl > 1 ? lvl : 1;
                        Rf1 = dRSF.filter(function (a) { return (a.RSF_Code == Rf[l].Sf_Code); });
                        if (Rf1.length > 0) {
                            for (var k = 0; k < Rf1.length; k++) {
                                rshq.push({ sfCode: Rf1[k].Sf_Code, RSFCode: Rf1[k].RSF_Code, Desig: Rf1[k].Designation, sfName: Rf1[k].sf_Name, level: '2' });
                                lvl = lvl > 2 ? lvl : 2;
                                Rf2 = dRSF.filter(function (a) { return (a.RSF_Code == Rf1[k].Sf_Code); });

                                if (Rf2.length > 0) {
                                    for (var c = 0; c < Rf2.length; c++) {
                                        rshq.push({ sfCode: Rf2[c].Sf_Code, RSFCode: Rf2[c].RSF_Code, Desig: Rf2[c].Designation, sfName: Rf2[c].sf_Name, level: '3' });
                                        lvl = lvl > 3 ? lvl : 3;
                                        Rf3 = dRSF.filter(function (a) { return (a.RSF_Code == Rf2[c].Sf_Code); });

                                        if (Rf3.length > 0) {
                                            for (var m = 0; m < Rf3.length; m++) {
                                                rshq.push({ sfCode: Rf3[m].Sf_Code, RSFCode: Rf3[m].RSF_Code, Desig: Rf3[m].Designation, sfName: Rf3[m].sf_Name, level: '4' });
                                                lvl = lvl > 4 ? lvl : 4;

                                                Rf4 = dRSF.filter(function (a) { return (a.RSF_Code == Rf3[m].Sf_Code); });
                                                if (Rf4.length > 0) {
                                                    for (var n = 0; n < Rf4.length; n++) {
                                                        rshq.push({ sfCode: Rf4[n].Sf_Code, RSFCode: Rf4[n].RSF_Code, Desig: Rf4[n].Designation, sfName: Rf4[n].sf_Name, level: '5' });
                                                        lvl = lvl > 5 ? lvl : 5;
                                                        //Rf4 = dRSF.filter(function (a) { return (a.RSF_Code == Rf3[c].Sf_Code); });
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }


            //console.log(rshq);
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptOrderDetailedView.aspx/GetData",
                data: "{'SF_Code':'" + sf_code + "', 'FYera':'" + Fyear + "', 'FMonth':'" + FMonth + "', 'SubDivCode':'" + SubDivCode + "'}",
                dataType: "json",
                success: function (data) {
                    if (data.d.length > 0) {
                        ordDtls = data.d;
                        genTable();
                        //console.log(ordDtls);
                        $('#lodimg').css({ 'display': 'none' });
                    }
                    else {
                        $('#OrderDtls ').append('<tr><td colspan="6" style="color:red">No Records Not Found..!</td></tr>');
                        $('#lodimg').css({ 'display': 'none' });
                    }
                },
                error: function (jqXHR, exception) {
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

            function genTable() {
                if (ordDtls.length > 0 && rshq.length > 0) {
                    var cnt = new Date(Fyear, FMonth, 0).getDate();
                    tDays = cnt;

                    str = '<th>Order Date</th><th>Emp ID</th> <th>Field Force Name</th> ';
                    var nst = ''
                    for (jk = 0; jk < lvl; jk++) {

                        if (jk > 0) {
                            nst = '<th>Level-' + jk + '</th> ' + nst;
                        }
                        else {
                            nst = '<th>State Head</th>' + nst;
                        }
                    }
                    str += nst;
                    if ((('<%=Session["div_code"]%>') == 78) || (('<%=Session["div_code"]%>') == 87) || (('<%=Session["div_code"]%>') == 89)) {
                        str += '<th>Territory</th><th>Distributor Name</th><th>Route</th><th>Retailer</th><th>Retailer Type</th><th>Product</th><th>Conv.Fact</th><th>Quantity</th><th>Rate</th><th>Discount</th><th>Free</th><th>Base Value</th><th>Tax Amount</th><th>Net Value</th>'; // <th>DisPrice</th><th>RetPrice</th>
                    }
                    else {
                        str += '<th>Territory</th><th>Distributor Name</th><th>Route</th><th>Retailer</th><th>Retailer Type</th><th>Product</th><th>Conv.Fact</th><th>Case.Qty</th><th>Pcs.Qty</th><th>Rate</th><th>Discount</th><th>Free</th><th>Base Value</th><th>Tax Amount</th><th>Net Value</th>'; // <th>DisPrice</th><th>RetPrice</th>
                    } $('#OrderDtls').append('<tr>' + str + '</tr>');
                    //for (var i = 0; i < cnt; i++) {

                        //var d = ((i < 9) ? '0' : '') + (i + 1);
                        //var m = ((Number(FMonth) < 10) ? '0' : '') + (Number(FMonth));
                        //var dt = (d) + "/" + m + "/" + Fyear;
                        //var dname = new Date('' + FMonth + '/' + (i + 1) + '/' + Fyear + '');
                        //var dNas = weekday[dname.getDay()];

                        //dts = ordDtls.filter(function (a) {
                        //    return (a.ordDate == dt);
                        //});
                        //  var str = '<th colspan="10">Order Date : ' + dNas + ' - ' + dt + '</th>';
                        //$('#OrderDtls').append('<tr class="mainhead">' + str + '</tr>');
                    dts = ordDtls;
                        if (dts.length > 0) {
                            var sf = '';
                            var dcTqty = 0;
                            var dpTqty = 0;
                            var dTqty = 0;
                            var dTrate = 0;
                            var dTdisco = 0;
                            var dTprice = 0;
                            var dTtaxamout = 0;
                            var dTnetvalue = 0;
                            for (var j = 0; j < dts.length; j++) {

                                if (sf != dts[j].sfCode) {

                                    Dsf = dts.filter(function (a) {
                                        return (a.sfCode == dts[j].sfCode);
                                    });

                                    if (Dsf.length > 0) {
                                        var oNum = '';
                                        var scTqty = 0;
                                        var spTqty = 0;
                                        var sTqty = 0;
                                        var sTrate = 0;
                                        var sTdisco = 0;
                                        var sTprice = 0;
                                        var sTtaxamout = 0;
                                        var sTnetvalue = 0;
                                        var sno = 1;
                                        for (var l = 0; l < Dsf.length; l++) {
                                            if (oNum != Dsf[l].ordNum) {
                                                ods = Dsf.filter(function (a) {
                                                    return (a.ordNum == Dsf[l].ordNum);
                                                });

                                                if (ods.length > 0) {
                                                    // var stkH = '<th>Product Name</th><th>QTY</th><th>Rate</th><th>Discount</th><th>Price</th> <th>DisPrice</th><th>RetPrice</th>';
                                                    //$('#OrderDtls').append('<tr>' + stkH + '</tr>');
                                                    var oTqty = 0;
                                                    var ocTqty = 0;
                                                    var opTqty = 0;
                                                    var oTrate = 0;
                                                    var oTdisco = 0;
                                                    var oTprice = 0;
                                                    var oTtaxamout = 0;
                                                    var oTnetvalue = 0;
                                                    var str = '';

                                                    vp = rshq.filter(function (a) { return (a.sfCode == ods[0].sfRSF); });
                                                    if (vp.length > 0) {
                                                        var ssF = vp[0].sfCode;
                                                        var llV = Number(vp[0].level || 0);
                                                        var kkk = llV;
                                                        var ssRf = vp[0].RSFCode;
                                                        var nStr = '';
                                                        while (Number(llV) != Number(0)) {
                                                            nStr += '<td>' + vp[0].sfName + '</td>';
                                                            vp = rshq.filter(function (a) { return (a.sfCode == ssRf); });
                                                            if (vp.length > 0) {
                                                                ssF = vp[0].sfCode;
                                                                llV = vp[0].level || 0;
                                                                ssRf = vp[0].RSFCode;
                                                            }
                                                            else {
                                                                llV = 0;
                                                            }

                                                        }
                                                        for (var u = kkk; u < lvl; u++) {
                                                            nStr = '<td></td>' + nStr;
                                                        }
                                                        str += nStr;
                                                    }
                                                    else {
                                                        var nStr = '';
                                                        for (var u = 1; u < lvl; u++) {
                                                            nStr = '<td></td>' + nStr;
                                                        }
                                                        str += '<td>' + dts[j].sfName + '</td>' + nStr;
                                                    }

                                                    for (var k = 0; k < ods.length; k++) {
                                                        oTqty += Number(ods[k].QTY || 0);
                                                        ocTqty += Number(ods[k].cQTY || 0);
                                                        opTqty += Number(ods[k].pQTY || 0);
                                                        oTrate += Number(ods[k].Rate || 0);
                                                        oTdisco += Number(ods[k].discount || 0);
                                                        oTprice += Number(ods[k].Price || 0);
                                                        oTtaxamout += Number(ods[k].taxamout || 0);
                                                        oTnetvalue += Number(ods[k].netvalue || 0);
                                                        var stkD = '';
                                                        // var stkD = '<td>' + dt + ' - ' + dNas.substring(0, 3) + '<td>' + ods[0].sfEmpId + '</td><td>' + ods[0].sfName + '</td> ' + str + ' <td>' + ods[0].sfHQ + '</td><td>' + ods[0].distName + '</td><td>' + ods[0].routeName + '</td><td>' + ods[0].custName + '</td><td>' + ods[0].custType + '</td><td>' + ods[0].ordTime + '</td><td>' + ods[k].prdName + '</td><td style="text-align:right">' + ods[k].QTY + '</td><td style="text-align:right">' + Number(ods[k].Rate).toFixed(2) + '</td><td style="text-align:right">' + Number(ods[k].discount).toFixed(2) + '</td><td style="text-align:right">' + Number(ods[k].free) + '</td> <td style="text-align:right"> ' + Number(ods[k].taxamout).toFixed(2) + '</td> <td style="text-align:right">' + Number(ods[k].netvalue).toFixed(2) + '</td><td style="text-align:right">' + Number(ods[k].Price).toFixed(2) + '</td>';
                                                        if ((('<%=Session["div_code"]%>') == 78) || (('<%=Session["div_code"]%>') == 87) || (('<%=Session["div_code"]%>') == 89)) {
                                                            stkD = '<td>' + dt + '<td>' + ods[0].sfEmpId + '</td><td>' + ods[0].sfName + '</td> ' + str + ' <td>' + ods[0].sfHQ + '</td><td>' + ods[0].distName + '</td><td>' + ods[0].routeName + '</td><td>' + ods[0].custName + '</td><td>' + ods[0].custType + '</td><td>' + ods[k].prdName + '</td><td style="text-align:right">' + ods[k].CnvQty + '</td><td style="text-align:right">' + ods[k].QTY + '</td><td style="text-align:right">' + Number(ods[k].Rate).toFixed(2) + '</td><td style="text-align:right">' + Number(ods[k].discount).toFixed(2) + '</td><td style="text-align:right">' + Number(ods[k].free) + '</td><td style="text-align:right">' + Number(ods[k].netvalue).toFixed(2) + '</td> <td style="text-align:right"> ' + Number(ods[k].taxamout).toFixed(2) + '</td><td style="text-align:right">' + Number(ods[k].Price).toFixed(2) + '</td>';
                                                        }//+ ' - ' + dNas.substring(0, 3)
                                                        else {
                                                            stkD = '<td>' + ods[0].ordDate+'</td><td>' + ods[0].sfEmpId + '</td><td>' + ods[0].sfName + '</td> ' + str + ' <td>' + ods[0].sfHQ + '</td><td>' + ods[0].distName + '</td><td>' + ods[0].routeName + '</td><td>' + ods[0].custName + '</td><td>' + ods[0].custType + '</td><td>' + ods[k].prdName + '</td><td style="text-align:right">' + ods[k].CnvQty + '</td><td style="text-align:right">' + ods[k].cQTY + '</td><td style="text-align:right">' + ods[k].pQTY + '</td><td style="text-align:right">' + Number(ods[k].Rate).toFixed(2) + '</td><td style="text-align:right">' + Number(ods[k].discount).toFixed(2) + '</td><td style="text-align:right">' + Number(ods[k].free) + '</td><td style="text-align:right">' + Number(ods[k].netvalue).toFixed(2) + '</td> <td style="text-align:right"> ' + Number(ods[k].taxamout).toFixed(2) + '</td><td style="text-align:right">' + Number(ods[k].Price).toFixed(2) + '</td>';
                                                        } 		//'<td>' + dt + ' - ' + dNas.substring(0, 3) + 										  //<td>' + Number(ods[k].DistributorPrice).toFixed(2) + '</td><td>' + Number(ods[k].RetailerPrice).toFixed(2) + '</td>'
                                                        $('#OrderDtls').append('<tr>' + stkD + '</tr>');
                                                    }
                                                    scTqty += ocTqty;
                                                    spTqty += opTqty;
                                                    sTqty += oTqty;
                                                    sTrate += oTrate;
                                                    sTdisco += oTdisco;
                                                    sTprice += oTprice;
                                                    sTtaxamout += oTtaxamout;
                                                    sTnetvalue += oTnetvalue;
                                                    var strTot = '';
                                                    if ((('<%=Session["div_code"]%>') == 78) || (('<%=Session["div_code"]%>') == 87) || (('<%=Session["div_code"]%>') == 89)) {
                                                        strTot = '<td colspan="' + (10 + lvl) + '">Order (' + (sno) + ') Total </td><td style="text-align:right">' + oTqty + '</td><td></td><td style="text-align:right">' + oTdisco.toFixed(2) + '</td><td></td><td style="text-align:right">' + oTnetvalue.toFixed(2) + '</td><td style="text-align:right">' + oTtaxamout.toFixed(2) + '</td><td style="text-align:right">' + oTprice.toFixed(2) + '</td>';
                                                    }
                                                    else {
                                                        strTot = '<td colspan="' + (10 + lvl) + '">Order (' + (sno) + ') Total </td><td style="text-align:right">' + ocTqty + '</td><td style="text-align:right">' + opTqty + '</td><td></td><td style="text-align:right">' + oTdisco.toFixed(2) + '</td><td></td><td style="text-align:right">' + oTnetvalue.toFixed(2) + '</td><td style="text-align:right">' + oTtaxamout.toFixed(2) + '</td><td style="text-align:right">' + oTprice.toFixed(2) + '</td>';
                                                    } $('#OrderDtls').append('<tr style="background-color: #cfdaf7;">' + strTot + '</tr>');
                                                    sno = sno + 1;
                                                }
                                                oNum = Dsf[l].ordNum;

                                            }

                                        }
                                        dcTqty += scTqty;
                                        dpTqty += spTqty;
                                        dTqty += sTqty;
                                        dTrate += sTrate;
                                        dTdisco += sTdisco;
                                        dTprice += sTprice;
                                        dTtaxamout += sTtaxamout;
                                        dTnetvalue += sTnetvalue;
                                        var FFTot = '';
                                        if ((('<%=Session["div_code"]%>') == 78) || (('<%=Session["div_code"]%>') == 87) || (('<%=Session["div_code"]%>') == 89)) {
                                             FFTot = '<td colspan="' + (10 + lvl) + '" > (' + dts[j].sfName + ') Total </td><td style="text-align:right">' + sTqty + '</td><td></td><td style="text-align:right">' + sTdisco.toFixed(2) + '</td><td></td><td style="text-align:right">' + sTnetvalue.toFixed(2) + '</td><td style="text-align:right">' + sTtaxamout.toFixed(2) + '</td><td style="text-align:right">' + sTprice.toFixed(2) + '</td>';
                                        }
                                         else {
                                             FFTot = '<td colspan="' + (10 + lvl) + '" > (' + dts[j].sfName + ') Total </td><td style="text-align:right">' + scTqty + '</td><td style="text-align:right">' + spTqty + '</td><td></td><td style="text-align:right">' + sTdisco.toFixed(2) + '</td><td></td><td style="text-align:right">' + sTnetvalue.toFixed(2) + '</td><td style="text-align:right">' + sTtaxamout.toFixed(2) + '</td><td style="text-align:right">' + sTprice.toFixed(2) + '</td>';
                                        } $('#OrderDtls').append('<tr style="background-color: #81a6f3;">' + FFTot + '</tr>');

                                    }
                                    sf = dts[j].sfCode;
                                }
                            }
                            if ((('<%=Session["div_code"]%>') == 78) || (('<%=Session["div_code"]%>') == 87) || (('<%=Session["div_code"]%>') == 89)) {
                                var dayTot = '<th colspan="' + (10 + lvl) + '" style="text-align:left"> Total </th><th style="text-align:right">' + dTqty + '</th><th></th><th style="text-align:right">' + dTdisco.toFixed(2) + '</th>><th></th><th style="text-align:right">' + dTnetvalue.toFixed(2) + '</th><th style="text-align:right">' + dTtaxamout.toFixed(2) + '</th><th style="text-align:right">' + dTprice.toFixed(2) + '</th>';
                            }
                            else {
                                var dayTot = '<th colspan="' + (10 + lvl) + '" style="text-align:left"> Total </th><th style="text-align:right">' + dcTqty + '</th><th style="text-align:right">' + dpTqty + '</th><th></th><th style="text-align:right">' + dTdisco.toFixed(2) + '</th>><th></th><th style="text-align:right">' + dTnetvalue.toFixed(2) + '</th><th style="text-align:right">' + dTtaxamout.toFixed(2) + '</th><th style="text-align:right">' + dTprice.toFixed(2) + '</th>';
                            } $('#OrderDtls').append('<tr style="background-color: #81a6f3;">' + dayTot + '</tr>');//(' + dt + ')
                        }

                        //$('#OrderDtls').append('<tr style="background-color: #fff;"><td colspan="9" style="border: none;">&nbsp;</td></tr>');
                    //}
                }
            }

            $(document).on('click', "#btnExcel", function (e) {

                //creating a temporary HTML link element (they support setting file names)
                //                var a = document.createElement('a');
                //                //getting data from our div that contains the HTML table
                //                var data_type = 'data:application/vnd.ms-excel';
                //                var table_div = document.getElementById('content');
                //                var table_html = table_div.outerHTML.replace(/ /g, '%20');
                //                a.href = data_type + ', ' + table_html;
                //                //setting the file name
                //                a.download = 'OrderDeatils_' + postfix + '.xls';
                //                //triggering the function
                //                a.click();
                //                //just in case, prevent default behaviour
                //                e.preventDefault();
                var dt = new Date();
                var day = dt.getDate();
                var month = dt.getMonth() + 1;
                var year = dt.getFullYear();
                var postfix = day + "_" + month + "_" + year;
                var a = document.createElement('a');
                var blob = new Blob([$('div[id$=content]').html()], {
                    type: "application/csv;charset=utf-8;"
                });
                document.body.appendChild(a);
                a.href = URL.createObjectURL(blob);
                a.download = 'OrderDeatils_' + postfix + '.xls';
                a.click();
                e.preventDefault();
            });

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="ddlFieldForce" runat="server" />
    <asp:HiddenField ID="ddlFYear" runat="server" />
    <asp:HiddenField ID="ddlFMonth" runat="server" />
    <asp:HiddenField ID="SubDivCode" runat="server" />
    <br />
    <div class="container" style="max-width: 100%; width: 90%">
        <div class="row">
            <div class="col-md-8">
                <asp:Label ID="Label2" runat="server" Text="Order wise Report" Style="font-weight: bold;
                    font-size: x-large"></asp:Label>
            </div>
            <div class="col-md-4" style="text-align: right">
                <a name="btnExcel" id="btnPrint" type="button" style="padding: 0px 20px; display: none"
                    href="#" class="btn btnPrint"></a><a name="btnExcel" id="btnPdf" type="button" style="padding: 0px 20px;
                        display: none" href="#" class="btn btnPdf"></a><a name="btnExcel" id="btnExcel" type="button"
                            style="padding: 0px 20px;" href="#" class="btn btnExcel"></a><a name="btnClose" id="btnClose"
                                type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();"
                                class="btn btnClose"></a>
            </div>
        </div>
    </div>
    <div class="container" style="max-width: 100%; width: 90%">
        <div id="content">
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            <br />
            <table id="OrderDtls" class="newStly">
            </table>
            <br />
            <br />
            <table id="PrdDtls" class="newStly">
            </table>
        </div>
    </div>
    <img id="lodimg" src="../Images/loadingN.gif" style="display: none" alt="" />
    </form>
</body>
</html>
