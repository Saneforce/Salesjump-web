<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GstInvoiceWithoutMaster.aspx.cs" Inherits="Stockist_GstInvoiceWithoutMaster" %>

<!DOCTYPE html>

<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<%-- <html xmlns="https://www.w3.org/1999/xhtml">--%>
<meta http-equiv="content-type" content="text/html;charset=UTF-8" />
<head>
    <link href="../../css/style.css" rel="stylesheet" />
     <link href="../../css/bootstrap.min.css" rel="stylesheet" type="text/css">
    <style></style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="HiddenField1" runat="Server" />
        <div class="container">
            <div class="row" <%--style="background-color:#f1f2f7;"--%>>
                <button style="float: right; margin-right: 20px;" type="button" id="btnback" class="btn btn-primary">Close</button>
                <button style="float: left; margin-left: 20px;" type="button" id="btnprint" class="btn btn-primary">Print</button>

                <%--                <div style="float: left; margin-right: 20px;">
                    <img src="../img/previous.png" id="btnpre" width:"20px" height:"20px" />
                </div>
                <div style="float: left; margin-right: 20px;">
                    <img src="../img/Next.png" id="btnnxt" width:"20px" height:"20px" />
                </div>--%>
            </div>
            <div class="row">
                <div id="div" style="padding-right: 10px; padding-top: 10px; background-color: #fff;">
                </div>
                <asp:HiddenField ID="hid_stockist_name" runat="server" />
                <asp:HiddenField ID="hid_order_id" runat="server" />
                <asp:HiddenField ID="hid_Stockist" runat="server" />
                <asp:HiddenField ID="hid_div" runat="server" />
                <asp:HiddenField ID="hid_cust" runat="server" />
            </div>
        </div>
    </form>
    <script type="text/javascript" src="../../js/jQuery-2.2.0.min.js"></script>
    <script type="text/javascript">
        var str = ""; var sl_no; var Orders = []; pgNo = 1; PgRecords = 45; TotalPg = 0; var AllOrdersdetails = []; var AllOrderspro = []; var $i; var netval = 0;
        var cgst = 0; var sgst = 0; totlcase = 0; totalpie = 0; cashdis = 0.00; grossvalue = 0; totalGst = 0.00; roundoff = 0.00; netvalue = 0.00; rndoff = 0.00;
        totaltax = 0.00; netwrd = ''; netwrd1 = ''; netwrd2 = ''; pagenumber = 0; PgRecordscnt = PgRecords; var nam1 = ''; var order_idx = 0;var hiden = ''; var imsgename = '';

        $(document).ready(function () {
            

            var comm = GetParameterValues('Order_id');
            function GetParameterValues(param) {
                var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
                for (var i = 0; i < url.length; i++) {
                    var urlparam = url[i].split('=');
                    if (urlparam[0] == param) {
                        return urlparam[1];
                    }
                }
            }

            var Orderid = comm.replace(/,\s*$/, "")
            nam1 = Orderid.split(",")
            if (nam1.length > 0) {
                $('#div').empty();
                for (i = 0; i < nam1.length; i++) {
                    loadData(nam1[i]);
                }
            }




            //function load_order_wise(data) {
            //    loadData(data);
            //}

            //$('#btnnxt').on('click', function () {
            //    order_idx = order_idx + 1;
            //    if (nam1.length > order_idx) {
            //        load_order_wise(nam1[order_idx]);
            //    }
            //    else {
            //        order_idx = nam1.length - 1;
            //    }
            //});

            //$('#btnpre').on('click', function () {
            //    order_idx = order_idx - 1;

            //    if (order_idx >= 0) {
            //        load_order_wise(nam1[order_idx]);
            //    }
            //    else {
            //        order_idx = 0;
            //    }
            //});

            $('#btnprint').on('click', function () {
                var originalContents = $("body").html();
                var printContents = $("#div").html();
                $("body").html(printContents);
                window.print();
                $("body").html("");
                $("body").html(originalContents);
                return false;
            });

            $('#btnback').on('click', function () {
                window.location.href = "/Stockist/Billing.aspx";
            });
        });
        function loaddivision() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "GstInvoiceWithoutMaster.aspx/GetDivision",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {                    
                    Alldivision = JSON.parse(data.d) || [];
                }
            });
        }

        function loadDistributor(CusCode, InvNo) {
            loaddivision();
            var hiden = $('#<%= HiddenField1.ClientID %>').val();
            var imsgename = '' + hiden + '_logo.png';
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "GstInvoiceWithoutMaster.aspx/GetDistributor",
                data: "{'cust_code':'" + CusCode + "','order_id':'" + InvNo + "'}",
                dataType: "json",
                success: function (data) {
                    AllOrdersdetails = JSON.parse(data.d) || [];

                    var str1 = '<div class="head" style="font-family: "Times New Roman", Times, serif;"><table border="0" class="headtable" align="left" style="width: 35%;margin-top:75px; border-collapse: collapse;"><tbody>';
                    var str2 = '<table border="0" align="left" class="middletable" style="width: 35%; border-collapse: collapse;"><tbody>';
                    var str3 = '<table border="0" align="left" class="lasttable" style="width: 30%; border-collapse: collapse;margin-top:75px"><tbody>';

                    var a = 0;
                    $('#<%=hid_stockist_name.ClientID%>').val(AllOrdersdetails[a].Stockist_Name);


                    str1 += '<tr><td  align="left" style="padding-right:0px; padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen; TimesNewRomen;display:none;">Hatsun Toll Free Support: 18001237355</td></tr>';
                    str1 += '<tr><td  align="left" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen; ">Dis State Name:-' + AllOrdersdetails[a].Retstate + '</td></tr>';
                    str1 += '<tr><td  align="left" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;display:none;"><b>Dis GST Tin No:</b>&nbsp;&nbsp;' + AllOrdersdetails[a].RetgstTin + '</td></tr>';
                    str1 += '<tr><td  align="left" style ="font: bold 14px TimesNewRomen;padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;"><b>' + AllOrdersdetails[a].ListedDr_Name + '</b></td></tr>';
                    str1 += '<tr><td  align="left" style="width:275px;padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;">' + AllOrdersdetails[a].ListedDr_Address1 + '</td></tr>';
                    str1 += '<tr><td  align="left" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;">Distributor Ph NO:- ' + AllOrdersdetails[a].RetMobile + '</td></tr></tbody></table>';

                    
                    str2 += '<tr align="center"><td ><img style="width:100px;height:100px;" src="http://fmcg.sanfmcg.com/limg/'+imsgename+'" /></td></tr>';
                    str2 += '<tr><td align="left" style="font: bold 16px TimesNewRomen;padding-top: 0px;padding-bottom: 0px;"><b>' + AllOrdersdetails[a].Stockist_Name + '</b></td></tr>';
                    str2 += '<tr><td align="left" style="font: bold 12px TimesNewRomen;padding-top: 0px;padding-bottom: 0px;"><b>(Authorised Supplier for '+Alldivision[0].Division_Name+'  ) </b></td></tr>';
                    str2 += '<tr><td align="left" style="width:250px;padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;">' + AllOrdersdetails[a].Stockist_Address + '</td></tr>';
                    str2 += '<tr><td align="center" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;">PH NO:&nbsp;&nbsp;' + AllOrdersdetails[a].Stockist_Mobile + '</b></td></tr>';
                    str2 += '<tr><td colspan="5" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;"></td></tr>';
                    str2 += '<tr><td colspan="5" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;"></td></tr></tbody></table>';

                    str3 += '<tr><td align="center" style="font: bold 16px TimesNewRomen;padding-top: 0px;padding-left:10px;padding-bottom: 0px;"><b>GST INVOICE</b></td></tr>';
                    str3 += '<tr><td align="left" style="padding-left:30px;padding-top:0px;padding-bottom:0px;font:12px TimesNewRomen;">Dist GSTIN:-' + AllOrdersdetails[a].DistgstTin + '</td></tr >';
                    str3 += '<tr><td align="left" style="padding-left:30px;padding-top:0px;padding-bottom:0px;font:12px TimesNewRomen;">Bill Date :-' + AllOrdersdetails[a].billdate + '</td></tr>';
                    str3 += '<tr><td align="left" style="padding-left:30px;padding-top:0px;padding-bottom:0px;font:12px TimesNewRomen;">Bill No:-' + AllOrdersdetails[a].billno + '</td></tr>';
                    str3 += '<tr><td align="left" style="padding-left:30px;padding-top:0px;padding-bottom:0px;font:12px TimesNewRomen;">D.State Name:- ' + AllOrdersdetails[a].Diststate + '</td></tr> ';
                    str3 += '<tr><td align="left" style="padding-left:30px;padding-top:0px;padding-bottom:0px;font:12px TimesNewRomen;">D.State Code:-' + AllOrdersdetails[a].DistStatecd + '</td></tr></tbody></table></div>';

                    str += str1 + str2 + str3;

                }
            });
        }
        function loadData(Orderid) {
            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "GstInvoiceWithoutMaster.aspx/GetProductdetails",
                data: "{'orderid':'" + Orderid + "'}",
                dataType: "json",
                async: false,
                success: function (data) {
                    AllOrderspro = JSON.parse(data.d) || [];
                    Orders = AllOrderspro;
                    ReloadTable();
                },
                error: function (result) {
                }
            });
        }

        function ReloadTable() {

            var tot_gross = 0;
            var rowcount = 0;
            cgst = 0;
            sgst = 0;
            totlcase = 0;
            totalpie = 0;
            cashdis = 0.00;
            grossvalue = 0;
            totalGst = 0.00;
            totaltax = 0.00;
            //pagenumber = 1;
            pgNo = 1;
            Multipage = '<div class="fullpage" style="padding-right: 10px;padding-top: 10px;background-color:#f1f2f7;"><page size="A4" layout="portrait" class="Printarea" ><div  class="page" style="font-family: "Times New Roman", Times, serif;page-break-before:always;">';
            singlepg = '<div class="fullpage" style="padding-right: 10px;padding-top: 10px;background-color:#f1f2f7;"><div  class="page" style="font-family: "Times New Roman", Times, serif;background-color:#f1f2f7;">';

            for (x = 0; x < (Orders.length / PgRecords); x++) {
                pagenumber++;
                rowcount = 0;
                str = '';
                st = PgRecords * (pgNo - 1); slno = 0;
                if (PgRecords > Orders.length) {
                    str += singlepg;
                }
                else
                    str += Multipage;

                loadDistributor(Orders[0].Stockist_Code, Orders[0].Trans_Inv_Slno);
                productheader();
                for ($i = st; $i < st + PgRecords; $i++) {
                    if ($i > Orders.length) {
                        str += "<tr><td class='Sh' style='padding-top:0px;padding-bottom:0px;height:14px;'></td></tr>";
                    }
                    if ($i < Orders.length) {

                        var caseval = "";
                        var pc = "";
                        var amtbyCase = 0.00;
                        var amount = Orders[$i].Amount;
                        var discnt = Orders[$i].Discount;
                        var cgstv = Orders[$i].CGST || 0;
                        var sgstv = Orders[$i].SGST || 0;
                        var grossamnt = amount + Orders[$i].Tax;
                        if (Orders[$i].qty == 0) {
                            amtbyCase = grossamnt;
                        } else {
                            amtbyCase = grossamnt / Orders[$i].qty;
                        }
                        rowcount = rowcount + 1;

                        str += "<tr><td class='Sh' align='center' style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'><b>" + ($i + 1) + "</b></td>";
                        str += "<td class='Sh' align='left'   style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'><b>" + Orders[$i].Product_Name + "</b></td>";
                        str += "<td class='Sh' align='center' style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + Orders[$i].HSN_Code + "</td>";
                        str += "<td class='Sh' align='right'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + Orders[$i].qty + "</td>";
                        str += "<td class='Sh' align='right'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + Orders[$i].Quantity + "</td>";
                        str += "<td class='Sh' align='right'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + Orders[$i].MRP_Price + "</td>";
                        str += "<td class='Sh' align='right'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + Orders[$i].Rate + "</td>";
                        str += "<td class='Sh' align='right'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + Orders[$i].Free + "</td>";
                        str += "<td class='Sh' align='right'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + discnt.toFixed(2) + "</td>";
                        str += "<td class='Sh' align='right'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + cgstv.toFixed(2) + "</td>";
                        str += "<td class='Sh' align='right'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + sgstv.toFixed(2) + "</td>";
                        str += "<td class='Sh' align='right'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + amtbyCase.toFixed(2) + "</td>";
                        str += "<td class='Sh' align='right'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;display:none;'>" + amount.toFixed(2) + "</td>";
                        str += "<td class='Sh' align='right'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + grossamnt.toFixed(2) + "</td></tr>";



                        cashdis += Orders[$i].Discount;
                        totlcase += Orders[$i].qty;
                        totalpie += Orders[$i].Quantity;
                        cgst += Orders[$i].CGST || 0;
                        sgst += Orders[$i].SGST || 0;
                        grossvalue += Orders[$i].Amount;
                        totalGst += Orders[$i].Tax;
                    }
                }
                if (Orders.length > PgRecordscnt) {
                    PgRecordscnt = PgRecordscnt + PgRecords;
                    str += "<tr><td class='Sh' align='right' colspan='13' style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'><b>Continue</b></td></tr>";
                    str += '</tbody></table><table class="pageno" style="background-color:#fff;width: 100%;"><tr style="font:12px TimesNewRomen;"><td colspan="8" style="padding-left:350px;float:center;">' + pagenumber + '</td><td colspan="3" style="float:right"></td></tr></table><div style="break-after:page"></div>';
                    str += '</div></page></div>';
                }
                else {
                    str += '</div>';
                }
                $('#div').append(str);
                pgNo++;
            }
            gstcalculi();

        }

        function productheader() {
            str += '<div class="product" style="font:12px TimesNewRomen;"><table class="rptOrders" align="center" style="width: 100%; border-collapse: collapse; bordercolor:blue;">';
            str += '<thead><tr style="border-top:thin dashed;border-bottom:thin dashed;font:12px TimesNewRomen;"><td class="Sh1" align="center" rowspan="2"><font face="calibri"/><b>Sl.No</b></td><td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>DESCRIPTION</b></td>';
            str += '<td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>HSN Code</b></td><td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>Case</b></td><td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>Pc</b></td>';
            str += '<td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>MRP</b></td><td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>Rate</b></td>';
            str += '<td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>FrQty</b></td>';
            str += '<td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>S.Disc</b></td></td><td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>CGST</b></td>';
            str += '<td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>SGST</b></td></td><td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>Amt/Case</b></td>';
            str += '<td class="Sh1" align="center" rowspan="2" style="font:12px TimesNewRomen;"><font face="calibri"/><b>Amount</b></td></tr></thead>';
            str += '<tbody>';
        }

        function numberToWords(number) {
            var digit = ['zero', 'one', 'two', 'three', 'four', 'five', 'six', 'seven', 'eight', 'nine'];
            var elevenSeries = ['ten', 'eleven', 'twelve', 'thirteen', 'fourteen', 'fifteen', 'sixteen', 'seventeen', 'eighteen', 'nineteen'];
            var countingByTens = ['twenty', 'thirty', 'forty', 'fifty', 'sixty', 'seventy', 'eighty', 'ninety'];
            var shortScale = ['', 'thousand', 'million', 'billion', 'trillion'];

            number = number.toString(); number = number.replace(/[\, ]/g, ''); if (number != parseFloat(number)) return 'not a number'; var x = number.indexOf('.'); if (x == -1) x = number.length; if (x > 15) return 'too big'; var n = number.split(''); var str = ''; var sk = 0; for (var i = 0; i < x; i++) { if ((x - i) % 3 == 2) { if (n[i] == '1') { str += elevenSeries[Number(n[i + 1])] + ' '; i++; sk = 1; } else if (n[i] != 0) { str += countingByTens[n[i] - 2] + ' '; sk = 1; } } else if (n[i] != 0) { str += digit[n[i]] + ' '; if ((x - i) % 3 == 0) str += 'hundred '; sk = 1; } if ((x - i) % 3 == 1) { if (sk) str += shortScale[(x - i - 1) / 3] + ' '; sk = 0; } } if (x != number.length) { var y = number.length; str += 'point '; for (var i = x + 1; i < y; i++) str += digit[n[i]] + ' '; } str = str.replace(/\number+/g, ' '); return str.trim() + ".";

        }

        function gstcalculi() {
            netval = grossvalue + totalGst - cashdis;
            netvalue = Math.round(netval);
            rndoff = netvalue - netval;
            intpart = netvalue.toString().split(".")[0];
            floatpart = netvalue.toString().split(".")[1];

            if (floatpart == undefined) {
                netwrd1 = "Zero"
            } else if (floatpart.length != 2) {
                floatpart += "0";
                netno1 = numberToWords(parseInt(floatpart)).replace(".", "");
                netwrd1 = netno1.replace(/^(.)|\s+(.)/g, c => c.toUpperCase());
            }
            netno = numberToWords(parseInt(intpart)).replace(".", "");
            netwrd = netno.replace(/^(.)|\s+(.)/g, c => c.toUpperCase());

            var distributor = "";
            distributor = $('#<%=hid_stockist_name.ClientID%>').val();

            str = '<div style="padding-bottom: -5px;background-color:#f1f2f7;" class="total"><table border="0" class="rpttotal" align="center" style="width: 100%; border-collapse: collapse; bordercolor:blue;"><tbody>';
            str += '<tr style="border-top:thin dashed;font:12px TimesNewRomen;"> <td></td><td></td> <td colspan="12" ><b>** GST Summary **</b></td></tr>';
            str += '<tr style="font:12px TimesNewRomen;"><td style="padding-top:0px;padding-bottom:0px;" colspan="2"><b>GST Desc</b></td><td style="padding-top: 0px;padding-bottom: 0px;"><b>GST%</b></td><td style="padding-top: 0px;padding-bottom: 0px;"><b>GST Amount</b></td><td style="padding-top: 0px;padding-bottom: 0px;" colspan="2"><b></b></td><td  colspan="4" style="padding-top:0px;padding-bottom:0px;"><b>Total Case:</b>&nbsp;&nbsp;' + totlcase + '</td><td style="padding-top:0px;padding-bottom:0px;"><b>Gross Value :</b>&nbsp;&nbsp;</td><td style="float:right;padding-top:0px;padding-bottom:0px;">' + grossvalue.toFixed(2) + '</td><td></td></tr>';
            str += '<tr style="font:12px TimesNewRomen;"><td style="padding-top:0px;padding-bottom:0px;" colspan="2"><b>CGST</b></td><td style="padding-top: 0px;padding-bottom: 0px;">0</td><td style="padding-top:0px;padding-bottom:0px;">' + cgst.toFixed(2) + '</td><td style="padding-top:0px;padding-bottom:0px;" colspan="3"></td><td  colspan="3" style="padding-top:0px;padding-bottom:0px;"><b>Total Piece:</b>&nbsp;&nbsp;' + totalpie + '</td><td style="padding-top:0px;padding-bottom:0px;"><b>Scheme Amt :</b></td><td style="float:right;padding-top:0px;padding-bottom:0px;">0.00</td><td></td></tr>';
            str += '<tr style="font:12px TimesNewRomen;"><td style="padding-top:0px;padding-bottom:0px;" colspan="2"><b>SGST</b></td><td style="padding-top: 0px;padding-bottom: 0px;">0</td><td style="padding-top:0px;padding-bottom:0px;">' + sgst.toFixed(2) + '</td><td style="padding-top:0px;padding-bottom:0px;" colspan="6"></td><td style="padding-top:0px;padding-bottom:0px;"><b>Cash Disc :</b>&nbsp;&nbsp;</td><td style="float:right;padding-top:0px;padding-bottom:0px;">' + cashdis.toFixed(2) + '</td><td></td></tr>';
            str += '<tr style="font:12px TimesNewRomen;"><td colspan="10" style="padding-top: 0px;padding-bottom: 0px;"></td><td style="padding-top: 0px;padding-bottom: 0px;"><b>GST Value :</b>&nbsp;&nbsp;</td><td style="float:right;padding-bottom: 0px;padding-top: 0px;">' + totalGst.toFixed(2) + '</td><td></td></tr>';
            str += '<tr style="border-bottom:thin dashed;font:12px TimesNewRomen;"><td colspan="10" style="padding-top: 0px;padding-bottom: 0px;"></td><td style="padding-top: 0px;padding-bottom: 0px;"><b>Round Off(+/-) :</b>&nbsp;&nbsp;</td><td style="float:right;padding-top: 0px;padding-bottom: 0px;">' + rndoff.toFixed(2) + '</td><td></td></tr>';
            str += '<tr style="border-bottom:thin dashed; padding-left:20px;font-family:monospace;font-size:14px;"><td  colspan="8">' + netwrd + ' Rupees  and ' + netwrd1 + ' Paise</td> </td><td></td><td></td><td><b>Net Value :</b>&nbsp;&nbsp;</td><td style="float:right; padding-bottom: 0px;">' + netvalue.toFixed(2) + '</td><td></td></tr></tbody></table ></div>';
            str += '<table border="0" class="rpttotal1" align="center" style="width: 100%; border-collapse: collapse; bordercolor:blue;background-color:#f1f2f7;"><tbody><tr style="font:12px TimesNewRomen;"><td  colspan="8" style="padding-left:20px"></td><td colspan="1" style="padding-top: 20px;font:bold 12px TimesNewRomen;float:right;"><b>For ' + distributor + '</b></td></tr>';
            str += '<tr style="font:12px TimesNewRomen;"><td colspan="8" style="padding-left:20px">Buyers Sign</td><td colspan="3" style="float:right">Authorized Signatory</td></tr></tbody></table><table class="pageno"><tr style="font:12px TimesNewRomen;"><td colspan="8" style="padding-left:350px;float:center">' + pagenumber + '</td><td colspan="3" style="float:right"></td></tr></table></div></div><div style="break-after:page"></div>';
            $('#div').append(str);

        }
    </script>
</body>
</html>
