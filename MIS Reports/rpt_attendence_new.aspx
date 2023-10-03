<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_attendence_new.aspx.cs" Inherits="MIS_Reports_rpt_attendence_new" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Attendance</title>
     <%--<link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />--%>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <style type="text/css">
        body
        {
            padding: 10px 25px;
        }
        .modal
        {
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
        .ttable tr:nth-child(odd) {
            background-color: #dbe2f9;
        }

        .ttable td {
            padding: 5px 2px;
            width: 14px;
            text-align: justify;
            border: solid 1px black;
        }

        .ttable th {
            padding: 4px 2px;
            color: #fff;
            background: #819DFB url(Images/grid-header.png) repeat-x top;
            border-left: solid 1px #525252;
            font-size: 0.9em;
        }


        .ttable table {
            overflow: hidden;
        }

        .ttable tr:hover {
            background-color: #ffa;
        }

        .ttable td, th {
            position: relative;
        }

            .ttable td:hover::after,
            .ttable th:hover::after {
                content: "";
                background-color: #ffa;
                left: 0;
                top: -5000px;
                height: 10000px;
                width: 100%;
                z-index: -1;
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
            dUsr = []; dUsrWt = [];
            var today = new Date();
            var dd = String(today.getDate()).padStart(2, '0');
            var mm = String(today.getMonth() + 1).padStart(2, '0'); 
            var yyyy = today.getFullYear();
            genReport = function () {
                if (dUsr.length > 0) {
                    str = '<th> <p style="margin: 0 0 0px;font-size:10px;width:150px;">User Name</p> </th><th> <p style="margin: 0 0 0px;">Designation</p> </th><th> <p style="margin: 0 0 0px;">DOJ</p> </th><th> <p style="margin: 0 0 0px;">Employee ID</p> </th><th> <p style="margin: 0 0 0px;">HQ</p> </th><th> <p style="margin: 0 0 0px;">Reporting SF</p> </th><th> <p style="margin: 0 0 0px;">DeactiveDate</p> </th><th> <p style="margin: 0 0 0px;">SF Status</p> </th><th> <p style="margin: 0 0 0px;font-size:10px;width:150px;">Reporting Manager</p> </th><th> <p style="margin: 0 0 0px;">Contact No</p> </th><th> <p style="margin: 0 0 0px;">StateName</p> </th>';
                    str += '<th> <p style="margin: 0 0 0px;">TotalDays</p> </th><th> <p style="margin: 0 0 0px;">WorkingDays</p> </th><th> <p style="margin: 0 0 0px;">OtherOfficialWork</p> </th><th> <p style="margin: 0 0 0px;">Leave</p> </th><th> <p style="margin: 0 0 0px;">Holiyday</p> </th><th> <p style="margin: 0 0 0px;">MD</p> </th><th> <p style="margin: 0 0 0px;">Weekoff</p> </th><th> <p style="margin: 0 0 0px;">Days</p> </th>';
                    var nmdys = daysInMonth('<%=FMonth%>','<%=FYear%>');
                    for (var i = 1; i <= nmdys; i++) {
                        str += '<th> <p style="margin: 0 0 0px;">'+(i)+'</p> </th>';                    }
                   
                    $('#atten_Table thead').append('<tr class="mainhead">' + str + '</tr>');
                    for (var j = 0; j < dUsr.length; j++) {
                        var strr = '', strf = '', wtyp = 0, lv = 0, wkof = 0, hldy = 0, otwrk=0,mdcnt=0;
                        strf = '<td>' + dUsr[j].User_Name + '</td><td>' + dUsr[j].Desig + '</td><td>' + dUsr[j].DOJ + '</td><td>' + dUsr[j].Emp_ID + '</td><td>' + dUsr[j].HQ + '</td><td>' + dUsr[j].Reporting_To_SF + '</td><td>' + ((dUsr[j].Deactive_Date == null) ? '' : dUsr[j].Deactive_Date)+ '</td><td>' + dUsr[j].SF_Status1 + '</td><td>' + dUsr[j].Reporting_Manager + '</td><td>' + dUsr[j].SR_Contact_No + '</td><td>' + dUsr[j].StateName + '</td><td>' + nmdys + '</td>';
                        
                        var urwt = dUsrWt.Customers.filter(function (a) {
                            return (a.sf_code == dUsr[j].SF_Code);
                        });
                        urwt.sort(function (a, b) {
                            return a.ddt1 - b.ddt1;
                        });
                        var misdt = dUsrWt.Customers1.filter(function (a) {
                            return (a.Sf_Code == dUsr[j].SF_Code);
                        });
                        misdt.sort(function (a, b) {
                            return a.md - b.md;
                        });
                        if (urwt.length > 0) {
                            var h = 1;
                            for (var k = 0; k < urwt.length; k++) { //looping throw worktypes
                                while (h <= nmdys) { //whole days of an month
                                    var newDate = new Date(<%=FYear%>, <%=FMonth%>-1, h);
                                    
                                    //else {
                                        if (h == urwt[k].ddt1) {
                                            if ('<%=FMonth%>' == parseInt(mm) && '<%=FYear%>' == parseInt(yyyy) && h > parseInt(dd)) {
                                                strr += '<td><span></span></td>';
                                                h++;
                                                break;
                                            }
                                            else {
                                                var mdclr = misdt.filter(function (e) {
                                                    return ((e.Sf_Code == dUsr[j].SF_Code) && e.md == urwt[k].ddt1)
                                                });
                                                if (mdclr.length > 0) {
                                                    strr += '<td><span style="Background-color:orange;display:inline-block;width:20px;font-size:10px;">' + urwt[k].WT + '</span></td>';

                                                }
                                                else {
                                                    strr += '<td><span style="' + ((urwt[k].WT).includes('L') ? 'Background-color: yellow; ' : ((((urwt[k].WT) == 'WO') || (urwt[k].WT) == 'H') ? 'Background-color : lightgreen;' : 'Background-color : none;')) + 'display:inline-block;width:20px;font-size:10px;">' + urwt[k].WT + '</span></td>';

                                                }
                                                h++;
                                                if ((urwt[k].WT).includes('L')) {
                                                    lv++;
                                                }
                                                else if (urwt[k].WT == 'WO') {
                                                    wkof++;
                                                }
                                                else if (urwt[k].WT == 'H') {
                                                    hldy++;
                                                }
                                                else if (urwt[k].WT == 'FW') {
                                                    wtyp++;
                                                }
                                                else {
                                                    otwrk++;
                                                }
                                                break;
                                            }

                                        }
                                        else {
										if (newDate.getDay() == 0) { // check if it is sunday
                                        strr += '<td><span style="color:red;display:inline-block;font-size:10px;width:20px;">S</span></td>';
                                        h++;
                                        wkof++;
										if(urwt[k].ddt1 != (h-1))
										k--;
                                        break;
                                       }
									    else{
                                            if ('<%=FMonth%>' == parseInt(mm) && '<%=FYear%>' == parseInt(yyyy) && h > parseInt(dd)) {
                                                strr += '<td><span></span></td>';
                                                h++;
                                            }
                                            else {
                                                strr += '<td><span style="color:red;display:inline-block;font-size:10px;width:20px;">MD</span></td>';
                                                h++;
                                                mdcnt++;
                                            }
											}

                                        }
                                //}
                                }
                            }
                            while (h <= nmdys) { 
                                var newDate = new Date(<%=FYear%>, <%=FMonth%>-1, h);
                                if (newDate.getDay() == 0) { // check if it is sunday
                                    strr += '<td><span style="color:red;display:inline-block;font-size:10px;width:20px;">S</span></td>';
                                    h++;
                                    wkof++;
                                }
                                else {
                                    if ('<%=FMonth%>' == parseInt(mm) && '<%=FYear%>' == parseInt(yyyy) && h > parseInt(dd)) {
                                        strr += '<td><span></span></td>';
                                        h++;
                                    }
                                    else {
                                        strr += '<td><span style="color:red;display:inline-block;font-size:10px;width:20px;">MD</span></td>';
                                        h++;
                                        mdcnt++;
                                    }
                                }
                            }
                        }
                        else {
                            //var h = 1;
							if (dUsr[j].SF_Status == '3') {
                                for (var m = 1; m <= nmdys; m++) {
                                    strr += '<td><span></span></td>';
                                }
                                
                            } 
							else{
                            for (var m = 1; m <= nmdys; m++) {
                                var newDate = new Date(<%=FYear%>, <%=FMonth%>-1, m);
                                if (newDate.getDay() == 0) { // check if it is sunday
                                    strr += '<td><span style="color:red;display:inline-block;font-size:10px;width:20px;">S</span></td>';
                                    h++;
                                    wkof++;
                                }
                                else {
                                    if ('<%=FMonth%>' == parseInt(mm) && '<%=FYear%>' == parseInt(yyyy) && m > parseInt(dd)) {
                                        strr += '<td><span></span></td>';
                                    }
                                    else {
                                        strr += '<td><span style="color:red;display:inline-block;font-size:10px;width:20px;">MD</span></td>';
                                        mdcnt++;
                                    }
                                }
                            }
							}
                        }
                        strf += '<td>' + wtyp + '</td><td>' + otwrk + '</td><td>' + lv + '</td><td>' + hldy + '</td><td>' + mdcnt+'</td><td>' + wkof+'</td><td>WT</td>';
                        strf += strr;
                        $('#atten_Table tbody').append('<tr>' + strf + '</tr>');

                        }
                    }
                }
            $('#atten_Table tr').remove();
            var len = 0;
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_attendence_new.aspx/getUserdata",
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
                url: "rpt_attendence_new.aspx/getUserWTdata",
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
            function daysInMonth(month, year) {
                return new Date(year, month, 0).getDate();
            }
            genReport();
            gethint();
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
                var tets = "Attendance Worktypewise <%=strFMonthName%> - <%=FYear%>" + ".xls";

                link.download = tets;
                link.href = uri + base64(format(template, ctx));
                link.click();
                event.preventDefault();
            })
        });
        function gethint() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_attendence_new.aspx/gethints",
                data: '{"divcode":"<%=Session["div_code"]%>"}',
                dataType: "json",
                success: function (data) {
                    hint = JSON.parse(data.d) || [];
                    str = '<tr>';

                    for (var i = 0; i < hint.length; i++) {
                        str += "<td style='color:#162dd3;font-weight: 900;'>" + hint[i].WType_SName + "</td><td  style='color:#cb16a4;font-weight: 700;'>" + hint[i].Wtype + "</td>";
                        if (((i + 1) % 3) == 0) {
                            str += "</tr>"
                            $(".Numbers").append(str); str = '<tr>';
                        } else {
                            continue;
                        }
                    }
                    $(".Numbers").append("<td style='color:#162dd3;font-weight: 900;'>MD</td><td  style='color:#cb16a4;font-weight: 700;'>Missed Date</td>");

                }
            });

        }
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
				<table id="atten_Table" border="1" cellspacing="2" cellpadding="2" class="ttable" style="border-color:Black;border-width:1px;border-style:Solid;font-size:9pt;width:98%;">
					<thead>
					</thead>
					<tbody>
					</tbody>
					<tfoot>
					</tfoot>
				</table>
			</div>
		</div>
                    <div align="center">
                        <br />
                        <br />
                        <h3 style="color: #d02c64;">Work Type Name</h3><br />
                        <table class="Numbers" id="grid" cellpadding="0" cellspacing="0">
                            <tr></tr>
                        </table>
						<br/>
                    </div>
		
	</asp:Panel>
            <div class="loading" align="center">
			Loading. Please wait.<br />
			<br />
		</div>
        </div>
    </form>
</body>
</html>
