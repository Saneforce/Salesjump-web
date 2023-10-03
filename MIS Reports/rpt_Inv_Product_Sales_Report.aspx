<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_Inv_Product_Sales_Report.aspx.cs"
    EnableEventValidation="false" Inherits="MIS_Reports_rpt_Inv_Product_Sales_Report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>INVOICE PRODUCT WISE SALES REPORT</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">  
function RefreshParent() {
           //  window.opener.document.getElementById('form1').click();
             window.close();
         }
   $(document).ready(function () {
            var k = 3;
            k = 0;


            $('#<%=GV_DATA.ClientID%> tr:nth-child(3)').find(' td').each(function () {
                //alert($(this).closest('table').find('th').eq($(this).index()).text());
                //  alert($(this).index());
                // $(tds[2]).hide();
                //if (k > 2) {
                //if ($(tds[1]).text() != "TOTAL") {
                //  $(tds[0]).text(k);

                //if ($(tds[2]).text() != "0") {
                if ($(this).index() > 1) {
                    //$($(this)).html('<a class="AAA" href="rpt_Customer_sales_analysis_days.aspx?&SF_Code=' + $('#<%=Prod.ClientID%>').val() + '&FYear=' + $('#<%=lblyear.ClientID%>').text() + '&SF_Name=' + $('#<%=Prod.ClientID%>').text() + '&FMonth=' + ($(this).index() - 1) + '&MonthNa=' + $(this).closest('table').find('th').eq($(this).index()).text() + '">' + $(this).text() + '</a>')
                }
                // $(this).html('<a href=Rpt_DCR_View.aspx?&sf_code=' + $(this).closest('tr').find('td:eq(2)').text() + '&Sf_Name=' + "" + '&cur_month=' + cmonth + '&cur_year=' + cyear + '&Mode=' + "SKU Summary" + '&FDate=' + "2017-12-01" +  '>' + $(this).html() + '</a>')

                //}
                //}
                //}

                k++;
            });


            $(".AAA").click(function () {
                //alert($(this).attr('href'));

                event.preventDefault();
                window.open($(this).attr("href"), "popupWindow", "width=600,height=600,scrollbars=yes");

            });
        });        
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
                    <asp:Label ID="lblHead" Text="Purchase Register-Distributor Wise" SkinID="lblMand" Font-Bold="true"  Font-Underline="true"
                runat="server"/>
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
                                    <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClick="btnExcel_Click" />
                                </td>
<td> <asp:Button ID="btnExport" runat="server" Text="PDF"  Font-Names="Verdana" Font-Size="10px"  BorderWidth="1" Height="25px" Width="60px"
                                        BorderColor="Black" BorderStyle="Solid" onclick="btnExport_Click" /></td>
                               
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
<asp:Panel ID="pnlContents" runat="server" Width="100%">
    <center>
        <br />
        <div>
           INVOICE PRODUCT WISE SALES REPORT<b>
                <asp:Label ID="lblyear" runat="server"></asp:Label></b></div>
        <div style="text-align: left; padding: 2px 50px;">
            <b>
              <asp:Label ID="ff" runat="server" align="left" style="font-size: small; font-weight: bold;font-family: Andalus;">PRODUCT NAME :</asp:Label>
                   <asp:Label ID="Prod" runat="server" Text="" ForeColor="Red"></asp:Label>
            </b>
        </div>
        <div>
        </div>
        <div>
            <asp:GridView ID="GV_DATA" runat="server" Width="95%" HorizontalAlign="Center" BorderWidth="1" EmptyDataText="No Data found for View"
                GridLines="Both" CssClass="mGrid" AlternatingRowStyle-CssClass="alt" OnRowDataBound="Dgv_SKU_RowDataBound"
                ItemStyle-HorizontalAlign="Right">
                <RowStyle HorizontalAlign="Right" />
                 <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
            </asp:GridView>
        </div>
    </center>
    </asp:Panel>
    </form>
</body>
</html>
