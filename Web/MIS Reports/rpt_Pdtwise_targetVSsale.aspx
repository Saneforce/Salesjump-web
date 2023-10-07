<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_Pdtwise_targetVSsale.aspx.cs" Inherits="MIS_Reports_rpt_Pdtwise_targetVSsale" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link type="text/css" rel="stylesheet" href="../css/style1.css" />
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <title>Primary Product Target Vs Sales </title>
    <style>

    </style>
    <script type="text/jscript" >
        $(document).ready(function () {
            //  $(document).ready(function () {
            var Dtls = 0;
            var stk = 0;
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_Pdtwise_targetVSsale.aspx/monthtarget",
                dataType: "json",
                success: function (data) {
                    stk = data.d;
                },
                error: function (result) {
                    console.log(result);
                    alert(JSON.stringify(result));
                }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_Pdtwise_targetVSsale.aspx/target_detail",
                dataType: "json",
                success: function (data) {
                    Dtls = data.d;
                    if (Dtls.length > 0) {
                        tbl = $("[id*=tbl]");  
                        $(tbl).find('thead').find('tr').remove();
                        $(tbl).find('tbody').find('tr').remove();
                        $(tbl).find('tfoot').find('tr').remove();
                        var str3 = '';
                        var str = '';
                        str = '<th rowspan=3 align> SlNO</td> <th  rowspan=3>Product Name</td>';
                        var mon = '';
                        var proc = '';
                        var str1 = '';
                        var str2 = '';
                        var stoki = '';

                        month = stk.filter(function (a) {
                            if (("," + mon + ",").indexOf("," + a.monye + ",") < 0) {
                                mon += a.monye + ",";
                                return true
                            }
                        });
                        stks = Dtls.filter(function (a) {
                            if (("," + proc + ",").indexOf("," + a.P_code + ",") < 0) {
                                proc += a.P_code + ",";
                                //if (("," + mon + ",").indexOf("," + a.Stockist_Name + ",") < 0) {
                                //     stoki += a.Stockist_Name + ",";
                                return true
                            }
                        });
                        var tqnty = 0;
                        var tval = 0;
                           var sqnty = 0;
                        var sval = 0;
                        var str4 = '<th colspan=2>Total</td>';
                        for (var k = 0; k < month.length; k++) {
                            for (var ij = 0; ij < stks.length; ij++) {
                                res = Dtls.filter(function (a) {
                                    return (a.P_code == stks[ij].P_code && a.monyear == month[k].monye)
                                });
                                if (res.length > 0) {
                                    tqnty += parseInt(Number(res[0].targ||0));
                                     tval += parseInt(Number(res[0].tval||0));
                                    sqnty += parseInt(Number(res[0].CQty||0));
                                     sval += parseInt(Number(res[0].value||0));
                                }
                                else {

                                }
                            }
                            str4 += '<th>' + tqnty + '</th><th>' + tval + '</th><th>' + sqnty + '</th><th>' + sval + '</th>';
                            tqnty = 0;
                            tval = 0;
                                 sqnty = 0;
                            sval = 0;
                        }
                        var str6 = '';
                        for (var i = 0; i < month.length; i++) {
                            str += '<th colspan=4>' + month[i].monye + '</th>';
                            str1 += '<th colspan=2>Target</th><th colspan=2>Sale</th>';
                                str6 += '<th colspan=1>Qty</th><th colspan=1>Value</th><th colspan=1>Qty</th><th colspan=1>Value</th>';
                            //  $(tbl).find('thead').append('<tr>' + str + '</tr>');
                            // $(tbl).find('thead').append('<tr>' + str1 + '</tr>');

                        }

                        str += '<th colspan=4>Total</th>';
                        str1 += '<th colspan=2>Target</th><th colspan=2>Sale</th>';
                           str6 += '<th colspan=1>Qty</th><th colspan=1>Value</th><th colspan=1>Qty</th><th colspan=1>Value</th>';
                        $(tbl).find('thead').append('<tr>' + str + '</tr>');
                        $(tbl).find('thead').append('<tr>' + str1 + '</tr>');
                           $(tbl).find('thead').append('<tr>' + str6 + '</tr>');
                         var tqnty1 = 0;
                        var tval1 = 0;
                           var sqnty1 = 0;
                        var sval1 = 0;
                         var tqnty2 = 0;
                        var tval2 = 0;
                           var sqnty2 = 0;
                        var sval2 = 0;
                        for (var ij = 0; ij < stks.length; ij++) {
                            str3 += '<td>' + (ij + 1) + '</td><td>' + stks[ij].p_name + '</td>';

                            for (var k = 0; k < month.length; k++) {
                                res = Dtls.filter(function (a) {
                                    return (a.P_code == stks[ij].P_code && a.monyear == month[k].monye)
                                });
                                if (res.length > 0) {
                                     tqnty1 += parseInt(Number(res[0].targ||0));
                                    tval1 += parseInt(Number(res[0].tval||0));
                                    sqnty1 += parseInt(Number(res[0].CQty||0));
                                    sval1 += parseInt(Number(res[0].value||0));
                                    
                                      tqnty2 += parseInt(Number(res[0].targ||0));
                                    tval2 += parseInt(Number(res[0].tval||0));
                                    sqnty2 += parseInt(Number(res[0].CQty||0));
                                    sval2 += parseInt(Number(res[0].value||0));
                                    str3 += '<td>' + Number(res[0].targ||0) + '</td><td>' + Number(res[0].tval||0) + '</td><td>' + Number(res[0].CQty||0) + '</td><td>' + Number(res[0].value||0) + '</td>';
                                   
                                }
                                else
                                    str3 += '<td>0</td><td>0</td><td>0</td><td>0</td>';
                            }
                            str3 += '<td>' + tqnty1 + '</td><td>' + tval1 + '</td><td>' + sqnty1 + '</td><td>' + sval1 + '</td>';
                           
                            $(tbl).find('tbody').append('<tr>' + str3 + '</tr>');

                            str3 = '';
                            tqnty1 = 0;
                            tval1 = 0;
                            sqnty1 = 0;
                            sval1 = 0;
                        }
                        str4 += '<th>' + tqnty2 + '</th><th>' + tval2 + '</th><th>' + sqnty2 + '</th><th>' + sval2 + '</th>';
                        $(tbl).find('tfoot').append('<tr>' + str4 + '</tr>');
                    }
                },

                error: function (result) {
                    console.log(result);
                    alert(JSON.stringify(result));
                }
            });

        });     
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
       <div>          
               
       
       <p style="align-content:center">    ProductWise Primary Target</p>
               <asp:Panel ID="pnlContents" runat="server" Width="100%">
          <div  id="divExcel">
              <div>
          <asp:label ID="lblsfname" Text="Field Force Name:" runat="server"></asp:label>
                <asp:Label ID="txtsfnane"  runat="server" ></asp:Label>
                  </div>
               <div>
          <asp:label ID="Lblstname1" Text="Distributor Name:" runat="server"></asp:label>
                <asp:Label ID="Lblstname"  runat="server" ></asp:Label>
                  </div><div>
                <asp:label ID="lblmonth" Text="From:" runat="server"></asp:label>
                  <asp:Label ID="txtmonyear"  runat="server" ></asp:Label></div>
          </div>
            <table id="tbl"  class="table newStly">
                <thead></thead>
                <tbody></tbody>
                <tfoot></tfoot>
            </table>
		</div>
            </asp:Panel>
            </div>
               
    </form>
</body>
</html>
