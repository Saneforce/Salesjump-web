<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_order_booking_analysis.aspx.cs" Inherits="MIS_Reports_rpt_order_booking_analysis" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Order Booking Analysis</title>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <script src="http://canvasjs.com/assets/script/canvasjs.min.js"></script>

    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
     <script language="Javascript">
         function RefreshParent() {
             // window.opener.document.getElementById('form1').click();
             window.close();
         }
    </script>
  <style type="text/css">
     
      
        body
        {
            padding: 10px;
        }
        .mGrid td, .mGrid th
        {
            padding: 2px 8px;
           
        }
           .rptCellBorder
        {
            border: 1px solid;
            border-color :#999999;
        }
       #GridView1 th
        {
             min-width:20px;
            width:20px;
            color:White;
            background-color:#39435c;
             padding: 2px 8px;
             font-size:15px;
        }
        #GridView1 td
        {
             padding: 8px 8px;
              font-size: 12px;
        }
       #GridView1 tr td:nth-child(1)
        {
              min-width:300px;
            width:300px;
            text-align:left;
        }
       <%-- #GridView1 tr td:nth-child(2),#GridView1 tr th:nth-child(2)
        {
            text-align:left;
             display : none;
        }--%>
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <asp:Panel ID="pnlbutton" runat="server">
<asp:Label id="lbltot" runat="server"/>
            <table width="100%">
			<tr>&nbsp</tr>
                <tr>
              <td width="60%" align="center" >
                    <asp:Label ID="lblHead"  SkinID="lblMand" style="font-weight:bold;FONT-SIZE: 24pt;COLOR: black;FONT-FAMILY: fantasy;float: left;padding: 5px;"
                runat="server"></asp:Label>
                    </td>
                    <td width="40%" align="right">
                        <table>
                            <tr>
                                <td>
                                 <asp:LinkButton ID="btnPrint" runat="Server" style="padding: 0px 20px;" class="btn btnPrint" OnClick="btnPrint_Click"/>
       <asp:LinkButton ID="btnExport"  runat="Server" style="padding: 0px 20px;" class="btn btnPdf"  OnClick="btnExport_Click" />
       <asp:LinkButton ID="btnExcel" runat="Server" style="padding: 0px 20px;" class="btn btnExcel" OnClick="btnExcel_Click"/>
       	<a name="btnClose" id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();" class="btn btnClose"></a>
                   </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>

       <asp:Panel ID="pnlContents" runat="server" Width="100%">
       
        <div>
                <table width="100%" align="center">
                    <tr>
                                    
                       
                        <td align="left" style="vertical-align: bottom;">
                            <asp:Label ID="lblIDsf_name" Text="Team:" Font-Bold="true"  Font-Underline="true" ForeColor="#476eec" runat="server" ></asp:Label>
                            <asp:Label ID="lblsf_name" runat="server"   Font-Underline="true" SkinID="lblMand"></asp:Label>
                        </td>
                       
                        <td align="right">
                 
                <asp:Image ID="logoo" runat="server" style="width: 28%;border-width:0px;height: 39px;"></asp:Image>
         
            </td>
                    </tr>
                </table>
          
         
        
            <table width="100%" align="center" >
                <tbody>
                    <tr>
                        <td align="center">
                        <asp:GridView ID="gvtotalorder" runat="server" Width="90%"    OnPreRender="gridView_PreRender" class="newStly"
                                HorizontalAlign="Center"  ShowFooter="true"
                                BorderWidth="1px" CellPadding="2" EmptyDataText="No Data found for View" 
                                AutoGenerateColumns="true" 
                                HeaderStyle-HorizontalAlign="Center" BorderColor="Black" 
                                BorderStyle="Solid" onrowdatabound="gvtotalorder_RowDataBound"  >                               
                                <Columns>
                            
                                   
                                </Columns>
                                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />

                            </asp:GridView>
                           <%-- <asp:Table ID="tbl"  runat="server" Style="border-collapse: collapse;  border: solid 1px Black;
                                 font-family: Calibri" Font-Size="8pt" GridLines="Both" Width="100%">
                            </asp:Table>--%>
                        </td>

                    </tr>
                 
                </tbody>
            </table>  
            <table width="100%" align="center" >
                <tbody>
                    <tr>
                        <td align="center">
                        <asp:GridView ID="GridView1" runat="server" Width="90%"   
                                OnPreRender="gridView_PreRender"  class="newStly"
                                HorizontalAlign="Center"  ShowFooter="true"
                                BorderWidth="1px" CellPadding="2" EmptyDataText="No Data found for View" 
                                AutoGenerateColumns="true" 
                                HeaderStyle-HorizontalAlign="Center" BorderColor="Black" 
                                BorderStyle="Solid" onrowdatabound="GridView1_RowDataBound" ItemStyle-HorizontalAlign="Right">                               
                                <Columns>
                            
                                   
                                </Columns>
                                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                                     <RowStyle HorizontalAlign="Right" />
                            </asp:GridView>
                           <%-- <asp:Table ID="tbl"  runat="server" Style="border-collapse: collapse;  border: solid 1px Black;
                                 font-family: Calibri" Font-Size="8pt" GridLines="Both" Width="100%">
                            </asp:Table>--%>
                        </td>

                    </tr>
                 
                </tbody>
            </table>  
            </div>      
    </asp:Panel>
    </form>
  
</body>
</html>
