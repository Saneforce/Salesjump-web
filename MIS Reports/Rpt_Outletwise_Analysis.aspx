<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_Outletwise_Analysis.aspx.cs" Inherits="MIS_Reports_Rpt_Outletwise_Analysis" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Outletwise Analysis</title>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        var sfcalls = [];
        var sfvisit = [];
        var sfsales = [];
        var cproducts = [];
        function getProducts() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Rpt_Outletwise_Analysis.aspx/GEt_Products",
                dataType: "json",
                success: function (data) {
                    cproducts = JSON.parse(data.d) || [];
                    console.log(cproducts);
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function getSfCalls() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Rpt_Outletwise_Analysis.aspx/GEt_Calls",
                dataType: "json",
                success: function (data) {
                    sfcalls = JSON.parse(data.d) || [];
                    console.log(sfcalls);
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }

            });
        }
        function getSfVisit() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Rpt_Outletwise_Analysis.aspx/GEt_Visit",
                dataType: "json",
                success: function (data) {
                    sfvisit = JSON.parse(data.d) || [];
                    console.log(sfvisit);
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }

            });
        }
        function getSfSales() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Rpt_Outletwise_Analysis.aspx/GEt_Sales",
                dataType: "json",
                success: function (data) {
                    sfsales = JSON.parse(data.d) || [];
                    console.log(sfsales);
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }

            });
        }
        function ReloadTable() {
            if (sfcalls.length > 0) {
                $('#Product_Table thead').html('');
                $hstr = '<tr><th rowspan="2">Employee</th><th colspan="6">Coverage</th>';
                $hstr1 = '<tr><th>Outlets Mapped</th><th>Outlets Visited</th><th>Outlets Not Visited</th><th>New Outlets Added</th><th>TC</th><th>EC</th>';
                for (var i = 0; i < cproducts.length; i++) {
                    $hstr += '<th colspan="4">' + cproducts[i].Product_Detail_Name + '</th>';
                    $hstr1 += '<th>O/L Billed</th><th>Target</th><th>Quantity</th><th>Value</th>';
                }
                $hstr += '</tr>';
                $hstr1 += '</tr>';
                $('#Product_Table thead').append($hstr);
                $('#Product_Table thead').append($hstr1);
                $TTC = 0; $TEC = 0;
                var totarr = [];
                for (var i = 0; i < sfcalls.length; i++) {
                    $str = '<tr><td>' + sfcalls[i].SF_Name + '</td><td>' + sfcalls[i].OM + '</td><td>' + sfcalls[i].OV + '</td><td>' + sfcalls[i].ONV + '</td><td>' + sfcalls[i].NOA + '</td>';
                    var fvisit = sfvisit.filter(function (a) {
                        return a.Sf_Code == sfcalls[i].Sf_Code
                    });
                    $str += '<td>' + (fvisit.length > 0 ? fvisit[0].TC : 0) + '</td><td>' + (fvisit.length > 0 ? fvisit[0].EC : 0) + '</td>';
                    $TTC += (fvisit.length > 0 ? parseFloat(fvisit[0].TC) : 0); $TEC += (fvisit.length > 0 ? parseFloat(fvisit[0].EC) : 0);
                    var ar = 0;
                    for (var j = 0; j < cproducts.length; j++) {
                        var fsales = sfsales.filter(function (a) {
                            return (a.Sf_Code == sfcalls[i].Sf_Code) && (a.Product_Code == cproducts[j].Product_Detail_Code)
                        });
                        $str += '<td>' + (fsales.length > 0 ? fsales[0].OLB : 0) + '</td><td>' + (fsales.length > 0 ? fsales[0].TAR : 0) + '</td><td>' + (fsales.length > 0 ? fsales[0].Qty : 0) + '</td><td>' + (fsales.length > 0 ? fsales[0].Value : 0) + '</td>';
                        totarr[ar] = ((fsales[0] != undefined) ? ((isNaN(totarr[ar]) ? 0 : totarr[ar]) + fsales[0].OLB) : ((isNaN(totarr[ar]) ? 0 : totarr[ar]) + 0));
                        ar++;
                        totarr[ar] = ((fsales[0] != undefined) ? ((isNaN(totarr[ar]) ? 0 : totarr[ar]) + fsales[0].TAR) : ((isNaN(totarr[ar]) ? 0 : totarr[ar]) + 0));
                        ar++;
                        totarr[ar] = ((fsales[0] != undefined) ? ((isNaN(totarr[ar]) ? 0 : totarr[ar]) + fsales[0].Qty) : ((isNaN(totarr[ar]) ? 0 : totarr[ar]) + 0));
                        ar++;
                        totarr[ar] = ((fsales[0] != undefined) ? ((isNaN(totarr[ar]) ? 0 : totarr[ar]) + fsales[0].Value) : ((isNaN(totarr[ar]) ? 0 : totarr[ar]) + 0));
                        ar++;
                    }
                    $str += '</tr>';
                    $('#Product_Table tbody').append($str);
                }
                $fstr = '<tr><th colspan="5">Total</th><th>' + $TTC + '</th><th>' + $TEC + '</th>';
                for (var i = 0; i < totarr.length; i++) {
                    $fstr += '<th>' + (totarr[i]).toFixed(2) + '</th>';
                }
                $fstr += '</tr>';
                $('#Product_Table tfoot').append($fstr);
            }
        }
        $(document).ready(function () {
            getProducts(); getSfCalls(); getSfSales(); getSfVisit(); ReloadTable();
            $(document).on('click', "#btnExcel", function (e) {
                var dt = new Date();
                var day = dt.getDate();
                var month = dt.getMonth() + 1;
                var year = dt.getFullYear();
                var postfix = day + "_" + month + "_" + year;
                //creating a temporary HTML link element (they support setting file names)
                var a = document.createElement('a');
                //getting data from our div that contains the HTML table
                var data_type = 'data:application/vnd.ms-excel';
                var table_div = document.getElementById('content');
                var table_html = table_div.outerHTML.replace(/ /g, '%20');
                a.href = data_type + ', ' + table_html;
                //setting the file name
                a.download = $('#<%=Label1.ClientID%>').text() + '.xls';
                //triggering the function
                a.click();
                //just in case, prevent default behaviour
                e.preventDefault();
            });
        })
    </script>
