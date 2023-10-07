<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_primarySTKwise_targetVSsale.aspx.cs" Inherits="MIS_Reports_rpt_primarySTKwise_targetVSsale" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link type="text/css" rel="stylesheet" href="../css/style1.css" />
        <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <title>Primary Distributer Target Vs Sales </title>
   <style type="text/css">
         tfoot,thead
        {
            padding: 3px 8px;
            background-color: #496a9a;
            color: #fff;
            border: 1px solid #bbb;
            font-weight: bold;
        } 
    </style>
    <script type="text/jscript" >

        function popUp(st_code, FYear, fmonth, TYear, Tmonth, subDiv,sfcode, st_Name,Sf_name) {

            strOpen = "rpt_Pdtwise_targetVSsale.aspx?st_code=" + st_code + "&FYear=" + FYear + "&fmonth=" + fmonth + "&TYear=" + TYear + "&Tmonth=" + Tmonth + "&subDiv=" + subDiv  + "&SF_Name=" + Sf_name + "&st_Name=" + st_Name + "&Sf_Code=" + sfcode
            window.open(strOpen, '_blank', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=1000,height=600,left = 0,top = 0');

        }

        $(document).ready(function () {
            //  $(document).ready(function () {
            var sf = $("#<%=dsf.ClientID%>").val();
			 var sfname = $("#<%= txtsfnane.ClientID%>").text();
            var Dtls = 0;
            var stk = 0;
            $.ajax({
            type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_primarySTKwise_targetVSsale.aspx/stockisttarget",
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
                url: "rpt_primarySTKwise_targetVSsale.aspx/target_detail",
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
                        str = '<th rowspan=3> SlNO</td><th  rowspan=2>  FieldForce Name</td> <th  rowspan=3>  Distributor Name</td>';
                        var mon = '';
                        var dist = '';
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
                            if (("," + dist + ",").indexOf("," + a.Stockist_code + ",") < 0) {
                                dist += a.Stockist_code + ",";
                                //if (("," + mon + ",").indexOf("," + a.Stockist_Name + ",") < 0) {
                                //     stoki += a.Stockist_Name + ",";
                                return true
                            }
                        });
                        var qn = 0;
                        var ta = 0;
                        var str4 = '<th colspan=3>Total</td>';
                        for (var k = 0; k < month.length; k++) {
                            for (var ij = 0; ij < stks.length; ij++) {
                                res = Dtls.filter(function (a) {
                                    return (a.Stockist_code == stks[ij].Stockist_code && a.monyear == month[k].monye)
                                });
                        if (res.length > 0) {
                            qn += parseInt(Number(res[0].qnty||0).toFixed(2));
                            ta += parseInt(Number(res[0].targ||0).toFixed(2));

                        }
                        else {

                        }
                    }
                    str4 += '<th style="text-align:right;">' +ta.toFixed(2)  + '</th><th style="text-align:right;">' +qn.toFixed(2)  + '</th>';

                }
                for (var i = 0; i < month.length; i++) {
                    str += '<th colspan=2>' + month[i].monye + '</th>';
                    str1 += '<th colspan=1>Target</th><th colspan=1>Sale</th>';

                    //  $(tbl).find('thead').append('<tr>' + str + '</tr>');
                    // $(tbl).find('thead').append('<tr>' + str1 + '</tr>');

                }

                str += '<th colspan=2>Total</th>';
                str1 += '<th colspan=1>Target</th><th colspan=1>Sale</th>';
                        $(tbl).find('thead').append('<tr>' + str + '</tr>');
                        $(tbl).find('thead').append('<tr>' + str1 + '</tr>');
                var qnty = 0;
                var targ = 0;
                var  qnty1 = 0;
                var   targ1 = 0;
                for (var ij = 0; ij < stks.length; ij++) {
                    //str3 += "<td>" + (ij + 1) + "</td><td onclick='popUp(\"" + stks[ij].Stockist_code + "\",<%=FYear%>, <%=fmonth%>, <%=TYear%>, <%=Tmonth%>, <%=subDiv%>,\"" + sf +"\",\"" + stks[ij].Stockist_Name + "\",\"" + sfname+"\")'><a>"+stks[ij].Stockist_Name+"</a></td>";
                     str3 += "<td>" + (ij + 1) + "</td><td>" + stks[ij].Sf_Name +"</td><td>"+stks[ij].Stockist_Name+"</td>";       
                            for (var k = 0; k < month.length; k++) {
                                res = Dtls.filter(function (a) {
                                    return (a.Stockist_code == stks[ij].Stockist_code && a.monyear == month[k].monye)
                                });
                                if (res.length > 0) {
                                    str3 += '<td style="text-align:right;">' + Number(res[0].targ||0).toFixed(2) + '</td><td style="text-align:right;">' + Number(res[0].qnty||0).toFixed(2) + '</td>';
                                    qnty += parseInt(Number(res[0].qnty||0));
                                    targ += parseInt(Number(res[0].targ||0));
                                    qnty1 += parseInt(Number(res[0].qnty||0));
                                    targ1 += parseInt(Number(res[0].targ||0));
                                }
                                else
                                    str3 += '<td>0</td><td>0</td>';
                            }
                            str3 += '<td style="text-align:right;">' + targ.toFixed(2) + '</td><td style="text-align:right;">' + qnty.toFixed(2) + '</td>';
                            $(tbl).find('tbody').append('<tr>' + str3 + '</tr>');
                            str3 = '';
                            qnty = 0;
                            targ = 0;
                        }
                        str4 += '<th style="text-align:right;">' + targ1.toFixed(2) + '</th><th style="text-align:right;">' +  qnty1.toFixed(2) + '</th>';
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
         <asp:HiddenField ID="dsf" runat="server" />
        <div>          
               
       
       <p style="align-content:center">    Distributorwise Primary Target </p>
               <asp:Panel ID="pnlContents" runat="server" Width="100%">
          <div  id="divExcel">
              <div>
          <asp:label ID="lblsfname" Text="Field Force Name:" runat="server"></asp:label>
                <asp:Label ID="txtsfnane"  runat="server" ></asp:Label>
                  </div><div>
                <asp:label ID="lblmonth" Text="From:" runat="server"></asp:label>
                  <asp:Label ID="txtmonyear"  runat="server" ></asp:Label></div>
          </div>
            <div style="text-align:right" >
        <asp:LinkButton ID="btnExcel" runat="Server" style="padding: 0px 20px;" class="btn btnExcel" />
       	<a name="btnClose" id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();" class="btn btnClose"></a>
        </div>
		  <div>
            <table id="tbl"  class="table newStly">
                <thead></thead>
                <tbody></tbody>
                <tfoot></tfoot>
            </table>           
            </asp:Panel>
            </div>               
    </form>
</body>
</html>

