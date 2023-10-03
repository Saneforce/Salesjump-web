<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptSKU_Summarys_rep1.aspx.cs" Inherits="MasterFiles_Reports_tsr_rptSKU_Summarys_rep1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SKU Summary</title>
    <link href="../../../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../../../css/style.css" rel="stylesheet" />
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
        
        
        #Product_Table tbody tr td, #Product_Table tfoot tr th
        {
            text-align: right;
        }
        
        #Product_Table tbody tr td:nth-child(1)
        {
            text-align: left;
        }
         #Product_Table tbody tr td:nth-child(2)
        {
            text-align: left;
        }
           #Product_Table tbody tr td:nth-child(3)
        {
            text-align: left;
        }
          #Product_Table tbody tr td:nth-child(4)
        {
            text-align: left;
        }
        #Product_Table tbody tr td:nth-child(5)
        {
            text-align: left;
        }
        #Product_Table tfoot tr th:nth-child(1)
        {
            text-align: center;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
	 <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        //$('form').live("submit", function () {
        //    ShowProgress();
        //});       
    </script>
    <script type="text/javascript">


        $(document).ready(function () {

            dProd = []; dPQV = []; dSF = []; dTcEc = []; dBrand = []; dBrandPQE = []; dDDTT = [];
            genReport = function () {
                if (dSF.length > 0 && dProd.length > 0 && dPQV.length > 0 && dDDTT.length > 0) {

                    console.log(dSF); console.log(dProd); console.log(dPQV); console.log(dDDTT);

                    for (var i = 0; i < dDDTT.length; i++) {


						str = '<td><p name="sfname"  style="margin: 0 0 0px;"> <a class="AAA" href="Rpt_DCR_View.aspx?&sf_code=' + dDDTT[i].sfCode + '&Mode=New_View_All_DCR_Date(s)&FDate=' + dDDTT[i].dt + '&cur_month=0&cur_year=0&Dst_code=&Dst_name=&st_name=&st_code=&Tdate=' + dDDTT[i].dt + '&Sf_Name=' + dDDTT[i].sfName + '">' + dDDTT[i].dt + '</a></p></td><td><input type="hidden" name="sfname" value="' + dDDTT[i].sfCode + '"/> <p name="sfname" style="margin: 0 0 0px;">' + dDDTT[i].sfName + '</p> </td><td><p name="sfname" style="margin: 0 0 0px;">' + dDDTT[i].dd + '</p></td><td><p name="sfname" style="margin: 0 0 0px;">' + dDDTT[i].tt + '</p></td><td><p name="sfname" style="margin: 0 0 0px;text-align:right;">'+ dDDTT[i].retag + '</p></td><td><p name="sfname" style="margin: 0 0 0px;">'+ dDDTT[i].notl +'</p></td><td><p name="sfname" style="margin: 0 0 0px;">'+ dDDTT[i].totl + '</p></td><td><p name="sfname" style="margin: 0 0 0px;">'+ dDDTT[i].wt + '</p></td>';
                        //str = '<td><p name="sfname"  style="margin: 0 0 0px;"> <a class="AAA" href="Retailwise_sales_report.aspx?&sf_code=' + dDDTT[i].sfCode + '&Mode=New_View_All_DCR_Date(s)&FDate=' + dDDTT[i].dt + '&cur_month=0&cur_year=0&Dst_code=&Dst_name=&st_name=&st_code=&Tdate=' + dDDTT[i].dt + '&Sf_Name=' + dDDTT[i].sfName + '">' + dDDTT[i].dt + '</a></p></td><td><input type="hidden" name="sfname" value="' + dDDTT[i].sfCode + '"/> <p name="sfname" style="margin: 0 0 0px;">' + dDDTT[i].sfName + '</p> </td><td><p name="sfname" style="margin: 0 0 0px;">' + dDDTT[i].dd + '</p></td><td><p name="sfname" style="margin: 0 0 0px;">' + dDDTT[i].tt + '</p></td><td><p name="sfname" style="margin: 0 0 0px;">' + dDDTT[i].wt + '</p></td>';
                        // str = '<td><input class="AAA" type="hidden" name="sfcode" value="' + data.d[i].sfCode + '"/> <a class="AAA" href="Report/rptTPView.aspx?&sf_code=' + data.d[i].sfCode + '&cur_month=' + data.d[i].Month + '&cur_year=' + data.d[i].Year + '&sf_name=' + data.d[i].sfName + '&level=' + -1 + '" name="sfname" style="margin: 0 0 0px;">' + data.d[i].sfName + '</a> </td>';
                        //str = '<td><input class="AAA" type="hidden" name="sfname" value="' + dDDTT[i].dt + '"/> <a class="AAA" href="Report/rptTPView.aspx?&sf_code=' + dDDTT[i].sfCode + '&cur_month=' + dDDTT[i].sfName + '&cur_year=' + dDDTT[i].sfName + '&sf_name=' + data.d[i].sfName + '&level=' + -1 + '" name="sfname" style="margin: 0 0 0px;">' + dDDTT[i].sfName + '</a> </td>';
                        //str = '<td><input class="AAA" type="hidden" name="sfname" value="' + dDDTT[i].dt + '"/> <a class="AAA" href="Report/rptTPView.aspx?&sf_code=' + dDDTT[i].sfCode + '&cur_month=' + dDDTT[i].dt + '&cur_year=' + dDDTT[i].dt + '&sf_name=' + data.d[i].sfName + '&level=' + -1 + '" name="sfname" style="margin: 0 0 0px;">' + dDDTT[i].sfName + '</a> </td><td><input type="hidden" name="sfcode" value="' + dDDTT[i].sfCode + '"/> <p name="sfname" style="margin: 0 0 0px;">' + dDDTT[i].sfName + '</p> </td><td><p name="sfname" style="margin: 0 0 0px;">' + dDDTT[i].dd + '</p></td><td><p name="sfname" style="margin: 0 0 0px;">' + dDDTT[i].tt + '</p></td>';
                        str1 = '';
                        var tc = '', ec = ''; TPS = '', TLS = ''; FC_TM = ''; LC_TM = '';
                        console.log(dTcEc);
                        fP = dTcEc.filter(function (a) {
                            return (a.sfCode == dDDTT[i].sfCode && a.dtt == dDDTT[i].dt);
                        });

                        if (fP.length > 0) {
                            tc = fP[0].TC_Count, ec = fP[0].EC_Count; TPS = fP[0].TPS_Count, TLS = fP[0].TLS_Count; FC_TM = fP[0].FC_TM; LC_TM = fP[0].LC_TM;
                        }
                        var TotVal = 0;
                        for (var j = 0; j < dProd.length; j++) {
                            var q = "", v = "";
                            fP = dPQV.filter(function (a) { return (a.sfCode == dDDTT[i].sfCode && a.Date == dDDTT[i].dt && a.proCode == dProd[j].product_id); });

                            if (fP.length > 0) {
                                q = fP[0].caseRate, v = fP[0].amount;
                                TotVal += Number(fP[0].amount);
                            }
                            str += '<td>' + q + '</td>';
                        }
                        var TotValb = 0;
                        for (var b = 0; b < dBrand.length; b++) {
                            var bq = "", bec = "";
                            fP1 = dBrandPQE.filter(function (a) { return (a.sfCode_b == dDDTT[i].sfCode && a.dt_b == dDDTT[i].dt && a.BraCode == dBrand[b].Brand_id); });
                            if (fP1.length > 0) {
                                bq = fP1[0].Braqty, bec = fP1[0].BrandEc;
                                TotValb += Number(fP1[0].Braqty);
                            }
                            str1 += '<td>' + bq + '</td><td>' + ((bec != "") ? parseFloat(bec) : "") + '</td>';
                        }


                        //console.log(parseFloat(TotVal).toFixed(2));
                        str += '<td>' + tc + '</td><td>' + ec + '</td><td>' + TPS + '</td><td>' + TLS + '</td><td>' + FC_TM + '</td><td>' + LC_TM + '</td>';
                        str1 += '<td>' + parseFloat(TotVal).toFixed(2) + '</td>';
                        $('#Product_Table tbody').append('<tr>' + str + str1 + '</tr>');
                        // $('#Product_Table tbody').append('<tr>' + str +  '</tr>');
                    }
                    $('#btnexcel').show();
//                    $('#Product_Table tbody').find('tr').each(function () {

//                        tds = $(this).find("td");
//                        console.log(tds);
//                        $(tds[0]).html('<a class="AAA" href=Rpt_DCR_View.aspx?&sf_code=' + dDDTT[i].sfCode + '&Mode=New_View_All_DCR_Date(s)&FDate=' + dDDTT[i].dt + '&cur_month=0&cur_year=0&Tdate=' + dDDTT[i].dt + '&Sf_Name=>' + dDDTT[i].sfName + '</a>')
//                        // $(this).html('<a href=Rpt_DCR_View.aspx?&sf_code=' + $(this).closest('tr').find('td:eq(2)').text() + '&Sf_Name=' + "" + '&cur_month=' + cmonth + '&cur_year=' + cyear + '&Mode=' + "SKU Summary" + '&FDate=' + "2017-12-01" +  '>' + $(this).html() + '</a>')

//                    });



                    $(".AAA").click(function () {
                        //alert($(this).attr('href'));

                        event.preventDefault();
                        window.open($(this).attr("href"), "popupWindow", "width=600,height=600,scrollbars=yes");

                    });
                }
            }



            var sf_code = $("#<%=ddlFieldForce.ClientID%>").val();
            if (sf_code == "---Select Field Force---") { alert('Select Field Force'); $("#<%=ddlFieldForce.ClientID%>").focus(); return false; }


            $('#Product_Table tr').remove();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "/MasterFiles/Reports/tsr/rptSKU_Summarys_rep1.aspx/getdata1",
                dataType: "json",
                success: function (data) {
                    dBrand = data.d;
                    //console.log(dBrand);

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



            var len = 0;
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "/MasterFiles/Reports/tsr/rptSKU_Summarys_rep1.aspx/getdata",
                dataType: "json",
                success: function (data) {
                    len = data.d.length;
                    dProd = data.d;
                    genReport();
                    if (data.d.length > 0 && dBrand.length > 0) {
						str = '<th  style="min-width:100px;" rowspan="2"> <p style="margin: 0 0 0px;">Date</p> </th><th  style="min-width:250px;" rowspan="2"> <p style="margin: 0 0 0px;">Field Force</p> </th><th  style="min-width:250px;" rowspan="2"> <p style="margin: 0 0 0px;">Distributor</p> </th><th  style="min-width:250px;" rowspan="2"> <p style="margin: 0 0 0px;">Route</p> </th><th  style="min-width:150px;" rowspan="2"> <p style="margin: 0 0 0px;">#Retag</p> </th><th  style="min-width:150px;" rowspan="2"> <p style="margin: 0 0 0px;">#Notl</p> </th><th  style="min-width:150px;" rowspan="2"> <p style="margin: 0 0 0px;">#Totl</p> </th><th  style="min-width:150px;" rowspan="2"> <p style="margin: 0 0 0px;">Work Type</p> </th>';
                        //str = '<th  style="min-width:100px;" rowspan="2"> <p style="margin: 0 0 0px;">Date</p> </th><th  style="min-width:250px;" rowspan="2"> <p style="margin: 0 0 0px;">Field Force</p> </th><th  style="min-width:250px;" rowspan="2"> <p style="margin: 0 0 0px;">Distributor</p> </th><th  style="min-width:250px;" rowspan="2"> <p style="margin: 0 0 0px;">Route</p> </th><th  style="min-width:150px;" rowspan="2"> <p style="margin: 0 0 0px;">Work Type</p> </th>';
                        str1 = '';
                        strff = '<th style="min-width:250px;" colspan="5"> <p style="margin: 0 0 0px;">Total</p> </th>';
                        strb = '';
                        strb1 = '';
                        strbff = '<th></th>';
                        for (var i = 0; i < data.d.length; i++) {

                            str += '<th style="min-width:150px; " colspan="1"> <input type="hidden" name="pcode" value="' + data.d[i].product_id + '"/> <p name="pname" style="margin: 0 0 0px;">' + data.d[i].product_name + '</p> </th>';
                            str1 += '<th>Quantity</th>';
                            strff += '<th style="text-align: right" ></th>';

                        }

                        for (var k = 0; k < dBrand.length; k++) {

                            strb += '<th style="min-width:150px; " colspan="2"> <input type="hidden" name="pcode" value="' + dBrand[k].Brand_id + '"/> <p name="pname" style="margin: 0 0 0px;">' + dBrand[k].Brand_name + '</p> </th>';
                            strb1 += '<th>Quantity</th><th>EC</th>';
                            strbff += '<th style="text-align: right" ></th><th style="text-align: right"></th>';

                        }

                        $('#Product_Table thead').append('<tr class="mainhead">' + str + '<th colspan="6">Total</th>' + strb + '<th>Total</th></tr>');
                        $('#Product_Table thead').append('<tr class="secondhead">' + str1 + '<th>TC</th><th>EC</th><th>TPS</th><th>TLS</th><th>FC_TM</th><th>LC_TM</th>' + strb1 + '<th>Values</th></tr>');

                        $('#Product_Table tfoot').append('<tr class="trfoot">' + strff + '<th></th><th></th><th></th><th></th><th></th><th></th>' + strbff + '</tr>');
                    }
                },
                error: function (jqXHR, exception) {
                    console.log(JSON.stringify(jqXHR));
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



            var Fdate = $("#<%=ddlFdate.ClientID%>").val();

            var Tdate = $("#<%=ddlTdate.ClientID%>").val();

            var subDiv = $("#<%=SubDivCode.ClientID%>").val();



            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "/MasterFiles/Reports/tsr/rptSKU_Summarys_rep1.aspx/IssuDatas",
                data: "{'SF_Code':'" + sf_code + "', 'Fdate':'" + Fdate + "','Tdate':'" + Tdate + "', 'SubDiv':'" + subDiv + "'}",
                dataType: "json",
                success: function (data) {
                    //console.log(JSON.stringify(data.d));
                    dBrandPQE = data.d;
                    //console.log(dBrandPQE)
                    //genReport();

                },


                error: function (jqXHR, exception) {
                    console.log(JSON.stringify(jqXHR));
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


            //get dd tt

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "/MasterFiles/Reports/tsr/rptSKU_Summarys_rep1.aspx/getddtt",
                data: "{'SF_Code':'" + sf_code + "', 'Fdate':'" + Fdate + "','Tdate':'" + Tdate + "', 'SubDiv':'" + subDiv + "'}",
                dataType: "json",
                success: function (data) {
                    dDDTT = data.d;
                    //console.log(dBrand);

                },
                error: function (jqXHR, exception) {
                    console.log(JSON.stringify(jqXHR));
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
                url: "/MasterFiles/Reports/tsr/rptSKU_Summarys_rep1.aspx/getIssuDataTcEc",
                data: "{'SF_Code':'" + sf_code + "', 'Fdate':'" + Fdate + "','Tdate':'" + Tdate + "', 'SubDiv':'" + subDiv + "'}",
                dataType: "json",
                success: function (data) {
                    dTcEc = data.d;
                    //console.log(dTcEc);
                },
                error: function (jqXHR, exception) {
                    console.log(JSON.stringify(jqXHR));
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
                url: "/MasterFiles/Reports/tsr/rptSKU_Summarys_rep1.aspx/getIssuData1",
                data: "{'SF_Code':'" + sf_code + "', 'Fdate':'" + Fdate + "','Tdate':'" + Tdate + "', 'SubDiv':'" + subDiv + "'}",
                dataType: "json",
                success: function (data) {
                    dSF = data.d;
                    // console.log(data.d);
                    genReport();
                },
                error: function (jqXHR, exception) {
                    console.log(JSON.stringify(jqXHR));
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
                url: "/MasterFiles/Reports/tsr/rptSKU_Summarys_rep1.aspx/getIssuData",
                data: "{'SF_Code':'" + sf_code + "', 'Fdate':'" + Fdate + "','Tdate':'" + Tdate + "', 'SubDiv':'" + subDiv + "'}",
                dataType: "json",
                success: function (data) {
                    // console.log(JSON.stringify(data.d));
                    dPQV = data.d;
                    genReport();

                },
                error: function (jqXHR, exception) {
                    console.log(JSON.stringify(jqXHR));
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




            var arr = [];
            $('#Product_Table tbody tr').each(function () {
                var i = 0;
                //console.log($(this).find('td').length);
                $(this).find('td').each(function () {
                    if (i > 4) {
                        arr[i - 5] = Number(arr[i - 5] || 0) + Number($(this).text() || 0);
                    }
                    i++;
                });
            });
            // console.log(arr);

            var i = 0;
            var leng = $('.trfoot th').length;
            $('.trfoot th').each(function () {
                if (i > 0) {
                    $(this).text(parseFloat(arr[i - 1]).toFixed(2));
                }
                i++;
            });

            //            $('.secondhead th').each(function (i) {
            //                var remove = 0;

            //                var tds = $(this).parents('table').find('tr td:nth-child(' + (i + 2) + ')')


            //                tds.each(function (j) {
            //                    if (this.innerHTML == '') remove++;
            //                });
            //                if (remove == ($('#Product_Table tbody tr').length)) {
            //                    $(this).hide();
            //                    tds.hide();
            //                    $('#Product_Table tfoot tr th').eq($(this).index() + 1).hide();
            //                    //  if (i > 0) {
            //                    if (i % 2 == 0) {
            //                        $('.mainhead th').eq((i / 2) + 1).hide();
            //                    }
            //                    //}
            //                }
            //            });
            //  sortTable();






            $(document).on('click', "#btnClose", function () {
                window.close();
            });

            $(document).on('click', "#btnExcel", function (e) {
                var dt = new Date();
                var day = dt.getDate();
                var month = dt.getMonth() + 1;
                var year = dt.getFullYear();
                var postfix = day + "_" + month + "_" + year;

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
                var tets = 'DailySecondaryReport_' + postfix + '.xls';   //create fname

                link.download = tets;
                link.href = uri + base64(format(template, ctx));
                link.click();





                //creating a temporary HTML link element (they support setting file names)
                //var a = document.createElement('a');
                //getting data from our div that contains the HTML table
                
                //var table_div = document.getElementById('content');
                //var table_html = table_div.outerHTML.replace(/ /g, '%20');
                //a.href = uri + ', ' + table_html;
                //setting the file name
                //a.download = 'Daily Secondary Report_' + postfix + '.xls';
                //triggering the function
                //a.click();
                //just in case, prevent default behaviour
                //e.preventDefault();
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
       
       
    </script>
</head>
<body>
    <form id="form" runat="server">
    <asp:HiddenField ID="ddlFieldForce" runat="server" />
    <asp:HiddenField ID="ddlFdate" runat="server" />
    <asp:HiddenField ID="ddlTdate" runat="server" />
    <asp:HiddenField ID="SubDivCode" runat="server" />
    <div class="row">
        <div class="col-sm-8" style="padding-left: 5px;">
            <asp:Label ID="Label2" runat="server" Text="SKU Summary" Style="font-weight: bold;
                font-size: x-large"></asp:Label>
        </div>
        <div class="col-sm-4" style="text-align: right">
            <a name="btnExcel" id="btnExcel" type="button" href="#" class="btn btnExcel"></a>
            <a name="btnClose" id="btnClose" type="button" href="javascript:window.open('','_self').close();"
                class="btn btnClose"></a>
        </div>
    </div>
    <div class="row">
        <br />
        <asp:Label ID="Label1" runat="server" Text="Label" Style="padding-left: 5px;"></asp:Label>
        <br />
        <div id="content">
            <table id="Product_Table" border="1" class="newStly" style="border-collapse: collapse;">
                <thead>
                </thead>
                <tbody>
                </tbody>
                <tfoot>
                </tfoot>
            </table>
        </div>
    </div>
    <%-- <div class="loading" align="center">
        Loading. Please wait.<br />
        <br />
        <%-- <img src="../../../Images/loader.gif" alt="" />
    </div>--%>
    </form>
</body>
</html>