</head>
<body>
    <form id="form1" runat="server" style="margin:22px;">

        <asp:HiddenField ID="hidn_sf_code" runat="server" />
        <asp:HiddenField ID="hFYear" runat="server" />
        <asp:HiddenField ID="hTYear" runat="server" />
        <asp:HiddenField ID="hFMonth" runat="server" />
        <asp:HiddenField ID="hTMonth" runat="server" />
        <asp:HiddenField ID="subDiv" runat="server" />
        <div class="row">
            <div class="col-sm-8" style="padding-left: 5px;">
                <asp:Label ID="Label1" runat="server" Text="Outletwise Analysis" Style="padding-left: 5px; font-size: x-large"></asp:Label>
            </div>
            <div class="col-sm-4" style="text-align: right">
                <a name="btnExcel" id="btnExcel" type="button" href="#" class="btn btnExcel"></a>
                <a name="btnClose" id="btnClose" type="button" href="javascript:window.open('','_self').close();" class="btn btnClose"></a>
            </div>
        </div>
        <div class="row" style="padding: 6px 0px 0px 11px;">
            <asp:Label ID="Label2" Text="Field Force Name :" runat="server" Style="font-size: larger"></asp:Label>
            <asp:Label ID="lblsf_name" runat="server" Style="font-size: larger"></asp:Label>
        </div>
        <div class="row">
            <br />
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
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
        </div>
    </form>
</body>
</html>
