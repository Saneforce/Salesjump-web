<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_Uotlet_Placement_Achived.aspx.cs" Inherits="MasterFiles_rpt_Uotlet_Placement_Achived" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src='https://cdn.rawgit.com/simonbengtsson/jsPDF/requirejs-fix-dist/dist/jspdf.debug.js'></script>
    <script src='https://unpkg.com/jspdf-autotable@2.3.2'></script>
    <link href="../css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="../css/style1.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
     <script src="https://cdn.jsdelivr.net/gh/linways/table-to-excel@v1.0.4/dist/tableToExcel.js"></script>
    <title></title>
     <script type="text/javascript">
        $(document).ready(function () {

        });
        function ExportToExcel() {
            var table = document.getElementById("gridtbl");
            TableToExcel.convert(table, {
                name: `Outlet Placement.xlsx`,
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
    </style>
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
       
                        <asp:GridView ID="gvtotalorder" runat="server" Width="100%" 
                                HorizontalAlign="Center" BorderWidth="1px" CellPadding="2" CellSpacing="2"
                                 EmptyDataText="No Data found for View" HeaderStyle-BackColor="#819dfb"
                                AutoGenerateColumns="true" CssClass="ttable"
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
