<%@ Page Title="Order Booking Analysis" Language="C#" AutoEventWireup="true" EnableEventValidation="false"
    CodeFile="Order_Booking_Analysis_month.aspx.cs" Inherits="MasterFiles_Order_Booking_Analysis_month" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">


        function RefreshParent() {
            // window.opener.document.getElementById('form1').click();
            window.close();
        }
        $(document).ready(function () {
            var k = 0;

            $('#<%=GV_DATA.ClientID%> tr:nth-child(1)').find('th').each(function () {

                if ($(this).index() > 0 && $(this).index() < 13) {
                    // alert($(this).text());
                    $($(this)).html('<a class="AAA" href="rpt_order_booking_analysis.aspx?&sfCode=' + $('#<%=hidn_sf_code.ClientID%>').val() + '&FYear=' + $('#<%=lblyear.ClientID%>').val() + '&sfname=' + $('#<%=lblsf_name.ClientID%>').text() + '&imgpath=' + $('#<%=imgpath.ClientID%>').val() + '&FMonth=' + $(this).index() + '">' + $(this).text() + '</a>')
                    //window.open("rpt_order_booking_analysis.aspx?&FMonth=" + FMonth + "&FYear=" + FYear + "&sfCode=" + sfcode + "&sfname=" + FO + "&imgpath=" + imgpth, null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
                }
                k++;
            });



//            $('#<%=GV_DATA.ClientID%> tr:last').find('td').each(function () {
//                if ($(this).index() > 0 && $(this).index() < 13) {
//                    $($(this)).html('<a class="AAA" href="Order_Booking_Analysis_Product_Wise.aspx?&sfCode=' + $('#<%=hidn_sf_code.ClientID%>').val() + '&FYear=' + $('#<%=lblyear.ClientID%>').val() + '&sfname=' + $('#<%=lblsf_name.ClientID%>').text() + '&imgpath=' + $('#<%=imgpath.ClientID%>').val() + '&FMonth=' + $(this).index() + '">' + $(this).text() + '</a>')
//                }
//            });

            $(".AAA").click(function () {
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
        .rptCellBorder
        {
            border: 1px solid;
            border-color: #999999;
        }
        
        .remove
        {
            text-decoration: none;
        }
        
        
        #GV_DATA tr td:nth-child(14)
        {
            background-color: #D0ECE7;
        }
        
        th a
        {
            color: White;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="pnlbutton" runat="server">
        <table width="100%">
            <tr>
                <td width="60%" align="center">
                    <asp:Label ID="lblHead" Text="Purchase Register-Distributor Wise" SkinID="lblMand"
                        Font-Bold="true" Font-Underline="true" runat="server" />
                </td>
                <td width="40%" align="right">
                    <asp:LinkButton ID="btnPrint" runat="Server" Style="padding: 0px 20px;" class="btn btnPrint"
                        OnClick="btnPrint_Click" />
                    <asp:LinkButton ID="btnExcel" runat="Server" Style="padding: 0px 20px;" class="btn btnExcel"
                        OnClick="btnExcel_Click" />
                    <asp:LinkButton ID="btnExport" runat="Server" Style="padding: 0px 20px;" class="btn btnPdf"
                        OnClick="btnExport_Click" />
                    <asp:LinkButton ID="LinkButton1" runat="Server" Style="padding: 0px 20px;" class="btn btnClose"
                        OnClientClick="javascript:window.open('','_self').close();" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlContents" runat="server" Width="100%">
        <table width="100%" align="center">
            <tr>
                <td align="left" style="vertical-align: bottom;">
                    <asp:Label ID="lblIDsf_name" Text="Team:" Font-Bold="true" Font-Underline="true"
                        ForeColor="#476eec" runat="server"></asp:Label>
                    <asp:Label ID="lblsf_name" runat="server" Font-Underline="true" SkinID="lblMand"></asp:Label>
                </td>
                <td align="right">
                    <asp:Image ID="logoo" runat="server" Style="width: 28%; border-width: 0px; height: 39px;">
                    </asp:Image>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hidn_sf_code" runat="server" />
        <asp:HiddenField ID="imgpath" runat="server" />
        <asp:HiddenField ID="lblyear" runat="server" />
        <asp:GridView ID="GV_DATA" runat="server" Width="100%" HorizontalAlign="Center" BorderWidth="1"
            class="newStly" GridLines="Both" OnRowDataBound="Dgv_SKU_RowDataBound" ItemStyle-HorizontalAlign="Right">
        </asp:GridView>
    </asp:Panel>
    </form>
</body>
</html>
