<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptDaywiseCalls.aspx.cs"
    Inherits="MIS_Reports_rptDaywiseCalls" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PRODUCTIVE CALLS</title>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <style type="text/css">
        
    </style>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var rshq = [];
            var dRSF = [];
            var lvl = 0;
            //          var SFCode = $("#<%=HSFCode.ClientID%>").val();
            //            $.ajax({
            //                type: "POST",
            //                contentType: "application/json; charset=utf-8",
            //                async: false,
            //                url: "rptDaywiseCalls.aspx/GetReportingToSF",
            //                data: "{'sfCode':'" + SFCode + "'}",
            //                dataType: "json",
            //                success: function (data) {
            //                    dRSF = data.d;
            //                },
            //                error: function (jqXHR, exception) {
            //                    console.log(jqXHR);
            //                    console.log(exception);
            //                    var msg = '';
            //                    if (jqXHR.status === 0) {
            //                        msg = 'Not connect.\n Verify Network.';
            //                    } else if (jqXHR.status == 404) {
            //                        msg = 'Requested page not found. [404]';
            //                    } else if (jqXHR.status == 500) {
            //                        msg = 'Internal Server Error [500].';
            //                    } else if (exception === 'parsererror') {
            //                        msg = 'Requested JSON parse failed.';
            //                    } else if (exception === 'timeout') {
            //                        msg = 'Time out error.';
            //                    } else if (exception === 'abort') {
            //                        msg = 'Ajax request aborted.';
            //                    } else {
            //                        msg = 'Uncaught Error.\n' + jqXHR.responseText;
            //                    }
            //                    alert(msg);
            //                }
            //            });

            //            // console.log(dRSF);

            //            $.ajax({
            //                type: "POST",
            //                contentType: "application/json; charset=utf-8",
            //                async: false,
            //                url: "rptDaywiseCalls.aspx/GetTCECdayCall",
            //                data: "{'sfCode':'" + SFCode + "', 'FYear':'" + '2018' + "', 'FMonth':'" + FMonth + "', 'SubDivCode':'" + SubDiv + "'}",
            //                dataType: "json",
            //                success: function (data) {
            //                    dRSF = data.d;
            //                },
            //                error: function (jqXHR, exception) {
            //                    console.log(jqXHR);
            //                    console.log(exception);
            //                    var msg = '';
            //                    if (jqXHR.status === 0) {
            //                        msg = 'Not connect.\n Verify Network.';
            //                    } else if (jqXHR.status == 404) {
            //                        msg = 'Requested page not found. [404]';
            //                    } else if (jqXHR.status == 500) {
            //                        msg = 'Internal Server Error [500].';
            //                    } else if (exception === 'parsererror') {
            //                        msg = 'Requested JSON parse failed.';
            //                    } else if (exception === 'timeout') {
            //                        msg = 'Time out error.';
            //                    } else if (exception === 'abort') {
            //                        msg = 'Ajax request aborted.';
            //                    } else {
            //                        msg = 'Uncaught Error.\n' + jqXHR.responseText;
            //                    }
            //                    alert(msg);
            //                }
            //            });


            //            var leng = 0;
            //            if (dRSF.length > 0) {
            //                leng = dRSF.length;
            //                Rf = dRSF.filter(function (a) { return (a.RSF_Code == 'admin'); });
            //                if (Rf.length > 0) {
            //                    var Rf1;
            //                    var strk = '';
            //                    leng = leng - Rf.length;

            //                    for (var l = 0; l < Rf.length; l++) {
            //                        rshq.push({ sfCode: Rf[l].Sf_Code, RSFCode: Rf[l].RSF_Code, Desig: Rf[l].Designation, sfName: Rf[l].sf_Name, level: '1' });
            //                        lvl = lvl > 1 ? lvl : 1;
            //                        Rf1 = dRSF.filter(function (a) { return (a.RSF_Code == Rf[l].Sf_Code); });
            //                        if (Rf1.length > 0) {
            //                            for (var k = 0; k < Rf1.length; k++) {
            //                                rshq.push({ sfCode: Rf1[k].Sf_Code, RSFCode: Rf1[k].RSF_Code, Desig: Rf1[k].Designation, sfName: Rf1[k].sf_Name, level: '2' });
            //                                lvl = lvl > 2 ? lvl : 2;
            //                                Rf2 = dRSF.filter(function (a) { return (a.RSF_Code == Rf1[k].Sf_Code); });

            //                                if (Rf2.length > 0) {
            //                                    for (var c = 0; c < Rf2.length; c++) {
            //                                        rshq.push({ sfCode: Rf2[c].Sf_Code, RSFCode: Rf2[c].RSF_Code, Desig: Rf2[c].Designation, sfName: Rf2[c].sf_Name, level: '3' });
            //                                        lvl = lvl > 3 ? lvl : 3;
            //                                        Rf3 = dRSF.filter(function (a) { return (a.RSF_Code == Rf2[c].Sf_Code); });

            //                                        if (Rf3.length > 0) {
            //                                            for (var m = 0; m < Rf3.length; m++) {
            //                                                rshq.push({ sfCode: Rf3[m].Sf_Code, RSFCode: Rf3[m].RSF_Code, Desig: Rf3[m].Designation, sfName: Rf3[m].sf_Name, level: '4' });
            //                                                lvl = lvl > 4 ? lvl : 4;

            //                                                Rf4 = dRSF.filter(function (a) { return (a.RSF_Code == Rf3[m].Sf_Code); });
            //                                                if (Rf4.length > 0) {
            //                                                    for (var n = 0; n < Rf4.length; n++) {
            //                                                        rshq.push({ sfCode: Rf4[n].Sf_Code, RSFCode: Rf4[n].RSF_Code, Desig: Rf4[n].Designation, sfName: Rf4[n].sf_Name, level: '5' });
            //                                                        lvl = lvl > 5 ? lvl : 5;
            //                                                        //Rf4 = dRSF.filter(function (a) { return (a.RSF_Code == Rf3[c].Sf_Code); });
            //                                                    }
            //                                                }
            //                                            }
            //                                        }
            //                                    }
            //                                }

            //                            }
            //                        }
            //                    }
            //                }
            //            }

            //            console.log(rshq);

            //            var totdays = new Date("2018", "10", 0).getDate();
            //            console.log(totdays);
            //            // $('#FFTable  tr').remove();
            //            var str = '<th>Name</th><th>Zone</th><th>Day</th>';
            //            for (var d = 1; d <= totdays; d++) {
            //                str += '<th>' + d + '</th>';
            //            }
            //            $('#FFTable').append('<tr>' + str + '</tr>');

            //            if (rshq.length > 0) {
            //                for (var i = 0; i < rshq.length; i++) {
            //                    str = '<td>' + rshq[i].sfName + '</td>';
            //                    $('#FFTable').append('<tr>' + str + '</tr>');
            //                }
            //            }



            //            $(document).on('click', "#btnExcel", function (e) {
            //                var dt = new Date();
            //                var day = dt.getDate();
            //                var month = dt.getMonth() + 1;
            //                var year = dt.getFullYear();
            //                var postfix = day + "_" + month + "_" + year;
            //                //creating a temporary HTML link element (they support setting file names)
            //                var a = document.createElement('a');
            //                //getting data from our div that contains the HTML table
            //                var data_type = 'data:application/vnd.ms-excel';
            //                var table_div = document.getElementById('DGVFFO');
            //                var table_html = table_div.outerHTML.replace(/ /g, '%20');
            //                a.href = data_type + ', ' + table_html;
            //                //setting the file name
            //                a.download = 'PRODUCTIVE_CALLS_' + postfix + '.xls';
            //                //triggering the function
            //                a.click();
            //                //just in case, prevent default behaviour
            //                e.preventDefault();
            //            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="HSFCode" runat="server" />
    <br />
    <div class="container" style="max-width: 100%; width: 98%">
        <div class="row">
            <div class="col-md-8">
            </div>
            <div class="col-md-4" style="text-align: right">
                <a name="btnExcel" id="btnPrint" type="button" style="padding: 0px 20px; display: none"
                    href="#" class="btn btnPrint"></a><a name="btnExcel" id="btnPdf" type="button" style="padding: 0px 20px;
                        display: none" href="#" class="btn btnPdf"></a>
                <asp:LinkButton ID="btnExcel" runat="server" Style="padding: 0px 20px;" OnClick="btnExcel_Click"
                    class="btn btnExcel" />
                <a name="btnClose" id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();"
                    class="btn btnClose"></a>
            </div>
        </div>
    </div>
    <asp:Panel ID="pnlContents" runat="server" Width="100%">
        <div class="container" id="excelDiv" style="max-width: 100%; width: 98%">
            <asp:Label runat="server" ID="lblHead" Style="font-size: x-large"></asp:Label>
            <br />
            <asp:Label runat="server" ID="lblsfname"></asp:Label>
            <asp:GridView ID="GVData" runat="server" class="newStly">
            </asp:GridView>
            <table id="FFTable" class="newStly" runat="server" style="border-collapse: collapse;">
            </table>
            <asp:Table ID="DGVFFO" runat="server" Style="border-collapse: collapse; border: solid 1px Black;"
                Width="95%" CssClass="newStly">
            </asp:Table>
        </div>
    </asp:Panel>
    </form>
</body>
</html>
