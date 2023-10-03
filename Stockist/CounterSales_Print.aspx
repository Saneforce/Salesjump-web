<%@ Page Title="" Language="C#" MasterPageFile="~/Master_DIS.master" AutoEventWireup="true" CodeFile="CounterSales_Print.aspx.cs" Inherits="Stockist_CounterSales_Print" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script>
        var Alldetails = []; str = "";
        totalQnty = 0.00;
        totalItem = 0;
        totalAmount = 0.00;
        totalTax = 0.00;
        balance = 0.00;
        Totalval = 0.00;

        $(document).ready(function () {
            loadCountersale();

            $('#btnprint').on('click', function () {
                var originalContents = $("body").html();
                var printContents = $("#div").html();
                $("body").html(printContents);
                window.print();
                $("body").html("");
                $("body").html(originalContents);
                return false;
            });

        });
      
        function loadCountersale() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "CounterSales_Print.aspx/GetcountersaleDetail",
                data: "{}",
                dataType: "json",
                success: function (data) {
                    //alert(data.d);
                    Alldetails = JSON.parse(data.d) || [];
                    str = "";
                    $("#div").html("");
                    var str1 = '<table border="0" align="left" class="middletable" style="width: 100%; border-collapse: collapse;"><tbody>';
                    var str2 = '<table border="0" align="left" class="datetable" style="width: 100%; border-collapse: collapse;"><tbody>';

                    str1 += '<tr><td  align="center" style="padding-right:0px;padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen; TimesNewRomen;"><b>' + Alldetails[0].Stockist_Name + '</b></td></tr>';
                    str1 += '<tr><td  align="center" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen; "><b>HAP DAILY</b></td></tr>';
                    str1 += '<tr><td  align="center" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;"><b>' + Alldetails[0].Stockist_Address + '</b></td></tr>';
                    str1 += '<tr><td  align="center" style ="font: bold 14px TimesNewRomen;padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;"><b>Ph:' + Alldetails[0].Stockist_Mobile + '</b></td></tr>';
                    str1 += '<tr><td  align="center" style="width:275px;padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;"><b>GSTIN:' + Alldetails[0].gstn + '</b></td></tr>';
                    str1 += '<tr><td  align="center" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;"><b>SAP CODE:' + Alldetails[0].ERP_Code + '</b></td></tr>';
                    str1 += '<tr><td  align="center" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;"><b>RETAIL INVOICE</b></td></tr></tbody></table>';

                    str2 += '<tr><td  align="left" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;">Bill#: ' + Alldetails[0].Trans_Count_Slno + '</td><td  align="right" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;">Date: ' + Alldetails[0].dt + '</td></tr></tbody></table>';

                    str = str1 + str2;

                    //str += '<table border="0" align="left" class="prodtable" style="width: 100%; border-collapse: collapse;"><tbody>';
                    productheader();

                    for ($i = 0; $i < Alldetails.length; $i++) {
                        var price = Alldetails[$i].Price;
                        var amount = Alldetails[$i].Amount;
                        var qnty = Alldetails[$i].Quantity;
                        var tax = Alldetails[$i].Tax_Total;

                        str += "<tr><td align='left' style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + Alldetails[$i].Product_Name + "</td>";
                        str += "<td  align='left'   style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + price.toFixed(2) + "</td>";
                        str += "<td  align='left' style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + qnty.toFixed(2) + "</td>";
                        str += "<td  align='right'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + price.toFixed(2) + "</td>";
                        str += "<td  align='right'  style='font:12px TimesNewRomen;padding-top:0px;padding-bottom:0px;'>" + amount.toFixed(2) + "</td></tr>";

                        totalQnty += qnty;
                        totalAmount += amount;
                        totalTax += tax;
                    }
                    str += '</tbody>';
                    $('#div').append(str);
                    TotalCalculation();
                },
                error: function (result) {
                }
            });
        }

        function TotalCalculation() {

            Totalval = totalAmount + totalTax;
            netvalue = Math.round(Totalval);
            rndoff = netvalue - Totalval;
            totalItem = Alldetails.length;

            var str3 = '<table border="0" align="left" class="datetable " style="width: 100%; border-collapse: collapse;bottom:50px; position: absolute;"><tbody>';
            str3 += '<tr style="border-top:thin dashed;"><td  align="left" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;">Tot Qty :</td><td  align="left" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;"> ' + totalQnty.toFixed(2) + '</td>';
            str3 += '<td align="right" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;">Total :</td><td align="right" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;"> ' + totalAmount.toFixed(2) + '</td></tr>';
            str3 += '<tr><td  align="left" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;">Total Items :</td><td  align="left" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;"> ' + totalItem + '</td>';
            str3 += '<td align="right" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;">Tot GST: </td><td align="right" style="padding-top: 0px;padding-bottom: 0px;font:12px TimesNewRomen;">' + totalTax.toFixed(2) + '</td></tr>';
            str3 += '<tr style="border-bottom:thin dashed;"><td></td><td></td><td  align="right" style="padding-top: 0px;padding-bottom: 5px;font:12px TimesNewRomen;">Coin Adj:</td><td  align="right" style="padding-top: 0px;padding-bottom: 5px;font:12px TimesNewRomen;"> ' + rndoff.toFixed(2) + '</td></tr>';
            str3 += '<tr style="border-bottom:thin dashed;"><td></td><td  align="right" style="padding-top: 5px;padding-bottom: 5px;font:14px TimesNewRomen;"><b>TOTAL: </b></td><td align="center" style="padding-top: 5px;padding-bottom: 5px;font:14px TimesNewRomen;"><b>' + Totalval.toFixed(2) + '</b></td><td></td></tr></tbody></table>';
            $('#div').append(str3);
        }
        function productheader() {
            str += '<table class="rptOrders" align="left" style="width: 100%; border-collapse: collapse; bordercolor:blue;">';
            str += '<thead><tr style="border-top:thin dashed;border-bottom:thin dashed;font:12px TimesNewRomen;"><td align="left"><b>Item Name</b></td>';
            str += '<td align="center" style="font:12px TimesNewRomen;"><b>MRP</b></td>';
            str += '<td align="center" style="font:12px TimesNewRomen;"><b>Qty</b></td>';
            str += '<td align="center" style="font:12px TimesNewRomen;"><b>Rate</b></td>';
            str += '<td align="center" style="font:12px TimesNewRomen;"><b>Amount</b></td></tr></thead>';
            str += '<tbody>';
        }


    </script>
     <button id="btnprint">Print</button>
            <div class="ticket" id="div" style="position: relative;width:595px;height:842px">
            </div>
</asp:Content>

