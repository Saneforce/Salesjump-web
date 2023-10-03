<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_SNJ_Attendace.aspx.cs" Inherits="MIS_Reports_rpt_SNJ_Attendace" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <title></title>
    <style type="text/css">
        body
        {
            padding: 10px 25px;
        }
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }
        .loading
        {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
        #grid {
            border: 1px solid #ddd;
            border-collapse: collapse;
        }

            #grid th {
                position: sticky;
                top: 0;
                background: #6c7ae0;
                text-align: center;
                font-weight: normal;
                font-size: 15px;
                color: white;
            }


            #grid td, #grid th {
                padding: 5px;
                border: 1px solid #ddd;
            }
         
    </style>
   <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
	<script type="text/javascript">
        $(document).ready(function () {
            dUsrWt = []; dUsr = [];
            var date1 = new Date('<%=Fdtate%>');
            var date2 = new Date('<%=Tdate%>');
            var daysBetween = []; var days = [];
            var currentDate = date1; var timeDifference = date2 - date1;
            var numDays = Math.floor(timeDifference / (1000 * 60 * 60 * 24));
            while (currentDate <= date2) { // Get dates between selected Dates
                daysBetween.push(currentDate.toISOString().split('T')[0]);
                days.push(currentDate.getDate());
                currentDate.setDate(currentDate.getDate() + 1);
            }
            var weekday = new Array();
            weekday[0] = "Sun";
            weekday[1] = "Mon";
            weekday[2] = "Tue";
            weekday[3] = "Wed";
            weekday[4] = "Thu";
            weekday[5] = "Fri";
            weekday[6] = "Sat";

            genReport = function () {
                if (dUsr.length > 0) {
                    str2 = "";
                    str = '<th  rowspan=2>Sl.NO</th><th  rowspan=2>Employee Name</th><th rowspan=2>StateName</th>';
                    for (var i = 0; i < days.length; i++) {
                        str += '<th>' + days[i] + '</th>'; //loop for date header
                        var newDate = new Date(daysBetween[i]);
                        str2 += '<th> ' + weekday[newDate.getDay()] + '</th>';
                    }
                    $('#atten_Table thead').append('<tr class="mainhead">' + str + '<th rowspan=2>Total Leave Taken</th><th rowspan=2>Remarks</th></tr>');
                    $('#atten_Table thead').append('<tr class="mainhead1">' + str2 + '</tr>');
                    var slno = 1;
                    for (var j = 0; j < dUsr.length; j++) {  //looping threw whole users
                        var strf = '', lv = 0;
                        strf = '<td>' + (slno++) + '</td><td>' + dUsr[j].User_Name + '</td><td>' + dUsr[j].StateName + '</td>';
                        var urwt = dUsrWt.filter(function (a) { //ckh if FO has worktypes for selected dates
                            return (a.sf_code == dUsr[j].SF_Code);
                        });
                        urwt.sort(function (a, b) {
                            return a.ddt1 - b.ddt1;
                        });
                        if (urwt.length > 0) {
                            for (var k = 0; k < days.length; k++) { // loop for data to fill under dates
                                var newDate = new Date(daysBetween[k]);
                                var tst = 0;
                                for (var l = 0; l < urwt.length; l++) {
                                    if (urwt[l].ddt1 == days[k]) {
                                        if (urwt[l].Wrktyp == 'L') { //count if worktype is leave
                                            strf += '<td><span style="background-color:#FFFF00;display:inline-block;font-size:10px;width:20px;">' + urwt[l].Wrktyp + '</span></td>';
                                            lv++;
                                        }
                                        else {
                                            strf += '<td>' + urwt[l].Wrktyp + '</td>';
                                        }
                                            tst = 1;
                                        }
                                    }
                                if (tst == 0) {
                                    if (newDate.getDay() == 0) {									//find sunday Holiday
									if('<%=Session["div_code"]%>'=='176')
									{
									strf += '<td style="background-color:#B0E0E6;">S</td>';
									}
									else{
                                        strf += '<td style="background-color:#B0E0E6;">H</td>';
                                    }
									}
                                    else {
                                        strf += '<td>A</td>';
                                    }
                                        
                                }
                            }
                        }
                        else {
                            for (var k = 0; k < days.length; k++) {
                                var newDate = new Date(daysBetween[k]);
                                if (newDate.getDay() == 0) {
								if('<%=Session["div_code"]%>'=='176')
									{
									strf += '<td style="background-color:#B0E0E6;">S</td>';
									}
									else{
                                    strf += '<td style="background-color:#B0E0E6;">H</td>';
									}
                                }
                                else {
                                    strf += '<td>A</td>';
                                }
                            }
                        }
                        $('#atten_Table tbody').append('<tr>' + strf + '<td>'+lv+'</td><td></td></tr>');
                    }
                }
            }
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_SNJ_Attendace.aspx/getUserdata",
                dataType: "json",
                success: function (data) {
                    len = data.d.length;
                    dUsr = JSON.parse(data.d) || [];//data.d;
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
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_SNJ_Attendace.aspx/getUserWTdata",
                dataType: "json",
                success: function (data) {
                    len = data.d.length;
                    dUsrWt = JSON.parse(data.d) || [];//data.d;


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
            $('#btnExcel').on('click', function () {
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
                htmls = document.getElementById("pnlContents").innerHTML;


                var ctx = {
                    worksheet: 'Worksheet',
                    table: htmls
                }
                var link = document.createElement("a");
                var tets = "Attendance_Report_SNJ.xls";

                link.download = tets;
                link.href = uri + base64(format(template, ctx));
                link.click();
                event.preventDefault();
            });
            genReport();
        });
        $(document).on('click', "#btnPrint", function () {
            var originalContents = $("body").html();
            var printContents = $("#content").html();
            $("body").html(printContents);
            window.print();
            window.location.reload();
        });
        
		</script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="row">
        <div class="col-sm-8" style="padding-left: 5px;">
        </div>
        <div class="col-sm-4" style="text-align: right">
           <asp:LinkButton ID="btnPrint" runat="Server" style="padding: 0px 20px;" class="btn btnPrint" />
            <a name="btnExcel" id="btnExcel" type="button" href="#" class="btn btnExcel"></a>
			<a name="btnClose" id="btnClose" type="button" href="javascript:window.open('','_self').close();" class="btn btnClose"></a>
        </div>
    </div>
            <asp:Panel ID="pnlContents" runat="server" Width="100%">
                <div class="row">		
			<div class="col-sm-8" style="padding-left: 5px;">
                <asp:Label ID="lblHead"  SkinID="lblMand" style="font-weight:bold;FONT-SIZE: 16pt;COLOR: black;FONT-FAMILY:Times New Roman;float: left;padding: 5px;" 
                runat="server"></asp:Label>
				 
			</div>
		</div>
		<div class="row">
			<br />
			<asp:Label ID="lblIDsf_name" Text="Team:" Font-Bold="true"  Font-Underline="true" ForeColor="#476eec" runat="server" ></asp:Label>
                            <asp:Label ID="lblsf_name" runat="server"   Font-Underline="true" SkinID="lblMand"></asp:Label>
			<br />
			<div>
				<table id="atten_Table" class="newStly table table-responsive">
					<thead>
					</thead>
					<tbody>
					</tbody>
					<tfoot>
					</tfoot>
				</table>
                 <%--<div id="vertical-placeholder">Vertical Placeholder</div>--%>
			</div>
		</div>
                <div align="center">
                        <br />
                        <br />
                        <h3 style="color: #d02c64;">Work Type Name</h3><br />
                        <table class="Numbers" id="grid" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style='color:#162dd3;font-weight: 900;'>P</td><td  style='color:#cb16a4;font-weight: 700;'>Present</td>
                                <td style='color:#162dd3;font-weight: 900;'>M</td><td  style='color:#cb16a4;font-weight: 700;'>Meeting</td>
                                <td style='color:#162dd3;font-weight: 900;'>H</td><td  style='color:#cb16a4;font-weight: 700;'>Holiday</td>
                                <td style='color:#162dd3;font-weight: 900;'>L</td><td  style='color:#cb16a4;font-weight: 700;'>Leave</td>
                                <td style='color:#162dd3;font-weight: 900;'>A</td><td  style='color:#cb16a4;font-weight: 700;'>Absent</td>
                            </tr>
                        </table>
						<br/>
                    </div>
                </asp:Panel>
        </div>
    </form>
</body>
</html>
