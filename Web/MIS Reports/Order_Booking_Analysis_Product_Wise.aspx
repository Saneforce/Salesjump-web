<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Order_Booking_Analysis_Product_Wise.aspx.cs"
    Inherits="MIS_Reports_Order_Booking_Analysis_Product_Wise" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
       <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function RefreshParent() {
            window.close();
        }
        $(document).ready(function () {
            dProd = []; dPQV = []; dSF = [];
            dTcEc = [];
            genReport = function () {
                if (dSF.length > 0 && dProd.length > 0 && dPQV.length > 0) {
                    for (var i = 0; i < dSF.length; i++) {
                        str = '<td><input type="hidden" name="sfcode" value="' + dSF[i].sfCode + '"/> <p name="sfname" style="margin: 0 0 0px;">' + dSF[i].sfName + '</p> </td>';
                        var tc = 0, ec = 0;
                        fP = dTcEc.filter(function (a) { return (a.sfCode == dSF[i].sfCode); });
                        if (fP.length > 0) {
                            tc = fP[0].TC_Count, ec = fP[0].EC_Count;
                        }
                        var TotVal = 0;
                        for (var j = 0; j < dProd.length; j++) {
                            var q = "", v = "";
                            fP = dPQV.filter(function (a) { return (a.sfCode == dSF[i].sfCode && a.proCode == dProd[j].product_id); });
                            if (fP.length > 0) {
                                q = fP[0].caseRate, v = fP[0].amount;
                                TotVal += Number(fP[0].amount);
                            }
                            str += '<td>' + q + '</td><td>' + ((v != "") ? parseFloat(v).toFixed(2) : "") + '</td>';
                            
                        }
                        //str += '<td>' + tc + '</td><td>' + ec + '</td>';
                        str += '<td>' + parseFloat(TotVal).toFixed(2) + '</td><td>' + tc + '</td><td>' + ec + '</td>';
                        $('#Product_Table tbody').append('<tr>' + str + '</tr>');
                    }
                }
            }

            $('#Product_Table tr').remove();
            var len = 0;
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Order_Booking_Analysis_Product_Wise.aspx/getdata",
                dataType: "json",
                success: function (data) {
                    dProd = data.d;
                    genReport();
                    len = data.d.length;
                    if (data.d.length > 0) {
                        str = '<th  style="min-width:250px; " " rowspan="2"> <p style="margin: 0 0 0px;">Field Force</p> </th>';
                        str1 = ''
                        strff = '<th style="min-width:250px;"> <p style="margin: 0 0 0px;">Total</p> </th>';
                        for (var i = 0; i < data.d.length; i++) {
                            str += '<th style="min-width:150px" colspan="2"> <input type="hidden" name="pcode" value="' + data.d[i].product_id + '"/> <p name="pname" style="margin: 0 0 0px;">' + data.d[i].product_name + '</p> </th>';
                            str1 += '<th>Quantity</th><th>Value</th>';
                            strff += '<th style="text-align: right" ></th><th style="text-align: right"></th>';
                        }
                        $('#Product_Table thead').append('<tr class="mainhead">' + str + '<th colspan="3">Calls</th></tr>');
                        $('#Product_Table thead').append('<tr class="secondhead">' + str1 + '<th>Total</th><th>TC</th><th>EC</th></tr>');
                        $('#Product_Table tfoot').append('<tr class="trfoot">' + strff + '<th></th><th></th><th></th></tr>');
                    }
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });

            var sfCode = $("#<%=hidn_sf_code.ClientID%>").val();
            var Fyear = $("#<%=lblyear.ClientID%>").val();
            var FMonth = $("#<%=ddlFMonth.ClientID%>").val();

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Order_Booking_Analysis_Product_Wise.aspx/getIssuDataTcEc",
                data: "{'SF_Code':'" + sfCode + "', 'FYera':'" + Fyear + "', 'FMonth':'" + FMonth + "'}",
                dataType: "json",
                success: function (data) {
                    dTcEc = data.d;
                    // console.log(dTcEc);
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

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Order_Booking_Analysis_Product_Wise.aspx/getIssuData",
                data: "{'SF_Code':'" + sfCode + "', 'FYera':'" + Fyear + "', 'FMonth':'" + FMonth + "'}",
                dataType: "json",
                success: function (data) {
                    dPQV = data.d;
                    genReport();
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });


            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Order_Booking_Analysis_Product_Wise.aspx/getSalesforce",
                data: "{'SF_Code':'" + sfCode + "'}",
                dataType: "json",
                success: function (data) {
                    dSF = data.d;
                    genReport();
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });

            var arr = [];
            $('#Product_Table tbody tr').each(function () {
                var i = 0;
                $(this).find('td').each(function () {
                    if (i != 0) {
                        arr[i - 1] = Number(arr[i - 1] || 0) + Number($(this).text() || 0);
                    }
                    i++;
                });
            });
            // console.log(arr);

            var i = 0;
            $('.trfoot th').each(function () {
                if (i != 0) {
                    //console.log($(this));

                    $(this).text(parseFloat(arr[i - 1]).toFixed(2));

                }
                i++;
            });

            $('.secondhead th').each(function (i) {
                var remove = 0;

                var tds = $(this).parents('table').find('tr td:nth-child(' + (i + 2) + ')')


                tds.each(function (j) {
                    if (this.innerHTML == '') remove++;
                });
                if (remove == ($('#Product_Table tbody tr').length)) {
                    $(this).hide();
                    tds.hide();
                    $('#Product_Table tfoot tr th').eq($(this).index() + 1).hide();
                    //  if (i > 0) {
                    if (i % 2 == 0) {
                        $('.mainhead th').eq((i / 2) + 1).hide();
                    }
                    //}
                }
            });
        });
    </script>
    <style type="text/css">
        input[type='text'], select, label
        {
            line-height: 22px;
            padding: 4px 6px;
            font-size: medium;
            border-radius: 7px;
            width: 100%;
        }
        thead th
        {
            border: 1px solid #ececec;
        }
        
        
        tbody td
        {
            text-align: right;
        }
        
        
        tbody td:first-child
        {
            text-align: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="top" style="width:95%; margin:0px auto;">
            <table style="width:100%">
                <tr>
                    <td>
                        <asp:Label ID="lblHead" SkinID="lblMand" Style="font-weight: bold; font-size: 24pt;
                            color: black; font-family: fantasy; float: left; padding: 5px;" runat="server"></asp:Label>
                    </td>
                    <td style="text-align:right">
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton  ID="btnPrint" runat="server" style="padding: 0px 20px;" class="btn btnPrint" 
                                        OnClick="btnPrint_Click" />
                                </td>
                                <td>
                                    <asp:LinkButton  ID="btnExcel" runat="server" style="padding: 0px 20px;" class="btn btnExcel" 
                                        OnClick="btnExcel_Click" />
                                </td>
                                <td>
                                    <asp:LinkButton  ID="btnClose" runat="server" style="padding: 0px 20px;"  class="btn btnClose" 
                                        OnClientClick="RefreshParent();" OnClick="btnClose_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td style="vertical-align: bottom;">
                        <asp:Label ID="lblIDsf_name" Text="Team:" Font-Bold="true" Font-Underline="true"
                            ForeColor="#476eec" runat="server"></asp:Label>
                        <asp:Label ID="lblsf_name" runat="server" Font-Underline="true" SkinID="lblMand"></asp:Label>
                    </td>
                    <td>
                        <asp:Image ID="logoo" runat="server" Style="width: 28%; border-width: 0px; height: 39px;">
                        </asp:Image>
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="hidn_sf_code" runat="server" />
        <asp:HiddenField ID="imgpath" runat="server" />
        <asp:HiddenField ID="lblyear" runat="server" />
        <asp:HiddenField ID="ddlFMonth" runat="server" />
        <div class="row" style="width:95%; margin:0px auto;">
            <div >
                <div id="printableArea" class="page">
                    <table id="Product_Table" class="newStly">
                        <thead>
                        </thead>
                        <tbody>
                        </tbody>
                        <tfoot>
                        </tfoot>
                    </table>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
