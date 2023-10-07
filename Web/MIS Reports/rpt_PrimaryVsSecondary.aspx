<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_PrimaryVsSecondary.aspx.cs" Inherits="MIS_Reports_rpt_PrimaryVsSecondary" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Primary Vs Secondary Sales</title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
      <script src='https://cdn.rawgit.com/simonbengtsson/jsPDF/requirejs-fix-dist/dist/jspdf.debug.js'></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src='https://unpkg.com/jspdf-autotable@2.3.2'></script>

    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />

    <script>
        function RefreshParent() {
            window.close();
        }
    </script>

    <script type="text/javascript">

        $(document).ready(function () {
            $(document).on('click', "#btnPrint", function () {
                var originalContents = $("body").html();
                var printContents = $("#content").html();
                $("body").html(printContents);
                window.print();
                $("body").html(originalContents);
                return false;
            });

            $(document).on('click', '#btnExport', function (e) {
                var data_type = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html());
                var a = document.createElement('a');
                a.href = data_type;
                a.download = 'Primary_Vs_Secondary xls';
                a.click();
                e.preventDefault();
            });



        });
    </script>

    <style>
            .TopButton {
             border-color: Black;
             border-width: 1px;
             border-style: Solid;
             font-family: Verdana;
             font-size: 10px;
             height: 25px;
             width: 60px;
         }
    </style>
</head>
<body>
    <form id="form1" runat="server">
   <div>
      <asp:Panel ID="pnlbutton" runat="server">
             <table width="100%">
                <tr>
                <td width="20%"></td>
                    <td width="80%" align="center" >
                    <asp:Label ID="lblHead" Font-Names="Andalus" Font-Bold="true"  Font-Underline="true"              
                       runat="server" Font-Size="Large"></asp:Label>
                    </td>
                    <td align="right">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnPrint" runat="server" Text="Print" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                         OnClick="btnPrint_Click" />
                                </td>
                                <td>

<td><asp:Button ID="btnExport" runat="server" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px" Text="Excel" OnClick="btnExport_Click" /></td>
                               
                                </td>
                                <td> 
                    <input id="pdfexport" class="TopButton" type="button" value="PDF" height="35px"  onclick="generate()"" >   


</td>
                               
                                <td>
                                    <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClientClick="RefreshParent();" OnClick="btnClose_Click" />
                                </td>

                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>


              <asp:Panel id="pnlContents"  runat="server" Width="100%">
                <table border="0" id='1'   width="100%">                 
                                                   
          <tr align="right"><td align="left" style="font-size: small; PADDING: 0PX 52PX; font-weight: bold;font-family: Andalus;">FieldForce Name:
                   <asp:Label ID="Feild" runat="server" Text="" ForeColor="Red"></asp:Label></td></tr>
                     <tr> 
                        <td width="100%" style="TEXT-ALIGN: -webkit-center;">
                        
                            <asp:GridView ID="pri_vs_sec_sales_grid" runat="server" Width="95%"   ShowHeader="false"
                                HorizontalAlign="Center" OnRowCreated="pri_vs_sec_sales_grid_RowCreated" 
                                BorderWidth="1px" CellPadding="2" EmptyDataText="No Data found for View" 
                                AutoGenerateColumns="true" CssClass="newStly" Font-Size="Small"
                                HeaderStyle-HorizontalAlign="Center" >                               
                                <Columns>
                               </Columns>
                                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>             
                      </td>
                         </tr>
    </asp:Panel>

        <script type="text/javascript">

            function generate() {

                var doc = new jsPDF('l', 'pt');

                var res = doc.autoTableHtmlToJson(document.getElementById("pri_vs_sec_sales_grid"));

                var header = function (data) {
                    doc.setFontSize(10);
                    doc.setTextColor(40);
                    doc.setFontStyle('normal');
                    //doc.addImage(headerImgData, 'JPEG', data.settings.margin.left, 17, 50, 50);
                    doc.text("Primary_Vs_Secondary.aspx", data.settings.margin.top, 70);
                };
                var options = {
                    beforePageContent: header,
                    margin: {
                        top: 80
                    },
                    startY: doc.autoTableEndPosY() + 20
                };
                doc.autoTable(res.columns, res.data, options);
                doc.save("Primary_Vs_Secondary.pdf");
            }</script>

    </form>
</body>
</html>
