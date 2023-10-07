<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TsrRpt_DCR_View.aspx.cs" Inherits="MasterFiles_Reports_tsr_TsrRpt_DCR_View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Attendance Report</title>
        <link type="text/css" rel="Stylesheet" href="../../../css/Report.css" />
        <link href="../../../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
        <link href="../../../css/style.css" rel="stylesheet" />
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
        </style>
		<style type="text/css">
		/*popup image*/
		
		.phimg img {
			border-radius: 5px;
			cursor: pointer;
			transition: 0.3s;
		}		
		.phimg img:hover {opacity: 0.7;}
		
		/* The Modal (background) */
		.modal {
			display: none; /* Hidden by default */
			position: fixed; /* Stay in place */
			z-index: 1; /* Sit on top */
			padding-top: 100px; /* Location of the box */
			left: 0;
			top: 0;
			width: 100%; /* Full width */
			height: 100%; /* Full height */
			overflow: auto; /* Enable scroll if needed */
			background-color: rgb(0,0,0); /* Fallback color */
			background-color: rgba(0,0,0,0.9); /* Black w/ opacity */
		}
		
		/* Modal Content (Image) */
		.modal-content {
			margin: auto;
			display: block;
			width: 80%;
			height: 80%;
		}
		
		/* Caption of Modal Image (Image Text) - Same Width as the Image */
		#caption {
			margin: auto;
			display: block;
			width: 80%;
			max-width: 700px;
			text-align: center;
			color: #ccc;
			padding: 10px 0;
			height: 150px;
		}
		
		/* Add Animation - Zoom in the Modal */
		.modal-content, #caption { 
			-webkit-animation-name: zoom;
			-webkit-animation-duration: 0.6s;
			animation-name: zoom;
			animation-duration: 0.6s;
		}
		
		@-webkit-keyframes zoom {
			from {-webkit-transform:scale(0)} 
			to {-webkit-transform:scale(1)}
		}
		
		@keyframes zoom {
			from {transform:scale(0)} 
			to {transform:scale(1)}
		}
		
		/* The Close Button */
		.close {
			position: absolute;
			top: 15px;
			right: 35px;
			color: #f1f1f1;
			font-size: 40px;
			font-weight: bold;
			transition: 0.3s;
		}
		
		.close:hover,
		.close:focus {
			color: #bbb;
			text-decoration: none;
			cursor: pointer;
		}
		
		/* 100% Image Width on Smaller Screens */
		@media only screen and (max-width: 700px){
			.modal-content {
				width: 100%;
			}
		}
	</style>        

        <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?key=AIzaSyBG-dwpBFBzcI8Y5AOoBK2H8SFQh9bRnY4"></script>     

        <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>        
    </head>
    <body>
        <form id="form1" runat="server">
             <asp:ScriptManager ID="ScriptManager1" runat="server"> </asp:ScriptManager>
            <div class="container" style="max-width: 100%; width: 95%; text-align: right">
                <img  style="cursor: pointer; float: right;" src="/img/excel.png" alt="" width="40" height="40" id="btnExport" />
            </div>

            <asp:HiddenField runat="server" Value="" ID="DCRdt" />
            <asp:HiddenField ID="hidn_sf_code" runat="server" />
            <asp:HiddenField ID="hFYear" runat="server" />
            <asp:HiddenField ID="hTYear" runat="server" />
            <asp:HiddenField ID="hFMonth" runat="server" />
            <asp:HiddenField ID="hTMonth" runat="server" />
            <asp:HiddenField ID="subDiv" runat="server" />
            
            <div class="row">
                <div class="col-sm-8">
                    <asp:Label ID="Label2" runat="server" Text="Attendance Summary" Style="margin-left: 10px; font-size: x-large"></asp:Label>
                </div>
            </div>
            <div class="row" style="margin: 6px 0px 0px 11px;">
                <asp:Label ID="Label4" Visible="false" Text="Field Force Name :" runat="server" Style="font-size: larger" ></asp:Label>
                <asp:Label ID="lblsf_name" Visible="false" runat="server" Style="font-size: larger"></asp:Label>
            </div>
            <div class="row" style="margin: 0px 0px 0px 5px;">
                <br />
                <br />
                <div id="content">
                    <table id="FFTbl" class="newStly" style="border-collapse: collapse;">
                        <thead>
                            <tr>
                                <th>SLNo.</th>
                                
                                <th>State</th>
                                <th>Zone</th>
                                <th>Area</th>
                                <th>Emp Id</th>  
                                <th>User Name</th>
                                <th>Contact No</th>
                                <th>HQ</th>                                                      
                                <th>Date</th>
                                <th>Time</th>
                                <th>Work Type</th>
                                <th>Leave Type</th>
                                <th>Distributors</th>
                                <th>Route Name</th>
                                <th>EC</th>
								<th>First Call Time</th>
								<th>Las Call Time</th>
                                <th>TPD(Yes/No)</th>
                                <th>Reason</th>                       
                                <th>Value</th>                                
                                <th>Image</th>

                                <%--<th>SLNo.</th>                                
                                <th>State</th>
                                <th>Zone</th>
                                <th>Area</th>
                                <th>Emp Id</th>                                
                                <th>User Name</th>
                                <th>Contact No</th>
                                <th>HQ</th>
                                <th>Date</th>
                                <th>Login</th>
                                <th>Log Out</th>                               
                                <th>Work Type</th>
                                <th>Leave Type</th>
                                <th>Distributors</th>
                                <th>Selected Beats</th>
                                <th>EC</th>
                                <th>TPD(Yes/No)</th>
                                <th>Value</th>
                                <th>Image</th>--%>
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
             
          
			 <div id="myModal" class="modal" style="opacity: 1;">
                <!-- The Close Button -->
                <span class="close" style="opacity: 1;">&times;</span>
                <!-- Modal Content (The Image) -->
                <img alt="" class="modal-content" id="img0o1" />
                <!-- Modal Caption (Image Text) -->
                <div id="caption"></div>
            </div>
        </form>
        <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script type="text/javascript">
            var tbplan = [], dcrcalls = [], stendtimes = [], rtscount = [], sfusers = [], tpdates = [], dayOrders = [], dayProfilePic = [], newRPOB = [];
            var bDat = []; var sfDate = []; var sNum = 1; var eachrow = 0;
            function getUsers() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "TsrRpt_DCR_View.aspx/getSFdets",
                    data: "{'Div':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        sfusers = JSON.parse(data.d) || [];
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            }
            function getDayPlan() {
                return $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    url: "TsrRpt_DCR_View.aspx/getDayPlan",
                    dataType: "json",
                    success: function (data) {
                        tbplan = JSON.parse(data.d) || [];
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
                    url: "TsrRpt_DCR_View.aspx/getNewRetailerPOB",
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
                    url: "TsrRpt_DCR_View.aspx/getDaywiseCalls",
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
                    url: "TsrRpt_DCR_View.aspx/getAttendance",
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
                    url: "TsrRpt_DCR_View.aspx/getRetailerCount",
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
                    url: "TsrRpt_DCR_View.aspx/GetTpDates",
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
                    url: "TsrRpt_DCR_View.aspx/getOrders",
                    dataType: "json",
                    success: function (data) {
                        dayOrders = JSON.parse(data.d) || [];
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            }
            function getProfilePic() {
                return $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    url: "TsrRpt_DCR_View.aspx/getProfilePic",
                    dataType: "json",                    
                    success: function (data) {
                       
                        dayProfilePic = JSON.parse(data.d) || [];
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            }
            function loadData() {
                $.when(getStartEnd(), getTpdates(), getDayPlan(), getProdCalls(), getNewRetailerPOB(), getRetailerCount(), getProfilePic(), getOrders()).then(function () {
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
                for (var i = 0; i < tpdates.length; i++) {
                    for (var j = 0; j < sfusers.length; j++) {
                        let actcalls = 0;
                        let totcalls = 0;
                        let prodcalls = 0;
                        let productivity = 0;
                        let phoneOrderCnt = 0;
                        let nretailers = 0;
                        let nretailerspob = 0;
                        let profilepic = '';
                        str += `<tr><td>${++slno}</td><td>${sfusers[j].StateName}</td><td>${sfusers[j].Zone_name}</td><td>${sfusers[j].Area_name}</td><td>${sfusers[j].Employee_Id}</td><td>${sfusers[j].Sf_Name}</td><td>${sfusers[j].SF_Mobile}</td><td>${sfusers[j].Sf_HQ}</td><td>${tpdates[i].actdt}</td>`;
                        let tbday = tbplan.filter(function (a) {
                            return a.sf_Code == sfusers[j].SF_Code && a.dt == tpdates[i].dt;
                        });
                        nretailers = newRPOB.filter(function (a) {
                            return a.sf_code == sfusers[j].SF_Code && a.Activity_Date == tpdates[i].dt;
                        }).map(function (a) { return a.NRC; }).toString() || 0;

                        nretailerspob = newRPOB.filter(function (a) {
                            return a.sf_code == sfusers[j].SF_Code && a.Activity_Date == tpdates[i].dt;
                        }).map(function (a) { return a.NRVAL; }).toString() || 0;

                        profilepic = dayProfilePic.filter(function (a) {
                            return a.sf_code == sfusers[j].SF_Code && a.Insert_Date == tpdates[i].dt;
                        }).map(function (a) { return '<img src="' + a.ImgUrl + '" alt="" class="picc" />'; /*a.ImgUrl*/ }).toString() || '';

                        console.log(profilepic)

                        let startOrderTime = '';
                        let jointwrk = tbday.map(function (a) {
                            return a.worked_with_name
                        }).join(',');
                        let todayWtype = tbday.map(function (a) {
                            return a.Wtype
                        }).join(',');
                        let todayLtype = tbday.map(function (a) {
                            return a.Leave_Name
                        }).join(',');

                        let todayTPD = tbday.map(function (a) {
                            return a.tpdyn
                        }).join(',');

                        let remarks = tbday.map(function (a) {
                            return a.remarks
                        }).join(',');

                        let trimg = tbday.map(function (a) {
                            return '<img src="' + a.ImgUrl + '" alt="" class="picc" />';
                        });




                        let mymap = new Map();
                        let dismap = new Map();
                        let selbeats = [];
                        let selstks = [];
                        let stkadrs = [];
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
                        if (tbday.length > 0) {
                            startOrderTime = tbday[0].lgtime;
                            let beats = selbeats.map(function (a) {
                                return a.ClstrName
                            }).join(',');
                            let stkscd = selstks.map(function (a) {
                                return a.StkName
                            }).join(',');
                            let stkadr = stkadrs.map(function (a) {
                                return a.Stockist_Address
                            }).join(',');
                            for (var k = 0; k < selbeats.length; k++) {
                                let filtcalls = parseInt(rtscount.filter(function (a) {
                                    return a.Territory_Code == selbeats[k].cluster
                                }).map(function (el) {
                                    return el.cnt
                                }));
                                actcalls += parseInt(isNaN(filtcalls) == true ? 0 : filtcalls);
                            }
                            str += `<td>${startOrderTime}</td><td>${todayWtype}</td><td>${todayLtype}</td><td>${stkscd}</td><td>${beats}</td>`;
                        }
                        else {
                            str += `<td>Absent</td><td></td><td></td><td></td><td></td>`;
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

                        //str += `<td>${callsmade[0].EfCall}</td><td>${todayTPD}</td><td>${remarks}</td>`;

                         if (callsmade.length > 0) {
                            var stime = new Date(callsmade[0].ActDate + " " + callsmade[0].minTime);
                            var etime = new Date(callsmade[0].ActDate + " " + callsmade[0].maxTime);
                            var timediff = (etime - stime) / 1000;
                            var hours = parseInt(timediff / 3600) % 24;
                            var minutes = parseInt(timediff / 60) % 60;
                            var seconds = timediff % 60;
                            var result = (hours < 10 ? "0" + hours : hours) + ":" + (minutes < 10 ? "0" + minutes : minutes) + ":" + (seconds < 10 ? "0" + seconds : seconds);

                            str += `<td>${callsmade[0].EfCall}</td><td>${callsmade[0].minTime}</td><td>${callsmade[0].maxTime}</td>`;
                        }
                        else {
                            str += `<td></td><td></td><td></td>`;
                        }
                        if (callsmade.length > 0) {
                            //productivity = (parseInt(callsmade[0].EfCall * 100) / parseInt(callsmade[0].TCall));
                            //
                            str += `<td>${todayTPD}</td><td>${remarks}</td>`;
                            //str += `<td>${callsmade[0].EfCall}</td>`;
                        }
                        else {
                            str += `<td></td><td></td>`;
                        }

                       
                        //str += `<td>${nretailers}</td><td>${nretailerspob}</td><td>${phoneOrderCnt}</td>`;
                        if (daystend.length > 0) {
                            var login = new Date(daystend[0].Start_DtTime);
                            var lout = new Date(daystend[0].End_DtTime);
                            var ltimediff = (lout - login) / 1000;
                            var hours = parseInt(ltimediff / 3600) % 24;
                            var minutes = parseInt(ltimediff / 60) % 60;
                            var seconds = ltimediff % 60;
                            var result = (hours < 10 ? "0" + hours : hours) + ":" + (minutes < 10 ? "0" + minutes : minutes) + ":" + (seconds < 10 ? "0" + seconds : seconds);

                            //str += `<td>${(daystend.length < 1) ? startOrderTime : daystend[0].Start_Time}</td><td>${daystend[0].End_Time}</td><td>${isNaN(ltimediff) == true ? '' : result}</td><td>${(daystend.length < 1) ? Start_Lat : daystend[0].Start_Lat}</td><td>${(daystend.length < 1) ? Start_Long : daystend[0].Start_Long}</td><td class="place"></td>`;
                        }
                        else {
                            //str += `<td></td><td></td><td></td><td></td><td></td>`;
                        }
                        if (callsmade.length > 0) {
                            var stime = new Date(callsmade[0].ActDate + " " + callsmade[0].minTime);
                            var etime = new Date(callsmade[0].ActDate + " " + callsmade[0].maxTime);
                            var timediff = (etime - stime) / 1000;
                            var hours = parseInt(timediff / 3600) % 24;
                            var minutes = parseInt(timediff / 60) % 60;
                            var seconds = timediff % 60;
                            var result = (hours < 10 ? "0" + hours : hours) + ":" + (minutes < 10 ? "0" + minutes : minutes) + ":" + (seconds < 10 ? "0" + seconds : seconds);

                            //str += `<td>${callsmade[0].minTime}</td><td>${callsmade[0].maxTime}</td><td>${isNaN(timediff) == true ? '' : result}</td>`;
                        }
                        else {
                            //str += `<td></td><td></td><td></td>`;
                        }
                        let daytotal = parseFloat(isNaN(todayOrderval) == true ? 0 : todayOrderval);
                        gtotal = parseFloat(gtotal) + parseFloat(daytotal);
                        str += `<td>${daytotal.toFixed(2)}</td><td>${profilepic}<td></tr>`;

                        //if (daytotal.length > 0) {
                        //    str += `<td>${daytotal.toFixed(2)}</td>`;
                        //}
                        //else {
                        //    str += `<td><td>`;
                        //}

                        //if (trimg.length > 0) {
                        //    str += `<td>${trimg}<td>`;
                        //}
                        //else {
                        //    str += `<td><td>`;
                        //}
                        //str +=`</tr>`
                    }
                }
                let fstr = `<tr><th colspan="19" style='text-align:right;'>Total</th><th>${gtotal.toFixed(2)}</th></tr>`
                $("#FFTbl>tbody").append(str);
                $("#FFTbl>tfoot").append(fstr);
            }
            $(document).ready(function () {
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
                    var tets = 'Attendance Report' + '.xls';   //create fname

                    link.download = tets;
                    link.href = uri + base64(format(template, ctx));
                    link.click();
                });
            });
        </script>
        <script type="text/javascript">

            function imgPOP(x) {
                // Get the modal
                var modal1 = document.getElementById('myModal');

                // Get the image and insert it inside the modal - use its "alt" text as a caption
                var img = document.getElementById('myImg');
                var modalImg = document.getElementById("img01");
                var captionText = document.getElementById("caption");
                //modal1.style.display = "block";
                $('#myModal').css("display", "block");
                modalImg.src = x.src;
                captionText.innerHTML = x.alt;
            }

            // Get the <span> element that closes the modal
            var span = document.getElementsByClassName("close")[0];

            // When the user clicks on <span> (x), close the modal
            span.onclick = function () {
                $('#myModal').css("display", "none");
                //modal1.style.display = "none";
            }
	</script>

    </body>
</html>
