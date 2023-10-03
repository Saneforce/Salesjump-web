<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptNew_Outlet_Sales_RepView.aspx.cs"
    Inherits="MIS_Reports_rptNew_Outlet_Sales_RepView" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" style="overflow-x: auto!important;">
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <link href="../css/style.css" rel="stylesheet" />
    <title>New Outlets Analysis</title>
    <style type="text/css">
        body
        {
            padding: 10px 25px;
        }
        
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="../js/plugins/table2excel.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var rshq = [];
            var dRSF = [];
            var dDist = [];
            var dDist_no = [];
            var Dtls = [];
            var CatVal = [];
            var catH = [];
            var lvl = 0;
            $("#btnExcel").click(function () {
                $("#1").table2excel({
                    filename: "New Outlet Sales Report.xls"
                });
                $("#2").table2excel({
                    filename: "Non Visited New Outlet Sales Report .xls"
                });
            });
            genReport = function () {
                if (dDist.length > 0 && rshq.length) {
                    //  console.log('en');
                    var ldcode = 0;
                    for (var i = 0; i < dDist.length; i++) {
                        str = '<td class="num2">' + (i + 1) + '</td><td>' + dDist[i].Date + '</td>';
                        //console.log(dDist[i].RSFs);
                        //  console.log(rshq);
                        var nStrr = 0;
                        //vpr = dDist.filter(function (a) { return (ldcode == dDist[i].ListedDr_code); });
                        if (ldcode == dDist[i].ListedDr_code) {
                            nStrr = 1;
                            
                        }
                       
                        ldcode = dDist[i].ListedDr_code;

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
						else {
						var nStr = '';
                            for (var h = 0; h < 4; h++) {
                                nStr = nStr + '<td></td>';
                            }
                            str += nStr;
                        }

                        //                        str += "<td></td><td></td><td></td><td></td><td></td><td></td><td><input type='hidden' name='sfcode'value='" + dDist[i].sfCode + "'/>" + dDist[i].sfName + "</td>";
                        //                        str += "<td>" + (dDist[i].ListedDr_Name || '') + "</td><td>" + (dDist[i].ListedDr_Mobile || '') + "</td><td>" + (dDist[i].ListedDr_Email || '') + "</td><td>" + (dDist[i].GST || '') + "</td><td>" + (dDist[i].Address || '') + "</td><td>" + (dDist[i].cityname || '') + "</td><td>" + (dDist[i].PIN_Code || '') + "</td><td>" + (dDist[i].StateName || '') + "</td><td>" + (dDist[i].Remarks || '') + "</td><td>" + (dDist[i].Order_Value || '') + "</td>";

                        str += '<td>' + (dDist[i].EmpID || '') + '</td> <td><input type="hidden" name="sfcode" value="' + dDist[i].sfCode + '"/>' + dDist[i].sfName + '</td> <td>' + (dDist[i].User_rank || '') + '</td> <td>' + (dDist[i].sf_hq || '') + '</td>';
                        str += '<td>' + (dDist[i].Beats || '') + '</td> <td>' + (dDist[i].Dst_Name || '') + '</td><td>' + (dDist[i].ListedDr_code || '') + '</td><td>' + (dDist[i].ListedDr_Name || '') + '</td> <td>' + (dDist[i].channel || '') + '</td> <td>' + (dDist[i].class_name || '') + '</td><td>' + (dDist[i].ListedDr_Name_per || '') + '</td> <td>' + (dDist[i].ListedDr_Mobile || '') + '</td><td>' + (dDist[i].ListedDr_Email || '') + '</td><td>' + (dDist[i].GST || '') + '</td><td>' + (dDist[i].Lat || '') + '</td><td>' + (dDist[i].Long || '') + '</td><td>' + (dDist[i].Address || '') + '</td><td>' + (dDist[i].cityname || '') + '</td><td>' + (dDist[i].PIN_Code || '') + '</td><td>' + (dDist[i].Cre_date || '') + '</td><td>' + (dDist[i].StateName || '') + '</td><td>' + (dDist[i].Remarks || '') + '</td><td>' + (dDist[i].Order_Value || '') + '</td><td>' + (dDist[i].FirstCall || '') + '</td><td>' + (dDist[i].SeconCall || '') + '</td><td>' + (dDist[i].ThirdCall || '') + '</td>';
                        if (nStrr == 0) {
                            $('#1 tbody').append('<tr>' + str + '</tr>');
                        }
                        else {
                            $('#1 tbody').append('<tr style="background-color:#a6f8d2;">' + str + '</tr>');
                        }


                    }
                    //str += '<td class="num2">' + TotQTY + '</td><td class="num2">' + TotVal.toFixed(2) + '</td>' + str2;

                }
            }



            //$('#ProductTable tr').remove();



            var sf_code = $("#<%=ddlFieldForce.ClientID%>").val();
            var Fyear = $("#<%=ddlFYear.ClientID%>").val();
            var FMonth = $("#<%=ddlFMonth.ClientID%>").val();
            var SubDivCode = $("#<%=SubDivCode.ClientID%>").val();
            var hdnDt = $("#<%=hdnDate.ClientID%>").val();

            //head
            var ReasonArray = [];
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptNew_Outlet_Sales_RepView.aspx/GetDataD",
                data: "{'SF_Code':'" + sf_code + "', 'FYera':'" + Fyear + "', 'FMonth':'" + FMonth + "', 'SubDivCode':'" + SubDivCode + "'}",
                dataType: "json",
                success: function (data) {
                    // console.log(data.d);
                    dRSF = data.d;
                    //                    strh += "<th>Reporting Manager</th><th>SR Erp Id</th><th> User</th><th>user Rank</th><th>SR HQ/Area</th><th>Beats </th><th> Sf_Name</th> <th> ListedDr_Name </th> <th>ListedDr_Mobile</th><th>ListedDr_Email</th><th>GST</th><th>Address</th><th>cityname</th><th>PIN_Code</th><th>StateName</th><th>Remarks</th><th>Order_Value</th>";
                    //                    $('#ProductTable >thead').append(' <tr>' + strh + ' </tr>');

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

            var leng = 0;
            if (dRSF.length > 0) {
                leng = dRSF.length;

                Rf = dRSF.filter(function (a) { return (a.RSF_Code == 'admin'); });
                if (Rf.length > 0) {
                    var Rf1;
                    var strk = '';
                    leng = leng - Rf.length;

                    for (var l = 0; l < Rf.length; l++) {
                        rshq.push({ sfCode: Rf[l].Sf_Code, RSFCode: Rf[l].RSF_Code, Desig: Rf[l].Designation, sfName: Rf[l].Sf_Name, level: '1' });
                        lvl = lvl > 1 ? lvl : 1;
                        Rf1 = dRSF.filter(function (a) { return (a.RSF_Code == Rf[l].Sf_Code); });
                        if (Rf1.length > 0) {
                            for (var k = 0; k < Rf1.length; k++) {
                                rshq.push({ sfCode: Rf1[k].Sf_Code, RSFCode: Rf1[k].RSF_Code, Desig: Rf1[k].Designation, sfName: Rf1[k].Sf_Name, level: '2' });
                                lvl = lvl > 2 ? lvl : 2;
                                Rf2 = dRSF.filter(function (a) { return (a.RSF_Code == Rf1[k].Sf_Code); });

                                if (Rf2.length > 0) {
                                    for (var c = 0; c < Rf2.length; c++) {
                                        rshq.push({ sfCode: Rf2[c].Sf_Code, RSFCode: Rf2[c].RSF_Code, Desig: Rf2[c].Designation, sfName: Rf2[c].Sf_Name, level: '3' });
                                        lvl = lvl > 3 ? lvl : 3;
                                        Rf3 = dRSF.filter(function (a) { return (a.RSF_Code == Rf2[c].Sf_Code); });

                                        if (Rf3.length > 0) {
                                            for (var m = 0; m < Rf3.length; m++) {
                                                rshq.push({ sfCode: Rf3[m].Sf_Code, RSFCode: Rf3[m].RSF_Code, Desig: Rf3[m].Designation, sfName: Rf3[m].Sf_Name, level: '4' });
                                                lvl = lvl > 4 ? lvl : 4;

                                                Rf4 = dRSF.filter(function (a) { return (a.RSF_Code == Rf3[m].Sf_Code); });
                                                if (Rf4.length > 0) {
                                                    for (var n = 0; n < Rf4.length; n++) {
                                                        rshq.push({ sfCode: Rf4[n].Sf_Code, RSFCode: Rf4[n].RSF_Code, Desig: Rf4[n].Designation, sfName: Rf4[n].Sf_Name, level: '5' });
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
                // console.log(rshq);
            }


            $('#1 tr').remove();
            var str = '<th style="min-width:70px;">SLNo.</th><th style="min-width:150px;">Date</th>';

            for (jk = 0; jk < lvl; jk++) {

                if (jk > 0) {
                    str += '<th style="min-width:250px;">Level-' + jk + '</th>';
                }
                else {
                    str += '<th style="min-width:250px;">Top Level</th>';
                }
            }

            //            strh += "<th>Reporting Manager</th><th>SR Erp Id</th><th> User</th><th>user Rank</th><th>SR HQ/Area</th><th>Beats </th><th> Sf_Name</th> <th> ListedDr_Name </th> <th>ListedDr_Mobile</th><th>ListedDr_Email</th><th>GST</th><th>Address</th><th>cityname</th><th>PIN_Code</th><th>StateName</th><th>Remarks</th><th>Order_Value</th>";
            str += '<th style="min-width:150px;">SR Erp Id</th>  <th style="min-width:250px;">User</th>  <th style="min-width:150px;">user Rank</th> <th style="min-width:150px;">SR HQ/Area</th> <th style="min-width:150px;">Beats</th>';
            str += '<th style="min-width:400px;">Distributor Name</th><th style="min-width:200px;">Outlet Code</th><th style="min-width:200px;">Outlets Name</th><th style="min-width:200px;">Channel</th><th style="min-width:200px;">Class</th> <th style="min-width:150px;">Owner</th> <th style="min-width:150px;">Owners Contact</th>';
            str += '<th style="min-width:150px;">Owners Email</th><th style="min-width:150px;">GSTIN</th><th>Lat</th><th>Long</th> <th style="min-width:300px;">Address</th> <th style="min-width:150px;">Market Area City Tehsil</th> <th style="min-width:150px;">Pin Code</th><th style="min-width:150px;">Created Date</th> <th style="min-width:150px;">State</th> <th style="min-width:150px;">No Sale Reason</th><th style="min-width:150px;">Value</th> <th style="min-width:100px;">First Call</th><th style="min-width:100px;">Second Call</th><th style="min-width:100px;">Third Call</th>';



            $('#1 thead').append('<tr class="mainhead">' + str + '</tr>');





            var ReasonArray = [];
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptNew_Outlet_Sales_RepView.aspx/GetData",
                data: "{'SF_Code':'" + sf_code + "', 'FYera':'" + Fyear + "', 'FMonth':'" + FMonth + "', 'SubDivCode':'" + SubDivCode + "', 'ModeDt':'" + hdnDt + "'}",
                dataType: "json",
                success: function (data) {
                    dDist = data.d;
                    genReport();
                    //  console.log(data.d);

                    //                        str += "<td></td><td></td><td></td><td></td><td></td><td></td><td><input type='hidden' name='sfcode'value='" + data.d[i].Sf_Code + "'/>" + data.d[i].Sf_Name + "</td>";
                    //                        str += "<td>" + (data.d[i].ListedDr_Name || '') + "</td><td>" + (data.d[i].ListedDr_Mobile || '') + "</td><td>" + (data.d[i].ListedDr_Email || '') + "</td><td>" + (data.d[i].GST || '') + "</td><td>" + (data.d[i].Address || '') + "</td><td>" + (data.d[i].cityname || '') + "</td><td>" + (data.d[i].PIN_Code || '') + "</td><td>" + (data.d[i].StateName || '') + "</td><td>" + (data.d[i].Remarks || '') + "</td><td>" + (data.d[i].Order_Value || '') + "</td>";

                    //                        $('#ProductTable >tbody').append('<tr>' + str + ' </tr>');
                    //                    }


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




            genReport_no = function () {
                if (dDist_no.length > 0) {
                    //  console.log('en');
                    for (var i = 0; i < dDist_no.length; i++) {
                        str1 = '<td class="num2">' + (i + 1) + '</td>';

                        str1 += '<td>' + dDist_no[i].ListedDrCode + '</td> <td><input type="hidden" name="sfcode" value="' + dDist_no[i].ListedDrCode + '"/>' + dDist_no[i].ListedDr_Name + '</td> <td>' + dDist_no[i].Dst_Name + '</td> <td>' + dDist_no[i].sf_name + '</td> <td>' + dDist_no[i].Retailer_Channel + '</td>';
                        str1 += '<td>' + dDist_no[i].Mobile_No + '</td> <td>' + dDist_no[i].Retailer_Class + '</td>  <td>' + dDist_no[i].ContactPerson + '</td><td>' + dDist_no[i].ListedDr_Email + '</td><td>' + dDist_no[i].GSTNO + '</td><td>' + dDist_no[i].Lat + '</td><td>' + dDist_no[i].Long + '</td><td>' + dDist_no[i].Address + '</td><td>' + dDist_no[i].City + '</td><td>' + dDist_no[i].PinCode + '</td><td>' + dDist_no[i].Cre_date + '</td>';
                        $('#2 tbody').append('<tr>' + str1 + '</tr>');

                    }
                    //str += '<td class="num2">' + TotQTY + '</td><td class="num2">' + TotVal.toFixed(2) + '</td>' + str2;

                }
            }







            $('#2 tr').remove();
             var str1 = '<th style="min-width:70px;">SLNo.</th><th style="min-width:150px;">Retailer Code</th>';
            str1 += '<th style="min-width:150px;">Retailer Name</th><th style="min-width:500px;">Distributor Name</th>  <th style="min-width:450px;">FieldForce</th><th style="min-width:150px;">Retailer Channel</th> <th style="min-width:150px;">Mobile No</th> <th style="min-width:150px;">Retailer Class</th>';
            str1 += '<th style="min-width:150px;">Owner</th>';
            str1 += '<th style="min-width:150px;">Owners Email</th> <th style="min-width:150px;">GSTIN</th><th>Lat</th><th>Long</th> <th style="min-width:300px;">Address</th> <th style="min-width:150px;">Market Area City Tehsil</th> <th style="min-width:150px;">Pin Code</th><th style="min-width:150px;">Created Date</th>';



            $('#2 thead').append('<tr class="mainhead">' + str1 + '</tr>');

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptNew_Outlet_Sales_RepView.aspx/GetData_no",
                data: "{'SF_Code':'" + sf_code + "', 'FYera':'" + Fyear + "', 'FMonth':'" + FMonth + "', 'SubDivCode':'" + SubDivCode + "', 'ModeDt':'" + hdnDt + "'}",
                dataType: "json",
                success: function (data) {
                    dDist_no = data.d;
                    genReport_no();
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



            $(document).on('click', "#btnClose", function () {
                window.close();
            });



            $(document).on('click', "#btnPrint", function () {
                var originalContents = $("body").html();
                var printContents = $("#content").html();
                $("body").html(printContents);
                window.print();
                $("body").html(originalContents);
                return false;
            });


        });
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
        , tablevar = '<table>Field Force Wise New Outlet Sales Report{table'
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

                //document.getElementById("dlink").href = uri + base64(format(template, ctx));
                //document.getElementById("dlink").download = filename;
                //document.getElementById("dlink").click();

                window.location.href = uri + base64(format(allOfIt, ctx));

            }
        })()
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="ddlFieldForce" runat="server" />
    <asp:HiddenField ID="ddlFYear" runat="server" />
    <asp:HiddenField ID="ddlFMonth" runat="server" />
    <asp:HiddenField ID="SubDivCode" runat="server" />
    <asp:HiddenField ID="hdnDate" runat="server" />
    <div class="row">
        <div class="col-sm-8">
            <asp:Label ID="lblhead1" runat="server" Style="font-weight: bold; font-size: x-large;
                padding: 0px 20px;" Text="New Outlet Sales Report"></asp:Label>
        </div>
        <div class="col-sm-4" style="text-align: right">
            <a name="btnExcel" id="btnPrint" type="button" style="padding: 0px 20px; display: none"
                href="#" class="btn btnPrint"></a><a id="btnExcel" style="padding: 0px 20px;" class="btn btnExcel" /><a name="btnClose"
                        id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();"
                        class="btn btnClose"></a>
        </div>
        <div>
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            <br />
            <br />
            <div id="content">
                <table id="1" class="newStly table table-responsive">
                    <thead>
                      
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
         <div>
            <br />
            <br />
           <asp:Label ID="Label2" runat="server" Style="font-weight: bold; font-size: x-large;
                padding: 0px 20px;" Text="Unseen New Outlet Sales Report"></asp:Label>
            <br />
            <br />
            <div id="content1">
                <table id="2" class="newStly table table-responsive">
                    <thead>
                      
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
