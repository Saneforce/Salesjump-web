<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptPrimary_Order_Dtl_RepView.aspx.cs" Inherits="MasterFiles_rptPrimary_Order_Dtl_RepView" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Primary Order Detail View</title>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link href="../css/style.css" type="text/css" rel="stylesheet" />
    
    <style type="text/css">
        body
        {
            padding: 10px 25px;
        }
        .tableContainer {
            /*max-height: 690px;*/
            height:700px !important;
            overflow-y: scroll;
            padding: 0;
            margin: 0;
            border: 1px solid black;
            width: 100%;
        }

        table {
            width: 100%;
        }

        thead {
            position: sticky;
            top: 0px;
            background-color: white;
        }

        /* for styling only */

        td {
            border: 1px solid black;
        }
    </style>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var div_code = '<%=Session["div_code"]%>';
            //alert(div_code);

            var dc = '';
            dc = '"' + div_code + '"';

            var genReport = '';
            var rshq = [];
            var dRSF = [];
            var dDist = [];
            
            var lvl = 0;

            //$('#Product_Table').empty();

            genReport = function () {

                //alert(rshq.length);
                if (dDist.length > 0 && (rshq.length == 0 || rshq.length > 0)) {
                    //  console.log('en');
                    var ldcode = 0;
                    for (var i = 0; i < dDist.length; i++) {
                        str = '<td class="num2">' + (i + 1) + '</td><td>' + dDist[i].order_no + '</td>';

                        var nStrr = 0;

                        if (ldcode == dDist[i].Dis_Code) {
                            nStrr = 1;
                        }

                        ldcode = dDist[i].Dis_Code;

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
                        str += '<td>' + (dDist[i].EmpID || '') + '</td>';
                        str += '<td><input type="hidden" name="sfcode" value="' + dDist[i].sfCode + '"/>' + dDist[i].sfName + '</td>';
                        str += '<td>' + (dDist[i].User_rank || '') + '</td>';
                        str += '<td>' + (dDist[i].sf_hq || '') + '</td><td>' + (dDist[i].Region || '') + '</td>';
                        str += '<td>' + (dDist[i].Zone || '') + '</td><td>' + (dDist[i].Terr || '') + '</td>';                            
                        str += '<td>' + (dDist[i].Dis_Code || '') + '</td><td>' + (dDist[i].Dis_Name || '') + '</td><td>' + (dDist[i].StateName || '') + '</td>';
                        str += '<td>' + (dDist[i].Order_Date || '') + '</td><td>' + (dDist[i].Pro_cat || '') + '</td>';
                        str += '<td>' + (dDist[i].Product || '') + '</td><td>' + (dDist[i].Qty || '') + '</td><td>' + (dDist[i].Unit || '') + '</td>';
                        str += '<td>' + (dDist[i].Discount || '') + '</td><td>' + (dDist[i].Price || '') + '</td><td>' + (dDist[i].Sale_Value || '') + '</td>';
                        str += '<td>' + (dDist[i].Net_Value || '') + '</td><td>' + (dDist[i].Remarks || '') + '</td>';

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
            var FromDate = $("#<%=txtFromDate.ClientID%>").val();
            var ToDate = $("#<%=txtToDate.ClientID%>").val();
            var SubDivCode = $("#<%=SubDivCode.ClientID%>").val();
            var hdnDt = $("#<%=hdnDate.ClientID%>").val();

            var ReasonArray = [];
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptPrimary_Order_Dtl_RepView.aspx/GetDataD",
                data: "{'SF_Code':'" + sf_code + "'}",
                dataType: "json",
                success: function (data) {
                    dRSF = data.d;
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

            str += '<th style="min-width:auto;">SR Erp Id</th><th style="min-width:auto;">User Name</th><th style="min-width:auto;">User Rank</th>';
            str += '<th style="min-width:auto;">SR HQ</th>';
            str += '<th style="min-width:auto;">Region</th><th style="min-width:auto;">Zone</th>'
            str += '<th style="min-width:auto;">Territory</th>';
            str += '<th style="min-width:auto;">Distributor Code</th><th style="min-width:auto;">Distributor Name</th>';
            str += '<th style="min-width:auto;">State</th>';
            str += '<th style="min-width:auto;">Order Date</th>';
            str += '<th style="min-width:auto;">Primary Category</th><th style="min-width:auto;">Product Name</th><th style="min-width:auto;">Qty(Pieces)</th>';
            str += '<th style="min-width:auto;">Unit</th><th style="min-width:auto;">Discount</th><th style="min-width:auto;">Price</th>';
            str += '<th style="min-width:auto;">Sale Value</th><th style="min-width:auto;">Net Value</th> <th style="min-width:auto;">No Sale Reason</th>';

            $('#Product_Table thead').append('<tr class="mainhead">' + str + '</tr>');

            var ReasonArray = [];
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptPrimary_Order_Dtl_RepView.aspx/GetData",
                data: "{'SF_Code':'" + sf_code + "', 'Fromdate':'" + FromDate + "', 'Todate':'" + ToDate + "', 'SubDivCode':'" + SubDivCode + "'}",
                dataType: "json",
                success: function (data) {
                    dDist = data.d;
                    genReport();
                    console.log(dDist);
                },
                error: function (jqXHR, exception) {
                    //alert(JSON.stringify(data));
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

        $('.btnExcel').click(function () {
           
        });

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
        <div class="card">
            <asp:HiddenField ID="ddlFieldForce" runat="server" />
            <asp:HiddenField ID="txtFromDate" runat="server" />
            <asp:HiddenField ID="txtToDate" runat="server" />
            <asp:HiddenField ID="SubDivCode" runat="server" />
            <asp:HiddenField ID="hdnDate" runat="server" />
            <br />
            <div class="card-body">
                <div class="row">
                    <div class="col-sm-8">
                        <asp:Label ID="lblhead1" runat="server" Style="font-weight: bold; font-size: x-large;padding: 0px 20px;" Text="Product Wise Primary Order Report"></asp:Label>
                    </div>
                    <div class="col-sm-4" style="text-align: right">
                        <a id="btnExcel" style="padding: 0px 20px;" class="btn btnExcel" onclick="tableToExcel('Product_Table', 'Product Wise Primary Order Report')" />
                        <%--<a id="btnExcel" style="padding: 0px 20px;" class="btn btnExcel" onclick="tableToExcel('Product_Table', 'Product Wise Secondary Order Report')" />--%>
                        <a name="btnClose" id="btnClose" type="button" href="javascript:window.open('','_self').close();" class="btn btnClose"></a>
                    </div>
                </div>
                <div>
                    <br />                   
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                    <br />  
                    <div class="tableContainer">
                         <table id="Product_Table" class="newStly table table-responsive">
                            <thead></thead>
                            <tbody></tbody>
                        </table>
                    </div>
                    
                </div>
            </div>
        </div>
    </form>
</body>
</html>
