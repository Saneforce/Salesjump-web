<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptDistributorPerformance.aspx.cs"
    Inherits="MIS_Reports_rptDistributorPerformance" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/plain; charset=UTF-8" />
    <title></title>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <style type="text/css">
        .newStly td
        {
            padding-top: 4px;
            padding-bottom: 4px;
            padding-left: 4px;
            padding-right: 4px;
            font-size: 12px;
        }
        
        .num2
        {
            text-align: right;
        }
    </style>
	<script src="//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="../js/plugins/table2excel.js"></script>
    <script type="text/javascript">
            
            
        $(document).ready(function () {
           
           var rshq = [];
            var dRSF = [];
            var dDist = [];
            var Dtls = [];
            var CatVal = [];
            var catH = [];
            var lvl = 0;
            var notDtls = [];
            var NCatVal = [];
           
            genReport = function () {
                if (dDist.length > 0 && Dtls.length > 0 && catH.length && rshq.length > 0 && CatVal.length > 0) {

                    var nTR = 0;
                    var nutc = 0;
                    var nupc = 0;
                    var nzer = 0;
                    var ncover = 0;
                    var nbutc = 0;
                    var nbTR = 0;
                    var nNTR = 0;
                    var nNTV = 0;
                    var nTotQ = 0;
                    var nTotV = 0;
                    var nTotOPent = 0;
                    var nTotPen = 0;
                    var nCTotArr = [];
                    var nOTPArr = [];
                    var nPArr = [];
                    for (var i = 0; i < dDist.length; i++) {
                        str = '<td class="num2">' + (i + 1) + '</td>';
                        vp = rshq.filter(function (a) { return (a.sfCode == dDist[i].RSFs); });
                        if (vp.length > 0) {
                            var ssF = vp[0].sfCode;
                            var llV = Number(vp[0].level || 0);
                            var kkk = llV;
                            var ssRf = vp[0].RSFCode;
                            var nStr = '';
                            while (Number(llV) != Number(0)) {
                                nStr = '<td>' + vp[0].sfName + '</td>' + nStr;
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
                                nStr = nStr + '<td></td>';
                            }
                            str += nStr;
                        }

                        str += '<td>' + dDist[i].EmpID + '</td> <td><input type="hidden" name="sfcode" value="' + dDist[i].sfCode + '"/>' + dDist[i].sfName + '</td> <td>' + dDist[i].Desig + '</td> <td>' + dDist[i].sfHQ + '</td>';
                        str += ' <td><input type="hidden" name="distcode" value="' + dDist[i].DistCode + '"/>' + dDist[i].DistName + '</td> <td>' + dDist[i].stateName + '</td> <td class="num2">' + dDist[i].TRetailes + '</td> ';
                        var TRC = dDist[i].TRetailes;
                        var Outlets = 0, UTC = 0, UPC = 0, ZeroBilled = 0, Covered = 0, BilledUTC = 0, BilledTotalOt = 0, NewOutlets = 0, NewOutletsVal = 0, OrdVal = 0;
                        fP = Dtls.filter(function (a) { return (a.DistCode == dDist[i].DistCode && a.Sf_Code == dDist[i].sfCode); });
                        if (fP.length > 0) {
                            UTC = fP[0].TVisited;
                            UPC = fP[0].TProductive;
                            ZeroBilled = fP[0].TNonProductive;

                            Covered = (Number(UTC) / Number((TRC == 0 ? 1 : TRC)) * 100).toFixed(2);
                            BilledUTC = (Number(UPC) / Number((UTC == 0 ? 1 : UTC)) * 100).toFixed(2);
                            BilledTotalOt = (Number(UPC) / Number((TRC == 0 ? 1 : TRC)) * 100).toFixed(2);

                            NewOutlets = fP[0].NewRetailers;
                            NewOutletsVal = Number(fP[0].NewRetValues || 0).toFixed(2);
                            OrdVal = fP[0].TotValues;
                        }

                        str += '<td class="num2">' + UTC + '</td> <td class="num2">' + UPC + '</td> <td class="num2">' + ZeroBilled + '</td> <td class="num2">' + Covered + '</td> <td class="num2">' + BilledUTC + '</td> <td class="num2">' + BilledTotalOt + '</td> <td class="num2">' + NewOutlets + '</td> <td class="num2">' + NewOutletsVal + '</td> ';
                        var str2 = '';
                        var TotQTY = 0;
                        var TotVal = 0;
                        var TotOutPen = 0;
                        var TotPen = 0;
                        for (var k = 0; k < catH.length; k++) {
                            var cVal = 0;
                            var cQty = 0;
                            var cOuPen = 0;
                            var cPen = 0;
                            cv = CatVal.filter(function (a) { return (a.Prd_Cat_Code == catH[k].Prd_Cat_Code && a.SF_Code == dDist[i].sfCode && a.Dist_Code == dDist[i].DistCode); });
                            if (cv.length > 0) {
                                cVal = (Number(cv[0].Cat_Values) || 0).toFixed(2);
                                cOuPen = (Number(cv[0].Cat_RtCount) || 0);
                                cPen = (cOuPen / (UPC == 0 ? 1 : UPC) * 100).toFixed(2);
                                cQty = cv[0].Cat_Qty || 0;
                            }
                            str2 += '<td class="num2" >' + cVal + '</td> <td class="num2" >' + cOuPen + '</td> <td class="num2" >' + cPen + '%' + '</td>';
                            nCTotArr[k] = Number(nCTotArr[k] || 0) + Number(cVal);
                            nOTPArr[k] = Number(nOTPArr[k] || 0) + Number(cOuPen);
                            nPArr[k] = Number(nPArr[k] || 0) + Number(cPen);
                            TotQTY += Number(cQty);
                            TotVal += Number(cVal);
                            // TotOutPen += Number(cOuPen);
                            // TotPen += Number(cPen);

                        }
                        nTR += Number(TRC || 0);
                        nutc += Number(UTC || 0);
                        nupc += Number(UPC || 0);
                        nzer += Number(ZeroBilled || 0);
                        ncover += Number(Covered || 0);
                        nbutc += Number(BilledUTC || 0);
                        nbTR += Number(BilledTotalOt || 0);
                        nNTR += Number(NewOutlets || 0);
                        nNTV += Number(NewOutletsVal || 0);
                        nTotQ += Number(TotQTY || 0);
                        nTotV += Number(TotVal || 0);
                        // nTotOPent += Number(TotOutPen || 0);
                        // nTotPen += Number(TotPen || 0);



                        str += '<td class="num2">' + TotQTY + '</td><td class="num2">' + TotVal.toFixed(2) + '</td>' + str2;
                        $('#DistTable tbody').append('<tr>' + str + '</tr>');
                    }
                    //console.log(nCTotArr);


                    if ($('#DistTable tbody tr').length > 0) {
                        var cou = Number(lvl || 0) + Number(7); //+ Number(catH.length || 0)
                        var sFoot = '<th colspan=' + cou + '>Total</th> <th style="text-align:right">' + nTR + '</th>  <th style="text-align:right">' + nutc + '</th>  <th style="text-align:right">' + nupc + '</th>  <th style="text-align:right">' + nzer + '</th>  <th style="text-align:right">' + (nutc / nTR * 100).toFixed(2) + '</th>  <th style="text-align:right">' + (nupc / nutc * 100).toFixed(2) + '</th>';
                        sFoot += ' <th style="text-align:right">' + (nupc / nTR * 100).toFixed(2) + '</th> <th style="text-align:right">' + nNTR + '</th> <th style="text-align:right">' + Number(nNTV).toFixed(2) + '</th> <th style="text-align:right">' + nTotQ + '</th> <th style="text-align:right">' + Number(nTotV).toFixed(2) + '</th>';

                        for (var b = 0; b < catH.length; b++) {
                            sFoot += '<th style="text-align:right">' + Number(nCTotArr[b]).toFixed(2) + '</th> <th style="text-align:right">' + nOTPArr[b] + '</th> <th style="text-align:right">' + Number(nPArr[b]).toFixed(2) + '%' + '</th> ';
                        }

                        $('#DistTable tfoot').append('<tr >' + sFoot + '</tr>');
                    }
                    $("#<%=Label2.ClientID%>").hide();
                }
            }





            genReport2 = function () {
                if (notDtls.length > 0 && NCatVal.length > 0) {

                    var nTR = 0;
                    var nutc = 0;
                    var nupc = 0;
                    var nzer = 0;
                    var ncover = 0;
                    var nbutc = 0;
                    var nbTR = 0;
                    var nNTR = 0;
                    var nNTV = 0;
                    var nTotQ = 0;
                    var nTotV = 0;
                    var nTotOPent = 0;
                    var nTotPen = 0;
                    var nCTotArr = [];
                    var nOTPArr = [];
                    var nPArr = [];
                    console.log(notDtls);
                    for (var i = 0; i < notDtls.length; i++) {
                        str2 = '<td class="num2">' + (i + 1) + '</td>';


                        str2 += '<td><input type="hidden" name="sfcode" value="' + notDtls[i].Sf_Code + '"/>' + notDtls[i].Sf_Name + '</td> ';
                        str2 += ' <td><input type="hidden" name="distcode" value="' + notDtls[i].DistCode + '"/>' + notDtls[i].DistName + '</td>';
                        var TRC = 0;
                        var Outlets = 0, UTC = 0, UPC = 0, ZeroBilled = 0, Covered = 0, BilledUTC = 0, BilledTotalOt = 0, NewOutlets = 0, NewOutletsVal = 0, OrdVal = 0;

                        UTC = notDtls[i].TVisited;
                        UPC = notDtls[i].TProductive;
                        ZeroBilled = notDtls[i].TNonProductive;
                        Covered = (Number(UTC) / Number((TRC == 0 ? 1 : TRC)) * 100).toFixed(2);
                        BilledUTC = (Number(UPC) / Number((UTC == 0 ? 1 : UTC)) * 100).toFixed(2);
                        BilledTotalOt = (Number(UPC) / Number((TRC == 0 ? 1 : TRC)) * 100).toFixed(2);
                        NewOutlets = notDtls[i].NewRetailers;
                        NewOutletsVal = Number(notDtls[i].NewRetValues || 0).toFixed(2);
                        OrdVal = notDtls[i].TotValues;


                        str2 += '<td class="num2">' + UTC + '</td> <td class="num2">' + UPC + '</td> <td class="num2">' + ZeroBilled + '</td> <td class="num2">' + Covered + '</td> <td class="num2">' + BilledUTC + '</td> <td class="num2">' + BilledTotalOt + '</td> <td class="num2">' + NewOutlets + '</td> <td class="num2">' + NewOutletsVal + '</td> ';


                        var str3 = '';
                        var TotQTY = 0;
                        var TotVal = 0;
                        var TotOutPen = 0;
                        var TotPen = 0;
                        for (var k = 0; k < catH.length; k++) {
                            var cVal = 0;
                            var cQty = 0;
                            var cOuPen = 0;
                            var cPen = 0;

                            cv = NCatVal.filter(function (a) { return (a.Prd_Cat_Code == catH[k].Prd_Cat_Code && a.SF_Code == notDtls[i].Sf_Code && a.Dist_Code == notDtls[i].DistCode); });
                            //  console.log(catH[k].Prd_Cat_Code + ':' + notDtls[i].Sf_Code + ':' + notDtls[i].DistCode);
                            if (cv.length > 0) {
                                cVal = (Number(cv[0].Cat_Values) || 0).toFixed(2);
                                cOuPen = (Number(cv[0].Cat_RtCount) || 0);
                                cPen = (cOuPen / (UPC == 0 ? 1 : UPC) * 100).toFixed(2);
                                cQty = cv[0].Cat_Qty || 0;
                            }
                            str3 += '<td class="num2" >' + cVal + '</td> <td class="num2" >' + cOuPen + '</td> <td class="num2" >' + cPen + '%' + '</td>';
                            nCTotArr[k] = Number(nCTotArr[k] || 0) + Number(cVal);
                            nOTPArr[k] = Number(nOTPArr[k] || 0) + Number(cOuPen);
                            nPArr[k] = Number(nPArr[k] || 0) + Number(cPen);
                            TotQTY += Number(cQty);
                            TotVal += Number(cVal);
                        }

                        nTR += Number(TRC || 0);
                        nutc += Number(UTC || 0);
                        nupc += Number(UPC || 0);
                        nzer += Number(ZeroBilled || 0);
                        ncover += Number(Covered || 0);
                        nbutc += Number(BilledUTC || 0);
                        nbTR += Number(BilledTotalOt || 0);
                        nNTR += Number(NewOutlets || 0);
                        nNTV += Number(NewOutletsVal || 0);
                        nTotQ += Number(TotQTY || 0);
                        nTotV += Number(TotVal || 0);
                        str2 += '<td class="num2">' + TotQTY + '</td><td class="num2">' + TotVal.toFixed(2) + '</td>' + str3;
                        $('#MisTable tbody').append('<tr>' + str2 + '</tr>');
                    }


                    if ($('#MisTable tbody tr').length > 0) {
                        var cou = Number(lvl || 0) + Number(7); //+ Number(catH.length || 0)
                        var sFoot = '<th colspan=' + 3 + '>Total</th> <th style="text-align:right">' + nutc + '</th>  <th style="text-align:right">' + nupc + '</th>  <th style="text-align:right">' + nzer + '</th>  <th style="text-align:right">' + (nutc / 1 * 100).toFixed(2) + '</th>  <th style="text-align:right">' + (nupc / nutc * 100).toFixed(2) + '</th>';
                        sFoot += ' <th style="text-align:right">' + (nupc / 1 * 100).toFixed(2) + '</th> <th style="text-align:right">' + nNTR + '</th> <th style="text-align:right">' + nNTV + '</th> <th style="text-align:right">' + nTotQ + '</th> <th style="text-align:right">' + nTotV + '</th>';
                        console.log(nCTotArr.length);
                        for (var b = 0; b < catH.length; b++) {
                            sFoot += '<th style="text-align:right">' + Number(nCTotArr[b]).toFixed(2) + '</th> <th style="text-align:right">' + nOTPArr[b] + '</th> <th style="text-align:right">' + nPArr[b] + '%' + '</th> ';
                        }
                        $('#MisTable tfoot').append('<tr >' + sFoot + '</tr>');
                    }
                }
            }




            var sf_code = $("#<%=hdnSFCode.ClientID%>").val();
            var FDate = $("#<%=hdnMonth.ClientID%>").val();
            var TDate = $("#<%=hdnYear.ClientID%>").val();
            var subDiv = $("#<%=hdnSubDivCode.ClientID%>").val();


            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptDistributorPerformance.aspx/GetReportingToSF",
                data: "{'SF_Code':'" + sf_code + "', 'SubDiv':'" + subDiv + "'}",
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
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptDistributorPerformance.aspx/GetCategoryHead",
                dataType: "json",
                success: function (data) {
                    catH = data.d;
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

            $('#DistTable tr').remove();
            var str = '<th style="min-width:70px;"  rowspan="2">SLNo.</th>';
            for (jk = 0; jk < lvl; jk++) {

                if (jk > 0) {
                    str += '<th style="min-width:250px;"  rowspan="2">Level-' + jk + '</th>';
                }
                else {
                    str += '<th style="min-width:250px;"  rowspan="2">State HQ</th>';
                }
            }
            str += '<th style="min-width:150px;" rowspan="2">EMP ID</th> <th style="min-width:250px;" rowspan="2">Field Force</th>  <th style="min-width:120px;" rowspan="2">User Rank</th>  <th style="min-width:150px;" rowspan="2">SR HQ/Area</th> <th style="min-width:250px;" rowspan="2">Distributor</th> <th style="min-width:200px;" rowspan="2">State</th>';
            str += '<th style="min-width:150px;" rowspan="2">Total Outlets</th> <th style="min-width:150px;" rowspan="2">UTC</th> <th style="min-width:150px;" rowspan="2">UPC</th> <th style="min-width:150px;" rowspan="2">Zero Billed</th>';
            str += '<th style="min-width:150px;" rowspan="2">% Covered</th> <th style="min-width:150px;" rowspan="2">% Billed(wrt UTC)</th> <th style="min-width:150px;" rowspan="2">% Billed(Wrt Total Outlets)</th> <th style="min-width:150px;" rowspan="2">New Outlets</th> <th style="min-width:150px;" rowspan="2">New Outlets Value</th> <th style="min-width:150px;" rowspan="2">Total QTY</th> <th style="min-width:150px;" rowspan="2">Total value</th>';
            strcat = '';
            if (catH.length > 0) {
                for (var k = 0; k < catH.length; k++) {
                    str += '<th style="min-width:360px;" colspan="3">' + catH[k].Prd_Cat_Name + '</th>';
                    strcat += '<th style="min-width:120px;">Value</th> <th style="min-width:120px;">Outlet PEN</th> <th style="min-width:120px;">% PEN</th>';
                }
            }
            $('#DistTable thead').append('<tr class="mainhead">' + str + '</tr>');
            $('#DistTable thead').append('<tr class="secondHead">' + strcat + '</tr>');



            strcat = '';
            $('#MisTable tr').remove();
            var str2 = '<th style="min-width:70px;"  rowspan="2">SLNo.</th>';
            str2 += '<th style="min-width:250px;" rowspan="2">Field Force</th>  <th style="min-width:250px;" rowspan="2">Distributor</th>';
            str2 += '<th style="min-width:150px;" rowspan="2">UTC</th><th style="min-width:150px;" rowspan="2">UPC</th> <th style="min-width:150px;" rowspan="2">Zero Billed</th>';
            str2 += '<th style="min-width:150px;" rowspan="2">% Covered</th> <th style="min-width:150px;" rowspan="2">% Billed(wrt UTC)</th> <th style="min-width:150px;" rowspan="2">% Billed(Wrt Total Outlets)</th> <th style="min-width:150px;" rowspan="2">New Outlets</th> <th style="min-width:150px;" rowspan="2">New Outlets Value</th> <th style="min-width:150px;" rowspan="2">Total QTY</th> <th style="min-width:150px;" rowspan="2">Total value</th>';

            if (catH.length > 0) {
                for (var k = 0; k < catH.length; k++) {
                    str2 += '<th style="min-width:360px;" colspan="3">' + catH[k].Prd_Cat_Name + '</th>';
                    strcat += '<th style="min-width:120px;">Value</th> <th style="min-width:120px;">Outlet PEN</th> <th style="min-width:120px;">% PEN</th>';
                }
            }
            $('#MisTable thead').append('<tr class="mainhead">' + str2 + '</tr>');
            $('#MisTable thead').append('<tr class="secondHead">' + strcat + '</tr>');


            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptDistributorPerformance.aspx/GetDistriButers",
                data: "{'SF_Code':'" + sf_code + "', 'SubDiv':'" + subDiv + "'}",
                dataType: "json",
                success: function (data) {
                    dDist = data.d;
                    //  console.log(dDist);
                    genReport();
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


            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptDistributorPerformance.aspx/GetDistDetailsValue",
                data: "{'SF_Code':'" + sf_code + "', 'FYera':'" + FDate + "', 'FMonth':'" + TDate + "', 'SubDiv':'" + subDiv + "'}",
                dataType: "json",
                success: function (data) {
                    Dtls = data.d;
                    // console.log(Dtls);
                    genReport();
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



            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptDistributorPerformance.aspx/GetDistDetailsValueNotMatch",
                data: "{'SF_Code':'" + sf_code + "', 'FYera':'" + FDate + "', 'FMonth':'" + TDate + "', 'SubDiv':'" + subDiv + "'}",
                dataType: "json",
                success: function (data) {
                    notDtls = data.d;
                    // console.log(Dtls);
                    genReport2();
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

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptDistributorPerformance.aspx/GetCategoryValue",
                data: "{'SF_Code':'" + sf_code + "', 'FYera':'" + FDate + "', 'FMonth':'" + TDate + "', 'SubDiv':'" + subDiv + "'}",
                dataType: "json",
                success: function (data) {
                    CatVal = data.d;
                    //console.log(CatVal);
                    genReport();
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


            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptDistributorPerformance.aspx/GetCategoryValuenotmatch",
                data: "{'SF_Code':'" + sf_code + "', 'FYera':'" + FDate + "', 'FMonth':'" + TDate + "', 'SubDiv':'" + subDiv + "'}",
                dataType: "json",
                success: function (data) {
                    NCatVal = data.d;
                    // console.log(NCatVal);
                    genReport2();
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
            $(document).on('click', '#btnExcel', function (e) {
                //window.open('data:application/vnd.ms-excel,' + encodeURIComponent($('#divtable').html()));
                //                e.preventDefault();


                var data_type = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#divtable').html());
                var a = document.createElement('a');
                a.href = data_type;


                a.download = 'DistributorPerformance.xls';
                //triggering the function                
                a.click();
                //just in case, prevent default behaviour
                e.preventDefault();

                //                var dt = new Date();
                //                var day = dt.getDate();
                //                var month = dt.getMonth() + 1;
                //                var year = dt.getFullYear();
                //                var hour = dt.getHours();
                //                var mins = dt.getMinutes();
                //                var postfix = day + "." + month + "." + year + "_" + hour + "." + mins;
                //                //creating a temporary HTML link element (they support setting file names)
                //                var a = document.createElement('a');
                //                //getting data from our div that contains the HTML table
                //                var data_type = 'data:application/vnd.ms-excel;charset=utf-8';

                //                var table_html = $('#divtable')[0].outerHTML;
                //                //    table_html = table_html.replace(/ /g, '%20');
                //                table_html = table_html.replace(/<tfoot[\s\S.]*tfoot>/gmi, '');

                //                var css_html = '<style>td {border: 0.5pt solid #c0c0c0} .tRight { text-align:right} .tLeft { text-align:left} </style>';
                //                //    css_html = css_html.replace(/ /g, '%20');

                //                a.href = data_type + ',' + encodeURIComponent('<html><head>' + css_html + '</' + 'head><body>' + table_html + '</body></html>');

                //                //setting the file name
                //                a.download = 'exported_table_' + postfix + '.xls';
                //                //triggering the function                
                //                a.click();
                //                //just in case, prevent default behaviour
                //                e.preventDefault();




            });

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
        })();
        // onclick="tablesToExcel(array1, array2, 'myfile.xls')"
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="row" style="max-width: 100%; width: 98%">
        <br />
        <div class="col-sm-8">
            <asp:Label ID="lblhead1" runat="server" Style="font-weight: bold; font-size: x-large;
                padding: 0px 20px;" Text="Distributor Performance"></asp:Label>
        </div>
        <div class="col-sm-4" style="text-align: right">
            <asp:LinkButton ID="btnPrint" runat="Server" Style="padding: 0px 20px;" class="btn btnPrint"
                Visible="false" />
            <a id="btnExport" runat="Server" style="padding: 0px 20px;" class="btn btnPdf" visible="false">
            </a><a id="btnExcel" style="padding: 0px 20px;" class="btn btnExcel" />
            <%---- onclick="tableToExcel('DistTable', 'Distributor Performance')" --%>
            <a name="btnClose" id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();"
                class="btn btnClose"></a>
        </div>
    </div>
    <asp:HiddenField ID="hdnSFCode" runat="server" />
    <asp:HiddenField ID="hdnSubDivCode" runat="server" />
    <asp:HiddenField ID="hdnYear" runat="server" />
    <asp:HiddenField ID="hdnMonth" runat="server" />
    <div id="divtable"> 
   <%-- <table border="0" id='1' width="90%">
        <tr align="right">
            <td align="left" style="font-size: LARGE; font-weight: bold; font-family: Andalus;">--%>
                <asp:Label ID="Label1" runat="server" Text="Label" Style="padding-left: 15px;"></asp:Label>
           <%-- </td>
        </tr>
        <tr>
            <td width="100%">--%>
                <table id="DistTable" border="1" class="newStly" style="margin-left: 15px;">
                    <thead>
                    </thead>
                    <tbody>
                    </tbody>
                    <tfoot>
                    </tfoot>
                </table>
           <%-- </td>
        </tr>
    </table>
    <br />
    <br />
    <table border="0" id='2' width="90%">
        <tr align="right">
            <td align="left" style="font-size: LARGE; font-weight: bold; font-family: Andalus;">--%>
            <%--    <asp:Label ID="Label3" runat="server" Style="padding-left: 15px;" Text="Not Mapped Field Force - Distributor"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="100%">--%>
            <asp:Label ID="Label3" runat="server" Style="padding-left: 15px;" Text="Not Mapped Field Force - Distributor"></asp:Label>
                <table id="MisTable" border="1" class="newStly" style="margin-left: 15px;">
                    <thead>
                    </thead>
                    <tbody>
                    </tbody>
                    <tfoot>
                    </tfoot>
                </table>
           <%-- </td>
        </tr>
    </table>--%>
    <br />
    <br />
    </div>
    <asp:Label ID="Label2" runat="server" Text="Label" Style="padding-left: 20px; text-align: center;
        font-size: x-large; color: #0210ff; font-weight: bold"></asp:Label>
    </form>
</body>
</html>
