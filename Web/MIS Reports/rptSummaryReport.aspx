<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptSummaryReport.aspx.cs"
    Inherits="MIS_Reports_rptSummaryReport" %>

<!DOCTYPE html>
<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="content-type" content="text/plain; charset=UTF-8" />
    <title>Summary Report</title>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <style type="text/css">
        
    </style>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">	
        var tpData = [];
        var tcData = [];
        var CatVal = [];
        var catH = [];
        var rshq = [];
        var dRSF = [];
        var oCount = [];
        var oTimes = [];
        var sTimes = [];
        var lvl = 0;
        var sf_code = "";
        var Tdate = "";
        var Fdate = "";
        var subDiv = "";
		function genReport() {
                if (tpDate.length > 0 && sfData.length > 0 && rshq.length > 0 && tcData.length > 0) { //&& sTimes.length > 0  && CatVal.length > 0 && oCount.length > 0 && oTimes.length > 0
                    var slno = 1;
                    for (var i = 0; i < tpDate.length; i++) {
                        for (var j = 0; j < sfData.length; j++) {
                            str = '<td>' + (slno++) + '</td> <td>' + tpDate[i].tpdays + '</td>';
                            vp = rshq.filter(function (a) { return (a.sfCode == sfData[j].RSFs); });
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
                            else {
                                var nStr = '';
                                for (var u = 1; u < lvl; u++) {
                                    nStr += '<td></td>';
                                }
                                str += '<td>' + sfData[j].sfName + '</td>' + nStr;
                            }
                            str += '<td>' + sfData[j].EmpID + '</td><td>' + sfData[j].sfName + '</td><td>' + sfData[j].Desig + '</td><td>' + sfData[j].sfMobile + '</td>';
                            mTP = tpData.filter(function (a) { return (a.tpDate == tpDate[i].tpdays && a.sfCode == sfData[j].sfCode); });
                            var tpType = 'Absent';
                            var tpRson = '';
                            var tpScName = '';
                            var tpScCounts = 0;
                            var chRoutes = ''
                            if (mTP.length > 0) {
                                tpType = '';
                                for (var k = 0; k < mTP.length; k++) {
                                    tpType += mTP[k].workType + ',';
                                    tpRson += mTP[k].Remarks + ',';
                                    tpScName += mTP[k].RouteName + ',';
                                    if (chRoutes != mTP[k].RouteCode) {
                                        tpScCounts += Number(mTP[k].ldrCount || 0);
                                        chRoutes = mTP[k].RouteCode;
                                    }
                                }
                            }
                            str += '<td>' + tpType + '</td><td>' + tpRson + '</td><td>' + tpScName + '</td><td>' + tpScCounts + '</td>';

                            TcEc = tcData.filter(function (a) { return (a.Activity_Date == tpDate[i].tpdays && a.sfCode == sfData[j].sfCode); });
                            var tc = 0;
                            var ec = 0;
                            var rtCnt = 0;
                            var rtVal = 0;
                            var phorder = 0;
                            if (TcEc.length > 0) {
                                tc = TcEc[0].TVRC;
                                ec = TcEc[0].TPRC;
                                rtCnt = TcEc[0].NRC;
                                rtVal = TcEc[0].NRVAL;
                                phorder = TcEc[0].phoneorder;
                            }
                            str += '<td>' + tc + '</td><td>' + ec + '</td><td>' + Number(Number(Number(ec) / Number(tc == 0 ? 1 : tc)) * 100).toFixed(2) + '</td> <td>' + phorder + '</td> ';
                            var sTime = '00.00';
                            var eTime = '00.00';
                            var dTime = '00.00';
                            var tLet = '';
                            TT = oTimes.filter(function (a) { return (a.ordDate == tpDate[i].tpdays && a.Sf_Code == sfData[j].sfCode); });
                            if (TT.length > 0) {
                                sTime = TT[0].minTime;
                                eTime = TT[0].maxTime;
                                dTime = TT[0].Duration;
                                var arTm = dTime.split(':');
                                tLet = arTm[0];
                            }
                            var sLet = ''
                            if (Number(tLet || 0) <= 2) {
                                sLet = '<td style="background-color: #af0f0f; color:#fff">C</td><td style="background-color: #af0f0f; color:#fff">Less than 3 Hours</td>';

                            }
                            else if (Number(tLet || 0) > 2 && Number(tLet || 0) < 6) {
                                sLet = '<td style="background-color: #00aee8; color:#fff">B</td><td style="background-color: #00aee8; color:#fff">3 to 6 Hours</td>';
                            }
                            else if (Number(tLet || 0) >= 6) {
                                sLet = '<td style="background-color: #00b500; color:#000">A</td><td style="background-color: #00b500; color:#000">More than 6 Hours</td>';
                            }

                            sTeT = sTimes.filter(function (a) { return (a.login_date == tpDate[i].tpdays && a.Sf_Code == sfData[j].sfCode); });
                            var sTim = '';
                            var eTim = '';
                            var sLog = '';
                            var duTime = '';
                           // console.log(sfData[j].sfCode);
                           // console.log(sTeT);
                            if (sTeT.length > 0) {
                                sTim = sTeT[0].Start_Time;
                                eTim = sTeT[0].End_Time;
                                sLog = sTeT[0].Start_Lat + ', ' + sTeT[0].Start_Long;
                                duTime = sTeT[0].durations;
                                //rtVal = sTeT[0].Start_Long;

                            }
                            else if(mTP.length>0) {
                                sTim = mTP[0].startTime
                            }

                            str += '<td>' + sTim + '</td><td>' + eTim + '</td><td>' + duTime + '</td><td>' + sLog + '</td>';
                            str += '<td>' + sTime + '</td><td>' + eTime + '</td><td>' + dTime + '</td>';
                            str += '<td>' + rtCnt + '</td><td>' + Number(rtVal).toFixed(2) + '</td>';
                            var str2 = '';
                            var TotQTY = 0;
                            var TotVal = 0;
                            var TotOutPen = 0;
                            var TotPen = 0;
                            subCat = CatVal.filter(function (a) { return (a.sfCode == sfData[j].sfCode && a.Activity_Date == tpDate[i].tpdays); });
                            for (var k = 0; k < catH.length; k++) {
                                var cVal = 0;
                                var cQty = 0;
                                if (subCat.length > 0) {
                                    cv = subCat.filter(function (a) { return (a.pCatCode == catH[k].pCatCode) });
                                    if (cv.length > 0) {
                                        cVal = (Number(cv[0].ordVal) || 0).toFixed(2);
                                        cQty = (Number(cv[0].ordQty) || 0);
                                    }
                                }
                                str2 += '<td>' + cVal + '</td>';
                                TotQTY += Number(cQty);
                                TotVal += Number(cVal);
                            }
                            var ordCnt = 0;
                            cc = oCount.filter(function (a) { return (a.Sf_Code == sfData[j].sfCode && a.ordDate == tpDate[i].tpdays); });
                            if (cc.length > 0) {
                                ordCnt = Number(cc[0].cnt || 0);
                            }
                            str += '<td>' + ordCnt + ' </td><td>' + TotQTY + '</td><td>' + Number(TotVal).toFixed(2) + '</td>' + str2 + sLet;
                            $('#FFTbl tbody').append('<tr>' + str + '</tr>');
                        }
                    }
                    if ($('#FFTbl tbody tr').length > 0) {
                        $("#<%=Label2.ClientID%>").hide();
                    }
                }

            }
        $(document).ready(function () {
            
            sf_code = $("#<%=hdnSFCode.ClientID%>").val();
            Tdate = $("#<%=hdnYear.ClientID%>").val();
            Fdate = $("#<%=hdnMonth.ClientID%>").val();
            subDiv = $("#<%=hdnSubDivCode.ClientID%>").val();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptSummaryReport.aspx/GetReportingToSF",
                dataType: "json",
                success: function (data) {
                    dRSF = data.d;
                    console.log(data.d);
                },
                error: function (error) {
                    console.log(error);
                }
            });
            getCatHead();
            $.when(getMyDayPlanDets(), getTCECDetails(), getOrderCount(), getOrderDates(), getEmployees(), getTpDates(), getCatOrdersDets(), getLoginDetails()).then(function () {
                genReport();
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
            $('#FFTbl tr').remove();
            var str = '<th>SLNo.</th><th>Date</th>';
            for (jk = 0; jk < lvl; jk++) {

                if (jk > 0) {
                    str += '<th>Level-' + jk + '</th>';
                }
                else {
                    str += '<th>State HQ</th>';
                }
            }
            str += '<th>EMP ID</th><th>User Name</th> <th>User Rank</th><th>SR Contact No</th><th>Type</th><th>Reason</th><th>Selected Beats</th><th>AC</th><th>TC</th><th>PC</th><th>Productivity</th><th>Telephonic Orders</th> ';
            str += '<th>Login</th><th>Log Out</th><th>Total Time(HH:MM)</th><th>Start Day Location</th>';
            str += '<th>First Call</th><th>Last Call</th><th>Total Retail Time</th>';
            str += '<th>New Outlets</th><th>N.O. Value</th> <th> Orders </th> <th>Qty(Std Unit)</th><th>Value</th>'
            if (catH.length > 0) {
                for (var k = 0; k < catH.length; k++) {
                    str += '<th>' + catH[k].pCatName + '</th>';
                }
            }
            $('#FFTbl thead').append('<tr >' + str + '<th style="width:200px">Retailing Grade</th><th style="width:200px">Retailing Grade Definiton</th></tr>');

            $(document).on('click', '#btnExcel', function (e) {

                //console.log('Enter');
                //                var data_type = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#excelDiv').html());
                //                var a = document.createElement('a');
                //                a.href = data_type;


                //                a.download = 'SummaryReport.xls';
                //                //triggering the function                
                //                a.click();
                //                //just in case, prevent default behaviour
                //                e.preventDefault();




                //                var a = document.createElement('a');
                //                var data_type = 'data:application/vnd.ms-excel';
                //                a.href = data_type + ', ' + encodeURIComponent($('div[id$=excelDiv]').html());
                //                document.body.appendChild(a);  
                //                a.download = 'SummaryReport.xls';
                //                a.click();
                //                e.preventDefault();

                ////                var fileName = 'Test file.xls';
                ////                var blob = new Blob([$('div[id$=excelDiv]').html()], {
                ////                    type: "application/csv;charset=utf-8;"
                ////                });

                ////                a.href = URL.createObjectURL(blob);
                ////                a.click();
                ////                e.preventDefault();


                var a = document.createElement('a');

                var fileName = 'Test file.xls';
                var blob = new Blob([$('div[id$=excelDiv]').html()], {
                    type: "application/csv;charset=utf-8;"
                });
                document.body.appendChild(a);
                a.href = URL.createObjectURL(blob);
                a.download = 'SummaryReport.xls';
                a.click();
                e.preventDefault();

            });

        });
        function getCatHead() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptSummaryReport.aspx/GetCategoryHead",
                dataType: "json",
                success: function (data) {
                    catH = data.d;
                    console.log(data.d);
                },
                error: function (error) {
                    console.log(error);
                }
            });
        }
        function getLoginDetails() {
            return $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "rptSummaryReport.aspx/GetStartTimes",
                data: "{'Fdate':'" + Fdate + "', 'Tdate':'" + Tdate + "'}",
                dataType: "json",
                success: function (data) {
                    sTimes = data.d;

                    // genReport();
                    //console.log(data.d);
                },
                error: function (error) {
                    console.log(error);
                }
            });
        }
        function getCatOrdersDets(){
            return $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "rptSummaryReport.aspx/GetCatOrderDetails",
                data: "{'Fdate':'" + Fdate + "', 'Tdate':'" + Tdate + "'}",
                dataType: "json",
                success: function (data) {
                    CatVal = data.d;
                    console.log(data.d);
                    // genReport();
                },

                error: function (error) {
                    console.log(error);
                }
            });
        }
        function getOrderDates() {
            return $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "rptSummaryReport.aspx/GetOrderTimes",
                data: "{'Fdate':'" + Fdate + "', 'Tdate':'" + Tdate + "'}",
                dataType: "json",
                success: function (data) {
                    oTimes = data.d;
                    console.log(data.d);

                },
                error: function (error) {
                    console.log(error);
                }
            });
        }
        function getOrderCount() {
            return $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "rptSummaryReport.aspx/GetOrderCounts",
                data: "{'Fdate':'" + Fdate + "', 'Tdate':'" + Tdate + "'}",
                dataType: "json",
                success: function (data) {
                    oCount = data.d;
                    console.log(data.d);
                },
                error: function (error) {
                    console.log(error);
                }
            });
        }
        function getTpDates() {
            return $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "rptSummaryReport.aspx/GetTpDates",
                data: "{'Fdate':'" + Fdate + "', 'Tdate':'" + Tdate + "'}",
                dataType: "json",
                success: function (data) {
                    tpDate = data.d;
                    console.log(data.d);
                },
                error: function (error) {
                    console.log(error);
                }
            });
        }
        function getEmployees() {
            return $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "rptSummaryReport.aspx/GetFieldForces",
                dataType: "json",
                data: "{'SF_Code':'" + sf_code + "', 'SubDiv':'" + subDiv + "'}",
                success: function (data) {
                    sfData = data.d;
                    console.log(data.d);
                },
                error: function (error) {
                    console.log(error);
                }
            });
        }
        function getMyDayPlanDets() {
            return $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "rptSummaryReport.aspx/GetTpMyDayDetails",
                data: "{'Fdate':'" + Fdate + "', 'Tdate':'" + Tdate + "','SF_Code':'" + sf_code + "'}",
                dataType: "json",
                success: function (data) {
                    tpData = data.d;
                    console.log(data.d);
                },
                error: function (error) {
                    console.log(error);
                }
            });
        }
        function getTCECDetails() {
           return $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "rptSummaryReport.aspx/GetTcEcDetails",
                data: "{'Fdate':'" + Fdate + "', 'Tdate':'" + Tdate + "'}",
                dataType: "json",
                success: function (data) {
                    tcData = data.d;
                    console.log(data.d);
                },
                error: function (error) {
                    console.log(error);
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <asp:HiddenField ID="hdnSFCode" runat="server" />
    <asp:HiddenField ID="hdnSubDivCode" runat="server" />
    <asp:HiddenField ID="hdnYear" runat="server" />
    <asp:HiddenField ID="hdnMonth" runat="server" />
    <div class="row" style="max-width: 100%; width: 98%">
        <br />
        <div class="col-sm-8">
            <asp:Label ID="lblhead1" runat="server" Style="font-weight: bold; font-size: x-large;
                padding: 0px 20px;" Text="Field Force Wise Summary"></asp:Label>
            <br />
        </div>
        <div class="col-sm-4" style="text-align: right">
            <asp:LinkButton ID="btnPrint" runat="Server" Style="padding: 0px 20px;" class="btn btnPrint"
                Visible="false" />
            <a id="btnExport" runat="Server" style="padding: 0px 20px;" class="btn btnPdf" visible="false">
            </a><a id="btnExcel" style="padding: 0px 20px;" class="btn btnExcel" /><a name="btnClose"
                id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();"
                class="btn btnClose"></a>
        </div>
    </div>
    <div class="container" id="excelDiv" style="max-width: 100%; width: 98%">
        
        <table id="FFTbl" class="newStly" style="border-collapse: collapse;">
            <thead>
            </thead>
            <tbody>
            </tbody>
            <tfoot>
            </tfoot>
        </table>
    </div>
    <asp:Label ID="Label2" runat="server" Text="Label" Style="padding-left: 20px; text-align: center;
            font-size: x-large; color: #0210ff; font-weight: bold"></asp:Label>
    </form>
</body>
</html>
