<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_FieldPerformance.aspx.cs" Inherits="MIS_Reports_Rpt_FieldPerformance" %>

<!DOCTYPE html>

<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
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
        <asp:HiddenField ID="hidsfname" runat="server" />
        <asp:HiddenField ID="hFYear" runat="server" />
        <asp:HiddenField ID="hTYear" runat="server" />
        <asp:HiddenField ID="hFMonth" runat="server" />
        <asp:HiddenField ID="hTMonth" runat="server" />
        <asp:HiddenField ID="subDiv" runat="server" />

        <div class="row">
            <div class="col-sm-8">
                <asp:Label ID="Label1" runat="server" Text="FieldForce Summary" Style="margin-left: 10px; font-size: x-large" />
            </div>
        </div>

        <div class="row" style="margin: 6px 0px 0px 11px;">
            <asp:Label ID="Label2" Text="Field Force Name :" runat="server" Style="font-size: larger" />
            <asp:Label ID="lblsf_name" runat="server" Style="font-size: larger" />
        </div>

        <div class="row" style="margin: 0px 0px 0px 5px;">
            <br />
            <br />
            <div id="content">
                <table id="FFTbl" class="newStly" style="border-collapse: collapse;">
                    <thead>
                        <tr>
                            <th>SLNo.</th>
                            <th>Date</th>
                            <th>State</th>
                            <th>HQ</th>
                            <th>Field Force Name</th>
                            <th>Designation</th>
                            <th>Employee Id</th>
                            <th>Reporting Manager</th>
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
                            <th>Value</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                    <tfoot>
                    </tfoot>
                </table>
                <br />
                <div class="detail" id="detail" runat="server" style="padding-left: 100px;">
                     <%--  <table align="center">
                        <tbody style="border: 1px solid black;">
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Andalus" Font-Underline="True" ForeColor="#0099CC" Text="Total Calls :" />
                                </td>
                                <td>&nbsp&nbsp</td>
                                <td>
                                    <asp:Label ID="callcount" runat="server" Font-Bold="True" SkinID="lblMand" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Andalus" Font-Underline="True"
                                        ForeColor="#0099CC" Text="Total Days :"></asp:Label>
                                </td>
                                <td>&nbsp&nbsp</td>
                                <td>
                                    <asp:Label ID="productive" runat="server" Font-Bold="True" SkinID="lblMand"></asp:Label>
                                </td>
                        </tr>--%>
                            <%-- <tr>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="Andalus" Font-Underline="True" ForeColor="#0099CC" Text="Drop Size :"></asp:Label>
                                    </td>
                                    <td>&nbsp&nbsp</td>
                                    <td>
                                        <asp:Label ID="drop_size" runat="server" Font-Bold="True" SkinID="lblMand"></asp:Label>
                                    </td>
                                </tr>--%>
                           <%-- <tr>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Andalus" Font-Underline="True" ForeColor="#0099CC" Text="Call Average :"></asp:Label>
                                </td>
                                <td>&nbsp&nbsp</td>
                                <td>
                                    <asp:Label ID="call_average" runat="server" Font-Bold="True" SkinID="lblMand"></asp:Label>
                                </td>
                            </tr>--%>
                            <%--<tr>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Andalus" Font-Underline="True" ForeColor="#0099CC" Text="Closing Time :"></asp:Label>
                                    </td>
                                    <td>&nbsp&nbsp</td>
                                    <td>
                                        <asp:Label ID="closingtime" runat="server" Font-Bold="True" SkinID="lblMand"></asp:Label>
                                    </td>
                                </tr>--%>
                           <%-- <tr>
                                <td>
                                    <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="Andalus" Font-Underline="True" ForeColor="#0099CC" Text="Total Hours Worked :"></asp:Label>
                                </td>
                                <td>&nbsp&nbsp</td>
                                <td>
                                    <asp:Label ID="tot_hours" runat="server" Font-Bold="True" SkinID="lblMand"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="Andalus" Font-Underline="True" ForeColor="#0099CC" Text="Total New Retailers :"></asp:Label>
                                </td>
                                <td>&nbsp&nbsp</td>
                                <td>
                                    <asp:Label ID="Total_new_rt" runat="server" Font-Bold="True" SkinID="lblMand"></asp:Label>
                                </td>
                            </tr>--%>
                            <%--<tr> 
                                    <td>
                                        <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Names="Andalus" 
                                            Font-Underline="True" ForeColor="#0099CC" Text="Visited New Retailers :"></asp:Label>
                                    </td>
                                    <td>&nbsp&nbsp</td>
                                    <td>
                                        <asp:Label ID="Tot_new_ret" runat="server" Font-Bold="True" SkinID="lblMand"></asp:Label>
                                    </td>
                                </tr>--%>
                        <%--</tbody>
                    </table>--%>
                </div>
            </div>
        </div>
        <div class="overlay" id="loadover" style="display: none;">
            <div id="loader"></div>
        </div>
    </form>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        var tbplan = [], dcrcalls = [], stendtimes = [], rtscount = [], sfusers = [], tpdates = [], dayOrders = [], newRPOB = []; leavedet = [];
        var bDat = [];
        var sfDate = [];
        var sNum = 1;
        var eachrow = 0; var nerc = 0; var tc = 0; var cavg = 0; var tds = 0; var ths = 0;

        function getDayPlan() {
            return $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "Rpt_FieldPerformance.aspx/getDayPlan",
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



        function getUsers() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Rpt_FieldPerformance.aspx/getSFdets",
                data: "{'Div':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    sfusers = JSON.parse(data.d) || [];
                    //console.log(sfusers);
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }



        function getNewRetailerPOB() {
            return $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "Rpt_FieldPerformance.aspx/getNewRetailerPOB",
                dataType: "json",
                success: function (data) {
                    newRPOB = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }

        function getProdCalls() {
            return $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "Rpt_FieldPerformance.aspx/getDaywiseCalls",
                dataType: "json",
                success: function (data) {
                    dcrcalls = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }

        function getStartEnd() {
            return $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "Rpt_FieldPerformance.aspx/getAttendance",
                dataType: "json",
                success: function (data) {
                    stendtimes = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }

        function getRetailerCount() {
            return $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "Rpt_FieldPerformance.aspx/getRetailerCount",
                dataType: "json",
                success: function (data) {
                    rtscount = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }

        function getTpdates() {
            return $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "Rpt_FieldPerformance.aspx/GetTpDates",
                dataType: "json",
                success: function (data) {
                    tpdates = JSON.parse(data.d) || [];
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
                url: "Rpt_FieldPerformance.aspx/getOrders",
                dataType: "json",
                success: function (data) {
                    dayOrders = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function getleave() {
            return $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "Rpt_FieldPerformance.aspx/getleave",
                dataType: "json",
                success: function (data) {
                    leavedet = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function loadData() {
            $.when(getStartEnd(), getTpdates(), getDayPlan(), getProdCalls(), getNewRetailerPOB(), getRetailerCount(), getOrders(), getleave()).then(function () {
                ReloadData();
                setTimeout(function () { loadaddrs($('#FFTbl tbody tr')[0]) }, 29);
            });
        }

        function loadaddrs($tr) {
            var Long = parseFloat($($tr).find('td').eq(24).text());
            var Lat = parseFloat($($tr).find('td').eq(23).text());
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
		var sfname=$("#hidsfname").val();
            $("#lblsf_name").html(sfname);
            $("#FFTbl>tbody").html('');
            var str = "";
            let slno = 0;
            let gtotal = 0; var divcode = '<%=Session["div_code"]%>';
            if (divcode == "179") {
                $('.detail').show();
            }
            else { $('.detail').hide(); }
            for (var i = 0; i < tpdates.length; i++) {
                for (var j = 0; j < sfusers.length; j++) {
                    let actcalls = 0;

                    //let totcalls = 0;
                    //let prodcalls = 0;

                    let productivity = 0;
                    let phoneOrderCnt = 0;
                    let nretailers = 0;
                    let nretailerspob = 0;

                    str += `<tr><td>${++slno}</td><td>${tpdates[i].actdt}</td><td>${sfusers[j].StateName}</td><td>${sfusers[j].sf_hq}</td><td>${sfusers[j].SF_Name}</td><td>${sfusers[j].Designation}</td><td>${sfusers[j].sf_emp_id}</td><td>${sfusers[j].ReportingManager}</td><td>${sfusers[j].SF_Mobile}</td>`;

                    let tbday = tbplan.filter(function (a) {
                        return a.Sf_Code == sfusers[j].SF_Code && a.dt == tpdates[i].dt;

                    });

                   // for (var h = 0; h < leavedet.length; h++) {
                      
                        let leaveday = leavedet.filter(function (a) {
                            return a.Sf_Code == sfusers[j].SF_Code && a.dt == tpdates[i].dt;

                        });
                    //}

                    //console.log(tbday);
                    nretailers = newRPOB.filter(function (a) {
                        return a.sf_code == sfusers[j].SF_Code && a.Activity_Date == tpdates[i].dt;
                    }).map(function (a) { return a.NRC; }).toString() || 0;

                    nretailerspob = newRPOB.filter(function (a) {
                        return a.sf_code == sfusers[j].SF_Code && a.Activity_Date == tpdates[i].dt;
                    }).map(function (a) { return a.NRVAL; }).toString() || 0;

                    let startOrderTime = '';

                    //let jointwrk = tbday.map(function (a) {
                    //    return a.worked_with_name
                    //}).join(',');

                    //let todayWtype = tbday.map(function (a) {
                    //    return a.Wtype
                    //}).join(',');

                    //let remarks = tbday.map(function (a) {
                    //    return a.remarks
                    //}).join(',');

                    let mymap = new Map();
                    let dismap = new Map();
                    let selbeats = [];
                    let selstks = [];
                    let stkadrs = [];
                    let todayWtype = [];
                    let remarks = [];
                    let jointwrk = [];

                    selbeats = tbday.filter(function (el) {
                        const val = mymap.get(el.cluster);
                        if (val) {
                            return false;
                        }
                        mymap.set(el.cluster, el.ClstrName);
                        return true;
                    }).map(function (a) { return a; }).sort();

                    selstks = tbday.filter(function (el) {
                        const val = dismap.get(el.StkName);
                        if (val) {
                            return false;
                        }
                        dismap.set(el.StkName, el.StkName);
                        return true;
                    }).map(function (a) { return a; }).sort();

                    stkadrs = tbday.filter(function (el) {
                        const val = dismap.get(el.Stockist_Address);
                        if (val) {
                            return false;
                        }
                        dismap.set(el.Stockist_Address, el.Stockist_Address);
                        return true;
                    }).map(function (a) { return a; }).sort();

                    todayWtype = tbday.filter(function (el) {
                        const val = dismap.get(el.Wtype);
                        if (val) {
                            return false;
                        }
                        dismap.set(el.Wtype, el.Wtype);
                        return true;
                    }).map(function (a) { return a; }).sort();


                    remarks = tbday.filter(function (el) {
                        const val = dismap.get(el.remarks);
                        if (val) {
                            return false;
                        }
                        dismap.set(el.remarks, el.remarks);
                        return true;
                    }).map(function (a) { return a; }).sort();

                    jointwrk = tbday.filter(function (el) {
                        const val = dismap.get(el.worked_with_name);
                        if (val) {
                            return false;
                        }
                        dismap.set(el.worked_with_name, el.worked_with_name);
                        return true;
                    }).map(function (a) { return a; }).sort();

                    if (tbday.length > 0) {
                        startOrderTime = tbday[0].lgtime;
                        let beats = selbeats.map(function (a) {
                            return a.ClstrName
                        });//.join(',');

                        let stkscd = selstks.map(function (a) {
                            if (a.StkName != '') {
                                return a.StkName
                            }

                        });//.join(',');

                        let stkadr = stkadrs.map(function (a) {
                            return a.Stockist_Address
                        }).join(',');

                        let wt = todayWtype.map(function (a) {
                            return a.Wtype
                        }).join(',');

                        let res = remarks.map(function (a) {
                            return a.remarks
                        }).join(',');

                        let jw = jointwrk.map(function (a) {
                            return a.worked_with_name

                        });//.join(',');

                       // for (var k = 0; k < selbeats.length; k++) {
                            //let filtcalls = parseInt(rtscount.filter(function (a) {
                             //   return a.Territory_Code == selbeats[k].cluster
                           // }).map(function (el) {
                             //   return el.cnt
                           // }));
                           // actcalls += parseInt(isNaN(filtcalls) == true ? 0 : filtcalls);
                       // }
			if (divcode != "29") {
                            for (var k = 0; k < selbeats.length; k++) {
                                let filtcalls = parseInt(rtscount.filter(function (a) {
                                    return a.Territory_Code == selbeats[k].cluster
                                }).map(function (el) {
                                    return el.cnt
                                }));
                                actcalls += parseInt(isNaN(filtcalls) == true ? 0 : filtcalls);
                            }
                        } else {
                            //let actcalls = 0;

                            for (var k = 0; k < selbeats.length; k++) {
                                let clusterCodes = selbeats[k].cluster.split(',').map(Number);
                                let filtcalls = rtscount.filter(function (a) {
                                    return clusterCodes.includes(a.Territory_Code);
                                }).map(function (el) {
                                    return el.cnt;
                                });

                                actcalls += filtcalls.reduce(function (sum, cnt) {
                                    return sum + cnt;
                                }, 0);
                            }
                        }
                        str += `<td>${wt}</td><td>${jw}</td><td>${res}</td><td>${stkscd}</td><td>${stkadr}</td><td>${beats}</td><td>${actcalls}</td>`;
                    }
                    else {
                        if (divcode == '29') {

                            if (leaveday != '') {
                                str += `<td>Leave</td><td></td><td></td><td></td><td></td><td></td><td></td>`;
                            } else {
                                str += `<td>Absent</td><td></td><td></td><td></td><td></td><td></td><td></td>`;
                            }

                        } else {
                            str += `<td>Absent</td><td></td><td></td><td></td><td></td><td></td><td></td>`;
                        }


                    }

                    let callsmade = dcrcalls.filter(function (a) {
                        return a.ActDate == tpdates[i].dt && a.Sf_Code == sfusers[j].SF_Code;
                    });

                    phoneOrderCnt = dayOrders.filter(function (a) {
                        return a.Sf_Code == sfusers[j].SF_Code && a.dt == tpdates[i].dt;
                    }).map(function (el) {
                        return el.cnt;
                    }).toString();

                    let daystend = stendtimes.filter(function (a) {
                        return a.login_date == tpdates[i].dt && a.Sf_Code == sfusers[j].SF_Code;
                    });

                    let todayOrderval = 0;
                    todayOrderval = parseFloat(dayOrders.filter(function (a) {
                        return a.Sf_Code == sfusers[j].SF_Code && a.dt == tpdates[i].dt;
                    }).map(function (el) {
                        return el.orderAmt;
                    })).toFixed(2);

                    if (callsmade.length > 0) {
                        productivity = (parseInt(callsmade[0].EfCall * 100) / parseInt(callsmade[0].TCall));
                        str += `<td>${callsmade[0].TCall}</td><td>${callsmade[0].EfCall}</td><td>${(isNaN(productivity) == true ? 0 : productivity.toFixed(2))}</td>`;

                        //alert(callsmade[0].totaldays);
                        if (divcode == "179") {
                            let tc1 = callsmade[0].TCall;
                            tc += +tc1;

                            tds = callsmade[0].totaldays
                        }
                        //alert(tds);

                    }
                    else {
                        str += `<td></td><td></td><td></td>`

                    }
                    str += `<td>${nretailers}</td><td>${nretailerspob}</td><td>${phoneOrderCnt}</td>`;

                    let nerc1 = parseFloat(isNaN(nretailers) == true ? 0 : nretailers);
                    nerc = parseFloat(nerc) + parseFloat(nerc1);

                    //nerc += +nretailers;
                    //nerc = sum(nerc + nretailers);
                    //alert(nerc);
                    if (daystend.length > 0) {
                        var login = new Date(daystend[0].Start_DtTime);
                        var lout = new Date(daystend[0].End_DtTime);
                        var ltimediff = (lout - login) / 1000;
                        var hours = parseInt(ltimediff / 3600) % 24;
                        var minutes = parseInt(ltimediff / 60) % 60;
                        var seconds = ltimediff % 60;
                        var result = (hours < 10 ? "0" + hours : hours) + ":" + (minutes < 10 ? "0" + minutes : minutes) + ":" + (seconds < 10 ? "0" + seconds : seconds);

                        str += `<td>${(daystend.length < 1) ? startOrderTime : daystend[0].Start_Time}</td><td>${daystend[0].End_Time}</td><td>${isNaN(ltimediff) == true ? '' : result}</td><td>${(daystend.length < 1) ? Start_Lat : daystend[0].Start_Lat}</td><td>${(daystend.length < 1) ? Start_Long : daystend[0].Start_Long}</td><td class="place"></td>`;


                        //alert(result1);


                    }
                    else {
                        str += `<td>${startOrderTime}</td><td></td><td></td><td></td><td></td><td></td>`;
                    }
                    if (callsmade.length > 0) {
                        var stime = new Date(callsmade[0].ActDate + " " + callsmade[0].minTime);
                        var etime = new Date(callsmade[0].ActDate + " " + callsmade[0].maxTime);
                        var timediff = (etime - stime) / 1000;
                        var hours = parseInt(timediff / 3600) % 24;
                        var minutes = parseInt(timediff / 60) % 60;
                        var seconds = timediff % 60;
                        var result = (hours < 10 ? "0" + hours : hours) + ":" + (minutes < 10 ? "0" + minutes : minutes) + ":" + (seconds < 10 ? "0" + seconds : seconds);

                        str += `<td>${callsmade[0].minTime}</td><td>${callsmade[0].maxTime}</td><td>${isNaN(timediff) == true ? '' : result}</td>`;
                        var ths1 = hours;
                        //alert(ths1);
                        var thm = (minutes < 10 ? "0" + minutes : minutes);
                        var result1 = 0;
                        result1 = ths1 + "." + thm;

                        ths += +result1;
                        //alert(ths);
                    }
                    else {
                        str += `<td></td><td></td><td></td>`;
                    }
                    let daytotal = parseFloat(isNaN(todayOrderval) == true ? 0 : todayOrderval);
                    gtotal = parseFloat(gtotal) + parseFloat(daytotal);
                    str += `<td>${daytotal.toFixed(2)}</td></tr>`;
                }
            }
            //let fstr = `<tr><th colspan="29">Total</th><th>${gtotal.toFixed(2)}</th></tr>`
            let fstr = `<tr><th colspan="31">Total</th><th>${gtotal.toFixed(2)}</th></tr>`
            $("#FFTbl >tbody").append(str);
            $("#FFTbl >tfoot").append(fstr);

             <%-- if (divcode == "179") {
                $('#<%=callcount.ClientID%>').html(tc);
                if (tc > tds) {
                    var cavg = parseFloat((tc) / (tds));

                    $('#<%=call_average.ClientID%>').html(cavg.toFixed(2));
                }
                else {
                    var cavg = 0;
                    $('#<%=call_average.ClientID%>').html(cavg);
                }

                $('#<%=Total_new_rt.ClientID%>').html(nerc);

                $('#<%=tot_hours.ClientID%>').html(ths);

                $('#<%=productive.ClientID%>').html(tds);
            }--%>



            //document.getElementById('Total_new_rt').text(nerc);

        }

        $(document).ready(function () {
            var divcode = '<%=Session["div_code"]%>';
            if (divcode == "179") {
                $('.detail').show();
            }
            else { $('.detail').hide(); }
            $('#loadover').show();
            getUsers();
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
