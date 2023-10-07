<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_SOB_POB_Order_Report.aspx.cs"
    Inherits="MIS_Reports_rpt_SOB_POB_Order_Report_" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="https://code.jquery.com/jquery-3.3.1.js"></script>
    <script type="text/javascript">
        jQuery(document).ready(function ($) {
            var sfList = [];
            var priDt = [];

            var Fyear = $("#<%=hFYear.ClientID%>").val();
            var FMonth = $("#<%=hFMonth.ClientID%>").val();
            var SubDivCode = $("#<%=hSubDiv.ClientID%>").val();
            var SfCode = $("#<%=hsfCode.ClientID%>").val();
            var desig = $("#<%=hdesignation.ClientID%>").val();

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_SOB_POB_Order_Report.aspx/GetFieldForce",
                data: "{'sfCode':'" + SfCode + "'}",
                dataType: "json",
                success: function (data) {
                    // console.log(data.d);
                    sfList = data.d;
                },
                error: function (jqXHR, exception) {
                    console.log(jqXHR);
                    console.log(exception);
                }
            });





            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_SOB_POB_Order_Report.aspx/GetPrimaryDt",
                data: "{'subDiv':'" + SubDivCode + "','FYear':'" + Fyear + "','FMonth':'" + FMonth + "','sfCode':'" + SfCode + "','Designation':'" + desig + "'}",
                dataType: "json",
                success: function (data) {
                    console.log(data.d);
                    priDt = data.d;
                },
                error: function (jqXHR, exception) {
                    console.log(jqXHR);
                    console.log(exception);
                }
            });

            var tbl = $('#FFTable');
            if (sfList.length > 0) {

                var oSTot = 0;
                var oPTot = 0;
                var oSTot_qty = 0;
                var oPTot_qty = 0;

                var oSTC = 0;
                var oSEC = 0;
                var oPTC = 0;
                var oPEC = 0;
                var opho = 0;
                var ochTot = 0;
                cnt = 1;

                var dateParts = Fyear.split("/");
                var fdt = new Date(+dateParts[2], dateParts[1] - 1, +dateParts[0]);

                var dateParts1 = FMonth.split("/");
                var tdt = new Date(+dateParts1[2], dateParts1[1] - 1, +dateParts1[0]);

                while (fdt <= tdt) {

                    var d = ((Number(fdt.getDate()) < 10) ? '0' : '') + (Number(fdt.getDate()));
                    var m = ((((Number(fdt.getMonth() + 1)) < 10) ? '0' : '') + (Number(fdt.getMonth() + 1)));
                    var dt = (d) + "/" + m + "/" + fdt.getFullYear();



                    dRt = priDt.filter(function (obj) {
                        return obj.sDate == dt;
                    });
                    if (dRt.length > 0) {
                        for (var i = 0; i < sfList.length; i++) {




                            oV = priDt.filter(function (obj) {
                                return obj.sfCode == sfList[i].sfCode;
                            });
                            var oSTV = 0;
                            var oPTV = 0;
                            var oSTV_qty = 0;
                            var oPTV_qty = 0;
                            var oPTTC = 0;
                            var oPTEC = 0;
                            var oSTTC = 0;
                            var oSTEC = 0;
                            var phOrd = 0;

                            if (oV.length > 0) {
                                for (var n = 0; n < oV.length; n++) {

                                    if (oV[n].type == "P") {
                                        oPTTC++;
                                    }
                                    else {
                                        oSTTC++;
                                    }

                                    if (Number(oV[n].POBVal) > 0) {
                                        oPTEC++;
                                    }
                                    if (Number(oV[n].SOB_Val) > 0) {
                                        oSTEC++;
                                    }

                                    if (Number(oV[n].Phone_Ooder) > 0) {
                                        phOrd = phOrd + Number(oV[n].Phone_Ooder);
                                    }

                                    oSTV = oSTV + Number(oV[n].SOB_Val);
                                    oPTV = oPTV + Number(oV[n].POBVal);

                                    oSTV_qty = oSTV_qty + Number(oV[n].SOP_Qty);
                                    oPTV_qty = oPTV_qty + Number(oV[n].POB_Qty);
                                }
                            }



                            Rt = dRt.filter(function (obj) {
                                return obj.sfCode == sfList[i].sfCode;
                            });

                            //console.log(Rt.length);

                            if (Rt.length > 0) {

                                stc = 0;
                                sec = 0;
                                ptc = 0;
                                pec = 0;
                                pho = 0;


                                RoName = '';
                                for (var j = 0; j < Rt.length; j++) {


                                    if (RoName.indexOf(Rt[j].RoteName) == -1) {
                                        RoName += Rt[j].RoteName + ', ';
                                    }

                                    if (RoName.indexOf(Rt[j].Address1) == -1) {
                                        RoName += Rt[j].Address1 + ', ';
                                    }


                                    str = '';
                                    if (Rt[j].type == "P") {
                                        ptc++;
                                    }
                                    else {
                                        stc++;
                                    }
                                    if (Number(Rt[j].POBVal) > 0) {
                                        pec++;
                                    }
                                    if (Number(Rt[j].SOB_Val) > 0) {
                                        sec++;
                                    }


                                    if (Number(Rt[j].Phone_Ooder) > 0) {
                                        pho++;
                                    }



                                    if (j == 0) {
                                        str += '<tr><td>' + (cnt++) + '</td><td>' + Rt[j].rtName + '</td><td rowspan="' + Rt.length + '"><a class="btndcr" id="' + (sfList[i].sfCode + d + 'date') + '"></a></td><td rowspan="' + Rt.length + '"><span id="' + (sfList[i].sfCode + d + 'Rt12') + '"></span></td><td>' + Rt[j].Emp_Code + '</td><td>' + sfList[i].sfName + '</td>';
                                        str += '<td>' + Rt[j].Working_with + '</td><td>' + Rt[j].SOP_Qty + '</td><td>' + Rt[j].SOB_Val + '</td><td>' + Rt[j].POB_Qty + '</td><td>' + Rt[j].POBVal + '</td><td rowspan="' + Rt.length + '"><span id="' + (sfList[i].sfCode + d + 'ST') + '"></span></td><td rowspan="' + Rt.length + '"><span id="' + (sfList[i].sfCode + d + 'SE') + '"></span></td><td rowspan="' + Rt.length + '"><span id="' + (sfList[i].sfCode + d + 'PT') + '"></span></td><td   rowspan="' + Rt.length + '"><span id="' + (sfList[i].sfCode + d + 'PE') + '"></span></td><td>' + Rt[j].Phone_Ooder + '</td><td>' + Rt[j].instrument_type + '</td><td>' + Rt[j].POB + '</td>  <td  rowspan="' + Rt.length + '">' + oSTV_qty.toFixed(0) + '</td>  <td  rowspan="' + Rt.length + '">' + oSTV.toFixed(2) + '</td><td  rowspan="' + Rt.length + '">' + oPTV_qty.toFixed(0) + '</td><td  rowspan="' + Rt.length + '">' + oPTV.toFixed(2) + '</td><td  rowspan="' + Rt.length + '">' + oSTTC + '</td><td  rowspan="' + Rt.length + '">' + oSTEC + '</td><td  rowspan="' + Rt.length + '">' + oPTTC + '</td><td  rowspan="' + Rt.length + '">' + oPTEC + '</td>  <td>' + phOrd + '</td> </tr>';
                                    }
                                    else {
                                        str += '<tr><td>' + (cnt++) + '</td><td>' + Rt[j].rtName + '</td><td>' + Rt[j].Emp_Code + '</td><td>' + sfList[i].sfName + '</td>';
                                        str += '<td>' + Rt[j].Working_with + '</td><td>' + Rt[j].SOP_Qty + '</td><td>' + Rt[j].SOB_Val + '</td><td>' + Rt[j].POB_Qty + '</td><td>' + Rt[j].POBVal + '</td><td>' + Rt[j].Phone_Ooder + '</td><td>' + Rt[j].instrument_type + '</td><td>' + Rt[j].POB + '</td>  <td>' + phOrd + '</td></tr>';
                                        //<td>' + oSTV.toFixed(2) + '</td><td>' + oPTV.toFixed(2) + '</td>  <td>' + oSTTC + '</td><td>' + oSTEC + '</td><td>' + oPTTC + '</td><td>' + oPTEC + '</td></tr>';
                                    }

                                    $(tbl).find('tbody').append(str);
                                    $(tbl).find('tbody').find('#' + (sfList[i].sfCode + d + 'ST')).text(stc);
                                    $(tbl).find('tbody').find('#' + (sfList[i].sfCode + d + 'SE')).text(sec);
                                    $(tbl).find('tbody').find('#' + (sfList[i].sfCode + d + 'PT')).text(ptc);
                                    $(tbl).find('tbody').find('#' + (sfList[i].sfCode + d + 'Rt12')).text(RoName);

                                    $(tbl).find('tbody').find('#' + (sfList[i].sfCode + d + 'date')).text(Rt[j].sDate);
                                    $(tbl).find('tbody').find('#' + (sfList[i].sfCode + d + 'date')).attr('sf', sfList[i].sfCode);
                                    $(tbl).find('tbody').find('#' + (sfList[i].sfCode + d + 'date')).attr('sfN', sfList[i].sfName);
                                    $(tbl).find('tbody').find('#' + (sfList[i].sfCode + d + 'date')).attr('fdate', Fyear);
                                    $(tbl).find('tbody').find('#' + (sfList[i].sfCode + d + 'date')).attr('tDate', FMonth);
                                    //  $(tbl).find('tbody').find('#' + (sfList[i].sfCode + d + 'date')).text(Rt[j].sDate);
                                    // $(tbl).find('tbody').find('#' + (sfList[i].sfCode + d + 'sfCode')).text(sfList[i].sfName);
                                    //$(tbl).find('tbody').find('#' + (sfList[i].sfCode + d + 'erCode')).text(Rt[j].Emp_Code);

                                    $(tbl).find('tbody').find('#' + (sfList[i].sfCode + d + 'PE')).text(pec);

                                    oSTot = Number(oSTot || 0) + Number(Rt[j].SOB_Val || 0);
                                    oPTot = Number(oPTot || 0) + Number(Rt[j].POBVal || 0);

                                    oSTot_qty = Number(oSTot_qty || 0) + Number(Rt[j].SOP_Qty || 0);
                                    oPTot_qty = Number(oPTot_qty || 0) + Number(Rt[j].POB_Qty || 0);

                                    ochTot = Number(ochTot) + Number(Rt[j].POB);
                                }
                                oSTC = Number(oSTC) + Number(stc);
                                oSEC = Number(oSEC) + Number(sec);
                                oPTC = Number(oPTC) + Number(ptc);
                                oPEC = Number(oPEC) + Number(pec);
                                opho = Number(opho) + Number(pho);

                            }
                        }
                    }
                    fdt.setDate(fdt.getDate() + 1);
                }
                str = '<td colspan="7">Total</td><td>' + oSTot_qty.toFixed(0) + '</td><td>' + oSTot.toFixed(2) + '</td><td>' + oPTot_qty.toFixed(0) + '</td><td>' + oPTot.toFixed(2) + '</td><td>' + oSTC + '</td><td>' + oSEC + '</td><td>' + oPTC + '</td><td>' + oPEC + '</td><td>' + opho + '</td><td></td><td>' + ochTot + '</td><td>-</td><td>-</td> <td>-</td><td>-</td><td>-</td><td>-</td><td>-</td><td>-</td><td>-</td>';
                $(tbl).find('tbody').append('<tr style="background-color: #496a9a;color: white;">' + str + '</tr>');
            }



            $(document).on('click', '#btnExcel', function (e) {

                var a = document.createElement('a');

                var fileName = 'Test file.xls';
                var blob = new Blob([$('div[id$=exlDiv]').html()], {
                    type: "application/csv;charset=utf-8;"
                });
                document.body.appendChild(a);
                a.href = URL.createObjectURL(blob);
                a.download = 'SOB_POB_Order.xls';
                a.click();
                e.preventDefault();

            });
            $(document).on('click', ".btndcr", function () {

                sfC = $(this).attr('sf');
                sfN = $(this).attr('sfN');

                var Fyear = $("#<%=hFYear.ClientID%>").val();
                var FMonth = $("#<%=hFMonth.ClientID%>").val();
                var divCode = $("#<%=hdivCode.ClientID%>").val();

                var dateParts = Fyear.split("/");
                var fdt = new Date(+dateParts[2], dateParts[1] - 1, +dateParts[0]);
                var d = ((Number(fdt.getDate()) < 10) ? '0' : '') + (Number(fdt.getDate()));
                var m = ((((Number(fdt.getMonth() + 1)) < 10) ? '0' : '') + (Number(fdt.getMonth() + 1)));
                var fDate = Fyear; //fdt.getFullYear() + '-' + m + '-' + d;
                var dateParts1 = FMonth.split("/");
                var tdt = new Date(+dateParts1[2], dateParts1[1] - 1, +dateParts1[0]);
                d = ((Number(tdt.getDate()) < 10) ? '0' : '') + (Number(tdt.getDate()));
                m = ((((Number(tdt.getMonth() + 1)) < 10) ? '0' : '') + (Number(tdt.getMonth() + 1)));
                var tDate = FMonth; //fdt.getFullYear() + '-' + m + '-' + d;




                var str = '../MasterFiles/Reports/Rpt_DCR_View.aspx?sf_code=' + sfC + '&div_code=' + divCode + '&cur_month=' + fdt.getMonth() + '&cur_year=' + fdt.getFullYear() + '&Mode=View_All_DCR_Date(s)&Sf_Name=' + sfN + '&FDate=' + fDate + '&TDate=' + tDate;
                window.open(str, "popupWindow", 'resizable=yes,toolbar=no,scrollbars=1,menubar=no,status=no,width=1100,height=700,left=0,top=0');
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container" style="max-width: 100%; width: 100%">
        <div class="row">
            <div class="col-sm-8">
            </div>
            <div class="col-sm-4" style="text-align: right">
                <a name="btnExcel" id="btnPrint" type="button" style="padding: 0px 20px; display: none"
                    href="#" class="btn btnPrint"></a><a name="btnExcel" id="btnPdf" type="button" style="padding: 0px 20px;
                        display: none" href="#" class="btn btnPdf"></a><a name="btnExcel" id="btnExcel" type="button"
                            style="padding: 0px 20px;" href="#" class="btn btnExcel"></a><a name="btnClose" id="btnClose"
                                type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();"
                                class="btn btnClose"></a>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hFYear" runat="server" />
    <asp:HiddenField ID="hFMonth" runat="server" />
    <asp:HiddenField ID="hSubDiv" runat="server" />
    <asp:HiddenField ID="hsfCode" runat="server" />
    <asp:HiddenField ID="hdivCode" runat="server" />
    <asp:HiddenField ID="hdesignation" runat="server" />
    <div id="exlDiv" class="container" style="max-width: 100%; width: 100%">
        <div class="row">
            <div class="col-sm-8">
                <asp:Label ID="Label1" runat="server" Text="SOB POB Orders" Style="font-weight: bold;
                    font-size: x-large"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-8">
                <asp:Label ID="Label2" runat="server"></asp:Label>
            </div>
        </div>
        <table id="FFTable" class="newStly" border="1" style="width: 100%; border-collapse: collapse;">
            <thead>
                <tr>
                    <th colspan="18">
                        FTD
                    </th>
                    <th colspan="9">
                        MTD
                    </th>
                </tr>
                <tr>
                    <th>
                        SlNo.
                    </th>
                    <th>
                        Party Name
                    </th>
                    <th>
                        Date
                    </th>
                    <th>
                        Working Station
                    </th>
                    <th>
                        Employee Code
                    </th>
                    <th>
                        Order Taken
                    </th>
                    <th>
                        Working With
                    </th>
                    <th>
                         No of Products-SOB
                    </th>
                    <th>
                        SOB-Order
                    </th>
                    <th>
                         No of Products-POB
                    </th>
                    <th>
                        POB-Order
                    </th>
                    <th>
                        SOB-All Call
                    </th>
                    <th>
                        SOB-Productive Call
                    </th>
                    <th>
                        POB-All Call
                    </th>
                    <th>
                        POB-Productive Call
                    </th>
                     <th>
                        POB-Phone Order
                    </th>
                    <th>
                        Cheque/cash
                    </th>
                    <th>
                        collection Value
                    </th>
                     <th>
                        No of Products-SOB
                    </th>
                    <th>
                        SOB-Order
                    </th>
                    <th>
                        No of Products-POB
                    </th>
                    <th>
                        POB-Order
                    </th>
                    <th>
                        SOB-All Call
                    </th>
                    <th>
                        SOB-Productive Call
                    </th>
                    <th>
                        POB-All Call
                    </th>
                    <th>
                        POB-Productive Call
                    </th>
                    <th>
                        POB-Phone Order
                    </th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
