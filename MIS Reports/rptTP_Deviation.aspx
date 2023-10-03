<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptTP_Deviation.aspx.cs"
    Inherits="MIS_Reports_rptTP_Deviation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TP - Deviation</title>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script type="text/javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
        $(document).ready(function () {


            var sfList = [];
            var SFCode = $("#<%=hSfCode.ClientID%>").val();
            var Fyear1 = $("#<%=hFyear.ClientID%>").val();
            var FMonth1 = $("#<%=hFMonth.ClientID%>").val();
            var month = $("#<%=Month.ClientID%>").val();
            var year = $("#<%=YEAR.ClientID%>").val();
		   var mode = $("#<%=hSfmode.ClientID%>").val();
            var tdl = new Date(FMonth1);
            var ydl = new Date(Fyear1);
            var FMonth = ydl.getMonth() + 1;
            var Fyear = ydl.getFullYear();
            var date1=Fyear1.split('/');              
            var TDay = date1[2] + '/' + date1[0] + '/' + date1[1];
            var date2=FMonth1.split('/');              
            var FDay = date2[2] + '/' + date2[0] + '/' + date2[1];
            var left = (window.innerWidth / 4);
            var top = (window.innerHeight / 4);
            //$('#lodimg').css({ 'display': '', top: top, left: left });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptTP_Deviation.aspx/GetSFList",
                data: "{'SF_Code':'" + SFCode + "'}",
                dataType: "json",
                success: function (data) {
                     if (mode == "0") {
                            sfList = data.d;
                        }
                        else {
                            sfList = data.d.filter(function (a) {
                                return (a.sfCode == SFCode);
                            });
                        }
                },
                error: function (err) {
                    console.log(err);
                    $('#lodimg').css({ 'display': 'none' });
                }
            });

            if (sfList.length > 0) {
                for (var mRow = 0; mRow < sfList.length; mRow++) {
                    $('#content').append('Field Force : <b>' + sfList[mRow].sfname + '</b>');
                    $('#content').append('<p>&nbsp</p>');

                    SFCode = sfList[mRow].sfCode;
                    var derDt = [];
                    var tpDt = [];
                    var ordTCEC = [];
                    var tDist = [];
                    var priDt = [];
                    var weekday = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
                    var monthNames = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "rptTP_Deviation.aspx/GetDataTCEC",
                        data: "{'SF_Code':'" + SFCode + "', 'FYera':'" + Fyear + "', 'FMonth':'" + FMonth + "'}",
                        dataType: "json",
                        success: function (data) {
                            ordTCEC = data.d;

                        },
                        error: function (err) {
                            console.log(err);
                        }

                    });

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "rptTP_Deviation.aspx/GetDataDistributo",
                        data: "{'SF_Code':'" + SFCode + "', 'FYera':'" + Fyear + "', 'FMonth':'" + FMonth + "'}",
                        dataType: "json",
                        success: function (data) {
                            tDist = data.d;

                        },

                        error: function (jqXHR, exception) {
                            // alert(JSON.stringify(result));
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
                        url: "rptTP_Deviation.aspx/GetDataDCR",
                        data: "{'SF_Code':'" + SFCode + "', 'FYera':'" + Fyear + "', 'FMonth':'" + FMonth + "'}",
                        dataType: "json",
                        success: function (data) {
                            derDt = data.d;

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
                        url: "rptTP_Deviation.aspx/GetDataTP",
                        data: "{'SF_Code':'" + SFCode + "', 'FYera':'" + Fyear + "', 'FMonth':'" + FMonth + "'}",
                        dataType: "json",
                        success: function (data) {
                            tpDt = data.d;


                        },
                        error: function (jqXHR, exception) {
                            //   alert(JSON.stringify(result));
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
                        url: "rptTP_Deviation.aspx/GetPrimaryVal",
                        data: "{'SF_Code':'" + SFCode + "', 'FYera':'" + Fyear + "', 'FMonth':'" + FMonth + "'}",
                        dataType: "json",
                        success: function (data) {
                            priDt = data.d;
                            // console.log(data.d);
                            //  genTable();

                        },
                        error: function (jqXHR, exception) {
                            // alert(JSON.stringify(result));
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





                    var mainRow = '<table  width="100%" class="newStly tpTable" style="border-collapse: collapse;"><thead><tr><th>DATE</th><th>DAY</th><th>TOUR PLAN</th><th>TP DISTRIBUTOR</th><th>DP DISTRIBUTOR</th><th>WORK WITH</th><th>AREA WORKED</th><th>STATION</th>';
                    mainRow += '<th>DEVIATION YES/NO</th><th>DTS</th><th>SOB</th><th>SEMI WHOLESELLER</th><th>SEMI WHOLESELLER VALUE</th><th>RETAILER</th><th>RETAILER VALUE</th><th>DOCTOR</th><th>DOCTOR VALUE</th><th>NURSHING HOME</th>';
                    mainRow += '<th>NURSHING HOME VAULE</th><th>PAYMENT COLLECTION</th><th>TC</th><th>PC</th> <th>Phone Order</th>   <th>VALUES</th><th>REMARKS</th></tr></thead>';



                    var arrDv = [];
                    var arrTC = [];
                    var arrEC = [];
                    var arrOrdVal = [];
                    var arrMett = [];
                    var arrLeave = [];
                    var arrHol = [];
                    var arrSunWork = [];
                    var arrWorkDay = [];
                    var arrMonthDays = [];
                    var arrpriQty = [];
                    var arrpriVal = [];


                    var arrsemQty = [];
                    var arrsemVal = [];

                    var arrretQty = [];
                    var arrretVal = [];

                    var arrdocQty = [];
                    var arrdocVal = [];

                    var arrndhQty = [];
                    var arrndhVal = [];

                    var semCode = 'All';
                    var nurCode = 'All';
                    var doctCode = 'All';
                    var retCode = 'All';

                    //console.log(priDt);
                    for (kk = 1; kk <= FMonth; kk++) {
                        var tdvCnt = 0;
                        var ttccnt = 0;
                        var teccnt = 0;
                        var tLeave = 0;
                        var tOrdVal = 0;
                        var tMetting = 0;
                        var tHoli = 0;
                        var tSunWork = 0;
                        var tWorkDay = 0;
                        var tDays = 0;
                        var tpriQty = 0;
                        var tpriVal = 0;
                        var tpriEC = 0;


                        var tsemiCls = 0;
                        var tsemiVal = 0;
                        var trtCal = 0;
                        var trtVal = 0;
                        var tdrCal = 0;
                        var tdrVal = 0;
                        var tnhCal = 0;
                        var tnhVal = 0;
                        var tPhcall = 0;

                        var cnt = new Date(Fyear, kk, 0).getDate();
                        tDays = cnt;


                        for (var i = 0; i < cnt; i++) {

                            var d = ((i < 9) ? '0' : '') + (i + 1);
                            var m = ((Number(kk) < 10) ? '0' : '') + (Number(kk));
                            var dt = Fyear + "/" + m + "/" + (d);

                            tP = tpDt.filter(function (a) {
                                return (a.tpDate == dt);
                            });

                            var dist = '';
                            var tfWork = '';
                            var chCon = true;

                            if (tP.length > 0) {
                                tfWork = tP[0].WorkType_Name;
                                dist = tP[0].Distributor;
                                if (tfWork.toLowerCase() == ("Weekly Off").toLowerCase() || tfWork.toLowerCase() == ("Holiday").toLowerCase()) {
                                    chCon = false;

                                }
                                if (tfWork.toLowerCase() == ("Field Work").toLowerCase()) {
                                    tfWork = '';
                                    for (var k = 0; k < tP.length; k++) {
                                        tfWork += tP[0].Plan_Name + ',';
                                    }
                                }
                            }

                            dcP = derDt.filter(function (a) {
                                return (a.tpDate == dt);
                            });
                            var wth = '';
                            var dfWork = '';
                            var addrs = '';
                            var remark = '';
                            if (dcP.length > 0) {
                                dfWork = dcP[0].WorkType_Name;
                                wth = dcP[0].workedwith;
                                tWorkDay++;
                                if (dfWork.toLowerCase() == ("Holiday").toLowerCase()) {
                                    tHoli++;
                                }
                                if (dfWork.toLowerCase() == ("Meeting").toLowerCase()) {
                                    tMetting++;
                                }
                                if (dfWork.toLowerCase() == ("Leave").toLowerCase()) {
                                    tLeave++;
                                }

                                if (dfWork.toLowerCase() == ("Field Work").toLowerCase()) {
                                    dfWork = '';
                                    for (var k = 0; k < dcP.length; k++) {
                                        if (dfWork.indexOf(dcP[k].Plan_Name) == -1) {
                                            dfWork += dcP[k].Plan_Name + ', ';
                                        }
                                    }
                                }
                            }
                            tfWork = tfWork.replace(/,\s*$/, "");

                            // dcrDist = dcrDist.replace(/,\s*$/, "");

                            var tpArr = tfWork.split(',');
                            var findStr = false;
                            if (tpArr.length > 0) {
                                findStr = (dfWork.indexOf(tpArr) > -1);
                            }
                            var conStr = '';
                            if (chCon) {
                                if (!findStr) {
                                    conStr = 'Yes';
                                    tdvCnt++;
                                }
                                else {
                                    conStr = 'No';
                                }
                            }


                            var stc = '';
                            var sec = '';
                            var sval = '';

                            var semiCls = '';
                            var semiVal = '';

                            var rtCal = '';
                            var rtVal = '';

                            var drCal = '';
                            var drVal = '';

                            var nhCal = '';
                            var nhVal = '';

                            var dcrDist = '';
                            var dname = new Date('' + FMonth + '/' + (i + 1) + '/' + Fyear + '');
                            var dNas = weekday[dname.getDay()].substring(0, 3);

                            pD = priDt.filter(function (a) {
                                return (a.Activity_Date == dt);
                            });

                            var priVal = '';
                            var priQty = '';
                            var priEC = '';

                            if (pD.length > 0) {
                                for (var pk = 0; pk < pD.length; pk++) {

                                    if (dcrDist.indexOf(pD[pk].WorkType_Name) == -1) {

                                        dcrDist += pD[pk].WorkType_Name + ',';
                                    }

                                    if (wth.indexOf(pD[pk].SplName) == -1) {
                                        wth += pD[pk].SplName + ', ';
                                    }

                                    if (dfWork.indexOf(pD[pk].route) == -1) {
                                        dfWork += pD[pk].route + ', ';
                                    }


                                    priVal = Number(pD[pk].Order_Value || 0) + (priVal || 0);
                                    priQty = (pk + 1);
                                    if (Number(priVal) > 0) {
                                        priEC = Number(priEC == '' ? 0 : priEC) + Number(1);
                                    }
                                }
                            }
                            //  console.log(ordTCEC);
                            dfWork = dfWork.replace(/,\s*$/, "");
                            te = ordTCEC.filter(function (a) {
                                return (a.Activity_Date == dt);
                            });

                            var phoneOrders = 0;

                            if (te.length > 0) {

                                for (var h = 0; h < te.length; h++) {

                                    phoneOrders += Number(te[h].phoneOrder);
                                    if (addrs.indexOf(te[h].Address1) == -1) {
                                        addrs += te[h].Address1 + ',';
                                    }

                                    if (remark.indexOf(te[h].Remarks1) == -1) {
                                        remark += te[h].Remarks1 + ',';
                                    }

                                    if (dcrDist.indexOf(te[h].Distributor) == -1) {
                                        dcrDist += te[h].Distributor + ',';
                                    }

                                    if (wth.indexOf(te[h].WorkType_Name) == -1) {
                                        wth += te[h].WorkType_Name + ', ';
                                    }
                                    if (te[h].SplName == 'SEMI WHOLESELLER') {
                                        semCode = te[h].SplCode;
                                        semiCls = Number(te[h].TC || 0) + (semiCls || 0);
                                        semiVal = Number(te[h].Order_Value || 0) + (semiVal || 0);
                                    }
                                    else if (te[h].SplName == 'DOCTOR') {
                                        drCal = Number(te[h].TC || 0) + (drCal || 0);
                                        drVal = Number(te[h].Order_Value || 0) + (drVal || 0);
                                        doctCode = te[h].SplCode;
                                    }
                                    else if (te[h].SplName == 'NURSHING HOME') {
                                        nhCal = Number(te[h].TC || 0) + (nhCal || 0);
                                        nhVal = Number(te[h].Order_Value || 0) + (nhVal || 0);
                                        nurCode = te[h].SplCode;
                                    }
                                    else {
                                        rtCal = Number(te[h].TC || 0) + (rtCal || 0);
                                        rtVal = Number(te[h].Order_Value || 0) + (rtVal || 0);
                                    }

                                    stc = Number(te[h].TC || 0) + (stc || 0);
                                    sec = Number(te[h].EC || 0) + (sec || 0);
                                    sval = Number(te[h].Order_Value || 0) + (sval || 0);

                                    ttccnt += Number(te[h].TC || 0);
                                    teccnt += Number(te[h].EC || 0.);
                                    tOrdVal += Number(te[h].Order_Value || 0);


                                    if (dNas == 'Sun') {
                                        tSunWork++;
                                    }
                                }
                            }
                            if (dt >= FDay && dt <= TDay) {

                                str = '<tr><td>' + dt + '</td><td>' + dNas + '</td><td>' + tfWork + '</td><td>' + dist + '</td><td>' + dcrDist + '</td><td>' + wth + '</td><td>' + dfWork + '</td><td>' + addrs + '</td> <td>' + conStr + '</td> <td><a class="btnview"  rType="All" SFCD="' + SFCode + '" oType="Pri" Day1="' + dt + '">' + priQty + '</td><td>' + (priVal == '' ? '' : Number(priVal).toFixed(2)) + '</td><td><a class="btnview" SFCD="' + SFCode + '" rType="' + semCode + '" oType="Sec" Day1="' + dt + '">' + semiCls + '</a></td> <td>' + (semiVal == '' ? '' : Number(semiVal).toFixed(2)) + '</td> <td><a class="btnview"  SFCD="' + SFCode + '" rType="' + retCode + '" oType="Sec" Day1="' + dt + '">' + rtCal + '</a></td> <td>' + (rtVal == '' ? '' : Number(rtVal).toFixed(2)) + '</td> ';
                                str += '<td><a class="btnview"  SFCD="' + SFCode + '"  rType="' + doctCode + '" oType="Sec" Day1="' + dt + '">' + drCal + '</a></td><td>' + (drVal == '' ? '' : Number(drVal).toFixed(2)) + '</td><td><a class="btnview"  SFCD="' + SFCode + '"  rType="' + nurCode + '" oType="Sec" Day1="' + dt + '">' + nhCal + '</a></td><td>' + (nhVal == '' ? '' : Number(nhVal).toFixed(2)) + '</td><td></td><td><a class="dayret"  SFCD="' + SFCode + '"  rType="All"  phType="1" oType="Sec" Day1="' + dt + '">' + Number(Number(stc) + Number(priQty)) + '</a></td><td>' + Number(Number(sec) + Number(priEC)) + '</td><td><a class="btnview"  SFCD="' + SFCode + '"  rType="All"  phType="1" oType="Sec" Day1="' + dt + '">' + phoneOrders + '</a></td>  <td>' + Number(Number(sval || 0) + Number(priVal || 0)).toFixed(2) + '</td><td>' + remark + '</td></tr>';

                                mainRow += str;
                                tPhcall += Number(phoneOrders);
                            tsemiCls += Number(semiCls);
                            tsemiVal += Number(semiVal);

                            trtCal += Number(rtCal);
                            trtVal += Number(rtVal);

                            tdrCal += Number(drCal);
                            tdrVal += Number(drVal);

                            tnhCal += Number(nhCal);
                            tnhVal += Number(nhVal);
                            tpriQty += Number(priQty);
                            tpriVal += Number(priVal);
                            tpriEC += Number(priEC);
                            }
                            
                        }
                        if (kk == FMonth) {
                            str = '<tr><td colspan="9" style="font-weight: bold;"> Total </td><td style="text-align:right;font-weight: bold;">' + tpriQty + '</td><td style="text-align:right;font-weight: bold;">' + tpriVal.toFixed(2) + '</td><td style="text-align:right;font-weight: bold;">' + tsemiCls + '</td> ';
                            str += '<td style="text-align:right;font-weight: bold;">' + tsemiVal.toFixed(2) + '</td><td style="text-align:right;font-weight: bold;">' + trtCal + '</td><td style="text-align:right;font-weight: bold;">' + trtVal.toFixed(2) + '</td><td style="text-align:right;font-weight: bold;">' + tdrCal + '</td><td style="text-align:right;font-weight: bold;">' + tdrVal.toFixed(2) + '</td><td style="text-align:right;font-weight: bold;">' + tnhCal + '</td><td style="text-align:right;font-weight: bold;">' + tnhVal.toFixed(2) + '</td><td></td><td style="text-align:right;font-weight: bold;">' + Number(Number(ttccnt || 0) + (tpriQty || 0)) + '</td><td style="text-align:right;font-weight: bold;">' + Number(Number(teccnt || 0) + (tpriEC || 0)) + '</td><td> ' + tPhcall + '</td><td style="text-align:right;font-weight: bold;">' + Number((tOrdVal || 0) + (tpriVal || 0)).toFixed(2) + '</td><td></td></table>';

                            mainRow += str;
                        }

                        arrDv.push(tdvCnt);
                        arrTC.push(ttccnt + tpriQty);
                        arrEC.push(teccnt + tpriEC);
                        arrOrdVal.push((tOrdVal + tpriVal).toFixed(2));
                        arrLeave.push(tLeave);
                        arrMett.push(tMetting);
                        arrHol.push(tHoli);
                        arrSunWork.push(tSunWork);
                        arrWorkDay.push(tWorkDay);
                        arrMonthDays.push(tDays);

                        arrpriQty.push(tpriQty);
                        arrpriVal.push(tpriVal.toFixed(2));


                        arrsemQty.push(tsemiCls);
                        arrsemVal.push(tsemiVal.toFixed(2));
                        arrretQty.push(trtCal);
                        arrretVal.push(trtVal.toFixed(2));
                        arrdocQty.push(tdrCal);
                        arrdocVal.push(tdrVal.toFixed(2));
                        arrndhQty.push(tnhCal);
                        arrndhVal.push(tnhVal.toFixed(2));



                    }

                    $('#content').append(mainRow);

                    var sumRow = '<table width="100%" class="newStly" style="border-collapse: collapse;">';

                    var str = '<th>Month</th>';
                    var strDevi = '<td>Tour Deviation</td>';
                    var strTC = '<td>TC</td>';
                    var strEC = '<td>PC</td>';
                    var strOrdVal = '<td>Order Value</td>';
                    var strLeave = '<td>Leave</td>';
                    var strMetting = '<td>Meeting</td>';
                    var strHoli = '<td>Holiday</td>';
                    var strSunWork = '<td>Sunday Work</td>';
                    var strNoofWork = '<td>No. Of. Day  Work</td>';
                    var strtotDays = '<td>Month Days</td>';
                    var strCalAvg = '<td>Call Avg.</td>';

                    var strDisCnt = '<td>DTS</td>';
                    var strDisOrder = '<td>SOB</td>';
                    var SemiWholesaler = '<td>Semi Wholesaler</td>';
                    var SemiWholesalerVal = '<td>Semi Wholesaler Value</td>';
                    var RetailerMet = '<td>Retailer </td>';
                    var RetailerMetVal = '<td>Retailer Value</td>';
                    var Pob = '<td>Doctor</td>';
                    var PobVal = '<td>Doctor Value</td>';
                    var NursingHomeMet = '<td>Nursing Home</td>';
                    var NursingHomeMetVal = '<td>Nursing Home Value</td>';



                    for (var i = 0; i < monthNames.length; i++) {
                        str += '<th>' + (monthNames[i]) + '</th>';
                        strDevi += '<td>' + (arrDv[i] || '') + '</td>';
                        strTC += '<td><a class="totret" SFCD="' + SFCode + '" MNCD="' + monthNames[i] + '" YRCD="' + year + '">' + (arrTC[i] || '') + '</a></td>';
                        strEC += '<td>' + (arrEC[i] || '') + '</td>';
                        strOrdVal += '<td>' + (arrOrdVal[i] || '') + '</td>';
                        strLeave += '<td>' + (arrLeave[i] || '') + '</td>';
                        strMetting += '<td>' + (arrMett[i] || '') + '</td>';
                        strHoli += '<td>' + (arrHol[i] || '') + '</td>';
                        strSunWork += '<td>' + (arrSunWork[i] || '') + '</td>';
                        strNoofWork += '<td>' + (arrWorkDay[i] || '') + '</td>';
                        strtotDays += '<td>' + (arrMonthDays[i] || '') + '</td>';
                        SemiWholesaler += '<td>' + (arrsemQty[i] || '') + '</td>';
                        SemiWholesalerVal += '<td>' + (arrsemVal[i] || '') + '</td>';
                        RetailerMet += '<td>' + (arrretQty[i] || '') + '</td>';
                        RetailerMetVal += '<td>' + (arrretVal[i] || '') + '</td>';
                        Pob += '<td>' + (arrdocQty[i] || '') + '</td>';
                        PobVal += '<td>' + (arrdocVal[i] || '') + '</td>';
                        NursingHomeMet += '<td>' + (arrndhQty[i] || '') + '</td>';
                        NursingHomeMetVal += '<td>' + (arrndhVal[i] || '') + '</td>';
                        var toT = (arrTC[i] || 0);
                        var toW = (arrWorkDay[i] || 1);
                        var tCal = (toT / toW);
                        strCalAvg += '<td>' + tCal.toFixed(2) + '</td>';
                        strDisCnt += '<td>' + (arrpriQty[i] || '') + '</td>';
                        strDisOrder += '<td>' + (arrpriVal[i] || '') + '</td>';

                    }
                    sumRow += '<tr>' + str + '</tr>';
                    sumRow += '<tr>' + strDisCnt + '</tr>';
                    sumRow += '<tr>' + strDisOrder + '</tr>';
                    sumRow += '<tr>' + SemiWholesaler + '</tr>';
                    sumRow += '<tr>' + SemiWholesalerVal + '</tr>';
                    sumRow += '<tr>' + RetailerMet + '</tr>';
                    sumRow += '<tr>' + RetailerMetVal + '</tr>';
                    sumRow += '<tr>' + Pob + '</tr>';
                    sumRow += '<tr>' + PobVal + '</tr>';
                    sumRow += '<tr>' + NursingHomeMet + '</tr>';
                    sumRow += '<tr>' + NursingHomeMetVal + '</tr>';
                    sumRow += '<tr>' + strDevi + '</tr>';
                    sumRow += '<tr>' + strLeave + '</tr>';
                    sumRow += '<tr>' + strMetting + '</tr>';
                    sumRow += '<tr>' + strHoli + '</tr>';
                    sumRow += '<tr>' + strTC + '</tr>';
                    sumRow += '<tr>' + strEC + '</tr>';
                    sumRow += '<tr>' + strOrdVal + '</tr>';
                    sumRow += '<tr>' + strSunWork + '</tr>';
                    sumRow += '<tr>' + strNoofWork + '</tr>';
                    sumRow += '<tr>' + strtotDays + '</tr>';
                    sumRow += '<tr>' + strCalAvg + '</tr>';
                    sumRow += '</table>';
                    $('#content').append('<p>&nbsp</p>');
                    $('#content').append(sumRow);
                    $('#content').append('<p>&nbsp</p>');
                }
                $('#lodimg').css({ 'display': 'none' });
            }
            else {
                $('#lodimg').css({ 'display': 'none' });
            }
            $(document).on('click', ".btnview", function () {
                //console.log($(this).attr("rType"));
                //console.log($(this).attr("oType"));
                //console.log($(this).attr("Day1"));   

                // var sfName = $("#<%=hSFName.ClientID%>").val();


                if (Number($(this).text()) > 0) {

                    var st = "0";
                    if ($(this).attr("phType") == "1") {
                        st = "1";
                    }

                    var str = 'rptTP_Deviation_Retailers.aspx?&SFCode=' + $(this).attr("SFCD") + '&FDate=' + $(this).attr("Day1") + '&oType=' + $(this).attr("oType") + '&rType=' + $(this).attr("rType") + '&phType=' + st;

                    window.open(str, "popupWindow", 'resizable=yes,toolbar=no,scrollbars=1,menubar=no,status=no,width=1100,height=700,left=0,top=0');
                }
            });
            $(document).on('click', ".totret", function () {                              
                if (Number($(this).text()) > 0) {

                    var st = "0";
                    if ($(this).attr("phType") == "1") {
                        st = "1";
                    }
                   // <a href=".cs">../Location_master.aspx.cs</a>
                    var str = 'tpdeviation_maploc.aspx?&SFCode=' + $(this).attr("SFCD") + '&FDate=' + $(this).attr("MNCD") + '&FYear=' + $(this).attr("YRCD") + '';

                    window.open(str, "popupWindow", 'resizable=yes,toolbar=no,scrollbars=1,menubar=no,status=no,width=1100,height=800,left=0,top=0');
                }
            });

            $(document).on('click', ".dayret", function () {                              
                if (Number($(this).text()) > 0) {

                    var st = "0";
                    if ($(this).attr("phType") == "1") {
                        st = "1";
                    }
                   // <a href=".cs">../Location_master.aspx.cs</a>
                    var str = '../Location_master.aspx?&SFCode=' + $(this).attr("SFCD") + '&FDate=' + $(this).attr("Day1") + '';

                    window.open(str, "popupWindow", 'resizable=yes,toolbar=no,scrollbars=1,menubar=no,status=no,width=1100,height=800,left=0,top=0');
                }
            });
            


            $(document).on('click', ".btnPrint", function () {
                var originalContents = $("body").html();
                var printContents = $("#content").html();
                $("body").html(printContents);
                window.print();
                $("body").html(originalContents);
                return false;
            });
            $(document).on('click', "#btnExcel", function (e) {
                var dt = new Date();
                var day = dt.getDate();
                var month = dt.getMonth() + 1;
                var year = dt.getFullYear();
                var postfix = day + "_" + month + "_" + year;
                //creating a temporary HTML link element (they support setting file names)
                var a = document.createElement('a');
                //getting data from our div that contains the HTML table
                var data_type = 'data:application/vnd.ms-excel';
                var table_div = document.getElementById('content');
                var table_html = table_div.outerHTML.replace(/ /g, '%20');
                a.href = data_type + ', ' + table_html;
                //setting the file name
                a.download = 'Deviation_' + postfix + '.xls';
                //triggering the function
                a.click();
                //just in case, prevent default behaviour
                e.preventDefault();
            });

        });

    </script>
    <style type="text/css">
        #lodimg
        {
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
        #DeviationTable tr td
        {
            text-align: right;
        }
        #DeviationTable tr td:nth-child(1), #DeviationTable tr td:nth-child(2), #DeviationTable tr td:nth-child(3), #DeviationTable tr td:nth-child(4), #DeviationTable tr td:nth-child(5), #DeviationTable tr td:nth-child(6), #DeviationTable tr td:nth-child(7)
        {
            text-align: left;
        }
        #MonthTable tr td:not(:first-child)
        {
            text-align: right;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hSfCode" runat="server" />
    <asp:HiddenField ID="hFyear" runat="server" />
    <asp:HiddenField ID="hFMonth" runat="server" />
    <asp:HiddenField ID="Month" runat="server" />
    <asp:HiddenField ID="YEAR" runat="server" />
    <asp:HiddenField ID="hSFName" runat="server" />
	<asp:HiddenField ID="hSfmode" runat="server" />
    <div>
        <br />
        <asp:Panel ID="pnlbutton" runat="server">
            <table width="90%" style="margin: 0px auto;">
                <tr>
                    <td width="60%" align="center">
                        <asp:Label ID="lblHead" Text="TP - Deviation" SkinID="lblMand" Font-Bold="true" Font-Underline="true"
                            runat="server" Style="font-size: larger"></asp:Label>
                    </td>
                    <td align="right">
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="btnPrint" runat="server" Style="padding: 0px 20px;" href="#"
                                        class="btn btnPrint" />
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnExcel" runat="server" OnClick="btnExcel_Click" Style="padding: 0px 20px;"
                                        href="#" class="btn btnExcel" />
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnClose" runat="server" Style="padding: 0px 20px;" href="javascript:window.open('','_self').close();"
                                        class="btn btnClose" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <asp:Panel ID="pnlContents" runat="server" Width="90%" Style="margin: 0px auto;">
        <div id="content">
            <div align="center">
            </div>
            <div>
                <table width="100%" align="center">
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblIdRegionName" Text="Team Name :" runat="server" SkinID="lblMand"> </asp:Label>
                            <asp:Label ID="lblRegionName" runat="server" SkinID="lblMand" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <%--<table id="DeviationTable" width="100%" class="newStly" style="border-collapse: collapse;">
                <thead>
                    <tr>
                        <th>
                            DATE
                        </th>
                        <th>
                            DAY
                        </th>
                        <th>
                            TOUR PLAN
                        </th>
                        <th>
                            TP DISTRIBUTOR
                        </th>
                        <th>
                            DP DISTRIBUTOR
                        </th>
                        <th>
                            WORK WITH
                        </th>
                        <th>
                            AREA WORKED
                        </th>
                        <th>
                            DEVIATION YES/NO
                        </th>
                        <th>
                            PRIMARY
                        </th>
                        <th>
                            PRIMARY VALUE
                        </th>
                        <th>
                            SEMI WHOLESELLER
                        </th>
                        <th>
                            SEMI WHOLESELLER VALUE
                        </th>
                        <th>
                            RETAILER
                        </th>
                        <th>
                            RETAILER VALUE
                        </th>
                        <th>
                            DOCTOR
                        </th>
                        <th>
                            DOCTOR VALUE
                        </th>
                        <th>
                            NURSHING HOME
                        </th>
                        <th>
                            NURSHING HOME VAULE
                        </th>
                        <th>
                            PAYMENT COLLECTION
                        </th>
                        <th>
                            TC
                        </th>
                        <th>
                            PC
                        </th>
                        <th>
                            VALUES
                        </th>
                        <th>
                            REPORT RECEIVING DATE
                        </th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>--%>
            <br />
            <%-- <table id="MonthTable" width="80%" class="newStly" style="border-collapse: collapse;">
                <thead>
                </thead>style="display: none"
                <tbody>
                </tbody>
            </table>--%>
            <img id="lodimg" src="../Images/loadingN.gif"  alt="" />
        </div>
    </asp:Panel>
    </form>
</body>
</html>
