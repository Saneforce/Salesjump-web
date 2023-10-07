<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_PrimaryFO_valuewise.aspx.cs" Inherits="MIS_Reports_rpt_PrimaryFO_valuewise" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
        <%--<script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>--%>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script lang="javascript" src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.15.2/xlsx.full.min.js"></script>
<script lang="javascript" src="https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/1.3.8/FileSaver.min.js"></script>
    <script src="https://cdn.jsdelivr.net/gh/linways/table-to-excel@v1.0.4/dist/tableToExcel.js"></script>
    <title>Primary FieldForce Target Vs Sales</title>
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
    <script type="text/jscript">
$(document).ready(function () {
            $.ajax({
            type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_PrimaryFO_valuewise.aspx/Userstarget",
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
                url: "rpt_PrimaryFO_valuewise.aspx/target_detail",
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
                        str = '<th rowspan=2>SlNO</td> <th rowspan=2>FieldForce Name</td>';
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
                            if (("," + dist + ",").indexOf("," + a.Sf_Code + ",") < 0) {
                                dist += a.Sf_Code + ",";
                                return true
                            }
                        });
                        var qn = 0;
                        var ta = 0;
                        var str4 = '<th colspan=2>Total</td>';
                        for (var k = 0; k < month.length; k++) {
                            for (var ij = 0; ij < stks.length; ij++) {
                                res = Dtls.filter(function (a) {
                                    return (a.Sf_Code == stks[ij].Sf_Code && a.monyear == month[k].monye)
                                });
                        if (res.length > 0) {
                            qn += parseInt(Number(res[0].qnty||0));
                            ta += parseInt(Number(res[0].targ||0));

                        }
                        else {

                        }
                    }
                    str4 += '<th>' +ta  + '</th><th>' +qn  + '</th>';

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
				var tst = 0;
                    //str3 += "<td>" + (ij + 1) + "</td><td onclick='popUp(\"" + stks[ij].Stockist_code + "\",<%=FYear%>, <%=fmonth%>, <%=TYear%>, <%=Tmonth%>, <%=subDiv%>,\"" + sf +"\",\"" + stks[ij].Stockist_Name + "\",\"" + sfname+"\")'><a>"+stks[ij].Stockist_Name+"</a></td>";
                    str3 += "<td>" + (ij + 1) + "</td><td>" + stks[ij].Sf_Name+"</td>";       
                            for (var k = 0; k < month.length; k++) {
							
                                res = Dtls.filter(function (a) {
                                    return (a.Sf_Code == stks[ij].Sf_Code && a.monyear == month[k].monye)
                                });
                                if (res.length > 0) {
								tst = 1;
                                    str3 += '<td>' + Number(res[0].targ||0) + '</td><td>' + Number(res[0].qnty||0) + '</td>';
                                    qnty += parseInt(Number(res[0].qnty||0));
                                    targ += parseInt(Number(res[0].targ||0));
                                    qnty1 += parseInt(Number(res[0].qnty||0));
                                    targ1 += parseInt(Number(res[0].targ||0));
                                }
                                else
                                    str3 += '<td>0</td><td>0</td>';
                            }
                            str3 += '<td>' + targ + '</td><td>' + qnty + '</td>';
							if (tst == 1) {
                            $(tbl).find('tbody').append('<tr>' + str3 + '</tr>');
							}
                            str3 = '';
                            qnty = 0;
                            targ = 0;
                        }
                        str4 += '<th>' + targ1 + '</th><th>' +  qnty1 + '</th>';
                        $(tbl).find('tfoot').append('<tr>' + str4 + '</tr>');
                    }
                },

                error: function (result) {
                    console.log(result);
                    alert(JSON.stringify(result));
                }
            });
    $(document).on('click', '#btnExcel', function (e) {

        //var dt = new Date();
        //var day = dt.getDate();
        //var month = dt.getMonth() + 1;
        //var year = dt.getFullYear();
        //var postfix = day + "_" + month + "_" + year;
        //var a = document.createElement('a');
        //var blob = new Blob([$('div[id$=tbl]').html()], {
        //    type: "application/csv;charset=utf-8;"
        //});
        //document.body.appendChild(a);
        //a.href = URL.createObjectURL(blob);
        //a.download = 'PrimaryFO_Valuewise' + postfix + '.xls';
        //a.click();
        //e.preventDefault();

        var table = document.getElementById("tbl");
        TableToExcel.convert(table, {
            name: `PrimaryFO_Valuewise.xlsx`,
            sheet: {
                name: 'PrimaryFO_Valuewise'
            }
        });

    });
});
        
</script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="dsf" runat="server" />
         <div class="container" style="max-width: 100%; width: 100%; text-align: right; padding-right: 50px" >
                <asp:LinkButton ID="btnExcel" runat="Server" Style="padding: 8px 20px;" class="btn btnExcel" />
             <a style="float: right; margin-right: 15px; cursor: pointer; width: 40px; height: 40px; float: right;" onclick="ExportToExcel()" href="javascript:__doPostBack('btnExcel','')" /></a>
        <a name="btnClose" id="btnClose" type="button" style="padding: 8px 20px;" href="javascript:window.open('','_self').close();"
            class="btn btnClose"></a>
        </div>
        <br />
        <br />
        <div id="excelDiv" class="container" style="max-width: 100%; width: 100%">
            <p style="align-content:center">FieldForce Valuewise Primary Target </p>
            <asp:Panel ID="pnlContents" runat="server" Width="100%">
          <div  id="divExcel">
              <div>
          <asp:label ID="lblsfname" Text="Field Force Name:" runat="server"></asp:label>
                <asp:Label ID="txtsfnane"  runat="server" ></asp:Label>
                  </div><div>
                <asp:label ID="lblmonth" Text="From:" runat="server"></asp:label>
                  <asp:Label ID="txtmonyear"  runat="server" ></asp:Label></div>
              
          </div>
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
