<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_Merchand_summary.aspx.cs" Inherits="MIS_Reports_rpt_Merchand_summary" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
  <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
      <script type="text/javascript" src="../js/jquery.CongelarFilaColumna.js"></script>
        
          <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
       
            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

        <script lang="javascript" src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.15.2/xlsx.full.min.js"></script>
<script lang="javascript" src="https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/1.3.8/FileSaver.min.js"></script>
        <%--<script type="text/javascript" src="../js/jquery.table2excel.js"></script>--%>
        <script src="https://cdn.jsdelivr.net/gh/linways/table-to-excel@v1.0.4/dist/tableToExcel.js"></script>
    <style>
        #sec_table th {
             position: sticky;
             top: 0;
             background-color: #496a9a;
         }
    </style>
    <title></title>
    <script>
        $(document).ready(function () {
            var di = $('.ContenedorTabla');
            $(di).empty();
            var htm = '<table class="table table-responsive newStly" style="width: 85%;" border="1" id="sec_table"> <thead></thead><tbody></tbody> </table>';
            $(di).append(htm);
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_Merchand_summary.aspx/getdatalist",
                data: "{}",
                dataType: "json",
                success: function (data) {
                    var slno = 0,stk=0,sale=0,tot=0,count=0;
                    res = JSON.parse(data.d);
                    if(res.length>0){
                    str = '<th>SI.No.</th><th>FieldForce Name</th><th>Outlet</th><th>Date</th><th>Meterial Name</th><th>Stock</th><th>Sale Qty</th><th>Current Stock</th>';
                    $('#sec_table thead').append('<tr>' + str + '</tr>');
                    for (var i = 0; i < res.length; i++) {
                        var length = res.length;
                        if (res[i].rn == "1") {
                            count++;
                            if (count > 1) {
                                var str = '<td></td><td></td><td></td><td></td><td style="font-weight:bold;color:blue;">Total</td><td style="font-weight:bold;">' + stk + '</td><td style="font-weight:bold;">' + sale + '</td><td style="font-weight:bold;">' + tot + '</td>';
                                $('#sec_table tbody').append('<tr>' + str + '</tr>');
                                stk = 0; sale = 0; tot = 0;
                            }
                        }
                        var str = '<td>' + (++slno) + '</td><td><input type="hidden" name="sfcode" value="' + res[i].sf_code + '"/>' + res[i].User_Name + '</td><td><input type="hidden" name="cuscode" value="' + res[i].customer_code + '"/>' + res[i].ListedDr_Name + '</td><td>' + res[i].Dcr_Date + '</td><td><input type="hidden" name="cuscode" value="' + res[i].pop_code + '"/>' + res[i].pop_name + '</td><td>' + res[i].stock + '</td><td>' + res[i].SalQ + '</td><td>' + res[i].totalst + '</td>';
                        $('#sec_table tbody').append('<tr>' + str + '</tr>');
                        stk += res[i].stock;
                        sale += res[i].SalQ;
                        tot += res[i].totalst;
                        if (i + 1 == length) {
                            var str = '<td></td><td></td><td></td><td></td><td style="font-weight:bold;color:blue;">Total</td><td style="font-weight:bold;">' + stk + '</td><td style="font-weight:bold;">' + sale + '</td><td style="font-weight:bold;">' + tot + '</td>';
                            $('#sec_table tbody').append('<tr>' + str + '</tr>');
                        }
                    }
                    }
                    else
                    {
                        $('#sec_table tbody').append('<tr><td style="font-weight:bold;">No Record Found...</td></tr>');
                    }

                }

            });
        });
    </script>
    <script language="Javascript">
        function RefreshParent() {
            window.close();
        }
          function ExportToExcel() {
            var table = document.getElementById("sec_table");
            TableToExcel.convert(table, {
                name: `MerchandSummary.xlsx`,
                sheet: {
                    name: 'Sheet1'
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>

             <asp:Panel ID="pnlbutton" runat="server">
            <table width="100%" >
                <tr>
                    <td width="70%" align="left" ></td>
                    <td align="right">
                        <img style="float: right; margin-right: 15px; cursor: pointer; width: 40px; height: 40px; float: right;" alt="" onclick="ExportToExcel()" src="../img/Excel-icon.png" />&nbsp&nbsp&nbsp
                   <%--<asp:LinkButton ID="btnExcel"  runat="Server" Style="padding: 0px 20px;" class="btn btnExcel" onclick="ExportToExcel()" /> --%>
                   <asp:LinkButton ID="LinkButton1" runat="Server" style="padding: 0px 20px;" class="btn btnClose"   OnClientClick="javascript:window.open('','_self').close();"/>

                   </td></tr>
            </table>
        </asp:Panel>

             <asp:Panel id="pnlContents"  runat="server" Width="100%">
                <table border="0" id='1'  width="90%" style="margin:auto;">
                    <tr>
                    <td width="70%" align="left" >
                    <asp:Label ID="lblHead" Text="Retail Lost  Details" SkinID="lblMand" Font-Bold="true"  Font-Underline="true"
                runat="server" Font-Size="Medium"></asp:Label>
                    </td>
					</tr>
                    <br />
          <tr align="right">
		  <td align="left" style="font-size: small; font-weight: bold;font-family: Andalus;">FieldForce Name:
                   <asp:Label ID="Feild" runat="server" Text="" ForeColor="Red"></asp:Label>
				   </td>
				   </tr>
<br/>
             <tr> 
                        <td width="100%" colspan="2">
            <main class="main">
		<div class="ContenedorTabla" class="card-body table-responsive" style="max-width:100%;"></div></main>
         </td>
                    </tr>
                      </table>
                 <br />
                            </asp:Panel>
        </div>
    </form>
</body>
</html>
