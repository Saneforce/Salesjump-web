<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptSec_Order_Dtl_RepView.aspx.cs"
    Inherits="MIS_Reports_rptSec_Order_Dtl_RepView" %>

<!DOCTYPE html>
<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server" style="overflow-x: auto!important;">
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <link href="../css/style.css" rel="stylesheet" />
    <title>Secondary Order Detail View</title>
    <style type="text/css">
        body
        {
            padding: 10px 25px;
        }
        
    </style>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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

            

            var div_code = '<%=Session["div_code"]%>';
            

            var dc = '';
            dc = '"' + div_code + '"';
			
            if (dc == "98") {
                genReport = function () {
                    if (dDist.length > 0 && (rshq.length == 0 || rshq.length > 0)) {
                        //  console.log('en');
                        var ldcode = 0;
                        for (var i = 0; i < dDist.length; i++) {
                            str = '<td class="num2">' + (i + 1) + '</td><td>' + dDist[i].order_no + '</td>';
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

                            //                        str += "<td></td><td></td><td></td><td></td><td></td><td></td><td><input type='hidden' name='sfcode'value='" + dDist[i].sfCode + "'/>" + dDist[i].sfName + "</td>";
                            //                        str += "<td>" + (dDist[i].ListedDr_Name || '') + "</td><td>" + (dDist[i].ListedDr_Mobile || '') + "</td><td>" + (dDist[i].ListedDr_Email || '') + "</td><td>" + (dDist[i].GST || '') + "</td><td>" + (dDist[i].Address || '') + "</td><td>" + (dDist[i].cityname || '') + "</td><td>" + (dDist[i].PIN_Code || '') + "</td><td>" + (dDist[i].StateName || '') + "</td><td>" + (dDist[i].Remarks || '') + "</td><td>" + (dDist[i].Order_Value || '') + "</td>";

                            str += '<td>' + (dDist[i].EmpID || '') + '</td> <td><input type="hidden" name="sfcode" value="' + dDist[i].sfCode + '"/>' + dDist[i].sfName + '</td> <td>' + (dDist[i].User_rank || '') + '</td> <td>' + (dDist[i].sf_hq || '') + '</td><td>' + (dDist[i].Dis_Code || '') + '</td><td>' + (dDist[i].Dis_name || '') + '</td><td>' + (dDist[i].StateName || '') + '</td><td>' + (dDist[i].Region || '') + '</td><td>' + (dDist[i].Zone || '') + '</td><td>' + (dDist[i].Terr || '') + '</td>';
                            str += '<td>' + (dDist[i].Beats || '') + '</td> <td>' + (dDist[i].ListedDr_Name || '') + '</td> <td>' + (dDist[i].channel || '') + '</td> <td>' + (dDist[i].class_name || '') + '</td><td>' + (dDist[i].ListedDr_Name_per || '') + '</td> <td>' + (dDist[i].ListedDr_Mobile || '') + '</td><td>' + (dDist[i].ListedDr_Email || '') + '</td><td>' + (dDist[i].GST || '') + '</td><td>' + (dDist[i].Address || '') + '</td><td>' + (dDist[i].Market || '') + '</td><td>' + (dDist[i].cityname || '') + '</td><td>' + (dDist[i].PIN_Code || '') + '</td><td>' + (dDist[i].StateName || '') + '</td><td>' + (dDist[i].Outlets_Created || '') + '</td><td>' + (dDist[i].Order_Date || '') + '</td><td>' + (dDist[i].Time || '') + '</td><td>' + (dDist[i].Sec_cat || '') + '</td><td>' + (dDist[i].ProductCode || '') + '</td><td>' + (dDist[i].Product || '') + '</td><td>' + (dDist[i].Qty || '') + '</td><td>' + (dDist[i].Unit || '') + '</td><td>' + (dDist[i].Discount || '') + '</td><td>' + (dDist[i].Price || '') + '</td><td>' + (dDist[i].Sale_Value || '') + '</td><td>' + (dDist[i].Net_Value || '') + '</td><td>' + (dDist[i].Remarks || '') + '</td>';
                            if (nStrr == 0) {
                                $('#Product_Table tbody').append('<tr>' + str + '</tr>');
                            }
                            else {
                                $('#Product_Table tbody').append('<tr style="background-color:#a6f8d2;">' + str + '</tr>');
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
                    url: "rptSec_Order_Dtl_RepView.aspx/GetDataD",
                    data: "{'SF_Code':'" + sf_code + "', 'FYera':'" + Fyear + "', 'FMonth':'" + FMonth + "', 'SubDivCode':'" + SubDivCode + "'}",
                    dataType: "json",
                    success: function (data) {
                        // console.log(data.d);
                        dRSF = data.d;
                        //                    strh += "<th>Reporting Manager</th><th>SR Erp Id</th><th> User</th><th>user Rank</th><th>SR HQ/Area</th><th>Beats </th><th> Sf_Name</th> <th> ListedDr_Name </th> <th>ListedDr_Mobile</th><th>ListedDr_Email</th><th>GST</th><th>Address</th><th>cityname</th><th>PIN_Code</th><th>StateName</th><th>Remarks</th><th>Order_Value</th>";
                        //                    $('#ProductTable >thead').append(' <tr>' + strh + ' </tr>');

                    },
                    error: function (jqXHR, exception) {
                        alert(JSON.stringify(result));
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
                var str = '<th style="min-width:70px;">SLNo.</th><th style="min-width:150px;">Order No</th>';

                for (jk = 0; jk < lvl; jk++) {

                    if (jk > 0) {
                        str += '<th style="min-width:250px;">Level-' + jk + '</th>';
                    }
                    else {
                        str += '<th style="min-width:250px;">Top Level</th>';
                    }
                }

                //            strh += "<th>Reporting Manager</th><th>SR Erp Id</th><th> User</th><th>user Rank</th><th>SR HQ/Area</th><th>Beats </th><th> Sf_Name</th> <th> ListedDr_Name </th> <th>ListedDr_Mobile</th><th>ListedDr_Email</th><th>GST</th><th>Address</th><th>cityname</th><th>PIN_Code</th><th>StateName</th><th>Remarks</th><th>Order_Value</th>";
                str += '<th style="min-width:150px;">SR Erp Id</th>  <th style="min-width:250px;">User</th>  <th style="min-width:150px;">user Rank</th> <th style="min-width:150px;">SR HQ</th> <th style="min-width:150px;">Distributor Code</th> <th style="min-width:150px;">Distributor</th> <th style="min-width:150px;">Distributor State</th><th style="min-width:150px;">Region</th><th style="min-width:150px;">Zone</th><th style="min-width:150px;">Territory</th> <th style="min-width:150px;">Beats</th>';
                str += '<th style="min-width:200px;">Outlets Name</th><th style="min-width:200px;">Channel</th><th style="min-width:200px;">Class</th> <th style="min-width:150px;">Owner</th> <th style="min-width:150px;">Owners Contact</th>';
                str += '<th style="min-width:150px;">Owners Email</th> <th style="min-width:150px;">GSTIN</th> <th style="min-width:300px;">Address</th> <th style="min-width:150px;">Market</th><th style="min-width:150px;">City</th> <th style="min-width:150px;">Pin Code</th> <th style="min-width:150px;">State</th> <th style="min-width:150px;">Outlets Created On</th> <th style="min-width:150px;">Order Date</th> <th style="min-width:150px;">Time</th> <th style="min-width:150px;">Secondary Category</th> <th style="min-width:150px;">Product Code</th> <th style="min-width:150px;">Product</th> <th style="min-width:150px;">Qty</th><th style="min-width:150px;">Unit</th><th style="min-width:150px;">Discount</th><th style="min-width:150px;">Price</th><th style="min-width:150px;">Sale Value</th><th style="min-width:150px;">Net Value</th> <th style="min-width:150px;">No Sale Reason</th>';


                $('#Product_Table thead').append('<tr class="mainhead">' + str + '</tr>');

                var ReasonArray = [];
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "rptSec_Order_Dtl_RepView.aspx/GetData",
                    data: "{'SF_Code':'" + sf_code + "', 'FYera':'" + Fyear + "', 'FMonth':'" + FMonth + "', 'SubDivCode':'" + SubDivCode + "', 'ModeDt':'" + hdnDt + "'}",
                    dataType: "json",
                    success: function (data) {
                        dDist = data.d;
                        genReport();


                    },
                    error: function (jqXHR, exception) {
                        alert(JSON.stringify(result));
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

            }
            else {
                var genReport1 = '';
                genReport1 = function () {
                    if (dDist.length > 0 && (rshq.length == 0 || rshq.length > 0)) {
                        //  console.log('en');
                        var ldcode = 0;
                        for (var i = 0; i < dDist.length; i++) {
                            str = '<td class="num2">' + (i + 1) + '</td><td>' + dDist[i].order_no + '</td>';
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



                            str += '<td>' + (dDist[i].EmpID || '') + '</td> <td><input type="hidden" name="sfcode" value="' + dDist[i].sfCode + '"/>' + dDist[i].sfName + '</td> <td>' + (dDist[i].User_rank || '') + '</td> <td>' + (dDist[i].sf_hq || '') + '</td><td>' + (dDist[i].Dis_name || '') + '</td><td>' + (dDist[i].StateName || '') + '</td><td>' + (dDist[i].Region || '') + '</td><td>' + (dDist[i].Zone || '') + '</td><td>' + (dDist[i].Terr || '') + '</td>';
                            str += '<td>' + (dDist[i].Beats || '') + '</td> <td>' + (dDist[i].ListedDr_Name || '') + '</td> <td>' + (dDist[i].channel || '') + '</td> <td>' + (dDist[i].class_name || '') + '</td><td>' + (dDist[i].ListedDr_Name_per || '') + '</td> <td>' + (dDist[i].ListedDr_Mobile || '') + '</td><td>' + (dDist[i].ListedDr_Email || '') + '</td><td>' + (dDist[i].GST || '') + '</td><td>' + (dDist[i].Address || '') + '</td><td>' + (dDist[i].Market || '') + '</td><td>' + (dDist[i].cityname || '') + '</td><td>' + (dDist[i].PIN_Code || '') + '</td><td>' + (dDist[i].StateName || '') + '</td><td>' + (dDist[i].Outlets_Created || '') + '</td><td>' + (dDist[i].Order_Date || '') + '</td><td>' + (dDist[i].Time || '') + '</td><td>' + (dDist[i].Sec_cat || '') + '</td><td>' + (dDist[i].Product || '') + '</td><td>' + (dDist[i].Qty || '') + '</td><td>' + (dDist[i].Unit || '') + '</td><td>' + (dDist[i].Discount || '') + '</td><td>' + (dDist[i].Price || '') + '</td><td>' + (dDist[i].Sale_Value || '') + '</td><td>' + (dDist[i].Net_Value || '') + '</td><td>' + (dDist[i].Remarks || '') + '</td>';
                            if (nStrr == 0) {
                                $('#Product_Table tbody').append('<tr>' + str + '</tr>');
                            }
                            else {
                                $('#Product_Table tbody').append('<tr style="background-color:#a6f8d2;">' + str + '</tr>');
                            }


                        }

                    }
                }



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
                    url: "rptSec_Order_Dtl_RepView.aspx/GetDataD",
                    data: "{'SF_Code':'" + sf_code + "', 'FYera':'" + Fyear + "', 'FMonth':'" + FMonth + "', 'SubDivCode':'" + SubDivCode + "'}",
                    dataType: "json",
                    success: function (data) {
                        // console.log(data.d);
                        dRSF = data.d;
                       
                    },
                    error: function (jqXHR, exception) {
                        alert(JSON.stringify(result));
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
                var str = '<th style="min-width:70px;">SLNo.</th><th style="min-width:150px;">Order No</th>';

                for (jk = 0; jk < lvl; jk++) {

                    if (jk > 0) {
                        str += '<th style="min-width:250px;">Level-' + jk + '</th>';
                    }
                    else {
                        str += '<th style="min-width:250px;">Top Level</th>';
                    }
                }

                
                str += '<th style="min-width:150px;">SR Erp Id</th>  <th style="min-width:250px;">User</th>  <th style="min-width:150px;">user Rank</th> <th style="min-width:150px;">SR HQ</th> <th style="min-width:150px;">Distributor</th> <th style="min-width:150px;">Distributor State</th><th style="min-width:150px;">Region</th><th style="min-width:150px;">Zone</th><th style="min-width:150px;">Territory</th> <th style="min-width:150px;">Beats</th>';
                str += '<th style="min-width:200px;">Outlets Name</th><th style="min-width:200px;">Channel</th><th style="min-width:200px;">Class</th> <th style="min-width:150px;">Owner</th> <th style="min-width:150px;">Owners Contact</th>';
                str += '<th style="min-width:150px;">Owners Email</th> <th style="min-width:150px;">GSTIN</th> <th style="min-width:300px;">Address</th> <th style="min-width:150px;">Market</th><th style="min-width:150px;">City</th> <th style="min-width:150px;">Pin Code</th> <th style="min-width:150px;">State</th> <th style="min-width:150px;">Outlets Created On</th> <th style="min-width:150px;">Order Date</th> <th style="min-width:150px;">Time</th> <th style="min-width:150px;">Secondary Category</th> <th style="min-width:150px;">Product</th> <th style="min-width:150px;">Qty</th><th style="min-width:150px;">Unit</th><th style="min-width:150px;">Discount</th><th style="min-width:150px;">Price</th><th style="min-width:150px;">Sale Value</th><th style="min-width:150px;">Net Value</th> <th style="min-width:150px;">No Sale Reason</th>';


                $('#Product_Table thead').append('<tr class="mainhead">' + str + '</tr>');

                var ReasonArray = [];
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "rptSec_Order_Dtl_RepView.aspx/GetData1",
                    data: "{'SF_Code':'" + sf_code + "', 'FYera':'" + Fyear + "', 'FMonth':'" + FMonth + "', 'SubDivCode':'" + SubDivCode + "', 'ModeDt':'" + hdnDt + "'}",
                    dataType: "json",
                    success: function (data) {
                        dDist = data.d;
                        genReport1();
                       

                    },
                    error: function (jqXHR, exception) {
                        alert(JSON.stringify(result));
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

            }

            


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
        function sortTable() {
            var table, rows, switching, i, x, y, shouldSwitch;
            table = document.getElementById("Product_Table");
            switching = true;
            /*Make a loop that will continue until
            no switching has been done:*/
            while (switching) {
                //start by saying: no switching is done:
                switching = false;
                rows = table.rows;
                /*Loop through all table rows (except the
                first, which contains table headers):*/
                for (i = 1; i < (rows.length - 1); i++) {
                    //start by saying there should be no switching:
                    shouldSwitch = false;
                    /*Get the two elements you want to compare,
                    one from current row and one from the next:*/
                    var colLen = rows[i].cells.length;
                    x = rows[i].getElementsByTagName("TD")[colLen - 3] || 0;
                    y = rows[i + 1].getElementsByTagName("TD")[colLen - 3] || 0;
                    //check if the two rows should switch place:
                    if (Number(x.innerHTML) < Number(y.innerHTML)) {
                        //if so, mark as a switch and break the loop:
                        shouldSwitch = true;
                        break;
                    }
                }
                if (shouldSwitch) {
                    /*If a switch has been marked, make the switch
                    and mark that a switch has been done:*/
                    rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
                    switching = true;
                }
            }
        }
        var tableToExcel = (function () {
            var uri = 'data:application/vnd.ms-excel;base64,'
              , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="https://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--><meta http-equiv="content-type" content="text/plain; charset=UTF-8"/></head><body><table>{table}</table></body></html>'
              , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
              , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
            return function (table, name) {
                if (!table.nodeType) table = document.getElementById(table)
                var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }
                window.location.href = uri + base64(format(template, ctx))
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
                padding: 0px 20px;" Text="Product Wise Secondary Order Report"></asp:Label>
        </div>
         <div class="col-sm-4" style="text-align: right">
           <a id="btnExcel" style="padding: 0px 20px;" class="btn btnExcel" onclick="tableToExcel('Product_Table', 'Product Wise Secondary Order Report')" />
			<a name="btnClose" id="btnClose" type="button" href="javascript:window.open('','_self').close();" class="btn btnClose"></a>
        </div>
        </div>
        <div>
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            <br />
            <br />
            <div id="content">
                <table id="Product_Table" class="newStly table table-responsive">
                    <thead>
                      
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
        <%-- <div>
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
        </div>--%>
 
    </form>
</body>
</html>
