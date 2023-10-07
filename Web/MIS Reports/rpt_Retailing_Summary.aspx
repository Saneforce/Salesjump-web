<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_Retailing_Summary.aspx.cs" Inherits="MIS_Reports_rpt_Retailing_Summary" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
	<script src="//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
	<script src="https://cdn.jsdelivr.net/gh/linways/table-to-excel@v1.0.4/dist/tableToExcel.js"></script>
    <title></title>
    <script type="text/javascript">
        $(document).ready(function () {
		function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
            var totcl = 0, totpcl=0;
            var grid = document.getElementById("<%=gvtotalorder.ClientID%>");
            var grdrows = grid.rows;
            var tbody = grid.tBodies[0];
            var tr = $(tbody).find('tr');
            var len = tr.length;
            for (var i = 1; i < grdrows.length; i++)
            {
                //var t = grid.rows(i).cells(0).text;
                totcl = totcl + parseInt($("tr").find("td.totcalls").attr('value'));
                totpcl = totpcl + parseInt($("tr").find("td.prodcalls").attr('value'));

            }
        });
        function ExportToExcel() {
            var table = document.getElementById("gridtbl");
            TableToExcel.convert(table, {
                name: `RetailingSummary.xlsx`,
                sheet: {
                    name: 'Sheet1'
                }
            });
        }
        </script>
     <style type="text/css">
         #1 th {
             padding: 2px 5px;
             position: sticky;
             top: 0;
             background-color: #496a9a;
         }
        /*.rptCellBorder
        {
            border: 1px solid;
            border-color: #999999;
        }
        td
        {
            padding: 2px 5px;
        }
        .subTot
        {
            font-size: 11pt;
            font-weight: bold;
        }
        .GrndTot
        {
            font-size: 13pt;
            font-weight: bold;
        }
        .remove
        {
            text-decoration: none;
        }
        .TopButton
        {
            border-color: Black;
            border-width: 1px;
            border-style: Solid;
            font-family: Verdana;
            font-size: 10px;
            height: 25px;
            width: 60px;
        }*/
    </style>
</head>
<body>
    <form id="form1" runat="server"> 
        <div class="container" style="width: 100%;">
            <asp:Panel ID="pnlbutton" runat="server">
            <table width="100%" >
                <tr>
                    <td width="70%" align="left" ></td>
                    <td align="right">
                        <img style="float: right; margin-right: 15px; cursor: pointer; width: 40px; height: 40px; float: right;" alt="" onclick="ExportToExcel()" src="../img/Excel-icon.png" />&nbsp&nbsp&nbsp
                   <asp:LinkButton ID="LinkButton2" runat="Server" style="padding: 0px 20px;" class="btn btnClose"   OnClientClick="javascript:window.open('','_self').close();"/>

                   </td></tr>
            </table>
        </asp:Panel>
             <asp:Panel ID="pnlContents" runat="server" Width="100%">
                 <table width="100%" align="center">
                <tr>
                <td colspan="4" >
                <asp:Label ID="lblHead"  SkinID="lblMand" style="font-weight:bold;FONT-SIZE: 16pt;COLOR: black;FONT-FAMILY:Times New Roman;float: left;padding: 5px;" 
                runat="server"></asp:Label>
                    </td></tr>
                    <tr>
                       
                        <td align="left">
                            <asp:Label ID="lblIDsf_name" Text="Team:" Font-Bold="true"  Font-Underline="true" ForeColor="#476eec" runat="server" ></asp:Label>
                            <asp:Label ID="lblsf_name" runat="server"   Font-Underline="true" SkinID="lblMand"></asp:Label>
                        </td>
                    </tr>
                </table>
          
         
        <div align="center">
             <table border="0" id='1' width="90%">
                  <tr>
                <td width="100%" class="table table-responsive newStly" id="gridtbl">
       
                        <asp:GridView ID="gvtotalorder" runat="server" Width="100%" OnDataBound="OnDataBound"
                                HorizontalAlign="Center" BorderWidth="1px" CellPadding="2" CellSpacing="2"
                                 EmptyDataText="No Data found for View" HeaderStyle-BackColor="#819dfb"
                                AutoGenerateColumns="true"  OnRowDataBound="GridView1_RowDataBound" CssClass="ttable"
                                HeaderStyle-HorizontalAlign="Center" BorderColor="Black" BorderStyle="Solid">                               
                                <Columns>
                                   
                                </Columns>
                               <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                            BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                            VerticalAlign="Middle" />
                            </asp:GridView>
                    </td>
            </tr>
        </table>
            </div>
                 </asp:Panel>

             
        </div>
    </form>
</body>
</html>
